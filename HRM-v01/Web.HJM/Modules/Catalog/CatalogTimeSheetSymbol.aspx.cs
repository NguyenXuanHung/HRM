using Ext.Net;
using System;
using System.Globalization;
using Web.Core;
using Web.Core.Framework;
using Web.Core.Object.Catalog;
using Web.Core.Service.Catalog;

namespace Web.HJM.Modules.Catalog
{

    public partial class CatalogTimeSheetSymbol : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ExtNet.IsAjaxRequest)
            {
                if (btnEdit.Visible)
                {
                    gridTimeSheetSymbol.Listeners.RowDblClick.Handler += " if(CheckSelectedRows(gridTimeSheetSymbol)){btnUpdate.show();btnUpdateNew.hide();btnUpdateClose.hide()}";
                }
            }
        }

        protected void EditTimeSheetSymbol_Click(object sender, DirectEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(hdfKeyRecord.Text))
                {
                    var symbol = cat_TimeSheetSymbolServices.GetById(Convert.ToInt32(hdfKeyRecord.Text));
                    if (symbol != null)
                    {
                        var money = symbol.MoneyConvert.ToString("#,###");
                        txtMoneyOfDay.Text = money.Replace("-", "").Replace(".", ",");
                        txtNumberOfDay.Text = (symbol.WorkConvert.ToString(CultureInfo.InvariantCulture).Replace("-", "")).Replace(".", ",");
                        txtTimeConvert.Text =
                            symbol.TimeConvert.Value.ToString(CultureInfo.InvariantCulture).Replace(".", ",");
                        chk_IsInUsed.Checked = symbol.IsInUsed;
                        txtSymbolCode.Text = symbol.Code;
                        txtSymbolDisplay.Text = symbol.SymbolDisplay;
                        txtSymbolName.Text = symbol.Name;
                        txtNote.Text = symbol.Description;
                        txtOrder.Text = symbol.Order.ToString();
                        hdfGroupSymbol.Text = symbol.Group;
                        var condition = "[ItemType] = N'GroupSymbolType' AND [Group] = '{0}' ".FormatWith(symbol.Group);
                        var groupSymbol = cat_GroupEnumServices.GetByCondition(condition);
                        if (groupSymbol != null)
                            cbxGroupSymbol.Text = groupSymbol.Name;
                        if (!symbol.TypeWork)
                            chkAddWork.Checked = true;
                        else
                            chkMinusWork.Checked = true;
                    }
                }

                // show window
                btnUpdate.Show();
                btnUpdateClose.Hide();
                btnUpdateNew.Hide();

                wdTimeSheetSymbol.Title = @"Cập nhật ký hiệu chấm công";
                wdTimeSheetSymbol.Show();
            }
            catch(Exception ex)
            {
                ExtNet.MessageBox.Alert("Có lỗi xảy ra", ex.Message).Show();
            }
        }
        protected void InsertOrUpdate(object sender, DirectEventArgs e)
        {
            try
            {
                if (e.ExtraParams["Command"] == "Update")
                    UpdateSymbol();
                else
                    InsertSymbol(e);
                //reload data
                gridTimeSheetSymbol.Reload();
            }
            catch (Exception ex)
            {
                ExtNet.MessageBox.Alert("Có lỗi xảy ra", ex.Message).Show();
            }
        }

        private void InsertSymbol(DirectEventArgs e)
        {
            try
            {
                var code = txtSymbolCode.Text;
                var checkExistSymbol = cat_TimeSheetSymbolServices.GetCodeTimeSheetSheetSymbols(null, code);
                if (checkExistSymbol != null && checkExistSymbol.Count>0)
                {
                    Dialog.Alert("Ký hiệu chấm công đã tồn tại! Vui lòng chọn ký hiệu chấm công khác!");
                    return;
                }
                var symbol = new cat_TimeSheetSymbol
                {
                    Code = txtSymbolCode.Text,
                    SymbolDisplay = txtSymbolDisplay.Text,
                    Name = txtSymbolName.Text,
                    Order = !string.IsNullOrEmpty(txtOrder.Text) ? Convert.ToInt32(txtOrder.Text) : 0,
                    Description = txtNote.Text,
                };
                if (groupRadioSelectWork.CheckedItems.Count > 0)
                {
                    foreach (var item in groupRadioSelectWork.CheckedItems)
                    {
                        if (item.ID == "chkAddWork")
                        {
                            EditWorkAndMoneyAdd(symbol);
                            
                            symbol.TypeWork = false;//Cộng
                        }
                        else
                        {
                            EditWorkAndMoneySub(symbol);
                            symbol.TypeWork = true;//Trừ
                        }
                    }
                }

                if (!string.IsNullOrEmpty(txtTimeConvert.Text))
                {
                    symbol.TimeConvert = Convert.ToDouble(txtTimeConvert.Text);
                }
                if (!string.IsNullOrEmpty(hdfGroupSymbol.Text))
                    symbol.Group = hdfGroupSymbol.Text;
                symbol.IsInUsed = chk_IsInUsed.Checked;
                symbol.CreatedDate = DateTime.Now;
                symbol.CreatedBy = CurrentUser.User.UserName;
                cat_TimeSheetSymbolServices.Create(symbol);
                if (e.ExtraParams["Close"] != "True") return;
                wdTimeSheetSymbol.Hide();
                ResetForm();
            }
            catch (Exception ex)
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình thêm mới: {0}".FormatWith(ex.Message));
            }
        }

        private void EditWorkAndMoneySub(cat_TimeSheetSymbol symbol)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtNumberOfDay.Text))
                    symbol.WorkConvert = Convert.ToDouble(("-" + txtNumberOfDay.Text).ToString(CultureInfo.InvariantCulture));
                if (!string.IsNullOrEmpty(txtMoneyOfDay.Text))
                    symbol.MoneyConvert = Convert.ToDouble(("-" + txtMoneyOfDay.Text.Replace(",","")).ToString(CultureInfo.InvariantCulture));
            }
            catch (Exception ex)
            {
                Dialog.ShowNotification("Có lỗi xảy ra" + ex.Message);
            }
        }

        private void EditWorkAndMoneyAdd(cat_TimeSheetSymbol symbol)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtNumberOfDay.Text))
                    symbol.WorkConvert = Convert.ToDouble(txtNumberOfDay.Text);
                if (!string.IsNullOrEmpty(txtMoneyOfDay.Text))
                    symbol.MoneyConvert = Convert.ToDouble(txtMoneyOfDay.Text.Replace(",",""));
            }
            catch (Exception ex)
            {
                Dialog.ShowNotification("Có lỗi xảy ra" + ex.Message);
            }
        }



        private void UpdateSymbol()
        {
            try
            {
                if (string.IsNullOrEmpty(hdfKeyRecord.Text)) return;
                var symbol = cat_TimeSheetSymbolServices.GetById(Convert.ToInt32(hdfKeyRecord.Text));
                if(symbol != null)
                {
                    symbol.Code = txtSymbolCode.Text;
                    symbol.SymbolDisplay = txtSymbolDisplay.Text;
                    symbol.Name = txtSymbolName.Text;
                    symbol.Description = txtNote.Text;
                    symbol.Order = !string.IsNullOrEmpty(txtOrder.Text) ? Convert.ToInt32(txtOrder.Text) : 0;
                    if (groupRadioSelectWork.CheckedItems.Count > 0)
                    {
                        foreach (var item in groupRadioSelectWork.CheckedItems)
                        {
                            if (item.ID == "chkAddWork")
                            {
                               EditWorkAndMoneyAdd(symbol);
                                symbol.TypeWork = false;//Cộng
                            }
                            else
                            {
                               EditWorkAndMoneySub(symbol);
                                symbol.TypeWork = true;//Trừ
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(txtTimeConvert.Text))
                    {
                        symbol.TimeConvert = Convert.ToDouble(txtTimeConvert.Text);
                    }
                    if (!string.IsNullOrEmpty(hdfGroupSymbol.Text))
                        symbol.Group = hdfGroupSymbol.Text;
                    symbol.IsInUsed = chk_IsInUsed.Checked;
                    symbol.EditedDate = DateTime.Now;
                }
                cat_TimeSheetSymbolServices.Update(symbol);
            }
            catch (Exception ex)
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình cập nhật: {0}".FormatWith(ex.Message));
            }
        }
        protected void Delete(object sender, DirectEventArgs e)
        {
            try
            {
                int id;
                if (!int.TryParse(hdfKeyRecord.Text, out id) || id <= 0) return;
                cat_TimeSheetSymbolServices.Delete(id);
                gridTimeSheetSymbol.Reload();
                RM.RegisterClientScriptBlock("Grid_Reload", "ReloadGrid();");
            }
            catch (Exception ex)
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình xóa: {0}".FormatWith(ex.Message));
            }
        }

        [DirectMethod]
        private void ResetForm()
        {
            txtNumberOfDay.Reset();
            txtSymbolCode.Reset();
            txtSymbolDisplay.Reset();
            txtSymbolName.Reset();
            txtNote.Reset();
            txtMoneyOfDay.Reset();
            txtOrder.Reset();
            hdfGroupSymbol.Reset();
            cbxGroupSymbol.Clear();
            chk_IsInUsed.Checked = false;
            txtTimeConvert.Reset();
        }
    }
}