using System;
using Ext.Net;
using Web.Core;
using Web.Core.Framework;
using Web.Core.Object.Catalog;
using Web.Core.Service.Catalog;

namespace Web.HRM.Modules.Catalog
{

    public partial class DepartmentManagement : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        { }

        #region Event Methods 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cbxParent_Store_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            cbxParent_Store.DataSource = cat_DepartmentServices.GetTree(null);
            cbxParent.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        protected void InitWindowDepartment(object sender, DirectEventArgs e)
        {
            if (int.TryParse(hdfRecordId.Text, out var id) && id > 0)
            {
                var department = cat_DepartmentServices.GetById(id);
                if (department != null)
                {
                    // init department properties
                    txtName.Text = department.Name;
                    txtShortName.Text = department.ShortName;
                    txtAddress.Text = department.Address;
                    txtTelephone.Text = department.Telephone;
                    txtFax.Text = department.Fax;
                    txtTaxCode.Text = department.TaxCode;
                    txtOrder.Text = department.Order.ToString();
                    chkIsPrimary.Checked = department.IsPrimary;
                    chkIsLocked.Checked = department.IsLocked;
                    // set parent id
                    if (department.ParentId > 0)
                    {
                        cbxParent.Text = cat_DepartmentServices.GetFieldValueById(department.ParentId);
                    }

                    // init command name & window properties
                    hdfCommandName.Text = @"Update";
                    wdAddDepartment.Icon = Icon.Pencil;
                    wdAddDepartment.Title = @"Cập nhật thông tin đơn vị";
                    // show window
                    wdAddDepartment.Show();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InsertOrUpdate(object sender, DirectEventArgs e)
        {
            //Check validate
            if (!chkIsPrimary.Checked)
            {
                if (string.IsNullOrEmpty(cbxParent.SelectedItem.Value))
                {
                    Dialog.ShowNotification("Bạn chưa chọn đơn vị cấp cha");
                    return;
                }
            }

            if (hdfCommandName.Text == @"Update")
            {
                // update
                Update();
            }
            else
            {
                // insert
                Insert();
            }
            // check close param
            if (e.ExtraParams["Close"] == "True")
            {
                // hide window
                wdAddDepartment.Hide();
                // reload data
                tgDonVi.ReloadAsyncNode("0", new JFunction());
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
                if (int.TryParse(hdfRecordId.Text, out var id) && id > 0)
                {
                    cat_DepartmentServices.Delete(id);
                }
            }
            catch (Exception ex)
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình xóa: {0}".FormatWith(ex.Message));
            }
        }

        #endregion

        #region Direct Methods

        [DirectMethod]
        public string NodeLoad(string nodeId)
        {
            try
            {
                var nodes = new TreeNodeCollection();

                int? parentId = null;
                if (int.TryParse(nodeId, out var parseParentId))
                    parentId = parseParentId;
                var obj = cat_DepartmentServices.GetAll(parentId, null, null, null, false, null, null, null);

                foreach (var item in obj)
                {
                    var asyncNode = new AsyncTreeNode
                    {
                        Text = item.Name,
                        NodeID = item.Id.ToString(),
                        Icon = Icon.House
                    };
                    asyncNode.CustomAttributes.Add(new ConfigItem("Name", item.Name, ParameterMode.Value));
                    asyncNode.CustomAttributes.Add(new ConfigItem("ShortName", item.ShortName, ParameterMode.Value));
                    asyncNode.CustomAttributes.Add(new ConfigItem("Order", item.Order.ToString(), ParameterMode.Value));
                    asyncNode.CustomAttributes.Add(new ConfigItem("IsPrimary", item.IsPrimary ? "<img  src='/Resource/Images/check.png'>" : "<img src='/Resource/Images/uncheck.gif'>", ParameterMode.Value));
                    asyncNode.CustomAttributes.Add(new ConfigItem("IsLocked", item.IsLocked ? "<img  src='/Resource/Images/check.png'>" : "<img src='/Resource/Images/uncheck.gif'>", ParameterMode.Value));
                    asyncNode.Expanded = true;
                    nodes.Add(asyncNode);
                }

                return nodes.ToJson();
            }
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
                return null;
            }
        }

        [DirectMethod]
        public void ResetWindowTitle()
        {
            wdAddDepartment.Title = @"Thêm mới một đơn vị";
            wdAddDepartment.Icon = Icon.Add;
            hdfCommandName.Text = string.Empty;
            txtName.Text = string.Empty;
            txtShortName.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtTelephone.Text = String.Empty;
            txtFax.Text = string.Empty;
            txtTaxCode.Text = string.Empty;
            txtOrder.Text = @"0";
            cbxParent.Text = string.Empty;
        }


        #endregion

        #region Private Methods
        
        /// <summary>
        /// insert new department
        /// </summary>
        private void Insert()
        {
            try
            {
                var department = new cat_Department
                {
                    Name = txtName.Text.Trim(),
                    ShortName = txtShortName.Text.Trim(),
                    Address = txtAddress.Text.Trim(),
                    Telephone = txtTelephone.Text.Trim(),
                    Fax = txtFax.Text.Trim(),
                    TaxCode = txtTaxCode.Text.Trim(),
                    Order = 0,
                    ParentId = 0,
                    IsPrimary = chkIsPrimary.Checked,
                    IsLocked = chkIsLocked.Checked
                };
                if (int.TryParse(cbxParent.SelectedItem.Value, out var parentId) && parentId >= 0)
                    department.ParentId = parentId;
                if (int.TryParse(txtOrder.Text, out var order))
                    department.Order = order;
                cat_DepartmentServices.Create(department);
            }
            catch (Exception e)
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình thêm mới: {0}".FormatWith(e.Message));
            }

        }

        /// <summary>
        /// update department
        /// </summary>
        private void Update()
        {
            try
            {
                if (int.TryParse(hdfRecordId.Text, out var id) && id > 0)
                {
                    var department = cat_DepartmentServices.GetById(id);
                    if (department != null)
                    {
                        department.Name = txtName.Text.Trim();
                        department.ShortName = txtShortName.Text.Trim();
                        department.Address = txtAddress.Text.Trim();
                        department.Telephone = txtTelephone.Text.Trim();
                        department.Fax = txtFax.Text.Trim();
                        department.TaxCode = txtTaxCode.Text.Trim();
                        if (int.TryParse(txtOrder.Text, out var order) && order > 0)
                            department.Order = order;
                        if (int.TryParse(cbxParent.SelectedItem.Value, out var parentId) && parentId > 0)
                            department.ParentId = parentId;
                        department.IsPrimary = chkIsPrimary.Checked;
                        department.IsLocked = chkIsLocked.Checked;
                        // update
                        cat_DepartmentServices.Update(department);
                    }
                }
            }
            catch (Exception e)
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình cập nhật: {0}".FormatWith(e.Message));
            }

        }

        #endregion
    }
}

