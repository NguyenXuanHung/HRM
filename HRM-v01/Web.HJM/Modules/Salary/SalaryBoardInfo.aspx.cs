using Ext.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Web.Core.Framework;
using Web.Core.Framework.Common;
using Web.Core.Framework.Controller;
using Web.Core.Framework.Controller.TimeSheet;
using Web.Core.Object.HumanRecord;
using Web.Core.Object.Salary;
using  Web.Core.Object.TimeSheet;
using Web.Core.Service.Catalog;
using Web.Core.Service.HumanRecord;
using Web.Core.Service.Salary;
using Web.Core.Service.TimeSheet;

namespace Web.HJM.Modules.Salary
{
    public partial class SalaryBoardInfo : BasePage
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

        protected int SalaryBoardListId;

        protected void Page_Load(object sender, EventArgs e)
        {
            hdfType.Text = Request.QueryString["TimeSheetHandlerType"];

            SalaryBoardListId = Request.QueryString["id"] != null ? Convert.ToInt32(Request.QueryString["id"]) : 0;

            if (!ExtNet.IsAjaxRequest)
            {
                //init
                InitController();
            }

        }

        private void InitController()
        {
            // init department
            storeDepartment.DataSource = CurrentUser.DepartmentsTree;
            storeDepartment.DataBind();

            hdfDepartments.Text = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
            hdfMonth.Text = DateTime.Now.Month.ToString();
            hdfYear.Text = DateTime.Now.Year.ToString();
            cbxMonth.SetValue(DateTime.Now.Month);
            spnYear.SetValue(DateTime.Now.Year);
            if (Request.QueryString["id"] == null)
            {
                wdSalaryBoardManage.Show();
            }
            else
            {
                //set configId
                var salaryBoard = sal_PayrollServices.GetById(SalaryBoardListId);
                if(salaryBoard != null)
                {
                    gridSalaryInfo.Title = salaryBoard.Title;
                }
            }
        }

        protected void CreateSalaryBoardList_Click(object sender, DirectEventArgs e)
        {
            var salaryBoard = new sal_Payroll()
            {
                CreatedDate = DateTime.Now,
                CreatedBy = CurrentUser.User.UserName
            };
            //edit data
            EditDataSalaryBoardList(salaryBoard);

            sal_PayrollServices.Create(salaryBoard);
            ResetFormSalaryList();
            grpSalaryBoardList.Reload();
        }

        [DirectMethod]
        public void ResetFormSalaryList()
        {
            cbxDepartment.Text = "";
            hdfDepartmentId.Reset();
            txtTitleSalaryBoard.Text = "";
            txtCode.Reset();
            hdfConfigId.Reset();
            cbxConfigList.Reset();
            txtDesciptionSalaryBoard.Text = "";
            hdfMonth.Text = DateTime.Now.Month.ToString();
            cbxMonth.Text = DateTime.Now.Month.ToString();
            hdfYear.Text = DateTime.Now.Year.ToString();
            spnYear.Text = DateTime.Now.Year.ToString();
        }

        protected void UpdateSalaryBoardList_Click(object sender, DirectEventArgs e)
        {
            if (!string.IsNullOrEmpty(hdfSalaryBoardListID.Text))
            {
                var salaryBoard = sal_PayrollServices.GetById(Convert.ToInt32(hdfSalaryBoardListID.Text));
                if (salaryBoard != null)
                {
                    //edit data
                    EditDataSalaryBoardList(salaryBoard);
                    salaryBoard.EditedDate = DateTime.Now;

                    sal_PayrollServices.Update(salaryBoard);
                    //reset form
                    ResetFormSalaryList();
                    grpSalaryBoardList.Reload();
                }
            }
        }

        private void EditDataSalaryBoardList(sal_Payroll salaryBoard)
        {
            if (!string.IsNullOrEmpty(hdfConfigId.Text))
                salaryBoard.ConfigId = Convert.ToInt32(hdfConfigId.Text);
            //if (!string.IsNullOrEmpty(hdfDepartmentId.Text))
            //    salaryBoard.DepartmentId = Convert.ToInt32(hdfDepartmentId.Text);
            if (!string.IsNullOrEmpty(hdfMonth.Text))
                salaryBoard.Month = Convert.ToInt32(hdfMonth.Text);
            if (!string.IsNullOrEmpty(hdfYear.Text))
                salaryBoard.Year = Convert.ToInt32(hdfYear.Text);
            salaryBoard.Title = txtTitleSalaryBoard.Text;
            salaryBoard.Code = txtCode.Text;
            salaryBoard.Description = txtDesciptionSalaryBoard.Text;
        }

        protected void btnDeleteSalaryBoardList_Click(object sender, DirectEventArgs e)
        {
            if (!string.IsNullOrEmpty(hdfSalaryBoardListID.Text))
            {
                //Xoa bang luong 
                //hr_SalaryBoardInfoServices.DeleteByBoardListId(Convert.ToInt32(hdfSalaryBoardListID.Text));
                //Xoa trong danh sach bang luong
                sal_PayrollServices.Delete(Convert.ToInt32(hdfSalaryBoardListID.Text));
                grpSalaryBoardList.Reload();
            }
        }

        [DirectMethod]
        public void btnEditSalaryBoardList_Click()
        {
            if (!string.IsNullOrEmpty(hdfSalaryBoardListID.Text))
            {
                var salary = sal_PayrollServices.GetById(Convert.ToInt32(hdfSalaryBoardListID.Text));
                if (salary != null)
                {
                    txtTitleSalaryBoard.Text = salary.Title;
                    txtCode.Text = salary.Code;
                    txtDesciptionSalaryBoard.Text = salary.Description;
                    hdfConfigId.Text = salary.ConfigId.ToString();
                    cbxConfigList.Text = sal_PayrollConfigServices.GetFieldValueById(salary.ConfigId, "Name");
                    hdfMonth.Text = salary.Month.ToString();
                    cbxMonth.Text = salary.Month.ToString();
                    hdfYear.Text = salary.Year.ToString();
                    spnYear.Text = salary.Year.ToString();
                    //hdfDepartmentId.Text = salary.DepartmentId.ToString();
                    //cbxDepartment.Text = cat_DepartmentServices.GetFieldValueById(salary.DepartmentId, "Name");
                }
                wdCreateSalaryBoardList.SetTitle("Cập nhật bảng lương");
                btnUpdateEditSalaryBoard.Show();
                btnCreateSalaryBoard.Hide();
                wdCreateSalaryBoardList.Show();
            }
        }

        [DirectMethod]
        public void btnAddSalaryBoardList_Click()
        {
            wdCreateSalaryBoardList.SetTitle("Tạo bảng lương");
            btnUpdateEditSalaryBoard.Hide();
            btnCreateSalaryBoard.Show();
            wdCreateSalaryBoardList.Show();
        }
        /// <summary>
        /// Tạo bảng tính lương
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ChooseSalaryBoardList_Click(Object sender, DirectEventArgs e)
        {
            if (!string.IsNullOrEmpty(hdfSalaryBoardListID.Text))
            {
                hdfSalaryBoardId.Text = hdfSalaryBoardListID.Text;
                //Lay danh sach bang luong
                var boardList = sal_PayrollServices.GetById(Convert.ToInt32(hdfSalaryBoardListID.Text));
                if (boardList != null)
                {
                    //TH chua ton tai bang luong thi di tao moi bang luong
                    var salaryBoard = SalaryBoardInfoController.GetAllBoardInfo(hdfSalaryBoardListID.Text);
                    if (salaryBoard != null && salaryBoard.Count <= 0)
                    {
                        //Lay danh sach nhan vien cua don vi can tinh bang luong
                        //var listRecordId = TimeSheetCodeController.GetAll(null, null, boardList.DepartmentId, null,
                        //    null, true, null, null, null, null);
                        //foreach (var item in listRecordId)
                        //{
                        //    //Tao moi bang tinh luong
                        //    var salaryInfo = new hr_SalaryBoardInfo()
                        //    {
                        //        SalaryBoardId = boardList.Id,
                        //        RecordId = item.RecordId,
                        //        CreatedDate = DateTime.Now,
                        //    };
                        //    //Lay tu timeSheet
                        //    GetDataFromTimeSheet(boardList, salaryInfo);

                        //    //Lay tu sal_SalaryDecision
                        //    GetDataFromSalary(item, salaryInfo);
                        //    hr_SalaryBoardInfoServices.Create(salaryInfo);
                        //}
                    }
                    else
                    {
                        if (chk_IsUpdateTimeSheet.Checked)
                        {
                            //Truong hop da co bang luong, thi cap nhat lai du lieu tu cham cong cho moi recordId
                            foreach (var salary in salaryBoard)
                            {
                                var salaryInfo = hr_SalaryBoardInfoServices.GetById(salary.Id);

                                //Lay tu timeSheet
                                GetDataFromTimeSheet(boardList, salaryInfo);
                                //Update
                                hr_SalaryBoardInfoServices.Update(salaryInfo);
                            }
                        }

                        if (chk_IsUpdateSalary.Checked)
                        {
                            //Truong hop da co bang luong, thi cap nhat lai du lieu tu luong cho moi recordId
                            foreach (var salary in salaryBoard)
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

                //Lay danh sach bang luong
                Response.Redirect("SalaryBoardInfo.aspx?id=" + hdfSalaryBoardListID.Text);
                //Reset
                chk_IsUpdateTimeSheet.Checked = false;
            }
        }

        /// <summary>
        /// Lay du lieu tu cham cong
        /// </summary>
        /// <param name="boardList"></param>
        /// <param name="salaryInfo"></param>
        private void GetDataFromTimeSheet(sal_Payroll boardList, hr_SalaryBoardInfo salaryInfo)
        {
            //Lay ID bang cham cong 
            //var timeSheetReport = hr_TimeSheetReportServices.GetTimeSheetReport(boardList.DepartmentId,
            //    boardList.Month, boardList.Year, TimeSheetHandlerType);
            //if (timeSheetReport != null)
            //{
            //    salaryInfo.WorkStandardDay = timeSheetReport.WorkInMonth;
            //    //Lay tong cong thuc te
            //    var timeSheetList = hr_TimeSheetServices.GetByTimeSheetReportIdAndRecordId(salaryInfo.RecordId, timeSheetReport.Id);
            //    double? totalWork = 0.0;
            //    double? totalNormal = 0.0;
            //    double? totalWorkPaidLeave = 0.0;
            //    double? totalOverTime = 0.0;
            //    double? totalOverTimeDay = 0.0;
            //    double? totalOverTimeNight = 0.0;
            //    double? totalOverTimeHoliday = 0.0;
            //    double? totalOverTimeWeekend = 0.0;
            //    double? totalGoWorkC = 0.0;
            //    double? totalHolidayL = 0.0;
            //    double? totalLateM = 0.0;
            //    double? totalUnleaveK = 0.0;
            //    double? totalUnpaidLeaveP = 0.0;

            //    foreach (var itemTimeSheet in timeSheetList)
            //    {
            //        var timeSheetEvent = JSON.Deserialize<List<TimeSheetItemModel>>(itemTimeSheet.Detail);
            //        if (timeSheetEvent.Count > 0)
            //        {
            //            var symbol = hr_TimeSheetSymbolServices.GetSymbolFullDay(Constant.TimesheetPaySalary, null);
            //            if (symbol != null)
            //            {
            //                var listTimeFullDay =
            //                    timeSheetEvent.Where(d => d.TypeGroup == Constant.TimesheetPaySalary && d.Symbol == symbol.Code);
            //                foreach (var itemNormal in listTimeFullDay)
            //                {
            //                    totalNormal += itemNormal.WorkConvert;
            //                }
            //            }

            //            // over time 
            //            var listOverTime =
            //                timeSheetEvent.Where(d => d.TypeGroup == Constant.TimesheetOverTime);
            //            // over time day
            //            var listOverTimeDay =
            //                timeSheetEvent.Where(d => d.TypeGroup == Constant.TimesheetOverTimeDay);
            //            // over time night
            //            var listOverTimeNight =
            //                timeSheetEvent.Where(d => d.TypeGroup == Constant.TimesheetOverTimeNight);
            //            // over time holiday
            //            var listOverTimeHoliday =
            //                timeSheetEvent.Where(d => d.TypeGroup == Constant.TimesheetOverTimeHoliday);
            //            // over time weekend
            //            var listOverTimeWeekend =
            //                timeSheetEvent.Where(d => d.TypeGroup == Constant.TimesheetOverTimeWeekend);
            //            // paid leaveday
            //            var listWorkLeaveDay =
            //                timeSheetEvent.Where(d => d.TypeGroup == Constant.TimesheetLeave);
            //            // go work
            //            var listGoWork = timeSheetEvent.Where(d => d.TypeGroup == Constant.TimesheetGoWork);
            //            // holiday
            //            var listHoliday = timeSheetEvent.Where(d => d.TypeGroup == Constant.TimesheetHoliday);
            //            // late
            //            var listLate = timeSheetEvent.Where(d => d.TypeGroup == Constant.TimesheetLate);
            //            //unleave
            //            var listUnLeave = timeSheetEvent.Where(d => d.TypeGroup == Constant.TimesheetUnLeave);

            //            //UnpaidLeave
            //            var listUnpaidLeave = timeSheetEvent.Where(d => d.TypeGroup == Constant.TimesheetNotPaySalary);

            //            foreach (var itemEvent in timeSheetEvent)
            //            {
            //                totalWork += itemEvent.WorkConvert;
            //            }

            //            totalWorkPaidLeave = listWorkLeaveDay.Where(item => item.WorkConvert != null).Aggregate(totalWorkPaidLeave, (current, item) => current + item.WorkConvert);

            //            totalOverTime = listOverTime.Where(item => item.TimeConvert != null).Aggregate(totalOverTime, (current, item) => current + item.TimeConvert);

            //            totalOverTimeDay = listOverTimeDay.Where(item => item.TimeConvert != null).Aggregate(totalOverTimeDay, (current, item) => current + item.TimeConvert);

            //            totalOverTimeNight = listOverTimeNight.Where(item => item.TimeConvert != null).Aggregate(totalOverTimeNight, (current, item) => current + item.TimeConvert);

            //            totalOverTimeHoliday = listOverTimeHoliday.Where(item => item.TimeConvert != null).Aggregate(totalOverTimeHoliday, (current, item) => current + item.TimeConvert);

            //            totalOverTimeWeekend = listOverTimeWeekend.Where(item => item.TimeConvert != null).Aggregate(totalOverTimeWeekend, (current, item) => current + item.TimeConvert);

            //            totalGoWorkC = listGoWork.Where(item => item.WorkConvert != null).Aggregate(totalGoWorkC, (current, item) => current + item.WorkConvert);

            //            totalLateM = listLate.Where(item => item.WorkConvert != null).Aggregate(totalLateM, (current, item) => current + item.WorkConvert);

            //            totalUnleaveK = listUnLeave.Where(item => item.WorkConvert != null).Aggregate(totalUnleaveK, (current, item) => current + item.WorkConvert);

            //            totalHolidayL = listHoliday.Where(item => item.WorkConvert != null).Aggregate(totalHolidayL, (current, item) => current + item.WorkConvert);

            //            totalUnpaidLeaveP = listUnpaidLeave.Where(item => item.WorkConvert != null).Aggregate(totalUnpaidLeaveP, (current, item) => current + item.WorkConvert);
            //        }
            //    }
            //    //Tổng ngày công thực tế
            //    salaryInfo.WorkActualDay = Math.Round(totalWork.Value, 2);
            //    //Tổng ngày công thường
            //    salaryInfo.WorkNormalDay = Math.Round(totalNormal.Value, 2);
            //    //Tổng công nghỉ phép hưởng lương
            //    salaryInfo.WorkPaidLeave = Math.Round(totalWorkPaidLeave.Value, 2);
            //    //Tổng công thêm giờ
            //    salaryInfo.OverTime = Math.Round(totalOverTime.Value, 2);
            //    //Tổng tăng ca ngày
            //    salaryInfo.OverTimeDay = Math.Round(totalOverTimeDay.Value, 2);
            //    //Tổng tăng ca đêm
            //    salaryInfo.OverTimeNight = Math.Round(totalOverTimeNight.Value, 2);
            //    //Tổng tăng ca ngày lễ
            //    salaryInfo.OverTimeHoliday = Math.Round(totalOverTimeHoliday.Value, 2);
            //    //Tổng tăng ca cuối tuần
            //    salaryInfo.OverTimeWeekend = Math.Round(totalOverTimeWeekend.Value, 2);
            //    //Tổng công không phép
            //    salaryInfo.WorkUnLeave = Math.Round(totalUnleaveK.Value, 2);
            //    //Tổng công công tác
            //    salaryInfo.WorkGoWork = Math.Round(totalGoWorkC.Value, 2);
            //    //Tổng công trễ
            //    salaryInfo.WorkLate = Math.Round(totalLateM.Value, 2);
            //    //Tổng công nghỉ phép không hưởng lương
            //    salaryInfo.WorkUnpaidLeave = Math.Round(totalUnpaidLeaveP.Value, 2);
            //    //Tổng nghỉ lễ
            //    salaryInfo.WorkHoliday = Math.Round(totalHolidayL.Value, 2);
            //}
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

        private static void EditDataSalary(int recordId, hr_SalaryBoardInfo salaryInfo)
        {
            var salary = sal_SalaryDecisionServices.GetCurrent(recordId);
            if (salary != null)
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
        /// Quay trở lại trang danh sách bảng lương
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBack_Click(object sender, DirectEventArgs e)
        {
            Response.Redirect("~/Modules/Salary/SalaryBoardList.aspx");
        }
    }
}

