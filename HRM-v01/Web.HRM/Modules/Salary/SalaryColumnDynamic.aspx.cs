using System;
using System.Collections.Generic;
using Ext.Net;
using Web.Core.Framework;
using System.Linq;
using Web.Core.Service.HumanRecord;
using SmartXLS;
using System.Data;
using System.Globalization;
using System.IO;
using Web.Core;
using Web.Core.Framework.Common;
using Web.Core.Framework.Controller;
using Web.Core.Framework.Model;
using Web.Core.Service.Salary;

namespace Web.HRM.Modules.Salary
{
    public partial class SalaryColumnDynamic : BasePage
    {
        private const string EmployeeCode = @"Mã nhân viên";
        private const string FullName = @"Họ tên";

        private const string ImportSalaryExcelFile = "/importSalaryBoardDynamicColumn.xlsx";
        private const string SalaryColumnDynamicUrl = "SalaryColumnDynamic.aspx";
        private const string SalaryBoardListUrl = "~/Modules/Salary/SalaryBoardList.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!ExtNet.IsAjaxRequest)
            {
                hdfMenuID.Text = MenuId.ToString();
                hdfDepartments.Text = DepartmentIds;
                hdfSalaryBoardListID.Text = Request.QueryString["id"];
                InitControl();
                InitDynamicColumn();
            }

            if (btnEdit.Visible)
            {
                RowSelectionModel1.Listeners.RowSelect.Handler += "if(hdfIsLocked.getValue() == 'true' ){btnEdit.disable();}else{btnEdit.enable();}  ";
                RowSelectionModel1.Listeners.RowDeselect.Handler += "btnEdit.disable(); ";
            }

            if (btnDelete.Visible)
            {
                RowSelectionModel1.Listeners.RowSelect.Handler += "if(hdfIsLocked.getValue() == 'true' ){ btnDelete.disable();} else { btnDelete.enable(); }";
                RowSelectionModel1.Listeners.RowDeselect.Handler += "btnDelete.disable(); ";
            }
        }

        #region Private Methods

        /// <summary>
        /// Sinh cột và record field động
        /// </summary>
        private void InitDynamicColumn()
        {
            if(string.IsNullOrEmpty(hdfSalaryBoardListID.Text)) return;

            // get payroll
            var payroll = PayrollController.GetById(Convert.ToInt32(hdfSalaryBoardListID.Text));

            if (payroll == null) return;

            // get payroll config
            var salaryBoardConfigs = SalaryBoardConfigController.GetAll(payroll.ConfigId, true, null, null,
                SalaryConfigDataType.DynamicValue, null, null);

            foreach (var salaryBoardConfig in salaryBoardConfigs)
            {
                // init record field
                var recordField = new RecordField
                {
                    Name = salaryBoardConfig.ColumnCode,
                };

                // init column
                var column = new Column
                {
                    DataIndex = recordField.Name,
                    Header = salaryBoardConfig.Display,
                    Width = 120,
                    Align = Alignment.Right,
                    Tooltip = salaryBoardConfig.Description,
                    Editor = { new TextField()},
                    Renderer = { Fn = "RenderValue"}
                };

                storeAdjustment.AddField(recordField);
                gridColumnDynamic.ColumnModel.Columns.Add(column);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitControl()
        {
            if (!string.IsNullOrEmpty(hdfSalaryBoardListID.Text))
            {
                //set configId
                var salaryBoard = sal_PayrollServices.GetById(Convert.ToInt32(hdfSalaryBoardListID.Text));
                if (salaryBoard == null) return;
                hdfConfigId.Text = salaryBoard.ConfigId.ToString();
                gridColumnDynamic.Title = @"Cấu hình cột động " + salaryBoard.Title;

                var infos = SalaryBoardInfoController.GetAll(null, hdfSalaryBoardListID.Text, null, null);
                if (infos.Count > 0)
                {
                    var listRecordIds = infos.Select(r => r.RecordId).ToList();
                    var recordIds = "";
                    foreach (var item in listRecordIds)
                    {
                        recordIds += "," + item ;
                    }
                    hdfRecordIds.Text = recordIds.TrimStart(',');
                }
            }
            else
            {
                btnAdd.Disabled = true;
                btnDelete.Disabled = true;
                btnEdit.Disabled = true;
                btnEditByExcel.Disabled = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void ResetFormUpdate()
        {
            gridColumnDynamic.Reload();
            txtUpdateColumnExcel.Reset();
            txtUpdateValue.Reset();
            txtUpdateColumnCode.Reset();
            txtUpdateDisplay.Reset();
            txtSheetName.Reset();
            fileExcel.Reset();
            chk_IsInUsedUpdate.Checked = false;
        }
      
        #endregion

        #region DirectMethod

        /// <summary>
        /// 
        /// </summary>
        [DirectMethod]
        public void SetColumnName()
        {
            if (string.IsNullOrEmpty(hdfConfigId.Text)) return;
            // get salary board config
            var configs = hr_SalaryBoardConfigServices.GetAllConfigs(Convert.ToInt32(hdfConfigId.Text));
            var listConfig = configs.FirstOrDefault(d => d.ColumnCode == hdfColumnCode.Text);
            if (listConfig != null)
            {
                txtDisplay.Text = listConfig.Display;
            }
        }

        [DirectMethod]
        public void SetEditValues(string recordId, string columnCode, string value)
        {
            // show button
            btnAcceptChange.Hidden = false;
            btnCancelChange.Hidden = false;

            List<SalaryBoardDynamicColumnModel> list;

            SalaryBoardDynamicColumnModel salaryBoardDynamicColumn;

            // get edit column
            var salaryBoardDynamicColumns = SalaryBoardDynamicColumnController.GetAll(null, recordId, int.Parse(hdfSalaryBoardListID.Text), columnCode, true, null, 1);

            if (salaryBoardDynamicColumns == null || salaryBoardDynamicColumns.Count == 0)
            {
                // create new column value
                salaryBoardDynamicColumn = new SalaryBoardDynamicColumnModel
                {
                    RecordId = int.Parse(recordId),
                    SalaryBoardId = int.Parse(hdfSalaryBoardListID.Text),
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
                list = new List<SalaryBoardDynamicColumnModel> {salaryBoardDynamicColumn};
            }

            hdfEditList.Text = JSON.Serialize(list);
        }

        #endregion

        #region Event Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnUpdateHL_Click(object sender, DirectEventArgs e)
        {
            try
            {
                foreach (var itemRow in chkEmployeeRowSelection.SelectedRows)
                {
                    var colDynamic = new SalaryBoardDynamicColumnModel
                    {
                        RecordId = Convert.ToInt32(itemRow.RecordID),
                        ColumnCode = cbxColumnCode.Text,
                        Display = txtDisplay.Text,
                        ColumnExcel = txtColumnExcel.Text,
                        Value = txtValue.Text,
                        CreatedDate = DateTime.Now,
                        SalaryBoardId = int.Parse(hdfSalaryBoardListID.Text)
                    };

                    //create 
                    SalaryBoardDynamicColumnController.Create(colDynamic);
                }

                //reload
                wdColumnDynamic.Hide();
                gridColumnDynamic.Reload();

            }
            catch (Exception ex)
            {
                Dialog.Alert("Thông báo từ hệ thống", "Có lỗi xảy ra: " + ex.Message);
            }
        }


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
        protected void BtnEdit_Click(object sender, DirectEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(hdfId.Text)) return;
                // get dynamic column
                var colDynamic = SalaryBoardDynamicColumnController.GetById(Convert.ToInt32(hdfId.Text));
                if (colDynamic != null)
                {
                    txtFullName.Text = colDynamic.FullName;
                    txtEmployeeCode.Text = colDynamic.EmployeeCode;
                    txtUpdateColumnCode.Text = colDynamic.ColumnCode;
                    txtUpdateColumnExcel.Text = colDynamic.ColumnExcel;
                    chk_IsInUsedUpdate.Checked = colDynamic.IsInUsed;
                    txtUpdateValue.Text = colDynamic.Value;
                    txtUpdateDisplay.Text = colDynamic.Display;
                }

                wdUpdateColumnDynamic.Show();
            }
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnUpdate_Click(object sender, DirectEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(hdfId.Text)) return;
                var colDynamic = SalaryBoardDynamicColumnController.GetById(Convert.ToInt32(hdfId.Text));
                if(colDynamic != null)
                {
                    colDynamic.ColumnExcel = txtUpdateColumnExcel.Text;
                    colDynamic.Value = txtUpdateValue.Text;
                    colDynamic.IsInUsed = chk_IsInUsedUpdate.Checked;
                    colDynamic.Display = txtUpdateDisplay.Text;
                    colDynamic.ColumnCode = txtUpdateColumnCode.Text;
                };
                SalaryBoardDynamicColumnController.Update(colDynamic);

                wdUpdateColumnDynamic.Hide();
                gridColumnDynamic.Reload();
                ResetFormUpdate();
            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Thông báo từ hệ thống", "Có lỗi xảy ra: " + ex.Message).Show();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnDelete_Click(object sender, DirectEventArgs e)
        {
            foreach (var item in RowSelectionModel1.SelectedRows)
            {
                //delete
                SalaryBoardDynamicColumnController.Delete(int.Parse("0" + item.RecordID));

            }
            gridColumnDynamic.Reload();
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
            var payroll = new PayrollModel();
            var salaryBoardInfos = new List<SalaryBoardInfoModel>();

            // adjust table
            dataTable.Rows.Add();
            dataTable.Columns.Add(new DataColumn(EmployeeCode));
            dataTable.Columns.Add(new DataColumn(FullName));

            // get config
            if (!string.IsNullOrEmpty(hdfConfigId.Text))
                salaryBoardConfigs = SalaryBoardConfigController.GetAll(int.Parse(hdfConfigId.Text), null, null, null,
                    SalaryConfigDataType.DynamicValue, null, null);

            // get payroll
            if (!string.IsNullOrEmpty(hdfSalaryBoardListID.Text))
                payroll = PayrollController.GetById(int.Parse(hdfSalaryBoardListID.Text));


            if (salaryBoardConfigs != null)
                foreach (var model in salaryBoardConfigs)
                    dataTable.Columns.Add(new DataColumn(model.Display));

            // get record by department
            if (payroll != null)
                salaryBoardInfos = SalaryBoardInfoController.GetAll(null, payroll.Id.ToString(), null, null);

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
                                var salaryBoardDynamicColumn = SalaryBoardDynamicColumnController.GetAll(null, record.Id.ToString(), int.Parse(hdfSalaryBoardListID.Text), item.ColumnCode, true, null, null);

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
                                        SalaryBoardId = int.Parse(hdfSalaryBoardListID.Text),
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
            ResetFormUpdate();
            Dialog.Alert("Cập nhật thành công");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAcceptChange_Click(object sender, DirectEventArgs e)
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
            storeAdjustment.CommitChanges();
            gridColumnDynamic.Reload();
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
            storeAdjustment.CommitChanges();
            gridColumnDynamic.Reload();
            btnAcceptChange.Hidden = true;
            btnCancelChange.Hidden = true;
        }


        #endregion

    }
}