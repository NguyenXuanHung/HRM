using System;
using System.Globalization;
using Ext.Net;
using Web.Core.Framework;
using Web.Core;
using Web.Core.Object.HumanRecord;
using System.Linq;
using SoftCore;
using Web.Core.Service.Catalog;
using Web.Core.Service.HumanRecord;

namespace Web.HJM.Modules.ProfileHuman.Management
{
    public partial class TimeSheetWorkShiftManagement : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ExtNet.IsAjaxRequest)
            {
                hdfDepartments.Text = string.Join(",", CurrentUser.Departments.Select(d => d.Id));

            }

            if (btnEdit.Visible)
            {
                gridTime.Listeners.RowDblClick.Handler +=
                    " if(CheckSelectedRows(gridTime)){btnUpdate.show();btnUpdateClose.hide()}";
            }

        }

        #region Event Method

        protected void InsertOrUpdate(object sender, DirectEventArgs e)
        {
            try
            {
                if (e.ExtraParams["Command"] == "Update")
                    Update();
                else
                    Insert(e);
                //reload data
                ResetForm();
                gridTime.Reload();
            }
            catch (Exception ex)
            {
                ExtNet.MessageBox.Alert("Có lỗi xảy ra", ex.Message).Show();
            }
        }

        private void Insert(DirectEventArgs e)
        {
            //validation
            if (timeSheetFromDate.SelectedValue == null)
            {
                Dialog.ShowNotification("Bạn chưa chọn ngày bắt đầu");
                return;
            }

            if (timeSheetToDate.SelectedValue == null)
            {
                Dialog.ShowNotification("Bạn chưa chọn ngày kết thúc");
                return;
            }

            if (checkGroupDay.CheckedItems.Count < 0)
            {
                Dialog.ShowNotification("Bạn chưa chọn thứ");
                return;
            }

            try
            {
                var timeSheet = new hr_TimeSheetWorkShift()
                {
                    Name = txtName.Text,
                    //Code = txtCode.Text,
                    CreatedDate = DateTime.Now,
                };

                EditData(timeSheet);

                var startDate = timeSheetFromDate.SelectedDate;
                var endDate = timeSheetToDate.SelectedDate;
                var startMonth = timeSheetFromDate.SelectedDate.Month;
                var endMonth = timeSheetToDate.SelectedDate.Month;
                var startYear = timeSheetFromDate.SelectedDate.Year;
                var endYear = timeSheetToDate.SelectedDate.Year;
                if (startMonth == endMonth)
                {
                    var dayOfMonth = DateTime.DaysInMonth(startYear, startMonth);
                    var endDay = endDate.Day;
                    if (endDate.Day > dayOfMonth)
                    {
                        endDay = dayOfMonth;
                    }

                    for (var i = startDate.Day; i <= endDay; i++)
                    {
                        var date = new DateTime(startYear, startMonth, i);

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
                                    && itemHoliday.MonthSolar == startMonth
                                    && itemHoliday.YearSolar == startYear)
                                {
                                    isProcess = true;
                                }
                            }

                            if (isProcess)
                            {
                                continue;
                            }
                        }

                        //create timeSheetWorkShift
                        CheckCreateTimeSheetWorkShift(date, timeSheet);
                    }
                }
                else
                {
                    //Chi xet trong 1 nam
                    if (startYear == endYear)
                    {
                        for (int j = startMonth; j <= endMonth; j++)
                        {
                            var dayOfMonth = DateTime.DaysInMonth(startYear, j);

                            for (var i = startDate.Day; i <= dayOfMonth; i++)
                            {
                                var date = new DateTime(startYear, j, i);
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
                                            && itemHoliday.MonthSolar == j
                                            && itemHoliday.YearSolar == startYear)
                                        {
                                            isProcess = true;
                                        }
                                    }

                                    if (isProcess)
                                    {
                                        continue;
                                    }
                                }

                                //create timeSheetWorkShift
                                CheckCreateTimeSheetWorkShift(date, timeSheet);
                            }
                        }
                    }
                }

                if (e.ExtraParams["Close"] != "True") return;
                wdTimeSheetRule.Hide();
                ResetForm();
            }
            catch (Exception ex)
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình thêm mới: {0}".FormatWith(ex.Message));
            }
        }

        /// <summary>
        /// Create by day of week
        /// </summary>
        /// <param name="date"></param>
        /// <param name="timeSheet"></param>
        private void CheckCreateTimeSheetWorkShift(DateTime date, hr_TimeSheetWorkShift timeSheet)
        {
            if (checkGroupDay.CheckedItems.Count > 0)
            {
                foreach (var item in checkGroupDay.CheckedItems)
                {
                    if (item.ID == "chkMonday" && date.DayOfWeek.ToString() == "Monday")
                        //Create
                        CreateWorkShift(timeSheet, date);
                    if (item.ID == "chkTuesday" && date.DayOfWeek.ToString() == "Tuesday")
                        //Create
                        CreateWorkShift(timeSheet, date);
                    if (item.ID == "chkWednesday" && date.DayOfWeek.ToString() == "Wednesday")
                        //Create
                        CreateWorkShift(timeSheet, date);
                    if (item.ID == "chkThursday" && date.DayOfWeek.ToString() == "Thursday")
                        //Create
                        CreateWorkShift(timeSheet, date);
                    if (item.ID == "chkFriday" && date.DayOfWeek.ToString() == "Friday")
                        //Create
                        CreateWorkShift(timeSheet, date);
                    if (item.ID == "chkSaturday" && date.DayOfWeek.ToString() == "Saturday")
                        //Create
                        CreateWorkShift(timeSheet, date);
                    if (item.ID == "chkSunday" && date.DayOfWeek.ToString() == "Sunday")
                        //Create
                        CreateWorkShift(timeSheet, date);
                }
            }
        }

        private static void CreateWorkShift(hr_TimeSheetWorkShift timeSheet, DateTime date)
        {
            timeSheet.SpecifyDate = date;
            //register table workShift
            hr_TimeSheetWorkShiftServices.Create(timeSheet);
        }

        private void EditData(hr_TimeSheetWorkShift timeSheet)
        {
            if (!string.IsNullOrEmpty(hdfGroupWorkShift.Text))
            {
                timeSheet.GroupWorkShiftId = Convert.ToInt32(hdfGroupWorkShift.Text);
            }

            if (!string.IsNullOrEmpty(inTime.Text))
                timeSheet.InTime = inTime.Text.Replace(":", "");
            if (!string.IsNullOrEmpty(outTime.Text))
                timeSheet.OutTime = outTime.Text.Replace(":", "");
            if (!string.IsNullOrEmpty(startInTime.Text))
                timeSheet.StartInTime = startInTime.Text.Replace(":", "");
            if (!string.IsNullOrEmpty(endInTime.Text))
                timeSheet.EndInTime = endInTime.Text.Replace(":", "");
            if (!string.IsNullOrEmpty(startOutTime.Text))
                timeSheet.StartOutTime = startOutTime.Text.Replace(":", "");
            if (!string.IsNullOrEmpty(endOutTime.Text))
                timeSheet.EndOutTime = endOutTime.Text.Replace(":", "");
            if (!string.IsNullOrEmpty(txtWorkConvert.Text))
                timeSheet.WorkConvert =
                    Convert.ToDouble(txtWorkConvert.Text.Replace(",", "."), CultureInfo.InvariantCulture);
            if (!string.IsNullOrEmpty(txtTimeConvert.Text))
                timeSheet.TimeConvert =
                    Convert.ToDouble(txtTimeConvert.Text.Replace(",", "."), CultureInfo.InvariantCulture);
            if (!string.IsNullOrEmpty(hdfSymbolId.Text))
            {
                timeSheet.SymbolId = Convert.ToInt32(hdfSymbolId.Text);
                timeSheet.Symbol = cat_TimeSheetSymbolServices.GetFieldValueById(timeSheet.SymbolId, "Code");
            }

            timeSheet.GroupSymbolType = hdfGroupSymbol.Text;
            timeSheet.SymbolDisplay = txtSymbolDisplay.Text;
            timeSheet.StyleColor = txtStyleColor.Text;
            if (!string.IsNullOrEmpty(txtFactorOverTime.Text))
                timeSheet.FactorOverTime =
                    Convert.ToDouble(txtFactorOverTime.Text.Replace(",", "."), CultureInfo.InvariantCulture);
            timeSheet.IsOverTime = chk_IsOverTime.Checked;
            timeSheet.IsHasInOutTime = chk_IsHasInOutTime.Checked;
        }

        private void Update()
        {
            try
            {
                if (string.IsNullOrEmpty(hdfKeyRecord.Text)) return;
                var timeSheet = hr_TimeSheetWorkShiftServices.GetById(Convert.ToInt32(hdfKeyRecord.Text));
                if (timeSheet != null)
                {
                    timeSheet.Name = txtName.Text;
                    timeSheet.Code = txtCode.Text;
                    EditData(timeSheet);
                    timeSheet.EditedDate = DateTime.Now;
                }

                hr_TimeSheetWorkShiftServices.Update(timeSheet);
            }
            catch (Exception ex)
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình cập nhật: {0}".FormatWith(ex.Message));
            }
        }

        [DirectMethod]
        public void ResetForm()
        {
            txtName.Text = "";
            timeSheetFromDate.Reset();
            timeSheetToDate.Reset();
            inTime.Reset();
            outTime.Reset();
            txtCode.Text = "";
            txtSymbolDisplay.Text = "";
            txtWorkConvert.Reset();
            txtTimeConvert.Reset();
            startInTime.Reset();
            endInTime.Reset();
            startOutTime.Reset();
            endOutTime.Reset();
            chkTetHoliday.Checked = false;
            foreach (var item in checkGroupDay.CheckedItems)
            {
                item.Checked = false;
            }

            cbxGroupWorkShift.Reset();
            chk_IsOverTime.Checked = false;
            txtStyleColor.Text = "";
            txtFactorOverTime.Reset();
            chk_IsHasInOutTime.Checked = false;
            hdfSymbolId.Reset();
            hdfGroupSymbol.Reset();
            cbxSymbol.Reset();
            cbxGroupSymbol.Reset();
        }

        protected void Delete(object sender, DirectEventArgs directEventArgs)
        {
            try
            {
                foreach (var item in RowSelectionModel1.SelectedRows)
                {
                    hr_TimeSheetWorkShiftServices.Delete(Convert.ToInt32(item.RecordID));
                }

                gridTime.Reload();
                RM.RegisterClientScriptBlock("Grid_Reload", "ReloadGrid();");
            }
            catch (Exception ex)
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình xóa: {0}".FormatWith(ex.Message));
            }
        }

        protected void EditTimeSheetRule_Click(object sender, DirectEventArgs e)
        {
            int id;
            if (!int.TryParse(hdfKeyRecord.Text, out id) || id <= 0) return;
            var timeSheet = hr_TimeSheetWorkShiftServices.GetById(id);
            if (timeSheet != null)
            {
                ResetForm();
                txtName.Text = timeSheet.Name;
                txtCode.Text = timeSheet.Code;
                txtSymbolDisplay.Text = timeSheet.SymbolDisplay;
                txtWorkConvert.Text = timeSheet.WorkConvert.ToString();
                txtTimeConvert.Text = timeSheet.TimeConvert.ToString();
                inTime.SetValue(timeSheet.InTime.Substring(0, 2) + ":" + timeSheet.InTime.Substring(2, 2));
                outTime.SetValue(timeSheet.OutTime.Substring(0, 2) + ":" + timeSheet.OutTime.Substring(2, 2));
                startInTime.SetValue(
                    timeSheet.StartInTime.Substring(0, 2) + ":" + timeSheet.StartInTime.Substring(2, 2));
                endInTime.SetValue(timeSheet.EndInTime.Substring(0, 2) + ":" + timeSheet.EndInTime.Substring(2, 2));
                startOutTime.SetValue(timeSheet.StartOutTime.Substring(0, 2) + ":" +
                                      timeSheet.StartOutTime.Substring(2, 2));
                endOutTime.SetValue(timeSheet.EndOutTime.Substring(0, 2) + ":" + timeSheet.EndOutTime.Substring(2, 2));
                hdfGroupWorkShift.Text = timeSheet.GroupWorkShiftId.ToString();
                cbxGroupWorkShift.Text =
                    hr_TimeSheetGroupWorkShiftServices.GetFieldValueById(timeSheet.GroupWorkShiftId, "Name");
                hdfSymbolId.Text = timeSheet.SymbolId.ToString();
                cbxSymbol.Text = cat_TimeSheetSymbolServices.GetFieldValueById(timeSheet.SymbolId, "Code");
                hdfGroupSymbol.Text = timeSheet.GroupSymbolType;
                var condition =
                    "[ItemType] = N'GroupSymbolType' AND [Group] = '{0}' ".FormatWith(timeSheet.GroupSymbolType);
                var groupSymbol = cat_GroupEnumServices.GetByCondition(condition);
                if (groupSymbol != null)
                    cbxGroupSymbol.Text = groupSymbol.Name;

                if (timeSheet.SpecifyDate.DayOfWeek == DayOfWeek.Monday)
                    chkMonday.Checked = true;
                if (timeSheet.SpecifyDate.DayOfWeek == DayOfWeek.Tuesday)
                    chkTuesday.Checked = true;
                if (timeSheet.SpecifyDate.DayOfWeek == DayOfWeek.Wednesday)
                    chkWednesday.Checked = true;
                if (timeSheet.SpecifyDate.DayOfWeek == DayOfWeek.Thursday)
                    chkThursday.Checked = true;
                if (timeSheet.SpecifyDate.DayOfWeek == DayOfWeek.Friday)
                    chkFriday.Checked = true;
                if (timeSheet.SpecifyDate.DayOfWeek == DayOfWeek.Saturday)
                    chkSaturday.Checked = true;
                if (timeSheet.SpecifyDate.DayOfWeek == DayOfWeek.Sunday)
                    chkSunday.Checked = true;
                chk_IsOverTime.Checked = timeSheet.IsOverTime;
                txtStyleColor.Text = timeSheet.StyleColor;
                txtFactorOverTime.Text = timeSheet.FactorOverTime.ToString();
                chk_IsHasInOutTime.Checked = timeSheet.IsHasInOutTime;
            }

            // show window
            btnUpdate.Show();
            btnUpdateClose.Hide();
            timeSheetFromDate.Disabled = true;
            timeSheetToDate.Disabled = true;
            cbxSymbol.Enabled = true;

            wdTimeSheetRule.Title = @"Cập nhật chi tiết bảng phân ca";
            wdTimeSheetRule.Show();
        }

        [DirectMethod]
        public void SetValueSelectSymbol()
        {
            if (!string.IsNullOrEmpty(hdfSymbolId.Text))
            {
                var symbol = cat_TimeSheetSymbolServices.GetById(Convert.ToInt32(hdfSymbolId.Text));
                if (symbol != null)
                {
                    txtSymbolDisplay.Text = symbol.Name;
                    txtWorkConvert.Text = symbol.WorkConvert.ToString();
                }
            }
        }


        #endregion
    }
}