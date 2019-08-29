using System;
using Ext.Net;
using Web.Core.Framework;
using Web.Core;
using Web.Core.Object.HumanRecord;
using System.Linq;
using System.Web;
using Web.Core.Framework.Common;

namespace Web.HRM.Modules.ProfileHuman.Management
{
    public partial class AttachFileManagement : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!ExtNet.IsAjaxRequest)
            {
                // Set resource
                gridAttachFile.ColumnModel.SetColumnHeader(3, "{0}".FormatWith(Resource.Get("Grid.EmployeeCode")));

                hdfDepartments.Text = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
                if(btnEdit.Visible)
                {
                    gridAttachFile.Listeners.RowDblClick.Handler +=
                        " if(CheckSelectedRows(gridAttachFile)){btnUpdate.show();btnUpdateNew.hide();btnUpdateClose.hide()}";
                }

                hdfAttachFilePathConst.Text = '/' + Constant.PathAttachFile + '/';
                gridAttachFile.Reload();
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
                var controller = new AttachFileController();
                var attachFile = new hr_AttachFile
                {
                    AttachFileName = txtAttachFileName.Text,
                    Note = txtNote.Text
                };
                // var util = new Util();
                if(!string.IsNullOrEmpty(hdfEmployeeSelectedId.Text))
                    attachFile.RecordId = Convert.ToInt32(hdfEmployeeSelectedId.Text);
                attachFile.CreatedDate = DateTime.Now;
                attachFile.EditedDate = DateTime.Now;
                // upload file
                string path = string.Empty;
                if(file_cv.HasFile)
                {
                    path = UploadFile(file_cv, Constant.PathAttachFile);
                }

                if(path != "")
                {
                    attachFile.URL = path;
                    HttpPostedFile file = file_cv.PostedFile;
                    attachFile.SizeKB = file.ContentLength / 1024;
                }
                else
                {
                    attachFile.URL = hdfAttachFile.Text;
                    attachFile.SizeKB = double.Parse(hdfFileSizeKB.Text);
                }

                if(e.ExtraParams["Command"] == "Update")
                {
                    if(!string.IsNullOrEmpty(hdfKeyRecord.Text))
                    {
                        attachFile.Id = Convert.ToInt32(hdfKeyRecord.Text);
                        controller.Update(attachFile);
                    }
                }
                else
                {
                    controller.Insert(attachFile);
                }

                if(e.ExtraParams["Close"] == "True")
                {
                    wdAttachFile.Hide();
                }

                //reload data
                gridAttachFile.Reload();
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
            txtAttachFileName.Reset();
            txtNote.Reset();
            hdfEmployeeSelectedId.Reset();
            cbxSelectedEmployee.Clear();
            file_cv.Reset();
            hdfAttachFile.Reset();
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
                AttachFileController.Delete(id);
                gridAttachFile.Reload();
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
        protected void EditAsset_Click(object sender, DirectEventArgs e)
        {
            if(!int.TryParse(hdfKeyRecord.Text, out var id) || id <= 0) return;
            var attachFile = AttachFileController.GetById(id);
            if(attachFile != null)
            {
                cbxSelectedEmployee.Text = attachFile.FullName;
                hdfEmployeeSelectedId.Text = attachFile.RecordId.ToString();
                txtAttachFileName.Text = attachFile.AttachFileName;
                txtNote.Text = attachFile.Note;
                hdfAttachFile.Text = attachFile.URL;
            }

            // show window
            btnUpdate.Show();
            btnUpdateNew.Hide();
            btnUpdateClose.Hide();
            cbxSelectedEmployee.Disabled = true;
            wdAttachFile.Title = @"Cập nhật thông tin tệp tin đính kèm";
            wdAttachFile.Show();
        }        

        #endregion

    }
}