using System;
using System.Collections.Generic;
using System.Linq;
using Ext.Net;
using Web.Core;
using Web.Core.Framework;

namespace Web.HRM.Modules.Kpi
{
    public partial class TestManagement : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ExtNet.IsAjaxRequest)
            {
                var data = new List<object>();
                var employee = EmployeeArgumentController.GetAll(null, null, null, null, null, null, null);
                var listRecordIds = employee.Select(rc=> new { rc.RecordId, rc.FullName}).Distinct().ToList();
                var index = 0;
                foreach (var item in listRecordIds)
                {
                    index++;
                    data.Add(new { ID = "{0}".FormatWith(item.RecordId), Name = item.FullName });
                }

                this.Store1.DataSource = data;
                this.Store1.DataBind();

            }
        }

        protected void BeforeExpand(object sender, DirectEventArgs e)
        {
            var id = e.ExtraParams["id"];

            var store = new Store { ID = "StoreRow_" + id };

            var reader = new JsonReader {IDProperty = "ID"};
            reader.Fields.Add("ID", "FullName", "EmployeeCode");
            store.Reader.Add(reader);

            var data = new List<object>();
            var employee = EmployeeArgumentController.GetAll(null, Convert.ToInt32(id), null, null, null, null, null);
            var index = 0;
            foreach (var item in employee)
            {
                index++;
                data.Add(new { ID = "RecordId" + index,
                    FullName = item.FullName,
                    EmployeeCode = item.EmployeeCode,
                });
            }
            //for (var i = 1; i <= 10; i++)
            //{
            //    data.Add(new
            //    {
            //        ID = "P" + i,
            //        FullName = "Product " + i1234567890-
            //        EmployeeCode = ""
            //    });
            //}

            store.DataSource = data;

            this.RemoveFromCache(store.ID);
            store.Render();
            this.AddToCache(store.ID);

            var list = new ListView
            {
                ID = "ListViewRow_" + id,
                MultiSelect = true,
                StoreID = "{raw}StoreRow_" + id,
                Height = 200
            };
            foreach (var item in employee)
            {
                list.Columns.AddRange(new ListViewColumn[]
                {
                    new ListViewColumn
                    {
                        Header = "Mã NV",
                        DataIndex = "{0}".FormatWith(item.EmployeeCode),
                        Width = 100,
                        Align = TextAlign.Left
                    }
                });
            }

            this.RemoveFromCache(list.ID);
            list.Render("row-" + id, RenderMode.RenderTo);
            this.AddToCache(list.ID);
        }

        private void AddToCache(string id)
        {
            this.ResourceManager1.AddScript("addToCache({0});", JSON.Serialize(id));
        }

        private void RemoveFromCache(string id)
        {
            this.ResourceManager1.AddScript("removeFromCache({0});", JSON.Serialize(id));
        }
    }
}