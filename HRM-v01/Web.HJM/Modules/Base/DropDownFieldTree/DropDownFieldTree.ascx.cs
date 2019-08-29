using System;
using Ext.Net;
using System.Data;
using DataController;
using Web.Core.Framework;

namespace Web.HRM.Modules.UserControl
{
    public partial class Modules_Base_DropDownFieldTree_DropDownFieldTree : BaseUserControl, IBaseControl
    {
        public string TableName { get; set; }
        public string DisplayColumn { get; set; }
        public string NodeIDColumn { get; set; }
        public string ParentIDColumnName { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void TreePanel1_LoadData(object sender, DirectEventArgs e)
        {

            //if (TreePanel1.Root.Count() == 0)
            TreePanel1.Root.Add(GetTreeNode());
        }

        private Ext.Net.TreeNode GetTreeNode()
        {
            Ext.Net.TreeNode TopNode = new Ext.Net.TreeNode();
            string sql = "select " + DisplayColumn + "," + NodeIDColumn + "," + ParentIDColumnName + " from " +
                         TableName + " where ISNULL(" + ParentIDColumnName + ",'0')='0' or " + ParentIDColumnName +
                         " = '0'";
            DataTable table = DataHandler.GetInstance().ExecuteDataTable(sql);
            foreach (DataRow row in table.Rows)
            {
                Ext.Net.TreeNode node = new Ext.Net.TreeNode();
                node.NodeID = row[NodeIDColumn].ToString();
                node.Text = row[DisplayColumn].ToString();
                node.Checked = ThreeStateBool.False;
                TopNode.Nodes.Add(node);
                //   node.Nodes.Add(GetChildNode(TableName,Text,NodeID,ParentColumnName,row[ParentColumnName].ToString()));
            }

            return TopNode;
        }

        private Ext.Net.TreeNode GetChildNode(string ParentIDValue)
        {
            Ext.Net.TreeNode Node = new Ext.Net.TreeNode();
            string sql = "select " + DisplayColumn + "," + NodeIDColumn + "," + ParentIDColumnName + " from " +
                         TableName + " where ISNULL(" + ParentIDColumnName + ",'0')='0' or " + ParentIDColumnName +
                         " = N'" + ParentIDValue + "'";
            DataTable table = DataHandler.GetInstance().ExecuteDataTable(sql);
            foreach (DataRow row in table.Rows)
            {
                Ext.Net.TreeNode child = new Ext.Net.TreeNode();
                child.NodeID = row[NodeIDColumn].ToString();
                child.Text = row[DisplayColumn].ToString();
                child.Checked = ThreeStateBool.False;
                Node.Nodes.Add(child);
                child.Nodes.Add(GetChildNode(row[ParentIDColumnName].ToString()));
            }

            return Node;
        }

        protected void NodeLoad(object sender, NodeLoadEventArgs e)
        {
            string sql = "select " + DisplayColumn + "," + NodeIDColumn + "," + ParentIDColumnName + " from " +
                         TableName + " where ISNULL(" + ParentIDColumnName + ",'0')='0' or " + ParentIDColumnName +
                         " = '0'";
            DataTable table = DataHandler.GetInstance().ExecuteDataTable(sql);
            foreach (DataRow row in table.Rows)
            {
                AsyncTreeNode node = new AsyncTreeNode();
                node.NodeID = row[NodeIDColumn].ToString();
                node.Text = row[DisplayColumn].ToString();
                node.Checked = ThreeStateBool.False;
                e.Nodes.Add(node);
                //   node.Nodes.Add(GetChildNode(TableName,Text,NodeID,ParentColumnName,row[ParentColumnName].ToString()));
            }
            //for (int i = 1; i < 6; i++)
            //{
            //    AsyncTreeNode asyncNode = new AsyncTreeNode();
            //    asyncNode.Text = prefix + e.NodeID + i;
            //    asyncNode.NodeID = e.NodeID + i;
            //    e.Nodes.Add(asyncNode);
            //}

            //for (int i = 6; i < 11; i++)
            //{
            //    Ext.Net.TreeNode treeNode = new Ext.Net.TreeNode();
            //    treeNode.Text = prefix + e.NodeID + i;
            //    treeNode.NodeID = e.NodeID + i;
            //    treeNode.Leaf = true;
            //    e.Nodes.Add(treeNode);
            //}

        }

        public string GetID()
        {
            throw new NotImplementedException();
        }

        public object GetValue()
        {
            throw new NotImplementedException();
        }

        public void SetValue(object value)
        {
            throw new NotImplementedException();
        }
    }
}