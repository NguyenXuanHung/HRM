using Ext.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraPrinting.Native;
using Web.Core;
using Web.Core.Framework;
using Web.Core.Framework.Common;
using Web.Core.Framework.Model;
using Web.Core.Object.Catalog;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.Catalog;
using Web.Core.Service.HumanRecord;

namespace Web.HRM.Modules.TimeKeeping
{
    public partial class TimeSheetManagement : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!ExtNet.IsAjaxRequest)
            {
                hdfTimeSheetReportId.Text = Request.QueryString["id"];
                hdfTypeTimeSheet.Text = Constant.TimesheetTypeTimeSheet;
                //init
                hdfDepartments.Text = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
                cbxMonth.SetValue(DateTime.Now.Month);
                spnYear.SetValue(DateTime.Now.Year);
                hdfYear.Text = DateTime.Now.Year.ToString();
                hdfMonth.Text = DateTime.Now.Month.ToString();

                if(!string.IsNullOrEmpty(hdfTimeSheetReportId.Text))
                {
                    var timeReport = hr_TimeSheetReportServices.GetById(Convert.ToInt32(hdfTimeSheetReportId.Text));
                    if(timeReport != null)
                    {
                        hdfDepartmentId.Text = timeReport.DepartmentId.ToString();
                        hdfMonth.Text = timeReport.Month.ToString();
                        hdfYear.Text = timeReport.Year.ToString();
                        gridTimeSheet.Title = timeReport.Title;
                    }

                    //check locked timesheet
                    CheckInitTimeSheet();
                }

                // init department
                storeDepartment.DataSource = CurrentUser.DepartmentsTree;
                storeDepartment.DataBind();

                LoadColumnInGrid();
            }
            //get number day of month
            hdfNumDayOfMonth.Text = GetNumberDayOfMonth(int.Parse(hdfYear.Text), int.Parse(hdfMonth.Text)).ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadColumnInGrid()
        {
            if(string.IsNullOrEmpty(hdfMonth.Text) ||
                (string.IsNullOrEmpty(hdfYear.Text))) return;

            var year = int.Parse(hdfYear.Text);
            var month = int.Parse(hdfMonth.Text);
            var daysInMonth = GetNumberDayOfMonth(year, month);
            var columnList = GetListColumn(daysInMonth, year, month);
            var columnIdFixedList = GetListColumnIdFixed();
            var columnIdByDayList = columnList.Select(c => c.ColumnID.ToString()).ToList();
            hdfMonth.Text = month.ToString();
            hdfYear.Text = year.ToString();
            var i = columnIdFixedList.Count;
            foreach(var item in columnList)
            {
                gridTimeSheet.ColumnModel.SetHidden(i, false);
                gridTimeSheet.ColumnModel.SetColumnHeader(i, item.Header);
                gridTimeSheet.ColumnModel.Columns[i].Css = item.Css;
                i++;
            }
            // totalCol = fixedCol + dayInMonth
            var startIndex = columnIdFixedList.Count + columnIdByDayList.Count;
            var maxIndex = columnIdFixedList.Count + 31;
            for(var j = startIndex; j < maxIndex; j++)
            {
                gridTimeSheet.ColumnModel.SetHidden(j, true);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private List<string> GetListColumnIdFixed()
        {
            var columnIdFixedList = new List<string>
            {
                "STT",
                "FullName",
                "EmployeeCode",
                "TotalActual",
                "TotalHolidayL",
                "TotalPaidLeaveT",
                "TotalUnpaidLeaveP",
                "TotalGoWorkC",
                "TotalUnleaveK",
                "TotalLateM"
            };
            return columnIdFixedList;
        }

        /// <summary>
        /// 
        /// </summary>
        [DirectMethod]
        public void CreateTimeSheet()
        {
            LoadColumnInGrid();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public int GetNumberDayOfMonth(int year, int month)
        {
            var days = DateTime.DaysInMonth(year, month);
            hdfMonth.Text = month.ToString();
            hdfYear.Text = year.ToString();
            return days;
        }

        /// <summary>
        /// TODO : giống hàm bên TimeSheetManagementManual
        /// </summary>
        /// <param name="daysInMonth"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        private List<Column> GetListColumn(int daysInMonth, int year, int month)
        {
            var listColumn = new List<Column>();
            var lstAdjust = hr_TimeSheetAdjustmentServices.GetListAdjustments(month, year);

            for(var i = 1; i <= daysInMonth; i++)
            {
                var datetime = new DateTime(year, month, i);
                var col = new Column()
                {
                    Header = "Day " + i + "</br> " + datetime.DayOfWeek,
                    DataIndex = "Day" + i,
                    ColumnID = "Day" + i,
                };
                if(datetime.DayOfWeek == DayOfWeek.Saturday || datetime.DayOfWeek == DayOfWeek.Sunday)
                {
                    col.Css = "background:#D3D3D3;";
                }
                //Color for adjustment
                foreach(var item in lstAdjust)
                {
                    if(item.Day == i)
                    {
                        col.Css = "background:#aaccf6;";
                    }
                }
                listColumn.Add(col);
            }
            return listColumn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAccept_Click(object sender, DirectEventArgs e)
        {
            try
            {
                //Chấm công theo ngày
                SaveTimeSheetForDay(sender, e);
                gridTimeSheet.Reload();
            }
            catch(Exception ex)
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
                var timeSheetEvent = new hr_TimeSheetEvent();
                var timeSheetId = 0;//Id bang cham cong
                if(!string.IsNullOrEmpty(hdfTimeSheetId.Text))
                {
                    timeSheetId = Convert.ToInt32(hdfTimeSheetId.Text);
                    if(timeSheetId == 0)
                    {
                        var timeSheet = new hr_TimeSheet();
                        if(!string.IsNullOrEmpty(hdfDay.Text))
                            timeSheet.Day = Convert.ToInt32(hdfDay.Text);
                        if(!string.IsNullOrEmpty(hdfMonth.Text))
                            timeSheet.Month = Convert.ToInt32(hdfMonth.Text);
                        if(!string.IsNullOrEmpty(hdfYear.Text))
                            timeSheet.Year = Convert.ToInt32(hdfYear.Text);
                        timeSheet.CreatedDate = DateTime.Now;
                        timeSheet.Detail = " ";
                        if(!string.IsNullOrEmpty(hdfRecordId.Text))
                        {
                            timeSheet.RecordId = Convert.ToInt32(hdfRecordId.Text);
                        }
                        timeSheet.TimeSheetCode = hdfTimeSheetCode.Text;
                        hr_TimeSheetServices.Create(timeSheet);
                        timeSheetEvent.TimeSheetId = timeSheet.Id;
                        timeSheetId = timeSheet.Id;
                    }
                    else
                    {
                        timeSheetEvent.TimeSheetId = timeSheetId;
                    }
                }
                timeSheetEvent.CreatedDate = DateTime.Now;
                //Add moi event
                AddNewTimeSheetEvent(timeSheetEvent);

                var timeSheetEdit = hr_TimeSheetServices.GetById(timeSheetId);
                //update timesheet
                UpdateTimeSheet(timeSheetEdit);
            }
            catch(Exception ex)
            {
                Dialog.ShowNotification("Có lỗi xảy ra" + ex.Message);
            }
        }

        /// <summary>
        /// Add moi timeSheetEvent
        /// </summary>
        /// <param name="timeSheetEvent"></param>
        private void AddNewTimeSheetEvent(hr_TimeSheetEvent timeSheetEvent)
        {
            try
            {
                //var description = string.Empty;
                var rowSelecteds = RowSelectionModel2.SelectedRows;
                if(rowSelecteds != null && rowSelecteds.Count > 0)
                {
                    //Lay thong tin tu bang ky hieu cham cong cat_TimeSheetSymbol
                    foreach(var item in rowSelecteds)
                    {
                        var timeSymbol = cat_TimeSheetSymbolServices.GetById(int.Parse("0" + item.RecordID));
                        if(timeSymbol != null)
                        {
                            timeSheetEvent.SymbolDisplay = "<span class='badge' title='{0}' style='background:{1}'>{2}</span>".FormatWith(timeSymbol.Name, timeSymbol.SymbolColor, timeSymbol.Code);
                            CreateNewTimeSheetEvent(timeSheetEvent, timeSymbol);
                            ////Nhóm ký hiệu công tác
                            //if (timeSymbol.Group == Constant.Timesheet_Work)
                            //{
                            //    timeSheetEvent.SymbolDisplay = "<span class='badge badge-{0}' title='{1}' >{2}</span>".FormatWith(timeSymbol.Group, timeSymbol.Code, timeSymbol.Name);
                            //    CreateNewTimeSheetEvent(timeSheetEvent, timeSymbol);
                            //}
                            ////Nhóm ký hiệu hưởng lương
                            //if (timeSymbol.Group == Constant.TimesheetPaySalary)
                            //{
                            //    timeSheetEvent.SymbolDisplay = "<span class='badge badge-normal' title='{1}'>{0}</span>".FormatWith(timeSymbol.Code, timeSymbol.Name);
                            //    CreateNewTimeSheetEvent(timeSheetEvent, timeSymbol);
                            //}
                            ////Nhóm ký hiệu không hưởng lương
                            //if (timeSymbol.Group == Constant.TimesheetNotPaySalary)
                            //{
                            //    timeSheetEvent.SymbolDisplay = "<span class='badge badge-red' title='{1}'>{0}</span>".FormatWith(timeSymbol.Code, timeSymbol.Name);
                            //    CreateNewTimeSheetEvent(timeSheetEvent, timeSymbol);
                            //}
                            ////Nhóm ký hiệu muộn
                            //if (timeSymbol.Group == Constant.TimesheetLate)
                            //{
                            //    timeSheetEvent.SymbolDisplay = "<span class='badge badge-late' title='{1}'>{0}</span>".FormatWith(timeSymbol.Code, timeSymbol.Name);
                            //    CreateNewTimeSheetEvent(timeSheetEvent, timeSymbol);
                            //}
                            ////Nhóm ký hiệu nghỉ lễ
                            //if (timeSymbol.Group == Constant.TimesheetHoliday)
                            //{
                            //    timeSheetEvent.SymbolDisplay = "<span class='badge badge-holiday' title='{1}'>{0}</span>".FormatWith(timeSymbol.Code, timeSymbol.Name);
                            //    CreateNewTimeSheetEvent(timeSheetEvent, timeSymbol);
                            //}
                            ////Nhóm ký hiệu nghỉ phép
                            //if (timeSymbol.Group == Constant.TimesheetLeave)
                            //{
                            //    timeSheetEvent.SymbolDisplay = "<span class='badge badge-leave' title='{1}'>{0}</span>".FormatWith(timeSymbol.Code, timeSymbol.Name);
                            //    CreateNewTimeSheetEvent(timeSheetEvent, timeSymbol);
                            //}
                            ////Nhóm ký hiệu không phép
                            //if (timeSymbol.Group == Constant.TimesheetNotLeave)
                            //{
                            //    timeSheetEvent.SymbolDisplay = "<span class='badge badge-notleave' title='{1}'>{0}</span>".FormatWith(timeSymbol.Code, timeSymbol.Name);
                            //    CreateNewTimeSheetEvent(timeSheetEvent, timeSymbol);
                            //}
                            ////Nhóm ký hiệu thêm giờ
                            //if (timeSymbol.Group == Constant.TimesheetOverTime)
                            //{
                            //    timeSheetEvent.SymbolDisplay = "<span class='badge badge-overtime' title='{1}'>{0}</span>".FormatWith(timeSymbol.Code, timeSymbol.Name);
                            //    CreateNewTimeSheetEvent(timeSheetEvent, timeSymbol);
                            //}
                            ////Nhóm ký hiệu tăng ca ngày
                            //if (timeSymbol.Group == Constant.TimesheetOverTimeDay)
                            //{
                            //    timeSheetEvent.SymbolDisplay = "<span class='badge badge-overtimeday' title='{1}'>{0}</span>".FormatWith(timeSymbol.Code, timeSymbol.Name);
                            //    CreateNewTimeSheetEvent(timeSheetEvent, timeSymbol);
                            //} 
                            ////Nhóm ký hiệu tăng ca đêm
                            //if (timeSymbol.Group == Constant.TimesheetOverTimeNight)
                            //{
                            //    timeSheetEvent.SymbolDisplay = "<span class='badge badge-overtimenight' title='{1}'>{0}</span>".FormatWith(timeSymbol.Code, timeSymbol.Name);
                            //    CreateNewTimeSheetEvent(timeSheetEvent, timeSymbol);
                            //}
                            ////Nhóm ký hiệu tăng ca ngày lễ
                            //if (timeSymbol.Group == Constant.TimesheetOverTimeHoliday)
                            //{
                            //    timeSheetEvent.SymbolDisplay = "<span class='badge badge-overtimeholiday' title='{1}'>{0}</span>".FormatWith(timeSymbol.Code, timeSymbol.Name);
                            //    CreateNewTimeSheetEvent(timeSheetEvent, timeSymbol);
                            //} 
                            ////Nhóm ký hiệu tăng ca ngày nghỉ
                            //if (timeSymbol.Group == Constant.TimesheetOverTimeWeekend)
                            //{
                            //    timeSheetEvent.SymbolDisplay = "<span class='badge badge-overtimeweekend' title='{1}'>{0}</span>".FormatWith(timeSymbol.Code, timeSymbol.Name);
                            //    CreateNewTimeSheetEvent(timeSheetEvent, timeSymbol);
                            //}

                            //Nhóm ký hiệu mặc định
                            CreateGroupTimeSheetDefault(timeSymbol, timeSheetEvent);
                        }
                    }
                }
            }
            catch(Exception ex)
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
            timeSheetEvent.SymbolDisplay = "<span class='badge' title='{0}' style='background:{1}'>{2}</span>".FormatWith(timeSymbol.Name, timeSymbol.SymbolColor, timeSymbol.Code);
            CreateNewTimeSheetEvent(timeSheetEvent, timeSymbol);
            //switch (timeSymbol.Group)
            //{

            //    case Constant.TimeSheetDefault1:
            //        timeSheetEvent.SymbolDisplay = "<span class='badge badge-default1' title='{1}'>{0}</span>".FormatWith(timeSymbol.Code, timeSymbol.Name);
            //        CreateNewTimeSheetEvent(timeSheetEvent, timeSymbol);
            //        break;
            //    case Constant.TimesheetDefault2:
            //        timeSheetEvent.SymbolDisplay = "<span class='badge badge-default2' title='{1}'>{0}</span>".FormatWith(timeSymbol.Code, timeSymbol.Name);
            //        CreateNewTimeSheetEvent(timeSheetEvent, timeSymbol);
            //        break;
            //    case Constant.TimesheetDefault3:
            //        timeSheetEvent.SymbolDisplay = "<span class='badge badge-default3' title='{1}'>{0}</span>".FormatWith(timeSymbol.Code, timeSymbol.Name);
            //        CreateNewTimeSheetEvent(timeSheetEvent, timeSymbol);
            //        break;
            //    case Constant.TimesheetDefault4:
            //        timeSheetEvent.SymbolDisplay = "<span class='badge badge-default4' title='{1}'>{0}</span>".FormatWith(timeSymbol.Code, timeSymbol.Name);
            //        CreateNewTimeSheetEvent(timeSheetEvent, timeSymbol);
            //        break;
            //    case Constant.TimesheetDefault5:
            //        timeSheetEvent.SymbolDisplay = "<span class='badge badge-default5' title='{1}'>{0}</span>".FormatWith(timeSymbol.Code, timeSymbol.Name);
            //        CreateNewTimeSheetEvent(timeSheetEvent, timeSymbol);
            //        break;
            //    case Constant.TimesheetDefault6:
            //        timeSheetEvent.SymbolDisplay = "<span class='badge badge-default6' title='{1}'>{0}</span>".FormatWith(timeSymbol.Code, timeSymbol.Name);
            //        CreateNewTimeSheetEvent(timeSheetEvent, timeSymbol);
            //        break;
            //    case Constant.TimesheetDefault7:
            //        timeSheetEvent.SymbolDisplay = "<span class='badge badge-default7' title='{1}'>{0}</span>".FormatWith(timeSymbol.Code, timeSymbol.Name);
            //        CreateNewTimeSheetEvent(timeSheetEvent, timeSymbol);
            //        break;
            //    case Constant.TimesheetDefault8:
            //        timeSheetEvent.SymbolDisplay = "<span class='badge badge-default8' title='{1}'>{0}</span>".FormatWith(timeSymbol.Code, timeSymbol.Name);
            //        CreateNewTimeSheetEvent(timeSheetEvent, timeSymbol);
            //        break;
            //    case Constant.TimesheetDefault9:
            //        timeSheetEvent.SymbolDisplay = "<span class='badge badge-default9' title='{1}'>{0}</span>".FormatWith(timeSymbol.Code, timeSymbol.Name);
            //        CreateNewTimeSheetEvent(timeSheetEvent, timeSymbol);
            //        break;
            //    case Constant.TimesheetDefault10:
            //        timeSheetEvent.SymbolDisplay = "<span class='badge badge-default10' title='{1}'>{0}</span>".FormatWith(timeSymbol.Code, timeSymbol.Name);
            //        CreateNewTimeSheetEvent(timeSheetEvent, timeSymbol);
            //        break;
            //    case Constant.TimesheetDefault11:
            //        timeSheetEvent.SymbolDisplay = "<span class='badge badge-default11' title='{1}'>{0}</span>".FormatWith(timeSymbol.Code, timeSymbol.Name);
            //        CreateNewTimeSheetEvent(timeSheetEvent, timeSymbol);
            //        break;
            //    case Constant.TimesheetDefault12:
            //        timeSheetEvent.SymbolDisplay = "<span class='badge badge-default12' title='{1}'>{0}</span>".FormatWith(timeSymbol.Code, timeSymbol.Name);
            //        CreateNewTimeSheetEvent(timeSheetEvent, timeSymbol);
            //        break;
            //    case Constant.TimesheetDefault13:
            //        timeSheetEvent.SymbolDisplay = "<span class='badge badge-default13' title='{1}'>{0}</span>".FormatWith(timeSymbol.Code, timeSymbol.Name);
            //        CreateNewTimeSheetEvent(timeSheetEvent, timeSymbol);
            //        break;
            //    case Constant.TimesheetDefault14:
            //        timeSheetEvent.SymbolDisplay = "<span class='badge badge-default14' title='{1}'>{0}</span>".FormatWith(timeSymbol.Code, timeSymbol.Name);
            //        CreateNewTimeSheetEvent(timeSheetEvent, timeSymbol);
            //        break;
            //}
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
                //create new event
                timeSheetEvent.StatusId = EventStatus.Active;
                timeSheetEvent.WorkConvert = timeSymbol.WorkConvert;
                timeSheetEvent.Symbol = timeSymbol.Code;
                timeSheetEvent.Description = timeSymbol.Name;
                timeSheetEvent.TypeGroup = timeSymbol.Group;
                hr_TimeSheetEventServices.Create(timeSheetEvent);

                var timeSheet = hr_TimeSheetServices.GetById(timeSheetEvent.TimeSheetId);

                if(timeSymbol.Group == Constant.TimesheetLeave)
                {
                    //create history
                    var useDate = new DateTime(Convert.ToInt32(hdfYear.Text), Convert.ToInt32(hdfMonth.Text),
                        Convert.ToInt32(hdfDay.Text));
                    var annual = new hr_AnnualLeaveHistory()
                    {
                        RecordId = Convert.ToInt32(hdfRecordId.Text),
                        TimeSheetEventId = timeSheetEvent.Id,
                        UsedLeaveDate = useDate,
                        UsedLeaveDay = timeSymbol.WorkConvert,
                        CreatedDate = DateTime.Now,
                        CreatedBy = CurrentUser.User.UserName,
                    };

                    hr_AnnualLeaveHistoryServices.Create(annual);
                    //Update
                    UpdateAnnualLeaveConfig(timeSheet, annual);
                }

                //Adjust timesheet
                if(!string.IsNullOrEmpty(hdfTimeSheetCode.Text) && !string.IsNullOrEmpty(hdfDay.Text))
                {
                    var timeAdjust = new hr_TimeSheetAdjustment()
                    {
                        RecordId = Convert.ToInt32(hdfRecordId.Text),
                        TimeSheetCode = hdfTimeSheetCode.Text,
                        TimeSheetEventIds = timeSheetEvent.Id.ToString(),
                        TimeSheetReportId = timeSheet.TimeSheetReportId,
                        Day = Convert.ToInt32(hdfDay.Text),
                        Month = hdfMonth.Text != null ? Convert.ToInt32(hdfMonth.Text) : 0,
                        Year = hdfYear.Text != null ? Convert.ToInt32(hdfYear.Text) : DateTime.Now.Year,
                        Reason = timeSymbol.Name,
                        CreatedDate = DateTime.Now,
                        CreatedBy = CurrentUser.User.UserName,
                    };

                    hr_TimeSheetAdjustmentServices.Create(timeAdjust);
                }
            }
            catch(Exception e)
            {
                Dialog.ShowNotification("Có lỗi xảy ra" + e.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeSheet"></param>
        /// <param name="annual"></param>
        private void UpdateAnnualLeaveConfig(hr_TimeSheet timeSheet, hr_AnnualLeaveHistory annual)
        {
            //update nguoc lai bang phep
            var leaveConfig =
                hr_AnnualLeaveConfigureServices.GetAnnualLeaveConfigByRecordId(
                    timeSheet.RecordId, annual.UsedLeaveDate.Year);
            if(leaveConfig != null)
            {
                leaveConfig.UsedLeaveDay = annual.UsedLeaveDay + leaveConfig.UsedLeaveDay;
                if(leaveConfig.AnnualLeaveDay > 0)
                {
                    leaveConfig.RemainLeaveDay = leaveConfig.AnnualLeaveDay - leaveConfig.UsedLeaveDay;
                }

                leaveConfig.EditedDate = DateTime.Now;
                leaveConfig.EditedBy = CurrentUser.User.UserName;
            }

            hr_AnnualLeaveConfigureServices.Update(leaveConfig);
        }

        /// <summary>
        /// 
        /// </summary>
        [DirectMethod]
        public void InitWindowTimeSheet()
        {
            if(!string.IsNullOrEmpty(hdfIsLocked.Text) && hdfIsLocked.Text == "true")
            {
                return;
            }
            var adjustDate = hdfDay.Text + '/' + hdfMonth.Text + '/' + hdfYear.Text;
            var dayOfWeek = new DateTime(Convert.ToInt32(hdfYear.Text), Convert.ToInt32(hdfMonth.Text), Convert.ToInt32(hdfDay.Text))
                .DayOfWeek.ToString();
            txtAdjustDate.Text = adjustDate;
            txtDayOfWeek.Text = dayOfWeek;
            txtUpdateAdjustDate.Text = adjustDate;
            txtUpdateDayOfWeek.Text = dayOfWeek;
            if(!string.IsNullOrEmpty(hdfTimeSheetId.Text))
            {
                var timeSheeet = hr_TimeSheetServices.GetById(Convert.ToInt32(hdfTimeSheetId.Text));
                if(timeSheeet != null)
                {
                    txtTimeLogs.Text = timeSheeet.TimeLogs;
                }
            }

            gridUpdateTimeSheet.Reload();
            btnDeleteUpdateTimeSheet.Disabled = true;
            wdUpdateTimeSheet.Show();
        }


        /// <summary>
        /// Khóa bảng công
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnLockTimeSheetClick(object sender, DirectEventArgs e)
        {
            try
            {
                //get list timesheet
                var timeSheets = GetAllTimeSheet();

                //lock
                foreach(var item in timeSheets)
                {
                    item.IsLocked = true;
                    hr_TimeSheetServices.Update(item);
                }

                //reload
                ReloadGrid();
            }
            catch(Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private List<hr_TimeSheet> GetAllTimeSheet()
        {
            return hr_TimeSheetServices.GetAllTimeSheets(Convert.ToInt32(hdfMonth.Text), Convert.ToInt32(hdfYear.Text), Constant.TimesheetTypeTimeSheet, Convert.ToInt32(hdfTimeSheetReportId.Text));
        }

        /// <summary>
        /// Mở bảng công
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnOpenTimeSheetClick(object sender, DirectEventArgs e)
        {
            try
            {
                //get list timesheet
                var timeSheets = GetAllTimeSheet();

                //open
                foreach(var item in timeSheets)
                {
                    item.IsLocked = false;
                    hr_TimeSheetServices.Update(item);
                }

                //reload
                ReloadGrid();
            }
            catch(Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void ReloadGrid()
        {
            try
            {
                //get list timesheet
                var timeSheets = GetAllTimeSheet();
                foreach(var item in timeSheets)
                {
                    if(item.IsLocked == true)
                    {
                        if(btnOpenTimeSheet.Visible)
                            btnOpenTimeSheet.Show();
                        if(btnLockTimeSheet.Visible)
                            btnLockTimeSheet.Hide();
                        wdTimeSheetAdd.Hide();
                        hdfIsLocked.Text = "true";
                    }
                    else
                    {
                        if(btnOpenTimeSheet.Visible)
                            btnOpenTimeSheet.Hide();
                        if(btnLockTimeSheet.Visible)
                            btnLockTimeSheet.Show();
                        hdfIsLocked.Text = "false";
                    }
                    break;
                }

                gridTimeSheet.Reload();
            }
            catch(Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void CheckInitTimeSheet()
        {
            var timeSheets = GetAllTimeSheet();
            foreach(var item in timeSheets)
            {
                if(item.IsLocked == true)
                {
                    Dialog.Alert("Bảng hiệu chỉnh đã khóa. Bạn không được phép thao tác");
                    wdTimeSheetAdd.Hide();
                    btnLockTimeSheet.Hide();
                    btnOpenTimeSheet.Show();
                    hdfIsLocked.Text = "true";
                    return;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDeleteTimeSheetManage_Click(object sender, EventArgs e)
        {
            try
            {
                if(!string.IsNullOrEmpty(hdfTimeSheetReportListID.Text))
                {
                    hr_TimeSheetReportServices.Delete(Convert.ToInt32(hdfTimeSheetReportListID.Text));

                    //update timesheet
                    UpdateTimeSheetDelete(Convert.ToInt32(hdfTimeSheetReportListID.Text));

                    grpTimeSheetReportList.Reload();
                    gridTimeSheet.Reload();
                }

            }
            catch(Exception ex)
            {
                Dialog.ShowNotification("Có lỗi xảy ra" + ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeReportId"></param>
        private void UpdateTimeSheetDelete(int timeReportId)
        {
            var times = hr_TimeSheetServices.GetAllTimeSheets(null, null, Constant.TimesheetTypeTimeSheet, timeReportId);
            foreach(var item in times)
            {
                //delete Event
                DeleteTimeSheetEvent(item.Id);

                //delete timeSheet
                hr_TimeSheetServices.Delete(item.Id);
            }
        }

        /// <summary>
        /// Delete event by TimeSheetId
        /// </summary>
        /// <param name="timeSheetId"></param>
        private void DeleteTimeSheetEvent(int timeSheetId)
        {
            try
            {
                var events = hr_TimeSheetEventServices.GetAllEventsByCondition(timeSheetId, EventStatus.Active, null);
                foreach(var itemEvent in events)
                {
                    hr_TimeSheetEventServices.Delete(itemEvent.Id);
                }
            }
            catch(Exception ex)
            {
                Dialog.ShowNotification("Có lỗi xảy ra" + ex.Message);
            }
        }

        /// <summary>
        /// Tạo mới bảng chấm công
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CreateTimeSheetReport_Click(object sender, EventArgs e)
        {
            try
            {
                var timeReport = new hr_TimeSheetReport {CreatedDate = DateTime.Now};
                if(!string.IsNullOrEmpty(hdfDepartmentId.Text))
                    timeReport.DepartmentId = Convert.ToInt32(hdfDepartmentId.Text);
                if(!string.IsNullOrEmpty(hdfMonth.Text))
                    timeReport.Month = Convert.ToInt32(hdfMonth.Text);
                if(!string.IsNullOrEmpty(hdfYear.Text))
                    timeReport.Year = Convert.ToInt32(hdfYear.Text);
                if(!string.IsNullOrEmpty(txtWorkInMonth.Text))
                    timeReport.WorkInMonth = double.Parse(txtWorkInMonth.Text);
                timeReport.Title = txtTitleTimeSheetReport.Text;
                timeReport.CreatedDate = DateTime.Now;
                timeReport.Type = Constant.TimesheetTypeTimeSheet;
                hr_TimeSheetReportServices.Create(timeReport);

                //create timesheet by selected department
                CreateTimeSheetByDepartment(timeReport.Id);


                grpTimeSheetReportList.Reload();
            }
            catch(Exception ex)
            {
                Dialog.ShowNotification("Có lỗi xảy ra" + ex.Message);
            }
        }

        /// <summary>
        /// Tạo bảng chấm công theo đơn vị lựa chọn
        /// </summary>
        /// <param name="timeReportId"></param>
        private void CreateTimeSheetByDepartment(int timeReportId)
        {
            var timeSheets = hr_TimeSheetServices.GetAllTimeSheets(Convert.ToInt32(hdfMonth.Text),
                Convert.ToInt32(hdfYear.Text), Constant.TimesheetTypeTimeSheet, null);

            if(!string.IsNullOrEmpty(hdfDepartmentId.Text))
            {
                var rootId = 0;
                var rootParentId = 0;
                var selectedDepartment = "{0},".FormatWith(hdfDepartmentId.Text);
                if(int.TryParse(hdfDepartmentId.Text, out var parseId))
                {
                    rootId = parseId;
                }

                var objDept = CurrentUser.RootDepartment;
                if(objDept != null)
                {
                    rootParentId = objDept.Id;
                }

                var lstDepartment = cat_DepartmentServices.GetTree(rootId).Select(d => d.Id).ToList();
                if(lstDepartment.Count > 0)
                {
                    selectedDepartment = lstDepartment.Aggregate(selectedDepartment, (current, d) => current + "{0},".FormatWith(d));
                }

                if(rootId != rootParentId)
                {
                    selectedDepartment = "{0}".FormatWith(selectedDepartment.TrimEnd(','));
                }
                else
                {
                    selectedDepartment = "{0}".FormatWith(selectedDepartment.TrimEnd(',').Remove(0, 2));
                }

                //Get list employee by department selected
                var listEmp = hr_RecordServices.GetAllEmployee(selectedDepartment, null, null, null).Select(d => d.Id).ToList();

                if(listEmp.Count > 0)
                {
                    foreach(var item in timeSheets)
                    {
                        if(listEmp.Contains(item.RecordId))
                        {
                            var newTimeSheet = item;
                            newTimeSheet.TimeSheetReportId = timeReportId;
                            newTimeSheet.CreatedDate = DateTime.Now;

                            //Create
                            hr_TimeSheetServices.Create(newTimeSheet);

                            var objEvent = JSON.Deserialize<List<TimeSheetEventModel>>(newTimeSheet.Detail);
                            //Create timeSheetEvent
                            foreach(var itemEvent in objEvent)
                            {
                                var timeEvent = new hr_TimeSheetEvent
                                {
                                    StatusId = EventStatus.Active,
                                    TypeGroup = itemEvent.TypeGroup,
                                    Description = itemEvent.Description,
                                    WorkConvert = itemEvent.WorkConvert,
                                    MoneyConvert = itemEvent.MoneyConvert,
                                    TimeConvert = itemEvent.TimeConvert,
                                    Symbol = itemEvent.Symbol,
                                    SymbolDisplay = itemEvent.SymbolDisplay,
                                    TimeSheetId = newTimeSheet.Id,
                                    CreatedDate = DateTime.Now,
                                    CreatedBy = CurrentUser.User.UserName,
                                };


                                hr_TimeSheetEventServices.Create(timeEvent);
                            }

                            //update timesheet
                            UpdateTimeSheet(newTimeSheet);
                        }
                    }
                }

            }
        }

        /// <summary>
        /// 
        /// </summary>
        [DirectMethod]
        public void UpdateTimeSheetBoard()
        {
            try
            {
                if(!string.IsNullOrEmpty((hdfTimeSheetReportListID.Text)))
                {
                    var timeBoard = hr_TimeSheetReportServices.GetById(Convert.ToInt32(hdfTimeSheetReportListID.Text));
                    if(timeBoard != null)
                    {

                    }
                    hr_TimeSheetReportServices.Update(timeBoard);
                    storeListTimeSheetManage.CommitChanges();
                    grpTimeSheetReportList.Reload();
                }

            }
            catch(Exception ex)
            {
                Dialog.ShowNotification("Có lỗi xảy ra" + ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeReportId"></param>
        private void UpdateTimeSheet(int timeReportId)
        {
            var timeSheets = hr_TimeSheetServices.GetAllTimeSheets(Convert.ToInt32(hdfMonth.Text),
                Convert.ToInt32(hdfYear.Text), Constant.TimesheetTypeTimeSheet, null);

            if(!string.IsNullOrEmpty(hdfDepartmentId.Text))
            {
                //Get list employee by department selected
                var listEmp = hr_RecordServices.GetAllEmployee(hdfDepartmentId.Text, null, null, null).Select(d => d.Id).ToList();
                if(listEmp.Count > 0)
                {
                    foreach(var item in timeSheets)
                    {
                        if(listEmp.Contains(item.RecordId))
                        {
                            item.TimeSheetReportId = timeReportId;
                            hr_TimeSheetServices.Update(item);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAcceptTimeSheetBoard_Click(object sender, EventArgs e)
        {
            try
            {
                var id = hdfSelectTimeSheetReportId.Text;
                Response.Redirect("TimeSheetManagement.aspx?id=" + id);
            }
            catch(Exception ex)
            {
                Dialog.ShowNotification("Có lỗi xảy ra" + ex.Message);
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
                if(!string.IsNullOrEmpty(hdfUpdateTimeSheetEventId.Text))
                {
                    var eventTime = hr_TimeSheetEventServices.GetById(Convert.ToInt32(hdfUpdateTimeSheetEventId.Text));
                    if(eventTime != null)
                    {
                        eventTime.StatusId = EventStatus.Delete;
                        eventTime.EditedDate = DateTime.Now;
                        eventTime.EditedBy = CurrentUser.User.UserName;
                    }
                    //update
                    hr_TimeSheetEventServices.Update(eventTime);
                    if(!string.IsNullOrEmpty(hdfTimeSheetId.Text))
                    {
                        var timeSheet = hr_TimeSheetServices.GetById(Convert.ToInt32(hdfTimeSheetId.Text));
                        if(timeSheet != null)
                        {
                            UpdateTimeSheet(timeSheet);
                            if(eventTime.TypeGroup == Constant.TimesheetLeave)
                            {
                                var dateUse = new DateTime(timeSheet.Year, timeSheet.Month, timeSheet.Day);
                                var annualHistory =
                                    hr_AnnualLeaveHistoryServices.GetAnnualHistoryByCondition(dateUse,
                                        timeSheet.RecordId, eventTime.Id);
                                if(annualHistory != null)
                                {
                                    var day = -annualHistory.UsedLeaveDay;
                                    annualHistory.UsedLeaveDay = day;
                                    hr_AnnualLeaveHistoryServices.Update(annualHistory);
                                    UpdateAnnualLeaveConfig(timeSheet, annualHistory);
                                }

                            }
                        }
                    }

                    gridUpdateTimeSheet.Reload();
                    gridTimeSheet.Reload();
                }
            }
            catch(Exception ex)
            {
                Dialog.ShowNotification("Có lỗi xảy ra" + ex.Message);
            }
        }

        /// <summary>
        /// update save DB
        /// </summary>
        /// <param name="timeSheet"></param>
        private void UpdateTimeSheet(hr_TimeSheet timeSheet)
        {
            var events = hr_TimeSheetEventServices.GetAllEventsByCondition(timeSheet.Id, EventStatus.Active, null);
            EditWorkConvertTimeSheet(events, timeSheet);
            timeSheet.Detail = events.ToJson();
            timeSheet.EditedDate = DateTime.Now;
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
            var timeConvert = 0.0;
            foreach(var itemEvent in events)
            {
                workConvert += itemEvent.WorkConvert;
                moneyConvert += itemEvent.MoneyConvert;
                if(itemEvent.TimeConvert != null && itemEvent.TypeGroup == Constant.TimesheetOverTime)
                {
                    timeConvert += itemEvent.TimeConvert.Value;
                }
            }

            timeSheet.WorkConvert = workConvert;
            timeSheet.MoneyConvert = moneyConvert;
            timeSheet.OverTimeConvert = timeConvert;
        }
    }
}
