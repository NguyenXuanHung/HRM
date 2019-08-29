using Ext.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Core;
using Web.Core.Framework;
using Web.Core.Object.Catalog;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.Catalog;
using Web.Core.Service.HumanRecord;

namespace Web.HJM.Modules.TimeKeeping
{
    public partial class TimeSheetManagementManual : BasePage
    {
        private const string ConstWork = "CongTac";
        private const string ConstPaySalary = "HuongLuong";
        private const string ConstNotPaySalary = "KhongLuong";
        private const string ConstNotLeave = "KhongPhep";
        private const string ConstLate = "Muon";
        private const string ConstHoliday = "NghiLe";
        private const string ConstLeave = "NghiPhep";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ExtNet.IsAjaxRequest)
            {
                hdfTimeSheetReportId.Text = Request.QueryString["id"];
                //init
                hdfDepartments.Text = "";
                hdfYear.Text = DateTime.Now.Year.ToString();
                hdfMonth.Text = DateTime.Now.Month.ToString();
                cbxMonth.SetValue(DateTime.Now.Month);
                spnYear.SetValue(DateTime.Now.Year);
                if (!string.IsNullOrEmpty(hdfTimeSheetReportId.Text))
                {
                    var timeReport = hr_TimeSheetReportServices.GetById(Convert.ToInt32(hdfTimeSheetReportId.Text));
                    if (timeReport != null)
                    {
                        hdfDepartments.Text = timeReport.DepartmentId.ToString();
                        hdfMonth.Text = timeReport.Month.ToString();
                        hdfYear.Text = timeReport.Year.ToString();
                        gridTimeSheet.Title = timeReport.Title;
                    }
                }
                // init department
                storeDepartment.DataSource = CurrentUser.DepartmentsTree;
                storeDepartment.DataBind();

                LoadColumnInGrid();
            }
            //get number day of month
            hdfNumDayOfMonth.Text = GetNumberDayOfMonth(int.Parse(hdfYear.Text), int.Parse(hdfMonth.Text)).ToString();
        }

        private void LoadColumnInGrid()
        {
            if (string.IsNullOrEmpty(hdfMonth.Text) ||
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
            foreach (var item in columnList)
            {
                gridTimeSheet.ColumnModel.SetHidden(i, false);
                gridTimeSheet.ColumnModel.SetColumnHeader(i, item.Header);
                gridTimeSheet.ColumnModel.Columns[i].Css = item.Css;
                i++;
            }
            // totalCol = fixedCol + dayInMonth
            var startIndex = columnIdFixedList.Count + columnIdByDayList.Count;
            var maxIndex = columnIdFixedList.Count + 31;
            for (var j = startIndex; j < maxIndex; j++)
            {
                gridTimeSheet.ColumnModel.SetHidden(j, true);
            }
        }

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

        [DirectMethod]
        public void CreateTimeSheet()
        {
            LoadColumnInGrid();
        }

        public int GetNumberDayOfMonth(int year, int month)
        {
            var days = DateTime.DaysInMonth(year, month);
            hdfMonth.Text = month.ToString();
            hdfYear.Text = year.ToString();
            return days;
        }

        private List<Column> GetListColumn(int daysInMonth, int year, int month)
        {
            var listColumn = new List<Column>();
            for (var i = 1; i <= daysInMonth; i++)
            {
                var datetime = new DateTime(year, month, i);
                var col = new Column()
                {
                    Header = "Day " + i + "</br> " + datetime.DayOfWeek,
                    DataIndex = "Day" + i,
                    ColumnID = "Day" + i,
                };
                if (datetime.DayOfWeek == DayOfWeek.Saturday || datetime.DayOfWeek == DayOfWeek.Sunday)
                {
                    col.Css = "background:#D3D3D3;";
                }
                listColumn.Add(col);

            }
            return listColumn;
        }

        protected void btnAccept_Click(object sender, DirectEventArgs e)
        {
            try
            {
                //Chấm công theo ngày
                SaveTimeSheetForDay(sender, e);
                gridTimeSheet.Reload();
            }
            catch (Exception ex)
            {
                Dialog.ShowNotification("Có lỗi xảy ra" + ex.Message);
            }
        }

        /// <summary>
        /// Chấm công cho tất cả nhân viên
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAccept_ClickAllEmployee(object sender, DirectEventArgs e)
        {
            try
            {
                //Chấm công ngày hôm nay cho tất cả các nhân viên
                SaveTimeSheetAllEmployee(sender, e);
                gridTimeSheet.Reload();
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
                var timeSheetEvent = new hr_TimeSheetEvent()
                {
                    StatusId = EventStatus.Active,
                    CreatedDate = DateTime.Now
                };
                var timeSheetId = 0;//Id bang cham cong
                if (!string.IsNullOrEmpty(hdfTimeSheetId.Text))
                {
                    timeSheetId = Convert.ToInt32(hdfTimeSheetId.Text);
                    if (timeSheetId == 0)
                    {
                        var timeSheet = new hr_TimeSheet();
                        if (!string.IsNullOrEmpty(hdfDay.Text))
                            timeSheet.Day = Convert.ToInt32(hdfDay.Text);
                        if (!string.IsNullOrEmpty(hdfMonth.Text))
                            timeSheet.Month = Convert.ToInt32(hdfMonth.Text);
                        if (!string.IsNullOrEmpty(hdfYear.Text))
                            timeSheet.Year = Convert.ToInt32(hdfYear.Text);
                        timeSheet.CreatedDate = DateTime.Now;
                        timeSheet.Detail = " ";
                        if (!string.IsNullOrEmpty(hdfRecordId.Text))
                        {
                            timeSheet.RecordId = Convert.ToInt32(hdfRecordId.Text);
                        }

                        if (!string.IsNullOrEmpty(hdfTimeSheetReportId.Text))
                            timeSheet.TimeSheetReportId = Convert.ToInt32(hdfTimeSheetReportId.Text);
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
                
                //Add moi event
                AddNewTimeSheetEvent(timeSheetEvent);
               
                var timeSheetEdit = hr_TimeSheetServices.GetById(timeSheetId);
                //update timesheet
                UpdateTimeSheet(timeSheetEdit);
            }
            catch (Exception ex)
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
                            }
                            //Nhóm ký hiệu không phép
                            if (timeSymbol.Group == ConstNotLeave)
                            {
                                timeSheetEvent.SymbolDisplay = "<span class='badge badge-notleave' title='{1}'>{0}</span>".FormatWith(timeSymbol.Code, timeSymbol.Name);
                                CreateNewTimeSheetEvent(timeSheetEvent, timeSymbol);
                            }
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

                if (timeSymbol.Group == ConstLeave)
                {
                    //create history
                    var useDate = new DateTime(Convert.ToInt32(hdfYear.Text), Convert.ToInt32(hdfMonth.Text),
                        Convert.ToInt32(hdfDay.Text));
                    var annual = new hr_AnnualLeaveHistory()
                    {
                        RecordId = Convert.ToInt32(hdfRecordId.Text),
                        UsedLeaveDate = useDate,
                        UsedLeaveDay = timeSymbol.WorkConvert,
                        CreatedDate = DateTime.Now,
                        CreatedBy = CurrentUser.User.UserName,
                    };

                    hr_AnnualLeaveHistoryServices.Create(annual);

                    //update nguoc lai bang phep
                    var leaveConfig =
                        hr_AnnualLeaveConfigureServices.GetAnnualLeaveConfigByRecordId(
                            Convert.ToInt32(hdfRecordId.Text), Convert.ToInt32(hdfYear.Text));
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
            }
            catch (Exception e)
            {
               Dialog.ShowNotification("Có lỗi xảy ra" + e.Message);
            }
        }

        /// <summary>
        /// Add moi timeSheetEvent all employee
        /// </summary>
        /// <param name="timeSheetEvent"></param>
        private void AddNewTimeSheetEventAllEmployee(hr_TimeSheetEvent timeSheetEvent, string typeDaySaturdayOrSunday)
        {
            try
            {
                var description = string.Empty;
                if (typeDaySaturdayOrSunday == "Saturday")
                {
                    if (cbxSaturday.SelectedItem.Value == "1")
                    {
                        description = "Một ngày công";
                        timeSheetEvent.WorkConvert = 1;
                        timeSheetEvent.Symbol = "1";
                        timeSheetEvent.SymbolDisplay = "<span class='badge badge-normal' title='{0}'>1</span>".FormatWith(description);
                    }
                    else
                    {
                        description = "Nửa ngày công";
                        timeSheetEvent.WorkConvert = 0.5;
                        timeSheetEvent.Symbol = "0.5";
                        timeSheetEvent.SymbolDisplay = "<span class='badge badge-normal' title='{0}'>0.5</span>".FormatWith(description);
                    }
                    timeSheetEvent.Description = description;
                    timeSheetEvent.TypeGroup = "HuongLuong";
                    hr_TimeSheetEventServices.Create(timeSheetEvent);
                    return;
                }
                if (typeDaySaturdayOrSunday == "Sunday")
                {
                    if (cbxSunday.SelectedItem.Value == "1")
                    {
                        description = "Một ngày công";
                        timeSheetEvent.WorkConvert = 1;
                        timeSheetEvent.Symbol = "1";
                        timeSheetEvent.SymbolDisplay = "<span class='badge badge-normal' title='{0}'>1</span>".FormatWith(description);
                    }
                    else
                    {
                        description = "Nửa ngày công";
                        timeSheetEvent.WorkConvert = 0.5;
                        timeSheetEvent.Symbol = "0.5";
                        timeSheetEvent.SymbolDisplay = "<span class='badge badge-normal' title='{0}'>0.5</span>".FormatWith(description);
                    }
                    timeSheetEvent.Description = description;
                    timeSheetEvent.TypeGroup = "HuongLuong";
                    hr_TimeSheetEventServices.Create(timeSheetEvent);
                    return;
                }

                //Nhóm ký hiệu hưởng lương
                if (groupPaySalaryAllEmp.CheckedItems.Count <= 0) return;
                foreach (var item in groupPaySalaryAllEmp.CheckedItems)
                {
                    if (item.ID == "chkFullDayAllEmp")
                    {
                        description = "Một ngày công";
                        timeSheetEvent.WorkConvert = 1;
                        timeSheetEvent.Symbol = "1";
                        timeSheetEvent.SymbolDisplay = "<span class='badge badge-normal' title='{0}'>1</span>".FormatWith(description);
                    }

                    if (item.ID != "chkHalfDayAllEmp") continue;
                    description = "Nửa ngày công";
                    timeSheetEvent.WorkConvert = 0.5;
                    timeSheetEvent.Symbol = "0.5";
                    timeSheetEvent.SymbolDisplay = "<span class='badge badge-normal' title='{0}'>0.5</span>".FormatWith(description);
                }

                timeSheetEvent.Description = description;
                timeSheetEvent.TypeGroup = "HuongLuong";
                hr_TimeSheetEventServices.Create(timeSheetEvent);
            }
            catch (Exception ex)
            {
                Dialog.ShowNotification("Có lỗi xảy ra" + ex.Message);
            }
        }

        /// <summary>
        /// Chấm công ngày hôm nay cho tất cả nhân viên
        /// </summary>
        private void SaveTimeSheetAllEmployee(object sender, DirectEventArgs e)
        {
            try
            {
                //Chọn ngày
                if (groupPaySalaryAllEmp.CheckedItems.Count <= 0) return;
                var departments = hdfDepartments.Text;
                var month = int.Parse(hdfMonth.Text);
                var year = int.Parse(hdfYear.Text);
                var arrDepartment = string.IsNullOrEmpty(departments) ? new string[] { } : departments.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                for (var j = 0; j < arrDepartment.Length; j++)
                {
                    arrDepartment[j] = "'{0}'".FormatWith(arrDepartment[j]);
                }
                //Lay tat ca ma cham cong de duyet
                var lstTimeSheetCode = hr_TimeSheetCodeServices.GetTimeSheetCodes(string.Join(",", arrDepartment), null, null, null, null);

                // Lấy tất cả các bản ghi trong bảng timesheet theo mã nhân viên, tháng, năm
                var timeSheetCodes = string.Join(",", lstTimeSheetCode.Select(code => code.RecordId));
                var arrCodes = string.IsNullOrEmpty(timeSheetCodes) ? new string[] { } : timeSheetCodes.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                for (var j = 0; j < arrCodes.Length; j++)
                {
                    arrCodes[j] = "'{0}'".FormatWith(arrCodes[j]);
                }
                var timeReportId = 0;
                if (!string.IsNullOrEmpty(hdfTimeSheetReportId.Text))
                    timeReportId = Convert.ToInt32(hdfTimeSheetReportId.Text);
                var lstTimesheet = hr_TimeSheetServices.GetByMonth(string.Join(",", arrCodes), month, year, null, timeReportId);
                var startDate = timeSheetFromDate.SelectedDate;
                var endDate = timeSheetToDate.SelectedDate;
                    
                //Voi moi ma cham cong lay timesheet
                foreach (var code in lstTimeSheetCode)
                {
                    for (var i = startDate.Day; i <= endDate.Day; i++)
                    {
                        var date = new DateTime(timeSheetFromDate.SelectedDate.Year, timeSheetFromDate.SelectedDate.Month, i);
                        var typeDay = string.Empty;
                        //Bo nghi le tet
                        //Lay danh sach ngay nghi le tet
                        //Kiem tra xem ngay do co phai la ngay le khong. Neu la ngay le thi bo qua khong di dang ky
                        if (chkTetHoliday.Checked)
                        {
                            var holiday = cat_HolidayServices.GetAll();
                            var isProcess = false;
                            foreach (var itemHoliday in holiday)
                            {
                                if (itemHoliday.DaySolar == i 
                                    && itemHoliday.MonthSolar == timeSheetFromDate.SelectedDate.Month 
                                    && itemHoliday.YearSolar == timeSheetFromDate.SelectedDate.Year)
                                {
                                    isProcess = true;
                                }
                            }
                            if (isProcess)
                            {
                                continue;
                            }
                        }

                        if (chkSaturday.Checked && date.DayOfWeek.ToString() == "Saturday") //Nếu chọn thứ 7
                        {
                            typeDay = "Saturday";
                        }
                        if (!chkSaturday.Checked && date.DayOfWeek.ToString() == "Saturday") //Nếu chọn thứ 7
                        {
                            continue;
                        }

                        //Nếu chọn CN
                        if (chkSunday.Checked && date.DayOfWeek.ToString() == "Sunday")
                        {
                            typeDay = "Sunday";
                        }
                        if (!chkSunday.Checked && date.DayOfWeek.ToString() == "Sunday")
                        {
                            continue;
                        }
                        var item = lstTimesheet.FirstOrDefault(ts => ts.RecordId == code.RecordId && ts.Day == i);
                        var timeSheetId = 0;

                        //TH timeSheet khac null cap nhat lai theo event duoc add moi vao
                        if (item != null)
                        {
                            timeSheetId = item.Id;
                            //Add moi timeSheetEvent
                            CreatNewTimeSheetEvent(item.Id, typeDay);
                        }
                        else
                        {
                            //TH timeSheet null -> tao moi
                            var timeSheet = new hr_TimeSheet();
                            EditDataTimeSheet(timeSheet, code);
                            timeSheet.Day = i;
                            if (!string.IsNullOrEmpty(hdfTimeSheetReportId.Text))
                                timeSheet.TimeSheetReportId = Convert.ToInt32(hdfTimeSheetReportId.Text);
                            hr_TimeSheetServices.Create(timeSheet);

                            if (timeSheet.Id > 0)
                            {
                                timeSheetId = timeSheet.Id;
                                //Add moi timeSheetEvent
                                CreatNewTimeSheetEvent(timeSheet.Id, typeDay);
                            }
                        }
                        
                        var timeSheetEdit = hr_TimeSheetServices.GetById(timeSheetId);
                        //update timesheet
                        UpdateTimeSheet(timeSheetEdit);
                    }
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowNotification("Có lỗi xảy ra" + ex.Message);
            }
        }

        private void CreatNewTimeSheetEvent(int id, string typeDay)
        {
            var timeSheetEvent = new hr_TimeSheetEvent
            {
                TimeSheetId = id,
                StatusId = EventStatus.Active,
                CreatedDate = DateTime.Now,
                CreatedBy = CurrentUser.User.UserName
            };
            AddNewTimeSheetEventAllEmployee(timeSheetEvent, typeDay);
        }

        private void EditDataTimeSheet(hr_TimeSheet timeSheet, hr_TimeSheetCode code)
        {
            if (!string.IsNullOrEmpty(hdfMonth.Text))
                timeSheet.Month = Convert.ToInt32(hdfMonth.Text);
            if (!string.IsNullOrEmpty(hdfYear.Text))
                timeSheet.Year = Convert.ToInt32(hdfYear.Text);
            timeSheet.RecordId = code.RecordId;
            timeSheet.TimeSheetCode = code.Code;
            timeSheet.CreatedDate = DateTime.Now;
            timeSheet.Detail = "Bình thường";
        }

        /// <summary>
        /// Tính số công cho tất cả nhân viên
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CalculateAllEmployee(object sender, EventArgs e)
        {
            try
            {
                gridTimeSheet.Reload();
            }
            catch (Exception ex)
            {
                Dialog.ShowNotification("Có lỗi xảy ra" + ex.Message);
            }
        }

        protected void DeleteTimeSheet(object sender, EventArgs e)
        {
            try
            {
                var departments = hdfDepartments.Text;
                var month = int.Parse(hdfMonth.Text);
                var year = int.Parse(hdfYear.Text);
                var arrDepartment = string.IsNullOrEmpty(departments) ? new string[] { } : departments.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                for (var j = 0; j < arrDepartment.Length; j++)
                {
                    arrDepartment[j] = "'{0}'".FormatWith(arrDepartment[j]);
                }
                //Lay tat ca ma cham cong de duyet
                var lstTimeSheetCode = hr_TimeSheetCodeServices.GetTimeSheetCodes(string.Join(",", arrDepartment), null, null, null, null);

                // Lấy tất cả các bản ghi trong bảng timesheet theo mã nhân viên, tháng, năm
                var timeSheetCodes = string.Join(",", lstTimeSheetCode.Select(code => code.RecordId));
                var arrCodes = string.IsNullOrEmpty(timeSheetCodes) ? new string[] { } : timeSheetCodes.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                for (var j = 0; j < arrCodes.Length; j++)
                {
                    arrCodes[j] = "'{0}'".FormatWith(arrCodes[j]);
                }
                var timeReportId = 0;
                if (!string.IsNullOrEmpty(hdfTimeSheetReportId.Text))
                    timeReportId = Convert.ToInt32(hdfTimeSheetReportId.Text);
                var lstTimesheet = hr_TimeSheetServices.GetByMonth(string.Join(",", arrCodes), month, year, null, timeReportId);
                //Xoa timeSheetEvent theo tung timeSheetId
                foreach (var item in lstTimesheet)
                {
                    var condition = "[TimeSheetId] = '{0}' ".FormatWith(item.Id);
                    hr_TimeSheetEventServices.Delete(condition);
                }
                //Xoa timeSheet
                var timeCondition = "[RecordId] IN ({0}) ".FormatWith(string.Join(",", arrCodes)) +
                    " AND [Month] = '{0}' ".FormatWith(month) +
                    " AND [Year] = '{0}' ".FormatWith(year) +
                    " AND [Type] IS NULL";
                hr_TimeSheetServices.Delete(timeCondition);
                gridTimeSheet.Reload();
            }
            catch (Exception ex)
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
                var timeReport = new hr_TimeSheetReport();
                timeReport.CreatedDate = DateTime.Now;
                if (!string.IsNullOrEmpty(hdfDepartmentId.Text))
                    timeReport.DepartmentId = Convert.ToInt32(hdfDepartmentId.Text);
                if (!string.IsNullOrEmpty(hdfMonth.Text))
                    timeReport.Month = Convert.ToInt32(hdfMonth.Text);
                if (!string.IsNullOrEmpty(hdfYear.Text))
                    timeReport.Year = Convert.ToInt32(hdfYear.Text);
                if (!string.IsNullOrEmpty(txtWorkInMonth.Text))
                    timeReport.WorkInMonth = double.Parse(txtWorkInMonth.Text);
                timeReport.Title = txtTitleTimeSheetReport.Text;
                timeReport.CreatedDate = DateTime.Now;
                hr_TimeSheetReportServices.Create(timeReport);
                grpTimeSheetReportList.Reload();
            }
            catch (Exception ex)
            {
                Dialog.ShowNotification("Có lỗi xảy ra" + ex.Message);
            }
        }

        [DirectMethod]
        public void UpdateTimeSheetBoard()
        {
            try
            {
                if (!string.IsNullOrEmpty((hdfTimeSheetReportListID.Text)))
                {
                    var timeBoard = hr_TimeSheetReportServices.GetById(Convert.ToInt32(hdfTimeSheetReportListID.Text));
                    if (timeBoard != null)
                    {
                        if (!string.IsNullOrEmpty(txtTitleOfTimeSheetBoard.Text))
                            timeBoard.Title = txtTitleOfTimeSheetBoard.Text;
                    }
                    hr_TimeSheetReportServices.Update(timeBoard);
                    storeListTimeSheetManage.CommitChanges();
                    grpTimeSheetReportList.Reload();
                }
                
            }
            catch (Exception ex)
            {
                Dialog.ShowNotification("Có lỗi xảy ra" + ex.Message);
            }
        }

        protected void btnDeleteTimeSheetManage_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(hdfTimeSheetReportListID.Text))
                {
                    hr_TimeSheetReportServices.Delete(Convert.ToInt32(hdfTimeSheetReportListID.Text));
                    //Xoa bang cham cong
                    var condition = "[TimeSheetReportId] = '{0}' ".FormatWith(hdfTimeSheetReportListID.Text);
                    hr_TimeSheetServices.Delete(condition);
                    grpTimeSheetReportList.Reload();
                }
                    
            }
            catch (Exception ex)
            {
                Dialog.ShowNotification("Có lỗi xảy ra" + ex.Message);
            }
        }

        protected void btnAcceptTimeSheetBoard_Click(object sender, EventArgs e)
        {
            try
            {
                var id = hdfSelectTimeSheetReportId.Text;
                Response.Redirect("TimeSheetManagementManual.aspx?id=" + id);
            }
            catch (Exception ex)
            {
                Dialog.ShowNotification("Có lỗi xảy ra" + ex.Message);
            }
        }

        [DirectMethod]
        public void InitWindowTimeSheet()
        {
            if (!string.IsNullOrEmpty(hdfIsLocked.Text) && hdfIsLocked.Text == "true")
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

            gridUpdateTimeSheet.Reload();
            btnDeleteUpdateTimeSheet.Disabled = true;
            wdUpdateTimeSheet.Show();
        }

        protected void btnDeleteTime_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(hdfUpdateTimeSheetEventId.Text))
                {
                    var eventTime = hr_TimeSheetEventServices.GetById(Convert.ToInt32(hdfUpdateTimeSheetEventId.Text));
                    if (eventTime != null)
                    {
                        eventTime.StatusId = EventStatus.Delete;
                        eventTime.EditedDate = DateTime.Now;
                        eventTime.EditedBy = CurrentUser.User.UserName;
                    }
                    //update
                    hr_TimeSheetEventServices.Update(eventTime);
                    if (!string.IsNullOrEmpty(hdfTimeSheetId.Text))
                    {
                        var timeSheet = hr_TimeSheetServices.GetById(Convert.ToInt32(hdfTimeSheetId.Text));
                        if (timeSheet != null)
                        {
                            UpdateTimeSheet(timeSheet);
                        }
                    }

                    gridUpdateTimeSheet.Reload();
                    gridTimeSheet.Reload();
                }
            }
            catch (Exception ex)
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
            var events = hr_TimeSheetEventServices.GetAllEventsByTimeSheetId(timeSheet.Id, EventStatus.Active);
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
            foreach (var itemEvent in events)
            {
                workConvert += itemEvent.WorkConvert;
                moneyConvert += itemEvent.MoneyConvert;
            }
            timeSheet.WorkConvert = workConvert;
            timeSheet.MoneyConvert = moneyConvert;
        }
    }
}
