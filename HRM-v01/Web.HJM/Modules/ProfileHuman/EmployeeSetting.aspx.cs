using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ext.Net;
using System.IO;
using SoftCore;
using System.Data.SqlClient;
using System.Globalization;
using Web.Core.Framework;
using Web.Core.Object.Catalog;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.Catalog;
using Web.Core.Service.Sample;
using Web.Core.Object.Sample;
using Web.Core.Object.Security;
using Web.Core;
using Web.Core.Framework.Common;
using Web.Core.Object.Salary;

namespace Web.HJM.Modules.ProfileHuman
{
    public partial class EmployeeSetting : BasePage
    {
        string[] dsDv;
        int countRole = -1;
        string generateEmployeeConst = "00000000000000000000";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ExtNet.IsAjaxRequest)
            {
                // init department
                storeDepartment.DataSource = CurrentUser.DepartmentsTree;
                storeDepartment.DataBind();

                // init employee type
                storeEmployeeType.DataSource = CatalogController.GetAll("cat_EmployeeType", null, null, null, false, null, null);
                storeEmployeeType.DataBind();

                //init generate employeeCode
                txtEmployeeCode.Text = GenerateEmployeeCode();

                //InitControl();

                if (Request.QueryString["Event"] == "Edit")
                {
                    btnEdit_Click();
                    iBtnSaveAndClear.Hide();
                    iBtnClear.Hide();
                }
               
            }
            hdfTime1.Text = DateTime.Now.ToString();
            EditdfNgayBatDau.Text = DateTime.Now.ToString();
            EditdfNgayKetThuc.Text = DateTime.Now.ToString();
            dfNgayQDKT.Text = DateTime.Now.ToString();
            
            ucSample1.AfterClickAcceptButton += new EventHandler(ucSample1_AfterClickAcceptButton);
        }

        private void ucSample1_AfterClickAcceptButton(object sender, EventArgs e)
        {
            try
            {
                foreach (var item in ucSample1.SelectedRow)
                {
                    hdfSampleID.Text = item.RecordID;
                }
                ResetControl();
                SetValueForControl();
            }
            catch (Exception ex)
            {
                X.Msg.Alert("Thông báo từ hệ thống", "Có lỗi xảy ra: " + ex.Message).Show();
            }
        }

        public void cbxSalaryGradeStore_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            cbxSalaryGrade.Text = "";
            if (!string.IsNullOrEmpty(hdfSalaryQuantumGrid.Text))
            {
                var quantum = cat_QuantumServices.GetById(Convert.ToInt32(hdfSalaryQuantumGrid.Text));
                var quantumGrade = cat_GroupQuantumServices.GetFieldValueById(quantum.GroupQuantumId, "GradeMax");
                int grade = Convert.ToInt32(quantumGrade);
                hdfSalaryGradeGrid.Text = grade.ToString();
                cbxSalaryGrade.Text = "Bậc" + grade.ToString();
                var objs = new List<StoreComboxObject>();
                for (var i = 1; i <= grade; i ++)
                {
                    var stob = new StoreComboxObject
                    {
                        MA = i.ToString(),
                        TEN = "Bậc " + i.ToString()
                    };
                    objs.Add(stob);
                }
                cbxSalaryGradeStore.DataSource = objs;
                cbxSalaryGradeStore.DataBind();
 
            }                  
        }

        protected void BtnSaveSample_Click(object sender, DirectEventArgs e)
        {
            var sl = new SampleList();
            var departments = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
            var arrDepartment = departments.Split(new[] { ',' }, StringSplitOptions.None);
            sl.CreatedBy = CurrentUser.User.UserName;
            sl.CreatedDate = DateTime.Now;
            sl.Name = txtSampleName.Text;
            sl.Note = txtSampleNote.Text;
            var listSampleDetail = new List<SampleDetail>();
            foreach (var item in pn.ContentControls)
            {
                var info = new SampleDetail();
                switch (item.ToString())
                {
                    case "Ext.Net.ComboBox":
                        var cb = item as Ext.Net.ComboBox;
                        info.Control = cb.ID;
                        info.Value = cb.SelectedItem.Text;
                        info.DataType = "Ext.Net.ComboBox";
                        break;
                    case "Ext.Net.Hidden":
                        var hdf = item as Ext.Net.Hidden;
                        info.Control = hdf.ID;
                        info.Value = hdf.Text;
                        info.DataType = "Ext.Net.Hidden";
                        break;
                    case "Ext.Net.TextArea":
                        var txta = item as Ext.Net.TextArea;
                        info.Control = txta.ID;
                        info.Value = txta.Text;
                        info.DataType = "Ext.Net.TextArea";
                        break;
                    case "Ext.Net.TextField":
                        var txtf = item as Ext.Net.TextField;
                        info.Control = txtf.ID;
                        info.Value = txtf.Text;
                        info.DataType = "Ext.Net.TextField";
                        break;
                    case "Ext.Net.NumberField":
                        var mbf = item as Ext.Net.NumberField;
                        info.Control = mbf.ID;
                        info.Value = mbf.Text;
                        info.DataType = "Ext.Net.NumberField";
                        break;
                    case "Ext.Net.DateField":
                        var df = item as Ext.Net.DateField;
                        info.Control = df.ID;
                        info.Value = df.SelectedDate.ToString("yyyy/MM/dd");
                        if (info.Value.Contains("0001/"))
                        {
                            info.Value = "";
                        }
                        info.DataType = "Ext.Net.DateField";
                        break;
                    default:
                        break;
                }
                if (!string.IsNullOrEmpty(info.Value) && info.Value != "" && info.Control != "txt_EmployeeCode")
                {
                    listSampleDetail.Add(info);
                }
            }
            if (listSampleDetail.Count == 0)
            {
                Dialog.ShowError("Chưa có dữ liệu nào được nhập trong để tạo mẫu");
                return;
            }
            else
            {
                try
                {
                    for (var i = 0; i < arrDepartment.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(arrDepartment[i]))
                        {
                            sl.DepartmentId = Convert.ToInt32(arrDepartment[i]);
                        }
                        SampleListServices.Create(sl);
                    }
                    hdfSampleID.Text = sl.Id.ToString();
                    foreach (var t in listSampleDetail)
                    {
                        t.IDSample = int.Parse("0" + hdfSampleID.Text);
                    }
                    foreach (var obj in listSampleDetail)
                    {
                        SampleDetailServices.Create(obj);
                    }
                   
                }
                catch (Exception ex)
                {
                    Dialog.ShowError("" + ex.Message);
                }
            }
            RM.RegisterClientScriptBlock("hideCreateSample", "hideCreateSample();");
        }
        public void ResetControl()
        {
            foreach (var item in pn.ContentControls)
            {
                var info = new SampleDetail();
                switch (item.ToString())
                {
                    case "Ext.Net.ComboBox":
                        var cb = item as Ext.Net.ComboBox;
                        cb.Reset();
                        break;
                    case "Ext.Net.Hidden":
                        var hdf = item as Ext.Net.Hidden;
                        hdf.Reset();
                        break;
                    case "Ext.Net.TextArea":
                        var txta = item as Ext.Net.TextArea;
                        txta.Reset();
                        break;
                    case "Ext.Net.TextField":
                        var txtf = item as Ext.Net.TextField;
                        txtf.Reset();
                        break;
                    case "Ext.Net.NumberField":
                        var mbf = item as Ext.Net.NumberField;
                        mbf.Reset();
                        break;
                    case "Ext.Net.DateField":
                        var df = item as Ext.Net.DateField;
                        df.Reset();
                        break;
                    default:
                        break;
                }
            }
        }
        [DirectMethod]
        public void SetValueForControl()
        {
            var sql = string.Empty;
            sql += "SELECT [Id], [Control], [Value], [DataType] FROM SampleDetail WHERE IDSample = {0} ".FormatWith(int.Parse("0" + hdfSampleID.Text));
            var data = SQLHelper.ExecuteTable(sql);

            for (var i = 0; i < data.Rows.Count; i++)
            {
                var control = Page.FindControl(data.Rows[i]["Control"].ToString());
                switch (control.ToString())
                {
                    case "Ext.Net.NumberField":
                        var f = control as Ext.Net.NumberField;
                        f.SetValue(data.Rows[i]["Value"].ToString());
                        break;
                    case "Ext.Net.Checkbox":
                        var ck = control as Ext.Net.Checkbox;
                        ck.Checked = data.Rows[i]["Value"].ToString().Equals("1") ? true : false;
                        break;
                    case "Ext.Net.Radio":
                        var rd = control as Ext.Net.Radio;
                        rd.Checked = data.Rows[i]["Value"].ToString().Equals("1") ? true : false;
                        break;
                    case "Ext.Net.TextArea":
                        var txt = control as Ext.Net.TextArea;
                        txt.SetValue(data.Rows[i]["Value"].ToString());
                        break;
                    case "Ext.Net.TextField":
                        var txtf = control as Ext.Net.TextField;
                        txtf.SetValue(data.Rows[i]["Value"].ToString());
                        break;
                    case "Ext.Net.ComboBox":
                        var cbb = control as Ext.Net.ComboBox;
                        cbb.SetValue(data.Rows[i]["Value"].ToString());
                        break;
                    case "Ext.Net.Hidden":
                        var hdf = control as Ext.Net.Hidden;
                        hdf.SetValue(data.Rows[i]["Value"].ToString());
                        break;
                    case "Ext.Net.DateField":
                        var df = control as Ext.Net.DateField;
                        df.SetValue(data.Rows[i]["Value"].ToString());
                        break;
                    default:
                        break;
                }
            }
        }

        [DirectMethod]
        public void GetSalaryInfoGrid()
        {
            if (!string.IsNullOrEmpty(hdfSalaryQuantumGrid.Text) && !string.IsNullOrEmpty(hdfSalaryGradeGrid.Text))
            {
                var quantum = cat_QuantumServices.GetById(Convert.ToInt32(hdfSalaryQuantumGrid.Text));
                var salaryFactor = CatalogGroupQuantumGradeController
                    .GetUnique(quantum.GroupQuantumId, Convert.ToInt32(hdfSalaryGradeGrid.Text)).Factor;
                nfDblHeSoLuong.SetValue(salaryFactor);
                
                RM.RegisterClientScriptBlock("aaaa", "updateSalaryProcess('SalaryFactor', parseFloat('" + salaryFactor + "'.replace(',', '.')));");
                RM.RegisterClientScriptBlock("cccc", "updateSalaryProcess('GroupQuantumId', parseInt('" + quantum.GroupQuantumId + "'));");
                RM.RegisterClientScriptBlock("bbbb", "GridPanelSalary.getView().refresh();");
            }          
        }
        [DirectMethod]
        public void getQuantumCode()
        {
            var quantum = cat_QuantumServices.GetById(Convert.ToInt32(hdfSalaryQuantumGrid.Text));
            txtColQuantumCode.Text = quantum.Code;
            txtColQuantumCode.SetValue(quantum.Code);
            RM.RegisterClientScriptBlock("code", "updateSalaryProcess('QuantumCode', '" + quantum.Code + "' );");
            RM.RegisterClientScriptBlock("name", "updateSalaryProcess('QuantumName', '" + quantum.Name + "' );");
            RM.RegisterClientScriptBlock("view", "GridPanelSalary.getView().refresh();");
        }

        public string UploadFile(object sender, string relativePath)
        {
            FileUploadField obj = (FileUploadField)sender;
            HttpPostedFile file = obj.PostedFile;
            DirectoryInfo dir = new DirectoryInfo(Server.MapPath("../") + relativePath);
            if (dir.Exists == false)
                dir.Create();
            string rdstr = SoftCore.Util.GetInstance().GetRandomString(7);
            string path = Server.MapPath("../") + relativePath + "/" + rdstr + "_" + obj.FileName.Trim().Replace(" ", "_");
            if (File.Exists(path))
                return "";
            FileInfo info = new FileInfo(path);
            file.SaveAs(path);
            return "/Modules/" + relativePath + "/" + rdstr + "_" + obj.FileName.Trim().Replace(" ", "_");
        }

        public void DownloadAttachFile(string path)
        {
            try
            {
                if (string.IsNullOrEmpty(path))
                {
                    Dialog.ShowNotification("Không có tệp tin đính kèm");
                    return;
                }
                string serverPath = Server.MapPath(path);
                if (Util.GetInstance().FileIsExists(serverPath) == false)
                {
                    Dialog.ShowNotification("Tệp tin không tồn tại hoặc đã bị xóa !");
                    return;
                }
                Response.Clear();

                Response.AddHeader("Content-Disposition", "attachment; filename=" + path.Replace(" ", "_"));
                Response.WriteFile(serverPath);
                Response.End();
            }
            catch (Exception ex)
            {
                X.Msg.Alert("Thông báo từ hệ thống", ex.Message).Show();
            }
        }

        protected void btnQDLDownload_Click(object sender, DirectEventArgs e)
        {
            if (!string.IsNullOrEmpty(hdfPrKeyHoSo.Text))
            {
                var hs = RecordController.GetById(Convert.ToInt32(hdfPrKeyHoSo.Text));
                DownloadAttachFile(hs.OriginalFile);
            }
        }

        protected void btnFileLDelete_Click(object sender, DirectEventArgs e)
        {
            if (!string.IsNullOrEmpty(hdfPrKeyHoSo.Text))
            {
                var hs = RecordController.GetById(Convert.ToInt32(hdfPrKeyHoSo.Text));
                string serverPath = Server.MapPath(hs.OriginalFile);
                if (Util.GetInstance().FileIsExists(serverPath) == false)
                {
                    Dialog.ShowNotification("Tệp tin không tồn tại hoặc đã bị xóa !");
                    return;
                }
                File.Delete(serverPath);
                hs.OriginalFile = null;
                fufTepTinDinhKem.Text = @"Chọn tệp tin";
                RM.RegisterClientScriptBlock("save", "gridsSave();");
                Dialog.ShowNotification("Cập nhật dữ liệu thành công!");
                if (e.ExtraParams["Reset"] == "True")
                {
                    RM.RegisterClientScriptBlock("rs1", "ResetForm();");
                }
            }
        }

        protected void iBtnSave_Click(object sender, DirectEventArgs e)
        {
            try
            {
                var util = new Util();
                var hoso = new RecordController();
                var hs = new hr_Record();
                
                if (!string.IsNullOrEmpty(hdfPrKeyHoSo.Text))
                {
                    // check if Employee Code exists
                    hs.EmployeeCode = txtEmployeeCode.Text;
                    if (!string.IsNullOrEmpty(txtEmployeeCode.Text.Trim()))
                    {
                        List<RecordModel> recordList = RecordController.CheckExistIDNumberAndEmployeeCode(null, null, txtEmployeeCode.Text);
                        if (recordList != null && recordList.Count > 1)
                        {
                            Dialog.ShowError("Mã cán bộ đã tồn tại. Vui lòng nhập mã cán bộ khác.");
                            txtEmployeeCode.Text = GenerateEmployeeCode();
                            return;
                        }
                    }

                }              
                else
                {
                    // check if Employee Code exists
                    hs.EmployeeCode = txtEmployeeCode.Text;
                    if (!string.IsNullOrEmpty(txtEmployeeCode.Text.Trim()))
                    {
                        List<RecordModel> recordList = RecordController.CheckExistIDNumberAndEmployeeCode(null, null, txtEmployeeCode.Text);
                        if (recordList != null && recordList.Count > 0)
                        {
                            Dialog.ShowError("Mã cán bộ đã tồn tại. Vui lòng nhập mã cán bộ khác.");
                            return;
                        }
                    }
                    // check if ID number exists
                    if (!string.IsNullOrEmpty(txtIDNumber.Text.Trim()))
                    {
                        List<RecordModel> recordList = RecordController.CheckExistIDNumberAndEmployeeCode(null, txtIDNumber.Text, null);
                        if (recordList != null && recordList.Count > 0)
                        {
                            Dialog.ShowError("Số chứng minh nhân dân đã tồn tại. Vui lòng nhập số chứng minh nhân dân khác.");
                            return;
                        }
                    }
                }
                // Ảnh đại diện
                if (!string.IsNullOrEmpty(hdfAnhDaiDien.Text))
                    hs.ImageUrl = hdfAnhDaiDien.Text;
                // Loại cán bộ
                if (!string.IsNullOrEmpty(hdfEmployeeTypeId.Text))
                    hs.EmployeeTypeId = Convert.ToInt32(hdfEmployeeTypeId.Text);
                // Cơ quan, đơn vị có thẩm quyền quản lý CBCC
                if (!string.IsNullOrEmpty(hdfManagementDepartmentId.Text))
                    hs.ManagementDepartmentId = Convert.ToInt32(hdfManagementDepartmentId.Text);
                // Cơ quan, đơn vị sử dụng CBCC
                if (!string.IsNullOrEmpty(hdfDepartmentId.Text))
                    hs.DepartmentId = Convert.ToInt32(hdfDepartmentId.Text);
                // 1) Họ và tên khai sinh
                hs.FullName = txtFullName.Text;
                // lấy họ và đệm từ họ tên
                var position = hs.FullName.LastIndexOf(' ');
                if (position == -1)
                {
                    hs.Name = hs.FullName;
                }
                else
                {
                    hs.Name = hs.FullName.Substring(position + 1).Trim();
                }
                // 2) Tên gọi khác
                hs.Alias = txtAlias.Text;
                // 3) Ngày sinh
                if (!util.IsDateNull(dfBirthDate.SelectedDate))
                    hs.BirthDate = dfBirthDate.SelectedDate;
                // Giới tính
                if(!string.IsNullOrEmpty(cbxSex.SelectedItem.Value) && cbxSex.SelectedItem.Value == "M")
                {
                    hs.Sex = true;
                }
                else
                {
                    hs.Sex = false;
                }
                // Tình trạng hôn nhân
                if (!string.IsNullOrEmpty(hdfMaritalStatusId.Text))
                    hs.MaritalStatusId = Convert.ToInt32(hdfMaritalStatusId.Text);
                // 4) Nơi sinh
                if (!string.IsNullOrEmpty(hdfBirthPlaceProvinceId.Text))
                    hs.BirthPlaceProvinceId = Convert.ToInt32(hdfBirthPlaceProvinceId.Text);
                if (!string.IsNullOrEmpty(hdfBirthPlaceDistrictId.Text))
                    hs.BirthPlaceDistrictId = Convert.ToInt32(hdfBirthPlaceDistrictId.Text);
                if(!string.IsNullOrEmpty(hdfBirthPlaceWardId.Text))
                    hs.BirthPlaceWardId = Convert.ToInt32(hdfBirthPlaceWardId.Text);
                // 5) Quê quán
                if (!string.IsNullOrEmpty(hdfHometownProvinceId.Text))
                    hs.HometownProvinceId = Convert.ToInt32(hdfHometownProvinceId.Text);
                if(!string.IsNullOrEmpty(hdfHometownDistrictId.Text))
                    hs.HometownDistrictId = Convert.ToInt32(hdfHometownDistrictId.Text);
                if(!string.IsNullOrEmpty(hdfHometownWardId.Text))
                    hs.HometownWardId = Convert.ToInt32(hdfHometownWardId.Text);
                // 6) Dân tộc
                if (!string.IsNullOrEmpty(hdfFolkId.Text))
                    hs.FolkId = Convert.ToInt32(hdfFolkId.Text);
                // 6)Hồ sơ gốc
                if (fufTepTinDinhKem.HasFile)
                {
                    string serverPath = Server.MapPath(hs.OriginalFile);
                    if (Util.GetInstance().FileIsExists(serverPath) == false)
                    {
                        hs.OriginalFile = UploadFile(fufTepTinDinhKem, Constant.PathContract);
                    }
                    else
                    {
                        File.Delete(serverPath);
                        hs.OriginalFile = UploadFile(fufTepTinDinhKem, Constant.PathContract);
                    }
                }
                // 7) Tôn giáo
                if (!string.IsNullOrEmpty(hdfReligionId.Text))
                    hs.ReligionId = Convert.ToInt32(hdfReligionId.Text);
                // Thành phần bản thân
                if (!string.IsNullOrEmpty(hdfPersonalClassId.Text))
                    hs.PersonalClassId = Convert.ToInt32(hdfPersonalClassId.Text);
                // Thành phần gia đình
                if (!string.IsNullOrEmpty(hdfFamilyClassId.Text))
                    hs.FamilyClassId = Convert.ToInt32(hdfFamilyClassId.Text);
                // 8) Nơi đăng ký hộ khẩu thường trú
                hs.ResidentPlace = txt_ResidentPlace.Text;
                // 9) Nơi ở hiện nay
                hs.Address = txt_Address.Text;
                // 10) Nghề nghiệp khi được tuyển dụng
                hs.PreviousJob = txtPreviousJob.Text;
                // 11) Ngày tuyển dụng
                if (!util.IsDateNull(RecruimentDate.SelectedDate))
                    hs.RecruimentDate = RecruimentDate.SelectedDate;
                if (!util.IsDateNull(FunctionaryDate.SelectedDate))
                    hs.FunctionaryDate = FunctionaryDate.SelectedDate;
                // 19) Danh hiệu được phong tặng cao nhất
                hs.TitleAwarded = txtTitleAwarded.Text;
                // 20) Sở trường công tác
                hs.Skills = txtSkills.Text;
                // 21) Tình trạng sức khỏe
                if (!string.IsNullOrEmpty(hdfHealthStatusId.Text))
                    hs.HealthStatusId = Convert.ToInt32(hdfHealthStatusId.Text);
                hs.BloodGroup = cbxBloodGroup.SelectedIndex >= 0 ? cbxBloodGroup.SelectedItem.Value : "";
                if (!string.IsNullOrEmpty(txtHeight.Text))
                    hs.Height = Convert.ToDecimal(txtHeight.Text);
                if (!string.IsNullOrEmpty(txtWeight.Text))
                    hs.Weight = Convert.ToDecimal(txtWeight.Text);   
                // 22) Là thương binh hạng
                hs.RankWounded = txtRankWounded.Text;
                if(!string.IsNullOrEmpty(hdfFamilyPolicyId.Text))
                    hs.FamilyPolicyId = Convert.ToInt32(hdfFamilyPolicyId.Text);
                // 23) Số chứng minh nhân dân
                if (!string.IsNullOrEmpty(txtIDNumber.Text))
                    hs.IDNumber = txtIDNumber.Text;
                if (!util.IsDateNull(IDIssueDate.SelectedDate))
                    hs.IDIssueDate = IDIssueDate.SelectedDate;
                if(!string.IsNullOrEmpty(hdfIDIssuePlaceId.Text))
                    hs.IDIssuePlaceId = Convert.ToInt32(hdfIDIssuePlaceId.Text);
                // 24) Số sổ BHXH
                hs.InsuranceNumber = txtInsuranceNumber.Text;
                hs.PersonalTaxCode = txtPersonalTaxCode.Text;
                if (!util.IsDateNull(InsuranceIssueDate.SelectedDate))
                {
                    hs.InsuranceIssueDate = InsuranceIssueDate.SelectedDate;
                }

                if (!util.IsDateNull(VYUJoinedDate.SelectedDate))
                    hs.VYUJoinedDate = VYUJoinedDate.SelectedDate;
                // 27) Đặc điểm lịch sử bản thân
                hs.Biography = txtBiography.Text;
                hs.ForeignOrganizationJoined = txtForeignOrganizationJoined.Text;
                hs.RelativesAboard = txtRelativesAboard.Text;

                hs.AssignedWork = txtAssignedWork.Text;
                hs.RecruimentDepartment = txtRecruitmentDepartment.Text;

                hs.CPVCardNumber = txtCPVCardNumber.Text;
                if(!string.IsNullOrEmpty(hdfJobTitleId.Text))
                    hs.JobTitleId = Convert.ToInt32(hdfJobTitleId.Text);
                if (!string.IsNullOrEmpty(hdfBasicEducationId.Text))
                    hs.BasicEducationId = Convert.ToInt32(hdfBasicEducationId.Text);
                if (!string.IsNullOrEmpty(hdfEducationId.Text))
                    hs.EducationId = Convert.ToInt32(hdfEducationId.Text);
                if (!string.IsNullOrEmpty(hdfPoliticLevelId.Text))
                    hs.PoliticLevelId = Convert.ToInt32(hdfPoliticLevelId.Text);
                if (!string.IsNullOrEmpty(hdfManagementLevelId.Text))
                    hs.ManagementLevelId = Convert.ToInt32(hdfManagementLevelId.Text);
                if (!string.IsNullOrEmpty(hdfLanguageLevelId.Text))
                    hs.LanguageLevelId = Convert.ToInt32(hdfLanguageLevelId.Text);
                if (!string.IsNullOrEmpty(hdfITLevelId.Text))
                    hs.ITLevelId = Convert.ToInt32(hdfITLevelId.Text);
                if (!util.IsDateNull(CPVJoinedDate.SelectedDate))
                    hs.CPVJoinedDate = CPVJoinedDate.SelectedDate;
                if (!util.IsDateNull(CPVOfficialJoinedDate.SelectedDate))
                    hs.CPVOfficialJoinedDate = CPVOfficialJoinedDate.SelectedDate;
                hs.CPVJoinedPlace = txtCPVJoinedPlace.Text;
                if(!string.IsNullOrEmpty(hdfCPVPositionId.Text))
                    hs.CPVPositionId = Convert.ToInt32(hdfCPVPositionId.Text);
                if(!string.IsNullOrEmpty(hdfVYUPositionId.Text))
                    hs.VYUPositionId = Convert.ToInt32(hdfVYUPositionId.Text);
                if (!util.IsDateNull(ArmyJoinedDate.SelectedDate))
                    hs.ArmyJoinedDate = ArmyJoinedDate.SelectedDate;
                if (!util.IsDateNull(ArmyLeftDate.SelectedDate))
                    hs.ArmyLeftDate = ArmyLeftDate.SelectedDate;
                if (!string.IsNullOrEmpty(hdfPositionId.Text))
                    hs.PositionId = Convert.ToInt32(hdfPositionId.Text);
                if (!string.IsNullOrEmpty(hdfGovernmentPositionId.Text))
                    hs.GovernmentPositionId = Convert.ToInt32(hdfGovernmentPositionId.Text);
                if (!string.IsNullOrEmpty(hdfPluralityPositionId.Text))
                    hs.PluralityPositionId = Convert.ToInt32(hdfPluralityPositionId.Text);
                if (!string.IsNullOrEmpty(hdfArmyLevelId.Text))
                    hs.ArmyLevelId = Convert.ToInt32(hdfArmyLevelId.Text);
                // 31) Nguồn thu nhập của gia đình (hàng năm)
                if (!string.IsNullOrEmpty(txtFamilyIncome.Text))
                    if (int.TryParse(txtFamilyIncome.Text, NumberStyles.AllowThousands,
                        CultureInfo.InvariantCulture, out var num))
                        hs.FamilyIncome = num;                 
                hs.OtherIncome = txtOtherIncome.Text;
                hs.AllocatedHouse = txtAllocatedHouse.Text;
                if (!string.IsNullOrEmpty(txtAllocatedHouseArea.Text))
                  hs.AllocatedtHouseArea = decimal.Parse(txtAllocatedHouseArea.Text);
                hs.House = txtHouse.Text;
                if (!string.IsNullOrEmpty(txtHouseArea.Text))
                    hs.HouseArea = decimal.Parse(txtHouseArea.Text);
                hs.OtherIncome = txtOtherIncome.Text;
                if (!string.IsNullOrEmpty(txtAllocatedLandArea.Text))
                    hs.AllocatedLandArea = decimal.Parse(txtAllocatedLandArea.Text);
                if (!string.IsNullOrEmpty(txtLandArea.Text))
                    hs.LandArea = decimal.Parse(txtLandArea.Text);
                hs.BusinessLandArea = decimal.Parse(txtBusinessLand.Text);
                // 37) Nhận xét, đánh giá của đơn vị quản lý sử dụng cán bộ, công chức
                hs.Review = txtReview.Text;


                if(!string.IsNullOrEmpty(hdfWorkStatusId.Text))
                    hs.WorkStatusId = Convert.ToInt32(hdfWorkStatusId.Text);
                if (!util.IsDateNull(WorkStatusDate.SelectedDate))
                    hs.WorkStatusDate = WorkStatusDate.SelectedDate;
                hs.WorkStatusReason = txtWorkStatusReason.Text;
                hs.WorkEmail = txtWorkEmail.Text;
                hs.PersonalEmail = txtPersonalEmail.Text;
                hs.CellPhoneNumber = txtCellPhoneNumber.Text;
                hs.HomePhoneNumber = txtHomePhoneNumber.Text;
                hs.WorkPhoneNumber = txtWorkPhoneNumber.Text;
                hs.TimeKeepingCode = txtTimeKeepingCode.Text;
                hs.ContactPersonName = txtContactPersonName.Text;
                hs.ContactPhoneNumber = txtContactPhoneNumber.Text;
                hs.ContactRelation = txtContactRelation.Text;
                hs.ContactAddress = txtContactAddress.Text;
                if (!util.IsDateNull(RevolutionJoinDate.SelectedDate))
                {
                    hs.RevolutionJoinedDate = RevolutionJoinDate.SelectedDate;
                }

                hs.LongestJob = txtLongestJob.Text;
                if (!string.IsNullOrEmpty(hdfInputIndustryId.Text))
                    hs.IndustryId = Convert.ToInt32(hdfInputIndustryId.Text);
                if (!util.IsDateNull(FunctionaryDate.SelectedDate))
                {
                    hs.FunctionaryDate = FunctionaryDate.SelectedDate;
                }
                if (Request.QueryString["Event"] == "Edit" || !string.IsNullOrEmpty(hdfPrKeyHoSo.Text))
                {
                    #region Update Ho so
                    hs.Id = Convert.ToInt32(hdfPrKeyHoSo.Text);
                    RecordController.UpdateRecord(hs);
                    RM.RegisterClientScriptBlock("save", "gridsSave();");
                    Dialog.ShowNotification("Cập nhật dữ liệu thành công!");
                    #endregion
                }
                else
                {
                    #region Insert Ho so
                    RecordController.InsertRecord(hs);
                    hdfPrKeyHoSo.Text = hs.Id.ToString();
                    RM.RegisterClientScriptBlock("save", "gridsSave();");
                    Dialog.ShowNotification("Thêm mới thành công!");
                    if (e.ExtraParams["Reset"] == "True")
                    {
                        RM.RegisterClientScriptBlock("rs2", "ResetForm();");
                    }
                    #endregion
                }

                int RecordId = hs.Id ;//hs.PR_KEY sau thay bang Id cua bang hr_Record (hs.Id)

                SaveGridEducationHistory(e.ExtraParams["jsonEducation"], RecordId);
                SaveGridSalary(e.ExtraParams["jsonSalary"], RecordId);
                SaveGridWorkHistory(e.ExtraParams["jsonWorkHistory"], RecordId);
                SaveGridReward(e.ExtraParams["jsonReward"], RecordId);
                SaveGridDiscipline(e.ExtraParams["jsonDiscipline"], RecordId);
                SaveGridContract(e.ExtraParams["jsonContract"], RecordId);
                SaveGridTrainingHistory(e.ExtraParams["jsonTrainingHistory"], RecordId);
                SaveGridFamilyRelationship(e.ExtraParams["jsonFamilyRelationship"], RecordId);
                SaveGridAbility(e.ExtraParams["jsonAbility"], RecordId);
                SaveGridWorkProcess(e.ExtraParams["jsonWorkProcess"], RecordId);
                SaveGridInsurance(e.ExtraParams["jsonInsurance"], RecordId);
                RM.RegisterClientScriptBlock("save", "gridsSave();");
                if (e.ExtraParams["Reset"] == "True")
                {
                    RM.RegisterClientScriptBlock("ClearForm", "ResetForm();");
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }


        /// <summary>
        /// Sinh mã cán bộ dựa vào cấu hình hệ thống:
        /// -   Tiền tố của mã cán bộ
        /// -   Số lượng chữ số theo sau tiền tố
        /// </summary>
        /// <returns>Mã cán bộ mới được sinh ra</returns>
        public string GenerateEmployeeCode()
        {
            string departments = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
            string prefix = SystemConfigController.GetValueByNameFollowDepartment(SystemConfigParameter.PREFIX, departments);
            string numberCharacter = SystemConfigController.GetValueByNameFollowDepartment(SystemConfigParameter.NUMBER_OF_CHARACTER, departments);
            if (string.IsNullOrEmpty(prefix))
                prefix = "CB";
            int number = string.IsNullOrEmpty(numberCharacter) ? 5 : int.Parse(numberCharacter);

            var record = RecordController.GetByEmployeeCodeGenerate(prefix, number);
            var oldMaCB = generateEmployeeConst;
            if (record != null && !string.IsNullOrEmpty(record.EmployeeCode))
                oldMaCB = record.EmployeeCode;
            long oldNumber = long.Parse("" + oldMaCB.Substring(prefix.Length));
            oldNumber++;
            string newMaCB = generateEmployeeConst + oldNumber;
            newMaCB = prefix + newMaCB.Substring(newMaCB.Length - number);
            return newMaCB;
        }

        #region Salary
        private void SaveGridSalary(string json, int recordId)
        {
            try
            {
                if (!string.IsNullOrEmpty(json))
                {
                    var controller = new SalaryDecisionController();
                    var rs = Ext.Net.JSON.Deserialize<List<SalaryDecisionModel>>(json);
                    foreach (var item in rs)
                    {
                        var obj = new sal_SalaryDecision
                        {
                            Id = item.Id,
                            DecisionNumber = item.DecisionNumber,
                            QuantumId = item.QuantumId,
                            DecisionDate = item.DecisionDate,
                            EffectiveDate = item.EffectiveDate,
                            Note = item.Note,
                            CreatedDate = DateTime.Now,
                            EditedDate = DateTime.Now,
                        };
                            obj.Factor = item.Factor;

                        obj.RecordId = recordId;
                        if (obj.Id > 0)
                        {
                            SalaryDecisionController.Update(new SalaryDecisionModel(obj));
                            StoreSalary.CommitChanges();
                        }
                        else
                        {
                            SalaryDecisionController.Create(new SalaryDecisionModel(obj));
                            StoreSalary.DataSource = SalaryDecisionController.GetAll(null, null, recordId, null, null, null, null, null, null, null, false, null, null);
                            StoreSalary.DataBind();
                            StoreSalary.CommitChanges();
                        }
                    }
                    GridPanelSalary.Reload();
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }
        #endregion

        private void SaveGridInsurance(string json, int recordId)
        {
            try
            {
                if (!string.IsNullOrEmpty(json))
                {
                    var controller = new InsuranceController();
                    var rs = Ext.Net.JSON.Deserialize<List<InsuranceModel>>(json);
                    foreach (var item in rs)
                    {
                        var obj = new hr_Insurance
                        {
                            Id = item.Id,
                            PositionId = item.PositionId,
                            SalaryFactor = item.SalaryFactor,
                            Allowance = item.Allowance,
                            SalaryLevel = item.SalaryLevel,
                            Note = item.Note,
                            FromDate = item.FromDate,
                            ToDate = item.ToDate,
                            CreatedDate = DateTime.Now,
                            EditedDate = DateTime.Now,
                        };
                        obj.RecordId = recordId;
                        if (obj.Id > 0)
                        {
                            controller.Update(obj);
                            storeInsurance.CommitChanges();
                        }
                        else
                        {
                            controller.Insert(obj);
                            storeInsurance.DataSource = InsuranceController.GetAll(recordId);
                            storeInsurance.DataBind();
                            storeInsurance.CommitChanges();
                        }
                    }
                    GridInsurance.Reload();
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }

        private void SaveGridWorkProcess(string json, int recordId)
        {
            try
            {
                if (!string.IsNullOrEmpty(json))
                {
                    var controller = new WorkProcessController();
                    var rs = Ext.Net.JSON.Deserialize<List<WorkProcessModel>>(json);
                    foreach (var item in rs)
                    {
                        var obj = new hr_WorkProcess
                        {
                            Id = item.Id,
                            Note = item.Note,
                            EffectiveDate = item.EffectiveDate,
                            EffectiveEndDate = item.EffectiveEndDate,
                            CreatedDate = DateTime.Now,
                            EditedDate = DateTime.Now,

                        };
                        obj.RecordId = recordId;
                        if (obj.Id > 0)
                        {
                            controller.Update(obj);
                            StoreWorkProcess.CommitChanges();
                        }
                        else
                        {
                            controller.Insert(obj);
                            StoreWorkProcess.DataSource = WorkProcessController.GetAll(recordId);
                            StoreWorkProcess.DataBind();
                            StoreWorkProcess.CommitChanges();
                        }
                    }
                    GridWorkProcess.Reload();
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }

        private void SaveGridAbility(string json, int recordId)
        {
            try
            {
                if (!string.IsNullOrEmpty(json))
                {
                    var controller = new AbilityController();
                    var rs = Ext.Net.JSON.Deserialize<List<AbilityModel>>(json);
                    foreach (var item in rs)
                    {
                        var obj = new hr_Ability
                        {
                            Id = item.Id,                 
                            Note = item.Note,
                            AbilityId = item.AbilityId,
                            GraduationTypeId = item.GraduationTypeId,
                            CreatedDate = DateTime.Now,
                            EditedDate = DateTime.Now,

                        };

                        obj.RecordId = recordId;
                        if (obj.Id > 0)
                        {
                            controller.Update(obj);
                            storeAbility.CommitChanges();
                        }
                        else
                        {
                            controller.Insert(obj);
                            storeAbility.DataSource = AbilityController.GetAll(recordId);
                            storeAbility.DataBind();
                            storeAbility.CommitChanges();
                        }
                    }
                    gridAbility.Reload();
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }

        private void SaveGridFamilyRelationship(string json, int recordId)
        {
            try
            {
                if (!string.IsNullOrEmpty(json))
                {
                    var controller = new FamilyRelationshipController();
                    var rs = Ext.Net.JSON.Deserialize<List<FamilyRelationshipModel>>(json);
                    foreach (var item in rs)
                    {
                        var obj = new hr_FamilyRelationship
                        {
                            Id = item.Id,
                            FullName = item.FullName,
                            BirthYear = item.BirthYear,
                            Sex = item.Sex,
                            RelationshipId = item.RelationshipId,
                            Occupation = item.Occupation,
                            WorkPlace = item.WorkPlace,
                            Note = item.Note,
                            IsDependent = item.IsDependent,
                            IDNumber = item.IDNumber,
                            CreatedDate = DateTime.Now,
                            EditedDate = DateTime.Now,
                        };
                        if (!string.IsNullOrEmpty(item.SexName) && item.SexName == "M")
                        {
                            obj.Sex = true;
                        }
                        else
                        {
                            obj.Sex = false;
                        }
                        obj.RecordId = recordId;
                        if (obj.Id > 0)
                        {
                            controller.Update(obj);
                            storeFamilyRelationship.CommitChanges();
                        }
                        else
                        {
                            controller.Insert(obj);
                            storeFamilyRelationship.DataSource = FamilyRelationshipController.GetAll(recordId);
                            storeFamilyRelationship.DataBind();
                            storeFamilyRelationship.CommitChanges();
                        }
                    }
                    GridPanelFamilyRelationship.Reload();
                }

            }
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }

        private void SaveGridEducationHistory(string json, int recordId)
        {
            try
            {
                if (!string.IsNullOrEmpty(json))
                {
                    var controller = new EducationHistoryController();  
                    var rs = Ext.Net.JSON.Deserialize<List<EducationHistoryModel>>(json);
                    foreach (var item in rs)
                    {
                        var obj = new hr_EducationHistory
                        {
                            Id = item.Id,
                            NationId = item.NationId,
                            UniversityId = item.UniversityId,
                            IndustryId = item.IndustryId,
                            TrainingSystemId = item.TrainingSystemId,
                            EducationId = item.EducationId,
                            CreatedDate = DateTime.Now,
                            EditedDate = DateTime.Now,

                        };
                        if (item.FromDate != null)
                            obj.FromDate = item.FromDate;
                        if (item.ToDate != null)
                            obj.ToDate = item.ToDate;

                        obj.RecordId = recordId;
                        if (obj.Id > 0)
                        {
                            controller.UpdateEducation(obj);
                            StoreEducation.CommitChanges();
                        }
                        else
                        {
                            controller.InsertEducation(obj);
                            StoreEducation.DataSource = EducationHistoryController.GetAll(recordId);
                            StoreEducation.DataBind();
                            StoreEducation.CommitChanges();
                        }
                    }
                    GridEducation.Reload();
                }
               
            }
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }

        private void SaveGridReward(string json, int RecordId)
        {
            try
            {
                if (!string.IsNullOrEmpty(json))
                {
                    var controller = new RewardController();
                    var rs = Ext.Net.JSON.Deserialize<List<RewardModel>>(json);
                    foreach (var item in rs)
                    {
                        var obj = new hr_Reward()
                        {
                            Id = item.Id,
                            LevelRewardId = item.LevelRewardId,
                            DecisionNumber = item.DecisionNumber,
                            FormRewardId = item.FormRewardId,
                            Note = item.Note,
                            CreatedDate = DateTime.Now,
                            EditedDate = DateTime.Now,
                        };
                        if (item.DecisionDate != null)
                            obj.DecisionDate = item.DecisionDate;
                        obj.RecordId = RecordId;
                        if (obj.Id > 0)
                        {
                            controller.Update(obj);
                            storeReward.CommitChanges();
                        }
                        else
                        {
                            controller.Insert(obj);
                            storeReward.DataSource = RewardController.GetAll(RecordId);
                            storeReward.DataBind();
                            storeReward.CommitChanges();
                        }
                    }
                    GridReward.Reload();
                }                
            }
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }
        private void SaveGridDiscipline(string json, int RecordId)
        {
            try
            {
                if (!string.IsNullOrEmpty(json))
                {
                    var controller = new DisciplineController();
                    var rs = Ext.Net.JSON.Deserialize<List<DisciplineModel>>(json);
                    foreach (var item in rs)
                    {
                        var obj = new hr_Discipline
                        {
                            Id = item.Id,
                            Note = item.Note,
                            LevelDisciplineId = item.LevelDisciplineId,
                            DecisionNumber = item.DecisionNumber,
                            FormDisciplineId = item.FormDisciplineId,
                            CreatedDate = DateTime.Now,
                            EditedDate = DateTime.Now,
                        };
                        if (item.DecisionDate != null)
                            obj.DecisionDate = item.DecisionDate;

                        obj.RecordId = RecordId;
                        if (obj.Id > 0)
                        {
                            controller.Update(obj);
                            storeDiscipline.CommitChanges();
                        }
                        else
                        {
                            controller.Insert(obj);
                            storeDiscipline.DataSource = DisciplineController.GetAll(RecordId);
                            storeDiscipline.DataBind();
                            storeDiscipline.CommitChanges();
                        }
                    }
                    GridDiscipline.Reload();
                }
               
            }
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }
        private void SaveGridWorkHistory(string json, int recordId)
        {
            try
            {
                if (!string.IsNullOrEmpty(json))
                {
                    var controller = new WorkHistoryController();
                    var rs = Ext.Net.JSON.Deserialize<List<WorkHistoryModel>>(json);

                    foreach (var item in rs)
                    {
                        var obj = new hr_WorkHistory
                        {
                            Id = item.Id,
                            Note = item.Note,
                            CreatedDate = DateTime.Now,
                            EditedDate = DateTime.Now,
                        };
                        if (item.FromDate != null)
                            obj.FromDate = item.FromDate;
                        if (item.ToDate != null)
                            obj.ToDate = item.ToDate;
                        obj.RecordId = recordId;
                        if (obj.Id > 0)
                        {
                            controller.UpdateWorkHistory(obj);
                            storeWorkHistory.CommitChanges();
                        }
                        else
                        {
                            controller.InsertWorkHistory(obj);
                            storeWorkHistory.DataSource = WorkHistoryController.GetAll(recordId);
                            storeWorkHistory.DataBind();
                            storeWorkHistory.CommitChanges();
                        }
                    }
                    GridWorkHistory.Reload();
                }                
            }
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }
        private void SaveGridTrainingHistory(string json, int RecordId)
        {
            try
            {
                if (!string.IsNullOrEmpty(json))
                {
                    var controller = new TrainingHistoryController();
                    var rs = Ext.Net.JSON.Deserialize<List<TrainingHistoryModel>>(json);
                   
                    foreach (var item in rs)
                    {
                        var obj = new hr_TrainingHistory()
                        {
                            Id = item.Id,
                            DecisionNumber = item.DecisionNumber,
                            TrainingName = item.TrainingName,
                            NationId = item.NationId,
                            TrainingSystemId = item.TrainingSystemId,
                            Reason = item.Reason,
                            TrainingPlace = item.TrainingPlace,
                            Note = item.Note,
                            CreatedDate = DateTime.Now,
                            EditedDate = DateTime.Now,
                        };
                        if (item.DecisionDate != null)
                            obj.DecisionDate = item.DecisionDate;
                        if (item.StartDate != null)
                            obj.StartDate = item.StartDate;
                        if (item.EndDate != null)
                            obj.EndDate = item.EndDate;
                        obj.RecordId = RecordId;
                       
                        if (obj.Id > 0)
                        {
                            controller.Update(obj);
                            storeTrainingHistory.CommitChanges();
                        }
                        else
                        {
                            controller.Insert(obj);
                            storeTrainingHistory.DataSource = TrainingHistoryController.GetAll(obj.RecordId);
                            storeTrainingHistory.DataBind();
                            storeTrainingHistory.CommitChanges();
                        }                   
                    }
                   
                    gridTrainingHistory.Reload();
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }
        private void SaveGridContract(string json, int RecordId)
        {
            try
            {
                if (!string.IsNullOrEmpty(json))
                {
                    var controller = new ContractController();
                    var rs = Ext.Net.JSON.Deserialize<List<ContractModel>>(json);
                    foreach (var item in rs)
                    {
                        var hopdongInfo = new hr_Contract
                        {
                            Id = item.Id,
                            ContractNumber = item.ContractNumber,
                            ContractStatusId = item.ContractStatusId,
                            ContractTypeId = item.ContractTypeId,
                            ContractDate = item.ContractDate,
                            ContractEndDate = item.ContractEndDate,
                            CreatedDate = DateTime.Now,
                            EditedDate = DateTime.Now,
                        };
                        hopdongInfo.RecordId = RecordId;
                        if (hopdongInfo.Id > 0)
                        {
                            controller.Update(hopdongInfo);
                            storeContract.CommitChanges();
                        }
                        else
                        {
                            controller.Insert(hopdongInfo);
                            storeContract.CommitChanges();
                        }
                    }
                    GridContract.Reload();
                }              
            }
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }

        #region OnRefreshData các ComboBox

        protected void btnSave_Click(object sender, DirectEventArgs e)
        {
            try
            {
                var model = new CatalogModel(hdfTableDM.Text)
                {
                    Name = txtTenDM.Text
                };
                CatalogController.Create(hdfTableDM.Text, model);
                RM.RegisterClientScriptBlock("add", "resetInputCategory();");
                grpCategory.Reload();
            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == ErrorNumber.DUPLICATE_PRIMARY_KEY)
                {
                    Dialog.ShowError("Mã danh mục đã bị trùng");
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowError("" + ex.Message);
            }

        }
        protected void btnSave_ClickGroup(object sender, DirectEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(cbxWdCategoryGroup.SelectedItem.Text))
                {
                    Dialog.ShowError("Bạn chưa chọn loại danh mục");
                    return;
                }
                var model = new CatalogModel (hdfTableDM.Text)
                {
                    Name = txtTenDMGroup.Text,
                    Group = cbxWdCategoryGroup.SelectedItem.Text
                };
                CatalogController.Create(hdfCurrentCatalogName.Text, model);
                RM.RegisterClientScriptBlock("add", "resetInputCategoryGroup();");
                grpCategoryGroup.Reload();
            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == ErrorNumber.DUPLICATE_PRIMARY_KEY)
                {
                    Dialog.ShowError("Mã danh mục đã bị trùng");
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowError("" + ex.Message);
            }

        }
        #endregion

        protected void btnAccept_Click(object sender, DirectEventArgs e)
        {
            try
            {
                var path1 = string.Empty;
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
                        path1 = hdfImageFolder.Text + "/" + Util.GetInstance().GetRandomString(10) + fufUploadControl.FileName;
                        file.SaveAs(path);
                        File.Move(path, Server.MapPath(path1));
                        hdfAnhDaiDien.Text = path1;
                    }
                    catch (Exception ex)
                    {
                        Dialog.ShowError(ex.Message);
                    }
                }
                wdUploadImageWindow.Hide();

                //Hiển thị lại ảnh sau khi đã cập nhật xong
                img_anhdaidien.ImageUrl = path1;
            }
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }

        public string GetNameFile(string path)
        {
            string[] str = path.Split('/');
            return str[str.Length - 1].ToString();
        }

        protected void btnEdit_Click()
        {
            try
            {
                hdfPrKeyHoSo.Text = Request.QueryString["PrKeyHoSo"];
                var hs = RecordController.GetById(Convert.ToInt32(hdfPrKeyHoSo.Text));
                var hsl = SalaryDecisionController.GetCurrent(Convert.ToInt32(hdfPrKeyHoSo.Text));
                var util = new Util();
                if (hs != null)
                {
                    // Ho so nhan su
                    if (!string.IsNullOrEmpty(hs.ImageUrl))
                        img_anhdaidien.ImageUrl = hs.ImageUrl;
                    txtEmployeeCode.Text = hs.EmployeeCode;
                    txtFullName.Text = hs.FullName;
                    txtAlias.Text = hs.Alias;
                    cboEmployeeType.Text = hs.EmployeeTypeName;
                    hdfEmployeeTypeId.Text = hs.EmployeeTypeId.ToString();
                    txtManagementDepartment.Text = hs.ManagementDepartmentName;
                    hdfManagementDepartmentId.Text = hs.ManagementDepartmentId.ToString();
                    cboDepartment.Text = hs.DepartmentName;
                    hdfDepartmentId.Text = hs.DepartmentId.ToString();
                    dfBirthDate.SetValue(hs.BirthDateVn);
                    cbxSex.Text = hs.Sex ? "M" : "F";

                    cboMaritalStatus.Text = hs.MaritalStatusName;
                    hdfMaritalStatusId.Text = hs.MaritalStatusId.ToString();
                    cbxPersonalClass.Text = hs.PersonalClassName;
                    hdfPersonalClassId.Text = hs.PersonalClassId.ToString();
                    cbxFamilyClass.Text = hs.FamilyClassName;
                    hdfFamilyClassId.Text = hs.FamilyClassId.ToString();
                    txtCellPhoneNumber.Text = hs.CellPhoneNumber;
                    txtHomePhoneNumber.Text = hs.HomePhoneNumber;
                    txtWorkPhoneNumber.Text = hs.WorkPhoneNumber;
                    txtWorkStatusReason.Text = hs.WorkStatusReason;
                    cbxFolk.Text = hs.FolkName;
                    hdfFolkId.Text = hs.FolkId.ToString();
                    cbxReligion.Text = hs.ReligionName;
                    hdfReligionId.Text = hs.ReligionId.ToString();
                    FunctionaryDate.SetValue(hs.FunctionaryDate);
                    txt_ResidentPlace.Text = hs.ResidentPlace;
                    txtCPVCardNumber.Text = hs.CPVCardNumber;
                    txtPersonalTaxCode.Text = hs.PersonalTaxCode;
                    txtWorkEmail.Text = hs.WorkEmail;
                    txtPersonalEmail.Text = hs.PersonalEmail;
                    txt_Address.Text = hs.Address;
                    txtPreviousJob.Text = hs.PreviousJob;
                    RecruimentDate.SetValue(hs.RecruimentDate);
                    txtRecruitmentDepartment.Text = hs.RecruimentDepartment;
                    cbxPosition.Text = hs.PositionName;
                    hdfPositionId.Text = hs.PositionId.ToString();
                    txtAssignedWork.Text = hs.AssignedWork;
                    WorkStatusDate.SetValue(hs.WorkStatusDate);
                    if (hsl != null)
                    {
                        //txtQuantumName.Text = hsl.QuantumName;
                        //txtQuantumCode.Text = hsl.QuantumCode;
                        //txtSalaryGrade.Text = "Bậc " + hsl.SalaryGrade == null ? "" : hsl.SalaryGrade;
                        //txtSalaryFactor.Text = hsl.SalaryFactor.ToString();
                        QuantumEffectiveDate.SetValue(hsl.EffectiveDate);
                        //txtPositionAllowance.Text = hsl.PositionAllowance.ToString();
                        //txtOtherAllowance.Text = hsl.PositionAllowance.ToString();
                    }
                    cbxBasicEducation.Text = hs.BasicEducationName;
                    hdfBasicEducationId.Text = hs.BasicEducationId.ToString();
                    cbxEducation.Text = hs.EducationName;
                    hdfEducationId.Text = hs.EducationId.ToString();
                    cbxPoliticLevel.Text = hs.PoliticLevelName;
                    hdfPoliticLevelId.Text = hs.PoliticLevelId.ToString();
                    cbxManagementLevel.Text = hs.ManagementLevelName;
                    hdfManagementLevelId.Text = hs.ManagementLevelId.ToString();
                    cbxJobTitle.Text = hs.JobTitleName;
                    hdfJobTitleId.Text = hs.JobTitleId.ToString();
                    cbxGovernmentPosition.Text = hs.GovernmentPositionName;
                    hdfGovernmentPositionId.Text = hs.GovernmentPositionId.ToString();
                    cbxPluralityPosition.Text = hs.PluralityPositionName;
                    hdfPluralityPositionId.Text = hs.PluralityPositionId.ToString();
                    cbxWorkStatus.Text = hs.WorkStatusName;
                    hdfWorkStatusId.Text = hs.WorkStatusId.ToString();
                    cbxBirthPlaceProvince.Text = hs.BirthPlaceProvinceName;
                    cbxBirthPlaceDistrict.Text = hs.BirthPlaceDistrictName;
                    cbxBirthPlaceWard.Text = hs.BirthPlaceWardName;
                    cbxHometownProvince.Text = hs.HometownProvinceName;
                    cbxHometownDistrict.Text = hs.HometownDistrictName;
                    cbxHometownWard.Text = hs.HometownWardName;
                    hdfBirthPlaceProvinceId.Text = hs.BirthPlaceProvinceId.ToString();
                    hdfBirthPlaceDistrictId.Text = hs.BirthPlaceDistrictId.ToString();
                    hdfBirthPlaceWardId.Text = hs.BirthPlaceWardId.ToString();
                    hdfHometownProvinceId.Text = hs.HometownProvinceId.ToString();
                    hdfHometownDistrictId.Text = hs.HometownDistrictId.ToString();
                    hdfHometownWardId.Text = hs.HometownWardId.ToString();
                    if (!string.IsNullOrEmpty(hdfBirthPlaceDistrictId.Text))
                        cbxBirthPlaceDistrict.Disabled = false;
                    if (!string.IsNullOrEmpty(hdfBirthPlaceWardId.Text))
                        cbxBirthPlaceWard.Disabled = false;
                    if (!string.IsNullOrEmpty(hdfHometownDistrictId.Text))
                        cbxHometownDistrict.Disabled = false;
                    if (!string.IsNullOrEmpty(hdfHometownWardId.Text))
                        cbxHometownWard.Disabled = false;
                    cbxITLevel.Text = hs.ITLevelName;
                    hdfITLevelId.Text = hs.ITLevelId.ToString();
                    cbxLanguageLevel.Text = hs.LanguageLevelName;
                    hdfLanguageLevelId.Text = hs.LanguageLevelId.ToString();
                    cbxCPVPosition.Text = hs.CPVPositionName;
                    hdfCPVPositionId.Text = hs.CPVPositionId.ToString();
                    cbxVYUPosition.Text = hs.VYUPositionName;
                    hdfVYUPositionId.Text = hs.VYUPositionId.ToString();
                    cbxArmyLevel.Text = hs.ArmyLevelName;
                    hdfArmyLevelId.Text = hs.ArmyLevelId.ToString();
                    ArmyJoinedDate.SetValue(hs.ArmyJoinedDate);
                    ArmyLeftDate.SetValue(hs.ArmyLeftDate);
                    cbxHealthStatus.Text = hs.HealthStatusName;
                    hdfHealthStatusId.Text = hs.HealthStatusId.ToString();
                    cbxFamilyPolicy.Text = hs.FamilyPolicyName;
                    hdfFamilyPolicyId.Text = hs.FamilyPolicyId.ToString();
                    cbxIDIssuePlace.Text = hs.IDIssuePlaceName;
                    hdfIDIssuePlaceId.Text = hs.IDIssuePlaceId.ToString();
                    CPVJoinedDate.SetValue(hs.CPVJoinedDate);
                    CPVOfficialJoinedDate.SetValue(hs.CPVOfficialJoinedDate);
                    txtCPVJoinedPlace.Text = hs.CPVJoinedPlace;
                    VYUJoinedDate.SetValue(hs.VYUJoinedDate);
                    txtTitleAwarded.Text = hs.TitleAwarded;
                    cbxBloodGroup.SetValue(hs.BloodGroup);
                    txtHeight.SetValue(hs.Height);
                    txtWeight.SetValue(hs.Weight);
                    txtRankWounded.Text = hs.RankWounded;
                    txtIDNumber.SetValue(hs.IDNumber);
                    IDIssueDate.SetValue(hs.IDIssueDate);
                    txtInsuranceNumber.Text = hs.InsuranceNumber;
                    InsuranceIssueDate.SetValue(hs.InsuranceIssueDate);
                    txtBiography.Text = hs.Biography;
                    txtForeignOrganizationJoined.Text = hs.ForeignOrganizationJoined;
                    txtRelativesAboard.Text = hs.RelativesAboard;
                    txtReview.Text = hs.Review;
                    txtFamilyIncome.Text = hs.FamilyIncome.ToString();
                    txtOtherIncome.Text = hs.OtherIncome;
                    txtAllocatedHouse.Text = hs.AllocatedHouse;
                    txtAllocatedHouseArea.Text = hs.AllocatedtHouseArea.ToString();
                    txtHouse.Text = hs.House;
                    txtHouseArea.Text = hs.HouseArea.ToString();
                    txtAllocatedLandArea.Text = hs.AllocatedLandArea.ToString();
                    txtLandArea.Text = hs.LandArea.ToString();
                    txtBusinessLand.Text = hs.BusinessLandArea.ToString();
                    txtSkills.Text = hs.Skills;
                    txtTimeKeepingCode.Text = hs.TimeKeepingCode;
                    txtContactAddress.Text = hs.ContactAddress;
                    txtContactPersonName.Text = hs.ContactPersonName;
                    txtContactPhoneNumber.Text = hs.ContactPhoneNumber;
                    txtContactRelation.Text = hs.ContactRelation;
                    cbxInputIndustry.Text = hs.IndustryName;
                    hdfInputIndustryId.Text = hs.IndustryId.ToString();
                    hdfAnhDaiDien.Text = hs.ImageUrl;
                    RevolutionJoinDate.SetValue(hs.RevolutionJoinedDate);
                    txtLongestJob.Text = hs.LongestJob;
                    if (hs.OriginalFile != null)
                    {
                        fufTepTinDinhKem.Text = GetNameFile(hs.OriginalFile);
                    }
                    else
                    {
                        fufTepTinDinhKem.Text = @"Chọn tệp tin";
                    }
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message.ToString());
            }
        }
        #region Diễn biến lương
        protected void StoreSalary_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            if (!string.IsNullOrEmpty(hdfPrKeyHoSo.Text))
            {
                StoreSalary.DataSource = SalaryDecisionController.GetCurrent(Convert.ToInt32(hdfPrKeyHoSo.Text));
                StoreSalary.DataBind();
            }
        }

        protected void HandleChangesSalary(object sender, BeforeStoreChangedEventArgs e)
        {
            try
            {
                var temp = e.DataHandler.ObjectData<SalaryDecisionModel>();
                foreach (var item in temp.Created)
                {
                    var info = new sal_SalaryDecision()
                    {
                        Id = item.Id,
                        RecordId = item.RecordId,
                        DecisionNumber = item.DecisionNumber,
                        QuantumId = item.QuantumId,
                        DecisionDate = item.DecisionDate,
                        EffectiveDate = item.EffectiveDate,
                        Note = item.Note,
                        CreatedDate = DateTime.Now,
                        EditedDate = DateTime.Now,
                    };

                    SalaryDecisionController.Create(new SalaryDecisionModel(info));
                }
                foreach (var item in temp.Updated)
                {
                    var info = new sal_SalaryDecision()
                    {     
                        DecisionNumber = item.DecisionNumber,
                        QuantumId = item.QuantumId,
                        DecisionDate = item.DecisionDate,
                        EffectiveDate = item.EffectiveDate,
                        Note = item.Note,
                        EditedDate = DateTime.Now,
                    };
                    SalaryDecisionController.Update(new SalaryDecisionModel(info));
                }
                foreach (var item in temp.Deleted)
                {
                    SalaryDecisionController.Delete(item.Id);
                }
                StoreSalary.CommitChanges();
            }
            catch (Exception ex)
            {
                Dialog.ShowError("" + ex.Message);
            }
            GridPanelSalary.Reload();
        }
        #endregion

        protected void storeWorkHistory_BeforeStoreChanged(object sender, BeforeStoreChangedEventArgs e)
        {
            try
            {
                var temp = e.DataHandler.ObjectData<WorkHistoryModel>();
                foreach (var item in temp.Created)
                {
                    var info = new hr_WorkHistory();
                    info.RecordId = Convert.ToInt32(hdfPrKeyHoSo.Text);
                    if (item.FromDate != null)
                        info.FromDate = item.FromDate;
                    if (item.ToDate != null)
                        info.ToDate = item.ToDate;
                    info.Note = item.Note;
                    new WorkHistoryController().InsertWorkHistory(info);                  
                }
                foreach (var item in temp.Updated)
                {
                    var info = new hr_WorkHistory();
                    info.RecordId = Convert.ToInt32(hdfPrKeyHoSo.Text);
                    if (item.FromDate != null)
                        info.FromDate = item.FromDate;
                    if (item.ToDate != null)
                        info.ToDate = item.ToDate;
                    info.Note = item.Note;
                    new WorkHistoryController().UpdateWorkHistory(info);                 
                }
                foreach (var item in temp.Deleted)
                {
                    new WorkHistoryController().DeleteWorkHistory(item.Id);                    
                }
                storeWorkHistory.CommitChanges();
            }
            catch (Exception ex)
            {
                Dialog.ShowError("" + ex.Message);
            }

            GridWorkHistory.Reload();
        }
        #region quá trình công tác
        protected void StoreWorkProcess_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            if (!string.IsNullOrEmpty(hdfPrKeyHoSo.Text))
            {
                StoreWorkProcess.DataSource = WorkProcessController.GetAll(Convert.ToInt32(hdfPrKeyHoSo.Text));
                StoreWorkProcess.DataBind();
            }
        }
        protected void HandleChangesWorkProcess(object sender, BeforeStoreChangedEventArgs e)
        {
            try
            {
                var work = e.DataHandler.ObjectData<WorkProcessModel>();
                // insert
                foreach (var created in work.Created)
                {
                        var info = new hr_WorkProcess();
                        info.RecordId = Convert.ToInt32(hdfPrKeyHoSo.Text);

                        info.Note = created.Note;
                        new WorkProcessController().Insert(info);
                }
                // update
                foreach (var updated in work.Updated)
                {
                        var info = new hr_WorkProcess();
                        if (updated.EffectiveDate != null)
                            info.EffectiveDate = updated.EffectiveDate;
                        if (updated.EffectiveEndDate != null)
                            info.EffectiveEndDate = updated.EffectiveEndDate;
                        else info.EffectiveEndDate = DateTime.Now;
                        info.Note = updated.Note;
                        new WorkProcessController().Update(info);
                }
                // delete
                foreach (var deleted in work.Deleted)
                {
                    new WorkProcessController().Delete(deleted.Id);
                }
                StoreWorkProcess.CommitChanges();
                GridWorkProcess.Reload();
            }
            catch (Exception ex)
            {
                Dialog.ShowError("Có lỗi xảy ra: " + ex.Message);
            }
        }
        #endregion
        #region Quan hệ gia đình
        protected void StoreFamilyRelationship_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            if (!string.IsNullOrEmpty(hdfPrKeyHoSo.Text))
            {
                storeFamilyRelationship.DataSource = FamilyRelationshipController.GetAll(Convert.ToInt32(hdfPrKeyHoSo.Text));
                storeFamilyRelationship.DataBind();
            }
        }
        protected void HandleChangesQuanHeGiaDinh(object sender, BeforeStoreChangedEventArgs e)
        {
            try
            {
                var giadinh = e.DataHandler.ObjectData<FamilyRelationshipModel>();
                // insert
                foreach (var created in giadinh.Created)
                {
                    var info = new hr_FamilyRelationship();
                    info.RecordId = Convert.ToInt32(hdfPrKeyHoSo.Text);
                    info.FullName = created.FullName;
                    info.BirthYear = created.BirthYear;
                    info.RelationshipId = created.RelationshipId;
                    info.Occupation = created.Occupation;
                    info.WorkPlace = created.WorkPlace;
                    info.Sex = created.Sex;
                    info.Note = created.Note;
                    info.IsDependent = created.IsDependent;
                    info.IDNumber = created.IDNumber;
                    new FamilyRelationshipController().Insert(info);
                }
                // update
                foreach (var updated in giadinh.Updated)
                {
                    var info = new hr_FamilyRelationship();
                    info.FullName = updated.FullName;
                    info.BirthYear = updated.BirthYear;
                    info.RelationshipId = updated.RelationshipId;
                    info.Occupation = updated.Occupation;
                    info.WorkPlace = updated.WorkPlace;
                    info.Sex = updated.Sex;
                    info.Note = updated.Note;
                    info.IsDependent = updated.IsDependent;
                    info.IDNumber = updated.IDNumber;
                    new FamilyRelationshipController().Update(info);
                }
                // delete
                foreach (var deleted in giadinh.Deleted)
                {
                    new FamilyRelationshipController().Delete(deleted.Id);
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowError("Có lỗi xảy ra: " + ex.Message);
            }
        }
        #endregion

        protected void StoreEducation_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            if (!string.IsNullOrEmpty(hdfPrKeyHoSo.Text))
            {
                StoreEducation.DataSource = EducationHistoryController.GetAll(Convert.ToInt32(hdfPrKeyHoSo.Text));
                StoreEducation.DataBind();
            }           
        }

        #region Khả năng
        protected void storeAbility_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            if (!string.IsNullOrEmpty(hdfPrKeyHoSo.Text))
            {
                storeAbility.DataSource = AbilityController.GetAll(Convert.ToInt32(hdfPrKeyHoSo.Text));
                storeAbility.DataBind();
            }
        }
        protected void HandleChangesAbility(object sender, BeforeStoreChangedEventArgs e)
        {
            try
            {
                var ability = e.DataHandler.ObjectData<AbilityModel>();
                foreach (var created in ability.Created)
                {
                    var info = new hr_Ability();
                    info.RecordId = Convert.ToInt32(hdfPrKeyHoSo.Text);
                    info.Note = created.Note;
                    info.AbilityId = created.AbilityId;
                    info.GraduationTypeId = created.GraduationTypeId;
                    new AbilityController().Insert(info);    
                }
                // update
                foreach (var updated in ability.Updated)
                {
                    var info = new hr_Ability();
                    info.Note = updated.Note;
                    info.AbilityId = updated.AbilityId;
                    info.GraduationTypeId = updated.GraduationTypeId;
                    new AbilityController().Update(info);
                }
                // delete
                foreach (var deleted in ability.Deleted)
                {
                    new AbilityController().Delete(deleted.Id);
                }
            }
            catch (Exception ex) { Dialog.ShowError("Lỗi xảy ra " + ex.Message); }
        }
        #endregion
        #region Hợp đồng lao động
        protected void storeContract_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            if (!string.IsNullOrEmpty(hdfPrKeyHoSo.Text))
            {
                storeContract.DataSource = ContractController.GetAll(Convert.ToInt32(hdfPrKeyHoSo.Text));
                storeContract.DataBind();
            }
        }

        protected void HandleChangesContract_BeforeStoreChanged(object sender, BeforeStoreChangedEventArgs e)
        {
            try
            {
                var hopdong = e.DataHandler.ObjectData<ContractModel>();
                foreach (var created in hopdong.Created)
                {
                    var info = new hr_Contract();
                    info.RecordId = Convert.ToInt32(hdfPrKeyHoSo.Text);
                    info.ContractNumber = created.ContractNumber;
                    info.ContractTypeId = created.ContractTypeId;
                    info.ContractStatusId = created.ContractStatusId;
                    if (created.ContractDate != null)
                        info.ContractDate = created.ContractDate;
                    if (created.ContractEndDate != null)
                        info.ContractEndDate = created.ContractEndDate;
                    new ContractController().Insert(info);
                }
                // update
                foreach (var updated in hopdong.Updated)
                {
                    var info = new hr_Contract();
                    info.ContractNumber = updated.ContractNumber;
                    info.ContractTypeId = updated.ContractTypeId;
                    info.ContractStatusId = updated.ContractStatusId;
                    if (updated.ContractDate != null)
                        info.ContractDate = updated.ContractDate;
                    if (updated.ContractEndDate != null)
                        info.ContractEndDate = updated.ContractEndDate;
                    new ContractController().Update(info);
                }
                // delete
                foreach (var deleted in hopdong.Deleted)
                {
                    new ContractController().Delete(deleted.Id);
                }
                storeContract.CommitChanges();
                GridContract.Reload();
            }
            catch (Exception ex) { Dialog.ShowError("Lỗi xảy ra " + ex.Message); }
        }
        #endregion
        #region Bảo hiểm
        protected void storeInsurance_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            if (!string.IsNullOrEmpty(hdfPrKeyHoSo.Text))
            {
                storeInsurance.DataSource = InsuranceController.GetAll(Convert.ToInt32(hdfPrKeyHoSo.Text));
                storeInsurance.DataBind();
            }
        }
        protected void HandlerChangesInsurance(object sender, BeforeStoreChangedEventArgs e)
        {
            try
            {
                var insurance = e.DataHandler.ObjectData<InsuranceModel>();
                // insert
                foreach (var created in insurance.Created)
                {
                    var info = new hr_Insurance();
                    info.RecordId = Convert.ToInt32(hdfPrKeyHoSo.Text);
                    info.FromDate = created.FromDate; 
                    info.ToDate = created.ToDate;
                    info.PositionId = created.PositionId;
                    info.SalaryFactor = created.SalaryFactor;
                    info.SalaryLevel = created.SalaryLevel;
                    info.Allowance = created.Allowance;
                    info.Note = created.Note;
                    new InsuranceController().Insert(info);
                }
                // update
                foreach (var updated in insurance.Updated)
                {
                    var info = new hr_Insurance();
                    info.FromDate = updated.FromDate;
                    info.ToDate = updated.ToDate;
                    info.PositionId = updated.PositionId;
                    info.SalaryFactor = updated.SalaryFactor;
                    info.SalaryLevel = updated.SalaryLevel;
                    info.Allowance = updated.Allowance;
                    info.Note = updated.Note;
                    new InsuranceController().Update(info);
                }
                // delete
                foreach (var deleted in insurance.Deleted)
                {
                    new InsuranceController().Delete(deleted.Id);
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowError("" + ex.Message);
            }
        }

        #endregion
        #region Khen thưởng kỷ luật
        protected void storeReward_RefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            if (!string.IsNullOrEmpty(hdfPrKeyHoSo.Text))
            {
                storeReward.DataSource = RewardController.GetAll(Convert.ToInt32(hdfPrKeyHoSo.Text));
                storeReward.DataBind();
            }
        }
        protected void storeDiscipline_RefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            if (!string.IsNullOrEmpty(hdfPrKeyHoSo.Text))
            {
                storeDiscipline.DataSource = DisciplineController.GetAll(Convert.ToInt32(hdfPrKeyHoSo.Text));
                storeDiscipline.DataBind();
            }
        }
        protected void storeReward_BeforeStoreChanged(object sender, BeforeStoreChangedEventArgs e)
        {
            try
            {
                var reward = e.DataHandler.ObjectData<RewardModel>();
                foreach (var created in reward.Created)
                {
                    var info = new hr_Reward();
                    info.RecordId = Convert.ToInt32(hdfPrKeyHoSo.Text);
                    info.Note = created.Note;
                    info.LevelRewardId = created.LevelRewardId;
                    info.DecisionNumber = created.DecisionNumber;
                    info.DecisionDate = created.DecisionDate;
                    new RewardController().Insert(info);                   
                }
                foreach (var updated in reward.Updated)
                {
                    var info = new hr_Reward();
                  
                    new RewardController().Update(info);

                }
                foreach (var delete in reward.Deleted)
                {
                    new RewardController().Delete(delete.Id);
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowError("" + ex.Message);
            }
        }
        protected void storeDiscipline_BeforeStoreChanged(object sender, BeforeStoreChangedEventArgs e)
        {
            try
            {
                var discipline = e.DataHandler.ObjectData<DisciplineModel>();
                foreach (var created in discipline.Created)
                {
                    var info = new hr_Discipline();
                    info.RecordId = Convert.ToInt32(hdfPrKeyHoSo.Text);
                    info.LevelDisciplineId = created.LevelDisciplineId;
                    info.FormDisciplineId = created.FormDisciplineId;
                    info.Note = created.Note;
                    info.DecisionNumber = created.DecisionNumber;
                    info.DecisionDate = created.DecisionDate;
                        
                    new DisciplineController().Insert(info);
                }
                foreach (var updated in discipline.Updated)
                {
                    var info = new hr_Discipline();
                    info.LevelDisciplineId = updated.LevelDisciplineId;
                    info.FormDisciplineId = updated.FormDisciplineId;
                    info.Note = updated.Note;
                    info.DecisionNumber = updated.DecisionNumber;
                    info.DecisionDate = updated.DecisionDate;
                    new DisciplineController().Update(info);
                }
                foreach (var delete in discipline.Deleted)
                {
                    new DisciplineController().Delete(delete.Id);
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowError("" + ex.Message);
            }
        }
        #endregion

        protected void storeTrainingHistory_RefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            if (!string.IsNullOrEmpty(hdfPrKeyHoSo.Text))
            {
                storeTrainingHistory.DataSource = TrainingHistoryController.GetAll(Convert.ToInt32(hdfPrKeyHoSo.Text));
                storeTrainingHistory.DataBind();
            }
        }

        protected void HandleChangesTrainingHistory(object sender, BeforeStoreChangedEventArgs e)
        {
            try
            {
                var work = e.DataHandler.ObjectData<TrainingHistoryModel>();
                // insert
                foreach (var created in work.Created)
                {
                    var info = new hr_TrainingHistory() { 
                        RecordId = Convert.ToInt32(hdfPrKeyHoSo.Text),
                        Id = created.Id,
                        DecisionNumber = created.DecisionNumber,
                        TrainingName = created.TrainingName,
                        NationId = created.NationId,
                        TrainingSystemId = created.TrainingSystemId,
                        Reason = created.Reason,
                        TrainingPlace = created.TrainingPlace,
                        Note = created.Note,
                        CreatedDate = DateTime.Now,
                        EditedDate = DateTime.Now,
                        DecisionDate = created.DecisionDate,
                        StartDate = created.StartDate,
                        EndDate = created.EndDate,
                    };
                    new TrainingHistoryController().Insert(info);
                }
                // update
                foreach (var updated in work.Updated)
                {
                    var info = new hr_TrainingHistory()
                    {
                        RecordId = Convert.ToInt32(hdfPrKeyHoSo.Text),
                        Id = updated.Id,
                        DecisionNumber = updated.DecisionNumber,
                        TrainingName = updated.TrainingName,
                        NationId = updated.NationId,
                        TrainingSystemId = updated.TrainingSystemId,
                        Reason = updated.Reason,
                        TrainingPlace = updated.TrainingPlace,
                        Note = updated.Note,
                        CreatedDate = DateTime.Now,
                        EditedDate = DateTime.Now,
                        DecisionDate = updated.DecisionDate,
                        StartDate = updated.StartDate,
                        EndDate = updated.EndDate,
                    };
                    new TrainingHistoryController().Update(info);
                }
                // delete
                foreach (var deleted in work.Deleted)
                {
                    new TrainingHistoryController().Delete(deleted.Id);
                }
                storeTrainingHistory.CommitChanges();
                gridTrainingHistory.Reload();

            }
            catch (Exception ex)
            {
                Dialog.ShowError("Có lỗi xảy ra: " + ex.Message);
            }
        }

        protected void StoreEducation_BeforeStoreChanged(object sender, BeforeStoreChangedEventArgs e)
        {
            try
            {
                var education = e.DataHandler.ObjectData<EducationHistoryModel>();
                foreach (var created in education.Created)
                {
                    var info = new hr_EducationHistory()
                    {
                        RecordId = Convert.ToInt32(hdfPrKeyHoSo.Text),
                        NationId = created.NationId,
                        UniversityId = created.UniversityId,
                        IndustryId = created.IndustryId,
                        TrainingSystemId = created.TrainingSystemId,
                        EducationId = created.EducationId,                      
                    };
                    if (created.FromDate != null)
                        info.FromDate = created.FromDate;
                    if (created.ToDate != null)
                        info.ToDate = created.ToDate;
                    else info.ToDate = DateTime.Now;
                    // insert
                    new EducationHistoryController().InsertEducation(info);
                }
                foreach (var update in education.Updated)
                {
                    var info = new hr_EducationHistory()
                    {
                        NationId = update.NationId,
                        UniversityId = update.UniversityId,
                        IndustryId = update.IndustryId,
                        TrainingSystemId = update.TrainingSystemId,
                        EducationId = update.EducationId,
                    };
                    if (update.FromDate != null)
                        info.FromDate = update.FromDate;
                    if (update.ToDate != null)
                        info.ToDate = update.ToDate;
               
                    // update
                    new EducationHistoryController().UpdateEducation(info);                                     
                }
                foreach (var deleted in education.Deleted)
                {
                    new EducationHistoryController().DeleteEducationHistory(deleted.Id);
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowError("" + ex.Message);
            }
        }

        protected void storeWorkHistory_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(hdfPrKeyHoSo.Text))
                {
                    storeWorkHistory.DataSource = WorkHistoryController.GetAll(Convert.ToInt32(hdfPrKeyHoSo.Text));
                    storeWorkHistory.DataBind();
                }              
            }
            catch (Exception ex)
            {
                Dialog.ShowError("" + ex.Message);
            }
        }

        #region Event Methods
        #endregion

        #region Direct Methods

        [DirectMethod]
        public void ChangeDepartment(int id)
        {
            // init parent name
            var managementDepartmentName = "";
            // find current department
            var currentDepartment = CurrentUser.Departments.FirstOrDefault(d => d.Id == id);
            // check current department
            if (currentDepartment != null)
            {
                var parentDepartment = CurrentUser.Departments.FirstOrDefault(d => d.Id == currentDepartment.ParentId);
                while (parentDepartment != null)
                {
                    // set parent department name
                    managementDepartmentName = parentDepartment.Name;
                    // set parent department id
                    hdfManagementDepartmentId.Text = parentDepartment.Id.ToString();
                    // check parent department type
                    if (parentDepartment.Type == DepartmentType.Organization)
                        // is organization, break
                        break;
                    // is board or department, move up
                    parentDepartment = CurrentUser.Departments.FirstOrDefault(d => d.Id == parentDepartment.ParentId);
                }
                // set display text field
                txtManagementDepartment.SetValue(managementDepartmentName);
            }
        }

        #endregion


        
    }

}

