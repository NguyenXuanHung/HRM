using Ext.Net;
using SoftCore;
using System;
using System.Globalization;
using Web.Core.Framework;
using System.Linq;
using Web.Core;
using Web.Core.Framework.Common;
using Web.Core.Helper;
using Web.Core.Object.Catalog;
using Web.Core.Service.Catalog;
using Web.Core.Service.HumanRecord;
using Web.Core.Object.Security;

namespace Web.HRM.Modules.ProfileHuman.Management
{
    public partial class RewardDisciplineManagement : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!ExtNet.IsAjaxRequest)
            {
                hdfTypePage.Text = Request.QueryString["type"];
                hdfDepartments.Text = DepartmentIds;
               
                // load west
                new Core.Framework.Common.BorderLayout()
                {
                    menuID = MenuId,
                    script = "#{hdfDepartmentSelectedId}.setValue('" + Core.Framework.Common.BorderLayout.nodeID +
                             "');#{txtSearch}.reset();#{PagingToolbar1}.pageIndex = 0; #{PagingToolbar1}.doLoad();"
                }.AddDepartmentList(brlayout, CurrentUser, false);
                if(hdfTypePage.Text.Equals("KhenThuong"))
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

        }

        #region Event Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cbHinhThucKhenThuongStore_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            try
            {
                if(hdfTypePage.Text.Equals("KhenThuong"))
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
            catch(Exception ex)
            {
                Dialog.ShowError("Có lỗi xảy ra: " + ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void storeLevelMany_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            storeLevelMany.DataSource = cat_LevelRewardDisciplineServices.GetAll();
            storeLevelMany.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnKhenThuongDownload_Click(object sender, DirectEventArgs e)
        {
            Server.MapPath("");
            DownloadAttachFile(hdfTypePage.Text.Equals("KhenThuong") ? "hr_Reward" : "hr_Discipline",
                hdfKhenThuongTepTinDinhKem);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnKhenThuongDelete_Click(object sender, DirectEventArgs e)
        {
            try
            {
                if(!string.IsNullOrEmpty(hdfRecordId.Text))
                {
                    DeleteAttackFile(hdfTypePage.Text.Equals("KhenThuong") ? "hr_Reward" : "hr_Discipline",
                        int.Parse(hdfRecordId.Text), hdfKhenThuongTepTinDinhKem);
                    hdfKhenThuongTepTinDinhKem.Text = "";
                    RM.RegisterClientScriptBlock("deleteAttack", "fufKhenThuongTepTinDinhKem.reset();");
                }
                else
                {
                    Dialog.ShowNotification("Tệp tin không tồn tại hoặc đã bị xóa!");
                }
            }
            catch(Exception ex)
            {
                Dialog.ShowError("Có lỗi xảy ra: " + ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Delete(object sender, DirectEventArgs e)
        {
            try
            {
                foreach(var item in RowSelectionModel1.SelectedRows)
                {
                    if(!int.TryParse(item.RecordID, out var id)) continue;
                    if(hdfTypePage.Text.Equals("KhenThuong"))
                    {
                        var hs = hr_RewardServices.GetById(id);
                        if(hs != null)
                        {
                            hr_RewardServices.Delete(id);
                        }
                    }
                    else
                    {
                        var hs = hr_DisciplineServices.GetById(id);
                        if(hs != null)
                        {
                            hr_DisciplineServices.Delete(id);
                        }
                    }
                }

                grp_KhenThuongKyLuat.Reload();
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
        protected void InitWindowEditRewardOrDiscipline(object sender, DirectEventArgs e)
        {
            if(!int.TryParse(hdfRecordId.Text, out var id)) return;
            if(hdfTypePage.Text.Equals("KhenThuong"))
            {
                var hs = RewardController.GetById(id);
                if(hs != null)
                {
                    txtKhenThuongSoQuyetDinh.Text = hs.DecisionNumber;
                    tgf_KhenThuong_NguoiRaQD.Text = hs.DecisionMaker;
                    if(hs.DecisionDate != null) dfKhenThuongNgayQuyetDinh.SelectedDate = (DateTime)hs.DecisionDate;
                    if(hs.EffectiveDate != null) dfKhenThuongNgayHieuLuc.SelectedDate = (DateTime)hs.EffectiveDate;
                    cbHinhThucKhenThuong.Text = hs.FormName;
                    cbLyDoKhenThuong.Text = hs.Reason;
                    cbxCapKhenThuong.Text = hs.LevelName;
                    txtKhenThuongGhiChu.Text = hs.Note;
                    txtUpdateSourceDepartment.Text = hs.SourceDepartment;
                    cbxUpdateMakerPosition.Text = hs.MakerPosition;
                    txtUpdateMoneyAmount.Text = hs.MoneyAmount.ToString(CultureInfo.InvariantCulture);
                    if(!string.IsNullOrEmpty(hs.AttachFileName))
                    {
                        var pos = hs.AttachFileName.LastIndexOf('/');
                        if(pos != -1)
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
                if(hs != null)
                {
                    txtKhenThuongSoQuyetDinh.Text = hs.DecisionNumber;
                    tgf_KhenThuong_NguoiRaQD.Text = hs.DecisionMaker;
                    if(hs.DecisionDate != null) dfKhenThuongNgayQuyetDinh.SelectedDate = (DateTime)hs.DecisionDate;
                    cbHinhThucKhenThuong.Text = hs.FormName;
                    cbLyDoKhenThuong.Text = hs.Reason;
                    cbxCapKhenThuong.Text = hs.LevelName;
                    txtKhenThuongGhiChu.Text = hs.Note;
                    txtUpdateSourceDepartment.Text = hs.SourceDepartment;
                    cbxUpdateMakerPosition.Text = hs.MakerPosition;
                    if(hs.ExpireDate != null)
                        dfUpdateExpireDate.SetValue(hs.ExpireDate);
                    if(!string.IsNullOrEmpty(hs.AttachFileName))
                    {
                        var pos = hs.AttachFileName.LastIndexOf('/');
                        if(pos != -1)
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [DirectMethod]
        protected void InsertOrUpdate(object sender, DirectEventArgs e)
        {
            if(e.ExtraParams["Command"] == "Create")
                Create(e);
            else if(e.ExtraParams["Command"] == "Update")
            {
                Update();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [DirectMethod]
        public void ResetForm()
        {
            //Create
            hdfAddMore.Reset();
            txtSoQuyetDinhHangLoat.Text = string.Empty;
            hdfNguoiQuyetDinhHangLoat.Reset();
            tgfNguoiQuyetDinhHangLoat.Text = string.Empty;
            hdfHinhThucHangLoat.Reset();
            cbbHinhThucHangLoat.SetValue("");
            dfNgayQuyetDinhHangLoat.SetValue("");
            hdfLyDoHangLoat.Reset();
            cbbLyDoHangLoat.SetValue("");
            hdfLevelManyId.Reset();
            cboLevelMany.Reset();
            hdfTepDinhKemHangLoat.Reset();
            fufTepTinHangLoat.Reset();
            txtGhuChuHangLoat.Text = string.Empty;
            txtMoneyAmount.Reset();
            cbxMakerPosition.Reset();
            hdfMakerPosition.Reset();
            txtSourceDepartment.Reset();

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
            if(hdfTypePage.Text == @"KyLuat")
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

        /// <summary>
        /// 
        /// </summary>
        private void Update()
        {
            try
            {
                var util = new Util();
                int id;
                if(hdfTypePage.Text.Equals("KhenThuong"))
                {
                    if(!int.TryParse(hdfRecordId.Text, out id)) return;
                    var hs = hr_RewardServices.GetById(id);
                    if(hs == null) return;
                    hs.DecisionNumber = txtKhenThuongSoQuyetDinh.Text.Trim();
                    hs.DecisionMaker = tgf_KhenThuong_NguoiRaQD.Text.Trim();
                    if(!util.IsDateNull(dfKhenThuongNgayQuyetDinh.SelectedDate))
                    {
                        hs.DecisionDate = dfKhenThuongNgayQuyetDinh.SelectedDate;
                    }

                    if(!util.IsDateNull(dfKhenThuongNgayHieuLuc.SelectedDate))
                    {
                        hs.EffectiveDate = dfKhenThuongNgayHieuLuc.SelectedDate;
                    }

                    if(int.TryParse(cbHinhThucKhenThuong.SelectedItem.Value, out var formRewardId))
                    {
                        hs.FormRewardId = formRewardId;
                    }

                    if(int.TryParse(cbxCapKhenThuong.SelectedItem.Value, out var levelRewardId))
                    {
                        hs.LevelRewardId = levelRewardId;
                    }

                    hs.Reason = cbLyDoKhenThuong.SelectedItem.Text;
                    hs.Note = txtKhenThuongGhiChu.Text.Trim();
                    hs.SourceDepartment = txtUpdateSourceDepartment.Text;
                    if(!string.IsNullOrEmpty(txtUpdateMoneyAmount.Text))
                        hs.MoneyAmount = decimal.Parse("0" + txtUpdateMoneyAmount.Text);
                    var makerPosition = hdfIsUpdateMakerPosition.Text == @"0"
                        ? cbxUpdateMakerPosition.Text
                        : cbxUpdateMakerPosition.SelectedItem.Text;
                    hs.MakerPosition = makerPosition;
                    hs.EditedDate = DateTime.Now;
                    var path = string.Empty;
                    if(fufKhenThuongTepTinDinhKem.HasFile)
                    {
                        path = UploadFile(fufKhenThuongTepTinDinhKem,
                            hdfTypePage.Text.Equals("KhenThuong") ? Constant.PathReward : Constant.PathDiscipline);
                    }

                    hs.AttachFileName = path != "" ? path : hdfKhenThuongTepTinDinhKem.Text.Trim();
                    hr_RewardServices.Update(hs);
                }
                else
                {
                    if(!int.TryParse(hdfRecordId.Text, out id)) return;
                    var hs = hr_DisciplineServices.GetById(id);
                    if(hs == null) return;
                    hs.DecisionNumber = txtKhenThuongSoQuyetDinh.Text.Trim();
                    hs.DecisionMaker = tgf_KhenThuong_NguoiRaQD.Text.Trim();
                    if(!util.IsDateNull(dfKhenThuongNgayQuyetDinh.SelectedDate))
                    {
                        hs.DecisionDate = dfKhenThuongNgayQuyetDinh.SelectedDate;
                    }

                    if(!util.IsDateNull(dfUpdateExpireDate.SelectedDate))
                        hs.ExpireDate = dfUpdateExpireDate.SelectedDate;
                    if(int.TryParse(cbHinhThucKhenThuong.SelectedItem.Value, out var formRewardId))
                    {
                        hs.FormDisciplineId = formRewardId;
                    }

                    if(int.TryParse(cbxCapKhenThuong.SelectedItem.Value, out var levelRewardId))
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
                    if(fufKhenThuongTepTinDinhKem.HasFile)
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
            catch(Exception e)
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình cập nhật: {0}".FormatWith(e.Message));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        private void Create(DirectEventArgs e)
        {
            try
            {
                var path = string.Empty;
                if(fufTepTinHangLoat.HasFile)
                {
                    path = UploadFile(fufTepTinHangLoat,
                        hdfTypePage.Text.Equals("KhenThuong") ? Constant.PathReward : Constant.PathDiscipline);
                }
                
                foreach (var itemRow in chkEmployeeRowSelection.SelectedRows)
                {
                    if (hdfTypePage.Text.Equals("KhenThuong"))
                    {
                        string reasonReward;
                        if (hdfIsDanhMuc2.Text == @"0")
                        {
                            var reason = new cat_ReasonReward { Name = cbbLyDoHangLoat.Text };
                            cat_ReasonRewardServices.Create(reason);
                            reasonReward = cbbLyDoHangLoat.Text;
                        }
                        else
                        {
                            reasonReward = cbbLyDoHangLoat.SelectedItem.Text;
                        }

                        if (string.IsNullOrEmpty(reasonReward))
                        {
                            Dialog.Alert("Thông báo", "Không tìm thấy lý do khen thưởng. Vui lòng thử lại!");
                            return;
                        }

                        if (txtSoQuyetDinhHangLoat.Text == "")
                        {
                            GenerateRewardDecisionNumber();
                        }

                        var record = new RewardModel
                        {
                            RecordId = Convert.ToInt32(itemRow.RecordID),
                            DecisionNumber = txtSoQuyetDinhHangLoat.Text.Trim(),
                            DecisionMaker = tgfNguoiQuyetDinhHangLoat.Text.Trim(),
                            FormRewardId = int.Parse(hdfHinhThucHangLoat.Text),
                            LevelRewardId = int.Parse(hdfLevelManyId.Text),
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
                        record.CreatedBy = CurrentUser.User.UserName;
                        record.EditedDate = DateTime.Now;
                        record.EditedBy = CurrentUser.User.UserName;
                        if (!DatetimeHelper.IsNull(dfNgayQuyetDinhHangLoat.SelectedDate))
                        {
                            record.DecisionDate = dfNgayQuyetDinhHangLoat.SelectedDate;
                        }

                        if (!DatetimeHelper.IsNull(dfNgayHieuLuc.SelectedDate))
                        {
                            record.EffectiveDate = dfNgayHieuLuc.SelectedDate;
                        }

                        record.AttachFileName = path != "" ? path : hdfTepDinhKemHangLoat.Text;
                        RewardController.Create(record);
                    }
                    else
                    {
                        string reasonDiscipline;
                        if (hdfIsDanhMuc2.Text == @"0")
                        {
                            var reason = new cat_ReasonDiscipline() { Name = cbbLyDoHangLoat.Text };
                            cat_ReasonDisciplineServices.Create(reason);
                            reasonDiscipline = cbbLyDoHangLoat.Text;
                        }
                        else
                        {
                            reasonDiscipline = cbbLyDoHangLoat.SelectedItem.Text;
                        }

                        if (string.IsNullOrEmpty(reasonDiscipline))
                        {
                            Dialog.Alert("Thông báo", "Không tìm thấy lý do khen thưởng. Vui lòng thử lại!");
                            return;
                        }

                        if (txtSoQuyetDinhHangLoat.Text == "")
                        {
                            GenerateRewardDecisionNumber();
                        }

                        var record = new DisciplineModel
                        {
                            RecordId = Convert.ToInt32(itemRow.RecordID),
                            DecisionNumber = txtSoQuyetDinhHangLoat.Text.Trim(),
                            DecisionMaker = tgfNguoiQuyetDinhHangLoat.Text.Trim(),
                            FormDisciplineId = int.Parse(hdfHinhThucHangLoat.Text),
                            LevelDisciplineId = int.Parse(hdfLevelManyId.Text),
                            Reason = reasonDiscipline,
                            SourceDepartment = txtSourceDepartment.Text,
                            Note = txtGhuChuHangLoat.Text.Trim()
                        };
                        var makerPosition = hdfIsMakerPosition.Text == @"0"
                            ? cbxMakerPosition.Text
                            : cbxMakerPosition.SelectedItem.Text;
                        record.MakerPosition = makerPosition;
                        record.CreatedDate = DateTime.Now;
                        record.CreatedBy = CurrentUser.User.UserName;
                        record.EditedDate = DateTime.Now;
                        record.EditedBy = CurrentUser.User.UserName;

                        if (!DatetimeHelper.IsNull(dfNgayQuyetDinhHangLoat.SelectedDate))
                        {
                            record.DecisionDate = dfNgayQuyetDinhHangLoat.SelectedDate;
                        }

                        if (!DatetimeHelper.IsNull(dfExpireDate.SelectedDate))
                            record.ExpireDate = dfExpireDate.SelectedDate;
                        record.AttachFileName = path != "" ? path : hdfTepDinhKemHangLoat.Text;
                        DisciplineController.Create(record);
                    }
                }
                
                grp_KhenThuongKyLuat.Reload();
                ResetForm();
                wdKhenThuongKyLuatHangLoat.Hide();
            }
            catch(Exception ex)
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình tạo quyết định: {0}".FormatWith(ex.Message));
            }

        }        

        /// <summary>
        /// 
        /// </summary>
        private void GenerateRewardDecisionNumber()
        {
            var departments = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
            var suffix =
                SystemConfigController.GetValueByNameFollowDepartment(SystemConfigParameter.SUFFIX_SOHOPDONG,
                    departments);
            if(string.IsNullOrEmpty(suffix))
                suffix = "QĐ";
            var decisionNumber = GenerateDecisionNumber(suffix);
            txtSoQuyetDinhHangLoat.Text = decisionNumber;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="suffix"></param>
        /// <returns></returns>
        private static string GenerateDecisionNumber(string suffix)
        {
            // get full suffix
            if(string.IsNullOrEmpty(suffix))
                return "";
            suffix = DateTime.Now.Year + "/" + suffix;
            // get max number of constract
            var rewardNumber = RewardController.GetRewardNumberByCondition(suffix);
            if(rewardNumber == null || rewardNumber.Id <= 0) return "001/" + suffix;
            var contractNumber = rewardNumber.DecisionNumber;
            var pos = contractNumber.IndexOf('/');
            if(pos == -1) return "001/" + suffix;
            var stt = contractNumber.Trim().Substring(0, pos);
            var number = int.Parse(stt);
            stt = "0000" + (number + 1);
            stt = stt.Substring(stt.Length - 3);
            stt = stt + "/" + suffix;
            return stt;
            // chưa có số hợp đồng nào theo định dạng
        }

        #endregion  
    }
}