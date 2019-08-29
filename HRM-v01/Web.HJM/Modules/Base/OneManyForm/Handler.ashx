<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;

public class Handler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        System.Collections.Generic.List<WebUI.Entity.OneManyFormInfo> rsList = WebUI.Controller.OneManyFormController.GetInstance().GetAll("OneManyForm1", -1);
        context.Response.Write(string.Format("{{TotalRecords:{1},'Data':{0}}}", Ext.Net.JSON.Serialize(rsList), 0));
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}