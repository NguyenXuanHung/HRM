using System;
using Ext.Net;
using Web.Core.Framework;
using System.Linq;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.HumanRecord;
using SmartXLS;
using System.Data;
using System.Globalization;
using System.IO;
using SoftCore;
using Web.Core;
using Web.Core.Service.Salary;

namespace Web.HJM.Modules.Salary
{
    public partial class SalaryColumnDynamic : BasePage
    {
        private const string PathTemplate = "../../File/Template";

        private const string EmployeeCodeColumn = @"Mã nhân viên";
        private const string FullName = @"Họ tên";

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!ExtNet.IsAjaxRequest)
            {
                hdfMenuID.Text = MenuId.ToString();
                hdfDepartments.Text = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
                hdfSalaryBoardListID.Text = Request.QueryString["id"];
                InitControl();
            }

            if (btnEdit.Visible)
            {
                RowSelectionModel1.Listeners.RowSelect.Handler += "if(hdfIsLocked.getValue() == 'true' ){btnEdit.disable();}else{btnEdit.enable();}  ";
                RowSelectionModel1.Listeners.RowDeselect.Handler += "btnEdit.disable(); ";
                gridColumnDynamic.DirectEvents.RowDblClick.Event += BtnEdit_Click;
            }

            if (btnDelete.Visible)
            {
                RowSelectionModel1.Listeners.RowSelect.Handler += "if(hdfIsLocked.getValue() == 'true' ){ btnDelete.disable();} else { btnDelete.enable(); }";
                RowSelectionModel1.Listeners.RowDeselect.Handler += "btnDelete.disable(); ";
            }

            ucChooseEmployee.AfterClickAcceptButton += new EventHandler(ucChooseEmployee_AfterClickAcceptButton);
        }

        private void InitControl()
        {
            if (!string.IsNullOrEmpty(hdfSalaryBoardListID.Text))
            {
                //set configId
                var salaryBoard = sal_PayrollServices.GetById(Convert.ToInt32(hdfSalaryBoardListID.Text));
                if (salaryBoard != null)
                {
                    hdfConfigId.Text = salaryBoard.ConfigId.ToString();
                    gridColumnDynamic.Title = @"Cấu hình cột động " + salaryBoard.Title;
                }
            }
            else
            {
                btnAdd.Disabled = true;
                btnDelete.Disabled = true;
                btnEdit.Disabled = true;
                btnImportExcel.Disabled = true;
                wdSalaryBoardManage.Show();
            }
        }

        private void ucChooseEmployee_AfterClickAcceptButton(object sender, EventArgs e)
        {
            hdfTotalRecord.Text = ucChooseEmployee.SelectedRow.Count.ToString();
            foreach (var item in ucChooseEmployee.SelectedRow)
            {
                // get employee information
                var hs = RecordController.GetByEmployeeCode(item.RecordID);
                var recordId = hs.Id.ToString();
                var employeeCode = hs.EmployeeCode;
                var fullName = hs.FullName;
                var departmentName = hs.DepartmentName;
                // insert record to grid
                RM.RegisterClientScriptBlock("insert" + recordId,
                    string.Format("addRecord('{0}', '{1}', '{2}', '{3}');", recordId, employeeCode, fullName,
                        departmentName));
            }
        }


        #region Edit and save data
        protected void BtnUpdateHL_Click(object sender, DirectEventArgs e)
        {
            try
            {
                var listId = e.ExtraParams["ListRecordId"].Split(',');
                if (listId.Count() < 1)
                {
                    ExtNet.Msg.Alert("Thông báo", "Bạn hãy chọn ít nhất 1 cán bộ").Show();
                    return;
                }

                for (var i = 0; i < listId.Length - 1; i++)
                {
                    var recordId = listId[i];
                    var colDynamic = new hr_SalaryBoardDynamicColumn()
                    {
                        RecordId = Convert.ToInt32(recordId),
                        ColumnCode = cbxColumnCode.Text,
                        Display = txtDisplay.Text,
                        ColumnExcel = txtColumnExcel.Text,
                        IsInUsed = chk_IsInUsed.Checked,
                        Value = txtValue.Text,
                        CreatedDate = DateTime.Now,
                        SalaryBoardId = int.Parse(hdfSalaryBoardListID.Text)
                    };

                    //create 
                    hr_SalaryBoardDynamicColumnService.Create(colDynamic);

                    wdColumnDynamic.Hide();
                    gridColumnDynamic.Reload();
                }

            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Thông báo từ hệ thống", "Có lỗi xảy ra: " + ex.Message).Show();
            }
        }

        [DirectMethod]
        public void SetColumnName()
        {
            if (!string.IsNullOrEmpty(hdfConfigId.Text))
            {
                var configs = hr_SalaryBoardConfigServices.GetAllConfigs(Convert.ToInt32(hdfConfigId.Text));
                var listConfig = configs.Where(d => d.ColumnCode == hdfColumnCode.Text).FirstOrDefault();
                if (listConfig != null)
                {
                    txtDisplay.Text = listConfig.Display;
                }
            }
        }

        protected void btnBack_Click(object sender, DirectEventArgs e)
        {
            Response.Redirect("~/Modules/Salary/SalaryBoardList.aspx");
        }

        protected void BtnEdit_Click(object sender, DirectEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(hdfId.Text))
                {
                    var colDynamic = hr_SalaryBoardDynamicColumnService.GetById(Convert.ToInt32(hdfId.Text));
                    if (colDynamic != null)
                    {
                        var record = hr_RecordServices.GetById(colDynamic.RecordId);
                        if (record != null)
                        {
                            txtFullName.Text = record.FullName;
                            txtEmployeeCode.Text = record.EmployeeCode;
                        }
                        txtUpdateColumnCode.Text = colDynamic.ColumnCode;
                        txtUpdateColumnExcel.Text = colDynamic.ColumnExcel;
                        chk_IsInUsedUpdate.Checked = colDynamic.IsInUsed;
                        txtUpdateValue.Text = colDynamic.Value;
                        txtUpdateDisplay.Text = colDynamic.Display;
                    }

                    wdUpdateColumnDynamic.Show();
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }

        protected void BtnUpdate_Click(object sender, DirectEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(hdfId.Text))
                {
                    var colDynamic = hr_SalaryBoardDynamicColumnService.GetById(Convert.ToInt32(hdfId.Text));
                    if (colDynamic != null)
                    {
                        colDynamic.ColumnExcel = txtUpdateColumnExcel.Text;
                        colDynamic.Value = txtUpdateValue.Text;
                        colDynamic.IsInUsed = chk_IsInUsedUpdate.Checked;
                        colDynamic.Display = txtUpdateDisplay.Text;
                        colDynamic.ColumnCode = txtUpdateColumnCode.Text;
                    }

                    hr_SalaryBoardDynamicColumnService.Update(colDynamic);
                    wdUpdateColumnDynamic.Hide();
                    gridColumnDynamic.Reload();
                    ResetFormUpdate();
                }
            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Thông báo từ hệ thống", "Có lỗi xảy ra: " + ex.Message).Show();
            }
        }

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

        protected void BtnDelete_Click(object sender, DirectEventArgs e)
        {
            foreach (var item in RowSelectionModel1.SelectedRows)
            {
                //delete
                hr_SalaryBoardDynamicColumnService.Delete(int.Parse("0" + item.RecordID));

            }
            gridColumnDynamic.Reload();
        }
        /// <summary>
        /// Chọn bảng tính lương
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ChooseSalaryBoardList_Click(Object sender, DirectEventArgs e)
        {
            if (!string.IsNullOrEmpty(hdfSalaryBoardListID.Text))
            {
                //Lay danh sach bang luong
                Response.Redirect("SalaryColumnDynamic.aspx?id=" + hdfSalaryBoardListID.Text);
            }
        }
        protected void DownloadTemplate_Click(object sender, DirectEventArgs e)
        {
            var serverPath = Server.MapPath(PathTemplate + "/importSalaryBoardDynamicColumn.xlsx");

            // create excel file
            var dataTable = new DataTable();
            var salaryBoardConfig = hr_SalaryBoardConfigServices.GetByConfigId(SalaryConfigDataType.DynamicValue, int.Parse(hdfConfigId.Text));
            var salaryBoardList = sal_PayrollServices.GetById(int.Parse(hdfSalaryBoardListID.Text));
           // var department = hr_RecordServices.GetByDepartmentId(salaryBoardList.DepartmentId);

            dataTable.Rows.Add();
            dataTable.Columns.Add(new DataColumn(EmployeeCodeColumn));
            dataTable.Columns.Add(new DataColumn(FullName));
            //for (var i = 0; i < salaryBoardConfig.Count; i++)
            //{
            //    dataTable.Columns.Add(new DataColumn(salaryBoardConfig[i].Display));
            //}
            //for (var i = 0; i < department.Count; i++)
            //{
            //    dataTable.Rows.Add();
            //    dataTable.Rows[i][EmployeeCodeColumn] = department[i].EmployeeCode;
            //    dataTable.Rows[i][FullName] = department[i].FullName;
            //}

            var workBook = new WorkBook();
            workBook.ImportDataTable(dataTable, true, 0, 0, dataTable.Rows.Count + 1, dataTable.Columns.Count + 1);
            workBook.writeXLSX(serverPath);

            Response.AddHeader("Content-Disposition", "attachment; filename=" + "importSalaryBoardDynamicColumn.xlsx");
            if (serverPath != null) Response.WriteFile(serverPath);
            Response.End();
        }

        protected void btnUpdateImportExcel_Click(object sender, DirectEventArgs e)
        {
            var workbook = new WorkBook();
            var fromRow = 2;
            var toRow = 2;

            // upload file

            var path = string.Empty;
            if (fileExcel.HasFile)
            {
                path = UploadFile(fileExcel, PathTemplate);
                if (path != null)
                {
                    // Read data from excel
                    workbook.readXLSX(path);

                    // Check validation workbook
                    if (CheckValidation(workbook, out fromRow, out toRow) == false)
                    {
                        return;
                    }

                    // Export to datatable
                    var dataTable = workbook.ExportDataTable(0, //first row
                        0, //first col
                        workbook.LastRow + 1, //last row
                        workbook.LastCol + 1, //last col
                        true, //first row as header
                        false //convert to DateTime object if it match date pattern
                    );

                    var salaryBoardConfig = hr_SalaryBoardConfigServices.GetByConfigId(null, int.Parse(hdfConfigId.Text));
                    foreach (DataRow row in dataTable.Rows)
                    {
                        var employeeCode = row[EmployeeCodeColumn].ToString();
                        if (!string.IsNullOrEmpty(employeeCode))
                        {
                            var recordId = hr_RecordServices.GetRecordIdByEmployeeCode(employeeCode);
                            foreach (DataColumn col in dataTable.Columns)
                            {
                                foreach (var item in salaryBoardConfig)
                                {
                                    if (item.Display == col.ColumnName)
                                    {
                                        // check if item exists
                                        var salaryBoardDynamicColumn = hr_SalaryBoardDynamicColumnService.GetBoardDynamicColumn(recordId, item.ColumnCode, int.Parse(hdfSalaryBoardListID.Text));
                                        var value = Convert.ToString(row[col], CultureInfo.InvariantCulture);
                                        if (salaryBoardDynamicColumn != null)
                                        {
                                            salaryBoardDynamicColumn.Value = value;
                                            hr_SalaryBoardDynamicColumnService.Update(salaryBoardDynamicColumn);
                                        }
                                        else
                                        {
                                            var salaryBoardConfigColumn = new hr_SalaryBoardDynamicColumn()
                                            {
                                                RecordId = recordId,
                                                SalaryBoardId = int.Parse(hdfSalaryBoardListID.Text),
                                                ColumnCode = item.ColumnCode,
                                                Value = value,
                                                IsInUsed = true,
                                                CreatedDate = DateTime.Now,
                                                Display = item.Display
                                            };
                                            hr_SalaryBoardDynamicColumnService.Create(salaryBoardConfigColumn);
                                        }
                                    }
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

        private bool CheckValidation(WorkBook workbook, out int fromRow, out int toRow)
        {
            fromRow = 2;
            toRow = 2;
            if (!txtSheetName.IsEmpty)
            {
                workbook.Sheet = workbook.findSheetByName(txtSheetName.Text);
            }
            else
            {
                workbook.Sheet = 0;
            }

            if (!txtFromRow.IsEmpty)
            {
                fromRow = Int32.Parse(txtFromRow.Text);
            }

            if (txtToRow.IsEmpty)
            {
                toRow = workbook.LastRow + 1;
            }
            else
            {
                toRow = Int32.Parse(txtToRow.Text);
            }

            if (fromRow < 2)
            {
                Dialog.Alert("Từ hàng lớn hơn 1");
                return false;
            }

            if (fromRow > toRow || toRow > workbook.LastRow + 1)
            {
                Dialog.Alert("Từ hàng đến hàng không hợp lệ, tệp đính kèm gồm có "
                             + (workbook.LastCol + 1) + " cột và " + (workbook.LastRow + 1) + " hàng.");
                return false;
            }

            return true;
        }

        private string UploadFile(object sender, string relativePath)
        {
            var obj = (FileUploadField)sender;
            var file = obj.PostedFile;
            var dir = new DirectoryInfo(Server.MapPath(relativePath));
            // if directory not exist then create this
            if (dir.Exists == false)
                dir.Create();
            var rdstr = Util.GetInstance().GetRandomString(7);
            var path = Server.MapPath(relativePath) + "/" + rdstr + "_" + obj.FileName;
            if (File.Exists(path))
                return "";
            file.SaveAs(path);
            return path;
        }
        #endregion
    }
}