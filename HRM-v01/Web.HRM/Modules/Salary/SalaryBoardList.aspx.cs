using Ext.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Core.Framework;
using Web.Core.Framework.Common;
using Web.Core.Framework.Controller;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.Salary;

namespace Web.HRM.Modules.Salary
{
    public partial class SalaryBoardList : BasePage
    {
        private const string SalaryBoardInfoUrl = "~/Modules/Salary/PayrollDetail.aspx";
        private const string SalaryColumnDynamicUrl = "~/Modules/Salary/SalaryColumnDynamic.aspx";

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
                hdfChkIsUpdateSalary.Text = bool.FalseString;
                hdfChkIsUpdateTimeSheet.Text = bool.FalseString;
            }
        }

        /// <summary>
        /// Init setting window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InitWindow(object sender, DirectEventArgs e)
        {
            ResetForm();
            var param = e.ExtraParams["Id"];
            if(int.TryParse(param, out var id))
            {
                if(id > 0)
                {
                    // edit
                    EmployeeGrid.Disabled = true;
                    cbxDepartment.Disabled = true;
                    wdSetting.Title = @"Sửa bảng lương";
                    wdSetting.Icon = Icon.Pencil;
                }
                else
                {
                    // insert
                    EmployeeGrid.Disabled = false;
                    cbxDepartment.Disabled = false;
                    wdSetting.Title = @"Thêm mới bảng lương";
                    wdSetting.Icon = Icon.Add;
                }
                hdfSalaryBoardListId.Text = id.ToString();
                var payrollModel = new PayrollModel();

                if(id > 0)
                {
                    var result = PayrollController.GetById(Convert.ToInt32(hdfSalaryBoardListId.Text));
                    if(result != null)
                        payrollModel = result;
                    //data
                    txtName.Text = payrollModel.Title;
                    txtCode.Text = payrollModel.Code;
                    txtNote.Text = payrollModel.Description;
                    hdfConfigId.Text = payrollModel.ConfigId.ToString();
                    cbxConfigList.Text = payrollModel.ConfigName;
                }
                hdfMonth.Text = payrollModel.Month.ToString();
                cbxMonth.Text = payrollModel.Month.ToString();
                hdfYear.Text = payrollModel.Year.ToString();
                spnYear.Text = payrollModel.Year.ToString();
                // show window
                wdSetting.Show();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [DirectMethod(Timeout = 60000)]
        public void SelectSalaryBoard()
        {
            if(!string.IsNullOrEmpty(hdfSalaryBoardListId.Text))
            {
                //Lay danh sach bang luong
                var boardList = PayrollController.GetById(Convert.ToInt32(hdfSalaryBoardListId.Text));

                if(boardList != null)
                {
                    //get all event
                    var lisEvents = GetAllEvents(boardList, false);
                    //TH chua ton tai bang luong thi di tao moi bang luong
                    var salaryBoard = SalaryBoardInfoController.GetAll(null, hdfSalaryBoardListId.Text, null, null);
                    if(salaryBoard != null && salaryBoard.Count > 0)
                    {
                        if(bool.Parse(hdfChkIsUpdateTimeSheet.Text))
                        {
                            //Truong hop da co bang luong, thi cap nhat lai du lieu tu cham cong cho moi recordId
                            foreach(var salary in salaryBoard)
                            {
                                var salaryInfo = SalaryBoardInfoController.GetById(salary.Id);

                                //Lay tu timeSheet
                                GetDataFromTimeSheet(salaryInfo, lisEvents);
                                //Update
                                SalaryBoardInfoController.Update(salaryInfo);
                            }
                        }

                        if(bool.Parse(hdfChkIsUpdateSalary.Text))
                        {
                            //Truong hop da co bang luong, thi cap nhat lai du lieu tu luong cho moi recordId
                            foreach(var salary in salaryBoard)
                            {
                                var salaryInfo = SalaryBoardInfoController.GetById(salary.Id);

                                //Lay tu sal_SalaryDecision
                                EditDataSalary(salary.RecordId, salaryInfo);

                                //Update
                                SalaryBoardInfoController.Update(salaryInfo);
                            }
                        }
                    }
                }
                Response.Redirect(SalaryBoardInfoUrl + "?mId=" + MenuId + "&id=" + hdfSalaryBoardListId.Text, true);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [DirectMethod]
        public void SelectColumnDynamic()
        {
            if(!string.IsNullOrEmpty(hdfSalaryBoardListId.Text))
            {
                Response.Redirect(SalaryColumnDynamicUrl + "?mId=" + MenuId + "&id=" + hdfSalaryBoardListId.Text, true);
            }
        }

        /// <summary>
        /// Insert or Update Catalog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InsertOrUpdate(object sender, DirectEventArgs e)
        {
            var payrollModel = new PayrollModel();

            // check id
            if(!string.IsNullOrEmpty(hdfSalaryBoardListId.Text) && Convert.ToInt32(hdfSalaryBoardListId.Text) > 0)
            {
                var result = PayrollController.GetById(Convert.ToInt32(hdfSalaryBoardListId.Text));
                if(result != null)
                    payrollModel = result;
            }
            // set new props
            if(!string.IsNullOrEmpty(hdfConfigId.Text))
                payrollModel.ConfigId = Convert.ToInt32(hdfConfigId.Text);

            if(!string.IsNullOrEmpty(hdfMonth.Text))
                payrollModel.Month = Convert.ToInt32(hdfMonth.Text);
            if(!string.IsNullOrEmpty(hdfYear.Text))
                payrollModel.Year = Convert.ToInt32(hdfYear.Text);
            payrollModel.Title = txtName.Text;
            payrollModel.Code = txtCode.Text;
            payrollModel.Description = txtNote.Text;
            if(payrollModel.Id > 0)
            {
                payrollModel.EditedDate = DateTime.Now;
                payrollModel.EditedBy = CurrentUser.User.UserName;
                // update
                var resultModel = PayrollController.Update(payrollModel);
                // check if payroll code exists
                if(resultModel != null)
                {
                    // show success message
                    Dialog.ShowNotification("Lưu thành công");
                    wdSetting.Hide();
                    ResetForm();
                }
                else
                    Dialog.ShowNotification("Mã bảng lương đã tồn tại");
            }
            else
            {
                payrollModel.CreatedDate = DateTime.Now;
                payrollModel.CreatedBy = CurrentUser.User.UserName;
                payrollModel.EditedDate = DateTime.Now;
                payrollModel.EditedBy = CurrentUser.User.UserName;
                // create payroll
                var resultModel = PayrollController.Create(payrollModel);
                // check if payroll code exists
                if(resultModel != null)
                {
                    // init list
                    var listEvents = GetAllEvents(resultModel, true);
                    // create  salaryBoardInfo
                    foreach(var employee in chkEmployeeRowSelection.SelectedRows)
                    {
                        var salaryInfo = new SalaryBoardInfoModel(new hr_SalaryBoardInfo())
                        {
                            RecordId = Convert.ToInt32(employee.RecordID),
                            SalaryBoardId = resultModel.Id,
                            CreatedDate = DateTime.Now,
                            EditedDate = DateTime.Now
                        };

                        //Lay tu timeSheet
                        GetDataFromTimeSheet(salaryInfo, listEvents);
                        //Lay tu sal_SalaryDecision
                        EditDataSalary(salaryInfo.RecordId, salaryInfo);

                        //create
                        SalaryBoardInfoController.Create(salaryInfo);

                    }
                    // show success message
                    Dialog.ShowNotification("Lưu thành công");
                    wdSetting.Hide();
                    ResetForm();
                }
                else
                    Dialog.ShowNotification("Mã bảng lương đã tồn tại");

            }
            grdSalaryBoardList.Reload();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resultModel"></param>
        /// <param name="isInsert"></param>
        /// <returns></returns>
        private List<TimeSheetEventModel> GetAllEvents(PayrollModel resultModel, bool isInsert)
        {
            // get event by record id
            var startDate = new DateTime(resultModel.Year, resultModel.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            string recordIds;
            //TH insert
            if(isInsert)
            {
                recordIds = string.Join(",",
                    chkEmployeeRowSelection.SelectedRows.Select(rc => Convert.ToInt32(rc.RecordID)).ToList());
            }
            else
            {
                recordIds = string.Join(",",
                    SalaryBoardInfoController.GetAll(null, resultModel.Id.ToString(), null, null).ToList()
                        .Select(sb => sb.RecordId));
            }
            //get all events
            var listEvents = TimeSheetEventController.GetAll(null, recordIds, null, null, null, null, false, null, startDate, endDate, null, null, null, null);
            return listEvents;
        }

        [DirectMethod]
        public void ResetForm()
        {
            cbxConfigList.Reset();
            cbxDepartment.Reset();
            cbxMonth.Reset();
            spnYear.Reset();
            txtName.Reset();
            txtCode.Reset();
            txtNote.Reset();
            chkEmployeeRowSelection.ClearSelections();
            hdfConfigId.Reset();
            cbxConfigList.Reset();
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
                SalaryBoardInfoController.DeleteByBoardListId(id);

                //delete salary column dynamic
                SalaryBoardDynamicColumnController.DeleteByCondition(id);

                //Xoa trong danh sach bang luong
                PayrollController.Delete(id);

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
        /// <param name="salaryInfo"></param>
        /// <param name="listEvents"></param>
        private void GetDataFromTimeSheet(SalaryBoardInfoModel salaryInfo, List<TimeSheetEventModel> listEvents)
        {
            // get event by record id
            var listEventByRecord = listEvents.Where(e => e.RecordId == salaryInfo.RecordId).ToList();

            double? totalWork = 0.0;
            double? totalNormal = 0.0;
            double? totalWorkPaidLeave = 0.0;
            double? totalOverTime = 0.0;
            double? totalOverTimeDay = 0.0;
            double? totalOverTimeNight = 0.0;
            double? totalOverTimeHoliday = 0.0;
            double? totalOverTimeWeekend = 0.0;
            double? totalGoWorkC = 0.0;
            double? totalHolidayL = 0.0;
            double? totalLateM = 0.0;
            double? totalUnleaveK = 0.0;
            double? totalUnpaidLeaveP = 0.0;
            double? totalWorkFullDay = 0.0;

            if (listEventByRecord.Count > 0)
            {
                //
                var listTimeFullDay =
                    listEventByRecord.Where(d => d.GroupSymbolGroup == Constant.TimesheetDayShift).ToList();

                // over time 
                var listOverTime =
                    listEventByRecord.Where(d => d.GroupSymbolGroup == Constant.TimesheetOverTime);
                // over time day
                var listOverTimeDay =
                    listEventByRecord.Where(d => d.GroupSymbolGroup == Constant.TimesheetOverTimeDay);
                // over time night
                var listOverTimeNight =
                    listEventByRecord.Where(d => d.GroupSymbolGroup == Constant.TimesheetOverTimeNight);
                // over time holiday
                var listOverTimeHoliday =
                    listEventByRecord.Where(d => d.GroupSymbolGroup == Constant.TimesheetOverTimeHoliday);
                // over time weekend
                var listOverTimeWeekend =
                    listEventByRecord.Where(d => d.GroupSymbolGroup == Constant.TimesheetOverTimeWeekend);
                // paid leaveday
                var listWorkLeaveDay =
                    listEventByRecord.Where(d => d.GroupSymbolGroup == Constant.TimesheetLeave);
                // go work
                var listGoWork = listEventByRecord.Where(d => d.GroupSymbolGroup == Constant.TimesheetGoWork);
                // holiday
                var listHoliday = listEventByRecord.Where(d => d.GroupSymbolGroup == Constant.TimesheetHoliday);
                // late
                var listLate = listEventByRecord.Where(d => d.GroupSymbolGroup == Constant.TimesheetLate);
                //unleave
                var listUnLeave = listEventByRecord.Where(d => d.GroupSymbolGroup == Constant.TimesheetUnLeave);

                //UnpaidLeave
                var listUnpaidLeave = listEventByRecord.Where(d => d.GroupSymbolGroup == Constant.TimesheetNotPaySalary);

                foreach(var itemEvent in listEventByRecord)
                    if(new[] { Constant.TimesheetUnLeave, Constant.TimesheetNotPaySalary }.All(x => x != itemEvent.GroupSymbolGroup))
                        totalWork += itemEvent.WorkConvert;

                totalNormal = listTimeFullDay.Aggregate(totalNormal, (current, itemNormal) => current + itemNormal.WorkConvert);
                totalWorkPaidLeave = listWorkLeaveDay.Aggregate(totalWorkPaidLeave, (current, item) => current + item.WorkConvert);
                totalOverTime = listOverTime.Aggregate(totalOverTime, (current, item) => current + item.TimeConvert);
                totalOverTimeDay = listOverTimeDay.Aggregate(totalOverTimeDay, (current, item) => current + item.TimeConvert);
                totalOverTimeNight = listOverTimeNight.Aggregate(totalOverTimeNight, (current, item) => current + item.TimeConvert);
                totalOverTimeHoliday = listOverTimeHoliday.Aggregate(totalOverTimeHoliday, (current, item) => current + item.TimeConvert);
                totalOverTimeWeekend = listOverTimeWeekend.Aggregate(totalOverTimeWeekend, (current, item) => current + item.TimeConvert);
                totalGoWorkC = listGoWork.Aggregate(totalGoWorkC, (current, item) => current + item.WorkConvert);
                totalLateM = listLate.Aggregate(totalLateM, (current, item) => current + 1);
                totalUnleaveK = listUnLeave.Aggregate(totalUnleaveK, (current, item) => current + item.WorkConvert);
                totalHolidayL = listHoliday.Aggregate(totalHolidayL, (current, item) => current + item.WorkConvert);
                totalUnpaidLeaveP = listUnpaidLeave.Aggregate(totalUnpaidLeaveP, (current, item) => current + item.WorkConvert);
                foreach (var item in listTimeFullDay)
                {
                    if (item.SymbolCode == Constant.SymbolFullDay)
                    {
                        totalWorkFullDay += item.WorkConvert;
                    }
                }
            }

            //Tổng ngày công thực tế
            salaryInfo.WorkActualDay = Math.Round(totalWork.Value, 2);
            //Tổng ngày công thường
            salaryInfo.WorkNormalDay = Math.Round(totalNormal.Value, 2);
            //Tổng công nghỉ phép hưởng lương
            salaryInfo.WorkPaidLeave = Math.Round(totalWorkPaidLeave.Value, 2);
            //Tổng công thêm giờ
            salaryInfo.OverTime = Math.Round(totalOverTime.Value, 2);
            //Tổng tăng ca ngày
            salaryInfo.OverTimeDay = Math.Round(totalOverTimeDay.Value, 2);
            //Tổng tăng ca đêm
            salaryInfo.OverTimeNight = Math.Round(totalOverTimeNight.Value, 2);
            //Tổng tăng ca ngày lễ
            salaryInfo.OverTimeHoliday = Math.Round(totalOverTimeHoliday.Value, 2);
            //Tổng tăng ca cuối tuần
            salaryInfo.OverTimeWeekend = Math.Round(totalOverTimeWeekend.Value, 2);
            //Tổng công không phép
            salaryInfo.WorkUnLeave = Math.Round(totalUnleaveK.Value, 2);
            //Tổng công công tác
            salaryInfo.WorkGoWork = Math.Round(totalGoWorkC.Value, 2);
            //Tổng công trễ
            salaryInfo.WorkLate = Math.Round(totalLateM.Value, 2);
            //Tổng công nghỉ phép không hưởng lương
            salaryInfo.WorkUnpaidLeave = Math.Round(totalUnpaidLeaveP.Value, 2);
            //Tổng nghỉ lễ
            salaryInfo.WorkHoliday = Math.Round(totalHolidayL.Value, 2);
            //Tổng công X
            salaryInfo.WorkFullDay = Math.Round(totalWorkFullDay.Value, 2);
        }

        /// <summary>
        /// Sửa salary info
        /// </summary>
        /// <param name="recordId"></param>
        /// <param name="salaryInfo"></param>
        private static void EditDataSalary(int recordId, SalaryBoardInfoModel salaryInfo)
        {
            var salary = sal_SalaryDecisionServices.GetCurrent(recordId);
            if(salary == null) return;
            salaryInfo.SalaryFactor = (double)salary.Factor;
            salaryInfo.SalaryBasic = (double)CatalogBasicSalaryController.GetCurrent().Value;
            salaryInfo.SalaryNet = (double)salary.NetSalary;
            salaryInfo.SalaryGross = (double)salary.GrossSalary;
            salaryInfo.SalaryContract = (double)salary.ContractSalary;
            salaryInfo.SalaryInsurance = (double)salary.InsuranceSalary;
        }

        /// <summary>
        /// lay du lieu luong co dinh
        /// </summary>
        /// <param name="item"></param>
        /// <param name="salaryInfo"></param>
        private static void GetDataFromSalary(TimeSheetCodeModel item, SalaryBoardInfoModel salaryInfo)
        {
            //Edit data salary
            EditDataSalary(item.RecordId, salaryInfo);
        }
    }
}