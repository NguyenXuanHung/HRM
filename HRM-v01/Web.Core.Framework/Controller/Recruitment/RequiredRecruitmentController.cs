using System.Collections.Generic;
using System.Linq;
using Web.Core.Framework.Common;
using Web.Core.Object.Recruitment;
using Web.Core.Service.Recruitment;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for RequiredRecruitmentController
    /// </summary>
    public class RequiredRecruitmentController
    {
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static RequiredRecruitmentModel GetById(int id)
        {
            // get entity
            var entity = rec_RequiredRecruitmentServices.GetById(id);    
            
            // return
            return entity != null ? new RequiredRecruitmentModel(entity) : null;
        }

        /// <summary>
        /// Get all by condition
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="jobTitlePositionId"></param>
        /// <param name="positionId"></param>
        /// <param name="departmentId"></param>
        /// <param name="educationId"></param>
        /// <param name="isDeleted"></param>
        /// <param name="status"></param>
        /// <param name="experienceType"></param>
        /// <param name="workingFormType"></param>
        /// <param name="order"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<RequiredRecruitmentModel> GetAll(string keyword, int? jobTitlePositionId,
            int? positionId, int? departmentId, int? educationId, bool? isDeleted, RecruitmentStatus? status,
            ExperienceType? experienceType, WorkingFormType? workingFormType, string order, int? limit)
        {
            // init default condition
            var condition = Constant.ConditionDefault;

            // keyword
            if (!string.IsNullOrEmpty(keyword))
            {
                condition += @" AND ([Code] LIKE N'%{0}%' OR [Name] LIKE N'%{0}%' OR [Description] LIKE N'%{0}%')".FormatWith(keyword.EscapeQuote());
            }

            if (jobTitlePositionId != null)
            {
                condition += " AND [JobTitlePositionId] = {0}".FormatWith(jobTitlePositionId);
            }
            if (positionId != null)
            {
                condition += " AND [PositionId] = {0}".FormatWith(positionId);
            }
            if (educationId != null)
            {
                condition += " AND [EducationId] = {0}".FormatWith(educationId);
            }
            if (departmentId != null)
            {
                condition += " AND [DepartmentId] = {0}".FormatWith(departmentId);
            }

            // is deleted
            if (isDeleted != null)
            {
                condition += " AND [IsDeleted] = {0}".FormatWith((bool)isDeleted ? "1" : "0");
            }

            if (status != null)
            {
                condition += " AND [Status] = {0}".FormatWith((int)status);
            }

            if (experienceType != null)
            {
                condition += " AND [ExperienceId] = {0}".FormatWith((int)experienceType);
            }
            if (workingFormType != null)
            {
                condition += " AND [WorkFormId] = {0}".FormatWith((int)workingFormType);
            }

            // return
            return rec_RequiredRecruitmentServices.GetAll(condition, order, limit).Select(c => new RequiredRecruitmentModel(c)).ToList();
        }

        /// <summary>
        /// Get paging by condition
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="jobTitlePositionId"></param>
        /// <param name="positionId"></param>
        /// <param name="departmentId"></param>
        /// <param name="educationId"></param>
        /// <param name="isDeleted"></param>
        /// <param name="status"></param>
        /// <param name="experienceType"></param>
        /// <param name="workingFormType"></param>
        /// <param name="order"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static PageResult<RequiredRecruitmentModel> GetPaging(string keyword, int? jobTitlePositionId,
            int? positionId, int? departmentId, int? educationId, bool? isDeleted, RecruitmentStatus? status,
            ExperienceType? experienceType, WorkingFormType? workingFormType, string order, int start, int limit)
        {
            // init default condition
            var condition = Constant.ConditionDefault;

            // keyword
            if(!string.IsNullOrEmpty(keyword))
            {
                condition += @" AND ([Code] LIKE N'%{0}%' OR [Name] LIKE N'%{0}%' OR [Description] LIKE N'%{0}%')".FormatWith(keyword.EscapeQuote());
            }

            if (jobTitlePositionId != null)
            {
                condition += " AND [JobTitlePositionId] = {0}".FormatWith(jobTitlePositionId);
            }
            if (positionId != null)
            {
                condition += " AND [PositionId] = {0}".FormatWith(positionId);
            }
            if (educationId != null)
            {
                condition += " AND [EducationId] = {0}".FormatWith(educationId);
            }
            if (departmentId != null)
            {
                condition += " AND [DepartmentId] = {0}".FormatWith(departmentId);
            }

            // is deleted
            if (isDeleted != null)
            {
                condition += " AND [IsDeleted] = {0}".FormatWith((bool)isDeleted ? "1" : "0");
            }

            if (status != null)
            {
                condition += " AND [Status] = {0}".FormatWith((int)status);
            }

            if (experienceType != null)
            {
                condition += " AND [ExperienceId] = {0}".FormatWith((int)experienceType);
            }
            if (workingFormType != null)
            {
                condition += " AND [WorkFormId] = {0}".FormatWith((int)workingFormType);
            }

            // get result
            var result = rec_RequiredRecruitmentServices.GetPaging(condition, order, start, limit);

            // return
            return new PageResult<RequiredRecruitmentModel>(result.Total, result.Data.Select(c => new RequiredRecruitmentModel(c)).ToList());
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static RequiredRecruitmentModel Create(RequiredRecruitmentModel model)
        {
            // init entity
            var entity = new rec_RequiredRecruitment();

            // fill entity
            model.FillEntity(ref entity);

            // create
            return new RequiredRecruitmentModel(rec_RequiredRecruitmentServices.Create(entity));
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static RequiredRecruitmentModel Update(RequiredRecruitmentModel model)
        {
            // int entity
            var entity = new rec_RequiredRecruitment();

            // fill entity
            model.FillEntity(ref entity);

            // update
            return new RequiredRecruitmentModel(rec_RequiredRecruitmentServices.Update(entity));
        }

        /// <summary>
        /// Update deleted status
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static RequiredRecruitmentModel Delete(int id)
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
