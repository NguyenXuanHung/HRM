using System;
using System.Collections.Generic;
using System.Linq;
using SoftCore;
using Ext.Net;
using Web.Core.Framework;

namespace Web.HJM.Modules.UserControl
{
    public partial class Modules_Base_CoreBase_DropDownList_DropDownList : BaseUserControl, IBaseControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Width != 0)
            {
                cbTable.Width = Width;
            }
            else
            {
                cbTable.Width = 350;
            }

            if (!string.IsNullOrEmpty(this.FieldLabel))
            {
                cbTable.FieldLabel = this.FieldLabel;
            }
            else
            {
                cbTable.FieldLabel = "Tên bảng";
            }

            if (!X.IsAjaxRequest)
            {
                if (!string.IsNullOrEmpty(this.FieldLabel))
                {
                    cbTable.FieldLabel = FieldLabel;
                }

                if (this.Width != 0)
                {
                    cbTable.Width = this.Width;
                }
            }

            //List<string> tableList = Util.GetInstance().GetTables();
            //int index = 0;
            //cbTable.SelectedIndex = 0; 
            //foreach (string item in tableList)
            //{
            //    Ext.Net.ListItem listItem = new Ext.Net.ListItem(item, item);
            //    cbTable.Items.Add(listItem); 
            //    index++;
            //}
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

        public string FieldLabel { get; set; }
        public int Width { get; set; }

        public string GetID()
        {
            return this.ID;
        }

        public object GetValue()
        {
            return cbTable.SelectedItem.Value;
        }

        public void SetValue(object value)
        {
            if (value != null)
                hdfSelectedTable.Value = value.ToString();
        }
    }

}