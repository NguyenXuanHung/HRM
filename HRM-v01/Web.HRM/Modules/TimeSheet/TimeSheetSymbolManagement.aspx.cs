using Ext.Net;
using System;
using System.Globalization;
using Web.Core;
using Web.Core.Framework;
using Web.Core.Framework.Controller.TimeSheet;
using Web.Core.Object.TimeSheet;

namespace Web.HRM.Modules.TimeSheet
{

    public partial class TimeSheetSymbolManagement : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ExtNet.IsAjaxRequest) return;
            //init
            hdfStatus.Text = ((int) TimeSheetStatus.Active).ToString();
            if (btnEdit.Visible)
            {
                gridTimeSheetSymbol.Listeners.RowDblClick.Handler += " if(CheckSelectedRows(gridTimeSheetSymbol)){btnUpdate.show();btnUpdateNew.hide();btnUpdateClose.hide();}";
            }
        }

        #region Event Method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AjaxColor_Changed(object sender, DirectEventArgs e)
        {
            dropDownSymbol.Text = ColorPalette2.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void EditTimeSheetSymbol_Click(object sender, DirectEventArgs e)
        {
            if (!string.IsNullOrEmpty(hdfKeyRecord.Text))
            {
                var symbol = TimeSheetSymbolController.GetById(Convert.ToInt32(hdfKeyRecord.Text));
                if (symbol != null)
                {
                    txtNumberOfDay.Text = (symbol.WorkConvert.ToString(CultureInfo.InvariantCulture).Replace("-", "")).Replace(".", ",");
                    txtTimeConvert.Text = symbol.TimeConvert.ToString(CultureInfo.InvariantCulture).Replace(".", ",");
                    chk_Status.Checked = symbol.Status == TimeSheetStatus.Active;
                    txtSymbolCode.Text = symbol.Code;
                    txtSymbolName.Text = symbol.Name;
                    if (!string.IsNullOrEmpty(symbol.Color))
                    {
                        dropDownSymbol.Text = symbol.Color.TrimStart('#');
                    }
                    txtNote.Text = symbol.Description;
                    txtOrder.Text = symbol.Order.ToString();
                    hdfGroupSymbolId.Text = symbol.GroupSymbolId.ToString();
                    cbxGroupSymbol.Text = symbol.GroupSymbolName;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InsertOrUpdate(object sender, DirectEventArgs e)
        {
            if (e.ExtraParams["Command"] == "Update")
                UpdateSymbol();
            else
                InsertSymbol(e);
            //reload data
            gridTimeSheetSymbol.Reload();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Delete(object sender, DirectEventArgs e)
        {
            // delete symbol
            if (!int.TryParse(hdfKeyRecord.Text, out var id) || id <= 0) return;
            TimeSheetSymbolController.Delete(id);

            // get all event have that symbol
            var timeSheetEvents = TimeSheetEventController.GetAll(null, null, null, null, null, id, false, null, null,
                null, null, null, null, null);

            // delete time sheet event
            if (timeSheetEvents != null)
                foreach (var timeSheetEvent in timeSheetEvents)
                    TimeSheetEventController.Delete(timeSheetEvent.Id);

            // reload
            gridTimeSheetSymbol.Reload();
            RM.RegisterClientScriptBlock("Grid_Reload", "ReloadGrid();");
        }

        #endregion

        #region Private Method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        private void InsertSymbol(DirectEventArgs e)
        {
            var symbol = new TimeSheetSymbolModel(new hr_TimeSheetSymbol
            {
                Code = txtSymbolCode.Text,
                CreatedDate = DateTime.Now,
                CreatedBy = CurrentUser.User.UserName
            });
            //edit data
            EditDataSymbol(symbol);

            if (TimeSheetSymbolController.Create(symbol) == null)
            {
                Dialog.Alert("Ký hiệu chấm công đã tồn tại! Vui lòng chọn ký hiệu chấm công khác!");
                return;
            }
            if (e.ExtraParams["Close"] != "True") return;
            wdTimeSheetSymbol.Hide();
            ResetForm();
        }

        /// <summary>
        /// 
        /// </summary>
        private void UpdateSymbol()
        {
            if (string.IsNullOrEmpty(hdfKeyRecord.Text)) return;
            var symbol = TimeSheetSymbolController.GetById(Convert.ToInt32(hdfKeyRecord.Text));
            EditDataSymbol(symbol);
            if (TimeSheetSymbolController.Update(symbol) == null)
            {
                Dialog.Alert("Ký hiệu chấm công đã tồn tại! Vui lòng chọn ký hiệu chấm công khác!");
                return;
            }
            wdTimeSheetSymbol.Hide();
            ResetForm();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="symbol"></param>
        private void EditDataSymbol(TimeSheetSymbolModel symbol)
        {
            symbol.Code = txtSymbolCode.Text;
            symbol.Color = "#" + dropDownSymbol.Text;
            symbol.Name = txtSymbolName.Text;
            symbol.Description = txtNote.Text;
            symbol.Order = !string.IsNullOrEmpty(txtOrder.Text) ? Convert.ToInt32(txtOrder.Text) : 0;
            if (groupRadioSelectWork.CheckedItems.Count > 0)
            {
                foreach (var item in groupRadioSelectWork.CheckedItems)
                {
                    if (item.ID == "chkAddWork")
                    {
                        // edit work and money add
                        if (!string.IsNullOrEmpty(txtNumberOfDay.Text))
                            symbol.WorkConvert = Convert.ToDouble(txtNumberOfDay.Text);
                        symbol.TypeWork = false; //Cộng
                    }
                    else
                    {
                        // edit work and money sub
                        if (!string.IsNullOrEmpty(txtNumberOfDay.Text))
                            symbol.WorkConvert = Convert.ToDouble(("-" + txtNumberOfDay.Text).ToString(CultureInfo.InvariantCulture));
                        symbol.TypeWork = true; //Trừ
                    }
                }
            }

            if (!string.IsNullOrEmpty(txtTimeConvert.Text))
            {
                symbol.TimeConvert = Convert.ToDouble(txtTimeConvert.Text);
            }

            if (!string.IsNullOrEmpty(hdfGroupSymbolId.Text))
                symbol.GroupSymbolId = int.Parse(hdfGroupSymbolId.Text);
            symbol.Status = chk_Status.Checked ? TimeSheetStatus.Active : TimeSheetStatus.Locked;
        }

        /// <summary>
        /// 
        /// </summary>
        [DirectMethod]
        public void ResetForm()
        {
            txtNumberOfDay.Reset();
            txtSymbolCode.Reset();
            txtSymbolName.Reset();
            txtNote.Reset();
            txtOrder.Reset();
            hdfGroupSymbolId.Reset();
            cbxGroupSymbol.Clear();
            chk_Status.Checked = false;
            txtTimeConvert.Reset();
        }

        #endregion

    }
}