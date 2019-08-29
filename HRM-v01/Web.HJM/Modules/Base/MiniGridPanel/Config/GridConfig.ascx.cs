using System;
using System.Collections.Generic;
using System.Linq;
using Ext.Net;
using System.Data;
using WebUI.Controller;
using WebUI.Entity;
using SoftCore;
using DataController;
using Web.Core.Framework;

namespace Web.HJM.Modules.UserControl
{
    public partial class Modules_Base_MiniGridPanel_Config_GridConfig : BaseUserControl
    {
        /// <summary>
        /// Table của GridPanel cần config
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// Tên của GridPanel được lưu trong CSDl
        /// </summary>
        public string GridPanelName { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            cbValueColumn.TargetTableControl = cbTableComboBox;
            cbDisplayColumn.TargetTableControl = cbTableComboBox;
            if (!X.IsAjaxRequest)
            {
                LoadColumnTable();
                //select cac truong trong bang hien thoi
                DataTable datatable = DataController.DataHandler.GetInstance()
                    .ExecuteDataTable("select top 1 * from " + TableName);
                foreach (DataColumn column in datatable.Columns)
                {
                    Ext.Net.ListItem item = new Ext.Net.ListItem();
                    item.Text = column.ColumnName;
                    item.Value = column.ColumnName;
                    cbColumnField.Items.Add(item);
                }

                CheckboxSelectionModel1.Listeners.RowSelect.Handler =
                    "SaveColumnName(" + GridPanel2.ClientID + "," + txtSelectedColumnName.ClientID + ");";

                InitComponent();
                InitUpdateTableWindow();
            }
        }

        private static object lockObj = new object();

        /// <summary>
        /// autosave cho store chứa thông tin cột
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void HandleColumnChanges(object sender, BeforeStoreChangedEventArgs e)
        {
            ChangeRecords<GridPanelColumnInfo> columns = e.DataHandler.ObjectData<GridPanelColumnInfo>();
            foreach (GridPanelColumnInfo item in columns.Updated)
            {
                lock (lockObj)
                {
                    item.TableName = this.TableName;
                    GridController.GetInstance().UpdateColumnInformation(item);
                }

                if (StoreColumnInfo.UseIdConfirmation)
                {
                    e.ConfirmationList.ConfirmRecord(item.ID.ToString());
                }
            }

            foreach (GridPanelColumnInfo item in columns.Created)
            {
                lock (lockObj)
                {
                    item.TableName = this.TableName;
                    item.GridName = this.ID;
                    GridController.GetInstance().InsertColumnInformation(item);
                }

                if (StoreColumnInfo.UseIdConfirmation)
                {
                    e.ConfirmationList.ConfirmRecord(item.ID.ToString());
                }

                //  StoreColumnInfo.
            }

            foreach (GridPanelColumnInfo item in columns.Deleted)
            {
                lock (lockObj)
                {
                    GridController.GetInstance().DeleteColumnInformation(item);
                    LoadColumnTable();
                }
            }

            e.Cancel = true;
        }

        /// <summary>
        /// Thiết lập các thông tin cấu hình cho bảng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void InitUpdateTableWindow()
        {
            try
            {
                GridPanelInfo gridPanel = GridController.GetInstance().GetGridPanel(GridPanelName);
                if (gridPanel == null)
                    return;
                cbTable.Text = gridPanel.TableName;
                txtTitle.Text = gridPanel.Title;
                if (gridPanel.Width.HasValue)
                    txtwidth.Text = gridPanel.Width.Value.ToString();
                if (gridPanel.Height.HasValue)
                    txtheight.Text = gridPanel.Height.Value.ToString();
                txtIcon.Text = gridPanel.Icon;
                cbAutoExpandColumn.Text = gridPanel.AutoExpandColumn;
                chkHeader.Checked = gridPanel.Header;
                chkRowNumber.Checked = gridPanel.DisplayRowNumber;
                chkFilter.Checked = gridPanel.AllowFilter;
                chkCheckBox.Checked = gridPanel.RowCheckBox;
                //   wdUpdatePanelInfo.Show();
                cbPageSize.SelectedIndex = gridPanel.PageSize.Value / 5 - 1;
                txtWhereClause.Text = gridPanel.WhereClause;
                txtOrderBy.Text = gridPanel.OrderBy;
                int index = 0;
                cbInformationPanel.SelectedIndex = index;
                foreach (var item in cbInformationPanel.Items)
                {
                    if (item.Value == gridPanel.InformationPanel)
                    {
                        cbInformationPanel.SelectedIndex = index;
                        break;
                    }

                    index++;
                }

                index = 0;
                cbPageSize.SelectedIndex = 0;
                foreach (var item in cbPageSize.Items)
                {
                    if (item.Value == gridPanel.PageSize.ToString())
                    {
                        cbPageSize.SelectedIndex = index;
                        break;
                    }
                }

                //Load các trường dữ liệu của bảng hiện thời
                DataTable datatable = DataController.DataHandler.GetInstance()
                    .ExecuteDataTable("select top 1 * from " + TableName);
                foreach (DataColumn column in datatable.Columns)
                {
                    Ext.Net.ListItem item = new Ext.Net.ListItem();
                    item.Text = column.ColumnName;
                    item.Value = column.ColumnName;
                    cbColumnField.Items.Add(item);
                    //cbbColumnField.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowError("MiniGridConfig/ShowUpdatePanel = " + ex.Message);
            }
        }

        /// <summary>
        /// Hiện window cấu hình bảng
        /// </summary>
        public void ShowUpdateTableWindow()
        {
            wdUpdatePanelInfo.Show();
        }

        /// <summary>
        /// Hiện window cấu hình thông tin cột
        /// </summary>
        public void ShowColumnInformationUpdate()
        {
            wdColumnInfomation.Show();
        }

        public string GetUpdateGridPanelClientID()
        {
            return wdUpdatePanelInfo.ClientID;
        }

        /// <summary>
        /// GetID của window cấu hình thông tin cột
        /// </summary>
        /// <returns></returns>
        public string GetColumnInformationConfig()
        {
            return wdColumnInfomation.ClientID;
        }

        protected void btnConfigComboBox_Click(object sender, DirectEventArgs e)
        {
            GridPanelColumnInfo col = GridController.GetInstance()
                .GetColumnInfo(int.Parse(RowSelectionModel2.SelectedRow.RecordID));
            cbTableComboBox.SetValue(col.ComboBoxTable);
            cbDisplayColumn.SetValue(col.DisplayFieldComboBox);
            cbValueColumn.SetValue(col.ValueFieldComboBox);
            txtWhereFilter.Text = col.WhereFilterComboBox;
            if (col.MasterColumnComboID.Value != 0)
                hdfColumnID.Value = col.MasterColumnComboID;
            wdSetComboBoxOnGrid.Show();
        }

        /// <summary>
        /// Cập nhật thiết lập ComboBox trên GridPanel cho cột
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdateComboBox_Click(object sender, DirectEventArgs e)
        {
            try
            {
                GridPanelColumnInfo col = GridController.GetInstance()
                    .GetColumnInfo(int.Parse(RowSelectionModel2.SelectedRow.RecordID));
                col.ComboBoxTable = cbTableComboBox.GetValue().ToString();
                col.DisplayFieldComboBox = cbDisplayColumn.GetValue().ToString();
                col.ValueFieldComboBox = cbValueColumn.GetValue().ToString();
                col.WhereFilterComboBox = txtWhereFilter.Text;
                if (!string.IsNullOrEmpty(cbMasterColumnID.SelectedItem.Value))
                {
                    col.MasterColumnComboID = int.Parse(cbMasterColumnID.SelectedItem.Value);
                }

                col.AllowComboBoxOnGrid = true;
                GridController.GetInstance().UpdateColumnInformation(col);
                wdSetComboBoxOnGrid.Hide();
            }
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }


        protected void btnUpdateConfigGridPanel_Click(object sender, DirectEventArgs e)
        {
            GridPanelInfo gridPanel = GridController.GetInstance().GetGridPanel(GridPanelName);
            gridPanel.TableName = cbTable.SelectedItem.Value;
            gridPanel.Title = txtTitle.Text;
            gridPanel.Width = int.Parse(txtwidth.Text);
            gridPanel.Header = chkHeader.Checked;
            gridPanel.AllowFilter = chkFilter.Checked;
            gridPanel.RowCheckBox = chkCheckBox.Checked;
            gridPanel.DisplayRowNumber = chkRowNumber.Checked;
            gridPanel.PageSize = int.Parse(cbPageSize.SelectedItem.Value);
            if (cbAutoExpandColumn.SelectedIndex >= 0)
            {
                gridPanel.AutoExpandColumn = cbAutoExpandColumn.SelectedItem.Value;
            }

            gridPanel.InformationPanel = ""; // cbInformationPanel.SelectedItem.Value; 
            gridPanel.WhereClause = txtWhereClause.Text;
            gridPanel.OrderBy = txtOrderBy.Text.Trim();
            if (cbDataSource.SelectedItem.Value == "View")
            {
                gridPanel.ViewName = cbView.SelectedItem.Value;
            }
            else
            {
                gridPanel.ViewName = "";
            }

            gridPanel.Icon = txtIcon.Text;
            this.TableName = cbTable.SelectedItem.Value;
            GridController.GetInstance().Update(gridPanel);
            wdUpdatePanelInfo.Hide();
        }

        private void InitComponent()
        {
            //InitComponent
            StoreColumnInfo.BaseParams.Add(new Ext.Net.Parameter("Table", this.TableName));
            StoreColumnInfo.BaseParams.Add(new Ext.Net.Parameter("GridPanel", GridPanelName));

            //if (!string.IsNullOrEmpty(gridPanel.ViewName))
            //{
            //    cbDataSource.SelectedIndex = 1;
            //}
            //else
            //{
            //    cbDataSource.SelectedIndex = 0;
            //} 

            int index = 0;
            List<string> tableList = Util.GetInstance().GetTables();
            foreach (string item in tableList)
            {
                cbTable.Items.Add(new Ext.Net.ListItem(item, item));
                if (item == this.TableName)
                {
                    cbTable.SelectedIndex = index;
                }

                index++;
            }

            //select các view
            index = 0;
            List<string> viewList = Util.GetInstance().GetAllView();
            foreach (string item in viewList)
            {
                cbView.Items.Add(new Ext.Net.ListItem(item, item));
                //if (item == gridPanel.ViewName)
                //{
                //    cbView.SelectedIndex = index;
                //}
                index++;
            }

            //Init Listener
            btnUpdateColumnTable.Listeners.Click.Handler = mnuColumnData.ClientID + ".hide();";
            Button1.Listeners.Click.Handler = mnuColumnData.ClientID + ".hide();";
            Button2.Listeners.Click.Handler = wdSetComboBoxOnGrid.ClientID + ".hide();";
            cbDataSource.Listeners.Select.Handler = "if(" + cbDataSource.ClientID + ".getValue()=='Table')" +
                                                    "{" + cbView.ClientID + ".disable();" + cbTable.ClientID +
                                                    ".enable();}" +
                                                    "else" +
                                                    "{" +
                                                    cbView.ClientID + ".enable();" + cbTable.ClientID + ".disable();" +
                                                    "}ChangeBetweenTableAndView(" + cbAutoExpandColumn.ClientID + "," +
                                                    FieldStore.ClientID + ");";
            cbTable.Listeners.Select.Handler = "ChangeBetweenTableAndView(" + cbAutoExpandColumn.ClientID + "," +
                                               FieldStore.ClientID + ");";
            cbView.Listeners.Select.Handler = "ChangeBetweenTableAndView(" + cbAutoExpandColumn.ClientID + "," +
                                              FieldStore.ClientID + ");";
            Button4.Listeners.Click.Handler = wdUpdatePanelInfo.ClientID + ".hide();";
            wdUpdatePanelInfo.Listeners.Show.Handler = FieldStore.ClientID + ".reload();if(" + cbDataSource.ClientID +
                                                       ".getValue()=='Table'){" + cbView.ClientID +
                                                       ".disable();}else{" + cbTable.ClientID + ".disable();}";
            cbMasterColumnID.Listeners.Expand.Handler =
                cbMasterColumnID.ClientID + ".setValue(" + hdfColumnID.ClientID + ".getValue());";
            btnDeleteColumnInfo.Listeners.Click.Handler = "RemoveItemOnGrid(" + gdPanel.ClientID + "," +
                                                          StoreColumnInfo.ClientID + ");" +
                                                          btnDeleteColumnInfo.ClientID + ".disable();";
            RowSelectionModel2.Listeners.RowSelect.Handler = btnDeleteColumnInfo.ClientID + ".enable();";
            btnRefreshPage.Listeners.Click.Handler = wdColumnInfomation.ClientID + ".hide();location.reload();";
            Button3.Listeners.Click.Handler = wdColumnInfomation.ClientID + ".hide();";
            wdColumnInfomation.Listeners.BeforeShow.Handler = btnDeleteColumnInfo.ClientID + ".disable();";
        }

        protected void FieldRefresh(object sender, StoreRefreshDataEventArgs e)
        {
            List<object> data = new List<object>();
            List<string> ColumnList = new List<string>();
            try
            {
                if (cbDataSource.SelectedIndex == 0)
                    ColumnList = Util.GetInstance().GetColumnOfTable(cbTable.SelectedItem.Value);
                else
                    ColumnList = Util.GetInstance().GetColumnOfTable(cbView.SelectedItem.Value);
            }
            catch
            {
            }

            foreach (string item in ColumnList)
            {
                data.Add(new {Name = item});
            }

            this.FieldStore.DataSource = data;
            this.FieldStore.DataBind();
        }

        protected void btnUpdateColumnTable_Click(object sender, DirectEventArgs e)
        {
            try
            {
                DataTable table = DataHandler.GetInstance().ExecuteDataTable("select top 1 * from " + this.TableName);
                string[] columnName = txtSelectedColumnName.Text.Split(',');
                foreach (string item in columnName)
                {
                    if (string.IsNullOrEmpty(item) == false)
                    {
                        GridPanelColumnInfo columnInfo = new GridPanelColumnInfo(0, this.TableName, GridPanelName, item,
                            item, 100, 0, true, "", true, false, "", "", "", 0, "",
                            table.Columns[item].DataType.ToString(), true, false);
                        GridController.GetInstance().InsertColumnInformation(columnInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowNotification(ex.Message);
            }

            LoadColumnTable();
        }

        protected void LoadColumnTable()
        {
            try
            {
                DataTable datatable = DataController.DataHandler.GetInstance()
                    .ExecuteDataTable("select top 1 * from " + TableName);
                List<GridPanelColumnInfo> columnList =
                    GridController.GetInstance().GetColumnInfo(this.ID, TableName, -1);
                object[] cols = new object[datatable.Columns.Count - columnList.Count()];
                int c = 0;
                foreach (DataColumn item in datatable.Columns)
                {
                    if (columnList.Where(p => p.ColumnName == item.ColumnName).Count() == 0)
                    {
                        cols[c] = new object[] {item.ColumnName};
                        c++;
                    }
                }

                GridPanel2_Store.DataSource = cols;
                GridPanel2_Store.DataBind();
            }
            catch
            {
            }
        }
    }
}