using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Ext.Net;
using Web.Core;
using Web.Core.Framework;
using Web.Core.Framework.Common;
using Web.Core.Helper;
using Web.Core.Object.HumanRecord;
using Web.Core.Object.Security;
using Web.Core.Service.Catalog;

namespace Web.HRM.Modules.UC
{

    public partial class InputEmployee : BaseUserControl
    {
        public event UserControlCloseHandler UserControlClose;
        public delegate void UserControlCloseHandler ();

        private const string GenerateEmployeeConst = "00000000000000000000";
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!ExtNet.IsAjaxRequest)
            {
                // init department
                storeDepartment.DataSource = CurrentUser.DepartmentsTree;
                storeDepartment.DataBind();
                hdfRequiredRecruitmentStatus.Text = ((int) RecruitmentStatus.Approved).ToString();
            }
        }
        
        #region Event Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveEmployee_Click(object sender, DirectEventArgs e)
        {
            var record = new RecordModel();
            var candidate = new CandidateModel();

            if(!string.IsNullOrEmpty(hdfRecordId.Text))
            {
                record.EmployeeCode = txtEmployeeCode.Text;
                if(!string.IsNullOrEmpty(txtEmployeeCode.Text.Trim()))
                {
                    var recordList = RecordController.CheckExistIDNumberAndEmployeeCode(null, null, txtEmployeeCode.Text);
                    if(recordList != null && recordList.Count > 1)
                    {
                        Dialog.ShowError("Mã nhân viên đã tồn tại. Vui lòng nhập mã khác.");
                        txtEmployeeCode.Text = GenerateEmployeeCode();
                        return;
                    }
                }
            }
            else
            {
                record.EmployeeCode = txtEmployeeCode.Text;
                if(!string.IsNullOrEmpty(txtEmployeeCode.Text.Trim()))
                {
                    var recordList = RecordController.CheckExistIDNumberAndEmployeeCode(null, null, txtEmployeeCode.Text);
                    if(recordList != null && recordList.Count > 0)
                    {
                        Dialog.ShowError("Mã nhân viên đã tồn tại. Vui lòng nhập mã khác.");
                        return;
                    }
                }
                if(!string.IsNullOrEmpty(txtIDNumber.Text.Trim()))
                {
                    var recordList = RecordController.CheckExistIDNumberAndEmployeeCode(null, txtIDNumber.Text, null);
                    if(recordList != null && recordList.Count > 0)
                    {
                        Dialog.ShowError("Số chứng minh nhân dân đã tồn tại. Vui lòng nhập số chứng minh nhân dân khác.");
                        return;
                    }
                }
            }
            if(!string.IsNullOrEmpty(hdfImagePerson.Text))
                record.ImageUrl = hdfImagePerson.Text;
            record.FullName = txtFullName.Text;
            // lấy họ và đệm từ họ tên
            var position = record.FullName.LastIndexOf(' ');
            record.Name = position == -1 ? record.FullName : record.FullName.Substring(position + 1).Trim();

            record.Alias = txtAlias.Text;
            if(!DatetimeHelper.IsNull(dfBirthDate.SelectedDate))
                record.BirthDate = dfBirthDate.SelectedDate;
            if(!string.IsNullOrEmpty(cbxSex.SelectedItem.Value) && cbxSex.SelectedItem.Value == "M")
            {
                record.Sex = true;
            }
            else
            {
                record.Sex = false;
            }
            if(string.IsNullOrEmpty(cbxBasicEducation.Text))
            {
                hdfBasicEducationId.Text = @"0";
            }
            if(string.IsNullOrEmpty(cbxEducation.Text))
            {
                hdfEducationId.Text = @"0";
            }
            if(string.IsNullOrEmpty(cbxInputIndustry.Text))
            {
                hdfInputIndustryId.Text = @"0";
            }
            if(string.IsNullOrEmpty(cbxITLevel.Text))
            {
                hdfITLevelId.Text = @"0";
            }
            if(string.IsNullOrEmpty(cbxLanguageLevel.Text))
            {
                hdfLanguageLevelId.Text = @"0";
            }
            if(string.IsNullOrEmpty(cbxPosition.Text))
            {
                hdfPositionId.Text = @"0";
            }
            if(string.IsNullOrEmpty(cbxJobTitle.Text))
            {
                hdfJobTitleId.Text = @"0";

            }
            if(!string.IsNullOrEmpty(hdfBirthPlaceProvinceId.Text))
                record.BirthPlaceProvinceId = Convert.ToInt32(hdfBirthPlaceProvinceId.Text);
            if(string.IsNullOrEmpty(cbxBirthPlaceDistrict.Text))
                hdfBirthPlaceDistrictId.Text = @"0";
            if(!string.IsNullOrEmpty(hdfBirthPlaceDistrictId.Text))
                record.BirthPlaceDistrictId = Convert.ToInt32(hdfBirthPlaceDistrictId.Text);
            if(string.IsNullOrEmpty(cbxBirthPlaceWard.Text))
                hdfBirthPlaceWardId.Text = @"0";
            if(!string.IsNullOrEmpty(hdfBirthPlaceWardId.Text))
                record.BirthPlaceWardId = Convert.ToInt32(hdfBirthPlaceWardId.Text);
            if(!string.IsNullOrEmpty(hdfHometownProvinceId.Text))
                record.HometownProvinceId = Convert.ToInt32(hdfHometownProvinceId.Text);
            if(string.IsNullOrEmpty(cbxHometownDistrict.Text))
                hdfHometownDistrictId.Text = @"0";
            if(!string.IsNullOrEmpty(hdfHometownDistrictId.Text))
                record.HometownDistrictId = Convert.ToInt32(hdfHometownDistrictId.Text);
            if(string.IsNullOrEmpty(cbxHometownWard.Text))
                hdfHometownWardId.Text = @"0";
            if(!string.IsNullOrEmpty(hdfHometownWardId.Text))
                record.HometownWardId = Convert.ToInt32(hdfHometownWardId.Text);
            if(!string.IsNullOrEmpty(hdfMaritalStatusId.Text))
                record.MaritalStatusId = Convert.ToInt32(hdfMaritalStatusId.Text);
            if(!string.IsNullOrEmpty(hdfFolkId.Text))
                record.FolkId = Convert.ToInt32(hdfFolkId.Text);
            if(!string.IsNullOrEmpty(hdfReligionId.Text))
                record.ReligionId = Convert.ToInt32(hdfReligionId.Text);
            record.ResidentPlace = txt_ResidentPlace.Text;
            record.Address = txt_Address.Text;
            record.CPVCardNumber = txtCPVCardNumber.Text;
            if(!string.IsNullOrEmpty(hdfJobTitleId.Text))
                record.JobTitleId = Convert.ToInt32(hdfJobTitleId.Text);
            //Ngay thu viec
            if(!DatetimeHelper.IsNull(RecruimentDate.SelectedDate))
                record.RecruimentDate = RecruimentDate.SelectedDate;
            //Ngay chinh thuc
            if(!DatetimeHelper.IsNull(ParticipationDate.SelectedDate))
                record.ParticipationDate = ParticipationDate.SelectedDate;
            record.RecruimentDepartment = txtRecruitmentDepartment.Text;
            if(!string.IsNullOrEmpty(hdfBasicEducationId.Text))
                record.BasicEducationId = Convert.ToInt32(hdfBasicEducationId.Text);
            if(!string.IsNullOrEmpty(hdfEducationId.Text))
                record.EducationId = Convert.ToInt32(hdfEducationId.Text);
            if(!string.IsNullOrEmpty(hdfPoliticLevelId.Text))
                record.PoliticLevelId = Convert.ToInt32(hdfPoliticLevelId.Text);
            if(!string.IsNullOrEmpty(hdfLanguageLevelId.Text))
                record.LanguageLevelId = Convert.ToInt32(hdfLanguageLevelId.Text);
            if(!string.IsNullOrEmpty(hdfITLevelId.Text))
                record.ITLevelId = Convert.ToInt32(hdfITLevelId.Text);
            if(!DatetimeHelper.IsNull(CPVJoinedDate.SelectedDate))
                record.CPVJoinedDate = CPVJoinedDate.SelectedDate;
            if(!DatetimeHelper.IsNull(CPVOfficialJoinedDate.SelectedDate))
                record.CPVOfficialJoinedDate = CPVOfficialJoinedDate.SelectedDate;
            record.CPVJoinedPlace = txtCPVJoinedPlace.Text;
            if(!string.IsNullOrEmpty(hdfCPVPositionId.Text))
                record.CPVPositionId = Convert.ToInt32(hdfCPVPositionId.Text);
            if(!string.IsNullOrEmpty(hdfVYUPositionId.Text))
                record.VYUPositionId = Convert.ToInt32(hdfVYUPositionId.Text);
            if(!DatetimeHelper.IsNull(ArmyJoinedDate.SelectedDate))
                record.ArmyJoinedDate = ArmyJoinedDate.SelectedDate;
            if(!DatetimeHelper.IsNull(ArmyLeftDate.SelectedDate))
                record.ArmyLeftDate = ArmyLeftDate.SelectedDate;
            if(!string.IsNullOrEmpty(hdfPositionId.Text))
                record.PositionId = Convert.ToInt32(hdfPositionId.Text);
            if(!string.IsNullOrEmpty(hdfArmyLevelId.Text))
                record.ArmyLevelId = Convert.ToInt32(hdfArmyLevelId.Text);
            if(!string.IsNullOrEmpty(hdfHealthStatusId.Text))
                record.HealthStatusId = Convert.ToInt32(hdfHealthStatusId.Text);
            record.BloodGroup = cbxBloodGroup.SelectedIndex >= 0 ? cbxBloodGroup.SelectedItem.Value : "";
            if(!string.IsNullOrEmpty(txtHeight.Text))
                record.Height = Convert.ToDecimal(txtHeight.Text);
            if(!string.IsNullOrEmpty(txtWeight.Text))
                record.Weight = Convert.ToDecimal(txtWeight.Text);
            record.DepartmentId = !string.IsNullOrEmpty(hdfDepartmentId.Text) ? Convert.ToInt32(hdfDepartmentId.Text) : CurrentUser.RootDepartment.Id;
            record.RankWounded = txtRankWounded.Text;
            if(!string.IsNullOrEmpty(hdfFamilyPolicyId.Text))
                record.FamilyPolicyId = Convert.ToInt32(hdfFamilyPolicyId.Text);
            if(!string.IsNullOrEmpty(txtIDNumber.Text))
                record.IDNumber = txtIDNumber.Text;
            if(!DatetimeHelper.IsNull(IDIssueDate.SelectedDate))
                record.IDIssueDate = IDIssueDate.SelectedDate;
            if(!string.IsNullOrEmpty(hdfIDIssuePlaceId.Text))
                record.IDIssuePlaceId = Convert.ToInt32(hdfIDIssuePlaceId.Text);
            if(!DatetimeHelper.IsNull(VYUJoinedDate.SelectedDate))
                record.VYUJoinedDate = VYUJoinedDate.SelectedDate;
            record.VYUJoinedPlace = txtVYUJoinedPlace.Text;
            record.PersonalTaxCode = txtPersonalTaxCode.Text;
            var workStatus = CatalogController.GetAll("cat_WorkStatus", null, ((int)RecordStatus.Working).ToString(),
                null, false, null, 1);
            if(workStatus.Count > 0)
            {
                record.WorkStatusId = workStatus.First().Id;
            }

            record.WorkEmail = txtWorkEmail.Text;
            record.PersonalEmail = txtPersonalEmail.Text;
            record.CellPhoneNumber = txtCellPhoneNumber.Text;
            record.HomePhoneNumber = txtHomePhoneNumber.Text;
            record.WorkPhoneNumber = txtWorkPhoneNumber.Text;
            record.InsuranceNumber = txtInsuranceNumber.Text;
            record.ContactPersonName = txtContactPersonName.Text;
            record.ContactPhoneNumber = txtContactPhoneNumber.Text;
            record.ContactRelation = txtContactRelation.Text;
            record.ContactAddress = txtContactAddress.Text;
            if(!string.IsNullOrEmpty(hdfInputIndustryId.Text))
                record.IndustryId = Convert.ToInt32(hdfInputIndustryId.Text);
            if(!DatetimeHelper.IsNull(InsuranceIssueDate.SelectedDate))
            {
                record.InsuranceIssueDate = InsuranceIssueDate.SelectedDate;
            }
            //other info
            if(!string.IsNullOrEmpty(hdfConstructionId.Text))
                record.ConstructionId = Convert.ToInt32(hdfConstructionId.Text);
            if(!string.IsNullOrEmpty(hdfTeamId.Text))
                record.TeamId = Convert.ToInt32(hdfTeamId.Text);
            if(!string.IsNullOrEmpty(txtStudyWorkingDay.Text))
                record.StudyWorkingDay = Convert.ToInt32(txtStudyWorkingDay.Text);
            //Thoi gian thu viec
            if(!string.IsNullOrEmpty(txtProbationWorkingTime.Text))
                record.ProbationWorkingTime = Convert.ToInt32(txtProbationWorkingTime.Text);
            if(!string.IsNullOrEmpty(hdfWorkingFormId.Text))
                record.WorkingFormId = (WorkingFormType)Enum.Parse(typeof(WorkingFormType), hdfWorkingFormId.Text);
            if(!string.IsNullOrEmpty(hdfWorkLocationId.Text))
                record.WorkLocationId = Convert.ToInt32(hdfWorkLocationId.Text);
            if(!string.IsNullOrEmpty(txtGraduationYear.Text))
                record.GraduationYear = Convert.ToInt32(txtGraduationYear.Text);
            if(!string.IsNullOrEmpty(hdfGraduationTypeId.Text))
                record.GraduationTypeId = Convert.ToInt32(hdfGraduationTypeId.Text);
            if(!string.IsNullOrEmpty(hdfUniversityId.Text))
                record.UniversityId = Convert.ToInt32(hdfUniversityId.Text);
            if(!DatetimeHelper.IsNull(dfUnionJoinedDate.SelectedDate))
                record.UnionJoinedDate = dfUnionJoinedDate.SelectedDate;
            if(!string.IsNullOrEmpty(hdfUnionPosition.Text))
                record.UnionJoinedPositionId = Convert.ToInt32(hdfUnionPosition.Text);
            //BHYT
            record.HealthInsuranceNumber = txtHealthInsuranceNumber.Text;
            if(!DatetimeHelper.IsNull(dfHealthJoinedDate.SelectedDate))
                record.HealthJoinedDate = dfHealthJoinedDate.SelectedDate;

            if(!DatetimeHelper.IsNull(dfHealthExpiredDate.SelectedDate))
                record.HealthExpiredDate = dfHealthExpiredDate.SelectedDate;

            // Tuyển dụng
            if(!string.IsNullOrEmpty(txtCandidateCode.Text))
                candidate.Code = txtCandidateCode.Text;
            if(!string.IsNullOrEmpty(txtDesiredSalary.Text))
                candidate.DesiredSalary = Convert.ToDecimal(txtDesiredSalary.Text);
            if (!string.IsNullOrEmpty(hdfRequiredRecruitmentId.Text))
                candidate.RequiredRecruitmentId = Convert.ToInt32(hdfRequiredRecruitmentId.Text);
            if (!DatetimeHelper.IsNull(dfApplyDate.SelectedDate))
                candidate.ApplyDate = dfApplyDate.SelectedDate;
            if (!string.IsNullOrEmpty(hdfCandidateStatus.Text))
                candidate.Status = (CandidateType) Enum.Parse(typeof(CandidateType), hdfCandidateStatus.Text);
            
            // Kiểm tra tuyển dụng
            if (int.TryParse(hdfType.Text, out var type) && type == 1)
            {
                record.Type = RecordType.Candidate;
            }

            var recordId = 0;
            var limitCount = SystemConfigController.GetLimitPackage();

            if (Request.QueryString["Event"] == "Edit" || !string.IsNullOrEmpty(hdfRecordId.Text))
            {
                //Update Ho so
                record.Id = Convert.ToInt32(hdfRecordId.Text);
                RecordController.Update(record);
                recordId = record.Id;

                // lấy record id thông tin ứng viên
                candidate.RecordId = Convert.ToInt32(hdfRecordId.Text);

                Dialog.ShowNotification("Cập nhật dữ liệu thành công!");
            }
            else
            {
                var recordCount = RecordController.GetCountRecords((int)RecordType.Default);
                //check over limit package
                if (recordCount > limitCount)
                {
                    Dialog.Alert("Gói phần mềm bạn đăng ký đã vượt quá giới hạn. Vui lòng đăng ký thêm để sử dụng.");
                    return;
                }
                //create
                var newRecord = RecordController.Create(record);
                recordId = newRecord.Id;

                // lấy record id thông tin ứng viên
                candidate.RecordId = newRecord.Id;

                Dialog.ShowNotification("Thêm mới thành công!");
            }

            // tuyển dụng
            if (type == 1)
            {
                if(int.TryParse(hdfCandidateId.Text, out var result) && result > 0)
                {
                    candidate.Id = result;
                    //update
                    CandidateController.Update(candidate);
                }
                else
                {
                    //count candidate exist
                    var recordCount = RecordController.GetCountRecords((int)RecordType.Candidate);
                    //check over limit package
                    if (recordCount > limitCount)
                    {
                        Dialog.Alert("Gói phần mềm bạn đăng ký đã vượt quá giới hạn. Vui lòng đăng ký thêm để sử dụng.");
                        return;
                    }
                    //create
                    CandidateController.Create(candidate);
                }
            }
               
            // Save family
            SaveGridFamilyRelationship(e.ExtraParams["jsonFamilyRelationship"], recordId);
            if(e.ExtraParams["Reset"] == "True")
            {
                hdfRecordId.Text = "";
                GridPanelFamilyRelationship.Reload();
            }
            //set tabindex default
            tab_Employee.SetActiveTab(panelEmployee);
            //reset
            hdfImagePerson.Reset();
            fufUploadControl.Reset();
            img_anhdaidien.ImageUrl = "";
            //close window
            wdInput.Hide();

            UserControlClose();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, DirectEventArgs e)
        {
            var model = new CatalogModel(hdfTableDM.Text)
            {
                Name = txtTenDM.Text
            };
            //create
            CatalogController.Create(hdfTableDM.Text, model);

            //reload grid
            grpCategory.Reload();
            ResetInputCategory();
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        protected void btnSave_ClickGroup(object sender, DirectEventArgs e)
        {
            if(string.IsNullOrEmpty(hdfGroup.Text))
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
            //reload
            grpCategoryGroup.Reload();
            ResetInputCategoryGroup();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAccept_Click(object sender, DirectEventArgs e)
        {

            //upload file
            if(fufUploadControl.HasFile == false && fufUploadControl.PostedFile.ContentLength > 2000000)
            {
                Dialog.ShowNotification("File không được lớn hơn 200kb");
                return;
            }
            try
            {
                hdfImagePerson.Text = UploadFileAndDisplay(fufUploadControl, Constant.PathLocationImageEmployee);

            }
            catch(Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
            wdUploadImageWindow.Hide();

            //Hiển thị lại ảnh sau khi đã cập nhật xong
            img_anhdaidien.ImageUrl = @"~/" + Constant.PathLocationImageEmployee + "/" + hdfImagePerson.Text;
        }

        #endregion

        #region Direct Methods

        /// <summary>
        /// Upload file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="relativePath"></param>
        /// <returns></returns>
        [DirectMethod]
        public string UploadFileAndDisplay(object sender, string relativePath)
        {
            var obj = (FileUploadField)sender;
            var file = obj.PostedFile;
            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            if(!FileTypeUpload.IsAllowImageFileExtension(fileExtension) &&
                !FileTypeUpload.IsAllowDocumentFileExtension(fileExtension))
            {
                throw new Exception("Hệ thống không hỗ trợ định dạng file này");
            }

            var dir = new DirectoryInfo(Server.MapPath("~/" + relativePath));

            // if directory not exist then create this
            if(dir.Exists == false)
                dir.Create();
            var guid = Guid.NewGuid();
            var fileRelativePath = relativePath + "/" + guid + "_" + obj.FileName;
            var filePath = Path.Combine(Server.MapPath("~/"), fileRelativePath);
            if(File.Exists(filePath))
                throw new Exception("File đã tồn tại");
            file.SaveAs(filePath);
            //File.Move(filePath, Server.MapPath(fileRelativePath));
            return guid + "_" + obj.FileName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        [DirectMethod]
        public void btnEdit_Click(RecordType type)
        {
            switch(type)
            {
                case RecordType.Default:
                    wdInput.SetTitle("Cập nhật thông tin hồ sơ");
                    hdfType.Text = @"0";
                    break;
                case RecordType.Candidate:
                    txtEmployeeCode.Hidden = true;
                    txtCandidateCode.Hidden = false;
                    panelJob.Disabled = true;
                    panelPolitic.Disabled = true;
                    panelEmployeeInsurance.Disabled = true;
                    ctnCandidate.Hidden = false;
                    wdInput.SetTitle("Cập nhật thông tin ứng viên");
                    hdfType.Text = @"1";
                    break;
                default:
                    break;
            }

            if(string.IsNullOrEmpty(hdfRecordId.Text)) return;
            var hs = RecordController.GetById(Convert.ToInt32(hdfRecordId.Text));
            if(hs == null) return;
            // Ho so nhan su
            if(!string.IsNullOrEmpty(hs.ImageUrl))
                img_anhdaidien.ImageUrl = @"~/" + Constant.PathLocationImageEmployee + @"/" + hs.ImageUrl;
            txtEmployeeCode.Text = hs.EmployeeCode;
            txtFullName.Text = hs.FullName;
            txtAlias.Text = hs.Alias;
            cboDepartment.Text = hs.DepartmentName;
            hdfDepartmentId.Text = hs.DepartmentId.ToString();
            dfBirthDate.SetValue(hs.BirthDateVn);
            cbxSex.Text = hs.SexName == "Nam" ? @"M" : @"F";
            cboMaritalStatus.Text = hs.MaritalStatusName;
            hdfMaritalStatusId.Text = hs.MaritalStatusId.ToString();
            txtCellPhoneNumber.Text = hs.CellPhoneNumber;
            txtHomePhoneNumber.Text = hs.HomePhoneNumber;
            txtWorkPhoneNumber.Text = hs.WorkPhoneNumber;
            cbxFolk.Text = hs.FolkName;
            hdfFolkId.Text = hs.FolkId.ToString();
            cbxReligion.Text = hs.ReligionName;
            hdfReligionId.Text = hs.ReligionId.ToString();
            txt_ResidentPlace.Text = hs.ResidentPlace;
            txtCPVCardNumber.Text = hs.CPVCardNumber;
            txtPersonalTaxCode.Text = hs.PersonalTaxCode;
            txtWorkEmail.Text = hs.WorkEmail;
            txtPersonalEmail.Text = hs.PersonalEmail;
            txt_Address.Text = hs.Address;
            //Ngay thu viec
            RecruimentDate.SetValue(hs.RecruimentDate);
            //Ngay chinh thuc
            ParticipationDate.SetValue(hs.ParticipationDate);
            txtRecruitmentDepartment.Text = hs.RecruimentDepartment;
            cbxPosition.Text = hs.PositionName;
            hdfPositionId.Text = hs.PositionId.ToString();
            cbxBasicEducation.Text = hs.BasicEducationName;
            hdfBasicEducationId.Text = hs.BasicEducationId.ToString();
            cbxEducation.Text = hs.EducationName;
            hdfEducationId.Text = hs.EducationId.ToString();
            cbxPoliticLevel.Text = hs.PoliticLevelName;
            hdfPoliticLevelId.Text = hs.PoliticLevelId.ToString();
            cbxJobTitle.Text = hs.JobTitleName;
            hdfJobTitleId.Text = hs.JobTitleId.ToString();
            txtWorkStatus.Text = hs.WorkStatusName;
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
            if(!string.IsNullOrEmpty(hdfBirthPlaceDistrictId.Text))
                cbxBirthPlaceDistrict.Disabled = false;
            if(!string.IsNullOrEmpty(hdfBirthPlaceWardId.Text))
                cbxBirthPlaceWard.Disabled = false;
            if(!string.IsNullOrEmpty(hdfHometownDistrictId.Text))
                cbxHometownDistrict.Disabled = false;
            if(!string.IsNullOrEmpty(hdfHometownWardId.Text))
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
            txtVYUJoinedPlace.Text = hs.VYUJoinedPlace;
            cbxBloodGroup.SetValue(hs.BloodGroup);
            txtHeight.SetValue(hs.Height);
            txtWeight.SetValue(hs.Weight);
            txtRankWounded.Text = hs.RankWounded;
            txtIDNumber.SetValue(hs.IDNumber);
            IDIssueDate.SetValue(hs.IDIssueDate);
            txtInsuranceNumber.Text = hs.InsuranceNumber;
            InsuranceIssueDate.SetValue(hs.InsuranceIssueDate);
            txtContactAddress.Text = hs.ContactAddress;
            txtContactPersonName.Text = hs.ContactPersonName;
            txtContactPhoneNumber.Text = hs.ContactPhoneNumber;
            txtContactRelation.Text = hs.ContactRelation;
            cbxInputIndustry.Text = hs.IndustryName;
            hdfInputIndustryId.Text = hs.IndustryId.ToString();
            hdfImagePerson.Text = hs.ImageUrl;

            //Thong tin to doi
            hdfConstructionId.Text = hs.ConstructionId.ToString();
            cbxConstruction.Text = cat_ConstructionServices.GetFieldValueById(hs.ConstructionId, "Name");
            hdfTeamId.Text = hs.TeamId.ToString();
            cbxTeam.Text = cat_TeamServices.GetFieldValueById(hs.TeamId, "Name");
            txtStudyWorkingDay.Text = hs.StudyWorkingDay.ToString();
            txtGraduationYear.Text = hs.GraduationYear.ToString();

            //BHYT
            txtHealthInsuranceNumber.Text = hs.HealthInsuranceNumber;
            dfHealthJoinedDate.SetValue(hs.HealthJoinedDate);
            dfHealthExpiredDate.SetValue(hs.HealthExpiredDate);

            //Xếp loại
            hdfGraduationTypeId.Text = hs.GraduationTypeId.ToString();
            cbxGraduationType.Text = cat_GraduationTypeServices.GetFieldValueById(hs.GraduationTypeId, "Name");
            hdfUniversityId.Text = hs.UniversityId.ToString();
            cbxUniversity.Text = cat_UniversityServices.GetFieldValueById(hs.UniversityId, "Name");
            dfUnionJoinedDate.SetValue(hs.UnionJoinedDate);
            hdfUnionPosition.Text = hs.UnionJoinedPositionId.ToString();
            cboUnionPosition.Text = cat_PositionServices.GetFieldValueById(hs.UnionJoinedPositionId);

            //Thoi gian thu viec
            txtProbationWorkingTime.Text = hs.ProbationWorkingTime.ToString();
            hdfWorkingFormId.Text = hs.WorkingFormId.ToString();
            cbxWorkingForm.Text = ((WorkingFormType)Enum.Parse(typeof(WorkingFormType), hdfWorkingFormId.Text)).Description();
            hdfWorkLocationId.Text = hs.WorkLocationId.ToString();
            cbxWorkLocation.Text = cat_WorkLocationServices.GetFieldValueById(hs.WorkLocationId, "Name");

            // Tuyển dụng
            if (int.TryParse(hdfCandidateId.Text, out var result) && result > 0)
            {
                var candidate = CandidateController.GetById(result);
                if (candidate != null)
                {
                    txtCandidateCode.Text = candidate.Code;
                    hdfRequiredRecruitmentId.Text = candidate.RequiredRecruitmentId.ToString();
                    cboRequiredRecruitment.Text = candidate.RequiredRecruitmentName;
                    hdfCandidateStatus.Text = candidate.Status.ToString();
                    cboCandidateStatus.Text = candidate.StatusName;
                    txtDesiredSalary.SetValue(candidate.DesiredSalary);
                    dfApplyDate.SetValue(candidate.ApplyDate);
                }
            }
            //show window
            hdfEven.Text = "";
            
            wdInput.Show();
            //Quan he gia dinh
            GridPanelFamilyRelationship.Reload();
        }

        /// <summary>
        /// 
        /// </summary>
        [DirectMethod]
        public void ResetInputCategory()
        {
            btnCancel.Hide();
            btnSave.Hide();
            txtTenDM.Hide();
            txtTenDM.Reset();
            btnAddCategory.Disabled = false;
        }

        /// <summary>
        /// 
        /// </summary>
        [DirectMethod]
        public void ResetInputCategoryGroup()
        {
            txtTenDMGroup.Reset();
            txtTenDMGroup.Hide();
            cboCategoryGroup.Reset();
            cboCategoryGroup.Hide();
            btnSaveGroupCategory.Hide();
            btnCancelGroupCategory.Hide();
            btnAddCategoryGroup.Disabled = false;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        public void InitWindowInput(RecordType type)
        {
            switch (type)
            {
                case RecordType.Default:
                    //insert
                    txtEmployeeCode.Text = GenerateEmployeeCode();
                    wdInput.SetTitle("Nhập thông tin hồ sơ");
                    hdfType.Text = @"0";
                    break;
                case RecordType.Candidate:
                    txtEmployeeCode.Hidden = true;
                    txtCandidateCode.Hidden = false;
                    panelJob.Disabled = true;
                    panelPolitic.Disabled = true;
                    panelEmployeeInsurance.Disabled = true;
                    ctnCandidate.Hidden = false;
                    wdInput.SetTitle("Nhập thông tin ứng viên");
                    hdfType.Text = @"1";
                    break;
            }
            //show window
            wdInput.Show();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Sinh mã cán bộ dựa vào cấu hình hệ thống:
        /// -   Tiền tố của mã cán bộ
        /// -   Số lượng chữ số theo sau tiền tố
        /// </summary>
        /// <returns>Mã cán bộ mới được sinh ra</returns>
        private string GenerateEmployeeCode()
        {
            var departments = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
            var prefix = SystemConfigController.GetValueByNameFollowDepartment(SystemConfigParameter.PREFIX, departments);
            var numberCharacter = SystemConfigController.GetValueByNameFollowDepartment(SystemConfigParameter.NUMBER_OF_CHARACTER, departments);
            if(string.IsNullOrEmpty(prefix))
                prefix = "CB";
            var number = string.IsNullOrEmpty(numberCharacter) ? 5 : int.Parse(numberCharacter);
            //get record
            var record = RecordController.GetByEmployeeCodeGenerate(prefix, number);
            var oldMaCb = GenerateEmployeeConst;
            if(record != null && !string.IsNullOrEmpty(record.EmployeeCode))
                oldMaCb = record.EmployeeCode;
            var oldNumber = long.Parse("" + oldMaCb.Substring(prefix.Length));
            oldNumber++;
            var newMaCb = GenerateEmployeeConst + oldNumber;
            newMaCb = prefix + newMaCb.Substring(newMaCb.Length - number);
            return newMaCb;
        }

        #endregion

        #region Store

        #region Quan hệ gia đình

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void StoreFamilyRelationship_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            if(!string.IsNullOrEmpty(hdfRecordId.Text))
            {
                storeFamilyRelationship.DataSource = FamilyRelationshipController.GetAll(Convert.ToInt32(hdfRecordId.Text));
                storeFamilyRelationship.DataBind();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void HandleChangesFamilyRelationship(object sender, BeforeStoreChangedEventArgs e)
        {
            var familyRelationshipModel = e.DataHandler.ObjectData<FamilyRelationshipModel>();
            // insert
            foreach(var created in familyRelationshipModel.Created)
            {
                var info = new hr_FamilyRelationship { RecordId = Convert.ToInt32(hdfRecordId.Text) };
                EditFamilyRelationship(info, created);

                new FamilyRelationshipController().Insert(info);
            }
            // update
            foreach(var updated in familyRelationshipModel.Updated)
            {
                var info = new hr_FamilyRelationship();
                EditFamilyRelationship(info, updated);
                new FamilyRelationshipController().Update(info);
            }
            // delete
            foreach(var deleted in familyRelationshipModel.Deleted)
            {
                new FamilyRelationshipController().Delete(deleted.Id);
            }
        }

        /// <summary>
        /// Edit data before save DB
        /// </summary>
        /// <param name="info"></param>
        /// <param name="familyModel"></param>
        private void EditFamilyRelationship(hr_FamilyRelationship info, FamilyRelationshipModel familyModel)
        {
            info.FullName = familyModel.FullName;
            info.BirthYear = familyModel.BirthYear;
            info.RelationshipId = familyModel.RelationshipId;
            info.Occupation = familyModel.Occupation;
            info.WorkPlace = familyModel.WorkPlace;
            info.Sex = familyModel.Sex;
            info.Note = familyModel.Note;
            info.IsDependent = familyModel.IsDependent;
            info.IDNumber = familyModel.IDNumber;
        }

        /// <summary>
        /// Save DB
        /// </summary>
        /// <param name="json"></param>
        /// <param name="recordId"></param>
        private void SaveGridFamilyRelationship(string json, int recordId)
        {
            if(!string.IsNullOrEmpty(json))
            {
                var controller = new FamilyRelationshipController();
                var rs = Ext.Net.JSON.Deserialize<List<FamilyRelationshipModel>>(json);
                foreach(var item in rs)
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
                    if(!string.IsNullOrEmpty(item.SexName) && item.SexName == "M")
                    {
                        obj.Sex = true;
                    }
                    else
                    {
                        obj.Sex = false;
                    }
                    obj.RecordId = recordId;
                    if(obj.Id > 0)
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
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void storeWorkingForm_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            storeWorkingForm.DataSource = typeof(WorkingFormType).GetIntAndDescription();
            storeWorkingForm.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void storeGroup_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            if(!string.IsNullOrEmpty(hdfCurrentCatalogGroupName.Text))
            {
                storeGroup.DataSource = EnumHelper.GetCatalogGroupItems(hdfCurrentCatalogGroupName.Text.ToLower());
                storeGroup.DataBind();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void storeCandidateStatus_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            storeCandidateStatus.DataSource = typeof(CandidateType).GetIntAndDescription();
            storeCandidateStatus.DataBind();
        }
        #endregion

    }
}

