<%@ WebHandler Language="C#" Class="ComboHandler" %>

using System;
using System.Web;

public class ComboHandler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        var tableName = context.Request["Table"];
        var valueField = context.Request["ValueField"];
        var displayField = context.Request["DisplayField"];
        var OrderField = context.Request["OrderField"];
        var WhereField = context.Request["WhereField"];
        if (string.IsNullOrEmpty(WhereField))
        {
            WhereField = " 1 = 1";
        }
        var sql = string.Format("select [{0}],[{1}] from {2} where {3} ", displayField, valueField, tableName, WhereField);
        if (!string.IsNullOrEmpty(OrderField))
        {
            sql += OrderField;
        }
        System.Data.DataTable data = DataController.DataHandler.GetInstance().ExecuteDataTable(sql);
        int count = 0;
        if (data != null)
        {
            count = data.Rows.Count;
        }
        context.Response.Write(string.Format("{{TotalRecords:{1},'Data':{0}}}", Ext.Net.JSON.Serialize(data), count));
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}