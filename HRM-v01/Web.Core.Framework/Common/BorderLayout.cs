using System.Linq;

namespace Web.Core.Framework.Common
{
    public class BorderLayout
    {       
        /// <summary>
        /// Đoạn javascript xử lý khi người dùng click vào tree node
        /// </summary>
        public string script { get; set; }
        public int menuID { get; set; }
        public const string nodeID = "#NodeID#";
        public const string noteText = "#NodeText#";
        private string departmentList = "";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="borderLayout"></param>
        /// <param name="currentUser"></param>
        /// <param name="collapsed"></param>
        public void AddDepartmentList(Ext.Net.BorderLayout borderLayout, UserModel currentUser, bool collapsed)
        {
            Ext.Net.Panel panel = new Ext.Net.Panel()
            {
                Width = 220,
                Collapsed = collapsed,
                ID = "pnlCoCauToChuc",
                CtCls = "west-panel",
                Border = false,
                Layout = "BorderLayout",
                Title = "Cơ cấu tổ chức",
                
            };

            Ext.Net.TreePanel treePanel = new Ext.Net.TreePanel()
            {
                RootVisible = false,
                AutoScroll = true,
                ID = "TreeCoCauToChuc",
                Border = false,
                Region = Ext.Net.Region.Center,
                Header = false,
            };
            Ext.Net.Toolbar tb = new Ext.Net.Toolbar();

            Ext.Net.Button btnExpand = new Ext.Net.Button()
            {
                Icon = Ext.Net.Icon.ArrowOut,
                Text = @"Mở rộng"
            };
            btnExpand.Listeners.Click.Handler = "#{TreeCoCauToChuc}.expandAll();";

            Ext.Net.Button btnCollapse = new Ext.Net.Button()
            {
                Text = @"Thu nhỏ",
                Icon = Ext.Net.Icon.ArrowIn
            };
            btnCollapse.Listeners.Click.Handler = "#{TreeCoCauToChuc}.collapseAll();";

            tb.Items.Add(btnExpand);
            tb.Items.Add(btnCollapse);
            treePanel.TopBar.Add(tb);
           
            borderLayout.West.Split = true;
            borderLayout.West.Collapsible = true;
            borderLayout.West.Items.Add(panel);
            panel.Items.Add(treePanel);
            AddData(treePanel, currentUser);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="treePanel"></param>
        /// <param name="currentUser"></param>
        private void AddData(Ext.Net.TreePanel treePanel, UserModel currentUser)
        {
            departmentList = string.Join(",", currentUser.Departments.Select(d => d.Id).ToList());
            if (!string.IsNullOrEmpty(departmentList))
            {
                departmentList = "," + departmentList + ",";
            }
            LoadDepartment(treePanel, currentUser);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TreeCoCauToChuc"></param>
        /// <param name="currentUser"></param>
        private void LoadDepartment(Ext.Net.TreePanel TreeCoCauToChuc, UserModel currentUser)
        {
            var rootDepartment = currentUser.RootDepartment;
            var root = new Ext.Net.TreeNode();
            var node = new Ext.Net.TreeNode
            {
                NodeID = rootDepartment.Id.ToString(),
                Text = rootDepartment.Name,
                Icon = Ext.Net.Icon.House,
                Expanded = true
            };
            root.Nodes.Add(node);
            LoadChildDepartment(rootDepartment.Id, node, currentUser);
            if (!string.IsNullOrEmpty(script))
            {
                node.Listeners.Click.Handler = script.Replace(nodeID, node.NodeID).Replace(noteText, node.Text);
            }
            TreeCoCauToChuc.Root.Clear();
            TreeCoCauToChuc.Root.Add(root);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="parentNode"></param>
        /// <param name="currentUser"></param>
        private void LoadChildDepartment(int parentId, Ext.Net.TreeNode parentNode, UserModel currentUser)
        {
            var lstDepartments = currentUser.Departments.Where(d => d.ParentId == parentId).ToList();
            foreach (var d in lstDepartments)
            {
                var node = new Ext.Net.TreeNode
                {
                    NodeID = d.Id.ToString(),
                    Text = d.Name,
                    Icon = Ext.Net.Icon.Folder,
                    Expanded = true
                };
                parentNode.Nodes.Add(node);
                if (!string.IsNullOrEmpty(script))
                {
                    node.Listeners.Click.Handler = script.Replace(nodeID, node.NodeID).Replace(noteText, node.Text);
                }
                LoadChildDepartment(d.Id, node, currentUser);
            }
        }
    }
}