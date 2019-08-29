using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using Ext.Net;
using SmartXLS;
using Web.Core;
using Web.Core.Framework;
using Web.Core.Framework.Common;
using Web.Core.Framework.Controller;
using Web.Core.Framework.Model;
using Web.Core.Framework.Utils;
using Web.Core.Object.Salary;
using Web.Core.Service.Salary;

namespace Web.HRM.Modules.Salary
{
    public partial class PayrollDetail : BasePage
    {
        private const string SalaryBoardListUrl = "~/Modules/Salary/SalaryBoardList.aspx";
        private const string ImportSalaryExcelFile = "/importSalaryBoardDynamicColumn.xlsx";
        private PayrollModel _payroll;
        private const string EmployeeCode = @"Mã nhân viên";
        private const string FullName = @"Họ tên";


        protected void Page_Load(object sender, EventArgs e)
        {
            hdfPayrollId.Text = Request.QueryString["id"];
            // get payroll
            _payroll = PayrollController.GetById(int.Parse(hdfPayrollId.Text));

            if (!ExtNet.IsAjaxRequest)
            {
                if (_payroll != null)
                {
                    hdfConfigId.Text = _payroll.ConfigId.ToString();
                    ReloadGrid();               
                }
            }

            InitDynamicColumn();
        }

        /// <summary>
        /// Sinh cột động
        /// </summary>
        public void InitDynamicColumn()
        {
            // get payroll
            var payroll = PayrollController.GetById(int.Parse(hdfPayrollId.Text));

            if (payroll == null) return;

            grpPayrollDetail.Title = payroll.Title;

            // get salary board config
            var salaryBoardConfigs = SalaryBoardConfigController.GetAll(payroll.ConfigId, true, null, null, null, null, null);


            //sort list by columnExcel
            salaryBoardConfigs.Sort((x, y) => CompareUtil.CompareStringByLength(x.ColumnExcel, y.ColumnExcel));


            foreach (var config in salaryBoardConfigs)
            {
                var recordField = new RecordField
                {
                    Name = config.ColumnCode
                };
                var column = new Column
                {
                    Header = config.Display,
                    DataIndex = recordField.Name,
                    Width = 120,
                    Align = Alignment.Right,
                    Renderer = { Fn = "RenderValue" },
                    Tooltip = config.Display,
                };
                switch (config.DataType)
                {
                    case SalaryConfigDataType.FieldFormula:
                    case SalaryConfigDataType.Formula:
                        column.Css = "font-weight:bold;";
                        break;
                    case SalaryConfigDataType.DynamicValue:
                        column.Css = "background:#e6eaf2;";
                        column.Editor.Add(new TextField());
                        break;
                }

                storePayrollDetail.AddField(recordField);
                grpPayrollDetail.ColumnModel.Columns.Add(column);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void ReloadGrid()
        {
            try
            {
                if (_payroll.Status == PayrollStatus.Locked)
                {
                    Dialog.Alert("Bảng lương đã khóa");
                    if (btnUnlock.Visible)
                        btnUnlock.Show();
                    if (btnLock.Visible)
                        btnLock.Hide();
                    if (btnEditByExcel.Visible)
                        btnEditByExcel.Disabled = true;
                }
                else
                {
                    if (btnUnlock.Visible)
                        btnUnlock.Hide();
                    if (btnLock.Visible)
                        btnLock.Show();
                    if (btnEditByExcel.Visible)
                        btnEditByExcel.Disabled = false;
                }
                grpPayrollDetail.Reload();
            }
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }

        #region Event method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBack_Click(object sender, DirectEventArgs e)
        {
            Response.Redirect(SalaryBoardListUrl + "?mId=" + MenuId, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLock_Click(object sender, DirectEventArgs e)
        {
            // get payroll
            _payroll = PayrollController.GetById(int.Parse(hdfPayrollId.Text));

            if (_payroll == null) return;

            // lock payroll
            _payroll.Status = PayrollStatus.Locked;

            // update payroll
            PayrollController.Update(_payroll);

            ReloadGrid();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUnlock_Click(object sender, DirectEventArgs e)
        {
            // get payroll
            _payroll = PayrollController.GetById(int.Parse(hdfPayrollId.Text));

            if (_payroll == null) return;

            // lock payroll
            _payroll.Status = PayrollStatus.Active;

            // update payroll
            PayrollController.Update(_payroll);

            ReloadGrid();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DownloadTemplate_Click(object sender, DirectEventArgs e)
        {
            // init table
            var dataTable = new DataTable();
            var salaryBoardConfigs = new List<SalaryBoardConfigModel>();
            var salaryBoardInfos = new List<SalaryBoardInfoModel>();

            // adjust table
            dataTable.Rows.Add();
            dataTable.Columns.Add(new DataColumn(EmployeeCode));
            dataTable.Columns.Add(new DataColumn(FullName));


            // get payroll
            if (!string.IsNullOrEmpty(hdfPayrollId.Text))
                _payroll = PayrollController.GetById(int.Parse(hdfPayrollId.Text));

            // get config
            if (_payroll != null)
                salaryBoardConfigs = SalaryBoardConfigController.GetAll(_payroll.ConfigId, null, null, null,
                    SalaryConfigDataType.DynamicValue, null, null);

            if (salaryBoardConfigs != null)
                foreach (var model in salaryBoardConfigs)
                    dataTable.Columns.Add(new DataColumn(model.Display));

            // get record by department
            if (_payroll != null)
                salaryBoardInfos = SalaryBoardInfoController.GetAll(null, _payroll.Id.ToString(), null, null);

            // fill employee name and code
            for (var i = 0; i < salaryBoardInfos.Count; i++)
            {
                dataTable.Rows.Add();
                dataTable.Rows[i][EmployeeCode] = salaryBoardInfos[i].EmployeeCode;
                dataTable.Rows[i][FullName] = salaryBoardInfos[i].FullName;
            }

            ExportToExcel(dataTable, "~/" + Constant.PathTemplate, ImportSalaryExcelFile);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdateImportExcel_Click(object sender, DirectEventArgs e)
        {
            var workbook = new WorkBook();

            // upload file

            if (fileExcel.HasFile)
            {
                var path = UploadFile(fileExcel, Constant.PathTemplate);
                if (path != null)
                {
                    // Read data from excel
                    workbook.readXLSX(Path.Combine(Server.MapPath("~/"), Constant.PathTemplate, path));

                    // Export to datatable
                    var dataTable = workbook.ExportDataTable(0, //first row
                        0, //first col
                        workbook.LastRow + 1, //last row
                        workbook.LastCol + 1, //last col
                        true, //first row as header
                        false //convert to DateTime object if it match date pattern
                    );

                    // get config by ID
                    var salaryBoardConfig = SalaryBoardConfigController.GetAll(int.Parse(hdfConfigId.Text), null, null, null,
                        SalaryConfigDataType.DynamicValue, null, null);

                    foreach (DataRow row in dataTable.Rows)
                    {
                        // get employee code
                        var employeeCode = row[EmployeeCode].ToString();
                        if (string.IsNullOrEmpty(employeeCode)) continue;

                        // get record by employee code
                        var record = RecordController.GetByEmployeeCode(employeeCode);

                        foreach (DataColumn col in dataTable.Columns)
                        {
                            foreach (var item in salaryBoardConfig)
                            {
                                // check column name exists
                                if (item.Display != col.ColumnName) continue;

                                // check empty string
                                var value = Convert.ToString(row[col], CultureInfo.InvariantCulture);
                                if (string.IsNullOrEmpty(value)) continue;

                                // check if dynamic column value exists
                                var salaryBoardDynamicColumn = SalaryBoardDynamicColumnController.GetAll(null, record.Id.ToString(), int.Parse(hdfPayrollId.Text), item.ColumnCode, true, null, null);

                                if (salaryBoardDynamicColumn.Count > 0)
                                {
                                    // update dynamic column value
                                    salaryBoardDynamicColumn.First().Value = value;
                                    SalaryBoardDynamicColumnController.Update(salaryBoardDynamicColumn.First());
                                }
                                else
                                {
                                    // create dynamic column value
                                    var salaryBoardConfigColumn = new SalaryBoardDynamicColumnModel()
                                    {
                                        RecordId = record.Id,
                                        SalaryBoardId = int.Parse(hdfPayrollId.Text),
                                        ColumnCode = item.ColumnCode,
                                        Value = value,
                                        Display = item.Display
                                    };
                                    SalaryBoardDynamicColumnController.Create(salaryBoardConfigColumn);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                Dialog.Alert("Bạn chưa chọn tệp tin đính kèm. Vui lòng chọn.");
                return;
            }
            Dialog.Alert("Cập nhật thành công");
            grpPayrollDetail.Reload();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAccept_Click(object sender, DirectEventArgs e)
        {
            if (string.IsNullOrEmpty(hdfEditList.Text)) return;
            // get column values
            var salaryBoardDynamicColumns = JSON.Deserialize<List<SalaryBoardDynamicColumnModel>>(hdfEditList.Text);

            // get insert list
            var updateList = salaryBoardDynamicColumns.Where(r => r.Id > 0).ToList();

            // get update list
            var insertList = salaryBoardDynamicColumns.Where(r => r.Id <= 0).ToList();

            // update value
            for (var i = 0; i < updateList.Count; i++)
            {
                SalaryBoardDynamicColumnController.Update(updateList[i]);
            }

            // insert value
            for (var i = insertList.Count - 1; i >= 0; i--)
            {
                SalaryBoardDynamicColumnController.Create(insertList[i]);
            }

            // reload
            hdfEditList.Reset();
            grpPayrollDetail.Store.Primary.CommitChanges();
            grpPayrollDetail.Reload();
            btnAcceptChange.Hidden = true;
            btnCancelChange.Hidden = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancelChange_Click(object sender, DirectEventArgs e)
        {
            
            hdfEditList.Reset();
            grpPayrollDetail.Store.Primary.CommitChanges();
            grpPayrollDetail.Reload();
            btnAcceptChange.Hidden = true;
            btnCancelChange.Hidden = true;
        }

        #endregion

        #region Direct Method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        [DirectMethod]
        public void SaveData(string json)
        {
            // get payroll
            _payroll = PayrollController.GetById(int.Parse(hdfPayrollId.Text));

            if (_payroll == null) return;

            // save data
            _payroll.Data = json;

            var table = PayrollController.GetPayrollDetail(null, _payroll.Id, null, null);
            if (table.Rows.Count > 0)
            {
                for (var i = 0; i < table.Rows.Count; i++)
                {
                    //save payrollInfo
                    var payrollInfo = new PayrollInfoModel()
                    {
                        SalaryBoardId = _payroll.Id,
                        RecordId = table.Rows[i]["RecordId"] != null ? Convert.ToInt32(table.Rows[i]["RecordId"].ToString()) : 0,
                        TotalIncome = 0,
                        IndividualTax = 0,
                        EnterpriseSocial = 0,
                        LaborerSocial = 0,
                        ActualSalary = 0,
                        Month = _payroll.Month,
                        Year = _payroll.Year,
                        CreatedBy = "admin",
                        CreatedDate = DateTime.Now,
                        EditedBy = "",
                        EditedDate = DateTime.Now,
                        IsDeleted = false
                    };

                    //check exist column
                    if (table.Columns.Contains("{0}".FormatWith(Constant.IndividualTax)))
                    {
                        payrollInfo.IndividualTax = table.Rows[i]["IndividualTax"] != null
                            ? Convert.ToDecimal(table.Rows[i]["IndividualTax"].ToString())
                            : 0;
                    }
                    if (table.Columns.Contains("{0}".FormatWith(Constant.TotalIncome)))
                    {
                        payrollInfo.TotalIncome = table.Rows[i]["TotalIncome"] != null
                            ? Convert.ToInt32(table.Rows[i]["TotalIncome"].ToString())
                            : 0;
                    }
                    if (table.Columns.Contains("{0}".FormatWith(Constant.EnterpriseSocialInsurance)))
                    {
                        payrollInfo.EnterpriseSocial = table.Rows[i]["EnterpriseSocialInsurance"] != null
                            ? Convert.ToInt32(table.Rows[i]["EnterpriseSocialInsurance"].ToString())
                            : 0;
                    }
                    if (table.Columns.Contains("{0}".FormatWith(Constant.LaborerSocialInsurance)))
                    {
                        payrollInfo.LaborerSocial = table.Rows[i]["LaborerSocialInsurance"] != null
                            ? Convert.ToInt32(table.Rows[i]["LaborerSocialInsurance"].ToString())
                            : 0;
                    }
                    if (table.Columns.Contains("{0}".FormatWith(Constant.ActualSalary)))
                    {
                        payrollInfo.ActualSalary = table.Rows[i]["ActualSalary"] != null
                            ? Convert.ToInt32(table.Rows[i]["ActualSalary"].ToString())
                            : 0;
                    }

                    var checkExistModel = PayrollInfoController.GetUnique(payrollInfo.SalaryBoardId, payrollInfo.RecordId, payrollInfo.Month, payrollInfo.Year);
                    if (checkExistModel != null)
                    {
                        payrollInfo.Id = checkExistModel.Id;
                        payrollInfo.EditedDate = DateTime.Now;
                        payrollInfo.EditedBy = CurrentUser.User.UserName;
                        //update
                        PayrollInfoController.Update(payrollInfo);
                    }
                    else
                    {
                        //create
                        PayrollInfoController.Create(payrollInfo);
                    }   
                }
            }

            // update payroll
            PayrollController.Update(_payroll);
        }

        [DirectMethod]
        public void SetEditValues(string recordId, string columnCode, string value)
        {
            btnAcceptChange.Hidden = false;
            btnCancelChange.Hidden = false;

            List<SalaryBoardDynamicColumnModel> list;

            SalaryBoardDynamicColumnModel salaryBoardDynamicColumn;

            // get edit column
            var salaryBoardDynamicColumns = SalaryBoardDynamicColumnController.GetAll(null, recordId, int.Parse(hdfPayrollId.Text), columnCode, true, null, 1);

            if (salaryBoardDynamicColumns == null || salaryBoardDynamicColumns.Count == 0)
            {
                // create new column value
                salaryBoardDynamicColumn = new SalaryBoardDynamicColumnModel
                {
                    RecordId = int.Parse(recordId),
                    SalaryBoardId = int.Parse(hdfPayrollId.Text),
                    ColumnCode = columnCode,
                    Value = value,
                    IsInUsed = true
                };
            }
            else
            {
                // update column value
                salaryBoardDynamicColumn = salaryBoardDynamicColumns.First();
                salaryBoardDynamicColumn.Value = value;
            }

            // set text hidden field
            if (!string.IsNullOrEmpty(hdfEditList.Text))
            {
                list = JSON.Deserialize<List<SalaryBoardDynamicColumnModel>>(hdfEditList.Text);
                list.Add(salaryBoardDynamicColumn);
            }
            else
            {
                list = new List<SalaryBoardDynamicColumnModel> { salaryBoardDynamicColumn };
            }

            hdfEditList.Text = JSON.Serialize(list);
        }

        #endregion

    }
}