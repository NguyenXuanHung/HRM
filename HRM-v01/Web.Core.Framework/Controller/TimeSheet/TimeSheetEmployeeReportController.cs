using System.Collections.Generic;
using System.Linq;
using Web.Core.Framework.Model.TimeSheet;
using Web.Core.Object.TimeSheet;
using Web.Core.Service.TimeSheet;

namespace Web.Core.Framework.Controller.TimeSheet
{
    public class TimeSheetEmployeeReportController
    {
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static TimeSheetEmployeeReportModel GetById(int id)
        {
            // get entity
            var entity = hr_TimeSheetEmployeeReportServices.GetById(id);

            // return
            return entity != null ? new TimeSheetEmployeeReportModel(entity) : null;
        }

        /// <summary>
        /// Get unique
        /// </summary>
        /// <param name="recordId"></param>
        /// <param name="groupWorkShiftId"></param>
        /// <returns></returns>
        public static TimeSheetEmployeeReportModel GetUnique(int recordId, int groupWorkShiftId)
        {
            // init condition
            var condition = @"[RecordId] = {0} AND [GroupWorkShiftId] = {0}".FormatWith(recordId, groupWorkShiftId);

            // get entity
            var entity = hr_TimeSheetEmployeeReportServices.GetByCondition(condition);

            // return
            return entity != null ? new TimeSheetEmployeeReportModel(entity) : null;
        }

        /// <summary>
        /// Get all by condition
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="departmentIds"></param>
        /// <param name="recordId"></param>
        /// <param name="reportId"></param>
        /// <param name="order"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<TimeSheetEmployeeReportModel> GetAll(string keyword, string departmentIds, int? recordId, int? reportId, string order, int? limit)
        {
            // init default condition
            var condition = @"1=1";

            // keyword
            if(!string.IsNullOrEmpty(keyword))
            {
                condition +=
                    " AND [RecordId] IN (SELECT Id FROM hr_Record WHERE [EmployeeCode] LIKE N'%{0}%' OR [FullName] LIKE N'%{0}%')"
                        .FormatWith(keyword.EscapeQuote());
            }

            // departments
            if(!string.IsNullOrEmpty(departmentIds))
            {
                condition += " AND [RecordId] IN (SELECT Id FROM hr_Record WHERE [DepartmentId] IN ({0}))".FormatWith(departmentIds);
            }

            // record
            if(recordId != null)
            {
                condition += " AND [RecordId] = {0}".FormatWith(recordId);
            }

            // group work shift
            if(reportId != null)
            {
                condition += " AND [ReportId] = {0}".FormatWith(reportId);
            }

            // return
            return hr_TimeSheetEmployeeReportServices.GetAll(condition, order, limit)
                .Select(er => new TimeSheetEmployeeReportModel(er)).ToList();
        }

        /// <summary>
        /// Get paging by condition
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="departmentIds"></param>
        /// <param name="recordId">record id</param>
        /// <param name="groupWorkShiftId">group work shift id</param>
        /// <param name="order">sort by</param>
        /// <param name="start">start</param>
        /// <param name="limit">limit</param>
        /// <returns></returns>
        public static PageResult<TimeSheetEmployeeReportModel> GetPaging(string keyword, string departmentIds, int? recordId, int? groupWorkShiftId, string order,int start, int limit)
        {
            // init default condition
            var condition = @"1=1";

            // keyword
            if(!string.IsNullOrEmpty(keyword))
            {
                condition +=
                    " AND [RecordId] IN (SELECT Id FROM hr_Record WHERE [EmployeeCode] LIKE N'%{0}%' OR [FullName] LIKE N'%{0}%')"
                        .FormatWith(keyword.EscapeQuote());
            }

            // departments
            if(!string.IsNullOrEmpty(departmentIds))
            {
                condition += " AND [RecordId] IN (SELECT Id FROM hr_Record WHERE [DepartmentId] IN ({0}))".FormatWith(departmentIds);
            }

            // record
            if(recordId != null)
            {
                condition += " AND [RecordId] = {0}".FormatWith(recordId);
            }

            // group work shift
            if(groupWorkShiftId != null)
            {
                condition += " AND [GroupWorkShiftId] = {0}".FormatWith(groupWorkShiftId);
            }

            // get result
            var result = hr_TimeSheetEmployeeReportServices.GetPaging(condition, order, start, limit);

            // return
            return new PageResult<TimeSheetEmployeeReportModel>(result.Total, 
                result.Data.Select(er => new TimeSheetEmployeeReportModel(er)).ToList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static TimeSheetEmployeeReportModel Create(TimeSheetEmployeeReportModel model)
        {
            // init entity
            var entity = new hr_TimeSheetEmployeeReport();

            // fill entity
            model.FillEntity(ref entity);

            // create
            return new TimeSheetEmployeeReportModel(hr_TimeSheetEmployeeReportServices.Create(entity));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static TimeSheetEmployeeReportModel Update(TimeSheetEmployeeReportModel model)
        {
            // int entity
            var entity = new hr_TimeSheetEmployeeReport();

            // fill entity
            model.FillEntity(ref entity);

            // update
            return new TimeSheetEmployeeReportModel(hr_TimeSheetEmployeeReportServices.Update(entity));
        }

        /// <summary>
        /// Delete by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static void Delete(int id)
        {
            hr_TimeSheetEmployeeReportServices.Delete(id);
        }

        /// <summary>
        /// Delete by condition
        /// </summary>
        /// <param name="recordId"></param>
        /// <param name="reportId"></param>
        /// <returns></returns>
        public static void Delete(int? recordId, int? reportId)
        {
            // init condition
            var condition = "1=1";

            // record
            if (recordId != null)
            {
                condition += " AND [RecordId] = {0}".FormatWith(recordId);
            }

            // group work shift
            if (reportId != null)
            {
                condition += " AND [ReportId] = {0}".FormatWith(reportId);
            }

            // delete
            hr_TimeSheetEmployeeReportServices.Delete(condition);
        }
    }
}
