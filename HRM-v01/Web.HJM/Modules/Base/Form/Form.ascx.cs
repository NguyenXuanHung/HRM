using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using DataController;
using WebUI.Entity;
using SoftCore;
using Ext.Net;
using WebUI.Controller;
using System.Data;
//using SoftCore.AccessHistory;
using System.IO;
using Web.Core.Framework;
using Web.Core.Service.Security;

namespace Web.HJM.Modules.UserControl
{
    public partial class Modules_Base_Form_Form : BaseForm, IBaseControl, IBaseWindow
    {
        private const string FormInvalid =
            @"Những trường có dấu(*) là bắt buộc phải nhập hoặc dữ liệu bạn nhập vào không hợp lệ!";

        private const string DefaultErrorMessage = @"Có lỗi xảy ra! Hãy liên hệ với chúng tôi để nhận được sự trợ giúp.";

        public event EventHandler AfterClickUpdateButton;
        public event EventHandler AfterClickUpdateConfig;

        /// <summary>
        /// sự kiện update do người dùng phát triển thêm
        /// </summary>
        public event EventHandler UpdateButtonClick;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(FormName))
            {
                FormName = GridPanelName + this.ID;
            }

            InitComponent();

            if (CurrentUser.User.IsSuperUser)
            {
                Control ct = Page.LoadControl("~/Modules/Base/Form/Config/ConfigLayout.ascx");
                ct.ID = ID + "frmConfigLayout";
                ((Modules_Base_Form_ConfigLayout) ct).formInfo = formInfo;
                ((Modules_Base_Form_ConfigLayout) ct)._FormName = this.FormName;
                ((Modules_Base_Form_ConfigLayout) ct)._AllowChangeTable = this.AllowChangeTable;
                //  ((Modules_Base_Form_ConfigLayout)ct).AfterClickUpdateConfig = this.AfterClickUpdateConfig;
                plcConfigControl.Controls.Add(ct);
            }
            else
            {
                btnConfig.Visible = false;
            }

            if (!ExtNet.IsAjaxRequest)
            {
                if (CurrentUser.User.IsSuperUser)
                {
                    btnConfig.Visible = true;
                    var ct =
                        plcConfigControl.FindControl(this.ID + "frmConfigLayout") as Modules_Base_Form_ConfigLayout;
                    wdWindow_btnConfigForm.Listeners.Click.Handler =
                        ct.GetWindowInformationConfigClientID() + ".show();";
                    mnuFormControl.Listeners.Click.Handler = ct.GetWindowElementConfigClientID() + ".show();";
                    mnuSetLayout.Listeners.Click.Handler = ct.GetwdSetupLayoutClientID() + ".show();";
                }
                //Tạm thời bỏ confirm khi đóng window là muốn lưu lại hay không ? 
                //    wdWindow.Listeners.BeforeHide.Handler = "ConfirmToSave(#{DirectMethods}," + hdfClickUpdateStatus.ClientID + ");";

                //nếu là form update thì ẩn nút cập nhật đi, chỉ để hiện 1 nút
                if (this.CommandButton != Command.Display)
                {
                    wdWindow.Listeners.BeforeShow.Handler = "if(" + btnAddNewAndClose.ClientID +
                                                            ".getText()=='Cập nhật'){try{" + wdBtnUpdate.ClientID +
                                                            ".hide();}catch(err){}}else{try{" + wdBtnUpdate.ClientID +
                                                            ".show();}catch(err){}}" + hdfClickUpdateStatus.ClientID +
                                                            ".setValue('');";
                }

                wdBtnUpdate.Listeners.Click.Handler = hdfClickUpdateStatus.ClientID + ".setValue('Clicked');";
                //bắt lỗi form  
                wdBtnUpdate.Listeners.Click.Handler = "if (" + frmControl.ClientID +
                                                      ".getForm().isValid()==false){Ext.Msg.alert('Thông báo', '" +
                                                      FormInvalid + "');return false;}";
                btnAddNewAndClose.Listeners.Click.Handler = "if (" + frmControl.ClientID +
                                                            ".getForm().isValid()==false){Ext.Msg.alert('Thông báo', '" +
                                                            FormInvalid +
                                                            "');return false;}else{" + hdfClickUpdateStatus.ClientID +
                                                            ".setValue('Clicked');}";
                btnUpdate.Listeners.Click.Handler = "if (" + frmControl.ClientID +
                                                    ".getForm().isValid()==false){Ext.Msg.alert('Thông báo', '" +
                                                    FormInvalid + "');return false;}";
                btnCloseForm.Listeners.Click.Handler = wdWindow.ClientID + ".hide();";
            }
        }

        #region Khởi tạo Form và các Control trên form

        private void InitComponent()
        {
            try
            {
                formInfo = FormController.GetInstance().GetForm(FormName);
                if (formInfo == null)
                {
                    formInfo = new FormInfo();
                    formInfo.FormName = FormName;
                    formInfo.TableName = TableName;
                    if (!string.IsNullOrEmpty(GridPanelName))
                    {
                        GridPanelInfo gridInfo = GridController.GetInstance().GetGridPanel(GridPanelName);
                        formInfo.TableName = gridInfo.TableName;
                        AllowChangeTable = false;
                        PrimaryColumnName = Util.GetInstance().GetPrimaryKeyOfTable(gridInfo.TableName);
                    }

                    formInfo.Title = "Tiêu đề";
                    if (this.FormType == FormKind.Window)
                    {
                        formInfo.Width = 500;
                        formInfo.Height = 0;
                    }
                    else
                    {
                        formInfo.Width = 0;
                        formInfo.Height = 200;
                    }

                    int formid = FormController.GetInstance().InsertForm(formInfo);
                    formInfo = FormController.GetInstance().GetForm(FormName);
                }

                this.Width = (int) formInfo.Width;
                this.Height = (int) formInfo.Height;
                switch (formInfo.CommandButton)
                {
                    case "Update":
                        CommandButton = Command.Update;
                        break;
                    case "Insert":
                        CommandButton = Command.Insert;
                        break;
                    case "Display":
                        CommandButton = Command.Display;
                        break;
                }

                if (FormType == FormKind.Form)
                {
                    SettingForm();
                }
                else if (FormType == FormKind.Window)
                {
                    SettingWindow();
                }

                if (!string.IsNullOrEmpty(formInfo.TableName))
                {
                    if (!string.IsNullOrEmpty(GridPanelName))
                    {
                        GridPanelInfo grid = GridController.GetInstance().GetGridPanel(GridPanelName);

                        if (formInfo.TableName != grid.TableName)
                        {
                            formInfo.DeleteFormElement();
                            formInfo.TableName = grid.TableName;
                            FormController.GetInstance().UpdateForm(formInfo);
                        }

                        PrimaryColumnName = Util.GetInstance().GetPrimaryKeyOfTable(grid.TableName);
                        AllowChangeTable = false;
                    }

                    List<FormElementInfo> elementInfoList = new List<FormElementInfo>();
                    if (string.IsNullOrEmpty(FormReference) == false
                    ) //nếu có form tham chiếu thì sẽ lấy control của form tham chiếu đó
                    {
                        elementInfoList = FormController.GetInstance().GetForm(FormReference).GetFormElements(-1);
                        mnuFormControl.Visible = false;
                    }
                    else
                    {
                        elementInfoList = formInfo.GetFormElements(-1);
                    }

                    if (elementInfoList.Count() == 0)
                    {
                        if (formInfo.CreateFormElement())
                        {
                            GenerateControls(formInfo.GetFormElements(-1));
                        }
                    }
                    else
                    {
                        GenerateControls(elementInfoList);
                    }
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowError("Form/InitComponent() = " + ex.Message + "Formname = " + FormName);
            }
        }

        private void SettingForm()
        {
            try
            {
                frmControl.Border = AllowBorder;
                frmControl.Title = formInfo.Title;
                frmControl.Header = AllowHeader;
                if (!string.IsNullOrEmpty(formInfo.Icon))
                {
                    frmControl.IconCls = formInfo.Icon;
                }

                if (formInfo.Width > 1)
                {
                    frmControl.Width = (int) formInfo.Width;
                }
                else if (formInfo.Width >= 0 && formInfo.Width <= 1)
                {
                    frmControl.AnchorHorizontal = (formInfo.Width * 100) + "%";
                }

                if (formInfo.Height.HasValue && formInfo.Height.Value != 0)
                {
                    frmControl.Height = formInfo.Height.Value;
                }
                else
                {
                    frmControl.AutoHeight = true;
                }

                if (CommandButton == Command.Insert)
                {
                    btnUpdate.Visible = true;
                    btnUpdate.Text = "Thêm mới";
                    btnUpdate.Icon = Icon.Add;
                }
                else if (CommandButton == Command.Update)
                {
                    btnUpdate.Visible = true;
                    btnUpdate.Text = "Cập nhật";
                }
                else
                {
                    //ẩn cả thanh toolbar
                    btnUpdate.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowError("Form/SettingForm = " + ex.Message);
            }
        }

        private void SettingWindow()
        {
            try
            {
                wdWindow.Items.Add(frmControl);
                wdWindow.Title = formInfo.Title;
                if (!string.IsNullOrEmpty(formInfo.Icon))
                {
                    wdWindow.IconCls = formInfo.Icon;
                }

                if (formInfo.Width > 1)
                {
                    wdWindow.Width = (int) formInfo.Width;
                }
                else if (formInfo.Width >= 0 && formInfo.Width <= 1)
                {
                    wdWindow.AnchorHorizontal = (formInfo.Width * 100) + "%";
                }

                if (formInfo.Height.HasValue && formInfo.Height.Value != 0)
                {
                    wdWindow.Height = formInfo.Height.Value;
                }
                else
                {
                    wdWindow.AutoHeight = true;
                }

                wdWindow.Resizable = formInfo.Resizeable;
                wdWindow.Maximizable = formInfo.Resizeable;
                frmControl.AnchorHorizontal = "100%";
                btnUpdate.Visible = false;
                if (CommandButton == Command.Insert)
                {
                    wdBtnUpdate.Visible = true;
                    btnAddNewAndClose.Visible = true;
                    btnAddNewAndClose.Text = "Cập nhật & đóng lại";
                }
                else if (CommandButton == Command.Update)
                {
                    wdBtnUpdate.Visible = false;
                    btnAddNewAndClose.Visible = true;
                    btnAddNewAndClose.Text = "Cập nhật";
                    btnAddNewAndClose.Icon = Icon.Disk;
                }
                else
                {
                    //  ToolBarControl.Visible = false;
                    wdBtnUpdate.Visible = false;
                    btnAddNewAndClose.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowError("Form/SettingWindow = " + ex.Message);
            }
        }

        private void GenerateControls(List<FormElementInfo> elementInfoList)
        {
            try
            {
                if (!string.IsNullOrEmpty(PrimaryColumnValue) && formInfo == null)
                {
                    formInfo = FormController.GetInstance().GetForm(FormName); //GridPanelName + this.ID);
                }

                if (string.IsNullOrEmpty(FormReference) == false) //Lấy form tham chiếu
                {
                    formInfo = FormController.GetInstance().GetForm(FormReference);
                }

                //Lấy danh sách các tab
                var TabList = FormGroupController.GetInstance().GetFromGroups(formInfo.ID, true, 0);
                if (TabList.Count() != 0)
                {
                    var TabHeight =
                        TabList.OrderByDescending(t => t.Height).FirstOrDefault()
                            .Height; //gán độ cao = độ cao của tab cao nhất
                    var tabPanel = new TabPanel()
                    {
                        Border = false,
                        ID = "tabpanel" + ID,
                    };
                    foreach (FormGroupInfo item in TabList)
                    {
                        Ext.Net.Panel panel = new Ext.Net.Panel(item.Title)
                        {
                            Padding = 6,
                            Border = false,
                            ID = "panel" + ID + item.ID,
                            AutoScroll = false,
                        };
                        if (TabHeight != 0)
                            panel.Height = TabHeight;
                        else
                            panel.AutoHeight = true;

                        var c = new Container()
                        {
                            ID = panel.ID + "container",
                            AnchorHorizontal = "90%", //tạm thời để
                            Layout = item.Layout,
                            Height = panel.Height,
                        };

                        switch (item.LabelAlign)
                        {
                            case "Left":
                                c.LabelAlign = LabelAlign.Left;
                                break;
                            case "Right":
                                c.LabelAlign = LabelAlign.Right;
                                break;
                            case "Top":
                                c.LabelAlign = LabelAlign.Top;
                                break;
                        }

                        panel.Items.Add(c);
                        tabPanel.Items.Add(panel);
                        GenerateControlInTab(item, c);
                    }

                    frmControl.Items.Add(tabPanel);
                    frmControl.Padding = 0;
                }
                else
                {
                    //Lấy form group cấp cao nhất, tức là group có ParentID = 0
                    List<FormGroupInfo> groupList = FormGroupController.GetInstance().GetFromGroups(formInfo.ID, 0);
                    if (groupList.Count() != 0)
                    {
                        foreach (FormGroupInfo group in groupList)
                        {
                            Ext.Net.Container panel = new Ext.Net.Container();
                            panel.Height = group.Height != 0 ? group.Height : 30;
                            switch (group.LabelAlign)
                            {
                                case "Left":
                                    panel.LabelAlign = LabelAlign.Left;
                                    break;
                                case "Right":
                                    panel.LabelAlign = LabelAlign.Right;
                                    break;
                                case "Top":
                                    panel.LabelAlign = LabelAlign.Top;
                                    panel.Height = group.Height != 0 ? group.Height : 60;
                                    break;
                            }

                            panel.Layout = group.Layout;
                            if (group.DisplayText)
                            {
                                Ext.Net.FieldSet fieldSet = new FieldSet();
                                Ext.Net.Container newContainer = new Container();
                                fieldSet.ID = this.ID + group.ID + group.ParentID; //Tạo ra 1 cái tên duy nhất
                                fieldSet.Title = group.Title;
                                fieldSet.AutoHeight = true;
                                fieldSet.Items.Add(panel);
                                if (group.Width > 0 && group.Width <= 1)
                                {
                                    fieldSet.ColumnWidth = group.Width;
                                    newContainer.ColumnWidth = group.Width;
                                }

                                fieldSet.Items.Add(panel);
                                newContainer.Items.Add(fieldSet);
                                frmControl.Items.Add(newContainer);
                            }
                            else
                            {
                                frmControl.Items.Add(panel);
                            }

                            GenerateControlBaseOnControlList(group.GetFormElements(-1), panel, group);
                            GenerateChildGroup(group, panel); //generate child group 
                        }

                        //Generate các control chưa được đưa vào nhóm nào
                        List<FormElementInfo> rs = elementInfoList
                            .Where(p => p.GroupID == 0 || p.GroupID.HasValue == false).OrderBy(t => t.Position)
                            .ToList();
                        if (rs.Count() != 0)
                            GenerateControlBaseOnControlList(rs, null, null);
                    }
                    else
                    {
                        GenerateControlBaseOnControlList(elementInfoList, null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowError("Form/GenerateControl = " + ex.Message);
            }
        }

        /// <summary>
        /// Generate các control bên trong Tab
        /// </summary>
        /// <param name="ParentGroup"></param>
        /// <param name="panel"></param>
        private void GenerateControlInTab(FormGroupInfo ParentGroup, Ext.Net.Container panel)
        {
            try
            {
                //Lấy các nhóm con của tab, bao gồm cả tab và container
                List<FormGroupInfo> groupList = FormGroupController.GetInstance().GetByParentID(ParentGroup.ID);
                //Lọc lấy các tab
                List<FormGroupInfo> Tablist = groupList.Where(t => t.IsTab == true).ToList();
                //Lọc lấy các container
                List<FormGroupInfo> ContainerList = groupList.Where(t => t.IsTab == false).ToList();
                foreach (FormGroupInfo group in ContainerList)
                {
                    Ext.Net.Container container = new Ext.Net.Container();
                    container.Height = group.Height != 0 ? group.Height : 30;
                    container.ID = "GenerateControlInTab_container" + group.ID;
                    switch (group.LabelAlign)
                    {
                        case "Left":
                            container.LabelAlign = LabelAlign.Left;
                            break;
                        case "Right":
                            container.LabelAlign = LabelAlign.Right;
                            break;
                        case "Top":
                            container.LabelAlign = LabelAlign.Top;
                            container.Height = group.Height != 0 ? group.Height : 60;
                            break;
                    }

                    container.Layout = group.Layout;
                    if (group.Width > 0 && group.Width <= 1)
                        container.ColumnWidth = group.Width;
                    if (group.DisplayText)
                    {
                        Ext.Net.Container newContainer = new Container();
                        newContainer.ID = "GenerateControlInTab_newContainer" + group.ID;
                        Ext.Net.FieldSet fs = new FieldSet();
                        fs.ID = "GenerateControlInTab_fieldSet" + group.ID;
                        fs.AutoHeight = true;
                        fs.Title = group.Title;
                        if (group.Width > 0.1 && group.Width <= 1)
                        {
                            newContainer.ColumnWidth = group.Width;
                        }

                        fs.Items.Add(container);
                        newContainer.Height = group.Height != 0 ? group.Height + 30 : 60;
                        newContainer.Items.Add(fs);
                        panel.Items.Add(newContainer);
                    }
                    else
                    {
                        panel.Items.Add(container);
                    }

                    GenerateControlBaseOnControlList(group.GetFormElements(-1), container, group);
                    GenerateChildGroup(group, container);
                }

                //add các tab con
                if (Tablist.Count() != 0)
                {
                    Ext.Net.TabPanel tabPanel = new TabPanel();
                    tabPanel.ID = "Tabpanel" + this.ID + panel.ID;
                    foreach (FormGroupInfo item in Tablist)
                    {
                        Ext.Net.Panel _panel = new Ext.Net.Panel(item.Title);
                        _panel.Padding = 6;
                        _panel.AutoHeight = true;
                        _panel.Border = false;
                        _panel.ID = "panel" + this.ID + item.ID;
                        tabPanel.Items.Add(_panel);

                        Ext.Net.Container c = new Container();
                        c.ID = _panel.ID + "container";
                        switch (item.LabelAlign)
                        {
                            case "Left":
                                c.LabelAlign = LabelAlign.Left;
                                break;
                            case "Right":
                                c.LabelAlign = LabelAlign.Right;
                                break;
                            case "Top":
                                c.LabelAlign = LabelAlign.Top;
                                break;
                        }

                        c.Layout = item.Layout;
                        c.Height = item.Height;
                        _panel.Items.Add(c);

                        GenerateControlInTab(item, c);
                    }

                    panel.Items.Add(tabPanel);
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowError("Form/GenerateControlInTab = " + ex.Message);
            }
        }

        /// <summary>
        /// Generate ra các child group
        /// </summary>
        /// <param name="group"></param>
        /// <param name="panel"></param>
        private void GenerateChildGroup(FormGroupInfo group, Ext.Net.Container panel)
        {
            try
            {
                List<FormGroupInfo> groupList = FormGroupController.GetInstance().GetByParentID(group.ID);
                int gCount = groupList.Count;
                if (group.ParentID != 0)
                {
                    if (group.Width == 0)
                    {
                        if (panel.Layout == "ColumnLayout" && gCount != 0)
                        {
                            panel.ColumnWidth = 1.0 / (double) gCount;
                        }
                    }
                    else if (group.Width > 0 && group.Width <= 1.0)
                    {
                        panel.ColumnWidth = group.Width;
                    }
                }

                foreach (FormGroupInfo child in groupList)
                {
                    Ext.Net.Container childPanel = new Ext.Net.Container();
                    childPanel.Height = child.Height != 0 ? child.Height : 30;
                    childPanel.Layout = child.Layout;
                    childPanel.ID = "GenerateChildGroup_childPanel" + child.ID;
                    switch (child.LabelAlign)
                    {
                        case "Left":
                            childPanel.LabelAlign = LabelAlign.Left;
                            break;
                        case "Right":
                            childPanel.LabelAlign = LabelAlign.Right;
                            break;
                        case "Top":
                            childPanel.LabelAlign = LabelAlign.Top;
                            childPanel.Height = child.Height != 0 ? child.Height : 60;
                            break;
                    }

                    if (child.DisplayText)
                    {
                        Ext.Net.FieldSet fieldSet = new FieldSet();
                        fieldSet.ID = "GenerateChildGroup_fieldSet" + child.ID;
                        Ext.Net.Container c = new Container();
                        fieldSet.ID = "GenerateChildGroup_c" + child.ID;
                        fieldSet.ID = this.ID + child.ID + child.FormID; //Tạo ra 1 cái tên duy nhất
                        fieldSet.Title = child.Title;
                        fieldSet.AutoHeight = true;
                        fieldSet.Items.Add(childPanel);
                        if (child.Width > 0 && child.Width <= 1)
                        {
                            fieldSet.ColumnWidth = child.Width;
                            c.ColumnWidth = child.Width;
                        }

                        c.ID = "container" + fieldSet.ID;
                        c.Height = childPanel.Height;
                        c.Layout = "Form";
                        c.Items.Add(fieldSet);
                        panel.Items.Add(c);
                    }
                    else
                    {
                        panel.Items.Add(childPanel);
                    }

                    GenerateControlBaseOnControlList(child.GetFormElements(-1), childPanel, child);
                    GenerateChildGroup(child, childPanel);
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowError("Form/GenerateChildGroup = " + ex.Message);
            }
        }

        private void GenerateControlBaseOnControlList(List<FormElementInfo> elementInfoList, Ext.Net.Container panel,
            FormGroupInfo group)
        {
            int elementCount = elementInfoList.Count;
            if (panel != null)
            {
                if (panel.Layout == "ColumnLayout" && elementCount != 0)
                {
                    panel.ColumnWidth = 1.0 / (double) elementCount;
                }
                else if (elementCount == 0)
                {
                    panel.ColumnWidth = 1.0;
                }
            }

            //if (this.CommandButton == Command.display)
            //{
            //    foreach (FormElementInfo element in elementInfoList)
            //    {
            //        GenerateDisplay(element, panel, group);
            //    } 
            //}
            foreach (FormElementInfo element in elementInfoList)
            {
                switch (element.Type)
                {
                    case "System.Double":
                    case "System.Decimal":
                        GenerateNumberField(element, panel, group, true);
                        break;
                    case "System.Int32":
                        if (string.IsNullOrEmpty(element.InputControl))
                        {
                            GenerateNumberField(element, panel, group, false);
                        }
                        else if (element.InputControl.Equals(InputControl_ComboBox))
                        {
                            GenerateComboBox(element, panel, group);
                        }
                        else if (element.InputControl.Equals(InputControl_AjaxSearch))
                        {
                            GenerateAjaxSearch(element, panel, group);
                        }

                        break;
                    case "System.String":
                        if (element.ValidateType.Equals(ValidateType.OnlyAllowNumber))
                        {
                            GenerateNumberField(element, panel, group, false);
                        }
                        else if (string.IsNullOrEmpty(element.InputControl))
                        {
                            GenerateTextField(element, panel, group);
                        }
                        else if (element.InputControl.Equals(InputControl_ComboBox))
                        {
                            GenerateComboBox(element, panel, group);
                        }
                        else if (element.InputControl.Equals(InputControl_AjaxSearch))
                        {
                            GenerateAjaxSearch(element, panel, group);
                        }
                        else if (element.InputControl.Equals(InputControl_HTMLInput))
                        {
                            GenerateHtmlInput(element, panel, group);
                        }
                        else if (element.InputControl.Equals(InputControl_Image))
                        {
                            GenerateImage(element, panel, group);
                        }
                        else if (element.InputControl.Equals(InputControl_TextArea))
                        {
                            GenerateTextArea(element, panel, group);
                        }

                        break;
                    case "System.DateTime":
                        GenerateDatetimeControl(element, panel, group);
                        break;
                    case "System.Boolean":
                        if (string.IsNullOrEmpty(element.InputControl))
                        {
                            GenerateCheckbox(element, panel, group);
                        }
                        else if (element.InputControl.Equals(InputControl_TrueFalseComboBox))
                        {
                            GenerateTrueFalseComboBox(element, panel, group);
                        }
                        else if (element.InputControl.Equals(InputControl_RadioButton))
                        {
                            GenerateRadioButton(element, panel, group);
                        }

                        break;
                    case "UserControl":
                        Control ct = this.Page.LoadControl(element.InputControl);
                        ct.ID = element.ColumnName;
                        plcUserControl.Controls.Add(ct);
                        ct.Visible = element.Visible;
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion

        #region các phương thức để sinh control

        /// <summary>
        /// Generate Combobox
        /// </summary>
        /// <param name="element"></param>
        /// <param name="container"></param>
        /// <param name="group"></param>
        private void GenerateComboBox(FormElementInfo element, Container container, FormGroupInfo group)
        {
            try
            {
                var ct = Page.LoadControl("../Base/ComboBox/ComboBox.ascx");
                var cbBox = ct as BaseComboBox;
                cbBox.TabIndex = (short) element.Position;
                ct.ID = element.ColumnName;
                cbBox.ComboName = GridPanelName + "_" + ID + "_ComboBox_" + element.ColumnName;
                cbBox.FieldLabel = element.LabelName;
                cbBox.Width = element.Width;
                cbBox.Disable = element.Disable;
                cbBox.FieldNote = element.FieldNote;
                if (group != null)
                    switch (group.LabelAlign)
                    {
                        case "Right":
                            cbBox.LabelAlign = LabelAlign.Right;
                            break;
                        case "Top":
                            cbBox.LabelAlign = LabelAlign.Top;
                            break;
                    }
                var config = element.ControlConfig.Split(',');
                foreach (string item in config)
                {
                    if (item.StartsWith("table"))
                    {
                        cbBox.TableName = item.Replace("table:", "");
                    }
                    else if (item.StartsWith("value"))
                    {
                        cbBox.ValueField = item.Replace("value:", "");
                    }
                    else if (item.StartsWith("text"))
                    {
                        cbBox.DisplayField = item.Replace("text:", "");
                    }
                    else if (item.StartsWith("where"))
                    {
                        cbBox.Where = item.Replace("where:", "");
                    }
                    else if (item.StartsWith("allowInsert"))
                    {
                        cbBox.AllowInsert = bool.Parse(item.Replace("allowInsert:", ""));
                    }
                }

                if (container == null)
                {
                    plcUserControl.Controls.Add(ct);
                }
                else
                {
                    if (group != null)
                    {
                        Ext.Net.Container c = new Container();
                        if (group.Width > 0 && group.Width <= 1)
                        {
                            c.AnchorHorizontal = group.Width * 100 + "%";
                        }

                        if (container.ColumnWidth > 0 && container.ColumnWidth <= 1)
                            c.ColumnWidth = container.ColumnWidth;
                        c.Layout = "FormLayout";
                        c.ContentControls.Add(ct);
                        container.Items.Add(c);
                    }
                }

                ct.Visible = element.Visible;
            }
            catch (Exception ex)
            {
                Dialog.ShowError("Form/GenerateComboBox = " + ex.Message);
            }
        }

        private void GenerateTrueFalseComboBox(FormElementInfo element, Ext.Net.Container panel, FormGroupInfo group)
        {
            try
            {
                Ext.Net.ComboBox cbtruefalse = new ComboBox();
                cbtruefalse.TabIndex = (short) element.Position;
                cbtruefalse.Disabled = element.Disable;
                cbtruefalse.EnableViewState = false;
                cbtruefalse.TabIndex = (short) element.Position;
                string[] v = element.ControlConfig.Split(',');
                cbtruefalse.FieldLabel = element.LabelName;
                cbtruefalse.SelectedIndex = 0;
                cbtruefalse.ID = element.ColumnName;
                if (string.IsNullOrEmpty(element.FieldNote) == false)
                    cbtruefalse.Note = element.FieldNote;
                foreach (string item in v)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        string[] tmp = item.Split(':'); //text : value
                        cbtruefalse.Items.Add(new Ext.Net.ListItem(tmp[0], tmp[1]));
                    }
                }

                if (element.Width > 0 && element.Width <= 1)
                    cbtruefalse.AnchorHorizontal = (100 * element.Width) + "%";
                else
                    cbtruefalse.AnchorHorizontal = "100%";
                if (panel == null)
                {
                    frmControl.Items.Add(cbtruefalse);
                    if (element.Width > 1)
                    {
                        cbtruefalse.Width = (int) element.Width;
                    }
                }
                else
                {
                    if (group.Layout == "ColumnLayout")
                    {
                        Ext.Net.Container c = new Container();
                        if (group.Width > 0 && group.Width <= 1)
                        {
                            c.AnchorHorizontal = group.Width * 100 + "%";
                        }

                        c.Layout = "FormLayout";
                        c.ColumnWidth = panel.ColumnWidth;
                        c.Items.Add(cbtruefalse);
                        panel.Items.Add(c);
                    }
                    else
                    {
                        panel.Items.Add(cbtruefalse);
                    }
                }

                cbtruefalse.Visible = element.Visible;
            }
            catch (Exception ex)
            {
                Dialog.ShowError("Form/GenerateTrueFalseComboBox = " + ex.Message);
            }
        }

        private void GenerateRadioButton(FormElementInfo element, Ext.Net.Container panel, FormGroupInfo groupInfo)
        {
            try
            {
                string[] value = element.ControlConfig.Split(',');
                Ext.Net.RadioGroup group = new RadioGroup()
                {
                    Disabled = element.Disable,
                    Enabled = false,
                    TabIndex = (short) element.Position,
                    FieldLabel = element.LabelName,
                    Visible = element.Visible,
                    ID = element.ColumnName,
                };
                if (string.IsNullOrEmpty(element.FieldNote) == false)
                    group.Note = element.FieldNote;
                //true value
                Ext.Net.Radio r = new Radio()
                {
                    EnableViewState = false,
                    LabelWidth = 50,
                    FieldLabel = value[0],
                };
                group.Items.Add(r);
                //r.Checked = true;

                //false value
                Ext.Net.Radio r2 = new Radio()
                {
                    EnableViewState = false,
                    LabelWidth = 50,
                    FieldLabel = value[1],
                };
                group.Items.Add(r2);

                if (panel == null)
                {
                    frmControl.Items.Add(group);
                }
                else
                {
                    if (element.Width > 0 && element.Width <= 1)
                        group.AnchorHorizontal = (100 * element.Width) + "%";
                    else
                        group.AnchorHorizontal = "100%";
                    if (groupInfo.Layout == "ColumnLayout")
                    {
                        Ext.Net.Container c = new Container();
                        if (groupInfo.Width > 0 && groupInfo.Width <= 1)
                        {
                            c.AnchorHorizontal = groupInfo.Width * 100 + "%";
                        }

                        c.Layout = "FormLayout";
                        c.ColumnWidth = panel.ColumnWidth;
                        c.Items.Add(group);
                        panel.Items.Add(c);
                    }
                    else
                    {
                        panel.Items.Add(group);
                    }
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowError("Form/GenerateRadioButton = " + ex.Message);
            }
        }

        private void GenerateDatetimeControl(FormElementInfo element, Ext.Net.Container panel, FormGroupInfo group)
        {
            try
            {
                Ext.Net.DateField dateField = new DateField()
                {
                    Disabled = element.Disable,
                    EnableViewState = false,
                    TabIndex = (short) element.Position,
                    FieldLabel = element.LabelName,
                    ID = element.ColumnName,
                    Format = "dd/MM/yyyy",
                    SelectedDate = DateTime.Now,
                };
                if (string.IsNullOrEmpty(element.FieldNote) == false)
                    dateField.Note = element.FieldNote;
                if (element.Width > 0 && element.Width <= 1)
                    dateField.AnchorHorizontal = (100 * element.Width) + "%";
                else if (element.Width == 0)
                    dateField.AnchorHorizontal = "100%";
                else if (element.Width > 1)
                    dateField.Width = (int) element.Width;
                if (panel == null)
                {
                    frmControl.Items.Add(dateField);
                    if (element.Width > 1)
                    {
                        dateField.Width = (int) element.Width;
                    }
                }
                else
                {
                    if (group.Layout == "ColumnLayout")
                    {
                        Ext.Net.Container c = new Container();
                        if (group.Width > 0 && group.Width <= 1)
                        {
                            c.AnchorHorizontal = group.Width * 100 + "%";
                        }

                        c.Layout = "FormLayout";
                        c.ColumnWidth = panel.ColumnWidth;
                        c.Items.Add(dateField);
                        panel.Items.Add(c);
                    }
                    else
                    {
                        panel.Items.Add(dateField);
                    }
                }

                if (string.IsNullOrEmpty(element.AutoGenerateValue) == false)
                {
                    dateField.Visible = false;
                }
                else
                {
                    dateField.Visible = element.Visible;
                }

                //geneate check lỗi
                if (element.ValidateType.StartsWith(ValidateType.DateRange))
                {
                    try
                    {
                        string[] dateconfig =
                            element.ValidateType.Replace(ValidateType.DateRange + ":", "")
                                .Split('-'); //startdate - control datetime 2
                        dateField.Listeners.KeyUp.Fn = "onKeyUp";
                        ConfigItem cfItem = new ConfigItem(dateconfig[0], "#{" + dateconfig[1] + "}",
                            ParameterMode.Value);
                        dateField.CustomConfig.Add(cfItem);
                        dateField.Vtype = "daterange";
                        dateField.EnableKeyEvents = true;
                    }
                    catch (Exception ex)
                    {
                        Dialog.ShowError("Form/GenerateDatetimeControl = " + ex.Message);
                    }
                }
                else if (element.ValidateType.Equals(ValidateType.DateTimeBiggerThanToday))
                {
                    dateField.MinDate = DateTime.Now;
                }
                else if (element.ValidateType.Equals(ValidateType.DateTimeSmallerThanToday))
                {
                    dateField.MaxDate = DateTime.Now;
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowError("Form/GenerateDatetimeControl = " + ex.Message);
            }
        }

        private void GenerateTextField(FormElementInfo element, Ext.Net.Container panel, FormGroupInfo group)
        {
            try
            {
                Ext.Net.TextField text = new TextField()
                {
                    TabIndex = (short) element.Position,
                    Disabled = element.Disable,
                    EnableViewState = false,
                    ID = element.ColumnName,
                    FieldLabel = element.LabelName,
                };
                if (string.IsNullOrEmpty(element.FieldNote) == false)
                    text.Note = element.FieldNote;
                if (string.IsNullOrEmpty(element.ValidateType) == false)
                {
                    text.AllowBlank = false;
                    text.BlankText = "Bạn phải nhập " + element.LabelName;
                    text.CtCls = "requiredData";
                    text.FieldLabel += "<span style='color:red;'>*</span>";
                }

                if (element.ValidateType.Equals(ValidateType.Email))
                {
                    text.Regex = @"\b[A-Z0-9._%-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b";
                    text.RegexText = "Email bạn nhập không đúng định dạng !";
                }
                else if (element.ValidateType.StartsWith(ValidateType.StringLength))
                {
                    try
                    {
                        string[] range = element.ValidateType.Replace(ValidateType.StringLength + ":", "")
                            .Split('-'); //StringLength:4-50
                        text.MaxLength = int.Parse(range[1]);
                        text.MinLength = int.Parse(range[0]);
                        text.MinLengthText = "Độ dài tối thiểu là " + range[0] + " kí tự";
                        text.MaxLengthText = "Độ dài tối đa là " + range[1] + " kí tự";
                    }
                    catch (Exception ex)
                    {
                        Dialog.ShowError("Form/GenerateTextField = " + ex.Message);
                    }
                }
                else if (element.ValidateType.Equals(ValidateType.OnlyAllowNumber))
                {
                    text.Regex = @"^[0-9]+$";
                    text.RegexText = "Dữ liệu nhập vào phải là kiểu số";
                }

                if (element.Width > 0 && element.Width <= 1)
                    text.AnchorHorizontal = (100 * element.Width) + "%";
                else if (element.Width == 0)
                    text.AnchorHorizontal = "100%";
                else if (element.Width > 1)
                    text.Width = (int) element.Width;
                if (panel == null)
                {
                    frmControl.Items.Add(text);
                    if (element.Width > 1)
                    {
                        text.Width = (int) element.Width;
                    }
                }
                else
                {
                    if (group.Layout == "ColumnLayout")
                    {
                        Ext.Net.Container c = new Container();
                        c.ID = "Container" + element.ColumnName;
                        if (group.Width > 0 && group.Width <= 1)
                        {
                            c.AnchorHorizontal = group.Width * 100 + "%";
                        }

                        c.ColumnWidth = panel.ColumnWidth;
                        c.Layout = "FormLayout";
                        c.Items.Add(text);
                        panel.Items.Add(c);
                    }
                    else
                    {
                        panel.Items.Add(text);
                    }
                }

                if (string.IsNullOrEmpty(element.AutoGenerateValue) == false)
                {
                    text.Visible = false;
                }
                else
                {
                    text.Visible = element.Visible;
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowError("Form/GenerateTextField = " + ex.Message);
            }
        }

        private void GenerateNumberField(FormElementInfo element, Ext.Net.Container panel, FormGroupInfo group,
            bool AllowDecimal)
        {
            try
            {
                Ext.Net.NumberField numberfield = new NumberField()
                {
                    Disabled = element.Disable,
                    EnableViewState = false,
                    TabIndex = (short) element.Position,
                    ID = element.ColumnName,
                    FieldLabel = element.LabelName,
                    AllowDecimals = AllowDecimal,
                };
                if (string.IsNullOrEmpty(element.FieldNote) == false)
                    numberfield.Note = element.FieldNote;
                if (string.IsNullOrEmpty(element.ValidateType) == false)
                {
                    numberfield.AllowBlank = false;
                    numberfield.BlankText = "Bạn phải nhập " + element.LabelName;
                    numberfield.CtCls = "requiredData";
                    numberfield.FieldLabel += "<span style='color:red;'>*</span>";
                }

                if (element.ValidateType.StartsWith(ValidateType.IntegerRange))
                {
                    try
                    {
                        string[] minmax = element.ValidateType.Replace(ValidateType.IntegerRange + ":", "").Split('-');
                        numberfield.MinValue = int.Parse(minmax[0]);
                        numberfield.MaxValue = int.Parse(minmax[1]);
                        numberfield.MaxText = "Giá trị tối đa là " + minmax[1];
                        numberfield.MinText = "Giá trị tối thiểu là " + minmax[0];
                    }
                    catch (Exception ex)
                    {
                        Dialog.ShowError("Form/GenerateNumberField = " + ex.Message);
                    }
                }

                if (element.Width > 0 && element.Width <= 1)
                    numberfield.AnchorHorizontal = (100 * element.Width) + "%";
                else if (element.Width == 0)
                    numberfield.AnchorHorizontal = "100%";
                else if (element.Width > 1)
                    numberfield.Width = (int) element.Width;
                if (panel == null)
                {
                    frmControl.Items.Add(numberfield);
                    if (element.Width > 1)
                    {
                        numberfield.Width = (int) element.Width;
                    }
                }
                else
                {
                    if (group.Layout == "ColumnLayout")
                    {
                        Ext.Net.Container c = new Container();
                        if (group.Width > 0 && group.Width <= 1)
                        {
                            c.AnchorHorizontal = group.Width * 100 + "%";
                        }

                        c.Layout = "FormLayout";
                        c.ColumnWidth = panel.ColumnWidth;
                        c.Items.Add(numberfield);
                        panel.Items.Add(c);
                    }
                    else
                    {
                        panel.Items.Add(numberfield);
                    }
                }

                if (string.IsNullOrEmpty(element.AutoGenerateValue) == false)
                {
                    numberfield.Visible = false;
                }
                else
                {
                    numberfield.Visible = element.Visible;
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowError("Form/GenerateNumberField = " + ex.Message);
            }
        }

        private void GenerateCheckbox(FormElementInfo element, Ext.Net.Container panel, FormGroupInfo group)
        {
            try
            {
                Ext.Net.Checkbox chk = new Checkbox()
                {
                    Disabled = element.Disable,
                    EnableViewState = false,
                    TabIndex = (short) element.Position,
                    ID = element.ColumnName,
                    FieldLabel = element.LabelName,
                };
                if (string.IsNullOrEmpty(element.FieldNote) == false)
                    chk.Note = element.FieldNote;
                if (panel == null)
                {
                    frmControl.Items.Add(chk);
                }
                else
                {
                    if (element.Width > 0 && element.Width <= 1)
                        chk.AnchorHorizontal = (100 * element.Width) + "%";
                    else
                        chk.AnchorHorizontal = "100%";
                    if (group.Layout == "ColumnLayout")
                    {
                        Ext.Net.Container c = new Container();
                        if (group.Width > 0 && group.Width <= 1)
                        {
                            c.AnchorHorizontal = group.Width * 100 + "%";
                        }

                        c.Layout = "FormLayout";
                        c.ColumnWidth = panel.ColumnWidth;
                        c.Items.Add(chk);
                        panel.Items.Add(c);
                    }
                    else
                    {
                        panel.Items.Add(chk);
                    }
                }

                chk.Visible = element.Visible;
            }
            catch (Exception ex)
            {
                Dialog.ShowError("Form/GenerateCheckbox = " + ex.Message);
            }
        }

        private void GenerateAjaxSearch(FormElementInfo element, Ext.Net.Container panel, FormGroupInfo group)
        {
            try
            {
                Control ct = this.Page.LoadControl("../Base/AjaxSearch/AjaxSearch.ascx");
                var ajaxSearch = ct as BaseAjaxSearch;
                ajaxSearch.Disable = element.Disable;
                ajaxSearch.AjaxSearchName = GridPanelName + "_" + this.ID + "_AjaxSearch_" + element.ColumnName;
                ajaxSearch.FieldLabel = element.LabelName;
                ajaxSearch.Width = element.Width;
                ajaxSearch.FieldNote = element.FieldNote;
                ajaxSearch.TabIndex = (short) element.Position;
                if (group != null)
                    switch (group.LabelAlign)
                    {
                        case "Right":
                            ajaxSearch.LabelAlign = LabelAlign.Right;
                            break;
                        case "Top":
                            ajaxSearch.LabelAlign = LabelAlign.Top;
                            break;
                    }
                string[] config = element.ControlConfig.Split(',');
                foreach (string item in config)
                {
                    if (item.StartsWith("Table"))
                    {
                        ajaxSearch.TableName = item.Replace("Table:", "");
                    }
                    else if (item.StartsWith("SearchField"))
                    {
                        ajaxSearch.SearchField = item.Replace("SearchField:", "");
                    }
                    else if (item.StartsWith("ValueField"))
                    {
                        ajaxSearch.ValueField = item.Replace("ValueField:", "");
                    }
                    else if (item.StartsWith("DisplayField"))
                    {
                        ajaxSearch.DisplayField = item.Replace("DisplayField:", "");
                    }
                    else if (item.StartsWith("PageSize"))
                    {
                        ajaxSearch.PageSize = int.Parse(item.Replace("PageSize:", ""));
                    }
                    else if (item.StartsWith("AllowInsert"))
                    {
                        ajaxSearch.AllowInsert = bool.Parse(item.Replace("AllowInsert:", ""));
                    }
                    else if (item.StartsWith("Width"))
                    {
                        ajaxSearch.Width = double.Parse(item.Replace("Width:", ""));
                    }
                }

                ct.ID = element.ColumnName;

                if (panel == null)
                {
                    plcUserControl.Controls.Add(ct);
                }
                else
                {
                    //if (group.Layout == "ColumnLayout")
                    //{
                    Ext.Net.Container c = new Container();
                    if (group.Width > 0 && group.Width <= 1)
                    {
                        c.AnchorHorizontal = group.Width * 100 + "%";
                    }

                    if (panel.ColumnWidth > 0 && panel.ColumnWidth <= 1)
                        c.ColumnWidth = panel.ColumnWidth;
                    c.Layout = "FormLayout";
                    c.ContentControls.Add(ct);
                    panel.Items.Add(c);
                    //}
                    //else
                    //{
                    //    panel.ContentControls.Add(ct);
                    //}
                }

                ct.Visible = element.Visible;
            }
            catch (Exception ex)
            {
                Dialog.ShowError("Form/GenerateAjaxSearch : " + ex.Message);
            }
        }

        private void GenerateHtmlInput(FormElementInfo element, Ext.Net.Container panel, FormGroupInfo group)
        {
            try
            {
                Ext.Net.HtmlEditor htmlEditor = new HtmlEditor()
                {
                    Disabled = element.Disable,
                    EnableViewState = false,
                    TabIndex = (short) element.Position,
                    ID = element.ColumnName,
                    FieldLabel = element.LabelName,
                    Height = element.Height != 0 ? element.Height.Value : 200,
                };
                if (string.IsNullOrEmpty(element.FieldNote) == false)
                    htmlEditor.Note = element.FieldNote;
                if (element.Width > 0 && element.Width <= 1)
                    htmlEditor.AnchorHorizontal = (100 * element.Width) + "%";
                else
                    htmlEditor.AnchorHorizontal = "100%";
                if (!string.IsNullOrEmpty(element.ControlConfig))
                {
                    htmlEditor.Height = int.Parse(element.ControlConfig.Replace("Height:", ""));
                }

                if (panel == null)
                {
                    frmControl.Items.Add(htmlEditor);
                    if (element.Width > 1)
                    {
                        htmlEditor.Width = (int) element.Width;
                    }
                }
                else
                {
                    Ext.Net.Container c = new Container();
                    if (group.Width > 0 && group.Width <= 1)
                    {
                        c.AnchorHorizontal = group.Width * 100 + "%";
                    }

                    c.Layout = "FormLayout";
                    if (group.Layout == "ColumnLayout")
                    {
                        c.ColumnWidth = panel.ColumnWidth;
                    }

                    c.Items.Add(htmlEditor);
                    panel.Items.Add(c);
                }

                htmlEditor.Visible = element.Visible;
            }
            catch (Exception ex)
            {
                Dialog.ShowError("Form/GenerateHtmlInput : " + ex.Message);
            }
        }

        private void GenerateImage(FormElementInfo element, Container panel, FormGroupInfo group)
        {
            try
            {
                Ext.Net.ImageButton img = new Ext.Net.ImageButton()
                {
                    ID = element.ColumnName,
                    Disabled = element.Disable,
                    Width = (int) element.Width,
                    Height = (int) element.Height,
                    ImageUrl = "Images/No_person.jpg",
                };
                if (string.IsNullOrEmpty(element.ControlConfig) == false)
                {
                    hdfImageFolder.Text = element.ControlConfig;
                    hdfColumnName.Text = element.ColumnName;
                    img.ToolTip = "Nhấp chuột vào ảnh để thay ảnh mới";
                    img.Listeners.Click.Handler = "#{wdUploadImageWindow}.show();";
                }

                if (panel == null)
                {
                    frmControl.Items.Add(img);
                }
                else
                {
                    Ext.Net.Container c = new Container()
                    {
                        Layout = "FormLayout",
                        Width = (int) element.Width,
                    };
                    c.Items.Add(img);
                    panel.Items.Add(c);
                }

                img.Visible = element.Visible;
            }
            catch (Exception ex)
            {
                Dialog.ShowError("Form/GenerateHtmlInput : " + ex.Message);
                throw;
            }
        }

        private void GenerateDisplay(FormElementInfo element, Container panel, FormGroupInfo group)
        {
            throw new NotImplementedException();
        }

        private void GenerateTextArea(FormElementInfo element, Container panel, FormGroupInfo group)
        {
            try
            {
                Ext.Net.TextArea txtArea = new TextArea()
                {
                    Disabled = element.Disable,
                    EnableViewState = false,
                    TabIndex = (short) element.Position,
                    ID = element.ColumnName,
                    FieldLabel = element.LabelName,
                    Height = element.Height != 0 ? element.Height.Value : 200,
                };
                if (string.IsNullOrEmpty(element.FieldNote) == false)
                    txtArea.Note = element.FieldNote;
                if (element.Width > 0 && element.Width <= 1)
                    txtArea.AnchorHorizontal = (100 * element.Width) + "%";
                else
                    txtArea.AnchorHorizontal = "100%";
                if (panel == null)
                {
                    frmControl.Items.Add(txtArea);
                    if (element.Width > 1)
                    {
                        txtArea.Width = (int) element.Width;
                    }
                }
                else
                {
                    Ext.Net.Container c = new Container();
                    if (group.Width > 0 && group.Width <= 1)
                    {
                        c.AnchorHorizontal = group.Width * 100 + "%";
                    }

                    c.Layout = "FormLayout";
                    if (group.Layout == "ColumnLayout")
                    {
                        c.ColumnWidth = panel.ColumnWidth;
                    }

                    c.Items.Add(txtArea);
                    panel.Items.Add(c);
                }

                txtArea.Visible = element.Visible;
            }
            catch (Exception ex)
            {
                Dialog.ShowError("Form/GenerateTextArea : " + ex.Message);
            }
        }

        #endregion

        #region lưu thông tin trên form vào CSDL

        protected void btnUpdate_Click(object sender, DirectEventArgs e)
        {
            //Sự kiện update của người dùng tự viết
            if (UpdateButtonClick != null)
            {
                UpdateButtonClick(sender, e);
                if (AfterClickUpdateButton != null)
                {
                    AfterClickUpdateButton(this, null);
                }

                if (FormType == FormKind.Window && e.ExtraParams["CloseCommand"] != "Close")
                {
                    SetValue(null);
                }
                else
                {
                    wdWindow.Hide();
                }

                return;
            }

            if (formInfo == null)
                formInfo = FormController.GetInstance().GetForm(FormName);
            List<FormElementInfo> ElementList;

            if (string.IsNullOrEmpty(FormReference) == false
            ) // nếu có form tham chiếu thì lấy các phần tử của form tham chiếu
            {
                ElementList = FormController.GetInstance().GetForm(FormReference).GetFormElements(1);
            }
            else
            {
                ElementList = formInfo.GetFormElements(1);
            }

            string insert = "insert into " + formInfo.TableName + "({0}) values({1})";
            string column = "";
            string value = "";
            string update = "";
            foreach (var item in ElementList)
            {
                if (item.AutoGenerateValue == "IntIncrement" ||
                    (CommandButton == Command.Update && item.ColumnName == PrimaryColumnName))
                {
                    continue;
                }

                if (CommandButton == Command.Update)
                {
                    if (item.AutoGenerateValue == "CreatedDate" || item.AutoGenerateValue == "CreatedBy")
                    {
                        continue;
                    }
                }

                Control ct = frmControl.FindControl(item.ColumnName);
                if (ct == null)
                {
                    if ((ct = plcUserControl.FindControl(item.ColumnName)) == null)
                    {
                        continue;
                    }
                }

                object obj = GetAutoGenerateValue(item);
                if (obj != null)
                {
                    column += "[" + item.ColumnName + "],";
                    value += "N'" + obj + "',";
                    update += "[" + item.ColumnName + "] = N'" + obj + "',";
                }
                else
                    switch (ct.ToString())
                    {
                        case "Ext.Net.TextField":
                            column += "[" + item.ColumnName + "],"; //cho vào từng case 1 để đảm bảo ko bị lỗi thừa cột
                            value += "N'" + ((TextField) ct).Text + "',";
                            update += "[" + item.ColumnName + "] = N'" + ((TextField) ct).Text + "',";
                            break;
                        case "Ext.Net.Checkbox":
                            column += "[" + item.ColumnName + "],";
                            int v = ((Ext.Net.Checkbox) ct).Checked ? 1 : 0;
                            value += v + ",";
                            update += "[" + item.ColumnName + "] = " + v + ",";
                            break;
                        case "Ext.Net.DateField":
                            column += "[" + item.ColumnName + "],";
                            DateTime selectedDate = ((DateField) ct).SelectedDate;
                            if (selectedDate != null)
                            {
                                string standard = "'" + selectedDate.Month + "/" + selectedDate.Day + "/" +
                                                  selectedDate.Year + "',";
                                value += standard;
                                update += "[" + item.ColumnName + "] = " +
                                          standard; //((Ext.Net.DateField)ct).SelectedDate + "',";
                            }

                            break;
                        case "Ext.Net.NumberField":
                            column += "[" + item.ColumnName + "],";
                            string vNumber = ((NumberField) ct).Text;
                            if (!string.IsNullOrEmpty(vNumber))
                            {
                                value += vNumber + ",";
                                update += "[" + item.ColumnName + "] = " + vNumber + ",";
                            }

                            break;
                        case "Ext.Net.ComboBox":
                            column += "[" + item.ColumnName + "],";
                            string val = ((ComboBox) ct).SelectedItem.Value;
                            if (val.ToLower().Equals("true"))
                                val = "1";
                            else if (val.ToLower().Equals("false"))
                                val = "0";
                            if (!string.IsNullOrEmpty(val))
                            {
                                value += "N'" + val + "',";
                                update += "[" + item.ColumnName + "] = N'" + val + "',";
                            }
                            else
                            {
                                value += "null";
                                update += "[" + item.ColumnName + "] = null,";
                            }

                            break;
                        case "Ext.Net.RadioGroup":
                            column += "[" + item.ColumnName + "],";
                            string va = ((RadioGroup) ct).Items[0].Checked ? "1" : "0";
                            value += va + ",";
                            update += "[" + item.ColumnName + "] = " + va + ",";
                            break;
                        case "Ext.Net.HtmlEditor":
                            column += "[" + item.ColumnName + "],";
                            string html = ((HtmlEditor) ct).Value.ToString();
                            value += "N'" + html + "',";
                            update += "[" + item.ColumnName + "] = N'" + html + "',";
                            break;
                        case "Ext.Net.TextArea":
                            column += "[" + item.ColumnName + "],";
                            string txtArea = ((TextArea) ct).Text;
                            value += "N'" + txtArea + "',";
                            update += "[" + item.ColumnName + "] = N'" + txtArea + "',";
                            break;
                        default:
                            var ctrol = ct as IBaseControl;
                            if (ctrol != null)
                            {
                                column += "[" + item.ColumnName + "],";
                                Object objData = ctrol.GetValue();
                                if (objData != null && string.IsNullOrEmpty(objData.ToString()) == false)
                                {
                                    value += "N'" + ctrol.GetValue() + "',";
                                    update += "[" + item.ColumnName + "] = N'" + ctrol.GetValue() + "',";
                                }
                                else
                                {
                                    value += "null,";
                                    update += "[" + item.ColumnName + "] = null,";
                                }
                            }

                            break;
                    }
            }

            string query = "";
            //NhatkyTruycapInfo accessDiary = null;
            //if (accessHistory != null)
            //    accessDiary = new NhatkyTruycapInfo()
            //     {
            //         CHUCNANG = accessHistory.ModuleDescription,
            //         IsError = false,
            //         USERNAME = CurrentUser.User.UserName,
            //         THOIGIAN = DateTime.Now,
            //         MANGHIEPVU = formInfo.TableName,
            //         TENMAY = Util.GetInstance().GetComputerName(Request.UserHostAddress),
            //         IPMAY = Request.UserHostAddress,
            //         THAMCHIEU = column.Replace("[", "").Replace("]", " "),
            //     };
            //var accessDiary = new Web.Core.Object.Security.AccessHistory();
            //accessDiary.Function = "";
            //accessDiary.Description = "";
            //accessDiary.IsError = false;
            //accessDiary.UserName = CurrentUser.User.UserName;
            //accessDiary.Time = DateTime.Now;
            //accessDiary.BusinessCode = formInfo.TableName;
            //accessDiary.ComputerName = Util.GetInstance().GetComputerName(Request.UserHostAddress);
            //accessDiary.ComputerIP = Request.UserHostAddress;
            //accessDiary.Referent = column.Replace("[", "").Replace("]", " ");

            try
            {
                if (CommandButton == Command.Insert)
                {
                    try
                    {
                        column = column.Remove(column.LastIndexOf(','));
                        value = value.Remove(value.LastIndexOf(','));
                        query = string.Format(insert, column, value);
                        SCOPE_IDENTITY = DataHandler.GetInstance().ExecuteScalar("execInsert", "@query", query);
                        //Thêm nhật ký truy cập
                        //if (this.accessHistory != null)
                        //{
                        //    accessDiary.MOTA = accessHistory.Insert_AccessHistoryDescription;
                        //    new AccessHistoryController().AddAccessHistory(accessDiary);
                        //}
                        //AccessHistoryServices.Create(accessDiary);
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("Cannot insert duplicate key"))
                        {
                            Dialog.ShowError("Khóa chính bị trùng, vui lòng nhập lại !");
                        }
                        else
                            Dialog.ShowError(ex.Message);

                        return;
                    }
                }
                else if (CommandButton == Command.Update)
                {
                    if (string.IsNullOrEmpty(PrimaryColumnValue) && Session["hdfPrimaryKeyValue"] != null)
                    {
                        PrimaryColumnValue = Session["hdfPrimaryKeyValue"].ToString();
                    }

                    update = update.Remove(update.LastIndexOf(','));
                    query = "update " + formInfo.TableName + " set " + update + " where " + PrimaryColumnName +
                            " = N'" + PrimaryColumnValue + "'";
                    DataHandler.GetInstance().ExecuteNonQuery(query);
                    //Thêm nhật ký truy cập
                    //if (this.accessHistory != null)
                    //{
                    //    accessDiary.MOTA = accessHistory.Update_AccessHistoryDescription + ", khóa chính bản ghi: " + PrimaryColumnValue;
                    //    new AccessHistoryController().AddAccessHistory(accessDiary);
                    //}
                    //AccessHistoryServices.Create(accessDiary);
                }

                // Dialog.ShowNotification(query);
            }
            catch (Exception ex)
            {
                Dialog.ShowError("Có lỗi xảy ra trong quá trình cập nhật dữ liệu !" + ex.Message);
                //if (accessDiary != null)
                //{
                //    accessDiary.MOTA = "<span>" + ex.Message.Replace("'", " ") + "</span>";
                //    accessDiary.IsError = true;
                //    new AccessHistoryController().AddAccessHistory(accessDiary);
                //}
                //AccessHistoryServices.Create(accessDiary);
            }

            //  Dialog.ShowNotification(query);
            if (AfterClickUpdateButton != null)
            {
                AfterClickUpdateButton(this, null);
            }

            if (FormType == FormKind.Window && e.ExtraParams["CloseCommand"] != "Close")
            {
                SetValue(null);
            }
            else
            {
                wdWindow.Hide();
            }
        }

        [DirectMethod]
        public void btnUpdate_Click_DirectMethod()
        {
            var param = new Parameter("CloseCommand", "Close", ParameterMode.Raw);
            ParameterCollection paramCollection = new Ext.Net.ParameterCollection();
            paramCollection.Add(param);
            btnUpdate_Click(null, new DirectEventArgs(paramCollection));
        }

        private object GetAutoGenerateValue(FormElementInfo element)
        {
            switch (element.AutoGenerateValue)
            {
                case "UseTrigger":
                    return new Random().NextDouble().ToString();
                case "CreatedDate":
                case "EdittedDate":
                    return Util.GetInstance().GetDateTimeEnfromVi(DateTime.Now.ToString());
                case "CreatedBy":
                case "EdittedBy":
                    return CurrentUser.User.Id;
                case "MaDonVi":
                    var dv = CurrentUser.Departments.Select(d => d.Id).FirstOrDefault();
                    return dv;
            }

            return null;
        }

        #endregion

        /// <summary>
        /// Add các user control do developer phát triển, thưởng được sử dụng để tạo ra các control nhập liệu phức tạp
        /// </summary>
        /// <param name="ControlUrl">Đường dẫn tới usercontrol</param>
        public void AddUserControl(string ControlUrl)
        {
            Control ct = this.Page.LoadControl(ControlUrl);
            plcUserControl.Controls.Add(ct);
        }

        #region các phương thức của IBaseControl

        public string GetID()
        {
            return ID;
        }

        public object GetValue()
        {
            throw new NotImplementedException();
            //return GetValue();
            //throw new NotSupportedException();
        }

        public void SetValue(object value) //truyền khóa chính vào
        {
            if (CommandButton == Command.Update || CommandButton == Command.Display)
            {
                if (value == null)
                {
                    //   Dialog.ShowError("Chưa có giá trị cho khóa chính");
                    return;
                }

                PrimaryColumnValue = value.ToString();
                Session["hdfPrimaryKeyValue"] = value.ToString();
            }

            List<FormElementInfo> ElementList = null;

            if (string.IsNullOrEmpty(FormReference) == false)
            {
                ElementList = FormController.GetInstance().GetForm(FormReference).GetFormElements(1);
            }
            else
            {
                if (formInfo == null)
                    formInfo = FormController.GetInstance().GetForm(FormName);
                ElementList = formInfo.GetFormElements(1);
            }

            //sinh câu truy vấn để lấy dữ liệu
            string sql = hdfQuery.Text;
            DataTable vTable = null;
            if (value != null)
            {
                if (string.IsNullOrEmpty(sql))
                {
                    if (formInfo == null)
                        formInfo = FormController.GetInstance().GetForm(FormName);
                    sql = "select top 1 {0} from " + formInfo.TableName + " where " +
                          PrimaryColumnName; // +" = N'{1}'";
                    string col = "";
                    foreach (var item in ElementList)
                    {
                        col += "[" + item.ColumnName + "],";
                    }

                    sql = string.Format(sql, col.Remove(col.LastIndexOf(',')));
                    hdfQuery.Text = sql;
                }

                vTable = DataHandler.GetInstance().ExecuteDataTable(sql + " = N'" + PrimaryColumnValue + "'");
            }

            foreach (var item in ElementList)
            {
                Control ct = frmControl.FindControl(item.ColumnName);
                if (ct == null)
                {
                    if ((ct = plcUserControl.FindControl(item.ColumnName)) == null)
                    {
                        continue;
                    }
                }

                object objValue = null;
                if (value != null)
                    objValue = vTable.Rows[0][item.ColumnName];
                switch (ct.ToString())
                {
                    case "Ext.Net.TextField":
                        try
                        {
                            Ext.Net.TextField text = ct as Ext.Net.TextField;
                            if (objValue != null)
                                text.Text = objValue.ToString();
                            else
                                text.Text = "";
                            if (item.ColumnName == PrimaryColumnName)
                            {
                                if (CommandButton == Command.Update)
                                    text.Disabled = true;
                                else
                                    text.Disabled = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            Dialog.ShowError("Form/SetValue/Ext.Net.TextField = " + ex.Message);
                        }

                        break;
                    case "Ext.Net.Checkbox":
                        try
                        {
                            Ext.Net.Checkbox chk = ct as Ext.Net.Checkbox;
                            if (objValue != null && !string.IsNullOrEmpty(objValue.ToString()))
                                chk.Checked = bool.Parse(objValue.ToString());
                            else
                                chk.Checked = false;
                        }
                        catch (Exception ex)
                        {
                            Dialog.ShowError("Form/SetValue/Ext.Net.Checkbox = " + ex.Message);
                        }

                        break;
                    case "Ext.Net.DateField":
                        try
                        {
                            Ext.Net.DateField date = ct as Ext.Net.DateField;
                            if (objValue != null)
                            {
                                string d = objValue.ToString();
                                if (!string.IsNullOrEmpty(d))
                                    date.SelectedDate = DateTime.Parse(d);
                            }
                            else
                                date.SelectedDate = DateTime.Now;
                        }
                        catch (Exception ex)
                        {
                            Dialog.ShowError("Form/SetValue/Ext.Net.DateField = " + ex.Message);
                        }

                        break;
                    case "Ext.Net.NumberField":
                        try
                        {
                            Ext.Net.NumberField number = ct as Ext.Net.NumberField;
                            if (objValue != null && string.IsNullOrEmpty(objValue.ToString()) == false)
                            {
                                number.Text = objValue.ToString();
                            }
                            else
                                number.Text = "0";

                            if (item.ColumnName == PrimaryColumnName)
                            {
                                if (CommandButton == Command.Update)
                                    number.Disabled = true;
                                else
                                    number.Disabled = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            Dialog.ShowError("Form/SetValue/Ext.Net.NumberField = " + ex.Message);
                        }

                        break;
                    case "Ext.Net.RadioGroup":
                        try
                        {
                            Ext.Net.RadioGroup group = ct as RadioGroup;
                            if (objValue != null)
                            {
                                string v = objValue.ToString();
                                if (v.Equals("False"))
                                    group.Items[1].Checked = true;
                                else
                                    group.Items[0].Checked = true;
                            }
                        }
                        catch (Exception ex)
                        {
                            Dialog.ShowError("Form/SetValue/Ext.Net.RadioGroup = " + ex.Message);
                        }

                        break;
                    case "Ext.Net.HtmlEditor":
                        try
                        {
                            Ext.Net.HtmlEditor html = ct as Ext.Net.HtmlEditor;
                            if (objValue != null)
                            {
                                html.SetValue(objValue.ToString());
                            }
                            else
                            {
                                html.SetValue("");
                            }
                        }
                        catch (Exception ex)
                        {
                            Dialog.ShowError("Form/SetValue/Ext.Net.HtmlEditor = " + ex.Message);
                        }

                        break;
                    case "Dth.Net.ComboBox":
                    case "Dth.Net.AjaxSearch":
                        IBaseControl cbControl = ct as IBaseControl;
                        cbControl.SetValue(objValue);
                        break;
                    case "Ext.Net.ImageButton":
                        try
                        {
                            Ext.Net.ImageButton img = ct as Ext.Net.ImageButton;
                            if (objValue != null)
                            {
                                if (string.IsNullOrEmpty(objValue.ToString()))
                                    img.ImageUrl = "Images/No_person.jpg";
                                else
                                    img.ImageUrl = objValue.ToString();
                            }
                            else //temp 
                                img.ImageUrl = "Images/No_person.jpg";
                        }
                        catch (Exception ex)
                        {
                            Dialog.ShowError("Form/SetValue/Ext.Net.ImageButton = " + ex.Message);
                        }

                        break;
                    case "Ext.Net.TextArea":
                        try
                        {
                            Ext.Net.TextArea txtArea = ct as Ext.Net.TextArea;
                            if (objValue != null)
                                txtArea.Text = objValue.ToString();
                            else
                                txtArea.Text = "";
                        }
                        catch (Exception ex)
                        {
                            Dialog.ShowError("Form/SetValue/Ext.Net.TextArea = " + ex.Message);
                        }

                        break;
                    case "Ext.Net.Slider":
                    case "Ext.Net.SliderField":
                    case "Ext.Net.MultiSelect":
                    case "Ext.Net.SelectBox":
                    default:
                        //   Dialog.ShowNotification(ct.ToString());
                        break;
                }
            }
        }

        #endregion

        public void Show()
        {
            if (FormType != FormKind.Window)
            {
                return;
            }

            if (CommandButton == Command.Update)
            {
                SetValue(PrimaryColumnValue);
            }
            else if (CommandButton == Command.Insert)
            {
                SetValue(null);
            }

            wdWindow.Show();
        }

        public void Hide()
        {
            wdWindow.Hide();
        }

        /// <summary>
        /// Lấy control trên form dựa vào ID
        /// </summary>
        /// <param name="controlID"></param>
        /// <returns></returns>
        public override Control GetComponent(string controlID)
        {
            Control ct = frmControl.FindControl(controlID);
            if (ct == null)
            {
                ct = plcUserControl.FindControl(controlID);
            }

            return ct;
        }

        public string GetWindowClientID()
        {
            return wdWindow.ClientID;
        }

        protected void btnAccept_Click(object sender, DirectEventArgs e)
        {
            try
            {

                //upload file
                HttpPostedFile file = fufUploadControl.PostedFile;
                //if (string.IsNullOrEmpty(fufUploadControl.Text))
                //{
                //    Dialog.ShowError(GlobalManager.GetInstance().GetErrorMessageValue("NotSelectFile"));
                //    return;
                //}
                //if (file.ContentType != "application/vnd.ms-excel") //ko phải Excel 2003
                //{
                //    Dialog.ShowError(GlobalManager.GetInstance().GetErrorMessageValue("NotExcel2003"));
                //    return;
                //}
                if (fufUploadControl.HasFile == false && file.ContentLength > 2000000)
                {
                    Dialog.ShowNotification("File không được lớn hơn 200kb");
                    return;
                }
                else
                {
                    string path = string.Empty;
                    try
                    {
                        DirectoryInfo dir = new DirectoryInfo(Server.MapPath(hdfImageFolder.Text));
                        if (dir.Exists == false)
                            dir.Create();
                        path = Server.MapPath(hdfImageFolder.Text + "/") + fufUploadControl.FileName;
                        file.SaveAs(path);
                        //update ảnh vào csdl
                        if (formInfo == null)
                            formInfo = FormController.GetInstance().GetForm(FormName);
                        string sql = "update " + formInfo.TableName + " set " + hdfColumnName.Text + " = N'" +
                                     hdfImageFolder.Text + "/" + fufUploadControl.FileName + "' where " +
                                     PrimaryColumnName + " = N'" + Session["hdfPrimaryKeyValue"] + "'";
                        DataHandler.GetInstance().ExecuteNonQuery(sql);
                    }
                    catch (Exception ex)
                    {
                        Dialog.ShowError(ex.Message);
                    }
                }

                wdUploadImageWindow.Hide();

                //Hiển thị lại ảnh sau khi đã cập nhật xong
                Control ct = frmControl.FindControl(hdfColumnName.Text);
                if (ct != null)
                {
                    try
                    {
                        Ext.Net.ImageButton img = ct as Ext.Net.ImageButton;
                        img.ImageUrl = hdfImageFolder.Text + "/" + fufUploadControl.FileName;
                    }
                    catch
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowNotification(ex.Message);
            }
        }

        #region các phương thức để phát triển form OneMany, ko liên quan gì đến Form nên xóa đi cũng ko ảnh hưởng ở Form

        public void AddDetailTable(Component component, string frmName)
        {
            try
            {
                wdWindow.Items.Clear();
                Ext.Net.BorderLayout brLayout = new BorderLayout();
                wdWindow.Items.Add(brLayout);
                Ext.Net.Panel pnl = new Ext.Net.Panel()
                {
                    Border = false,
                    Collapsible = false,
                    ID = "centerPanel",
                    AutoScroll = true,
                };
                pnl.Items.Add(frmControl);
                brLayout.Center.Items.Add(pnl);
                brLayout.South.Collapsible = true;
                // brLayout.South.Split = true;
                Ext.Net.Panel pnlSouth = new Ext.Net.Panel()
                {
                    Border = true,
                    Collapsible = true,
                    ID = "pnlSouthPanel",
                    Height = 200,
                    Collapsed = true,
                    Title = "Thông tin thêm",
                    CtCls = "south-panel"
                };
                pnlSouth.Items.Add(component);
                brLayout.South.Items.Add(pnlSouth);
            }
            catch (Exception ex)
            {
                //var accessDiary = new Web.Core.Object.Security.AccessHistory();
                //accessDiary.Function = "Thêm bảng chi tiết vào form OneMany";
                //accessDiary.Description = "Form/AddDetailTable = " + ex.Message.Replace("'", "");
                //accessDiary.IsError = true;
                //accessDiary.UserName = CurrentUser.User.UserName;
                //accessDiary.Time = DateTime.Now;
                //accessDiary.BusinessCode = "";
                //accessDiary.ComputerName = Util.GetInstance().GetComputerName(Request.UserHostAddress);
                //accessDiary.ComputerIP = Request.UserHostAddress;
                //accessDiary.Referent = "";
                //AccessHistoryServices.Create(accessDiary);
                //NhatkyTruycapInfo accessDiary = new NhatkyTruycapInfo()
                //{
                //    CHUCNANG = "Thêm bảng chi tiết vào form OneMany",
                //    IsError = true,
                //    USERNAME = CurrentUser.User.UserName,
                //    THOIGIAN = DateTime.Now,
                //    MANGHIEPVU = "",
                //    TENMAY = Util.GetInstance().GetComputerName(Request.UserHostAddress),
                //    IPMAY = Request.UserHostAddress,
                //    THAMCHIEU = "",
                //    MOTA = "Form/AddDetailTable = " + ex.Message.Replace("'", ""),
                //};
                //new SoftCore.AccessHistory.AccessHistoryController().AddAccessHistory(accessDiary);

                Dialog.ShowError(DefaultErrorMessage);
            }
        }

        public void ReloadTableDetail(string ForeignKeyValue)
        {
            try
            {
                TabPanel tab = null;
                Ext.Net.Panel tmpPanel = wdWindow.FindControl("pnlSouthPanel") as Ext.Net.Panel;
                foreach (var item in tmpPanel.Items)
                {
                    if (item.ToString() == "Ext.Net.TabPanel")
                    {
                        tab = item as TabPanel;
                        break;
                    }
                }

                foreach (Ext.Net.Panel panel in tab.Items)
                {
                    try
                    {
                        var gridTable = ((Container) panel.Items[0]).ContentControls[0] as BaseGridTable;
                        ((Modules_Base_MiniGridPanel_MiniGrid) gridTable).LoadData(
                            gridTable.OutSideQuery + " = " + ForeignKeyValue, gridTable.OutSideQuery, ForeignKeyValue);
                    }
                    catch (Exception ex)
                    {
                        //var accessDiary = new Web.Core.Object.Security.AccessHistory();
                        //accessDiary.Function = "Form/ReloadTableDetail";
                        //accessDiary.Description = "Inside Form/ReloadTableDetail = " + ex.Message.Replace("'", "");
                        //accessDiary.IsError = true;
                        //accessDiary.UserName = CurrentUser.User.UserName;
                        //accessDiary.Time = DateTime.Now;
                        //accessDiary.BusinessCode = "";
                        //accessDiary.ComputerName = Util.GetInstance().GetComputerName(Request.UserHostAddress);
                        //accessDiary.ComputerIP = Request.UserHostAddress;
                        //accessDiary.Referent = "";
                        //AccessHistoryServices.Create(accessDiary);
                        //NhatkyTruycapInfo accessDiary = new NhatkyTruycapInfo()
                        //{
                        //    CHUCNANG = "Form/ReloadTableDetail",
                        //    IsError = true,
                        //    USERNAME = CurrentUser.User.UserName,
                        //    THOIGIAN = DateTime.Now,
                        //    MANGHIEPVU = "",
                        //    TENMAY = Util.GetInstance().GetComputerName(Request.UserHostAddress),
                        //    IPMAY = Request.UserHostAddress,
                        //    THAMCHIEU = "",
                        //    MOTA = "Inside Form/ReloadTableDetail = " + ex.Message.Replace("'", ""),
                        //};
                        //new SoftCore.AccessHistory.AccessHistoryController().AddAccessHistory(accessDiary);

                        Dialog.ShowError(
                            DefaultErrorMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                //NhatkyTruycapInfo accessDiary = new NhatkyTruycapInfo()
                //{
                //    CHUCNANG = "Form/ReloadTableDetail",
                //    IsError = true,
                //    USERNAME = CurrentUser.User.UserName,
                //    THOIGIAN = DateTime.Now,
                //    MANGHIEPVU = "",
                //    TENMAY = Util.GetInstance().GetComputerName(Request.UserHostAddress),
                //    IPMAY = Request.UserHostAddress,
                //    THAMCHIEU = "",
                //    MOTA = "Form/ReloadTableDetail = " + ex.Message.Replace("'", ""),
                //};
                //var accessDiary = new Web.Core.Object.Security.AccessHistory();
                //accessDiary.Function = "Form/ReloadTableDetail";
                //accessDiary.Description = "Form/ReloadTableDetail = " + ex.Message.Replace("'", "");
                //accessDiary.IsError = true;
                //accessDiary.UserName = CurrentUser.User.UserName;
                //accessDiary.Time = DateTime.Now;
                //accessDiary.BusinessCode = "";
                //accessDiary.ComputerName = Util.GetInstance().GetComputerName(Request.UserHostAddress);
                //accessDiary.ComputerIP = Request.UserHostAddress;
                //accessDiary.Referent = "";
                //AccessHistoryServices.Create(accessDiary);
                Dialog.ShowError(DefaultErrorMessage);
            }
        }

        public List<MiniGridData> GetMiniGridData()
        {
            try
            {
                List<MiniGridData> miniGridDataList = new List<MiniGridData>();
                TabPanel tab = null;
                Ext.Net.Panel tmpPanel = wdWindow.FindControl("pnlSouthPanel") as Ext.Net.Panel;
                foreach (var item in tmpPanel.Items)
                {
                    if (item.ToString() == "Ext.Net.TabPanel")
                    {
                        tab = item as TabPanel;
                        break;
                    }
                }

                foreach (Ext.Net.Panel panel in tab.Items)
                {
                    try
                    {
                        var gridTable = ((Container) panel.Items[0]).ContentControls[0] as BaseGridTable;
                        miniGridDataList.Add(new MiniGridData()
                        {
                            MiniGridName = gridTable.ID,
                            JsonData = ((Modules_Base_MiniGridPanel_MiniGrid) gridTable).GetJsonData()
                        });
                    }
                    catch (Exception ex)
                    {
                        //var accessDiary = new Web.Core.Object.Security.AccessHistory();
                        //accessDiary.Function = "Form/GetMiniGridData";
                        //accessDiary.Description = "Form/GetMiniGridData = " + ex.Message.Replace("'", "");
                        //accessDiary.IsError = true;
                        //accessDiary.UserName = CurrentUser.User.UserName;
                        //accessDiary.Time = DateTime.Now;
                        //accessDiary.BusinessCode = "";
                        //accessDiary.ComputerName = Util.GetInstance().GetComputerName(Request.UserHostAddress);
                        //accessDiary.ComputerIP = Request.UserHostAddress;
                        //accessDiary.Referent = "";
                        //AccessHistoryServices.Create(accessDiary);
                        //NhatkyTruycapInfo accessDiary = new NhatkyTruycapInfo()
                        //{
                        //    CHUCNANG = "Form/GetMiniGridData",
                        //    IsError = true,
                        //    USERNAME = CurrentUser.User.UserName,
                        //    THOIGIAN = DateTime.Now,
                        //    MANGHIEPVU = "",
                        //    TENMAY = Util.GetInstance().GetComputerName(Request.UserHostAddress),
                        //    IPMAY = Request.UserHostAddress,
                        //    THAMCHIEU = "",
                        //    MOTA = "Form/GetMiniGridData = " + ex.Message.Replace("'", ""),
                        //};
                        //new SoftCore.AccessHistory.AccessHistoryController().AddAccessHistory(accessDiary);

                        Dialog.ShowError(
                            DefaultErrorMessage);
                    }
                }

                return miniGridDataList;
            }
            catch (Exception ex)
            {
                //var accessDiary = new Web.Core.Object.Security.AccessHistory();
                //accessDiary.Function = "Form/GetMiniGridData";
                //accessDiary.Description = "Form/GetMiniGridData = " + ex.Message.Replace("'", "");
                //accessDiary.IsError = true;
                //accessDiary.UserName = CurrentUser.User.UserName;
                //accessDiary.Time = DateTime.Now;
                //accessDiary.BusinessCode = "";
                //accessDiary.ComputerName = Util.GetInstance().GetComputerName(Request.UserHostAddress);
                //accessDiary.ComputerIP = Request.UserHostAddress;
                //accessDiary.Referent = "";
                //AccessHistoryServices.Create(accessDiary);
                //NhatkyTruycapInfo accessDiary = new NhatkyTruycapInfo()
                //{
                //    CHUCNANG = "Form/GetMiniGridData",
                //    IsError = true,
                //    USERNAME = CurrentUser.User.UserName,
                //    THOIGIAN = DateTime.Now,
                //    MANGHIEPVU = "",
                //    TENMAY = Util.GetInstance().GetComputerName(Request.UserHostAddress),
                //    IPMAY = Request.UserHostAddress,
                //    THAMCHIEU = "",
                //    MOTA = "Form/GetMiniGridData = " + ex.Message.Replace("'", ""),
                //};
                Dialog.ShowError(DefaultErrorMessage);
            }

            return null;
        }


        public void AddButton(Ext.Net.Button component)
        {
            // ToolBarControl.Items.Insert(0, component);
            //  wdWindow.Buttons.Add(component);
            wdWindow.Buttons.Insert(0, component);
        }

        public void CallInsertFunction()
        {
            CommandButton = Command.Insert;
            Ext.Net.Parameter param = new Ext.Net.Parameter("CloseCommand", "", ParameterMode.Raw);
            Ext.Net.ParameterCollection paramCollection = new Ext.Net.ParameterCollection();
            paramCollection.Add(param);
            btnUpdate_Click(null, new DirectEventArgs(paramCollection));
        }

        public void CallUpdateFunction(string PrimaryKeyName, string PrimarykeyValue)
        {
            this.PrimaryColumnName = PrimaryKeyName;
            this.PrimaryColumnValue = PrimarykeyValue;
            CommandButton = Command.Update;
            Ext.Net.Parameter param = new Ext.Net.Parameter("CloseCommand", "", ParameterMode.Raw);
            Ext.Net.ParameterCollection paramCollection = new Ext.Net.ParameterCollection();
            paramCollection.Add(param);
            btnUpdate_Click(null, new DirectEventArgs(paramCollection));
            CommandButton = Command.Display;
        }

        public void AddToConfigMenu(Ext.Net.MenuItem menuItem)
        {
            Menu3.Items.Add(menuItem);
        }

        #endregion
    }
}