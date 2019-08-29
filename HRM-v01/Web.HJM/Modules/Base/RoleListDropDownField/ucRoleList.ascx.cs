using System;
using Ext.Net;
using Web.Core.Framework;
using Web.Core.Service.Security;

namespace Web.HRM.Modules.UserControl
{
    public partial class Modules_RoleListDropDownField_ucRoleList : BaseUserControl
    {
        public event EventHandler AfterClickAcceptButton;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                LoadRole(TreePanel1);
            }
        }

        protected void btnAccept_Click(object sender, DirectEventArgs e)
        {
            wdRoleList.Hide();
            if (AfterClickAcceptButton != null)
            {
                AfterClickAcceptButton(txtRoleId.Text, null);
            }
        }

        private void LoadRole(TreePanel TreePanelRole)
        {
            var RoleList = RoleServices.GetAll(false, null, null, null);
            Ext.Net.TreeNode root = new Ext.Net.TreeNode();
            TreePanelRole.Root.Clear();
            TreePanelRole.Root.Add(root);
            foreach (var ParentRole in RoleList)
            {
                Ext.Net.TreeNode node = new Ext.Net.TreeNode(ParentRole.RoleName);
                root.Nodes.Add(node);
                node.Checked = ThreeStateBool.False;
                node.Expanded = true;
                node.NodeID = ParentRole.Id.ToString();
            }
        }
    }
}