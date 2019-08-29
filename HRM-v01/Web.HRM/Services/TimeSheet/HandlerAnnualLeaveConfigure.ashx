<%@ WebHandler Language="C#" Class="Web.HRM.Services.HandlerAnnualLeaveConfigure" %>

using System;
using System.Collections.Generic;
using System.Web;
using Web.Core;
using Web.Core.Framework;
using Web.Core.Service.Catalog;

namespace Web.HRM.Services
{
    public class HandlerAnnualLeaveConfigure : Core.Framework.BaseControl.BaseHandler, IHttpHandler
    {
        // init variables      
        private string _keyword;
        private int _start;
        private int _limit;

        private string _order;

        public void ProcessRequest(HttpContext context)
        {
            // init paging argument
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
            if(!string.IsNullOrEmpty(context.Request["SearchKey"]))
            {
                _keyword = context.Request["SearchKey"];
            }

            if(!string.IsNullOrEmpty(context.Request["order"]))
            {
                _order = context.Request["order"];
            }

            var departmentIds = string.Empty;
            if (!string.IsNullOrEmpty(context.Request["departmentSelected"]))
            {
                departmentIds =
                    cat_DepartmentServices.GetAllDepartment(Convert.ToInt32(context.Request["departmentSelected"]));
            }
            else
            {
                departmentIds = context.Request["departments"];
            }


            // select from db
            var pageResult = AnnualLeaveConfigureController.GetPaging(_keyword, null, departmentIds, null, false, _order, _start, _limit);

            // response
            context.Response.ContentType = "text/json";
            context.Response.Write("{{TotalRecords:{0},Data:{1}}}".FormatWith(pageResult.Total, Ext.Net.JSON.Serialize(pageResult.Data)));
        }

        public bool IsReusable => false;
    }
}