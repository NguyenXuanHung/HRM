using Ext.Net;
using System;
using Web.Core;
using Web.Core.Framework;
using Web.Core.Helper;
using Web.Core.Object.Catalog;
using Web.Core.Service.Catalog;

namespace Web.HJM.Modules.Catalog
{

    public partial class CatalogBase : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ExtNet.IsAjaxRequest) return;
            if (btnEdit.Visible)
            {
                gridHoliday.Listeners.RowDblClick.Handler +=
                    " if(CheckSelectedRows(gridHoliday)){btnUpdate.show();btnUpdateNew.hide();btnUpdateClose.hide()}";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void EditHoliday_Click(object sender, DirectEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(hdfKeyRecord.Text))
                {
                    var holiday = cat_HolidayServices.GetById(Convert.ToInt32(hdfKeyRecord.Text));
                    if (holiday != null)
                    {
                        txtDay.Text = holiday.Day.ToString();
                        txtMonth.Text = holiday.Month.ToString();
                        txtHolidayName.Text = holiday.Name;
                        hdfGroupHoliday.Text = holiday.Group;
                        var condition =
                            "[ItemType] = N'GroupHolidayType' AND [Group] = '{0}' ".FormatWith(holiday.Group);
                        var groupHoliday = cat_GroupEnumServices.GetByCondition(condition);
                        if (groupHoliday != null)
                            cbxGroupHoliday.Text = groupHoliday.Name;
                    }
                }

                // show window
                btnUpdate.Show();
                btnUpdateClose.Hide();
                btnUpdateNew.Hide();

                wdHoliday.Title = @"Cập nhật ngày lễ tết";
                wdHoliday.Show();
            }
            catch (Exception ex)
            {
                ExtNet.MessageBox.Alert("Có lỗi xảy ra", ex.Message).Show();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InsertOrUpdate(object sender, DirectEventArgs e)
        {
            try
            {
                if (e.ExtraParams["Command"] == "Update")
                    Update();
                else
                    Insert(e);
                //reload data
                gridHoliday.Reload();
            }
            catch (Exception ex)
            {
                ExtNet.MessageBox.Alert("Có lỗi xảy ra", ex.Message).Show();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        private void Insert(DirectEventArgs e)
        {
            try
            {
                var holiday = new cat_Holiday()
                {
                    Name = txtHolidayName.Text,
                    Day = !string.IsNullOrEmpty(txtDay.Text) ? Convert.ToInt32(txtDay.Text) : DateTime.Now.Day,
                    Month = !string.IsNullOrEmpty(txtMonth.Text) ? Convert.ToInt32(txtMonth.Text) : DateTime.Now.Month,
                    Year = !string.IsNullOrEmpty(txtYear.Text) ? Convert.ToInt32(txtYear.Text) : DateTime.Now.Year,
                    DaySolar = !string.IsNullOrEmpty(txtDay.Text) ? Convert.ToInt32(txtDay.Text) : DateTime.Now.Day,
                    MonthSolar = !string.IsNullOrEmpty(txtMonth.Text) ? Convert.ToInt32(txtMonth.Text) : DateTime.Now.Month,
                    YearSolar = !string.IsNullOrEmpty(txtYear.Text) ? Convert.ToInt32(txtYear.Text) : DateTime.Now.Year,
                };
                if (!string.IsNullOrEmpty(hdfGroupHoliday.Text))
                    holiday.Group = hdfGroupHoliday.Text;
                if (hdfGroupHoliday.Text == "AL")
                {
                    var arr = DatetimeHelper.ConvertLunar2Solar(Convert.ToInt32(txtDay.Text), Convert.ToInt32(txtMonth.Text),
                        Convert.ToInt32(txtYear.Text), 0, 7);
                    holiday.DaySolar = arr[0];
                    holiday.MonthSolar = arr[1];
                    holiday.YearSolar = arr[2];
                }
                holiday.CreatedDate = DateTime.Now;
                cat_HolidayServices.Create(holiday);
                ResetForm();
                if (e.ExtraParams["Close"] == "True")
                {
                    wdHoliday.Hide();
                }
            }
            catch (Exception ex)
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình thêm mới: {0}".FormatWith(ex.Message));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void Update()
        {
            try
            {
                if (string.IsNullOrEmpty(hdfKeyRecord.Text)) return;
                var holiday = cat_HolidayServices.GetById(Convert.ToInt32(hdfKeyRecord.Text));
                if (holiday != null)
                {
                    holiday.Name = txtHolidayName.Text;
                    holiday.Day = !string.IsNullOrEmpty(txtDay.Text) ? Convert.ToInt32(txtDay.Text) : 0;
                    holiday.Month = !string.IsNullOrEmpty(txtMonth.Text) ? Convert.ToInt32(txtMonth.Text) : 0;
                    holiday.EditedDate = DateTime.Now;
                    if (!string.IsNullOrEmpty(hdfGroupHoliday.Text))
                        holiday.Group = hdfGroupHoliday.Text;
                }

                cat_HolidayServices.Update(holiday);

            }
            catch (Exception ex)
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình cập nhật: {0}".FormatWith(ex.Message));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Delete(object sender, DirectEventArgs e)
        {
            try
            {
                if (!int.TryParse(hdfKeyRecord.Text, out var id) || id <= 0) return;
                cat_HolidayServices.Delete(id);
                gridHoliday.Reload();
                RM.RegisterClientScriptBlock("Grid_Reload", "ReloadGrid();");
            }
            catch (Exception ex)
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình xóa: {0}".FormatWith(ex.Message));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [DirectMethod]
        public void ResetForm()
        {
            txtDay.Reset();
            txtMonth.Reset();
            txtHolidayName.Reset();
            hdfGroupHoliday.Reset();
            cbxGroupHoliday.Clear();
        }        
    }
}