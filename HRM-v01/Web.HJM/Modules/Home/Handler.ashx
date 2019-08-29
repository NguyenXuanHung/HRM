<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using Web.Core;
using Web.Core.Framework;

public class Handler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        var keyword = context.Request["keyword"];
        int start = 0;
        int limit = 0;
        int thamSoTragThai=1;
        // int ThamSoTragThai = 0;

        var maDonVi = context.Request["MaDonVi"];
        var arrOrgCode = string.IsNullOrEmpty(maDonVi)
       ? new string[] { }
       : maDonVi.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
        for (var i = 0; i < arrOrgCode.Length; i++)
        {
            arrOrgCode[i] = "'{0}'".FormatWith(arrOrgCode[i]);
        }

        if (!string.IsNullOrEmpty(context.Request["start"]))
        {
            start = int.Parse(context.Request["start"]);
        }
        if (!string.IsNullOrEmpty(context.Request["limit"]))
        {
            limit = int.Parse(context.Request["limit"]);
        }
        if (!string.IsNullOrEmpty(keyword))
        {
            keyword = "%" + SoftCore.Util.GetInstance().GetKeyword(keyword).Replace(' ', '%') + "%";
        }
        if (!string.IsNullOrEmpty(context.Request["StatusParam"]))
        {
            thamSoTragThai = int.Parse(context.Request["StatusParam"]);
        }
       
        var data = SQLHelper.ExecuteTable(
            SQLManagementAdapter.GetStore_GetRecords(string.Join(",", arrOrgCode), keyword, thamSoTragThai, start, limit));
        var count = SQLHelper.ExecuteScalar(
            SQLManagementAdapter.GetStore_CountGetRecords(string.Join(",", arrOrgCode), keyword, thamSoTragThai));
        var countRecords = int.Parse("0" + count);
        context.Response.Write(string.Format("{{TotalRecords:{1},'Data':{0}}}", Ext.Net.JSON.Serialize(data), countRecords));
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}