using System.Collections.Generic;
using Web.Core.Object.HumanRecord;

namespace Web.Core.Service.HumanRecord
{
    public class hr_SalaryBoardConfigServices : BaseServices<hr_SalaryBoardConfig>
    {
        private const string ConditionDefault = @" 1=1 ";
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configId"></param>
        /// <returns></returns>
        public static List<hr_SalaryBoardConfig> GetAllConfigs(int? configId)
        {
            // init condition
            var condition = ConditionDefault;
            condition += " AND [IsInUsed] = 1";
            if (configId != null)
            {
                condition += " AND [ConfigId] = {0}".FormatWith(configId);
            }
            return GetAll(condition);
        }

    }
}
