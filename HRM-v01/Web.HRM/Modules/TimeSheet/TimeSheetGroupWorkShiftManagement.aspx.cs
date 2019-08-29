using System;
using System.Globalization;
using Ext.Net;
using Web.Core.Framework;
using System.Linq;
using Web.Core;
using Web.Core.Framework.Common;
using Web.Core.Framework.Controller.TimeSheet;

namespace Web.HRM.Modules.TimeSheet
{
    public partial class TimeSheetGroupWorkShiftManagement : BasePage
    {
        private const string TimeSheetWorkShiftUrl = "~/Modules/TimeSheet/TimeSheetWorkShiftManagement.aspx";
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ExtNet.IsAjaxRequest)
            {
                //init
                hdfDepartments.Text = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
            }
        }

        #region Event Method

        /// <summary>
        /// 
        /// </summary>
        [DirectMethod]
        public void ResetForm()
        {
            //work normal
            txtName.Reset();
            hdfNameNormal.Reset();
            txtDescriptionNormal.Reset();
            timeSheetFromDate.Reset();
            timeSheetToDate.Reset();
            inTime.Reset();
            outTime.Reset();
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
            chk_IsOverTime.Checked = false;
            chk_IsLimitMaxTime.Checked = false;
            chk_IsHasInOutTime.Checked = false;
            hdfSymbolId.Reset();
            hdfGroupSymbol.Reset();
            cbxSymbol.Reset();
            cbxGroupSymbol.Reset();
            hdfGroupWorkShift.Reset();
            cboGroup.Reset();

            //work break
            txtWorkName.Reset();
            hdfName.Reset();
            txtDescription.Reset();
            hdfSymbolWorkId.Reset();
            hdfGroupSymbolShiftId.Reset();
            foreach (var item in chk_GroupHasWork.CheckedItems)
            {
                item.Checked = false;
            }
            foreach (var item in chkDayOfWeek.CheckedItems)
            {
                item.Checked = false;
            }
            dfFromDate.Reset();
            dfToDate.Reset();
            cboSymbolWork.Reset();
            cbxGroupSymbolShift.Reset();
            txtWorkShiftConvert.Reset();
            txtTimeWorkConvert.Reset();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="directEventArgs"></param>
        protected void Delete(object sender, DirectEventArgs directEventArgs)
        {
            if (!string.IsNullOrEmpty(hdfKeyRecord.Text))
            {
                //delete timeSheetGroupWorkShift
                TimeSheetGroupWorkShiftController.Delete(Convert.ToInt32(hdfKeyRecord.Text));

                //delete timeSheetWorkShift
                TimeSheetWorkShiftController.DeleteByCondition(Convert.ToInt32(hdfKeyRecord.Text), null);
              
                gpGroupWorkShift.Reload();
            }
        }

       
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SelectGroupWorkShift_Click(object sender, DirectEventArgs e)
        {
            if (!string.IsNullOrEmpty(hdfKeyRecord.Text))
            {
                Response.Redirect(TimeSheetWorkShiftUrl + "?mId=" + MenuId + "&id=" + hdfKeyRecord.Text, true);
            }
        }

        #region TimeSheetWorkBreak

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InsertWork(object sender, DirectEventArgs e)
        {
            //create group workShift
            //init entity
            var groupModel = new TimeSheetGroupWorkShiftModel(null)
            {
                Name = txtWorkName.Text,
                StartDate = dfFromDate.SelectedDate,
                EndDate = dfToDate.SelectedDate,
                Description = txtDescription.Text,
                CreatedDate = DateTime.Now,
                CreatedBy = CurrentUser.User.UserName,
                EditedDate = DateTime.Now,
            };

            //Create
            var resultGroupModel = TimeSheetGroupWorkShiftController.Create(groupModel);
            
            //create workShift
            var model = new TimeSheetWorkShiftModel()
            {
                GroupWorkShiftId = resultGroupModel.Id,
                Type = WorkShiftType.Break,
                CreatedDate = DateTime.Now,
                CreatedBy = CurrentUser.User.UserName,
                EditedDate = DateTime.Now,
                EditedBy = " ",
            };

            //edit data
            EditDataWork(model);

            var startDate = dfFromDate.SelectedDate;
            var endDate = dfToDate.SelectedDate;
            var startMonth = startDate.Month;
            var endMonth = endDate.Month;
            var startYear = startDate.Year;
            var endYear = endDate.Year;

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
                    //Kiem tra xem ngay do co phai la ngay le khong. Neu la ngay le thi bo qua khong di dang ky
                    if (chk_HasWorkHoliday.Checked)
                    {
                        //check holiday
                        var isProcess = CheckHoliday(i, startMonth, startYear);

                        if (isProcess)
                        {
                            continue;
                        }
                    }

                    //check combo
                    CreateWorkBreak(date, model);
                }
            }
            else
            {
                //Chi xet trong 1 nam
                if (startYear == endYear)
                {
                    for (var j = startMonth; j <= endMonth; j++)
                    {
                        var dayOfMonth = DateTime.DaysInMonth(startYear, j);
                        for (var i = startDate.Day; i <= dayOfMonth; i++)
                        {
                            var date = new DateTime(startYear, j, i);
                            //Kiem tra xem ngay do co phai la ngay le khong. Neu la ngay le thi bo qua khong di dang ky
                            if (chk_HasWorkHoliday.Checked)
                            {
                                //check holiday
                                var isProcess = CheckHoliday(i, j, startYear);

                                if (isProcess)
                                {
                                    continue;
                                }
                            }
                            //check combo
                            CreateWorkBreak(date, model);
                        }
                    }
                }
            }

            //reload data
            ResetForm();
            wdTimeWork.Hide();
            gpGroupWorkShift.Reload();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="day"></param>
        /// <param name="startMonth"></param>
        /// <param name="startYear"></param>
        /// <returns></returns>
        private static bool CheckHoliday(int day, int startMonth, int startYear)
        {
            var isProcess = false;
            //Lay danh sach ngay nghi le tet
            var holiday = CatalogHolidayController.GetAll(null, null, null, false, null, null);
            foreach (var itemHoliday in holiday)
            {
                if (itemHoliday.DaySolar == day
                    && itemHoliday.MonthSolar == startMonth
                    && itemHoliday.YearSolar == startYear)
                {
                    isProcess = true;
                }
            }

            return isProcess;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <param name="model"></param>
        private void CreateWorkBreak(DateTime date, TimeSheetWorkShiftModel model)
        {
            //combobox Monday
            if (date.DayOfWeek == DayOfWeek.Monday)
            {
                if (!string.IsNullOrEmpty(tfStartMonday.SelectedItem.Value))
                {
                    //edit data for combo select day
                    EditDataComboSelectedDay(date, model, DayOfWeek.Monday);

                    //register table workShift
                    TimeSheetWorkShiftController.Create(model);
                }
            }

            //combobox Tuesday
            if (date.DayOfWeek == DayOfWeek.Tuesday)
            {
                if (!string.IsNullOrEmpty(tfStartTuesday.SelectedItem.Value))
                {
                    //edit data for combo select day
                    EditDataComboSelectedDay(date, model, DayOfWeek.Tuesday);

                    //register table workShift
                    TimeSheetWorkShiftController.Create(model);
                }
            }

            //combobox Wednesday
            if (date.DayOfWeek == DayOfWeek.Wednesday)
            {
                if (!string.IsNullOrEmpty(tfStartWednesday.SelectedItem.Value))
                {
                    //edit data for combo select day
                    EditDataComboSelectedDay(date, model, DayOfWeek.Wednesday);

                    //register table workShift
                    TimeSheetWorkShiftController.Create(model);
                }
            }

            //combobox Thursday
            if (date.DayOfWeek == DayOfWeek.Thursday)
            {
                if (!string.IsNullOrEmpty(tfStartThursday.SelectedItem.Value))
                {
                    //edit data for combo select day
                    EditDataComboSelectedDay(date, model, DayOfWeek.Thursday);

                    //register table workShift
                    TimeSheetWorkShiftController.Create(model);
                }
            }

            //combobox Friday
            if (date.DayOfWeek == DayOfWeek.Friday)
            {
                if (!string.IsNullOrEmpty(tfStartFriday.SelectedItem.Value))
                {
                    //edit data for combo select day
                    EditDataComboSelectedDay(date, model, DayOfWeek.Friday);

                    //register table workShift
                    TimeSheetWorkShiftController.Create(model);
                }
            }

            //combobox Saturday
            if (date.DayOfWeek == DayOfWeek.Saturday)
            {
                if (!string.IsNullOrEmpty(tfStartSaturday.SelectedItem.Value))
                {
                    //edit data for combo select day
                    EditDataComboSelectedDay(date, model, DayOfWeek.Saturday);

                    //register table workShift
                    TimeSheetWorkShiftController.Create(model);
                }
            }

            //combobox Sunday
            if (date.DayOfWeek == DayOfWeek.Sunday)
            {
                if (!string.IsNullOrEmpty(tfStartSunday.SelectedItem.Value))
                {
                    //edit data for combo select day
                    EditDataComboSelectedDay(date, model, DayOfWeek.Sunday);

                    //register table workShift
                    TimeSheetWorkShiftController.Create(model);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <param name="model"></param>
        /// <param name="dayOfWeek"></param>
        private void EditDataComboSelectedDay(DateTime date, TimeSheetWorkShiftModel model,
            DayOfWeek dayOfWeek)
        {
            if (dayOfWeek == DayOfWeek.Monday)
            {
                model.StartDate = new DateTime(date.Year, date.Month, date.Day, tfStartMonday.SelectedTime.Hours,
                    tfStartMonday.SelectedTime.Minutes, 0);

                var startInDate = model.StartDate.AddDays(Convert.ToInt32(cboStartInMonday.SelectedItem.Value));
                model.StartInTime = new DateTime(startInDate.Year, startInDate.Month, startInDate.Day, tfStartInMonday.SelectedTime.Hours,
                    tfStartInMonday.SelectedTime.Minutes, 0);

                var endInDate = model.StartDate.AddDays(Convert.ToInt32(cboEndInMonday.SelectedItem.Value));
                model.EndInTime = new DateTime(endInDate.Year, endInDate.Month, endInDate.Day, tfEndInMonday.SelectedTime.Hours,
                    tfEndInMonday.SelectedTime.Minutes, 0);

                var endDate = model.StartDate.AddDays(Convert.ToInt32(cboEndMonday.SelectedItem.Value));
                model.EndDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, tfEndMonday.SelectedTime.Hours,
                    tfEndMonday.SelectedTime.Minutes, 0);

                var startOutDate = endDate.AddDays(Convert.ToInt32(cboStartOutMonday.SelectedItem.Value));
                model.StartOutTime = new DateTime(startOutDate.Year, startOutDate.Month, startOutDate.Day,
                    tfStartOutMonday.SelectedTime.Hours, tfStartOutMonday.SelectedTime.Minutes, 0);

                var endOutDate = endDate.AddDays(Convert.ToInt32(cboEndOutMonday.SelectedItem.Value));
                model.EndOutTime = new DateTime(endOutDate.Year, endOutDate.Month, endOutDate.Day,
                    tfEndOutMonday.SelectedTime.Hours, tfEndOutMonday.SelectedTime.Minutes, 0);
            }

            if (dayOfWeek == DayOfWeek.Tuesday)
            {
                model.StartDate = new DateTime(date.Year, date.Month, date.Day, tfStartTuesday.SelectedTime.Hours,
                    tfStartTuesday.SelectedTime.Minutes, 0);

                var startInDate = model.StartDate.AddDays(Convert.ToInt32(cboStartInTuesday.SelectedItem.Value));
                model.StartInTime = new DateTime(startInDate.Year, startInDate.Month, startInDate.Day, tfStartInTuesday.SelectedTime.Hours,
                    tfStartInTuesday.SelectedTime.Minutes, 0);

                var endInDate = model.StartDate.AddDays(Convert.ToInt32(cboEndInTuesday.SelectedItem.Value));
                model.EndInTime = new DateTime(endInDate.Year, endInDate.Month, endInDate.Day, tfEndInTuesday.SelectedTime.Hours,
                    tfEndInTuesday.SelectedTime.Minutes, 0);

                var endDate = model.StartDate.AddDays(Convert.ToInt32(cboEndTuesday.SelectedItem.Value));
                model.EndDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, tfEndTuesday.SelectedTime.Hours,
                    tfEndTuesday.SelectedTime.Minutes, 0);

                var startOutDate = endDate.AddDays(Convert.ToInt32(cboStartOutTuesday.SelectedItem.Value));
                model.StartOutTime = new DateTime(startOutDate.Year, startOutDate.Month, startOutDate.Day,
                    tfStartOutTuesday.SelectedTime.Hours, tfStartOutTuesday.SelectedTime.Minutes, 0);

                var endOutDate = endDate.AddDays(Convert.ToInt32(cboEndOutTuesday.SelectedItem.Value));
                model.EndOutTime = new DateTime(endOutDate.Year, endOutDate.Month, endOutDate.Day,
                    tfEndOutTuesday.SelectedTime.Hours, tfEndOutTuesday.SelectedTime.Minutes, 0);
            }

            if (dayOfWeek == DayOfWeek.Wednesday)
            {
                model.StartDate = new DateTime(date.Year, date.Month, date.Day, tfStartWednesday.SelectedTime.Hours,
                    tfStartWednesday.SelectedTime.Minutes, 0);

                var startInDate = model.StartDate.AddDays(Convert.ToInt32(cboStartInWednesday.SelectedItem.Value));
                model.StartInTime = new DateTime(startInDate.Year, startInDate.Month, startInDate.Day, tfStartInWednesday.SelectedTime.Hours,
                    tfStartInWednesday.SelectedTime.Minutes, 0);

                var endInDate = model.StartDate.AddDays(Convert.ToInt32(cboEndInWednesday.SelectedItem.Value));
                model.EndInTime = new DateTime(endInDate.Year, endInDate.Month, endInDate.Day, tfEndInWednesday.SelectedTime.Hours,
                    tfEndInWednesday.SelectedTime.Minutes, 0);

                var endDate = model.StartDate.AddDays(Convert.ToInt32(cboEndWednesday.SelectedItem.Value));
                model.EndDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, tfEndWednesday.SelectedTime.Hours,
                    tfEndWednesday.SelectedTime.Minutes, 0);

                var startOutDate = endDate.AddDays(Convert.ToInt32(cboStartOutWednesday.SelectedItem.Value));
                model.StartOutTime = new DateTime(startOutDate.Year, startOutDate.Month, startOutDate.Day,
                    tfStartOutWednesday.SelectedTime.Hours, tfStartOutWednesday.SelectedTime.Minutes, 0);

                var endOutDate = endDate.AddDays(Convert.ToInt32(cboEndOutWednesday.SelectedItem.Value));
                model.EndOutTime = new DateTime(endOutDate.Year, endOutDate.Month, endOutDate.Day,
                    tfEndOutWednesday.SelectedTime.Hours, tfEndOutWednesday.SelectedTime.Minutes, 0);
            }

            if (dayOfWeek == DayOfWeek.Thursday)
            {
                model.StartDate = new DateTime(date.Year, date.Month, date.Day, tfStartThursday.SelectedTime.Hours,
                    tfStartThursday.SelectedTime.Minutes, 0);

                var startInDate = model.StartDate.AddDays(Convert.ToInt32(cboStartInThursday.SelectedItem.Value));
                model.StartInTime = new DateTime(startInDate.Year, startInDate.Month, startInDate.Day, tfStartInThursday.SelectedTime.Hours,
                    tfStartInThursday.SelectedTime.Minutes, 0);

                var endInDate = model.StartDate.AddDays(Convert.ToInt32(cboEndInThursday.SelectedItem.Value));
                model.EndInTime = new DateTime(endInDate.Year, endInDate.Month, endInDate.Day, tfEndInThursday.SelectedTime.Hours,
                    tfEndInThursday.SelectedTime.Minutes, 0);

                var endDate = model.StartDate.AddDays(Convert.ToInt32(cboEndThursday.SelectedItem.Value));
                model.EndDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, tfEndThursday.SelectedTime.Hours,
                    tfEndThursday.SelectedTime.Minutes, 0);

                var startOutDate = endDate.AddDays(Convert.ToInt32(cboStartOutThursday.SelectedItem.Value));
                model.StartOutTime = new DateTime(startOutDate.Year, startOutDate.Month, startOutDate.Day,
                    tfStartOutThursday.SelectedTime.Hours, tfStartOutThursday.SelectedTime.Minutes, 0);

                var endOutDate = endDate.AddDays(Convert.ToInt32(cboEndOutThursday.SelectedItem.Value));
                model.EndOutTime = new DateTime(endOutDate.Year, endOutDate.Month, endOutDate.Day,
                    tfEndOutThursday.SelectedTime.Hours, tfEndOutThursday.SelectedTime.Minutes, 0);
            }

            if (dayOfWeek == DayOfWeek.Friday)
            {
                model.StartDate = new DateTime(date.Year, date.Month, date.Day, tfStartFriday.SelectedTime.Hours,
                    tfStartFriday.SelectedTime.Minutes, 0);

                var startInDate = model.StartDate.AddDays(Convert.ToInt32(cboStartInFriday.SelectedItem.Value));
                model.StartInTime = new DateTime(startInDate.Year, startInDate.Month, startInDate.Day, tfStartInFriday.SelectedTime.Hours,
                    tfStartInFriday.SelectedTime.Minutes, 0);

                var endInDate = model.StartDate.AddDays(Convert.ToInt32(cboEndInFriday.SelectedItem.Value));
                model.EndInTime = new DateTime(endInDate.Year, endInDate.Month, endInDate.Day, tfEndInFriday.SelectedTime.Hours,
                    tfEndInFriday.SelectedTime.Minutes, 0);

                var endDate = model.StartDate.AddDays(Convert.ToInt32(cboEndFriday.SelectedItem.Value));
                model.EndDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, tfEndFriday.SelectedTime.Hours,
                    tfEndFriday.SelectedTime.Minutes, 0);

                var startOutDate = endDate.AddDays(Convert.ToInt32(cboStartOutFriday.SelectedItem.Value));
                model.StartOutTime = new DateTime(startOutDate.Year, startOutDate.Month, startOutDate.Day,
                    tfStartOutFriday.SelectedTime.Hours, tfStartOutFriday.SelectedTime.Minutes, 0);

                var endOutDate = endDate.AddDays(Convert.ToInt32(cboEndOutFriday.SelectedItem.Value));
                model.EndOutTime = new DateTime(endOutDate.Year, endOutDate.Month, endOutDate.Day,
                    tfEndOutFriday.SelectedTime.Hours, tfEndOutFriday.SelectedTime.Minutes, 0);
            }

            if (dayOfWeek == DayOfWeek.Saturday)
            {
                model.StartDate = new DateTime(date.Year, date.Month, date.Day, tfStartSaturday.SelectedTime.Hours,
                    tfStartSaturday.SelectedTime.Minutes, 0);

                var startInDate = model.StartDate.AddDays(Convert.ToInt32(cboStartInSaturday.SelectedItem.Value));
                model.StartInTime = new DateTime(startInDate.Year, startInDate.Month, startInDate.Day, tfStartInSaturday.SelectedTime.Hours,
                    tfStartInSaturday.SelectedTime.Minutes, 0);

                var endInDate = model.StartDate.AddDays(Convert.ToInt32(cboEndInSaturday.SelectedItem.Value));
                model.EndInTime = new DateTime(endInDate.Year, endInDate.Month, endInDate.Day, tfEndInSaturday.SelectedTime.Hours,
                    tfEndInSaturday.SelectedTime.Minutes, 0);

                var endDate = model.StartDate.AddDays(Convert.ToInt32(cboEndSaturday.SelectedItem.Value));
                model.EndDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, tfEndSaturday.SelectedTime.Hours,
                    tfEndSaturday.SelectedTime.Minutes, 0);

                var startOutDate = endDate.AddDays(Convert.ToInt32(cboStartOutSaturday.SelectedItem.Value));
                model.StartOutTime = new DateTime(startOutDate.Year, startOutDate.Month, startOutDate.Day,
                    tfStartOutSaturday.SelectedTime.Hours, tfStartOutSaturday.SelectedTime.Minutes, 0);

                var endOutDate = endDate.AddDays(Convert.ToInt32(cboEndOutSaturday.SelectedItem.Value));
                model.EndOutTime = new DateTime(endOutDate.Year, endOutDate.Month, endOutDate.Day,
                    tfEndOutSaturday.SelectedTime.Hours, tfEndOutSaturday.SelectedTime.Minutes, 0);
            }

            if (dayOfWeek == DayOfWeek.Sunday)
            {
                model.StartDate = new DateTime(date.Year, date.Month, date.Day, tfStartSunday.SelectedTime.Hours,
                    tfStartSunday.SelectedTime.Minutes, 0);

                var startInDate = model.StartDate.AddDays(Convert.ToInt32(cboStartInSunday.SelectedItem.Value));
                model.StartInTime = new DateTime(startInDate.Year, startInDate.Month, startInDate.Day, tfStartInSunday.SelectedTime.Hours,
                    tfStartInSunday.SelectedTime.Minutes, 0);

                var endInDate = model.StartDate.AddDays(Convert.ToInt32(cboEndInSunday.SelectedItem.Value));
                model.EndInTime = new DateTime(endInDate.Year, endInDate.Month, endInDate.Day, tfEndInSunday.SelectedTime.Hours,
                    tfEndInSunday.SelectedTime.Minutes, 0);

                var endDate = model.StartDate.AddDays(Convert.ToInt32(cboEndSunday.SelectedItem.Value));
                model.EndDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, tfEndSunday.SelectedTime.Hours,
                    tfEndSunday.SelectedTime.Minutes, 0);

                var startOutDate = endDate.AddDays(Convert.ToInt32(cboStartOutSunday.SelectedItem.Value));
                model.StartOutTime = new DateTime(startOutDate.Year, startOutDate.Month, startOutDate.Day,
                    tfStartOutSunday.SelectedTime.Hours, tfStartOutSunday.SelectedTime.Minutes, 0);

                var endOutDate = endDate.AddDays(Convert.ToInt32(cboEndOutSunday.SelectedItem.Value));
                model.EndOutTime = new DateTime(endOutDate.Year, endOutDate.Month, endOutDate.Day,
                    tfEndOutSunday.SelectedTime.Hours, tfEndOutSunday.SelectedTime.Minutes, 0);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        private void EditDataWork(TimeSheetWorkShiftModel model)
        {
            model.Name = txtWorkName.Text;
            if (!string.IsNullOrEmpty(txtWorkShiftConvert.Text))
                model.WorkConvert =
                    Convert.ToDouble(txtWorkShiftConvert.Text.Replace(",", "."), CultureInfo.InvariantCulture);
            if (!string.IsNullOrEmpty(txtTimeWorkConvert.Text))
                model.TimeConvert =
                    Convert.ToDouble(txtTimeWorkConvert.Text.Replace(",", "."), CultureInfo.InvariantCulture);
            if (!string.IsNullOrEmpty(hdfSymbolWorkId.Text))
            {
                model.SymbolId = Convert.ToInt32(hdfSymbolWorkId.Text);
            }

            model.HasOverTime = false;
            model.HasLimitTime = false;
            model.HasInOutTime = false;
            if (chk_GroupHasWork.CheckedItems.Count > 0)
            {
                model.HasOverTime = chk_HasWorkOverTime.Checked;
                model.HasLimitTime = chk_HasWorkLimitTime.Checked;
                model.HasInOutTime = chk_HasWorkInOutTime.Checked;
            }
        }

        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InitWindowTimeWorkBreak(object sender, DirectEventArgs e)
        {
            try
            {
                //reset form
                ResetForm();
                wdTimeWork.Title = @"Tạo mới chi tiết bảng phân ca gãy";

                //show window
                wdTimeWork.Show();
                btnUpdateCloseWork.Show();
                dfFromDate.Enabled = true;
                dfToDate.Enabled = true;
            }
            catch (Exception exception)
            {
                Dialog.ShowError(exception);
            }
        }

        #region TimeWork
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InitWindowTimeWork(object sender, DirectEventArgs e)
        {
            try
            {
                //reset form
                ResetForm();
                wdTimeSheetRule.Title = @"Tạo mới chi tiết bảng phân ca";

                //show window
                wdTimeSheetRule.Show();
                btnUpdateClose.Show();
                timeSheetFromDate.Enabled = true;
                timeSheetToDate.Enabled = true;
            }
            catch (Exception exception)
            {
                Dialog.ShowError(exception);
            }
        }

        /// <summary>
        /// insert work normal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Insert(object sender, DirectEventArgs e)
        {
            //chose group
            var groupWorkShiftId = 0;
            if (!string.IsNullOrEmpty(hdfGroupWorkShift.Text) && Convert.ToInt32(hdfGroupWorkShift.Text) > 0)
            {
                groupWorkShiftId = Convert.ToInt32(hdfGroupWorkShift.Text);
            }
            else
            {
                //create group workShift
                //init entity
                var groupModel = new TimeSheetGroupWorkShiftModel()
                {
                    Name = txtName.Text,
                    StartDate = timeSheetFromDate.SelectedDate,
                    EndDate = timeSheetToDate.SelectedDate,
                    Description = txtDescriptionNormal.Text,
                    CreatedDate = DateTime.Now,
                    CreatedBy = CurrentUser.User.UserName,
                    EditedDate = DateTime.Now,
                };

                //Create
                var resultGroupModel = TimeSheetGroupWorkShiftController.Create(groupModel);
                groupWorkShiftId = resultGroupModel.Id;
            }
                
            if (checkGroupDay.CheckedItems.Count <= 0)
            {
                Dialog.ShowNotification("Bạn chưa chọn thứ");
                return;
            }
           
            //Create detail
            var timeSheetWorkShiftModel = new TimeSheetWorkShiftModel()
            {
                Name = txtName.Text,
                Type = WorkShiftType.Normal,
                GroupWorkShiftId = groupWorkShiftId,
                CreatedDate = DateTime.Now,
                CreatedBy = CurrentUser.User.UserName,
                EditedDate = DateTime.Now,
                EditedBy = " ",
            };
            //edit data
            EditData(timeSheetWorkShiftModel);

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

                    //Kiem tra xem ngay do co phai la ngay le khong. Neu la ngay le thi bo qua khong di dang ky
                    if (chkTetHoliday.Checked)
                    {
                        //check holiday
                        var isProcess = CheckHoliday(i, startMonth, startYear);

                        if (isProcess)
                        {
                            continue;
                        }
                    }

                    //create timeSheetWorkShift
                    CheckCreateTimeSheetWorkShift(date, timeSheetWorkShiftModel);
                }
            }
            else
            {
                //Chi xet trong 1 nam
                if (startYear == endYear)
                {
                    for (var j = startMonth; j <= endMonth; j++)
                    {
                        var dayOfMonth = DateTime.DaysInMonth(startYear, j);

                        for (var i = startDate.Day; i <= dayOfMonth; i++)
                        {
                            var date = new DateTime(startYear, j, i);
                            //Kiem tra xem ngay do co phai la ngay le khong. Neu la ngay le thi bo qua khong di dang ky
                            if (chkTetHoliday.Checked)
                            {
                                //check holiday
                                var isProcess = CheckHoliday(i, j, startYear);

                                if (isProcess)
                                {
                                    continue;
                                }
                            }

                            //create timeSheetWorkShift
                            CheckCreateTimeSheetWorkShift(date, timeSheetWorkShiftModel);
                        }
                    }
                }
            }

            wdTimeSheetRule.Hide();
            ResetForm();
            //reload data
            gpGroupWorkShift.Reload();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeSheetModel"></param>
        private void EditData(TimeSheetWorkShiftModel timeSheetModel)
        {
            if (!string.IsNullOrEmpty(txtWorkConvert.Text))
                timeSheetModel.WorkConvert =
                    Convert.ToDouble(txtWorkConvert.Text.Replace(",", "."), CultureInfo.InvariantCulture);
            if (!string.IsNullOrEmpty(txtTimeConvert.Text))
                timeSheetModel.TimeConvert =
                    Convert.ToDouble(txtTimeConvert.Text.Replace(",", "."), CultureInfo.InvariantCulture);
            if (!string.IsNullOrEmpty(hdfSymbolId.Text))
            {
                timeSheetModel.SymbolId = Convert.ToInt32(hdfSymbolId.Text);
            }

            timeSheetModel.HasOverTime = chk_IsOverTime.Checked;
            timeSheetModel.HasLimitTime = chk_IsLimitMaxTime.Checked;
            timeSheetModel.HasInOutTime = chk_IsHasInOutTime.Checked;
        }


        /// <summary>
        /// Create by day of week
        /// </summary>
        /// <param name="date"></param>
        /// <param name="timeSheet"></param>
        private void CheckCreateTimeSheetWorkShift(DateTime date, TimeSheetWorkShiftModel timeSheet)
        {
            if (checkGroupDay.CheckedItems.Count > 0)
            {
                foreach (var item in checkGroupDay.CheckedItems)
                {
                    switch (item.ID)
                    {
                        //Create
                        case "chkMonday" when date.DayOfWeek.ToString() == Constant.Monday:
                        //Create
                        case "chkTuesday" when date.DayOfWeek.ToString() == Constant.Tuesday:
                        //Create
                        case "chkWednesday" when date.DayOfWeek.ToString() == Constant.Wednesday:
                        //Create
                        case "chkThursday" when date.DayOfWeek.ToString() == Constant.Thursday:
                        //Create
                        case "chkFriday" when date.DayOfWeek.ToString() == Constant.Friday:
                        //Create
                        case "chkSaturday" when date.DayOfWeek.ToString() == Constant.Saturday:
                        //Create
                        case "chkSunday" when date.DayOfWeek.ToString() == Constant.Sunday:
                            CreateWorkShift(timeSheet, date);
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeSheetModel"></param>
        /// <param name="date"></param>
        private void CreateWorkShift(TimeSheetWorkShiftModel timeSheetModel, DateTime date)
        {
            timeSheetModel.StartDate = new DateTime(date.Year, date.Month, date.Day, inTime.SelectedTime.Hours, inTime.SelectedTime.Minutes, 0);
            timeSheetModel.EndDate = new DateTime(date.Year, date.Month, date.Day, outTime.SelectedTime.Hours, outTime.SelectedTime.Minutes, 0);
            timeSheetModel.StartInTime = new DateTime(date.Year, date.Month, date.Day, startInTime.SelectedTime.Hours, startInTime.SelectedTime.Minutes, 0);
            timeSheetModel.EndInTime = new DateTime(date.Year, date.Month, date.Day, endInTime.SelectedTime.Hours, endInTime.SelectedTime.Minutes, 0);
            timeSheetModel.StartOutTime = new DateTime(date.Year, date.Month, date.Day, startOutTime.SelectedTime.Hours, startOutTime.SelectedTime.Minutes, 0);
            timeSheetModel.EndOutTime = new DateTime(date.Year, date.Month, date.Day, endOutTime.SelectedTime.Hours, endOutTime.SelectedTime.Minutes, 0);
            //register table workShift
            TimeSheetWorkShiftController.Create(timeSheetModel);
        }

        /// <summary>
        /// 
        /// </summary>
        [DirectMethod]
        public void SetValueSelectSymbol()
        {
            if (!string.IsNullOrEmpty(hdfSymbolId.Text))
            {
                var symbolModel = TimeSheetSymbolController.GetById(Convert.ToInt32(hdfSymbolId.Text));
                if (symbolModel == null) return;
                txtWorkConvert.Text = symbolModel.WorkConvert.ToString();
                txtTimeConvert.Text = symbolModel.TimeConvert.ToString();
            }

            //work break
            if (!string.IsNullOrEmpty(hdfSymbolWorkId.Text))
            {
                var symbolModel = TimeSheetSymbolController.GetById(Convert.ToInt32(hdfSymbolWorkId.Text));
                if (symbolModel == null) return;
                txtWorkShiftConvert.Text = symbolModel.WorkConvert.ToString();
                txtTimeWorkConvert.Text = symbolModel.TimeConvert.ToString();
            }

            //template
            if (!string.IsNullOrEmpty(hdfSymbolIdUtil.Text))
            {
                var symbolModel = TimeSheetSymbolController.GetById(Convert.ToInt32(hdfSymbolIdUtil.Text));
                if (symbolModel == null) return;
                txtWorkConvertUtil.Text = symbolModel.WorkConvert.ToString();
                txtTimeConvertUtil.Text = symbolModel.TimeConvert.ToString();
            }
        }
        #endregion

        #region Template
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void storeType_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            try
            {
                storeType.DataSource = typeof(WorkShiftType).GetIntAndDescription();
                storeType.DataBind();
                
            }
            catch (Exception exception)
            {
                Dialog.ShowError(exception);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InsertTemplate(object sender, DirectEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(hdfType.Text))
                {
                    var templateModel = new TimeSheetWorkShiftTemplateModel();

                    //edit data common
                    EditDataWorkShiftTemplate(templateModel);
                    var date = DateTime.Now;
                    //case normal
                    if (Convert.ToInt32(hdfType.Text) == (int) WorkShiftType.Normal)
                    {
                        templateModel.StartDate = new DateTime(date.Year, date.Month, date.Day, tfStartDateNormal.SelectedTime.Hours, tfStartDateNormal.SelectedTime.Minutes, 0);
                        templateModel.EndDate = new DateTime(date.Year, date.Month, date.Day, tfEndDateNormal.SelectedTime.Hours, tfEndDateNormal.SelectedTime.Minutes, 0);
                        templateModel.StartInTime = new DateTime(date.Year, date.Month, date.Day, tfStartInNormal.SelectedTime.Hours, tfStartInNormal.SelectedTime.Minutes, 0);
                        templateModel.EndInTime = new DateTime(date.Year, date.Month, date.Day, tfEndInNormal.SelectedTime.Hours, tfEndInNormal.SelectedTime.Minutes, 0);
                        templateModel.StartOutTime = new DateTime(date.Year, date.Month, date.Day, tfStartOutNormal.SelectedTime.Hours, tfStartOutNormal.SelectedTime.Minutes, 0);
                        templateModel.EndOutTime = new DateTime(date.Year, date.Month, date.Day, tfEndOutNormal.SelectedTime.Hours, tfEndOutNormal.SelectedTime.Minutes, 0);
                    }
                    //case break
                    if (Convert.ToInt32(hdfType.Text) == (int)WorkShiftType.Break)
                    {
                        templateModel.StartDate = new DateTime(date.Year, date.Month, date.Day, tfStartDateBreak.SelectedTime.Hours,
                            tfStartDateBreak.SelectedTime.Minutes, 0);

                        var startInDate = templateModel.StartDate.AddDays(Convert.ToInt32(cboStartInUtil.SelectedItem.Value));
                        templateModel.StartInTime = new DateTime(startInDate.Year, startInDate.Month, startInDate.Day, tfStartInBreak.SelectedTime.Hours,
                            tfStartInBreak.SelectedTime.Minutes, 0);
                        templateModel.TypeStartIn = Convert.ToInt32(cboStartInUtil.SelectedItem.Value);

                        var endInDate = templateModel.StartDate.AddDays(Convert.ToInt32(cboEndInUtil.SelectedItem.Value));
                        templateModel.EndInTime = new DateTime(endInDate.Year, endInDate.Month, endInDate.Day, tfEndInBreak.SelectedTime.Hours,
                            tfEndInBreak.SelectedTime.Minutes, 0);
                        templateModel.TypeEndIn = Convert.ToInt32(cboEndInUtil.SelectedItem.Value);

                        templateModel.EndDate = new DateTime(date.Year, date.Month, date.Day, tfEndDateBreak.SelectedTime.Hours,
                            tfEndDateBreak.SelectedTime.Minutes, 0);

                        var startOutDate = date.AddDays(Convert.ToInt32(cboStartOutUtil.SelectedItem.Value));
                        templateModel.StartOutTime = new DateTime(startOutDate.Year, startOutDate.Month, startOutDate.Day,
                            tfStartOutBreak.SelectedTime.Hours, tfStartOutBreak.SelectedTime.Minutes, 0);
                        templateModel.TypeStartOut= Convert.ToInt32(cboStartOutUtil.SelectedItem.Value);

                        var endOutDate = date.AddDays(Convert.ToInt32(cboEndOutUtil.SelectedItem.Value));
                        templateModel.EndOutTime = new DateTime(endOutDate.Year, endOutDate.Month, endOutDate.Day,
                            tfEndOutBreak.SelectedTime.Hours, tfEndOutBreak.SelectedTime.Minutes, 0);
                        templateModel.TypeEndOut = Convert.ToInt32(cboEndOutUtil.SelectedItem.Value);
                    }

                    //create template
                    TimeSheetWorkShiftTemplateController.Create(templateModel);
                    //hide window
                    wdUtil.Hide();
                }
            }
            catch (Exception exception)
            {
                Dialog.ShowError(exception);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        private void EditDataWorkShiftTemplate(TimeSheetWorkShiftTemplateModel model)
        {
            model.Name = txtNameTemplate.Text;
            if (!string.IsNullOrEmpty(txtWorkConvertUtil.Text))
                model.WorkConvert = Convert.ToDouble(txtWorkConvertUtil.Text.Replace(",", "."), CultureInfo.InvariantCulture);
            if (!string.IsNullOrEmpty(txtTimeConvertUtil.Text))
                model.TimeConvert = Convert.ToDouble(txtTimeConvertUtil.Text.Replace(",", "."), CultureInfo.InvariantCulture);
            if (!string.IsNullOrEmpty(hdfSymbolIdUtil.Text))
            {
                model.SymbolId = Convert.ToInt32(hdfSymbolIdUtil.Text);
            }

            if (!string.IsNullOrEmpty(hdfGroupSymbolUtil.Text))
            {
                model.GroupSymbolId = Convert.ToInt32(hdfGroupSymbolUtil.Text);
            }

            if (!string.IsNullOrEmpty(hdfType.Text))
            {
                model.Type = (WorkShiftType)Enum.Parse(typeof(WorkShiftType), hdfType.Text);
            }

            model.HasOverTime = false;
            model.HasLimitTime = false;
            model.HasInOutTime = false;
            model.HasHoliday = false;
            if (checkGroupOptionUtil.CheckedItems.Count > 0)
            {
                model.HasOverTime = chk_HasWorkOverTimeUtil.Checked;
                model.HasLimitTime = chk_HasWorkLimitTimeUtil.Checked;
                model.HasInOutTime = chk_HasWorkInOutTimeUtil.Checked;
                model.HasHoliday = chk_HasWorkHolidayUtil.Checked;
            }

            model.HasMonday = false;
            model.HasTuesday = false;
            model.HasWednesday = false;
            model.HasThursday = false;
            model.HasFriday = false;
            model.HasSaturday = false;
            model.HasSunday = false;
            if (checkGroupDayUtil.CheckedItems.Count > 0)
            {
                model.HasMonday = chkMondayUtil.Checked;
                model.HasTuesday = chkTuesdayUtil.Checked;
                model.HasWednesday = chkWednesdayUtil.Checked;
                model.HasThursday = chkThursdayUtil.Checked;
                model.HasFriday = chkFridayUtil.Checked;
                model.HasSaturday = chkSaturdayUtil.Checked;
                model.HasSunday = chkSundayUtil.Checked;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AcceptTemplateClick(object sender, DirectEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(hdfTemplateId.Text))
                {
                    var templateModel =
                        TimeSheetWorkShiftTemplateController.GetById(Convert.ToInt32(hdfTemplateId.Text));
                    if (templateModel != null)
                    {
                        switch (templateModel.Type)
                        {
                            case WorkShiftType.Normal:
                                InitWindowNormal(templateModel);
                                wdTimeSheetRule.Show();
                                break;
                            case WorkShiftType.Break:
                                InitWindowBreak(templateModel);
                                wdTimeWork.Show();
                                break;
                            default:
                                break;
                        }
                    }
                }

                //close window
                wdTemplate.Hide();
            }
            catch (Exception exception)
            {
                Dialog.ShowError(exception);
            }
        }

        /// <summary>
        /// init workShift normal
        /// </summary>
        /// <param name="templateModel"></param>
        private void InitWindowNormal(TimeSheetWorkShiftTemplateModel templateModel)
        {
            try
            {
                hdfGroupSymbol.Text = templateModel.GroupSymbolId.ToString();
                cbxGroupSymbol.Text = templateModel.GroupSymbolName;
                txtWorkConvert.Text = templateModel.WorkConvert.ToString();
                hdfSymbolId.Text = templateModel.SymbolId.ToString();
                cbxSymbol.Text = templateModel.SymbolCode;
                cbxSymbol.Disabled = false;
                txtTimeConvert.Text = templateModel.TimeConvert.ToString();
                inTime.Text = templateModel.StartDate.Hour + @":" + templateModel.StartDate.Minute;
                startInTime.Text = templateModel.StartInTime.Hour + @":" + templateModel.StartInTime.Minute;
                startOutTime.Text = templateModel.StartOutTime.Hour + @":" + templateModel.StartOutTime.Minute;
                outTime.Text = templateModel.EndDate.Hour + @":" + templateModel.EndDate.Minute;
                endInTime.Text = templateModel.EndInTime.Hour + @":" + templateModel.EndInTime.Minute;
                endOutTime.Text = templateModel.EndOutTime.Hour + @":" + templateModel.EndOutTime.Minute;
                chkMonday.Checked = templateModel.HasMonday;
                chkTuesday.Checked = templateModel.HasTuesday;
                chkWednesday.Checked = templateModel.HasWednesday;
                chkThursday.Checked = templateModel.HasThursday;
                chkFriday.Checked = templateModel.HasFriday;
                chkSaturday.Checked = templateModel.HasSaturday;
                chkSunday.Checked = templateModel.HasSunday;
                chkTetHoliday.Checked = templateModel.HasHoliday;
                chk_IsHasInOutTime.Checked = templateModel.HasInOutTime;
                chk_IsOverTime.Checked = templateModel.HasOverTime;
                chk_IsLimitMaxTime.Checked = templateModel.HasLimitTime;
            }
            catch (Exception exception)
            {
                Dialog.ShowError(exception);
            }
        }

        /// <summary>
        /// init workShift break
        /// </summary>
        /// <param name="templateModel"></param>
        private void InitWindowBreak(TimeSheetWorkShiftTemplateModel templateModel)
        {
            try
            {
                hdfGroupSymbolShiftId.Text = templateModel.GroupSymbolId.ToString();
                cbxGroupSymbolShift.Text = templateModel.GroupSymbolName;
                txtWorkShiftConvert.Text = templateModel.WorkConvert.ToString();
                hdfSymbolWorkId.Text = templateModel.SymbolId.ToString();
                cboSymbolWork.Text = templateModel.SymbolCode;
                cboSymbolWork.Disabled = false;
                txtTimeWorkConvert.Text = templateModel.TimeConvert.ToString();
                chkEnableMonday.Checked = templateModel.HasMonday;
                chkEnableTuesday.Checked = templateModel.HasTuesday;
                chkEnableWednesday.Checked = templateModel.HasWednesday;
                chkEnableThursday.Checked = templateModel.HasThursday;
                chkEnableFriday.Checked = templateModel.HasFriday;
                chkEnableSaturday.Checked = templateModel.HasSaturday;
                chkEnableSunday.Checked = templateModel.HasSunday;
                chk_HasWorkHoliday.Checked = templateModel.HasHoliday;
                chk_HasWorkInOutTime.Checked = templateModel.HasInOutTime;
                chk_HasWorkOverTime.Checked = templateModel.HasOverTime;
                chk_HasWorkLimitTime.Checked = templateModel.HasLimitTime;
                //monday
                tfStartMonday.Text = templateModel.StartDate.Hour + @":" + templateModel.StartDate.Minute;
                tfStartInMonday.Text = templateModel.StartInTime.Hour + @":" + templateModel.StartInTime.Minute;
                tfEndInMonday.Text = templateModel.EndInTime.Hour + @":" + templateModel.EndInTime.Minute;
                tfEndMonday.Text = templateModel.EndDate.Hour + @":" + templateModel.EndDate.Minute;
                tfStartOutMonday.Text = templateModel.StartOutTime.Hour + @":" + templateModel.StartOutTime.Minute;
                tfEndOutMonday.Text = templateModel.EndOutTime.Hour + @":" + templateModel.EndOutTime.Minute;
                cboStartInMonday.SetValue(templateModel.TypeStartIn);
                cboEndInMonday.SetValue(templateModel.TypeEndIn);
                cboStartOutMonday.SetValue(templateModel.TypeStartOut);
                cboEndOutMonday.SetValue(templateModel.TypeEndOut);

                //tuesday
                tfStartTuesday.Text = templateModel.StartDate.Hour + @":" + templateModel.StartDate.Minute;
                tfStartInTuesday.Text = templateModel.StartInTime.Hour + @":" + templateModel.StartInTime.Minute;
                tfEndInTuesday.Text = templateModel.EndInTime.Hour + @":" + templateModel.EndInTime.Minute;
                tfEndTuesday.Text = templateModel.EndDate.Hour + @":" + templateModel.EndDate.Minute;
                tfStartOutTuesday.Text = templateModel.StartOutTime.Hour + @":" + templateModel.StartOutTime.Minute;
                tfEndOutTuesday.Text = templateModel.EndOutTime.Hour + @":" + templateModel.EndOutTime.Minute;
                cboStartInTuesday.SetValue(templateModel.TypeStartIn);
                cboEndInTuesday.SetValue(templateModel.TypeEndIn);
                cboStartOutTuesday.SetValue(templateModel.TypeStartOut);
                cboEndOutTuesday.SetValue(templateModel.TypeEndOut);

                //wednesday
                tfStartWednesday.Text = templateModel.StartDate.Hour + @":" + templateModel.StartDate.Minute;
                tfStartInWednesday.Text = templateModel.StartInTime.Hour + @":" + templateModel.StartInTime.Minute;
                tfEndInWednesday.Text = templateModel.EndInTime.Hour + @":" + templateModel.EndInTime.Minute;
                tfEndWednesday.Text = templateModel.EndDate.Hour + @":" + templateModel.EndDate.Minute;
                tfStartOutWednesday.Text = templateModel.StartOutTime.Hour + @":" + templateModel.StartOutTime.Minute;
                tfEndOutWednesday.Text = templateModel.EndOutTime.Hour + @":" + templateModel.EndOutTime.Minute;
                cboStartInWednesday.SetValue(templateModel.TypeStartIn);
                cboEndInWednesday.SetValue(templateModel.TypeEndIn);
                cboStartOutWednesday.SetValue(templateModel.TypeStartOut);
                cboEndOutWednesday.SetValue(templateModel.TypeEndOut);

                //thursday
                tfStartThursday.Text = templateModel.StartDate.Hour + @":" + templateModel.StartDate.Minute;
                tfStartInThursday.Text = templateModel.StartInTime.Hour + @":" + templateModel.StartInTime.Minute;
                tfEndInThursday.Text = templateModel.EndInTime.Hour + @":" + templateModel.EndInTime.Minute;
                tfEndThursday.Text = templateModel.EndDate.Hour + @":" + templateModel.EndDate.Minute;
                tfStartOutThursday.Text = templateModel.StartOutTime.Hour + @":" + templateModel.StartOutTime.Minute;
                tfEndOutThursday.Text = templateModel.EndOutTime.Hour + @":" + templateModel.EndOutTime.Minute;
                cboStartInThursday.SetValue(templateModel.TypeStartIn);
                cboEndInThursday.SetValue(templateModel.TypeEndIn);
                cboStartOutThursday.SetValue(templateModel.TypeStartOut);
                cboEndOutThursday.SetValue(templateModel.TypeEndOut);

                //friday
                tfStartFriday.Text = templateModel.StartDate.Hour + @":" + templateModel.StartDate.Minute;
                tfStartInFriday.Text = templateModel.StartInTime.Hour + @":" + templateModel.StartInTime.Minute;
                tfEndInFriday.Text = templateModel.EndInTime.Hour + @":" + templateModel.EndInTime.Minute;
                tfEndFriday.Text = templateModel.EndDate.Hour + @":" + templateModel.EndDate.Minute;
                tfStartOutFriday.Text = templateModel.StartOutTime.Hour + @":" + templateModel.StartOutTime.Minute;
                tfEndOutFriday.Text = templateModel.EndOutTime.Hour + @":" + templateModel.EndOutTime.Minute;
                cboStartInFriday.SetValue(templateModel.TypeStartIn);
                cboEndInFriday.SetValue(templateModel.TypeEndIn);
                cboStartOutFriday.SetValue(templateModel.TypeStartOut);
                cboEndOutFriday.SetValue(templateModel.TypeEndOut);

                //saturday
                tfStartSaturday.Text = templateModel.StartDate.Hour + @":" + templateModel.StartDate.Minute;
                tfStartInSaturday.Text = templateModel.StartInTime.Hour + @":" + templateModel.StartInTime.Minute;
                tfEndInSaturday.Text = templateModel.EndInTime.Hour + @":" + templateModel.EndInTime.Minute;
                tfEndSaturday.Text = templateModel.EndDate.Hour + @":" + templateModel.EndDate.Minute;
                tfStartOutSaturday.Text = templateModel.StartOutTime.Hour + @":" + templateModel.StartOutTime.Minute;
                tfEndOutSaturday.Text = templateModel.EndOutTime.Hour + @":" + templateModel.EndOutTime.Minute;
                cboStartInSaturday.SetValue(templateModel.TypeStartIn);
                cboEndInSaturday.SetValue(templateModel.TypeEndIn);
                cboStartOutSaturday.SetValue(templateModel.TypeStartOut);
                cboEndOutSaturday.SetValue(templateModel.TypeEndOut);

                //sunday
                tfStartSunday.Text = templateModel.StartDate.Hour + @":" + templateModel.StartDate.Minute;
                tfStartInSunday.Text = templateModel.StartInTime.Hour + @":" + templateModel.StartInTime.Minute;
                tfEndInSunday.Text = templateModel.EndInTime.Hour + @":" + templateModel.EndInTime.Minute;
                tfEndSunday.Text = templateModel.EndDate.Hour + @":" + templateModel.EndDate.Minute;
                tfStartOutSunday.Text = templateModel.StartOutTime.Hour + @":" + templateModel.StartOutTime.Minute;
                tfEndOutSunday.Text = templateModel.EndOutTime.Hour + @":" + templateModel.EndOutTime.Minute;
                cboStartInSunday.SetValue(templateModel.TypeStartIn);
                cboEndInSunday.SetValue(templateModel.TypeEndIn);
                cboStartOutSunday.SetValue(templateModel.TypeStartOut);
                cboEndOutSunday.SetValue(templateModel.TypeEndOut);

            }
            catch (Exception exception)
            {
                Dialog.ShowError(exception);
            }
        }

        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CopyData_Click(object sender, DirectEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(hdfKeyRecord.Text))
                {
                    var listWorkShift = TimeSheetWorkShiftController.GetAll(null, false,
                        Convert.ToInt32(hdfKeyRecord.Text), null, null, null, null, null, null, null, null);
                    if (listWorkShift.Count > 0)
                    {
                        var workShiftModel = listWorkShift.First();
                        switch (workShiftModel.Type)
                        {
                            case WorkShiftType.Normal:
                                InitWindowCopyNormal(workShiftModel);
                                wdTimeSheetRule.Show();
                                break;
                            case WorkShiftType.Break:
                                InitWindowCopyBreak(workShiftModel);
                                wdTimeWork.Show();
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Dialog.ShowError(exception);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workShiftModel"></param>
        private void InitWindowCopyNormal(TimeSheetWorkShiftModel workShiftModel)
        {
            try
            {
                hdfGroupSymbol.Text = workShiftModel.GroupSymbolId.ToString();
                cbxGroupSymbol.Text = workShiftModel.GroupSymbolName;
                txtWorkConvert.Text = workShiftModel.WorkConvert.ToString();
                hdfSymbolId.Text = workShiftModel.SymbolId.ToString();
                cbxSymbol.Text = workShiftModel.SymbolCode;
                cbxSymbol.Disabled = false;
                txtTimeConvert.Text = workShiftModel.TimeConvert.ToString();
                inTime.Text = workShiftModel.StartDate.Hour + @":" + workShiftModel.StartDate.Minute;
                startInTime.Text = workShiftModel.StartInTime.Hour + @":" + workShiftModel.StartInTime.Minute;
                startOutTime.Text = workShiftModel.StartOutTime.Hour + @":" + workShiftModel.StartOutTime.Minute;
                outTime.Text = workShiftModel.EndDate.Hour + @":" + workShiftModel.EndDate.Minute;
                endInTime.Text = workShiftModel.EndInTime.Hour + @":" + workShiftModel.EndInTime.Minute;
                endOutTime.Text = workShiftModel.EndOutTime.Hour + @":" + workShiftModel.EndOutTime.Minute;
                chkMonday.Checked = true;
                chkTuesday.Checked = true;
                chkWednesday.Checked = true;
                chkThursday.Checked = true;
                chkFriday.Checked = true;
                chkSaturday.Checked = true;
                chkSunday.Checked = true;
                chkTetHoliday.Checked = workShiftModel.HasHoliday;
                chk_IsHasInOutTime.Checked = workShiftModel.HasInOutTime;
                chk_IsOverTime.Checked = workShiftModel.HasOverTime;
                chk_IsLimitMaxTime.Checked = workShiftModel.HasLimitTime;
            }
            catch (Exception exception)
            {
                Dialog.ShowError(exception);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workShiftModel"></param>
        private void InitWindowCopyBreak(TimeSheetWorkShiftModel workShiftModel)
        {
            try
            {
                hdfGroupSymbolShiftId.Text = workShiftModel.GroupSymbolId.ToString();
                cbxGroupSymbolShift.Text = workShiftModel.GroupSymbolName;
                txtWorkShiftConvert.Text = workShiftModel.WorkConvert.ToString();
                hdfSymbolWorkId.Text = workShiftModel.SymbolId.ToString();
                cboSymbolWork.Text = workShiftModel.SymbolCode;
                cboSymbolWork.Disabled = false;
                txtTimeWorkConvert.Text = workShiftModel.TimeConvert.ToString();
                chkEnableMonday.Checked = true;
                chkEnableTuesday.Checked = true;
                chkEnableWednesday.Checked = true;
                chkEnableThursday.Checked = true;
                chkEnableFriday.Checked = true;
                chkEnableSaturday.Checked = true;
                chkEnableSunday.Checked = true;
                chk_HasWorkHoliday.Checked = workShiftModel.HasHoliday;
                chk_HasWorkInOutTime.Checked = workShiftModel.HasInOutTime;
                chk_HasWorkOverTime.Checked = workShiftModel.HasOverTime;
                chk_HasWorkLimitTime.Checked = workShiftModel.HasLimitTime;

                //monday
                tfStartMonday.Text = workShiftModel.StartDate.Hour + @":" + workShiftModel.StartDate.Minute;
                tfStartInMonday.Text = workShiftModel.StartInTime.Hour + @":" + workShiftModel.StartInTime.Minute;
                tfEndInMonday.Text = workShiftModel.EndInTime.Hour + @":" + workShiftModel.EndInTime.Minute;
                tfEndMonday.Text = workShiftModel.EndDate.Hour + @":" + workShiftModel.EndDate.Minute;
                tfStartOutMonday.Text = workShiftModel.StartOutTime.Hour + @":" + workShiftModel.StartOutTime.Minute;
                tfEndOutMonday.Text = workShiftModel.EndOutTime.Hour + @":" + workShiftModel.EndOutTime.Minute;
               
                //tuesday
                tfStartTuesday.Text = workShiftModel.StartDate.Hour + @":" + workShiftModel.StartDate.Minute;
                tfStartInTuesday.Text = workShiftModel.StartInTime.Hour + @":" + workShiftModel.StartInTime.Minute;
                tfEndInTuesday.Text = workShiftModel.EndInTime.Hour + @":" + workShiftModel.EndInTime.Minute;
                tfEndTuesday.Text = workShiftModel.EndDate.Hour + @":" + workShiftModel.EndDate.Minute;
                tfStartOutTuesday.Text = workShiftModel.StartOutTime.Hour + @":" + workShiftModel.StartOutTime.Minute;
                tfEndOutTuesday.Text = workShiftModel.EndOutTime.Hour + @":" + workShiftModel.EndOutTime.Minute;
               
                //wednesday
                tfStartWednesday.Text = workShiftModel.StartDate.Hour + @":" + workShiftModel.StartDate.Minute;
                tfStartInWednesday.Text = workShiftModel.StartInTime.Hour + @":" + workShiftModel.StartInTime.Minute;
                tfEndInWednesday.Text = workShiftModel.EndInTime.Hour + @":" + workShiftModel.EndInTime.Minute;
                tfEndWednesday.Text = workShiftModel.EndDate.Hour + @":" + workShiftModel.EndDate.Minute;
                tfStartOutWednesday.Text = workShiftModel.StartOutTime.Hour + @":" + workShiftModel.StartOutTime.Minute;
                tfEndOutWednesday.Text = workShiftModel.EndOutTime.Hour + @":" + workShiftModel.EndOutTime.Minute;
              
                //thursday
                tfStartThursday.Text = workShiftModel.StartDate.Hour + @":" + workShiftModel.StartDate.Minute;
                tfStartInThursday.Text = workShiftModel.StartInTime.Hour + @":" + workShiftModel.StartInTime.Minute;
                tfEndInThursday.Text = workShiftModel.EndInTime.Hour + @":" + workShiftModel.EndInTime.Minute;
                tfEndThursday.Text = workShiftModel.EndDate.Hour + @":" + workShiftModel.EndDate.Minute;
                tfStartOutThursday.Text = workShiftModel.StartOutTime.Hour + @":" + workShiftModel.StartOutTime.Minute;
                tfEndOutThursday.Text = workShiftModel.EndOutTime.Hour + @":" + workShiftModel.EndOutTime.Minute;
              
                //friday
                tfStartFriday.Text = workShiftModel.StartDate.Hour + @":" + workShiftModel.StartDate.Minute;
                tfStartInFriday.Text = workShiftModel.StartInTime.Hour + @":" + workShiftModel.StartInTime.Minute;
                tfEndInFriday.Text = workShiftModel.EndInTime.Hour + @":" + workShiftModel.EndInTime.Minute;
                tfEndFriday.Text = workShiftModel.EndDate.Hour + @":" + workShiftModel.EndDate.Minute;
                tfStartOutFriday.Text = workShiftModel.StartOutTime.Hour + @":" + workShiftModel.StartOutTime.Minute;
                tfEndOutFriday.Text = workShiftModel.EndOutTime.Hour + @":" + workShiftModel.EndOutTime.Minute;
               
                //saturday
                tfStartSaturday.Text = workShiftModel.StartDate.Hour + @":" + workShiftModel.StartDate.Minute;
                tfStartInSaturday.Text = workShiftModel.StartInTime.Hour + @":" + workShiftModel.StartInTime.Minute;
                tfEndInSaturday.Text = workShiftModel.EndInTime.Hour + @":" + workShiftModel.EndInTime.Minute;
                tfEndSaturday.Text = workShiftModel.EndDate.Hour + @":" + workShiftModel.EndDate.Minute;
                tfStartOutSaturday.Text = workShiftModel.StartOutTime.Hour + @":" + workShiftModel.StartOutTime.Minute;
                tfEndOutSaturday.Text = workShiftModel.EndOutTime.Hour + @":" + workShiftModel.EndOutTime.Minute;
               
                //sunday
                tfStartSunday.Text = workShiftModel.StartDate.Hour + @":" + workShiftModel.StartDate.Minute;
                tfStartInSunday.Text = workShiftModel.StartInTime.Hour + @":" + workShiftModel.StartInTime.Minute;
                tfEndInSunday.Text = workShiftModel.EndInTime.Hour + @":" + workShiftModel.EndInTime.Minute;
                tfEndSunday.Text = workShiftModel.EndDate.Hour + @":" + workShiftModel.EndDate.Minute;
                tfStartOutSunday.Text = workShiftModel.StartOutTime.Hour + @":" + workShiftModel.StartOutTime.Minute;
                tfEndOutSunday.Text = workShiftModel.EndOutTime.Hour + @":" + workShiftModel.EndOutTime.Minute;
               
                if (workShiftModel.StartDate.Day == workShiftModel.StartInTime.Day)
                {
                    cboStartInMonday.SetValue(0);
                    cboStartInTuesday.SetValue(0);
                    cboStartInWednesday.SetValue(0);
                    cboStartInThursday.SetValue(0);
                    cboStartInFriday.SetValue(0);
                    cboStartInSaturday.SetValue(0);
                    cboStartInSunday.SetValue(0);
                }
                else
                {
                    cboStartInMonday.SetValue(-1);
                    cboStartInTuesday.SetValue(-1);
                    cboStartInWednesday.SetValue(-1);
                    cboStartInThursday.SetValue(-1);
                    cboStartInFriday.SetValue(-1);
                    cboStartInSaturday.SetValue(-1);
                    cboStartInSunday.SetValue(-1);
                }

                if (workShiftModel.StartDate.Day == workShiftModel.EndInTime.Day)
                {
                    cboEndInMonday.SetValue(0);
                    cboEndInTuesday.SetValue(0);
                    cboEndInWednesday.SetValue(0);
                    cboEndInThursday.SetValue(0);
                    cboEndInFriday.SetValue(0);
                    cboEndInSaturday.SetValue(0);
                    cboEndInSunday.SetValue(0);
                }
                else
                {
                    cboEndInMonday.SetValue(1);
                    cboEndInTuesday.SetValue(1);
                    cboEndInWednesday.SetValue(1);
                    cboEndInThursday.SetValue(1);
                    cboEndInFriday.SetValue(1);
                    cboEndInSaturday.SetValue(1);
                    cboEndInSunday.SetValue(1);
                }

                if (workShiftModel.EndDate.Day == workShiftModel.StartOutTime.Day)
                {
                    cboStartOutMonday.SetValue(0);
                    cboStartOutTuesday.SetValue(0);
                    cboStartOutWednesday.SetValue(0);
                    cboStartOutThursday.SetValue(0);
                    cboStartOutFriday.SetValue(0);
                    cboStartOutSaturday.SetValue(0);
                    cboStartOutSunday.SetValue(0);
                }
                else
                {
                    cboStartOutMonday.SetValue(-1);
                    cboStartOutTuesday.SetValue(-1);
                    cboStartOutWednesday.SetValue(-1);
                    cboStartOutThursday.SetValue(-1);
                    cboStartOutFriday.SetValue(-1);
                    cboStartOutSaturday.SetValue(-1);
                    cboStartOutSunday.SetValue(-1);
                }


                if (workShiftModel.EndDate.Day == workShiftModel.EndOutTime.Day)
                {
                    cboEndOutMonday.SetValue(0);
                    cboEndOutTuesday.SetValue(0);
                    cboEndOutWednesday.SetValue(0);
                    cboEndOutThursday.SetValue(0);
                    cboEndOutFriday.SetValue(0);
                    cboEndOutSaturday.SetValue(0);
                    cboEndOutSunday.SetValue(0);
                }
                else
                {
                    cboEndOutMonday.SetValue(1);
                    cboEndOutTuesday.SetValue(1);
                    cboEndOutWednesday.SetValue(1);
                    cboEndOutThursday.SetValue(1);
                    cboEndOutFriday.SetValue(1);
                    cboEndOutSaturday.SetValue(1);
                    cboEndOutSunday.SetValue(1);
                }


            }
            catch (Exception exception)
            {
                Dialog.ShowError(exception);
            }
        }
    }
}
