<%@ WebHandler Language="C#" Class="Web.HRM.Services.HandlerTrainingHistory" %>

using System;
using System.Globalization;
using System.Web;
using Web.Core;
using Web.Core.Framework;
using Web.Core.Service.Catalog;

namespace Web.HRM.Services
{
    public class HandlerTrainingHistory : Core.Framework.BaseControl.BaseHandler, IHttpHandler
    {
        // init variables      
        private string _keyword;
        private int _start;
        private int _limit;
        private string _order;
        private DateTime? _fromDate;
        private DateTime? _toDate;

        public void ProcessRequest(HttpContext context)
        {
            // init paging argument
            _start = Start;
            _limit = Limit;
            int? nationId = null;

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


            if (!string.IsNullOrEmpty(context.Request["nationId"]))
            {
                nationId = Convert.ToInt32(context.Request["nationId"]);
            }

            if (DateTime.TryParseExact(context.Request["fromDate"], "d/M/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out var parseFromDate))
            {
                _fromDate = parseFromDate;
            }
            if(DateTime.TryParseExact(context.Request["toDate"], "d/M/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out var parseToDate))
            {
                _toDate = parseToDate;
            }

            TrainingStatus? status = null;
            if (!string.IsNullOrEmpty(context.Request["status"]))
            {
                status = (TrainingStatus) Enum.Parse(typeof(TrainingStatus), context.Request["status"]);
            }

            var departmentIds = string.Empty;
            departmentIds = !string.IsNullOrEmpty(context.Request["departmentSelected"]) ? cat_DepartmentServices.GetAllDepartment(Convert.ToInt32(context.Request["departmentSelected"])) : context.Request["departments"];


            // select from db

            var pageResult = TrainingHistoryController.GetPaging(_keyword, departmentIds, null, nationId, status, _fromDate, _toDate, _order, _start, _limit);

            // response
            context.Response.ContentType = "text/json";
            context.Response.Write("{{TotalRecords:{0},Data:{1}}}".FormatWith(pageResult.Total, Ext.Net.JSON.Serialize(pageResult.Data)));
        }

        public bool IsReusable => false;
    }
}