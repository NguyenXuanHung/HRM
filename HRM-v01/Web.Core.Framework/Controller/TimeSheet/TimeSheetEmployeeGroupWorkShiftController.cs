using System.Collections.Generic;
using System.Linq;
using Web.Core.Framework.Common;
using Web.Core.Framework.Model.TimeSheet;
using Web.Core.Object.TimeSheet;
using Web.Core.Service.TimeSheet;

namespace Web.Core.Framework.Controller.TimeSheet
{
    public class TimeSheetEmployeeGroupWorkShiftController
    {
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static TimeSheetEmployeeGroupWorkShiftModel GetById(int id)
        {
            // get entity
            var entity = hr_TimeSheetEmployeeGroupWorkShiftServices.GetById(id);

            // return
            return entity != null ? new TimeSheetEmployeeGroupWorkShiftModel(entity) : null;
        }

        /// <summary>
        /// Get unique
        /// </summary>
        /// <param name="recordId"></param>
        /// <param name="groupWorkShiftId"></param>
        /// <returns></returns>
        public static TimeSheetEmployeeGroupWorkShiftModel GetUnique(int recordId, int groupWorkShiftId)
        {
            // init condition
            var condition = @"[RecordId] = {0} AND [GroupWorkShiftId] = {0}".FormatWith(recordId, groupWorkShiftId);

            // get entity
            var entity = hr_TimeSheetEmployeeGroupWorkShiftServices.GetByCondition(condition);

            // return
            return entity != null ? new TimeSheetEmployeeGroupWorkShiftModel(entity) : null;
        }

        /// <summary>
        /// Get all by condition
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="departmentIds"></param>
        /// <param name="recordId"></param>
        /// <param name="groupWorkShiftId"></param>
        /// <param name="order"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<TimeSheetEmployeeGroupWorkShiftModel> GetAll(string keyword, string departmentIds, int? recordId, int? groupWorkShiftId, string order, int? limit)
        {
            var condition = GetCondition(keyword, departmentIds, recordId, groupWorkShiftId);

            // return
            return hr_TimeSheetEmployeeGroupWorkShiftServices.GetAll(condition, order, limit)
                .Select(egws => new TimeSheetEmployeeGroupWorkShiftModel(egws)).ToList();
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
        public static PageResult<TimeSheetEmployeeGroupWorkShiftModel> GetPaging(string keyword, string departmentIds, int? recordId, int? groupWorkShiftId, string order,int start, int limit)
        {
            var condition = GetCondition(keyword, departmentIds, recordId, groupWorkShiftId);

            // get result
            var result = hr_TimeSheetEmployeeGroupWorkShiftServices.GetPaging(condition, order, start, limit);

            // return
            return new PageResult<TimeSheetEmployeeGroupWorkShiftModel>(result.Total, 
                result.Data.Select(egws => new TimeSheetEmployeeGroupWorkShiftModel(egws)).ToList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="departmentIds"></param>
        /// <param name="recordId"></param>
        /// <param name="groupWorkShiftId"></param>
        /// <returns></returns>
        private static string GetCondition(string keyword, string departmentIds, int? recordId, int? groupWorkShiftId)
        {
            // init default condition
            var condition = Constant.ConditionDefault;

            // keyword
            if (!string.IsNullOrEmpty(keyword))
            {
                condition +=
                    " AND [RecordId] IN (SELECT Id FROM hr_Record WHERE [EmployeeCode] LIKE N'%{0}%' OR [FullName] LIKE N'%{0}%')"
                        .FormatWith(keyword.GetKeyword());
            }

            // departments
            if (!string.IsNullOrEmpty(departmentIds))
            {
                condition +=
                    " AND [RecordId] IN (SELECT Id FROM hr_Record WHERE [DepartmentId] IN ({0}))".FormatWith(departmentIds);
            }

            // record
            if (recordId != null)
            {
                condition += " AND [RecordId] = {0}".FormatWith(recordId);
            }

            // group work shift
            if (groupWorkShiftId != null)
            {
                condition += " AND [GroupWorkShiftId] = {0}".FormatWith(groupWorkShiftId);
            }

            return condition;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static TimeSheetEmployeeGroupWorkShiftModel Create(TimeSheetEmployeeGroupWorkShiftModel model)
        {
            // init entity
            var entity = new hr_TimeSheetEmployeeGroupWorkShift();

            // fill entity
            model.FillEntity(ref entity);

            // create
            return new TimeSheetEmployeeGroupWorkShiftModel(hr_TimeSheetEmployeeGroupWorkShiftServices.Create(entity));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static TimeSheetEmployeeGroupWorkShiftModel Update(TimeSheetEmployeeGroupWorkShiftModel model)
        {
            // int entity
            var entity = new hr_TimeSheetEmployeeGroupWorkShift();

            // fill entity
            model.FillEntity(ref entity);

            // update
            return new TimeSheetEmployeeGroupWorkShiftModel(hr_TimeSheetEmployeeGroupWorkShiftServices.Update(entity));
        }

        /// <summary>
        /// Delete by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static void Delete(int id)
        {
            hr_TimeSheetEmployeeGroupWorkShiftServices.Delete(id);
        }

        /// <summary>
        /// Delete by condition
        /// </summary>
        /// <param name="recordId"></param>
        /// <param name="groupWorkShiftId"></param>
        /// <returns></returns>
        public static void Delete(int? recordId, int? groupWorkShiftId)
        {
            // init condition
            var condition = "1=1";

            // record
            if (recordId != null)
            {
                condition += " AND [RecordId] = {0}".FormatWith(recordId);
            }

            // group work shift
            if (groupWorkShiftId != null)
            {
                condition += " AND [GroupWorkShiftId] = {0}".FormatWith(groupWorkShiftId);
            }

            // delete
            hr_TimeSheetEmployeeGroupWorkShiftServices.Delete(condition);
        }
    }
}
