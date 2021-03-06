﻿<%@ WebHandler Language="C#" Class="Web.HRM.Services.HandlerCatalogQuantum" %>

using System;
using System.Web;
using Web.Core;
using Web.Core.Framework;

namespace Web.HRM.Services
{
    public class HandlerCatalogQuantum : Core.Framework.BaseControl.BaseHandler, IHttpHandler
    {
        // init default value        
        private string _keyword;
        private int? _groupQuantumId;
        private string _group;
        private CatalogStatus? _status;
        private string _order;
        private int _start;
        private int _limit;

        public void ProcessRequest(HttpContext context)
        {
            // set default paging
            _start = Start;
            _limit = Limit;

            // get params value
            if(!string.IsNullOrEmpty(context.Request["start"]))
            {
                _start = int.Parse(context.Request["start"]);
            }
            if(!string.IsNullOrEmpty(context.Request["limit"]))
            {
                _limit = int.Parse(context.Request["limit"]);
            }
            if(!string.IsNullOrEmpty(context.Request["query"]))
            {
                _keyword = context.Request["query"];
            }
            if(!string.IsNullOrEmpty(context.Request["groupQuantumId"]))
            {
                _groupQuantumId = int.Parse(context.Request["groupQuantumId"]);
            }
            if(!string.IsNullOrEmpty(context.Request["group"]))
            {
                _group = context.Request["group"];
            }
            if(!string.IsNullOrEmpty(context.Request["status"]))
            {
                _status = (CatalogStatus) Convert.ToInt32(context.Request["status"]);
            }
            if(!string.IsNullOrEmpty(context.Request["order"]))
            {
                _order = context.Request["order"];
            }
            // init page result
            var pageResult = CatalogQuantumController.GetPaging(_keyword, _groupQuantumId, _group, _status, false, _order, _start, _limit);
            // response
            context.Response.ContentType = "text/json";
            context.Response.Write("{{TotalRecords:{0},Data:{1}}}".FormatWith(pageResult.Total, Ext.Net.JSON.Serialize(pageResult.Data)));
        }

        public bool IsReusable => false;
    }
}