using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Core.Framework.Common;
using Web.Core.Framework.Model;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.HumanRecord;

namespace Web.Core.Framework.Controller
{
    public class SalaryBoardDynamicColumnController
    {

        public static List<SalaryBoardDynamicColumnModel> GetAll(string keyword, string recordIds, int? salaryBoardId, string columnCode,
            bool? isInUsed, string order, int? limit)
        {
            var condition = Constant.ConditionDefault;

            if (!string.IsNullOrEmpty(keyword))
                condition += " AND [Display] LIKE N'%{0}%' OR [ColumnCode] LIKE N'%{0}%'".FormatWith(keyword);

            if (!string.IsNullOrEmpty(recordIds))
                condition += " AND [RecordId] IN ({0})".FormatWith(recordIds);

            if (!string.IsNullOrEmpty(columnCode))
                condition += " AND [ColumnCode] = '{0}' ".FormatWith(columnCode);

            if (salaryBoardId != null)
                condition += " AND [SalaryBoardId] = {0}".FormatWith(salaryBoardId);

            if (isInUsed != null)
                condition += " AND [IsInUsed] = {0}".FormatWith((bool)isInUsed ? 1 : 0);

            return hr_SalaryBoardDynamicColumnService.GetAll(condition, order, limit).Select(r => new SalaryBoardDynamicColumnModel(r)).ToList();
        }

        public static PageResult<SalaryBoardDynamicColumnModel> GetPaging(string keyword, string recordIds, int? salaryBoardId, string columnCode,
            bool? isInUsed, string order, int start, int limit)
        {
            var condition = Constant.ConditionDefault;

            if (!string.IsNullOrEmpty(keyword))
                condition += " AND [Display] LIKE N'%{0}%' OR [ColumnCode] LIKE N'%{0}%' OR (SELECT rc.[FullName] FROM hr_Record rc WHERE rc.Id = hr_SalaryBoardDynamicColumn.RecordId) LIKE N'%{0}%'".FormatWith(keyword);

            if (!string.IsNullOrEmpty(recordIds))
                condition += " AND [RecordId] IN ({0})".FormatWith(recordIds);

            if (!string.IsNullOrEmpty(columnCode))
                condition += " AND [ColumnCode] = '{0}' ".FormatWith(columnCode);

            if (salaryBoardId != null)
                condition += " AND [SalaryBoardId] = {0}".FormatWith(salaryBoardId);

            if (isInUsed != null)
                condition += " AND [IsInUsed] = {0}".FormatWith((bool)isInUsed ? 1 : 0);

            var result = hr_SalaryBoardDynamicColumnService.GetPaging(condition, order, start, limit);

            return new PageResult<SalaryBoardDynamicColumnModel>(result.Total, result.Data.Select(r => new SalaryBoardDynamicColumnModel(r)).ToList());
        }

        public static SalaryBoardDynamicColumnModel GetById(int id)
        {
            var entity = hr_SalaryBoardDynamicColumnService.GetById(id);
            return entity == null ? null : new SalaryBoardDynamicColumnModel(entity);
        }

        public static SalaryBoardDynamicColumnModel Create(SalaryBoardDynamicColumnModel model)
        {
            var entity = new hr_SalaryBoardDynamicColumn();

            // check if column value exists
            var existsModel = GetAll(null, model.RecordId.ToString(), model.SalaryBoardId, model.ColumnCode, true, null, 1);
            if (existsModel != null && existsModel.Count > 0) return null;

            model.FillEntity(ref entity);

            return new SalaryBoardDynamicColumnModel(hr_SalaryBoardDynamicColumnService.Create(entity));
        }

        public static SalaryBoardDynamicColumnModel Update(SalaryBoardDynamicColumnModel model)
        {
            var entity = hr_SalaryBoardDynamicColumnService.GetById(model.Id);
            if (entity == null) return null;
            model.FillEntity(ref entity);
            return new SalaryBoardDynamicColumnModel(hr_SalaryBoardDynamicColumnService.Update(entity));
        }

        public static SalaryBoardDynamicColumnModel Delete(int id)
        {
            var model = GetById(id);
            if (model == null) return null;
            model.IsInUsed = false;
            return Update(model);
        }

        public static DataTable GetTable(string keyword,int payrollId, string order, int start, int limit)
        {
            var dataTable = new DataTable();

            // get payroll
            var payroll = PayrollController.GetById(payrollId);

            if (payroll == null) return null;

            // get payroll config
            var salaryBoardConfigs = SalaryBoardConfigController.GetAll(payroll.ConfigId, true, null, null,
                SalaryConfigDataType.DynamicValue, null, null);

            // get dynamic column value
            var salaryBoardDynamicColumns = GetAll(null, null, payrollId, null, true, null, null);

            // get salary board info
            var salaryInfos = SalaryBoardInfoController.GetPaging(keyword, payrollId.ToString(), order, start, limit);

            // generate fixed column
            var fixedColumn = new[] {
                new DataColumn("Id"),
                new DataColumn("RecordId"),
                new DataColumn("FullName"),
                new DataColumn("EmployeeCode"),
                new DataColumn("DepartmentName")
            };
            dataTable.Columns.AddRange(fixedColumn);

            // generate dynamic column
            if(salaryBoardConfigs != null && salaryBoardConfigs.Count > 0)
                foreach (var salaryBoardConfig in salaryBoardConfigs)
                {
                    dataTable.Columns.Add(new DataColumn(salaryBoardConfig.ColumnCode));
                }

            // fill data to table
            foreach (var info in salaryInfos.Data)
            {
                // init row
                var row = dataTable.NewRow();

                // fill data to row
                row["Id"] = info.Id;
                row["RecordId"] = info.RecordId;
                row["FullName"] = info.FullName;
                row["EmployeeCode"] = info.EmployeeCode;
                row["DepartmentName"] = info.DepartmentName;

                // fill dynamic value
                foreach (var salaryBoardDynamicColumn in salaryBoardDynamicColumns.Where(s => s.RecordId == info.RecordId))
                    if(row.Table.Columns.Contains(salaryBoardDynamicColumn.ColumnCode))
                        row[salaryBoardDynamicColumn.ColumnCode] = salaryBoardDynamicColumn.Value;

                // add row to data table
                dataTable.Rows.Add(row);
            }

            return dataTable;
        }

        /// <summary>
        /// delete
        /// </summary>
        /// <param name="salaryBoardId"></param>
        public static void DeleteByCondition(int salaryBoardId)
        {
            var condition = " [SalaryBoardId] = {0}".FormatWith(salaryBoardId);
            hr_SalaryBoardDynamicColumnService.Delete(condition);
        }
    }
}
