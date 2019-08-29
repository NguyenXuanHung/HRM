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

namespace Web.HRM.Modules.ProfileHuman.Management
{
    public partial class WorkProcessManagement : BasePage
    {                
        protected void Page_Load(object sender, EventArgs e)
        {
            // Set resource
            grp_QuanLyNangNgach.ColumnModel.SetColumnHeader(3, "{0}".FormatWith(Resource.Get("Employee.Code")));


            hdfMaDonVi.Text = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
            hdfUserID.SetValue(CurrentUser.User.Id);
            hdfMenuID.SetValue(MenuId);
            hdfDepartments.Text = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
            new Core.Framework.Common.BorderLayout()
            {
                menuID = MenuId,
                script = "#{hdfDepartmentSelectedId}.setValue('" + Core.Framework.Common.BorderLayout.nodeID +
                         "');#{txtSearch}.reset();#{PagingToolbar1}.pageIndex = 0; #{PagingToolbar1}.doLoad();"
            }.AddDepartmentList(brlayout, CurrentUser, false);
            if (btnEdit.Visible)
            {
                grp_QuanLyNangNgach.Listeners.RowDblClick.Handler +=
                    " if(CheckSelectedRows(grp_QuanLyNangNgach)){Ext.net.DirectMethods.GetDataForCanBo();}";
            }
        }

        #region Event Method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cbxChucVu_Selected(object sender, DirectEventArgs e)
        {
            var positionId = int.Parse(cbxChucVu.SelectedItem.Value);
            var lstPosition = cat_PositionServices.GetById(positionId);
            // var data = lst
            if (lstPosition != null)
            {
                txtPhuCapChucVuMoi.Text = cat_PositionServices.GetFieldValueById(positionId, "PositionAllowance");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cbxChucVuMoi_Selected(object sender, DirectEventArgs e)
        {

            var positionId = int.Parse(cbxChucVuMoi.SelectedItem.Value);
            var lstPosition = cat_PositionServices.GetById(positionId);
            // var data = lst
            if (lstPosition != null)
            {
                txtPhuCapChucVuMoi.Text = cat_PositionServices.GetFieldValueById(positionId, "PositionAllowance");
            }            
        }

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="directEventArgs"></param>
        protected void Delete(object sender, DirectEventArgs directEventArgs)
        {
            try
            {
                if (!int.TryParse(hdfKeyRecord.Text, out var id) || id <= 0) return;
                hr_WorkProcessServices.Delete(id);
                grp_QuanLyNangNgach.Reload();
                RM.RegisterClientScriptBlock("Grid_Reload", "ReloadGrid();");
            }
            catch (Exception ex)
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình xóa: {0}".FormatWith(ex.Message));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InitWindowWorkProcess(object sender, DirectEventArgs e)
        {
            if (!int.TryParse(hdfKeyRecord.Text, out var id) || id <= 0) return;
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

            // init command name & window properties
            hdfCommandName.Text = @"Update";
            hdfCommandUpdate.Text = @"Update";
            // show window
            wdCapNhatBoNhiemChucVu.Show();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ShowReport_Click(object sender, DirectEventArgs e)
        {
            BindQuyetDinhBoNhiemCb();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void stDepartmentList_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            stDepartmentList.DataSource = CurrentUser.DepartmentsTree;

            cbxOldDEpartment.DataBind();
        }                

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="prkey"></param>
        /// <param name="sender"></param>
        public void DeleteTepTinDinhKem(string tableName, decimal prkey, Hidden sender)
        {
            // TODO : using controller
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQDLDownload_Click(object sender, DirectEventArgs e)
        {
            DownloadAttachFile("hr_WorkProcess", hdfTepTinDinhKem);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 
        /// </summary>
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
            txtPhuCapChucVuMoi.Text = string.Empty;
            txtGhiChuMoi.Text = string.Empty;

        }

        #region Private Method

        /// <summary>
        /// 
        /// </summary>
        private void Insert()
        {
            try
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
            }
            catch (Exception e)
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình thêm mới: {0}".FormatWith(e.Message));
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
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
            //        .Replace(Constant.HO_TEN,
            //            string.IsNullOrEmpty(hoso.RecordId.ToString())
            //                ? "..."
            //                : hr_RecordServices.GetFieldValueById(hoso.RecordId, "FullName"))
            //        .Replace(Constant.TEN_COQUAN,
            //            string.IsNullOrEmpty(rct.GetCompanyName("1")) ? "..." : rct.GetCompanyName("1"))
            //        .Replace(Constant.CHUC_VU,
            //            string.IsNullOrEmpty(hoso.OldDepartmentId.ToString())
            //                ? "..."
            //                : cat_PositionServices.GetFieldValueById(hoso.OldDepartmentId, "Name"))
            //        .Replace(Constant.CVM,
            //            string.IsNullOrEmpty(hoso.NewPositionId.ToString())
            //                ? "..."
            //                : cat_PositionServices.GetFieldValueById(hoso.NewPositionId, "Name"))
            //        .Replace(Constant.SO_QD, string.IsNullOrEmpty(hoso.DecisionNumber) ? "..." : hoso.DecisionNumber)
            //        .Replace(Constant.PHONG_BAN,
            //            string.IsNullOrEmpty(hoso.NewDepartmentId.ToString())
            //                ? "..."
            //                : cat_DepartmentServices.GetFieldValueById(hoso.NewDepartmentId, "Name"))
            //        .Replace(Constant.TP, string.IsNullOrEmpty(rct.GetCityName("1")) ? "..." : rct.GetCityName("1"))
            //        .Replace(Constant.THUTRUONG,
            //            string.IsNullOrEmpty(rct.GetHeadOfHRroom("1", filter.Name3))
            //                ? "..."
            //                : rct.GetHeadOfHRroom("1", filter.Name3)).Replace(Constant.DAY, today.Day.ToString())
            //        .Replace(Constant.MONTH, today.Month.ToString()).Replace(Constant.YEAR, today.Year.ToString())
            //        .Replace(Constant.NGAY_HIEN_TAI, ngayct2);
            //    htmlEditor.Value = rs;
            //}
            //else
            //{
            //    htmlEditor.Value = content;
            //}

            //wdShowReport.Show();
        }

        /// <summary>
        /// 
        /// </summary>
        private void Update()
        {
            try
            {
                if (!int.TryParse(hdfKeyRecord.Text, out var id) || id <= 0) return;
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
                if (int.TryParse(cbxChucVuMoi.SelectedItem.Value, out var newPositionId) && newPositionId > 0)
                {
                    wp.NewPositionId = newPositionId;
                }

                wp.OldPositionId = !string.IsNullOrEmpty(hdfOldPositionId.Text)
                    ? Convert.ToInt32(hdfOldPositionId.Text)
                    : 0;
                if (int.TryParse(cbxOldDEpartment.SelectedItem.Value, out var oldDepartment))
                {
                    wp.OldDepartmentId = oldDepartment;
                }

                if (!Util.GetInstance().IsDateNull(dfCapNhatNgayQD.SelectedDate))
                {
                    wp.DecisionDate = dfCapNhatNgayQD.SelectedDate;
                }

                hr_WorkProcessServices.Update(wp);
            }
            catch (Exception e)
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình cập nhật: {0}".FormatWith(e.Message));
            }

        }

        #endregion        
    }
}