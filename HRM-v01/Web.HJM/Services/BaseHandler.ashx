 <%@ WebHandler Language="C#" Class="Web.HJM.Services.BaseHandler" %>

using Ext.Net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using Web.Core;
using Web.Core.Framework;
using Web.Core.Framework.Adapter;
using Web.Core.Object.Catalog;
using Web.Core.Service.Catalog;
using Web.Core.Framework.Controller;
using Web.Core.Service.HumanRecord;
using Web.Core.Service.Security;
using Web.HJM.Modules.ProfileHuman.Management;

namespace Web.HJM.Services
{
    public class BaseHandler : IHttpHandler
    {
        public int Start = 0;
        public int Limit = 20;
        public string SearchKey = "";
        public string Handler;
        public string Departments;
        public string DepartmentSelected;
        public string BusinessType = string.Empty;
        public int TimeSheetId = 0;

        public void ProcessRequest(HttpContext context)
        {
            // init params
            context.Response.ContentType = "text/plain";
            if(!string.IsNullOrEmpty(context.Request["handlers"]))
            {
                Handler = context.Request["handlers"];
            }
            if(!string.IsNullOrEmpty(context.Request["SearchKey"]))
            {
                SearchKey = "%" + SoftCore.Util.GetInstance().GetKeyword(context.Request["SearchKey"]) + "%";
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
                Departments = context.Request["departments"];
            }
            if(!string.IsNullOrEmpty(context.Request["departmentSelected"]))
            {
                DepartmentSelected = context.Request["departmentSelected"];
            }
            if(!string.IsNullOrEmpty(context.Request["businessType"]))
            {
                BusinessType = context.Request["businessType"];
            }
            if(!string.IsNullOrEmpty(context.Request["timeSheetId"]))
            {
                TimeSheetId = Convert.ToInt32(context.Request["timeSheetId"]);
            }

            var arrOrgCode = string.IsNullOrEmpty(Departments)
                ? new string[] { }
                : Departments.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            for(var i = 0; i < arrOrgCode.Length; i++)
            {
                arrOrgCode[i] = "'{0}'".FormatWith(arrOrgCode[i]);
            }

            switch(Handler)
            {
                case "Home":
                    Home(context, arrOrgCode, Start, Limit, SearchKey);
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
                    RiseSalary(context, SearchKey, arrOrgCode, Start, Limit);
                    break;
                case "Catalog":
                    Catalog(context, Start, Limit);
                    break;
                case "SearchUser":
                    SearchUser(context, Start, Limit);
                    break;
                case "WorkProcess":
                    WorkProcess(context, SearchKey, Start, Limit, DepartmentSelected);
                    break;
                case "Quantum":
                    Quantum(context, SearchKey, Start, Limit);
                    break;
                case "Record":
                    Record(context, Departments, Start, Limit);
                    break;
                case "Category":
                    Category(context, SearchKey);
                    break;
                case "CPVPosition":
                    CpvPosition(context, SearchKey, Start, Limit, Departments, DepartmentSelected);
                    break;
                case "UcChooseEmployee":
                    UcChooseEmployee(context, SearchKey, Start, Limit);
                    break;
                case "Reward":
                    RewardOrDiscipline(context, arrOrgCode, SearchKey, Start, Limit, DepartmentSelected);
                    break;
                case "DepartmentReward":
                    DepartmentReward(context, arrOrgCode, SearchKey, Start, Limit);
                    break;
                case "GoAboard":
                    GoAboard(context, arrOrgCode, SearchKey, Start, Limit, DepartmentSelected);
                    break;
                case "Training":
                    Training(context, arrOrgCode, SearchKey, DepartmentSelected, Start, Limit);
                    break;
                case "BusinessHistory":
                    BusinessHistory(context, SearchKey, Start, Limit, DepartmentSelected, BusinessType);
                    break;
                case "Asset":
                    Asset(context, arrOrgCode, SearchKey, Start, Limit);
                    break;
                case "AttachFile":
                    AttachFile(context, arrOrgCode, SearchKey, Start, Limit);
                    break;
                case "Bank":
                    Bank(context, arrOrgCode, SearchKey, Start, Limit);
                    break;
                case "Holiday":
                    Holiday(context, SearchKey, Start, Limit);
                    break;
                case "AnnualLeave":
                    AnnualLeave(context, SearchKey, arrOrgCode, DepartmentSelected, Start, Limit);
                    break;
                case "TimeSheetRuleLateOrEarly":
                    TimeSheetRuleLateOrEarly(context, SearchKey, Start, Limit);
                    break;
                case "Scheduler":
                    SchedulerManagement(context, SearchKey, Start, Limit);
                    break;
                case "SchedulerHistory":
                    SchedulerHistoryManagement(context, SearchKey, Start, Limit);
                    break;
                case "SchedulerLog":
                    SchedulerLogManagement(context, Start, Limit);
                    break;
                case "FluctuationEmployee":
                    FluctuationEmployeeManagement(context, arrOrgCode, SearchKey, Start, Limit, DepartmentSelected);
                    break;
                case "FluctuationInsurance":
                    FluctuationInsuranceManagement(context, arrOrgCode, SearchKey, Start, Limit, DepartmentSelected);
                    break;
                case "TimeSheetSymbolEvent":
                    TimeSheetSymbolEvent(context, SearchKey, Start, Limit);
                    break;
                case "TimeSheetReportList":
                    TimeSheetReportList(context, SearchKey, Start, Limit);
                    break;
                case "SalaryBoardList":
                    SalaryBoardList(context, Departments, Start, Limit);
                    break;
                case "SalaryConfig":
                    SalaryConfig(context, SearchKey, Start, Limit);
                    break;
                case "SalaryColumnDynamic":
                    SalaryColumnDynamic(context, SearchKey, Start, Limit);
                    break;
                case "PlanJobTitle":
                    PlanJobTitle(context, arrOrgCode, SearchKey, Start, Limit);
                    break;
            }
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
        /// <param name="departments"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        private void SalaryBoardList(HttpContext context, string departments, int start, int limit)
        {
            var pageResult = PayrollController.GetPaging(null, null, departments, null, null, null, false, "[Year],[Month]", start, limit);
            context.Response.Write("{{TotalRecords:{0},'Data':{1}}}".FormatWith(pageResult.Total, JSON.Serialize(pageResult.Data)));
        }

        /// <summary>
        /// Danh sach bang cham cong
        /// </summary>
        /// <param name="context"></param>
        /// <param name="searchKey"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        private void TimeSheetReportList(HttpContext context, string searchKey, int start, int limit)
        {
            var typeTimeSheet = string.Empty;
            if(!string.IsNullOrEmpty(context.Request["typeTimeSheet"]))
            {
                typeTimeSheet = context.Request["typeTimeSheet"];
            }
            var data = SQLHelper.ExecuteTable(SQLManagementAdapter.GetStore_GetTimeSheetReportList(
                searchKey, start, limit, typeTimeSheet));
            var count = int.Parse("0" + SQLHelper.ExecuteTable(SQLManagementAdapter.GetStore_GetTimeSheetReportList(
                                      searchKey, null, null, null)).Rows.Count);
            context.Response.Write(string.Format("{{TotalRecords:{1},'Data':{0}}}", Ext.Net.JSON.Serialize(data), count));
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
            var data = SQLHelper.ExecuteTable(SQLManagementAdapter.GetStore_GetListTimeSheetSymbol(
                searchKey, start, limit, "eventTimeSheet"));
            var count = SQLHelper.ExecuteTable(SQLManagementAdapter.GetStore_GetListTimeSheetSymbol(
                                      searchKey, null, null, "eventTimeSheet")).Rows.Count;
            context.Response.Write("{{TotalRecords:{0},'Data':{1}}}".FormatWith(count, Ext.Net.JSON.Serialize(data)));
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
            var condition = "1=1";
            if(!string.IsNullOrEmpty(searchKey))
            {
                condition += " AND [Description] LIKE N'%{0}%'".FormatWith(searchKey);
            }
            var pageResult = SchedulerHistoryServices.GetPaging(condition, null, start, limit);
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
                condition += "AND [SchedulerTimeSheetHandlerTypeId] = {0}".FormatWith(schedulerType);
            }
            if(!string.IsNullOrEmpty(context.Request["repeateType"]))
            {
                int? repeateType = Convert.ToInt32(context.Request["repeateType"]);
                condition += " AND [RepeateTimeSheetHandlerType] = {0}".FormatWith(repeateType);
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
        /// Cấu hình đi muộn, về sớm
        /// </summary>
        /// <param name="context"></param>
        /// <param name="searchKey"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        private void TimeSheetRuleLateOrEarly(HttpContext context, string searchKey, int start, int limit)
        {
            var pageResult = TimeSheetRuleWrongTimeController.GetPaging(null, null, null, null, start, limit);
            context.Response.Write("{{TotalRecords:{0},'Data':{1}}}".FormatWith(pageResult.Total, Ext.Net.JSON.Serialize(pageResult.Data)));
        }

        /// <summary>
        /// cấu hình ngày phép
        /// </summary>
        /// <param name="context"></param>
        /// <param name="searchKey"></param>
        /// <param name="departments"></param>
        /// <param name="departmentSelected"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        private void AnnualLeave(HttpContext context, string searchKey, string[] departments, string departmentSelected, int start, int limit)
        {
            var data = SQLHelper.ExecuteTable(
                SQLManagementAdapter.GetStore_AnnualLeaveConfig(string.Join(",", departments), departmentSelected, searchKey, null, start, limit));
            var countTotal = SQLHelper.ExecuteTable(
                SQLManagementAdapter.GetStore_AnnualLeaveConfig(string.Join(",", departments), departmentSelected, searchKey, null, null, null)).Rows.Count;
            context.Response.Write(string.Format("{{TotalRecords:{1},'Data':{0}}}", Ext.Net.JSON.Serialize(data), countTotal));
        }

        /// <summary>
        /// Danh sách ngày lễ, tết
        /// </summary>
        /// <param name="context"></param>
        /// <param name="searchKey"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        private void Holiday(HttpContext context, string searchKey, int start, int limit)
        {
            var data = SQLHelper.ExecuteTable(SQLManagementAdapter.GetStore_GetListHoliday(
                searchKey, start, limit));
            var count = SQLHelper.ExecuteTable(SQLManagementAdapter.GetStore_GetListHoliday(
                                      searchKey, null, null)).Rows.Count;
            context.Response.Write(string.Format("{{TotalRecords:{1},'Data':{0}}}", Ext.Net.JSON.Serialize(data), count));
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
        /// get list training history
        /// </summary>
        /// <param name="context"></param>
        /// <param name="arrDepartment"></param>
        /// <param name="searchKey"></param>
        /// <param name="departmentSelected"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        private void Training(HttpContext context, string[] arrDepartment, string searchKey, string departmentSelected, int start, int limit)
        {
            var nationId = 0;
            var fromDate = string.Empty;
            var toDate = string.Empty;
            var type = string.Empty;
            int? status = null;
            if(!string.IsNullOrEmpty(context.Request["NationCode"]))
            {
                nationId = Convert.ToInt32(context.Request["NationCode"]);
            }
            if(!string.IsNullOrEmpty(context.Request["FromDate"]))
            {
                fromDate = DateTime.Parse(context.Request["FromDate"]).ToString("MM/dd/yyyy");
            }
            if(!string.IsNullOrEmpty(context.Request["ToDate"]))
            {
                toDate = DateTime.Parse(context.Request["ToDate"]).ToString("MM/dd/yyyy");
            }

            if(!string.IsNullOrEmpty(context.Request["type"]))
            {
                type = context.Request["type"];
            }

            if(!string.IsNullOrEmpty(context.Request["status"]))
            {
                status = Convert.ToInt32(context.Request["status"]);
            }

            var data = SQLHelper.ExecuteTable(SQLManagementAdapter.GetStore_GetEmployeeTraining(
                string.Join(",", arrDepartment), searchKey, nationId, departmentSelected, fromDate, toDate, start, limit, type, status));
            var count = SQLHelper.ExecuteTable(SQLManagementAdapter.GetStore_GetEmployeeTraining(
                string.Join(",", arrDepartment), searchKey, nationId, departmentSelected, fromDate, toDate, null, null, type, status)).Rows.Count;
            context.Response.Write(string.Format("{{total:{1},'plants':{0}}}", Ext.Net.JSON.Serialize(data), count));
        }

        private void GoAboard(HttpContext context, string[] arrOrgCode, string searchKey, int start, int limit, string departmentSelected)
        {
            var nation = string.Empty;
            start = 0;
            limit = 25;
            if(!string.IsNullOrEmpty(context.Request["QuocGia"]))
            {
                nation = context.Request["QuocGia"];
            }
            var data = SQLHelper.ExecuteTable(
                SQLManagementAdapter.GetStore_GetEmployeeGoAboard(string.Join(",", arrOrgCode), searchKey, nation, start,
                    limit, departmentSelected));
            var count = int.Parse("0" + SQLHelper.ExecuteTable(
                                      SQLManagementAdapter.GetStore_CountGetEmployeeGoAboard(string.Join(",", arrOrgCode),
                                          searchKey, nation, departmentSelected)));
            context.Response.Write(string.Format("{{total:{1},'plants':{0}}}", Ext.Net.JSON.Serialize(data), count));

        }

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

        private void RewardOrDiscipline(HttpContext context, string[] arrOrgCode, string searchKey, int start, int limit, string departmentSelected)
        {
            var type = string.Empty;
            string fromDate;
            string toDate;
            int form = 0, level = 0;
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
                data = SQLHelper.ExecuteTable(SQLManagementAdapter.GetStore_GetEmployeeReward(string.Join(",", arrOrgCode),
                    searchKey,
                    fromDate, toDate, form, level, start, limit, departmentSelected));
                count = int.Parse("0" + SQLHelper.ExecuteScalar(
                                      SQLManagementAdapter.GetStore_CountGetEmployeeReward(string.Join(",", arrOrgCode),
                                          searchKey, fromDate, toDate, form, level, departmentSelected)));
            }
            else
            {
                data = SQLHelper.ExecuteTable(SQLManagementAdapter.GetStore_GetEmployeeDiscipline(string.Join(",", arrOrgCode),
                    searchKey,
                    fromDate, toDate, form, level, start, limit, departmentSelected));
                count = int.Parse("0" + SQLHelper.ExecuteScalar(
                                      SQLManagementAdapter.GetStore_CountGetEmployeeDiscipline(string.Join(",", arrOrgCode),
                                          searchKey, fromDate, toDate, form, level, departmentSelected)));

            }
            context.Response.Write(string.Format("{{total:{1},'plants':{0}}}", Ext.Net.JSON.Serialize(data), count));
        }

        /// <summary>
        /// nang luong
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

        private void RetirementOfEmployee(HttpContext context, string[] arrOrgCode, int start, int limit)
        {
            var data = SQLHelper.ExecuteTable(SQLManagementAdapter.GetStore_RetirementOfEmployee(start, limit, string.Join(",", arrOrgCode)));
            var count = int.Parse("0" + SQLHelper.ExecuteTable(SQLManagementAdapter.GetStore_RetirementOfEmployee(null, null, string.Join(",", arrOrgCode))).Rows.Count);
            context.Response.Write(string.Format("{{TotalRecords:{1},'Data':{0}}}", Ext.Net.JSON.Serialize(data), count));
        }

        public void EndContract(HttpContext context, string[] arrOrgCode, int start, int limit)
        {
            var data = SQLHelper.ExecuteTable(
                SQLManagementAdapter.GetStore_DanhSachNhanVienSapHetHopDong(start, limit, 30, string.Join(",", arrOrgCode)));
            var obj = SQLHelper.ExecuteTable(
                SQLManagementAdapter.GetStore_DanhSachNhanVienSapHetHopDong(null, null, 30, string.Join(",", arrOrgCode))).Rows.Count;
            var count = int.Parse("0" + obj);
            context.Response.Write(string.Format("{{TotalRecords:{1},'Data':{0}}}", Ext.Net.JSON.Serialize(data), count));

        }

        public void BirthdayOfEmployee(HttpContext context, string[] arrOrgCode, int start, int limit)
        {
            var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);  // get start of month
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1); // last day of month
            var departmentIds = string.Join(",", arrOrgCode);
            var data = SQLHelper.ExecuteTable(SQLManagementAdapter.GetStore_BirthdayOfEmployee(departmentIds, firstDayOfMonth, lastDayOfMonth, start, limit));
            var count = int.Parse("0" + SQLHelper.ExecuteTable(
                                      SQLManagementAdapter.GetStore_BirthdayOfEmployee(departmentIds, firstDayOfMonth, lastDayOfMonth,
                                          null, null)).Rows.Count);
            context.Response.Write(string.Format("{{TotalRecords:{1},'Data':{0}}}", Ext.Net.JSON.Serialize(data), count));

        }

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
            if(!string.IsNullOrEmpty(context.Request["query"]))
            {
                query = "%" + SoftCore.Util.GetInstance().GetKeyword(context.Request["query"]) + "%";
            }
            var departments = string.Empty;
            if(!string.IsNullOrEmpty(context.Request["departments"]))
            {
                departments = context.Request["departments"];
            }

            var arrOrgCode = string.IsNullOrEmpty(departments)
                ? new string[] { }
                : departments.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            for(var i = 0; i < arrOrgCode.Length; i++)
            {
                arrOrgCode[i] = "'{0}'".FormatWith(arrOrgCode[i]);
            }
            const int count = 0;
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
            var pageResult = new PageResult<cat_Base>(0, new List<cat_Base>());
            // check table name
            if(!string.IsNullOrEmpty(objName))
                // select from table
                pageResult = cat_BaseServices.GetPaging(objName.EscapeQuote(), keyword, null, false, null, start, limit);
            // response
            context.Response.ContentType = "text/json";
            context.Response.Write("{{TotalRecords:{0},'Data':{1}}}".FormatWith(pageResult.Total, Ext.Net.JSON.Serialize(pageResult.Data)));
        }

        /// <summary>
        /// Quantum Handler
        /// </summary>
        /// <param name="context"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        public void Quantum(HttpContext context, string searchKey, int start, int limit)
        {
            var data = SQLHelper.ExecuteTable(SQLManagementAdapter.GetStore_GetAllQuantum(searchKey, start, limit));
            var count = SQLHelper.ExecuteTable(SQLManagementAdapter.GetStore_GetAllQuantum(searchKey, null, null)).Rows.Count;
            context.Response.Write(string.Format("{{TotalRecords:{1},'Data':{0}}}", Ext.Net.JSON.Serialize(data), count));
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
        /// <param name="searchKey"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        private void UcChooseEmployee(HttpContext context, string searchKey, int start, int limit)
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

            var data = SQLHelper.ExecuteTable(
                SQLManagementAdapter.GetStore_ChooseEmployee(string.Join(",", arrOrgCode), searchEmployee, workStatus,
                    positionId, jobTitleId, start, limit, selectedDepartment, false));
            var count = (int)SQLHelper.ExecuteScalar(SQLManagementAdapter.GetStore_ChooseEmployee(string.Join(",", arrOrgCode),
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

        #region Salary

        private static void SalaryConfig(HttpContext context, string searchKey, int start, int limit)
        {
            var configId = 0;
            if(!string.IsNullOrEmpty(context.Request["ConfigId"]))
            {
                configId = int.Parse(context.Request["ConfigId"]);
            }
            var pageResult = SalaryBoardConfigController.GetListSalaryBoardConfig(searchKey, configId, start, limit);
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
            var data = hr_SalaryBoardDynamicColumnService.GetStore_GetSalaryDynamicColumns(searchKey, start, limit, salaryBoardId);
            var total = hr_SalaryBoardDynamicColumnService.GetStore_GetSalaryDynamicColumns(searchKey, null, null, salaryBoardId).Rows.Count;

            context.Response.Write("{{TotalRecords:{0},'Data':{1}}}".FormatWith(total, Ext.Net.JSON.Serialize(data)));
        }
        #endregion
    }
}