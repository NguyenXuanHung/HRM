using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using Web.Core.Object.Catalog;

namespace Web.Core.Service.Catalog
{
    public class cat_BaseServices
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static cat_Base GetById(string objName, int id)
        {
            var sql = "SELECT TOP 1 * FROM [{0}] WHERE Id='{1}'".FormatWith(objName, id);
            var data = SQLHelper.ExecuteTable(sql);
            if (data.Rows.Count > 0)
            {
                return FillObject(objName, data.Rows[0]);
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objName"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static cat_Base GetByName(string objName, string name)
        {
            // check report name
            if(!string.IsNullOrEmpty(name))
            {
                var sql = "SELECT TOP 1 * FROM [{0}] WHERE [Name] = '{1}' AND [IsDeleted] = 0".FormatWith(objName, name);
                var data = SQLHelper.ExecuteTable(sql);
                if(data.Rows.Count > 0)
                {
                    return FillObject(objName, data.Rows[0]);
                }
            }
            // invalid name
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objName"></param>
        /// <param name="keyword"></param>
        /// <param name="group"></param>
        /// <param name="status"></param>
        /// <param name="isDeleted"></param>
        /// <param name="order"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<cat_Base> GetAll(string objName, string keyword, string group, CatalogStatus? status, bool? isDeleted, string order, int? limit)
        {
            // init list object
            var lstObj = new List<cat_Base>();

            // init select top
            var selectTop = limit != null && limit.Value > 0 ? @" TOP {0}".FormatWith(limit.Value) : "";

            // init condition
            var condition = "1=1";

            // keyword
            if (!string.IsNullOrEmpty(keyword))
                condition += @" AND ([Name] LIKE N'%{0}%' OR [Description] LIKE N'%{0}%')".FormatWith(keyword.EscapeQuote());

            // group
            if (!string.IsNullOrEmpty(group))
                condition += @" AND [Group]=N'{0}'".FormatWith(group);

            // status 
            if (status != null)
                condition += @" AND [Status]='{0}'".FormatWith(status.Value);

            // is deleted
            if (isDeleted != null)
                condition += @" AND [IsDeleted]='{0}'".FormatWith(isDeleted.Value);

            // order
            if (string.IsNullOrEmpty(order))
                order = @"[Order],[Name]";

            // create select query
            var sql = @"SELECT{0} * FROM [{1}] WHERE {2} ORDER BY {3}".FormatWith(selectTop, objName, condition, order.EscapeQuote());

            // execute table
            var dt = SQLHelper.ExecuteTable(sql);

            // insert into list
            if (dt.Rows.Count > 0)
            {
                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    lstObj.Add(FillObject(objName, dt.Rows[i]));
                }
            }

            // return
            return lstObj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objName"></param>
        /// <param name="keyword"></param>
        /// <param name="group"></param>
        /// <param name="status"></param>
        /// <param name="isDeleted"></param>
        /// <param name="order"></param>
        /// <param name="start"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public static PageResult<cat_Base> GetPaging(string objName, string keyword, string group, CatalogStatus? status, bool? isDeleted, string order, int start, int pagesize)
        {
            // init condition
            var condition = "1=1";

            // keyword
            if(!string.IsNullOrEmpty(keyword))
                condition += @" AND ([Name] LIKE N'%{0}%' OR [Description] LIKE N'%{0}%')".FormatWith(keyword.EscapeQuote());

            // group
            if(!string.IsNullOrEmpty(group))
                condition += @" AND [Group]=N'{0}'".FormatWith(group);

            // status 
            if(status != null)
                condition += @" AND [Status]='{0}'".FormatWith(status.Value);

            // is deleted
            if(isDeleted != null)
                condition += @" AND [IsDeleted]='{0}'".FormatWith(isDeleted.Value);

            // order
            if(string.IsNullOrEmpty(order))
                order = @"[Order],[Name]";

            // init start, pagesize
            if (start < 0) start = 0;
            if (pagesize < 0) pagesize = 0;

            // count total
            var countQuery = @"SELECT COUNT(*) AS TOTAL FROM [{0}] WHERE {1}".FormatWith(objName.EscapeQuote(), condition);
            var objCount = SQLHelper.ExecuteScalar(countQuery);
            var total = (int?) objCount ?? 0;

            // get paging
            var getQuery = "SELECT * FROM (SELECT *, ROW_NUMBER() OVER (ORDER BY {0}) as row_number FROM [{1}] WHERE {2}) t0 WHERE t0.row_number > {3} AND  t0.row_number <= {4}"
                .FormatWith(order.EscapeQuote(), objName.EscapeQuote(), condition, start, start + pagesize);

            // init list object
            var lstObj = new List<cat_Base>();

            // execute table
            var dt = SQLHelper.ExecuteTable(getQuery);

            // insert into result
            if (dt.Rows.Count > 0)
            {
                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    lstObj.Add(FillObject(objName, dt.Rows[i]));
                }
            }

            // return
            return new PageResult<cat_Base>(total, lstObj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objName"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static cat_Base Create(string objName, cat_Base obj)
        {
            // init insert query
            var sql = @"INSERT INTO [{0}] ({1}) VALUES ({2}); SELECT SCOPE_IDENTITY();";
            // get object type
            var type = obj.GetType();
            // get array of property info
            var arrPropertyInfo = type.GetProperties();
            // init array of fields name & params name
            // except index 0 for Id field
            var arrFieldNames = new List<string>();
            var arrParamNames = new List<string>();
            var arrParamValues = new List<object>();
            foreach (var p in arrPropertyInfo)
            {
                if (p.Name.ToLower() != "id" && p.CanWrite)
                {
                    arrFieldNames.Add("[{0}]".FormatWith(p.Name));
                    arrParamNames.Add("@{0}".FormatWith(p.Name));
                    arrParamValues.Add(type.InvokeMember(p.Name, BindingFlags.GetProperty, null, obj, null));
                }
            }
            // create insert sql
            sql = sql.FormatWith(objName, string.Join(",", arrFieldNames), string.Join(",", arrParamNames));
            // execute
            var objReturn = SQLHelper.ExecuteScalar(sql, arrParamNames.ToArray(), arrParamValues.ToArray());
            if (objReturn != null && Convert.ToInt32(objReturn) > 0)
            {
                obj.Id = Convert.ToInt32(objReturn);
                return obj;
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objName"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static cat_Base Update(string objName, cat_Base obj)
        {
            // init insert query
            var sql = @"UPDATE [{0}] SET {1} WHERE [Id]='{2}'";
            // get object type
            var type = Type.GetType("Web.Core.Object.Catalog.{0}".FormatWith(objName));
            if (type != null)
            {
                // get array of property info
                var arrPropertyInfo = type.GetProperties();
                // init array of fields name & params name
                // except index 0 for Id field
                var arrExpressions = new List<string>();
                var arrParamNames = new List<string>();
                var arrParamValues = new List<object>();
                foreach (var p in arrPropertyInfo)
                {
                    if (p.Name.ToLower() != "id" && p.CanWrite)
                    {
                        arrExpressions.Add(string.Format("[{0}]=@{0}", p.Name));
                        arrParamNames.Add("@{0}".FormatWith(p.Name));
                        arrParamValues.Add(type.InvokeMember(p.Name, BindingFlags.GetProperty, null, obj, null));
                    }
                }

                // create update sql
                sql = sql.FormatWith(objName, string.Join(",", arrExpressions), obj.Id);
                // execute
                var rowEffected = SQLHelper.ExecuteNonQuery(sql, arrParamNames.ToArray(), arrParamValues.ToArray());
                return rowEffected > 0 ? obj : null;
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool Delete(string objName, int id)
        {
            var sql = "DELETE FROM [{0}] WHERE [Id]='{1}'".FormatWith(objName, id);
            return SQLHelper.ExecuteNonQuery(sql) > 0;
        }
        
        #region Private Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objName"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        private static cat_Base FillObject(string objName, DataRow row)
        {
            var obj = Assembly.GetExecutingAssembly().CreateInstance("Web.Core.Object.Catalog.{0}".FormatWith(objName));
            if (obj != null)
            {
                // get object type
                var type = obj.GetType();
                // get array of property info
                var properties = type.GetProperties();
                // set property value            
                foreach (var p in properties)
                {
                    // property can write & is not enum
                    if(p.CanWrite && !p.PropertyType.IsEnum)
                    {
                        var val = row[p.Name];
                        if(!(val is DBNull))
                            type.InvokeMember(p.Name, BindingFlags.SetProperty, null, obj, new[] { val });
                    }
                    // property can write & is enum
                    if(p.CanWrite && p.PropertyType.IsEnum)
                    {
                        var val = Enum.ToObject(p.PropertyType, ((int)row[p.Name]));
                        if(!(val is DBNull))
                            type.InvokeMember(p.Name, BindingFlags.SetProperty, null, obj, new[] { val });
                    }
                }
                return (cat_Base)obj;
            }
            return null;
        }

        #endregion
    }
}
