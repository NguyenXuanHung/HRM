 <%@ WebHandler Language="C#" Class="Web.HRM.Services.BaseHandler" %>

using Ext.Net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web;
using Web.Core;
using Web.Core.Framework;
using Web.Core.Framework.Adapter;
using Web.Core.Framework.Common;
using Web.Core.Framework.Controller;
using Web.Core.Framework.Controller.TimeSheet;
using Web.Core.Framework.SQLAdapter;
using Web.Core.Service.Catalog;
using Web.Core.Service.Security;

namespace Web.HRM.Services
{
    public class BaseHandler : Core.Framework.BaseControl.BaseHandler, IHttpHandler
    {
        private string _searchKey = "";
        private string _handler;
        private string _departments;
        private string _departmentSelected;
        private string _businessType = string.Empty;
        private int _timeSheetId = 0;

        public void ProcessRequest(HttpContext context)
        {
            // init params
            context.Response.ContentType = "text/plain";
            if(!string.IsNullOrEmpty(context.Request["handlers"]))
            {
                _handler = context.Request["handlers"];
            }
            if(!string.IsNullOrEmpty(context.Request["SearchKey"]))
            {
                _searchKey = context.Request["SearchKey"].GetKeyword();
            }
            if(!string.IsNullOrEmpty(context.Request["start"]))
            {
                Start = int.Parse(context.Request["start"]);
            }
            if(!string.IsNullOrEmpty(context.Request["limit"]))
            {
                Limit = int.Parse(context.Request["limit"]);
            }
            if(!string.IsNullOrEmpty(context.Request["departments"]))
            {
                _departments = context.Request["departments"];
            }
            if(!string.IsNullOrEmpty(context.Request["departmentSelected"]))
            {
                _departmentSelected = context.Request["departmentSelected"];
            }
            if(!string.IsNullOrEmpty(context.Request["businessType"]))
            {
                _businessType = context.Request["businessType"];
            }
            if(!string.IsNullOrEmpty(context.Request["timeSheetId"]))
            {
                _timeSheetId = Convert.ToInt32(context.Request["timeSheetId"]);
            }

            var arrOrgCode = string.IsNullOrEmpty(_departments)
                ? new string[] { }
                : _departments.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            for(var i = 0; i < arrOrgCode.Length; i++)
            {
                arrOrgCode[i] = "'{0}'".FormatWith(arrOrgCode[i]);
            }

            switch(_handler)
            {
                case "Home":
                    Home(context, arrOrgCode, Start, Limit, _searchKey);
                    break;
                case "BirthdayOfEmployee":
                    BirthdayOfEmployee(context, arrOrgCode, Start, Limit);
                    break;
                case "EndContract":
                    EndContract(context, arrOrgCode, Start, Limit);
                    break;
                case "RetirementOfEmployee":
                    RetirementOfEmployee(context, arrOrgCode, Start, Limit);
                    break;
                case "RiseSalary":
                    RiseSalary(context, _searchKey, arrOrgCode, Start, Limit);
                    break;
                case "Catalog":
                    Catalog(context, Start, Limit);
                    break;
                case "SearchUser":
                    SearchUser(context, Start, Limit);
                    break;
                case "WorkProcess":
                    WorkProcess(context, _searchKey, Start, Limit, _departmentSelected);
                    break;
                case "Quantum":
                    Quantum(context, _searchKey, Start, Limit);
                    break;
                case "Record":
                    Record(context, _departments, Start, Limit);
                    break;
                case "Category":
                    Category(context, _searchKey);
                    break;
                case "CPVPosition":
                    CpvPosition(context, _searchKey, Start, Limit, _departments, _departmentSelected);
                    break;
                case "UcChooseEmployee":
                    UcChooseEmployee(context, Start, Limit);
                    break;
                case "Reward":
                    RewardOrDiscipline(context, arrOrgCode, _searchKey, Start, Limit, _departmentSelected);
                    break;
                case "DepartmentReward":
                    DepartmentReward(context, arrOrgCode, _searchKey, Start, Limit);
                    break;
                case "BusinessHistory":
                    BusinessHistory(context, _searchKey, Start, Limit, _departmentSelected, _businessType);
                    break;
                case "Asset":
                    Asset(context, arrOrgCode, _searchKey, Start, Limit);
                    break;
                case "AttachFile":
                    AttachFile(context, arrOrgCode, _searchKey, Start, Limit);
                    break;
                case "Bank":
                    Bank(context, arrOrgCode, _searchKey, Start, Limit);
                    break;
                case "TimeSheetCode":
                    TimeSheetCode(context, arrOrgCode, _searchKey, Start, Limit, _departmentSelected);
                    break;
                case "WorkShift":
                    TimeSheetWorkShift(context, _searchKey, Start, Limit);
                    break;
                case "TimeSheetSymbol":
                    TimeSheetSymbol(context, _searchKey, Start, Limit);
                    break;
                case "Scheduler":
                    SchedulerManagement(context, _searchKey, Start, Limit);
                    break;
                case "SchedulerHistory":
                    SchedulerHistoryManagement(context, _searchKey, Start, Limit);
                    break;
                case "SchedulerLog":
                    SchedulerLogManagement(context, Start, Limit);
                    break;
                case "FluctuationEmployee":
                    FluctuationEmployeeManagement(context, arrOrgCode, _searchKey, Start, Limit, _departmentSelected);
                    break;
                case "FluctuationInsurance":
                    FluctuationInsuranceManagement(context, arrOrgCode, _searchKey, Start, Limit, _departmentSelected);
                    break;
                case "TimeSheetSymbolEvent":
                    TimeSheetSymbolEvent(context, _searchKey, Start, Limit);
                    break;
                case "TimeSheetReportList":
                    TimeSheetReport(context, _searchKey, Start, Limit);
                    break;
                case "SalaryBoardList":
                    SalaryBoardList(context, _searchKey,  _departments, Start, Limit);
                    break;
                case "GroupTimeSheetSymbol":
                    GroupTimeSheetSymbol(context, _searchKey, Start, Limit);
                    break;
                case "GroupWorkShift":
                    GroupWorkShift(context, _searchKey, Start, Limit);
                    break;
                case "TimeSheetEvent":
                    TimeSheetEvent(context, _searchKey, _timeSheetId, Start, Limit);
                    break;
                case "UpdateTimeSheetEvent":
                    UpdateTimeSheetEvent(context, _searchKey, _timeSheetId, Start, Limit);
                    break;
                case "SalaryConfig":
                    SalaryConfig(context, _searchKey, Start, Limit);
                    break;
                case "SalaryColumnDynamic":
                    SalaryColumnDynamic(context, _searchKey, Start, Limit);
                    break;
                case "PlanJobTitle":
                    PlanJobTitle(context, arrOrgCode, _searchKey, Start, Limit);
                    break;
                case "TimeSheetMachine":
                    TimeSheetMachine(context, arrOrgCode, _searchKey, Start, Limit, _departmentSelected);
                    break;
                case "PayrollDetail":
                    PayrollDetail(context, _searchKey, Start, Limit);
                    break;
            }
        }

        /// <summary>
        /// Danh sách máy chấm công
        /// </summary>
        /// <param name="context"></param>
        /// <param name="departments"></param>
        /// <param name="searchKey"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <param name="departmentSelected"></param>
        private void TimeSheetMachine(HttpContext context, string[] departments, string searchKey, int start, int limit, string departmentSelected)
        {
            var pageResult = TimeSheetMachineController.GetPaging(searchKey, null, null, String.Join(",", departments), departmentSelected, null, start, limit);
            context.Response.Write("{{TotalRecords:{0},'Data':{1}}}".FormatWith(pageResult.Total, Ext.Net.JSON.Serialize(pageResult.Data)));
        }

        /// <summary>
        /// Quy hoạch chức danh
        /// </summary>
        /// <param name="context"></param>
        /// <param name="arrDepartment"></param>
        /// <param name="searchKey"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        private void PlanJobTitle(HttpContext context, string[] arrDepartment, string searchKey, int start, int limit)
        {
            var data = SQLHelper.ExecuteTable(SQLManagementAdapter.GetStore_GetListPlanJobTitle(string.Join(",", arrDepartment),
                searchKey, start, limit));
            var count = SQLHelper.ExecuteTable(SQLManagementAdapter.GetStore_GetListPlanJobTitle(string.Join(",", arrDepartment),
                searchKey, null, null)).Rows.Count;
            context.Response.Write(string.Format("{{TotalRecords:{1},'Data':{0}}}", Ext.Net.JSON.Serialize(data), count));
        }

        /// <summary>
        /// Danh sach bang tien luong
        /// </summary>
        /// <param name="context"></param>
        /// <param name="searchKey"></param>
        /// <param name="departments"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        private void SalaryBoardList(HttpContext context, string searchKey, string departments, int start, int limit)
        {
            var pageResult = PayrollController.GetPaging(searchKey, null, departments, null, null, null, false, "[Year] DESC,[Month] DESC ", start, limit);
            context.Response.Write("{{TotalRecords:{0},'Data':{1}}}".FormatWith(pageResult.Total, JSON.Serialize(pageResult.Data)));
        }

        /// <summary>
        /// Danh sach bang cham cong
        /// </summary>
        /// <param name="context"></param>
        /// <param name="searchKey"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        private void TimeSheetReport(HttpContext context, string searchKey, int start, int limit)
        {
            var pageResult = TimeSheetReportController.GetPaging(searchKey, null, null, null,  false, null, "EditedDate DESC, CreatedDate DESC", start, limit);
            context.Response.Write("{{TotalRecords:{0},'Data':{1}}}".FormatWith(pageResult.Total, JSON.Serialize(pageResult.Data)));
        }

        /// <summary>
        /// Ký hiệu chấm công khi add event
        /// </summary>
        /// <param name="context"></param>
        /// <param name="searchKey"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        private void TimeSheetSymbolEvent(HttpContext context, string searchKey, int start, int limit)
        {
            var adjustType = string.Empty;
            if(!string.IsNullOrEmpty(context.Request["AdjustType"]))
            {
                adjustType = context.Request["AdjustType"];
            }
            var data = SQLHelper.ExecuteTable(SQLManagementAdapter.GetStore_GetListTimeSheetSymbol(
                searchKey, start, limit, adjustType));
            var count = SQLHelper.ExecuteTable(SQLManagementAdapter.GetStore_GetListTimeSheetSymbol(
                                      searchKey, null, null, adjustType)).Rows.Count;
            context.Response.Write("{{TotalRecords:{0},'Data':{1}}}".FormatWith(count, JSON.Serialize(data)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="arrOrgCode"></param>
        /// <param name="searchKey"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <param name="departmentSelected"></param>
        private void FluctuationInsuranceManagement(HttpContext context, string[] arrOrgCode, string searchKey, int start, int limit, string departmentSelected)
        {
            var data = SQLHelper.ExecuteTable(
                SQLReportAdapter.GetStore_BaoCaoTangGiamBh(string.Join(",", arrOrgCode), null, searchKey,
                    null, start, limit, departmentSelected));
            var count = data.Rows.Count;
            context.Response.Write(string.Format("{{total:{1},'plants':{0}}}", Ext.Net.JSON.Serialize(data), count));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="arrOrgCode"></param>
        /// <param name="searchKey"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <param name="departmentSelected"></param>
        private void FluctuationEmployeeManagement(HttpContext context, string[] arrOrgCode, string searchKey, int start, int limit, string departmentSelected)
        {
            string fromDate, toDate;
            if(!string.IsNullOrEmpty(context.Request["TuNgay"]))
            {
                var date = DateTime.Parse(context.Request["TuNgay"]);
                fromDate = date.ToString("MM/dd/yyyy");
            }
            else
            {
                fromDate = new DateTime(1901, 1, 1).ToString("d");
            }
            if(!string.IsNullOrEmpty(context.Request["DenNgay"]))
            {
                var date = DateTime.Parse(context.Request["DenNgay"]);
                toDate = date.ToString("MM/dd/yyyy");
            }
            else
            {
                toDate = new DateTime(2900, 1, 1).ToString("d");
            }
            var data = SQLHelper.ExecuteTable(
                SQLReportAdapter.GetStore_BaoCaoTangGiamNhanSu(string.Join(",", arrOrgCode), null, searchKey,
                    null, start, limit, departmentSelected, fromDate, toDate));
            var count = data.Rows.Count;
            context.Response.Write(string.Format("{{total:{1},'plants':{0}}}", Ext.Net.JSON.Serialize(data), count));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        private void SchedulerLogManagement(HttpContext context, int start, int limit)
        {
            int? schedulerHistory = null;
            if(!string.IsNullOrEmpty(context.Request["SchedulerHistory"]))
            {
                schedulerHistory = Convert.ToInt32(context.Request["SchedulerHistory"]);
            }
            var pageResult = SchedulerLogServices.GetPaging(schedulerHistory, null, start, limit);
            context.Response.Write("{{TotalRecords:{0},'Data':{1}}}".FormatWith(pageResult.Total, Ext.Net.JSON.Serialize(pageResult.Data)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="searchKey"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        private void SchedulerHistoryManagement(HttpContext context, string searchKey, int start, int limit)
        {
            var condition = Constant.ConditionDefault;
            if(!string.IsNullOrEmpty(searchKey))
            {
                condition += " AND [Description] LIKE N'%{0}%'".FormatWith(searchKey);
            }
            //default
            var order = " [StartTime] DESC ";
            var pageResult = SchedulerHistoryServices.GetPaging(condition, order, start, limit);
            context.Response.Write("{{TotalRecords:{0},'Data':{1}}}".FormatWith(pageResult.Total, Ext.Net.JSON.Serialize(pageResult.Data)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="searchKey"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        private void SchedulerManagement(HttpContext context, string searchKey, int start, int limit)
        {
            var condition = "1=1";
            if(!string.IsNullOrEmpty(context.Request["schedulerType"]))
            {
                int? schedulerType = Convert.ToInt32(context.Request["schedulerType"]);
                condition += "AND [SchedulerTypeId] = {0}".FormatWith(schedulerType);
            }
            if(!string.IsNullOrEmpty(context.Request["repeatType"]))
            {
                int? repeatType = Convert.ToInt32(context.Request["repeatType"]);
                condition += " AND [RepeatType] = {0}".FormatWith(repeatType);
            }
            if(!string.IsNullOrEmpty(context.Request["status"]))
            {
                int? status = Convert.ToInt32(context.Request["status"]);
                condition += " AND [Status] = {0}".FormatWith(status);
            }
            if(!string.IsNullOrEmpty(context.Request["scope"]))
            {
                int? scope = Convert.ToInt32(context.Request["scope"]);
                condition += " AND [Scope] = {0}".FormatWith(scope);
            }
            var pageResult = SchedulerServices.GetPaging(condition, null, start, limit);
            context.Response.Write("{{TotalRecords:{0},'Data':{1}}}".FormatWith(pageResult.Total, Ext.Net.JSON.Serialize(pageResult.Data)));
        }

        /// <summary>
        /// Danh mục ký hiệu chấm công
        /// </summary>
        /// <param name="context"></param>
        /// <param name="searchKey"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        private void TimeSheetSymbol(HttpContext context, string searchKey, int start, int limit)
        {
            int? groupSymbolTypeId = null;
            var group = string.Empty;
            if(!string.IsNullOrEmpty(context.Request["GroupSymbolTypeId"]))
            {
                groupSymbolTypeId = Convert.ToInt32(context.Request["GroupSymbolTypeId"]);
            }
            if(!string.IsNullOrEmpty(context.Request["group"]))
            {
                group = context.Request["group"];
            }
            // order, set default order 
            var order = !string.IsNullOrEmpty(context.Request["order"]) ? context.Request["order"] : "[Order] ";

            var pageResult = TimeSheetSymbolController.GetPaging(searchKey, null, groupSymbolTypeId, group, null, false, order, start, limit);
            context.Response.Write(string.Format("{{TotalRecords:{1},'Data':{0}}}", JSON.Serialize(pageResult.Data), pageResult.Total));
        }

        /// <summary>
        /// Danh sách bảng phân ca tháng
        /// </summary>
        /// <param name="context"></param>
        /// <param name="searchKey"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        private void TimeSheetWorkShift(HttpContext context, string searchKey, int start, int limit)
        {
            DateTime? fromDate = null;
            DateTime? toDate = null;
            int? groupWorkShiftId = null;
            if(!string.IsNullOrEmpty(context.Request["fromDate"]))
            {
                fromDate = context.Request["fromDate"].ToVnTime("d/M/yyyy");
            }
            if(!string.IsNullOrEmpty(context.Request["toDate"]))
            {
                toDate = context.Request["toDate"].ToVnTime("d/M/yyyy");
            }

            if(!string.IsNullOrEmpty(context.Request["groupWorkShift"]))
            {
                groupWorkShiftId = Convert.ToInt32(context.Request["groupWorkShift"]);
            }

            var pageResult = TimeSheetWorkShiftController.GetPaging(searchKey, false, groupWorkShiftId, null, null, fromDate, toDate, null, start, limit);
            context.Response.Write("{{TotalRecords:{0},'Data':{1}}}".FormatWith(pageResult.Total, JSON.Serialize(pageResult.Data)));
        }

        /// <summary>
        /// Đăng ký mã chấm công
        /// </summary>
        /// <param name="context"></param>
        /// <param name="arrDepartmentId"></param>
        /// <param name="searchKey"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <param name="departmentSelectedId"></param>
        private void TimeSheetCode(HttpContext context, string[] arrDepartmentId, string searchKey, int start, int limit, string departmentSelectedId)
        {
            var order = " [EditedDate] DESC, [CreatedDate] DESC ";
            var pageResult = TimeSheetCodeController.GetPaging(searchKey, string.Join(",", arrDepartmentId), Convert.ToInt32(departmentSelectedId), null, null, order, start, limit);
            context.Response.Write("{{TotalRecords:{0},'Data':{1}}}".FormatWith(pageResult.Total, JSON.Serialize(pageResult.Data)));
        }

        /// <summary>
        /// Thẻ ngân hàng
        /// </summary>
        /// <param name="context"></param>
        /// <param name="arrOrgCode"></param>
        /// <param name="searchKey"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        private void Bank(HttpContext context, string[] arrOrgCode, string searchKey, int start, int limit)
        {
            var data = SQLHelper.ExecuteTable(SQLManagementAdapter.GetStore_GetListBank(string.Join(",", arrOrgCode),
                searchKey, start, limit));
            var count = int.Parse("0" + SQLHelper.ExecuteScalar(SQLManagementAdapter.GetStore_GetListBank(string.Join(",", arrOrgCode),
                                      searchKey, null, null)));
            context.Response.Write(string.Format("{{TotalRecords:{1},'Data':{0}}}", Ext.Net.JSON.Serialize(data), count));
        }

        /// <summary>
        /// Tệp tin đính kèm
        /// </summary>
        /// <param name="context"></param>
        /// <param name="arrOrgCode"></param>
        /// <param name="searchKey"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        private void AttachFile(HttpContext context, string[] arrOrgCode, string searchKey, int start, int limit)
        {
            var data = SQLHelper.ExecuteTable(SQLManagementAdapter.GetStore_GetListAttachFile(string.Join(",", arrOrgCode),
                searchKey, start, limit));
            var count = int.Parse("0" + SQLHelper.ExecuteScalar(SQLManagementAdapter.GetStore_GetListAttachFile(string.Join(",", arrOrgCode),
                                      searchKey, null, null)));
            context.Response.Write(string.Format("{{TotalRecords:{1},'Data':{0}}}", Ext.Net.JSON.Serialize(data), count));
        }
        /// <summary>
        /// Công cụ cấp phát
        /// </summary>
        /// <param name="context"></param>
        /// <param name="arrOrgCode"></param>
        /// <param name="searchKey"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        private void Asset(HttpContext context, string[] arrOrgCode, string searchKey, int start, int limit)
        {
            var data = SQLHelper.ExecuteTable(SQLManagementAdapter.GetStore_GetListAsset(string.Join(",", arrOrgCode),
                searchKey, start, limit));
            var count = int.Parse("0" + SQLHelper.ExecuteScalar(SQLManagementAdapter.GetStore_GetListAsset(string.Join(",", arrOrgCode),
                                      searchKey, null, null)));
            context.Response.Write(string.Format("{{TotalRecords:{1},'Data':{0}}}", Ext.Net.JSON.Serialize(data), count));
        }

        /// <summary>
        /// Điều động đến/đi
        /// </summary>
        /// <param name="context"></param>
        /// <param name="searchKey"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <param name="departmentSelected"></param>
        public void BusinessHistory(HttpContext context, string searchKey, int start, int limit, string departmentSelected, string businessType)
        {
            var dsDv = context.Request["departments"];
            // select form db
            var arrOrgCode = string.IsNullOrEmpty(dsDv)
                ? new string[] { }
                : dsDv.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            for(var i = 0; i < arrOrgCode.Length; i++)
            {
                arrOrgCode[i] = "'{0}'".FormatWith(arrOrgCode[i]);
            }

            var dataWorkProcess = SQLHelper.ExecuteTable(
                SQLReportAdapter.GetStore_GetBusinessHistoryList(string.Join(",", arrOrgCode), searchKey, start, limit, departmentSelected, businessType));

            var obj = SQLHelper.ExecuteTable(SQLReportAdapter.GetStore_GetBusinessHistoryList(string.Join(",", arrOrgCode), searchKey, null, null, departmentSelected, businessType)).Rows.Count;

            var countWorkProcess = int.Parse("0" + obj);
            context.Response.Write(string.Format("{{total:{1},'plants':{0}}}", Ext.Net.JSON.Serialize(dataWorkProcess), countWorkProcess));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="arrOrgCode"></param>
        /// <param name="searchKey"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        private void DepartmentReward(HttpContext context, string[] arrOrgCode, string searchKey, int start, int limit)
        {
            var type = string.Empty;
            string fromDate, toDate;
            var level = 0;
            var form = 0;
            DataTable data;
            int count;
            if(!string.IsNullOrEmpty(context.Request["TypePage"]))
            {
                type = context.Request["TypePage"];
            }
            if(!string.IsNullOrEmpty(context.Request["TuNgay"]))
            {
                var date = DateTime.Parse(context.Request["TuNgay"]);
                fromDate = date.ToString("MM/dd/yyyy");
            }
            else
            {
                fromDate = new DateTime(1901, 1, 1).ToString("MM/dd/yyyy");
            }
            if(!string.IsNullOrEmpty(context.Request["DenNgay"]))
            {
                var date = DateTime.Parse(context.Request["DenNgay"]);
                toDate = date.ToString("MM/dd/yyyy");
            }
            else
            {
                toDate = new DateTime(2900, 1, 1).ToString("MM/dd/yyyy");
            }
            if(!string.IsNullOrEmpty(context.Request["Cap"]))
            {
                level = int.Parse(context.Request["Cap"]);
            }
            if(!string.IsNullOrEmpty(context.Request["HinhThuc"]))
            {
                form = int.Parse(context.Request["HinhThuc"]);
            }
            if(type == "KhenThuong")
            {
                data = SQLHelper.ExecuteTable(SQLManagementAdapter.GetStore_GetDepartmentReward(
                    string.Join(",", arrOrgCode), searchKey, fromDate, toDate, form, level, start, limit));
                count = int.Parse("0" + SQLHelper.ExecuteTable(
                                          SQLManagementAdapter.GetStore_GetDepartmentReward(
                                              string.Join(",", arrOrgCode),
                                              searchKey, fromDate, toDate, form, level, null, null)).Rows.Count);
            }
            else
            {
                data = SQLHelper.ExecuteTable(SQLManagementAdapter.GetStore_GetDepartmentDiscipline(
                    string.Join(",", arrOrgCode), searchKey, fromDate, toDate, form, level, start, limit));
                count = int.Parse("0" + SQLHelper.ExecuteTable(
                                          SQLManagementAdapter.GetStore_GetDepartmentDiscipline(string.Join(",", arrOrgCode),
                                              searchKey, fromDate, toDate, form, level, null, null)).Rows.Count);
            }
            context.Response.Write(string.Format("{{total:{1},'plants':{0}}}", Ext.Net.JSON.Serialize(data), count));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="arrOrgCode"></param>
        /// <param name="searchKey"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <param name="departmentSelected"></param>
        private void RewardOrDiscipline(HttpContext context, string[] arrOrgCode, string searchKey, int start, int limit, string departmentSelected)
        {
            var type = string.Empty;
            int? form = null, level = null;
            DateTime? fromDate = null;
            DateTime? toDate = null;

            if(!string.IsNullOrEmpty(context.Request["TypePage"]))
            {
                type = context.Request["TypePage"];
            }

            if (DateTime.TryParseExact(context.Request["TuNgay"], "d/M/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out var parseFromDate))
            {
                fromDate = parseFromDate;
            }
            if(DateTime.TryParseExact(context.Request["DenNgay"], "d/M/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out var parseToDate))
            {
                toDate = parseToDate;
            }

            if(!string.IsNullOrEmpty(context.Request["Cap"]))
            {
                level = int.Parse(context.Request["Cap"]);
            }
            if(!string.IsNullOrEmpty(context.Request["HinhThuc"]))
            {
                form = int.Parse(context.Request["HinhThuc"]);
            }

            var departmentIds = string.Empty;
            departmentIds = !string.IsNullOrEmpty(departmentSelected) ? cat_DepartmentServices.GetAllDepartment(Convert.ToInt32(departmentSelected)) : context.Request["departments"];

            if(type == "KhenThuong")
            {
                // select from db
                var pageResult = RewardController.GetPaging(searchKey, departmentIds, null, level, form, fromDate, toDate, null, start, limit);

                context.Response.Write(string.Format("{{total:{1},'plants':{0}}}", Ext.Net.JSON.Serialize(pageResult.Data), pageResult.Total));
            }
            else
            {
                // select from db
                var pageResult = DisciplineController.GetPaging(searchKey, departmentIds, null, level, form, fromDate, toDate, null, start, limit);

                context.Response.Write(string.Format("{{total:{1},'plants':{0}}}", Ext.Net.JSON.Serialize(pageResult.Data), pageResult.Total));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="searchKey"></param>
        /// <param name="arrOrgCode"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        private void RiseSalary(HttpContext context, string searchKey, string[] arrOrgCode, int start, int limit)
        {
            var fromDate = DateTime.Parse("1900-01-01");
            var toDate = DateTime.Now;
            var salaryRaiseType = !string.IsNullOrEmpty(context.Request["salaryRaiseType"]) ? Convert.ToInt32(context.Request["salaryRaiseType"]) : 0;
            if(!string.IsNullOrEmpty(context.Request["fromDate"]))
            {
                fromDate = DateTime.Parse(context.Request["fromDate"]);
            }
            if(!string.IsNullOrEmpty(context.Request["toDate"]))
            {
                toDate = DateTime.Parse(context.Request["toDate"]);
            }

            var data = SQLHelper.ExecuteTable(
                SQLManagementAdapter.GetStore_ListSalaryRaise(string.Join(",", arrOrgCode), fromDate.ToString("MM/dd/yyyy"), toDate.ToString("MM/dd/yyyy"), start, limit, salaryRaiseType, null, searchKey));
            var count = int.Parse("0" + SQLHelper.ExecuteTable(
                                      SQLManagementAdapter.GetStore_ListSalaryRaise(string.Join(",", arrOrgCode), fromDate.ToString("MM/dd/yyyy"), toDate.ToString("MM/dd/yyyy"), null, null, salaryRaiseType, null, searchKey)).Rows.Count);
            context.Response.Write(string.Format("{{TotalRecords:{1},'Data':{0}}}", Ext.Net.JSON.Serialize(data), count));

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="arrOrgCode"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        private void RetirementOfEmployee(HttpContext context, string[] arrOrgCode, int start, int limit)
        {
            var data = SQLHelper.ExecuteTable(SQLManagementAdapter.GetStore_RetirementOfEmployee(start, limit, string.Join(",", arrOrgCode)));
            var count = int.Parse("0" + SQLHelper.ExecuteTable(SQLManagementAdapter.GetStore_RetirementOfEmployee(null, null, string.Join(",", arrOrgCode))).Rows.Count);
            context.Response.Write(string.Format("{{TotalRecords:{1},'Data':{0}}}", Ext.Net.JSON.Serialize(data), count));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="arrOrgCode"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        public void EndContract(HttpContext context, string[] arrOrgCode, int start, int limit)
        {
            var data = SQLHelper.ExecuteTable(
                SQLManagementAdapter.GetStore_DanhSachNhanVienSapHetHopDong(start, limit, 30, string.Join(",", arrOrgCode)));
            var obj = SQLHelper.ExecuteTable(
                SQLManagementAdapter.GetStore_DanhSachNhanVienSapHetHopDong(null, null, 30, string.Join(",", arrOrgCode))).Rows.Count;
            var count = int.Parse("0" + obj);
            context.Response.Write(string.Format("{{TotalRecords:{1},'Data':{0}}}", Ext.Net.JSON.Serialize(data), count));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="arrOrgCode"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        public void BirthdayOfEmployee(HttpContext context, string[] arrOrgCode, int start, int limit)
        {
            var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);  // get start of month
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1); // last day of month
            string departmentIds = string.Join(",", arrOrgCode);

            var data = SQLHelper.ExecuteTable(SQLManagementAdapter.GetStore_BirthdayOfEmployee(string.Join(",", arrOrgCode), firstDayOfMonth, lastDayOfMonth, start, limit));
            var count = int.Parse("0" + SQLHelper.ExecuteTable(
                                      SQLManagementAdapter.GetStore_BirthdayOfEmployee(departmentIds, firstDayOfMonth, lastDayOfMonth, null, null)).Rows.Count);
            context.Response.Write(string.Format("{{TotalRecords:{1},'Data':{0}}}", Ext.Net.JSON.Serialize(data), count));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="arrOrgCode"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <param name="searchKey"></param>
        private void Home(HttpContext context, string[] arrOrgCode, int start, int limit, string searchKey)
        {
            var statusParam = 0;
            if(!string.IsNullOrEmpty(context.Request["StatusParam"]))
            {
                statusParam = int.Parse(context.Request["StatusParam"]);
            }

            var data = SQLHelper.ExecuteTable(
                SQLManagementAdapter.GetStore_GetRecords(string.Join(",", arrOrgCode), searchKey, statusParam, start, limit));
            var count = SQLHelper.ExecuteScalar(
                SQLManagementAdapter.GetStore_CountGetRecords(string.Join(",", arrOrgCode), searchKey, statusParam));
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
        /// <summary>
        /// Category
        /// </summary>
        /// <param name="context"></param>
        /// <param name="searchKey"></param>
        public void Category(HttpContext context, string searchKey)
        {
            var tableName = string.Empty;

            if(!string.IsNullOrEmpty(context.Request["Table"]))
            {
                tableName = context.Request["Table"];
            }

            var data = SQLHelper.ExecuteTable(
                SQLManagementAdapter.GetStore_GetCategoryByKeyword(searchKey, tableName));

            var obj = SQLHelper.ExecuteScalar(
                SQLManagementAdapter.GetStore_GetCountCategoryByKeyword(searchKey, tableName));
            var count = int.Parse("0" + obj);
            context.Response.Write(string.Format("{{total:{1},'plants':{0}}}", Ext.Net.JSON.Serialize(data), count));
        }

        /// <summary>
        /// Search User Handler
        /// </summary>
        /// <param name="context"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        public void SearchUser(HttpContext context, int start, int limit)
        {
            var query = string.Empty;
            var departments = string.Empty;
            const int count = 0;
            if(!string.IsNullOrEmpty(context.Request["query"]))
            {
                query = context.Request["query"].GetKeyword();
            }

            if(!string.IsNullOrEmpty(context.Request["departments"]))
            {
                departments = context.Request["departments"];
            }

            var arrOrgCode = string.IsNullOrEmpty(departments)
                ? new string[] { }
                : departments.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            for(var i = 0; i < arrOrgCode.Length; i++)
            {
                arrOrgCode[i] = "{0}".FormatWith(arrOrgCode[i]);
            }

            var result =
                SQLHelper.ExecuteTable(SQLManagementAdapter.GetStore_SearchUser(query, string.Join(",", arrOrgCode)));
            context.Response.Write(string.Format("{{total:{1},'plants':{0}}}", Ext.Net.JSON.Serialize(result), count));
        }

        /// <summary>
        /// Work Process handler
        /// </summary>
        /// <param name="context"></param>
        /// <param name="searchKey"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <param name="departmentSelected"></param>
        public void WorkProcess(HttpContext context, string searchKey, int start, int limit, string departmentSelected)
        {
            //string maDonVi = string.Empty;
            string dsDv = context.Request["departments"];
            // select form db
            var arrOrgCode = string.IsNullOrEmpty(dsDv)
                ? new string[] { }
                : dsDv.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            for(var i = 0; i < arrOrgCode.Length; i++)
            {
                arrOrgCode[i] = "'{0}'".FormatWith(arrOrgCode[i]);
            }

            var dataWorkProcess = SQLHelper.ExecuteTable(
                SQLManagementAdapter.GetStore_ListWorkProcess(string.Join(",", arrOrgCode), searchKey, start, limit, departmentSelected));

            var obj = SQLHelper.ExecuteTable(SQLManagementAdapter.GetStore_ListWorkProcess(string.Join(",", arrOrgCode), searchKey, null, null, departmentSelected)).Rows.Count;

            var countWorkProcess = int.Parse("0" + obj);
            context.Response.Write(string.Format("{{total:{1},'plants':{0}}}", Ext.Net.JSON.Serialize(dataWorkProcess), countWorkProcess));
        }

        /// <summary>
        /// Catalog Handler
        /// </summary>
        /// <param name="context"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        public void Catalog(HttpContext context, int start, int limit)
        {
            var objName = string.Empty;
            var keyword = string.Empty;
            if(!string.IsNullOrEmpty(context.Request["objname"]))
            {
                objName = context.Request["objname"];
            }
            if(!string.IsNullOrEmpty(context.Request["query"]))
            {
                keyword = SoftCore.Util.GetInstance().GetKeyword(context.Request["query"]);
            }
            // init page result
            var pageResult = new PageResult<CatalogModel>(0, new List<CatalogModel>());
            // check table name
            if(!string.IsNullOrEmpty(objName))
                // select from table
                pageResult = CatalogController.GetPaging(objName.EscapeQuote(), keyword, null, null, false, null, start, limit);
            // response
            context.Response.ContentType = "text/json";
            context.Response.Write("{{TotalRecords:{0},'Data':{1}}}".FormatWith(pageResult.Total, Ext.Net.JSON.Serialize(pageResult.Data)));
        }

        /// <summary>
        /// Quantum Handler
        /// </summary>
        /// <param name="context"></param>
        /// <param name="searchKey"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        public void Quantum(HttpContext context, string searchKey, int start, int limit)
        {
            var data = SQLHelper.ExecuteTable(SQLManagementAdapter.GetStore_GetAllQuantum(searchKey, start, limit));
            var count = SQLHelper.ExecuteTable(SQLManagementAdapter.GetStore_GetAllQuantum(searchKey, null, null)).Rows.Count;
            context.Response.Write(string.Format("{{TotalRecords:{1},'Data':{0}}}", JSON.Serialize(data), count));
        }

        /// <summary>
        /// Record Handler
        /// </summary>
        /// <param name="context"></param>
        /// <param name="departments"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        public void Record(HttpContext context, string departments, int start, int limit)
        {
            var pageResult = RecordController.GetPaging(departments, null, null, null, start, limit);
            context.Response.Write("{{TotalRecords:{0},'Data':{1}}}".FormatWith(pageResult.Total, Ext.Net.JSON.Serialize(pageResult.Data)));
        }

        /// <summary>
        /// Uc Choose Employee
        /// </summary>
        /// <param name="context"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        private void UcChooseEmployee(HttpContext context, int start, int limit)
        {
            var lstDepartment = context.Request["Department"];
            var workStatus = 0;
            var positionId = 0;
            var jobTitleId = 0;
            var searchEmployee = string.Empty;
            var arrOrgCode = new string[] { };
            var selectedDepartment = string.Empty;
            if(!string.IsNullOrEmpty(context.Request["SearchEmployee"]))
            {
                searchEmployee = context.Request["SearchEmployee"];
            }
            if(!string.IsNullOrEmpty(context.Request["SelectedDepartment"]))
            {
                selectedDepartment = context.Request["SelectedDepartment"];
            }
            if(!string.IsNullOrEmpty(context.Request["Filter"]))
            {
                workStatus = int.Parse(context.Request["Filter"]);
            }
            if(!string.IsNullOrEmpty(context.Request["Position"]))
            {
                positionId = int.Parse(context.Request["Position"]);
            }
            if(!string.IsNullOrEmpty(context.Request["JobTitle"]))
            {
                jobTitleId = int.Parse(context.Request["JobTitle"]);
            }
            if(!string.IsNullOrEmpty(lstDepartment))
            {
                arrOrgCode = string.IsNullOrEmpty(lstDepartment)
                       ? new string[] { }
                       : lstDepartment.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                for(var i = 0; i < arrOrgCode.Length; i++)
                {
                    arrOrgCode[i] = "'{0}'".FormatWith(arrOrgCode[i]);
                }
            }

            var arrRecordIds = new string[] { };
            if(!string.IsNullOrEmpty(context.Request["RecordIds"]))
            {
                var recordIds = context.Request["RecordIds"];
                arrRecordIds = string.IsNullOrEmpty(recordIds)
                    ? new string[] { }
                    : recordIds.Split(new[] {","}, StringSplitOptions.RemoveEmptyEntries);
            }

            var data = SQLHelper.ExecuteTable(
                SQLManagementAdapter.GetStore_ChooseEmployee(string.Join(",", arrOrgCode), string.Join(",", arrRecordIds) , searchEmployee, workStatus,
                    positionId, jobTitleId, start, limit, selectedDepartment, false));
            var count = (int)SQLHelper.ExecuteScalar(SQLManagementAdapter.GetStore_ChooseEmployee(string.Join(",", arrOrgCode),string.Join(",", arrRecordIds) ,
                 searchEmployee, workStatus,
                 positionId, jobTitleId, null, null, selectedDepartment, true));
            context.Response.Write(string.Format("{{TotalRecords:{1},'Data':{0}}}", Ext.Net.JSON.Serialize(data), count));
        }

        /// <summary>
        /// CPV Position
        /// </summary>
        /// <param name="context"></param>
        /// <param name="searchKey"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <param name="departments"></param>
        /// <param name="departmentSeleted"></param>
        private void CpvPosition(HttpContext context, string searchKey, int start, int limit, string departments, string departmentSeleted)
        {
            var lstDepartment = string.Empty;
            if(context.Request["ListDepartment"] != null)
            {
                lstDepartment = context.Request["ListDepartment"];
            }
            var arrOrgCode = string.IsNullOrEmpty(lstDepartment)
                ? new string[] { }
                : lstDepartment.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            for(var i = 0; i < arrOrgCode.Length; i++)
            {
                arrOrgCode[i] = "'{0}'".FormatWith(arrOrgCode[i]);
            }
            var data = SQLHelper.ExecuteTable(
                SQLManagementAdapter.GetStore_GetEmployeeIsCPV(string.Join(",", arrOrgCode), departments, searchKey, start, limit, departmentSeleted));
            var obj = SQLHelper.ExecuteScalar(
                SQLManagementAdapter.GetStore_CountGetEmployeeIsCPV(string.Join(",", arrOrgCode), departments, searchKey, departmentSeleted));
            var count = int.Parse("0" + obj);
            context.Response.Write(string.Format("{{total:{1},'plants':{0}}}", Ext.Net.JSON.Serialize(data), count));
        }

        #region TimeKeeping

        /// <summary>
        /// get all Nhóm ký hiệu chấm công
        /// </summary>
        /// <param name="context"></param>
        /// <param name="searchKey"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        private void GroupTimeSheetSymbol(HttpContext context, string searchKey, int start, int limit)
        {
            TimeSheetStatus? status = null;
            if (!string.IsNullOrEmpty(context.Request["Status"]))
            {
                status = (TimeSheetStatus) Enum.Parse(typeof(TimeSheetStatus), context.Request["Status"]);
            }
            var pageResult = TimeSheetGroupSymbolController.GetPaging(searchKey, status, false, null, null, start, limit);
            context.Response.Write(string.Format("{{TotalRecords:{1},'Data':{0}}}", Ext.Net.JSON.Serialize(pageResult.Data), pageResult.Total));
        }

        /// <summary>
        /// Nhóm bảng phân ca
        /// </summary>
        /// <param name="context"></param>
        /// <param name="arrOrgCode"></param>
        /// <param name="searchKey"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        private void GroupWorkShift(HttpContext context, string searchKey, int start, int limit)
        {
            var order = " [EditedDate] DESC, [CreatedDate] DESC ";
            var pageResult = TimeSheetGroupWorkShiftController.GetPaging(searchKey, false, order, start, limit);
            context.Response.Write("{{TotalRecords:{0},'Data':{1}}}".FormatWith(pageResult.Total, JSON.Serialize(pageResult.Data)));
        }

        /// <summary>
        /// Cập nhật lại bảng timesheet dựa vào event
        /// </summary>
        /// <param name="context"></param>
        /// <param name="searchKey"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        private void UpdateTimeSheetEvent(HttpContext context, string searchKey, int timeSheetId, int start, int limit)
        {
            var data = SQLHelper.ExecuteTable(SQLTimeTrackingAdapter.GetStore_GetTimeSheetEventsByTimeSheetId(
                searchKey, timeSheetId, start, limit, null));

            context.Response.Write("{{'Data':{0}}}".FormatWith(Ext.Net.JSON.Serialize(data)));
        }

        private void TimeSheetEvent(HttpContext context, string searchKey, int timeSheetId, int start, int limit)
        {
            int? type = null;
            DateTime? fromDate = null;
            DateTime? toDate = null;
            int? groupWorkShiftId = null;
            var recordIds = string.Empty;

            if(!string.IsNullOrEmpty(context.Request["Type"]))
            {
                type = int.Parse(context.Request["Type"]);
            }

            if(!string.IsNullOrEmpty(context.Request["adjustTimeSheetHandlerType"]))
            {
                type = int.Parse(context.Request["adjustTimeSheetHandlerType"]) == (int) TimeSheetAdjustmentType.AdjustmentOverTime
                    ? (int)TimeSheetAdjustmentType.AdjustmentOverTime
                    : (int)TimeSheetAdjustmentType.Adjustment;
            }

            if(!string.IsNullOrEmpty(context.Request["groupWorkShiftId"]))
            {
                groupWorkShiftId = Int32.Parse(context.Request["groupWorkShiftId"]);
            }

            if(!string.IsNullOrEmpty(context.Request["fromDate"]))
            {
                fromDate = context.Request["fromDate"].ToVnTime("d/MM/yyyy");
            }

            if(!string.IsNullOrEmpty(context.Request["toDate"]))
            {
                toDate = context.Request["toDate"].ToVnTime("d/MM/yyyy");
            }

            if (!string.IsNullOrEmpty(context.Request["recordIds"]))
            {
                recordIds = context.Request["recordIds"];
            }
            var record = TimeSheetEventController.GetPaging(searchKey, recordIds, null, groupWorkShiftId, null, null, false, type, fromDate, toDate, null, null, start, limit);
            context.Response.Write("{{TotalRecords:{0},'Data':{1}}}".FormatWith(record.Total, JSON.Serialize(record.Data)));
        }
        #endregion

        #region Salary

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="searchKey"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        private static void SalaryConfig(HttpContext context, string searchKey, int start, int limit)
        {
            var configId = 0;
            SalaryConfigDataType? dataType = null;
            if(!string.IsNullOrEmpty(context.Request["ConfigId"]))
            {
                configId = int.Parse(context.Request["ConfigId"]);
            }
            if (!string.IsNullOrEmpty(context.Request["DataType"]))
            {
                dataType = (SalaryConfigDataType) Enum.Parse(typeof(SalaryConfigDataType), context.Request["DataType"]);
            }

            var pageResult = SalaryBoardConfigController.GetListSalaryBoardConfig(searchKey, configId, dataType, start, limit);
            context.Response.Write("{{TotalRecords:{0},'Data':{1}}}".FormatWith(pageResult.Total, Ext.Net.JSON.Serialize(pageResult.Data)));
        }

        /// <summary>
        /// Get list salaryboardColumnDynamic
        /// </summary>
        /// <param name="context"></param>
        /// <param name="searchKey"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        private static void SalaryColumnDynamic(HttpContext context, string searchKey, int start, int limit)
        {
            var salaryBoardId = 0;
            if(!string.IsNullOrEmpty(context.Request["salaryBoardListID"]))
            {
                salaryBoardId = int.Parse(context.Request["salaryBoardListID"]);
            }

            var result = SalaryBoardDynamicColumnController.GetTable(searchKey, salaryBoardId, null, start, limit);
            // get salary board info
            var salaryInfos = SalaryBoardInfoController.GetAll(null, salaryBoardId.ToString(), null, null);

            context.Response.Write("{{TotalRecords:{0},'Data':{1}}}".FormatWith(salaryInfos.Count, JSON.Serialize(result)));
        }

        private static void PayrollDetail(HttpContext context, string searchKey, int start, int limit)
        {
            var payrollId = 0;
            if (!string.IsNullOrEmpty(context.Request["payrollId"]))
                payrollId = int.Parse(context.Request["payrollId"]);

            var result = PayrollController.GetPayrollDetail(searchKey, payrollId, null, null);

            context.Response.Write("{{TotalRecords:{0},Data:{1}}}".FormatWith(result.Rows.Count, JSON.Serialize(result)));
        }
        #endregion
    }
}