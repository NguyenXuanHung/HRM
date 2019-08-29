using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Ext.Net;
using SoftCore;
using Web.Core;
using Web.Core.Framework;
using Web.Core.Framework.Common;
using Web.Core.Helper;
using Web.Core.Object.HumanRecord;
using Web.Core.Object.Security;
using Web.Core.Service.Catalog;
using Web.Core.Service.HumanRecord;

namespace Web.HRM.Modules.ProfileHuman
{
    public partial class EmployeeEnterprise : BasePage
    {
        // TODO : ???
        private const string _generateEmployeeConst = "00000000000000000000";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ExtNet.IsAjaxRequest)
            {
                //init
                InitController();

                if (Request.QueryString["Event"] != "Edit") return;
                btnEdit_Click();
                btnUpdateClose.Hide();
                btnUpdate.Hide();
                btnUpdateEdit.Show();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitController()
        {
            // init department
            storeDepartment.DataSource = CurrentUser.DepartmentsTree;
            storeDepartment.DataBind();

            // init employee type
            storeEmployeeType.DataSource = CatalogController.GetAll("cat_EmployeeType", null, null, null, false, null, null);
            storeEmployeeType.DataBind();

            //init generate employeeCode
            txtEmployeeCode.Text = GenerateEmployeeCode();
            wdEmployeeEnterprise.Show();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveEmployee_Click(object sender, DirectEventArgs e)
        {
            var recordController = new RecordController();
            var hs = new RecordModel();

            if (!string.IsNullOrEmpty(hdfPrKeyHoSo.Text))
            {
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
            if (!string.IsNullOrEmpty(hdfAnhDaiDien.Text))
                hs.ImageUrl = hdfAnhDaiDien.Text;
            hs.FullName = txtFullName.Text;
            // lấy họ và đệm từ họ tên
            var position = hs.FullName.LastIndexOf(' ');
            hs.Name = position == -1 ? hs.FullName : hs.FullName.Substring(position + 1).Trim();

            hs.Alias = txtAlias.Text;
            if (!DatetimeHelper.IsNull(dfBirthDate.SelectedDate))
                hs.BirthDate = dfBirthDate.SelectedDate;
            if (!string.IsNullOrEmpty(cbxSex.SelectedItem.Value) && cbxSex.SelectedItem.Value == "M")
            {
                hs.Sex = true;
            }
            else
            {
                hs.Sex = false;
            }
            if (string.IsNullOrEmpty(cbxBasicEducation.Text))
            {
                hdfBasicEducationId.Text = @"0";
            }
            if (string.IsNullOrEmpty(cbxEducation.Text))
            {
                hdfEducationId.Text = @"0";
            }
            if (string.IsNullOrEmpty(cbxInputIndustry.Text))
            {
                hdfInputIndustryId.Text = @"0";
            }
            if (string.IsNullOrEmpty(cbxITLevel.Text))
            {
                hdfITLevelId.Text = @"0";
            }
            if (string.IsNullOrEmpty(cbxLanguageLevel.Text))
            {
                hdfLanguageLevelId.Text = @"0";
            }
            if (string.IsNullOrEmpty(cbxPosition.Text))
            {
                hdfPositionId.Text = @"0";
            }
            if (string.IsNullOrEmpty(cbxJobTitle.Text))
            {
                hdfJobTitleId.Text = @"0";

            }
            if (!string.IsNullOrEmpty(hdfBirthPlaceProvinceId.Text))
                hs.BirthPlaceProvinceId = Convert.ToInt32(hdfBirthPlaceProvinceId.Text);
            if (string.IsNullOrEmpty(cbxBirthPlaceDistrict.Text))
                hdfBirthPlaceDistrictId.Text = @"0";
            if (!string.IsNullOrEmpty(hdfBirthPlaceDistrictId.Text))
                hs.BirthPlaceDistrictId = Convert.ToInt32(hdfBirthPlaceDistrictId.Text);
            if (string.IsNullOrEmpty(cbxBirthPlaceWard.Text))
                hdfBirthPlaceWardId.Text = @"0";
            if (!string.IsNullOrEmpty(hdfBirthPlaceWardId.Text))
                hs.BirthPlaceWardId = Convert.ToInt32(hdfBirthPlaceWardId.Text);
            if (!string.IsNullOrEmpty(hdfHometownProvinceId.Text))
                hs.HometownProvinceId = Convert.ToInt32(hdfHometownProvinceId.Text);
            if (string.IsNullOrEmpty(cbxHometownDistrict.Text))
                hdfHometownDistrictId.Text = @"0";
            if (!string.IsNullOrEmpty(hdfHometownDistrictId.Text))
                hs.HometownDistrictId = Convert.ToInt32(hdfHometownDistrictId.Text);
            if (string.IsNullOrEmpty(cbxHometownWard.Text))
                hdfHometownWardId.Text = @"0";
            if (!string.IsNullOrEmpty(hdfHometownWardId.Text))
                hs.HometownWardId = Convert.ToInt32(hdfHometownWardId.Text);
            if (!string.IsNullOrEmpty(hdfMaritalStatusId.Text))
                hs.MaritalStatusId = Convert.ToInt32(hdfMaritalStatusId.Text);
            if (!string.IsNullOrEmpty(hdfFolkId.Text))
                hs.FolkId = Convert.ToInt32(hdfFolkId.Text);
            if (!string.IsNullOrEmpty(hdfReligionId.Text))
                hs.ReligionId = Convert.ToInt32(hdfReligionId.Text);
            if (!string.IsNullOrEmpty(hdfPersonalClassId.Text))
                hs.PersonalClassId = Convert.ToInt32(hdfPersonalClassId.Text);
            if (!string.IsNullOrEmpty(hdfFamilyClassId.Text))
                hs.FamilyClassId = Convert.ToInt32(hdfFamilyClassId.Text);
            hs.ResidentPlace = txt_ResidentPlace.Text;
            hs.Address = txt_Address.Text;
            hs.CPVCardNumber = txtCPVCardNumber.Text;
            if (!string.IsNullOrEmpty(hdfJobTitleId.Text))
                hs.JobTitleId = Convert.ToInt32(hdfJobTitleId.Text);
            //Ngay thu viec
            if (!DatetimeHelper.IsNull(RecruimentDate.SelectedDate))
                hs.RecruimentDate = RecruimentDate.SelectedDate;
            //Ngay chinh thuc
            if (!DatetimeHelper.IsNull(ParticipationDate.SelectedDate))
                hs.ParticipationDate = ParticipationDate.SelectedDate;
            hs.RecruimentDepartment = txtRecruitmentDepartment.Text;
            if (!string.IsNullOrEmpty(hdfBasicEducationId.Text))
                hs.BasicEducationId = Convert.ToInt32(hdfBasicEducationId.Text);
            if (!string.IsNullOrEmpty(hdfEducationId.Text))
                hs.EducationId = Convert.ToInt32(hdfEducationId.Text);
            if (!string.IsNullOrEmpty(hdfPoliticLevelId.Text))
                hs.PoliticLevelId = Convert.ToInt32(hdfPoliticLevelId.Text);
            if (!string.IsNullOrEmpty(hdfLanguageLevelId.Text))
                hs.LanguageLevelId = Convert.ToInt32(hdfLanguageLevelId.Text);
            if (!string.IsNullOrEmpty(hdfITLevelId.Text))
                hs.ITLevelId = Convert.ToInt32(hdfITLevelId.Text);
            if (!DatetimeHelper.IsNull(CPVJoinedDate.SelectedDate))
                hs.CPVJoinedDate = CPVJoinedDate.SelectedDate;
            if (!DatetimeHelper.IsNull(CPVOfficialJoinedDate.SelectedDate))
                hs.CPVOfficialJoinedDate = CPVOfficialJoinedDate.SelectedDate;
            hs.CPVJoinedPlace = txtCPVJoinedPlace.Text;
            if (!string.IsNullOrEmpty(hdfCPVPositionId.Text))
                hs.CPVPositionId = Convert.ToInt32(hdfCPVPositionId.Text);
            if (!string.IsNullOrEmpty(hdfVYUPositionId.Text))
                hs.VYUPositionId = Convert.ToInt32(hdfVYUPositionId.Text);
            if (!DatetimeHelper.IsNull(ArmyJoinedDate.SelectedDate))
                hs.ArmyJoinedDate = ArmyJoinedDate.SelectedDate;
            if (!DatetimeHelper.IsNull(ArmyLeftDate.SelectedDate))
                hs.ArmyLeftDate = ArmyLeftDate.SelectedDate;
            if (!string.IsNullOrEmpty(hdfPositionId.Text))
                hs.PositionId = Convert.ToInt32(hdfPositionId.Text);
            if (!string.IsNullOrEmpty(hdfArmyLevelId.Text))
                hs.ArmyLevelId = Convert.ToInt32(hdfArmyLevelId.Text);
            if (!string.IsNullOrEmpty(hdfHealthStatusId.Text))
                hs.HealthStatusId = Convert.ToInt32(hdfHealthStatusId.Text);
            hs.BloodGroup = cbxBloodGroup.SelectedIndex >= 0 ? cbxBloodGroup.SelectedItem.Value : "";
            if (!string.IsNullOrEmpty(txtHeight.Text))
                hs.Height = Convert.ToDecimal(txtHeight.Text);
            if (!string.IsNullOrEmpty(txtWeight.Text))
                hs.Weight = Convert.ToDecimal(txtWeight.Text);
            if (!string.IsNullOrEmpty(hdfDepartmentId.Text))
                hs.DepartmentId = Convert.ToInt32(hdfDepartmentId.Text);
            hs.RankWounded = txtRankWounded.Text;
            if (!string.IsNullOrEmpty(hdfFamilyPolicyId.Text))
                hs.FamilyPolicyId = Convert.ToInt32(hdfFamilyPolicyId.Text);
            if (!string.IsNullOrEmpty(txtIDNumber.Text))
                hs.IDNumber = txtIDNumber.Text;
            if (!DatetimeHelper.IsNull(IDIssueDate.SelectedDate))
                hs.IDIssueDate = IDIssueDate.SelectedDate;
            if (!string.IsNullOrEmpty(hdfIDIssuePlaceId.Text))
                hs.IDIssuePlaceId = Convert.ToInt32(hdfIDIssuePlaceId.Text);
            if (!DatetimeHelper.IsNull(VYUJoinedDate.SelectedDate))
                hs.VYUJoinedDate = VYUJoinedDate.SelectedDate;
            hs.VYUJoinedPlace = txtVYUJoinedPlace.Text;
            hs.PersonalTaxCode = txtPersonalTaxCode.Text;
            if (!string.IsNullOrEmpty(hdfWorkStatusId.Text))
                hs.WorkStatusId = Convert.ToInt32(hdfWorkStatusId.Text);
            var workStatus = CatalogController.GetAll("cat_WorkStatus", null, ((int) RecordStatus.Working).ToString(),
                null, false, null, 1);
            if (workStatus.Count > 0)
            {
                hs.WorkStatusId = workStatus.First().Id;
            }
             
            hs.WorkEmail = txtWorkEmail.Text;
            hs.PersonalEmail = txtPersonalEmail.Text;
            hs.CellPhoneNumber = txtCellPhoneNumber.Text;
            hs.HomePhoneNumber = txtHomePhoneNumber.Text;
            hs.WorkPhoneNumber = txtWorkPhoneNumber.Text;
            hs.InsuranceNumber = txtInsuranceNumber.Text;
            hs.ContactPersonName = txtContactPersonName.Text;
            hs.ContactPhoneNumber = txtContactPhoneNumber.Text;
            hs.ContactRelation = txtContactRelation.Text;
            hs.ContactAddress = txtContactAddress.Text;
            if (!string.IsNullOrEmpty(hdfInputIndustryId.Text))
                hs.IndustryId = Convert.ToInt32(hdfInputIndustryId.Text);
            if (!DatetimeHelper.IsNull(InsuranceIssueDate.SelectedDate))
            {
                hs.InsuranceIssueDate = InsuranceIssueDate.SelectedDate;
            }
            
            if (Request.QueryString["Event"] == "Edit" || !string.IsNullOrEmpty(hdfPrKeyHoSo.Text))
            {
                //#region Update Ho so
                hs.Id = Convert.ToInt32(hdfPrKeyHoSo.Text);
                RecordController.Update(hs);
                var team = hr_TeamServices.GetByRecordId(hs.Id);
                if (team != null)
                {
                    team.RecordId = hs.Id;
                    EditDataFromHrTeam(team);
                    team.EditedDate = DateTime.Now;
                    hr_TeamServices.Update(team);
                }
                Dialog.ShowNotification("Cập nhật dữ liệu thành công!");
                //#endregion
            }
            else
            {
                #region Insert Ho so

                var resultModel = RecordController.Create(hs);
                //set 
                hs = resultModel;
                hdfPrKeyHoSo.Text = hs.Id.ToString();
                //insert info team
                var team = new hr_Team { RecordId = hs.Id };
                EditDataFromHrTeam(team);
                team.CreatedDate = DateTime.Now;
                hr_TeamServices.Create(team);
                Dialog.ShowNotification("Thêm mới thành công!");
                if (e.ExtraParams["Reset"] == "True")
                {
                    RM.RegisterClientScriptBlock("resetModeNew", "ResetForm();");
                }

                #endregion
            }
            // Save family
            SaveGridFamilyRelationship(e.ExtraParams["jsonFamilyRelationship"], hs.Id);
            if (e.ExtraParams["Reset"] == "True")
            {
                RM.RegisterClientScriptBlock("ClearForm", "ResetForm();");
                hdfPrKeyHoSo.Text = "";
                GridPanelFamilyRelationship.Reload();

            }
            if (e.ExtraParams["Close"] == "True")
            {
                wdEmployeeEnterprise.Hide();
            }
        }

        /// <summary>
        /// edit data before save DB
        /// </summary>
        /// <param name="team"></param>
        private void EditDataFromHrTeam(hr_Team team)
        {
            if (!string.IsNullOrEmpty(hdfConstructionId.Text))
                team.ConstructionId = Convert.ToInt32(hdfConstructionId.Text);
            if (!string.IsNullOrEmpty(hdfTeamId.Text))
                team.TeamId = Convert.ToInt32(hdfTeamId.Text);
            if (!string.IsNullOrEmpty(txtStudyWorkingDay.Text))
                team.StudyWorkingDay = Convert.ToInt32(txtStudyWorkingDay.Text);
            //Thoi gian thu viec
            if (!string.IsNullOrEmpty(txtProbationWorkingTime.Text))
                team.ProbationWorkingTime = Convert.ToInt32(txtProbationWorkingTime.Text);
            if (!string.IsNullOrEmpty(hdfWorkingFormId.Text))
                team.WorkingFormId = Convert.ToInt32(hdfWorkingFormId.Text);
            if (!string.IsNullOrEmpty(hdfWorkLocationId.Text))
                team.WorkLocationId = Convert.ToInt32(hdfWorkLocationId.Text);
            if (!string.IsNullOrEmpty(txtGraduationYear.Text))
                team.GraduationYear = Convert.ToInt32(txtGraduationYear.Text);
            if (!string.IsNullOrEmpty(hdfGraduationTypeId.Text))
                team.GraduationTypeId = Convert.ToInt32(hdfGraduationTypeId.Text);
            if (!string.IsNullOrEmpty(hdfUniversityId.Text))
                team.UniversityId = Convert.ToInt32(hdfUniversityId.Text);
            if (!DatetimeHelper.IsNull(dfUnionJoinedDate.SelectedDate))
                team.UnionJoinedDate = dfUnionJoinedDate.SelectedDate;
            else
                team.UnionJoinedDate = null;
            team.UnionJoinedPosition = txtUnionJoinedPosition.Text;
            //BHYT
            team.HealthInsuranceNumber = txtHealthInsuranceNumber.Text;
            if (!DatetimeHelper.IsNull(dfHealthJoinedDate.SelectedDate))
                team.HealthJoinedDate = dfHealthJoinedDate.SelectedDate;
            else
                team.HealthJoinedDate = null;
            if (!DatetimeHelper.IsNull(dfHealthExpiredDate.SelectedDate))
                team.HealthExpiredDate = dfHealthExpiredDate.SelectedDate;
            else
                team.HealthExpiredDate = null;
        }

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
            if (string.IsNullOrEmpty(prefix))
                prefix = "CB";
            var number = string.IsNullOrEmpty(numberCharacter) ? 5 : int.Parse(numberCharacter);

            var record = RecordController.GetByEmployeeCodeGenerate(prefix, number);
            var oldMaCb = _generateEmployeeConst;
            if (record != null && !string.IsNullOrEmpty(record.EmployeeCode))
                oldMaCb = record.EmployeeCode;
            var oldNumber = long.Parse("" + oldMaCb.Substring(prefix.Length));
            oldNumber++;
            var newMaCb = _generateEmployeeConst + oldNumber;
            newMaCb = prefix + newMaCb.Substring(newMaCb.Length - number);
            return newMaCb;
        }

        #region save and click

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, DirectEventArgs e)
        {
            var model = new CatalogModel (hdfTableDM.Text)
            {
                Name = txtTenDM.Text
            };
            CatalogController.Create(hdfTableDM.Text, model);
            RM.RegisterClientScriptBlock("add", "resetInputCategory();");
            grpCategory.Reload();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_ClickGroup(object sender, DirectEventArgs e)
        {
            if (string.IsNullOrEmpty(hdfGroup.Text))
            {
                Dialog.ShowError("Bạn chưa chọn loại danh mục");
                return;
            }
            var model = new CatalogModel (hdfTableDM.Text)
            {
                Name = txtTenDMGroup.Text,
                Group = hdfGroup.Text
            };
            CatalogController.Create(hdfCurrentCatalogName.Text, model);
            RM.RegisterClientScriptBlock("add", "resetInputCategoryGroup();");
            grpCategoryGroup.Reload();
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAccept_Click(object sender, DirectEventArgs e)
        {

            //upload file
            if (fufUploadControl.HasFile == false && fufUploadControl.PostedFile.ContentLength > 2000000)
            {
                Dialog.ShowNotification("File không được lớn hơn 200kb");
                return;
            }
            try
            {
                hdfAnhDaiDien.Text = UploadFileAndDisplay(fufUploadControl, Constant.PathLocationImageEmployee);

            }
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
            wdUploadImageWindow.Hide();

            //Hiển thị lại ảnh sau khi đã cập nhật xong
            img_anhdaidien.ImageUrl = Constant.PathLocationImageEmployee + "/" + hdfAnhDaiDien.Text;
        }

        /// <summary>
        /// 
        /// </summary>
        protected void btnEdit_Click()
        {
            hdfPrKeyHoSo.Text = Request.QueryString["PrKeyHoSo"];
            var hs = RecordController.GetById(Convert.ToInt32(hdfPrKeyHoSo.Text));
            if (hs == null) return;
            // Ho so nhan su
            if (!string.IsNullOrEmpty(hs.ImageUrl))
                img_anhdaidien.ImageUrl = Constant.PathLocationImageEmployee + @"/" + hs.ImageUrl;
            txtEmployeeCode.Text = hs.EmployeeCode;
            txtFullName.Text = hs.FullName;
            txtAlias.Text = hs.Alias;
            cboDepartment.Text = hs.DepartmentName;
            hdfDepartmentId.Text = hs.DepartmentId.ToString();
            dfBirthDate.SetValue(hs.BirthDateVn);
            cbxSex.Text = hs.SexName == "Nam" ? @"M" : @"F";

            cboMaritalStatus.Text = hs.MaritalStatusName;
            hdfMaritalStatusId.Text = hs.MaritalStatusId.ToString();
            cbxPersonalClass.Text = hs.PersonalClassName;
            hdfPersonalClassId.Text = hs.PersonalClassId.ToString();
            cbxFamilyClass.Text = hs.FamilyClassName;
            hdfFamilyClassId.Text = hs.FamilyClassId.ToString();
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
            cbxWorkStatus.Text = hs.WorkStatusName;
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
            hdfAnhDaiDien.Text = hs.ImageUrl;

            //Thong tin to doi
            var team = hr_TeamServices.GetByRecordId(hs.Id);
            if (team != null)
            {
                hdfConstructionId.Text = team.ConstructionId.ToString();
                cbxConstruction.Text = cat_ConstructionServices.GetFieldValueById(team.ConstructionId, "Name");
                hdfTeamId.Text = team.TeamId.ToString();
                cbxTeam.Text = cat_TeamServices.GetFieldValueById(team.TeamId, "Name");
                txtStudyWorkingDay.Text = team.StudyWorkingDay.ToString();
                txtGraduationYear.Text = team.GraduationYear.ToString();

                //BHYT
                txtHealthInsuranceNumber.Text = team.HealthInsuranceNumber;
                dfHealthJoinedDate.SetValue(team.HealthJoinedDate);
                dfHealthExpiredDate.SetValue(team.HealthExpiredDate);

                //Xếp loại
                hdfGraduationTypeId.Text = team.GraduationTypeId.ToString();
                cbxGraduationType.Text = cat_GraduationTypeServices.GetFieldValueById(team.GraduationTypeId, "Name");
                hdfUniversityId.Text = team.UniversityId.ToString();
                cbxUniversity.Text = cat_UniversityServices.GetFieldValueById(team.UniversityId, "Name");
                dfUnionJoinedDate.SetValue(team.UnionJoinedDate);
                txtUnionJoinedPosition.Text = team.UnionJoinedPosition;
                //Thoi gian thu viec
                txtProbationWorkingTime.Text = team.ProbationWorkingTime.ToString();
                hdfWorkingFormId.Text = team.WorkingFormId.ToString();
                cbxWorkingForm.Text = ((WorkingFormType) Enum.Parse(typeof(WorkingFormType), hdfWorkingFormId.Text)).Description();
                hdfWorkLocationId.Text = team.WorkLocationId.ToString();
                cbxWorkLocation.Text = cat_WorkLocationServices.GetFieldValueById(team.WorkLocationId, "Name");
            }

            //Quan he gia dinh
            GridPanelFamilyRelationship.Reload();
        }

        #region Quan hệ gia đình

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void StoreFamilyRelationship_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            if (!string.IsNullOrEmpty(hdfPrKeyHoSo.Text))
            {
                storeFamilyRelationship.DataSource = FamilyRelationshipController.GetAll(Convert.ToInt32(hdfPrKeyHoSo.Text));
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
            foreach (var created in familyRelationshipModel.Created)
            {
                var info = new hr_FamilyRelationship {RecordId = Convert.ToInt32(hdfPrKeyHoSo.Text)};
                EditFamilyRelationship(info, created);

                new FamilyRelationshipController().Insert(info);
            }
            // update
            foreach (var updated in familyRelationshipModel.Updated)
            {
                var info = new hr_FamilyRelationship();
                EditFamilyRelationship(info, updated);
                new FamilyRelationshipController().Update(info);
            }
            // delete
            foreach (var deleted in familyRelationshipModel.Deleted)
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
            if (!string.IsNullOrEmpty(hdfCurrentCatalogGroupName.Text))
            {
                storeGroup.DataSource = EnumHelper.GetCatalogGroupItems(hdfCurrentCatalogGroupName.Text.ToLower());
                storeGroup.DataBind();
            }
        }
    }
}