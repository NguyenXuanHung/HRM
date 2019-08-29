using System;
using Ext.Net;
using Web.Core.Framework;
using Web.Core;
using Web.Core.Object.HumanRecord;
using System.Linq;

namespace Web.HRM.Modules.ProfileHuman.Management
{
    public partial class BankManagement : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!ExtNet.IsAjaxRequest)
            {
                // Set resource
                gridBank.ColumnModel.SetColumnHeader(2, "{0}".FormatWith(Resource.Get("Grid.EmployeeCode")));

                hdfDepartments.Text = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
                if(btnEdit.Visible)
                {
                    gridBank.Listeners.RowDblClick.Handler +=
                        " if(CheckSelectedRows(gridBank)){btnUpdate.show();btnUpdateNew.hide();btnUpdateClose.hide()}";
                }

                gridBank.Reload();
            }
        }

        #region Event Method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InsertOrUpdate(object sender, DirectEventArgs e)
        {
            try
            {
                var bankController = new BankController();
                var bank = new hr_Bank
                {
                    AccountNumber = txtAccountNumber.Text,
                    AccountName = txtAccountName.Text
                };

                if(!string.IsNullOrEmpty(hdfBankId.Text))
                    bank.BankId = Convert.ToInt32(hdfBankId.Text);
                bank.Note = txt_Note.Text;
                if(!string.IsNullOrEmpty(hdfEmployeeSelectedId.Text))
                    bank.RecordId = Convert.ToInt32(hdfEmployeeSelectedId.Text);
                bank.CreatedDate = DateTime.Now;
                bank.EditedDate = DateTime.Now;
                bank.IsInUsed = chk_IsInUsed.Checked;
                var bankModel = new BankModel(bank);
                if(e.ExtraParams["Command"] == "Update")
                {
                    if(!string.IsNullOrEmpty(hdfKeyRecord.Text))
                    {
                        bankModel.Id = Convert.ToInt32(hdfKeyRecord.Text);
                        bankController.Update(bankModel);
                    }
                }
                else
                {
                    bankController.Insert(bank);
                }

                if(e.ExtraParams["Close"] == "True")
                {
                    wdBank.Hide();
                }

                //reload data
                gridBank.Reload();
            }
            catch(Exception ex)
            {
                ExtNet.MessageBox.Alert("Có lỗi xảy ra", ex.Message).Show();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [DirectMethod]
        public void ResetForm()
        {
            txtAccountName.Reset();
            txtAccountNumber.Reset();
            txt_Note.Reset();
            chk_IsInUsed.Reset();
            hdfBankId.Text = "";
            cbxBank.Text = "";
            cbxSelectedEmployee.Clear();
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
                BankController.Delete(id);
                gridBank.Reload();
                RM.RegisterClientScriptBlock("Grid_Reload", "ReloadGrid();");
            }
            catch(Exception ex)
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình xóa: {0}".FormatWith(ex.Message));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void EditBank_Click(object sender, DirectEventArgs e)
        {
            if(!int.TryParse(hdfKeyRecord.Text, out var id) || id <= 0) return;
            var bank = BankController.GetById(id);
            if(bank != null)
            {
                cbxSelectedEmployee.Text = bank.FullName;
                hdfEmployeeSelectedId.Text = bank.RecordId.ToString();
                txtAccountNumber.Text = bank.AccountNumber;
                txtAccountName.Text = bank.AccountName;
                hdfBankId.Text = bank.BankId.ToString();
                cbxBank.Text = bank.BankName;
                txt_Note.Text = bank.Note;
                chk_IsInUsed.Checked = bank.IsInUsed;
            }

            // show window
            btnUpdate.Show();
            btnUpdateNew.Hide();
            btnUpdateClose.Hide();
            cbxSelectedEmployee.Disabled = true;
            wdBank.Title = @"Cập nhật thông tin thẻ ngân hàng";
            wdBank.Show();
        }

        #endregion
    }
}