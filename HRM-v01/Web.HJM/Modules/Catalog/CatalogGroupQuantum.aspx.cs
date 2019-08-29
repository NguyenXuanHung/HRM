using System;
using Ext.Net;
using Web.Core;
using Web.Core.Framework;
using Web.Core.Object.Catalog;
using Web.Core.Service.Catalog;

namespace Web.HJM.Modules.Catalog
{
    public partial class CatalogGroupQuantum : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region Method protected

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void EditGroupQuantum_Click(object sender, DirectEventArgs e)
        {
            if (!string.IsNullOrEmpty(hdfKeyRecord.Text))
            {
                var groupQuantum = cat_GroupQuantumServices.GetById(Convert.ToInt32(hdfKeyRecord.Text));
                if (groupQuantum != null)
                {
                    txtMonth.Text = groupQuantum.MonthStep.ToString();
                    txtGradeMax.Text = groupQuantum.GradeMax.ToString();
                    txtName.Text = groupQuantum.Name;
                    hdfGroupQuantum.Text = groupQuantum.Group;
                    txtNote.Text = groupQuantum.Description;
                    var condition =
                        "[ItemType] = N'cat_GroupQuantum' AND [Group] = '{0}' ".FormatWith(groupQuantum.Group);
                    var group = cat_GroupEnumServices.GetByCondition(condition);
                    if (group != null)
                        cbxGroupQuantum.Text = group.Name;
                }
            }

            // show window
            btnUpdate.Show();
            btnUpdateNew.Hide();
            wdGroupQuantum.Title = @"Sửa thông tin nhóm ngạch";
            wdGroupQuantum.Show();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InsertOrUpdate(object sender, DirectEventArgs e)
        {
            if (e.ExtraParams["Command"] == "Update")
                Update();
            else
                Insert();
            //reload data
            gridGroupQuantum.Reload();
            //reset form
            ResetForm();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Delete(object sender, DirectEventArgs e)
        {
            foreach (var item in RowSelectionModel1.SelectedRows)
            {
                //delete
                cat_GroupQuantumServices.Delete(int.Parse("0" + item.RecordID));
            }
            //reload
            gridGroupQuantum.Reload();
        }

        #endregion

        #region Method private

        /// <summary>
        /// Insert group quantum
        /// </summary>
        private void Insert()
        {
            var groupQuantum = new cat_GroupQuantum()
            {
                CreatedDate = DateTime.Now,
                CreatedBy = CurrentUser.User.UserName
            };
            //edit data
            EditDataBeforeSave(groupQuantum);
            //create
            cat_GroupQuantumServices.Create(groupQuantum);
        }

        /// <summary>
        /// Edit data befor save
        /// </summary>
        /// <param name="groupQuantum"></param>
        private void EditDataBeforeSave(cat_GroupQuantum groupQuantum)
        {
            groupQuantum.Name = txtName.Text;
            groupQuantum.GradeMax = !string.IsNullOrEmpty(txtGradeMax.Text) ? Convert.ToInt32(txtGradeMax.Text) : 0;
            if (!string.IsNullOrEmpty(hdfGroupQuantum.Text))
            {
                groupQuantum.Group = hdfGroupQuantum.Text;
            }
            groupQuantum.MonthStep = !string.IsNullOrEmpty(txtMonth.Text) ? Convert.ToDouble(txtMonth.Text) : 0;
            groupQuantum.Description = txtNote.Text;
        }

        /// <summary>
        /// Update
        /// </summary>
        private void Update()
        {
            if (string.IsNullOrEmpty(hdfKeyRecord.Text)) return;
            var groupQuantum = cat_GroupQuantumServices.GetById(Convert.ToInt32(hdfKeyRecord.Text));
            if (groupQuantum != null)
            {
                //edit data
                EditDataBeforeSave(groupQuantum);
                groupQuantum.EditedDate = DateTime.Now;
            }
            //update
            cat_GroupQuantumServices.Update(groupQuantum);
        }

        /// <summary>
        /// Reset form
        /// </summary>
        [DirectMethod]
        public void ResetForm()
        {
            txtMonth.Reset();
            txtName.Reset();
            txtNote.Reset();
            txtGradeMax.Reset();
            hdfGroupQuantum.Reset();
            cbxGroupQuantum.Clear();
        }

        #endregion
    }
}