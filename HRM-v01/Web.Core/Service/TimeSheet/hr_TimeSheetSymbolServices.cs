using System.Collections.Generic;
using Web.Core.Object.TimeSheet;

namespace Web.Core.Service.TimeSheet
{
    public class hr_TimeSheetSymbolServices : BaseServices<hr_TimeSheetSymbol>
    {
        private const string DefaultCondition = " 1=1 ";
        private const string ConstFullDay = @"Một ngày công";
        public static List<hr_TimeSheetSymbol> GetCodeTimeSheetSheetSymbols(int? id, string code)
        {
            var condition = DefaultCondition;
            if (id != null)
            {
                condition += " AND [Id] = {0}".FormatWith(id);
            }
            if (!string.IsNullOrEmpty(code))
            {
                condition += " AND [Code] = '{0}'".FormatWith(code);
            }
            return GetAll(condition);
        }
    }
}
