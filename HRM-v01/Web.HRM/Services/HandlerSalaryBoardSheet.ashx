<%@ WebHandler Language="C#" Class="Web.HRM.Services.HandlerSalaryBoardSheet" %>

using System;
using System.Web;
using Web.Core;
using Web.Core.Service.HumanRecord;
using Web.Core.Framework.Controller;
using System.Linq;
using System.Data;
using System.Globalization;
using System.Reflection;
using Web.Core.Framework.Utils;
using Web.Core.Service.Salary;

namespace Web.HRM.Services
{
    public class HandlerSalaryBoardSheet : Core.Framework.BaseControl.BaseHandler, IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json; charset=UTF-8";
            context.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            context.Response.AppendHeader("Access-Control-Allow-Credentials", "true");

            // config id
            var configId = 0;
            // salary board id
            var salaryBoardId = context.Request.QueryString["id"];

            // get salary board list
            var salaryBoardList = sal_PayrollServices.GetById(Convert.ToInt32(salaryBoardId));

            if(salaryBoardList != null)
            {
                configId = salaryBoardList.ConfigId;
            }

            // get salary board info
            var salaryInfos = SalaryBoardInfoController.GetAll(null, salaryBoardId, null, null);

            // get salary board config
            var salaryBoardConfigs = hr_SalaryBoardConfigServices.GetAllConfigs(configId);

            //sort list by columnExcel
            salaryBoardConfigs.Sort((x, y) => CompareUtil.CompareStringByLength(x.ColumnExcel, y.ColumnExcel));

            // dynamic column list
            var condition = " [SalaryBoardId] = {0} ".FormatWith(salaryBoardId);

            // get salary board dynamics
            var salaryBoardDynamics = hr_SalaryBoardDynamicColumnService.GetAll(condition);

            var table = new DataTable();

            // generate default column
            var defaultCol = new[] {
                new DataColumn("Id"),
                new DataColumn("RecordId"),
                new DataColumn("FullName"),
                new DataColumn("EmployeeCode"),
                new DataColumn("PositionName")
            };
            table.Columns.AddRange(defaultCol);

            // generate other column
            foreach(var config in salaryBoardConfigs)
            {
                // Setting column names as Property names
                table.Columns.Add(config.Display);
            }

            // fill data into table
            foreach(var salaryInfo in salaryInfos)
            {
                // init row
                var row = table.NewRow();
                // add value for defaut column
                row["Id"] = salaryInfo.Id;
                row["RecordId"] = salaryInfo.RecordId;
                row["FullName"] = salaryInfo.FullName;
                row["EmployeeCode"] = salaryInfo.EmployeeCode;
                row["PositionName"] = salaryInfo.PositionName;

                // add value for dynamic column
                foreach(var config in salaryBoardConfigs)
                {
                    switch(config.DataType)
                    {
                        case SalaryConfigDataType.FieldValue:
                            var type = salaryInfo.GetType();
                            var propInfo = type.GetProperty(config.ColumnCode);
                            var value = Convert.ToString(propInfo.GetValue(salaryInfo, BindingFlags.GetProperty, null, null, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
                            row[config.Display] = value;
                            break;
                        case SalaryConfigDataType.FixedValue:
                        case SalaryConfigDataType.FieldFormula:
                        case SalaryConfigDataType.Formula:
                            row[config.Display] = config.Formula;
                            break;
                        case SalaryConfigDataType.DynamicValue:
                            var findObj = salaryBoardDynamics.Find(o =>
                                o.RecordId == salaryInfo.RecordId && o.ColumnCode == config.ColumnCode);
                            row[config.Display] = findObj != null ? findObj.Value : "";
                            break;
                        default:
                            row[config.Display] = "";
                            break;
                    }
                }
                // insert row into table
                table.Rows.Add(row);
            }

            // sort salary board config by column name
            salaryBoardConfigs = salaryBoardConfigs.OrderBy(o => ExcelColumnNameToNumber(o.ColumnExcel)).ToList();

            context.Response.Write("{{\"SalaryBoardConfigs\":{0},\"SalaryBoardDynamics\":{1}}}".FormatWith(Ext.Net.JSON.Serialize(salaryBoardConfigs), Ext.Net.JSON.Serialize(table)));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// TODO: fix total column ?
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        private static int ExcelColumnNameToNumber(string columnName)
        {
            columnName = columnName.ToUpperInvariant();

            int sum = 0;

            foreach(var t in columnName)
            {
                sum *= 26;
                sum += (t - 'A' + 1);
            }

            return sum;
        }
    }
}