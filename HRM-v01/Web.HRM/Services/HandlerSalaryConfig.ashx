<%@ WebHandler Language="C#" Class="Web.HRM.Services.HandlerSalaryConfig" %>

using System;
using System.Web;
using Web.Core;
using Web.Core.Framework.Controller;

namespace Web.HRM.Services
{
    public class HandlerSalaryConfig : Core.Framework.BaseControl.BaseHandler, IHttpHandler
    {        
        private int _year = 0;
        private int _month = 0;
        private string _departments = string.Empty;
        private string _searchKey = string.Empty;
        private int _salaryBoardId = 0;
        private int _count = 0;

        public void ProcessRequest(HttpContext context)
        {

            if(!string.IsNullOrEmpty(context.Request["start"]))
            {
                Start = int.Parse(context.Request["start"]);
            }
            if(!string.IsNullOrEmpty(context.Request["limit"]))
            {
                Limit = int.Parse(context.Request["limit"]);
            }
            if(!string.IsNullOrEmpty(context.Request["searchKey"]))
            {
                _searchKey = "%" + SoftCore.Util.GetInstance().GetKeyword(context.Request["searchKey"]) + "%";
            }
            if(!string.IsNullOrEmpty(context.Request["departments"]))
            {
                _departments = context.Request["departments"];
            }

            if(!string.IsNullOrEmpty(context.Request["year"]))
            {
                _year = int.Parse(context.Request["year"]);
            }
            if(!string.IsNullOrEmpty(context.Request["month"]))
            {
                _month = int.Parse(context.Request["month"]);
            }

            if(!string.IsNullOrEmpty(context.Request["salaryBoardId"]))
                _salaryBoardId = int.Parse(context.Request["salaryBoardId"]);

            var arrOrgCode = string.IsNullOrEmpty(_departments) ? new string[] { }
                : _departments.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            for(var i = 0; i < arrOrgCode.Length; i++)
            {
                arrOrgCode[i] = "'{0}'".FormatWith(arrOrgCode[i]);
            }
            var pageResult = SalaryBoardConfigController.GetListSalaryBoardConfig(_searchKey, _salaryBoardId, null, Start, Limit);
            
            context.Response.Write("{{TotalRecords:{0},'Data':{1}}}".FormatWith(pageResult.Total, Ext.Net.JSON.Serialize(pageResult.Data)));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

    }
}
