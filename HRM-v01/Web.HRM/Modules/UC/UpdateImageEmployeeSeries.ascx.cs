using System;
using Ext.Net;
using System.IO;
using SharpCompress.Common;
using SharpCompress.Archive;
using Web.Core.Framework;
using Web.Core.Service.HumanRecord;
using Web.Core;
using Web.Core.Framework.Common;

namespace Web.HRM.Modules.UC
{
    public partial class UpdateImageEmployeeSeries : global::System.Web.UI.UserControl
    {
        public event EventHandler AfterClickXemCanBoChuaCoAnhButton;
        public event EventHandler AfterClickHideWindow;
        public event EventHandler AfterClickCapNhat;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnXemCBChuaCoAnh_Click(object sender, DirectEventArgs e)
        {
            bool isChuaCoAnh = true;
            AfterClickXemCanBoChuaCoAnhButton?.Invoke(isChuaCoAnh, e);
        }

        protected void HideCapNhatAnhHangLoat(object sender, DirectEventArgs e)
        {
            const bool isChuaCoAnh = false;
            AfterClickHideWindow?.Invoke(isChuaCoAnh, e);
        }

        protected void btnCapNhatAnh_Click(object sender, DirectEventArgs e)
        {
            try
            {
                var file = fufChonAnh.PostedFile;

                if (fufChonAnh.HasFile == false || file.ContentLength > 10485760)
                {
                    Dialog.ShowNotification("Tệp tin bạn chọn có kích thước lớn hơn cho phép (10MB)");
                }
                else
                {
                    // upload file nén lên server
                    var path = string.Empty;
                    var dir = new DirectoryInfo(Server.MapPath("ImagesUpload/"));
                    if (dir.Exists == false)
                        dir.Create();
                    path = Server.MapPath("ImagesUpload/") + fufChonAnh.FileName;
                    file.SaveAs(path);
                    // giải nén file
                    var zipFileName = path;
                    var destLocation = Server.MapPath("ImagesUpload/");

                    // giải nén file rar/zip
                    var compressed = ArchiveFactory.Open(zipFileName);
                    foreach (var entry in compressed.Entries)
                    {
                        entry.WriteToDirectory(destLocation, ExtractOptions.ExtractFullPath | ExtractOptions.Overwrite);
                    }
                    compressed.Dispose();
                    // xóa file nén
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                    // xử lý ảnh
                    string[] files = Directory.GetFiles(destLocation, "*", SearchOption.AllDirectories);
                    if (rdMaCanBo.Checked)
                        UpdateImageByEmployeeCode(files);
                    if (rdSoCMND.Checked)
                        UpdateImageByIdNumber(files);

                    wdCapNhatAnhHangLoat.Hide();
                    if (AfterClickCapNhat != null)
                    {
                        AfterClickCapNhat(null, e);
                    }
                }
            }
            catch (Exception ex)
            {
                Dialog.Alert("Thông báo", "Có lỗi xảy ra: " + ex.Message);
            }
            finally
            {
                var path = Server.MapPath("ImagesUpload/") + fufChonAnh.FileName;
                // xóa file nén
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
        }

        /// <summary>
        /// Xử lý cập nhật ảnh hàng loạt với ảnh có định dạng [Mã cán bộ].[JPG/PNG]
        /// -   Lọc mã cán bộ từ tên file
        /// -   Đổi tên ảnh và copy sang thư mục chứa ảnh hồ sơ
        /// -   Cập nhật ảnh cho cán bộ vào cơ sở dữ liệu
        /// -   Xóa ảnh trong thư mục giải nén
        /// </summary>
        /// <param name="files">Tên các file trong thư mục giải nén</param>
        private void UpdateImageByEmployeeCode(string[] files)
        {
            try
            {
                var count = 0;
                var invalidCount = 0;
                var notSuccsess = string.Empty;
                // thư mục chứa ảnh hồ sơ
                var rootFolder = Server.MapPath("~/" + Constant.PathImageEmployee);
                
                foreach (var fName in files)
                {
                    if (fName.ToLower().Contains(".jpg") || fName.ToLower().Contains(".png") ||
                        fName.ToLower().Contains(".gif") || fName.ToLower().Contains(".bmp") || fName.ToLower().Contains(".jpeg"))   // chỉ chấp nhận file ảnh JPG và PNG
                    {
                        var fileName = fName.Substring(fName.LastIndexOf('\\') + 1);
                        // lọc mã cán bộ
                        var employeeCode = fileName.Substring(0, fileName.LastIndexOf('.'));
                        // đổi tên ảnh và chuyển sang ảnh hồ sơ
                        var newName = Guid.NewGuid() + fileName;
                        // get record by employeeCode
                        var recordModel = RecordController.GetByEmployeeCode(employeeCode);
                        if (recordModel != null)
                        {
                            recordModel.ImageUrl = newName;
                        }
                        //update cơ sở dữ liệu
                        var isSuccess = RecordController.Update(recordModel);
                        if (isSuccess != null)
                        {
                            File.Move(fName, rootFolder + newName);
                            count++;
                        }
                        else
                            notSuccsess += employeeCode + ", ";
                    }
                    else
                        invalidCount++;
                }

                // thông báo
                var notifyMessage = string.Empty;
                if (count > 0)
                    notifyMessage += "Cập nhật ảnh thành công cho " + count + " cán bộ.";
                else
                    notifyMessage += "Không có cán bộ nào được cập nhật ảnh.";
                if (invalidCount > 0)
                    notifyMessage += " Có " + invalidCount + " tệp tin không phải định dạng ảnh cho phép.";
                if (notSuccsess.LastIndexOf(',') != -1)
                    notifyMessage += " Không tìm thấy các cán bộ có mã: " + notSuccsess;
                Dialog.Alert("Thông báo", notifyMessage);
            }
            catch (Exception ex)
            {
                Dialog.Alert("Thông báo", "Có lỗi xảy ra:" + ex.Message);
            }
            finally
            {
                // xóa các thư mục thừa trong thư mục giải nén
                var dInfo = new DirectoryInfo(Server.MapPath("ImagesUpload/"));
                dInfo.Delete(true);
            }
        }

        /// <summary>
        /// Cập nhật ảnh theo định dạng số chứng minh thư
        /// </summary>
        /// <param name="files">Danh sách tên các file ảnh</param>
        private void UpdateImageByIdNumber(string[] files)
        {
            try
            {
                var count = 0;
                var countInvalid = 0;
                var notSuccsess = string.Empty;
                // thư mục chứa ảnh hồ sơ
                var rootFolder = Server.MapPath("~/" +Constant.PathImageEmployee);
                
                foreach (var fName in files)
                {
                    // chỉ chấp nhận file ảnh JPG và PNG
                    if (fName.ToLower().Contains(".jpg") || fName.ToLower().Contains(".png") ||
                        fName.ToLower().Contains(".gif") || fName.ToLower().Contains(".bmp") || fName.ToLower().Contains(".jpeg"))   
                    {
                        var fileName = fName.Substring(fName.LastIndexOf('\\') + 1);
                        // lọc số chứng minh nhân dân
                        var idNumber = fileName.Substring(0, fileName.LastIndexOf('.'));
                        // đổi tên ảnh và chuyển sang ảnh hồ sơ
                        var newName = Guid.NewGuid() + fileName;
                        // update cơ sở dữ liệu
                        var condition = @" [IDNumber] = '{0}' ".FormatWith(idNumber);
                        var hs = hr_RecordServices.GetByCondition(condition);
                        hs.ImageUrl = newName;
                        var isSuccess = hr_RecordServices.Update(hs);
                        if (isSuccess != null)
                        {
                            File.Move(fName, rootFolder + newName);
                            count++;
                        }
                        else
                            notSuccsess += idNumber + ", ";
                    }
                    else
                        countInvalid++;
                }

                // thông báo
                var messageNotify = string.Empty;
                if (count > 0)
                    messageNotify += "Cập nhật ảnh thành công cho " + count + " cán bộ.";
                else
                    messageNotify += "Không có cán bộ nào được cập nhật ảnh.";
                if (countInvalid > 0)
                    messageNotify += " Có " + countInvalid + " tệp tin không phải định dạng ảnh cho phép.";
                if (notSuccsess.LastIndexOf(',') != -1)
                    messageNotify += " Không tìm thấy các cán bộ có số CMND: " + notSuccsess;
                Dialog.Alert("Thông báo", messageNotify);
            }
            catch (Exception ex)
            {
                Dialog.Alert("Thông báo", "Có lỗi xảy ra: " + ex.Message);
            }
        }
    }
}

