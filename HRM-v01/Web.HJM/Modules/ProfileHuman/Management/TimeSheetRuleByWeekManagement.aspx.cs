using System;
using System.Globalization;
using Ext.Net;
using Web.Core.Framework;
using Web.Core;
using Web.Core.Object.HumanRecord;
using System.Linq;
using SoftCore;
using Web.Core.Service.HumanRecord;

namespace Web.HJM.Modules.ProfileHuman.Management
{
    public partial class TimeSheetRuleByWeekManagement : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ExtNet.IsAjaxRequest)
            {
                hdfDepartments.Text = string.Join(",", CurrentUser.Departments.Select(d => d.Id));

            }
            if (btnEdit.Visible)
            {
                gridTimeSheetRule.Listeners.RowDblClick.Handler += " if(CheckSelectedRows(gridTimeSheetRule)){btnUpdate.show();btnUpdateClose.hide()}";
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
                gridTimeSheetRule.Reload();
            }
            catch (Exception ex)
            {
                ExtNet.MessageBox.Alert("Có lỗi xảy ra", ex.Message).Show();
            }
        }

        private void Insert(DirectEventArgs e)
        {
            try
            {
                var day = string.Empty;
                if (checkGroupDay.CheckedItems.Count > 0)
                {
                    foreach (var item in checkGroupDay.CheckedItems)
                    {
                        if (item.ID == "chkMonday")
                            day = "Monday";
                        if (item.ID == "chkTuesday")
                            day = "Tuesday";
                        if (item.ID == "chkWednesday")
                            day = "Wednesday";
                        if (item.ID == "chkThursday")
                            day = "Thursday";
                        if (item.ID == "chkFriday")
                            day = "Friday";
                        if (item.ID == "chkSaturday")
                            day = "Saturday";
                        if (item.ID == "chkSunday")
                            day = "Sunday";
                        var timeSheet = new hr_TimeSheetRuleByWeek()
                        {
                            Name = txtName.Text,
                            Code = txtCode.Text,
                            CreatedDate = DateTime.Now,
                        };
                        //if (!util.IsDateNull(dfSpecifiedDate.SelectedDate))
                        //{
                        //    timeSheet.SpecifiedDate = dfSpecifiedDate.SelectedDate;
                        //}
                        timeSheet.DayOfWeek = day;
                        if (!string.IsNullOrEmpty(txtNumberOfDay.Text))
                            timeSheet.WorkConvert = decimal.Parse(txtNumberOfDay.Text);
                        if (!string.IsNullOrEmpty(startInputTime.Text))
                            timeSheet.StartInputTime = startInputTime.Text.Replace(":", "");
                        if (!string.IsNullOrEmpty(endInputTime.Text))
                            timeSheet.EndInputTime = endInputTime.Text.Replace(":", "");
                        if (!string.IsNullOrEmpty(startOutputTime.Text))
                            timeSheet.StartOutputTime = startOutputTime.Text.Replace(":", "");
                        if (!string.IsNullOrEmpty(endOutputTime.Text))
                            timeSheet.EndOutputTime = endOutputTime.Text.Replace(":", "");
                        hr_TimeSheetRuleByWeekServices.Create(timeSheet);
                    }
                }
                else
                {
                    //if (util.IsDateNull(dfSpecifiedDate.SelectedDate))
                    //{
                    //    Dialog.Alert("Vui lòng chọn thứ hoặc chọn ngày nếu có!");
                    //    return;
                    //}
                    var timeSheet = new hr_TimeSheetRuleByWeek()
                    {
                        Name = txtName.Text,
                        Code = txtCode.Text,
                        CreatedDate = DateTime.Now,
                    };
                    //if (!util.IsDateNull(dfSpecifiedDate.SelectedDate))
                    //{
                    //    timeSheet.SpecifiedDate = dfSpecifiedDate.SelectedDate;
                    //}
                    if (!string.IsNullOrEmpty(txtNumberOfDay.Text))
                        timeSheet.WorkConvert = decimal.Parse(txtNumberOfDay.Text);
                    if (!string.IsNullOrEmpty(startInputTime.Text))
                        timeSheet.StartInputTime = startInputTime.Text.Replace(":", "");
                    if (!string.IsNullOrEmpty(endInputTime.Text))
                        timeSheet.EndInputTime = endInputTime.Text.Replace(":", "");
                    if (!string.IsNullOrEmpty(startOutputTime.Text))
                        timeSheet.StartOutputTime = startOutputTime.Text.Replace(":", "");
                    if (!string.IsNullOrEmpty(endOutputTime.Text))
                        timeSheet.EndOutputTime = endOutputTime.Text.Replace(":", "");
                    hr_TimeSheetRuleByWeekServices.Create(timeSheet);
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

        private void Update()
        {
            try
            {
                if (string.IsNullOrEmpty(hdfKeyRecord.Text)) return;
                var util = new Util();
                var timeSheet = hr_TimeSheetRuleByWeekServices.GetById(Convert.ToInt32(hdfKeyRecord.Text));
                if (timeSheet != null)
                {
                    timeSheet.Name = txtName.Text;
                    timeSheet.Code = txtCode.Text;
                    //if (!util.IsDateNull(dfSpecifiedDate.SelectedDate))
                    //{
                    //    timeSheet.SpecifiedDate = dfSpecifiedDate.SelectedDate;
                    //}
                    if (!string.IsNullOrEmpty(txtNumberOfDay.Text))
                        timeSheet.WorkConvert = decimal.Parse(txtNumberOfDay.Text);
                    if (!string.IsNullOrEmpty(startInputTime.Text))
                        timeSheet.StartInputTime = startInputTime.Text.Replace(":", "");
                    if (!string.IsNullOrEmpty(endInputTime.Text))
                        timeSheet.EndInputTime = endInputTime.Text.Replace(":", "");
                    if (!string.IsNullOrEmpty(startOutputTime.Text))
                        timeSheet.StartOutputTime = startOutputTime.Text.Replace(":", "");
                    if (!string.IsNullOrEmpty(endOutputTime.Text))
                        timeSheet.EndOutputTime = endOutputTime.Text.Replace(":", "");
                    timeSheet.EditedDate = DateTime.Now;
                }
                hr_TimeSheetRuleByWeekServices.Update(timeSheet);
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
            // dfSpecifiedDate.Reset();
            startInputTime.Reset();
            endInputTime.Reset();
            startOutputTime.Reset();
            endOutputTime.Reset();
            txtNumberOfDay.Reset();
            foreach (var item in checkGroupDay.CheckedItems)
            {
                item.Checked = false;
            }
        }

        protected void Delete(object sender, DirectEventArgs directEventArgs)
        {
            try
            {
                int id;
                if (!int.TryParse(hdfKeyRecord.Text, out id) || id <= 0) return;
                hr_TimeSheetRuleByWeekServices.Delete(id);
                gridTimeSheetRule.Reload();
                RM.RegisterClientScriptBlock("Grid_Reload", "ReloadGrid();");
            }
            catch (Exception ex)
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình xóa: {0}".FormatWith(ex.Message));
            }
        }

        protected void EditTimeSheetRule_Click(object sender, DirectEventArgs e)
        {
            foreach (var item in checkGroupDay.CheckedItems)
            {
                item.Checked = false;
            }
            int id;
            if (!int.TryParse(hdfKeyRecord.Text, out id) || id <= 0) return;
            var timeSheet = hr_TimeSheetRuleByWeekServices.GetById(id);
            if (timeSheet != null)
            {
                txtName.Text = timeSheet.Name;
                txtCode.Text = timeSheet.Code;
                //dfSpecifiedDate.SetValue(timeSheet.SpecifiedDate);
                startInputTime.SetValue(timeSheet.StartInputTime.Substring(0, 2) + ":" + timeSheet.StartInputTime.Substring(2, 2));
                endInputTime.SetValue(timeSheet.EndInputTime.Substring(0, 2) + ":" + timeSheet.EndInputTime.Substring(2, 2));
                startOutputTime.SetValue(timeSheet.StartOutputTime.Substring(0, 2) + ":" + timeSheet.StartOutputTime.Substring(2, 2));
                endOutputTime.SetValue(timeSheet.EndOutputTime.Substring(0, 2) + ":" + timeSheet.EndOutputTime.Substring(2, 2));
                txtNumberOfDay.Text = timeSheet.WorkConvert.ToString(CultureInfo.InvariantCulture);
                if (timeSheet.DayOfWeek.Contains("Monday"))
                {
                    chkMonday.Checked = true;
                }
                if (timeSheet.DayOfWeek.Contains("Tuesday"))
                {
                    chkTuesday.Checked = true;
                }
                if (timeSheet.DayOfWeek.Contains("Wednesday"))
                {
                    chkWednesday.Checked = true;
                }
                if (timeSheet.DayOfWeek.Contains("Thursday"))
                {
                    chkThursday.Checked = true;
                }
                if (timeSheet.DayOfWeek.Contains("Friday"))
                {
                    chkFriday.Checked = true;
                }
                if (timeSheet.DayOfWeek.Contains("Saturday"))
                {
                    chkSaturday.Checked = true;
                }
                if (timeSheet.DayOfWeek.Contains("Sunday"))
                {
                    chkSunday.Checked = true;
                }
            }
            // show window
            btnUpdate.Show();
            btnUpdateClose.Hide();

            wdTimeSheetRule.Title = @"Cập nhật luật chấm công theo tuần";
            wdTimeSheetRule.Show();
        }

        #endregion
    }
}