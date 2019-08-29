using System;
using System.Web;
using Ext.Net;
using System.IO;
using Web.Core.Framework;

namespace Web.HRM.Modules.UserControl
{
    public partial class Modules_Base_UploadImageWindow_ucUploadImageForm : BaseUserControl
    {
        public event EventHandler UploadClick;
        public string FolderToSaveFile { get; set; }
        public string FileName { get; set; } //Not full server path

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAcceptUpload_Click(object sender, DirectEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(FolderToSaveFile))
                {
                    Dialog.ShowError("Bạn chưa chỉ định đường dẫn lưu tệp tin");
                    return;
                }

                HttpPostedFile file = fufUploadControl.PostedFile;
                if (fufUploadControl.HasFile == false && file.ContentLength > 2000000)
                {
                    Dialog.ShowNotification("File không được lớn hơn 200kb");
                    return;
                }
                else
                {
                    string path = string.Empty;
                    try
                    {
                        DirectoryInfo dir = new DirectoryInfo(Server.MapPath(FolderToSaveFile));
                        if (dir.Exists == false)
                            dir.Create();
                        FileName = fufUploadControl.FileName;
                        path = Server.MapPath(FolderToSaveFile + "/") + FileName;
                        while (SoftCore.Util.GetInstance().FileIsExists(path))
                        {
                            FileName = SoftCore.Util.GetInstance().GetRandomString(3) + fufUploadControl.FileName;
                            path = Server.MapPath(FolderToSaveFile + "/") + FileName;
                        }

                        file.SaveAs(path);
                    }
                    catch (Exception ex)
                    {
                        Dialog.ShowError(ex.Message);
                    }

                    if (UploadClick != null)
                    {
                        UploadClick(path, null);
                    }
                }

                wdUploadImageWindow.Hide();
            }
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }
    }
}