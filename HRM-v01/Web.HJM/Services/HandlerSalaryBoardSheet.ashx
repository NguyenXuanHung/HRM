<%@ WebHandler Language="C#" Class="Web.HJM.Services.HandlerSalaryBoardSheet" %>

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

namespace Web.HJM.Services
{
    public class HandlerSalaryBoardSheet : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json; charset=UTF-8";
            context.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            context.Response.AppendHeader("Access-Control-Allow-Credentials", "true");

            var salaryBoardId = context.Request.QueryString["id"];
            var boardList = sal_PayrollServices.GetById(Convert.ToInt32(salaryBoardId));
            var configId = 0;
            if (boardList != null)
            {
                configId = boardList.ConfigId;
            }

            var salaryInfos = SalaryBoardInfoController.GetAllBoardInfo(salaryBoardId);
            var salaryBoardConfigs = hr_SalaryBoardConfigServices.GetAllConfigs(configId);
            //sort list by columnExcel
            salaryBoardConfigs.Sort((x, y) => CompareUtil.CompareStringByLength(x.ColumnExcel, y.ColumnExcel));

            // dynamic column list
            var salaryBoardDynamics = hr_SalaryBoardDynamicColumnService.GetAll();

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
            foreach (var config in salaryBoardConfigs)
            {
                // Setting column names as Property names
                table.Columns.Add(config.Display);
            }

            // fill data into table
            foreach (var salaryInfo in salaryInfos)
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
                foreach (var config in salaryBoardConfigs)
                {
                    switch (config.DataType)
                    {
                        case SalaryConfigDataType.FieldValue:
                            var type = salaryInfo.GetType();
                            var propInfo = type.GetProperty(config.ColumnCode);
                            var value = Convert.ToString(propInfo.GetValue(salaryInfo, BindingFlags.GetProperty, null, null, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
                            row[config.Display] = propInfo != null ? value : "";
                            break;
                        case SalaryConfigDataType.FixedValue:
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

        private static int ExcelColumnNameToNumber(string columnName)
        {
            columnName = columnName.ToUpperInvariant();

            int sum = 0;

            for (int i = 0; i < columnName.Length; i++)
            {
                sum *= 26;
                sum += (columnName[i] - 'A' + 1);
            }

            return sum;
        }
    }
}