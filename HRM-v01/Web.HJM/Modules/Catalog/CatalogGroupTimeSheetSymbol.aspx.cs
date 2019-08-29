using Ext.Net;
using System;
using Web.Core.Framework;
using Web.Core.Service.Catalog;

namespace Web.HJM.Modules.Catalog
{

    public partial class CatalogGroupTimeSheetSymbol : BasePage
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

        protected void EditGroupSymbol_Click(object sender, DirectEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(hdfKeyRecord.Text))
                {
                    var symbol = cat_GroupTimeSheetSymbolServices.GetById(Convert.ToInt32(hdfKeyRecord.Text));
                    if (symbol != null)
                    {
                        chk_IsInUsed.Checked = symbol.IsInUsed;
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
        protected void UpdateGroup_Click(object sender, DirectEventArgs e)
        {
            try
            {
                if (e.ExtraParams["Command"] == "Update")
                {
                    if (string.IsNullOrEmpty(hdfKeyRecord.Text)) return;
                    var symbol = cat_GroupTimeSheetSymbolServices.GetById(Convert.ToInt32(hdfKeyRecord.Text));
                    if (symbol != null)
                    {
                        symbol.Name = txtGroupName.Text;
                        symbol.IsInUsed = chk_IsInUsed.Checked;
                        symbol.EditedDate = DateTime.Now;
                    }
                    cat_GroupTimeSheetSymbolServices.Update(symbol);
                }

                //reload data
                gridGroupSymbol.Reload();
            }
            catch (Exception ex)
            {
                ExtNet.MessageBox.Alert("Có lỗi xảy ra trong quá trình cập nhật", ex.Message).Show();
            }
        }
     
        [DirectMethod]
        public void ResetForm()
        {
            txtGroupName.Reset();
            chk_IsInUsed.Checked = false;
        }
    }
}