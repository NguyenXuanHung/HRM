using System;
using System.Globalization;
using Ext.Net;
using Web.Core.Framework;
using Web.Core;
using Web.Core.Object.HumanRecord;
using System.Linq;
using Web.Core.Service.HumanRecord;
using System.ComponentModel;

namespace Web.HJM.Modules.ProfileHuman.Management
{
    public partial class TimeSheetRuleEarlyOrLateManagement : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ExtNet.IsAjaxRequest)
            {
                hdfDepartments.Text = string.Join(",", CurrentUser.Departments.Select(d => d.Id));

            }

            if (btnEdit.Visible)
            {
                gridTimeSheetRuleEarlyOrLate.Listeners.RowDblClick.Handler +=
                    " if(CheckSelectedRows(gridTimeSheetRuleEarlyOrLate)){btnUpdate.show();btnUpdateClose.hide()}";
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
                gridTimeSheetRuleEarlyOrLate.Reload();
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
                var timeSheet = new hr_TimeSheetRuleEarlyOrLate
                {
                    CreatedDate = DateTime.Now
                };

                EditDataSave(timeSheet);

                hr_TimeSheetRuleEarlyOrLateServices.Create(timeSheet);

                if (e.ExtraParams["Close"] != "True") return;
                wdTimeSheetRule.Hide();
                ResetForm();
            }
            catch (Exception ex)
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình thêm mới: {0}".FormatWith(ex.Message));
            }
        }

        private void EditDataSave(hr_TimeSheetRuleEarlyOrLate timeSheet)
        {
            if (!string.IsNullOrEmpty(txtFromMinute.Text))
                timeSheet.FromMinute = Convert.ToInt32(txtFromMinute.Text);
            if (!string.IsNullOrEmpty(txtToMinute.Text))
                timeSheet.ToMinute = Convert.ToInt32(txtToMinute.Text);
            timeSheet.Type = Convert.ToInt32(cbxType.SelectedItem.Value);
            timeSheet.Symbol = txtSymbol.Text;
            timeSheet.SymbolDisplay = txtSymbolDisplay.Text;
            if (!string.IsNullOrEmpty(txtOrder.Text))
            {
                timeSheet.Order = Convert.ToInt32(txtOrder.Text);
            }

            if (!string.IsNullOrEmpty(txtTimeConvert.Text))
            {
                timeSheet.TimeConvert = Convert.ToDouble(txtTimeConvert.Text);
            }

            if (groupRadioSelectWork.CheckedItems.Count > 0)
            {
                foreach (var item in groupRadioSelectWork.CheckedItems)
                {
                    if (item.ID == "chkAddWork")
                    {
                        EditWorkAndMoneyAdd(timeSheet);
                        timeSheet.TypeAddMinus = 1; //Cộng
                    }
                    else
                    {
                        EditWorkAndMoneySub(timeSheet);
                        timeSheet.TypeAddMinus = 0; //Trừ
                    }
                }
            }
        }

        private void EditWorkAndMoneySub(hr_TimeSheetRuleEarlyOrLate timeSheet)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtWorkConvert.Text))
                    timeSheet.WorkConvert =
                        Convert.ToDouble(
                            ("-" + txtWorkConvert.Text.Replace(".", ",")).ToString(CultureInfo.InvariantCulture));
                if (!string.IsNullOrEmpty(txtMoneyConvert.Text))
                    timeSheet.MoneyConvert =
                        Convert.ToDouble(
                            ("-" + txtMoneyConvert.Text.Replace(".", ",")).ToString(CultureInfo.InvariantCulture));
            }
            catch (Exception ex)
            {
                Dialog.ShowNotification("Có lỗi xảy ra" + ex.Message);
            }
        }

        private void EditWorkAndMoneyAdd(hr_TimeSheetRuleEarlyOrLate timeSheet)
        {
            try
            {
                var workConvert = txtWorkConvert.Text.ToString(CultureInfo.InvariantCulture);

                if (!string.IsNullOrEmpty(txtMoneyConvert.Text))
                    timeSheet.MoneyConvert = Convert.ToDouble(txtMoneyConvert.Text);
                if (!string.IsNullOrEmpty(txtWorkConvert.Text))
                    timeSheet.WorkConvert = Convert.ToDouble(workConvert, CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                Dialog.ShowNotification("Có lỗi xảy ra" + ex.Message);
            }
        }

        private void Update()
        {
            try
            {
                if (string.IsNullOrEmpty(hdfKeyRecord.Text)) return;
                var timeSheet = hr_TimeSheetRuleEarlyOrLateServices.GetById(Convert.ToInt32(hdfKeyRecord.Text));
                if (timeSheet != null)
                {

                    EditDataSave(timeSheet);
                    timeSheet.EditedDate = DateTime.Now;
                }

                hr_TimeSheetRuleEarlyOrLateServices.Update(timeSheet);
            }
            catch (Exception ex)
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình cập nhật: {0}".FormatWith(ex.Message));
            }
        }

        [DirectMethod]
        public void ResetForm()
        {
            txtFromMinute.Reset();
            txtToMinute.Reset();
            txtMoneyConvert.Reset();
            txtTimeConvert.Reset();
            txtWorkConvert.Reset();
            txtOrder.Reset();
            cbxType.Reset();
            txtSymbol.Reset();
            txtSymbolDisplay.Reset();
        }

        protected void Delete(object sender, DirectEventArgs directEventArgs)
        {
            try
            {
                int id;
                if (!int.TryParse(hdfKeyRecord.Text, out id) || id <= 0) return;
                hr_TimeSheetRuleEarlyOrLateServices.Delete(id);
                gridTimeSheetRuleEarlyOrLate.Reload();
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
            var timeSheet = hr_TimeSheetRuleEarlyOrLateServices.GetById(id);
            if (timeSheet != null)
            {
                txtFromMinute.Text = timeSheet.FromMinute.ToString();
                txtToMinute.Text = timeSheet.ToMinute.ToString();
                txtMoneyConvert.Text = timeSheet.MoneyConvert.ToString(CultureInfo.InvariantCulture).Replace("-", "");
                txtWorkConvert.Text = timeSheet.WorkConvert.ToString(CultureInfo.InvariantCulture).Replace("-", "");
                txtTimeConvert.Text = timeSheet.TimeConvert.ToString();
                cbxType.SelectedItem.Value = timeSheet.Type.ToString();
                txtOrder.Text = timeSheet.Order.ToString();
                txtSymbol.Text = timeSheet.Symbol;
                txtSymbolDisplay.Text = timeSheet.SymbolDisplay;
                if (timeSheet.TypeAddMinus == 1)
                {
                    chkAddWork.Checked = true;
                }
                else
                {
                    chkMinusWork.Checked = true;
                }
            }

            // show window
            btnUpdate.Show();
            btnUpdateClose.Hide();

            wdTimeSheetRule.Title = @"Cập nhật cấu hình đi muộn, về sớm";
            wdTimeSheetRule.Show();
        }

        #endregion
    }
}