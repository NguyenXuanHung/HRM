using System;
using System.Collections.Generic;
using System.Linq;
using Ext.Net;
using Web.Core;
using Web.Core.Framework;
using Web.Core.Object.Catalog;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.Catalog;
using Web.Core.Service.HumanRecord;

namespace Web.HJM.Modules.TimeKeeping
{
    public partial class TimeSheetOverTime : BasePage
    {
        private const string TypeTimeSheet = "Automatic";

        private string TimeSheetEventIds = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ExtNet.IsAjaxRequest)
            {
                hdfDepartments.Text = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
                hdfTypeTimeSheet.Text = TypeTimeSheet;

                // load west
                new Core.Framework.Common.BorderLayout()
                {
                    menuID = MenuId,
                    script = "#{hdfDepartmentSelected}.setValue('" + Core.Framework.Common.BorderLayout.nodeID + "');#{txtSearch}.reset();#{PagingToolbar1}.pageIndex = 0; #{PagingToolbar1}.doLoad();",
                }.AddDepartmentList(brlayout, CurrentUser, true);

                //get list timesheetAdjustment
                CheckInitAdjustment();
            }

            if (btnEdit.Visible)
            {
                RowSelectionModel1.Listeners.RowSelect.Handler += "if(hdfIsLocked.getValue() == 'true' ){btnEdit.disable();}else{btnEdit.enable();}  ";
                RowSelectionModel1.Listeners.RowDeselect.Handler += "btnEdit.disable(); ";
                gridTimeAdjust.DirectEvents.RowDblClick.Event += btnEdit_Click;
            }

            if (btnDelete.Visible)
            {
                RowSelectionModel1.Listeners.RowSelect.Handler += "if(hdfIsLocked.getValue() == 'true' ){ btnDelete.disable();} else { btnDelete.enable(); }";
                RowSelectionModel1.Listeners.RowDeselect.Handler += "btnDelete.disable(); ";
            }

            ucChooseEmployee.AfterClickAcceptButton += new EventHandler(ucChooseEmployee_AfterClickAcceptButton);

        }

        private void CheckInitAdjustment()
        {
            var adjusts = hr_TimeSheetAdjustmentServices.GetAll();
            foreach (var item in adjusts)
            {
                if (item.IsLock == true)
                {
                    Dialog.Alert("Bảng hiệu chỉnh đã khóa. Bạn không được phép thao tác");
                    wdUpdateAdjustment.Hide();
                    btnLockAdjust.Hide();
                    btnOpenAdjust.Show();
                    btnEdit.Disabled = true;
                    btnDelete.Disabled = true;
                    btnAdd.Disabled = true;
                    hdfIsLocked.Text = "true";
                    return;
                }
            }
        }

        #region DirectEvent

        protected void btnEdit_Click(object sender, DirectEventArgs e)
        {
            if (string.IsNullOrEmpty(hdfId.Text))
            {
                ExtNet.Msg.Alert("Thông báo", "Bạn chưa chọn bản ghi nào").Show();
            }
            else
            {
                if (!string.IsNullOrEmpty(hdfIsLocked.Text) && hdfIsLocked.Text == "true")
                {
                    wdUpdateAdjustment.Hide();
                    btnEdit.Disabled = true;
                    btnDelete.Disabled = true;
                    return;
                }
                var adjust = hr_TimeSheetAdjustmentServices.GetById(Convert.ToInt32(hdfId.Text));
                if (adjust != null)
                {
                    AdjustUpdateDate.SetValue(new DateTime(adjust.Year, adjust.Month, adjust.Day));
                    txtReasonUpdate.Text = adjust.Reason;
                    txtFullName.Text = hr_RecordServices.GetFieldValueById(Convert.ToInt32(hdfRecordId.Text), "FullName");
                }

                wdUpdateAdjustment.Show();
            }
        }

        protected void btnDelete_Click(object sender, DirectEventArgs e)
        {
            foreach (var item in RowSelectionModel1.SelectedRows)
            {
                //get adjust need to delete
                var adjust = hr_TimeSheetAdjustmentServices.GetById(Convert.ToInt32(item.RecordID));
                var timeSymbol = cat_TimeSheetSymbolServices.GetById(int.Parse("0" + item.RecordID));
                if (adjust != null)
                {
                    var eventIds = adjust.TimeSheetEventIds.Split(',');
                    for (var i = 0; i < eventIds.Length; i++)
                    {
                        eventIds[i] = "'{0}'".FormatWith(eventIds[i]);
                    }
                    var dateAdjust = new DateTime(adjust.Year, adjust.Month, adjust.Day);
                    //get timeSheet by condition
                    var timeSheet = hr_TimeSheetServices.GetTimeSheetByCondition(adjust.Day, adjust.Month, adjust.Year, adjust.RecordId, adjust.TimeSheetReportId, TypeTimeSheet);
                    if (timeSheet != null)
                    {
                        //get event is active
                        var listEvent = hr_TimeSheetEventServices.GetEventByCondition(timeSheet.Id, EventStatus.Active, string.Join(",", eventIds));
                        foreach (var eventTime in listEvent)
                        {
                            //after update status delete event
                            eventTime.StatusId = EventStatus.Delete;
                            eventTime.EditedDate = DateTime.Now;
                            eventTime.EditedBy = CurrentUser.User.UserName;
                            //update
                            hr_TimeSheetEventServices.Update(eventTime);
                        }
                        timeSheet.OverTimeConvert -= timeSymbol.TimeConvert;
                        //after update detail of timesheet following status event delete 
                        UpdateTimeSheet(timeSheet);
                    }
                }

                //delete
                hr_TimeSheetAdjustmentServices.Delete(int.Parse("0" + item.RecordID));

            }

            RM.RegisterClientScriptBlock("Grid_Reload", "ReloadGrid();");
        }

        /// <summary>
        /// update save DB
        /// </summary>
        /// <param name="timeSheet"></param>
        private void UpdateTimeSheet(hr_TimeSheet timeSheet)
        {
            var events = hr_TimeSheetEventServices.GetAllEventsByTimeSheetId(timeSheet.Id, EventStatus.Active);
            EditWorkConvertTimeSheet(events, timeSheet);
            timeSheet.Detail = events.ToJson();
            hr_TimeSheetServices.Update(timeSheet);
        }

        /// <summary>
        /// Edit work by day
        /// </summary>
        /// <param name="events"></param>
        /// <param name="timeSheet"></param>
        private void EditWorkConvertTimeSheet(List<hr_TimeSheetEvent> events, hr_TimeSheet timeSheet)
        {
            var workConvert = 0.0;
            var moneyConvert = 0.0;
            foreach (var itemEvent in events)
            {
                workConvert += itemEvent.WorkConvert;
                moneyConvert += itemEvent.MoneyConvert;
            }
            timeSheet.WorkConvert = workConvert;
            timeSheet.MoneyConvert = moneyConvert;
        }

        #endregion

        private void ucChooseEmployee_AfterClickAcceptButton(object sender, EventArgs e)
        {
            hdfTotalRecord.Text = ucChooseEmployee.SelectedRow.Count.ToString();
            foreach (var item in ucChooseEmployee.SelectedRow)
            {
                // get employee information
                var hs = RecordController.GetByEmployeeCode(item.RecordID);
                var recordId = hs.Id.ToString();
                var employeeCode = hs.EmployeeCode;
                var fullName = hs.FullName;
                var departmentName = hs.DepartmentName;
                // insert record to grid
                RM.RegisterClientScriptBlock("insert" + recordId,
                    string.Format("addRecord('{0}', '{1}', '{2}', '{3}');", recordId, employeeCode, fullName,
                        departmentName));
            }
        }

        protected void btnUpdateHL_Click(object sender, DirectEventArgs e)
        {
            SaveTimeSheetForDay(sender, e);
            var listId = e.ExtraParams["ListRecordId"].Split(',');
            if (listId.Count() < 1)
            {
                ExtNet.Msg.Alert("Thông báo", "Bạn hãy chọn ít nhất 1 cán bộ").Show();
                return;
            }

            for (var i = 0; i < listId.Length - 1; i++)
            {
                var recordId = listId[i];
                //Lay ma cham cong theo recordId
                var timeSheetCode = GetTimeSheetCode(recordId);
                if (timeSheetCode != null)
                {
                    var timeAdjust = new hr_TimeSheetAdjustment()
                    {
                        RecordId = Convert.ToInt32(recordId),
                        TimeSheetCode = timeSheetCode.Code,
                        Day = startDate.SelectedDate.Day,
                        Month = startDate.SelectedDate.Month,
                        Year = startDate.SelectedDate.Year,
                        Reason = txtReason.Text,
                        TimeSheetReportId = Convert.ToInt32(hdfTimeSheetReport.Text),
                        CreatedDate = DateTime.Now,
                        CreatedBy = CurrentUser.User.UserName,
                        Type = AdjustmentTimeSheetType.AdjustmentOverTime
                    };
                    timeAdjust.TimeSheetEventIds = hdfTimeSheetEventIds.Text.TrimStart(',').TrimEnd(',');
                    hr_TimeSheetAdjustmentServices.Create(timeAdjust);
                }
            }

            TimeSheetEventIds = "";
            hdfTimeSheetEventIds.Text = "";
            wdAdjustment.Hide();
            gridTimeAdjust.Reload();
        }

        protected void btnAccept_Click(object sender, DirectEventArgs e)
        {
            //Chấm công theo ngày
            SaveTimeSheetForDay(sender, e);
        }

        /// <summary>
        /// Chấm công theo ngày
        /// </summary>
        private void SaveTimeSheetForDay(object sender, DirectEventArgs e)
        {
            var listId = e.ExtraParams["ListRecordId"].Split(',');

            if (startDate.SelectedValue == null)
            {
                ExtNet.Msg.Alert("Thông báo", "Bạn chưa chọn ngày cần hiệu chỉnh").Show();
                return;
            }

            var day = startDate.SelectedDate.Day;
            var month = startDate.SelectedDate.Month;
            var year = startDate.SelectedDate.Year;

            for (var i = 0; i < listId.Length - 1; i++)
            {
                var recordId = listId[i];
                //Lay ma cham cong theo recordId
                var timeSheetCode = GetTimeSheetCode(recordId);

                if (timeSheetCode != null)
                {
                    var timeSheet = hr_TimeSheetServices.GetTimeSheetByCondition(day, month, year, Convert.ToInt32(recordId), Convert.ToInt32(hdfTimeSheetReport.Text), TypeTimeSheet);
                    if (timeSheet != null)
                    {
                        var timeSheetEvent = new hr_TimeSheetEvent
                        {
                            TimeSheetId = timeSheet.Id,
                            StatusId = EventStatus.Active,
                            CreatedDate = DateTime.Now,
                            CreatedBy = CurrentUser.User.UserName
                        };
                        //Add moi event
                        AddNewTimeSheetEvent(timeSheet, timeSheetEvent, Convert.ToInt32(recordId));
                        var events = hr_TimeSheetEventServices.GetAllEventsByTimeSheetId(timeSheet.Id, EventStatus.Active);
                        //update timesheet
                        timeSheet.Detail = events.ToJson();
                        hr_TimeSheetServices.Update(timeSheet);
                    }
                    else
                    {
                        //create new timesheet by day selected
                        var newTimeSheet = new hr_TimeSheet()
                        {
                            RecordId = Convert.ToInt32(recordId),
                            TimeSheetCode = timeSheetCode.Code,
                            Month = month,
                            Year = year,
                            Day = day,
                            Detail = " ",
                            CreatedDate = DateTime.Now,
                            CreatedBy = CurrentUser.User.UserName,
                            Type = TypeTimeSheet,
                            OverTimeConvert = 0
                        };
                        hr_TimeSheetServices.Create(newTimeSheet);

                        var timeSheetEvent = new hr_TimeSheetEvent()
                        {
                            TimeSheetId = newTimeSheet.Id,
                            StatusId = EventStatus.Active,
                            CreatedDate = DateTime.Now,
                            CreatedBy = CurrentUser.User.UserName
                        };

                        //Add moi event
                        AddNewTimeSheetEvent(newTimeSheet, timeSheetEvent, Convert.ToInt32(recordId));
                        var events = hr_TimeSheetEventServices.GetAllEventsByTimeSheetId(newTimeSheet.Id, EventStatus.Active);
                        //update timesheet
                        newTimeSheet.Detail = events.ToJson();
                        hr_TimeSheetServices.Update(newTimeSheet);
                    }

                }
            }
        }

        /// <summary>
        /// get timesheetcode
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns></returns>
        private hr_TimeSheetCode GetTimeSheetCode(string recordId)
        {
            return hr_TimeSheetCodeServices.GetTimeSheetCodeByRecordId(Convert.ToInt32(recordId));
        }

        /// <summary>
        /// Add moi timeSheetEvent
        /// </summary>
        /// <param name="timeSheetEvent"></param>
        private void AddNewTimeSheetEvent(hr_TimeSheet timeSheet, hr_TimeSheetEvent timeSheetEvent, int recordId)
        {
            var rowSelecteds = RowSelectionModel2.SelectedRows;
            if (rowSelecteds != null && rowSelecteds.Count > 0)
            {
                //Lay thong tin tu bang ky hieu cham cong cat_TimeSheetSymbol
                foreach (var item in rowSelecteds)
                {
                    var timeSymbol = cat_TimeSheetSymbolServices.GetById(int.Parse("0" + item.RecordID));
                    if (timeSymbol != null)
                    {
                        timeSheetEvent.SymbolDisplay = "<span class='badge badge-normal' title='{1}' >{0}</span>".FormatWith(timeSymbol.Code, timeSymbol.Name);
                        CreateNewTimeSheetEvent(timeSheetEvent, timeSymbol);
                        timeSheet.OverTimeConvert += timeSymbol.TimeConvert;
                    }
                }
            }
        }

        /// <summary>
        /// New event one employee
        /// </summary>
        /// <param name="timeSheetEvent"></param>
        /// <param name="timeSymbol"></param>
        private void CreateNewTimeSheetEvent(hr_TimeSheetEvent timeSheetEvent, cat_TimeSheetSymbol timeSymbol)
        {
            timeSheetEvent.WorkConvert = timeSymbol.WorkConvert;
            timeSheetEvent.TimeConvert = timeSymbol.TimeConvert;
            timeSheetEvent.Symbol = timeSymbol.Code;
            timeSheetEvent.Description = timeSymbol.Name;
            timeSheetEvent.TypeGroup = timeSymbol.Group;
            hr_TimeSheetEventServices.Create(timeSheetEvent);

            TimeSheetEventIds = TimeSheetEventIds + "," + timeSheetEvent.Id;
            hdfTimeSheetEventIds.Text = TimeSheetEventIds;
        }

        protected void btnUpdate_Click(object sender, DirectEventArgs e)
        {
            var adjust = hr_TimeSheetAdjustmentServices.GetById(Convert.ToInt32(hdfId.Text));
            if (adjust != null)
            {
                adjust.Reason = txtReasonUpdate.Text;
                adjust.EditedDate = DateTime.Now;

                hr_TimeSheetAdjustmentServices.Update(adjust);
                gridTimeAdjust.Reload();
            }
        }

        /// <summary>
        /// Khóa hiệu chỉnh
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnLockAdjustClick(object sender, DirectEventArgs e)
        {
            //get list timesheetAdjustment
            var adjusts = hr_TimeSheetAdjustmentServices.GetAll();

            //lock
            foreach (var item in adjusts)
            {
                item.IsLock = true;
                hr_TimeSheetAdjustmentServices.Update(item);
            }

            //reload
            ReloadGrid();

        }

        /// <summary>
        /// Mở hiệu chỉnh
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnOpenAdjustClick(object sender, DirectEventArgs e)
        {
            //get list timesheetAdjustment
            var adjusts = hr_TimeSheetAdjustmentServices.GetAll();

            //open
            foreach (var item in adjusts)
            {
                item.IsLock = false;
                hr_TimeSheetAdjustmentServices.Update(item);
            }

            //reload
            ReloadGrid();
        }

        private void ReloadGrid()
        {
            //get list timesheetAdjustment
            var adjusts = hr_TimeSheetAdjustmentServices.GetAll();
            foreach (var item in adjusts)
            {
                if (item.IsLock == true)
                {
                    if (btnAdd.Visible)
                        btnAdd.Disabled = true;
                    if (btnEdit.Visible)
                        btnEdit.Disabled = true;
                    if (btnDelete.Visible)
                        btnDelete.Disabled = true;
                    if (btnOpenAdjust.Visible)
                        btnOpenAdjust.Show();
                    if (btnLockAdjust.Visible)
                        btnLockAdjust.Hide();
                    wdUpdateAdjustment.Hide();
                    hdfIsLocked.Text = "true";
                }
                else
                {
                    if (btnAdd.Visible)
                        btnAdd.Disabled = false;
                    if (btnEdit.Visible)
                        btnEdit.Disabled = false;
                    if (btnDelete.Visible)
                        btnDelete.Disabled = false;
                    if (btnOpenAdjust.Visible)
                        btnOpenAdjust.Hide();
                    if (btnLockAdjust.Visible)
                        btnLockAdjust.Show();
                    hdfIsLocked.Text = "false";
                }
                break;
            }

            gridTimeAdjust.Reload();
        }
    }
}