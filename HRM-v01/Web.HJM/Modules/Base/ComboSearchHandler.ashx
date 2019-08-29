<%@ WebHandler Language="C#" Class="ComboSearchHandler" %>

using System;
using System.Web;

public class ComboSearchHandler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        int start = 0;
        int limit = 15;
        var tableName = context.Request["Table"];
        var valueField = context.Request["ValueField"];
        var displayField = context.Request["DisplayField"];
        var query = string.Empty;
        if (!string.IsNullOrEmpty(context.Request["start"]))
        {
            start = int.Parse(context.Request["start"]);
        }

        if (!string.IsNullOrEmpty(context.Request["limit"]))
        {
            limit = int.Parse(context.Request["limit"]);
        }
        if (!string.IsNullOrEmpty(context.Request["query"]))
        {
            query = SoftCore.Util.GetInstance().GetKeyword(context.Request["query"]);
        }
        System.Data.DataTable data = DataController.DataHandler.GetInstance().ExecuteDataTable("base_SearchCombobox", "@TableName", "@ValueField", "@DisplayField", "@SearchKey", "@Start", "@Limit",
                tableName, valueField, displayField, query, start, limit);
        int count = int.Parse(DataController.DataHandler.GetInstance().ExecuteScalar("base_CountSearchCombobox", "@TableName", "@ValueField", "@DisplayField", "@SearchKey",
                tableName, valueField, displayField, query).ToString());
        context.Response.Write(string.Format("{{total:{1},'plants':{0}}}", Ext.Net.JSON.Serialize(data), count));
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}