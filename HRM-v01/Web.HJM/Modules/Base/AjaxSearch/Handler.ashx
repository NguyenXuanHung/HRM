<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;

public class Handler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/json";

        var start = 0;
        var limit = 10;
        var DisplayField = string.Empty;
        var SearchField = string.Empty;
        var ValueField = string.Empty;
        var query = string.Empty;
        var Table = string.Empty;
        if (!string.IsNullOrEmpty(context.Request["start"]))
        {
            start = int.Parse(context.Request["start"]);
        }

        if (!string.IsNullOrEmpty(context.Request["limit"]))
        {
            limit = int.Parse(context.Request["limit"]);
        }

        if (!string.IsNullOrEmpty(context.Request["DisplayField"]))
        {
            DisplayField = context.Request["DisplayField"];
        }

        if (!string.IsNullOrEmpty(context.Request["SearchField"]))
        {
            SearchField = context.Request["SearchField"];
        }

        if (!string.IsNullOrEmpty(context.Request["ValueField"]))
        {
            ValueField = context.Request["ValueField"];
        }

        if (!string.IsNullOrEmpty(context.Request["Table"]))
        {
            Table = context.Request["Table"];
        }

        if (!string.IsNullOrEmpty(context.Request["query"])) //this is key work
        {
            query = context.Request["query"];
        }
        string column = "" ;
        string[] cols = { DisplayField, ValueField, SearchField };
        System.Collections.ArrayList colList = new System.Collections.ArrayList();
        foreach (var item in cols)
        {
            if (colList.Contains(item) == false)
            {
                colList.Add(item);
                column += item+","; 
            }
        }
        column = column.Remove(column.LastIndexOf(','));
        int count = int.Parse(DataController.DataHandler.GetInstance().ExecuteScalar("select count(" + DisplayField + ") from " + Table + " where " + SearchField + " like N'" + SoftCore.Util.GetInstance().GetKeyword(query) + "%'").ToString());
        string sql = "select " + column +
                    " from (select " + column +
                    ",ROW_NUMBER() OVER(order by " + SearchField + ") row from " + Table + " where " + SearchField + " like N'" + SoftCore.Util.GetInstance().GetKeyword(query) + "%')a where (row between " +
                    start + " and " + (start + limit) + " )";
        System.Data.DataTable data = DataController.DataHandler.GetInstance().ExecuteDataTable(sql);
        context.Response.Write(string.Format("{{total:{1},'plants':{0}}}", Ext.Net.JSON.Serialize(data), 10));
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}