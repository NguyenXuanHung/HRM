using System.Collections.Generic;

namespace Web.Core
{
    public class BaseServices<T> where T : BaseEntity, new()
    {
        /// <summary>
        /// Get object by id
        /// </summary>
        /// <param name="id">Object Id</param>
        /// <returns>Found object, return null if no object found</returns>
        public static T GetById(int id)
        {
            return SQLHelper.Find<T>(id);
        }

        /// <summary>
        /// Get object by condition
        /// </summary>
        /// <param name="condition">T_SQL condition (ex: [Id]='1' and [Name] LIKE '%name%')</param>
        /// <returns>Found object, return null if no object found</returns>
        public static T GetByCondition(string condition)
        {
            return SQLHelper.Find<T>(condition, null);
        }

        /// <summary>
        /// Get object by condition
        /// </summary>
        /// <param name="condition">T_SQL condition (ex: [Id]='1' and [Name] LIKE '%name%')</param>
        /// <param name="order">Sort order</param>
        /// <returns>Found object, return null if no object found</returns>
        public static T GetByCondition(string condition, string order)
        {
            return SQLHelper.Find<T>(condition, order);
        }

        /// <summary>
        /// Get all objects
        /// </summary>
        /// <returns></returns>
        public static List<T> GetAll()
        {
            return SQLHelper.FindAll<T>(null, null, null);
        }

        /// <summary>
        /// Get all objects by condition
        /// </summary>
        /// <param name="condition">T_SQL condition (ex: [Id]='1' and [Name] LIKE '%name%')</param>
        /// <returns></returns>
        public static List<T> GetAll(string condition)
        {
            return SQLHelper.FindAll<T>(condition, null, null);
        }

        /// <summary>
        /// Get all objects by condition
        /// </summary>
        /// <param name="condition">T_SQL condition (ex: [Id]='1' and [Name] LIKE '%name%')</param>
        /// <param name="order">Sort order</param>
        /// <returns></returns>
        public static List<T> GetAll(string condition, string order)
        {
            return SQLHelper.FindAll<T>(condition, order, null);
        }

        /// <summary>
        /// Get all objects by condition
        /// </summary>
        /// <param name="condition">T_SQL condition (ex: [Id]='1' and [Name] LIKE '%name%')</param>
        /// <param name="order">Sort order</param>
        /// <param name="limit">Number of rows selected</param>
        /// <returns></returns>
        public static List<T> GetAll(string condition, string order, int? limit)
        {
            return SQLHelper.FindAll<T>(condition, order, limit);
        }

        /// <summary>
        /// Get paging objects by condition
        /// </summary>
        /// <param name="condition">T_SQL condition (ex: [Id]='1' and [Name] LIKE '%name%')</param>
        /// <param name="order">Sort order</param>
        /// <param name="start">From row</param>
        /// <param name="limit">Number of rows selected</param>
        /// <returns></returns>
        public static PageResult<T> GetPaging(string condition, string order, int start, int limit)
        {
            return SQLHelper.FindPaging<T>(condition, order, start, limit);
        }

        /// <summary>
        /// Count by condition
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static int Count(string condition)
        {
            return SQLHelper.Count<T>(condition);
        }

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T Create(T obj)
        {
            var id = SQLHelper.Add(obj);
            if(id <= 0) return null;
            obj.Id = id;
            return obj;
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T Update(T obj)
        {
            var rowEffected = SQLHelper.Update(obj);
            return rowEffected > 0 ? obj : null;
        }

        /// <summary>
        /// Delete by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool Delete(int id)
        {
            return SQLHelper.Remove<T>(id);
        }

        /// <summary>
        /// Delete by condition
        /// </summary>
        /// <param name="condition">T_SQL condition (ex: [Id]='1' and [Name] LIKE '%name%')</param>
        /// <returns></returns>
        public static bool Delete(string condition)
        {
            return SQLHelper.Remove<T>(condition);
        }
        
        #region Catalog Methods

        /// <summary>
        /// Lấy giá trị trường tên [Name] theo Id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Trả về giá trị trường [Name] dạng string, mặc định trả về string rỗng</returns>
        public static string GetFieldValueById(int id)
        {
            return GetFieldValueById(id, "Name");
        }

        /// <summary>
        /// Lấy giá trị trường (dạng string) theo Id
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="fieldName">Tên trường cần lấy</param>
        /// <returns>Trả về giá trị dạng string, mặc định trả về string rỗng</returns>
        public static string GetFieldValueById(int id, string fieldName)
        {
            var obj = SQLHelper.Find<T>(id);
            if(obj == null) return "";
            var type = obj.GetType();
            var fieldInfo = type.GetProperty(fieldName);
            return fieldInfo != null ? fieldInfo.GetValue(obj, null).ToString() : "";
        }

        #endregion
    }
}
