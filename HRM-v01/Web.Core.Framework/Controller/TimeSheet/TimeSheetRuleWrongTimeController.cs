using System.Collections.Generic;
using System.Linq;
using Web.Core.Framework.Common;
using Web.Core.Object.TimeSheet;
using Web.Core.Service.TimeSheet;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for TimeSheetRuleWrongTimeController
    /// </summary>
    public class TimeSheetRuleWrongTimeController
    {
       
        /// <summary>
        /// get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static TimeSheetRuleWrongTimeModel GetById(int id)
        {
            var timeSheetRuleEarlyOrLateModel = hr_TimeSheetRuleWrongTimeServices.GetById(id);            
            return timeSheetRuleEarlyOrLateModel != null
                ? new TimeSheetRuleWrongTimeModel(timeSheetRuleEarlyOrLateModel)
                : null;
        }


        /// <summary>
        /// Get all by condition
        /// </summary>
        /// <param name="isMinus"></param>
        /// <param name="type"></param>
        /// <param name="symbolId"></param>
        /// <param name="isDeleted"></param>
        /// <param name="order"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<TimeSheetRuleWrongTimeModel> GetAll(int? symbolId, bool? isMinus, TimeSheetRuleWrongTimeType? type, bool? isDeleted, string order, int? limit)
        {
            // init condition
            var condition = Constant.ConditionDefault;

            // symbol
            if(symbolId != null)
            {
                condition += " AND [SymbolId]='{0}'".FormatWith(symbolId);
            }

            // plus or minus
            if (isMinus != null)
            {
                condition += " AND [TypeAddMinute]='{0}'".FormatWith(isMinus.Value ? 1 : 0);
            }

            // type
            if (type != null)
            {
                condition += " AND [Type]='{0}'".FormatWith((int)type);
            }
            
            // deleted status
            if (isDeleted != null)
            {
                condition += " AND [IsDeleted]='{0}'".FormatWith(isDeleted.Value ? 1 : 0);
            }

            // return
            return hr_TimeSheetRuleWrongTimeServices.GetAll(condition, order, limit).Select(tsrwt => new TimeSheetRuleWrongTimeModel(tsrwt)).ToList();
        } 

        /// <summary>
        /// Get paging by condition
        /// </summary>
        /// <param name="symbolId"></param>
        /// <param name="isMinus"></param>
        /// <param name="type"></param>
        /// <param name="isDeleted"></param>
        /// <param name="order"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static PageResult<TimeSheetRuleWrongTimeModel> GetPaging(int? symbolId, bool? isMinus, TimeSheetRuleWrongTimeType? type, bool? isDeleted, string order, int start, int limit)
        {
            // init condition
            var condition = @"1=1";

            // symbol
            if(symbolId != null)
            {
                condition += " AND [SymbolId]='{0}'".FormatWith(symbolId);
            }

            // plus or minus
            if(isMinus != null)
            {
                condition += " AND [TypeAddMinute]='{0}'".FormatWith(isMinus.Value ? 1 : 0);
            }

            // type
            if(type != null)
            {
                condition += " AND [Type]='{0}'".FormatWith((int)type);
            }

            // deleted status
            if(isDeleted != null)
            {
                condition += " AND [IsDeleted]='{0}'".FormatWith(isDeleted.Value ? 1 : 0);
            }

            // get result
            var result = hr_TimeSheetRuleWrongTimeServices.GetPaging(condition, order, start, limit);

            // return 
            return new PageResult<TimeSheetRuleWrongTimeModel>(result.Total, result.Data.Select(tsrwt => new TimeSheetRuleWrongTimeModel(tsrwt)).ToList());
        }

        /// <summary>
        /// create
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static TimeSheetRuleWrongTimeModel Create(TimeSheetRuleWrongTimeModel model)
        {
            var entity = new hr_TimeSheetRuleWrongTime();
            //fill
            model.FillEntity(ref entity);

            //create
            return new TimeSheetRuleWrongTimeModel(hr_TimeSheetRuleWrongTimeServices.Create(entity));
        }

        /// <summary>
        /// update
        /// </summary>
        /// <param name="model"></param>
        public static TimeSheetRuleWrongTimeModel Update(TimeSheetRuleWrongTimeModel model)
        {            
            var existedEntity = GetById(model.Id);
            if (existedEntity != null)
            {
                var entity = new hr_TimeSheetRuleWrongTime();
                model.FillEntity(ref entity);
                return  new TimeSheetRuleWrongTimeModel(hr_TimeSheetRuleWrongTimeServices.Update(entity));
            }
            return null;
        }

        /// <summary>
        /// delete
        /// </summary>
        /// <param name="id"></param>
        public static TimeSheetRuleWrongTimeModel Delete(int id)
        {
            var model = GetById(id);
            if (model != null)
            {
                model.IsDeleted = true;
                return Update(model);
            }
            return null;
        }
    }
}
