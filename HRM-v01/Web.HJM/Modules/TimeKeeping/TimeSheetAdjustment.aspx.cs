using System;
using System.Collections.Generic;
using System.Web;
using Ext.Net;
using System.IO;
using SoftCore;
using Web.Core.Framework;
using System.Linq;
using Web.Core;
using Web.Core.Object.Catalog;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.HumanRecord;
using Web.Core.Service.Catalog;
using Web.Core.Object.Security;

namespace Web.HJM.Modules.TimeKeeping
{
    public partial class TimeSheetAdjustment : BasePage
    {
        private const string ConstWork = "CongTac";
        private const string ConstPaySalary = "HuongLuong";
        private const string ConstNotPaySalary = "KhongLuong";
        private const string ConstNotLeave = "KhongPhep";
        private const string ConstLate = "Muon";
        private const string ConstHoliday = "NghiLe";
        private const string ConstLeave = "NghiPhep";
        private const string TypeTimeSheet = "Automatic";
        private const string ConstDefault1 = "Default1";
        private const string ConstDefault2 = "Default2";
        private const string ConstDefault3 = "Default3";
        private const string ConstDefault4 = "Default4";
        private const string ConstDefault5 = "Default5";
        private const string ConstDefault6 = "Default6";
        private const string ConstDefault7 = "Default7";
        private const string ConstDefault8 = "Default8";
        private const string ConstDefault9 = "Default9";
        private const string ConstDefault10 = "Default10";
        private const string ConstDefault11 = "Default11";
        private const string ConstDefault12 = "Default12";
        private const string ConstDefault13 = "Default13";
        private const string ConstDefault14 = "Default14";
        private string TimeSheetEventIds = "";

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!X.IsAjaxRequest)
            {
                hdfMenuID.Text = MenuId.ToString();
                hdfDepartments.Text = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
                hdfTypeTimeSheet.Text = TypeTimeSheet;

                // load west
                new Core.Framework.Common.BorderLayout()
                {
                    menuID = MenuId,
                    script = "#{hdfDepartmentSelected}.setValue('" + Core.Framework.Common.BorderLayout.nodeID +
                             "');#{txtSearch}.reset();#{PagingToolbar1}.pageIndex = 0; #{PagingToolbar1}.doLoad();"
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
            try
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
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }

        protected void btnDelete_Click(object sender, DirectEventArgs e)
        {
            try
            {
                foreach (var item in RowSelectionModel1.SelectedRows)
                {
                    //get adjust need to delete
                    var adjust = hr_TimeSheetAdjustmentServices.GetById(Convert.ToInt32(item.RecordID));
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

                                if (eventTime.TypeGroup == ConstLeave)
                                {
                                    var annualHistory =
                                        hr_AnnualLeaveHistoryServices.GetAnnualHistoryByCondition(dateAdjust,
                                            timeSheet.RecordId, eventTime.Id);
                                    if (annualHistory != null)
                                    {
                                        var day = -annualHistory.UsedLeaveDay;
                                        annualHistory.UsedLeaveDay = day;
                                        hr_AnnualLeaveHistoryServices.Update(annualHistory);
                                        UpdateAnnualLeaveConfig(timeSheet.RecordId, annualHistory);
                                    }

                                }
                                
                            }
                            //after update detail of timesheet following status event delete 
                            UpdateTimeSheet(timeSheet);
                        }
                    }

                    //delete
                    hr_TimeSheetAdjustmentServices.Delete(int.Parse("0" + item.RecordID));

                }

                RM.RegisterClientScriptBlock("Grid_Reload", "ReloadGrid();");
            }
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
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
            try
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
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Thông báo từ hệ thống", "Có lỗi xảy ra: " + ex.Message).Show();
            }
        }
     
        protected void btnUpdateHL_Click(object sender, DirectEventArgs e)
        {
            try
            {
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
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Thông báo từ hệ thống", "Có lỗi xảy ra: " + ex.Message).Show();
            }
        }

        protected void btnAccept_Click(object sender, DirectEventArgs e)
        {
            try
            {
                //Chấm công theo ngày
                SaveTimeSheetForDay(sender, e);
                wdTimeSheetAdd.Hide();
            }
            catch (Exception ex)
            {
                Dialog.ShowNotification("Có lỗi xảy ra" + ex.Message);
            }
        }

        /// <summary>
        /// Chấm công theo ngày
        /// </summary>
        private void SaveTimeSheetForDay(object sender, DirectEventArgs e)
        {
            try
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
                            AddNewTimeSheetEvent(timeSheetEvent, Convert.ToInt32(recordId));
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
                            AddNewTimeSheetEvent(timeSheetEvent, Convert.ToInt32(recordId));
                            var events = hr_TimeSheetEventServices.GetAllEventsByTimeSheetId(newTimeSheet.Id, EventStatus.Active);
                            //update timesheet
                            newTimeSheet.Detail = events.ToJson();
                            hr_TimeSheetServices.Update(newTimeSheet);
                        }
                            
                    }
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowNotification("Có lỗi xảy ra" + ex.Message);
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
        private void AddNewTimeSheetEvent(hr_TimeSheetEvent timeSheetEvent, int recordId)
        {
            try
            {
                //var description = string.Empty;
                var rowSelecteds = RowSelectionModel2.SelectedRows;
                if (rowSelecteds != null && rowSelecteds.Count > 0)
                {
                    //Lay thong tin tu bang ky hieu cham cong cat_TimeSheetSymbol
                    foreach (var item in rowSelecteds)
                    {
                        var timeSymbol = cat_TimeSheetSymbolServices.GetById(int.Parse("0" + item.RecordID));
                        if (timeSymbol != null)
                        {
                            //Nhóm ký hiệu công tác
                            if (timeSymbol.Group == ConstWork)
                            {
                                timeSheetEvent.SymbolDisplay = "<span class='badge badge-work' title='{1}' >{0}</span>".FormatWith(timeSymbol.Code, timeSymbol.Name);
                                CreateNewTimeSheetEvent(timeSheetEvent, timeSymbol);
                            }
                            //Nhóm ký hiệu hưởng lương
                            if (timeSymbol.Group == ConstPaySalary)
                            {
                                timeSheetEvent.SymbolDisplay = "<span class='badge badge-normal' title='{1}'>{0}</span>".FormatWith(timeSymbol.Code, timeSymbol.Name);
                                CreateNewTimeSheetEvent(timeSheetEvent, timeSymbol);
                            }
                            //Nhóm ký hiệu không hưởng lương
                            if (timeSymbol.Group == ConstNotPaySalary)
                            {
                                timeSheetEvent.SymbolDisplay = "<span class='badge badge-red' title='{1}'>{0}</span>".FormatWith(timeSymbol.Code, timeSymbol.Name);
                                CreateNewTimeSheetEvent(timeSheetEvent, timeSymbol);
                            }
                            //Nhóm ký hiệu muộn
                            if (timeSymbol.Group == ConstLate)
                            {
                                timeSheetEvent.SymbolDisplay = "<span class='badge badge-late' title='{1}'>{0}</span>".FormatWith(timeSymbol.Code, timeSymbol.Name);
                                CreateNewTimeSheetEvent(timeSheetEvent, timeSymbol);
                            }
                            //Nhóm ký hiệu nghỉ lễ
                            if (timeSymbol.Group == ConstHoliday)
                            {
                                timeSheetEvent.SymbolDisplay = "<span class='badge badge-holiday' title='{1}'>{0}</span>".FormatWith(timeSymbol.Code, timeSymbol.Name);
                                CreateNewTimeSheetEvent(timeSheetEvent, timeSymbol);
                            }
                            //Nhóm ký hiệu nghỉ phép
                            if (timeSymbol.Group == ConstLeave)
                            {
                                timeSheetEvent.SymbolDisplay = "<span class='badge badge-leave' title='{1}'>{0}</span>".FormatWith(timeSymbol.Code, timeSymbol.Name);
                                CreateNewTimeSheetEvent(timeSheetEvent, timeSymbol);

                                //Đăng ký bảng history phép thường niên
                                CreateAnnualLeave(timeSymbol, recordId, timeSheetEvent.Id);
                            }
                            //Nhóm ký hiệu không phép
                            if (timeSymbol.Group == ConstNotLeave)
                            {
                                timeSheetEvent.SymbolDisplay = "<span class='badge badge-notleave' title='{1}'>{0}</span>".FormatWith(timeSymbol.Code, timeSymbol.Name);
                                CreateNewTimeSheetEvent(timeSheetEvent, timeSymbol);
                            }

                            //Nhóm ký hiệu mặc định
                            CreateGroupTimeSheetDefault(timeSymbol, timeSheetEvent);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowNotification("Có lỗi xảy ra" + ex.Message);
            }
        }

        /// <summary>
        /// Nhóm ký hiệu mặc định
        /// </summary>
        /// <param name="timeSymbol"></param>
        /// <param name="timeSheetEvent"></param>
        private void CreateGroupTimeSheetDefault(cat_TimeSheetSymbol timeSymbol, hr_TimeSheetEvent timeSheetEvent)
        {
            if (timeSymbol.Group == ConstDefault1)
            {
                timeSheetEvent.SymbolDisplay = "<span class='badge badge-default1' title='{1}'>{0}</span>".FormatWith(timeSymbol.Code, timeSymbol.Name);
                CreateNewTimeSheetEvent(timeSheetEvent, timeSymbol);
            }
            if (timeSymbol.Group == ConstDefault2)
            {
                timeSheetEvent.SymbolDisplay = "<span class='badge badge-default2' title='{1}'>{0}</span>".FormatWith(timeSymbol.Code, timeSymbol.Name);
                CreateNewTimeSheetEvent(timeSheetEvent, timeSymbol);
            }
            if (timeSymbol.Group == ConstDefault3)
            {
                timeSheetEvent.SymbolDisplay = "<span class='badge badge-default3' title='{1}'>{0}</span>".FormatWith(timeSymbol.Code, timeSymbol.Name);
                CreateNewTimeSheetEvent(timeSheetEvent, timeSymbol);
            }
            if (timeSymbol.Group == ConstDefault4)
            {
                timeSheetEvent.SymbolDisplay = "<span class='badge badge-default4' title='{1}'>{0}</span>".FormatWith(timeSymbol.Code, timeSymbol.Name);
                CreateNewTimeSheetEvent(timeSheetEvent, timeSymbol);
            }
            if (timeSymbol.Group == ConstDefault5)
            {
                timeSheetEvent.SymbolDisplay = "<span class='badge badge-default5' title='{1}'>{0}</span>".FormatWith(timeSymbol.Code, timeSymbol.Name);
                CreateNewTimeSheetEvent(timeSheetEvent, timeSymbol);
            }
            if (timeSymbol.Group == ConstDefault6)
            {
                timeSheetEvent.SymbolDisplay = "<span class='badge badge-default6' title='{1}'>{0}</span>".FormatWith(timeSymbol.Code, timeSymbol.Name);
                CreateNewTimeSheetEvent(timeSheetEvent, timeSymbol);
            }
            if (timeSymbol.Group == ConstDefault7)
            {
                timeSheetEvent.SymbolDisplay = "<span class='badge badge-default7' title='{1}'>{0}</span>".FormatWith(timeSymbol.Code, timeSymbol.Name);
                CreateNewTimeSheetEvent(timeSheetEvent, timeSymbol);
            }
            if (timeSymbol.Group == ConstDefault8)
            {
                timeSheetEvent.SymbolDisplay = "<span class='badge badge-default8' title='{1}'>{0}</span>".FormatWith(timeSymbol.Code, timeSymbol.Name);
                CreateNewTimeSheetEvent(timeSheetEvent, timeSymbol);
            }
            if (timeSymbol.Group == ConstDefault9)
            {
                timeSheetEvent.SymbolDisplay = "<span class='badge badge-default9' title='{1}'>{0}</span>".FormatWith(timeSymbol.Code, timeSymbol.Name);
                CreateNewTimeSheetEvent(timeSheetEvent, timeSymbol);
            }
            if (timeSymbol.Group == ConstDefault10)
            {
                timeSheetEvent.SymbolDisplay = "<span class='badge badge-default10' title='{1}'>{0}</span>".FormatWith(timeSymbol.Code, timeSymbol.Name);
                CreateNewTimeSheetEvent(timeSheetEvent, timeSymbol);
            }
            if (timeSymbol.Group == ConstDefault11)
            {
                timeSheetEvent.SymbolDisplay = "<span class='badge badge-default11' title='{1}'>{0}</span>".FormatWith(timeSymbol.Code, timeSymbol.Name);
                CreateNewTimeSheetEvent(timeSheetEvent, timeSymbol);
            }
            if (timeSymbol.Group == ConstDefault12)
            {
                timeSheetEvent.SymbolDisplay = "<span class='badge badge-default12' title='{1}'>{0}</span>".FormatWith(timeSymbol.Code, timeSymbol.Name);
                CreateNewTimeSheetEvent(timeSheetEvent, timeSymbol);
            }
            if (timeSymbol.Group == ConstDefault13)
            {
                timeSheetEvent.SymbolDisplay = "<span class='badge badge-default13' title='{1}'>{0}</span>".FormatWith(timeSymbol.Code, timeSymbol.Name);
                CreateNewTimeSheetEvent(timeSheetEvent, timeSymbol);
            }
            if (timeSymbol.Group == ConstDefault14)
            {
                timeSheetEvent.SymbolDisplay = "<span class='badge badge-default14' title='{1}'>{0}</span>".FormatWith(timeSymbol.Code, timeSymbol.Name);
                CreateNewTimeSheetEvent(timeSheetEvent, timeSymbol);
            }
        }

        private void CreateAnnualLeave(cat_TimeSheetSymbol timeSymbol, int recordId, int timeSheetEventId)
        {
            try
            {
                var useDate = new DateTime(startDate.SelectedDate.Year, startDate.SelectedDate.Month, startDate.SelectedDate.Day);
                var annual = new hr_AnnualLeaveHistory()
                {
                    RecordId = recordId,
                    TimeSheetEventId = timeSheetEventId,
                    UsedLeaveDate = useDate,
                    UsedLeaveDay = timeSymbol.WorkConvert,
                    CreatedDate = DateTime.Now,
                    CreatedBy = CurrentUser.User.UserName,
                };

                hr_AnnualLeaveHistoryServices.Create(annual);
                //update 
                UpdateAnnualLeaveConfig(recordId, annual);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Cập nhật ngày phép
        /// </summary>
        /// <param name="recordId"></param>
        /// <param name="annual"></param>
        private void UpdateAnnualLeaveConfig(int recordId, hr_AnnualLeaveHistory annual)
        {
            //update nguoc lai bang phep
            var leaveConfig =
                hr_AnnualLeaveConfigureServices.GetAnnualLeaveConfigByRecordId(recordId, annual.UsedLeaveDate.Year);
            if (leaveConfig != null)
            {
                leaveConfig.UsedLeaveDay = annual.UsedLeaveDay + leaveConfig.UsedLeaveDay;
                if (leaveConfig.AnnualLeaveDay > 0)
                {
                    leaveConfig.RemainLeaveDay = leaveConfig.AnnualLeaveDay - leaveConfig.UsedLeaveDay;
                }

                leaveConfig.EditedDate = DateTime.Now;
                leaveConfig.EditedBy = CurrentUser.User.UserName;
            }

            hr_AnnualLeaveConfigureServices.Update(leaveConfig);
        }

        /// <summary>
        /// New event one employee
        /// </summary>
        /// <param name="timeSheetEvent"></param>
        /// <param name="timeSymbol"></param>
        private void CreateNewTimeSheetEvent(hr_TimeSheetEvent timeSheetEvent, cat_TimeSheetSymbol timeSymbol)
        {
            try
            {
                timeSheetEvent.WorkConvert = timeSymbol.WorkConvert;
                timeSheetEvent.Symbol = timeSymbol.Code;
                timeSheetEvent.Description = timeSymbol.Name;
                timeSheetEvent.TypeGroup = timeSymbol.Group;
                hr_TimeSheetEventServices.Create(timeSheetEvent);

                TimeSheetEventIds = TimeSheetEventIds + "," + timeSheetEvent.Id;
                hdfTimeSheetEventIds.Text = TimeSheetEventIds;
            }
            catch (Exception e)
            {
                Dialog.ShowNotification("Có lỗi xảy ra" + e.Message);
            }
        }

        protected void btnUpdate_Click(object sender, DirectEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// Khóa hiệu chỉnh
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnLockAdjustClick(object sender, DirectEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// Mở hiệu chỉnh
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnOpenAdjustClick(object sender, DirectEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }

        private void ReloadGrid()
        {
            try
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
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }
    }
}