using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using DevExpress.Web.Internal;
using DevExpress.XtraPrinting.Native;
using Ext.Net;
using SmartXLS;
using Web.Core;
using Web.Core.Framework;
using Web.Core.Framework.Common;
using Web.Core.Framework.Controller;
using Web.Core.Framework.Model;
using Web.Core.Framework.Utils;
using Web.Core.Helper;
using Web.Core.Object.Salary;

namespace Web.HRM.Modules.Salary
{
    public partial class SalaryFluctuationHRM : BasePage
    {
        private const string ImportAllowanceExcelFile = "/ImportSalaryAllowance.xlsx";

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!ExtNet.IsAjaxRequest)
            {
                hdfDepartmentIds.Text = DepartmentIds;
                hdfOrder.Text = Request.QueryString["order"];
                // init department
                storeDepartment.DataSource = CurrentUser.DepartmentsTree;
                storeDepartment.DataBind();

                new Core.Framework.Common.BorderLayout()
                {
                    menuID = MenuId,
                    script = "#{hdfDepartmentSelectedId}.setValue('" + Core.Framework.Common.BorderLayout.nodeID +
                             "');storeSalaryDecision.reload();",
                }.AddDepartmentList(brlayout, CurrentUser, false);
            }
        }

        #region Private methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        private void InitFormUpdate(int id)
        {
            // get current decision
            var model = SalaryDecisionController.GetById(id);
            if(model != null)
            {
                // get previous decision
                var previousDecision = SalaryDecisionController.GetPrevious(model.RecordId);

                // bind previous data
                if(previousDecision != null)
                {
                    txtCurrName.Text = previousDecision.Name;
                    txtCurrDecisionNumber.Text = previousDecision.DecisionNumber;
                    txtCurrDecisionDate.Text = previousDecision.DecisionDate.ToString("dd/MM/yyyy");
                    txtCurrSignerName.Text = previousDecision.SignerName;
                    txtCurrSignerPosition.Text = previousDecision.SignerPosition;
                    txtCurrContractTypeName.Text = previousDecision.ContractTypeName;
                    txtCurrBasicSalary.Text = previousDecision.BasicSalary.ToString("#,###");
                    txtCurrFactor.Text = previousDecision.Factor.ToString("#,##0.00");
                    txtCurrGrossSalary.Text = previousDecision.GrossSalary.ToString("#,###");
                    txtCurrNetSalary.Text = previousDecision.NetSalary.ToString("#,###");
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
                txtBasicSalary.Text = model.BasicSalary.ToString("#,###");
                txtFactor.Text = model.Factor.ToString("#,##0.00");
                txtGrossSalary.Text = model.GrossSalary.ToString("#,###");
                txtNetSalary.Text = model.NetSalary.ToString("#,###");
                txtContractSalary.Text = model.ContractSalary.ToString("#,###");
                txtInsuranceSalary.Text = model.InsuranceSalary.ToString("#,###");
                txtPercentageSalary.Text = model.PercentageSalary.ToString("0.00");
                txtPercentageLeader.Text = model.PercentageLeader.ToString("0.00");
                txtPercentageOverGrade.Text = model.PercentageOverGrade.ToString("0.00");
                // get file upload
                if(cfAttachFile.Visible)
                {
                    hdfAttachFile.Text = model.AttachFileName;
                    if(!string.IsNullOrEmpty(model.AttachFileName))
                    {
                        hdfAttachFile.Text = model.AttachFileName;
                        fufAttachFile.Text = GetFileName(model.AttachFileName);
                    }
                }
                //insurance
                var insurance = FluctuationInsuranceController.GetByRecordId(model.RecordId, model.EffectiveDate.Month, model.EffectiveDate.Year);

                if(insurance != null)
                {
                    hdfReason.Text = insurance.ReasonId.ToString();
                    cboReason.Text = insurance.ReasonName;
                    hdfInsuranceType.Text = ((int)insurance.Type).ToString();
                    if(insurance.Type != 0)
                        cboInsuranceType.Text = insurance.TypeName;
                    if(!string.IsNullOrEmpty(hdfInsuranceType.Text))
                    {
                        cboReason.Show();
                    }
                }
                else
                {
                    //reset insurance
                    ResetInsurance();
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        private void ProcessInsurance(SalaryDecisionModel model)
        {
            var insuranceModel = new FluctuationInsuranceModel()
            {
                RecordId = model.RecordId,
                ReasonId = !string.IsNullOrEmpty(hdfReason.Text) ? Convert.ToInt32(hdfReason.Text) : 0,
                EffectiveDate = model.EffectiveDate,
                CreatedBy = CurrentUser.User.UserName,
                CreatedDate = DateTime.Now,
                EditedBy = "",
                EditedDate = DateTime.Now
            };

            if(!string.IsNullOrEmpty(hdfInsuranceType.Text))
                insuranceModel.Type = (InsuranceType)Enum.Parse(typeof(InsuranceType), hdfInsuranceType.Text);

            //check exist insurance
            var checkModel = FluctuationInsuranceController.GetByRecordId(model.RecordId, insuranceModel.EffectiveDate.Month, insuranceModel.EffectiveDate.Year);
            if(checkModel != null)
            {
                //update
                insuranceModel.Id = checkModel.Id;
                insuranceModel.EditedBy = CurrentUser.User.UserName;
                insuranceModel.EditedDate = DateTime.Now;

                //update
                FluctuationInsuranceController.Update(insuranceModel);
            }
            else
            {
                //create
                FluctuationInsuranceController.Create(insuranceModel);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        private void EditDataMany(SalaryDecisionModel model)
        {
            var util = new ConvertUtils();
            if(!string.IsNullOrEmpty(hdfPositionMany.Text))
            {
                model.SignerPositionId = Convert.ToInt32(hdfPositionMany.Text);
            }

            model.Name = txtNameMany.Text;
            model.DecisionNumber = txtDecisionNumberMany.Text;
            if(!util.IsDateNull(dfDecisionDateMany.SelectedDate))
                model.DecisionDate = dfDecisionDateMany.SelectedDate;
            if(!util.IsDateNull(dfEffectiveDateMany.SelectedDate))
                model.EffectiveDate = dfEffectiveDateMany.SelectedDate;
            model.SignerName = txtSignerNameMany.Text;
            model.Note = txtNoteMany.Text;
            if(!string.IsNullOrEmpty(txtGrossSalaryMany.Text))
                model.GrossSalary = Convert.ToDecimal(txtGrossSalaryMany.Text);
            if(!string.IsNullOrEmpty(txtContractSalaryMany.Text))
                model.ContractSalary = Convert.ToDecimal(txtContractSalaryMany.Text);
            if(!string.IsNullOrEmpty(txtPercentageSalaryMany.Text))
                model.PercentageSalary = Convert.ToDecimal(txtPercentageSalaryMany.Text);
            if(!string.IsNullOrEmpty(txtFactorMany.Text))
                model.Factor = Convert.ToDecimal(txtFactorMany.Text);
            if(!string.IsNullOrEmpty(txtNetSalaryMany.Text))
                model.NetSalary = Convert.ToDecimal(txtNetSalaryMany.Text);
            if(!string.IsNullOrEmpty(txtInsuranceSalaryMany.Text))
                model.InsuranceSalary = Convert.ToDecimal(txtInsuranceSalaryMany.Text);
            if(!string.IsNullOrEmpty(txtPercentageLeaderMany.Text))
                model.PercentageLeader = Convert.ToDecimal(txtPercentageLeaderMany.Text);
            // upload file
            var path = string.Empty;
            if(fufAttachFileMany.HasFile)
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
            txtPercentageOverGrade.Reset();
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
            txtGrossSalary.Reset();
            txtNetSalary.Reset();
            txtFactor.Reset();
            txtInsuranceSalary.Reset();
            txtBasicSalary.Reset();
            txtContractSalary.Reset();
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
            txtGrossSalaryMany.Reset();
            txtNetSalaryMany.Reset();
            txtFactorMany.Reset();
            txtInsuranceSalaryMany.Reset();
            txtContractSalaryMany.Reset();
            txtPercentageSalaryMany.Reset();
            txtPercentageLeaderMany.Reset();
            hdfDepartmentId.Reset();
            cboDepartment.Reset();
            //insurance
            ResetInsurance();
        }

        /// <summary>
        /// 
        /// </summary>
        private void ResetInsurance()
        {
            cboReason.Reset();
            hdfReason.Reset();
            hdfInsuranceType.Reset();
            cboInsuranceType.Reset();
            cboReason.Hide();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        private void EditData(SalaryDecisionModel model)
        {
            var util = new ConvertUtils();
            if(!string.IsNullOrEmpty(hdfEmployee.Text))
            {
                model.RecordId = Convert.ToInt32(hdfEmployee.Text);
            }
            if(!string.IsNullOrEmpty(hdfPosition.Text))
            {
                model.SignerPositionId = Convert.ToInt32(hdfPosition.Text);
            }
            if(!string.IsNullOrEmpty(hdfContractId.Text))
            {
                model.ContractId = Convert.ToInt32(hdfContractId.Text);
            }

            model.Name = txtName.Text;
            model.DecisionNumber = txtDecisionNumber.Text;
            if(!util.IsDateNull(dfDecisionDate.SelectedDate))
                model.DecisionDate = dfDecisionDate.SelectedDate;
            if(!util.IsDateNull(dfEffectiveDate.SelectedDate))
                model.EffectiveDate = dfEffectiveDate.SelectedDate;
            model.SignerName = txtSignerName.Text;
            model.Note = txtNote.Text;
            if(!string.IsNullOrEmpty(txtGrossSalary.Text))
                model.GrossSalary = Convert.ToDecimal(txtGrossSalary.Text);
            if(!string.IsNullOrEmpty(txtContractSalary.Text))
                model.ContractSalary = Convert.ToDecimal(txtContractSalary.Text);
            if(!string.IsNullOrEmpty(txtPercentageSalary.Text))
                model.PercentageSalary = Convert.ToDecimal(txtPercentageSalary.Text);
            if(!string.IsNullOrEmpty(txtFactor.Text))
                model.Factor = Convert.ToDecimal(txtFactor.Text);
            if(!string.IsNullOrEmpty(txtNetSalary.Text))
                model.NetSalary = Convert.ToDecimal(txtNetSalary.Text);
            if(!string.IsNullOrEmpty(txtInsuranceSalary.Text))
                model.InsuranceSalary = Convert.ToDecimal(txtInsuranceSalary.Text);
            if(!string.IsNullOrEmpty(txtPercentageLeader.Text))
                model.PercentageLeader = Convert.ToDecimal(txtPercentageLeader.Text);
            if(!string.IsNullOrEmpty(txtPercentageOverGrade.Text))
                model.PercentageOverGrade = Convert.ToDecimal(txtPercentageOverGrade.Text);
            // upload file
            var path = string.Empty;
            if(fufAttachFile.HasFile)
            {
                path = UploadFile(fufAttachFile, Constant.PathDecisionSalary);
            }

            model.AttachFileName = path != "" ? path : hdfAttachFile.Text;

        }

        /// <summary>
        /// Lưu phụ cấp
        /// </summary>
        /// <param name="model"></param>
        private void ProcessAllowance(SalaryDecisionModel model)
        {
            // check edit data
            if(string.IsNullOrEmpty(hdfEditData.Text)) return;

            // get list data
            var allowances = JSON.Deserialize<List<SalaryAllowanceModel>>(hdfEditData.Text);

            // group latest salary allowance by code
            allowances = allowances.GroupBy(r => r.AllowanceCode)
                .Select(g => g.OrderByDescending(c => c.CreatedDate).FirstOrDefault()).ToList();

            // create and update
            foreach(var allowance in allowances)
            {
                if(allowance.Id == 0)
                {
                    allowance.SalaryDecisionId = model.Id;
                    SalaryAllowanceController.Create(allowance);
                }
                else
                {
                    SalaryAllowanceController.Update(allowance);
                }
            }
        }

        /// <summary>
        /// Tạo bảng đầy đủ các phụ cấp
        /// </summary>
        /// <returns></returns>
        private DataTable CreateSalaryAllowanceTable(List<SalaryAllowanceModel> existedSalaryAllowances)
        {
            // get catalog
            var catalogAllowances = CatalogAllowanceController.GetAll(null, null, null, null, CatalogStatus.Active, false, null, null);
            var count = 0;

            // fill data to datatable
            foreach(var catalogAllowance in catalogAllowances)
            {
                var match = existedSalaryAllowances.Where(s => s.AllowanceCode.Contains(catalogAllowance.Code)).ToList();
                if(match.Count == 0)
                {
                    existedSalaryAllowances.Add(new SalaryAllowanceModel { AllowanceCode = catalogAllowance.Code, AllowanceName = catalogAllowance.Name });
                }
            }

            // add taxable column
            var data = existedSalaryAllowances.ToDataTable();
            data.Columns.Add("Taxable");

            // fill data to taxable column
            foreach(var catalogAllowance in catalogAllowances)
            {
                foreach(DataRow row in data.Rows)
                {
                    if(row["AllowanceCode"].ToString() == catalogAllowance.Code)
                    {
                        data.Rows[count++]["Taxable"] = catalogAllowance.Taxable ? 1 : 0;
                    }
                }
            }

            return data;
        }

        #endregion

        #region Event methods

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
                if(int.TryParse(param, out var id))
                {
                    // init window props
                    if(id > 0)
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
                        txtBasicSalary.Text = CatalogBasicSalaryController.GetCurrent().Value.ToString("#,###");
                        ResetForm();
                    }
                    // init id
                    hdfId.Text = id.ToString();

                    // check id
                    if(id > 0)
                    {
                        //init
                        InitFormUpdate(id);
                    }
                    // set  props
                    // status
                    // show window
                    wdSetting.Show();
                    gpSalaryAllowance.Reload();
                    hdfEditData.Reset();
                }
            }
            catch(Exception exception)
            {
                Dialog.ShowError(exception.StackTrace);
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
                foreach(var employee in chkEmployeeRowSelection.SelectedRows)
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
            catch(Exception exception)
            {
                Dialog.ShowError(exception);
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
                txtBasicSalaryMany.Text = CatalogBasicSalaryController.GetCurrent().Value.ToString("#,###");
                ResetForm();
                // show window
                wdSettingMany.Show();
            }
            catch(Exception exception)
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
                if(!string.IsNullOrEmpty(hdfId.Text) && Convert.ToInt32(hdfId.Text) > 0)
                {
                    var result = SalaryDecisionController.GetById(Convert.ToInt32(hdfId.Text));
                    if(result != null)
                        model = result;
                }
                // set new props for model
                model.Status = !string.IsNullOrEmpty(hdfStatus.Text) ? (SalaryDecisionStatus)Convert.ToInt32(hdfStatus.Text) : SalaryDecisionStatus.Approved;

                //edit data
                EditData(model);

                // check model id
                if(model.Id > 0)
                {
                    // update
                    SalaryDecisionController.Update(model);
                    if (!string.IsNullOrEmpty(hdfInsuranceType.Text))
                        //create insurance
                        ProcessInsurance(model);
                    //create allowance
                    ProcessAllowance(model);
                }
                else
                {
                    // set created user
                    model.CreatedBy = CurrentUser.User.UserName;
                    // insert
                    var resultModel = SalaryDecisionController.Create(model);
                    if (!string.IsNullOrEmpty(hdfInsuranceType.Text))
                        //create insurance
                        ProcessInsurance(resultModel);
                    //create allowance
                    ProcessAllowance(resultModel);
                }



                // hide window
                wdSetting.Hide();
                //reset form
                ResetForm();
                // reload data
                gpSalaryDecision.Reload();
            }
            catch(Exception exception)
            {
                Dialog.ShowError(exception);
            }
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
                if(!int.TryParse(param, out var id) || id <= 0)
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
            catch(Exception exception)
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
                if(!string.IsNullOrEmpty(cboEmployee.SelectedItem.Value))
                {
                    var record = RecordController.GetById(Convert.ToInt32(cboEmployee.SelectedItem.Value));
                    if(record != null)
                    {
                        // bind record data
                        txtDepartmentName.Text = record.DepartmentName;
                        txtPositionName.Text = record.PositionName;
                        txtJobTitleName.Text = record.JobTitleName;

                        // get current salary decision
                        var salaryDecsion = SalaryDecisionController.GetCurrent(record.Id);
                        if(salaryDecsion != null)
                        {
                            // bind current salary decision
                            txtCurrName.Text = salaryDecsion.Name;
                            txtCurrDecisionNumber.Text = salaryDecsion.DecisionNumber;
                            txtCurrDecisionDate.Text = salaryDecsion.DecisionDate.ToString("dd/MM/yyyy");
                            txtCurrSignerName.Text = salaryDecsion.SignerName;
                            txtCurrSignerPosition.Text = salaryDecsion.SignerPosition;
                            txtCurrContractTypeName.Text = salaryDecsion.ContractTypeName;
                            txtCurrBasicSalary.Text = salaryDecsion.BasicSalary.ToString("#,###");
                            txtCurrFactor.Text = salaryDecsion.Factor.ToString("#,###.00");
                            txtCurrGrossSalary.Text = salaryDecsion.GrossSalary.ToString("#,###");
                            txtCurrNetSalary.Text = salaryDecsion.NetSalary.ToString("#,###");
                            txtCurrContractSalary.Text = salaryDecsion.ContractSalary.ToString("#,###");
                            txtCurrInsuranceSalary.Text = salaryDecsion.InsuranceSalary.ToString("#,###");
                            txtCurrPercentageLeader.Text = salaryDecsion.PercentageLeader.ToString("0.00 %");

                            // bind new salary decision
                            txtBasicSalary.Text = salaryDecsion.BasicSalary.ToString("#,###");
                            txtPercentageLeader.Text = salaryDecsion.PercentageLeader.ToString("0.00");
                            txtPercentageOverGrade.Text = salaryDecsion.PercentageOverGrade.ToString("0.00");
                        }
                    }
                }
            }
            catch(Exception ex)
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
            catch(Exception ex)
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
            catch(Exception ex)
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
                if(!string.IsNullOrEmpty(hdfId.Text))
                {
                    DeleteAttachFile("sal_SalaryDecision", int.Parse("0" + hdfId.Text), hdfAttachFile);
                    hdfAttachFile.Text = "";
                }
            }
            catch(Exception ex)
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
                if(!string.IsNullOrEmpty(hdfId.Text))
                {
                    DeleteAttachFile("sal_SalaryDecision", int.Parse("0" + hdfId.Text), hdfAttachFileMany);
                    hdfAttachFile.Text = "";
                }
            }
            catch(Exception ex)
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
        protected void DownloadTemplate_Click(object sender, DirectEventArgs e)
        {
            // get allowance
            var salaryAllowances = new List<SalaryAllowanceModel>();
            if(!string.IsNullOrEmpty(hdfId.Text) && int.Parse(hdfId.Text) > 0)
            {
                salaryAllowances = SalaryAllowanceController.GetAll(null, int.Parse(hdfId.Text), null, null, null);
            }
            // init table
            var workBook = new WorkBook();
            var dataTable = CreateSalaryAllowanceTable(salaryAllowances);
            // fixed column
            var fixedColumnNames = new[]
            {
                nameof(SalaryAllowanceModel.AllowanceName),
                nameof(SalaryAllowanceModel.AllowanceCode),
                nameof(SalaryAllowanceModel.Factor),
                nameof(SalaryAllowanceModel.Percent),
                nameof(SalaryAllowanceModel.Value)
            };
            var fixedColumns = new Dictionary<string, string>
            {
                {nameof(SalaryAllowanceModel.AllowanceName), "Tên phụ cấp"},
                {nameof(SalaryAllowanceModel.AllowanceCode), "Mã phụ cấp"},
                {nameof(SalaryAllowanceModel.Factor), "Hệ số"},
                {nameof(SalaryAllowanceModel.Percent), "Phần trăm"},
                {nameof(SalaryAllowanceModel.Value), "Giá trị"}
            };

            var data = new DataTable();
            data.Rows.Add();
            data.Rows.Add();
            foreach (var item in fixedColumns)
            {
                var col = data.Columns.Add();
                col.ColumnName = item.Key;
                data.Rows[0][col] = item.Value;
                data.Rows[1][col] = item.Key;
            }

            data.Merge(dataTable);
            // delete column
            var deleteColumnNames = new List<string>();
            // add remove column to list
            foreach(DataColumn column in dataTable.Columns)
            {
                if(fixedColumnNames.Contains(column.ColumnName)) continue;
                deleteColumnNames.Add(column.ColumnName);
            }
            // remove column
            deleteColumnNames.ForEach(col => dataTable.Columns.Remove(col));
            // set allowance name
            dataTable.Columns[nameof(SalaryAllowanceModel.AllowanceName)].SetOrdinal(0);

            workBook.ImportDataTable(dataTable, true, 0, 0, dataTable.Rows.Count + 1, dataTable.Columns.Count + 1);

            ExportToExcel(dataTable, "~/" + Constant.PathTemplate, ImportAllowanceExcelFile);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdateImportExcel_Click(object sender, DirectEventArgs e)
        {
            var workbook = new WorkBook();
            if(fileExcel.HasFile)
            {
                var path = UploadFile(fileExcel, Constant.PathTemplate);
                if(path != null)
                {
                    // init list changes
                    var allowanceChanges = new List<SalaryAllowanceModel>();
                    // Read data from excel
                    workbook.readXLSX(Path.Combine(Server.MapPath("~/"), Constant.PathTemplate, path));

                    // Export to data table
                    var dataTable = workbook.ExportDataTable(0, //first row
                        0, //first col
                        workbook.LastRow + 1, //last row
                        workbook.LastCol + 1, //last col
                        true, //first row as header
                        false //convert to DateTime object if it match date pattern
                    );

                    foreach(DataRow row in dataTable.Rows)
                    {
                        // get value from cell
                        var allowanceCode = row[nameof(SalaryAllowanceModel.AllowanceCode)].ToString();
                        var allowanceName = row[nameof(SalaryAllowanceModel.AllowanceName)].ToString();
                        var factor = row[nameof(SalaryAllowanceModel.Factor)].ToString();
                        var percent = row[nameof(SalaryAllowanceModel.Percent)].ToString();
                        var value = row[nameof(SalaryAllowanceModel.Value)].ToString();
                        var salaryDecisionId = int.TryParse(hdfId.Text, out var result) && result > 0 ? result : 0;

                        // check condition
                        if(string.IsNullOrEmpty(allowanceCode)) continue;
                        if(!decimal.TryParse(factor, out var factorResult)) continue;
                        if(!decimal.TryParse(percent, out var percentResult)) continue;
                        if(!decimal.TryParse(value, out var valueResult)) continue;

                        // get catalog by code
                        var catalogAllowance = CatalogAllowanceController.GetByCode(allowanceCode);

                        if(catalogAllowance == null) continue;

                        // init model
                        var salaryAllowance = new SalaryAllowanceModel();

                        // get allowance by decision and code
                        var salaryAllowances = SalaryAllowanceController.GetAll(null, salaryDecisionId, allowanceCode, null, 1);
                        if(salaryAllowances.Any())
                        {
                            salaryAllowance = salaryAllowances.First();
                        }
                        else
                        {
                            salaryAllowance.Id = 0;
                            salaryAllowance.AllowanceCode = allowanceCode;
                        }

                        // check are there any changes
                        if (salaryAllowance.Factor != factorResult || salaryAllowance.Percent != percentResult ||
                            salaryAllowance.Value != valueResult)
                        {
                            // set value
                            salaryAllowance.AllowanceName = allowanceName;
                            salaryAllowance.Factor = factorResult;
                            salaryAllowance.Percent = percentResult;
                            salaryAllowance.Value = valueResult;
                            // save data to hidden field
                            SaveEditData(JSON.Serialize(salaryAllowance));
                            // add data to list changes
                            allowanceChanges.Add(salaryAllowance);
                        }

                        if (salaryAllowance.Factor != 0 || salaryAllowance.Percent != 0 || salaryAllowance.Value != 0)
                        {
                            allowanceChanges.Add(salaryAllowance);
                        }
                    }
                    // hide window
                    wdExcel.Hide();
                    // bind data
                    storeAllowanceCatalog.DataSource = CreateSalaryAllowanceTable(allowanceChanges);
                    storeAllowanceCatalog.DataBind();
                }
                Dialog.ShowNotification("Thêm thành công");
            }
            else
            {
                Dialog.Alert("Bạn chưa chọn tệp tin đính kèm. Vui lòng chọn.");
            }
        }

        #endregion

        #region Store

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
                if(!string.IsNullOrEmpty(hdfEmployee.Text))
                {
                    var contractModels = ContractController.GetAllByRecordCondition(Convert.ToInt32(hdfEmployee.Text));
                    if(contractModels.Count == 0)
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
            catch(Exception exception)
            {
                Dialog.ShowError(exception);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void storeInsuranceType_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            try
            {
                storeInsuranceType.DataSource = typeof(InsuranceType).GetIntAndDescription();
                storeInsuranceType.DataBind();
            }
            catch(Exception exception)
            {
                Dialog.ShowError(exception);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void storeAllowanceCatalog_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            try
            {
                var salaryAllowances = new List<SalaryAllowanceModel>();
                if(!string.IsNullOrEmpty(hdfId.Text) && int.Parse(hdfId.Text) > 0)
                {
                    salaryAllowances = SalaryAllowanceController.GetAll(null, int.Parse(hdfId.Text), null, null, null);
                }

                storeAllowanceCatalog.DataSource = CreateSalaryAllowanceTable(salaryAllowances);
                storeAllowanceCatalog.DataBind();
            }
            catch(Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }

        #endregion

        #region Direct Methods

        [DirectMethod]
        public void SaveEditData(string data)
        {
            // check data
            if(string.IsNullOrEmpty(data)) return;
            // get data
            var allowance = JSON.Deserialize<SalaryAllowanceModel>(data);
            var allowances = new List<SalaryAllowanceModel>();
            if(!string.IsNullOrEmpty(hdfEditData.Text))
            {
                // get data from hidden field
                allowances = JSON.Deserialize<List<SalaryAllowanceModel>>(hdfEditData.Text);
            }

            // add data to list
            allowances.Add(allowance);
            // save list to hidden field
            hdfEditData.Text = JSON.Serialize(allowances);
        }


        /// <summary>
        /// 
        /// </summary>
        [DirectMethod]
        public void ResetExcelForm()
        {
            fileExcel.Reset();
            txtSheetName.Reset();
        }

        #endregion
    }
}