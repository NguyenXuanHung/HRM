using Ext.Net;
using SmartXLS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Web.Core;
using Web.Core.Framework;
using Web.Core.Framework.Adapter;
using Web.Core.Framework.Common;
using Web.Core.Object.Catalog;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.Catalog;
using Web.Core.Service.HumanRecord;

namespace Web.HRM.Modules.ProfileHuman
{
    public partial class DefaultHRM : BasePage
    {
        private const string RelativePath = "../../File/Template";
        private const string ImportHumanRecord = "ImportHumanRecord.xlsx";
        private const string ShortProfile = "ShortProfile.xlsx";
        private const string ConstGender = "\"Nam,Nữ\"";
        private const int FontSize = 11 * 20;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ExtNet.IsAjaxRequest)
            {
                // Set resource
                grp_HoSoNhanSu.ColumnModel.SetColumnHeader(1, "{0}".FormatWith(Resource.Get("Employee.Code")));
               
                hdfMenuID.Text = MenuId.ToString();
                hdfMaDonVi.Text = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
                hdfDepartments.Text = string.Join(",", CurrentUser.Departments.Select(d => d.Id));

                new Core.Framework.Common.BorderLayout()
                {
                    menuID = MenuId,
                    script = "#{hdfSelectedDepartment}.setValue('" + Core.Framework.Common.BorderLayout.nodeID + "');#{PagingToolbar1}.pageIndex = 0;#{DirectMethods}.SetValueQuery();"
                }.AddDepartmentList(brlayout, CurrentUser, true);
               
                if (btnDelete.Visible)
                {
                    rowSelection.Listeners.RowSelect.Handler += "btnDelete.enable();";
                }

                grp_HoSoNhanSu.StoreID = "store_HoSoNhanSu";
                grp_HoSoNhanSu.Reload();
               
                #region phân quyền context menu
                mnuAdd.Visible = btnAddNew.Visible;
                mnuEdit.Visible = btnEdit.Visible;
                mnuDelete.Visible = btnDelete.Visible;
              
                #endregion
            }
            
            CapNhatAnhHangLoat1.AfterClickXemCanBoChuaCoAnhButton += new EventHandler(AfterClickXemCanBoChuaCoAnhButton_AfterClickXemCanBoChuaCoAnhButton);
            CapNhatAnhHangLoat1.AfterClickHideWindow += new EventHandler(AfterClickHideWindow_AfterClickHideWindow);
            CapNhatAnhHangLoat1.AfterClickCapNhat += new EventHandler(AfterClickCapNhat_AfterClickCapNhat);
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitControl()
        {
            // departments
            cbx_phongban_Store.DataSource = CurrentUser.DepartmentsTree;
            cbx_phongban_Store.DataBind();
        }

        #region Event Method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void filterDepartmentStore_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            cbx_phongban_Store.DataSource = CurrentUser.DepartmentsTree;
            cbx_phongban_Store.DataBind();
        }

        /// <summary>
        /// Xem nhân viên chưa có ảnh
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void AfterClickXemCanBoChuaCoAnhButton_AfterClickXemCanBoChuaCoAnhButton(object sender, EventArgs e)
        {
            try
            {
                hdfMaDonVi.Text = "";
                filterSex.Reset();
                filterBirthDate.Reset();
                filterEducation.Reset();
                filterDiDong.Reset();
                filterEmail.Reset();
                txtSearch.Reset();
                bool isChuaCoAnh = (bool)sender;
                hdfIsChuaCoAnh.Text = isChuaCoAnh.ToString();
                SetValueQuery();
                PagingToolbar1.PageIndex = 0;
                grp_HoSoNhanSu.Reload();
            }
            catch (Exception ex)
            {
                Dialog.Alert("Thông báo", "Có lỗi xảy ra: " + ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AfterClickHideWindow_AfterClickHideWindow(object sender, EventArgs e)
        {
            try
            {
                RM.RegisterClientScriptBlock("tbenable", "tb.show();employeeDetail_Toolbar1sdsds.show();");
                var isChuaCoAnh = (bool)sender;
                if (hdfIsChuaCoAnh.Text.ToLower() != "true") return;
                hdfIsChuaCoAnh.Text = isChuaCoAnh.ToString();
                PagingToolbar1.PageIndex = 0;
                grp_HoSoNhanSu.Reload();
            }
            catch (Exception ex)
            {
                X.Msg.Alert("Thông báo", "Có lỗi xảy ra: " + ex.Message.ToString()).Show();
            }
        }

        /// <summary>
        /// Sau khi cập nhật ảnh hàng loạt thì reload lại grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AfterClickCapNhat_AfterClickCapNhat(object sender, EventArgs e)
        {
            PagingToolbar1.PageIndex = 0;
            grp_HoSoNhanSu.Reload();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void filterBirthDateStore_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            try
            {
                //get list year
                var table = SQLHelper.ExecuteTable(SQLBusinessAdapter.GetBirthYear());

                var obj = new List<object> { new { NAM_SINH = -1, TEN_NAMSINH = "Tất cả" } };
                foreach (DataRow item in table.Rows)
                {
                    obj.Add(new { NAM_SINH = item["BirthYear"], TEN_NAMSINH = item["BirthYear"] });
                }
                filterBirthDateStore.DataSource = obj;
                filterBirthDateStore.DataBind();
            }
            catch (Exception ex)
            {
                Dialog.Alert("Thông báo", ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void filterRecruitmentDateStore_Store_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            try
            {
                // get list recruitmentYear
                var table = SQLHelper.ExecuteTable(SQLBusinessAdapter.GetRecruitmentYear());
                var obj = new List<object> { new { MA = -1, TEN = "Tất cả" } };
                foreach (DataRow item in table.Rows)
                {
                    obj.Add(new { MA = item["RecruitmentYear"], TEN = item["RecruitmentYear"] });
                }
                filterRecruitmentDateStore.DataSource = obj;
                filterRecruitmentDateStore.DataBind();

            }
            catch (Exception ex)
            {
                Dialog.Alert("Thông báo", ex.Message);
            }
        }

        #endregion

        #region Direct Method

        /// <summary>
        /// 
        /// </summary>
        [DirectMethod]
        public void DeleteRecord()
        {
            if (!string.IsNullOrEmpty(hdfRecordId.Text))
            {
                //delete
                RecordController.Delete(Convert.ToInt32(hdfRecordId.Text));
            }

            //reload
            grp_HoSoNhanSu.Reload();
        }

        /// <summary>
        /// 
        /// </summary>
        [DirectMethod]
        public void SetValueQuery()
        {
            hdfRecordId.Text = "";
            RM.RegisterClientScriptBlock("clearSelections", "grp_HoSoNhanSu.getSelectionModel().clearSelections();");
            var query = string.Empty;
            query += filterBirthDate.SelectedItem.Value ?? ""; query += ";";
            query += filterSex.SelectedItem.Value ?? ""; query += ";";
            query += filterMaritalStatus.SelectedItem.Value ?? ""; query += ";";
            query += filterPhongBan.SelectedItem.Value ?? ""; query += ";";
            query += filterFolk.SelectedItem.Value ?? ""; query += ";";
            query += filterReligion.SelectedItem.Value ?? ""; query += ";";
            query += filterPersonalClass.SelectedItem.Value ?? ""; query += ";";
            query += filterFamilyClass.SelectedItem.Value ?? ""; query += ";";
            query += filterRecruimentDate.SelectedItem.Value ?? ""; query += ";";
            query += filterPosition.SelectedItem.Value ?? ""; query += ";";
            query += filterJobTitle.SelectedItem.Value ?? ""; query += ";";
            query += filterBasicEducation.SelectedItem.Value ?? ""; query += ";";
            query += filterEducation.SelectedItem.Value ?? ""; query += ";";
            query += filterPoliticLevel.SelectedItem.Value ?? ""; query += ";";
            query += filterManagementLevel.SelectedItem.Value ?? ""; query += ";";
            query += filterLanguageLevel.SelectedItem.Value ?? ""; query += ";";
            query += filterITLevel.SelectedItem.Value ?? ""; query += ";";
            query += filterCPVPosition.SelectedItem.Value ?? ""; query += ";";
            query += filterVYUPosition.SelectedItem.Value ?? ""; query += ";";
            query += filterArmyLevel.SelectedItem.Value ?? ""; query += ";";
            query += filterDiDong.Text + ";";
            query += filterEmail.Text + ";";
            query += filterEmailRieng.Text + ";";
            query += filterWorkStatus.SelectedItem.Value ?? ""; query += ";";
            query += hdfSelectedDepartment.Text + ";";

            hdfQuery.Text = query;
            PagingToolbar1.PageIndex = 0;
            grp_HoSoNhanSu.Reload();
        }

        /// <summary>
        /// 
        /// </summary>
        [DirectMethod]
        public void ResetForm()
        {
            fileExcel.Reset();
            txtSheetName.Reset();
            grpImportExcel.Reload();
        }
        #endregion

        #region Excel Handler

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void menuExportExcel_Event(object sender, DirectEventArgs e)
        {
            var searchKey = txtSearch.Text;
            var maDonVi = hdfSelectedDepartment.Text;
            var birthDate = filterBirthDate.SelectedItem.Value ?? "";
            var sex = filterSex.SelectedItem.Value ?? "";
            switch (sex)
            {
                case "F":
                    sex = "0";
                    break;
                case "M":
                    sex = "1";
                    break;
                default:
                    sex = string.Empty;
                    break;
            }
            var selectedDepartment = filterPhongBan.SelectedItem.Value ?? "";
            var folk = filterFolk.SelectedItem.Value ?? "";
            var religion = filterReligion.SelectedItem.Value ?? "";
            var personalClass = filterPersonalClass.SelectedItem.Value ?? "";
            var familyClass = filterFamilyClass.SelectedItem.Value ?? "";
            var recruitmentDate = filterRecruimentDate.SelectedItem.Value ?? "";
            var position = filterPosition.SelectedItem.Value ?? "";
            var jobTitle = filterJobTitle.SelectedItem.Value ?? "";
            var basicEducation = filterBasicEducation.SelectedItem.Value ?? "";
            var education = filterEducation.SelectedItem.Value ?? "";
            var politicLevel = filterPoliticLevel.SelectedItem.Value ?? "";
            var managementLevel = filterManagementLevel.SelectedItem.Value ?? "";
            var languageLevel = filterLanguageLevel.SelectedItem.Value ?? "";
            var itLevel = filterITLevel.SelectedItem.Value ?? "";
            var cpvPosition = filterCPVPosition.SelectedItem.Value ?? "";
            var vyuPosition = filterVYUPosition.SelectedItem.Value ?? "";
            var armyLevel = filterArmyLevel.SelectedItem.Value ?? "";
            var maritalStatus = filterMaritalStatus.SelectedItem.Value ?? "";
            var phone = filterDiDong.Text;
            var email = filterEmail.Text;

            var departmentIds = hdfDepartments.Text;

            var arrOrgCode = string.IsNullOrEmpty(departmentIds)
                ? new string[] { }
                : departmentIds.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            for (var i = 0; i < arrOrgCode.Length; i++)
            {
                arrOrgCode[i] = "'{0}'".FormatWith(arrOrgCode[i]);
            }

            var table = SQLHelper.ExecuteTable(SQLManagementAdapter.GetStore_ExcelLyLichTrichNgang(
                string.Join(",", arrOrgCode), selectedDepartment, maDonVi, searchKey, birthDate, sex, folk, religion, familyClass,
                personalClass, recruitmentDate, position, jobTitle, basicEducation, education, politicLevel, managementLevel, languageLevel, itLevel,
                cpvPosition, vyuPosition, armyLevel, maritalStatus, phone, email));

            var stt = 0;
            for (var i = 0; i < table.Rows.Count; i++)
            {
                stt++;
                table.Rows[i]["STT"] = stt.ToString();
            }

            // export short profile excel
            ExportToExcel(table, RelativePath, ShortProfile);
        }

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

                if (fileExcel.HasFile)
                {
                    var path = UploadFile(fileExcel, Constant.PathTemplate);

                    if (path == null) return;

                    // Read data from excel
                    workbook.readXLSX(Path.Combine(Server.MapPath("~/"), Constant.PathTemplate, path));

                    if (!txtSheetName.IsEmpty)
                    {
                        workbook.Sheet = workbook.findSheetByName(txtSheetName.Text);
                    }

                    // Export to datatable
                    var dataTable = workbook.ExportDataTable(1, //first row
                        0, //first col
                        workbook.LastRow, //last row
                        workbook.LastCol + 1, //last col
                        true, //first row as header
                        true //convert to DateTime object if it match date pattern
                    );

                    //check limit package
                    var limitPackage = SystemConfigController.GetLimitPackage();
                    if (dataTable.Rows.Count > limitPackage)
                    {
                        Dialog.Alert("File Excel của bạn đã vượt quá giới hạn gói phần mềm đã đăng ký. Vui lòng đăng ký thêm để sử dụng.");
                        return;
                    }

                    // count success create
                    var successRecords = new List<int>();
                    foreach (DataRow row in dataTable.Rows)
                    {
                        // Check employee code
                        var employeeCode = row["EmployeeCode"].ToString().Trim();
                        var idNumber = row["IDNumber"].ToString().Trim();
                        if (!string.IsNullOrEmpty(employeeCode))
                        {
                            var recordList = RecordController.CheckExistIDNumberAndEmployeeCode(null, null, employeeCode);
                            if (recordList != null && recordList.Count > 0)
                            {
                                continue;
                            }
                        }
                        // Check id number
                        if (!string.IsNullOrEmpty(idNumber))
                        {
                            var recordList = RecordController.CheckExistIDNumberAndEmployeeCode(null, idNumber, null);
                            if (recordList != null && recordList.Count > 0)
                            {
                                continue;
                            }
                        }
                        // Create new record
                        var record = new hr_Record
                        {
                            // Set default work status
                            WorkStatusId = CatalogWorkStatusController.GetByGroup(RecordStatus.Working).Id
                        };

                        foreach (DataColumn col in dataTable.Columns)
                        {
                            if (string.IsNullOrEmpty(row[col].ToString()))
                            {
                                continue;
                            }
                            var condition = " [Name] = N'{0}' ".FormatWith(row[col]);
                            cat_Location location;
                            switch (col.ColumnName)
                            {
                                case nameof(hr_Record.FullName):
                                    record.FullName = row[col].ToString();
                                    // lấy họ và đệm từ họ tên
                                    var position = record.FullName.LastIndexOf(' ');
                                    record.Name = position == -1 ? record.FullName : record.FullName.Substring(position + 1).Trim();
                                    break;
                                case nameof(hr_Record.Sex):
                                    record.Sex = row[col].ToString().Equals("Nam");
                                    break;
                                case nameof(hr_Record.BirthPlaceWardId):
                                    location = cat_LocationServices.GetByCondition(condition);
                                    record.BirthPlaceWardId = location.Id;
                                    break;
                                case nameof(hr_Record.BirthPlaceDistrictId):
                                    location = cat_LocationServices.GetByCondition(condition);
                                    record.BirthPlaceDistrictId = location.Id;
                                    break;
                                case nameof(hr_Record.BirthPlaceProvinceId):
                                    location = cat_LocationServices.GetByCondition(condition);
                                    record.BirthPlaceProvinceId = location.Id;
                                    break;
                                case nameof(hr_Record.HometownWardId):
                                    location = cat_LocationServices.GetByCondition(condition);
                                    record.HometownWardId = location.Id;
                                    break;
                                case nameof(hr_Record.HometownDistrictId):
                                    location = cat_LocationServices.GetByCondition(condition);
                                    record.HometownDistrictId = location.Id;
                                    break;
                                case nameof(hr_Record.HometownProvinceId):
                                    location = cat_LocationServices.GetByCondition(condition);
                                    record.HometownProvinceId = location.Id;
                                    break;
                                default:
                                    // TODO : need create util function
                                    var reg = @"\(([^)]*)\)";
                                    var propVal = row[col];
                                    if (Regex.IsMatch(propVal.ToString(), reg))
                                    {
                                        propVal = Regex.Match(propVal.ToString(), reg).Groups[1].Value;
                                    }
                                    //get the property information based on the type
                                    var propInfo = record.GetType().GetProperty(col.ColumnName);
                                    //find the property type
                                    if (propInfo == null) break;
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
                                            if (!DateTime.TryParseExact(row[col].ToString(), "dd/MM/yyyy",
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
                                    propInfo.SetValue(record, propVal, null);
                                    break;
                            }
                        }
                        // add record
                        hr_RecordServices.Create(record);
                        successRecords.Add(dataTable.Rows.IndexOf(row));
                    }
                    Dialog.Alert("Thêm thành công " + successRecords.Count + " bản ghi");
                    grpImportExcel.Reload();
                }
                else
                {
                    Dialog.Alert("Bạn chưa chọn tệp tin đính kèm. Vui lòng chọn.");
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowError("Đã có lỗi xảy ra: " + ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void fileAttachment_FileSelected(object sender, DirectEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình xử lý {0}".FormatWith(ex.Message));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DownloadTemplate_Click(object sender, DirectEventArgs e)
        {
            try
            {
                var serverPath = Server.MapPath(RelativePath + "/" + ImportHumanRecord);

                var dataTable = GetRecordDataTable();

                CreateExcelFile(serverPath, dataTable);
                
                Response.AddHeader("Content-Disposition", "attachment; filename=" + ImportHumanRecord);
                Response.WriteFile(serverPath);
                Response.End();
            }
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serverPath"></param>
        /// <param name="dataTable"></param>
        private void CreateExcelFile(string serverPath, DataTable dataTable)
        {
            var workbook = new WorkBook();
            workbook.ImportDataTable(dataTable, false, 0, 0, dataTable.Rows.Count + 1, dataTable.Columns.Count + 1);

            // set header style
            var range = workbook.getRangeStyle();
            range.VerticalAlignment = RangeStyle.VerticalAlignmentCenter;
            range.HorizontalAlignment = RangeStyle.HorizontalAlignmentCenter;
            range.FontBold = true;
            range.FontSize = FontSize;
            workbook.setRangeStyle(range, 0, 0, 0, workbook.LastCol);

            workbook.setSheetName(0, "Thêm mới thông tin hồ sơ");
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
                    case nameof(hr_Record.DepartmentId):
                        CreateDropDownExcel("cat_Department", workbook, col, 2, 50);
                        break;
                    case nameof(hr_Record.ManagementDepartmentId):
                        CreateDropDownExcel("cat_Department", workbook, col, 2, 50);
                        break;
                    case nameof(hr_Record.Sex):
                        var validation = workbook.CreateDataValidation();
                        validation.Type = DataValidation.eUser;
                        var validateList = ConstGender;
                        validation.Formula1 = validateList;
                        workbook.setSelection(2, col.Ordinal, 50, col.Ordinal);
                        workbook.DataValidation = validation;
                        break;
                    case nameof(hr_Record.MaritalStatusId):
                        CreateDropDownExcel("cat_MaritalStatus", workbook, col, 2, 50);
                        break;
                    case nameof(hr_Record.ReligionId):
                        CreateDropDownExcel("cat_Religion", workbook, col, 2, 50);
                        break;
                    case nameof(hr_Record.PersonalClassId):
                        CreateDropDownExcel("cat_PersonalClass", workbook, col, 2, 50);
                        break;
                    case nameof(hr_Record.FolkId):
                        CreateDropDownExcel("cat_Folk", workbook, col, 2, 50);
                        break;
                    case nameof(hr_Record.FamilyClassId):
                        CreateDropDownExcel("cat_FamilyClass", workbook, col, 2, 50);
                        break;
                    case nameof(hr_Record.PositionId):
                        CreateDropDownExcel("cat_Position", workbook, col, 2, 50);
                        break;
                    case nameof(hr_Record.JobTitleId):
                        CreateDropDownExcel("cat_JobTitle", workbook, col, 2, 50);
                        break;
                    case nameof(hr_Record.BasicEducationId):
                        CreateDropDownExcel("cat_BasicEducation", workbook, col, 2, 50);
                        break;
                    case nameof(hr_Record.EducationId):
                        CreateDropDownExcel("cat_Education", workbook, col, 2, 50);
                        break;
                    case nameof(hr_Record.PoliticLevelId):
                        CreateDropDownExcel("cat_PoliticLevel", workbook, col, 2, 50);
                        break;
                    case nameof(hr_Record.ManagementLevelId):
                        CreateDropDownExcel("cat_ManagementLevel", workbook, col, 2, 50);
                        break;
                    case nameof(hr_Record.LanguageLevelId):
                        CreateDropDownExcel("cat_LanguageLevel", workbook, col, 2, 50);
                        break;
                    case nameof(hr_Record.ITLevelId):
                        CreateDropDownExcel("cat_ITLevel", workbook, col, 2, 50);
                        break;
                    case nameof(hr_Record.CPVPositionId):
                        CreateDropDownExcel("cat_CPVPosition", workbook, col, 2, 50);
                        break;
                    case nameof(hr_Record.VYUPositionId):
                        CreateDropDownExcel("cat_VYUPosition", workbook, col, 2, 50);
                        break;
                    case nameof(hr_Record.ArmyLevelId):
                        CreateDropDownExcel("cat_ArmyLevel", workbook, col, 2, 50);
                        break;
                    case nameof(hr_Record.HealthStatusId):
                        CreateDropDownExcel("cat_HealthStatus", workbook, col, 2, 50);
                        break;
                    case nameof(hr_Record.FamilyPolicyId):
                        CreateDropDownExcel("cat_FamilyPolicy", workbook, col, 2, 50);
                        break;
                    case nameof(hr_Record.IDIssuePlaceId):
                        CreateDropDownExcel("cat_IDIssuePlace", workbook, col, 2, 50);
                        break;
                    case nameof(hr_Record.EmployeeTypeId):
                        CreateDropDownExcel("cat_EmployeeType", workbook, col, 2, 50);
                        break;
                    case nameof(hr_Record.IndustryId):
                        CreateDropDownExcel("cat_Industry", workbook, col, 2, 50);
                        break;
                    default:
                        break;
                }
            }
            workbook.writeXLSX(serverPath);
        }


        #endregion

        #region Private Method

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private DataTable GetRecordDataTable()
        {
            var dataTable = new DataTable();
            dataTable.Rows.Add();
            dataTable.Rows.Add();
            var colCount = 0;

            // create header by property name and description
            foreach (var prop in typeof(hr_Record).GetProperties())
            {
                // get prop description
                var attribute = prop.GetCustomAttribute(typeof(DescriptionAttribute));
                var description = string.Empty;
                if (attribute != null)
                {
                    description = ((DescriptionAttribute)attribute).Description;
                }
                if(string.IsNullOrEmpty(description)) continue;

                dataTable.Columns.Add();
                dataTable.Rows[0][colCount] = description;
                dataTable.Rows[1][colCount] = prop.Name;

                // set column datatable name
                dataTable.Columns[colCount].ColumnName = prop.Name;
                colCount++;
            }

            return dataTable;
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InitWindowEdit(object sender, DirectEventArgs e)
        {
            var param = e.ExtraParams["event"];
            if (!string.IsNullOrEmpty(param) && param == "Edit")
            {
                inputEmployee.btnEdit_Click(RecordType.Default);
            }
            else
            {
                //get employeeCode
               inputEmployee.InitWindowInput(RecordType.Default);
            }
            
        }

        protected void inputEmployee_OnUserControlClose()
        {
            grp_HoSoNhanSu.Reload();
        }
    }
}

