using System;
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

namespace Web.HRM.Modules.Kpi
{
    public partial class EmployeeArgumentManagement : BasePage
    {
        private const string ArgumentManagementUrl = @"~/Modules/Kpi/EvaluationManagement.aspx?groupId={0}&month={1}&year={2}";
        private const string EmployeeCode = @"Mã nhân viên";
        private const string FullName = @"Họ tên";
        private const string ImportEmployeeArgumentExcelFile = "/ImportEmployeeArgument.xlsx";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ExtNet.IsAjaxRequest)
            {
                // get month
                if (!string.IsNullOrEmpty(Request.QueryString["month"]))
                {
                    hdfMonth.Text = Request.QueryString["month"];
                    cboMonth.Text = @"Tháng " + Request.QueryString["month"];
                    hdfMonthFilter.Text = Request.QueryString["month"];
                    cboMonthFilter.Text = @"Tháng " + Request.QueryString["month"];
                }
                else
                {
                    hdfMonth.Text = DateTime.Now.Month.ToString();
                    cboMonth.Text = @"Tháng " + DateTime.Now.Month;
                    hdfMonthFilter.Text = DateTime.Now.Month.ToString();
                    cboMonthFilter.Text = @"Tháng " + DateTime.Now.Month;
                }

                // get year
                if(!string.IsNullOrEmpty(Request.QueryString["year"]))
                {
                    hdfYear.Text = Request.QueryString["year"];
                    spnYear.SetValue(Request.QueryString["year"]);
                    hdfYearFilter.Text = Request.QueryString["year"];
                    spnYearFilter.SetValue(Request.QueryString["year"]);
                }
                else
                {
                    hdfYear.Text = DateTime.Now.Year.ToString();
                    spnYear.SetValue(DateTime.Now.Year);
                    hdfYearFilter.Text = DateTime.Now.Year.ToString();
                    spnYearFilter.SetValue(DateTime.Now.Year);
                }

                hdfDepartmentIds.Text = DepartmentIds;

                hdfStatus.Text = ((int)KpiStatus.Active).ToString();
                storeDepartment.DataSource = CurrentUser.DepartmentsTree;
                storeDepartment.DataBind();
                
                // get group
                var groupId = Request.QueryString["groupId"];
                if(!string.IsNullOrEmpty(groupId))
                {
                    var group = GroupKpiController.GetById(int.Parse(groupId));
                    if (group != null)
                    {
                        cboGroupFilter.Text = group.Name;
                        hdfGroupFilter.Text = group.Id.ToString();
                    }
                }
                else
                {
                    var groups = GroupKpiController.GetAll(null, false, KpiStatus.Active, null, null);
                    if(groups.Count > 0)
                    {
                        cboGroupFilter.Text = groups.First().Name;
                        hdfGroupFilter.Text = groups.First().Id.ToString();
                    }
                }

                new Core.Framework.Common.BorderLayout()
                {
                    menuID = MenuId,
                    script = "#{hdfDepartmentSelectedId}.setValue('" + Core.Framework.Common.BorderLayout.nodeID +
                             "');#{txtKeyword}.reset();#{pagingToolbar}.pageIndex = 0; #{pagingToolbar}.doLoad();"
                }.AddDepartmentList(brlayout, CurrentUser, false);

                //generate column
                AddColumnToGridPanel();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddColumnToGridPanel()
        {
            var arguments = ArgumentController.GetAll(null, false, KpiStatus.Active, null, null, null);
            var count = 0;
            foreach (var item in arguments)
            {
                var recordField = new RecordField()
                {
                    Name = item.Code,
                    Mapping = "ArgumentDetailModels[{0}].Value".FormatWith(count++)
                };
                
                var col = new Column
                {
                    ColumnID = recordField.Name,
                    Header = item.Name,
                    DataIndex = recordField.Name,
                    Width = 150,
                    Renderer = {Fn = "RenderArgument"}
                };
                switch (item.ValueType)
                {
                    case KpiValueType.Percent:
                        col.Renderer.Fn = "RenderPercent";
                        col.Align = Alignment.Left;
                        break;
                    case KpiValueType.String:
                        col.Align = Alignment.Center;
                        break;
                    case KpiValueType.Number:
                        break;
                    case KpiValueType.Formula:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                storeEmployeeArgument.AddField(recordField);
                gpEmployeeArgument.ColumnModel.Columns.Add(col);
            }
        }

        /// <summary>
        /// init setting window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InitWindow(object sender, DirectEventArgs e)
        {
            try
            {
                //reset form
                ResetForm();

                // init window props
                if (e.ExtraParams["Command"] == "Update")
                {
                    // edit
                    wdSetting.Title = @"Cập nhật tham số KPI cho nhân viên";
                    wdSetting.Icon = Icon.Pencil;
                    if (!string.IsNullOrEmpty(e.ExtraParams["Id"]))
                    {
                        var model = EmployeeArgumentController.GetById(Convert.ToInt32(e.ExtraParams["Id"]));
                        if (model != null)
                        {
                            // set props
                            hdfChooseEmployee.Text = model.RecordId.ToString();
                            cboEmployee.Text = model.FullName;
                            txtValue.Text = model.Value;
                            hdfArgumentId.Text = model.ArgumentId.ToString();
                            cboArgument.Text = model.ArgumentName;
                            txtValueType.Text = model.ValueTypeName;
                            cboMonth.Text = @"Tháng " + model.Month;
                            hdfYear.Text = model.Year.ToString();
                            spnYear.SetValue(model.Year);
                            hdfGroupInput.Text = model.GroupId.ToString();
                            cboGroupInput.Text = model.GroupName;
                        }
                    }
                }
                else
                {
                    // insert
                    wdSetting.Title = @"Thêm mới tham số KPI cho nhân viên";
                    wdSetting.Icon = Icon.Add;
                    cboArgument.Disabled = false;
                    cboEmployee.Disabled = false;
                    cboMonth.Disabled = false;
                    spnYear.Disabled = false;
                }
                
                // show window
                wdSetting.Show();
            }
            catch (Exception exception)
            {
                Dialog.ShowError(exception);
            }
        }

        /// <summary>
        /// insert or update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InsertOrUpdate(object sender, DirectEventArgs e)
        {
            try
            {
                // init entity
                var model = new EmployeeArgumentModel();

                // check id
                if (!string.IsNullOrEmpty(hdfId.Text))
                {
                    if (Convert.ToInt32(hdfId.Text) > 0)
                    {
                        var result = EmployeeArgumentController.GetById(Convert.ToInt32(hdfId.Text));
                        ;
                        if (result != null)
                            model = result;
                    }
                }

                // set new props for entity
                if (!string.IsNullOrEmpty(hdfArgumentId.Text))
                {
                    model.ArgumentId = Convert.ToInt32(hdfArgumentId.Text);
                }

                if (!string.IsNullOrEmpty(hdfGroupInput.Text))
                {
                    model.GroupId = Convert.ToInt32(hdfGroupInput.Text);
                }
              
                if (!string.IsNullOrEmpty(hdfChooseEmployee.Text))     
                    model.RecordId = Convert.ToInt32(hdfChooseEmployee.Text);
                model.Value = txtValue.Text;
                if (!string.IsNullOrEmpty(hdfMonth.Text))
                {
                    model.Month = Convert.ToInt32(hdfMonth.Text);
                }
                if (!string.IsNullOrEmpty(hdfYear.Text))
                {
                    model.Year = Convert.ToInt32(hdfYear.Text);
                }
                // check entity id
                if (model.Id > 0)
                {
                    model.EditedDate = DateTime.Now;
                    model.EditedBy = CurrentUser.User.UserName;
                    // update
                    EmployeeArgumentController.Update(model);
                }
                else
                {
                    model.CreatedBy = CurrentUser.User.UserName;
                    model.CreatedDate = DateTime.Now;
                    model.EditedDate = DateTime.Now;
                    model.EditedBy = "";
                    //check argument exist
                    var employeeArgument =
                        EmployeeArgumentController.GetUnique(model.GroupId, model.RecordId, model.ArgumentId, model.Month, model.Year);
                    if (employeeArgument != null)
                        Dialog.ShowNotification("Giá trị tham số này đã được tạo. Vui lòng chọn tham số khác.");
                    else
                        // insert
                        EmployeeArgumentController.Create(model);
                }
                
                // hide window
                wdSetting.Hide();

                //reset form
                ResetForm();
                // reload data
                gpEmployeeArgument.Reload();
            }
            catch (Exception exception)
            {
                Dialog.ShowError(exception);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void ResetForm()
        {
            hdfArgumentId.Reset();
            cboArgument.Reset();
            hdfChooseEmployee.Reset();
            txtValueType.Reset();
            cboEmployee.Reset();
            txtValue.Reset();
            hdfGroupInput.Reset();
            cboGroupInput.Reset();
        }

        /// <summary>
        /// Delete 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Delete(object sender, DirectEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(hdfRecordId.Text) && !string.IsNullOrEmpty(hdfMonth.Text) && !string.IsNullOrEmpty(hdfYear.Text))
                {
                    //delete
                    EmployeeArgumentController.DeleteByCondition(null, Convert.ToInt32(hdfRecordId.Text), null, Convert.ToInt32(hdfMonth.Text), Convert.ToInt32(hdfYear.Text));
                }
                
                // reload data
                gpEmployeeArgument.Reload();
            }
            catch (Exception exception)
            {
                Dialog.ShowError(exception);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnSelectArgument(object sender, DirectEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(hdfArgumentId.Text))
                {
                    var argument = ArgumentController.GetById(Convert.ToInt32(hdfArgumentId.Text));
                    if (argument != null)
                    {
                        txtValueType.Text = argument.ValueTypeName;
                    }
                }
                else
                {
                    txtValueType.Text = "";
                }
            }
            catch (Exception exception)
            {
                Dialog.ShowError(exception);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InitDeleteMultiple(object sender, DirectEventArgs e)
        {
            try
            {
                var recordIds = hdfRecordIds.Text.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();
                if (recordIds.Count > 0 && !string.IsNullOrEmpty(hdfMonth.Text) && !string.IsNullOrEmpty(hdfYear.Text))
                {
                    //delete
                    EmployeeArgumentController.DeleteByCondition(recordIds, null, null, Convert.ToInt32(hdfMonth.Text), Convert.ToInt32(hdfYear.Text));
                }
                gpEmployeeArgument.Reload();
            }
            catch (Exception exception)
            {
                Dialog.ShowError(exception);
            }
        }

        [DirectMethod]
        public void InitWindowArgument(string argumentCode, string recordId)
        {
            try
            {
                wdSetting.Title = @"Cập nhật tham số KPI cho nhân viên";
                wdSetting.Icon = Icon.Pencil;

                if (!string.IsNullOrEmpty(argumentCode) && !string.IsNullOrEmpty(recordId))
                {
                    var argument = ArgumentController.GetUnique(argumentCode, null);
                    if (argument != null)
                    {
                        var model = EmployeeArgumentController.GetUnique(Convert.ToInt32(hdfGroupFilter.Text), Convert.ToInt32(recordId), argument.Id, Convert.ToInt32(hdfMonth.Text), Convert.ToInt32(hdfYear.Text));
                        if (model != null)
                        {
                            // set props
                            hdfChooseEmployee.Text = model.RecordId.ToString();
                            cboEmployee.Text = model.FullName;
                            txtValue.Text = model.Value;
                            hdfArgumentId.Text = model.ArgumentId.ToString();
                            cboArgument.Text = model.ArgumentName;
                            txtValueType.Text = model.ValueTypeName;
                            cboMonth.Text = @"Tháng " + model.Month;
                            hdfYear.Text = model.Year.ToString();
                            spnYear.SetValue(model.Year);
                            hdfId.Text = model.Id.ToString();
                            hdfGroupInput.Text = model.GroupId.ToString();
                            cboGroupInput.Text = model.GroupName;
                        }
                    }

                    cboArgument.Disabled = true;
                    cboEmployee.Disabled = true;
                    cboMonth.Disabled = true;
                    spnYear.Disabled = true;
                    //show window
                    wdSetting.Show();
                }
            }
            catch (Exception exception)
            {
                Dialog.ShowError(exception);
            }
        }

        #region Import Excel
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DownloadTemplate_Click(object sender, DirectEventArgs e)
        {
            // init table
            var dataTable = new DataTable();
           
            // adjust table
            dataTable.Rows.Add();
            dataTable.Columns.Add(new DataColumn(EmployeeCode));
            dataTable.Columns.Add(new DataColumn(FullName));

            //add argument
            var argumentModels = ArgumentController.GetAll(null, false, KpiStatus.Active, null, null, null);
            foreach (var argument in argumentModels)
            {
                dataTable.Columns.Add(new DataColumn(argument.Name));
            }
          
            // get record by department
            var departmentIds = DepartmentIds;
            if (!string.IsNullOrEmpty(hdfDepartmentId.Text))
            {
                departmentIds = hdfDepartmentId.Text;
            }
            var records = RecordController.GetAll(null, null, departmentIds, RecordType.Default, null, null);

            // fill employee name and code
            for (var i = 0; i < records.Count; i++)
            {
                dataTable.Rows.Add();
                dataTable.Rows[i][EmployeeCode] = records[i].EmployeeCode;
                dataTable.Rows[i][FullName] = records[i].FullName;
            }

            ExportToExcel(dataTable, "~/" + Constant.PathTemplate, ImportEmployeeArgumentExcelFile);
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

                    // Check validation workbook
                    if (CheckValidation(workbook, out _, out _, txtFromRow, txtToRow, txtSheetName) == false)
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

                    var argumentModels = ArgumentController.GetAll(null, false, KpiStatus.Active, null, null, null);
                    foreach (DataRow row in dataTable.Rows)
                    {
                        //get employee code
                        var employeeCode = row[EmployeeCode].ToString();
                        if(string.IsNullOrEmpty(employeeCode)) continue;

                        // get record by employee code
                        var record = RecordController.GetByEmployeeCode(employeeCode);

                        foreach (DataColumn col in dataTable.Columns)
                        {
                            foreach (var argument in argumentModels)
                            {
                                //check column name exists
                                if (argument.Name != col.ColumnName) continue;
                                
                                 //check empty string
                                var value = Convert.ToString(row[col], CultureInfo.InvariantCulture);
                                if(string.IsNullOrEmpty(value)) continue;

                                //check if value exists
                                var employeeArgument = EmployeeArgumentController.GetUnique(Convert.ToInt32(hdfGroup.Text), record.Id, argument.Id,
                                    Convert.ToInt32(hdfMonth.Text), Convert.ToInt32(hdfYear.Text));
                                if (employeeArgument != null)
                                {
                                    //update value
                                    employeeArgument.Value = value;
                                    employeeArgument.GroupId = !string.IsNullOrEmpty(hdfGroup.Text)
                                        ? Convert.ToInt32(hdfGroup.Text)
                                        : 0;
                                    employeeArgument.EditedBy = CurrentUser.User.UserName;
                                    employeeArgument.EditedDate = DateTime.Now;
                                    EmployeeArgumentController.Update(employeeArgument);
                                }
                                else
                                {
                                    var model = new EmployeeArgumentModel()
                                    {
                                        RecordId = record.Id,
                                        ArgumentId = argument.Id,
                                        GroupId = !string.IsNullOrEmpty(hdfGroup.Text)
                                            ? Convert.ToInt32(hdfGroup.Text)
                                            : 0,
                                        Month = Convert.ToInt32(hdfMonth.Text),
                                        Year = Convert.ToInt32(hdfYear.Text),
                                        Value = value
                                    };

                                    EmployeeArgumentController.Create(model);
                                }

                            }
                        }
                    }
                    //reset
                    hdfGroup.Reset();
                    cboGroup.Reset();
                    hdfDepartmentId.Reset();
                    cboDepartment.Reset();
                }
            }
            else
            {
                Dialog.Alert("Bạn chưa chọn tệp tin đính kèm. Vui lòng chọn.");
                return;
            }
            
            Dialog.Alert("Cập nhật thành công");
            //Reset form excel
            fileExcel.Reset();
            txtSheetName.Reset();
            //reload grid
            gpEmployeeArgument.Reload();
        }
        

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBack_Click(object sender, DirectEventArgs e)
        {
            Response.Redirect(ArgumentManagementUrl.FormatWith(hdfGroupFilter.Text, hdfMonthFilter.Text, hdfYearFilter.Text) + "&mId=" + MenuId, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ReloadGridColumn(object sender, DirectEventArgs e)
        {
            AddColumnToGridPanel();
        }
    }
}