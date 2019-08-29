using System;
using System.Collections.Generic;
using System.Linq;
using Web.Core.Framework.Common;
using Web.Core.Object.Recruitment;
using Web.Core.Service.Recruitment;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for InterviewController
    /// </summary>
    public class InterviewController
    {
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static InterviewModel GetById(int id)
        {
            // get entity
            var entity = rec_InterviewServices.GetById(id);    
            
            // return
            return entity != null ? new InterviewModel(entity) : null;
        }

        /// <summary>
        /// Get all by condition
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="requiredRecruitmentId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="isDeleted"></param>
        /// <param name="order"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<InterviewModel> GetAll(string keyword, int? requiredRecruitmentId,
            DateTime? fromDate, DateTime? toDate, bool? isDeleted,
            string order, int? limit)
        {
            var condition = Condition(keyword, requiredRecruitmentId, fromDate, toDate, isDeleted);

            // return
            return rec_InterviewServices.GetAll(condition, order, limit).Select(c => new InterviewModel(c)).ToList();
        }

        /// <summary>
        /// Get paging by condition
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="requiredRecruitmentId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="isDeleted"></param>
        /// <param name="order"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static PageResult<InterviewModel> GetPaging(string keyword, int? requiredRecruitmentId,
            DateTime? fromDate, DateTime? toDate, bool? isDeleted,
            string order, int start, int limit)
        {
            var condition = Condition(keyword, requiredRecruitmentId, fromDate, toDate, isDeleted);

            // get result
            var result = rec_InterviewServices.GetPaging(condition, order, start, limit);

            // return
            return new PageResult<InterviewModel>(result.Total, result.Data.Select(c => new InterviewModel(c)).ToList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="requiredRecruitmentId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="isDeleted"></param>
        /// <returns></returns>
        private static string Condition(string keyword, int? requiredRecruitmentId, DateTime? fromDate, DateTime? toDate,
            bool? isDeleted)
        {
            // init default condition
            var condition = Constant.ConditionDefault;

            // keyword
            if (!string.IsNullOrEmpty(keyword))
            {
                condition += @" AND ([Name] LIKE N'%{0}%' OR [Interviewer] LIKE N'%{0}%')".FormatWith(keyword.EscapeQuote());
            }

            if (requiredRecruitmentId != null)
            {
                condition += " AND [RequiredRecruitmentId] = {0}".FormatWith(requiredRecruitmentId);
            }

            if (fromDate.HasValue)
            {
                condition += " AND [InterviewDate] >= '{0}'".FormatWith(fromDate.Value.ToString("yyyy-MM-dd"));
            }

            if (toDate.HasValue)
            {
                condition += " AND [InterviewDate] <= '{0}'".FormatWith(toDate.Value.AddDays(1).ToString("yyyy-MM-dd"));
            }

            // is deleted
            if (isDeleted != null)
            {
                condition += " AND [IsDeleted] = {0}".FormatWith((bool) isDeleted ? "1" : "0");
            }

            return condition;
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static InterviewModel Create(InterviewModel model)
        {
            // init entity
            var entity = new rec_Interview();

            // fill entity
            model.FillEntity(ref entity);

            // create
            return new InterviewModel(rec_InterviewServices.Create(entity));
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static InterviewModel Update(InterviewModel model)
        {
            // int entity
            var entity = new rec_Interview();

            // fill entity
            model.FillEntity(ref entity);

            // update
            return new InterviewModel(rec_InterviewServices.Update(entity));
        }

        /// <summary>
        /// Update deleted status
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static InterviewModel Delete(int id)
        {
            // get model
            var model = GetById(id);

            // check existed
            if(model != null)
            {
                // set deleted status
                model.IsDeleted = true;

                // update
                return Update(model);
            }

            // no record found
            return null;
        }
    }
}
