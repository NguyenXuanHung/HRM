using System.Collections.Generic;
using System.Linq;
using Web.Core.Framework.Common;
using Web.Core.Object.Recruitment;
using Web.Core.Service.Recruitment;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for CandidateInterviewController
    /// </summary>
    public class CandidateInterviewController
    {
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static CandidateInterviewModel GetById(int id)
        {
            // get entity
            var entity = rec_CandidateInterviewServices.GetById(id);    
            
            // return
            return entity != null ? new CandidateInterviewModel(entity) : null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="interviewId"></param>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public static CandidateInterviewModel GetUnique(int interviewId, int recordId)
        {
            var condition = " [InterviewId] = {0}".FormatWith(interviewId) +
                            " AND [RecordId] = {0}".FormatWith(recordId);
            // get entity
            var entity = rec_CandidateInterviewServices.GetByCondition(condition);    
            
            // return
            return entity != null ? new CandidateInterviewModel(entity) : null;
        }

        /// <summary>
        /// Get all by condition
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="interviewId"></param>
        /// <param name="recordId"></param>
        /// <param name="isDeleted"></param>
        /// <param name="order"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<CandidateInterviewModel> GetAll(string keyword, int? interviewId, int? recordId, bool? isDeleted,
            string order, int? limit)
        {
            var condition = Condition(keyword, interviewId, recordId, isDeleted);

            // return
            return rec_CandidateInterviewServices.GetAll(condition, order, limit).Select(c => new CandidateInterviewModel(c)).ToList();
        }

        /// <summary>
        /// Get paging by condition
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="interviewId"></param>
        /// <param name="recordId"></param>
        /// <param name="isDeleted"></param>
        /// <param name="order"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static PageResult<CandidateInterviewModel> GetPaging(string keyword, int? interviewId, int? recordId, bool? isDeleted,
            string order, int start, int limit)
        {
            var condition = Condition(keyword, interviewId, recordId, isDeleted);

            // get result
            var result = rec_CandidateInterviewServices.GetPaging(condition, order, start, limit);

            // return
            return new PageResult<CandidateInterviewModel>(result.Total, result.Data.Select(c => new CandidateInterviewModel(c)).ToList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="interviewId"></param>
        /// <param name="recordId"></param>
        /// <param name="isDeleted"></param>
        /// <returns></returns>
        private static string Condition(string keyword, int? interviewId, int? recordId , bool? isDeleted)
        {
            // init default condition
            var condition = Constant.ConditionDefault;

            // keyword
            if (!string.IsNullOrEmpty(keyword))
            {
                condition += @" AND [InterviewId] IN ( SELECT ri.Id FROM rec_Interview ri WHERE ri.[Name] LIKE N'%{0}%' )".FormatWith(keyword.EscapeQuote());
            }

            if (interviewId != null)
            {
                condition += " AND [InterviewId] = {0}".FormatWith(interviewId);
            }

            if (recordId != null)
            {
                condition += " AND [RecordId] = {0}".FormatWith(recordId);
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
        public static CandidateInterviewModel Create(CandidateInterviewModel model)
        {
            // init entity
            var entity = new rec_CandidateInterview();

            // fill entity
            model.FillEntity(ref entity);

            // create
            return new CandidateInterviewModel(rec_CandidateInterviewServices.Create(entity));
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static CandidateInterviewModel Update(CandidateInterviewModel model)
        {
            // int entity
            var entity = new rec_CandidateInterview();

            // fill entity
            model.FillEntity(ref entity);

            // update
            return new CandidateInterviewModel(rec_CandidateInterviewServices.Update(entity));
        }

        /// <summary>
        /// Update deleted status
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static CandidateInterviewModel Delete(int id)
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

        /// <summary>
        /// delete
        /// </summary>
        /// <param name="interviewId"></param>
        public static void DeleteByCondition(int interviewId)
        {
            var condition = " [InterviewId] = {0}".FormatWith(interviewId);
            //delete
            rec_CandidateInterviewServices.Delete(condition);
        }
    }
}
