using Ext.Net;
using SoftCore;
using System;
using System.Globalization;
using System.IO;
using Web.Core.Framework;
using System.Linq;
using Web.Core;
using Web.Core.Framework.Common;
using Web.Core.Object.Catalog;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.Catalog;
using Web.Core.Service.HumanRecord;
using Web.Core.Object.Security;

namespace Web.HJM.Modules.ProfileHuman.Management
{
    public partial class RewardDisciplineManagement : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ExtNet.IsAjaxRequest)
            {
                hdfTypePage.Text = Request.QueryString["type"];
                hdfMenuID.SetValue(MenuId);
                hdfUserID.SetValue(CurrentUser.User.Id);
                hdfDepartments.Text = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
                hdfCapKhenThuongKyLuatHangLoat.Text = @"0";
                // load west
                new Core.Framework.Common.BorderLayout()
                {
                    menuID = MenuId,
                    script = "#{hdfDepartmentSelectedId}.setValue('" + Core.Framework.Common.BorderLayout.nodeID +
                             "');#{txtSearch}.reset();#{PagingToolbar1}.pageIndex = 0; #{PagingToolbar1}.doLoad();"
                }.AddDepartmentList(brlayout, CurrentUser, true);
                if (hdfTypePage.Text.Equals("KhenThuong"))
                {
                    hdfTableLyDo.Text = @"cat_ReasonReward";
                    hdfValueLyDo.Text = @"Id";
                    hdfDisplayLyDo.Text = @"Name";
                    hdfTitleReport.Text = @"Báo cáo danh sách CCVC được khen thưởng";
                    hdfTypeReport.Text = @"EmployeeRewarded";
                    wdShowReport.Title = @"Báo cáo danh sách khen thưởng";
                    dfExpireDate.Hidden = true;
                    txtMoneyAmount.Hidden = false;

                }
                else
                {
                    hdfTableLyDo.Text = @"cat_ReasonDiscipline";
                    hdfValueLyDo.Text = @"Id";
                    hdfDisplayLyDo.Text = @"Name";
                    hdfTitleReport.Text = @"Báo cáo danh sách CCVC bị kỷ luật";
                    hdfTypeReport.Text = @"EmployeeDisciplined";
                    wdShowReport.Title = @"Báo cáo danh sách kỷ luật";
                    dfExpireDate.Hidden = false;
                    txtMoneyAmount.Hidden = true;
                }
            }

            if (btnEdit.Visible)
            {
                RowSelectionModel1.Listeners.RowSelect.Handler += " try{btnEdit.enable()}catch(e){};";
                RowSelectionModel1.Listeners.RowDeselect.Handler += " try{btnEdit.disable()}catch(e){};";
                grp_KhenThuongKyLuat.Listeners.RowDblClick.Handler +=
                    " if(CheckSelectedRows(grp_KhenThuongKyLuat)){Ext.net.DirectMethods.GetDataForKhenThuong();}";
            }

            if (btnDelete.Visible)
            {
                RowSelectionModel1.Listeners.RowSelect.Handler += " try{ btnDelete.enable()}catch(e){};";
                RowSelectionModel1.Listeners.RowDeselect.Handler += " try{btnDelete.disable()}catch(e){};";
            }

            ucChooseEmployee.AfterClickAcceptButton += ucChooseEmployee_AfterClickAcceptButton;

        }

        public void ucChooseEmployee_AfterClickAcceptButton(object sender, EventArgs e)
        {
            try
            {
                hdfTotalRecord.Text = ucChooseEmployee.SelectedRow.Count.ToString();
                foreach (var item in ucChooseEmployee.SelectedRow)
                {
                    // get employee information
                    var hs = RecordController.GetByEmployeeCode(item.RecordID);
                    RM.RegisterClientScriptBlock("insert" + hs.Id,
                        string.Format("addRecord('{0}', '{1}', '{2}', '{3}');", hs.Id, hs.EmployeeCode, hs.FullName,
                            hs.DepartmentName));
                }
            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Thông báo từ hệ thống", "Có lỗi xảy ra: " + ex.Message).Show();
            }
        }

        #region Event Methods

        protected void cbHinhThucKhenThuongStore_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            try
            {
                if (hdfTypePage.Text.Equals("KhenThuong"))
                {
                    cbHinhThucKhenThuongStore.DataSource = cat_RewardServices.GetAll();
                    cbHinhThucKhenThuongStore.DataBind();
                }
                else
                {
                    cbHinhThucKhenThuongStore.DataSource = cat_DisciplineServices.GetAll();
                    cbHinhThucKhenThuongStore.DataBind();
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowError("Có lỗi xảy ra: " + ex.Message);
            }
        }

        protected void StoreCapKhenThuongKyLuat_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            StoreCapKhenThuongKyLuat.DataSource = cat_LevelRewardDisciplineServices.GetAll();
            StoreCapKhenThuongKyLuat.DataBind();
        }

        protected void btnKhenThuongDownload_Click(object sender, DirectEventArgs e)
        {
            Server.MapPath("");
            DownloadAttachFile(hdfTypePage.Text.Equals("KhenThuong") ? "hr_Reward" : "hr_Discipline",
                hdfKhenThuongTepTinDinhKem);
        }

        protected void btnKhenThuongDelete_Click(object sender, DirectEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(hdfRecordId.Text))
                {
                    DeleteTepTinDinhKem(hdfTypePage.Text.Equals("KhenThuong") ? "hr_Reward" : "hr_Discipline",
                        int.Parse(hdfRecordId.Text), hdfKhenThuongTepTinDinhKem);
                    hdfKhenThuongTepTinDinhKem.Text = "";
                    RM.RegisterClientScriptBlock("deleteAttack", "fufKhenThuongTepTinDinhKem.reset();");
                }
                else
                {
                    Dialog.ShowNotification("Tệp tin không tồn tại hoặc đã bị xóa!");
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowError("Có lỗi xảy ra: " + ex.Message);
            }
        }

        protected void Delete(object sender, DirectEventArgs e)
        {
            try
            {
                foreach (var item in RowSelectionModel1.SelectedRows)
                {
                    int id;
                    if (!int.TryParse(item.RecordID, out id)) continue;
                    if (hdfTypePage.Text.Equals("KhenThuong"))
                    {
                        var hs = hr_RewardServices.GetById(id);
                        if (hs != null)
                        {
                            hr_RewardServices.Delete(id);
                        }
                    }
                    else
                    {
                        var hs = hr_DisciplineServices.GetById(id);
                        if (hs != null)
                        {
                            hr_DisciplineServices.Delete(id);
                        }
                    }
                }

                grp_KhenThuongKyLuat.Reload();
                RM.RegisterClientScriptBlock("Grid_Reload", "ReloadGrid();");
            }
            catch (Exception ex)
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình xóa: {0}".FormatWith(ex.Message));
            }

        }

        protected void InitWindowEditRewardOrDiscipline(object sender, DirectEventArgs e)
        {
            int id;
            if (!int.TryParse(hdfRecordId.Text, out id)) return;
            if (hdfTypePage.Text.Equals("KhenThuong"))
            {
                var hs = RewardController.GetById(id);
                if (hs != null)
                {
                    txtKhenThuongSoQuyetDinh.Text = hs.DecisionNumber;
                    tgf_KhenThuong_NguoiRaQD.Text = hs.DecisionMaker;
                    if (hs.DecisionDate != null) dfKhenThuongNgayQuyetDinh.SelectedDate = (DateTime) hs.DecisionDate;
                    if (hs.EffectiveDate != null) dfKhenThuongNgayHieuLuc.SelectedDate = (DateTime) hs.EffectiveDate;
                    cbHinhThucKhenThuong.Text = hs.FormRewardName;
                    cbLyDoKhenThuong.Text = hs.Reason;
                    cbxCapKhenThuong.Text = hs.LevelRewardName;
                    txtKhenThuongGhiChu.Text = hs.Note;
                    txtUpdateSourceDepartment.Text = hs.SourceDepartment;
                    cbxUpdateMakerPosition.Text = hs.MakerPosition;
                    txtUpdateMoneyAmount.Text = hs.MoneyAmount.ToString(CultureInfo.InvariantCulture);
                    if (!string.IsNullOrEmpty(hs.AttachFileName))
                    {
                        var pos = hs.AttachFileName.LastIndexOf('/');
                        if (pos != -1)
                        {
                            var tenTt = hs.AttachFileName.Substring(pos + 1);
                            fufKhenThuongTepTinDinhKem.Text = tenTt;
                        }

                        hdfKhenThuongTepTinDinhKem.Text = hs.AttachFileName;
                    }
                }

                dfUpdateExpireDate.Hidden = true;
                txtUpdateMoneyAmount.Hidden = false;
                wdKhenThuong.Show();
            }
            else
            {
                var hs = DisciplineController.GetById(id);
                if (hs != null)
                {
                    txtKhenThuongSoQuyetDinh.Text = hs.DecisionNumber;
                    tgf_KhenThuong_NguoiRaQD.Text = hs.DecisionMaker;
                    if (hs.DecisionDate != null) dfKhenThuongNgayQuyetDinh.SelectedDate = (DateTime) hs.DecisionDate;
                    cbHinhThucKhenThuong.Text = hs.FormDisciplineName;
                    cbLyDoKhenThuong.Text = hs.Reason;
                    cbxCapKhenThuong.Text = hs.LevelDisciplineName;
                    txtKhenThuongGhiChu.Text = hs.Note;
                    txtUpdateSourceDepartment.Text = hs.SourceDepartment;
                    cbxUpdateMakerPosition.Text = hs.MakerPosition;
                    if (hs.ExpireDate != null)
                        dfUpdateExpireDate.SetValue(hs.ExpireDate);
                    if (!string.IsNullOrEmpty(hs.AttachFileName))
                    {
                        var pos = hs.AttachFileName.LastIndexOf('/');
                        if (pos != -1)
                        {
                            var tenTt = hs.AttachFileName.Substring(pos + 1);
                            fufKhenThuongTepTinDinhKem.Text = tenTt;
                        }

                        hdfKhenThuongTepTinDinhKem.Text = hs.AttachFileName;
                    }
                }

                dfUpdateExpireDate.Hidden = false;
                txtUpdateMoneyAmount.Hidden = true;
                wdKhenThuong.Show();
            }
        }

        #endregion

        #region Direct Methods

        [DirectMethod]
        protected void InsertOrUpdate(object sender, DirectEventArgs e)
        {
            if (e.ExtraParams["Command"] == "Create")
                Create(e);
            else if (e.ExtraParams["Command"] == "Update")
            {
                Update();
            }
        }

        [DirectMethod]
        public void ResetForm()
        {
            //Create
            grp_DanhSachCanBoStore.RemoveAll();
            hdfAddMore.Reset();
            txtSoQuyetDinhHangLoat.Text = string.Empty;
            hdfNguoiQuyetDinhHangLoat.Reset();
            tgfNguoiQuyetDinhHangLoat.Text = string.Empty;
            hdfHinhThucHangLoat.Reset();
            cbbHinhThucHangLoat.SetValue("");
            dfNgayQuyetDinhHangLoat.SetValue("");
            hdfLyDoHangLoat.Reset();
            cbbLyDoHangLoat.SetValue("");
            hdfCapKhenThuongKyLuatHangLoat.Reset();
            cbbCapKhenThuongKyLuatHangLoat.SetValue("");
            hdfTepDinhKemHangLoat.Reset();
            fufTepTinHangLoat.Reset();
            txtGhuChuHangLoat.Text = string.Empty;


            //Update
            txtKhenThuongSoQuyetDinh.Text = string.Empty;
            dfKhenThuongNgayQuyetDinh.Text = string.Empty;
            cbLyDoKhenThuong.Text = string.Empty;
            cbHinhThucKhenThuong.Text = string.Empty;
            hdfHinhThucKhenThuongKyLuat.Text = string.Empty;
            cbxCapKhenThuong.Text = string.Empty;
            hdfCapKhenThuongKyLuat.Text = string.Empty;
            txtKhenThuongGhiChu.Text = string.Empty;
            hdfKhenThuongNguoiQD.Text = string.Empty;
            tgf_KhenThuong_NguoiRaQD.Text = string.Empty;
            fufKhenThuongTepTinDinhKem.Text = string.Empty;
            hdfKhenThuongTepTinDinhKem.Text = string.Empty;
            hdfLyDoKTTemp.Text = string.Empty;
            hdfIsDanhMuc.Text = string.Empty;
            dfNgayQuyetDinhHangLoat.SetValue("");
            dfNgayHieuLuc.SetValue("");
        }

        [DirectMethod]
        public void ResetWindowTitle()
        {
            if (hdfTypePage.Text == @"KyLuat")
            {
                wdKhenThuong.Title = @"Thông tin kỷ luật";
                cbxCapKhenThuong.FieldLabel = @"Cấp kỷ luật<span style='color:red'>*</span>";
                cbLyDoKhenThuong.FieldLabel = @"Lý do kỷ luật<span style='color:red'>*</span>";
            }
            else
            {
                wdKhenThuong.Title = @"Thông tin khen thưởng";
                cbLyDoKhenThuong.FieldLabel = @"Lý do khen thưởng<span style='color:red'>*</span>";
                cbxCapKhenThuong.FieldLabel = @"Cấp khen thưởng<span style='color:red'>*</span>";
            }

            txtKhenThuongSoQuyetDinh.Focus();
        }

        #endregion

        #region Private Methods

        private void Update()
        {
            try
            {
                var util = new Util();
                int id;
                if (hdfTypePage.Text.Equals("KhenThuong"))
                {
                    if (!int.TryParse(hdfRecordId.Text, out id)) return;
                    var hs = hr_RewardServices.GetById(id);
                    if (hs == null) return;
                    hs.DecisionNumber = txtKhenThuongSoQuyetDinh.Text.Trim();
                    hs.DecisionMaker = tgf_KhenThuong_NguoiRaQD.Text.Trim();
                    if (!util.IsDateNull(dfKhenThuongNgayQuyetDinh.SelectedDate))
                    {
                        hs.DecisionDate = dfKhenThuongNgayQuyetDinh.SelectedDate;
                    }

                    if (!util.IsDateNull(dfKhenThuongNgayHieuLuc.SelectedDate))
                    {
                        hs.EffectiveDate = dfKhenThuongNgayHieuLuc.SelectedDate;
                    }

                    int formRewardId;
                    if (int.TryParse(cbHinhThucKhenThuong.SelectedItem.Value, out formRewardId))
                    {
                        hs.FormRewardId = formRewardId;
                    }

                    int levelRewardId;
                    if (int.TryParse(cbxCapKhenThuong.SelectedItem.Value, out levelRewardId))
                    {
                        hs.LevelRewardId = levelRewardId;
                    }

                    hs.Reason = cbLyDoKhenThuong.SelectedItem.Text;
                    hs.Note = txtKhenThuongGhiChu.Text.Trim();
                    hs.SourceDepartment = txtUpdateSourceDepartment.Text;
                    if (!string.IsNullOrEmpty(txtUpdateMoneyAmount.Text))
                        hs.MoneyAmount = decimal.Parse("0" + txtUpdateMoneyAmount.Text);
                    var makerPosition = hdfIsUpdateMakerPosition.Text == @"0"
                        ? cbxUpdateMakerPosition.Text
                        : cbxUpdateMakerPosition.SelectedItem.Text;
                    hs.MakerPosition = makerPosition;
                    hs.EditedDate = DateTime.Now;
                    var path = string.Empty;
                    if (fufKhenThuongTepTinDinhKem.HasFile)
                    {
                        path = UploadFile(fufKhenThuongTepTinDinhKem,
                            hdfTypePage.Text.Equals("KhenThuong") ? Constant.PathReward : Constant.PathDiscipline);
                    }

                    hs.AttachFileName = path != "" ? path : hdfKhenThuongTepTinDinhKem.Text.Trim();
                    hr_RewardServices.Update(hs);
                }
                else
                {
                    if (!int.TryParse(hdfRecordId.Text, out id)) return;
                    var hs = hr_DisciplineServices.GetById(id);
                    if (hs == null) return;
                    hs.DecisionNumber = txtKhenThuongSoQuyetDinh.Text.Trim();
                    hs.DecisionMaker = tgf_KhenThuong_NguoiRaQD.Text.Trim();
                    if (!util.IsDateNull(dfKhenThuongNgayQuyetDinh.SelectedDate))
                    {
                        hs.DecisionDate = dfKhenThuongNgayQuyetDinh.SelectedDate;
                    }

                    if (!util.IsDateNull(dfUpdateExpireDate.SelectedDate))
                        hs.ExpireDate = dfUpdateExpireDate.SelectedDate;
                    int formRewardId;
                    if (int.TryParse(cbHinhThucKhenThuong.SelectedItem.Value, out formRewardId))
                    {
                        hs.FormDisciplineId = formRewardId;
                    }

                    int levelRewardId;
                    if (int.TryParse(cbxCapKhenThuong.SelectedItem.Value, out levelRewardId))
                    {
                        hs.LevelDisciplineId = levelRewardId;
                    }

                    hs.Reason = cbLyDoKhenThuong.SelectedItem.Text;
                    hs.Note = txtKhenThuongGhiChu.Text.Trim();
                    hs.SourceDepartment = txtUpdateSourceDepartment.Text;
                    var makerPosition = hdfIsUpdateMakerPosition.Text == @"0"
                        ? cbxUpdateMakerPosition.Text
                        : cbxUpdateMakerPosition.SelectedItem.Text;
                    hs.MakerPosition = makerPosition;
                    hs.EditedDate = DateTime.Now;
                    var path = string.Empty;
                    if (fufKhenThuongTepTinDinhKem.HasFile)
                    {
                        path = UploadFile(fufKhenThuongTepTinDinhKem,
                            hdfTypePage.Text.Equals("KhenThuong") ? Constant.PathReward : Constant.PathDiscipline);
                    }

                    hs.AttachFileName = path != "" ? path : hdfKhenThuongTepTinDinhKem.Text.Trim();
                    hr_DisciplineServices.Update(hs);
                }

                grp_KhenThuongKyLuat.Reload();
                RM.RegisterClientScriptBlock("Grid_Reload", "ReloadGrid();");
            }
            catch (Exception e)
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình cập nhật: {0}".FormatWith(e.Message));
            }

        }

        private void Create(DirectEventArgs e)
        {
            try
            {
                var path = string.Empty;
                var util = new Util();
                if (fufTepTinHangLoat.HasFile)
                {
                    path = UploadFile(fufTepTinHangLoat,
                        hdfTypePage.Text.Equals("KhenThuong") ? Constant.PathReward : Constant.PathDiscipline);
                }

                var listId = e.ExtraParams["ListId"].Split(',');
                if (listId.Length <= 1)
                {
                    Dialog.Alert("Bạn chưa chọn cán bộ nào!");
                    return;
                }

                for (var i = 0; i < listId.Length - 1; i++)
                {
                    var id = listId[i];
                    if (hdfTypePage.Text.Equals("KhenThuong"))
                    {
                        string reasonReward;
                        if (hdfIsDanhMuc2.Text == @"0")
                        {
                            var reason = new cat_ReasonReward {Name = cbbLyDoHangLoat.Text};
                            cat_ReasonRewardServices.Create(reason);
                            reasonReward = cbbLyDoHangLoat.Text;
                        }
                        else
                        {
                            reasonReward = cbbLyDoHangLoat.SelectedItem.Text;
                        }

                        if (string.IsNullOrEmpty(reasonReward))
                        {
                            ExtNet.Msg.Alert("Thông báo", "Không tìm thấy lý do khen thưởng. Vui lòng thử lại!").Show();
                            return;
                        }

                        if (txtSoQuyetDinhHangLoat.Text == "")
                        {
                            GenerateRewardDecisionNumber();
                        }

                        var record = new hr_Reward
                        {
                            RecordId = int.Parse(id),
                            DecisionNumber = txtSoQuyetDinhHangLoat.Text.Trim(),
                            DecisionMaker = tgfNguoiQuyetDinhHangLoat.Text.Trim(),
                            FormRewardId = int.Parse(hdfHinhThucHangLoat.Text),
                            LevelRewardId = int.Parse(hdfCapKhenThuongKyLuatHangLoat.Text),
                            Reason = reasonReward,
                            SourceDepartment = txtSourceDepartment.Text,
                            Note = txtGhuChuHangLoat.Text.Trim()
                        };
                        var makerPosition = hdfIsMakerPosition.Text == @"0"
                            ? cbxMakerPosition.Text
                            : cbxMakerPosition.SelectedItem.Text;
                        record.MakerPosition = makerPosition;
                        if (!string.IsNullOrEmpty(txtMoneyAmount.Text))
                            record.MoneyAmount = decimal.Parse("0" + txtMoneyAmount.Text);
                        record.CreatedDate = DateTime.Now;
                        if (!util.IsDateNull(dfNgayQuyetDinhHangLoat.SelectedDate))
                        {
                            record.DecisionDate = dfNgayQuyetDinhHangLoat.SelectedDate;
                        }

                        if (!util.IsDateNull(dfNgayHieuLuc.SelectedDate))
                        {
                            record.EffectiveDate = dfNgayHieuLuc.SelectedDate;
                        }

                        record.AttachFileName = path != "" ? path : hdfTepDinhKemHangLoat.Text;
                        hr_RewardServices.Create(record);
                    }
                    else
                    {
                        string reasonDiscipline;
                        if (hdfIsDanhMuc2.Text == @"0")
                        {
                            var reason = new cat_ReasonDiscipline() {Name = cbbLyDoHangLoat.Text};
                            cat_ReasonDisciplineServices.Create(reason);
                            reasonDiscipline = cbbLyDoHangLoat.Text;
                        }
                        else
                        {
                            reasonDiscipline = cbbLyDoHangLoat.SelectedItem.Text;
                        }

                        if (string.IsNullOrEmpty(reasonDiscipline))
                        {
                            ExtNet.Msg.Alert("Thông báo", "Không tìm thấy lý do khen thưởng. Vui lòng thử lại!").Show();
                            return;
                        }

                        if (txtSoQuyetDinhHangLoat.Text == "")
                        {
                            GenerateRewardDecisionNumber();
                        }

                        var record = new hr_Discipline
                        {
                            RecordId = int.Parse(id),
                            DecisionNumber = txtSoQuyetDinhHangLoat.Text.Trim(),
                            DecisionMaker = tgfNguoiQuyetDinhHangLoat.Text.Trim(),
                            FormDisciplineId = int.Parse(hdfHinhThucHangLoat.Text),
                            LevelDisciplineId = int.Parse(hdfCapKhenThuongKyLuatHangLoat.Text),
                            Reason = reasonDiscipline,
                            SourceDepartment = txtSourceDepartment.Text,
                            Note = txtGhuChuHangLoat.Text.Trim()
                        };
                        var makerPosition = hdfIsMakerPosition.Text == @"0"
                            ? cbxMakerPosition.Text
                            : cbxMakerPosition.SelectedItem.Text;
                        record.MakerPosition = makerPosition;
                        record.CreatedDate = DateTime.Now;
                        if (!util.IsDateNull(dfNgayQuyetDinhHangLoat.SelectedDate))
                        {
                            record.DecisionDate = dfNgayQuyetDinhHangLoat.SelectedDate;
                        }

                        if (!util.IsDateNull(dfExpireDate.SelectedDate))
                            record.ExpireDate = dfExpireDate.SelectedDate;
                        record.AttachFileName = path != "" ? path : hdfTepDinhKemHangLoat.Text;
                        hr_DisciplineServices.Create(record);
                    }
                }

                grp_KhenThuongKyLuat.Reload();
                ResetForm();
                wdKhenThuongKyLuatHangLoat.Hide();
            }
            catch (Exception ex)
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình tạo quyết định: {0}".FormatWith(ex.Message));
            }

        }

        #endregion

        public string UploadFile(object sender, string relativePath)
        {
            var obj = (FileUploadField) sender;
            var file = obj.PostedFile;
            var
                dir = new DirectoryInfo(Server.MapPath("../") + relativePath); // save file to directory
            // if directory not exist then create this
            if (dir.Exists == false)
                dir.Create();
            var rdstr = Util.GetInstance().GetRandomString(7);
            var path = Server.MapPath("../") + relativePath + "/" + rdstr + "_" + obj.FileName;
            if (File.Exists(path))
                return "";
            var fileInfo = new FileInfo(path);
            file.SaveAs(path);
            return relativePath + "/" + rdstr + "_" + obj.FileName;
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

                var serverPath = Server.MapPath("../") + sender.Text;
                if (Util.GetInstance().FileIsExists(serverPath) == false)
                {
                    Dialog.ShowNotification("Tệp tin không tồn tại hoặc đã bị xóa !");
                    return;
                }

                Response.Clear();
                //  Response.ContentType = "application/octet-stream";

                Response.AddHeader("Content-Disposition", "attachment; filename=" + sender.Text.Replace(" ", "_"));
                Response.WriteFile(serverPath);
                Response.End();
            }
            catch (Exception ex)
            {
                Dialog.ShowError("Lỗi xảy ra " + ex.Message);
            }
        }

        public void DeleteTepTinDinhKem(string tableName, int prkey, Hidden sender)
        {
            try
            {
                // xóa trong csdl
                var reward = hr_RewardServices.GetById(prkey);
                reward.AttachFileName = "";
                new RewardController().Update(reward);
                // xóa file trong thư mục
                var serverPath = Server.MapPath("../") + sender.Text;
                if (Util.GetInstance().FileIsExists(serverPath) == false)
                {
                    Dialog.ShowNotification("Tệp tin không tồn tại hoặc đã bị xóa !");
                    return;
                }

                File.Delete(serverPath);
            }
            catch (Exception ex)
            {
                Dialog.Alert("Có lỗi xảy ra: " + ex.Message);
            }
        }

        public void GenerateRewardDecisionNumber()
        {
            var departments = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
            var suffix =
                SystemConfigController.GetValueByNameFollowDepartment(SystemConfigParameter.SUFFIX_SOHOPDONG,
                    departments);
            if (string.IsNullOrEmpty(suffix))
                suffix = "QĐ";
            var decisioNumber = GenerateDecisionNumber(suffix);
            txtSoQuyetDinhHangLoat.Text = decisioNumber;
        }

        private static string GenerateDecisionNumber(string suffix)
        {
            // get full suffix
            if (string.IsNullOrEmpty(suffix))
                return "";
            suffix = DateTime.Now.Year + "/" + suffix;
            // get max number of constract
            var rewardNumber = RewardController.GetRewardNumberByCondition(suffix);
            if (rewardNumber == null || rewardNumber.Id <= 0) return "001/" + suffix;
            var sohd = rewardNumber.DecisionNumber;
            var pos = sohd.IndexOf('/');
            if (pos == -1) return "001/" + suffix;
            var stt = sohd.Trim().Substring(0, pos);
            var number = int.Parse(stt);
            stt = "0000" + (number + 1);
            stt = stt.Substring(stt.Length - 3);
            stt = stt + "/" + suffix;
            return stt;
            // chưa có số hợp đồng nào theo định dạng
        }
    }
}