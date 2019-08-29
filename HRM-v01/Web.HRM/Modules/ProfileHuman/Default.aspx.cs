using DataController;
using Ext.Net;
using SmartXLS;
using SoftCore;
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
    public partial class Default : BasePage
    {
        private const string RelativePath = "../../File/Template";
        private const string ImportHumanRecord = "ImportHumanRecord.xlsx";
        private const string ShortProfile = "ShortProfile.xlsx";
        private const string ConstGender = "\"Nam,Nữ\"";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ExtNet.IsAjaxRequest)
            {
                // Set resource
                grp_HoSoNhanSu.ColumnModel.SetColumnHeader(1, "{0}".FormatWith(Resource.Get("Employee.Code")));
                wdInput.Title = Resource.Get("Employee.WindowUpdateTitle");

                // init control
                //InitControl();

                hdfMenuID.Text = MenuId.ToString();
                hdfMaDonVi.Text = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
                hdfDepartments.Text = string.Join(",", CurrentUser.Departments.Select(d => d.Id));

                //txtReportCreator.Text = CurrentUser.User.DisplayName;
                // transfer user id
                //store_HoSoNhanSu.BaseParams.Add(new Parameter("uID", CurrentUser.User.Id.ToString()));
                //store_HoSoNhanSu.BaseParams.Add(new Parameter("mID", MenuID.ToString()));

                new Core.Framework.Common.BorderLayout()
                {
                    menuID = MenuId,
                    script = "#{hdfSelectedDepartment}.setValue('" + Core.Framework.Common.BorderLayout.nodeID + "');#{PagingToolbar1}.pageIndex = 0;#{DirectMethods}.SetValueQuery();"
                }.AddDepartmentList(brlayout, CurrentUser, true);
                //if (CurrentUser.User.Image == "0" || CurrentUser.User.Image == null || CurrentUser.User.Image == "")
                //{
                //    grp_HoSoNhanSu.StoreID = "store_HoSoNhanSu";
                //    grp_HoSoNhanSu.Reload();
                //}
                //else
                //{
                //    hdfRecordId.Text = CurrentUser.User.Image;
                //}            
                //SetVisibleByControlID(btnAddNew, btnEdit, btnDelete,btnEditPersonInfo,
                //                      btnBaoCao, btnTienIchHoSo,
                //    //mnuNhanDoiDuLieu, mnuNhapTuExcel,
                //                      mnuCapNhatAnhHangLoat);
                if (btnDelete.Visible)
                {
                    rowSelection.Listeners.RowSelect.Handler += "btnDelete.enable();";
                }

                grp_HoSoNhanSu.StoreID = "store_HoSoNhanSu";
                grp_HoSoNhanSu.Reload();
                //}
                //if (mnuNhanDoiDuLieu.Visible)
                //{
                //    rowSelection.Listeners.RowSelect.Handler += "mnuNhanDoiDuLieu.enable();";
                //}
                #region phân quyền context menu
                mnuAdd.Visible = btnAddNew.Visible;
                mnuEdit.Visible = btnEdit.Visible;
                mnuDelete.Visible = btnDelete.Visible;
                //mnuNhanDoi.Visible = mnuNhanDoiDuLieu.Visible;
                //mnuNhapExcel.Visible = mnuNhapTuExcel.Visible; 
                #endregion
            }
            if (btnEdit.Visible)
            {
                rowSelection.Listeners.RowSelect.Handler += "btnEdit.enable();";
                grp_HoSoNhanSu.Listeners.RowDblClick.Handler = "#{hdfEven}.setValue('Edit');wdInput.show();";
                //     grp_HoSoNhanSu.DirectEvents.RowDblClick.Event += new ComponentDirectEvent.DirectEventHandler(btnEdit_Click);
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
                //filterThamNien.Reset();
                filterEducation.Reset();
                //filterLoaiHopDong.Reset();
                //filterDiaChiLH.Reset();
                filterDiDong.Reset();
                filterEmail.Reset();
                //filterDaNghi.Reset();
                txtSearch.Reset();
                bool isChuaCoAnh = (bool)sender;
                hdfIsChuaCoAnh.Text = isChuaCoAnh.ToString();
                SetValueQuery();
                PagingToolbar1.PageIndex = 0;
                grp_HoSoNhanSu.Reload();
            }
            catch (Exception ex)
            {
                X.Msg.Alert("Thông báo", "Có lỗi xảy ra: " + ex.Message.ToString()).Show();
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
                // TODO : need move sqladapter or using controller
                DataTable table = DataHandler.GetInstance().ExecuteDataTable("select distinct YEAR(BirthDate) AS NAM_SINH from hr_Record WHERE BirthDate IS NOT NULL ORDER BY NAM_SINH ASC");
                var obj = new List<object> { new { NAM_SINH = -1, TEN_NAMSINH = "Tất cả" } };
                foreach (DataRow item in table.Rows)
                {
                    obj.Add(new { NAM_SINH = item["NAM_SINH"], TEN_NAMSINH = item["NAM_SINH"] });
                }
                filterBirthDateStore.DataSource = obj;
                filterBirthDateStore.DataBind();
            }
            catch (Exception ex)
            {
                X.Msg.Alert("Thông báo", ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void filterRecruimentDateStore_Store_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            try
            {
                // TODO : need move sqladapter or using controller
                var table = DataHandler.GetInstance().ExecuteDataTable("SELECT DISTINCT YEAR(RecruimentDate) AS NGAYTUYENDUNG FROM hr_Record WHERE RecruimentDate IS NOT NULL ORDER BY NGAYTUYENDUNG ASC");
                var obj = new List<object> { new { MA = -1, TEN = "Tất cả" } };
                foreach (DataRow item in table.Rows)
                {
                    obj.Add(new { MA = item["NGAYTUYENDUNG"], TEN = item["NGAYTUYENDUNG"] });
                }
                filterRecruimentDateStore.DataSource = obj;
                filterRecruimentDateStore.DataBind();

            }
            catch (Exception ex)
            {
                X.Msg.Alert("Thông báo", ex.Message);
            }
        }

        /// <summary>
        /// TODO : using file upload function in base 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAccept_Click(object sender, DirectEventArgs e)
        {
            try
            {
                var filePath = string.Empty;
                //upload file
                var file = fufUploadControl.PostedFile;
                if (fufUploadControl.HasFile == false && file.ContentLength > 2000000)
                {
                    Dialog.ShowNotification("File không được lớn hơn 200kb");
                    return;
                }
                else
                {
                    try
                    {
                        var dir = new DirectoryInfo(Server.MapPath(hdfImageFolder.Text));
                        if (dir.Exists == false)
                            dir.Create();
                        var path = Server.MapPath(hdfImageFolder.Text + "/") + fufUploadControl.FileName;
                        filePath = hdfImageFolder.Text + "/" + Util.GetInstance().GetRandomString(10) + fufUploadControl.FileName;
                        file.SaveAs(path);
                        File.Move(path, Server.MapPath(filePath));
                        //update ảnh vào csdl 
                        if (!string.IsNullOrEmpty(hdfRecordId.Text))
                        {
                            var hs = RecordController.GetById(Convert.ToInt32(hdfRecordId.Text));
                            hs.ImageUrl = filePath;
                            RecordController.Update(hs);
                        }
                        hdfAnhDaiDien.Text = filePath;
                    }
                    catch (Exception ex)
                    {
                        Dialog.ShowError(ex.Message);
                    }
                }
                wdUploadImageWindow.Hide();

                //Hiển thị lại ảnh sau khi đã cập nhật xong
                //  img_anhdaidien.ImageUrl = path1;//hdfImageFolder.Text + "/" + fufUploadControl.FileName;
                employeeDetail.SetProfileImage(filePath);
                grp_HoSoNhanSu.Reload();
            }
            catch (Exception ex)
            {
                Dialog.ShowNotification(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, DirectEventArgs e)
        {
            var selectedRows = rowSelection.SelectedRows;
            int count = 0, success = 0;
            foreach (var item in selectedRows)
            {
                try
                {
                    if (!string.IsNullOrEmpty(item.RecordID))
                    {
                        RecordController.Delete(Convert.ToInt32(item.RecordID));
                        success++;
                    }
                }
                catch
                {
                    count++;
                }
            }
            hdfRecordId.Text = "";
            if (count > 0)
            {
                ExtNet.Msg.Alert("Thông báo từ hệ thống", "Có " + count + " cán bộ không được xóa do đang được sử dụng").Show();
            }
            if (success > 0)
                grp_HoSoNhanSu.Reload();
        }

        #endregion

        #region Direct Method

        /// <summary>
        /// 
        /// </summary>
        [DirectMethod]
        public void DeleteRecord()
        {
            var selectedRows = rowSelection.SelectedRows;
            var count = 0;
            foreach (var item in selectedRows)
            {
                var prkey = int.Parse("0" + item.RecordID);
                try
                {
                    hr_RecordServices.Delete(prkey);
                }
                catch
                {
                    count++;
                }
            }
            hdfRecordId.Text = "";
            if (count > 0)
            {
                ExtNet.Msg.Alert("Thông báo từ hệ thống", "Có " + count + " cán bộ không được xóa do đang được sử dụng").Show();
            }
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
        public void GetEmployeeDetailInformation()
        {
            if (!string.IsNullOrEmpty(hdfRecordId.Text))
            {
                var record = RecordController.GetById(Convert.ToInt32(hdfRecordId.Text));
                var imageUrl = Constant.PathProfileImageDefault;
                if (!string.IsNullOrEmpty(record.ImageUrl))
                    //imageUrl = record.ImageUrl.Replace("~/Modules", "..");
                    imageUrl = Constant.PathLocationImageEmployee + @"/" +  record.ImageUrl;
                var cpvOfficialJoinedDate = string.Empty;
                var cpvJoinedDate = string.Empty;
                var effectiveDate = string.Empty;
                var idIssueDate = string.Empty;
                if (record.CPVOfficialJoinedDate != null)
                    cpvOfficialJoinedDate = "employeeDetail_txtCPVOfficialJoinedDate.setValue(RenderDate('" + record.CPVOfficialJoinedDate + "', null, null)); ";
                if (record.CPVJoinedDate != null)
                    cpvJoinedDate = "employeeDetail_txtCPVJoinedDate.setValue(RenderDate('" + record.CPVJoinedDate + " ', null, null)); ";
                if (record.EffectiveDate != null)
                    effectiveDate = "employeeDetail_txtEffectiveDate.setValue(RenderDate('" + record.EffectiveDate + "', null, null)); ";
                if (record.IDIssueDate != null)
                    idIssueDate = "employeeDetail_txtIDIssueDate.setValue(RenderDate('" + record.IDIssueDate + "', null, null)); ";

                RM.RegisterClientScriptBlock("setValueEmployeeDetail", "employeeDetail_hsImage.setImageUrl('" + imageUrl + "');" +
                    "employeeDetail_txtEmployeeCode.setValue('" + record.EmployeeCode + "');" +
                    "employeeDetail_txtFullName.setValue('" + record.FullName + "');" +
                    "employeeDetail_txtAlias.setValue('" + record.Alias + "');" +
                    "employeeDetail_txtBirthDate.setValue('" + record.BirthDateVn + "');" +
                    "employeeDetail_txtBirthPlace.setValue('" + record.BirthPlace + "');" +
                    "employeeDetail_txtHometown.setValue('" + record.Hometown + "');" +
                    "employeeDetail_txtPersonalClassName.setValue('" + record.PersonalClassName + "');" +
                    "employeeDetail_txtFamilyClassName.setValue('" + record.FamilyClassName + "');" +
                    "employeeDetail_txtFolkName.setValue('" + record.FolkName + "');" +
                    "employeeDetail_txtReligionName.setValue('" + record.ReligionName + "');" +
                    "employeeDetail_txtResidentPlace.setValue('" + record.ResidentPlace + "');" +
                    "employeeDetail_txtAddress.setValue('" + record.Address + "');" +
                    "employeeDetail_txtBasicEducationName.setValue('" + record.BasicEducationName + "');" +
                    "employeeDetail_txtEducationName.setValue('" + record.EducationName + "');" +
                    "employeeDetail_txtPoliticLevelName.setValue('" + record.PoliticLevelName + "');" +
                    "employeeDetail_txtManagementLevelName.setValue('" + record.ManagementLevelName + "');" +
                    "employeeDetail_txtLanguageLevelName.setValue('" + record.LanguageLevelName + "');" +
                    "employeeDetail_txtITLevelName.setValue('" + record.ITLevelName + "');" +
                    "employeeDetail_txtPreviousJob.setValue('" + record.PreviousJob + "');" +
                    "employeeDetail_txtRecruimentDate.setValue('" + record.RecruimentDate + "');" +
                    "employeeDetail_txtRecruimentDepartment.setValue('" + record.RecruimentDepartment + "'); " +
                    "employeeDetail_txtPositionName.setValue('" + record.PositionName + "'); " +
                    cpvJoinedDate +
                    cpvOfficialJoinedDate +
                    "employeeDetail_txtCPVCardNumber.setValue('" + record.CPVCardNumber + "'); " +
                    "employeeDetail_txtCPVPositionName.setValue('" + record.CPVPositionName + "'); " +
                    "employeeDetail_txtVYUJoinedDate.setValue('" + record.VYUJoinedDate + "'); " +
                    "employeeDetail_txtVYUPositionName.setValue('" + record.VYUPositionName + "'); " +
                    "employeeDetail_txtAssignedWork.setValue('" + record.AssignedWork + "'); " +
                    "employeeDetail_txtQuantumName.setValue('" + record.QuantumName + "'); " +
                    "employeeDetail_txtQuantumCode.setValue('" + record.QuantumCode + "'); " +
                    //"employeeDetail_txtSalaryGrade.setValue('" + record.SalaryGrade + "'); " +
                    "employeeDetail_txtSalaryFactor.setValue('" + record.SalaryFactor + "'); " +
                    effectiveDate +
                    "employeeDetail_txtIDNumber.setValue('" + record.IDNumber + "');" +
                    idIssueDate +
                    "employeeDetail_txtIDIssuePlaceName.setValue('" + record.IDIssuePlaceName + "'); " +
                    "employeeDetail_txtArmyJoinedDate.setValue('" + record.ArmyJoinedDate + "'); " +
                    "employeeDetail_txtArmyLeftDate.setValue('" + record.ArmyLeftDate + "'); " +
                    "employeeDetail_txtArmyLevelName.setValue('" + record.ArmyLevelName + "'); " +
                    "employeeDetail_txtTitleAwarded.setValue('" + record.TitleAwarded + "'); " +
                    "employeeDetail_txtSkills.setValue('" + record.Skills + "'); " +
                    "employeeDetail_txtHealthStatusName.setValue('" + record.HealthStatusName + "'); " +
                    "employeeDetail_txtHeight.setValue('" + record.Height + "'); " +
                    "employeeDetail_txtWeight.setValue('" + record.Weight + "'); " +
                    "employeeDetail_txtBloodGroup.setValue('" + record.BloodGroup + "'); " +
                    "employeeDetail_txtRankWounded.setValue('" + record.RankWounded + "'); " +
                    "employeeDetail_txtFamilyPolicyName.setValue('" + record.FamilyPolicyName + "'); " +
                    "employeeDetail_txtInsuranceNumber.setValue('" + record.InsuranceNumber + "'); " +
                    "employeeDetail_txtInsuranceIssueDate.setValue('" + record.InsuranceIssueDate + "'); " +
                    "employeeDetail_txtWorkStatusName.setValue('" + record.WorkStatusName + "'); " +
                    "employeeDetail_txtMaritalStatusName.setValue('" + record.MaritalStatusName + "'); " +
                    "employeeDetail_txtPersonalTaxCode.setValue('" + record.PersonalTaxCode + "'); " +
                    "employeeDetail_txtCellPhoneNumber.setValue('" + record.CellPhoneNumber + "'); " +
                    "employeeDetail_txtHomePhoneNumber.setValue('" + record.HomePhoneNumber + "'); " +
                    "employeeDetail_txtWorkPhoneNumber.setValue('" + record.WorkPhoneNumber + "'); " +
                    "employeeDetail_txtWorkEmail.setValue('" + record.WorkEmail + "'); " +
                    "employeeDetail_txtPersonalEmail.setValue('" + record.PersonalEmail + "'); " +
                    "employeeDetail_txtAccountNumber.setValue('" + record.AccountNumber + "'); " +
                    "employeeDetail_txtBankName.setValue('" + record.BankName + "'); " +
                    "employeeDetail_txtContactPersonName.setValue('" + record.ContactPersonName + "'); " +
                    "employeeDetail_txtContactRelation.setValue('" + record.ContactRelation + "'); " +
                    "employeeDetail_txtContactPhoneNumber.setValue('" + record.ContactPhoneNumber + "'); " +
                    "employeeDetail_txtContactAddress.setValue('" + record.ContactAddress + "');");
            }
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
            var recruimentDate = filterRecruimentDate.SelectedItem.Value ?? "";
            var position = filterPosition.SelectedItem.Value ?? "";
            var jobTitle = filterJobTitle.SelectedItem.Value ?? "";
            //string ngayHuong = "";
            var basicEducation = filterBasicEducation.SelectedItem.Value ?? "";
            var education = filterEducation.SelectedItem.Value ?? "";
            var politicLevel = filterPoliticLevel.SelectedItem.Value ?? "";
            var manegementLevel = filterManagementLevel.SelectedItem.Value ?? "";
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
                personalClass, recruimentDate, position, jobTitle, basicEducation, education, politicLevel, manegementLevel, languageLevel, itLevel,
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
            range.FontSize = 11 * 20; // TODO : font size -> need move to param

            workbook.setRangeStyle(range, 0, 0, 0, workbook.LastCol);

            workbook.setSheetName(0, "Thêm mới thông tin hồ sơ");
            workbook.insertSheets(1, 1);
            workbook.setSheetName(1, "Info");
            workbook.SheetHidden = WorkBook.SheetStateHidden;
            workbook.Sheet = 0;

            //WriteExcelFile(serverPath, dataTable, workbook);

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
                    //case nameof(hr_Record.WorkStatusId):
                    //    CreateDropDownExcel("cat_WorkStatus", workbook, col, 2, 50);
                    //    break;
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
    }
}

