using System;
using Ext.Net;
using Web.Core.Framework;
using Web.Core.Object.Catalog;
using Web.Core.Service.Catalog;

namespace Web.HJM.Modules.Catalog
{
    public partial class CatalogQuantum : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region Method protected
        protected void EditQuantum_Click(object sender, DirectEventArgs e)
        {
            if (!string.IsNullOrEmpty(hdfKeyRecord.Text))
            {
                var quantum = cat_QuantumServices.GetById(Convert.ToInt32(hdfKeyRecord.Text));
                if (quantum != null)
                {
                    txtMonth.Text = quantum.MonthNumber.ToString();
                    txtGrade.Text = quantum.SalaryGrade.ToString();
                    txtName.Text = quantum.Name;
                    txtCode.Text = quantum.Code;
                    txtPercent.Text = quantum.Percent.ToString();
                    hdfGroupQuantumId.Text = quantum.GroupQuantumId.ToString();
                    txtNote.Text = quantum.Description;
                    cbxGroupQuantum.Text = cat_GroupQuantumServices.GetFieldValueById(quantum.GroupQuantumId, "Name");
                }
            }

            // show window
            btnUpdate.Show();
            btnUpdateNew.Hide();
            wdQuantum.Title = @"Sửa thông tin ngạch";
            wdQuantum.Show();
        }

        protected void InsertOrUpdate(object sender, DirectEventArgs e)
        {
            if (e.ExtraParams["Command"] == "Update")
                Update();
            else
                Insert();
            //reload data
            gridQuantum.Reload();
            //reset form
            ResetForm();
        }

        protected void Delete(object sender, DirectEventArgs e)
        {
            foreach (var item in RowSelectionModel1.SelectedRows)
            {
                //delete
                cat_QuantumServices.Delete(int.Parse("0" + item.RecordID));
            }
            //reload
            gridQuantum.Reload();
        }



        #endregion

        #region Method private
        private void Insert()
        {
            var quantum = new cat_Quantum()
            {
                CreatedDate = DateTime.Now,
                CreatedBy = CurrentUser.User.UserName
            };
            //edit data
            EditDataBeforeSave(quantum);
            //create
            cat_QuantumServices.Create(quantum);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="quantum"></param>
        private void EditDataBeforeSave(cat_Quantum quantum)
        {
            quantum.Name = txtName.Text;
            quantum.Code = txtCode.Text;
            quantum.SalaryGrade = !string.IsNullOrEmpty(txtGrade.Text) ? Convert.ToDecimal(txtGrade.Text) : 0;
            quantum.Percent = !string.IsNullOrEmpty(txtPercent.Text) ? Convert.ToDecimal(txtPercent.Text) : 0;
            quantum.GroupQuantumId = !string.IsNullOrEmpty(hdfGroupQuantumId.Text) ? Convert.ToInt32(hdfGroupQuantumId.Text) : 0;
            quantum.MonthNumber = !string.IsNullOrEmpty(txtMonth.Text) ? Convert.ToDouble(txtMonth.Text) : 0;
            quantum.Description = txtNote.Text;
        }

        /// <summary>
        /// 
        /// </summary>
        private void Update()
        {
            if (string.IsNullOrEmpty(hdfKeyRecord.Text)) return;
            var quantum = cat_QuantumServices.GetById(Convert.ToInt32(hdfKeyRecord.Text));
            if (quantum != null)
            {
                //edit data
                EditDataBeforeSave(quantum);
                quantum.EditedDate = DateTime.Now;
            }
            //update
            cat_QuantumServices.Update(quantum);

            //close window
            wdQuantum.Hide();
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        [DirectMethod]
        private void ResetForm()
        {
            txtMonth.Reset();
            txtName.Reset();
            txtNote.Reset();
            txtGrade.Reset();
            txtCode.Reset();
            hdfGroupQuantumId.Reset();
            txtPercent.Reset();
            cbxGroupQuantum.Clear();
        }
    }
}