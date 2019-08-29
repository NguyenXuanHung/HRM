using System;
using System.Collections.Generic;
using System.Linq;
using Ext.Net;
using System.Data;
using SoftCore;
using WebUI.Entity;
using WebUI.Controller;
using DataController;
using Web.Core.Framework;

namespace Web.HJM.Modules.UserControl
{
    public partial class Modules_Base_GridPanel_Config_GridConfig : global::System.Web.UI.UserControl
    {
        public event EventHandler AfterClickUpdateInformation;
        public event EventHandler AfterCreateNewColumn;
        private GridPanelInfo gridPanel;

        /// <summary>
        /// Table của GridPanel cần config
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// Tên của GridPanel được lưu trong CSDl
        /// </summary>
        public string GridPanelName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ViewName { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            cbValueColumn.TargetTableControl = cbTableComboBox;
            cbDisplayColumn.TargetTableControl = cbTableComboBox;
            if (!ExtNet.IsAjaxRequest)
            {
                LoadColumnTable();
                //select cac truong trong bang hien thoi
                var datatable = DataHandler.GetInstance().ExecuteDataTable("select top 1 * from " + TableName);
                foreach (DataColumn column in datatable.Columns)
                {
                    var item = new ListItem {Text = column.ColumnName, Value = column.ColumnName};
                    cbColumnField.Items.Add(item);
                }
            }

            if (!ExtNet.IsAjaxRequest)
            {
                InitComponent();
                btnReloadData.Listeners.Click.Handler = FieldStore.ClientID + ".reload();";
            }
        }

        private void InitComponent()
        {
            StoreColumnInfo.BaseParams.Add(new Parameter("Table", TableName));
            StoreColumnInfo.BaseParams.Add(new Parameter("GridPanel", GridPanelName));

            if (!string.IsNullOrEmpty(ViewName))
            {
                cbDataSource.SelectedIndex = 1;
            }
            else
            {
                cbDataSource.SelectedIndex = 0;
            }

            //select các bảng trong CSDL
            int index = 0;
            List<string> tableList = Util.GetInstance().GetTables();
            foreach (string item in tableList)
            {
                cbTable.Items.Add(new ListItem(item, item));
                if (item == TableName)
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
                cbView.Items.Add(new ListItem(item, item));
                if (item == ViewName)
                {
                    cbView.SelectedIndex = index;
                }

                index++;
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
            var columns = e.DataHandler.ObjectData<GridPanelColumnInfo>();
            foreach (var item in columns.Updated)
            {
                lock (lockObj)
                {
                    item.TableName = TableName;
                    GridController.GetInstance().UpdateColumnInformation(item);
                }

                if (StoreColumnInfo.UseIdConfirmation)
                {
                    e.ConfirmationList.ConfirmRecord(item.ID.ToString());
                }
            }

            foreach (var item in columns.Created)
            {
                lock (lockObj)
                {
                    item.TableName = TableName;
                    item.GridName = ID;
                    GridController.GetInstance().InsertColumnInformation(item);
                }

                if (StoreColumnInfo.UseIdConfirmation)
                {
                    e.ConfirmationList.ConfirmRecord(item.ID.ToString());
                }
            }

            foreach (var item in columns.Deleted)
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
        /// 
        /// </summary>
        public void ShowGridPanelInformationConfig()
        {
            if (gridPanel == null)
                gridPanel = GridController.GetInstance().GetGridPanel(GridPanelName);
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
            chkAllowEditOnGrid.Checked = gridPanel.AllowEditOnGrid;
            chkOneManyForm.Checked = gridPanel.OneManyForm;
            wdUpdatePanelInfo.Show();
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

        /// <summary>
        /// 
        /// </summary>
        public void ShowColumnInformationConfig()
        {
            wdColumnInfomation.Show();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnConfigComboBox_Click(object sender, DirectEventArgs e)
        {
            if (RowSelectionModel2.SelectedRow != null)
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
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdateComboBox_Click(object sender, DirectEventArgs e)
        {
            try
            {
                if (RowSelectionModel2.SelectedRow != null)
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
            }
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdateConfigGridPanel_Click(object sender, DirectEventArgs e)
        {
            if (gridPanel == null)
                gridPanel = GridController.GetInstance().GetGridPanel(GridPanelName);
            gridPanel.TableName = cbTable.SelectedItem.Value;
            gridPanel.Title = txtTitle.Text;
            gridPanel.Width = int.Parse(txtwidth.Text);
            gridPanel.Header = chkHeader.Checked;
            gridPanel.AllowFilter = chkFilter.Checked;
            gridPanel.RowCheckBox = chkCheckBox.Checked;
            gridPanel.AllowEditOnGrid = chkAllowEditOnGrid.Checked;
            gridPanel.OneManyForm = chkOneManyForm.Checked;
            gridPanel.DisplayRowNumber = chkRowNumber.Checked;
            gridPanel.PageSize = int.Parse(cbPageSize.SelectedItem.Value);
            if (cbAutoExpandColumn.SelectedIndex >= 0)
            {
                gridPanel.AutoExpandColumn = cbAutoExpandColumn.SelectedItem.Value;
            }

            gridPanel.InformationPanel = cbInformationPanel.SelectedItem.Value;
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
            if (AfterClickUpdateInformation != null)
            {
                AfterClickUpdateInformation(null, null);
            }

            //  Page.ClientScript.RegisterStartupScript(Page.GetType(),"ReloadPageAgain", "location.reload();");
        }

        protected void FieldRefresh(object sender, StoreRefreshDataEventArgs e)
        {
            var data = new List<object>();
            var columnList = new List<string>();
            try
            {
                if (cbDataSource.SelectedIndex == 0)
                    columnList = Util.GetInstance().GetColumnOfTable(cbTable.SelectedItem.Value);
                else
                    columnList = Util.GetInstance().GetColumnOfTable(cbView.SelectedItem.Value);
            }
            catch
            {
            }

            foreach (string item in columnList)
            {
                data.Add(new {Name = item});
            }

            FieldStore.DataSource = data;
            FieldStore.DataBind();
        }

        protected void FilterStore_Refresh(object sender, StoreRefreshDataEventArgs e)
        {
            if (gridPanel == null)
            {
                gridPanel = GridController.GetInstance().GetGridPanel(GridPanelName);
            }

            List<GridFilterPermissionInfo> rsList = gridPanel.GetAllFilter(-1);
            FilterStore.DataSource = rsList;
            FilterStore.DataBind();
        }

        protected void LoadColumnTable()
        {
            try
            {
                DataTable datatable = DataController.DataHandler.GetInstance()
                    .ExecuteDataTable("select top 1 * from " + TableName);
                List<GridPanelColumnInfo> columnList =
                    GridController.GetInstance().GetColumnInfo(GridPanelName, TableName, -1);
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
                        GridPanelColumnInfo columnInfo = new GridPanelColumnInfo(0, this.TableName, this.GridPanelName,
                            item, item, 100, 0, true, "", true, false, "", "", "", 0, "",
                            table.Columns[item].DataType.ToString(), true, false);
                        GridController.GetInstance().InsertColumnInformation(columnInfo);
                    }
                }

                LoadColumnTable();
                if (AfterCreateNewColumn != null)
                {
                    AfterCreateNewColumn(this, null);
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowNotification(ex.Message);
            }
        }
    }

}