using System.Collections.Generic;
using System.Linq;
using Web.Core.Framework.Common;
using Web.Core.Framework.Utils;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.HumanRecord;

namespace Web.Core.Framework.Controller
{
    public class SalaryBoardConfigController
    {
        public static List<SalaryBoardConfigModel> GetAll(int? payrollConfigId, bool? isInUsed, bool? isReadOnly, bool? isDisable,
            SalaryConfigDataType? dataType, string order, int? limit)
        {
            // init default condition
            var condition = Constant.ConditionDefault;
            // check payroll config
            if (payrollConfigId != null)
                condition += @" AND [ConfigId]={0}".FormatWith(payrollConfigId.Value);
            // check is in used
            if (isInUsed != null)
                condition += @" AND [IsInUsed]={0}".FormatWith(isInUsed.Value ? "1" : "0");
            // check is read only
            if (isReadOnly != null)
                condition += @" AND [IsReadOnly]={0}".FormatWith(isReadOnly.Value ? "1" : "0");
            // check is disabled
            if (isDisable != null)
                condition += @" AND [IsDisable]={0}".FormatWith(isDisable.Value ? "1" : "0");
            // check is read only
            if (dataType != null)
                condition += @" AND [DataType]={0}".FormatWith((int)dataType.Value);
            
            // get all data from database
            var salaryBoarConfigEntities = hr_SalaryBoardConfigServices.GetAll(condition, order, limit);
            // init return data
            var salaryBoardConfigModels = new List<SalaryBoardConfigModel>();

            // check result
            if (salaryBoarConfigEntities.Count > 0)
                // bind data to model
                salaryBoardConfigModels.AddRange(salaryBoarConfigEntities.Select(s => new SalaryBoardConfigModel(s)));

            // return
            return salaryBoardConfigModels;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<SalaryBoardConfigModel> GetAll()
        {
            var listModel = new List<SalaryBoardConfigModel>();
            var boardConfigs = hr_SalaryBoardConfigServices.GetAll();
            foreach (var item in boardConfigs)
            {
                var model = new SalaryBoardConfigModel(item)
                {
                    Id = item.Id,
                    ColumnCode = item.ColumnCode,
                    Display = item.Display,
                    Formula = item.Formula,
                    ColumnExcel = item.ColumnExcel,
                    Order = item.Order,
                };
                listModel.Add(model);
            }

            return listModel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchKey"></param>
        /// <param name="configId"></param>
        /// <param name="dataType"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static PageResult<SalaryBoardConfigModel> GetListSalaryBoardConfig(string searchKey, int? configId, SalaryConfigDataType? dataType, int start, int limit)
        {
            var lstTimeModels = new List<SalaryBoardConfigModel>();

            var condition = Constant.ConditionDefault;
            if (configId != null)
            {
                condition += " AND [ConfigId] = {0} ".FormatWith(configId);
            }

            // add search key
            if (!string.IsNullOrEmpty(searchKey))
            {
                condition +=
                    @" AND ([Display] LIKE N'%{0}%' OR [ColumnCode] LIKE N'%{0}%' OR [ColumnExcel] LIKE N'%{0}%') "
                        .FormatWith(searchKey);
            }

            if (dataType != null)
                condition += @" AND [DataType]={0}".FormatWith((int)dataType.Value);
            var hrPageResult = hr_SalaryBoardConfigServices.GetPaging(condition, null, start, limit);

            if (hrPageResult.Data.Count <= 0)
                return new PageResult<SalaryBoardConfigModel>(hrPageResult.Total, lstTimeModels);
            lstTimeModels.AddRange(hrPageResult.Data.Select(record => new SalaryBoardConfigModel(record)));
            //sort
            lstTimeModels.Sort((x,y) => CompareUtil.CompareStringByLength(x.ColumnExcel, y.ColumnExcel));
       
            return new PageResult<SalaryBoardConfigModel>(hrPageResult.Total, lstTimeModels);
        }

        public static SalaryBoardConfigModel GetById(int id)
        {
            var entity = hr_SalaryBoardConfigServices.GetById(id);
            return entity != null ? new SalaryBoardConfigModel(entity) : null;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static SalaryBoardConfigModel Create(SalaryBoardConfigModel model)
        {
            var entity = new hr_SalaryBoardConfig();
            model.FillEntity(ref entity);
            return new SalaryBoardConfigModel(hr_SalaryBoardConfigServices.Create(entity));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configId"></param>
        public static void DeleteByCondition(int configId)
        {
            var condition = @" [ConfigId] = {0} ".FormatWith(configId);
            hr_SalaryBoardConfigServices.Delete(condition);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public static void Delete(int id)
        {
            hr_SalaryBoardConfigServices.Delete(id);
        }

    }
}
