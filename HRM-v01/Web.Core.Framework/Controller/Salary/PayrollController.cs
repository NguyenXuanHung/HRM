using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using SmartXLS;
using Web.Core.Framework.Common;
using Web.Core.Object.Salary;
using Web.Core.Service.Salary;

namespace Web.Core.Framework.Controller
{
    public class PayrollController
    {
        /// <summary>
        /// Get payroll by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static PayrollModel GetById(int id)
        {
            var entity = sal_PayrollServices.GetById(id);
            return entity != null ? new PayrollModel(entity) : null;
        }

        /// <summary>
        /// Get payroll by unique code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static PayrollModel GetByCode(string code)
        {
            // check report name
            if(!string.IsNullOrEmpty(code))
            {
                var condition = "[Code]='{0}' AND [IsDeleted] = 0".FormatWith(code);
                var entity = sal_PayrollServices.GetByCondition(condition);
                return entity != null ? new PayrollModel(entity) : null;
            }
            // invalid name
            return null;
        }

        /// <summary>
        /// Get all payroll by condition
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="configId"></param>
        /// <param name="departments"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <param name="status"></param>
        /// <param name="isDeleted"></param>
        /// <param name="order"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<PayrollModel> GetAll(string keyword, int? configId, string departments, int? month, int? year, PayrollStatus? status,
            bool? isDeleted, string order, int? limit)
        {
            // init condition
            var condition = "1=1";
            // keyword
            if(!string.IsNullOrEmpty(keyword))
                condition += " AND ([Code] LIKE '%{0}%' OR [Title] LIKE '%{0}%' OR [Description] LIKE '%{0}%')".FormatWith(keyword.EscapeQuote());

            // config
            if(configId != null)
                condition += " AND [ConfigId]='{0}'".FormatWith(configId.Value);
            
            // month
            if(month != null)
                condition += " AND [Month]='{0}'".FormatWith(month.Value);

            // year
            if(year != null)
                condition += " AND [Year]='{0}'".FormatWith(year.Value);

            // status
            if(status != null)
                condition += " AND [Status]='{0}'".FormatWith((int)status.Value);

            // deleted
            if(isDeleted != null)
                condition += " AND [IsDeleted]='{0}'".FormatWith(isDeleted.Value ? 1 : 0);

            // order
            if (string.IsNullOrEmpty(order))
                order = "[Year],[Month] DESC";

            // return
            return sal_PayrollServices.GetAll(condition, order, limit).Select(r => new PayrollModel(r)).ToList();
        }

        /// <summary>
        /// Get payroll paging by condition
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="configId"></param>
        /// <param name="departments"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <param name="status"></param>
        /// <param name="isDeleted"></param>
        /// <param name="order"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static PageResult<PayrollModel> GetPaging(string keyword, int? configId, string departments, int? month, int? year, PayrollStatus? status,
            bool? isDeleted, string order, int start, int limit)
        {
            // init condition
            var condition = Constant.ConditionDefault;
            // keyword
            if(!string.IsNullOrEmpty(keyword))
                condition += " AND ([Code] LIKE N'%{0}%' OR [Title] LIKE N'%{0}%' OR [Description] LIKE N'%{0}%')".FormatWith(keyword.EscapeQuote());

            // config
            if(configId != null)
                condition += " AND [ConfigId]='{0}'".FormatWith(configId.Value);

            // month
            if(month != null)
                condition += " AND [Month]='{0}'".FormatWith(month.Value);

            // year
            if(year != null)
                condition += " AND [Year]='{0}'".FormatWith(year.Value);

            // status
            if(status != null)
                condition += " AND [Status]='{0}'".FormatWith((int)status.Value);

            // deleted
            if(isDeleted != null)
                condition += " AND [IsDeleted]='{0}'".FormatWith(isDeleted.Value ? 1 : 0);

            // order
            if(string.IsNullOrEmpty(order))
                order = "[Year],[Month] DESC";

            // get result
            var result = sal_PayrollServices.GetPaging(condition, order, start, limit);

            // return
            return new PageResult<PayrollModel>(result.Total, result.Data.Select(p => new PayrollModel(p)).ToList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="payrollId"></param>
        /// <param name="order"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static DataTable GetPayrollDetail(string keyword, int payrollId, string order, int? limit)
        {
            // get payroll
            var payroll = GetById(payrollId);

            if (payroll == null) return null;

            // get salary board info
            var salaryBoardInfos = SalaryBoardInfoController.GetAll(keyword, payrollId.ToString(), order, limit);

            if (payroll.Status == PayrollStatus.Locked)
            {
                return JsonConvert.DeserializeObject<DataTable>(payroll.Data);
            }

            // get salary board config
            var salaryBoardConfigs = SalaryBoardConfigController.GetAll(payroll.ConfigId, true, null, null, null, null, null);

            // get payroll dynamic data
            var salaryBoardDynamicColumns = SalaryBoardDynamicColumnController.GetAll(null, null, payroll.Id, null, true, null, null);


            var workbook = new WorkBook { AutoCalc = false };

            // generate default column
            workbook.setText("A1", "Id");
            workbook.setText("B1", "RecordId");
            workbook.setText("C1", "FullName");
            workbook.setText("D1", "EmployeeCode");
            workbook.setText("E1", "PositionName");
            // generate other column
            foreach (var config in salaryBoardConfigs)
            {
                // set column name as Property name
                workbook.setText(config.ColumnExcel + 1, config.ColumnCode);
            }

            // fill data into table
            for (var i = 0; i < salaryBoardInfos.Count; i++)
            {

                workbook.setNumber("A{0}".FormatWith(i + 2), salaryBoardInfos[i].Id);
                workbook.setNumber("B{0}".FormatWith(i + 2), salaryBoardInfos[i].RecordId);
                workbook.setText("C{0}".FormatWith(i + 2), salaryBoardInfos[i].FullName);
                workbook.setText("D{0}".FormatWith(i + 2), salaryBoardInfos[i].EmployeeCode);
                workbook.setText("E{0}".FormatWith(i + 2), salaryBoardInfos[i].PositionName);

                // add value for dynamic column
                foreach (var config in salaryBoardConfigs)
                {
                    var columnExcel = config.ColumnExcel + (i + 2);
                    switch (config.DataType)
                    {
                        case SalaryConfigDataType.FieldValue:
                            var type = salaryBoardInfos[i].GetType();
                            var propInfo = type.GetProperty(config.ColumnCode);
                            if (propInfo == null) break;
                            var value = Convert.ToString(propInfo.GetValue(salaryBoardInfos[i], BindingFlags.GetProperty, null, null, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
                            // convert to double
                            if (double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out var result))
                                workbook.setNumber(columnExcel, result);
                            else
                                workbook.setText(columnExcel, value);
                            break;
                        case SalaryConfigDataType.FixedValue:
                        case SalaryConfigDataType.FieldFormula:
                        case SalaryConfigDataType.Formula:
                            workbook.setFormula(columnExcel, config.Formula.FormatWith(i + 2).TrimStart('='));
                            break;
                        case SalaryConfigDataType.DynamicValue:
                            var findObj = salaryBoardDynamicColumns.Find(o =>
                                o.RecordId == salaryBoardInfos[i].RecordId && o.ColumnCode == config.ColumnCode);
                            if (findObj != null)
                                // convert to double
                                if (double.TryParse(findObj.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out var result2))
                                    workbook.setNumber(columnExcel, result2);
                                else
                                    workbook.setText(columnExcel, findObj.Value);
                            else
                                workbook.setText(columnExcel, string.Empty);
                            break;
                        default:
                            workbook.setText(columnExcel, string.Empty);
                            break;
                    }
                }
            }
            workbook.recalc();
            var table = workbook.ExportDataTable(0, 0, workbook.LastRow + 1, workbook.LastCol + 1);
            return workbook.ExportDataTable(0, 0, workbook.LastRow + 1, workbook.LastCol + 1);
        }

        /// <summary>
        /// Create payroll
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static PayrollModel Create(PayrollModel model)
        {
            // get existed payroll by code
            var existedEntity = GetByCode(model.Code);
            // check result
            if (existedEntity != null) return null;
            // init new entity
            var entity = new sal_Payroll();
            // set entity props
            model.FillEntity(ref entity);
            // insert
            return new PayrollModel(sal_PayrollServices.Create(entity));
        }

        /// <summary>
        /// Update payroll
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static PayrollModel Update(PayrollModel model)
        {
            // get existed payroll by code
            var existedEntity = GetByCode(model.Code);
            // check result
            if (existedEntity != null && existedEntity.Id != model.Id) return null;
            // init new entity
            var entity = new sal_Payroll();
            // set entity props
            model.FillEntity(ref entity);
            // update
            return new PayrollModel(sal_PayrollServices.Update(entity));
        }

        /// <summary>
        /// Change to deleted
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static PayrollModel Delete(int id)
        {
            // get payroll by id
            var model = GetById(id);
            // check result
            if(model != null)
            {
                // update deleted status
                model.IsDeleted = true;
                // update
                return Update(model);
            }
            // invalid id
            return null;
        }
    }
}
