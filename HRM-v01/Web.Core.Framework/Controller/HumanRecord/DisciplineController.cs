using System;
using System.Collections.Generic;
using System.Linq;
using Web.Core.Framework.Common;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.HumanRecord;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for DisciplineController
    /// </summary>
    public class DisciplineController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DisciplineModel GetById(int id)
        {
            var recordDiscipline = hr_DisciplineServices.GetById(id);
            return new DisciplineModel(recordDiscipline);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="suffix"></param>
        /// <returns></returns>
        public static DisciplineModel GetDisciplineNumberByCondition(string suffix)
        {
            var condition = @" [DecisionNumber] LIKE  N'%{0}%' ".FormatWith(suffix);
            var order = " [DecisionNumber] DESC ";
            var record = hr_DisciplineServices.GetByCondition(condition, order);
            return new DisciplineModel(record);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="departmentIds"></param>
        /// <param name="recordId"></param>
        /// <param name="levelId"></param>
        /// <param name="formDisciplineId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="order"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<DisciplineModel> GetAll(string keyword, string departmentIds, int? recordId, int? levelId, int? formDisciplineId,
            DateTime? fromDate, DateTime? toDate, string order, int? limit)
        {
            var condition = Condition(keyword, departmentIds, recordId, levelId, formDisciplineId, fromDate, toDate);

            return hr_DisciplineServices.GetAll(condition, order, limit).Select(c => new DisciplineModel(c)).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="departmentIds"></param>
        /// <param name="recordId"></param>
        /// <param name="levelId"></param>
        /// <param name="formDisciplineId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="order"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static PageResult<DisciplineModel> GetPaging(string keyword, string departmentIds, int? recordId, int? levelId, int? formDisciplineId,
            DateTime? fromDate, DateTime? toDate, string order, int start, int limit)
        {
            var condition = Condition(keyword, departmentIds, recordId, levelId, formDisciplineId, fromDate, toDate);

            var pageResult = hr_DisciplineServices.GetPaging(condition, order, start, limit);

            return new PageResult<DisciplineModel>(pageResult.Total, pageResult.Data.Select(r => new DisciplineModel(r)).ToList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="departmentIds"></param>
        /// <param name="recordId"></param>
        /// <param name="levelId"></param>
        /// <param name="formDisciplineId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        private static string Condition(string keyword, string departmentIds, int? recordId, int? levelId, int? formDisciplineId, DateTime? fromDate, DateTime? toDate)
        {
            var condition = Constant.ConditionDefault;

            if (!string.IsNullOrEmpty(keyword))
                condition +=
                    " AND [RecordId] IN (SELECT Id FROM hr_Record WHERE [EmployeeCode] LIKE N'%{0}%' OR [FullName] LIKE N'%{0}%')"
                        .FormatWith(keyword.GetKeyword());
            if (!string.IsNullOrEmpty(departmentIds))
            {
                condition += @" AND ([RecordId] IN (SELECT Id FROM hr_Record WHERE [DepartmentId] IN ({0})))".FormatWith(departmentIds);
            }

            if (recordId != null)
                condition += " AND [RecordId] = {0}".FormatWith(recordId);

            if (levelId != null)
                condition += " AND [LevelDisciplineId] = {0}".FormatWith(levelId);

            if (formDisciplineId != null)
                condition += " AND [FormDisciplineId] = {0}".FormatWith(formDisciplineId);

            if (fromDate.HasValue)
            {
                condition += " AND [StartDate] >= '{0}'".FormatWith(fromDate.Value.ToString("yyyy-MM-dd"));
            }

            if (toDate.HasValue)
            {
                condition += " AND [StartDate] <= '{0}'".FormatWith(toDate.Value.AddDays(1).ToString("yyyy-MM-dd"));
            }

            return condition;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DisciplineModel Update(DisciplineModel model)
        {
            // int entity
            var entity = hr_DisciplineServices.GetById(model.Id);
            if (entity == null) return null;

            // fill entity
            model.FillEntity(ref entity);

            // update
            return new DisciplineModel(hr_DisciplineServices.Update(entity));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DisciplineModel Create(DisciplineModel model)
        {
            // init entity
            var entity = new hr_Discipline();

            // fill entity
            model.FillEntity(ref entity);

            // create
            return new DisciplineModel(hr_DisciplineServices.Create(entity));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordId"></param>
        public void Delete(int recordId)
        {
            hr_DisciplineServices.Delete(recordId);
        }
    }
}