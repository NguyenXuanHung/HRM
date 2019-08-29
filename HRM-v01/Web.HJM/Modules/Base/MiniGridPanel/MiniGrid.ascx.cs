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
using Web.Core.Framework;

namespace Web.HJM.Modules.UserControl
{
    public partial class Modules_Base_MiniGridPanel_MiniGrid : BaseGridTable
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GridPanel = GridController.GetInstance().GetGridPanel(this.ID);
            if (GridPanel != null)
            {
                if (string.IsNullOrEmpty(TableName) && GridPanel != null)
                {
                    if (string.IsNullOrEmpty(GridPanel.ViewName) == false)
                    {
                        this.TableName = GridPanel.ViewName;
                    }
                    else
                    {
                        this.TableName = GridPanel.TableName;
                    }
                }
                else
                {
                    this.TableName = DefaultTable;
                }

                this.PrimaryKey = Util.GetInstance().GetPrimaryKeyOfTable(GridPanel.TableName);
            }
            else
            {
                TableName = DefaultTable;
            }

            if (CurrentUser.User.IsSuperUser)
            {
                AddConfigControl();
            }

            if (!X.IsAjaxRequest)
            {
                ShowData();
                if (CurrentUser.User.IsSuperUser)
                {
                    //  mnuTableInformationConfig.Listeners.Click.Handler = ((Modules_Base_MiniGridPanel_Config_GridConfig)plcConfig.Controls[0]).GetUpdateGridPanelClientID() + ".show();";
                    mnuColumnConfig.Listeners.Click.Handler =
                        ((Modules_Base_MiniGridPanel_Config_GridConfig) plcConfig.Controls[0])
                        .GetColumnInformationConfig() + ".show();";
                    btnConfig.Visible = true;
                }
                else
                {
                    btnConfig.Visible = false;
                    //  PagingToolbar1.Visible = false;
                }
            }

            AddColumn();
        }


        private void AddConfigControl()
        {
            Control ct = this.Page.LoadControl("../Base/MiniGridPanel/Config/GridConfig.ascx");
            ct.ID = this.ID + "ConfigControl";
            ((Modules_Base_MiniGridPanel_Config_GridConfig) ct).TableName = this.TableName;
            ((Modules_Base_MiniGridPanel_Config_GridConfig) ct).GridPanelName = this.ID;
            plcConfig.Controls.Add(ct);
        }

        private void GenerateListener()
        {
            RowSelectionModel1.Listeners.RowSelect.Handler =
                hdfRecordId.ClientID + ".setValue(" + RowSelectionModel1.ClientID + ".getSelected().id);" +
                "SetGridPanelAndDirectMethod(" + GridPanel1.ClientID + ",#{DirectMethods}," + Store1.ClientID + ");";

            //sự kiện khi ấn nút thêm mới trên grid
            mnuAddNew.Listeners.Click.Handler = GridPanel1.ClientID + ".insertRecord();" + Store1.ClientID +
                                                ".commitChanges();" + hdfIsInserting.ClientID + ".setValue('true')";
            string scriptContextMenu = "e.preventDefault(); " + RowContextMenu.ClientID +
                                       ".dataRecord = this.store.getAt(rowIndex);" + RowContextMenu.ClientID +
                                       ".showAt(e.getXY());" + RowSelectionModel1.ClientID + ".selectRow(rowIndex);";
            GridPanel1.Listeners.RowContextMenu.Handler = scriptContextMenu;
            if (CurrentUser.User.IsSuperUser)
            {
                GridPanel1.Listeners.ColumnMove.Handler =
                    "#{DirectMethods}.SaveColumnOrder(ChangeColumnOrder(" + GridPanel1.ClientID + "));";
                GridPanel1.Listeners.ColumnResize.Handler =
                    "#{DirectMethods}.SaveColumnWidth(ChangeColumnWidth(" + GridPanel1.ClientID + "));";
            }
            //sự kiện khi ấn nút thêm mới trên grid

            btnDeleteRecord.Listeners.Click.Handler =
                "DeleteItemOnGrid(" + GridPanel1.ClientID + "," + Store1.ClientID + ")";

            btnAddRecord.Listeners.Click.Handler = GridPanel1.ClientID + ".insertRecord();" + Store1.ClientID +
                                                   ".commitChanges();" + hdfIsInserting.ClientID +
                                                   ".setValue('true');SetGridPanelAndDirectMethod(" +
                                                   GridPanel1.ClientID + ",#{DirectMethods}," + Store1.ClientID + ");";

        }

        public void ShowData()
        {
            GridPanel = GridController.GetInstance().GetGridPanel(this.ID);
            if (GridPanel == null)
            {
                GridPanel = new GridPanelInfo(1, this.ID, "", "", 800, 200, "", 5, DefaultTable, true, false, false,
                    false, "", "", "", false, "", false);
                GridController.GetInstance().AddGridPanel(GridPanel);
            }

            if (ColumnList == null)
                ColumnList = GridController.GetInstance().GetColumnInfo(this.ID, TableName, 1);

            InitComponent(GridPanel, ColumnList);
            LoadField2Store();
        }

        public void LoadData(string where, string ForeignKeyName, string ForeignKeyValue)
        {
            try
            {
                Session["where"] = where;
                hdfForeignKeyName.Text = ForeignKeyName;
                hdfForeignKeyValue.Text = ForeignKeyValue;
                if (ColumnList == null)
                    ColumnList = GridController.GetInstance().GetColumnInfo(this.ID, TableName, 1);
                int fieldCount = ColumnList.Count();
                string sql = "select {0} from " + this.TableName;
                if (fieldCount != 0)
                {
                    if (string.IsNullOrEmpty(PrimaryKey))
                        PrimaryKey = Util.GetInstance().GetPrimaryKeyOfTable(this.TableName);
                    string _col = "";
                    bool gotPrimaryKey = false;
                    foreach (GridPanelColumnInfo col in ColumnList)
                    {
                        if (col.ColumnName == PrimaryKey)
                            gotPrimaryKey = true;
                        _col += ",[" + col.ColumnName + "]";
                    }

                    if (gotPrimaryKey == false)
                    {
                        _col = PrimaryKey + _col;
                    }
                    else
                    {
                        _col = _col.Remove(0, 1);
                    }

                    sql = string.Format(sql, _col);
                }
                else
                {
                    sql = string.Format(sql, "*");
                }

                if (!string.IsNullOrEmpty(where))
                    sql += " where " + where;
                DataTable table = DataHandler.GetInstance().ExecuteDataTable(sql);
                object[] objData = new object[table.Rows.Count];
                int i = 0;

                //Nếu không có ColumnList
                if (fieldCount == 0)
                {
                    fieldCount = table.Columns.Count;
                    foreach (DataRow row in table.Rows)
                    {
                        object[] item = new object[fieldCount];
                        for (int j = 0; j < fieldCount; j++)
                        {
                            item[j] = row[j];
                        }

                        objData[i] = item;
                        i++;
                    }
                }
                else //nếu có ColumnList
                {
                    foreach (DataRow row in table.Rows)
                    {
                        object[] item = new object[fieldCount];
                        for (int j = 0; j < fieldCount; j++)
                        {
                            item[j] = row[ColumnList[j].ColumnName];
                        }

                        objData[i] = item;
                        i++;
                    }
                }

                Store1.DataSource = objData;
                Store1.DataBind();
            }
            catch (Exception ex)
            {
                Dialog.ShowError("MiniGrid/LoadData = " + ex.Message);
            }
        }

        protected void Store1_refreshData(object sender, StoreRefreshDataEventArgs e)
        {
            if (Session["where"] != null)
                LoadData(Session["where"].ToString(), hdfForeignKeyName.Text, hdfForeignKeyValue.Text);
            else
                LoadData("", hdfForeignKeyName.Text, hdfForeignKeyValue.Text);
        }

        protected void Store1_OnSubmitData(object sender, StoreSubmitDataEventArgs e)
        {
            hdfJsonObjects.Text = e.Json;
        }

        public string GetJsonData()
        {
            return hdfJsonObjects.Text;
        }

        /// <summary>
        /// Load field vào store
        /// </summary>
        private void LoadField2Store()
        {
            if (ColumnList == null)
                ColumnList = GridController.GetInstance().GetColumnInfo(this.ID, TableName, 1);
            ArrayReader arrReader = new ArrayReader();
            arrReader.IDProperty = this.PrimaryKey; //Util.GetInstance().GetPrimaryKeyOfTable(this.TableName);
            if (ColumnList.FirstOrDefault() != null)
            {
                foreach (GridPanelColumnInfo columnInfo in ColumnList)
                {
                    RecordField field = new RecordField();
                    field.Name = columnInfo.ColumnName;
                    arrReader.Fields.Add(field);
                }
            }
            else
            {
                DataTable datatable = DataController.DataHandler.GetInstance()
                    .ExecuteDataTable("select top 1 * from " + TableName);
                foreach (DataColumn item in datatable.Columns)
                {
                    RecordField field = new RecordField();
                    field.Name = item.ColumnName;
                    arrReader.Fields.Add(field);
                }
            }

            Store1.Reader.Add(arrReader);
        }

        /// <summary>
        /// Load column vào GridPanel
        /// </summary>
        private void AddColumn()
        {
            if (ColumnList == null)
                ColumnList = GridController.GetInstance().GetColumnInfo(this.ID, TableName, 1);
            Datatable = DataController.DataHandler.GetInstance().ExecuteDataTable("select top 1 * from " + TableName);

            if (ColumnList.FirstOrDefault() == null)
            {
                foreach (DataColumn item in Datatable.Columns)
                {
                    Column column = new Column();
                    column.DataIndex = item.ColumnName;
                    column.Header = item.ColumnName;
                    GridPanel1.ColumnModel.Columns.Add(column);
                    if (item.DataType.ToString().Equals("System.DateTime"))
                    {
                        column.Renderer.Fn = "GetDateFormat";
                    }

                    if (column.Editor.Count == 0)
                        switch (GetColumnDataType(Datatable, item.ColumnName))
                        {
                            case "System.Decimal":
                            case "System.Int32":
                                Ext.Net.SpinnerField spin = new SpinnerField();
                                spin.ID = item.ColumnName + "spin";
                                column.Editor.Add(spin);
                                break;
                            case "System.DateTime":
                                Ext.Net.DateField df = new Ext.Net.DateField();
                                df.ID = item.ColumnName + "datefield";
                                column.Editor.Add(df);
                                break;
                            case "System.String":
                                Ext.Net.TextField text = new Ext.Net.TextField();
                                text.ID = item.ColumnName + "string";
                                column.Editor.Add(text);
                                break;
                            case "System.Boolean":
                                Ext.Net.Checkbox chk = new Checkbox();
                                chk.ID = item.ColumnName + "chk";
                                column.Editor.Add(chk);
                                break;
                        }
                }
            }
            else
            {
                string listener = "";
                bool hasComboBox = false;
                foreach (GridPanelColumnInfo columnInfo in ColumnList)
                {
                    Column column = new Column();
                    column.DataIndex = columnInfo.ColumnName;
                    column.Header = columnInfo.ColumnHeader;
                    if (columnInfo.Width.HasValue && columnInfo.Width != 0)
                        column.Width = columnInfo.Width.Value;
                    GridPanel1.ColumnModel.Columns.Add(column);
                    if (string.IsNullOrEmpty(columnInfo.RenderJS) == false)
                    {
                        column.Renderer.Fn = columnInfo.RenderJS;
                    }

                    if (columnInfo.AllowComboBoxOnGrid && string.IsNullOrEmpty(columnInfo.TableName) == false)
                    {
                        hasComboBox = true;
                        Ext.Net.ComboBox cbBox = new ComboBox();
                        cbBox.LoadingText = "Đang tải...";
                        cbBox.ID = "combo" + columnInfo.ColumnName;
                        cbBox.DisplayField = "displayField";
                        cbBox.ValueField = "valueField";
                        cbBox.EnableViewState = false;
                        column.Editor.Add(cbBox);
                        Store store = CreateStore(cbBox.ID);
                        cbBox.Store.Add(store);
                        StoreList.Add(new StoreDaTa(store, columnInfo.ComboBoxTable, columnInfo.DisplayFieldComboBox,
                            columnInfo.ValueFieldComboBox, columnInfo.WhereFilterComboBox, columnInfo.ColumnName,
                            columnInfo.MasterColumnComboID.Value, columnInfo.ID));

                        if (columnInfo.MasterColumnComboID.HasValue && columnInfo.MasterColumnComboID.Value != 0)
                        {
                            GridPanelColumnInfo col = ColumnList
                                .Where(p => p.ID == columnInfo.MasterColumnComboID.Value).FirstOrDefault();
                            if (col != null)
                            {
                                listener += string.Format(
                                    "case \"{0}\": this.getColumnModel().getCellEditor(e.column, e.row).field.allQuery = e.record.get('{1}');break;",
                                    columnInfo.ColumnName, col.ColumnName);
                            }
                        }
                    }

                    if (column.Editor.Count == 0)
                        switch (columnInfo.DataType)
                        {
                            case "System.Decimal":
                            case "System.Int32":
                                Ext.Net.SpinnerField spin = new SpinnerField();
                                spin.ID = columnInfo.ColumnName + "spin";
                                column.Editor.Add(spin);
                                break;
                            case "System.DateTime":
                                Ext.Net.DateField df = new Ext.Net.DateField();
                                df.ID = columnInfo.ColumnName + "datefield";
                                column.Editor.Add(df);
                                break;
                            case "System.String":
                                Ext.Net.TextField text = new Ext.Net.TextField();
                                text.ID = columnInfo.ColumnName + "string";
                                column.Editor.Add(text);
                                break;
                            case "System.Boolean":
                                Ext.Net.Checkbox chk = new Checkbox();
                                chk.ID = columnInfo.ColumnName + "chk";
                                column.Editor.Add(chk);
                                break;
                        }
                }

                if (!string.IsNullOrEmpty(listener))
                {
                    GridPanel1.Listeners.BeforeEdit.Handler = "switch (e.field) {" + listener + "}";
                }

                if (hasComboBox)
                {
                    GridPanel1.Listeners.AfterEdit.Handler = Store1.ClientID + ".commitChanges();";
                    GridPanel1.DirectEvents.AfterEdit.Event +=
                        new ComponentDirectEvent.DirectEventHandler(AfterEdit_Event);
                    GridPanel1.DirectEvents.AfterEdit.ExtraParams.Add(new Ext.Net.Parameter("id", "e.record.id",
                        ParameterMode.Raw));
                    GridPanel1.DirectEvents.AfterEdit.ExtraParams.Add(new Ext.Net.Parameter("field", "e.field",
                        ParameterMode.Raw));
                    GridPanel1.DirectEvents.AfterEdit.ExtraParams.Add(new Ext.Net.Parameter("record", "e.record.data",
                        ParameterMode.Raw, true));
                    GridPanel1.DirectEvents.AfterEdit.ExtraParams.Add(new Ext.Net.Parameter("value", "e.value",
                        ParameterMode.Raw));
                }
            }
        }

        /// <summary>
        /// Lấy kiểu dữ liệu của cột để từ đó generate ra editor
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        private string GetColumnDataType(DataTable table, string ColumnName)
        {
            foreach (DataColumn col in table.Columns)
            {
                if (col.ColumnName == ColumnName)
                    return col.DataType.ToString();
            }

            return null;
        }

        /// <summary>
        /// Sự kiện AfterEdit của GridPanel1, xảy ra khi người dùng thay đổi giá trị combobox trên Grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void AfterEdit_Event(object sender, DirectEventArgs e)
        {
            JsonObject data = JSON.Deserialize<JsonObject>(e.ExtraParams["record"]);
            foreach (StoreDaTa store in StoreList)
            {
                StoreDaTa _s = StoreList.Where(p => p.ColumnID == store.MasterColumnID).FirstOrDefault();
                if (_s == null)
                    continue;
                if (!string.IsNullOrEmpty(store.WhereFilter))
                    store.WhereFilter = string.Format(store.WhereFilter, "N'" + data[_s.ColumnName] + "'");
                else
                    store.WhereFilter = "1=1";
                string sql = string.Format("select top 1 {0} from {1} where {2}", store.DisplayField, store.TableName,
                    store.WhereFilter);
                DataTable Data = DataHandler.GetInstance().ExecuteDataTable(sql);
                if (Data.Rows.Count != 0)
                    this.Store1.UpdateRecordField(e.ExtraParams["id"], store.ColumnName,
                        Data.Rows[0][store.DisplayField].ToString());
            }

        }

        /// <summary>
        /// Tạo Store cho các ComboBox trên GridPanel
        /// </summary>
        /// <returns></returns>
        private Ext.Net.Store CreateStore(string StoreName)
        {
            Ext.Net.Store st = new Store();
            st.ID = "store" + StoreName;
            st.AutoLoad = false;
            st.RefreshData += new Store.AjaxRefreshDataEventHandler(cbBoxStoreRefresh);
            st.Proxy.Add(new PageProxy());
            JsonReader reader = new JsonReader();
            reader.IDProperty = "valueField";
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
                StoreDaTa storeData = StoreList.Where(p => p.Store.ID == ((Store) sender).ID).FirstOrDefault();
                string query = e.Parameters["query"];
                List<object> dataList = new List<object>();
                if (string.IsNullOrEmpty(storeData.WhereFilter))
                    storeData.WhereFilter = "1=1";
                else if (string.IsNullOrEmpty(query) == false
                ) //Note: Nếu muốn sử dụng query thì trong trường filter phải có đoạn text như FieldName like {0}
                {
                    storeData.WhereFilter = string.Format(storeData.WhereFilter, "N'" + query + "'");
                }

                string sql = string.Format("select {0},{1} from {2} where {3}", storeData.DisplayField,
                    storeData.ValueField, storeData.TableName, storeData.WhereFilter);
                DataTable Data = DataHandler.GetInstance().ExecuteDataTable(sql);

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

        private void InitComponent(GridPanelInfo panel, List<GridPanelColumnInfo> columnList)
        {
            if (panel.DisplayRowNumber)
            {
                RowNumbererColumn stt = new RowNumbererColumn();
                stt.Header = "STT";
                GridPanel1.ColumnModel.Columns.Add(stt);
            }

            if (panel.Header == false)
                GridPanel1.Title = "";
            else
                GridPanel1.Title = panel.Title;
            if (!string.IsNullOrEmpty(panel.AutoExpandColumn))
            {
                columnList = GridController.GetInstance().GetColumnInfo(this.ID, TableName, 1);
                if (columnList.Count() != 0)
                {
                    if (columnList.Where(p => p.ColumnName == panel.AutoExpandColumn).FirstOrDefault() != null)
                    {
                        GridPanel1.AutoExpandColumn = panel.AutoExpandColumn;
                    }
                }
                else
                {
                    Datatable = DataController.DataHandler.GetInstance()
                        .ExecuteDataTable("select top 1 * from " + TableName);
                    foreach (DataColumn item in Datatable.Columns)
                    {
                        if (item.ColumnName == panel.AutoExpandColumn)
                        {
                            GridPanel1.AutoExpandColumn = panel.AutoExpandColumn;
                            break;
                        }
                    }
                }
            }

            this.PrimaryKey = Util.GetInstance().GetPrimaryKeyOfTable(panel.TableName);
            // Store1.AutoLoadParams.Add(new Ext.Net.Parameter("limit", panel.PageSize.ToString()));

            //sHdfgridName.Value = this.ID.ToString();
            //sHdftable.Value = this.TableName;
            //sHdfPrimaryKey.Value = panel.PrimaryColumn;
            //sHdfwhere.Value = panel.WhereClause.Replace("where", "");

            //if (this.Height != 0)
            //    GridPanel1.Height = this.Height;
            //else
            //    GridPanel1.Height = panel.Height.Value;
            //if (this.Width != 0)
            //    GridPanel1.Width = this.Width;
            //else
            //    GridPanel1.Width = panel.Width.Value;

            GenerateListener();
            //if (panel.PageSize.HasValue)
            //    PagingToolbar1.PageSize = panel.PageSize.Value;
            //else
            //    PagingToolbar1.PageSize = 5;

            if (!string.IsNullOrEmpty(panel.Icon))
            {
                GridPanel1.IconCls = panel.Icon;
            }
            else
            {
                GridPanel1.Icon = Icon.Table;
            }

            //set local filter
            if (panel.AllowFilter)
            {
                GridFilters filter = new GridFilters();
                filter.Local = true;
                filter.FiltersText = "Lọc";
                GridPanel1.Plugins.Add(filter);
                Datatable = DataController.DataHandler.GetInstance()
                    .ExecuteDataTable("select top 1 * from " + TableName);
                foreach (DataColumn item in Datatable.Columns)
                {
                    switch (item.DataType.ToString())
                    {
                        case "System.Int32":
                            NumericFilter numberic = new NumericFilter();
                            numberic.DataIndex = item.ColumnName;
                            filter.Filters.Add(numberic);
                            break;
                        case "System.Boolean":
                            BooleanFilter boolean = new BooleanFilter();
                            boolean.DataIndex = item.ColumnName;
                            filter.Filters.Add(boolean);
                            break;
                        case "System.String":
                            StringFilter str = new StringFilter();
                            str.DataIndex = item.ColumnName;
                            filter.Filters.Add(str);
                            break;
                        case "System.DateTime":
                            DateFilter datefilter = new DateFilter();
                            datefilter.BeforeText = "Trước ngày";
                            datefilter.AfterText = "Sau ngày";
                            datefilter.OnText = "Vào ngày";
                            datefilter.DataIndex = item.ColumnName;
                            filter.Filters.Add(datefilter);
                            break;
                    }
                }
            }
        }

        [DirectMethod]
        public void AfterEdit(string field, string value)
        {
            //try
            //{
            //    if (value.Contains("\""))
            //        value = value.Replace("\"", "");
            //    if (string.IsNullOrEmpty(hdfIsInserting.Text))
            //    {
            //        if (string.IsNullOrEmpty(this.PrimaryKey))
            //            PrimaryKey = Util.GetInstance().GetPrimaryKeyOfTable(this.TableName);
            //        string sql = "update " + this.TableName + " set " + field + " = N'" + value + "' where " + PrimaryKey + " = N'" + hdfRecordId.Text + "'";
            //        DataHandler.GetInstance().ExecuteNonQuery(sql);
            //    }
            //    else//insert 
            //    {
            //        if (hdfSqlInsertCommand.Text.Contains(field + "="))
            //        {
            //            string[] couple = hdfSqlInsertCommand.Text.Split(';');
            //            for (int i = 0; i < couple.Length; i++)
            //            {
            //                if (string.IsNullOrEmpty(couple[i]) == false)
            //                {
            //                    string[] tmp = couple[i].Split('=');
            //                    if (tmp[0] == field)
            //                    {
            //                        couple[i] = field + "=" + value;
            //                        break;
            //                    }
            //                }
            //            }
            //            hdfSqlInsertCommand.Text = "";
            //            foreach (var item in couple)
            //            {
            //                hdfSqlInsertCommand.Text += item + ";";
            //            }
            //        }
            //        else
            //            hdfSqlInsertCommand.Text += field + "=" + value + ";";
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Dialog.ShowError("MiniGrid/AfterEdit = " + ex.Message);
            //}
        }

        [DirectMethod]
        public void SaveInsert()
        {
            if (!string.IsNullOrEmpty(hdfSqlInsertCommand.Text))
            {
                try
                {
                    string sql = "insert into " + this.TableName + "({0}) values({1})";
                    string[] couple = hdfSqlInsertCommand.Text.Split(';');
                    foreach (string item in couple)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            string[] element = item.Split('=');
                            if (hdfForeignKeyName.Text == element[0])
                            {
                                continue;
                            }

                            sql = string.Format(sql, "[" + element[0] + "],{0}", "N'" + element[1] + "',{1}");
                        }
                    }

                    if (string.IsNullOrEmpty(hdfForeignKeyName.Text) == false &&
                        string.IsNullOrEmpty(hdfForeignKeyValue.Text) == false)
                        sql = string.Format(sql, "[" + hdfForeignKeyName.Text + "]",
                            "N'" + hdfForeignKeyValue.Text + "'");
                    DataHandler.GetInstance().ExecuteNonQuery(sql.Replace(",{0}", "").Replace(",{1}", ""));
                    //  Dialog.ShowNotification(sql.Replace(",{0}", "").Replace(",{1}", ""));
                    hdfSqlInsertCommand.Text = "";
                    hdfIsInserting.Text = "";
                }
                catch (Exception ex)
                {
                    Dialog.ShowError("MiniGrid/SaveInsert() = " + ex.Message);
                }
            }
        }

        protected void btnDelete_Click(object sender, DirectEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.PrimaryKey))
                {
                    this.PrimaryKey = Util.GetInstance().GetPrimaryKeyOfTable(this.TableName);
                }

                string query = "delete from " + GridPanel.TableName + " where " + this.PrimaryKey + " = " +
                               hdfRecordId.Text;
                //  Dialog.ShowNotification(query);
                DataController.DataHandler.GetInstance().ExecuteNonQuery(query);
                hdfRecordId.Text = "";
                Store1_refreshData(null, null);
            }
            catch (Exception ex)
            {
                Dialog.ShowError("Base/Module/GridPanel " + ex.Message);
            }
        }

        [DirectMethod]
        public void DirectDelete()
        {
            btnDelete_Click(null, null);
        }

        [DirectMethod]
        public void SaveColumnOrder(string column)
        {
            try
            {
                string[] columnname = column.Split(',');
                int order = 0;
                foreach (var item in columnname)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        GridController.GetInstance().UpdateColumnOrder(this.ID, TableName, item, order);
                        order++;
                    }
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowError("Thông báo lỗi 3", ex.Message);
            }

            Dialog.ShowNotification("Cập nhật thứ tự cột thành công !");
        }

        [DirectMethod]
        public void SaveColumnWidth(string column)
        {
            try
            {
                string[] col = column.Split(',');
                foreach (var item in col)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        string[] tmp = item.Split('=');
                        GridController.GetInstance().UpdateColumnWidth(this.ID, TableName, tmp[0], int.Parse(tmp[1]));
                    }
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowError("Thông báo lỗi 4", ex.Message);
            }

            Dialog.ShowNotification("Cập nhật độ rộng của cột thành công !");
        }

        /// <summary>
        /// Gọi window config Table Information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void mnuTableInformationConfig_Click(object sender, DirectEventArgs e)
        {
            try
            {
                Control ct = plcConfig.FindControl(this.ID + "ConfigControl");
                ((Modules_Base_MiniGridPanel_Config_GridConfig) ct).ShowUpdateTableWindow();
            }
            catch (Exception ex)
            {
                Dialog.ShowError("MiniGrid/mnuTableInformationConfig_Click = " + ex.Message);
            }
        }

        /// <summary>
        /// Xuất hiện window cấu hình thông tin cột
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void mnuColumnConfig_Click(object sender, DirectEventArgs e)
        {
            try
            {
                Control ct = plcConfig.FindControl(this.ID + "ConfigControl");
                ((Modules_Base_MiniGridPanel_Config_GridConfig) ct).ShowColumnInformationUpdate();
            }
            catch (Exception ex)
            {
                Dialog.ShowError("MiniGrid/mnuTableInformationConfig_Click = " + ex.Message);
            }
        }

    }
}