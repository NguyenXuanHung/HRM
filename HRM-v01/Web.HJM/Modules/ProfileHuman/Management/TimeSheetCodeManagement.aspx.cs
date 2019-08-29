using System;
using System.Data;
using System.IO;
using Ext.Net;
using Web.Core.Framework;
using Web.Core;
using Web.Core.Object.HumanRecord;
using System.Linq;
using System.Web;
using SmartXLS;
using SoftCore;
using Web.Core.Service.HumanRecord;
using System.Reflection;
using System.Collections.Generic;
using Web.Core.Framework.Adapter;
using Web.Core.Framework.Common;

namespace Web.HJM.Modules.ProfileHuman.Management
{
    public partial class TimeSheetCodeManagement : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ExtNet.IsAjaxRequest)
            {
                hdfDepartments.Text = string.Join(",", CurrentUser.Departments.Select(d => d.Id));

                new Core.Framework.Common.BorderLayout()
                {
                    menuID = MenuId,
                    script = "#{hdfDepartmentSelectedId}.setValue('" + Core.Framework.Common.BorderLayout.nodeID +
                             "');#{txtSearch}.reset();#{PagingToolbar1}.pageIndex = 0; #{PagingToolbar1}.doLoad();"
                }.AddDepartmentList(brlayout, CurrentUser, true);
            }

            if (btnEdit.Visible)
            {
                gridTimeSheetCode.Listeners.RowDblClick.Handler +=
                    " if(CheckSelectedRows(gridTimeSheetCode)){btnUpdate.show();btnUpdateClose.hide()}";
            }

            //ucChooseEmployee.AfterClickAcceptButton += new EventHandler(ucChooseEmployee_AfterClickAcceptButton);
        }

        #region Event Method

        protected void InsertOrUpdate(object sender, DirectEventArgs e)
        {
            try
            {
                if (e.ExtraParams["Command"] == "Update")
                    Update();
                else
                    Insert(e);
                //reload data
                gridTimeSheetCode.Reload();
                cbxSelectedEmployee.Disabled = false;
            }
            catch (Exception ex)
            {
                ExtNet.MessageBox.Alert("Có lỗi xảy ra", ex.Message).Show();
            }
        }

        private void Insert(DirectEventArgs e)
        {
            try
            {
                var util = new Util();
                var startTime = string.Empty;
                var endTime = string.Empty;

                var timeSheet = new hr_TimeSheetCode()
                {
                    Code = txtTimeSheetCode.Text,
                    CreatedDate = DateTime.Now,
                };
                if (!string.IsNullOrEmpty(hdfEmployeeSelectedId.Text))
                    timeSheet.RecordId = Convert.ToInt32(hdfEmployeeSelectedId.Text);
                if (!util.IsDateNull(dfStartTime.SelectedDate))
                {
                    timeSheet.StartTime = dfStartTime.SelectedDate;
                    startTime = dfStartTime.SelectedDate.ToString("yyyy-MM-dd");
                }

                if (!util.IsDateNull(dfEndTime.SelectedDate))
                {
                    timeSheet.EndTime = dfEndTime.SelectedDate;
                    endTime = dfEndTime.SelectedDate.ToString("yyyy-MM-dd");
                }

                timeSheet.IsActive = chk_IsActive.Checked;
                var checkTime =
                    hr_TimeSheetCodeServices.CheckExitTimeSheetCode(txtTimeSheetCode.Text, startTime, endTime);
                if (checkTime != null && checkTime.Count > 0)
                {
                    Dialog.Alert("Mã chấm công đã tồn tại. Vui lòng nhập mã chấm công khác!");
                    return;
                }
                else
                {
                    hr_TimeSheetCodeServices.Create(timeSheet);
                }

                if (e.ExtraParams["Close"] == "True")
                {
                    wdTimeSheetCode.Hide();
                    ResetForm();
                }
            }
            catch (Exception ex)
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình thêm mới: {0}".FormatWith(ex.Message));
            }
        }

        private void Update()
        {
            try
            {
                if (!string.IsNullOrEmpty(hdfKeyRecord.Text))
                {
                    var util = new Util();
                    var startTime = string.Empty;
                    var endTime = string.Empty;
                    var timeSheet = hr_TimeSheetCodeServices.GetById(Convert.ToInt32(hdfKeyRecord.Text));
                    var currentTimeSheetCode = string.Empty;

                    if (timeSheet != null)
                    {
                        currentTimeSheetCode = timeSheet.Code;
                        timeSheet.Code = txtTimeSheetCode.Text;
                        if (!util.IsDateNull(dfStartTime.SelectedDate))
                        {
                            timeSheet.StartTime = dfStartTime.SelectedDate;
                            startTime = dfStartTime.SelectedDate.ToString("yyyy-MM-dd");
                        }

                        if (!util.IsDateNull(dfEndTime.SelectedDate))
                        {
                            timeSheet.EndTime = dfEndTime.SelectedDate;
                            endTime = dfEndTime.SelectedDate.ToString("yyyy-MM-dd");
                        }

                        timeSheet.EditedDate = DateTime.Now;
                        timeSheet.IsActive = chk_IsActive.Checked;
                    }

                    var checkTime =
                        hr_TimeSheetCodeServices.CheckExitTimeSheetCode(txtTimeSheetCode.Text, startTime, endTime);

                    if (checkTime.IsNullOrEmpty() || currentTimeSheetCode == txtTimeSheetCode.Text)
                    {
                        hr_TimeSheetCodeServices.Update(timeSheet);
                        Dialog.Alert("Cập nhật thành công");
                    }
                    else
                    {
                        Dialog.Alert("Mã chấm công đã tồn tại. Vui lòng nhập mã chấm công khác!");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình cập nhật: {0}".FormatWith(ex.Message));
            }
        }

        [DirectMethod]
        public void ResetForm()
        {
            txtTimeSheetCode.Text = "";
            dfStartTime.Reset();
            dfEndTime.Reset();
            cbxSelectedEmployee.Clear();
            hdfEmployeeSelectedId.Text = "";
            chk_IsActive.Checked = false;
        }

        protected void Delete(object sender, DirectEventArgs directEventArgs)
        {
            try
            {
                int id;
                if (!int.TryParse(hdfKeyRecord.Text, out id) || id <= 0) return;
                hr_TimeSheetCodeServices.Delete(id);
                gridTimeSheetCode.Reload();
                RM.RegisterClientScriptBlock("Grid_Reload", "ReloadGrid();");
            }
            catch (Exception ex)
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình xóa: {0}".FormatWith(ex.Message));
            }
        }

        protected void AddTimeSheetCode_Click(object sender, DirectEventArgs e)
        {
            wdTimeSheetCode.Title = @"Đăng ký mới mã chấm công";
            wdTimeSheetCode.Show();
            btnUpdate.Hide();
            btnUpdateClose.Show();
            cbxSelectedEmployee.Disabled = false;
            ResetForm();
        }

        protected void EditTimeSheetCode_Click(object sender, DirectEventArgs e)
        {
            int id;
            if (!int.TryParse(hdfKeyRecord.Text, out id) || id <= 0) return;
            var timeSheet = hr_TimeSheetCodeServices.GetById(id);
            if (timeSheet != null)
            {
                txtTimeSheetCode.Text = timeSheet.Code;
                dfStartTime.SetValue(timeSheet.StartTime);
                dfEndTime.SetValue(timeSheet.EndTime);
                hdfEmployeeSelectedId.Text = timeSheet.RecordId.ToString();
                cbxSelectedEmployee.Text = hr_RecordServices.GetFieldValueById(timeSheet.RecordId, "FullName");
                chk_IsActive.Checked = timeSheet.IsActive;
            }

            // show window
            btnUpdate.Show();
            btnUpdateClose.Hide();
            cbxSelectedEmployee.Disabled = true;

            wdTimeSheetCode.Title = @"Cập nhật mã chấm công";
            wdTimeSheetCode.Show();
        }

        public int count = 0;

        protected void ImportFile(object sender, DirectEventArgs e)
        {
            try
            {
                var workbook = new WorkBook();
                var fromRow = 2;
                var toRow = 2;

                // upload file

                var path = string.Empty;
                if (fileExcel.HasFile)
                {
                    path = UploadFile(fileExcel, Constant.PathTemplate);
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
                        var dataTable = workbook.ExportDataTable(fromRow - 1, //first row
                            0, //first col
                            toRow - 1, //last row
                            workbook.LastCol + 1, //last col
                            false, //first row as header
                            false //convert to DateTime object if it match date pattern
                        );

                        hdfDataTable.Text = JSON.Serialize(dataTable);

                        ContinueProcess(count);
                    }
                }
                else
                {
                    Dialog.Alert("Bạn chưa chọn tệp tin đính kèm. Vui lòng chọn.");
                    return;
                }
            }
            catch (Exception ex)
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình xử lý {0}".FormatWith(ex.Message));
            }
        }

        // Read data from excel file and save to db
        private void MainProcess(DataTable dataTable, int currentRow)
        {
            // Finish import file
            if (currentRow >= dataTable.Rows.Count)
            {
                Dialog.Alert("Success");
                gridTimeSheetCode.Reload();
                txtSheetName.Clear();
                txtFromRow.Clear();
                txtToRow.Clear();
                return;
            }

            count++;

            var listRecordId = hr_TimeSheetCodeServices.GetListRecordIds();
            string employeeCode = string.Empty;
            string fullname = string.Empty;

            var timeSheetCode = new hr_TimeSheetCode()
            {
                StartTime = DateTime.Now,
                IsActive = true,
                CreatedDate = DateTime.Now,
                CreatedBy = CurrentUser.User.DisplayName,
            };
            var recordId = 0;

            foreach (DataColumn col in dataTable.Columns)
            {
                var item = dataTable.Rows[currentRow][col];

                if (item != DBNull.Value)
                {
                    switch (col.Ordinal)
                    {
                        case 1:
                            employeeCode = item.ToString();
                            recordId = hr_RecordServices.GetRecordIdByEmployeeCode(employeeCode);
                            timeSheetCode.RecordId = recordId;
                            break;
                        case 2:
                            fullname = item.ToString();
                            break;
                        case 3:
                            timeSheetCode.Code = item.ToString();
                            break;
                        case 4:
                            timeSheetCode.MachineSerialNumber = item.ToString();
                            break;
                        case 5:
                            timeSheetCode.MachineName = item.ToString();
                            break;
                        case 6:
                            timeSheetCode.LocationName = item.ToString();
                            break;
                        case 7:
                            timeSheetCode.IPAddress = item.ToString();
                            break;
                    }
                }
            }

            // Check if RecordId exists in TimeSheetCode table
            if (!string.IsNullOrEmpty(timeSheetCode.Code))
            {
                if (!listRecordId.Any(tsc => tsc.RecordId == recordId))
                {
                    hr_TimeSheetCodeServices.Create(timeSheetCode);
                    ContinueProcess(count);
                }
                else
                {
                    // Find TimeSheetCode to edit
                    var editTimeSheetCode = hr_TimeSheetCodeServices.GetTimeSheetCodeByRecordId(recordId);

                    string timeSheetCodeJson = JSON.Serialize(timeSheetCode);

                    hdfTimeSheetCode.Text = timeSheetCodeJson;

                    // Open Messagebox
                    RM.RegisterClientScriptBlock("confirm",
                        "showResult(" + count + ", '" + (string.IsNullOrEmpty(employeeCode) ? "0" : employeeCode) +
                        "', '" + (string.IsNullOrEmpty(fullname) ? "0" : fullname) + "');");

                    // Update is accepted
                    //if (editTimeSheetCode != null && hdfIsAgree.Text == "1")
                    //{
                    //    timeSheetCode.Id = editTimeSheetCode.Id;
                    //    timeSheetCode.CreatedDate = editTimeSheetCode.CreatedDate;
                    //    timeSheetCode.CreatedBy = editTimeSheetCode.CreatedBy;
                    //    timeSheetCode.EditedDate = DateTime.Now;
                    //    timeSheetCode.EditedBy = CurrentUser.User.DisplayName;
                    //    hr_TimeSheetCodeServices.Update(timeSheetCode);
                    //}
                }
            }
        }

        [DirectMethod]
        public void ContinueProcess(int value)
        {
            // get data
            var dataTable = JSON.Deserialize<DataTable>(hdfDataTable.Text);

            // get current row
            count = value;

            MainProcess(dataTable, count);
        }

        [DirectMethod]
        public void UpdateDuplicate(string json)
        {
            var timeSheetCode = JSON.Deserialize<hr_TimeSheetCode>(json);

            var editTimeSheetCode = hr_TimeSheetCodeServices.GetTimeSheetCodeByRecordId(timeSheetCode.RecordId);
            if (editTimeSheetCode != null)
            {
                timeSheetCode.Id = editTimeSheetCode.Id;
                timeSheetCode.CreatedDate = editTimeSheetCode.CreatedDate;
                timeSheetCode.CreatedBy = editTimeSheetCode.CreatedBy;
                timeSheetCode.EditedDate = DateTime.Now;
                timeSheetCode.EditedBy = CurrentUser.User.DisplayName;

                hr_TimeSheetCodeServices.Update(timeSheetCode);
            }
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

        /// <summary>
        /// upload file from computer to server
        /// </summary>
        /// <param name="sender">ID of FileUploadField</param>
        /// <param name="relativePath">the relative path to place you want to save file</param>
        /// <returns>The path of file after upload to server</returns>
        private string UploadFile(object sender, string relativePath)
        {
            FileUploadField obj = (FileUploadField) sender;
            HttpPostedFile file = obj.PostedFile;
            DirectoryInfo dir = new DirectoryInfo(Server.MapPath(relativePath));
            // if directory not exist then create this
            if (dir.Exists == false)
                dir.Create();
            string rdstr = Util.GetInstance().GetRandomString(7);
            string path = Server.MapPath(relativePath) + "/" + rdstr + "_" + obj.FileName;
            if (File.Exists(path))
                return "";
            file.SaveAs(path);
            //return relativePath + "/" + rdstr + "_" + obj.FileName;
            return path;
        }

        protected void DownloadTemplate_Click(object sender, DirectEventArgs e)
        {
            try
            {
                var serverPath = Server.MapPath("../../../File/Template/importTimeSheetCodeTemplate.xlsx");
                DataTable dataTable = SQLHelper.ExecuteTable(SQLManagementAdapter.GetStore_ExcelTimeSheetCode(null));

                WorkBook workBook = new WorkBook();
                workBook.ImportDataTable(dataTable, true, 0, 0, dataTable.Rows.Count + 1, dataTable.Columns.Count + 1);
                workBook.writeXLSX(serverPath);

                Response.AddHeader("Content-Disposition", "attachment; filename=" + "importTimeSheetCodeTemplate.xlsx");
                if (serverPath != null) Response.WriteFile(serverPath);
                Response.End();
            }
            catch (Exception ex)
            {
                ExtNet.MessageBox.Alert("Có lỗi xảy ra", ex.Message).Show();
            }
        }

        protected void CloseForm(object sender, DirectEventArgs e)
        {
            wdTimeSheetCode.Hide();
            txtTimeSheetCode.Disabled = false;
            cbxSelectedEmployee.Disabled = false;
        }

        #endregion
    }
}