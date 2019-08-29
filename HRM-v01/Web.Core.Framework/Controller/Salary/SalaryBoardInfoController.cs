using System.Collections.Generic;
using System.Linq;
using Web.Core.Framework.Common;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.HumanRecord;

namespace Web.Core.Framework.Controller
{
    public class SalaryBoardInfoController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="salaryBoardId"></param>
        /// <param name="order"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<SalaryBoardInfoModel> GetAll(string keyword, string salaryBoardId, string order, int? limit)
        {
            var condition = Constant.ConditionDefault;

            if (!string.IsNullOrEmpty(keyword))
                condition += " AND ((SELECT rc.[FullName] FROM hr_Record rc WHERE rc.Id = hr_SalaryBoardInfo.RecordId) LIKE N'%{0}%'".FormatWith(keyword)
                    + " OR (SELECT rc.[EmployeeCode] FROM hr_Record rc WHERE rc.Id = hr_SalaryBoardInfo.RecordId) LIKE N'%{0}%')".FormatWith(keyword);

            if (!string.IsNullOrEmpty(salaryBoardId))
                condition += " AND [SalaryBoardId] = '{0}'".FormatWith(salaryBoardId);

            return hr_SalaryBoardInfoServices.GetAll(condition, order, limit)
                .Select(r => new SalaryBoardInfoModel(r)).ToList();
        }

        public static PageResult<SalaryBoardInfoModel> GetPaging(string keyword, string salaryBoardId, string order,
            int start, int limit)
        {
            var condition = Constant.ConditionDefault;

            if (!string.IsNullOrEmpty(keyword))
                condition += " AND ((SELECT rc.[FullName] FROM hr_Record rc WHERE rc.Id = hr_SalaryBoardInfo.RecordId) LIKE N'%{0}%'".FormatWith(keyword)
                             + " OR (SELECT rc.[EmployeeCode] FROM hr_Record rc WHERE rc.Id = hr_SalaryBoardInfo.RecordId) LIKE N'%{0}%')".FormatWith(keyword);

            if (!string.IsNullOrEmpty(salaryBoardId))
                condition += " AND [SalaryBoardId] = '{0}'".FormatWith(salaryBoardId);

            var result = hr_SalaryBoardInfoServices.GetPaging(condition, order, start, limit);

            return new PageResult<SalaryBoardInfoModel>(result.Total, result.Data.Select(r => new SalaryBoardInfoModel(r)).ToList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="boardListId"></param>
        public static void DeleteByBoardListId(int? boardListId)
        {
            var condition = Constant.ConditionDefault;
            if (boardListId != null)
            {
                condition += " AND [SalaryBoardId] = '{0}' ".FormatWith(boardListId);
            }

            hr_SalaryBoardInfoServices.Delete(condition);
        }

        /// <summary>
        /// Get payroll by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static SalaryBoardInfoModel GetById(int id)
        {
            var entity = hr_SalaryBoardInfoServices.GetById(id);
            return entity != null ? new SalaryBoardInfoModel(entity) : null;
        }

        /// <summary>
        /// Create payroll
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static SalaryBoardInfoModel Create(SalaryBoardInfoModel model)
        {
            // init new entity
            var entity = new hr_SalaryBoardInfo();
            // set entity props
            model.FillEntity(ref entity);
            // insert
            return new SalaryBoardInfoModel(hr_SalaryBoardInfoServices.Create(entity));
        }

        /// <summary>
        /// Update payroll
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static SalaryBoardInfoModel Update(SalaryBoardInfoModel model)
        {
            var entity = hr_SalaryBoardInfoServices.GetById(model.Id);
            if (entity == null) return null;
            // set entity props
            model.FillEntity(ref entity);
            // update
            return new SalaryBoardInfoModel(hr_SalaryBoardInfoServices.Update(entity));

        }

        /// <summary>
        /// Change to deleted
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static void  Delete(int id)
        {
            // get payroll by id
            var model = GetById(id);
            // check result
            if (model != null)
            {
                // delete
                hr_SalaryBoardInfoServices.Delete(id);
            }
        }

    }
}
