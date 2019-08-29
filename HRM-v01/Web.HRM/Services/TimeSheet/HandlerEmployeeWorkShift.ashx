<%@ WebHandler Language="C#" Class="Web.HRM.Services.HandlerEmployeeWorkShift" %>

using System;
using System.Web;
using Web.Core;
using Web.Core.Framework.Controller.TimeSheet;
using Web.Core.Service.Catalog;

namespace Web.HRM.Services
{
    public class HandlerEmployeeWorkShift : Core.Framework.BaseControl.BaseHandler, IHttpHandler
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

            _order = !string.IsNullOrEmpty(context.Request["order"]) ? context.Request["order"] : " [EditedDate] DESC, [CreatedDate] DESC ";

            var departmentIds = string.Empty;
            departmentIds = !string.IsNullOrEmpty(context.Request["departmentSelected"]) ? cat_DepartmentServices.GetAllDepartment(Convert.ToInt32(context.Request["departmentSelected"])) : context.Request["departments"];


            // select from db
            int? groupWorkShiftId = null;
            if(!string.IsNullOrEmpty(context.Request["groupWorkShiftId"]))
            {
                groupWorkShiftId = int.Parse(context.Request["groupWorkShiftId"]);
            }
           
            var pageResult = TimeSheetEmployeeGroupWorkShiftController.GetPaging(_keyword, departmentIds, null, groupWorkShiftId, _order, _start, _limit);

            // response
            context.Response.ContentType = "text/json";
            context.Response.Write("{{TotalRecords:{0},Data:{1}}}".FormatWith(pageResult.Total, Ext.Net.JSON.Serialize(pageResult.Data)));
        }

        public bool IsReusable => false;
    }
}