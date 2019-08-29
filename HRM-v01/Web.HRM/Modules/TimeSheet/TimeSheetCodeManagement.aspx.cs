using System;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Ext.Net;
using SmartXLS;
using SoftCore;
using Web.Core;
using Web.Core.Framework;
using Web.Core.Framework.Common;
using Web.Core.Framework.Controller.TimeSheet;
using Web.Core.Object.TimeSheet;

namespace Web.HRM.Modules.TimeSheet
{
    public partial class TimeSheetCodeManagement : BasePage
    {
        public int count = 0;
        private const string TimeSheetCodeFileName = "importTimeSheetCodeTemplate.xlsx";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ExtNet.IsAjaxRequest)
            {
                hdfDepartments.Text = DepartmentIds;

                new Core.Framework.Common.BorderLayout()
                {
                    menuID = MenuId,
                    script = "#{hdfDepartmentSelectedId}.setValue('" + Core.Framework.Common.BorderLayout.nodeID +
                             "');#{txtSearch}.reset();#{PagingToolbar1}.pageIndex = 0; #{PagingToolbar1}.doLoad();"
                }.AddDepartmentList(brlayout, CurrentUser, false);
            }

            if (btnEdit.Visible)
            {
                gridTimeSheetCode.Listeners.RowDblClick.Handler +=
                    " if(CheckSelectedRows(gridTimeSheetCode)){btnUpdate.show();btnUpdateClose.hide()}";
            }
        }

        #region Event Method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InsertOrUpdate(object sender, DirectEventArgs e)
        {
            if (e.ExtraParams["Command"] == "Update")
                Update();
            else
                Insert(e);
            //reload data
            gridTimeSheetCode.Reload();
            cbxSelectedEmployee.Disabled = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        private void Insert(DirectEventArgs e)
        {
            DateTime? startTime = null;
            DateTime? endTime = null;

            var modelTimeSheet = new TimeSheetCodeModel(null) { CreatedDate = DateTime.Now };

            //Edit data
            EditData(modelTimeSheet, ref startTime, ref endTime);

            var checkTime = TimeSheetCodeController.GetAll(null, null, null, txtTimeSheetCode.Text, null, true, startTime, endTime, null, null);
            if (checkTime != null && checkTime.Count > 0)
            {
                Dialog.Alert("Mã chấm công đã tồn tại. Vui lòng nhập mã chấm công khác!");
                return;
            }

            TimeSheetCodeController.Create(modelTimeSheet);

            if (e.ExtraParams["Close"] == "True")
            {
                wdTimeSheetCode.Hide();
                ResetForm();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void Update()
        {
            if (!string.IsNullOrEmpty(hdfKeyRecord.Text))
            {
                DateTime? startTime = null;
                DateTime? endTime = null;

                var modelTimeSheet = TimeSheetCodeController.GetById(Convert.ToInt32(hdfKeyRecord.Text));
                var currentTimeSheetCode = string.Empty;

                if (modelTimeSheet != null)
                {
                    modelTimeSheet.EditedDate = DateTime.Now;
                    currentTimeSheetCode = modelTimeSheet.Code;
                    //Edit data
                    EditData(modelTimeSheet, ref startTime, ref endTime);
                }

                var checkTime = TimeSheetCodeController.GetAll(null, null, null, txtTimeSheetCode.Text, null, true, startTime, endTime, null, null);

                if (checkTime.IsNullOrEmpty() || currentTimeSheetCode == txtTimeSheetCode.Text)
                {
                    TimeSheetCodeController.Update(modelTimeSheet);
                    Dialog.Alert("Cập nhật thành công");
                }
                else
                {
                    Dialog.Alert("Mã chấm công đã tồn tại. Vui lòng nhập mã chấm công khác!");
                    return;
                }
            }
        }

        /// <summary>
        /// Edit data
        /// </summary>
        /// <param name="modelTimeSheet"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        private void EditData(TimeSheetCodeModel modelTimeSheet, ref DateTime? startTime, ref DateTime? endTime)
        {
            var util = new Util();
            if (!string.IsNullOrEmpty(hdfEmployeeSelectedId.Text))
                modelTimeSheet.RecordId = Convert.ToInt32(hdfEmployeeSelectedId.Text);

            if (!string.IsNullOrEmpty(hdfTimeMachineId.Text))
                modelTimeSheet.MachineId = Convert.ToInt32(hdfTimeMachineId.Text);

            modelTimeSheet.Code = txtTimeSheetCode.Text;
            if (!util.IsDateNull(dfStartTime.SelectedDate))
            {
                modelTimeSheet.StartTime = dfStartTime.SelectedDate;
                startTime = dfStartTime.SelectedDate;
            }

            if (!util.IsDateNull(dfEndTime.SelectedDate))
            {
                modelTimeSheet.EndTime = dfEndTime.SelectedDate;
                endTime = dfEndTime.SelectedDate;
            }

            modelTimeSheet.IsActive = chk_IsActive.Checked;
        }

        /// <summary>
        /// 
        /// </summary>
        [DirectMethod]
        public void ResetForm()
        {
            txtTimeSheetCode.Text = "";
            dfStartTime.Reset();
            dfEndTime.Reset();
            cbxSelectedEmployee.Clear();
            hdfEmployeeSelectedId.Text = "";
            chk_IsActive.Checked = false;
            hdfTimeMachineId.Reset();
            cbxTimeMachine.Reset();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="directEventArgs"></param>
        protected void Delete(object sender, DirectEventArgs directEventArgs)
        {
            if (!int.TryParse(hdfKeyRecord.Text, out var id) || id <= 0) return;
            TimeSheetCodeController.Delete(id);
            gridTimeSheetCode.Reload();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AddTimeSheetCode_Click(object sender, DirectEventArgs e)
        {
            wdTimeSheetCode.Title = @"Đăng ký mới mã chấm công";
            wdTimeSheetCode.Show();
            btnUpdate.Hide();
            btnUpdateClose.Show();
            cbxSelectedEmployee.Disabled = false;
            ResetForm();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void EditTimeSheetCode_Click(object sender, DirectEventArgs e)
        {
            if (!int.TryParse(hdfKeyRecord.Text, out var id) || id <= 0) return;
            var modelTimeSheetCode = TimeSheetCodeController.GetById(id);
            if (modelTimeSheetCode != null)
            {
                txtTimeSheetCode.Text = modelTimeSheetCode.Code;
                dfStartTime.SetValue(modelTimeSheetCode.StartTime);
                dfEndTime.SetValue(modelTimeSheetCode.EndTime);
                hdfEmployeeSelectedId.Text = modelTimeSheetCode.RecordId.ToString();
                cbxSelectedEmployee.Text = modelTimeSheetCode.FullName;
                chk_IsActive.Checked = modelTimeSheetCode.IsActive;
                hdfTimeMachineId.Text = modelTimeSheetCode.MachineId.ToString();
                cbxTimeMachine.Text = modelTimeSheetCode.SerialNumber;
            }

            // show window
            btnUpdate.Show();
            btnUpdateClose.Hide();
            cbxSelectedEmployee.Disabled = true;

            wdTimeSheetCode.Title = @"Cập nhật mã chấm công";
            wdTimeSheetCode.Show();
        }

        #region Excel

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ImportFile(object sender, DirectEventArgs e)
        {
            try
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

                        // Check validation workbook
                        if (CheckValidation(workbook, out var fromRow, out var toRow) == false)
                        {
                            return;
                        }

                        // Export to datatable
                        var dataTable = workbook.ExportDataTable(fromRow - 1, //first row
                            0, //first col
                            toRow - 1, //last row
                            workbook.LastCol + 1, //last col
                            true, //first row as header
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

        /// <summary>
        /// Read data from excel file and save to db
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="currentRow"></param>
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

            var listRecordId = TimeSheetCodeController.GetAll(null, null, null, null, null, true, null, null, null, null);

            var timeSheetCode = new hr_TimeSheetCode()
            {
                StartTime = DateTime.Now,
                IsActive = true,
                CreatedDate = DateTime.Now,
                CreatedBy = CurrentUser.User.DisplayName,
            };

            foreach (DataColumn col in dataTable.Columns)
            {
                switch (col.ColumnName)
                {
                    case nameof(TimeSheetCodeModel.EmployeeCode):
                        var record = RecordController.GetByEmployeeCode(dataTable.Rows[currentRow][col].ToString());
                        if (record != null)
                        {
                            timeSheetCode.RecordId = record.Id;
                        }
                        break;
                    default:
                        var reg = @"\(([^)]*)\)";
                        var propVal = dataTable.Rows[currentRow][col];
                        if (Regex.IsMatch(propVal.ToString(), reg))
                        {
                            propVal = Regex.Match(propVal.ToString(), reg).Groups[1].Value;
                        }
                        //get the property information based on the type
                        var propInfo = timeSheetCode.GetType().GetProperty(col.ColumnName);
                        //find the property type
                        if (propInfo == null) continue;
                        if (!propInfo.CanWrite) continue;
                        var propType = propInfo.PropertyType;

                        //equivalent to the specified object.
                        if (propType.IsEnum)
                        {
                            propVal = Enum.ToObject(propType, int.Parse(propVal.ToString()));
                        }
                        if (propType.Name == typeof(Nullable<>).Name)
                        {
                            if (Nullable.GetUnderlyingType(propType) == typeof(DateTime))
                            {
                                if (!DateTime.TryParseExact(propVal.ToString(), "dd/MM/yyyy",
                                    CultureInfo.CurrentCulture, DateTimeStyles.None, out var date)) break;
                                propVal = Convert.ChangeType(date, typeof(DateTime));
                            }
                            else
                            {
                                propVal = Convert.ChangeType(propVal, Nullable.GetUnderlyingType(propType));
                            }
                        }
                        else
                        {
                            propVal = Convert.ChangeType(propVal, propType);
                        }
                        //Set the value of the property
                        propInfo.SetValue(timeSheetCode, propVal, null);
                        break;
                }

            }

            // Check if RecordId exists in TimeSheetCode table
            if (!string.IsNullOrEmpty(timeSheetCode.Code))
            {
                // create model
                var timeSheetCodeMode = new TimeSheetCodeModel(timeSheetCode);
                if (listRecordId.All(tsc => tsc.RecordId != timeSheetCode.RecordId))
                {
                    TimeSheetCodeController.Create(timeSheetCodeMode);
                    ContinueProcess(count);
                }
                else
                {
                    var timeSheetCodeJson = JSON.Serialize(timeSheetCode);

                    hdfTimeSheetCode.Text = timeSheetCodeJson;

                    // Open Messagebox
                    RM.RegisterClientScriptBlock("confirm",
                        "showResult(" + count + ", '" + (string.IsNullOrEmpty(timeSheetCodeMode.EmployeeCode) ? "0" : timeSheetCodeMode.EmployeeCode) +
                        "', '" + (string.IsNullOrEmpty(timeSheetCodeMode.FullName) ? "0" : timeSheetCodeMode.FullName) + "');");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        [DirectMethod]
        public void ContinueProcess(int value)
        {
            // get data
            var dataTable = JSON.Deserialize<DataTable>(hdfDataTable.Text);

            // get current row
            count = value;

            MainProcess(dataTable, count);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        [DirectMethod]
        public void UpdateDuplicate(string json)
        {
            var timeSheetCode = JSON.Deserialize<hr_TimeSheetCode>(json);

            var editTimeSheetCodeModel = TimeSheetCodeController.GetUnique(timeSheetCode.RecordId, null, true);
            if (editTimeSheetCodeModel == null) return;
            timeSheetCode.Id = editTimeSheetCodeModel.Id;
            //update
            TimeSheetCodeController.Update(new TimeSheetCodeModel(timeSheetCode));
        }

        /// <summary>
        /// validation
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="fromRow"></param>
        /// <param name="toRow"></param>
        /// <returns></returns>
        private bool CheckValidation(WorkBook workbook, out int fromRow, out int toRow)
        {
            fromRow = 2;
            toRow = 2;
            workbook.Sheet = !txtSheetName.IsEmpty ? workbook.findSheetByName(txtSheetName.Text) : 0;

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
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DownloadTemplate_Click(object sender, DirectEventArgs e)
        {
            // var dataTable = SQLHelper.ExecuteTable(SQLManagementAdapter.GetStore_ExcelTimeSheetCode(null));

            var serverPath = Server.MapPath("~/" + Constant.PathTemplate + "/" + TimeSheetCodeFileName);

            var dataTable = new DataTable();
            var colCount = 0;

            dataTable.Rows.Add();
            dataTable.Rows.Add();
            // create header by property name and description
            foreach (var property in typeof(TimeSheetCodeModel).GetProperties())
            {
                var description = string.Empty;
                // get prop description
                var attribute = property.GetCustomAttribute(typeof(DescriptionAttribute));
                if (attribute != null)
                {
                    description = ((DescriptionAttribute)attribute).Description;
                }
                // set description
                if (string.IsNullOrEmpty(description)) continue;

                // add column
                dataTable.Columns.Add();
                dataTable.Rows[0][colCount] = description;
                dataTable.Rows[1][colCount] = property.Name;

                // set column datatable name
                dataTable.Columns[colCount].ColumnName = property.Name;
                colCount++;
            }

            CreateExcelFile(serverPath, dataTable);

            Response.AddHeader("Content-Disposition", "attachment; filename=" + TimeSheetCodeFileName);
            Response.WriteFile(serverPath);
            Response.End();
        }

        /// <summary>
        /// 
        /// </summary>
        private void CreateExcelFile(string serverPath, DataTable dataTable)
        {
            var workbook = new WorkBook();
            workbook.ImportDataTable(dataTable, false, 0, 0, dataTable.Rows.Count + 1, dataTable.Columns.Count + 1);

            // set header style
            var range = workbook.getRangeStyle();
            range.VerticalAlignment = RangeStyle.VerticalAlignmentCenter;
            range.HorizontalAlignment = RangeStyle.HorizontalAlignmentCenter;
            range.FontBold = true;
            range.FontSize = 11 * 20;

            workbook.setRangeStyle(range, 0, 0, 0, workbook.LastCol);

            workbook.setSheetName(0, "Thêm mới thông tin");
            workbook.insertSheets(1, 1);
            workbook.setSheetName(1, "Info");
            workbook.SheetHidden = WorkBook.SheetStateHidden;
            workbook.Sheet = 0;

            // auto resize columns
            for (var i = 0; i < workbook.LastCol; i++)
            {
                workbook.setColWidthAutoSize(i, true);
            }
            // hide prop name row
            workbook.setRowHidden(1, true);

            foreach (DataColumn col in dataTable.Columns)
            {
                switch (col.ColumnName)
                {
                    case nameof(TimeSheetCodeModel.MachineId):
                        CreateDropDownSerialNumberExcel(workbook, col, 2, 50);
                        break;
                }
            }
            workbook.writeXLSX(serverPath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="col"></param>
        /// <param name="fromRow"></param>
        /// <param name="toRow"></param>
        private void CreateDropDownSerialNumberExcel(WorkBook workbook, DataColumn col, int fromRow, int toRow)
        {
            var list = TimeSheetMachineController.GetAll(null, null, null, null, null, null, null);
            if (list == null) return;
            var validation = workbook.CreateDataValidation();
            validation.Type = DataValidation.eUser;
            var validateList = "\"{0}\"".FormatWith(string.Join(",", list.Select(l => l.SerialNumber + " ({0})".FormatWith(l.Id)).ToList()));
            // formula string length cannot be greater than 256
            if (validateList.Length < 256)
            {
                // set formula by string
                validation.Formula1 = validateList;
            }
            else
            {
                var columnName = GetExcelColumnName(col.Ordinal + 1);
                // select info sheet
                workbook.Sheet = 1;
                // write list into info sheet
                foreach (var item in list.Select((value, index) => new { value, index }))
                {
                    workbook.setText(item.index + 1, col.Ordinal, item.value.Name + " ({0})".FormatWith(item.value.Id));
                }
                // select 
                workbook.Sheet = 0;
                // set formula by selected range
                validation.Formula1 = "Info!${0}$2:${0}${1}".FormatWith(columnName, list.Count);
            }
            // selection range
            workbook.setSelection(fromRow, col.Ordinal, toRow, col.Ordinal);
            workbook.DataValidation = validation;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CloseForm(object sender, DirectEventArgs e)
        {
            wdTimeSheetCode.Hide();
            txtTimeSheetCode.Disabled = false;
            cbxSelectedEmployee.Disabled = false;
        }
        #endregion



        #endregion
    }
}