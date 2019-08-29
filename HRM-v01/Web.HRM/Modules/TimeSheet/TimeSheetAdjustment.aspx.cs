using System;
using Ext.Net;
using Web.Core.Framework;
using System.Linq;
using Web.Core;
using Web.Core.Framework.Common;
using Web.Core.Framework.Controller.TimeSheet;
using Web.Core.Framework.Utils;

namespace Web.HRM.Modules.TimeSheet
{
    public partial class TimeSheetAdjustment : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ExtNet.IsAjaxRequest)
            {
                hdfMenuID.Text = MenuId.ToString();
                hdfDepartments.Text = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
                hdfTypeTimeSheet.Text = Constant.TimesheetTypeTimeSheet;
                hdfAdjustTimeSheetHandlerType.Text = Request.QueryString["AdjustType"];
                if (int.TryParse(hdfAdjustTimeSheetHandlerType.Text, out var adjustType))
                    if (adjustType == (int)TimeSheetAdjustmentType.AdjustmentOverTime)
                        hdfGroupSymbol.Text = Constant.TimesheetOverTime;
                // init department
                storeDepartment.DataSource = CurrentUser.DepartmentsTree;
                storeDepartment.DataBind();
                dfFromDate.SelectedDate = DateTime.Today;
                dfToDate.SelectedDate = DateTime.Today;
                hdfGroupWorkShift.Text = null;
                dfFromDateSearch.SetValue(ConvertUtils.GetStartDayOfMonth());
                dfToDateSearch.SetValue(ConvertUtils.GetLastDayOfMonth());

                // load west
                new Core.Framework.Common.BorderLayout()
                {
                    menuID = MenuId,
                    script = "#{hdfDepartmentSelected}.setValue('" + Core.Framework.Common.BorderLayout.nodeID +
                             "');#{txtSearch}.reset();#{PagingToolbar1}.pageIndex = 0; #{PagingToolbar1}.doLoad();"
                }.AddDepartmentList(brlayout, CurrentUser, false);
                //get list timesheet Adjustment
            }

            if (btnEdit.Visible)
            {
                //RowSelectionModel1.Listeners.RowSelect.Handler += "if(hdfIsLocked.getValue() == 'true' ){btnEdit.disable();}else{btnEdit.enable();}  ";
                //RowSelectionModel1.Listeners.RowDeselect.Handler += "btnEdit.disable(); ";
            }

            if (btnDelete.Visible)
            {
                RowSelectionModel1.Listeners.RowSelect.Handler += "btnDelete.enable();";
                RowSelectionModel1.Listeners.RowDeselect.Handler += "btnDelete.disable(); ";
            }
        }

        protected void btnEdit_Click(object sender, DirectEventArgs e)
        {

        }

        protected void Delete(object sender, DirectEventArgs e)
        {
            try
            {
                // init id
                var param = e.ExtraParams["Id"];
                // parse id
                if (!int.TryParse(param, out var id) || id <= 0)
                {
                    // parse error, show error
                    Dialog.ShowError("Có lỗi xảy ra trong quá trình xử lý");
                    return;
                }
                // delete
                var deletedTimeSheetEvent = TimeSheetEventController.Delete(id);

                // get symbol group
                var timeSheetGroupSymbol = TimeSheetGroupSymbolController.GetById(deletedTimeSheetEvent.GroupSymbolId);

                // update annual leave
                if (timeSheetGroupSymbol.Group == Constant.TimesheetLeave)
                {
                    // get annual leave history by record id and event id
                    var annualLeaveHistorys = AnnualLeaveHistoryController.GetAll(null,
                        deletedTimeSheetEvent.RecordId.ToString(), deletedTimeSheetEvent.Id, false, null, 1);
                    if (annualLeaveHistorys != null && annualLeaveHistorys.Count > 0)
                    {
                        // delete annual leave history
                        var annualLeaveHistory = AnnualLeaveHistoryController.Delete(annualLeaveHistorys.First().Id);

                        // get annual leave config by record id and year
                        var annualLeaveConfigures = AnnualLeaveConfigureController.GetAll(null, annualLeaveHistory.RecordId.ToString(), null,
                            annualLeaveHistory.UsedLeaveDate.Year, false, null, 1);
                        if (annualLeaveConfigures != null && annualLeaveConfigures.Count > 0)
                        {
                            // get first item
                            var annualLeaveConfigure = annualLeaveConfigures.First();

                            // calc used and remain leave day
                            annualLeaveConfigure.UsedLeaveDay -= annualLeaveHistory.UsedLeaveDay;
                            if (annualLeaveConfigure.AnnualLeaveDay > 0)
                                annualLeaveConfigure.RemainLeaveDay =
                                    annualLeaveConfigure.AnnualLeaveDay - annualLeaveConfigure.UsedLeaveDay;
                            annualLeaveConfigure.EditedBy = CurrentUser.User.UserName;

                            // update annual leave configure
                            AnnualLeaveConfigureController.Update(annualLeaveConfigure);
                        }
                    }
                }

                // reload data
                gridTimeAdjust.Reload();
            }
            catch (Exception exception)
            {
                Dialog.ShowError(exception);
            }
        }

        protected void btnUpdateHL_Click(object sender, DirectEventArgs e)
        {
            try
            {
                foreach (var employee in chkEmployeeRowSelection.SelectedRows)
                {
                    var timeSheetEmployeeWorkShift =
                        TimeSheetEmployeeGroupWorkShiftController.GetById(int.Parse(employee.RecordID));
                    foreach (var workShift in chkWorkShiftRowSelection.SelectedRows)
                    {
                        // var timeSheetWorkShift = TimeSheetWorkShiftController.GetById(int.Parse(workShift.RecordID));
                        foreach (var symbol in chkSymbolRowSelection.SelectedRows)
                        {
                            // get symbol
                            var timeSheetSymbol = TimeSheetSymbolController.GetById(int.Parse(symbol.RecordID));
                            // get symbol group
                            var timeSheetGroupSymbol =
                                TimeSheetGroupSymbolController.GetById(timeSheetSymbol.GroupSymbolId);
                            // update time sheet event
                            var timeSheetEvent = new TimeSheetEventModel
                            {
                                RecordId = timeSheetEmployeeWorkShift.RecordId,
                                SymbolId = timeSheetSymbol.Id,
                                GroupSymbolId = timeSheetSymbol.GroupSymbolId,
                                WorkShiftId = int.Parse(workShift.RecordID),
                                WorkConvert = timeSheetSymbol.WorkConvert,
                                TimeConvert = timeSheetSymbol.TimeConvert,
                                Type = timeSheetGroupSymbol.Group == Constant.TimesheetOverTime
                                    ? TimeSheetAdjustmentType.AdjustmentOverTime
                                    : TimeSheetAdjustmentType.Adjustment,
                                Description = timeSheetGroupSymbol.Group == Constant.TimesheetOverTime
                                    ? "Thêm giờ"
                                    : "Hiệu chỉnh",
                                CreatedBy = CurrentUser.User.UserName
                            };
                            var newTimeSheetEvent = TimeSheetEventController.Create(timeSheetEvent);

                            // update annual leave
                            if (timeSheetGroupSymbol.Group == Constant.TimesheetLeave)
                            {
                                // update annual leave history
                                var annualLeaveHistory = new AnnualLeaveHistoryModel
                                {
                                    RecordId = newTimeSheetEvent.RecordId,
                                    TimeSheetEventId = newTimeSheetEvent.Id,
                                    UsedLeaveDate = newTimeSheetEvent.StartDate,
                                    UsedLeaveDay = newTimeSheetEvent.WorkConvert,
                                    CreatedBy = CurrentUser.User.UserName
                                };
                                AnnualLeaveHistoryController.Create(annualLeaveHistory);

                                // get annual leave configure by record id and year
                                var annualLeaveConfigures = AnnualLeaveConfigureController.GetAll(null, annualLeaveHistory.RecordId.ToString(), null,
                                    annualLeaveHistory.UsedLeaveDate.Year, false, null, 1);

                                if (annualLeaveConfigures == null || annualLeaveConfigures.Count == 0) continue;

                                // get first item in list
                                var annualLeaveConfigure = annualLeaveConfigures.First();

                                // calc used and remain leave day
                                annualLeaveConfigure.UsedLeaveDay += annualLeaveHistory.UsedLeaveDay;
                                if (annualLeaveConfigure.AnnualLeaveDay > 0)
                                    annualLeaveConfigure.RemainLeaveDay =
                                        annualLeaveConfigure.AnnualLeaveDay - annualLeaveConfigure.UsedLeaveDay;
                                annualLeaveConfigure.EditedBy = CurrentUser.User.UserName;

                                // update annual leave configure
                                AnnualLeaveConfigureController.Update(annualLeaveConfigure);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Dialog.ShowError(exception);
            }
            gridTimeAdjust.Reload();
            wdAdjustment.Hide();
        }

        protected void btnUpdate_Click(object sender, DirectEventArgs e)
        {

        }

        [DirectMethod]
        public void ResetForm()
        {
            chkEmployeeRowSelection.ClearSelections();
            chkSymbolRowSelection.ClearSelections();
            chkWorkShiftRowSelection.ClearSelections();
        }
    }
}