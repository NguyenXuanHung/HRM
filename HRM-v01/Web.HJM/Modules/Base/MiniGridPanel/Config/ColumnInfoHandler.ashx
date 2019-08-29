<%@ WebHandler Language="C#" Class="ColumnInfoHandler" %>

using System;
using System.Web;
using System.Linq;
public class ColumnInfoHandler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        var GridPanel = string.Empty;
        var Table = string.Empty;
        if (!string.IsNullOrEmpty(context.Request["GridPanel"]))
        {
            GridPanel = context.Request["GridPanel"];
        }

        if (!string.IsNullOrEmpty(context.Request["Table"]))
        {
            Table = context.Request["Table"];
        }
        System.Collections.Generic.List<WebUI.Entity.GridPanelColumnInfo> columnList = WebUI.Controller.GridController.GetInstance().GetColumnInfo(GridPanel, Table, -1).OrderByDescending(p => p.ID).ToList();
        context.Response.Write(string.Format("{{TotalRecords:{1},'Data':{0}}}", Ext.Net.JSON.Serialize(columnList), 0));
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}