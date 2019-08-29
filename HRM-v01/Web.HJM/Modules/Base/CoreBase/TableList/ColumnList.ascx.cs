using System;
using System.Collections.Generic;
using Ext.Net;
using SoftCore;
using Web.Core.Framework;

namespace Web.HJM.Modules.UserControl
{
    public partial class Modules_Base_CoreBase_TableList_ColumnList : BaseUserControl, IBaseControl
    {
        public IBaseControl TargetTableControl { get; set; } //Control chứa bảng
        public string FieldLabel { get; set; }
        public int Width { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                cbColumn.FieldLabel = this.FieldLabel;
                cbColumn.Width = Width != 0 ? Width : 350;
            }
        }

        protected void CitiesRefresh(object sender, StoreRefreshDataEventArgs e)
        {

            string table = "";
            try
            {
                table = TargetTableControl.GetValue().ToString();
            }
            catch
            {
            }

            if (string.IsNullOrEmpty(table))
            {
                //  Dialog.ShowError("Bạn chưa xác định bảng");
                return;
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

        public string GetID()
        {
            return this.ID;
        }

        public object GetValue()
        {
            return cbColumn.SelectedItem.Value;
        }

        public void SetValue(object value)
        {
            hdfColumn.Value = value.ToString();
            //int index = 0;
            //foreach (Ext.Net.ListItem item in cbColumn.Items)
            //{
            //    if (item.Value == value.ToString())
            //    {
            //        cbColumn.SelectedIndex = index;
            //        break;
            //    }
            //    index++;
            //}
        }
    }
}