using System;
using System.Collections.Generic;
using System.Linq;
using Ext.Net;
using WebUI.Entity;
using WebUI.Controller;
using DataController;
using SoftCore;
using Web.Core.Framework;

namespace Web.HJM.Modules.UserControl
{
    public partial class Modules_Base_Form_ConfigLayout : BaseForm, IBaseWindow
    {
        public FormInfo _formInfo { get; set; }
        public string _FormName { get; set; }
        public bool _AllowChangeTable { get; set; }
        public event EventHandler AfterClickUpdateConfig; //sự kiện xảy ra sau khi ấn nút update config

        protected void Page_Load(object sender, EventArgs e)
        {
            InitUpdateForm();
            CheckboxSelectionModel1.Listeners.RowSelect.Handler =
                "document.getElementById('" + hdfRecordId.ClientID + "').value = " + CheckboxSelectionModel1.ClientID +
                ".getSelected().id;" + btnAdduserControl.ClientID + ".enable();";
            StoreFieldInfo.BaseParams.Add(new Ext.Net.Parameter("FormName", _FormName));
            if (!X.IsAjaxRequest)
            {
                wdSetupLayout.Hide();
                BuildTree(TreePanel1.Root);
                RenderJS();
            }
        }

        /// <summary>
        /// Gắn các đoạn mã JS vào các control
        /// </summary>
        private void RenderJS()
        {
            wdConfigElement.Listeners.BeforeShow.Handler =
                btnAdduserControl.ClientID + ".disable();setStore(" + StoreFieldInfo.ClientID + ")";
            BtnClosewdConfigElement.Listeners.Click.Handler = wdConfigElement.ClientID + ".hide();";
            wdConfigWindow_btnCancel.Listeners.Click.Handler = wdConfigWindow.ClientID + ".hide();";
            RowSelectionModel1.Listeners.RowSelect.Handler =
                hdfColumnID.ClientID + ".setValue(" + RowSelectionModel1.ClientID + ".getSelected().id);";
            ddfGroupTree.Listeners.Expand.Handler = "refreshTree(" + TreePanel1.ClientID + ");";
            ddfGroupTree.Listeners.TriggerClick.Handler = "refreshTree(" + TreePanel1.ClientID + ");";
            ddfGroupTree.Listeners.Change.Handler = "$('#sortable').sortable();$('#sortable').disableSelection();";
            btnCreateNewFormGroup.Listeners.Click.Handler = txtGroupTitle.ClientID + ".setValue('');" +
                                                            txtWidth.ClientID + ".setValue('0');" +
                                                            txtHeight.ClientID + ".setValue('0');" +
                                                            cbFormLayout.ClientID + ".setValue('RowLayout');" +
                                                            hdfCommand.ClientID + ".setValue('insert');" +
                                                            chkDisplayText.ClientID + ".checked=false;" +
                                                            btnSaveFormGroupInformation.ClientID + ".enable();";
            btnDeleteGroup.Listeners.Click.Handler = "#{DirectMethods}.btnDeleteGroup_Click();" + Store1.ClientID +
                                                     ".reload();" + btnDeleteGroup.ClientID +
                                                     ".disable();$('#sortable').remove();";
            btnCloseSetupLayout.Listeners.Click.Handler = wdSetupLayout.ClientID + ".hide();";
            wdSetupLayout.Listeners.BeforeShow.Handler =
                "SetGridStore(" + GridPanel1.ClientID + "," + Store1.ClientID + ",#{DirectMethods});" +
                "$('#sortable').sortable();" +
                "$('#sortable').disableSelection();" +
                btnDeleteGroup.ClientID + ".disable();" + btnSaveFormGroupInformation.ClientID + ".disable();";

            btnReloadStore1.Listeners.Click.Handler = Store1.ClientID + ".reload();";

            //listener cho combobox
            cbTableComboBox.Listeners.Select.Handler = ColumnStore.ClientID + ".reload();";
            cbTableSearch.Listeners.Select.Handler = hdfAjaxSearchConfig.ClientID + ".setValue('AjaxSearch');" +
                                                     ColumnStore.ClientID + ".reload();";

            //bắt nhập chiều cao nếu chọn Tab
            chkIsTab.Listeners.Check.Handler = "if(" + chkIsTab.ClientID +
                                               ".checked==true){alert('Bạn nên thiết lập chiều cao cho Tab');" +
                                               txtHeight.ClientID + ".focus();}";
        }

        #region cấu hình layout

        protected void GroupStore_Change(object sender, DirectEventArgs e)
        {
            if (string.IsNullOrEmpty(hdfGroupID.Text))
                return;
            FormGroupInfo groupInfo = FormGroupController.GetInstance().Get(int.Parse(hdfGroupID.Text));
            List<FormElementInfo> columnfield = groupInfo.GetFormElements(1);
            string menuStr = "<li style='display:none;'></li>";
            foreach (FormElementInfo field in columnfield)
            {
                menuStr = string.Concat(menuStr,
                    string.Format("<li id='{0}'>{1}<b onclick=RemoveField('{2}')>.</b></li>", field.ID.ToString(),
                        field.LabelName, field.ID.ToString()));
            }

            string output = @" <div class='demo'>
                            <ul id='sortable'>
                             {0}
                            </ul>
                           </div>";

            lblOutput.Html = string.Format(output, menuStr);
            txtGroupTitle.Text = groupInfo.Title;
            txtWidth.Text = groupInfo.Width.ToString();
            txtHeight.Text = groupInfo.Height.ToString();
            cbFormLayout.SelectedIndex = (groupInfo.Layout == "RowLayout") ? 0 : 1;
            chkDisplayText.Checked = groupInfo.DisplayText;
            chkIsTab.Checked = groupInfo.IsTab;
            for (int i = 0; i < 3; i++)
            {
                if (groupInfo.LabelAlign.Equals(cbLabelAlign.Items[i].Value))
                {
                    cbLabelAlign.SelectedIndex = i;
                    break;
                }
            }

            if (groupInfo.ParentID != 0)
            {
                int index = 0;
                foreach (Ext.Net.ListItem item in cbGroup2.Items)
                {
                    if (item.Value == groupInfo.ParentID.ToString())
                    {
                        cbGroup2.SelectedIndex = index++;
                        cbGroup2.SetValue(groupInfo.ParentID);
                        break;
                    }
                }
            }
            else
            {
                cbGroup2.Clear();
            }

            hdfCommand.Text = "update";
        }

        protected void ColumnStoreRefresh(object sender, StoreRefreshDataEventArgs e)
        {
            try
            {
                if (_formInfo == null)
                {
                    _formInfo = FormController.GetInstance().GetForm(_FormName);
                }

                this.Store1.DataSource = FormElementController.GetInstance().GetFormElements(_formInfo.ID, 1)
                    .Where(p => p.GroupID.HasValue == false || p.GroupID.Value == 0);
                this.Store1.DataBind();
            }
            catch (Exception ex)
            {
                Dialog.ShowError("ConfigLayout.ascx/ColumnStoreRefresh = " + ex.Message);
            }
        }

        /// <summary>
        /// Lưu layout
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveLayout_Click(object sender, DirectEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(hdfGroupID.Text))
                {
                    Dialog.ShowNotification("Bạn chưa chọn nhóm");
                    return;
                }

                int groupid = int.Parse(hdfGroupID.Text);
                string[] columnID = e.ExtraParams["order"].Split(',');
                int position = 0;
                foreach (var item in columnID)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        FormElementInfo element = FormElementController.GetInstance().Get(int.Parse(item));
                        element.GroupID = int.Parse(hdfGroupID.Text);
                        element.Position = position++;
                        FormElementController.GetInstance().Update(element);
                    }
                }

                string[] removedColumnID = hdfRemoveColumnID.Text.ToString().Split(',');
                foreach (string item in removedColumnID)
                {
                    if (!string.IsNullOrEmpty(item) && columnID.Where(p => p == item).Count() == 0)
                    {
                        DataHandler.GetInstance()
                            .ExecuteNonQuery("update FormElements set GroupID = NULL where ID = " + item);
                    }
                }

                hdfRemoveColumnID.Text = "";
            }
            catch (Exception ex)
            {
                Dialog.ShowError("btnSaveLayout_Click : " + ex.Message);
            }

            // Dialog.ShowNotification("Lưu cấu hình thành công", false); 
        }

        [DirectMethod]
        public void btnDeleteGroup_Click() //object sender, DirectEventArgs e
        {
            if (string.IsNullOrEmpty(hdfGroupID.Text))
            {
                Dialog.ShowNotification("Bạn chưa chọn nhóm !");
            }

            try
            {
                FormGroupController.GetInstance().Delete(int.Parse(hdfGroupID.Text));
                //    btnDeleteGroup.Enabled = false;
            }
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }

        [DirectMethod]
        public void SaveRemovedColumnID(string id)
        {
            hdfRemoveColumnID.Text += id + ",";
        }

        protected void btnSaveFormGroupInformation_Click(object sender, DirectEventArgs e)
        {
            if (hdfCommand.Text == "insert")
            {
                try
                {
                    int groupid = 0;
                    if (!string.IsNullOrEmpty(cbGroup2.SelectedItem.Value))
                    {
                        groupid = int.Parse(cbGroup2.SelectedItem.Value);
                    }

                    if (_formInfo == null)
                    {
                        _formInfo = FormController.GetInstance().GetForm(_FormName);
                    }

                    double width = 0;
                    if (txtWidth.Text.Contains("0."))
                        width = Convert.ToDouble(txtWidth.Text) / 10;
                    else
                        width = Convert.ToDouble(txtWidth.Text);
                    int id = FormGroupController.GetInstance().CreateFormGroup(new FormGroupInfo(0,
                        txtGroupTitle.Text,
                        0,
                        _formInfo.ID,
                        width,
                        int.Parse(txtHeight.Text),
                        groupid,
                        cbFormLayout.SelectedItem.Text,
                        chkDisplayText.Checked,
                        cbLabelAlign.SelectedItem.Value,
                        chkIsTab.Checked
                    ));
                    //reload lại store nhóm cấp trên
                    GroupStoreRefresh(sender, null); //có khả năng gây lỗi
                }
                catch (Exception ex)
                {
                    Dialog.ShowError(ex.Message);
                }

                txtGroupTitle.Text = "";
                txtHeight.Text = "0";
                txtWidth.Text = "0";
                chkDisplayText.Checked = false;
                cbFormLayout.SelectedIndex = 0;
            }
            else if (hdfCommand.Text == "update")
            {
                try
                {
                    FormGroupInfo formGroupInfo = FormGroupController.GetInstance().Get(int.Parse(hdfGroupID.Text));
                    formGroupInfo.DisplayText = chkDisplayText.Checked;
                    formGroupInfo.Title = txtGroupTitle.Text;
                    if (txtWidth.Text.Contains("0."))
                        formGroupInfo.Width = double.Parse(txtWidth.Text) / 10;
                    else
                        formGroupInfo.Width = double.Parse(txtWidth.Text);
                    formGroupInfo.Height = int.Parse(txtHeight.Text);
                    formGroupInfo.Layout = cbFormLayout.SelectedItem.Text;
                    formGroupInfo.LabelAlign = cbLabelAlign.SelectedItem.Value;
                    formGroupInfo.IsTab = chkIsTab.Checked;
                    if (!string.IsNullOrEmpty(cbGroup2.SelectedItem.Value))
                        formGroupInfo.ParentID = int.Parse(cbGroup2.SelectedItem.Value);
                    FormGroupController.GetInstance().Update(formGroupInfo);
                }
                catch (Exception ex)
                {
                    Dialog.ShowNotification(ex.Message);
                }
            }

            // hdfCommand.Text = "";
            //    
        }

        #region đoạn code để load tree

        protected void GroupStoreRefresh(object sender, StoreRefreshDataEventArgs e)
        {
            try
            {
                if (_formInfo == null)
                {
                    _formInfo = FormController.GetInstance().GetForm(_FormName);
                }

                List<FormGroupInfo> rs = new List<FormGroupInfo>();
                List<FormGroupInfo> formGroupList = FormGroupController.GetInstance().GetFromGroups(_formInfo.ID, -1);
                GroupStore.DataSource = formGroupList;
                GroupStore.DataBind();
            }
            catch (Exception ex)
            {
                Dialog.ShowError("FormConfigLayout/GroupStoreRefresh = " + ex.Message);
            }
        }

        string script = "";

        private Ext.Net.TreeNodeCollection BuildTree(Ext.Net.TreeNodeCollection nodes)
        {
            try
            {
                script = ddfGroupTree.ClientID + ".collapse();" + ddfGroupTree.ClientID + ".setValue('{0}',false);" +
                         hdfGroupID.ClientID + ".setValue('{1}');" + btnDeleteGroup.ClientID + ".enable();" +
                         btnSaveFormGroupInformation.ClientID + ".enable();";
                if (nodes == null)
                {
                    nodes = new Ext.Net.TreeNodeCollection();
                }

                Ext.Net.TreeNode root = new Ext.Net.TreeNode();
                root.Text = "Danh mục nhóm";
                root.NodeID = "0";
                nodes.Add(root);
                root.Expanded = true;
                if (_formInfo == null)
                    _formInfo = FormController.GetInstance().GetForm(_FormName);
                List<FormGroupInfo> formGroupList = FormGroupController.GetInstance().GetFromGroups(_formInfo.ID, 0);
                foreach (FormGroupInfo item in formGroupList)
                {
                    Ext.Net.TreeNode node = new Ext.Net.TreeNode();
                    node.Expanded = true;
                    node.Text = item.Title;
                    node.NodeID = item.ID.ToString();
                    root.Nodes.Add(node);
                    if (item.IsTab)
                        node.Icon = Icon.Tab;
                    else
                        node.Icon = Icon.Folder;
                    node.Listeners.Click.Handler = string.Format(script, item.Title, item.ID);
                    GetChildNode(node);
                    LoadControl(node, item);
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowError("Form/FormConfigLayout = " + ex.Message);
            }

            return nodes;
        }

        private void GetChildNode(Ext.Net.TreeNode ParentNode)
        {
            List<FormGroupInfo> formGroupList =
                FormGroupController.GetInstance().GetByParentID(int.Parse(ParentNode.NodeID));
            foreach (FormGroupInfo item in formGroupList)
            {
                Ext.Net.TreeNode node = new Ext.Net.TreeNode();
                node.Text = item.Title;
                node.NodeID = item.ID.ToString();
                ParentNode.Nodes.Add(node);
                // node.Icon = item.IsTab ? Icon.Tab : Icon.Folder;
                if (item.IsTab)
                    node.Icon = Icon.Tab;
                else
                    node.Icon = Icon.Folder;
                node.Listeners.Click.Handler = string.Format(script, item.Title, item.ID);
                GetChildNode(node);
                LoadControl(node, item);
            }
        }

        /// <summary>
        /// lấy các phần tử(control) trong nhóm đưa lên tree
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="group"></param>
        private void LoadControl(Ext.Net.TreeNode parent, FormGroupInfo group)
        {
            List<FormElementInfo> element = group.GetFormElements(1);
            if (element.Count() == 0)
                parent.Expanded = true;
            else
                foreach (FormElementInfo item in element)
                {
                    Ext.Net.TreeNode node = new Ext.Net.TreeNode();
                    node.Text = item.LabelName + "_";
                    node.NodeID = item.ID.ToString();
                    node.Leaf = true;
                    // node.Disabled = true;
                    parent.Nodes.Add(node);
                }
        }

        [DirectMethod]
        public string RefreshMenu()
        {
            Ext.Net.TreeNodeCollection nodes = this.BuildTree(null);

            return nodes.ToJson();
        }

        [DirectMethod]
        public void UpdateGroup(object index, string nodeName, object nodeID, object newParentNodeID)
        {
            int i = int.Parse(index.ToString());
            if (nodeName.Contains("_") == false) //Cập nhật thông tin FormGroup
            {
                try
                {

                    List<FormGroupInfo> sameParentGroup =
                        (from t in FormGroupController.GetInstance()
                                .GetByParentID(int.Parse(newParentNodeID.ToString()))
                            where t.Position >= i
                            select t).ToList();
                    foreach (var item in sameParentGroup)
                    {
                        item.Position += 1;
                        FormGroupController.GetInstance().Update(item);
                    }

                    FormGroupInfo group = FormGroupController.GetInstance().Get(int.Parse(nodeID.ToString()));
                    group.ParentID = int.Parse(newParentNodeID.ToString());
                    group.Position = int.Parse(index.ToString());
                    FormGroupController.GetInstance().Update(group);
                }
                catch (Exception ex)
                {
                    Dialog.ShowError("FormConfigLayout/UpdateGroup  " + ex.Message);
                }
            }
            else //Cập nhật thông tin Control
            {
                try
                {
                    int controlId = int.Parse(nodeID.ToString());
                    int groupID = int.Parse(newParentNodeID.ToString());
                    if (_formInfo == null)
                    {
                        _formInfo = FormController.GetInstance().GetForm(_FormName);
                    }

                    List<FormElementInfo> controlList =
                        (from t in FormElementController.GetInstance().GetFormElements(_formInfo.ID, 1)
                            where t.Position >= i && t.GroupID == groupID
                            select t).ToList();
                    foreach (var item in controlList)
                    {
                        item.Position += 1;
                        FormElementController.GetInstance().Update(item);
                    }

                    FormElementInfo controlInfo = FormElementController.GetInstance().Get(controlId);
                    controlInfo.Position = i;
                    controlInfo.GroupID = groupID;
                    FormElementController.GetInstance().Update(controlInfo);
                }
                catch (Exception ex)
                {
                    Dialog.ShowError("FormConfigLayout/UpdateGroup/Update control =   " + ex.Message);
                    throw;
                }
            }
        }

        #endregion

        #endregion


        /// <summary>
        /// Lấy ClientID của window wdConfigWindow để truyền sang Form.ascx phục vụ cho việc gọi JS
        /// </summary>
        /// <returns></returns>
        public string GetWindowInformationConfigClientID()
        {
            return wdConfigWindow.ClientID;
        }

        /// <summary>
        /// Lấy ClientID của window wdConfigElement để truyền sang Form.ascx phục vụ cho việc gọi JS
        /// </summary>
        /// <returns></returns>
        public string GetWindowElementConfigClientID()
        {
            return wdConfigElement.ClientID;
        }

        public string GetwdSetupLayoutClientID()
        {
            return wdSetupLayout.ClientID;
        }

        private static object lockObj = new object();

        protected void HandleFieldChanges(object sender, BeforeStoreChangedEventArgs e)
        {
            ChangeRecords<FormElementInfo> elements = e.DataHandler.ObjectData<FormElementInfo>();
            foreach (FormElementInfo item in elements.Updated)
            {
                lock (lockObj)
                {
                    if (item.GroupID == null)
                        item.GroupID = 0;
                    FormElementController.GetInstance().Update(item);
                }

                if (StoreFieldInfo.UseIdConfirmation)
                {
                    e.ConfirmationList.ConfirmRecord(item.ID.ToString());
                }
            }
        }

        private void InitUpdateForm()
        {
            try
            {
                if (_formInfo == null)
                {
                    _formInfo = FormController.GetInstance().GetForm(_FormName);
                }

                txtTitle.Text = _formInfo.Title;
                txtIcon.Text = _formInfo.Icon;
                SpinnerHeight.Text = _formInfo.Height.ToString();
                SpinnerWidth.Text = _formInfo.Width.ToString();
                TableList1.Visible = _AllowChangeTable;
                TableList1.SetValue(_formInfo.TableName);
                chkResizeable.Checked = _formInfo.Resizeable;
                cbCommandButton.SetValue(_formInfo.CommandButton);
                //load cột ngày tháng để config bắt lỗi ngày tháng
                List<FormElementInfo> FieldList =
                    (from t in FormController.GetInstance().GetForm(_FormName).GetFormElements(-1)
                        where t.Type.Equals("System.DateTime")
                        select t).ToList();
                foreach (var item in FieldList)
                {
                    cbStartDate.Items.Add(new Ext.Net.ListItem(item.ColumnName, item.ColumnName));
                    cbEndDate.Items.Add(new Ext.Net.ListItem(item.ColumnName, item.ColumnName));
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowError("ConfigLayout/InitUpdateForm() = " + ex.Message);
            }
        }

        protected void ColumnStore_RefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            try
            {
                string table = "";
                if (string.IsNullOrEmpty(hdfAjaxSearchConfig.Text))
                    table = cbTableComboBox.SelectedItem.Value;
                else
                {
                    table = cbTableSearch.SelectedItem.Value;
                    cbTableComboBox.Value = "";
                }

                List<string> column = Util.GetInstance().GetColumnOfTable(table);
                List<object> data = new List<object>();

                foreach (string col in column)
                {
                    data.Add(new {Name = col});

                }

                this.ColumnStore.DataSource = data;

                this.ColumnStore.DataBind();
            }
            catch (Exception ex)
            {
                Dialog.ShowError("FormConfigLayout/ColumnStore_RefreshData = " + ex.Message);
            }
        }

        protected void storeTableRefresh(object sender, StoreRefreshDataEventArgs e)
        {
            List<string> tableList = Util.GetInstance().GetTables();
            object[] obj = new object[tableList.Count()];
            int i = 0;
            foreach (var item in tableList)
            {
                obj[i] = new object[] {item};
                i++;
            }

            storeTable.DataSource = obj;
            storeTable.DataBind();
        }

        #region các sự kiện cấu hình control nhập liệu

        /// <summary>
        /// Cấu hình combobox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnwdSetComboBox_Click(object sender, DirectEventArgs e)
        {
            try
            {
                FormElementInfo control = FormElementController.GetInstance().Get(int.Parse(hdfRecordId.Text));
                control.ControlConfig = string.Format("table:{0},text:{1},value:{2},allowInsert:{3}",
                    cbTableComboBox.SelectedItem.Value,
                    cbDisplayColumn.SelectedItem.Value,
                    cbValueColumn.SelectedItem.Value,
                    chkAllowInsert.Checked);
                control.InputControl = InputControl_ComboBox;
                if (control.GroupID.HasValue == false)
                {
                    control.GroupID = 0;
                }

                FormElementController.GetInstance().Update(control);
                wdSetComboBox.Hide();
            }
            catch (Exception ex)
            {
                Dialog.ShowError("Form/btnwdSetComboBox_Click = " + ex.Message);
            }
        }

        /// <summary>
        /// Hiện cửa sổ cấu hình combobox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void mnuComboBox_Click(object sender, DirectEventArgs e)
        {
            try
            {
                FormElementInfo control = FormElementController.GetInstance().Get(int.Parse(hdfRecordId.Text));
                if (control != null && string.IsNullOrEmpty(control.ControlConfig) == false &&
                    control.InputControl == InputControl_ComboBox)
                {
                    string[] config = control.ControlConfig.Split(',');
                    foreach (string item in config)
                    {
                        if (item.StartsWith("table"))
                        {
                            cbTableComboBox.SetValue(item.Replace("table:", ""));
                        }
                        else if (item.StartsWith("value"))
                        {
                            cbValueColumn.SetValue(item.Replace("value:", ""));
                        }
                        else if (item.StartsWith("text"))
                        {
                            cbDisplayColumn.SetValue(item.Replace("text:", ""));
                        }
                        else if (item.StartsWith("allowInsert"))
                        {
                            chkAllowInsert.Checked = bool.Parse(item.Replace("allowInsert:", ""));
                        }
                    }
                }

                wdSetComboBox.Show();
            }
            catch (Exception ex)
            {
                Dialog.ShowError("Base\\Window\\mnuComboBox_Click : " + ex.Message);
            }
        }

        protected void mnuTreePanel_Click(object sender, DirectEventArgs e)
        {

        }

        protected void mnuBooleanControl_Click(object sender, DirectEventArgs e)
        {
            wdBooleanControl.Show();
        }

        protected void mnuImage_Click(object sender, DirectEventArgs e)
        {
            try
            {
                FormElementInfo element = FormElementController.GetInstance().Get(int.Parse(hdfRecordId.Text));
                if (chkAllowChangeImage.Checked)
                    element.ControlConfig = txtImageFolderUrl.Text;
                else
                    element.ControlConfig = "";
                element.InputControl = InputControl_Image;
                if (element.GroupID.HasValue == false)
                {
                    element.GroupID = 0;
                }

                FormElementController.GetInstance().Update(element);
                Dialog.ShowNotification("Thiết lập thành công !");
            }
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }

        protected void mnuTextArea_Click(object sender, DirectEventArgs e)
        {
            try
            {
                FormElementInfo element = FormElementController.GetInstance().Get(int.Parse(hdfRecordId.Text));
                element.ControlConfig = "";
                element.InputControl = InputControl_TextArea;
                if (element.GroupID.HasValue == false)
                {
                    element.GroupID = 0;
                }

                FormElementController.GetInstance().Update(element);
                Dialog.ShowNotification("Thiết lập thành công !");
            }
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// Cập nhật Boolean Control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void wdBooleanControl_Click(object sender, DirectEventArgs e)
        {
            try
            {
                FormElementInfo element = FormElementController.GetInstance().Get(int.Parse(hdfRecordId.Text));
                element.InputControl = InputControl_RadioButton;
                element.ControlConfig = txtTrueLabel.Text + "," + txtFalseLabel.Text;
                if (element.GroupID.HasValue == false)
                {
                    element.GroupID = 0;
                }

                FormElementController.GetInstance().Update(element);
                wdBooleanControl.Hide();
            }
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }

        protected void btnSetDefaultControl_Click(object sender, DirectEventArgs e)
        {
            try
            {
                FormElementInfo element = FormElementController.GetInstance().Get(int.Parse(hdfRecordId.Text));
                element.ControlConfig = "";
                element.InputControl = "";
                if (element.GroupID.HasValue == false)
                {
                    element.GroupID = 0;
                }

                FormElementController.GetInstance().Update(element);
                Dialog.ShowNotification("Chuyển về control mặc định thành công !");
            }
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// Hiện window cấu hình AjaxSearch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void mnuAjaxSearch_Click(object sender, DirectEventArgs e)
        {
            FormElementInfo element = FormElementController.GetInstance().Get(int.Parse(hdfRecordId.Text));
            if (!String.IsNullOrEmpty(element.ControlConfig) && element.InputControl.Equals(InputControl_AjaxSearch))
            {
                string[] config = element.ControlConfig.Split(',');
                foreach (string item in config)
                {
                    if (item.StartsWith("Table"))
                    {
                        cbTableSearch.SetValue(item.Replace("Table:", ""));
                    }
                    else if (item.StartsWith("SearchField"))
                    {
                        cbSearchField.SetValue(item.Replace("SearchField:", ""));
                    }
                    else if (item.StartsWith("ValueField"))
                    {
                        cbValueField.SetValue(item.Replace("ValueField:", ""));
                    }
                    else if (item.StartsWith("DisplayField"))
                    {
                        cbDisplayField.SetValue(item.Replace("DisplayField:", ""));
                    }
                    else if (item.StartsWith("PageSize"))
                    {
                        txtPageSize.Text = item.Replace("PageSize:", "");
                    }
                    else if (item.StartsWith("AllowInsert"))
                    {
                        chkAllowInsertAjaxSearch.Checked = bool.Parse(item.Replace("AllowInsert:", ""));
                    }
                }
            }

            wdSetAjaxSearch.Show();
        }

        protected void btnUpdateHtmlConfig_Click(object sender, DirectEventArgs e)
        {
            FormElementInfo element = FormElementController.GetInstance().Get(int.Parse(hdfRecordId.Text));
            element.InputControl = InputControl_HTMLInput;
            if (string.IsNullOrEmpty(txtHtmlHeight.Text.Trim()) == false)
                element.ControlConfig = "Height:" + txtHtmlHeight.Text;
            if (element.GroupID.HasValue == false)
            {
                element.GroupID = 0;
            }

            FormElementController.GetInstance().Update(element);
            WdUpdateHtmlConfig.Hide();
            Dialog.ShowNotification("Thiết lập thành công !");
        }

        protected void mnuHTMLInput_Click(object sender, DirectEventArgs e)
        {
            FormElementInfo element = FormElementController.GetInstance().Get(int.Parse(hdfRecordId.Text));
            if (!string.IsNullOrEmpty(element.ControlConfig))
                txtHtmlHeight.Text = element.ControlConfig.Replace("Height:", "");
            WdUpdateHtmlConfig.Show();
        }

        /// <summary>
        /// Lưu thông số cấu hình AjaxSearch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSetAjaxSearch_Click(object sender, DirectEventArgs e)
        {
            try
            {
                FormElementInfo element = FormElementController.GetInstance().Get(int.Parse(hdfRecordId.Text));
                element.InputControl = InputControl_AjaxSearch;
                element.ControlConfig = string.Format(
                    "Table:{0},SearchField:{1},ValueField:{2},DisplayField:{3},PageSize:{4},AllowInsert:{5}",
                    cbTableSearch.SelectedItem.Value,
                    cbSearchField.SelectedItem.Value,
                    cbValueField.SelectedItem.Value,
                    cbDisplayField.SelectedItem.Value,
                    txtPageSize.Text,
                    chkAllowInsertAjaxSearch.Checked);
                if (element.GroupID.HasValue == false)
                {
                    element.GroupID = 0;
                }

                FormElementController.GetInstance().Update(element);
                wdSetAjaxSearch.Hide();
            }
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }

        #endregion

        protected void btnUpdateConfig_Click(object sender, DirectEventArgs e)
        {
            if (_formInfo == null)
            {
                _formInfo = FormController.GetInstance().GetForm(_FormName);
            }

            string OldTable = _formInfo.TableName;
            try
            {
                if (TableList1.Visible == true)
                {
                    _formInfo.TableName = TableList1.SelectedItem.Value.ToString();
                }
            }
            catch
            {
            }

            _formInfo.Title = txtTitle.Text;
            _formInfo.Icon = txtIcon.Text;
            _formInfo.Height = int.Parse(SpinnerHeight.Text);
            _formInfo.Width = int.Parse(SpinnerWidth.Text);
            _formInfo.Resizeable = chkResizeable.Checked;
            _formInfo.CommandButton = cbCommandButton.SelectedItem.Value;
            if (FormController.GetInstance().UpdateForm(_formInfo))
            {
                wdConfigWindow.Hide();
                Dialog.ShowNotification("Đã cập nhật thành công !");
                if (OldTable != _formInfo.TableName)
                {
                    _formInfo.DeleteFormElement();
                }

                if (this.AfterClickUpdateConfig != null)
                {
                    this.AfterClickUpdateConfig(this, null);
                }
            }
            else
            {
                Dialog.ShowError("Đã có lỗi xảy ra trong quá trình cập nhật !");
            }
        }

        public void Show()
        {
            wdSetupLayout.Show();
        }
    }
}