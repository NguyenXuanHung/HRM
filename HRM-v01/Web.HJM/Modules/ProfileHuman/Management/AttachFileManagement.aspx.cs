using System;
using Ext.Net;
using Web.Core.Framework;
using Web.Core;
using Web.Core.Object.HumanRecord;
using System.Linq;
using SoftCore;
using System.Web;
using System.IO;
using Web.Core.Framework.Common;

namespace Web.HJM.Modules.ProfileHuman.Management
{
    public partial class AttachFileManagement : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ExtNet.IsAjaxRequest)
            {
                hdfDepartments.Text = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
                if (btnEdit.Visible)
                {
                    gridAttachFile.Listeners.RowDblClick.Handler +=
                        " if(CheckSelectedRows(gridAttachFile)){btnUpdate.show();btnUpdateNew.hide();btnUpdateClose.hide()}";
                }

                gridAttachFile.Reload();
            }
        }

        #region Event Method

        protected void InsertOrUpdate(object sender, DirectEventArgs e)
        {
            try
            {
                var controller = new AttachFileController();
                var attachFile = new hr_AttachFile();
                // var util = new Util();
                attachFile.AttachFileName = txtAttachFileName.Text;
                attachFile.Note = txtNote.Text;
                if (!string.IsNullOrEmpty(hdfEmployeeSelectedId.Text))
                    attachFile.RecordId = Convert.ToInt32(hdfEmployeeSelectedId.Text);
                attachFile.CreatedDate = DateTime.Now;
                attachFile.EditedDate = DateTime.Now;
                // upload file
                string path = string.Empty;
                if (file_cv.HasFile)
                {
                    path = UploadFile(file_cv, Constant.PathAttachFile);
                }

                if (path != "")
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

                if (e.ExtraParams["Command"] == "Update")
                {
                    if (!string.IsNullOrEmpty(hdfKeyRecord.Text))
                    {
                        attachFile.Id = Convert.ToInt32(hdfKeyRecord.Text);
                        controller.Update(attachFile);
                    }
                }
                else
                {
                    controller.Insert(attachFile);
                }

                if (e.ExtraParams["Close"] == "True")
                {
                    wdAttachFile.Hide();
                }

                //reload data
                gridAttachFile.Reload();
            }
            catch (Exception ex)
            {
                ExtNet.MessageBox.Alert("Có lỗi xảy ra", ex.Message).Show();
            }
        }

        /// <summary>
        /// upload file from computer to server
        /// </summary>
        /// <param name="sender">ID of FileUploadField</param>
        /// <param name="relativePath">the relative path to place you want to save file</param>
        /// <returns>The path of file after upload to server</returns>
        public string UploadFile(object sender, string relativePath)
        {
            FileUploadField obj = (FileUploadField) sender;
            HttpPostedFile file = obj.PostedFile;
            DirectoryInfo
                dir = new DirectoryInfo(Server.MapPath(relativePath)); // save file to directory HoSoNhanSu/File
            // if directory not exist then create this
            if (dir.Exists == false)
                dir.Create();
            string rdstr = SoftCore.Util.GetInstance().GetRandomString(7);
            string path = Server.MapPath(relativePath) + "/" + rdstr + "_" + obj.FileName;
            if (File.Exists(path))
                return "";
            FileInfo info = new FileInfo(path);
            file.SaveAs(path);
            return relativePath + "/" + rdstr + "_" + obj.FileName;
        }

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

        protected void Delete(object sender, DirectEventArgs directEventArgs)
        {
            try
            {
                int id;
                if (!int.TryParse(hdfKeyRecord.Text, out id) || id <= 0) return;
                AttachFileController.Delete(id);
                gridAttachFile.Reload();
                RM.RegisterClientScriptBlock("Grid_Reload", "ReloadGrid();");
            }
            catch (Exception ex)
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình xóa: {0}".FormatWith(ex.Message));
            }
        }

        protected void EditAsset_Click(object sender, DirectEventArgs e)
        {
            int id;
            if (!int.TryParse(hdfKeyRecord.Text, out id) || id <= 0) return;
            var attachFile = AttachFileController.GetById(id);
            if (attachFile != null)
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

        [DirectMethod]
        public void DownloadAttach(string path)
        {
            try
            {
                string serverPath = Server.MapPath("") + "/" + path;
                if (Util.GetInstance().FileIsExists(serverPath) == false)
                {
                    Dialog.ShowNotification("Tệp tin không tồn tại hoặc đã bị xóa !");
                    return;
                }

                string str = path.Replace(" ", "_");
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + str);
                Response.WriteFile(serverPath);
                Response.End();
            }
            catch (Exception ex)
            {
                Dialog.Alert("Có lỗi xảy ra: " + ex.Message);
            }
        }

        #endregion

    }
}