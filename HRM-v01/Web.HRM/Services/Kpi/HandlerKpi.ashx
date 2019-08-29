<%@ WebHandler Language="C#" Class="Web.HRM.Services.HandlerKpi" %>

using System;
using System.Web;
using Web.Core;
using Web.Core.Framework;
using Web.Core.Service.Catalog;

namespace Web.HRM.Services
{
    public class HandlerKpi : Core.Framework.BaseControl.BaseHandler, IHttpHandler
    {
        /// <summary>
        /// Declare private variables
        /// </summary>
        private int _start;
        private int _limit;
        private string _handler;
        private string _keyWord;

        /// <inheritdoc />
        /// <summary>
        /// Process request
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            // init params
            _start = Start;
            _limit = Limit;

            // init params
            context.Response.ContentType = "text/plain";
            if(!string.IsNullOrEmpty(context.Request["handlers"]))
            {
                _handler = context.Request["handlers"];
            }

            // start
            if(!string.IsNullOrEmpty(context.Request["start"]))
            {
                _start = int.Parse(context.Request["start"]);
            }

            // limit
            if(!string.IsNullOrEmpty(context.Request["limit"]))
            {
                _limit = int.Parse(context.Request["limit"]);
            }

            if (!string.IsNullOrEmpty(context.Request["query"]))
            {
                _keyWord = context.Request["query"];
            }

            switch (_handler)
            {
                case "GroupKpi":
                    GroupKpi(context, _keyWord, _start, _limit);
                    break;
                case "Criterion":
                    Criterion(context, _keyWord, _start, _limit);
                    break;
                case "Argument":
                    Argument(context, _keyWord, _start, _limit);
                    break;
                case "EmployeeArgument":
                    EmployeeArgument(context, _keyWord, _start, _limit);
                    break;
                case "Evaluation":
                    Evaluation(context, _keyWord, _start, _limit);
                    break;
                case "CriterionGroup":
                    CriterionGroup(context, _keyWord, _start, _limit);
                    break;
                default:
                    break;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="keyWord"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        private void CriterionGroup(HttpContext context, string keyWord, int start, int limit)
        {
            var groupId = !string.IsNullOrEmpty(context.Request["group"])
                ? Convert.ToInt32(context.Request["group"])
                : 0;
            // select from db
            var pageResult = CriterionGroupController.GetPaging(keyWord, null, groupId, null, start, limit);

            // response
            context.Response.ContentType = "text/json";
            context.Response.Write("{{TotalRecords:{0},Data:{1}}}".FormatWith(pageResult.Total, Ext.Net.JSON.Serialize(pageResult.Data)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="keyWord"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        private void Evaluation(HttpContext context, string keyWord, int start, int limit)
        {
            // order, set default order by editedDate, CreatedDate
            var order = !string.IsNullOrEmpty(context.Request["order"]) ? context.Request["order"] : "[Year] DESC,[Month] DESC ";
            var month = !string.IsNullOrEmpty(context.Request["month"])
                ? Convert.ToInt32(context.Request["month"])
                : DateTime.Now.Month;
            var year = !string.IsNullOrEmpty(context.Request["year"])
                ? Convert.ToInt32(context.Request["year"])
                : DateTime.Now.Year;
            var groupId = !string.IsNullOrEmpty(context.Request["group"])
                ? Convert.ToInt32(context.Request["group"])
                : 0;
            var departmentIds = string.Empty;
            if (!string.IsNullOrEmpty(context.Request["departmentIds"]))
            {
                departmentIds =
                    cat_DepartmentServices.GetAllDepartment(Convert.ToInt32(context.Request["departmentIds"]));
            }
            else
            {
                departmentIds = context.Request["departments"];
            }

            // select from db
            var pageResult = EvaluationController.GetPaging(keyWord,departmentIds, null, groupId, month, year, order, start, limit);

            // response
            context.Response.ContentType = "text/json";
            context.Response.Write("{{TotalRecords:{0},Data:{1}}}".FormatWith(pageResult.Total, Ext.Net.JSON.Serialize(pageResult.Data)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="keyWord"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        private void EmployeeArgument(HttpContext context, string keyWord, int start, int limit)
        {
            // order, set default order by editedDate, CreatedDate
            var order = !string.IsNullOrEmpty(context.Request["order"]) ? context.Request["order"] : "[EditedDate] DESC,[CreatedDate] DESC, RecordId ";
            var month = !string.IsNullOrEmpty(context.Request["month"])
                ? Convert.ToInt32(context.Request["month"])
                : DateTime.Now.Month;
            var year = !string.IsNullOrEmpty(context.Request["year"])
            ? Convert.ToInt32(context.Request["year"])
            : DateTime.Now.Year;
            var departmentIds = string.Empty;
                
            if (!string.IsNullOrEmpty(context.Request["departmentIds"]))
            {
                departmentIds =
                    cat_DepartmentServices.GetAllDepartment(Convert.ToInt32(context.Request["departmentIds"]));
            }
            else
            {
                departmentIds = context.Request["departments"];
            }
            var groupId = !string.IsNullOrEmpty(context.Request["group"])
                ? Convert.ToInt32(context.Request["group"])
                : 0;
            // select from db
            var pageResult = EmployeeArgumentController.GetPaging(keyWord,departmentIds, groupId, null, month, year, order, start, limit);

            // response
            context.Response.ContentType = "text/json";
            context.Response.Write("{{TotalRecords:{0},Data:{1}}}".FormatWith(pageResult.Total, Ext.Net.JSON.Serialize(pageResult.Data)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="keyWord"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        private void Argument(HttpContext context, string keyWord, int start, int limit)
        {
            // order, set default order by editedDate, CreatedDate
            var order = !string.IsNullOrEmpty(context.Request["order"]) ? context.Request["order"] : "[EditedDate] DESC,[CreatedDate] DESC";

            // select from db
            var pageResult = ArgumentController.GetPaging(keyWord, false, null, null, order, start, limit);

            // response
            context.Response.ContentType = "text/json";
            context.Response.Write("{{TotalRecords:{0},Data:{1}}}".FormatWith(pageResult.Total, Ext.Net.JSON.Serialize(pageResult.Data)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="keyWord"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        private void Criterion(HttpContext context, string keyWord, int start, int limit)
        {
            // order, set default order by editedDate, CreatedDate
            var order = !string.IsNullOrEmpty(context.Request["order"]) ? context.Request["order"] : "[EditedDate] DESC,[CreatedDate] DESC";
            int? groupId = null;
            if (!string.IsNullOrEmpty(context.Request["group"]))
            {
                groupId = Convert.ToInt32(context.Request["group"]);
            }

            // select from db
            var pageResult = CriterionController.GetPaging(keyWord, groupId, false, null, null, order, start, limit);

            // response
            context.Response.ContentType = "text/json";
            context.Response.Write("{{TotalRecords:{0},Data:{1}}}".FormatWith(pageResult.Total, Ext.Net.JSON.Serialize(pageResult.Data)));
        }

        /// <summary>
        /// List group kpi
        /// </summary>
        /// <param name="context"></param>
        /// <param name="keyWord"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        private void GroupKpi(HttpContext context, string keyWord, int start, int limit)
        {
            // order, set default order by editedDate, CreatedDate
            var order = !string.IsNullOrEmpty(context.Request["order"]) ? context.Request["order"] : "[EditedDate] DESC,[CreatedDate] DESC";
            KpiStatus? status = null;
            if (!string.IsNullOrEmpty(context.Request["Status"]))
            {
                status = (KpiStatus) Enum.Parse(typeof(KpiStatus), context.Request["Status"]);
            }
            // select from db
            var pageResult = GroupKpiController.GetPaging(keyWord, false, status, order, start, limit);

            // response
            context.Response.ContentType = "text/json";
            context.Response.Write("{{TotalRecords:{0},Data:{1}}}".FormatWith(pageResult.Total, Ext.Net.JSON.Serialize(pageResult.Data)));
        }

        /// <inheritdoc />
        /// <summary>
        /// Reusable
        /// </summary>
        public bool IsReusable => false;
    }
}