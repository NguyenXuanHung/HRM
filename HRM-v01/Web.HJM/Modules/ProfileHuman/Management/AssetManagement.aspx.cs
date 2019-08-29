using System;
using Ext.Net;
using Web.Core.Framework;
using Web.Core;
using Web.Core.Object.HumanRecord;
using System.Linq;
using SoftCore;

namespace Web.HJM.Modules.ProfileHuman.Management
{
    public partial class AssetManagement : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ExtNet.IsAjaxRequest)
            {
                hdfDepartments.Text = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
                if (btnEdit.Visible)
                {
                    gridAsset.Listeners.RowDblClick.Handler +=
                        " if(CheckSelectedRows(gridAsset)){btnUpdate.show();btnUpdateNew.hide();btnUpdateClose.hide()}";
                }

                gridAsset.Reload();
            }
        }

        #region Event Method

        protected void InsertOrUpdate(object sender, DirectEventArgs e)
        {
            try
            {
                var controller = new AssetController();
                var asset = new hr_Asset();
                var util = new Util();
                asset.AssetName = txtAssetName.Text;
                asset.AssetCode = txtAssetCode.Text;
                asset.Note = txtNote.Text;
                if (!string.IsNullOrEmpty(hdfEmployeeSelectedId.Text))
                    asset.RecordId = Convert.ToInt32(hdfEmployeeSelectedId.Text);
                asset.CreatedDate = DateTime.Now;
                asset.EditedDate = DateTime.Now;
                asset.Status = txtStatus.Text;
                asset.Quantity = int.Parse("0" + txtQuantity.Text);
                asset.UnitCode = txtUnitCode.Text;
                if (!util.IsDateNull(dfReceiveDate.SelectedDate))
                    asset.ReceiveDate = dfReceiveDate.SelectedDate;
                if (e.ExtraParams["Command"] == "Update")
                {
                    if (!string.IsNullOrEmpty(hdfKeyRecord.Text))
                    {
                        asset.Id = Convert.ToInt32(hdfKeyRecord.Text);
                        controller.Update(asset);
                    }
                }
                else
                {
                    controller.Insert(asset);
                }

                if (e.ExtraParams["Close"] == "True")
                {
                    wdAsset.Hide();
                }

                //reload data
                gridAsset.Reload();
            }
            catch (Exception ex)
            {
                ExtNet.MessageBox.Alert("Có lỗi xảy ra", ex.Message).Show();
            }
        }

        [DirectMethod]
        public void ResetForm()
        {
            txtAssetName.Reset();
            txtAssetCode.Reset();
            txtNote.Reset();
            hdfEmployeeSelectedId.Reset();
            txtQuantity.Text = "";
            txtStatus.Text = "";
            txtUnitCode.Text = "";
            dfReceiveDate.Reset();
            cbxSelectedEmployee.Clear();
        }

        protected void Delete(object sender, DirectEventArgs directEventArgs)
        {
            try
            {
                int id;
                if (!int.TryParse(hdfKeyRecord.Text, out id) || id <= 0) return;
                AssetController.Delete(id);
                gridAsset.Reload();
                RM.RegisterClientScriptBlock("Grid_Reload", "ReloadGrid();");
            }
            catch (Exception ex)
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình xóa: {0}".FormatWith(ex.Message));
            }
        }

        protected void EditAsset_Click(object sender, DirectEventArgs e)
        {
            int id;
            if (!int.TryParse(hdfKeyRecord.Text, out id) || id <= 0) return;
            var asset = AssetController.GetById(id);
            if (asset != null)
            {
                cbxSelectedEmployee.Text = asset.FullName;
                hdfEmployeeSelectedId.Text = asset.RecordId.ToString();
                txtAssetName.Text = asset.AssetName;
                txtAssetCode.Text = asset.AssetCode;
                txtNote.Text = asset.Note;
                txtQuantity.Text = asset.Quantity.ToString();
                txtStatus.Text = asset.Status;
                txtUnitCode.Text = asset.UnitCode;
            }

            // show window
            btnUpdate.Show();
            btnUpdateNew.Hide();
            btnUpdateClose.Hide();
            cbxSelectedEmployee.Disabled = true;
            wdAsset.Title = @"Cập nhật thông tin công cụ cấp phát";
            wdAsset.Show();
        }

        #endregion

    }
}