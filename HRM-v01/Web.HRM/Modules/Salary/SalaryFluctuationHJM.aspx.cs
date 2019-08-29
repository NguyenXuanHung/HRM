using System;
using System.Collections.Generic;
using Ext.Net;
using Web.Core;
using Web.Core.Framework;
using Web.Core.Framework.Common;
using Web.Core.Framework.Utils;
using Web.Core.Service.Catalog;

namespace Web.HRM.Modules.Salary
{
    public partial class SalaryFluctuationHJM : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ExtNet.IsAjaxRequest)
            {
                hdfDepartmentIds.Text = DepartmentIds;
                hdfOrder.Text = Request.QueryString["order"];
                // init department
                storeDepartment.DataSource = CurrentUser.DepartmentsTree;
                storeDepartment.DataBind();
                hdfBasicSalary.Text = CatalogBasicSalaryController.GetCurrent().Value.ToString();
                new Core.Framework.Common.BorderLayout()
                {
                    menuID = MenuId,
                    script = "#{hdfDepartmentSelectedId}.setValue('" + Core.Framework.Common.BorderLayout.nodeID +
                             "');storeSalaryDecision.reload();",
                }.AddDepartmentList(brlayout, CurrentUser, true);
            }
        }
        /// <summary>
        /// Init setting window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InitWindow(object sender, DirectEventArgs e)
        {
            try
            {
                // init id
                var param = e.ExtraParams["Id"];
                // parse id
                if (int.TryParse(param, out var id))
                {
                    // init window props
                    if (id > 0)
                    {
                        // edit
                        wdSetting.Title = @"Sửa quyết định lương";
                        wdSetting.Icon = Icon.Pencil;
                    }
                    else
                    {
                        // insert
                        wdSetting.Title = @"Tạo quyết định lương";
                        wdSetting.Icon = Icon.Add;
                        
                        //ResetForm();
                    }
                    // init id
                    hdfId.Text = id.ToString();

                    // check id
                    if (id > 0)
                    {
                        //init
                        InitFormUpdate(id);
                    }
                    // set  props
                    // status
                    // show window
                    wdSetting.Show();
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
        /// <param name="id"></param>
        private void InitFormUpdate(int id)
        {
            // get current decision
            var model = SalaryDecisionController.GetById(id);
            if (model != null)
            {
                // get previous decision
                var previousDecision = SalaryDecisionController.GetPrevious(model.RecordId);

                // bind previous data
                if (previousDecision != null)
                {
                    txtCurrName.Text = previousDecision.Name;
                    txtCurrDecisionNumber.Text = previousDecision.DecisionNumber;
                    txtCurrDecisionDate.Text = previousDecision.DecisionDate.ToString("dd/MM/yyyy");
                    txtCurrSignerName.Text = previousDecision.SignerName;
                    txtCurrSignerPosition.Text = previousDecision.SignerPosition;
                    txtCurrContractTypeName.Text = previousDecision.ContractTypeName;
                    txtCurrQuantumCode.Text = previousDecision.QuantumCode;
                    txtCurrGrade.Text = previousDecision.Grade.ToString();
                    txtCurrQuantumCode.Text = previousDecision.QuantumCode;
                    txtCurrFactor.Text = previousDecision.Factor.ToString("#,##0.00");
                    txtCurrSalaryLevel.Text = previousDecision.Salary.ToString("#,###");
                    txtCurrContractSalary.Text = previousDecision.ContractSalary.ToString("#,###");
                    txtCurrInsuranceSalary.Text = previousDecision.InsuranceSalary.ToString("#,###");
                    txtCurrPercentageLeader.Text = previousDecision.PercentageLeader.ToString("0.00 %");
                }

                // bind current salary decision
                cboEmployee.Text = model.EmployeeName;
                hdfEmployee.Text = model.RecordId.ToString();
                txtName.Text = model.Name;
                txtDepartmentName.Text = model.DepartmentName;
                txtPositionName.Text = model.PositionName;
                txtJobTitleName.Text = model.JobTitleName;
                cboContractType.Text = model.ContractTypeName;
                txtDecisionNumber.Text = model.DecisionNumber;
                dfDecisionDate.SelectedDate = model.DecisionDate;
                dfEffectiveDate.SelectedDate = model.EditedDate;
                txtSignerName.Text = model.SignerName;
                hdfPosition.Text = model.SignerPositionId.ToString();
                cboPosition.Text = model.SignerPosition;
                txtNote.Text = model.Note;
                hdfQuantumId.Text = model.QuantumId.ToString();
                cboQuantum.Text = model.QuantumName;
                hdfSalaryGrade.Text = model.Grade.ToString();
                cboSalaryGrade.Text = @"Bậc " + model.Grade;
                txtFactor.Text = model.Factor.ToString("#,##0.00");
                txtSalaryLevel.Text = model.Salary.ToString("##,###");
                txtPercentageSalary.Text = model.PercentageSalary.ToString("0.00");
                txtPercentageLeader.Text = model.PercentageLeader.ToString("0.00");
                // get file upload
                if (cfAttachFile.Visible)
                {
                    hdfAttachFile.Text = model.AttachFileName;
                    if (!string.IsNullOrEmpty(model.AttachFileName))
                    {
                        hdfAttachFile.Text = model.AttachFileName;
                        fufAttachFile.Text = GetFileName(model.AttachFileName);
                    }
                }
            }
        }

        /// <summary>
        /// Init setting window many employee
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InitWindowMany(object sender, DirectEventArgs e)
        {
            try
            {
                // insert
                wdSettingMany.Title = @"Tạo quyết định lương hàng loạt";
                wdSettingMany.Icon = Icon.Add;
                ResetForm();
                // show window
                wdSettingMany.Show();
            }
            catch (Exception exception)
            {
                Dialog.ShowError(exception);
            }

        }

        /// <summary>
        /// Insert or Update decision one employee
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InsertOrUpdate(object sender, DirectEventArgs e)
        {
            try
            {
                // init model
                var model = new SalaryDecisionModel()
                {
                    EditedBy = CurrentUser.User.UserName
                };
                // check id
                if (!string.IsNullOrEmpty(hdfId.Text) && Convert.ToInt32(hdfId.Text) > 0)
                {
                    var result = SalaryDecisionController.GetById(Convert.ToInt32(hdfId.Text)); ;
                    if (result != null)
                        model = result;
                }
                // set new props for model
                model.Status = !string.IsNullOrEmpty(hdfStatus.Text) ? (SalaryDecisionStatus)Convert.ToInt32(hdfStatus.Text) : SalaryDecisionStatus.Approved;

                //edit data
                EditData(model);

                // check model id
                if (model.Id > 0)
                {
                    // update
                    SalaryDecisionController.Update(model);
                }
                else
                {
                    // set created user
                    model.CreatedBy = CurrentUser.User.UserName;
                    // insert
                    SalaryDecisionController.Create(model);
                }
                // hide window
                wdSetting.Hide();
                //reset form
                ResetForm();
                // reload data
                gpSalaryDecision.Reload();
            }
            catch (Exception exception)
            {
                Dialog.ShowError(exception);
            }
        }

        /// <summary>
        /// Insert or Update decision many employee
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InsertMany(object sender, DirectEventArgs e)
        {
            try
            {
                foreach (var employee in chkEmployeeRowSelection.SelectedRows)
                {
                    // init model
                    var model = new SalaryDecisionModel
                    {
                        RecordId = Convert.ToInt32(employee.RecordID),
                        CreatedBy = CurrentUser.User.UserName,
                        EditedBy = CurrentUser.User.UserName,
                        Status = !string.IsNullOrEmpty(hdfStatus.Text)
                            ? (SalaryDecisionStatus)Convert.ToInt32(hdfStatus.Text)
                            : SalaryDecisionStatus.Approved
                    };

                    //edit data
                    EditDataMany(model);

                    // insert
                    SalaryDecisionController.Create(model);
                }

                // hide window
                wdSettingMany.Hide();
                //reset form
                ResetForm();
                // reload data
                gpSalaryDecision.Reload();
            }
            catch (Exception exception)
            {
                Dialog.ShowError(exception);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        private void EditDataMany(SalaryDecisionModel model)
        {
            var util = new ConvertUtils();
            if (!string.IsNullOrEmpty(hdfPositionMany.Text))
            {
                model.SignerPositionId = Convert.ToInt32(hdfPositionMany.Text);
            }

            model.Name = txtNameMany.Text;
            model.DecisionNumber = txtDecisionNumberMany.Text;
            if (!util.IsDateNull(dfDecisionDateMany.SelectedDate))
                model.DecisionDate = dfDecisionDateMany.SelectedDate;
            if (!util.IsDateNull(dfEffectiveDateMany.SelectedDate))
                model.EffectiveDate = dfEffectiveDateMany.SelectedDate;
            model.SignerName = txtSignerNameMany.Text;
            model.Note = txtNoteMany.Text;
            if (!string.IsNullOrEmpty(hdfQuantumIdMany.Text))
            {
                model.QuantumId = Convert.ToInt32(hdfQuantumIdMany.Text);
                var quantum = CatalogQuantumController.GetById(Convert.ToInt32(hdfQuantumIdMany.Text));
                if (quantum != null)
                {
                    model.GroupQuantumId = quantum.GroupQuantumId;
                }
            }

            if (!string.IsNullOrEmpty(hdfSalaryGradeMany.Text))
            {
                model.Grade = Convert.ToInt32(hdfSalaryGradeMany.Text);
            }

            if (!string.IsNullOrEmpty(txtPercentageSalaryMany.Text))
                model.PercentageSalary = Convert.ToDecimal(txtPercentageSalaryMany.Text);
            if (!string.IsNullOrEmpty(txtFactorMany.Text))
                model.Factor = Convert.ToDecimal(txtFactorMany.Text);
            if (!string.IsNullOrEmpty(txtPercentageLeaderMany.Text))
                model.PercentageLeader = Convert.ToDecimal(txtPercentageLeaderMany.Text);
            // upload file
            var path = string.Empty;
            if (fufAttachFileMany.HasFile)
            {
                path = UploadFile(fufAttachFileMany, Constant.PathDecisionSalary);
            }

            model.AttachFileName = path != "" ? path : hdfAttachFileMany.Text;

        }

        /// <summary>
        /// 
        /// </summary>
        private void ResetForm()
        {
            txtName.Reset();
            txtJobTitleName.Reset();
            txtPositionName.Reset();
            txtDepartmentName.Reset();
            txtPercentageLeader.Reset();
            txtNote.Reset();
            txtDecisionNumber.Reset();
            txtSignerName.Reset();
            hdfAttachFile.Reset();
            fufAttachFile.Reset();
            cboContractType.Reset();
            cboEmployee.Reset();
            cboPosition.Reset();
            hdfEmployee.Reset();
            hdfContractId.Reset();
            dfDecisionDate.Reset();
            dfEffectiveDate.Reset();
            cboQuantum.Reset();
            hdfQuantumId.Reset();
            cboSalaryGrade.Reset();
            hdfSalaryGrade.Reset();
            txtSalaryLevel.Reset();
            txtFactor.Reset();
            txtPercentageSalary.Reset();
            txtPercentageLeader.Reset();
            txtNameMany.Reset();
            txtNoteMany.Reset();
            txtDecisionNumberMany.Reset();
            txtSignerNameMany.Reset();
            hdfAttachFileMany.Reset();
            fufAttachFileMany.Reset();
            cboPositionMany.Reset();
            dfDecisionDateMany.Reset();
            dfEffectiveDateMany.Reset();
            cboQuantumMany.Reset();
            hdfQuantumIdMany.Reset();
            cboSalaryGradeMany.Reset();
            hdfSalaryGradeMany.Reset();
            txtSalaryLevelMany.Reset();
            txtFactorMany.Reset();
            txtPercentageSalaryMany.Reset();
            txtPercentageLeaderMany.Reset();
            hdfDepartmentId.Reset();
            cboDepartment.Reset();
            txtCurrQuantumCode.Reset();
            txtCurrContractSalary.Reset();
            txtCurrContractTypeName.Reset();
            txtCurrDecisionDate.Reset();
            txtCurrDecisionNumber.Reset();
            txtCurrFactor.Reset();
            txtCurrGrade.Reset();
            txtCurrInsuranceSalary.Reset();
            txtCurrName.Reset();
            txtCurrPercentageLeader.Reset();
            txtCurrSignerName.Reset();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        private void EditData(SalaryDecisionModel model)
        {
            var util = new ConvertUtils();
            if (!string.IsNullOrEmpty(hdfEmployee.Text))
            {
                model.RecordId = Convert.ToInt32(hdfEmployee.Text);
            }
            if (!string.IsNullOrEmpty(hdfPosition.Text))
            {
                model.SignerPositionId = Convert.ToInt32(hdfPosition.Text);
            }
            if (!string.IsNullOrEmpty(hdfContractId.Text))
            {
                model.ContractId = Convert.ToInt32(hdfContractId.Text);
            }

            model.Name = txtName.Text;
            model.DecisionNumber = txtDecisionNumber.Text;
            if (!util.IsDateNull(dfDecisionDate.SelectedDate))
                model.DecisionDate = dfDecisionDate.SelectedDate;
            if (!util.IsDateNull(dfEffectiveDate.SelectedDate))
                model.EffectiveDate = dfEffectiveDate.SelectedDate;
            model.SignerName = txtSignerName.Text;
            model.Note = txtNote.Text;
           
            if (!string.IsNullOrEmpty(txtPercentageSalary.Text))
                model.PercentageSalary = Convert.ToDecimal(txtPercentageSalary.Text);
            if (!string.IsNullOrEmpty(txtFactor.Text))
                model.Factor = Convert.ToDecimal(txtFactor.Text.Replace(".", ","));
           
            if (!string.IsNullOrEmpty(txtPercentageLeader.Text))
                model.PercentageLeader = Convert.ToDecimal(txtPercentageLeader.Text);

            if (!string.IsNullOrEmpty(hdfQuantumId.Text))
            {
                model.QuantumId = Convert.ToInt32(hdfQuantumId.Text);
                var quantum = CatalogQuantumController.GetById(Convert.ToInt32(hdfQuantumId.Text));
                if (quantum != null)
                {
                    model.GroupQuantumId = quantum.GroupQuantumId;
                }
            }

            if (!string.IsNullOrEmpty(hdfSalaryGrade.Text))
            {
                model.Grade = Convert.ToInt32(hdfSalaryGrade.Text);
            }
            // upload file
            var path = string.Empty;
            if (fufAttachFile.HasFile)
            {
                path = UploadFile(fufAttachFile, Constant.PathDecisionSalary);
            }

            model.AttachFileName = path != "" ? path : hdfAttachFile.Text;

        }

        /// <summary>
        /// Delete catalog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Delete(object sender, DirectEventArgs e)
        {
            try
            {
                // init id
                var param = e.ExtraParams["Id"];
                // parse id
                if (!int.TryParse(param, out var id) || id <= 0)
                {
                    // parse error, show error
                    Dialog.ShowError("Có lỗi xảy ra trong quá trình xử lý");
                    return;
                }
                // delete
                SalaryDecisionController.Delete(id);
                // reload data
                gpSalaryDecision.Reload();
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
        protected void cboEmployee_Selected(object sender, DirectEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(cboEmployee.SelectedItem.Value))
                {
                    var record = RecordController.GetById(Convert.ToInt32(cboEmployee.SelectedItem.Value));
                    if (record != null)
                    {
                        // bind record data
                        txtDepartmentName.Text = record.DepartmentName;
                        txtPositionName.Text = record.PositionName;
                        txtJobTitleName.Text = record.JobTitleName;

                        // get current salary decision
                        var salaryDecsion = SalaryDecisionController.GetCurrent(record.Id);
                        if (salaryDecsion != null)
                        {
                            // bind current salary decision
                            txtCurrName.Text = salaryDecsion.Name;
                            txtCurrDecisionNumber.Text = salaryDecsion.DecisionNumber;
                            txtCurrDecisionDate.Text = salaryDecsion.DecisionDate.ToString("dd/MM/yyyy");
                            txtCurrSignerName.Text = salaryDecsion.SignerName;
                            txtCurrSignerPosition.Text = salaryDecsion.SignerPosition;
                            txtCurrContractTypeName.Text = salaryDecsion.ContractTypeName;
                            txtCurrQuantumCode.Text = salaryDecsion.QuantumCode;
                            txtCurrGrade.Text = salaryDecsion.Grade.ToString();
                            txtCurrFactor.Text = salaryDecsion.Factor.ToString("#,##0.00");
                            txtCurrSalaryLevel.Text = salaryDecsion.Salary.ToString("#,###");
                            txtCurrContractSalary.Text = salaryDecsion.ContractSalary.ToString("#,###");
                            txtCurrInsuranceSalary.Text = salaryDecsion.InsuranceSalary.ToString("#,###");
                            txtCurrPercentageLeader.Text = salaryDecsion.PercentageLeader.ToString("0.00 %");

                            // bind new salary decision
                            txtPercentageLeader.Text = salaryDecsion.PercentageLeader.ToString("0.00");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // show dilog
                Dialog.ShowError(ex);

                // log exception
                SystemLogController.Create(new SystemLogModel(CurrentUser.User.UserName, "Diễn biến lương - cboEmployee_Selected", ex));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDownloadAttachFile_Click(object sender, DirectEventArgs e)
        {
            try
            {
                DownloadAttachFile("sal_SalaryDecision", hdfAttachFile);
            }
            catch (Exception ex)
            {
                // show dialog
                Dialog.ShowError(ex);

                // log exception
                SystemLogController.Create(new SystemLogModel(CurrentUser.User.UserName, "Diễn biến lương - btnDownloadAttachFile_Click", ex));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDownloadAttachFile_ClickMany(object sender, DirectEventArgs e)
        {
            try
            {
                DownloadAttachFile("sal_SalaryDecision", hdfAttachFileMany);
            }
            catch (Exception ex)
            {
                // show dialog
                Dialog.ShowError(ex);

                // log exception
                SystemLogController.Create(new SystemLogModel(CurrentUser.User.UserName, "Diễn biến lương - btnDownloadAttachFile_Click", ex));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDeleteAttachFile_Click(object sender, DirectEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(hdfId.Text))
                {
                    DeleteAttachFile("sal_SalaryDecision", int.Parse("0" + hdfId.Text), hdfAttachFile);
                    hdfAttachFile.Text = "";
                }
            }
            catch (Exception ex)
            {
                // show dilog
                Dialog.ShowError(ex);

                // log exception
                SystemLogController.Create(new SystemLogModel(CurrentUser.User.UserName, "Diễn biến lương - btnDeleteAttachFile_Click", ex));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDeleteAttachFile_ClickMany(object sender, DirectEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(hdfId.Text))
                {
                    DeleteAttachFile("sal_SalaryDecision", int.Parse("0" + hdfId.Text), hdfAttachFileMany);
                    hdfAttachFile.Text = "";
                }
            }
            catch (Exception ex)
            {
                // show dilog
                Dialog.ShowError(ex);

                // log exception
                SystemLogController.Create(new SystemLogModel(CurrentUser.User.UserName, "Diễn biến lương - btnDeleteAttachFile_Click", ex));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void storeStatus_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            storeStatus.DataSource = typeof(SalaryDecisionStatus).GetIntAndDescription();
            storeStatus.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void storeContractType_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(hdfEmployee.Text))
                {
                    var contractModels = ContractController.GetAllByRecordCondition(Convert.ToInt32(hdfEmployee.Text));
                    if (contractModels.Count == 0)
                    {
                        RM.RegisterClientScriptBlock("contract",
                            "alert('Không tìm thấy hợp đồng nào còn hiệu lực. Vui lòng tạo hợp đồng mới trước khi tạo quyết định lương!');");
                    }

                    storeContractType.DataSource = contractModels;
                    storeContractType.DataBind();
                }
                else
                {
                    RM.RegisterClientScriptBlock("employee", "alert('Bạn chưa chọn cán bộ'); cboEmployee.focus();");
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
        [DirectMethod]
        public void GetInformationSalary()
        {
            if (!string.IsNullOrEmpty(hdfQuantumId.Text) && !string.IsNullOrEmpty(hdfSalaryGrade.Text))
            {
                var quantum = CatalogQuantumController.GetById(Convert.ToInt32(hdfQuantumId.Text));
                if (quantum == null) return;
                var groupQuantum = CatalogGroupQuantumGradeController.GetUnique(quantum.GroupQuantumId,
                    Convert.ToInt32(hdfSalaryGrade.Text));
                if (groupQuantum == null) return;
                txtFactor.SetValue(groupQuantum.Factor);
                txtSalaryLevel.SetValue(Math.Round(groupQuantum.Factor * Convert.ToDecimal(hdfBasicSalary.Text)).ToString("##,###"));
            }
            else
            {
                txtFactor.Text = @"0";
                txtSalaryLevel.Text = @"0";
            }
        }  
        
        /// <summary>
        /// 
        /// </summary>
        [DirectMethod]
        public void GetInformationSalaryMany()
        {
            if (!string.IsNullOrEmpty(hdfQuantumIdMany.Text) && !string.IsNullOrEmpty(hdfSalaryGradeMany.Text))
            {
                var quantum = CatalogQuantumController.GetById(Convert.ToInt32(hdfQuantumIdMany.Text));
                if (quantum == null) return;
                var groupQuantum = CatalogGroupQuantumGradeController.GetUnique(quantum.GroupQuantumId,
                    Convert.ToInt32(hdfSalaryGradeMany.Text));
                if (groupQuantum == null) return;
                txtFactorMany.SetValue(groupQuantum.Factor);
                txtSalaryLevelMany.SetValue(Math.Round(groupQuantum.Factor * Convert.ToDecimal(hdfBasicSalary.Text)).ToString("##,###"));
            }
            else
            {
                txtFactorMany.Text = @"0";
                txtSalaryLevelMany.Text = @"0";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void storeSalaryGrade_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            if (string.IsNullOrEmpty(hdfQuantumId.Text)) return;
            var quantum = CatalogQuantumController.GetById(Convert.ToInt32(hdfQuantumId.Text));
            if (quantum == null) return;
            var quantumGrade = CatalogGroupQuantumController.GetById(quantum.GroupQuantumId);
            if (quantumGrade == null) return;
            var grade = quantumGrade.GradeMax;
            hdfSalaryGrade.Text = grade.ToString();
            var comboObjects = new List<ComboObject>();
            for (var i = 1; i <= grade; i++)
            {
                var combo = new ComboObject
                {
                    Code = i.ToString(),
                    Name = "Bậc " + i
                };
                comboObjects.Add(combo);
            }

            storeSalaryGrade.DataSource = comboObjects;
            storeSalaryGrade.DataBind();
        } 
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void storeSalaryGradeMany_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            if (string.IsNullOrEmpty(hdfQuantumIdMany.Text)) return;
            var quantum = CatalogQuantumController.GetById(Convert.ToInt32(hdfQuantumIdMany.Text));
            if (quantum == null) return;
            var quantumGrade = CatalogGroupQuantumController.GetById(quantum.GroupQuantumId);
            if (quantumGrade == null) return;
            var grade = quantumGrade.GradeMax;
            hdfSalaryGradeMany.Text = grade.ToString();
            var comboObjects = new List<ComboObject>();
            for (var i = 1; i <= grade; i++)
            {
                var combo = new ComboObject
                {
                    Code = i.ToString(),
                    Name = "Bậc " + i
                };
                comboObjects.Add(combo);
            }

            storeSalaryGradeMany.DataSource = comboObjects;
            storeSalaryGradeMany.DataBind();
        }
    }
}