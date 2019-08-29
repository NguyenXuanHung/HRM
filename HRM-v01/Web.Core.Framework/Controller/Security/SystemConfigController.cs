using System;
using System.Collections.Generic;
using System.Linq;
using Web.Core.Framework.Common;
using Web.Core.Object.Security;
using Web.Core.Service.Security;

namespace Web.Core.Framework
{
    public class SystemConfigController
    {
        /// <summary>
        /// Lấy SystemConfig theo tên và đơn vị
        /// </summary>
        /// <param name="name"></param>
        /// <param name="departments"></param>
        /// <returns></returns>
        public static List<SystemConfig> GetAllByNameAndDepartment(string name, string departments)
        {
            var arrDepartment = departments.Split(new[] { ',' }, StringSplitOptions.None);
            for (var i = 0; i < arrDepartment.Length; i++)
            {
                arrDepartment[i] = "{0}".FormatWith(arrDepartment[i]);
            }
            var condition = "[Name] LIKE '{0}'".FormatWith(name) +
                            " AND [DepartmentId] IN ({0}) ".FormatWith(string.Join(",", arrDepartment));
            var data = SystemConfigServices.GetAll(condition);
            return data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public static string GetValueByName(string name, int departmentId)
        {
            var condition = " [Name] = '{0}'".FormatWith(name) +
                               " AND [DepartmentId] = {0}".FormatWith(departmentId);
            var data = SystemConfigServices.GetByCondition(condition);
            return data != null ? data.Value : string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="departments"></param>
        /// <returns></returns>
        public static string GetValueByNameFollowDepartment(string name, string departments)
        {
            var arrDepartment = departments.Split(new[] { ',' }, StringSplitOptions.None);
            for(var i = 0; i < arrDepartment.Length; i++)
            {
                arrDepartment[i] = "'{0}'".FormatWith(arrDepartment[i]);
            }
            var condition = "[Name] = '{0}'".FormatWith(name) +
                            " AND [DepartmentId] IN ({0}) ".FormatWith(string.Join(",", arrDepartment));
            var data = SystemConfigServices.GetByCondition(condition);
            return data != null ? data.Value : string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="departmentId"></param>
        public void SaveValue(string name, string value, int departmentId)
        {
            var condition = "[DepartmentId] = {0}".FormatWith(departmentId) +
                            " AND [Name] = '{0}'".FormatWith(name);
            var record = SystemConfigServices.GetAll(condition);
            if(record == null) return;
            foreach(var item in record)
            {
                item.Name = name;
                SystemConfigServices.Update(item);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="departmentId"></param>
        public void CreateParameter(string name, string value, int departmentId)
        {
            var record = new SystemConfig
            {
                Name = name,
                Value = value,
                DepartmentId = departmentId
            };
            SystemConfigServices.Create(record);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="departmentIds"></param>
        public void CreateOrSave(string name, string value, string departmentIds)
        {
            var arrDepartment = departmentIds.Split(new[] { ',' }, StringSplitOptions.None);
            for(var i = 0; i < arrDepartment.Length; i++)
            {
                arrDepartment[i] = "'{0}'".FormatWith(arrDepartment[i]);
            }
            var condition = "1=1";
            if(!string.IsNullOrEmpty(name))
            {
                condition += @" AND [Name]='{0}'".FormatWith(name);
            }
            if(!string.IsNullOrEmpty(departmentIds))
            {
                condition += @" AND [DepartmentId] IN ({0})".FormatWith(string.Join(",", arrDepartment));
            }
            var record = SystemConfigServices.GetAll(condition);
            if(record != null && record.Any())
            {
                foreach(var item in record)
                {
                    item.Value = value;
                    item.EditedDate = DateTime.Now;
                    SystemConfigServices.Update(item);
                }
            }
            else
            {
                foreach(var item in arrDepartment)
                {
                    var result = new SystemConfig
                    {
                        Name = name,
                        Value = value,
                        CreatedDate = DateTime.Now,
                        DepartmentId = !string.IsNullOrEmpty(item) ? Convert.ToInt32(item.Replace("'", " ")) : 0,
                    };

                    SystemConfigServices.Create(result);
                }
            }
        }

        /// <summary>
        /// Giới hạn gói người dùng sử dụng
        /// </summary>
        /// <returns></returns>
        public static int GetLimitPackage()
        {
            var condition = " [Name] = N'{0}'".FormatWith(Constant.LimitPackage);
            var data = SystemConfigServices.GetByCondition(condition);

            return data != null ? Convert.ToInt32(data.Value) : 0;
        }
    }
}
