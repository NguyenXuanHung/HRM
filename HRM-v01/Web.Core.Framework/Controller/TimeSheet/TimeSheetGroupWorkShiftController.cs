using System.Collections.Generic;
using System.Linq;
using Web.Core.Framework.Common;
using Web.Core.Object.TimeSheet;
using Web.Core.Service.TimeSheet;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for TimeSheetGroupWorkShiftController
    /// </summary>
    public class TimeSheetGroupWorkShiftController
    {
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static TimeSheetGroupWorkShiftModel GetById(int id)
        {
            // get entity
            var entity = hr_TimeSheetGroupWorkShiftServices.GetById(id);    
            
            // return
            return entity != null ? new TimeSheetGroupWorkShiftModel(entity) : null;
        }

        /// <summary>
        /// Get all by condition
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="isDeleted"></param>
        /// <param name="order"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<TimeSheetGroupWorkShiftModel> GetAll(string keyword, bool? isDeleted, string order, int? limit)
        {
            // init default condition
            var condition = Constant.ConditionDefault;

            // keyword
            if(!string.IsNullOrEmpty(keyword))
            {
                condition += @" AND ([Name] LIKE N'%{0}%' OR [Description] LIKE N'%{0}%')".FormatWith(keyword.EscapeQuote());
            }

            // is deleted
            if(isDeleted != null)
            {
                condition += " AND [IsDeleted] = {0}".FormatWith((bool)isDeleted ? "1" : "0");
            }

            // return
            return hr_TimeSheetGroupWorkShiftServices.GetAll(condition, order, limit)
                .Select(gws => new TimeSheetGroupWorkShiftModel(gws)).ToList();
        }

        /// <summary>
        /// Get paging by condition
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="isDeleted"></param>
        /// <param name="order"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static PageResult<TimeSheetGroupWorkShiftModel> GetPaging(string keyword, bool? isDeleted, string order, int start, int limit)
        {
            // init default condition
            var condition = Constant.ConditionDefault;

            // keyword
            if(!string.IsNullOrEmpty(keyword))
            {
                condition += @" AND ([Name] LIKE N'%{0}%' OR [Description] LIKE N'%{0}%')".FormatWith(keyword.EscapeQuote());
            }

            // is deleted
            if(isDeleted != null)
            {
                condition += " AND [IsDeleted] = {0}".FormatWith((bool)isDeleted ? "1" : "0");
            }

            // get result
            var result = hr_TimeSheetGroupWorkShiftServices.GetPaging(condition, order, start, limit);

            // return
            return new PageResult<TimeSheetGroupWorkShiftModel>(result.Total, result.Data.Select(r => new TimeSheetGroupWorkShiftModel(r)).ToList());
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static TimeSheetGroupWorkShiftModel Create(TimeSheetGroupWorkShiftModel model)
        {
            // init entity
            var entity = new hr_TimeSheetGroupWorkShift();

            // fill entity
            model.FillEntity(ref entity);

            // create
            return new TimeSheetGroupWorkShiftModel(hr_TimeSheetGroupWorkShiftServices.Create(entity));
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static TimeSheetGroupWorkShiftModel Update(TimeSheetGroupWorkShiftModel model)
        {
            // int entity
            var entity = new hr_TimeSheetGroupWorkShift();

            // fill entity
            model.FillEntity(ref entity);

            // update
            return new TimeSheetGroupWorkShiftModel(hr_TimeSheetGroupWorkShiftServices.Update(entity));
        }

        /// <summary>
        /// Update deleted status
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static TimeSheetGroupWorkShiftModel Delete(int id)
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
