using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Ext.Net;
using WebUI.Controller;
using WebUI.Entity;
using System.Data;
using SoftCore;
using DataController;
//using SoftCore.AccessHistory;
using Web.Core;
using Web.Core.Framework;
using Button = Ext.Net.Button;
using MenuItem = Ext.Net.MenuItem;
using Panel = Ext.Net.Panel;
using Parameter = Ext.Net.Parameter;
using Web.Core.Service.Security;

namespace Web.HJM.Modules.UserControl
{
    public partial class Modules_Base_GridPanel : BaseGridTable
    {
        private const string EditOnGrid = "Chỉnh sửa thông tin trực tiếp trên Grid";

        public event EventHandler RowSelect;

        //public event EventHandler RowDoubleClick;
        public event EventHandler AfterEditOnGrid;

        /// <summary>
        /// 
        /// </summary>
        public string RowClickListener { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RowSelection { get; set; }

        /// <summary>
        /// Được sử dụng để biết được ngày tạo khi thực hiện nhân đôi bản ghi
        /// </summary>
        public string DateCreatedField { get; set; }

        /// <summary>
        /// Được sử dụng để biết được người tạo khi thực hiện nhân đôi bản ghi
        /// </summary>
        public string UserCreatedField { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

            // get grid panel
            GridPanel = GridController.GetInstance().GetGridPanel(ID);
            // check result not null
            if (GridPanel != null)
            {
                // grid avaiable
                // check table name
                if (string.IsNullOrEmpty(TableName))
                {
                    // table name null or empty, set table name
                    TableName = !string.IsNullOrEmpty(GridPanel.ViewName) ? GridPanel.ViewName : GridPanel.TableName;
                }
                else
                {
                    // set table name as default table
                    TableName = DefaultTable;
                }

                // check row select action
                if (RowSelect != null)
                {
                    // add row select event handler
                    rowSelectionModel.DirectEvents.RowSelect.Event += RowSelect_Event;
                }

                // check is ajax request
                if (!ExtNet.IsAjaxRequest)
                {
                    // post request, init toolbar
                    InitToolbar();
                }

                //Nếu ko có ComboBox trên Grid thì cho sự kiện chuột trên GridPanel
                //chỗ này tối ưu lại
                if (GridController.GetInstance().GetColumnInfo(ID, TableName, 1).Count(p =>
                        p.AllowComboBoxOnGrid && !string.IsNullOrEmpty(p.ComboBoxTable)) == 0 &&
                    GridPanel.AllowEditOnGrid == false)
                {
                    if (!string.IsNullOrEmpty(GridPanel.InformationPanel))
                    {
                        rowSelectionModel.DirectEvents.RowSelect.Event += RowSelect_Event;
                        rowSelectionModel.DirectEvents.RowSelect.EventMask.Msg = @"Đang tải dữ liệu";
                        rowSelectionModel.DirectEvents.RowSelect.EventMask.ShowMask = true;
                    }
                    else
                    {
                        if (DisableEditWindow == false && btnEdit.Visible)
                        {
                            extGridPanel.DirectEvents.RowDblClick.Event += btnEdit_Click;
                            extGridPanel.DirectEvents.RowDblClick.EventMask.Msg = @"Chờ trong giây lát";
                            extGridPanel.DirectEvents.RowDblClick.EventMask.ShowMask = true;
                        }
                    }
                }
                else
                {
                    //Nếu có Panel phụ thì sẽ có sự kiện click chuột để xem thông tin chi tiết(ko phải double click)
                    if (!String.IsNullOrEmpty(GridPanel.InformationPanel))
                    {
                        rowSelectionModel.DirectEvents.RowSelect.Event += RowSelect_Event;
                        rowSelectionModel.DirectEvents.RowSelect.EventMask.Msg = @"Đang tải dữ liệu";
                        rowSelectionModel.DirectEvents.RowSelect.EventMask.ShowMask = true;
                    }
                }
            }
            else
            {
                // grid panel null, set default table value for tablename
                TableName = DefaultTable;
            }

            // check ext ajax request
            if (!X.IsAjaxRequest)
            {
                // bind data
                InitData();
                // check row listener
                if (!string.IsNullOrEmpty(RowClickListener))
                    // add event handler
                    extGridPanel.Listeners.RowClick.Handler = RowClickListener;
                // check row selection
                if (!string.IsNullOrEmpty(RowSelection))
                    // add event handler
                    rowSelectionModel.Listeners.RowSelect.Handler += RowSelection;
            }

            //Nếu developr thì để addColumn ở ngoài, còn người bình thường sẽ cho vào X.IsAjax...
            // if (CurrentUser.User.IsSuperUser)
            // {
            AddColumn();
            // }

            if (CurrentUser.User.IsSuperUser)
            {
                if (plcConfig.Controls.Count == 0)
                {
                    var ct =
                        Page.LoadControl("~/Modules/Base/GridPanel/Config/GridConfig.ascx") as
                            Modules_Base_GridPanel_Config_GridConfig;
                    ct.ID = ID + "ConfigControl";
                    ct.TableName = TableName;
                    ct.GridPanelName = ID;
                    ct.ViewName = GridPanel.ViewName;
                    ct.AfterClickUpdateInformation += ct_AfterClickUpdateInformation;
                    ct.AfterCreateNewColumn += ct_AfterCreateNewColumn;
                    plcConfig.Controls.Add(ct);
                }

                btnConfig.Hidden = false;

                extGridPanel.Listeners.ColumnResize.Handler =
                    "#{DirectMethods}.SaveColumnWidth(ChangeColumnWidth(#{extGridPanel}));";
                extGridPanel.Listeners.ColumnMove.Handler =
                    "#{DirectMethods}.SaveColumnOrder(ChangeColumnOrder(#{extGridPanel}));";
            }

            //if (!IsPostBack)
            //{
            // init detail from
            InitDetailForm();
            // init add from
            InitAddEditForm(plcWindowAddForm, BaseForm.Command.Insert, "ucWindowAddNew");
            // init edit from
            InitAddEditForm(plcWindowEditForm, BaseForm.Command.Update, "ucWindowUpdate");
            //}
        }

        #region InitControl

        /// <summary>
        /// Init viewport toolbar
        /// </summary>
        private void InitToolbar()
        {
            if (CurrentUser.User.IsSuperUser)
            {
                return;
            }

            foreach (var item in Toolbar1.Items)
            {
                switch (item.InstanceOf)
                {
                    case "Ext.Button":
                        var btn = (Button) item;
                        if (btn.ID != "btnSearch")
                        {
                            //btn.Visible = SetVisible(btn.Text);
                            btn.Visible = true;
                        }

                        break;
                }
            }

            foreach (var item in RowContextMenu.Items)
            {
                switch (item.ToString())
                {
                    case "Ext.Net.MenuItem":
                        var mnuItem = (MenuItem) item;
                        //mnuItem.Visible = SetVisible(mnuItem.Text);
                        mnuItem.Visible = true;
                        break;
                }
            }

            //btnButtonTienIch.Visible = SetVisible(menuCopyData.Text);
            btnButtonTienIch.Visible = true;
        }

        /// <summary>
        /// Init detail form
        /// </summary>
        private void InitDetailForm()
        {
            if (GridPanel != null)
            {
                if (!string.IsNullOrEmpty(GridPanel.InformationPanel))
                {
                    var formDetail = Page.LoadControl("~/Modules/Base/Form/Form.ascx");
                    formDetail.ID = "formDetail";
                    var frmDetail = (BaseForm) formDetail;
                    frmDetail.FormType = BaseForm.FormKind.Form;
                    frmDetail.GridPanelName = ID;
                    ((Modules_Base_Form_Form) formDetail).AfterClickUpdateConfig += AfterUpdateConfig;
                    ((Modules_Base_Form_Form) formDetail).AfterClickUpdateButton += AfterUpdateRecord;
                    switch (GridPanel.InformationPanel)
                    {
                        case "Top":
                            plNorth.Controls.Add(formDetail);
                            break;
                        case "Right":
                            plEast.Controls.Add(formDetail);
                            break;
                        case "Bottom":
                            plcSouth.Controls.Add(formDetail);
                            break;
                        case "Left":
                            plWest.Controls.Add(formDetail);
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Init Add/Edit window
        /// </summary>
        /// <param name="plcWindow"></param>
        /// <param name="command"></param>
        /// <param name="surfixName"></param>
        private void InitAddEditForm(Control plcWindow, BaseForm.Command command, string surfixName)
        {
            // check disable edit window
            if (DisableEditWindow)
                // disable edit, return
                return;
            // check gird panel
            if (GridPanel == null)
            {
                // get from database
                GridPanel = GridController.GetInstance().GetGridPanel(ID);
                // grid panel still null
                if (GridPanel == null)
                    // grid panel was not configured, return
                    return;
            }

            // check grid panel use one => many form
            if (!GridPanel.OneManyForm)
            {
                // init form
                var form = (Modules_Base_Form_Form) Page.LoadControl("~/Modules/Base/Form/Form.ascx");
                // init form information
                //form.accessHistory = accessHistory;
                form.FormType = BaseForm.FormKind.Window;
                form.WindowHidden = true;
                form.GridPanelName = ID;
                // init variable info
                form.FormName = ID + surfixName;
                form.CommandButton = command;
                // event handler
                form.AfterClickUpdateButton += AfterUpdateRecord;
                // add to place holder
                plcWindow.Controls.Add(form);
            }
            else
            {
                // init form
                var oneManyForm =
                    (Modules_Base_OneManyForm_OneManyForm) Page.LoadControl(
                        "~/Modules/Base/OneManyForm/OneManyForm.ascx");
                // init form information
                //oneManyForm.accessHistory = accessHistory;
                oneManyForm.FormType = BaseForm.FormKind.Window;
                oneManyForm.XmlConfigUrl = XmlConfigUrl;
                oneManyForm.WindowHidden = true;
                oneManyForm.GridPanelName = ID;
                oneManyForm.FormName = ID + "OneManyForm";
                // event handler
                oneManyForm.AfterClickUpdateButton += AfterUpdateRecord;
                // add to placeholder
                plcWindowOneManyForm.Controls.Add(oneManyForm);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitData()
        {
            if (GridPanel == null)
            {
                GridPanel = new GridPanelInfo(1, ID, "", "", 0, 0, "", 30, DefaultTable, true, false, false, false, "",
                    "", "", false, "", false);
                GridController.GetInstance().AddGridPanel(GridPanel);
            }

            if (ColumnList == null)
                ColumnList = GridController.GetInstance().GetColumnInfo(ID, TableName, 1);

            InitGrid(GridPanel, ColumnList);
            LoadField();
        }

        private void InitGrid(GridPanelInfo panel, List<GridPanelColumnInfo> lstGridPanelColumn)
        {
            //if (panel.RowCheckBox)
            //{
            //    CheckboxSelectionModel r = new CheckboxSelectionModel();
            //    r.ID = "rowSelectionModel";
            //    r.Listeners.SelectionChange.Handler = "GetColumnID(#{rowSelectionModel});";
            //    extGridPanel.SelectionModel.Add(r);
            //}
            //else
            //{
            //    RowSelectionModel r = new RowSelectionModel();
            //    r.ID = "rowSelectionModel";
            //    r.Listeners.RowSelect.Handler = "document.getElementById('hdfRecordId').value = #{rowSelectionModel}.getSelected().id;";
            //    extGridPanel.SelectionModel.Add(r);
            //}  

            // init row expand
            if (!string.IsNullOrEmpty(RowExpanderTemplateUrl))
            {
                var rowExpander = new RowExpander()
                {
                    ID = "rx",
                };
                rowExpander.Template.Html = Util.GetInstance().ReadFile(Server.MapPath(RowExpanderTemplateUrl));
                extGridPanel.Plugins.Add(rowExpander);
            }

            // check column list
            if (lstGridPanelColumn == null)
                // null column list, get from database
                lstGridPanelColumn = GridController.GetInstance().GetColumnInfo(ID, TableName, 1);
            // display row number
            if (panel.DisplayRowNumber)
            {
                var stt = new RowNumbererColumn();
                if (lstGridPanelColumn.Any(t => t.Locked))
                {
                    stt.Locked = true;
                }

                stt.Width = 32;
                stt.Header = "STT";
                extGridPanel.ColumnModel.Columns.Add(stt);
            }

            // grid title
            extGridPanel.Title = panel.Header == false ? "" : panel.Title;
            // auto expand column
            if (!string.IsNullOrEmpty(panel.AutoExpandColumn))
            {
                if (lstGridPanelColumn.Count() != 0)
                {
                    if (lstGridPanelColumn.FirstOrDefault(p => p.ColumnName == panel.AutoExpandColumn) != null)
                    {
                        extGridPanel.AutoExpandColumn = panel.AutoExpandColumn;
                    }
                }
                else
                {
                    Datatable = DataHandler.GetInstance().ExecuteDataTable("select top 1 * from " + TableName);
                    foreach (DataColumn item in Datatable.Columns)
                    {
                        if (item.ColumnName == panel.AutoExpandColumn)
                        {
                            extGridPanel.AutoExpandColumn = panel.AutoExpandColumn;
                            break;
                        }
                    }
                }
            }

            // primary key
            PrimaryKey = Util.GetInstance().GetPrimaryKeyOfTable(panel.TableName);
            if (string.IsNullOrEmpty(PrimaryKey))
            {
                PrimaryKey = Util.GetInstance().GetPrimaryKeyOfTable(panel.TableName);
            }

            // datasource page size
            Store1.AutoLoadParams.Add(new Parameter("limit", panel.PageSize.ToString()));
            // set hidden field value
            sHdfgridName.Value = ID;
            sHdftable.Value = TableName;
            sHdfwhere.Value = panel.WhereClause.Replace("where", "");
            sHdfOrderBy.Value = panel.OrderBy;
            sHdfPrimaryKeyName.Value = PrimaryKey;
            // get column name for sql query
            var column = "";
            if (lstGridPanelColumn.Count() != 0)
            {
                column = "[" + PrimaryKey + "]";
                column = lstGridPanelColumn.Where(item => !item.ColumnName.Equals(PrimaryKey))
                    .Aggregate(column, (current, item) => current + (",[" + item.ColumnName + "]"));
            }

            // set hidden field column list
            hdfColumnList.Value = column;
            // page size
            if (panel.PageSize.HasValue)
            {
                ComboBoxPaging.SelectedIndex = panel.PageSize.Value / 5 - 1;
                StaticPagingToolbar.PageSize = panel.PageSize.Value;
            }

            // icon
            if (!string.IsNullOrEmpty(panel.Icon))
            {
                extGridPanel.IconCls = panel.Icon;
            }
            else
            {
                extGridPanel.Icon = Icon.Table;
            }

            // create script after edit       
            ltrAfterEditJs.Text = @"<script type='text/javascript'>" +
                                  @"var afterEdit = function (e) {  " +
                                  @"Ext.Msg.confirm('Xác Nhận', 'Bạn có muốn lưu lại không ?', function (btn) {" +
                                  @"if (btn == 'yes') {" +
                                  @" directMethod.AfterEdit(e.record.data." + PrimaryKey + @"+'',e.field, e.value);" +
                                  @" }});store.commitChanges();" +
                                  @"};" +
                                  @"</script>";


            //load bảng thông tin chi tiết
            if (!string.IsNullOrEmpty(GridPanel.InformationPanel))
            {
                var frmInfo = FormController.GetInstance().GetForm(ID + "formDetail");
                if (frmInfo != null)
                    switch (GridPanel.InformationPanel)
                    {
                        case "Top":
                            plNorth.Height = frmInfo.Height.HasValue ? frmInfo.Height.Value : 200;
                            plNorth.Visible = true;
                            plNorth.Title = frmInfo.Title;
                            break;
                        case "Right":
                            if (frmInfo.Width > 1)
                                plEast.Width = (int) frmInfo.Width;
                            else if (frmInfo.Width > 0 && frmInfo.Width <= 1)
                                plEast.AnchorHorizontal = frmInfo.Width * 100 + "%";
                            else if (frmInfo.Width == 0)
                                plEast.AnchorHorizontal = "40%";
                            plEast.Visible = true;
                            plEast.Title = frmInfo.Title;
                            break;
                        case "Bottom":
                            plSouth.Height = frmInfo.Height.HasValue ? frmInfo.Height.Value : 200;
                            plSouth.Visible = true;
                            plSouth.Title = frmInfo.Title;
                            break;
                        case "Left":
                            if (frmInfo.Width > 1)
                                plWest.Width = (int) frmInfo.Width;
                            else if (frmInfo.Width > 0 && frmInfo.Width <= 1)
                                plWest.AnchorHorizontal = frmInfo.Width * 100 + "%";
                            else if (frmInfo.Width == 0)
                                plWest.AnchorHorizontal = "40%";
                            plWest.Visible = true;
                            plWest.Title = frmInfo.Title;
                            break;
                    }
            }


            //set local filter
            if (panel.AllowFilter)
            {
                var filter = new GridFilters {Local = true, FiltersText = "Lọc"};
                extGridPanel.Plugins.Add(filter);
                Datatable = DataHandler.GetInstance().ExecuteDataTable("select top 1 * from " + TableName);
                foreach (DataColumn item in Datatable.Columns)
                {
                    switch (item.DataType.ToString())
                    {
                        case "System.Int32":
                            var numberic = new NumericFilter {DataIndex = item.ColumnName};
                            filter.Filters.Add(numberic);
                            break;
                        case "System.Boolean":
                            var boolean = new BooleanFilter {DataIndex = item.ColumnName};
                            filter.Filters.Add(boolean);
                            break;
                        case "System.String":
                            var str = new StringFilter {DataIndex = item.ColumnName};
                            filter.Filters.Add(str);
                            break;
                        case "System.DateTime":
                            var datefilter = new DateFilter
                            {
                                BeforeText = @"Trước ngày",
                                AfterText = @"Sau ngày",
                                OnText = @"Vào ngày",
                                DataIndex = item.ColumnName
                            };
                            filter.Filters.Add(datefilter);
                            break;
                    }
                }
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// After update config, reload page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void AfterUpdateConfig(object sender, EventArgs e)
        {
            RM.RegisterClientScriptBlock("reloadpage", "location.reload();");
        }

        /// <summary>
        /// After update record, reload store
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AfterUpdateRecord(object sender, EventArgs e)
        {
            RM.RegisterClientScriptBlock("reloadstore", "#{Store1}.reload();");
        }

        #endregion

        #region Toolbar Event Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, DirectEventArgs e)
        {
            try
            {
                if (!GridPanel.OneManyForm)
                {
                    var form = (Modules_Base_Form_Form) plcWindowAddForm.Controls[0];
                    form.Show();
                }
                else
                {
                    var oneManyForm = (Modules_Base_OneManyForm_OneManyForm) plcWindowOneManyForm.Controls[0];
                    oneManyForm.CommandButton = BaseForm.Command.Insert;
                    oneManyForm.Show(hdfRecordId.Text);
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowError("Có lỗi xảy ra trong quá trình thêm: {0}".FormatWith(ex.Message));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEdit_Click(object sender, DirectEventArgs e)
        {
            try
            {
                if (!GridPanel.OneManyForm)
                {
                    var form = (Modules_Base_Form_Form) plcWindowEditForm.Controls[0];
                    form.FormReference = ID + "ucWindowAddNew";
                    form.PrimaryColumnValue = hdfRecordId.Text;
                    form.Show();
                }
                else
                {
                    var oneManyForm = (Modules_Base_OneManyForm_OneManyForm) plcWindowOneManyForm.Controls[0];
                    oneManyForm.CommandButton = BaseForm.Command.Update;
                    oneManyForm.Show(hdfRecordId.Text);
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowError("Có lỗi xảy ra trong quá trình sửa: {0}".FormatWith(ex.Message));
            }
        }

        #endregion

        #region Direct Methods

        #endregion

        /// <summary>
        /// sự kiện refresh lại trang sau khi cấu hình thông tin GridPanel xong
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ct_AfterClickUpdateInformation(object sender, EventArgs e)
        {
            RM.RegisterClientScriptBlock("ReloadPageAgain", "location.reload();");
        }

        /// <summary>
        /// Reload lại store sau khi thêm mới cột
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ct_AfterCreateNewColumn(object sender, EventArgs e)
        {
            RM.RegisterClientScriptBlock("reloadStoreColumnInfo", "#{StoreColumnInfo}.reload();");
        }




        protected void DeselectedRow1(object sender, DirectEventArgs e)
        {
            var sm = extGridPanel.SelectionModel.Primary as RowSelectionModel;
            //foreach (SelectedRow row in sm.SelectedRows)
            //{
            //    int index = int.Parse(hdfRecordId.Text);
            //    if (row.RowIndex == index) {
            //        Dialog.ShowError(hdfRecordId.Text);
            //    }
            //}
        }





        /// <summary>
        /// Gọi hàm javascript sử dụng Resource Manager
        /// </summary>
        /// <param name="script"></param>
        public void CallScriptUsingResourceManager(string script)
        {
            RM.RegisterClientScriptBlock("CallScript", script);
        }

        public RowSelectionModel GetRowSelectionModel()
        {
            return rowSelectionModel;
        }

        /// <summary>
        /// Load field vào store
        /// </summary>
        private void LoadField()
        {
            Store1.AutoLoad = !DisableAutoLoad;
            if (ColumnList == null)
                ColumnList = GridController.GetInstance().GetColumnInfo(ID, TableName, 1);
            var jsonReader = new JsonReader {Root = "Data"};
            if (string.IsNullOrEmpty(PrimaryKey))
            {
                PrimaryKey = Util.GetInstance().GetPrimaryKeyOfTable(GridPanel.TableName);
            }

            jsonReader.IDProperty = PrimaryKey;
            jsonReader.TotalProperty = "TotalRecords";

            if (ColumnList.FirstOrDefault() != null)
            {
                foreach (var field in ColumnList.Select(columnInfo => new RecordField {Name = columnInfo.ColumnName}))
                {
                    jsonReader.Fields.Add(field);
                }
            }
            else
            {
                var dataTable = DataHandler.GetInstance().ExecuteDataTable("select top 1 * from " + TableName);
                foreach (DataColumn item in dataTable.Columns)
                {
                    var field = new RecordField {Name = item.ColumnName};
                    jsonReader.Fields.Add(field);
                }
            }

            Store1.Reader.Add(jsonReader);
        }

        /// <summary>
        /// Load column vào GridPanel
        /// </summary>
        private void AddColumn()
        {
            var htmlExpandRowTemplate = "";
            if (!string.IsNullOrEmpty(RowExpanderTemplateUrl))
                htmlExpandRowTemplate = Util.GetInstance().ReadFile(Server.MapPath(RowExpanderTemplateUrl));
            if (ColumnList == null)
                ColumnList = GridController.GetInstance().GetColumnInfo(ID, TableName, 1);
            if (ColumnList.FirstOrDefault() == null)
            {
                Datatable = DataHandler.GetInstance().ExecuteDataTable("select top 1 * from " + TableName);
                foreach (DataColumn item in Datatable.Columns)
                {
                    var column = new Column {DataIndex = item.ColumnName, Header = item.ColumnName};
                    extGridPanel.ColumnModel.Columns.Add(column);
                    if (item.DataType.ToString().Equals("System.DateTime"))
                    {
                        column.Renderer.Fn = "GetDateFormat";
                    }
                }
            }
            else
            {
                var listener = "";
                var hasComboBox = false;
                //Khóa cột
                if (ColumnList.Count(t => t.Locked) > 0)
                {
                    var lockview = new LockingGridView {ID = "LockingGridView" + ID, EnableViewState = false};
                    extGridPanel.View.Clear();
                    extGridPanel.View.Add(lockview);
                }

                foreach (var columnInfo in ColumnList)
                {
                    if (htmlExpandRowTemplate.Contains("{" + columnInfo.ColumnName + "}"))
                        continue;
                    var column = new Column
                    {
                        DataIndex = columnInfo.ColumnName,
                        Header = columnInfo.ColumnHeader,
                        Locked = columnInfo.Locked
                    };
                    switch (columnInfo.Align)
                    {
                        case "right":
                            column.Align = Alignment.Right;
                            break;
                        case "center":
                            column.Align = Alignment.Right;
                            break;
                    }

                    if (columnInfo.Width.HasValue && columnInfo.Width != 0)
                        column.Width = columnInfo.Width.Value;
                    extGridPanel.ColumnModel.Columns.Add(column);
                    if (string.IsNullOrEmpty(columnInfo.RenderJS) == false)
                    {
                        column.Renderer.Fn = columnInfo.RenderJS;
                    }

                    if (columnInfo.AllowSearch)
                    {
                        column.Renderer.Fn = "RenderHightLight";
                    }

                    if (columnInfo.AllowComboBoxOnGrid && string.IsNullOrEmpty(columnInfo.TableName) == false)
                    {
                        hasComboBox = true;
                        var cbBox = new ComboBox
                        {
                            LoadingText = @"Đang tải...",
                            ID = "combo" + columnInfo.ColumnName,
                            DisplayField = "displayField",
                            ValueField = "valueField",
                            EnableViewState = false,
                            Editable = false
                        };
                        //add trigger X 
                        var fieldTrigger =
                            new FieldTrigger {Icon = TriggerIcon.Clear, HideTrigger = true, Qtip = "Hủy"};
                        cbBox.Triggers.Add(fieldTrigger);
                        cbBox.Listeners.Select.Handler = "this.triggers[0].show();";
                        cbBox.Listeners.BeforeQuery.Handler =
                            "this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();";
                        cbBox.Listeners.TriggerClick.Handler =
                            "if (index == 0) { this.clearValue(); this.triggers[0].hide(); }";
                        // cbBox.PageSize = 10;
                        //end add trigger X

                        column.Editor.Add(cbBox);
                        var store = CreateStore(cbBox.ID);
                        cbBox.Store.Add(store);
                        StoreList.Add(new StoreDaTa(store,
                            columnInfo.ComboBoxTable,
                            columnInfo.DisplayFieldComboBox,
                            columnInfo.ValueFieldComboBox,
                            columnInfo.WhereFilterComboBox,
                            columnInfo.ColumnName,
                            columnInfo.MasterColumnComboID.Value,
                            columnInfo.ID));

                        if (columnInfo.MasterColumnComboID.HasValue && columnInfo.MasterColumnComboID.Value != 0)
                        {
                            var col = ColumnList.FirstOrDefault(p => p.ID == columnInfo.MasterColumnComboID.Value);
                            if (col != null)
                            {
                                listener += string.Format(
                                    "case \"{0}\": this.getColumnModel().getCellEditor(e.column, e.row).field.allQuery = e.record.get('{1}');break;",
                                    columnInfo.ColumnName, col.ColumnName);
                            }
                        }
                    }

                    if (GridPanel == null)
                        GridPanel = GridController.GetInstance().GetGridPanel(ID);
                    if (GridPanel.AllowEditOnGrid)
                    {
                        //Nếu cột cho phép sửa thì sẽ add control trên Grid để sửa
                        if (columnInfo.AllowEditOnGrid)
                        {
                            if (!column.Editor.Any())
                                AddEditor(column, columnInfo);
                        }
                    }
                }

                if (!string.IsNullOrEmpty(listener))
                {
                    extGridPanel.Listeners.BeforeEdit.Handler = "switch (e.field) {" + listener + "}";
                }

                if (hasComboBox)
                {
                    extGridPanel.DirectEvents.AfterEdit.Event += AfterEdit_Event;
                    extGridPanel.DirectEvents.AfterEdit.ExtraParams.Add(new Parameter("id", "e.record.id",
                        ParameterMode.Raw));
                    extGridPanel.DirectEvents.AfterEdit.ExtraParams.Add(new Parameter("field", "e.field",
                        ParameterMode.Raw));
                    extGridPanel.DirectEvents.AfterEdit.ExtraParams.Add(new Parameter("record", "e.record.data",
                        ParameterMode.Raw, true));
                    extGridPanel.DirectEvents.AfterEdit.ExtraParams.Add(new Parameter("value", "e.value",
                        ParameterMode.Raw));
                }
            }
        }

        /// <summary>
        /// Add các control vào Grid để có thể sửa trực tiếp trên Grid
        /// </summary>
        /// <param name="column"></param>
        /// <param name="columnInfo"></param>
        private void AddEditor(Column column, GridPanelColumnInfo columnInfo)
        {
            switch (columnInfo.DataType)
            {
                case "System.DateTime":
                    var datefield = new DateField {ID = "datefield" + column.DataIndex};
                    column.Editor.Add(datefield);
                    break;
                case "System.String":
                    var textfield = new TextField {ID = "textfield" + column.DataIndex};
                    column.Editor.Add(textfield);
                    break;
                case "System.Boolean":
                    var chkCheckBox = new Checkbox {ID = "chkCheckBox" + column.DataIndex};
                    column.Editor.Add(chkCheckBox);
                    break;
                case "System.Int32":
                    var intNumber = new NumberField {AllowDecimals = false, ID = "intNumber" + column.DataIndex};
                    column.Editor.Add(intNumber);
                    break;
                case "System.Decimal":
                    var decNumber = new NumberField {AllowDecimals = true, ID = "decNumber" + column.DataIndex};
                    column.Editor.Add(decNumber);
                    break;
            }
        }

        /// <summary>
        /// Lưu các thông tin sau khi edit trực tiếp trên Grid vào CSDL
        /// </summary>
        /// <param name="recordId"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        [DirectMethod]
        public void AfterEdit(string recordId, string field, string value)
        {
            if (AfterEditOnGrid != null)
            {
                AfterEditOnGrid(recordId + ";" + field + ";" + value, null);
                return;
            }

            string sql = string.Empty;
            try
            {
                if (GridPanel == null)
                    GridPanel = GridController.GetInstance().GetGridPanel(ID);
                PrimaryKey = Util.GetInstance().GetPrimaryKeyOfTable(GridPanel.TableName);
                if (value.Replace("\"", "") == "false")
                    value = "0";
                else if (value.Replace("\"", "") == "true")
                    value = "1";
                sql = "update " + GridPanel.TableName + " set " + field + " = N'" + value.Replace("\"", "") +
                      "' where " + this.PrimaryKey + " = N'" + recordId + "'"; //hdfRecordId.Text.Trim()
                DataHandler.GetInstance().ExecuteNonQuery(sql);
                //Ghi log cho hành động thêm mới
                //var accessDiary = new Web.Core.Object.Security.AccessHistory
                //{
                //    Function = EditOnGrid,
                //    Description = EditOnGrid,
                //    IsError = false,
                //    UserName = CurrentUser.User.UserName,
                //    Time = DateTime.Now,
                //    BusinessCode = GridPanel.TableName,
                //    ComputerName = Util.GetInstance().GetComputerName(Request.UserHostAddress),
                //    ComputerIP = Request.UserHostAddress,
                //    Referent = field + " : Giá trị mới = " + value + ", câu truy vấn sql = " + sql
                //};
                //AccessHistoryServices.Create(accessDiary);
                //var accessDiary = new NhatkyTruycapInfo()
                //{
                //    CHUCNANG = GlobalResourceManager.GetInstance().GetHistoryAccessValue("EditOnGrid"),
                //    MOTA = GlobalResourceManager.GetInstance().GetHistoryAccessValue("EditOnGrid"),
                //    IsError = false,
                //    USERNAME = CurrentUser.User.UserName,
                //    THOIGIAN = DateTime.Now,
                //    MANGHIEPVU = gridPanel.TableName,
                //    TENMAY = Util.GetInstance().GetComputerName(Request.UserHostAddress),
                //    IPMAY = Request.UserHostAddress,
                //    THAMCHIEU = field + " : Giá trị mới = " + value + ", câu truy vấn sql = " + sql,
                //};
                //new AccessHistoryController().AddAccessHistory(accessDiary);
            }
            catch (Exception ex)
            {
                Dialog.ShowNotification("Có lỗi xảy ra trong quá trình cập nhật, vui lòng thử lại!");
                //var accessDiary = new Web.Core.Object.Security.AccessHistory
                //{
                //    Function = EditOnGrid,
                //    Description = EditOnGrid,
                //    IsError = true,
                //    UserName = CurrentUser.User.UserName,
                //    Time = DateTime.Now,
                //    BusinessCode = TableName,
                //    ComputerName = Util.GetInstance().GetComputerName(Request.UserHostAddress),
                //    ComputerIP = Request.UserHostAddress,
                //    Referent = field + " : Giá trị mới = " + value + ", câu truy vấn sql = " + sql
                //};
                //AccessHistoryServices.Create(accessDiary);
                //var accessDiary = new NhatkyTruycapInfo()
                //{
                //    CHUCNANG = GlobalResourceManager.GetInstance().GetHistoryAccessValue("EditOnGrid"),
                //    MOTA = ex.Message,
                //    IsError = true,
                //    USERNAME = CurrentUser.User.UserName,
                //    THOIGIAN = DateTime.Now,
                //    MANGHIEPVU = TableName,
                //    TENMAY = Util.GetInstance().GetComputerName(Request.UserHostAddress),
                //    IPMAY = Request.UserHostAddress,
                //    THAMCHIEU = field + " : Giá trị mới = " + value + ", câu truy vấn sql = " + sql,
                //};
                //new AccessHistoryController().AddAccessHistory(accessDiary);
            }
        }

        /// <summary>
        /// Sự kiện AfterEdit của extGridPanel, xảy ra khi người dùng thay đổi giá trị combobox trên Grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void AfterEdit_Event(object sender, DirectEventArgs e)
        {
            var data = JSON.Deserialize<JsonObject>(e.ExtraParams["record"]);
            foreach (var store in StoreList)
            {
                var s = StoreList.FirstOrDefault(p => p.ColumnID == store.MasterColumnID);
                if (s == null)
                    continue;
                if (!string.IsNullOrEmpty(store.WhereFilter))
                    store.WhereFilter = string.Format(store.WhereFilter, "N'" + data[s.ColumnName] + "'");
                else
                    store.WhereFilter = "1=1";
                var sql = string.Format("select top 1 {0} from {1} where {2}", store.DisplayField, store.TableName,
                    store.WhereFilter);
                var Data = DataHandler.GetInstance().ExecuteDataTable(sql);
                if (Data.Rows.Count != 0)
                    Store1.UpdateRecordField(e.ExtraParams["id"], store.ColumnName,
                        Data.Rows[0][store.DisplayField].ToString());
            }
        }

        /// <summary>
        /// Tạo Store cho các ComboBox trên GridPanel
        /// </summary>
        /// <returns></returns>
        private Store CreateStore(string StoreName)
        {
            var st = new Store();
            st.ID = "store" + StoreName;
            st.AutoLoad = false;
            st.RefreshData += cbBoxStoreRefresh;
            st.Proxy.Add(new PageProxy());
            var reader = new JsonReader {IDProperty = "valueField"};
            reader.Fields.Add(new RecordField("valueField"));
            reader.Fields.Add(new RecordField("displayField"));
            st.Reader.Add(reader);
            return st;
        }

        /// <summary>
        /// Store được sử dụng để fill data vào combobox trên grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cbBoxStoreRefresh(object sender, StoreRefreshDataEventArgs e)
        {
            try
            {
                var storeData = StoreList.Where(p => p.Store.ID == ((Store) sender).ID).FirstOrDefault();
                var query = e.Parameters["query"];
                var dataList = new List<object>();
                if (string.IsNullOrEmpty(storeData.WhereFilter))
                    storeData.WhereFilter = "1=1";
                else if (string.IsNullOrEmpty(query) == false
                ) //Note: Nếu muốn sử dụng query thì trong trường filter phải có đoạn text như FieldName like {0}
                {
                    storeData.WhereFilter = string.Format(storeData.WhereFilter, "N'" + query + "'");
                }

                var sql = string.Format("select {0},{1} from {2} where {3}", storeData.DisplayField,
                    storeData.ValueField, storeData.TableName, storeData.WhereFilter);
                var Data = DataHandler.GetInstance().ExecuteDataTable(sql);

                foreach (DataRow row in Data.Rows)
                {
                    string _displayField = row[storeData.DisplayField].ToString();
                    string _valueField = row[storeData.ValueField].ToString();

                    dataList.Add(new {valueField = _valueField, displayField = _displayField});
                }

                if (storeData.Store != null)
                {
                    storeData.Store.DataSource = dataList;
                    storeData.Store.DataBind();
                }
            }
            catch
            {
            }
        }

        void RowSelect_Event(object sender, DirectEventArgs e)
        {
            if (RowSelect != null)
            {
                RowSelect(sender, null);
            }
            else
            {
                switch (GridPanel.InformationPanel)
                {
                    case "Top":
                        Dialog.ShowNotification(plcNorth.Controls.Count.ToString() + plcWest.Controls.Count +
                                                plcEast.Controls.Count + plcNorth.Controls.Count);
                        //      (plcNorth.Controls[0] as Form).PrimaryColumnValue = hdfRecordId.Text;
                        //   ((IControl)plcNorth.Controls[0]).SetValue(hdfRecordId.Text);
                        break;
                    case "Right":
                        (plcEast.Controls[0] as BaseForm).PrimaryColumnValue = hdfRecordId.Text;
                        ((IBaseControl) plcEast.Controls[0]).SetValue(hdfRecordId.Text);
                        break;
                    case "Bottom":
                        (plcSouth.Controls[0] as BaseForm).PrimaryColumnValue = hdfRecordId.Text;
                        ((IBaseControl) plcSouth.Controls[0]).SetValue(hdfRecordId.Text);
                        break;
                    case "Left":
                        (plcWest.Controls[0] as BaseForm).PrimaryColumnValue = hdfRecordId.Text;
                        ((IBaseControl) plcWest.Controls[0]).SetValue(hdfRecordId.Text);
                        break;
                }
            }
        }

        protected void btnDelete_Click(object sender, DirectEventArgs e)
        {
            //var accessDiary = new AccessHistory();
            //accessDiary.Function = GlobalResourceManager.GetInstance().GetHistoryAccessValue("History");
            //accessDiary.Description = GlobalResourceManager.GetInstance().GetHistoryAccessValue("History");
            //accessDiary.IsError = false;
            //accessDiary.UserName = CurrentUser.User.UserName;
            //accessDiary.Time = DateTime.Now;
            //accessDiary.BusinessCode = gridPanel.TableName;
            //accessDiary.ComputerName = Util.GetInstance().GetComputerName(Request.UserHostAddress);
            //accessDiary.ComputerIP = Request.UserHostAddress;
            //accessDiary.Referent = "";
            //AccessHistoryServices.Create(accessDiary);

            //NhatkyTruycapInfo accessDiary = null;
            //if (accessHistory != null)
            //    accessDiary = new NhatkyTruycapInfo()
            //    {
            //        CHUCNANG = accessHistory.ModuleDescription,
            //        IsError = false,
            //        USERNAME = CurrentUser.User.UserName,
            //        THOIGIAN = DateTime.Now,
            //        MANGHIEPVU = gridPanel.TableName,
            //        TENMAY = Util.GetInstance().GetComputerName(Request.UserHostAddress),
            //        IPMAY = Request.UserHostAddress,
            //        THAMCHIEU = "",
            //    };
            try
            {
                if (string.IsNullOrEmpty(PrimaryKey))
                {
                    if (GridPanel == null)
                        GridPanel = GridController.GetInstance().GetGridPanel(ID);
                    PrimaryKey = Util.GetInstance().GetPrimaryKeyOfTable(GridPanel.TableName);
                }

                foreach (var item in rowSelectionModel.SelectedRows)
                {
                    var query = "delete from " + GridPanel.TableName + " where " + PrimaryKey + " = N'" +
                                item.RecordID + "'";
                    DataHandler.GetInstance().ExecuteNonQuery(query);

                    //if (accessHistory != null)
                    //{
                    //    accessDiary.MOTA = accessHistory.Delete_AccessHistoryDescription + ", khóa chính bản ghi: " + item.RecordID;
                    //    new AccessHistoryController().AddAccessHistory(accessDiary);
                    //}
                }

                hdfRecordId.Text = "";
                RM.RegisterClientScriptBlock("rlStore",
                    "RemoveItemOnBaseGrid(#{extGridPanel},#{Store1},#{DirectMethods});");
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("conflicted with the REFERENCE constraint"))
                {
                    Dialog.ShowError("Không thể xóa được vì dữ liệu này đang được sử dụng ở chỗ khác !");
                }
                else
                    Dialog.ShowError("Base/Module/GridPanel " + ex.Message);

                //if (accessHistory != null)
                //{
                //    accessDiary.MOTA = ex.Message;
                //    accessDiary.IsError = true;
                //    new AccessHistoryController().AddAccessHistory(accessDiary);
                //}
            }
        }


        [DirectMethod]
        public void DirectEdit()
        {
            btnEdit_Click(null, null);
        }

        [DirectMethod]
        public void DirectAdd()
        {
            btnAdd_Click(null, null);
        }

        [DirectMethod]
        public void SaveColumnOrder(string column)
        {
            try
            {
                var columnname = column.Split(',');
                var order = 0;
                foreach (var item in columnname)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        GridController.GetInstance().UpdateColumnOrder(ID, TableName, item, order);
                        order++;
                    }
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowError("Thông báo lỗi 1", ex.Message);
            }

            // Dialog.ShowNotification("Cập nhật thứ tự cột thành công !", false);
        }

        [DirectMethod]
        public void SaveColumnWidth(string column)
        {
            try
            {
                var col = column.Split(',');
                foreach (var item in col)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        var tmp = item.Split('=');
                        GridController.GetInstance().UpdateColumnWidth(ID, TableName, tmp[0], int.Parse(tmp[1]));
                    }
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowError("Thông báo lỗi 2", ex.Message);
            }

            //  Dialog.ShowNotification("Cập nhật độ rộng của cột thành công !", false);
        }



        #region Toolbar action

        public void HiddenAddButton(bool hidden)
        {
            btnAddRecord.Hidden = hidden;
            mnuAddNew.Hidden = hidden;
        }

        public void HiddenEditButton(bool hidden)
        {
            btnEdit.Hidden = hidden;
            mnuEditUser.Hidden = hidden;
        }

        public void HiddenDeleteButton(bool hidden)
        {
            btnDelete.Hidden = hidden;
            MenuItem4.Hidden = hidden;
        }

        public void HiddenSearchBox(bool hidden)
        {
            btnSearch.Hidden = hidden;
            txtSearchKey.Hidden = hidden;
        }

        public void HiddenTienIch(bool hidden)
        {
            btnButtonTienIch.Hidden = hidden;
        }

        public void HiddenDuplicateButton(bool hidden)
        {
            menuCopyData.Hidden = true;
            if (menuTienIch.Items.Count() == 0)
                btnButtonTienIch.Hidden = hidden;
        }

        public void ClearAllButtonOnToolbar()
        {
            foreach (var item in Toolbar1.Items)
            {
                if (item.ID != "btnConfig")
                    item.Hidden = true;
            }
        }

        public void HiddenPagingToolbar(bool hidden)
        {
            StaticPagingToolbar.Hidden = hidden;
        }

        #endregion






        /// <summary>
        /// Hiện window cấu hình GridPanel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void mnuShowConfigWindow_Click(object sender, DirectEventArgs e)
        {
            try
            {
                if (plcConfig.Controls.Count != 0)
                {
                    Control ct = plcConfig.Controls[0];
                    ((Modules_Base_GridPanel_Config_GridConfig) ct).ShowGridPanelInformationConfig();
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowError("GridPanel/mnuShowConfigWindow_Click = " + ex.Message);
            }
        }

        /// <summary>
        /// Hiện form thông tin cấu hình cột
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void mnuColumnInformation_Click(object sender, DirectEventArgs e)
        {
            try
            {
                if (plcConfig.Controls.Count != 0)
                {
                    var ct = plcConfig.Controls[0];
                    ((Modules_Base_GridPanel_Config_GridConfig) ct).ShowColumnInformationConfig();
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowError("GridPanel/mnuColumnInformation_Click = " + ex.Message);
            }
        }

        /// <summary>
        /// Thêm control vào Toolbar, được sử dụng để developer thêm các control bên ngoài vào
        /// </summary>
        /// <param name="component"></param>
        public void AddComponent(Component component)
        {
            if (Toolbar1.Items.Count() >= 5)
                Toolbar1.Items.Insert(Toolbar1.Items.Count() - 5, component);
            else
                Toolbar1.Items.Insert(0, component);
        }

        public void AddComponent(Component component, int position)
        {
            try
            {
                Toolbar1.Items.Insert(position, component);
            }
            catch (Exception ex)
            {
                Dialog.ShowError("GridPanel/AddComponent = " + ex.Message);
            }
        }

        public void AddMenuItemToTienIch(MenuItem menuItem, int position)
        {
            menuTienIch.Items.Insert(position, menuItem);
        }

        public void SetHeaderTitle(string Title)
        {
            extGridPanel.Title = Title;
        }

        /// <summary>
        /// Lấy ID của các bản ghi được chọn
        /// </summary>
        /// <returns></returns>
        public List<string> GetSelectedRowsRecordId()
        {
            var rs = from r in rowSelectionModel.SelectedRows select r.RecordID;
            return rs.ToList();
        }

        /// <summary>
        /// Phương thức tạm thời để test
        /// </summary>
        public void ReloadStore()
        {
            hdfQueryPhu.Text = OutSideQuery;
            RM.RegisterClientScriptBlock("zz", "#{Store1}.reload();");
        }

        /// <summary>
        /// Thêm nút vào MenuContext
        /// </summary>
        public void AddToMenuContext(Component menuItem)
        {
            RowContextMenu.Items.Add(menuItem);
        }

        public void AddToMenuContext(int position, MenuItem menuItem)
        {
            RowContextMenu.Items.Insert(position, menuItem);
        }

        /// <summary>
        /// cho phép developer add các control tự viết thêm vào bên trái như TreePanel, GridPanel...
        /// </summary>
        /// <param name="component"></param>
        /// <param name="PanelTitle"></param>
        /// <param name="collapsed">true = thu nhỏ thanh panel, false = mở rộng</param>
        public void AddComponentToLeftPanel(Component component, string PanelTitle, bool collapsed)
        {
            plWest.Items.Add(component);
            plWest.Collapsed = collapsed;
            plWest.Visible = true;
            plWest.Width = component.Width;
            plWest.Title = PanelTitle;
        }

        /// <summary>
        /// cho phép developer add các control tự viết thêm vào bên phải như TreePanel, GridPanel...
        /// </summary>
        /// <param name="component"></param>
        /// <param name="PanelTitle"></param>
        public void AddComponentToRightPanel(Component component, string PanelTitle)
        {
            plEast.Items.Add(component);
            plEast.Visible = true;
            plEast.Width = component.Width;
            plEast.Title = PanelTitle;
        }

        /// <summary>
        /// cho phép developer add các control tự viết thêm vào bên dưới như TreePanel, GridPanel...
        /// </summary>
        /// <param name="component"></param>
        /// <param name="PanelTitle"></param>
        public void AddComponentToBottomPanel(Component component, string PanelTitle)
        {
            var p = new Panel {Border = false};
            p.Items.Add(extGridPanel);
            extGridPanel.Height = 400;
            borderLayout.Center.Items.Clear();
            borderLayout.Center.Items.Add(p);
            p.Items.Insert(1, component);
            component.Height = 250;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="component"></param>
        /// <param name="PanelTitle"></param>
        /// <param name="height"></param>
        public void AddComponentToBottomPanel(Control component, string PanelTitle, int height)
        {
            //Ext.Net.Panel p = new Ext.Net.Panel();
            //p.Border = false;
            //p.Items.Add(extGridPanel);
            //borderLayout.Center.Items.Clear();
            //borderLayout.Center.Items.Add(p);
            //p.ContentContainer.Controls.Add(component);

            var br = new BorderLayout();
            br.Center.Items.Add(extGridPanel);
            borderLayout.Center.Items.Clear();
            var pl = new Panel {Layout = "Fit"};
            pl.Items.Add(br);
            pl.Border = false;
            borderLayout.Center.Items.Add(pl);
            var p = new Panel {Height = height, Border = false};
            //  p.Height = ((Form)component).Height;
            //    br.South.Collapsible = true;
            p.ContentContainer.Controls.Add(component);
            br.South.Items.Add(p);
        }

        protected void btnCopyData_Click(object sender, DirectEventArgs e)
        {
            var d = rowSelectionModel.SelectedRows.Count();
            if (d <= 1)
            {
                if (CopyData())
                    ReloadStore();
            }
            else
            {
                Dialog.ShowError("Bạn chỉ được chọn một bản ghi");
            }

        }

        #region Private Methods

        private bool CopyData()
        {
            if (GridPanel == null)
                GridPanel = GridController.GetInstance().GetGridPanel(ID);
            var columnList = Util.GetInstance().GetColumnOfTable(GridPanel.TableName);
            if (string.IsNullOrEmpty(PrimaryKey))
            {
                PrimaryKey = Util.GetInstance().GetPrimaryKeyOfTable(GridPanel.TableName);
            }

            string sql = "";
            var column = columnList.Where(item => item != PrimaryKey)
                .Aggregate("", (current, item) => current + ("[" + item + "],"));
            column = column.Remove(column.LastIndexOf(","));
            var recordIdList = new List<string>();
            if (IsPrimaryKeyAutoIncrement)
            {
                sql = string.Format(
                    "insert into " + GridPanel.TableName + "({0}) select {1} from " + GridPanel.TableName + " where " +
                    this.PrimaryKey + " = ", column, column);
                recordIdList = (from t in rowSelectionModel.SelectedRows select t.RecordID).ToList();
            }
            else
            {
                var t = DataHandler.GetInstance()
                    .ExecuteDataTable("select " + column + " from " + GridPanel.TableName + " where " +
                                      this.PrimaryKey + " = N'" + hdfRecordId.Text + "'");
                column += ",[" + PrimaryKey + "]";
                string v = "";
                foreach (DataRow r in t.Rows)
                {
                    foreach (DataColumn c in t.Columns)
                    {
                        if (c.DataType.ToString() == "System.DateTime")
                            v += "N'" + Util.GetInstance().GetDateTimeEnfromVi(r[c.ColumnName].ToString()) + "',";
                        else
                            v += "N'" + r[c.ColumnName] + "',";
                    }
                }

                //v += "N'" + txtNewPrimaryKey.Text + "'";
                recordIdList.Add(hdfRecordId.Text);
                sql = "insert into " + GridPanel.TableName + "(" + column + ") values(" + v + ")";
            }

            foreach (var recordId in recordIdList)
            {
                //var accessDiary = new Core.Object.Security.AccessHistory
                //{
                //    Function = "Sao chép dữ liệu",
                //    Description = "Sao chép dữ liệu",
                //    IsError = false,
                //    UserName = CurrentUser.User.UserName,
                //    Time = DateTime.Now,
                //    BusinessCode = GridPanel.TableName,
                //    ComputerName = Util.GetInstance().GetComputerName(Request.UserHostAddress),
                //    ComputerIP = Request.UserHostAddress,
                //    Referent = ""
                //};
                //AccessHistoryServices.Create(accessDiary);
                //var accessDiary = new NhatkyTruycapInfo()
                //{
                //    CHUCNANG = GlobalResourceManager.GetInstance().GetHistoryAccessValue("CopyData"),
                //    IsError = false,
                //    USERNAME = CurrentUser.User.UserName,
                //    THOIGIAN = DateTime.Now,
                //    MANGHIEPVU = gridPanel.TableName,
                //    TENMAY = Util.GetInstance().GetComputerName(Request.UserHostAddress),
                //    IPMAY = Request.UserHostAddress,
                //    THAMCHIEU = "",
                //};
                try
                {
                    if (IsPrimaryKeyAutoIncrement)
                        DataHandler.GetInstance().ExecuteNonQuery(sql + "N'" + recordId + "'");
                    else
                        // Dialog.ShowError(sql);
                        DataHandler.GetInstance().ExecuteNonQuery(sql);
                    //accessDiary.MOTA = ", Khóa chính bản ghi gốc " + recordId;
                }
                catch (Exception ex)
                {
                    //accessDiary.MOTA = ex.Message.Replace("'", "") + ", Khóa chính bản ghi gốc " + recordId;
                    //accessDiary.IsError = true;
                    Dialog.ShowError(ex.Message.Contains("Cannot insert duplicate key in object")
                        ? "Khóa chính (mã ) của bản ghi bị trùng, yêu cầu bạn nhập lại!"
                        : "Có lỗi xảy ra! Hãy liên hệ với chúng tôi để nhận được sự trợ giúp!");
                    return false;
                }

                //AccessHistoryServices.Create(accessDiary);
                //new AccessHistoryController().AddAccessHistory(accessDiary);
            }

            return true;
        }

        #endregion


    }
}

