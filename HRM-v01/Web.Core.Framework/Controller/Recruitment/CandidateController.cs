using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Core.Framework.Common;
using Web.Core.Object;
using Web.Core.Service.Recruitment;

namespace Web.Core.Framework
{
    public class CandidateController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="recordIds"></param>
        /// <param name="requiredRecruitmentId"></param>
        /// <param name="status"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="isDeleted"></param>
        /// <param name="order"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<CandidateModel> GetAll(string keyword, string recordIds, int? requiredRecruitmentId,
            CandidateType? status, DateTime? fromDate, DateTime? toDate, bool? isDeleted, string order, int? limit)
        {
            var condition = Condition(keyword, recordIds, requiredRecruitmentId, status, fromDate, toDate, isDeleted);

            return rec_CandidateServices.GetAll(condition, order, limit).Select(c => new CandidateModel(c)).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="recordIds"></param>
        /// <param name="requiredRecruitmentId"></param>
        /// <param name="status"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="isDeleted"></param>
        /// <param name="order"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static PageResult<CandidateModel> GetPaging(string keyword, string recordIds, int? requiredRecruitmentId,
            CandidateType? status, DateTime? fromDate, DateTime? toDate, bool? isDeleted, string order, int start, int limit)
        {
            var condition = Condition(keyword, recordIds, requiredRecruitmentId, status, fromDate, toDate, isDeleted);

            var pageResult = rec_CandidateServices.GetPaging(condition, order, start, limit);

            return new PageResult<CandidateModel>(pageResult.Total, pageResult.Data.Select(r => new CandidateModel(r)).ToList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static CandidateModel GetById(int id)
        {
            var entity = rec_CandidateServices.GetById(id);

            return entity != null ? new CandidateModel(entity) : null;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static CandidateModel GetByCode(string code, CandidateType? status)
        {
            if (string.IsNullOrEmpty(code)) return null;
            var condition = " [Code] = N'{0}' ".FormatWith(code);
            if (status != null)
            {
                condition += " AND [Status] = {0}".FormatWith((int)status);
            }
            
            var entity = rec_CandidateServices.GetByCondition(condition);

            return entity != null ? new CandidateModel(entity) : null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static CandidateModel Create(CandidateModel model)
        {
            var entity = new rec_Candidate();
            model.FillEntity(ref entity);
            return new CandidateModel(rec_CandidateServices.Create(entity));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static CandidateModel Update(CandidateModel model)
        {
            var entity = rec_CandidateServices.GetById(model.Id);
            if (entity == null) return null;
            model.FillEntity(ref entity);
            return new CandidateModel(rec_CandidateServices.Update(entity));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static CandidateModel Delete(int id)
        {
            var model = GetById(id);
            if(model == null) return null;
            model.IsDeleted = true;
            return Update(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="recordIds"></param>
        /// <param name="requiredRecruitmentId"></param>
        /// <param name="status"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="isDeleted"></param>
        /// <returns></returns>
        private static string Condition(string keyword, string recordIds, int? requiredRecruitmentId,
            CandidateType? status, DateTime? fromDate, DateTime? toDate, bool? isDeleted)
        {
            var condition = Constant.ConditionDefault;

            if(!string.IsNullOrEmpty(keyword))
                condition += "AND ([Code] LIKE N'%{0}%' OR (SELECT [FullName] FROM hr_Record rc WHERE rc.Id = rec_Candidate.RecordId) LIKE N'%{0}%')".FormatWith(keyword);

            if(!string.IsNullOrEmpty(recordIds))
                condition += " AND [RecordId] IN ({0})".FormatWith(recordIds);

            if(requiredRecruitmentId != null)
                condition += " AND [RequiredRecruitmentId] = {0}".FormatWith(requiredRecruitmentId);

            if(status != null)
                condition += " AND [Status] = {0}".FormatWith((int)status);

            if(fromDate != null)
                condition +=
                    " AND [ApplyDate] IS NOT NULL AND YEAR([ApplyDate]) = {0} AND MONTH([ApplyDate]) >= {1} AND DAY([ApplyDate]) >= {2}"
                        .FormatWith(fromDate.Value.Year, fromDate.Value.Month, fromDate.Value.Day);

            if(toDate != null)
                condition +=
                    " AND [ApplyDate] IS NOT NULL AND YEAR([ApplyDate]) = {0} AND MONTH([ApplyDate]) <= {1} AND DAY([ApplyDate]) <= {2}"
                        .FormatWith(toDate.Value.Year, toDate.Value.Month, toDate.Value.Day);

            if(isDeleted != null)
                condition += " AND [IsDeleted] = {0}".FormatWith((bool)isDeleted ? 1 : 0);

            return condition;
        }
    }
}
