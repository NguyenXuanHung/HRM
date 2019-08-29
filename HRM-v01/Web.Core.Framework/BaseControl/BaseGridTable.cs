using System.Collections.Generic;
using System.Data;
using Ext.Net;
using WebUI.Entity;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for GridTable
    /// </summary>
    public class BaseGridTable : BaseUserControl
    {
        protected List<StoreDaTa> StoreList = new List<StoreDaTa>();
        protected DataTable Datatable = new DataTable();
        protected string DefaultTable = "NoTable";
        protected GridPanelInfo GridPanel;
        protected List<GridPanelColumnInfo> ColumnList;

        public bool IsPrimaryKeyAutoIncrement { get; set; }

        public string PrimaryKey { get; set; }

        public string OutSideQuery { get; set; }

        public string TableName { get; set; }

        public string ViewName { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

        public string XmlConfigUrl { get; set; }

        public string RowExpanderTemplateUrl { get; set; }

        public bool DisableEditWindow { get; set; }

        public bool DisableAutoLoad { get; set; }

        public AccessHistory AccessHistory { get; set; }

        protected class StoreDaTa
        {
            public string TableName { get; set; }

            public string DisplayField { get; set; }

            public string ValueField { get; set; }

            public string WhereFilter { get; set; }

            public Store Store { get; set; }

            public string ColumnName { get; set; }

            public int MasterColumnID { get; set; }

            public int ColumnID { get; set; }

            public StoreDaTa(Store store, string tableName, string displayField, string valueField, string whereFilter, string columnName, int masterColumnID, int columnID)
            {
                this.Store = store;
                this.TableName = tableName;
                this.DisplayField = displayField;
                this.ValueField = valueField;
                this.WhereFilter = whereFilter;
                this.ColumnName = columnName;
                this.MasterColumnID = masterColumnID;
                this.ColumnID = columnID;
            }
        }
    }
}

