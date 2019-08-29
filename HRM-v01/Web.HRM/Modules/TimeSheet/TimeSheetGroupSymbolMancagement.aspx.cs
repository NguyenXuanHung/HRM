using Ext.Net;
using System;
using Web.Core;
using Web.Core.Framework;
using Web.Core.Framework.Controller.TimeSheet;
using Web.Core.Service.Catalog;

namespace Web.HRM.Modules.TimeSheet
{

    public partial class TimeSheetGroupSymbolManagement : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ExtNet.IsAjaxRequest)
            {
                if (btnEdit.Visible)
                {
                    gridGroupSymbol.Listeners.RowDblClick.Handler += " if(CheckSelectedRows(gridGroupSymbol)){btnUpdate.show();}";
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void EditGroupSymbol_Click(object sender, DirectEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(hdfKeyRecord.Text))
                {
                    var symbol = TimeSheetGroupSymbolController.GetById(Convert.ToInt32(hdfKeyRecord.Text));
                    if (symbol != null)
                    {
                        chk_Status.Checked = symbol.Status == TimeSheetStatus.Active;
                        txtGroupName.Text = symbol.Name;
                    }
                }

                // show window
                btnUpdate.Show();

                wdGroupSymbol.Title = @"Cập nhật nhóm ký hiệu chấm công";
                wdGroupSymbol.Show();
            }
            catch(Exception ex)
            {
                ExtNet.MessageBox.Alert("Có lỗi xảy ra", ex.Message).Show();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UpdateGroup_Click(object sender, DirectEventArgs e)
        {
            try
            {
                if (e.ExtraParams["Command"] == "Update")
                {
                    if (string.IsNullOrEmpty(hdfKeyRecord.Text)) return;
                    var symbol = TimeSheetGroupSymbolController.GetById(Convert.ToInt32(hdfKeyRecord.Text));
                    if (symbol != null)
                    {
                        symbol.Name = txtGroupName.Text;
                        symbol.Status = chk_Status.Checked ? TimeSheetStatus.Active : TimeSheetStatus.Locked;
                        symbol.EditedDate = DateTime.Now;
                    }
                    TimeSheetGroupSymbolController.Update(symbol);
                }

                //reload data
                gridGroupSymbol.Reload();
            }
            catch (Exception ex)
            {
                ExtNet.MessageBox.Alert("Có lỗi xảy ra trong quá trình cập nhật", ex.Message).Show();
            }
        }
     
        /// <summary>
        /// 
        /// </summary>
        [DirectMethod]
        public void ResetForm()
        {
            txtGroupName.Reset();
            chk_Status.Checked = false;
        }
    }
}