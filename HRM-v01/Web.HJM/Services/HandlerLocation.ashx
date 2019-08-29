<%@ WebHandler Language="C#" Class="Web.HJM.Services.HandlerLocation" %>

using System;
using System.Web;
using Web.Core;
using Web.Core.Framework;

namespace Web.HJM.Services
{
    public class HandlerLocation : Core.Framework.BaseControl.BaseHandler, IHttpHandler
    {
        // init default value        
        private string _keyword = string.Empty;
        private int? _parentId = null;
        private string _group = string.Empty;
        private bool? _isDeleted = null;
        private string _order = string.Empty;

        public void ProcessRequest(HttpContext context)
        {
            // get params value
            if(!string.IsNullOrEmpty(context.Request["start"]))
            {
                Start = int.Parse(context.Request["start"]);
            }
            if(!string.IsNullOrEmpty(context.Request["limit"]))
            {
                Limit = int.Parse(context.Request["limit"]);
            }
            if(!string.IsNullOrEmpty(context.Request["query"]))
            {
                _keyword = context.Request["query"];
            }
            if(!string.IsNullOrEmpty(context.Request["parentId"]))
            {
                _parentId = Convert.ToInt32(context.Request["parentId"]);
            }
            if(!string.IsNullOrEmpty(context.Request["group"]))
            {
                _group = context.Request["group"];
            }
            if(!string.IsNullOrEmpty(context.Request["isDeleted"]))
            {
                _isDeleted = Convert.ToBoolean(context.Request["isDeleted"]);
            }
            if(!string.IsNullOrEmpty(context.Request["order"]))
            {
                _order = context.Request["order"];
            }

            var arrGroup = string.IsNullOrEmpty(_group)
                ? new string[] { }
                : _group.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            for(var i = 0; i < arrGroup.Length; i++)
            {
                arrGroup[i] = "'{0}'".FormatWith(arrGroup[i]);
            }

            // select from database
            var pageResult = CatalogLocationController.GetPaging(_keyword, _parentId, string.Join(",", arrGroup), _isDeleted, _order, Start, Limit);
            // response
            context.Response.ContentType = "text/json";
            context.Response.Write("{{TotalRecords:{0},Data:{1}}}".FormatWith(pageResult.Total, Ext.Net.JSON.Serialize(pageResult.Data)));
           
        }

        public bool IsReusable
        {
            get { return false; }
        }
    }
}