using System;
using System.Web;
using Ext.Net;
using System.IO;
using SharpCompress.Common;
using SharpCompress.Archive;
using Web.Core.Framework;
using Web.Core.Service.HumanRecord;
using Web.Core;
using Web.Core.Framework.Common;

namespace Web.HJM.Modules.UserControl
{
    public partial class UpdateImageEmployeeSeries : global::System.Web.UI.UserControl
    {
        public event EventHandler AfterClickXemCanBoChuaCoAnhButton;
        public event EventHandler AfterClickHideWindow;
        public event EventHandler AfterClickCapNhat;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnXemCBChuaCoAnh_Click(object sender, DirectEventArgs e)
        {
            bool isChuaCoAnh = true;
            if (AfterClickXemCanBoChuaCoAnhButton != null)
            {
                AfterClickXemCanBoChuaCoAnhButton(isChuaCoAnh, e);
            }
        }

        protected void HideCapNhatAnhHangLoat(object sender, DirectEventArgs e)
        {
            bool isChuaCoAnh = false;
            if (AfterClickHideWindow != null)
            {
                AfterClickHideWindow(isChuaCoAnh, e);
            }
        }

        protected void btnCapNhatAnh_Click(object sender, DirectEventArgs e)
        {
            try
            {
                HttpPostedFile file = fufChonAnh.PostedFile;

                if (fufChonAnh.HasFile == false || file.ContentLength > 10485760)
                {
                    Dialog.ShowNotification("Tệp tin bạn chọn có kích thước lớn hơn cho phép (10MB)");
                }
                else
                {
                    // upload file nén lên server
                    string path = string.Empty;
                    DirectoryInfo dir = new DirectoryInfo(Server.MapPath("ImagesUpload/"));
                    if (dir.Exists == false)
                        dir.Create();
                    path = Server.MapPath("ImagesUpload/") + fufChonAnh.FileName;
                    file.SaveAs(path);
                    // giải nén file
                    string zipFileName = path;
                    string destLocation = Server.MapPath("ImagesUpload/");

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
                        CapNhatAnhTheoMaCanBo(files);
                    if (rdMaPhong_Ten.Checked)
                        CapNhatAnhTheoMaPhongVaTenCanBo(files);
                    if (rdSoCMND.Checked)
                        CapNhatAnhTheoSoCMND(files);

                    wdCapNhatAnhHangLoat.Hide();
                    if (AfterClickCapNhat != null)
                    {
                        AfterClickCapNhat(null, e);
                    }
                }
            }
            catch (Exception ex)
            {
                X.Msg.Alert("Thông báo", "Có lỗi xảy ra: " + ex.Message.ToString()).Show();
            }
            finally
            {
                string path = Server.MapPath("ImagesUpload/") + fufChonAnh.FileName;
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
        private void CapNhatAnhTheoMaCanBo(string[] files)
        {
            try
            {
                int count = 0;
                int koHopLe = 0;
                string notSuccsess = string.Empty;
                // thư mục chứa ảnh hồ sơ
                string rootFolder = Server.MapPath(Constant.PathImageEmployee);
                string loc = Constant.PathLocationImageEmployee;
                foreach (string fName in files)
                {
                    if (fName.ToLower().Contains(".jpg") || fName.ToLower().Contains(".png") ||
                        fName.ToLower().Contains(".gif") || fName.ToLower().Contains(".bmp") || fName.ToLower().Contains(".jpeg"))   // chỉ chấp nhận file ảnh JPG và PNG
                    {
                        string fileName = fName.Substring(fName.LastIndexOf('\\') + 1);
                        // lọc mã cán bộ
                        string macb = fileName.Substring(0, fileName.LastIndexOf('.'));
                        // đổi tên ảnh và chuyển sang ảnh hồ sơ
                        string newName = SoftCore.Util.GetInstance().GetRandomString(5) + fileName;
                        // update cơ sở dữ liệu
                        var condition = @" [EmployeeCode] = '{0}' ".FormatWith(macb);
                        var hs = hr_RecordServices.GetByCondition(condition);
                        hs.ImageUrl = loc + newName;
                        var isSuccess = hr_RecordServices.Update(hs);
                        if (isSuccess != null)
                        {
                            File.Move(fName, rootFolder + newName);
                            count++;
                        }
                        else
                            notSuccsess += macb + ", ";
                    }
                    else
                        koHopLe++;
                }

                // thông báo
                string thongBao = string.Empty;
                if (count > 0)
                    thongBao += "Cập nhật ảnh thành công cho " + count + " cán bộ.";
                else
                    thongBao += "Không có cán bộ nào được cập nhật ảnh.";
                if (koHopLe > 0)
                    thongBao += " Có " + koHopLe + " tệp tin không phải định dạng ảnh cho phép.";
                if (notSuccsess.LastIndexOf(',') != -1)
                    thongBao += " Không tìm thấy các cán bộ có mã: " + notSuccsess;
                X.Msg.Alert("Thông báo", thongBao).Show();
            }
            catch (Exception ex)
            {
                X.Msg.Alert("Thông báo", "Có lỗi xảy ra:" + ex.Message.ToString()).Show();
            }
            finally
            {
                // xóa các thư mục thừa trong thư mục giải nén
                DirectoryInfo dInfo = new DirectoryInfo(Server.MapPath("ImagesUpload/"));
                dInfo.Delete(true);
            }
        }

        /// <summary>
        /// Xử lý cập nhật ảnh hàng loạt với ảnh có định dạng [Mã phòng]_[Họ tên cán bộ].[JPG/PNG]
        /// </summary>
        /// <param name="files">Tên các file trong thư mục giải nén</param>
        private void CapNhatAnhTheoMaPhongVaTenCanBo(string[] files)
        {
            try
            {
                int count = 0;
                int koHopLe = 0;
                string notSuccsess = string.Empty;
                // thư mục chứa ảnh hồ sơ
                string rootFolder = Server.MapPath(Constant.PathImageEmployee);
                string loc = Constant.PathLocationImageEmployee;
                foreach (string fName in files)
                {
                    if (fName.ToLower().Contains(".jpg") || fName.ToLower().Contains(".png") ||
                        fName.ToLower().Contains(".gif") || fName.ToLower().Contains(".bmp") || fName.ToLower().Contains(".jpeg"))   // chỉ chấp nhận file ảnh JPG và PNG
                    {
                        string fileName = fName.Substring(fName.LastIndexOf('\\') + 1);
                        // lấy mã phòng và họ tên
                        int pos = fileName.IndexOf('_');
                        if (pos != -1)
                        {
                            string fn = fileName.Substring(0, fileName.LastIndexOf('.'));
                            string maphong = fn.Substring(0, pos);
                            string hoten = fn.Substring(pos + 1);
                            // đổi tên ảnh và chuyển sang ảnh hồ sơ
                            string newName = SoftCore.Util.GetInstance().GetRandomString(5) + fileName;
                            // update cơ sở dữ liệu
                            //bool isSuccess = new HoSoController().UpdateImageByMaPhongAndName(maphong, hoten, loc + newName);
                            //if (isSuccess)
                            //{
                            //    File.Move(fName, rootFolder + newName);
                            //    count++;
                            //}
                            //else
                            notSuccsess += fileName + ", ";
                        }
                    }
                    else
                        koHopLe++;
                }

                // thông báo
                string thongBao = string.Empty;
                if (count > 0)
                    thongBao += "Cập nhật ảnh thành công cho " + count + " cán bộ.";
                else
                    thongBao += "Không có cán bộ nào được cập nhật ảnh.";
                if (koHopLe > 0)
                    thongBao += " Có " + koHopLe + " tệp tin không phải định dạng ảnh cho phép.";
                if (notSuccsess.LastIndexOf(',') != -1)
                    thongBao += " Không tìm thấy các cán bộ có thông tin: " + notSuccsess;
                X.Msg.Alert("Thông báo", thongBao).Show();
            }
            catch (Exception ex)
            {
                X.Msg.Alert("Thông báo", "Có lỗi xảy ra:" + ex.Message.ToString()).Show();
            }
            finally
            {
                // xóa các thư mục thừa trong thư mục giải nén
                DirectoryInfo dInfo = new DirectoryInfo(Server.MapPath("ImagesUpload/"));
                dInfo.Delete(true);
            }
        }

        /// <summary>
        /// Cập nhật ảnh theo định dạng số chứng minh thư
        /// </summary>
        /// <param name="files">Danh sách tên các file ảnh</param>
        private void CapNhatAnhTheoSoCMND(string[] files)
        {
            try
            {
                int count = 0;
                int koHopLe = 0;
                string notSuccsess = string.Empty;
                // thư mục chứa ảnh hồ sơ
                string rootFolder = Server.MapPath(Constant.PathImageEmployee);
                string loc = Constant.PathLocationImageEmployee;
                foreach (string fName in files)
                {
                    if (fName.ToLower().Contains(".jpg") || fName.ToLower().Contains(".png") ||
                        fName.ToLower().Contains(".gif") || fName.ToLower().Contains(".bmp") || fName.ToLower().Contains(".jpeg"))   // chỉ chấp nhận file ảnh JPG và PNG
                    {
                        string fileName = fName.Substring(fName.LastIndexOf('\\') + 1);
                        // lọc số chứng minh nhân dân
                        string socmnd = fileName.Substring(0, fileName.LastIndexOf('.'));
                        // đổi tên ảnh và chuyển sang ảnh hồ sơ
                        string newName = SoftCore.Util.GetInstance().GetRandomString(5) + fileName;
                        // update cơ sở dữ liệu
                        var condition = @" [IDNumber] = '{0}' ".FormatWith(socmnd);
                        var hs = hr_RecordServices.GetByCondition(condition);
                        hs.ImageUrl = loc + newName;
                        var isSuccess = hr_RecordServices.Update(hs);
                        if (isSuccess != null)
                        {
                            File.Move(fName, rootFolder + newName);
                            count++;
                        }
                        else
                            notSuccsess += socmnd + ", ";
                    }
                    else
                        koHopLe++;
                }

                // thông báo
                string thongBao = string.Empty;
                if (count > 0)
                    thongBao += "Cập nhật ảnh thành công cho " + count + " cán bộ.";
                else
                    thongBao += "Không có cán bộ nào được cập nhật ảnh.";
                if (koHopLe > 0)
                    thongBao += " Có " + koHopLe + " tệp tin không phải định dạng ảnh cho phép.";
                if (notSuccsess.LastIndexOf(',') != -1)
                    thongBao += " Không tìm thấy các cán bộ có số CMND: " + notSuccsess;
                X.Msg.Alert("Thông báo", thongBao).Show();
            }
            catch (Exception ex)
            {
                X.Msg.Alert("Thông báo", "Có lỗi xảy ra: " + ex.Message.ToString()).Show();
            }
        }
    }
}

