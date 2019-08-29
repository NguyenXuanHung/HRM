using System;
using Ext.Net;
using Web.Core.Framework;
using System.Linq;
using Web.Core;
using Web.Core.Framework.Controller;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.HumanRecord;

namespace Web.HRM.Modules.ProfileHuman.Management
{
    public partial class FluctuationInsuranceManagement : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Set resource
            grp_FluctuationInsurance.ColumnModel.SetColumnHeader(5, "{0}".FormatWith(Resource.Get("Employee.Code")));
            txtEmployeeCode.FieldLabel = Resource.Get("Employee.Code");

            hdfDepartments.Text = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
            new Core.Framework.Common.BorderLayout()
            {
                menuID = MenuId,
                script = "#{hdfDepartmentSelectedId}.setValue('" + Core.Framework.Common.BorderLayout.nodeID +
                         "');#{txtSearch}.reset();#{PagingToolbar1}.pageIndex = 0; #{PagingToolbar1}.doLoad();"
            }.AddDepartmentList(brlayout, CurrentUser, true);
            //if (btnEdit.Visible)
            //{
            //    grp_FluctuationInsurance.Listeners.RowDblClick.Handler += " if(CheckSelectedRows(grp_QuanLyNangNgach)){Ext.net.DirectMethods.GetDataForCanBo();}";
            //}
        }

        #region Event Method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cbxChonCanBo_Selected(object sender, DirectEventArgs e)
        {
            try
            {
                var id = int.Parse(cbxChonCanBo.SelectedItem.Value);
                var hs = RecordController.GetById(id);
                var hsTeam = TeamController.GetByRecordId(id);
                var hsSalary = SalaryDecisionController.GetCurrent(id);
                var hsContract = ContractController.GetByConditionRecord(id);
                if(hs == null) return;
                txtEmployeeCode.Text = hs.EmployeeCode;
                txtBirthDate.Text = hs.BirthDateVn;
                txtDepartment.Text = hs.DepartmentName;
                txtPosition.Text = hs.PositionName;
                txtTeam.Text = hsTeam.TeamName;
                txtConstruction.Text = hsTeam.ConstructionName;
                txtSalaryInsurance.Text = hsSalary.InsuranceSalary.ToString();
                txtInsuranceSubmit.Text = (hsSalary.InsuranceSalary * (decimal)0.32).ToString();
                txtContractNumber.Text = hsContract.ContractNumber;
                if(hsContract.ContractDate != null) txtContractDate.SelectedDate = (DateTime)hsContract.ContractDate;
                if(hsContract.EffectiveDate != null)
                    txtEffectiveDate.SelectedDate = (DateTime)hsContract.EffectiveDate;
            }
            catch(Exception ex)
            {
                ExtNet.Msg.Alert("Thông báo từ hệ thống", "Có lỗi xảy ra khi chọn cán bộ: " + ex.Message).Show();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InsertOrUpdate(object sender, DirectEventArgs e)
        {
            if(hdfCommandName.Text != @"Update")
            {
                Insert();
            }
            else
            {
                Update();
            }

            if(e.ExtraParams["Close"] == "True")
            {
                wdCreateFluctuation.Hide();
            }

            //reload data
            grp_FluctuationInsurance.Reload();
            RM.RegisterClientScriptBlock("Grid_Reload", "ReloadGrid();");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="directEventArgs"></param>
        protected void Delete(object sender, DirectEventArgs directEventArgs)
        {
            try
            {
                if(!int.TryParse(hdfKeyRecord.Text, out var id) || id <= 0) return;
                hr_FluctuationInsuranceServices.Delete(id);
                grp_FluctuationInsurance.Reload();
                RM.RegisterClientScriptBlock("Grid_Reload", "ReloadGrid();");
            }
            catch(Exception ex)
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình xóa: {0}".FormatWith(ex.Message));
            }

        }

        #endregion

        [DirectMethod]
        public void ResetForm()
        {
            cbxChonCanBo.Reset();
            txtDepartment.Reset();
            txtReason.Reset();
            txtBirthDate.Reset();
            txtConstruction.Reset();
            txtEmployeeCode.Reset();
            txtPosition.Reset();
            txtTeam.Reset();
            rgType.Reset();
            txtSalaryInsurance.Reset();
            txtInsuranceSubmit.Reset();
            txtContractDate.Reset();
            txtContractNumber.Reset();
            txtEffectiveDate.Reset();
        }

        #region Private Method

        /// <summary>
        /// 
        /// </summary>
        private void Insert()
        {
            try
            {
                hdfChonCanBo.Text = cbxChonCanBo.SelectedItem.Value;
                var fluctuation = new hr_FluctuationInsurance
                {
                    RecordId = int.Parse(hdfChonCanBo.Text),
                    //Type = rbDecrease.Checked,
                    //Reason = txtReason.Text.Trim(),
                    CreatedDate = DateTime.Now,
                    EditedDate = DateTime.Now
                };
                hr_FluctuationInsuranceServices.Create(fluctuation);
            }
            catch(Exception e)
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình thêm mới: {0}".FormatWith(e.Message));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void Update()
        {
            try
            {
                if(!int.TryParse(hdfKeyRecord.Text, out var id) || id <= 0) return;
                var recordId = Convert.ToInt32(hdfChonCanBo.Text);
                var record = hr_FluctuationInsuranceServices.GetById(id);
                record.RecordId = recordId;
                //record.Reason = txtReason.Text;
                //record.Type = rbDecrease.Checked;
                hr_FluctuationInsuranceServices.Update(record);
            }
            catch(Exception e)
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình cập nhật: {0}".FormatWith(e.Message));

            }

        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InitWindowFluctuation(object sender, DirectEventArgs e)
        {
            if(!int.TryParse(hdfKeyRecord.Text, out var id) || id <= 0) return;
            var recordId = int.Parse(hdfRecordId.Text);
            var hs = RecordController.GetById(recordId);
            var hsTeam = TeamController.GetByRecordId(recordId);
            var hsSalary = SalaryDecisionController.GetCurrent(recordId);
            var hsContract = ContractController.GetByConditionRecord(recordId);
            if(hs != null)
            {
                cbxChonCanBo.SelectedItem.Text = hs.FullName;
                txtEmployeeCode.Text = hs.EmployeeCode;
                txtDepartment.Text = hs.DepartmentName;
                txtBirthDate.Text = hs.BirthDateVn;
                txtPosition.Text = hs.PositionName;
            }

            txtTeam.Text = hsTeam.TeamName;
            txtConstruction.Text = hsTeam.ConstructionName;
            txtSalaryInsurance.Text = hsSalary.InsuranceSalary.ToString();
            txtInsuranceSubmit.Text = (hsSalary.InsuranceSalary * (decimal)0.32).ToString();
            txtContractNumber.Text = hsContract.ContractNumber;
            if(hsContract.ContractDate != null) txtContractDate.SelectedDate = (DateTime)hsContract.ContractDate;
            if(hsContract.EffectiveDate != null) txtEffectiveDate.SelectedDate = (DateTime)hsContract.EffectiveDate;
            var fluc = hr_FluctuationInsuranceServices.GetById(Convert.ToInt32(hdfKeyRecord.Text));
            //txtReason.Text = fluc.Reason;
            //if(fluc.Type == false)
            //    rbIncrease.Checked = true;
            //else
            //{
            //    rbDecrease.Checked = true;
            //}

            wdCreateFluctuation.Title = @"Cập nhật quyết định tăng giảm BHXH, BHYT, BHTN";
            wdCreateFluctuation.Show();
            hdfCommandName.Text = @"Update";
            hdfChonCanBo.Text = hsTeam.RecordId.ToString();
        }
    }
}