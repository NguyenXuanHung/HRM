using System;
using Ext.Net;
using Web.Core.Framework;
using Web.Core.Object.Catalog;
using Web.Core.Service.Catalog;
using Web.Core;

namespace Web.HJM.Modules.Catalog
{
    public partial class CatalogProvince : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ExtNet.IsAjaxRequest)
            {
                hdfCatalogGroupName.Text = "cat_GroupEnum";
                hdfGroupItemType.Text = "cat_Location";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCapNhat_Click(object sender, DirectEventArgs e)
        {
            try
            {
                var location = new cat_Location
                {
                    Description = txtNote.Text,
                    Name = txtProvinceName.Text
                };
                if(!string.IsNullOrEmpty(hdfLocationGroupChildId.Text))
                    location.Group = hdfLocationGroupChildId.Text;

                location.ParentId = !string.IsNullOrEmpty(hdfProvinceParentId.Text) ? Convert.ToInt32(hdfProvinceParentId.Text) : 0;

                if (e.ExtraParams["Command"] == "Edit")
                {
                    if (!string.IsNullOrEmpty(hdfRecordId.Text))
                    {
                        location.Id = Convert.ToInt32(hdfRecordId.Text);
                        cat_LocationServices.Update(location);
                        wdAddWindow.Hide();
                    } 
                }
                else
                {
                    cat_LocationServices.Create(location);
                    Dialog.ShowNotification("Thêm mới thành công");
                    if (e.ExtraParams["Close"] == "True")
                    {
                        wdAddWindow.Hide();
                    }
                }
                txtNote.Reset();
                txtProvinceName.Reset();
                cbxLocationGroup.Reset();
                cbxLocationGroupChild.Reset();
                hdfLocationGroupChildId.Reset();
                cbxLocationParent.Reset();
                hdfProvinceParentId.Reset();
                hdfLocationGroupId.Reset();
                GridPanel1.Reload();
            }
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [DirectMethod]
        public void DuplicateLocation()
        {
            try
            {
                if (string.IsNullOrEmpty(hdfRecordId.Text)) return;
                var location = cat_LocationServices.GetById(Convert.ToInt32(hdfRecordId.Text));
                var newLocation = new cat_Location
                {
                    Name = location.Name,
                    Description = location.Description,
                    Group = location.Group,
                    ParentId = location.ParentId
                };
                cat_LocationServices.Create(newLocation);

                GridPanel1.Reload();
            }
            catch (Exception ex)
            {

                Dialog.ShowNotification("Đã có lỗi xảy ra trong quá trình nhân đôi dữ liệu !");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pr_key"></param>
        [DirectMethod]
        public void DeleteProvince(string pr_key)
        {
            try
            {
                cat_LocationServices.Delete(Convert.ToInt32(pr_key));
                hdfRecordId.Text = "";
            }
            catch (Exception ex)
            {
                Dialog.ShowError("Tỉnh, thành phố này đang được sử dụng. Bạn không được phép xóa!");
                GridPanel1.Reload();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [DirectMethod]
        public void ResetWindowTitle()
        {
            wdAddWindow.Icon = Icon.Add;
            wdAddWindow.Title = "Thêm mới tỉnh, thành phố";
            hdfCommand.SetValue("");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEdit_Click(object sender, DirectEventArgs e)
        {
            var location = cat_LocationServices.GetById(Convert.ToInt32(hdfRecordId.Text));
            var condition = "[Group] like N'%{0}%' ".FormatWith(location.Group);
            txtProvinceName.Text = location.Name;
            txtNote.Text = location.Description;
            cbxLocationGroupChild.Text = cat_GroupEnumServices.GetByCondition(condition).Name;
            cbxLocationParent.Text = cat_LocationServices.GetFieldValueById(location.ParentId);
            hdfProvinceParentId.Text = location.ParentId.ToString();
            hdfLocationGroupId.Text = cat_LocationServices.GetFieldValueById(location.ParentId, "Group");
            var conditionGroup = "[Group] like N'%{0}%' ".FormatWith(cat_LocationServices.GetFieldValueById(location.ParentId, "Group"));
            cbxLocationGroup.Text = cat_GroupEnumServices.GetByCondition(conditionGroup).Name;
            hdfLocationGroupChildId.Text = location.Group;
            hdfCommand.Text = "Update";
            wdAddWindow.Icon = Icon.Pencil;
            wdAddWindow.Title = "Sửa thông tin tỉnh, thành phố";
            wdAddWindow.Show();
        }
    }
}
