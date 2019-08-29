using System;
using System.Collections.Generic;
using System.Linq;
using Ext.Net;
using Web.Core.Framework;
using Web.Core.Service.HumanRecord;
using Web.Core.Service.Catalog;
using Web.Core;
using Web.Core.Framework.Common;
using Web.Core.Helper;
using Web.Core.Object.Salary;
using Web.Core.Object.Security;

namespace Web.HRM.Modules.ProfileHuman.DecisionSalary
{
    public partial class DecisionSalaryEnterprise : BasePage
    {
        private const string NameCondition = "QDL_%";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ExtNet.IsAjaxRequest)
            {
                HideColumn();
                hdfDepartments.Text = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
                new Core.Framework.Common.BorderLayout()
                {
                    menuID = MenuId,
                    script = "#{hdfDepartmentSelectedId}.setValue('" + Core.Framework.Common.BorderLayout.nodeID +
                             "');storeSalaryEnterprise.reload();",
                }.AddDepartmentList(br, CurrentUser, true);
            }

            if (btnEdit.Visible)
            {
                gridDecisionSalaryEnterprise.DirectEvents.RowDblClick.Event +=
                    new ComponentDirectEvent.DirectEventHandler(btnEdit_Click);
                gridDecisionSalaryEnterprise.DirectEvents.RowDblClick.Before +=
                    "#{hdfButtonClick}.setValue('Edit');#{btnUpdate}.hide();#{btnUpdateEdit}.show();#{btnUpdateClose}.hide();";
                gridDecisionSalaryEnterprise.DirectEvents.RowDblClick.EventMask.ShowMask = true;
            }

            ucChooseEmployee1.AfterClickAcceptButton += new EventHandler(ucChooseEmployee1_AfterClickAcceptButton);
        }

        #region cấu hình cột cho gridpanel

        /// <summary>
        /// Ẩn cột
        /// </summary>
        private void HideColumn()
        {
            try
            {
                var departments = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
                var list = SystemConfigController.GetAllByNameAndDepartment(NameCondition, departments);
                var columnList = new List<string>();
                foreach (var item in list)
                {
                    if (string.IsNullOrEmpty(item.Value) || (!string.IsNullOrEmpty(item.Value) && bool.Parse(item.Value)))
                    {
                        columnList.Add(item.Name);
                    }
                }

                foreach (var item in gridDecisionSalaryEnterprise.ColumnModel.Columns)
                {
                    if (columnList.Contains(item.ColumnID))
                    {
                        item.Hidden = true;
                    }
                }
            }
            catch (Exception ex)
            {
                X.Msg.Alert("Thông báo từ hệ thống", "Có lỗi xảy ra: " + ex.Message).Show();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [DirectMethod]
        public void LoadConfigGridPanel()
        {
            try
            {
                var departments = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
                var list = SystemConfigController.GetAllByNameAndDepartment(NameCondition, departments);
                foreach (var item in list)
                {
                    switch (item.Name)
                    {
                        case SystemConfigParameter.QDL_LUONGCUNG:
                            chkLuongCung.Checked = bool.Parse(item.Value);
                            break;
                        case SystemConfigParameter.QDL_HESOLUONG:
                            chkHeSoLuong.Checked = bool.Parse(item.Value);
                            break;
                        case SystemConfigParameter.QDL_PHANTRAMHL:
                            chkPercentageSalary.Checked = bool.Parse(item.Value);
                            break;
                        case SystemConfigParameter.QDL_LUONGDONGBHXH:
                            chkLuongDongBHXH.Checked = bool.Parse(item.Value);
                            break;
                        case SystemConfigParameter.QDL_BACLUONG:
                            chkBacLuong.Checked = bool.Parse(item.Value);
                            break;
                        case SystemConfigParameter.QDL_BACLUONGNB:
                            chkBacLuongNB.Checked = bool.Parse(item.Value);
                            break;
                        case SystemConfigParameter.QDL_NGAYHL:
                            chkNgayHL.Checked = bool.Parse(item.Value);
                            break;
                        case SystemConfigParameter.QDL_NGAYHLNB:
                            chkNgayHLNB.Checked = bool.Parse(item.Value);
                            break;
                        case SystemConfigParameter.QDL_SOQD:
                            chkSoQD.Checked = bool.Parse(item.Value);
                            break;
                        case SystemConfigParameter.QDL_NGAYQD:
                            chkNgayQD.Checked = bool.Parse(item.Value);
                            break;
                        case SystemConfigParameter.QDL_NGAYHIEULUC:
                            chkNgayHieuLuc.Checked = bool.Parse(item.Value);
                            break;
                        case SystemConfigParameter.QDL_NGAYHETHIEULUC:
                            chkNgayHetHieuLuc.Checked = bool.Parse(item.Value);
                            break;
                        case SystemConfigParameter.QDL_NGUOIQD:
                            chkNguoiQD.Checked = bool.Parse(item.Value);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                X.Msg.Alert("Thông báo từ hệ thống", "Có lỗi xảy ra: " + ex.Message).Show();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdateConfig_Click(object sender, DirectEventArgs e)
        {
            try
            {
                var controller = new SystemConfigController();
                var departments = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
                // department
                var arrDepartment = departments.Split(new[] {','}, StringSplitOptions.None);
                for (var i = 0; i < arrDepartment.Length; i++)
                {
                    arrDepartment[i] = "{0}".FormatWith(arrDepartment[i]);
                }

                controller.CreateOrSave(SystemConfigParameter.QDL_LUONGCUNG, chkLuongCung.Checked.ToString(),
                    string.Join(",", arrDepartment));
                controller.CreateOrSave(SystemConfigParameter.QDL_HESOLUONG, chkHeSoLuong.Checked.ToString(),
                    string.Join(",", arrDepartment));
                controller.CreateOrSave(SystemConfigParameter.QDL_PHANTRAMHL, chkPercentageSalary.Checked.ToString(),
                    string.Join(",", arrDepartment));
                controller.CreateOrSave(SystemConfigParameter.QDL_LUONGDONGBHXH, chkLuongDongBHXH.Checked.ToString(),
                    string.Join(",", arrDepartment));
                controller.CreateOrSave(SystemConfigParameter.QDL_BACLUONG, chkBacLuong.Checked.ToString(),
                    string.Join(",", arrDepartment));
                controller.CreateOrSave(SystemConfigParameter.QDL_BACLUONGNB, chkBacLuongNB.Checked.ToString(),
                    string.Join(",", arrDepartment));
                controller.CreateOrSave(SystemConfigParameter.QDL_NGAYHL, chkNgayHL.Checked.ToString(),
                    string.Join(",", arrDepartment));
                controller.CreateOrSave(SystemConfigParameter.QDL_NGAYHLNB, chkNgayHLNB.Checked.ToString(),
                    string.Join(",", arrDepartment));
                controller.CreateOrSave(SystemConfigParameter.QDL_SOQD, chkSoQD.Checked.ToString(),
                    string.Join(",", arrDepartment));
                controller.CreateOrSave(SystemConfigParameter.QDL_NGAYQD, chkNgayQD.Checked.ToString(),
                    string.Join(",", arrDepartment));
                controller.CreateOrSave(SystemConfigParameter.QDL_NGAYHIEULUC, chkNgayHieuLuc.Checked.ToString(),
                    string.Join(",", arrDepartment));
                controller.CreateOrSave(SystemConfigParameter.QDL_NGAYHETHIEULUC, chkNgayHetHieuLuc.Checked.ToString(),
                    string.Join(",", arrDepartment));
                controller.CreateOrSave(SystemConfigParameter.QDL_NGUOIQD, chkNguoiQD.Checked.ToString(),
                    string.Join(",", arrDepartment));

                Dialog.ShowNotification("Đã lưu cấu hình");
                wdConfigGridPanel.Hide();
                Response.Redirect(Request.RawUrl);
            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Thông báo từ hệ thống", "Có lỗi xảy ra: " + ex.Message).Show();
            }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ucChooseEmployee1_AfterClickAcceptButton(object sender, EventArgs e)
        {
            foreach (var item in ucChooseEmployee1.SelectedRow)
            {
                //RecordId, EmployeeCode, FullName, DepartmentName, PositionName, SalaryBasic, SalaryContract,
                //SalaryInsurance, SalaryPayDate
                // get employee information
                if (!string.IsNullOrEmpty(item.RecordID))
                {
                    // get employee information
                    var recordModel = RecordController.GetById(Convert.ToInt32(item.RecordID));
                    // insert record to grid
                    RM.RegisterClientScriptBlock("insert" + recordModel.Id, AddRecordString(recordModel));
                }
            }
        }       

        #region Event Method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDownloadAttachFile_Click(object sender, DirectEventArgs e)
        {
            try
            {
                if (hdfRecordId.Text == "")
                {
                    X.Msg.Alert("Thông báo từ hệ thống", "Không tìm thấy quyết định lương").Show();
                }

                var hsl = SalaryDecisionController.GetById(Convert.ToInt32(hdfRecordId.Text));
                DownloadAttachFile("sal_SalaryDecision", hsl.AttachFileName);
            }
            catch (Exception ex)
            {
                X.Msg.Alert("Thông báo từ hệ thống", "Có lỗi xảy ra: " + ex.Message).Show();
            }
        }

        /// <summary>
        /// #############################################
        /// Create salary decision for employee
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdate_Click(object sender, DirectEventArgs e)
        {
            try
            {
                
                var hsl = new sal_SalaryDecision();
                // upload file
                var path = string.Empty;
                if (uploadAttachFile.HasFile)
                {
                    path = UploadFile(uploadAttachFile, Constant.PathDecisionSalary);
                }

                // decide information
                if (!string.IsNullOrEmpty(hdfChooseEmployee.Text))
                    hsl.RecordId = Convert.ToInt32(hdfChooseEmployee.Text);
                if (txtDecisionNumberNew.Visible)
                    hsl.DecisionNumber = txtDecisionNumberNew.Text;
                if (!DatetimeHelper.IsNull(DecisionDateNew.SelectedDate))
                    hsl.DecisionDate = DecisionDateNew.SelectedDate;
                if (txtDecisionNameNew.Visible)
                    hsl.Name = txtDecisionNameNew.Text;
                if (!DatetimeHelper.IsNull(EffectiveDateNew.SelectedDate))
                    hsl.EffectiveDate = EffectiveDateNew.SelectedDate;
                if (txtDecisionMakerNew.Visible)
                    hsl.SignerName = txtDecisionMakerNew.Text;
                var makerPosition = hdfIsMakerPosition.Text == @"0" ? cbxMakerPosition.Text : cbxMakerPosition.SelectedItem.Text;
                //hsl.SignerPosition = makerPosition;
                if (cbxContractTypeNew.SelectedItem.Value != null)
                {
                    hsl.ContractId = Convert.ToInt32(hdfContractTypeNew.Text);
                }

                // attach file
                hsl.AttachFileName = path != "" ? path : hdfAttachFile.Text;
                if (txtNoteNew.Visible)
                    hsl.Note = txtNoteNew.Text;
                // salary information
                //if (!string.IsNullOrEmpty(txtSalaryBasicNew.Text))
                //    hsl.SalaryBasic = double.Parse(txtSalaryBasicNew.Text);
                //if (!string.IsNullOrEmpty(txtSalaryContractNew.Text))
                //    hsl.SalaryContract = double.Parse(txtSalaryContractNew.Text);
                //if (!string.IsNullOrEmpty(txtSalaryGrossNew.Text))
                //    hsl.SalaryGross = double.Parse(txtSalaryGrossNew.Text);
                //if (!string.IsNullOrEmpty(txtSalaryNetNew.Text))
                //    hsl.SalaryNet = double.Parse(txtSalaryNetNew.Text);
                //if (!string.IsNullOrEmpty(txtPercentageSalaryNew.Text))
                //    hsl.PercentageSalary = double.Parse(txtPercentageSalaryNew.Text);
                //if (!string.IsNullOrEmpty(txtSalaryInsuranceNew.Text))
                //    hsl.SalaryInsurance = double.Parse(txtSalaryInsuranceNew.Text);
                //if (!string.IsNullOrEmpty(txtSalaryFactorNew.Text))
                //    hsl.SalaryFactor = double.Parse(txtSalaryFactorNew.Text);
                hsl.CreatedBy = CurrentUser.User.UserName;
                hsl.CreatedDate = DateTime.Now;

                if (e.ExtraParams["Command"] == "Edit")
                {
                    hsl.Id = int.Parse("0" + hdfRecordId.Text);
                    SalaryDecisionController.Update(new SalaryDecisionModel(hsl));
                    wdCreateDecisionSalary.Hide();
                }
                else
                {
                    // add salary decision
                    SalaryDecisionController.Create(new SalaryDecisionModel(hsl));
                    if (e.ExtraParams["Close"] == "True")
                    {
                        wdCreateDecisionSalary.Hide();
                    }
                }

                gridDecisionSalaryEnterprise.Reload();
            }
            catch (Exception ex)
            {
                X.Msg.Alert("Thông báo từ hệ thống", "Có lỗi xảy ra khi lưu quyết định lương: " + ex.Message).Show();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQDLDownload_Click(object sender, DirectEventArgs e)
        {
            DownloadAttachFile("sal_SalaryDecision", hdfAttachFile);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQDLDelete_Click(object sender, DirectEventArgs e)
        {
            if (hdfRecordId.Text != "")
            {
                DeleteAttackFile("sal_SalaryDecision", int.Parse("0" + hdfRecordId.Text), hdfAttachFile);
                hdfAttachFile.Text = "";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdateHL_Click(object sender, DirectEventArgs e)
        {
            try
            {
                var salaryController = new SalaryDecisionController();
                // upload file
                string path = string.Empty;
                if (cpfAttachHL.Visible && uploadAttachFileHL.HasFile)
                {
                    path = UploadFile(uploadAttachFileHL, Constant.PathDecisionSalary);
                }

                var rs = JSON.Deserialize<List<SalaryDecisionModel>>(e.ExtraParams["json"]);
                foreach (var created in rs)
                {
                    var hsl = new sal_SalaryDecision();

                    // decision information
                    if (txtDecisionNumberHL.Visible)
                        hsl.DecisionNumber = txtDecisionNumberHL.Text.Trim();
                    if (txtDecisionNameHL.Visible)
                        hsl.Name = txtDecisionNameHL.Text.Trim();
                    if (DecisionDateHL.Visible && !DatetimeHelper.IsNull(DecisionDateHL.SelectedDate))
                        hsl.DecisionDate = DecisionDateHL.SelectedDate;
                    if (EffectiveDateHL.Visible && !DatetimeHelper.IsNull(EffectiveDateHL.SelectedDate))
                        hsl.EffectiveDate = EffectiveDateHL.SelectedDate;
                    if (txtDecisionMakerHL.Visible && !string.IsNullOrEmpty(txtDecisionMakerHL.Text))
                        hsl.SignerName = txtDecisionMakerHL.Text;
                    var makerPosition = hdfIsMakerPositionHL.Text == @"0" ? cbxMakerPositionHL.Text : cbxMakerPositionHL.SelectedItem.Text;
                    //hsl.SignerPosition = makerPosition;
                    hsl.AttachFileName = path != "" ? path : hdfAttachFileHL.Text;
                    hsl.Note = txtNoteHL.Text;
                    // salary information
                    hsl.RecordId = created.RecordId;
                    hsl.QuantumId = created.QuantumId;
                    //hsl.SalaryFactor = created.SalaryFactor;
                    //hsl.SalaryBasic = created.SalaryBasic;
                    //hsl.SalaryContract = created.SalaryContract;
                    //hsl.SalaryInsurance = created.SalaryInsurance;
                    //hsl.OutFrame = created.OutFrame;
                    hsl.CreatedBy = CurrentUser.User.UserName;
                    hsl.CreatedDate = DateTime.Now;
                    SalaryDecisionController.Create(new SalaryDecisionModel(hsl));
                }

                if (e.ExtraParams["close"] == "True")
                {
                    wdCreateDecisionSalaryHL.Hide();
                }

                gridDecisionSalaryEnterprise.Reload();
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
        protected void cbxChooseEmployee_Selected(object sender, DirectEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(cbxChooseEmployee.SelectedItem.Value))
                {
                    int recordId = Convert.ToInt32(cbxChooseEmployee.SelectedItem.Value);
                    var hs = RecordController.GetById(recordId);
                    if (hs != null)
                    {
                        // set general infomation
                        txtPosition.Text = hs.PositionName;
                        txtJob.Text = hs.JobTitleName;
                        txtDepartment.Text = hs.DepartmentName;

                        // get newest salary development
                        var hsl = SalaryDecisionController.GetCurrent(recordId);
                        if (hsl != null)
                        {
                            //if (!string.IsNullOrEmpty(hsl.DecisionNumber))
                            //    txtDecisionNumberOld.Text = hsl.DecisionNumber;
                            //if (hsl.DecisionName != null)
                            //    txtDecisionNameOld.Text = hsl.DecisionName;
                            //DecisionDateOld.SetValue(hsl.DecisionDate);
                            //EffectiveDateOld.SetValue(hsl.EffectiveDate);
                            //if (hsl.SalaryBasic != null)
                            //    txtSalaryBasicOld.SetValue(hsl.SalaryBasic);
                            //if (hsl.SalaryInsurance != null)
                            //    txtSalaryInsuranceOld.SetValue(hsl.SalaryInsurance);
                            //if (hsl.OtherAllowance != null)
                            //    txtOtherAllowanceOld.SetValue(hsl.OtherAllowance);
                            //var strContractTypeId =
                            //    hr_ContractServices.GetFieldValueById(hsl.ContractId, "ContractTypeId");
                            //if (!string.IsNullOrEmpty(strContractTypeId))
                            //{
                            //    var contractTypeName =
                            //        cat_ContractTypeServices.GetFieldValueById(Convert.ToInt32(strContractTypeId),
                            //            "Name");
                            //    txtContractTypeOld.Text = "" + contractTypeName;
                            //}

                            //if (hsl.BranchAllowance != null)
                            //    txtBranchAllowanceOld.SetValue(hsl.BranchAllowance);
                        }
                        else
                        {
                            txtDecisionNumberOld.Text = "";
                            txtDecisionNameOld.Text = "";
                            DecisionDateOld.SetValue("");
                            txtDecisionMakerOld.Text = "";
                            EffectiveDateOld.SetValue("");
                            txtSalaryBasicOld.Text = "";
                            txtSalaryInsuranceOld.Text = "";
                            txtOtherAllowanceOld.Text = "";
                            txtContractTypeOld.Text = "";
                        }
                    }
                    else
                    {
                        txtPosition.Text = "";
                        txtJob.Text = "";
                        txtDepartment.Text = "";
                    }
                }

            }
            catch (Exception ex)
            {
                X.Msg.Alert("Thông báo từ hệ thống", "Có lỗi xảy ra khi chọn cán bộ: " + ex.Message).Show();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, DirectEventArgs e)
        {
            try
            {
                foreach (var item in RowSelectionModel1.SelectedRows)
                {
                    // delete salary decision
                    SalaryDecisionController.Delete(int.Parse(item.RecordID));
                }

                btnEdit.Disabled = true;
                btnDelete.Disabled = true;
                gridDecisionSalaryEnterprise.Reload();
            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Có lỗi xảy ra", ex.Message).Show();
            }
        }

        #endregion

        /// <summary>
        /// Sửa thông tin quyết định lương
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEdit_Click(object sender, DirectEventArgs e)
        {
            try
            {
                var hsLuong = SalaryDecisionController.GetById(Convert.ToInt32(hdfRecordId.Text));
                if (hsLuong == null)
                {
                    X.Msg.Alert("Thông báo từ hệ thống", "Không tìm thấy quyết định lương").Show();
                    return;
                }

                // load general information for employee
                var hs = RecordController.GetById(hsLuong.RecordId);
                if (hs != null)
                {
                    hdfChooseEmployee.Text = hs.Id.ToString();
                    cbxChooseEmployee.Text = hs.FullName;
                    txtDepartment.Text = hs.DepartmentName;
                    txtPosition.Text = hs.PositionName;
                    txtJob.Text = hs.JobTitleName;
                }

                // load newest salary decision information
                var hsl = SalaryDecisionController.GetCurrent(hsLuong.RecordId);
                if (hsl != null)
                {
                    //if (txtDecisionNumberOld.Visible == true && !string.IsNullOrEmpty(hsl.DecisionNumber))
                    //    txtDecisionNumberOld.Text = hsl.DecisionNumber;
                    //if (txtDecisionNameOld.Visible == true && !string.IsNullOrEmpty(hsl.DecisionName))
                    //    txtDecisionNameOld.Text = hsl.DecisionName;
                    //if (DecisionDateOld.Visible == true)
                    //    DecisionDateOld.SetValue(hsl.DecisionDate);
                    //if (txtDecisionMakerOld.Visible == true && hsl.DecisionMaker != null)
                    //    txtDecisionMakerOld.Text = hsl.DecisionMaker;
                    //if (EffectiveDateOld.Visible == true)
                    //    EffectiveDateOld.SetValue(hsl.EffectiveDate);
                    //if (txtSalaryBasicOld.Visible == true && hsl.SalaryBasic != null)
                    //    txtSalaryBasicOld.SetValue(hsl.SalaryBasic);
                    //txtSalaryGrossOld.SetValue(hsl.SalaryGross);
                    //txtSalaryNetOld.SetValue(hsl.SalaryNet);
                    //if (txtSalaryInsuranceOld.Visible == true && hsl.SalaryInsurance != null)
                    //    txtSalaryInsuranceOld.SetValue(hsl.SalaryInsurance);
                    //if (txtOtherAllowanceOld.Visible == true && hsl.OtherAllowance != null)
                    //    txtOtherAllowanceOld.SetValue(hsl.OtherAllowance);
                    //if (txtResponsibilityAllowanceOld.Visible == true && hsl.ResponsibilityAllowance != null)
                    //    txtResponsibilityAllowanceOld.SetValue(hsl.ResponsibilityAllowance);
                    //if (txtAreaAllowanceOld.Visible == true && hsl.AreaAllowance != null)
                    //    txtAreaAllowanceOld.SetValue(hsl.AreaAllowance);

                    var strContractTypeOldId =
                        hr_ContractServices.GetFieldValueById(hsLuong.ContractId, "ContractTypeId");
                    if (!string.IsNullOrEmpty(strContractTypeOldId))
                    {
                        var contractTypeOldName =
                            cat_ContractTypeServices.GetFieldValueById(Convert.ToInt32(strContractTypeOldId), "Name");
                        txtContractTypeOld.Text = "" + contractTypeOldName;
                    }
                }

                // load current decision information
                //if (txtDecisionNumberNew.Visible)
                //    txtDecisionNumberNew.Text = hsLuong.DecisionNumber;
                //if (DecisionDateNew.Visible)
                //    DecisionDateNew.SetValue(hsLuong.DecisionDate);
                //if (txtDecisionNameNew.Visible)
                //    txtDecisionNameNew.Text = hsLuong.DecisionName;
                //if (EffectiveDateNew.Visible)
                //    EffectiveDateNew.SetValue(hsLuong.EffectiveDate);
                //if (txtDecisionMakerNew.Visible)
                //    txtDecisionMakerNew.Text = hsLuong.DecisionMaker;
                //cbxMakerPosition.Text = hsLuong.MakerPosition;
                var strContractTypeId = hr_ContractServices.GetFieldValueById(hsLuong.ContractId, "ContractTypeId");
                if (!string.IsNullOrEmpty(strContractTypeId))
                {
                    var contractTypeName =
                        cat_ContractTypeServices.GetFieldValueById(Convert.ToInt32(strContractTypeId), "Name");
                    cbxContractTypeNew.Text = "" + contractTypeName;
                    hdfContractTypeNew.SetValue(strContractTypeId);
                }

                // them ten nguoi qd
                if (composifieldAttach.Visible)
                {
                    hdfAttachFile.Text = hsLuong.AttachFileName;
                    if (!string.IsNullOrEmpty(hsLuong.AttachFileName))
                    {
                        hdfAttachFile.Text = hsLuong.AttachFileName;
                        uploadAttachFile.Text = GetFileName(hsLuong.AttachFileName);
                    }
                }

                if (txtNoteNew.Visible)
                    txtNoteNew.Text = hsLuong.Note;
                //txtSalaryBasicNew.SetValue(hsLuong.SalaryBasic);
                //txtSalaryContractNew.SetValue(hsLuong.SalaryContract);
                //txtSalaryGrossNew.SetValue(hsLuong.SalaryGross);
                //txtSalaryNetNew.SetValue(hsLuong.SalaryNet);
                //txtSalaryInsuranceNew.SetValue(hsLuong.SalaryInsurance);
                //txtSalaryFactorNew.SetValue(hsLuong.SalaryFactor.ToString().Replace(".", ","));
                //txtPercentageSalaryNew.SetValue(hsLuong.PercentageSalary);
                //txtOtherAllowanceNew.SetValue(hsLuong.OtherAllowance);
                //txtAreaAllowanceNew.SetValue(hsLuong.AreaAllowance);
                //txtPositionAllowanceNew.SetValue(hsLuong.PositionAllowance);
                //txtResponsibilityAllowanceNew.SetValue(hsLuong.ResponsibilityAllowance);
                //txtBranchAllowanceNew.SetValue(hsLuong.BranchAllowance);
                // update form information
                wdCreateDecisionSalary.Title = @"Cập nhật thông tin quyết định lương";
                wdCreateDecisionSalary.Icon = Icon.Pencil;
                btnUpdate.Hide();
                btnUpdateEdit.Show();
                btnUpdateClose.Hide();
                wdCreateDecisionSalary.Show();
            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Có lỗi xảy ra: ", ex.Message).Show();
            }
        }

        #region Store onrefreshdata

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void storeContractType_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(hdfChooseEmployee.Text))
                {
                    var datas = ContractController.GetAllByRecordCondition(Convert.ToInt32(hdfChooseEmployee.Text));
                    if (datas.Count == 0)
                    {
                        RM.RegisterClientScriptBlock("rl1",
                            "alert('Không tìm thấy hợp đồng nào còn hiệu lực. Vui lòng tạo hợp đồng mới trước khi tạo quyết định lương!');");
                    }

                    storeContractType.DataSource = datas;
                    storeContractType.DataBind();
                }
                else
                {
                    RM.RegisterClientScriptBlock("rl", "alert('Bạn chưa chọn cán bộ'); cbxChooseEmployee.focus();");
                }
            }
            catch (Exception ex)
            {
                X.Msg.Alert("Thông báo từ hệ thống", "Có lỗi xảy ra: " + ex.Message).Show();
            }
        }

        #endregion

    }
}