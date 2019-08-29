using Ext.Net;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Web.Core;
using Web.Core.Framework;
using Web.Core.Framework.Common;
using Web.Core.Framework.Controller.TimeSheet;
using Web.Core.Framework.Utils;

namespace Web.HRM.Modules.TimeSheet
{
    public partial class TimeSheetDetailManagement : BasePage
    {
        private const string TimeSheetReportUrl = "~/Modules/TimeSheet/TimeSheetListManagement.aspx";
        private TimeSheetReportModel _timeSheetReport;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ExtNet.IsAjaxRequest)
            {
                hdfTimeSheetReportId.Text = Request.QueryString["id"];
                hdfTypeTimeSheet.Text = Constant.TimesheetTypeTimeSheet;
                hdfDepartments.Text = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
                if (!string.IsNullOrEmpty(hdfTimeSheetReportId.Text))
                {
                    // get time sheet report
                    _timeSheetReport = TimeSheetReportController.GetById(Convert.ToInt32(hdfTimeSheetReportId.Text));
                    if (_timeSheetReport != null)
                    {
                        // set title for grid
                        gridTimeSheet.Title = _timeSheetReport.Name;
                        // set start date
                        if (_timeSheetReport.StartDate != null)
                        {
                            dfFromDateSearch.MinDate = (DateTime)_timeSheetReport.StartDate;
                            dfToDateSearch.MinDate = (DateTime)_timeSheetReport.StartDate;
                            dfFromDateSearch.SelectedDate = (DateTime)_timeSheetReport.StartDate;
                            hdfStartDate.Text = _timeSheetReport.StartDate.ToString();
                        }
                        // set end date
                        if (_timeSheetReport.EndDate != null)
                        {
                            dfToDateSearch.MinDate = (DateTime)_timeSheetReport.EndDate;
                            dfToDateSearch.MaxDate = (DateTime)_timeSheetReport.EndDate;
                            dfToDateSearch.SelectedDate = (DateTime)_timeSheetReport.EndDate;
                            hdfEndDate.Text = _timeSheetReport.EndDate.ToString();
                        }

                        ReloadGrid();
                    }

                    var timeSheetEmployeeReports = TimeSheetEmployeeReportController.GetAll(null, null, null,
                        Convert.ToInt32(hdfTimeSheetReportId.Text), null, null);
                    if (timeSheetEmployeeReports != null)
                    {
                        hdfRecordIds.Text = string.Join(",", timeSheetEmployeeReports.Select(tser => tser.RecordId));
                    }
                }

                // init department
                storeDepartment.DataSource = CurrentUser.DepartmentsTree;
                storeDepartment.DataBind();

                // generate dynamic column by day
                AddDayColumnToGrid(dfFromDateSearch.SelectedDate, dfToDateSearch.SelectedDate);
            }
        }



        #region Direct Method

        /// <summary>
        /// 
        /// </summary>
        [DirectMethod]
        public void InitWindowTimeSheet(string day, string recordId)
        {
            var startDate = ConvertUtils.GetStartDayOfMonth();
            if (DateTime.TryParse(day, out var date))
            {
                hdfStartDateEmployee.Text = date.ToString("dd/MM/yyyy");
                hdfEndDateEmployee.Text = date.ToString("dd/MM/yyyy");
                cbxDay.Text = date.ToString("dd/MM/yyyy");
                startDate = date;
            }

            _timeSheetReport = TimeSheetReportController.GetById(Convert.ToInt32(hdfTimeSheetReportId.Text));
            if (_timeSheetReport != null)
            {
                var timeSheet = TimeSheetController.GetTimeSheet(Convert.ToInt32(recordId), startDate, startDate);
                if (timeSheet != null)
                {
                    txtTimeLogs.Text = timeSheet.TimeLogs;
                }
            }
            if (_timeSheetReport.Status == TimeSheetStatus.Locked) return;
            gridUpdateTimeSheet.Reload();
            btnDeleteUpdateTimeSheet.Disabled = true;
            wdUpdateTimeSheet.Show();
        }

        #endregion

        #region Event Method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAccept_Click(object sender, DirectEventArgs e)
        {
            try
            {
                foreach (var workShift in chkWorkShiftRowSelection.SelectedRows)
                {
                    foreach (var symbol in chkSelectionModelSymbol.SelectedRows)
                    {
                        var timeSheetSymbol = TimeSheetSymbolController.GetById(int.Parse(symbol.RecordID));
                        var timeSheetGroupSymbol =
                            TimeSheetGroupSymbolController.GetById(timeSheetSymbol.GroupSymbolId);
                        var timeSheetEvent = new TimeSheetEventModel
                        {
                            RecordId = int.Parse(hdfRecordId.Text),
                            WorkShiftId = int.Parse(workShift.RecordID),
                            SymbolId = timeSheetSymbol.Id,
                            GroupSymbolId = timeSheetSymbol.GroupSymbolId,
                            WorkConvert = timeSheetSymbol.WorkConvert,
                            TimeConvert = timeSheetSymbol.TimeConvert,
                            Type = timeSheetGroupSymbol.Group == Constant.TimesheetOverTime
                                ? TimeSheetAdjustmentType.AdjustmentOverTime
                                : TimeSheetAdjustmentType.Adjustment,
                            Description = timeSheetGroupSymbol.Group == Constant.TimesheetOverTime
                                ? "Thêm giờ"
                                : "Hiệu chỉnh"
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
                gridTimeSheet.Reload();
                gridUpdateTimeSheet.Reload();
            }
            catch (Exception ex)
            {
                Dialog.ShowNotification("Có lỗi xảy ra" + ex.Message);
            }
        }

        /// <summary>
        /// Khóa bảng công
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLockTimeSheetReportClick(object sender, DirectEventArgs e)
        {
            try
            {
                _timeSheetReport = TimeSheetReportController.GetById(Convert.ToInt32(hdfTimeSheetReportId.Text));
                _timeSheetReport.Status = TimeSheetStatus.Locked;
                TimeSheetReportController.Update(_timeSheetReport);
                ReloadGrid();
            }
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// Mở bảng công
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUnlockTimeSheetReportClick(object sender, DirectEventArgs e)
        {
            try
            {
                _timeSheetReport = TimeSheetReportController.GetById(Convert.ToInt32(hdfTimeSheetReportId.Text));
                _timeSheetReport.Status = TimeSheetStatus.Active;
                TimeSheetReportController.Update(_timeSheetReport);
                ReloadGrid();
            }
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDeleteTime_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(hdfUpdateTimeSheetEventId.Text)) return;
                var id = int.Parse(hdfUpdateTimeSheetEventId.Text);

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

                gridUpdateTimeSheet.Reload();
                gridTimeSheet.Reload();

            }
            catch (Exception ex)
            {
                Dialog.ShowNotification("Có lỗi xảy ra" + ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBack_Click(object sender, DirectEventArgs e)
        {
            Response.Redirect(TimeSheetReportUrl + "?mId=" + MenuId, true);
        }

        /// <summary>
        /// 
        /// </summary>
        [DirectMethod]
        public void OnFilterDay()
        {
            // get start date
            if (!DateTime.TryParse(hdfStartDate.Text, out var startDate)) return;

            // get end date
            if (!DateTime.TryParse(hdfEndDate.Text, out var endDate)) return;

            // get column count
            var columnsCount = gridTimeSheet.ColumnModel.Columns.Count;

            // hidden unselected column
            for (var day = startDate.Date; day.Date <= endDate.Date; day = day.AddDays(1))
            {
                if (dfFromDateSearch.SelectedDate > day || dfToDateSearch.SelectedDate < day)
                {
                    gridTimeSheet.ColumnModel.SetHidden(columnsCount++, true);
                }
                else
                {
                    gridTimeSheet.ColumnModel.SetHidden(columnsCount++, false);
                }
            }
        }
        #endregion

        #region Private Method

        /// <summary>
        /// Sinh cột và record field động
        /// </summary>
        private void AddDayColumnToGrid(DateTime startDate, DateTime endDate)
        {

            //if (!startDate.HasValue || !endDate.HasValue) return;
            // get list day for combo box
            var days = new List<object>();
            var count = 0;
            for (var day = startDate.Date; day.Date <= endDate.Date; day = day.AddDays(1))
            {
                var recordField = new RecordField
                {
                    Name = "Day{0}".FormatWith(day.ToString("ddMM")),
                    Mapping = "TimeSheetEventDayModels[{0}]".FormatWith(count++)
                };
                var col = new Column
                {
                    ColumnID = recordField.Name,
                    DataIndex = recordField.Name,
                    Header = day.ToString("dddd - dd/MM"),
                    Align = Alignment.Left,
                    Width = 100,
                    Renderer = { Fn = "RenderDay" },
                    Tooltip = day.ToString("dd/MM/yyyy")
                };
                days.Add(new { Day = day.ToString("dd/MM/yyyy") });
                storeTimeSheet.AddField(recordField);
                gridTimeSheet.ColumnModel.Columns.Add(col);
            }
            // add days to combobox
            storeCbxDay.DataSource = days;
            storeCbxDay.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        private void ReloadGrid()
        {
            try
            {
                if (_timeSheetReport.Status == TimeSheetStatus.Locked)
                {
                    Dialog.Alert("Bảng hiệu chỉnh đã khóa. Bạn không được phép thao tác");
                    wdTimeSheetAdd.Hide();
                    if (btnOpenTimeSheet.Visible)
                        btnOpenTimeSheet.Show();
                    if (btnLockTimeSheet.Visible)
                        btnLockTimeSheet.Hide();
                }
                else
                {
                    if (btnOpenTimeSheet.Visible)
                        btnOpenTimeSheet.Hide();
                    if (btnLockTimeSheet.Visible)
                        btnLockTimeSheet.Show();
                }
                // gridTimeSheet.Reload();
            }
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }

        #endregion
    }
}
