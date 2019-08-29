using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Core.Framework.Common;
using Web.Core.Service.Catalog;

namespace Web.Core.Framework
{
    public class CatalogWorkStatusController
    {
        public static List<CatalogWorkStatusModel> GetAll(string keyword, string name, string code, RecordStatus? group, string order, int? limit)
        {
            var condition = Constant.ConditionDefault;

            if (keyword != null)
                condition += " AND [Name] LIKE N'%{0}%'".FormatWith(keyword);

            if (group != null)
                condition += " AND [Group] = {0}".FormatWith((int) group.Value);

            if (name != null)
                condition += " AND [Name] = N'{0}'".FormatWith(name);

            if (code != null)
                condition += " AND [Code] = N'{0}'".FormatWith(code);

            return cat_WorkStatusServices.GetAll(condition, order, limit).Select(c => new CatalogWorkStatusModel(c))
                .ToList();
        }

        public static CatalogWorkStatusModel GetByGroup(RecordStatus? group)
        {
            var condition = Constant.ConditionDefault;

            if (group != null)
                condition += " AND [Group] = {0}".FormatWith((int)group.Value);

            return new CatalogWorkStatusModel(cat_WorkStatusServices.GetByCondition(condition)); 
        }
    }
}
