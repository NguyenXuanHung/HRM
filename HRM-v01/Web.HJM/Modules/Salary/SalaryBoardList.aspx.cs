using Ext.Net;
using System;
using System.Linq;
using Web.Core.Framework;
using Web.Core.Framework.Controller;
using Web.Core.Framework.Controller.TimeSheet;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.Catalog;
using Web.Core.Service.HumanRecord;
using Web.Core.Service.Salary;

namespace Web.HJM.Modules.Salary
{
    public partial class SalaryBoardList : BasePage
    {
        private const string ConstPaySalary = "HuongLuong";
        private const string ConstFullDay = @"Một ngày công";
        private const string TypeTimeSheet = "Automatic";
        private const string ConstOverTime = "ThemGio";
        private const string ConstOverTimeDay = "TangCaNgay";
        private const string ConstOverTimeNight = "TangCaDem";
        private const string ConstOverTimeHoliday = "TangCaNgayLe";
        private const string ConstOverTimeWeekend = "TangCaNgayNghi";
        private const string ConstLeave = "NghiPhep";

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!ExtNet.IsAjaxRequest)
            {
                // init department
                storeDepartment.DataSource = CurrentUser.DepartmentsTree;
                storeDepartment.DataBind();

                hdfDepartments.Text = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
                hdfMonth.Text = DateTime.Now.Month.ToString();
                hdfYear.Text = DateTime.Now.Year.ToString();
                cbxMonth.SetValue(DateTime.Now.Month);
                spnYear.SetValue(DateTime.Now.Year);
            }
        }

        /// <summary>
        /// Init setting window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InitWindow(object sender, DirectEventArgs e)
        {
            var param = e.ExtraParams["Id"];
            if(int.TryParse(param, out var id))
            {
                if(id > 0)
                {
                    // edit
                    wdSetting.Title = @"Sửa bảng lương";
                    wdSetting.Icon = Icon.Pencil;
                }
                else
                {
                    // insert
                    wdSetting.Title = @"Thêm mới bảng lương";
                    wdSetting.Icon = Icon.Add;
                }
                hdfSalaryBoardListId.Text = id.ToString();
                var salaryBoardList = new hr_SalaryBoardList();
                if(id > 0)
                {
                    var result = hr_SalaryBoardListServices.GetById(Convert.ToInt32(hdfSalaryBoardListId.Text));
                    if(result != null)
                        salaryBoardList = result;
                    hdfMonth.Text = salaryBoardList.Month.ToString();
                    cbxMonth.Text = salaryBoardList.Month.ToString();
                    hdfYear.Text = salaryBoardList.Year.ToString();
                    spnYear.Text = salaryBoardList.Year.ToString();
                }
                
                txtTitleSalaryBoard.Text = salaryBoardList.Title;
                txtCode.Text = salaryBoardList.Code;
                txtDesciptionSalaryBoard.Text = salaryBoardList.Description;
                hdfConfigId.Text = salaryBoardList.ConfigId.ToString();
                cbxConfigList.Text = cat_PayrollConfigServices.GetFieldValueById(salaryBoardList.ConfigId, "Name");
                hdfDepartmentId.Text = salaryBoardList.DepartmentId.ToString();
                cbxDepartment.Text = cat_DepartmentServices.GetFieldValueById(salaryBoardList.DepartmentId, "Name");
                // show window
                wdSetting.Show();
            }
        }

        /// <summary>
        /// Row double click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rowDbl_Click(object sender, DirectEventArgs e)
        {
            if(!string.IsNullOrEmpty(hdfSalaryBoardListId.Text))
            {
                Response.Redirect("~/Modules/Salary/SalaryBoardInfo.aspx?id=" + hdfSalaryBoardListId.Text);
            }
        }

        [DirectMethod]
        public void SelectSalaryBoard()
        {
            if(!string.IsNullOrEmpty(hdfSalaryBoardListId.Text))
            {
                //Lay danh sach bang luong
                var boardList = hr_SalaryBoardListServices.GetById(Convert.ToInt32(hdfSalaryBoardListId.Text));
                if(boardList != null)
                {
                    //TH chua ton tai bang luong thi di tao moi bang luong
                    var salaryBoard = SalaryBoardInfoController.GetAllBoardInfo(hdfSalaryBoardListId.Text);
                    if(salaryBoard != null && salaryBoard.Count <= 0)
                    {
                        //Lay danh sach nhan vien cua don vi can tinh bang luong
                        var listRecordId = TimeSheetCodeController.GetAll(null, null, boardList.DepartmentId, null,
                            null, true, null, null, null, null);
                        foreach (var item in listRecordId)
                        {
                            //Tao moi bang tinh luong
                            var salaryInfo = new hr_SalaryBoardInfo()
                            {
                                SalaryBoardId = boardList.Id,
                                RecordId = item.RecordId,
                                CreatedDate = DateTime.Now,
                            };
                            //Lay tu timeSheet
                            GetDataFromTimeSheet(boardList, salaryInfo);

                            //Lay tu sal_SalaryDecision
                            GetDataFromSalary(item, salaryInfo);
                            hr_SalaryBoardInfoServices.Create(salaryInfo);
                        }
                    }
                    else
                    {
                        if(chk_IsUpdateTimeSheet.Checked)
                        {
                            //Truong hop da co bang luong, thi cap nhat lai du lieu tu cham cong cho moi recordId
                            foreach(var salary in salaryBoard)
                            {
                                var salaryInfo = hr_SalaryBoardInfoServices.GetById(salary.Id);

                                //Lay tu timeSheet
                                GetDataFromTimeSheet(boardList, salaryInfo);
                                //Update
                                hr_SalaryBoardInfoServices.Update(salaryInfo);
                            }
                        }

                        if(chk_IsUpdateSalary.Checked)
                        {
                            //Truong hop da co bang luong, thi cap nhat lai du lieu tu luong cho moi recordId
                            foreach(var salary in salaryBoard)
                            {
                                var salaryInfo = hr_SalaryBoardInfoServices.GetById(salary.Id);

                                //Lay tu sal_SalaryDecision
                                EditDataSalary(salary.RecordId, salaryInfo);

                                //Update
                                hr_SalaryBoardInfoServices.Update(salaryInfo);
                            }
                        }
                    }
                }
                Response.Redirect("~/Modules/Salary/SalaryBoardInfo.aspx?id=" + hdfSalaryBoardListId.Text);
            }
        }

        [DirectMethod]
        public void SelectColumnDynamic()
        {
            if(!string.IsNullOrEmpty(hdfSalaryBoardListId.Text))
            {
                Response.Redirect("~/Modules/Salary/SalaryColumnDynamic.aspx?id=" + hdfSalaryBoardListId.Text);
            }
        }

        /// <summary>
        /// Insert or Update Catalog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InsertOrUpdate(object sender, DirectEventArgs e)
        {
            try
            {
                var salaryBoardList = new hr_SalaryBoardList();
                // check id
                if(!string.IsNullOrEmpty(hdfSalaryBoardListId.Text) && Convert.ToInt32(hdfSalaryBoardListId.Text) > 0)
                {
                    var result = hr_SalaryBoardListServices.GetById(Convert.ToInt32(hdfSalaryBoardListId.Text));
                    if(result != null)
                        salaryBoardList = result;
                }
                // set new props
                if(!string.IsNullOrEmpty(hdfConfigId.Text))
                    salaryBoardList.ConfigId = Convert.ToInt32(hdfConfigId.Text);
                if(!string.IsNullOrEmpty(hdfDepartmentId.Text))
                    salaryBoardList.DepartmentId = Convert.ToInt32(hdfDepartmentId.Text);
                if(!string.IsNullOrEmpty(hdfMonth.Text))
                    salaryBoardList.Month = Convert.ToInt32(hdfMonth.Text);
                if(!string.IsNullOrEmpty(hdfYear.Text))
                    salaryBoardList.Year = Convert.ToInt32(hdfYear.Text);
                salaryBoardList.Title = txtTitleSalaryBoard.Text;
                salaryBoardList.Code = txtCode.Text;
                salaryBoardList.Description = txtDesciptionSalaryBoard.Text;
                if(salaryBoardList.Id > 0)
                {
                    salaryBoardList.EditedDate = DateTime.Now;
                    salaryBoardList.EditedBy = CurrentUser.User.UserName;
                    hr_SalaryBoardListServices.Update(salaryBoardList);
                }
                else
                {
                    salaryBoardList.CreatedDate = DateTime.Now;
                    salaryBoardList.CreatedBy = CurrentUser.User.UserName;
                    hr_SalaryBoardListServices.Create(salaryBoardList);
                }
                wdSetting.Hide();
                grdSalaryBoardList.Reload();
                Dialog.ShowNotification("Lưu thành công");
            }
            catch(Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }

        }

        /// <summary>
        /// Delete catalog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Delete(object sender, DirectEventArgs e)
        {
            try
            {
                var param = e.ExtraParams["Id"];
                // parse id
                if(!int.TryParse(param, out var id) || id <= 0)
                {
                    // parse error, show error
                    Dialog.ShowError("Có lỗi xảy ra trong quá trình xử lý");
                    return;
                }
                // delete
                //Xoa bang luong 
                hr_SalaryBoardInfoServices.DeleteByBoardListId(id);
                //Xoa trong danh sach bang luong
                hr_SalaryBoardListServices.Delete(id);

                // reload data
                grdSalaryBoardList.Reload();
            }
            catch(Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// Lay du lieu tu cham cong
        /// </summary>
        /// <param name="boardList"></param>
        /// <param name="salaryInfo"></param>
        private void GetDataFromTimeSheet(hr_SalaryBoardList boardList, hr_SalaryBoardInfo salaryInfo)
        {
            ////Lay ID bang cham cong 
            //var timeSheetReport = hr_TimeSheetReportServices.GetTimeSheetReport(boardList.DepartmentId,
            //    boardList.Month, boardList.Year, TimeSheetHandlerType);
            //if(timeSheetReport != null)
            //{
            //    salaryInfo.WorkStandardDay = timeSheetReport.WorkInMonth;
            //    //Lay tong cong thuc te
            //    var timeSheetList = hr_TimeSheetServices.GetByTimeSheetReportIdAndRecordId(salaryInfo.RecordId, timeSheetReport.Id);
            //    double? totalWork = 0.0;
            //    double? totalNormal = 0.0;
            //    double? totalWorkLeaveDay = 0.0;
            //    double? totalOverTime = 0.0;
            //    double? totalOverTimeDay = 0.0;
            //    double? totalOverTimeNight = 0.0;
            //    double? totalOverTimeHoliday = 0.0;
            //    double? totalOverTimeWeekend = 0.0;

            //    foreach(var itemTimeSheet in timeSheetList)
            //    {
            //        var timeSheetEvent = JSON.Deserialize<List<TimeSheetItemModel>>(itemTimeSheet.Detail);
            //        if(timeSheetEvent.Count > 0)
            //        {
            //            var symbol = hr_TimeSheetSymbolServices.GetSymbolFullDay(ConstPaySalary, null);
            //            if(symbol != null)
            //            {
            //                var listTimeFullDay =
            //                    timeSheetEvent.Where(d => d.TypeGroup == ConstPaySalary && d.Symbol == symbol.Code);
            //                foreach(var itemNormal in listTimeFullDay)
            //                {
            //                    totalNormal += itemNormal.WorkConvert;
            //                }
            //            }

            //            var listOverTime =
            //                timeSheetEvent.Where(d => d.TypeGroup == ConstOverTime);
            //            var listOverTimeDay =
            //                timeSheetEvent.Where(d => d.TypeGroup == ConstOverTimeDay);
            //            var listOverTimeNight =
            //                timeSheetEvent.Where(d => d.TypeGroup == ConstOverTimeNight);
            //            var listOverTimeHoliday =
            //                timeSheetEvent.Where(d => d.TypeGroup == ConstOverTimeHoliday);
            //            var listOverTimeWeekend =
            //                timeSheetEvent.Where(d => d.TypeGroup == ConstOverTimeWeekend);
            //            var listWorkLeaveDay =
            //                timeSheetEvent.Where(d => d.TypeGroup == ConstLeave);

            //            foreach(var itemEvent in timeSheetEvent)
            //            {
            //                totalWork += itemEvent.WorkConvert;
            //            }

            //            foreach(var item in listWorkLeaveDay)
            //            {
            //                if(item.WorkConvert != null)
            //                {
            //                    totalWorkLeaveDay += item.WorkConvert;
            //                }
            //            }

            //            foreach(var item in listOverTime)
            //            {
            //                if(item.TimeConvert != null)
            //                {
            //                    totalOverTime += item.TimeConvert;
            //                }
            //            }

            //            foreach(var item in listOverTimeDay)
            //            {
            //                if(item.TimeConvert != null)
            //                {
            //                    totalOverTimeDay += item.TimeConvert;
            //                }
            //            }

            //            foreach(var item in listOverTimeNight)
            //            {
            //                if(item.TimeConvert != null)
            //                {
            //                    totalOverTimeNight += item.TimeConvert;
            //                }
            //            }

            //            foreach(var item in listOverTimeHoliday)
            //            {
            //                if(item.TimeConvert != null)
            //                {
            //                    totalOverTimeHoliday += item.TimeConvert;
            //                }
            //            }

            //            foreach(var item in listOverTimeWeekend)
            //            {
            //                if(item.TimeConvert != null)
            //                {
            //                    totalOverTimeWeekend += item.TimeConvert;
            //                }
            //            }

            //        }
            //    }

            //    salaryInfo.WorkActualDay = Math.Round(totalWork.Value, 2);
            //    salaryInfo.WorkNormalDay = Math.Round(totalNormal.Value, 2);
            //    salaryInfo.WorkPaidLeave = Math.Round(totalWorkLeaveDay.Value, 2);
            //    salaryInfo.OverTime = Math.Round(totalOverTime.Value, 2);
            //    salaryInfo.OverTimeDay = Math.Round(totalOverTimeDay.Value, 2);
            //    salaryInfo.OverTimeNight = Math.Round(totalOverTimeNight.Value, 2);
            //    salaryInfo.OverTimeHoliday = Math.Round(totalOverTimeHoliday.Value, 2);
            //    salaryInfo.OverTimeWeekend = Math.Round(totalOverTimeWeekend.Value, 2);
            //}
        }

        /// <summary>
        /// Sửa salary info
        /// </summary>
        /// <param name="recordId"></param>
        /// <param name="salaryInfo"></param>
        private static void EditDataSalary(int recordId, hr_SalaryBoardInfo salaryInfo)
        {
            var salary = sal_SalaryDecisionServices.GetSalaryByRecordId(recordId);
            if(salary != null)
            {
                salaryInfo.SalaryFactor = (double) salary.Factor;
                salaryInfo.SalaryBasic = (double) CatalogBasicSalaryController.GetCurrent().Value;
                salaryInfo.SalaryNet = (double) salary.NetSalary;
                salaryInfo.SalaryGross = (double) salary.GrossSalary;
                salaryInfo.SalaryContract = (double) salary.ContractSalary;
                salaryInfo.SalaryInsurance = (double) salary.InsuranceSalary;
            }
        }

        /// <summary>
        /// lay du lieu luong co dinh
        /// </summary>
        /// <param name="item"></param>
        /// <param name="salaryInfo"></param>
        private static void GetDataFromSalary(TimeSheetCodeModel item, hr_SalaryBoardInfo salaryInfo)
        {
            //Edit data salary
            EditDataSalary(item.RecordId, salaryInfo);
        }
    }
}