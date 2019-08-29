<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using System.Collections.Generic;
using WebUI.Entity;
using WebUI.Controller;
using System.Linq;
public class Handler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        var SearchKey = string.Empty;
        if (!string.IsNullOrEmpty(context.Request["SearchKey"]))
        {
            SearchKey = context.Request["SearchKey"].ToString();
        }
        if (!string.IsNullOrEmpty(context.Request["FormName"]))
        {
            List<FormElementInfo> FieldList = FormController.GetInstance().GetForm(context.Request["FormName"]).GetFormElements(-1);
            if (!string.IsNullOrEmpty(SearchKey))
            {
                List<FormElementInfo> eList = FieldList.Where(t => t.ColumnName.Contains(SearchKey) || t.LabelName.Contains(SearchKey)).ToList();
                context.Response.Write(string.Format("{{TotalRecords:{1},'Data':{0}}}", Ext.Net.JSON.Serialize(eList), 0));
            }
            else
                context.Response.Write(string.Format("{{TotalRecords:{1},'Data':{0}}}", Ext.Net.JSON.Serialize(FieldList), 0));
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}