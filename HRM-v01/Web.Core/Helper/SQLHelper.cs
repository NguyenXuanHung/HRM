using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace Web.Core
{
    public class SQLHelper
    {
        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["StandardConfig"].ConnectionString;

        #region Execute Query

        public static DataTable ExecuteTable(string sql)
        {
            return ExecuteTable(sql, CommandType.Text, null, null);
        }

        public static DataTable ExecuteTable(string sql, string[] paramsName, object[] paramsValue)
        {
            return ExecuteTable(sql, CommandType.Text, paramsName, paramsValue);
        }

        public static DataTable ExecuteTable(string sql, CommandType type, string[] paramsName, object[] paramsValue)
        {
            // init connection string
            var conn = new SqlConnection(ConnectionString);
            try
            {
                // init command
                var command = new SqlCommand
                {
                    Connection = conn,
                    CommandText = sql,
                    CommandType = type
                };
                // add command params
                if (paramsName != null && paramsValue != null && paramsName.Length == paramsValue.Length)
                {
                    for (var i = 0; i < paramsName.Length; i++)
                    {
                        command.Parameters.AddWithValue(paramsName[i], paramsValue[i]);
                    }
                }
                // init table
                var table = new DataTable();
                // init adapter
                var adapter = new SqlDataAdapter(command);
                // open connection
                conn.Open();
                // execute adapter
                adapter.Fill(table);
                // returnreturn table;
                return table;
            }
            finally
            {
                // close connection
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }
        }

        public static List<T> ExecuteReader<T>(string sql) where T : class, new()
        {
            return ExecuteReader<T>(sql, null, null);
        }

        public static List<T> ExecuteReader<T>(string sql, string[] paramsName, object[] paramsValue) where T : class, new()
        {
            return ExecuteReader<T>(sql, CommandType.Text, paramsName, paramsValue);
        }

        public static List<T> ExecuteReader<T>(string sql, CommandType type, string[] paramsName, object[] paramsValue) where T : class, new()
        {
            // init return value
            var lstObj = new List<T>();
            // init reader
            SqlDataReader dr = null;
            // init connection string
            var conn = new SqlConnection(ConnectionString);
            try
            {
                // init command
                var command = new SqlCommand
                {
                    Connection = conn,
                    CommandText = sql,
                    CommandType = type
                };
                // add command params
                if (paramsName != null && paramsValue != null && paramsName.Length == paramsValue.Length)
                {
                    for (var i = 0; i < paramsName.Length; i++)
                    {
                        command.Parameters.AddWithValue(paramsName[i], paramsValue[i]);
                    }
                }
                // open connection
                conn.Open();
                // execute reader
                dr = command.ExecuteReader();
                while (dr.Read())
                {
                    // fill object into list
                    var obj = FillObject<T>(dr);
                    // add object into list
                    if (obj != null) lstObj.Add(obj);
                }
                // return
                return lstObj;
            }
            finally
            {
                // close reader
                dr?.Close();
                // close connection
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }
        }

        public static object ExecuteScalar(string sql)
        {
            return ExecuteScalar(sql, null, null);
        }

        public static object ExecuteScalar(string sql, string[] paramsName, object[] paramsValue)
        {
            return ExecuteScalar(sql, CommandType.Text, paramsName, paramsValue);
        }

        public static object ExecuteScalar(string sql, CommandType type, string[] paramsName, object[] paramsValue)
        {
            // init return object
            object obj = null;
            // init reader
            SqlDataReader dr = null;
            // init connection
            var conn = new SqlConnection(ConnectionString);
            try
            {
                // init command
                var command = new SqlCommand
                {
                    Connection = conn,
                    CommandText = sql,
                    CommandType = type
                };
                // add command params
                if (paramsName != null && paramsValue != null && paramsName.Length == paramsValue.Length)
                {
                    for (var i = 0; i < paramsName.Length; i++)
                    {
                        command.Parameters.AddWithValue(paramsName[i], paramsValue[i]);
                    }
                }
                // open connection
                conn.Open();
                // execute reader
                dr = command.ExecuteReader();
                // read data
                if (dr.Read())
                {
                    obj = dr[0];
                }
                // return
                return obj;
            }
            finally
            {
                // close reader
                dr?.Close();
                // close connection
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }
        }

        public static int ExecuteNonQuery(string sql)
        {
            return ExecuteNonQuery(sql, null, null);
        }

        public static int ExecuteNonQuery(string sql, string[] paramsName, object[] paramsValue)
        {
            return ExecuteNonQuery(sql, CommandType.Text, paramsName, paramsValue);
        }

        public static int ExecuteNonQuery(string sql, CommandType type, string[] paramsName, object[] paramsValue)
        {
            // init connection
            var conn = new SqlConnection(ConnectionString);
            try
            {
                // init command
                var command = new SqlCommand
                {
                    Connection = conn,
                    CommandText = sql,
                    CommandType = type
                };
                // add command params
                if (paramsName != null && paramsValue != null && paramsName.Length == paramsValue.Length)
                {
                    for (var i = 0; i < paramsName.Length; i++)
                    {
                        command.Parameters.AddWithValue(paramsName[i], paramsValue[i]);
                    }
                }
                // open connection
                conn.Open();
                // execute non query & return
                return command.ExecuteNonQuery();
            }
            finally
            {
                // close connection
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }
        }

        #endregion

        #region CRUD Menthods

        public static T Find<T>(int id) where T : class, new()
        {
            // validate id
            if (id <= 0) return null;
            // init cache key
            var cacheKey = "{0}find:id:{1}".FormatWith(GeneratePreCacheKey(typeof(T).Name), id);
            // get value from cache
            if(ContextCache.Get<T>(cacheKey) != null)
                return ContextCache.Get<T>(cacheKey);
            // get data form database
            SqlDataReader dr = null;
            // init connection 
            var conn = new SqlConnection(ConnectionString);
            try
            {
                // init sql command
                var command = new SqlCommand
                {
                    Connection = conn,
                    CommandText = CreateSelectByIdQuery<T>(),
                    CommandType = CommandType.Text
                };
                // add params
                command.Parameters.AddWithValue("@Id", id);
                // open connection
                conn.Open();
                // excute
                dr = command.ExecuteReader();
                // check reader
                if (dr.Read())
                {
                    // fill data
                    var obj = FillObject<T>(dr);
                    // insert cache
                    ContextCache.Set(cacheKey, obj);
                    // return object
                    return obj;
                }
                // return null
                return default(T);
            }
            finally
            {
                // close reader
                dr?.Close();
                // close connection
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }
        }

        public static T Find<T>(string condition, string order) where T : class, new()
        {
            // excute sql
            // init reader
            SqlDataReader dr = null;
            // init connection
            var conn = new SqlConnection(ConnectionString);
            try
            {
                // init command
                var cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandText = CreateSellectTopQuery<T>(condition, order, 1),
                    CommandType = CommandType.Text
                };
                // open connection
                conn.Open();
                // excute
                dr = cmd.ExecuteReader();
                // check reader
                if (dr.Read())
                {
                    return FillObject<T>(dr);
                }
                // fill data to object
                return default(T);
            }
            finally
            {
                // close reader
                dr?.Close();
                // close connection
                if (conn.State != ConnectionState.Closed) conn.Close();
            }
        }

        public static List<T> FindAll<T>(string condition, string order, int? limit) where T : class, new()
        {
            // init return data
            var returnList = new List<T>();
            // init reader
            SqlDataReader dr = null;
            // init connection
            var conn = new SqlConnection(ConnectionString);
            try
            {
                // init sql command
                var cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandText = CreateSellectTopQuery<T>(condition, order, limit),
                    CommandType = CommandType.Text
                };
                // open connection
                conn.Open();
                // excute
                dr = cmd.ExecuteReader();
                // fill data
                while (dr.Read())
                {
                    // fill each object
                    var item = FillObject<T>(dr);
                    if (item != null) returnList.Add(item);
                }
                // return
                return returnList;
            }
            finally
            {
                // close reader
                dr?.Close();
                // close connection
                if (conn.State != ConnectionState.Closed) conn.Close();
            }
        }

        public static PageResult<T> FindPaging<T>(string condition, string order, int? start, int? pageSize) where T : class, new()
        {
            var total = Count<T>(condition);
            var objs = new List<T>();
            SqlDataReader dr = null;
            var conn = new SqlConnection(ConnectionString);
            try
            {
                var cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandText = CreateSellectPagingQuery<T>(condition, order, start, pageSize),
                    CommandType = CommandType.Text
                };
                conn.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var o = FillObject<T>(dr);
                    if (o != null) objs.Add(o);
                }
                return new PageResult<T>(total, objs);
            }
            finally
            {
                // close reader
                dr?.Close();
                // close connection
                if(conn.State != ConnectionState.Closed) conn.Close();
            }
        }

        public static int Add<T>(T obj)
        {
            var type = obj.GetType();
            var conn = new SqlConnection(ConnectionString);
            try
            {
                var command = new SqlCommand
                {
                    Connection = conn,
                    CommandText = CreateInsertQuery<T>(),
                    CommandType = CommandType.Text
                };
                var arrPropertyInfo = type.GetProperties();
                foreach (var p in arrPropertyInfo)
                {
                    // ignore id parameter
                    if (p.Name.ToLower() != "id")
                    {
                        if (p.CanWrite & !p.PropertyType.IsEnum)
                        {
                            var objValue = p.GetValue(obj, null);
                            command.Parameters.AddWithValue(p.Name, objValue ?? DBNull.Value);
                        }

                        if (p.CanWrite & p.PropertyType.IsEnum)
                        {
                            var objValue = p.GetValue(obj, null);
                            command.Parameters.AddWithValue(p.Name, (int) objValue);
                        }
                    }
                }

                conn.Open();
                var id = command.ExecuteScalar();
                // check id
                if (id == null) return 0;
                // remove cache
                ContextCache.RemoveRegex("{0}find:id:".FormatWith(GeneratePreCacheKey(typeof(T).Name)));
                // return inserted id
                return Convert.ToInt32(id);
            }
            finally
            {
                // close connection
                if(conn.State != ConnectionState.Closed) conn.Close();
            }
        }

        public static int Update<T>(T obj)
        {
            if (obj == null) return 0;
            var type = obj.GetType();
            var conn = new SqlConnection(ConnectionString);
            try
            {
                var command = new SqlCommand
                {
                    Connection = conn,
                    CommandText = CreateUpdateQuery<T>(),
                    CommandType = CommandType.Text
                };
                var arrPropertyInfo = type.GetProperties();
                foreach (var p in arrPropertyInfo)
                {
                    if (p.CanWrite & !p.PropertyType.IsEnum)
                    {
                        var objValue = p.GetValue(obj, null);
                        command.Parameters.AddWithValue(p.Name, objValue ?? DBNull.Value);
                    }
                    if (p.CanWrite & p.PropertyType.IsEnum)
                    {
                        var objValue = p.GetValue(obj, null);
                        command.Parameters.AddWithValue(p.Name, (int)objValue);
                    }
                }
                conn.Open();
                // excute
                var rowEffected = command.ExecuteNonQuery();
                // check excute result
                if (rowEffected > 0)
                {
                    // remove cache
                    ContextCache.RemoveRegex("{0}find:id:".FormatWith(GeneratePreCacheKey(typeof(T).Name)));
                }
                // return row effected
                return rowEffected;
            }
            finally
            {
                // close connection
                if(conn.State != ConnectionState.Closed) conn.Close();
            }
        }

        public static bool Remove<T>(int id)
        {
            var conn = new SqlConnection(ConnectionString);
            try
            {
                var command = new SqlCommand
                {
                    Connection = conn,
                    CommandText = CreateDeleteQuery<T>(),
                    CommandType = CommandType.Text
                };
                command.Parameters.AddWithValue("@Id", id);
                conn.Open();
                // excute
                var rowEffected = command.ExecuteNonQuery();
                // check excute result
                if (rowEffected > 0)
                {
                    // remove cache
                    ContextCache.RemoveRegex("{0}find:id:".FormatWith(GeneratePreCacheKey(typeof(T).Name)));
                    // return success
                    return true;
                }
                // return fail
                return false;
            }
            finally
            {
                // close connection
                if(conn.State != ConnectionState.Closed) conn.Close();
            }
        }

        public static bool Remove<T>(string condition)
        {
            if (string.IsNullOrEmpty(condition)) return true;
            var conn = new SqlConnection(ConnectionString);
            try
            {
                var command = new SqlCommand
                {
                    Connection = conn,
                    CommandText = CreateDeleteQuery<T>(condition),
                    CommandType = CommandType.Text
                };
                conn.Open();
                // excute
                var rowEffected = command.ExecuteNonQuery();
                // check excute result
                if (rowEffected > 0)
                {
                    // remove cache
                    ContextCache.RemoveRegex("{0}find:id:".FormatWith(GeneratePreCacheKey(typeof(T).Name)));
                    // return success
                    return true;
                }
                // return fail
                return false;
            }
            finally
            {
                // close connection
                if(conn.State != ConnectionState.Closed) conn.Close();
            }
        }

        public static int Count<T>(string condition)
        {
            if (string.IsNullOrEmpty(condition)) return 0;
            var conn = new SqlConnection(ConnectionString);
            try
            {
                var command = new SqlCommand
                {
                    Connection = conn,
                    CommandText = CreateSelectCountQuery<T>(condition),
                    CommandType = CommandType.Text
                };
                conn.Open();
                var obj = command.ExecuteScalar();
                return (int?) obj ?? 0;
            }
            finally
            {
                // close connection
                if(conn.State != ConnectionState.Closed) conn.Close();
            }
        }

        #endregion

        #region Private Methods

        private static string CreateSelectByIdQuery<T>()
        {
            var tblName = typeof(T).Name.ToLower();
            return "SELECT * FROM [{0}] WHERE [Id]=@Id".FormatWith(tblName);
        }

        private static string CreateSellectTopQuery<T>(string condition, string order, int? limit)
        {
            var tblName = typeof(T).Name.ToLower();
            var qTake = limit != null && limit.Value > 0 ? "TOP {0}".FormatWith(limit) : string.Empty;
            var qOrder = !string.IsNullOrEmpty(order) ? "ORDER BY {0}".FormatWith(order) : string.Empty;
            if (string.IsNullOrEmpty(condition)) condition = "1=1";
            return "SELECT {0} * FROM [{1}] WHERE {2} {3}".FormatWith(qTake, tblName, condition, qOrder);
        }

        private static string CreateSellectPagingQuery<T>(string condition, string order, int? start, int? pagesize)
        {
            start = start ?? 0;
            pagesize = pagesize ?? 0;
            var tblName = typeof(T).Name.ToLower();
            var qTake = string.Empty;
            var qOrder = "ORDER BY {0}".FormatWith(!string.IsNullOrEmpty(order) ? order : "Id");
            if (string.IsNullOrEmpty(condition)) condition = "1=1";
            return "SELECT {0} * FROM (SELECT *, ROW_NUMBER() OVER ({1}) as row_number FROM [{2}] WHERE {3}) t0 WHERE t0.row_number > {4} AND  t0.row_number <= {5}".FormatWith(
                qTake, qOrder, tblName, condition, start, start + pagesize);
        }

        //private static string CreateSelectScalarQuery<T>(string column, string condition, string order)
        //{
        //    var tblName = typeof(T).Name.ToLower();
        //    if (string.IsNullOrEmpty(condition)) condition = "1=1";
        //    return "SELECT TOP 1 {0} FROM [{1}] WHERE {2} ORDER BY {3}".FormatWith(column, tblName, condition, order);
        //}

        private static string CreateSelectCountQuery<T>(string condition)
        {
            var tblName = typeof(T).Name.ToLower();
            if (string.IsNullOrEmpty(condition)) condition = "1=1";
            return "SELECT COUNT(*) AS TOTAL FROM [{0}] WHERE {1}".FormatWith(tblName, condition);
        }

        private static string CreateInsertQuery<T>()
        {
            // get table name
            var tblName = typeof(T).Name.ToLower();
            // get array of property info
            var arrPropertyInfo = typeof(T).GetProperties();
            // init array of fields name & params name
            // except index 0 for Id field
            var arrFieldNames = new List<string>();
            var arrParamNames = new List<string>();
            foreach (var p in arrPropertyInfo)
            {
                if (p.Name.ToLower() != "id" && p.CanWrite)
                {
                    arrFieldNames.Add("[{0}]".FormatWith(p.Name));
                    arrParamNames.Add("@{0}".FormatWith(p.Name));
                }
            }
            return "INSERT INTO [{0}] ({1}) VALUES ({2}); SELECT SCOPE_IDENTITY();".FormatWith(tblName, string.Join(",", arrFieldNames), string.Join(",", arrParamNames));
        }

        private static string CreateUpdateQuery<T>()
        {
            return CreateUpdateQuery<T>(string.Empty);
        }

        private static string CreateUpdateQuery<T>(string condition)
        {
            // get table name
            var tblName = typeof(T).Name.ToLower();
            // get array of property info
            var arrPropertyInfo = typeof(T).GetProperties();
            // init array of expression
            // except index 0 for Id field
            var arrExpressions = new List<string>();
            foreach (var p in arrPropertyInfo)
            {
                if (p.Name.ToLower() != "id" && p.CanWrite)
                    arrExpressions.Add(string.Format("[{0}]=@{0}", p.Name));
            }
            return "UPDATE [{0}] SET {1} WHERE {2}".FormatWith(tblName, string.Join(",", arrExpressions),
                string.IsNullOrEmpty(condition) ? "[Id]=@Id" : condition);
        }

        private static string CreateDeleteQuery<T>()
        {
            return CreateDeleteQuery<T>(string.Empty);
        }

        private static string CreateDeleteQuery<T>(string condition)
        {
            var tblName = typeof(T).Name.ToLower();
            return string.IsNullOrEmpty(condition)
                ? "DELETE FROM [{0}] WHERE [Id]=@Id".FormatWith(tblName)
                : "DELETE FROM [{0}] WHERE {1}".FormatWith(tblName, condition);
        }

        private static T FillObject<T>(IDataRecord dr) where T : class, new()
        {
            // init object
            var obj = new T();
            var type = obj.GetType();
            // get array of property info
            var properties = type.GetProperties();
            // set property value            
            foreach (var p in properties)
            {
                // property can write & is not enum
                if (p.CanWrite && !p.PropertyType.IsEnum)
                {
                    var val = dr[p.Name];
                    if (!(val is DBNull))
                        type.InvokeMember(p.Name, BindingFlags.SetProperty, null, obj, new[] { val });
                }
                // property can write & is enum
                if (p.CanWrite && p.PropertyType.IsEnum)
                {
                    var val = Enum.ToObject(p.PropertyType, ((int)dr[p.Name]));
                    if (!(val is DBNull))
                        type.InvokeMember(p.Name, BindingFlags.SetProperty, null, obj, new[] { val });
                }
            }
            return obj;
        }

        private static string GeneratePreCacheKey(string objectName)
        {
            var appKey = AppSettingHelper.GetAppSetting(typeof(string), "CacheName", true);
            return "human-resource-app:{0}:{1}:".FormatWith(appKey, objectName).ToLower();
        }

        #endregion
    }
}
