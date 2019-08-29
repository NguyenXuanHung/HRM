using Web.Core.Object.Catalog;

namespace Web.Core.Service.Catalog
{
    public class cat_AllowanceServices : BaseServices<cat_Allowance>
    {
        public static cat_Allowance GetByCode(string code)
        {
            // check params
            if(!string.IsNullOrEmpty(code))
            {
                // return entity
                return GetByCondition("[Code]='{0}'".FormatWith(code));
            }

            // invalid params
            return null;
        }
    }
}
