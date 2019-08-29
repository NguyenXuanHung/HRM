<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;

public class Handler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        string ValueField = string.Empty;
        string DisplayField = string.Empty;
        string TableName = string.Empty;
        string Where = string.Empty;
        int Start = 0;
        int Limit = 10;
        string Order = string.Empty;
        //if (!string.IsNullOrEmpty(context.Request["Start"]))
        //{
        //    Start = int.Parse(context.Request["Start"]);
        //}

        //if (!string.IsNullOrEmpty(context.Request["Limit"]))
        //{
        //    Limit = int.Parse(context.Request["Limit"]);
        //}

        if (!string.IsNullOrEmpty(context.Request["ValueField"]))
        {
            ValueField = "[" + context.Request["ValueField"] + "]";
        }

        if (!string.IsNullOrEmpty(context.Request["DisplayField"]))
        {
            DisplayField = "[" + context.Request["DisplayField"] + "]";
        }

        if (!string.IsNullOrEmpty(context.Request["TableName"]))
        {
            TableName = context.Request["TableName"];
        }

        if (!string.IsNullOrEmpty(context.Request["Where"]))
        {
            Where = context.Request["Where"];
        }
        else
        {
            Where = "1=1";
        }

        Order = DisplayField;
        string sql = "";
        if (ValueField != DisplayField)
            sql = string.Format("select {0} from {1} where {2}", ValueField + "," + DisplayField, TableName, Where);
        //sql = string.Format("select " + ValueField + "," + DisplayField + " from " +
        //                                      "(select " + ValueField + "," + DisplayField + ", ROW_NUMBER() OVER(order by " + Order + " desc) row from " + TableName + ")a " +
        //                                      "where row between " + Start + " and " + Limit + " and " + Where);
        else
            sql = string.Format("select {0} from {1} where {2}", ValueField, TableName, Where);
        //sql = string.Format("select " + ValueField + " from " +
        //                                      "(select " + ValueField + ", ROW_NUMBER() OVER(order by " + Order + " desc) row from " + TableName + ")a " +
        //                                      "where row between " + Start + " and " + Limit + " and " + Where);

        System.Data.DataTable table = DataController.DataHandler.GetInstance().ExecuteDataTable(sql);

        context.Response.Write(string.Format("{{total:{1},'plants':{0}}}", Ext.Net.JSON.Serialize(table), table.Rows.Count)); //table.Rows.Count
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}