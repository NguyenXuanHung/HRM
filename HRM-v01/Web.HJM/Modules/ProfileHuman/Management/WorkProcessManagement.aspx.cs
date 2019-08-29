using System;
using Ext.Net;
using System.IO;
using SoftCore;
using Web.Core.Framework;
using System.Linq;
using Web.Core;
using Web.Core.Framework.Adapter;
using Web.Core.Framework.Common;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.Catalog;
using Web.Core.Service.HumanRecord;
using Web.Core.Service.Salary;

namespace Web.HJM.Modules.ProfileHuman.Management
{
    public partial class WorkProcessManagement : BasePage
    {
        private string HO_TEN = "##{HO_TEN}##";
        private string TEN_COQUAN = "##{TEN_COQUAN}##";
        private string CHUC_VU = "##{CHUC_VU}##";
        private string PHONG_BAN = "##{PHONG_BAN}##";
        private string THUTRUONG = "##{THUTRUONG}##";
        private string DAY = "{D}";
        private string MONTH = "{M}";
        private string YEAR = "{Y}";
        private string NGAY_HIEN_TAI = "##{NGAY_HT}##";
        private string CVM = "##{CVM}##";
        private string SO_QD = "##{SO_QD}##";
        private string TP = "##{TP}##";

        protected void Page_Load(object sender, EventArgs e)
        {
            hdfMaDonVi.Text = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
            hdfUserID.SetValue(CurrentUser.User.Id);
            hdfMenuID.SetValue(MenuId);
            hdfDepartments.Text = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
            new Core.Framework.Common.BorderLayout()
            {
                menuID = MenuId,
                script = "#{hdfDepartmentSelectedId}.setValue('" + Core.Framework.Common.BorderLayout.nodeID +
                         "');#{txtSearch}.reset();#{PagingToolbar1}.pageIndex = 0; #{PagingToolbar1}.doLoad();"
            }.AddDepartmentList(brlayout, CurrentUser, true);
            if (btnEdit.Visible)
            {
                grp_QuanLyNangNgach.Listeners.RowDblClick.Handler +=
                    " if(CheckSelectedRows(grp_QuanLyNangNgach)){Ext.net.DirectMethods.GetDataForCanBo();}";
            }
        }

        #region Event Method

        protected void cbxChucVu_Selected(object sender, DirectEventArgs e)
        {
            var positionId = int.Parse(cbxChucVu.SelectedItem.Value);
            var lstPosition = cat_PositionServices.GetById(positionId);
            // var data = lst
            if (lstPosition != null)
            {
                txtPositionAllowanceNew.Text = cat_PositionServices.GetFieldValueById(positionId, "PositionAllowance");
            }
        }

        protected void cbxChucVuMoi_Selected(object sender, DirectEventArgs e)
        {

            var positionId = int.Parse(cbxChucVuMoi.SelectedItem.Value);
            var lstPosition = cat_PositionServices.GetById(positionId);
            // var data = lst
            if (lstPosition != null)
            {
                txtPositionAllowanceNew.Text = cat_PositionServices.GetFieldValueById(positionId, "PositionAllowance");
            }

        }

        protected void cbxChonCanBo_Selected(object sender, DirectEventArgs e)
        {
            try
            {
                var id = int.Parse(cbxChonCanBo.SelectedItem.Value);
                var hs = RecordController.GetById(id);
                if (hs != null)
                {
                    // set general infomation
                    txtChucVu.Text = hs.PositionName;
                    txtCongViec.Text = hs.JobTitleName;
                    txtDepartment.Text = hs.DepartmentName;

                    // get newest salary development
                    var wp = WorkProcessController.GetNewestDecision(id);
                    var qtdc = wp.FirstOrDefault();
                    if (qtdc != null)
                    {
                        if (!string.IsNullOrEmpty(qtdc.DecisionNumber))
                            txtSoQDCu.Text = qtdc.DecisionNumber;
                        dfNgayQDCu.SetValue(qtdc.DecisionDate);
                        if (qtdc.DecisionMaker != null)
                            txtNguoiQDCu.Text = qtdc.DecisionMaker;
                        dfNgayCoHieuLucCu.SetValue(qtdc.EffectiveDate);
                        if (!string.IsNullOrEmpty(qtdc.OldDepartmentName))
                            txtBoPhanCu.Text = qtdc.OldDepartmentName;
                        if (!string.IsNullOrEmpty(qtdc.OldPositionName))
                            txtChucVuCu.Text = qtdc.OldPositionName;
                        if (!string.IsNullOrEmpty(qtdc.OldDepartmentName))
                            txtDepartment.Text = qtdc.OldDepartmentName;
                        hdfOldPositionId.Text = qtdc.OldDepartmentId.ToString();
                    }
                    else
                    {
                        txtSoQDCu.Text = "";
                        dfNgayQDCu.SetValue("");
                        txtNguoiQDCu.Text = "";
                        dfNgayCoHieuLucCu.SetValue("");
                        txtBoPhanCu.Text = "";
                        txtChucVuCu.Text = "";
                        hdfOldPositionId.Text = "";
                    }
                }
                else
                {
                    txtChucVu.Text = "";
                    txtCongViec.Text = "";
                    txtDepartment.Text = "";
                    hdfOldPositionId.Text = "";
                }
            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Thông báo từ hệ thống", "Có lỗi xảy ra khi chọn cán bộ: " + ex.Message).Show();
            }
        }

        protected void InsertOrUpdate(object sender, DirectEventArgs e)
        {
            if (hdfCommandName.Text == @"Update" || hdfCommandUpdate.Text == @"Update")
            {
                Update();
            }
            else
            {
                Insert();
            }

            if (e.ExtraParams["Close"] != "True") return;
            //hide window
            wdTaoQuyetBoNhiemChucVu.Hide();
            wdCapNhatBoNhiemChucVu.Hide();

            //reload data
            grp_QuanLyNangNgach.Reload();
            RM.RegisterClientScriptBlock("Grid_Reload", "ReloadGrid();");
        }

        protected void Delete(object sender, DirectEventArgs directEventArgs)
        {
            try
            {
                int id;
                if (!int.TryParse(hdfKeyRecord.Text, out id) || id <= 0) return;
                hr_WorkProcessServices.Delete(id);
                grp_QuanLyNangNgach.Reload();
                RM.RegisterClientScriptBlock("Grid_Reload", "ReloadGrid();");
            }
            catch (Exception ex)
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình xóa: {0}".FormatWith(ex.Message));
            }

        }

        protected void InitWindowWorkProcess(object sender, DirectEventArgs e)
        {
            int id;
            if (!int.TryParse(hdfKeyRecord.Text, out id) || id <= 0) return;
            var recordId = int.Parse(hdfRecordId.Text);
            var hs = RecordController.GetById(recordId);

            if (hs != null)
            {
                txtFullname.Text = hs.FullName;
                txtNewPosition.Text = hs.PositionName;
                txtNewJobTitle.Text = hs.JobTitleName;
                txtNewDepartment.Text = hs.DepartmentName;
            }

            var wp = hr_WorkProcessServices.GetById(id);
            if (wp == null) return;
            txtCapNhatSoQD.Text = wp.DecisionNumber;

            if (wp.DecisionDate != null) dfCapNhatNgayQD.SelectedDate = (DateTime) wp.DecisionDate;

            txtCapNhatNguoiQD.Text = wp.DecisionMaker;
            if (wp.EffectiveDate != null)
            {
                dfCapNhatNgayHieuLuc.SelectedDate = (DateTime) wp.EffectiveDate;
            }

            if (wp.ExpireDate != null)
                dfUpdateExpireDate.SetValue(wp.ExpireDate);
            txtUpdateSourceDepartment.Text = wp.SourceDepartment;
            cbxUpdateMakerPosition.Text = wp.MakerPosition;
            cbxChucVuMoi.Text = cat_PositionServices.GetFieldValueById(wp.NewPositionId, "Name");
            cbxOldDEpartment.Text = cat_DepartmentServices.GetFieldValueById(wp.OldDepartmentId, "Name");

            hdfOldPositionId.Text = wp.OldPositionId.ToString();
            cbxOldPosition.Text = cat_PositionServices.GetFieldValueById(wp.OldPositionId, "Name");
            //positionAllowance
            var salary = sal_SalaryDecisionServices.GetCurrent(wp.RecordId);

            // init command name & window properties
                hdfCommandName.Text = @"Update";
            hdfCommandUpdate.Text = @"Update";
            // show window
            wdCapNhatBoNhiemChucVu.Show();
        }

        protected void InReport_Click(object sender, DirectEventArgs e)
        {
            var value = htmlEditor.Value.ToString();
            while (value.IndexOf("redColor___", StringComparison.Ordinal) > 0)
            {
                value = value.Replace("redColor___", "");
            }

            Session["report"] = value;
            RM.RegisterClientScriptBlock("redirect", "window.open('/Modules/HTMLReport/Printed.aspx','_blank')");
        }

        protected void ShowReport_Click(object sender, DirectEventArgs e)
        {
            BindQuyetDinhBoNhiemCb();
        }

        private string GetContent(string fileName)
        {
            return Util.GetInstance().ReadFile(Server.MapPath(fileName));
        }

        private void BindQuyetDinhBoNhiemCb()
        {
            // TODO: Sửa lổi report filter
            //var today = DateTime.Now;
            //var content = GetContent("DecisionAppointmentEmployee.html");

            //var hoso = hr_WorkProcessServices.GetById(Convert.ToInt32(hdfKeyRecord.Text));
            //var rct = new ReportController();
            //var filter = new ReportFilter();
            //if (hoso != null)
            //{
            //    var ngayct = hoso.EffectiveDate.ToString();
            //    var ngayct1 = ngayct.Substring(0, 6);
            //    var nam = decimal.Parse(ngayct.Substring(6, 4)) + 5;
            //    var ngayct2 = ngayct1 + nam;
            //    Dialog.ShowNotification("");
            //    var rs = content
            //        .Replace(HO_TEN,
            //            string.IsNullOrEmpty(hoso.RecordId.ToString())
            //                ? "..."
            //                : hr_RecordServices.GetFieldValueById(hoso.RecordId, "FullName"))
            //        .Replace(TEN_COQUAN,
            //            string.IsNullOrEmpty(rct.GetCompanyName("1")) ? "..." : rct.GetCompanyName("1"))
            //        .Replace(CHUC_VU,
            //            string.IsNullOrEmpty(hoso.OldDepartmentId.ToString())
            //                ? "..."
            //                : cat_PositionServices.GetFieldValueById(hoso.OldDepartmentId, "Name"))
            //        .Replace(CVM,
            //            string.IsNullOrEmpty(hoso.NewPositionId.ToString())
            //                ? "..."
            //                : cat_PositionServices.GetFieldValueById(hoso.NewPositionId, "Name"))
            //        .Replace(SO_QD, string.IsNullOrEmpty(hoso.DecisionNumber) ? "..." : hoso.DecisionNumber)
            //        .Replace(PHONG_BAN,
            //            string.IsNullOrEmpty(hoso.NewDepartmentId.ToString())
            //                ? "..."
            //                : cat_DepartmentServices.GetFieldValueById(hoso.NewDepartmentId, "Name"))
            //        .Replace(TP, string.IsNullOrEmpty(rct.GetCityName("1")) ? "..." : rct.GetCityName("1"))
            //        .Replace(THUTRUONG,
            //            string.IsNullOrEmpty(rct.GetHeadOfHRroom("1", filter.Name3))
            //                ? "..."
            //                : rct.GetHeadOfHRroom("1", filter.Name3)).Replace(DAY, today.Day.ToString())
            //        .Replace(MONTH, today.Month.ToString()).Replace(YEAR, today.Year.ToString())
            //        .Replace(NGAY_HIEN_TAI, ngayct2);
            //    htmlEditor.Value = rs;
            //}
            //else
            //{
            //    htmlEditor.Value = content;
            //}

            //wdShowReport.Show();
        }

        public void DownloadAttachFile(string tableName, Hidden sender)
        {
            try
            {
                if (string.IsNullOrEmpty(sender.Text))
                {
                    Dialog.ShowNotification("Không có tệp tin đính kèm");
                    return;
                }

                var serverPath = Server.MapPath(sender.Text);
                if (Util.GetInstance().FileIsExists(serverPath) == false)
                {
                    Dialog.ShowNotification("Tệp tin không tồn tại hoặc đã bị xóa !");
                    return;
                }

                Response.Clear();
                //  Response.ContentType = "application/octet-stream";

                if (sender.Text != null)
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + sender.Text.Replace(" ", "_"));
                if (serverPath != null) Response.WriteFile(serverPath);
                Response.End();
            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Thông báo từ hệ thống", ex.Message).Show();
            }
        }

        public void DownloadAttachFile(string tableName, string path)
        {
            try
            {
                if (string.IsNullOrEmpty(path))
                {
                    Dialog.ShowNotification("Không có tệp tin đính kèm");
                    return;
                }

                var serverPath = Server.MapPath(path);
                if (Util.GetInstance().FileIsExists(serverPath) == false)
                {
                    Dialog.ShowNotification("Tệp tin không tồn tại hoặc đã bị xóa !");
                    return;
                }

                Response.Clear();
                //  Response.ContentType = "application/octet-stream";

                Response.AddHeader("Content-Disposition", "attachment; filename=" + path.Replace(" ", "_"));
                Response.WriteFile(serverPath);
                Response.End();
            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Thông báo từ hệ thống", ex.Message).Show();
            }
        }

        public void DeleteTepTinDinhKem(string tableName, decimal prkey, Hidden sender)
        {
            // xóa trong csdl
            SQLHelper.ExecuteTable(
                SQLManagementAdapter.GetStore_DeleteAttachFile("hr_WorkProcess", Convert.ToInt32(hdfRecordId.Text)));
            // xóa file trong thư mục
            var serverPath = Server.MapPath(sender.Text);
            if (Util.GetInstance().FileIsExists(serverPath) == false)
            {
                Dialog.ShowNotification("Tệp tin không tồn tại hoặc đã bị xóa !");
                return;
            }

            File.Delete(serverPath);
        }

        public void DeleteTepTinDinhKem(string tableName, int id, Hidden sender)
        {
            // xóa trong csdl
            SQLHelper.ExecuteTable(
                SQLManagementAdapter.GetStore_DeleteAttachFile("hr_WorkProcess", Convert.ToInt32(hdfKeyRecord.Text)));
            // xóa file trong thư mục
            var serverPath = Server.MapPath(sender.Text);
            if (Util.GetInstance().FileIsExists(serverPath) == false)
            {
                Dialog.ShowNotification("Tệp tin không tồn tại hoặc đã bị xóa !");
                return;
            }

            File.Delete(serverPath);
        }

        protected void btnQDLDownload_Click(object sender, DirectEventArgs e)
        {
            DownloadAttachFile("hr_WorkProcess", hdfTepTinDinhKem);
        }

        protected void btnQDLDelete_Click(object sender, DirectEventArgs e)
        {
            try
            {
                if (hdfRecordId.Text == "") return;
                DeleteTepTinDinhKem("hr_WorkProcess", int.Parse("0" + hdfRecordId.Text), hdfTepTinDinhKem);
                hdfTepTinDinhKem.Text = "";
            }
            catch (Exception)
            {
                Dialog.ShowNotification("Tệp tin không tồn tại hoặc đã bị xóa !");
            }

        }

        #endregion

        [DirectMethod]
        public void ResetForm()
        {
            cbxChonCanBo.SetValue("");
            txtDepartment.Text = string.Empty;
            txtChucVu.Text = string.Empty;
            txtCongViec.Text = string.Empty;
            txtSoQDCu.Text = string.Empty;
            dfNgayQDCu.SetValue("");
            txtNguoiQDCu.Text = string.Empty;
            dfNgayCoHieuLucCu.SetValue("");
            txtBoPhanCu.Text = string.Empty;
            txtChucVuCu.Text = string.Empty;

            txtSoQDMoi.Text = string.Empty;
            dfNgayQDMoi.SetValue("");
            cbxChucVu.SetValue("");
            dfNgayHieuLucMoi.SetValue("");
            txtNguoiQD.Text = string.Empty;
            fufTepTinDinhKem.Text = string.Empty;
            txtPositionAllowanceNew.Text = string.Empty;
            txtGhiChuMoi.Text = string.Empty;

        }

        #region Private Method
        /// <summary>
        /// create
        /// </summary>
        private void Insert()
        {
            var wp = new hr_WorkProcess
            {
                RecordId = int.Parse(cbxChonCanBo.SelectedItem.Value),
                DecisionNumber = txtSoQDMoi.Text.Trim(),
                DecisionDate = dfNgayQDMoi.SelectedDate,
                DecisionMaker = txtNguoiQD.Text.Trim(),
                EffectiveDate = dfNgayHieuLucMoi.SelectedDate,
                OldDepartmentId = 0,
                NewPositionId = 0,
                SourceDepartment = txtSourceDepartment.Text,
                CreatedDate = DateTime.Now,
                EditedDate = DateTime.Now,
                Note = txtGhiChuMoi.Text.Trim()
            };
            var util = new Util();
            if (!util.IsDateNull(dfExpireDate.SelectedDate))
                wp.ExpireDate = dfExpireDate.SelectedDate;
            var makerPosition = hdfIsMakerPosition.Text == @"0"
                ? cbxMakerPosition.Text
                : cbxMakerPosition.SelectedItem.Text;
            wp.MakerPosition = makerPosition;

            // upload file
            var path = string.Empty;
            if (fufTepTinDinhKem.HasFile)
            {
                var directory = Server.MapPath("../");
                path = UploadFile(fufTepTinDinhKem, Constant.PathAttachFile);
            }

            wp.AttachFileName = path != "" ? path : hdfTepTinDinhKem.Text;
            if (!string.IsNullOrEmpty(hdfPositionId.Text))
                wp.NewPositionId = Convert.ToInt32(hdfPositionId.Text);
            if (!string.IsNullOrEmpty(hdfOldPositionId.Text))
                wp.OldDepartmentId = Convert.ToInt32(hdfOldPositionId.Text);
            hr_WorkProcessServices.Create(wp);
            grp_QuanLyNangNgach.Reload();

            //update PositionAllowance in decision salary
            var salary = sal_SalaryDecisionServices.GetCurrent(wp.RecordId);
            //Update
            sal_SalaryDecisionServices.Update(salary);
        }

        public string UploadFile(object sender, string relativePath)
        {
            var obj = (FileUploadField) sender;
            var file = obj.PostedFile;
            var dir = new DirectoryInfo(Server.MapPath("../") + relativePath); // save file to directory HoSoNhanSu/File
            // if directory not exist then create this
            if (dir.Exists == false)
                dir.Create();
            var rdstr = Util.GetInstance().GetRandomString(7);
            var path = Server.MapPath("../") + relativePath + "/" + rdstr + "_" + obj.FileName;
            if (File.Exists(path))
                return "";
            var info = new FileInfo(path);
            file.SaveAs(path);
            return relativePath + "/" + rdstr + "_" + obj.FileName;
        }

        private void Update()
        {
            try
            {
                int id;
                if (!int.TryParse(hdfKeyRecord.Text, out id) || id <= 0) return;
                var wp = hr_WorkProcessServices.GetById(id);
                if (wp == null) return;
                wp.DecisionNumber = txtCapNhatSoQD.Text.Trim();

                wp.DecisionMaker = txtCapNhatNguoiQD.Text.Trim();
                wp.EffectiveDate = dfCapNhatNgayHieuLuc.SelectedDate;
                wp.CreatedDate = DateTime.Now;
                wp.EditedDate = DateTime.Now;
                wp.Note = txtGhiChuMoi.Text.Trim();
                var util = new Util();
                if (!util.IsDateNull(dfUpdateExpireDate.SelectedDate))
                    wp.ExpireDate = dfUpdateExpireDate.SelectedDate;
                var makerPosition = hdfIsUpdateMakerPosition.Text == @"0"
                    ? cbxUpdateMakerPosition.Text
                    : cbxUpdateMakerPosition.SelectedItem.Text;
                wp.MakerPosition = makerPosition;
                wp.SourceDepartment = txtUpdateSourceDepartment.Text;
                int newPositionId;
                if (int.TryParse(cbxChucVuMoi.SelectedItem.Value, out newPositionId) && newPositionId > 0)
                {
                    wp.NewPositionId = newPositionId;
                }

                wp.OldPositionId = !string.IsNullOrEmpty(hdfOldPositionId.Text)
                    ? Convert.ToInt32(hdfOldPositionId.Text)
                    : 0;
                int oldDepartment;
                if (int.TryParse(cbxOldDEpartment.SelectedItem.Value, out oldDepartment))
                {
                    wp.OldDepartmentId = oldDepartment;
                }

                if (!Util.GetInstance().IsDateNull(dfCapNhatNgayQD.SelectedDate))
                {
                    wp.DecisionDate = dfCapNhatNgayQD.SelectedDate;
                }
                
                hr_WorkProcessServices.Update(wp);

                //update PositionAllowance in decision salary
                var salary = sal_SalaryDecisionServices.GetCurrent(wp.RecordId);
                //Update
                sal_SalaryDecisionServices.Update(salary);
            }
            catch (Exception e)
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình cập nhật: {0}".FormatWith(e.Message));
            }

        }

        #endregion

        protected void stDepartmentList_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            stDepartmentList.DataSource = CurrentUser.DepartmentsTree;

            cbxOldDEpartment.DataBind();
        }
    }
}