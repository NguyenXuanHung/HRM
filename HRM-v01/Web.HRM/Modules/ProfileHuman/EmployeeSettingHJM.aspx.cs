using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using Ext.Net;
using SoftCore;
using Web.Core;
using Web.Core.Framework;
using Web.Core.Framework.Common;
using Web.Core.Framework.Controller;
using Web.Core.Object.Catalog;
using Web.Core.Object.HumanRecord;
using Web.Core.Object.Salary;
using Web.Core.Object.Sample;
using Web.Core.Object.Security;
using Web.Core.Service.Catalog;
using Web.Core.Service.Sample;

namespace Web.HRM.Modules.ProfileHuman
{
    public partial class EmployeeSettingHJM : BasePage
    {
        private const string GenerateEmployeeConst = "00000000000000000000";
        private const string PositionAllowance = "PhuCapChucVu";
        private const string OtherAllowance = "PhuCapKhac";


        #region Event Methods

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
            hdfTime1.Text = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            EditdfNgayBatDau.Text = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            EditdfNgayKetThuc.Text = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            dfNgayQDKT.Text = DateTime.Now.ToString(CultureInfo.InvariantCulture);

            ucSample1.AfterClickAcceptButton += ucSample1_AfterClickAcceptButton;
        }

        #region OnClick

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                        if (item is ComboBox cb)
                        {
                            info.Control = cb.ID;
                            info.Value = cb.SelectedItem.Text;
                        }
                        info.DataType = "Ext.Net.ComboBox";
                        break;
                    case "Ext.Net.Hidden":
                        if (item is Hidden hdf)
                        {
                            info.Control = hdf.ID;
                            info.Value = hdf.Text;
                        }
                        info.DataType = "Ext.Net.Hidden";
                        break;
                    case "Ext.Net.TextArea":
                        if (item is TextArea txta)
                        {
                            info.Control = txta.ID;
                            info.Value = txta.Text;
                        }
                        info.DataType = "Ext.Net.TextArea";
                        break;
                    case "Ext.Net.TextField":
                        if (item is TextField txtf)
                        {
                            info.Control = txtf.ID;
                            info.Value = txtf.Text;
                        }
                        info.DataType = "Ext.Net.TextField";
                        break;
                    case "Ext.Net.NumberField":
                        if (item is NumberField mbf)
                        {
                            info.Control = mbf.ID;
                            info.Value = mbf.Text;
                        }
                        info.DataType = "Ext.Net.NumberField";
                        break;
                    case "Ext.Net.DateField":
                        if (item is DateField df)
                        {
                            info.Control = df.ID;
                            info.Value = df.SelectedDate.ToString("yyyy/MM/dd");
                        }
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
            try
            {
                foreach (var department in arrDepartment)
                {
                    if (!string.IsNullOrEmpty(department))
                    {
                        sl.DepartmentId = Convert.ToInt32(department);
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
            RM.RegisterClientScriptBlock("hideCreateSample", "hideCreateSample();");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQDLDownload_Click(object sender, DirectEventArgs e)
        {
            if (string.IsNullOrEmpty(hdfPrKeyHoSo.Text)) return;
            var hs = RecordController.GetById(Convert.ToInt32(hdfPrKeyHoSo.Text));
            DownloadAttachFile(hs.OriginalFile);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnFileLDelete_Click(object sender, DirectEventArgs e)
        {
            if (string.IsNullOrEmpty(hdfPrKeyHoSo.Text)) return;
            var hs = RecordController.GetById(Convert.ToInt32(hdfPrKeyHoSo.Text));
            var serverPath = Server.MapPath(hs.OriginalFile);
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

        /// <summary>
        /// Lưu dữ liệu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void iBtnSave_Click(object sender, DirectEventArgs e)
        {
            try
            {
                var util = new Util();
                var hs = new RecordModel();
                if (!string.IsNullOrEmpty(hdfPrKeyHoSo.Text))
                {
                    // check if Employee Code exists
                    hs.EmployeeCode = txtEmployeeCode.Text;
                    if (!string.IsNullOrEmpty(txtEmployeeCode.Text.Trim()))
                    {
                        var recordList = RecordController.CheckExistIDNumberAndEmployeeCode(null, null, txtEmployeeCode.Text);
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
                        var recordList = RecordController.CheckExistIDNumberAndEmployeeCode(null, null, txtEmployeeCode.Text);
                        if (recordList != null && recordList.Count > 0)
                        {
                            Dialog.ShowError("Mã cán bộ đã tồn tại. Vui lòng nhập mã cán bộ khác.");
                            return;
                        }
                    }
                    // check if ID number exists
                    if (!string.IsNullOrEmpty(txtIDNumber.Text.Trim()))
                    {
                        var recordList = RecordController.CheckExistIDNumberAndEmployeeCode(null, txtIDNumber.Text, null);
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
                hs.Name = position == -1 ? hs.FullName : hs.FullName.Substring(position + 1).Trim();
                // 2) Tên gọi khác
                hs.Alias = txtAlias.Text;
                // 3) Ngày sinh
                if (!util.IsDateNull(dfBirthDate.SelectedDate))
                    hs.BirthDate = dfBirthDate.SelectedDate;
                // Giới tính
                hs.Sex = !string.IsNullOrEmpty(cbxSex.SelectedItem.Value) && cbxSex.SelectedItem.Value == "M";
                // Tình trạng hôn nhân
                if (!string.IsNullOrEmpty(hdfMaritalStatusId.Text))
                    hs.MaritalStatusId = Convert.ToInt32(hdfMaritalStatusId.Text);
                // 4) Nơi sinh
                if (!string.IsNullOrEmpty(hdfBirthPlaceProvinceId.Text))
                    hs.BirthPlaceProvinceId = Convert.ToInt32(hdfBirthPlaceProvinceId.Text);
                if (!string.IsNullOrEmpty(hdfBirthPlaceDistrictId.Text))
                    hs.BirthPlaceDistrictId = Convert.ToInt32(hdfBirthPlaceDistrictId.Text);
                if (!string.IsNullOrEmpty(hdfBirthPlaceWardId.Text))
                    hs.BirthPlaceWardId = Convert.ToInt32(hdfBirthPlaceWardId.Text);
                // 5) Quê quán
                if (!string.IsNullOrEmpty(hdfHometownProvinceId.Text))
                    hs.HometownProvinceId = Convert.ToInt32(hdfHometownProvinceId.Text);
                if (!string.IsNullOrEmpty(hdfHometownDistrictId.Text))
                    hs.HometownDistrictId = Convert.ToInt32(hdfHometownDistrictId.Text);
                if (!string.IsNullOrEmpty(hdfHometownWardId.Text))
                    hs.HometownWardId = Convert.ToInt32(hdfHometownWardId.Text);
                // 6) Dân tộc
                if (!string.IsNullOrEmpty(hdfFolkId.Text))
                    hs.FolkId = Convert.ToInt32(hdfFolkId.Text);
                // 6)Hồ sơ gốc
                if (fufTepTinDinhKem.HasFile)
                {
                    var serverPath = Server.MapPath(hs.OriginalFile);
                    if (Util.GetInstance().FileIsExists(serverPath) == false)
                    {
                        hs.OriginalFile = "~/" + UploadFile(fufTepTinDinhKem, Constant.PathContract);
                    }
                    else
                    {
                        File.Delete(serverPath);
                        hs.OriginalFile = "~/" + UploadFile(fufTepTinDinhKem, Constant.PathContract);
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
                if (!string.IsNullOrEmpty(hdfFamilyPolicyId.Text))
                    hs.FamilyPolicyId = Convert.ToInt32(hdfFamilyPolicyId.Text);
                // 23) Số chứng minh nhân dân
                if (!string.IsNullOrEmpty(txtIDNumber.Text))
                    hs.IDNumber = txtIDNumber.Text;
                if (!util.IsDateNull(IDIssueDate.SelectedDate))
                    hs.IDIssueDate = IDIssueDate.SelectedDate;
                if (!string.IsNullOrEmpty(hdfIDIssuePlaceId.Text))
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
                if (!string.IsNullOrEmpty(hdfJobTitleId.Text))
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
                if (!string.IsNullOrEmpty(hdfCPVPositionId.Text))
                    hs.CPVPositionId = Convert.ToInt32(hdfCPVPositionId.Text);
                if (!string.IsNullOrEmpty(hdfVYUPositionId.Text))
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
                if (!string.IsNullOrEmpty(txtBusinessLand.Text))
                    hs.BusinessLandArea = decimal.Parse(txtBusinessLand.Text);
                // 37) Nhận xét, đánh giá của đơn vị quản lý sử dụng cán bộ, công chức
                hs.Review = txtReview.Text;


                if (!string.IsNullOrEmpty(hdfWorkStatusId.Text))
                    hs.WorkStatusId = Convert.ToInt32(hdfWorkStatusId.Text);
                if (!util.IsDateNull(WorkStatusDate.SelectedDate))
                    hs.WorkStatusDate = WorkStatusDate.SelectedDate;
                hs.WorkStatusReason = txtWorkStatusReason.Text;
                hs.WorkEmail = txtWorkEmail.Text;
                hs.PersonalEmail = txtPersonalEmail.Text;
                hs.CellPhoneNumber = txtCellPhoneNumber.Text;
                hs.HomePhoneNumber = txtHomePhoneNumber.Text;
                hs.WorkPhoneNumber = txtWorkPhoneNumber.Text;
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
                    hs.Id = Convert.ToInt32(hdfPrKeyHoSo.Text);
                    RecordController.Update(hs);
                    RM.RegisterClientScriptBlock("save", "gridsSave();");
                    Dialog.ShowNotification("Cập nhật dữ liệu thành công!");
                }
                else
                {
                    RecordController.Create(hs);
                    hdfPrKeyHoSo.Text = hs.Id.ToString();
                    RM.RegisterClientScriptBlock("save", "gridsSave();");
                    Dialog.ShowNotification("Thêm mới thành công!");
                    if (e.ExtraParams["Reset"] == "True")
                    {
                        RM.RegisterClientScriptBlock("rs2", "ResetForm();");
                    }
                }

                var recordId = hs.Id;//hs.PR_KEY sau thay bang Id cua bang hr_Record (hs.Id)

                SaveGridEducationHistory(e.ExtraParams["jsonEducation"], recordId);
                SaveGridSalary(e.ExtraParams["jsonSalary"], recordId);
                SaveGridWorkHistory(e.ExtraParams["jsonWorkHistory"], recordId);
                SaveGridReward(e.ExtraParams["jsonReward"], recordId);
                SaveGridDiscipline(e.ExtraParams["jsonDiscipline"], recordId);
                SaveGridContract(e.ExtraParams["jsonContract"], recordId);
                SaveGridTrainingHistory(e.ExtraParams["jsonTrainingHistory"], recordId);
                SaveGridFamilyRelationship(e.ExtraParams["jsonFamilyRelationship"], recordId);
                SaveGridAbility(e.ExtraParams["jsonAbility"], recordId);
                SaveGridWorkProcess(e.ExtraParams["jsonWorkProcess"], recordId);
                SaveGridInsurance(e.ExtraParams["jsonInsurance"], recordId);
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
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                if (sqlex.Number == 2627)
                {
                    Dialog.ShowError("Mã danh mục đã bị trùng");
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowError("" + ex.Message);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_ClickGroup(object sender, DirectEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(hdfGroup.Text))
                {
                    Dialog.ShowError("Bạn chưa chọn loại danh mục");
                    return;
                }
                var model = new CatalogModel(hdfTableDM.Text)
                {
                    Name = txtTenDMGroup.Text,
                    Group = hdfGroup.Text
                };
                CatalogController.Create(hdfCurrentCatalogName.Text, model);
                RM.RegisterClientScriptBlock("add", "resetInputCategoryGroup();");
                grpCategoryGroup.Reload();
            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 2627)
                {
                    Dialog.ShowError("Mã danh mục đã bị trùng");
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowError("" + ex.Message);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAccept_Click(object sender, DirectEventArgs e)
        {
            try
            {
                //upload file
                if (fufUploadControl.HasFile == false && fufUploadControl.PostedFile.ContentLength > 2000000)
                {
                    Dialog.ShowNotification("File không được lớn hơn 200kb");
                    return;
                }
                try
                {
                    hdfAnhDaiDien.Text = "~/" + UploadFile(fufUploadControl, Constant.PathLocationImageEmployee);
                }
                catch (Exception ex)
                {
                    Dialog.ShowError(ex.Message);
                }
                wdUploadImageWindow.Hide();

                //Hiển thị lại ảnh sau khi đã cập nhật xong
                img_anhdaidien.ImageUrl = hdfAnhDaiDien.Text;
            }
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected void btnEdit_Click()
        {
            try
            {
                hdfPrKeyHoSo.Text = Request.QueryString["PrKeyHoSo"];
                var record = RecordController.GetById(Convert.ToInt32(hdfPrKeyHoSo.Text));
                var salaryDecision = SalaryDecisionController.GetCurrent(Convert.ToInt32(hdfPrKeyHoSo.Text));
                if (record == null) return;
                // Ho so nhan su
                if (!string.IsNullOrEmpty(record.ImageUrl))
                    img_anhdaidien.ImageUrl = record.ImageUrl;
                txtEmployeeCode.Text = record.EmployeeCode;
                txtFullName.Text = record.FullName;
                txtAlias.Text = record.Alias;
                cboEmployeeType.Text = record.EmployeeTypeName;
                hdfEmployeeTypeId.Text = record.EmployeeTypeId.ToString();
                txtManagementDepartment.Text = record.ManagementDepartmentName;
                hdfManagementDepartmentId.Text = record.ManagementDepartmentId.ToString();
                cboDepartment.Text = record.DepartmentName;
                hdfDepartmentId.Text = record.DepartmentId.ToString();
                dfBirthDate.SetValue(record.BirthDateVn);
                cbxSex.Text = record.SexName == "Nam" ? "M" : "F";
                cboMaritalStatus.Text = record.MaritalStatusName;
                hdfMaritalStatusId.Text = record.MaritalStatusId.ToString();
                cbxPersonalClass.Text = record.PersonalClassName;
                hdfPersonalClassId.Text = record.PersonalClassId.ToString();
                cbxFamilyClass.Text = record.FamilyClassName;
                hdfFamilyClassId.Text = record.FamilyClassId.ToString();
                txtCellPhoneNumber.Text = record.CellPhoneNumber;
                txtHomePhoneNumber.Text = record.HomePhoneNumber;
                txtWorkPhoneNumber.Text = record.WorkPhoneNumber;
                txtWorkStatusReason.Text = record.WorkStatusReason;
                cbxFolk.Text = record.FolkName;
                hdfFolkId.Text = record.FolkId.ToString();
                cbxReligion.Text = record.ReligionName;
                hdfReligionId.Text = record.ReligionId.ToString();
                FunctionaryDate.SetValue(record.FunctionaryDate);
                txt_ResidentPlace.Text = record.ResidentPlace;
                txtCPVCardNumber.Text = record.CPVCardNumber;
                txtPersonalTaxCode.Text = record.PersonalTaxCode;
                txtWorkEmail.Text = record.WorkEmail;
                txtPersonalEmail.Text = record.PersonalEmail;
                txt_Address.Text = record.Address;
                txtPreviousJob.Text = record.PreviousJob;
                RecruimentDate.SetValue(record.RecruimentDate);
                txtRecruitmentDepartment.Text = record.RecruimentDepartment;
                cbxPosition.Text = record.PositionName;
                hdfPositionId.Text = record.PositionId.ToString();
                txtAssignedWork.Text = record.AssignedWork;
                WorkStatusDate.SetValue(record.WorkStatusDate);
                if (salaryDecision != null)
                {
                    var salaryAllowances = SalaryAllowanceController.GetAll(null, salaryDecision.Id, null, null, null);
                    txtQuantumName.Text = salaryDecision.QuantumName;
                    txtQuantumCode.Text = salaryDecision.QuantumCode;
                    txtSalaryGrade.Text = "Bậc " + (salaryDecision.Grade);
                    txtSalaryFactor.Text = salaryDecision.Factor.ToString();
                    QuantumEffectiveDate.SetValue(salaryDecision.EffectiveDate);
                    if (salaryAllowances != null)
                        foreach (var salaryAllowance in salaryAllowances)
                        {
                            switch (salaryAllowance.AllowanceCode)
                            {
                                case PositionAllowance:
                                    txtPositionAllowance.Text = salaryAllowance.Value.ToString();
                                    break;
                                case OtherAllowance:
                                    txtOtherAllowance.Text = salaryAllowance.Value.ToString();
                                    break;
                                default:
                                    break;
                            }
                        }
                }
                cbxBasicEducation.Text = record.BasicEducationName;
                hdfBasicEducationId.Text = record.BasicEducationId.ToString();
                cbxEducation.Text = record.EducationName;
                hdfEducationId.Text = record.EducationId.ToString();
                cbxPoliticLevel.Text = record.PoliticLevelName;
                hdfPoliticLevelId.Text = record.PoliticLevelId.ToString();
                cbxManagementLevel.Text = record.ManagementLevelName;
                hdfManagementLevelId.Text = record.ManagementLevelId.ToString();
                cbxJobTitle.Text = record.JobTitleName;
                hdfJobTitleId.Text = record.JobTitleId.ToString();
                cbxGovernmentPosition.Text = record.GovernmentPositionName;
                hdfGovernmentPositionId.Text = record.GovernmentPositionId.ToString();
                cbxPluralityPosition.Text = record.PluralityPositionName;
                hdfPluralityPositionId.Text = record.PluralityPositionId.ToString();
                cbxWorkStatus.Text = record.WorkStatusName;
                hdfWorkStatusId.Text = record.WorkStatusId.ToString();
                cbxBirthPlaceProvince.Text = record.BirthPlaceProvinceName;
                cbxBirthPlaceDistrict.Text = record.BirthPlaceDistrictName;
                cbxBirthPlaceWard.Text = record.BirthPlaceWardName;
                cbxHometownProvince.Text = record.HometownProvinceName;
                cbxHometownDistrict.Text = record.HometownDistrictName;
                cbxHometownWard.Text = record.HometownWardName;
                hdfBirthPlaceProvinceId.Text = record.BirthPlaceProvinceId.ToString();
                hdfBirthPlaceDistrictId.Text = record.BirthPlaceDistrictId.ToString();
                hdfBirthPlaceWardId.Text = record.BirthPlaceWardId.ToString();
                hdfHometownProvinceId.Text = record.HometownProvinceId.ToString();
                hdfHometownDistrictId.Text = record.HometownDistrictId.ToString();
                hdfHometownWardId.Text = record.HometownWardId.ToString();
                if (!string.IsNullOrEmpty(hdfBirthPlaceDistrictId.Text))
                    cbxBirthPlaceDistrict.Disabled = false;
                if (!string.IsNullOrEmpty(hdfBirthPlaceWardId.Text))
                    cbxBirthPlaceWard.Disabled = false;
                if (!string.IsNullOrEmpty(hdfHometownDistrictId.Text))
                    cbxHometownDistrict.Disabled = false;
                if (!string.IsNullOrEmpty(hdfHometownWardId.Text))
                    cbxHometownWard.Disabled = false;
                cbxITLevel.Text = record.ITLevelName;
                hdfITLevelId.Text = record.ITLevelId.ToString();
                cbxLanguageLevel.Text = record.LanguageLevelName;
                hdfLanguageLevelId.Text = record.LanguageLevelId.ToString();
                cbxCPVPosition.Text = record.CPVPositionName;
                hdfCPVPositionId.Text = record.CPVPositionId.ToString();
                cbxVYUPosition.Text = record.VYUPositionName;
                hdfVYUPositionId.Text = record.VYUPositionId.ToString();
                cbxArmyLevel.Text = record.ArmyLevelName;
                hdfArmyLevelId.Text = record.ArmyLevelId.ToString();
                ArmyJoinedDate.SetValue(record.ArmyJoinedDate);
                ArmyLeftDate.SetValue(record.ArmyLeftDate);
                cbxHealthStatus.Text = record.HealthStatusName;
                hdfHealthStatusId.Text = record.HealthStatusId.ToString();
                cbxFamilyPolicy.Text = record.FamilyPolicyName;
                hdfFamilyPolicyId.Text = record.FamilyPolicyId.ToString();
                cbxIDIssuePlace.Text = record.IDIssuePlaceName;
                hdfIDIssuePlaceId.Text = record.IDIssuePlaceId.ToString();
                CPVJoinedDate.SetValue(record.CPVJoinedDate);
                CPVOfficialJoinedDate.SetValue(record.CPVOfficialJoinedDate);
                txtCPVJoinedPlace.Text = record.CPVJoinedPlace;
                VYUJoinedDate.SetValue(record.VYUJoinedDate);
                txtTitleAwarded.Text = record.TitleAwarded;
                cbxBloodGroup.SetValue(record.BloodGroup);
                txtHeight.SetValue(record.Height);
                txtWeight.SetValue(record.Weight);
                txtRankWounded.Text = record.RankWounded;
                txtIDNumber.SetValue(record.IDNumber);
                IDIssueDate.SetValue(record.IDIssueDate);
                txtInsuranceNumber.Text = record.InsuranceNumber;
                InsuranceIssueDate.SetValue(record.InsuranceIssueDate);
                txtBiography.Text = record.Biography;
                txtForeignOrganizationJoined.Text = record.ForeignOrganizationJoined;
                txtRelativesAboard.Text = record.RelativesAboard;
                txtReview.Text = record.Review;
                txtFamilyIncome.Text = record.FamilyIncome.ToString();
                txtOtherIncome.Text = record.OtherIncome;
                txtAllocatedHouse.Text = record.AllocatedHouse;
                txtAllocatedHouseArea.Text = record.AllocatedtHouseArea.ToString();
                txtHouse.Text = record.House;
                txtHouseArea.Text = record.HouseArea.ToString();
                txtAllocatedLandArea.Text = record.AllocatedLandArea.ToString();
                txtLandArea.Text = record.LandArea.ToString();
                txtBusinessLand.Text = record.BusinessLandArea.ToString();
                txtSkills.Text = record.Skills;
                txtContactAddress.Text = record.ContactAddress;
                txtContactPersonName.Text = record.ContactPersonName;
                txtContactPhoneNumber.Text = record.ContactPhoneNumber;
                txtContactRelation.Text = record.ContactRelation;
                cbxInputIndustry.Text = record.IndustryName;
                hdfInputIndustryId.Text = record.IndustryId.ToString();
                hdfAnhDaiDien.Text = record.ImageUrl;
                RevolutionJoinDate.SetValue(record.RevolutionJoinedDate);
                txtLongestJob.Text = record.LongestJob;
                fufTepTinDinhKem.Text = record.OriginalFile != null ? GetNameFile(record.OriginalFile) : @"Chọn tệp tin";
            }
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }

        #endregion

        #region OnRefreshData

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cbxSalaryGradeStore_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            cbxSalaryGrade.Text = "";
            if (string.IsNullOrEmpty(hdfSalaryQuantumGrid.Text)) return;
            var quantum = cat_QuantumServices.GetById(Convert.ToInt32(hdfSalaryQuantumGrid.Text));
            var quantumGrade = cat_GroupQuantumServices.GetFieldValueById(quantum.GroupQuantumId, "GradeMax");
            var grade = Convert.ToInt32(quantumGrade);
            hdfSalaryGradeGrid.Text = grade.ToString();
            cbxSalaryGrade.Text = "Bậc" + grade;
            var objs = new List<StoreComboxObject>();
            for (var i = 1; i <= grade; i++)
            {
                var stob = new StoreComboxObject
                {
                    MA = i.ToString(),
                    TEN = "Bậc " + i
                };
                objs.Add(stob);
            }
            cbxSalaryGradeStore.DataSource = objs;
            cbxSalaryGradeStore.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void StoreSalary_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            if (string.IsNullOrEmpty(hdfPrKeyHoSo.Text)) return;
            var salaryDecisions = SalaryDecisionController.GetAll(null, null, Convert.ToInt32(hdfPrKeyHoSo.Text), null, null, null, null, null, null, null, false, null, null);
            if (salaryDecisions == null) return;
            StoreSalary.DataSource = salaryDecisions;
            StoreSalary.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void storeWorkHistory_BeforeStoreChanged(object sender, BeforeStoreChangedEventArgs e)
        {
            try
            {
                var temp = e.DataHandler.ObjectData<WorkHistoryModel>();
                foreach (var item in temp.Created)
                {
                    var info = new hr_WorkHistory { RecordId = Convert.ToInt32(hdfPrKeyHoSo.Text) };
                    if (item.FromDate != null)
                        info.FromDate = item.FromDate;
                    if (item.ToDate != null)
                        info.ToDate = item.ToDate;
                    info.Note = item.Note;
                    new WorkHistoryController().InsertWorkHistory(info);
                }
                foreach (var item in temp.Updated)
                {
                    var info = new hr_WorkHistory { RecordId = Convert.ToInt32(hdfPrKeyHoSo.Text) };
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void StoreWorkProcess_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            if (string.IsNullOrEmpty(hdfPrKeyHoSo.Text)) return;
            StoreWorkProcess.DataSource = WorkProcessController.GetAll(Convert.ToInt32(hdfPrKeyHoSo.Text));
            StoreWorkProcess.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void StoreFamilyRelationship_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            if (string.IsNullOrEmpty(hdfPrKeyHoSo.Text)) return;
            storeFamilyRelationship.DataSource = FamilyRelationshipController.GetAll(Convert.ToInt32(hdfPrKeyHoSo.Text));
            storeFamilyRelationship.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void StoreEducation_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            if (string.IsNullOrEmpty(hdfPrKeyHoSo.Text)) return;
            StoreEducation.DataSource = EducationHistoryController.GetAll(Convert.ToInt32(hdfPrKeyHoSo.Text));
            StoreEducation.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void storeAbility_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            if (string.IsNullOrEmpty(hdfPrKeyHoSo.Text)) return;
            storeAbility.DataSource = AbilityController.GetAll(Convert.ToInt32(hdfPrKeyHoSo.Text));
            storeAbility.DataBind();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void storeContract_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            if (string.IsNullOrEmpty(hdfPrKeyHoSo.Text)) return;
            storeContract.DataSource = ContractController.GetAll(Convert.ToInt32(hdfPrKeyHoSo.Text));
            storeContract.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void storeInsurance_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            if (string.IsNullOrEmpty(hdfPrKeyHoSo.Text)) return;
            storeInsurance.DataSource = InsuranceController.GetAll(Convert.ToInt32(hdfPrKeyHoSo.Text));
            storeInsurance.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void storeReward_RefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            if (string.IsNullOrEmpty(hdfPrKeyHoSo.Text)) return;
            storeReward.DataSource = RewardController.GetAll(Convert.ToInt32(hdfPrKeyHoSo.Text));
            storeReward.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void storeDiscipline_RefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            if (string.IsNullOrEmpty(hdfPrKeyHoSo.Text)) return;
            storeDiscipline.DataSource = DisciplineController.GetAll(Convert.ToInt32(hdfPrKeyHoSo.Text));
            storeDiscipline.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void storeReward_BeforeStoreChanged(object sender, BeforeStoreChangedEventArgs e)
        {
            try
            {
                var reward = e.DataHandler.ObjectData<RewardModel>();
                foreach (var created in reward.Created)
                {
                    var info = new hr_Reward
                    {
                        RecordId = Convert.ToInt32(hdfPrKeyHoSo.Text),
                        Note = created.Note,
                        LevelRewardId = created.LevelRewardId,
                        DecisionNumber = created.DecisionNumber,
                        DecisionDate = created.DecisionDate
                    };
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void storeDiscipline_BeforeStoreChanged(object sender, BeforeStoreChangedEventArgs e)
        {
            try
            {
                var discipline = e.DataHandler.ObjectData<DisciplineModel>();
                foreach (var created in discipline.Created)
                {
                    var info = new hr_Discipline
                    {
                        RecordId = Convert.ToInt32(hdfPrKeyHoSo.Text),
                        LevelDisciplineId = created.LevelDisciplineId,
                        FormDisciplineId = created.FormDisciplineId,
                        Note = created.Note,
                        DecisionNumber = created.DecisionNumber,
                        DecisionDate = created.DecisionDate
                    };

                    new DisciplineController().Insert(info);
                }
                foreach (var updated in discipline.Updated)
                {
                    var info = new hr_Discipline
                    {
                        LevelDisciplineId = updated.LevelDisciplineId,
                        FormDisciplineId = updated.FormDisciplineId,
                        Note = updated.Note,
                        DecisionNumber = updated.DecisionNumber,
                        DecisionDate = updated.DecisionDate
                    };
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void storeTrainingHistory_RefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            if (string.IsNullOrEmpty(hdfPrKeyHoSo.Text)) return;
            storeTrainingHistory.DataSource = TrainingHistoryController.GetAll(Convert.ToInt32(hdfPrKeyHoSo.Text));
            storeTrainingHistory.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    info.ToDate = created.ToDate ?? DateTime.Now;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void storeWorkHistory_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(hdfPrKeyHoSo.Text)) return;
                storeWorkHistory.DataSource = WorkHistoryController.GetAll(Convert.ToInt32(hdfPrKeyHoSo.Text));
                storeWorkHistory.DataBind();
            }
            catch (Exception ex)
            {
                Dialog.ShowError("" + ex.Message);
            }
        }

        #endregion

        #region OnBeforeStoreChanged

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void HandleChangesWorkProcess(object sender, BeforeStoreChangedEventArgs e)
        {
            try
            {
                var work = e.DataHandler.ObjectData<WorkProcessModel>();
                // insert
                foreach (var created in work.Created)
                {
                    var info = new hr_WorkProcess { RecordId = Convert.ToInt32(hdfPrKeyHoSo.Text), Note = created.Note };

                    new WorkProcessController().Insert(info);
                }
                // update
                foreach (var updated in work.Updated)
                {
                    var info = new hr_WorkProcess();
                    if (updated.EffectiveDate != null)
                        info.EffectiveDate = updated.EffectiveDate;
                    info.EffectiveEndDate = updated.EffectiveEndDate ?? DateTime.Now;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void HandleChangesQuanHeGiaDinh(object sender, BeforeStoreChangedEventArgs e)
        {
            try
            {
                var giadinh = e.DataHandler.ObjectData<FamilyRelationshipModel>();
                // insert
                foreach (var created in giadinh.Created)
                {
                    var info = new hr_FamilyRelationship
                    {
                        RecordId = Convert.ToInt32(hdfPrKeyHoSo.Text),
                        FullName = created.FullName,
                        BirthYear = created.BirthYear,
                        RelationshipId = created.RelationshipId,
                        Occupation = created.Occupation,
                        WorkPlace = created.WorkPlace,
                        Sex = created.Sex,
                        Note = created.Note,
                        IsDependent = created.IsDependent,
                        IDNumber = created.IDNumber
                    };
                    new FamilyRelationshipController().Insert(info);
                }
                // update
                foreach (var updated in giadinh.Updated)
                {
                    var info = new hr_FamilyRelationship
                    {
                        FullName = updated.FullName,
                        BirthYear = updated.BirthYear,
                        RelationshipId = updated.RelationshipId,
                        Occupation = updated.Occupation,
                        WorkPlace = updated.WorkPlace,
                        Sex = updated.Sex,
                        Note = updated.Note,
                        IsDependent = updated.IsDependent,
                        IDNumber = updated.IDNumber
                    };
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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void HandleChangesAbility(object sender, BeforeStoreChangedEventArgs e)
        {
            try
            {
                var ability = e.DataHandler.ObjectData<AbilityModel>();
                foreach (var created in ability.Created)
                {
                    var info = new hr_Ability
                    {
                        RecordId = Convert.ToInt32(hdfPrKeyHoSo.Text),
                        Note = created.Note,
                        AbilityId = created.AbilityId,
                        GraduationTypeId = created.GraduationTypeId
                    };
                    new AbilityController().Insert(info);
                }
                // update
                foreach (var updated in ability.Updated)
                {
                    var info = new hr_Ability
                    {
                        Note = updated.Note,
                        AbilityId = updated.AbilityId,
                        GraduationTypeId = updated.GraduationTypeId
                    };
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void HandleChangesContract_BeforeStoreChanged(object sender, BeforeStoreChangedEventArgs e)
        {
            try
            {
                var hopdong = e.DataHandler.ObjectData<ContractModel>();
                foreach (var created in hopdong.Created)
                {
                    var info = new hr_Contract
                    {
                        RecordId = Convert.ToInt32(hdfPrKeyHoSo.Text),
                        ContractNumber = created.ContractNumber,
                        ContractTypeId = created.ContractTypeId,
                        ContractStatusId = created.ContractStatusId
                    };
                    if (created.ContractDate != null)
                        info.ContractDate = created.ContractDate;
                    if (created.ContractEndDate != null)
                        info.ContractEndDate = created.ContractEndDate;
                    new ContractController().Insert(info);
                }
                // update
                foreach (var updated in hopdong.Updated)
                {
                    var info = new hr_Contract
                    {
                        ContractNumber = updated.ContractNumber,
                        ContractTypeId = updated.ContractTypeId,
                        ContractStatusId = updated.ContractStatusId
                    };
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void HandlerChangesInsurance(object sender, BeforeStoreChangedEventArgs e)
        {
            try
            {
                var insurance = e.DataHandler.ObjectData<InsuranceModel>();
                // insert
                foreach (var created in insurance.Created)
                {
                    var info = new InsuranceModel
                    {
                        RecordId = Convert.ToInt32(hdfPrKeyHoSo.Text),
                        FromDate = created.FromDate,
                        ToDate = created.ToDate,
                        PositionId = created.PositionId,
                        SalaryFactor = created.SalaryFactor,
                        SalaryLevel = created.SalaryLevel,
                        Allowance = created.Allowance,
                        Note = created.Note
                    };

                    InsuranceController.Insert(info);
                }
                // update
                foreach (var updated in insurance.Updated)
                {
                    var info = new InsuranceModel
                    {
                        FromDate = updated.FromDate,
                        ToDate = updated.ToDate,
                        PositionId = updated.PositionId,
                        SalaryFactor = updated.SalaryFactor,
                        SalaryLevel = updated.SalaryLevel,
                        Allowance = updated.Allowance,
                        Note = updated.Note
                    };
                    InsuranceController.Update(info);
                }
                // delete
                foreach (var deleted in insurance.Deleted)
                {
                    InsuranceController.Delete(deleted.Id);
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowError("" + ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void HandleChangesTrainingHistory(object sender, BeforeStoreChangedEventArgs e)
        {
            try
            {
                var work = e.DataHandler.ObjectData<TrainingHistoryModel>();
                // insert
                foreach (var created in work.Created)
                {
                    var info = new hr_TrainingHistory()
                    {
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

        #endregion

        #endregion

        #region Public Methods

        /// <summary>
        /// Sinh mã cán bộ dựa vào cấu hình hệ thống:
        /// -   Tiền tố của mã cán bộ
        /// -   Số lượng chữ số theo sau tiền tố
        /// </summary>
        /// <returns>Mã cán bộ mới được sinh ra</returns>
        public string GenerateEmployeeCode()
        {
            var departments = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
            var prefix = SystemConfigController.GetValueByNameFollowDepartment(SystemConfigParameter.PREFIX, departments);
            var numberCharacter = SystemConfigController.GetValueByNameFollowDepartment(SystemConfigParameter.NUMBER_OF_CHARACTER, departments);
            if (string.IsNullOrEmpty(prefix))
                prefix = "CB";
            var number = string.IsNullOrEmpty(numberCharacter) ? 5 : int.Parse(numberCharacter);

            var record = RecordController.GetByEmployeeCodeGenerate(prefix, number);
            var oldMaCB = GenerateEmployeeConst;
            if (record != null && !string.IsNullOrEmpty(record.EmployeeCode))
                oldMaCB = record.EmployeeCode;
            var oldNumber = long.Parse("" + oldMaCB.Substring(prefix.Length));
            oldNumber++;
            var newMaCB = GenerateEmployeeConst + oldNumber;
            newMaCB = prefix + newMaCB.Substring(newMaCB.Length - number);
            return newMaCB;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        public void DownloadAttachFile(string path)
        {
            try
            {
                if (string.IsNullOrEmpty(path))
                {
                    Dialog.ShowNotification("Không có tệp tin đính kèm");
                    return;
                }
                var serverPath = Server.MapPath(path);
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
                ExtNet.Msg.Alert("Thông báo từ hệ thống", ex.Message).Show();
            }
        }

        public string GetNameFile(string path)
        {
            var str = path.Split('/');
            return str[str.Length - 1];
        }

        /// <summary>
        /// 
        /// </summary>
        public void ResetControl()
        {
            foreach (var item in pn.ContentControls)
            {
                switch (item.ToString())
                {
                    case "Ext.Net.ComboBox":
                        if (item is ComboBox cb) cb.Reset();
                        break;
                    case "Ext.Net.Hidden":
                        if (item is Hidden hdf) hdf.Reset();
                        break;
                    case "Ext.Net.TextArea":
                        if (item is TextArea txta) txta.Reset();
                        break;
                    case "Ext.Net.TextField":
                        if (item is TextField txtf) txtf.Reset();
                        break;
                    case "Ext.Net.NumberField":
                        if (item is NumberField mbf) mbf.Reset();
                        break;
                    case "Ext.Net.DateField":
                        if (item is DateField df) df.Reset();
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                ExtNet.Msg.Alert("Thông báo từ hệ thống", "Có lỗi xảy ra: " + ex.Message).Show();
            }
        }

        #region SaveGrid

        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <param name="recordId"></param>
        private void SaveGridSalary(string json, int recordId)
        {
            try
            {
                if (string.IsNullOrEmpty(json)) return;
                var rs = JSON.Deserialize<List<SalaryDecisionModel>>(json);
                foreach (var item in rs)
                {
                    var groupQuantum = CatalogGroupQuantumController.GetById(item.GroupQuantumId);
                    var groupQuantumGrade =
                        CatalogGroupQuantumGradeController.GetUnique(item.GroupQuantumId, item.Grade);
                    var obj = new sal_SalaryDecision
                    {
                        Id = item.Id,
                        Name = item.Name,
                        DecisionNumber = item.DecisionNumber,
                        QuantumId = item.QuantumId,
                        GroupQuantumId = item.GroupQuantumId,
                        DecisionDate = item.DecisionDate,
                        EffectiveDate = item.EffectiveDate,
                        InsuranceSalary = item.InsuranceSalary,
                        SignerName = item.SignerName,
                        Note = item.Note,
                        Factor = item.Factor,
                        Grade = item.Grade,
                        RecordId = recordId,
                        CreatedDate = DateTime.Now,
                        EditedDate = DateTime.Now
                    };

                    if (groupQuantum != null && groupQuantumGrade != null && item.EffectiveDate != null)
                        obj.NextRaiseDate = item.EffectiveDate.AddMonths(groupQuantumGrade.MonthStep == 0
                            ? groupQuantum.MonthStep
                            : groupQuantumGrade.MonthStep);
                    else
                        continue;

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
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <param name="recordId"></param>
        private void SaveGridInsurance(string json, int recordId)
        {
            try
            {
                if (string.IsNullOrEmpty(json)) return;
                var rs = JSON.Deserialize<List<InsuranceModel>>(json);
                foreach (var item in rs)
                {
                    var obj = new InsuranceModel
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
                        RecordId = recordId,
                    };
                    if (obj.Id > 0)
                    {
                        InsuranceController.Update(obj);
                        storeInsurance.CommitChanges();
                    }
                    else
                    {
                        InsuranceController.Insert(obj);
                        storeInsurance.DataSource = InsuranceController.GetAll(recordId);
                        storeInsurance.DataBind();
                        storeInsurance.CommitChanges();
                    }
                }
                GridInsurance.Reload();
            }
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <param name="recordId"></param>
        private void SaveGridWorkProcess(string json, int recordId)
        {
            try
            {
                if (string.IsNullOrEmpty(json)) return;
                var controller = new WorkProcessController();
                var rs = JSON.Deserialize<List<WorkProcessModel>>(json);
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
                        RecordId = recordId,
                    };
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
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <param name="recordId"></param>
        private void SaveGridAbility(string json, int recordId)
        {
            try
            {
                if (string.IsNullOrEmpty(json)) return;
                var controller = new AbilityController();
                var rs = JSON.Deserialize<List<AbilityModel>>(json);
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
                        RecordId = recordId,
                    };

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
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <param name="recordId"></param>
        private void SaveGridFamilyRelationship(string json, int recordId)
        {
            try
            {
                if (string.IsNullOrEmpty(json)) return;
                var controller = new FamilyRelationshipController();
                var rs = JSON.Deserialize<List<FamilyRelationshipModel>>(json);
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
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <param name="recordId"></param>
        private void SaveGridEducationHistory(string json, int recordId)
        {
            try
            {
                if (string.IsNullOrEmpty(json)) return;
                var controller = new EducationHistoryController();
                var rs = JSON.Deserialize<List<EducationHistoryModel>>(json);
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
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <param name="recordId"></param>
        private void SaveGridReward(string json, int recordId)
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
                        obj.RecordId = recordId;
                        if (obj.Id > 0)
                        {
                            controller.Update(obj);
                            storeReward.CommitChanges();
                        }
                        else
                        {
                            controller.Insert(obj);
                            storeReward.DataSource = RewardController.GetAll(recordId);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <param name="recordId"></param>
        private void SaveGridDiscipline(string json, int recordId)
        {
            try
            {
                if (string.IsNullOrEmpty(json)) return;
                var controller = new DisciplineController();
                var rs = JSON.Deserialize<List<DisciplineModel>>(json);
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

                    obj.RecordId = recordId;
                    if (obj.Id > 0)
                    {
                        controller.Update(obj);
                        storeDiscipline.CommitChanges();
                    }
                    else
                    {
                        controller.Insert(obj);
                        storeDiscipline.DataSource = DisciplineController.GetAll(recordId);
                        storeDiscipline.DataBind();
                        storeDiscipline.CommitChanges();
                    }
                }
                GridDiscipline.Reload();

            }
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <param name="recordId"></param>
        private void SaveGridWorkHistory(string json, int recordId)
        {
            try
            {
                if (string.IsNullOrEmpty(json)) return;
                var controller = new WorkHistoryController();
                var rs = JSON.Deserialize<List<WorkHistoryModel>>(json);

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
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <param name="recordId"></param>
        private void SaveGridTrainingHistory(string json, int recordId)
        {
            try
            {
                if (string.IsNullOrEmpty(json)) return;
                var controller = new TrainingHistoryController();
                var rs = JSON.Deserialize<List<TrainingHistoryModel>>(json);

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
                    obj.RecordId = recordId;

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
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <param name="recordId"></param>
        private void SaveGridContract(string json, int recordId)
        {
            try
            {
                if (string.IsNullOrEmpty(json)) return;
                var controller = new ContractController();
                var rs = JSON.Deserialize<List<ContractModel>>(json);
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
                        RecordId = recordId,
                    };
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
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }

        #endregion

        #endregion

        #region Direct Methods
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
                        if (control is NumberField f) f.SetValue(data.Rows[i]["Value"].ToString());
                        break;
                    case "Ext.Net.Checkbox":
                        if (control is Checkbox ck) ck.Checked = data.Rows[i]["Value"].ToString().Equals("1") ? true : false;
                        break;
                    case "Ext.Net.Radio":
                        if (control is Radio rd) rd.Checked = data.Rows[i]["Value"].ToString().Equals("1") ? true : false;
                        break;
                    case "Ext.Net.TextArea":
                        if (control is TextArea txt) txt.SetValue(data.Rows[i]["Value"].ToString());
                        break;
                    case "Ext.Net.TextField":
                        if (control is TextField txtf) txtf.SetValue(data.Rows[i]["Value"].ToString());
                        break;
                    case "Ext.Net.ComboBox":
                        if (control is ComboBox cbb) cbb.SetValue(data.Rows[i]["Value"].ToString());
                        break;
                    case "Ext.Net.Hidden":
                        if (control is Hidden hdf) hdf.SetValue(data.Rows[i]["Value"].ToString());
                        break;
                    case "Ext.Net.DateField":
                        if (control is DateField df) df.SetValue(data.Rows[i]["Value"].ToString());
                        break;
                    default:
                        break;
                }
            }
        }
        [DirectMethod]
        public void GetSalaryInfoGrid()
        {
            if (string.IsNullOrEmpty(hdfSalaryQuantumGrid.Text) ||
                string.IsNullOrEmpty(hdfSalaryGradeGrid.Text)) return;
            var quantum = cat_QuantumServices.GetById(Convert.ToInt32(hdfSalaryQuantumGrid.Text));
            var salaryFactor = CatalogGroupQuantumGradeController
                .GetUnique(quantum.GroupQuantumId, Convert.ToInt32(hdfSalaryGradeGrid.Text)).Factor;
            nfDblHeSoLuong.Text = salaryFactor.ToString();
            nfDblHeSoLuong.SetValue(salaryFactor);

            RM.RegisterClientScriptBlock("aaaa", "updateSalaryProcess('Factor', parseFloat('" + salaryFactor + "'.replace(',', '.')));");
            RM.RegisterClientScriptBlock("cccc", "updateSalaryProcess('GroupQuantumId', parseInt('" + quantum.GroupQuantumId + "'));");
            RM.RegisterClientScriptBlock("bbbb", "GridPanelSalary.getView().refresh();");
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
        [DirectMethod]
        public void ChangeDepartment(int id)
        {
            // init parent name
            var managementDepartmentName = "";
            // find current department
            var currentDepartment = CurrentUser.Departments.FirstOrDefault(d => d.Id == id);
            // check current department
            if (currentDepartment == null) return;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void storeGroup_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            if (!string.IsNullOrEmpty(hdfCurrentCatalogGroupName.Text))
            {
                storeGroup.DataSource = EnumHelper.GetCatalogGroupItems(hdfCurrentCatalogGroupName.Text.ToLower());
                storeGroup.DataBind();
            }
        }

    }

}

