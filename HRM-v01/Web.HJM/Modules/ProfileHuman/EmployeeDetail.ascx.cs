using System;
using System.Web;
using System.Data;
using DataController;
using Ext.Net;
using System.IO;
using SoftCore;
using Web.Core.Framework;
using Web.Core.Object.HumanRecord;
using System.Linq;
using Web.Core.Service.Catalog;
using Web.Core.Object.Catalog;
using Web.Core;
using Web.Core.Framework.Common;
using Web.Core.Object.Security;

namespace Web.HJM.Modules.UserControl
{
    public partial class EmployeeDetail : BaseUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ExtNet.IsAjaxRequest)
            {
                // init department
                storeDepartment.DataSource = CurrentUser.DepartmentsTree;
                storeDepartment.DataBind();
                SetPermission();
            }
        }
        /// <summary>
        /// Phân quyền
        /// </summary>
        private void SetPermission()
        {
            {
                #region Button Sửa
                RowSelectionModelQuaTrinhDaoTao.Listeners.RowSelect.Handler += "#{btnEditRecord}.enable();";
                RowSelectionModelBaoHiem.Listeners.RowSelect.Handler += "#{btnEditRecord}.enable();";
                RowSelectionModelDaiBieu.Listeners.RowSelect.Handler += "#{btnEditRecord}.enable();";
                RowSelectionModelDienBienLuong.Listeners.RowSelect.Handler += "#{btnEditRecord}.disable();";
                RowSelectionModelHopDong.Listeners.RowSelect.Handler += "#{btnEditRecord}.enable();";
                RowSelectionModelKhaNang.Listeners.RowSelect.Handler += "#{btnEditRecord}.enable();";
                RowSelectionModelKhenThuong.Listeners.RowSelect.Handler += "#{btnEditRecord}.enable();";
                RowSelectionModelKyLuat.Listeners.RowSelect.Handler += "#{btnEditRecord}.enable();";
                RowSelectionModelQHGD.Listeners.RowSelect.Handler += "#{btnEditRecord}.enable();";
                RowSelectionModelQuaTrinhDieuChuyen.Listeners.RowSelect.Handler += "#{btnEditRecord}.enable();";
                RowSelectionModelTaiSan.Listeners.RowSelect.Handler += "#{btnEditRecord}.enable();";
                RowSelectionModelTepTinDinhKem.Listeners.RowSelect.Handler += "#{btnEditRecord}.enable();";
                RowSelectionModel_BangCap.Listeners.RowSelect.Handler += "#{btnEditRecord}.enable();";
                RowSelectionModel_NgoaiNgu.Listeners.RowSelect.Handler += "#{btnEditRecord}.enable();";
                RowSelectionModelKinhNghiemLamViec.Listeners.RowSelect.Handler += "#{btnEditRecord}.enable();";
                RowSelectionModel_ChungChi.Listeners.RowSelect.Handler += "#{btnEditRecord}.enable();";
                #endregion
                #region Listener Active
                panelDienBienLuong.Listeners.Activate.Handler += "#{btnEditRecord}.disable();";
                #endregion
                #region DoubleClick
                GridPanelBaoHiem.Listeners.RowDblClick.Handler += "#{DirectMethods}.GetDataForBaoHiem(); #{btnEditBaoHiem}.show();#{btnUpdateCongViec}.hide();#{btnCNVaDongBaoHiem}.hide();";
                GridPanelDaiBieu.Listeners.RowDblClick.Handler += "#{DirectMethods}.GetDataForDaiBieu();#{btnCapNhatDaiBieu}.hide();#{btnEditDaiBieu}.show();#{Button7}.hide();";              
                GridPanelHopDong.Listeners.RowDblClick.Handler += "#{DirectMethods}.GetDataForHopDong();#{hdfButtonClick}.setValue('Edit');#{btnUpdateHopDong}.hide();#{btnEditHopDong}.show();#{Button20}.hide();";
                GridPanelKhaNang.Listeners.RowDblClick.Handler += "#{DirectMethods}.GetDataForKhaNang();#{btnUpdateKhaNang}.hide();#{btnEditKhaNang}.show();#{Button22}.hide();";
                GridPanelKhenThuong.Listeners.RowDblClick.Handler += "#{DirectMethods}.GetDataForKhenThuong();#{hdfButtonClick}.setValue('Edit');#{btnUpdateKhenThuong}.hide();#{btnEditKhenThuong}.show();#{Button24}.hide();";
                GridPanelKyLuat.Listeners.RowDblClick.Handler += "#{DirectMethods}.GetDataForKyLuat();#{hdfButtonClick}.setValue('Edit');#{btnCapNhatKyLuat}.hide();  #{Button26}.hide(); #{btnEditKyLuat}.show();";
                GridPanelQHGD.Listeners.RowDblClick.Handler += "#{DirectMethods}.GetDataForQuanHeGiaDinh();#{btnUpdateQuanHeGiaDinh}.hide();#{btnUpdate}.show();#{Button28}.hide();";             
                GridPanelQuaTrinhDieuChuyen.Listeners.RowDblClick.Handler += "#{DirectMethods}.GetDataForQuaTrinhDieuChuyen();#{btnCapNhatQuaTrinhDieuChuyen}.hide();#{btnUpdateQuaTrinhDieuChuyen}.show();#{Button32}.hide();";
                GridPanelTaiSan.Listeners.RowDblClick.Handler += "#{DirectMethods}.GetDataForTaiSan();#{Button2}.hide();#{btnEditTaiSan}.show();#{btnUpdateTaiSan}.hide();";
                grpTepTinDinhKem.Listeners.RowDblClick.Handler += "#{btnEditAttachFile}.show();#{Button10}.hide();#{btnUpdateAtachFile}.hide();#{DirectMethods}.GetDataForTepTin();#{wdAttachFile}.show();";
                GridPanel_BangCap.Listeners.RowDblClick.Handler += "#{DirectMethods}.GetDataForQuaTrinhHocTap();#{btnUpdateBangCap}.hide(); #{btnUpdateandCloseBangCap}.hide();#{btn_EditBangCap}.show();";
                grpNgoaiNgu.Listeners.RowDblClick.Handler += "#{DirectMethods}.GetDataForNgoaiNgu();#{btnNgoaiNguInsert}.hide(); #{btnNgoaiNguClose}.hide();#{btnNgoaiNguEdit}.show();";
                GridPanelKinhNghiemLamViec.Listeners.RowDblClick.Handler += "#{DirectMethods}.GetDataForKinhNghiemLamViec();#{Update}.hide(); #{UpdateandClose}.hide();#{btnEditKinhNghiem}.show();";
                GridPanel_ChungChi.Listeners.RowDblClick.Handler += "#{DirectMethods}.GetDataForChungChi();#{btnUpdateChungChi}.hide();#{btnUpdateandCloseChungChi}.hide();#{btnEditChungChi}.show();";
                GridPanelQTDT.Listeners.RowDblClick.Handler += "#{DirectMethods}.GetDataForDaoTao();#{btnDTInsert}.hide();#{btnDTUpdateAndClose}.hide();#{btnDTEdit}.show();";
                grpDiNuocNgoai.Listeners.RowDblClick.Handler += "#{DirectMethods}.GetDataForDiNuocNgoai();#{btn_UpdateAndCloseDNN}.hide();#{btn_InsertDNN}.hide();#{btn_updateDNN}.show();";
                
                #endregion
            }
            if (btnDeleteRecord.Visible)
            {
                #region Button Xóa
                RowSelectionModelQuaTrinhDaoTao.Listeners.RowSelect.Handler += "#{btnDeleteRecord}.enable();";
                RowSelectionModelBaoHiem.Listeners.RowSelect.Handler += "#{btnDeleteRecord}.enable();";
                RowSelectionModelDaiBieu.Listeners.RowSelect.Handler += "#{btnDeleteRecord}.enable();";
                RowSelectionModelDienBienLuong.Listeners.RowSelect.Handler += "#{btnDeleteRecord}.disable();";
                RowSelectionModelHopDong.Listeners.RowSelect.Handler += "#{btnDeleteRecord}.enable();";
                RowSelectionModelKhaNang.Listeners.RowSelect.Handler += "#{btnDeleteRecord}.enable();";
                RowSelectionModelKhenThuong.Listeners.RowSelect.Handler += "#{btnDeleteRecord}.enable();";
                RowSelectionModelKyLuat.Listeners.RowSelect.Handler += "#{btnDeleteRecord}.enable();";
                RowSelectionModelQHGD.Listeners.RowSelect.Handler += "#{btnDeleteRecord}.enable();";
                RowSelectionModelQuaTrinhDieuChuyen.Listeners.RowSelect.Handler += "#{btnDeleteRecord}.enable();";
                RowSelectionModelTaiSan.Listeners.RowSelect.Handler += "#{btnDeleteRecord}.enable();";
                RowSelectionModelTepTinDinhKem.Listeners.RowSelect.Handler += "#{btnDeleteRecord}.enable();";
                RowSelectionModel_BangCap.Listeners.RowSelect.Handler += "#{btnDeleteRecord}.enable();";
                RowSelectionModel_NgoaiNgu.Listeners.RowSelect.Handler += "#{btnDeleteRecord}.enable();";
                RowSelectionModelKinhNghiemLamViec.Listeners.RowSelect.Handler += "#{btnDeleteRecord}.enable();";
                RowSelectionModel_ChungChi.Listeners.RowSelect.Handler += "#{btnDeleteRecord}.enable();";

                #endregion
                #region Listener Active
                panelDienBienLuong.Listeners.Activate.Handler += "#{btnDeleteRecord}.disable();";
                panelHopDong.Listeners.Activate.Handler += "#{btnDeleteRecord}.disable();";
                #endregion
            }
            if (btnAddRecordInDetailTable.Visible)
            {
                #region Button Thêm mới
                panelQuaTrinhDaoTao.Listeners.Activate.Handler += "#{btnAddRecordInDetailTable}.enable();";
                panelBaoHiem.Listeners.Activate.Handler += "#{btnAddRecordInDetailTable}.enable();";
                panelDaiBieu.Listeners.Activate.Handler += "#{btnAddRecordInDetailTable}.enable();";
                panelDienBienLuong.Listeners.Activate.Handler += "#{btnAddRecordInDetailTable}.disable();";
                panelHopDong.Listeners.Activate.Handler += "#{btnAddRecordInDetailTable}.enable();";
                panelKhaNang.Listeners.Activate.Handler += "#{btnAddRecordInDetailTable}.enable();";
                panelKhenThuong.Listeners.Activate.Handler += "#{btnAddRecordInDetailTable}.enable();";
                panelKiLuat.Listeners.Activate.Handler += "#{btnAddRecordInDetailTable}.enable();";
                panelQuanHeGiaDinh.Listeners.Activate.Handler += "#{btnAddRecordInDetailTable}.enable();";
                panelTaiSan.Listeners.Activate.Handler += "#{btnAddRecordInDetailTable}.enable();";
                panelTepDinhKem.Listeners.Activate.Handler += "#{btnAddRecordInDetailTable}.enable();";
                panelQuaTrinhHocTap.Listeners.Activate.Handler += "#{btnAddRecordInDetailTable}.enable();";
                panelNgoaiNgu.Listeners.Activate.Handler += "#{btnAddRecordInDetailTable}.enable();";
                panelKinhNghiemLamViec.Listeners.Activate.Handler += "#{btnAddRecordInDetailTable}.enable();";
                panelBangCapChungChi.Listeners.Activate.Handler += "#{btnAddRecordInDetailTable}.enable();";

                #endregion
                #region Listener Active
                panelGeneralInformation.Listeners.Activate.Handler += "#{btnAddRecordInDetailTable}.disable();";
                panelDienBienLuong.Listeners.Activate.Handler += "#{btnAddRecordInDetailTable}.disable();";
                #endregion
            }
        }
        
        public void SetProfileImage(string url)
        {
            hsImage.ImageUrl = url;
        }

        #region Các hàm liên quan đến Store
        protected void StoreNgoaiNgu_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            if (string.IsNullOrEmpty(hdfRecordId.Text)) return;
            StoreNgoaiNgu.DataSource = LanguageController.GetAll(Convert.ToInt32(hdfRecordId.Text));
            StoreNgoaiNgu.DataBind();
        }

        protected void StoregrpATM_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            if (string.IsNullOrEmpty(hdfRecordId.Text)) return;
            StoregrpATM.DataSource = BankController.GetAll(Convert.ToInt32(hdfRecordId.Text));                
            StoregrpATM.DataBind();
        }
        protected void StoregrpDiNuocNgoai_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            if (string.IsNullOrEmpty(hdfRecordId.Text)) return;
            StoregrpDiNuocNgoai.DataSource = GoAboardController.GetAll(Convert.ToInt32(hdfRecordId.Text)); 
            StoregrpDiNuocNgoai.DataBind();
        }

        protected void StoreDienBienLuong_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            if (string.IsNullOrEmpty(hdfRecordId.Text)) return;
            StoreDienBienLuong.DataSource = SalaryDecisionController.GetAll(null, null, Convert.ToInt32(hdfRecordId.Text), null, null, null, null, null, null, null, false, null, null);
            StoreDienBienLuong.DataBind();
        }

        protected void StoreQuaTrinhDaoTao_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            if (string.IsNullOrEmpty(hdfRecordId.Text)) return;
            StoreQuaTrinhDaoTao.DataSource = TrainingHistoryController.GetAll(Convert.ToInt32(hdfRecordId.Text));
            StoreQuaTrinhDaoTao.DataBind();
        }

        protected void StoreBaoHiems_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            if (string.IsNullOrEmpty(hdfRecordId.Text)) return;
            StoreBaoHiem.DataSource = InsuranceController.GetAll(Convert.ToInt32(hdfRecordId.Text));
            StoreBaoHiem.DataBind();
        }

        protected void StoreDaiBieu_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            if (string.IsNullOrEmpty(hdfRecordId.Text)) return;
            StoreDaiBieu.DataSource = DelegateController.GetAll(Convert.ToInt32(hdfRecordId.Text));
            StoreDaiBieu.DataBind();
        }

        protected void StoreHopDong_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            if (string.IsNullOrEmpty(hdfRecordId.Text)) return;
            StoreHopDong.DataSource = ContractController.GetAll(Convert.ToInt32(hdfRecordId.Text));
            StoreHopDong.DataBind();
        }

        protected void StoreKhaNang_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            if (string.IsNullOrEmpty(hdfRecordId.Text)) return;
            StoreKhaNang.DataSource = AbilityController.GetAll(Convert.ToInt32(hdfRecordId.Text));
            StoreKhaNang.DataBind();
        }

        protected void StoreKhenThuong_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            if (string.IsNullOrEmpty(hdfRecordId.Text)) return;
            StoreKhenThuong.DataSource = RewardController.GetAll(Convert.ToInt32(hdfRecordId.Text));
            StoreKhenThuong.DataBind();
        }

        protected void StoreKyLuat_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            if (string.IsNullOrEmpty(hdfRecordId.Text)) return;
            StoreKyLuat.DataSource = DisciplineController.GetAll(Convert.ToInt32(hdfRecordId.Text));
            StoreKyLuat.DataBind();
        }

        protected void StoreQHGD_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            if (string.IsNullOrEmpty(hdfRecordId.Text)) return;
            StoreQHGD.DataSource = FamilyRelationshipController.GetAll(Convert.ToInt32(hdfRecordId.Text));
            StoreQHGD.DataBind();
        }

        protected void StoreQuaTrinhDieuChuyen_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            if (string.IsNullOrEmpty(hdfRecordId.Text)) return;
            StoreQuaTrinhDieuChuyen.DataSource = WorkProcessController.GetAll(Convert.ToInt32(hdfRecordId.Text));
            StoreQuaTrinhDieuChuyen.DataBind();
        }

        protected void StoreTaiSan_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            if (string.IsNullOrEmpty(hdfRecordId.Text)) return;
            StoreTaiSan.DataSource = AssetController.GetAll(Convert.ToInt32(hdfRecordId.Text));
            StoreTaiSan.DataBind();
        }

        protected void grpTepTinDinhKemStore_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            if (string.IsNullOrEmpty(hdfRecordId.Text)) return;
            grpTepTinDinhKemStore.DataSource = AttachFileController.GetAll(Convert.ToInt32(hdfRecordId.Text));
            grpTepTinDinhKemStore.DataBind();
        }

        protected void Store_BangCap_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            Store_BangCap.DataSource = EducationHistoryController.GetAll(Convert.ToInt32(hdfRecordId.Text));
            Store_BangCap.DataBind();
        }

        protected void StoreKinhNghiemLamViec_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(hdfRecordId.Text)) return;
                StoreKinhNghiemLamViec.DataSource = WorkHistoryController.GetAll(Convert.ToInt32(hdfRecordId.Text));
                StoreKinhNghiemLamViec.DataBind();
            }
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }

        protected void Store_BangCapChungChi_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            if (string.IsNullOrEmpty(hdfRecordId.Text)) return;
            Store_BangCapChungChi.DataSource = CertificateController.GetAll(Convert.ToInt32(hdfRecordId.Text));
            Store_BangCapChungChi.DataBind();
        }
        #endregion

        #region Các hàm xử lý sự kiện Click
        protected void btnUpdateDaoTao_Click(object sender, DirectEventArgs e)
        {
            try
            {
                var util = new Util();
                var trainingController = new TrainingHistoryController();
                var trainingHistory = new hr_TrainingHistory();
                if (!string.IsNullOrEmpty(hdfRecordId.Text))
                    trainingHistory.RecordId = Convert.ToInt32(hdfRecordId.Text);
                trainingHistory.TrainingName = txtDTTenKhoaDaoTao.Text;
                if (!util.IsDateNull(dfDTTuNgay.SelectedDate))
                {
                    trainingHistory.StartDate = dfDTTuNgay.SelectedDate;
                }
                if (!util.IsDateNull(dfDTDenNgay.SelectedDate))
                {
                    trainingHistory.EndDate = dfDTDenNgay.SelectedDate;
                }
                if (!string.IsNullOrEmpty(hdfDTQuocGia.Text))
                    trainingHistory.NationId = Convert.ToInt32(hdfDTQuocGia.Text);
                trainingHistory.Reason = txtLyDoDaoTao.Text;
                trainingHistory.TrainingPlace = txtNoiDaoTao.Text;
                trainingHistory.Note = txtDTGhiChu.Text;
                trainingHistory.CreatedDate = DateTime.Now;
                trainingHistory.EditedDate = DateTime.Now;

                if (e.ExtraParams["Command"] == "Update")
                {
                    trainingHistory.Id = int.Parse(RowSelectionModelQuaTrinhDaoTao.SelectedRecordID);
                    trainingController.Update(trainingHistory);
                    wdQuaTrinhDaoTao.Hide();
                }
                else
                {
                    trainingController.Insert(trainingHistory);
                }
                if (e.ExtraParams["Close"] == "True")
                {
                    wdQuaTrinhDaoTao.Hide();
                }
                GridPanelQTDT.Reload();
            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Thông báo từ hệ thống", "Có lỗi xảy ra: " + ex.Message).Show();
            }
        }

        /// <summary>
        /// Bảo hiểm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdateCongViec_Click(object sender, DirectEventArgs e)
        {
            try
            {
                var controller = new InsuranceController();
                var insurance = new hr_Insurance();
                var util = new Util();

                if (!string.IsNullOrEmpty(hdfRecordId.Text))
                    insurance.RecordId = Convert.ToInt32(hdfRecordId.Text);
                insurance.Note = txtBHGhiChu.Text;
                insurance.SalaryFactor = decimal.Parse("0" + txtBHHSLuong.Text);
                if(!string.IsNullOrEmpty(hdfInsurancePositionId.Text))
                    insurance.PositionId = Convert.ToInt32(hdfInsurancePositionId.Text);
                insurance.SalaryLevel = decimal.Parse("0" + txtBHMucLuong.Text);
                insurance.Allowance = decimal.Parse("0" + txtBHPhuCap.Text);
                insurance.Rate = txtBHTyle.Text;
                insurance.CreatedDate = DateTime.Now;

                if (!util.IsDateNull(dfBHDenNgay.SelectedDate))
                    insurance.ToDate = dfBHDenNgay.SelectedDate;
                if (!util.IsDateNull(dfBHTuNgay.SelectedDate))
                    insurance.FromDate = dfBHTuNgay.SelectedDate;
                if (e.ExtraParams["Command"] == "Update")
                {
                    insurance.EditedDate = DateTime.Now;
                    insurance.Id = Convert.ToInt32(RowSelectionModelBaoHiem.SelectedRecordID);
                    controller.Update(insurance);
                    wdBaoHiem.Hide();
                }
                else
                {
                    controller.Insert(insurance);
                }
                if (e.ExtraParams["Close"] == "True")
                {
                    wdBaoHiem.Hide();
                }
                GridPanelBaoHiem.Reload();
            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Thông báo từ hệ thống", "Có lỗi xảy ra: " + ex.Message).Show();
            }
        }

        protected void btnCapNhatDaiBieu_Click(object sender, DirectEventArgs e)
        {
            try
            {
                var controller = new DelegateController();
                var dele = new hr_Delegate();
                var util = new Util();
                if (!string.IsNullOrEmpty(hdfRecordId.Text))
                    dele.RecordId = Convert.ToInt32(hdfRecordId.Text);
                dele.Note = txtDBGhiChu.Text;
                dele.Term = txtDBNhiemKy.Text;
                if (!util.IsDateNull(dfDBTuNgay.SelectedDate))
                    dele.FromDate = dfDBTuNgay.SelectedDate;
                if (!util.IsDateNull(dfDBDenNgay.SelectedDate))
                    dele.ToDate = dfDBDenNgay.SelectedDate;
                dele.Type = txtDBLoaiHinh.Text;
                dele.CreatedDate = DateTime.Now;
                if (e.ExtraParams["Command"] == "Update")
                {
                    dele.Id = Convert.ToInt32(RowSelectionModelDaiBieu.SelectedRecordID);
                    controller.Update(dele);
                    wdDaiBieu.Hide();
                }
                else
                {
                    controller.Insert(dele);
                }
                if (e.ExtraParams["Close"] == "True")
                {
                    wdDaiBieu.Hide();
                }
                GridPanelDaiBieu.Reload();
            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Thông báo từ hệ thống", "Có lỗi xảy ra: " + ex.Message).Show();
            }
        }

        /// <summary>
        /// Cập nhật đi nước ngoài
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdateDiNuocNgoai_Click(object sender, DirectEventArgs e)
        {
            try
            {
                var controller = new GoAboardController();
                var aboard = new hr_GoAboard();
                var util = new Util();
                if (!string.IsNullOrEmpty(hdfRecordId.Text))
                    aboard.RecordId = Convert.ToInt32(hdfRecordId.Text);
                if (!string.IsNullOrEmpty(hdfGoBoardNationId.Text))
                    aboard.NationId = Convert.ToInt32(hdfGoBoardNationId.Text);
                aboard.Note = txtGoAboardNote.Text;
                aboard.Reason = txtGoAboardReason.Text;
                aboard.CreatedDate = DateTime.Now;
                aboard.CreatedBy = CurrentUser.User.UserName;
                if (!util.IsDateNull(dfNgayBatDau.SelectedDate))
                    aboard.StartDate = dfNgayBatDau.SelectedDate;
                if (!util.IsDateNull(dfNgayKetThuc.SelectedDate))
                    aboard.EndDate = dfNgayKetThuc.SelectedDate;
                if (e.ExtraParams["Command"] == "Update")
                {
                    aboard.Id = Convert.ToInt32(RowSelectionModelDiNuocNgoai.SelectedRecordID);
                    controller.Update(aboard);
                    wdDiNuocNgoai.Hide();
                    Dialog.ShowNotification("Câp nhật thành công");
                }
                else
                {
                    controller.Insert(aboard);
                }
                if (e.ExtraParams["Close"] == "True")
                {
                    wdDiNuocNgoai.Hide();
                }
                grpDiNuocNgoai.Reload();
            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Thông báo từ hệ thống", "Có lỗi xảy ra: " + ex.Message).Show();
            }
        }

        #region các hàm dùng chung
        /// <summary>
        /// upload file from computer to server
        /// </summary>
        /// <param name="sender">ID of FileUploadField</param>
        /// <param name="directory">directory of folder HoSoNhanSu</param>
        /// <param name="relativePath">the relative path to place you want to save file</param>
        /// <returns>The path of file after upload to server</returns>
        public string UploadFile(object sender, string relativePath)
        {
            FileUploadField obj = (FileUploadField)sender;
            HttpPostedFile file = obj.PostedFile;
            DirectoryInfo dir = new DirectoryInfo(Server.MapPath(relativePath));  // save file to directory HoSoNhanSu/File
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

        /// <summary>
        /// Delete file attach
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="id"></param>
        /// <param name="sender"></param>
        public void DeleteTepTinDinhKem(string tableName, int id, Hidden sender)
        {
            try
            {
                // xóa trong csdl
                var sql = string.Empty;
                sql += " UPDATE {0} ".FormatWith(tableName) +
                    " SET [AttachFileName] = '' " +
                    " WHERE Id = '{0}' ".FormatWith(id);
                SQLHelper.ExecuteNonQuery(sql);
                // xóa file trong thư mục
                string serverPath = Server.MapPath(sender.Text);
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

        /// <summary>
        /// Return attach file to client when user click download button
        /// </summary>
        /// <param name="TableName">Name of table</param>
        /// <param name="Prkey">Prkey of employee</param>
        public void DownloadAttachFile(string TableName, Hidden sender)
        {
            try
            {
                if (string.IsNullOrEmpty(sender.Text))
                {
                    Dialog.ShowNotification("Không có tệp tin đính kèm");
                    return;
                }
                string serverPath = Server.MapPath(sender.Text);
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
                ExtNet.Msg.Alert("Thông báo từ hệ thống", ex.Message).Show();
            }
        }

        [DirectMethod]
        public void DownloadAttach(string path)
        {
            string serverPath = Server.MapPath("") + "/" + path;
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
        #endregion

        /// <summary>
        /// hợp đồng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdateHopDong_Click(object sender, DirectEventArgs e)
        {
            try
            {
                var controller = new ContractController();
                var contract = new hr_Contract();
                var util = new Util();

                // upload file
                string path = string.Empty;
                if (fufHopDongTepTin.HasFile)
                {
                    string directory = Server.MapPath("");
                    path = UploadFile(fufHopDongTepTin, Constant.PathContract);
                }
                if (!string.IsNullOrEmpty(hdfRecordId.Text))
                    contract.RecordId = Convert.ToInt32(hdfRecordId.Text);
                if (!string.IsNullOrEmpty(hdfJobTitleOldId.Text))
                    contract.JobId = Convert.ToInt32(hdfJobTitleOldId.Text);
                if (!string.IsNullOrEmpty(hdfContractTypeId.Text))
                    contract.ContractTypeId = Convert.ToInt32(hdfContractTypeId.Text);
                if (!string.IsNullOrEmpty(hdfContractStatusId.Text))
                    contract.ContractStatusId = Convert.ToInt32(hdfContractStatusId.Text);
                if (!util.IsDateNull(dfHopDongNgayHopDong.SelectedDate))
                    contract.ContractDate = dfHopDongNgayHopDong.SelectedDate;
                // sinh số hợp đồng
                string departments = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
                string suffix = SystemConfigController.GetValueByNameFollowDepartment(SystemConfigParameter.SUFFIX_SOHOPDONG, departments);
                if (string.IsNullOrEmpty(suffix))
                    suffix = "QĐ";
                contract.ContractNumber = GenerateContractNumber(suffix);
                if (!string.IsNullOrEmpty(txtHopDongSoHopDong.Text))
                    contract.ContractNumber = txtHopDongSoHopDong.Text;
                if (!util.IsDateNull( dfHopDongNgayKiKet.SelectedDate))
                    contract.ContractEndDate = dfHopDongNgayKiKet.SelectedDate;
                if (!util.IsDateNull( dfNgayCoHieuLuc.SelectedDate))
                    contract.EffectiveDate = dfNgayCoHieuLuc.SelectedDate;
                contract.PersonRepresent = txt_NguoiKyHD.Text;
                if (!string.IsNullOrEmpty(hdfOldPositionId.Text))
                    contract.PersonPositionId = Convert.ToInt32(hdfOldPositionId.Text);
                if (path != "")
                    contract.AttachFileName = path;
                else
                    contract.AttachFileName = hdfHopDongTepTinDK.Text;
                contract.Note = txtHopDongGhiChu.Text;
                contract.CreatedBy = CurrentUser.User.UserName;
                contract.ContractCondition = cbx_HopDongTrangThai.SelectedItem.Value;
                contract.CreatedDate = DateTime.Now;
                if (e.ExtraParams["Command"] == "Update")
                {
                    contract.EditedDate = DateTime.Now;
                    contract.Id = Convert.ToInt32(RowSelectionModelHopDong.SelectedRecordID);
                    controller.Update(contract);
                    wdHopDong.Hide();
                }
                else
                {
                    // kiểm tra còn hợp đồng nào chưa hết hạn không
                    var checkContract = controller.CheckContractBeforeInsert(Convert.ToInt32(hdfRecordId.Text), contract.EffectiveDate);
                    if(checkContract != null)
                    {
                        ExtNet.Msg.Alert("Thông báo", "Hợp đồng hiện tại của cán bộ vẫn còn hiệu lực. Bạn không được phép thay đổi loại hợp đồng.").Show();
                        return;
                    }
                    controller.Insert(contract);
                    if (e.ExtraParams["Close"] == "True")
                    {
                        wdHopDong.Hide();
                    }
                }
                GridPanelHopDong.Reload();
            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Thông báo từ hệ thống", "Có lỗi xảy ra: " + ex.Message).Show();
            }
        }

        private string GenerateContractNumber(string suffix)
        {
            // get full suffix
            if (string.IsNullOrEmpty(suffix))
                return "";
            suffix = DateTime.Now.Year + "/" + suffix;
            // get max number of contract
            var contNum = ContractController.GetContractNumberByCondition(suffix);
            if (contNum != null && contNum.Id > 0)   
            {
                string contractNumber = contNum.ContractNumber;
                int pos = contractNumber.IndexOf('/');
                if (pos != -1)
                {
                    string stt = contractNumber.Trim().Substring(0, pos);
                    int number = int.Parse(stt);
                    stt = "0000" + (number + 1);
                    stt = stt.Substring(stt.Length - 3);
                    stt = stt + "/" + suffix;
                    return stt;
                }
            }
            // chưa có số hợp đồng nào theo định dạng
            return "001/" + suffix;
        }

        protected void btnHopDongAttachDownload_Click(object sender, DirectEventArgs e)
        {
            string directory = Server.MapPath("");
            DownloadAttachFile("hr_Contract", hdfHopDongTepTinDK);
        }
        protected void btnHopDongAttachDelete_Click(object sender, DirectEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(RowSelectionModelHopDong.SelectedRecordID))
                {
                    DeleteTepTinDinhKem("hr_Contract", Convert.ToInt32(RowSelectionModelHopDong.SelectedRecordID), hdfHopDongTepTinDK);
                    hdfHopDongTepTinDK.Text = "";
                    GridPanelHopDong.Reload();
                }
                else
                {
                    Dialog.ShowNotification("Tệp tin không tồn tại hoặc đã bị xóa!");
                }
            }
            catch (Exception ex)
            {
                Dialog.Alert("Có lỗi xảy ra: " + ex.Message);
            }
        }

        [DirectMethod]
        public void GenerateSoQD()
        {
            string departments = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
            string suffix = SystemConfigController.GetValueByNameFollowDepartment(SystemConfigParameter.SUFFIX_SOHOPDONG, departments);
            if (string.IsNullOrEmpty(suffix))
                suffix = "QĐ";
            string contractNumber = GenerateContractNumber(suffix);
            txtHopDongSoHopDong.Text = contractNumber;
        }

        protected void btnUpdateKhaNang_Click(object sender, DirectEventArgs e)
        {
            try
            {
                var ability = new hr_Ability();
                var controller = new AbilityController();
                if (!string.IsNullOrEmpty(hdfRecordId.Text))
                    ability.RecordId = Convert.ToInt32(hdfRecordId.Text);
                ability.Note = txtKhaNangGhiChu.Text;
                if (!string.IsNullOrEmpty(hdfAbilityId.Text))
                    ability.AbilityId = Convert.ToInt32(hdfAbilityId.Text);
                if (!string.IsNullOrEmpty(hdfAbilityGraduationTypeId.Text))
                    ability.GraduationTypeId = Convert.ToInt32(hdfAbilityGraduationTypeId.Text);
                if (e.ExtraParams["Command"] == "Update")
                {
                    ability.Id = Convert.ToInt32(RowSelectionModelKhaNang.SelectedRecordID);
                    wdKhaNang.Hide();
                    controller.Update(ability);
                }
                else
                {
                    controller.Insert(ability);
                    if (e.ExtraParams["Close"] == "True")
                    {
                        wdKhaNang.Hide();
                    }
                }
                GridPanelKhaNang.Reload();
            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Thông báo từ hệ thống", "Có lỗi xảy ra: " + ex.Message).Show();
            }
        }

        protected void btnUpdateKhenThuong_Click(object sender, DirectEventArgs e)
        {
            try
            {
                // insert ly do khen thuong (neu co)
                var reasonReward = string.Empty;
                if (hdfIsDanhMuc.Text == "0")
                {
                    var reason = new cat_ReasonReward { Name = cbLyDoKhenThuong.Text };
                    cat_ReasonRewardServices.Create(reason);
                    reasonReward = cbLyDoKhenThuong.Text;
                }
                else
                {
                    reasonReward = cbLyDoKhenThuong.SelectedItem.Text;
                }
                if (string.IsNullOrEmpty(reasonReward))
                {
                    ExtNet.Msg.Alert("Thông báo", "Không tìm thấy lý do khen thưởng. Vui lòng thử lại!").Show();
                    return;
                }

                var reward = new hr_Reward();
                var controller = new RewardController();
                var util = new Util();

                // upload file
                string path = string.Empty;
                if (fufKhenThuongTepTinDinhKem.HasFile)
                {
                    string directory = Server.MapPath("");
                    path = UploadFile(fufKhenThuongTepTinDinhKem, Constant.PathReward);
                }
                if (!string.IsNullOrEmpty(hdfRecordId.Text))
                    reward.RecordId = Convert.ToInt32(hdfRecordId.Text);
                reward.Note = txtKhenThuongGhiCHu.Text;
                reward.DecisionMaker = txtRewardDecisionMaker.Text;
                reward.Reason = reasonReward;
                if (!string.IsNullOrEmpty(hdfRewardFormId.Text))
                    reward.FormRewardId = Convert.ToInt32(hdfRewardFormId.Text);
                if (!string.IsNullOrEmpty(hdfLevelRewardId.Text))
                    reward.LevelRewardId = Convert.ToInt32(hdfLevelRewardId.Text);
                // generate decisionNumber
                string departments = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
                string suffix = SystemConfigController.GetValueByNameFollowDepartment(SystemConfigParameter.SUFFIX_SOQDKHENTHUONG, departments);
                if (string.IsNullOrEmpty(suffix))
                    suffix = "QĐ";
                reward.DecisionNumber = GenerateRewardNumber(suffix);
                if(!string.IsNullOrEmpty(txtKhenThuongSoQuyetDinh.Text))
                    reward.DecisionNumber =  txtKhenThuongSoQuyetDinh.Text;
                reward.CreatedDate = DateTime.Now;
                if (!util.IsDateNull(dfKhenThuongNgayQuyetDinh.SelectedDate))
                    reward.DecisionDate = dfKhenThuongNgayQuyetDinh.SelectedDate;
                if (path != "")
                    reward.AttachFileName = path;
                else
                    reward.AttachFileName = hdfKhenThuongTepTinDinhKem.Text;
                if (e.ExtraParams["Command"] == "Update")
                {
                    reward.Id = Convert.ToInt32(RowSelectionModelKhenThuong.SelectedRecordID);
                    controller.Update(reward);
                    wdKhenThuong.Hide();
                }
                else
                {
                    controller.Insert(reward);
                    if (e.ExtraParams["Close"] == "True")
                    {
                        wdKhenThuong.Hide();
                    }
                }
                
                GridPanelKhenThuong.Reload();
            }
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }
        protected void btnKhenThuongDownload_Click(object sender, DirectEventArgs e)
        {
            string directory = Server.MapPath("");
            DownloadAttachFile("hr_Reward", hdfKhenThuongTepTinDinhKem);
        }
        protected void btnKhenThuongDelete_Click(object sender, DirectEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(RowSelectionModelKhenThuong.SelectedRecordID))
                {
                    DeleteTepTinDinhKem("hr_Reward", int.Parse(RowSelectionModelKhenThuong.SelectedRecordID), hdfKhenThuongTepTinDinhKem);
                    hdfKhenThuongTepTinDinhKem.Text = "";
                    GridPanelKhenThuong.Reload();
                }
                else
                {
                    Dialog.ShowNotification("Tệp tin không tồn tại hoặc đã bị xóa!");
                }
            }
            catch (Exception ex)
            {
                Dialog.Alert("Có lỗi xảy ra: " + ex.Message);
            }
        }

        [DirectMethod]
        public void GenerateKhenThuongSoQD()
        {
            string departments = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
            string suffix = SystemConfigController.GetValueByNameFollowDepartment(SystemConfigParameter.SUFFIX_SOQDKHENTHUONG, departments);
            if (string.IsNullOrEmpty(suffix))
                suffix = "QĐ";
            string rewardNumber = GenerateRewardNumber(suffix);
            txtKhenThuongSoQuyetDinh.Text = rewardNumber;
        }

        private string GenerateRewardNumber(string suffix)
        {
            // get full suffix
            if (string.IsNullOrEmpty(suffix))
                return "";
            suffix = DateTime.Now.Year + "/" + suffix;
            // get max number of reward
            var rewardNum = RewardController.GetRewardNumberByCondition(suffix);
            if (rewardNum != null && rewardNum.Id > 0)   // có số khen thuong lớn nhất
            {
                string rewardNumber = rewardNum.DecisionNumber;
                int pos = rewardNumber.IndexOf('/');
                if (pos != -1)
                {
                    string stt = rewardNumber.Trim().Substring(0, pos);
                    int number = int.Parse(stt);
                    stt = "0000" + (number + 1);
                    stt = stt.Substring(stt.Length - 3);
                    stt = stt + "/" + suffix;
                    return stt;
                }
            }
            // chưa có số khen thuong nào theo định dạng
            return "001/" + suffix;
        }

        /// <summary>
        /// Cập nhật kỷ luật
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCapNhatKyLuat_Click(object sender, DirectEventArgs e)
        {
            try
            {
                // insert ly do kỷ luật
                var reasonDiscipline = string.Empty;
                if (hdfIsDanhMucKL.Text == "0")
                {
                    var reason = new cat_ReasonDiscipline() { Name = cbLyDoKyLuat.Text };
                    cat_ReasonDisciplineServices.Create(reason);
                    reasonDiscipline = cbLyDoKyLuat.Text;
                }
                else
                {
                    reasonDiscipline = cbLyDoKyLuat.SelectedItem.Text;
                }
                if (string.IsNullOrEmpty(reasonDiscipline))
                {
                    ExtNet.Msg.Alert("Thông báo", "Không tìm thấy lý do kỷ luật. Vui lòng thử lại!").Show();
                    return;
                }

                var controller = new DisciplineController();
                var discipline = new hr_Discipline();
                var util = new Util();

                // upload file
                string path = string.Empty;
                if (fufKyLuatTepTinDinhKem.HasFile)
                {
                    string directory = Server.MapPath("");
                    path = UploadFile(fufKyLuatTepTinDinhKem, "File/KyLuat");
                }
                if (!string.IsNullOrEmpty(hdfRecordId.Text))
                    discipline.RecordId = Convert.ToInt32(hdfRecordId.Text);
                discipline.Reason = reasonDiscipline;
                discipline.Note = txtKyLuatGhiChu.Text;
                if(!string.IsNullOrEmpty(hdfDisciplineFormId.Text))
                    discipline.FormDisciplineId = Convert.ToInt32(hdfDisciplineFormId.Text);
                if (!string.IsNullOrEmpty(hdfLevelDisciplineId.Text))
                    discipline.LevelDisciplineId = Convert.ToInt32(hdfLevelDisciplineId.Text);
                string departments = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
                string suffix = SystemConfigController.GetValueByNameFollowDepartment(SystemConfigParameter.SUFFIX_SOQDKYLUAT, departments);
                if (string.IsNullOrEmpty(suffix))
                    suffix = "QĐ";
                discipline.DecisionNumber = GenerateDisciplineNumber(suffix);
                if(!string.IsNullOrEmpty(txtKyLuatSoQD.Text))
                    discipline.DecisionNumber = txtKyLuatSoQD.Text;
                if (!util.IsDateNull(dfKyLuatNgayQuyetDinh.SelectedDate))
                    discipline.DecisionDate = dfKyLuatNgayQuyetDinh.SelectedDate;
                discipline.DecisionMaker = tgfKyLuatNguoiQD.Text;
                discipline.CreatedDate = DateTime.Now;
                if (path != "")
                    discipline.AttachFileName = path;
                else
                    discipline.AttachFileName = hdfKyLuatTepTinDinhKem.Text;
                if (e.ExtraParams["Command"] == "Update")
                {
                    discipline.Id = Convert.ToInt32(RowSelectionModelKyLuat.SelectedRecordID);
                    controller.Update(discipline);
                    wdKyLuat.Hide();
                }
                else
                {
                    controller.Insert(discipline);
                    if (e.ExtraParams["Close"] == "True")
                    {
                        wdKyLuat.Hide();
                    }
                }
                GridPanelKyLuat.Reload();
            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Thông báo từ hệ thống", "Có lỗi xảy ra: " + ex.Message).Show();
            }
        }

        private string GenerateDisciplineNumber(string suffix)
        {
            // get full suffix
            if (string.IsNullOrEmpty(suffix))
                return "";
            suffix = DateTime.Now.Year + "/" + suffix;
            // get max number of reward
            var disciplineNum = DisciplineController.GetDisciplineNumberByCondition(suffix);
            if (disciplineNum != null && disciplineNum.Id > 0)   // có số kỷ luật lớn nhất
            {
                string disciplineNumber = disciplineNum.DecisionNumber;
                int pos = disciplineNumber.IndexOf('/');
                if (pos != -1)
                {
                    string stt = disciplineNumber.Trim().Substring(0, pos);
                    int number = int.Parse(stt);
                    stt = "0000" + (number + 1);
                    stt = stt.Substring(stt.Length - 3);
                    stt = stt + "/" + suffix;
                    return stt;
                }
            }
            // chưa có số kỷ luật nào theo định dạng
            return "001/" + suffix;
        }

        protected void btnKyLuatDownload_Click(object sender, DirectEventArgs e)
        {
            string directory = Server.MapPath("");
            DownloadAttachFile("hr_Discipline", hdfKyLuatTepTinDinhKem);
        }
        protected void btnKyLuatDelete_Click(object sender, DirectEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(RowSelectionModelKyLuat.SelectedRecordID))
                {
                    DeleteTepTinDinhKem("hr_Discipline", Convert.ToInt32(RowSelectionModelKyLuat.SelectedRecordID), hdfKyLuatTepTinDinhKem);
                    hdfKyLuatTepTinDinhKem.Text = "";
                    GridPanelKyLuat.Reload();
                }
                else
                {
                    Dialog.ShowNotification("Tệp tin không tồn tại hoặc đã bị xóa!");
                }
            }
            catch (Exception ex)
            {
                Dialog.Alert("Có lỗi xảy ra: " + ex.Message);
            }
        }

        [DirectMethod]
        public void GenerateKyLuatSoQD()
        {
            string departments = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
            string suffix = SystemConfigController.GetValueByNameFollowDepartment(SystemConfigParameter.SUFFIX_SOQDKYLUAT, departments);
            if (string.IsNullOrEmpty(suffix))
                suffix = "QĐ";
            string disciplineNumber = GenerateDisciplineNumber(suffix);
            txtKyLuatSoQD.Text = disciplineNumber;
        }
        ///////////////////////////

        protected void btnUpdateQuanHeGiaDinh_Click(object sender, DirectEventArgs e)
        {
            try
            {
                var util = new Util();
                var relationship = new hr_FamilyRelationship();
                var controller = new FamilyRelationshipController();

                if (!string.IsNullOrEmpty(hdfRecordId.Text))
                    relationship.RecordId = Convert.ToInt32(hdfRecordId.Text);
                relationship.Note = txtQHGDGhiChu.Text;
                relationship.FullName = txtQHGDHoTen.Text;
                if (!string.IsNullOrEmpty(hdfRelationshipId.Text))
                    relationship.RelationshipId = Convert.ToInt32(hdfRelationshipId.Text);
                relationship.WorkPlace = txtQHGDNoiLamViec.Text;
                relationship.Occupation = txtQHGDNgheNghiep.Text;
                relationship.IsDependent = chkQHGDLaNguoiPhuThuoc.Checked;
                relationship.IDNumber = txtSoCMT.Text;
                relationship.CreatedDate = DateTime.Now;
                if(!string.IsNullOrEmpty(txtQHGDNamSinh.Text))
                relationship.BirthYear = Convert.ToInt32(txtQHGDNamSinh.Text);
                if("1" == hdfRelationshipSex.Text)
                    relationship.Sex = true;
                else
                    relationship.Sex = false;
                if (chkQHGDLaNguoiPhuThuoc.Checked == true)
                {
                    if (!util.IsDateNull(dfQHGDBatDauGiamTru.SelectedDate))
                        relationship.ReduceStartDate = dfQHGDBatDauGiamTru.SelectedDate;
                    if (!util.IsDateNull(dfQHGDKetThucGiamTru.SelectedDate))
                        relationship.ReduceEndDate = dfQHGDKetThucGiamTru.SelectedDate;
                }
                if (e.ExtraParams["Command"] == "Update")
                {
                    relationship.Id = Convert.ToInt32(RowSelectionModelQHGD.SelectedRecordID);
                    controller.Update(relationship);
                    wdQuanHeGiaDinh.Hide();
                }
                else
                {
                    controller.Insert(relationship);
                    if (e.ExtraParams["Close"] == "True")
                    {
                        wdQuanHeGiaDinh.Hide();
                    }
                }
                GridPanelQHGD.Reload();
            }
            catch (Exception ex)
            {
                X.Msg.Alert("Thông báo từ hệ thống", "Có lỗi xảy ra: " + ex.Message).Show();
            }
        }

        /// <summary>
        /// Qúa trình công tác tại đơn vị
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCapNhatQuaTrinhDieuChuyen_Click(object sender, DirectEventArgs e)
        {
            try
            {
                var controller = new WorkProcessController();
                var work = new hr_WorkProcess();
                var util = new Util();

                // upload file
                string path = string.Empty;
                if (fufQTDCTepTinDinhKem.HasFile)
                {
                    string directory = Server.MapPath("");
                    path = UploadFile(fufQTDCTepTinDinhKem, Constant.PathTransfer);
                }
                if (!string.IsNullOrEmpty(hdfRecordId.Text))
                    work.RecordId = Convert.ToInt32(hdfRecordId.Text);
                string departments = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
                string suffix = SystemConfigController.GetValueByNameFollowDepartment(SystemConfigParameter.SUFFIX_SOQDDIEUCHUYEN, departments);
                if (string.IsNullOrEmpty(suffix))
                    suffix = "QĐ";
                work.DecisionNumber = GenerateWorkProcessNumber(suffix);
                if(!string.IsNullOrEmpty(txtQTDCSoQuyetDinh.Text))
                    work.DecisionNumber = txtQTDCSoQuyetDinh.Text;
                if (!string.IsNullOrEmpty(hdfNewDepartmentId.Text))
                    work.NewDepartmentId = Convert.ToInt32(hdfNewDepartmentId.Text);
                if (!string.IsNullOrEmpty(hdfNewPositionId.Text))
                    work.NewPositionId = Convert.ToInt32(hdfNewPositionId.Text);
                if (!string.IsNullOrEmpty(hdfJobTitleNewId.Text))
                    work.NewJobId = Convert.ToInt32(hdfJobTitleNewId.Text);
                work.Note = txtDieuChuyenGhiChu.Text;
                work.DecisionMaker = tgfQTDCNguoiQuyetDinh.Text;
                if (!util.IsDateNull(dfQTDCNgayQuyetDinh.SelectedDate))
                    work.DecisionDate = dfQTDCNgayQuyetDinh.SelectedDate;
                if (!util.IsDateNull(dfQTDCNgayCoHieuLuc.SelectedDate))
                    work.EffectiveDate = dfQTDCNgayCoHieuLuc.SelectedDate;
                work.CreatedDate = DateTime.Now;
                if (path != "")
                    work.AttachFileName = path;
                else
                    work.AttachFileName = hdfQTDCTepTinDinhKem.Text;
                if (e.ExtraParams["Command"] == "Update")
                {
                    work.Id = Convert.ToInt32(RowSelectionModelQuaTrinhDieuChuyen.SelectedRecordID);
                    controller.Update(work);
                    wdQuaTrinhDieuChuyen.Hide();
                }
                else
                {
                    var record = RecordController.GetById(work.RecordId);
                    work.OldPositionId = record.PositionId;
                    work.OldJobId = record.JobTitleId;
                    work.OldDepartmentId = record.DepartmentId;

                    controller.Insert(work);
                    if (e.ExtraParams["Close"] == "True")
                    {
                        wdQuaTrinhDieuChuyen.Hide();
                    }
                }
                hdfNewDepartmentId.SetValue(null);
                hdfNewPositionId.SetValue(null);
                hdfJobTitleNewId.SetValue(null);
                cbxQTDCBoPhanMoi.SetValue(null);
                dfQTDCNgayQuyetDinh.SetValue(null);
                dfQTDCNgayCoHieuLuc.SetValue(null);
                GridPanelQuaTrinhDieuChuyen.Reload();
            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Thông báo từ hệ thống", "Có lỗi xảy ra: " + ex.Message).Show();
            }
        }

        private string GenerateWorkProcessNumber(string suffix)
        {
            // get full suffix
            if (string.IsNullOrEmpty(suffix))
                return "";
            suffix = DateTime.Now.Year + "/" + suffix;
            // get max number of reward
            var workNum = WorkProcessController.GetWorkProcessNumberByCondition(suffix);
            if (workNum != null && workNum.Id > 0)   // có số công tác tại đơn vị lớn nhất
            {
                string workNumber = workNum.DecisionNumber;
                int pos = workNumber.IndexOf('/');
                if (pos != -1)
                {
                    string stt = workNumber.Trim().Substring(0, pos);
                    int number = int.Parse(stt);
                    stt = "0000" + (number + 1);
                    stt = stt.Substring(stt.Length - 3);
                    stt = stt + "/" + suffix;
                    return stt;
                }
            }
            // chưa có số công tác tại đơn vị nào theo định dạng
            return "001/" + suffix;
        }

        protected void btnQTDCDownload_Click(object sender, DirectEventArgs e)
        {
            string directory = Server.MapPath("");
            DownloadAttachFile("hr_WorkProcess", hdfQTDCTepTinDinhKem);
        }
        protected void btnQTDCDelete_Click(object sender, DirectEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(RowSelectionModelQuaTrinhDieuChuyen.SelectedRecordID))
                {
                    DeleteTepTinDinhKem("hr_WorkProcess", Convert.ToInt32(RowSelectionModelQuaTrinhDieuChuyen.SelectedRecordID), hdfQTDCTepTinDinhKem);
                    hdfQTDCTepTinDinhKem.Text = "";
                    GridPanelQuaTrinhDieuChuyen.Reload();
                }
                else
                {
                    Dialog.ShowNotification("Tệp tin không tồn tại hoặc đã bị xóa!");
                }
            }
            catch (Exception ex)
            {
                Dialog.Alert("Có lỗi xảy ra: " + ex.Message);
            }
        }

        [DirectMethod]
        public void GenerateQTDCSoQD()
        {
            string departments = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
            string suffix = SystemConfigController.GetValueByNameFollowDepartment(SystemConfigParameter.SUFFIX_SOQDDIEUCHUYEN, departments);
            if (string.IsNullOrEmpty(suffix))
                suffix = "QĐ";
            string workProcessNumber = GenerateWorkProcessNumber(suffix);
            txtQTDCSoQuyetDinh.Text = workProcessNumber;
        }

        /// <summary>
        /// Tài sản
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdateTaiSan_Click(object sender, DirectEventArgs e)
        {
            try
            {
                var controller = new AssetController();
                var assset = new hr_Asset();

                if (!string.IsNullOrEmpty(hdfRecordId.Text))
                    assset.RecordId = Convert.ToInt32(hdfRecordId.Text);
                assset.Note = tsGhiChu.Text;
                assset.AssetName = txtAssetName.Text;
                assset.Quantity = int.Parse("0" + txtTaiSanSoLuong.Text);
                assset.Status = tsTxtinhTrang.Text;
                assset.AssetCode = txtTheTaiSan.Text;
                assset.UnitCode = txtUnitCode.Text;
                assset.CreatedDate = DateTime.Now;
                assset.CreatedBy = CurrentUser.User.UserName;
                if (!Util.GetInstance().IsDateNull(tsDateField.SelectedDate))
                    assset.ReceiveDate = tsDateField.SelectedDate;
                if (e.ExtraParams["Command"] == "Update")
                {
                    assset.Id = Convert.ToInt32(RowSelectionModelTaiSan.SelectedRecordID);
                    controller.Update(assset);
                    wdAddTaiSan.Hide();
                }
                else
                {
                    controller.Insert(assset);
                    if (e.ExtraParams["Close"] == "True")
                    {
                        wdAddTaiSan.Hide();
                    }
                }
                GridPanelTaiSan.Reload();
            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Thông báo từ hệ thống", "Có lỗi xảy ra: " + ex.Message).Show();
            }
        }

        protected void btnUpdateAtachFile_Click(object sender, DirectEventArgs e)
        {
            try
            {
                var controller = new AttachFileController();
                var attach = new hr_AttachFile();

                // upload file
                string path = string.Empty;
                if (file_cv.HasFile)
                {
                    path = UploadFile(file_cv, Constant.PathAttachFile);
                }
                attach.AttachFileName = txtFileName.Text;
                attach.Note = txtGhiChu.Text;
                if (!string.IsNullOrEmpty(hdfRecordId.Text))
                    attach.RecordId = Convert.ToInt32(hdfRecordId.Text);
                attach.CreatedDate = DateTime.Now;
                attach.IsApproved = false;
                attach.CreatedBy = CurrentUser.User.UserName;

                if (path != "")
                {
                    attach.URL = path;
                    HttpPostedFile file = file_cv.PostedFile;
                    attach.SizeKB = file.ContentLength / 1024;
                }
                else
                {
                    attach.URL = hdfDinhKem.Text;
                    attach.SizeKB = double.Parse(hdfFileSizeKB.Text);
                }
                if (e.ExtraParams["Command"] == "Update")
                {

                    attach.Id = int.Parse(RowSelectionModelTepTinDinhKem.SelectedRecordID);
                    controller.Update(attach);
                    wdAttachFile.Hide();
                }
                else
                {
                    controller.Insert(attach);
                    if (e.ExtraParams["Close"] == "True")
                    {
                        wdAttachFile.Hide();
                    }
                }
                grpTepTinDinhKem.Reload();
            }
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }

        protected void btnDownloadAttachFile_Click(object sender, DirectEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(e.ExtraParams["AttachFile"]))
                {
                    Dialog.ShowNotification("Không có tệp tin đính kèm");
                    return;
                }
                string serverPath = Server.MapPath(e.ExtraParams["AttachFile"]);
                //Dialog.ShowError(serverPath);
                if (Util.GetInstance().FileIsExists(serverPath) == false)
                {
                    Dialog.ShowNotification("Tệp tin không tồn tại hoặc đã bị xóa !");
                    return;
                }
                Response.Clear();
                //  Response.ContentType = "application/octet-stream";

                Response.AddHeader("Content-Disposition", "attachment; filename=" + e.ExtraParams["AttachFile"].Replace(" ", "_"));
                Response.WriteFile(serverPath);
                Response.End();
            }
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// Thong tin them ngoại ngữ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNgoaiNgu_Click(object sender, DirectEventArgs e)
        {
            try
            {
                var controller = new LanguageController();
                var language = new hr_Language();
                if (!string.IsNullOrEmpty(hdfRecordId.Text))
                    language.RecordId = Convert.ToInt32(hdfRecordId.Text);
                language.Note = txtGhiChuNgoaiNgu.Text;
                language.Rank = txtXepLoaiNgoaiNgu.Text;
                language.CreatedDate = DateTime.Now;
                if (!string.IsNullOrEmpty(hdfLanguageLevelId.Text))
                    language.LanguageId = Convert.ToInt32(hdfLanguageLevelId.Text);
                if (e.ExtraParams["Command"] == "Update")
                {
                    language.Id = int.Parse(RowSelectionModel_NgoaiNgu.SelectedRecordID);
                    controller.Update(language);
                    wdNgoaiNgu.Hide();
                }
                else
                {
                    controller.Insert(language);
                    if (e.ExtraParams["Close"] == "True")
                    {
                        wdNgoaiNgu.Hide();
                    }
                }
               
                grpNgoaiNgu.Reload();
            }
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }

        protected void Update_Click(object sender, DirectEventArgs e)
        {
            try
            {
                var controller = new WorkHistoryController();
                var workHistory = new hr_WorkHistory();
                var util = new Util();

                if (!string.IsNullOrEmpty(hdfRecordId.Text))
                    workHistory.RecordId = Convert.ToInt32(hdfRecordId.Text);
                workHistory.WorkPlace = txt_noilamviec.Text;
                workHistory.ExperienceWork = txtThanhTichTrongCongViec.Text;
                workHistory.ReasonLeave = txtLyDoThoiViec.Text;
                workHistory.SalaryLevel = decimal.Parse("0" + nbfMucLuong.Text);
                workHistory.Note = txtGhiChuKinhNghiemLamViec.Text;
                workHistory.WorkPosition = txt_vitriconviec.Text;

                if (!util.IsDateNull(dfKNLVTuNgay.SelectedDate))
                    workHistory.FromDate = dfKNLVTuNgay.SelectedDate;
                if (!util.IsDateNull(dfKNLVDenNgay.SelectedDate))
                    workHistory.ToDate = dfKNLVDenNgay.SelectedDate; 
                workHistory.CreatedBy = CurrentUser.User.UserName;
                workHistory.CreatedDate = DateTime.Now;

                if (e.ExtraParams["Command"] == "Edit")
                {
                    workHistory.EditedDate = DateTime.Now;
                    workHistory.Id = Convert.ToInt32(RowSelectionModelKinhNghiemLamViec.SelectedRecordID);
                    controller.UpdateWorkHistory(workHistory);
                    wdKinhNghiemLamViec.Hide();
                }
                else
                {
                    controller.InsertWorkHistory(workHistory);
                    if (e.ExtraParams["Close"] == "True")
                    {
                        wdKinhNghiemLamViec.Hide();
                    }
                }
                GridPanelKinhNghiemLamViec.Reload();
            }
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }

        protected void btnUpdateBangCap_Click(object sender, DirectEventArgs e)
        {
            try
            {
                var historyController = new EducationHistoryController();
                var educationHistory = new hr_EducationHistory();
                if (!string.IsNullOrEmpty(hdfRecordId.Text))
                    educationHistory.RecordId = Convert.ToInt32(hdfRecordId.Text);
                if (!string.IsNullOrEmpty(hdfMaTruongDaoTao.Text))
                    educationHistory.UniversityId = Convert.ToInt32(hdfMaTruongDaoTao.Text);
                educationHistory.Faculty = txt_khoa.Text;
                if (!string.IsNullOrEmpty(hdfQuocGia.Text))
                    educationHistory.NationId = Convert.ToInt32(hdfQuocGia.Text);
                educationHistory.IsGraduated = Chk_DaTotNghiep.Checked;
                if (!string.IsNullOrEmpty(txtTuNam.Text))
                    educationHistory.FromDate = new DateTime(Convert.ToInt32(txtTuNam.Text), 1, 1);
                if (!string.IsNullOrEmpty(txtDenNam.Text))
                    educationHistory.ToDate = new DateTime(Convert.ToInt32(txtDenNam.Text), 1, 1);
                educationHistory.CreatedDate = DateTime.Now;
                educationHistory.EditedDate = DateTime.Now;
                if (!string.IsNullOrEmpty(hdfTrainingSystemId.Text))
                    educationHistory.TrainingSystemId = Convert.ToInt32(hdfTrainingSystemId.Text);
                if (!string.IsNullOrEmpty(hdfMaChuyenNganh.Text))
                    educationHistory.IndustryId = Convert.ToInt32(hdfMaChuyenNganh.Text);
                if (!string.IsNullOrEmpty(hdfEducationId.Text))
                    educationHistory.EducationId = Convert.ToInt32(hdfEducationId.Text);
                if (!string.IsNullOrEmpty(hdfGraduationTypeId.Text))
                    educationHistory.GraduationTypeId = Convert.ToInt32(hdfGraduationTypeId.Text);

                if (e.ExtraParams["Command"] == "Edit")
                {
                    educationHistory.Id = Convert.ToInt32(RowSelectionModel_BangCap.SelectedRecordID);
                    historyController.UpdateEducation(educationHistory);
                    wdAddBangCap.Hide();
                }
                else
                {
                    historyController.InsertEducation(educationHistory);
                    if (e.ExtraParams["Close"] == "True")
                    {
                        wdAddBangCap.Hide();
                    }
                }
                GridPanel_BangCap.Reload();
            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Thông báo", ex.Message.ToString());
            }
        }
        protected void btnUpdateChungChi_Click(object sender, DirectEventArgs e)
        {
            try
            {
                var controller = new CertificateController();
                var certify = new hr_Certificate();
                var util = new Util();
                if (!string.IsNullOrEmpty(hdfRecordId.Text))
                    certify.RecordId = Convert.ToInt32(hdfRecordId.Text);
                if (!string.IsNullOrEmpty(hdfCertificateId.Text))
                    certify.CertificationId = Convert.ToInt32(hdfCertificateId.Text);
                if (!util.IsDateNull( df_NgayCap.SelectedDate))
                    certify.IssueDate = df_NgayCap.SelectedDate;
                if (!util.IsDateNull(df_NgayHetHan.SelectedDate))
                    certify.ExpirationDate = df_NgayHetHan.SelectedDate;
                certify.EducationPlace = txt_EducationPlace.Text;
                if (!string.IsNullOrEmpty(hdfCertificateGraduationTypeId.Text))
                    certify.GraduationTypeId = Convert.ToInt32(hdfCertificateGraduationTypeId.Text);
                certify.Note = txtGhiChuChungChi.Text;
              
                if (e.ExtraParams["Command"] == "Edit")
                {
                    certify.Id = int.Parse(RowSelectionModel_ChungChi.SelectedRecordID);
                    controller.Update(certify);
                    wdAddChungChi.Hide();
                }
                else
                {
                    controller.Insert(certify);
                    if (e.ExtraParams["Close"] == "True")
                    {
                        wdAddChungChi.Hide();
                    }
                }
                GridPanel_ChungChi.Reload();
            }
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }
        #endregion

        /// <summary>
        /// Xóa các bản ghi trên Grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void HandleChangesDelete(object sender, BeforeStoreChangedEventArgs e)
        {
            var deleted = e.DataHandler.JsonData;
            var prkey = deleted.Remove(deleted.IndexOf(','));
            prkey = prkey.Substring(prkey.LastIndexOf(':') + 1);
            var store = sender as Store;
            switch (store.ID)
            {
                case "StoreBaoHiem":
                    DataHandler.GetInstance().ExecuteNonQuery("delete from hr_Insurance where Id = " + prkey);
                    break;
                case "StoreDaiBieu":
                    DataHandler.GetInstance().ExecuteNonQuery("delete from hr_Delegate where Id = " + prkey);
                    break;
                case "StoreKhaNang":
                    DataHandler.GetInstance().ExecuteNonQuery("delete from hr_Ability where Id = " + prkey);
                    break;
                case "StoreKhenThuong":
                    DataHandler.GetInstance().ExecuteNonQuery("delete from hr_Reward where Id = " + prkey);
                    break;
                case "StoreKyLuat":
                    DataHandler.GetInstance().ExecuteNonQuery("delete from hr_Discipline where Id = " + prkey);
                    break;
                case "StoreQHGD":
                    DataHandler.GetInstance().ExecuteNonQuery("delete from hr_FamilyRelationship where Id = " + prkey);
                    break;
                case "StoreQuaTrinhDieuChuyen":
                    DataHandler.GetInstance().ExecuteNonQuery("delete from hr_WorkProcess where Id = " + prkey);
                    break;
                case "StoreTaiSan":
                    DataHandler.GetInstance().ExecuteNonQuery("delete from hr_Asset where Id = " + prkey);
                    break;
                case "Store_BangCapChungChi":
                    DataHandler.GetInstance().ExecuteNonQuery("delete from hr_Certificate where Id = " + RowSelectionModel_ChungChi.SelectedRecordID);
                    break;
                case "grpTepTinDinhKemStore":
                    DataHandler.GetInstance().ExecuteNonQuery("delete from hr_AttachFile where Id = " + RowSelectionModelTepTinDinhKem.SelectedRecordID);
                    break;
                case "StoreKinhNghiemLamViec":
                    DataHandler.GetInstance().ExecuteNonQuery("delete from hr_WorkHistory where Id = " + RowSelectionModelKinhNghiemLamViec.SelectedRecordID);
                    break;
                case "Store_BangCap":
                    DataHandler.GetInstance().ExecuteNonQuery("delete from hr_EducationHistory where Id = " + RowSelectionModel_BangCap.SelectedRecordID);
                    break;
                case "StoreNgoaiNgu":
                    DataHandler.GetInstance().ExecuteNonQuery("delete from hr_Language where Id = " + RowSelectionModel_NgoaiNgu.SelectedRecordID);
                    break;
                case "StoreDienBienLuong":
                    DataHandler.GetInstance().ExecuteNonQuery("delete from sal_SalaryDecision where Id = " + RowSelectionModelDienBienLuong.SelectedRecordID);
                    break;
                case "StoreQuaTrinhDaoTao":
                    DataHandler.GetInstance().ExecuteNonQuery("delete from hr_TrainingHistory where Id = " + RowSelectionModelQuaTrinhDaoTao.SelectedRecordID);
                    break;
                case "StoreHopDong":
                    DataHandler.GetInstance().ExecuteNonQuery("delete from hr_Contract where Id = " + RowSelectionModelHopDong.SelectedRecordID);
                    break;
                case "StoregrpDiNuocNgoai":
                    DataHandler.GetInstance().ExecuteNonQuery("delete from hr_GoAboard where Id = " + RowSelectionModelDiNuocNgoai.SelectedRecordID);
                    break;
            }
            btnDeleteRecord.Disabled = true;
            btnEditRecord.Disabled = true;
        }

        #region Các hàm xử lý sự kiện sửa
        [DirectMethod]
        public void GetDataForBaoHiem()
        {
            string id = RowSelectionModelBaoHiem.SelectedRecordID;
            if (id == "")
            {
                ExtNet.Msg.Alert("Thông báo", "Bạn chưa chọn bản ghi nào").Show();
            }
            else
            {
                var insurance = InsuranceController.GetById(Convert.ToInt32(id));
                var util = new Util();

                cbBHChucVu.Text = insurance.PositionName;
                hdfInsurancePositionId.Text = insurance.PositionId.ToString();
                if (!util.IsDateNull(insurance.FromDate))
                    dfBHTuNgay.SetValue(insurance.FromDate);
                if (!util.IsDateNull(insurance.ToDate))
                    dfBHDenNgay.SetValue(insurance.ToDate);
                txtBHPhuCap.Text = insurance.Allowance.ToString();
                txtBHMucLuong.Text = insurance.SalaryLevel.ToString();
                txtBHHSLuong.Text = insurance.SalaryFactor.ToString();
                txtBHGhiChu.Text = insurance.Note;
                txtBHTyle.Text = insurance.Rate;
                wdBaoHiem.Show();
            }
        }

        [DirectMethod]
        public void GetDataForDaiBieu()
        {
            string id = RowSelectionModelDaiBieu.SelectedRecordID;
            if (id == "")
            {
                ExtNet.Msg.Alert("Thông báo", "Bạn chưa chọn bản ghi nào").Show();
            }
            else
            {
                var dele = DelegateController.GetById(Convert.ToInt32(id));
                txtDBLoaiHinh.Text = dele.Type;
                txtDBNhiemKy.Text = dele.Term;
                if (!Util.GetInstance().IsDateNull(dele.FromDate))
                    dfDBTuNgay.SetValue(dele.FromDate);
                if (!Util.GetInstance().IsDateNull(dele.ToDate))
                    dfDBDenNgay.SetValue(dele.ToDate);
                txtDBGhiChu.Text = dele.Note;
                wdDaiBieu.Show();
            }
        }
        [DirectMethod]
        public void GetDataForHopDong()
        {
            string id = RowSelectionModelHopDong.SelectedRecordID;
            if (id == "")
            {
                ExtNet.Msg.Alert("Thông báo", "Bạn chưa chọn bản ghi nào").Show();
            }
            else
            {
                var contract = ContractController.GetById(Convert.ToInt32(id));
                var util = new Util();
                txtHopDongSoHopDong.Text = contract.ContractNumber;
                cbHopDongLoaiHopDong.Text = contract.ContractTypeName;
                hdfContractTypeId.Text = contract.ContractTypeId.ToString();
                cbHopDongTinhTrangHopDong.Text = contract.ContractStatusName;
                hdfContractStatusId.Text = contract.ContractStatusId.ToString();
                cbHopDongCongViec.Text = contract.JobName;
                hdfJobTitleOldId.Text = contract.JobId.ToString();
                if (!util.IsDateNull( contract.ContractEndDate))
                    dfHopDongNgayKiKet.SetValue(contract.ContractEndDate);
                if (!util.IsDateNull(contract.ContractDate))
                    dfHopDongNgayHopDong.SetValue(contract.ContractDate);
                if (!util.IsDateNull(contract.EffectiveDate))
                    dfNgayCoHieuLuc.SetValue(contract.EffectiveDate);
                txt_NguoiKyHD.Text = contract.PersonRepresent;
                hdfOldPositionId.Text = contract.PersonPositionId.ToString();
                cbx_HopDongChucVu.Text = contract.PersonPositionName;
                if (!string.IsNullOrEmpty(contract.AttachFileName))
                {
                    int pos = contract.AttachFileName.LastIndexOf('/');
                    if (pos != -1)
                    {
                        string tenTT = contract.AttachFileName.Substring(pos + 1);
                        fufHopDongTepTin.Text = tenTT;
                    }
                    hdfHopDongTepTinDK.Text = contract.AttachFileName;
                }
                cbx_HopDongTrangThai.Text = contract.ContractCondition;
                txtHopDongGhiChu.Text = contract.Note;

                wdHopDong.Show();
            }
        }

        [DirectMethod]
        public void GetDataForKhaNang()
        {
            string id = RowSelectionModelKhaNang.SelectedRecordID;
            if (id == "")
            {
                ExtNet.Msg.Alert("Thông báo", "Bạn chưa chọn bản ghi nào").Show();
            }
            else
            {
                var ability = AbilityController.GetById(Convert.ToInt32(id));
                cbKhaNang.Text = ability.AbilityName;
                hdfAbilityId.Text = ability.AbilityId.ToString();
                cbKhaNangXepLoai.Text = ability.GraduationTypeName;
                hdfAbilityGraduationTypeId.Text = ability.GraduationTypeId.ToString();
                txtKhaNangGhiChu.Text = ability.Note;
                wdKhaNang.Show();
            }
        }

        [DirectMethod]
        public void GetDataForKhenThuong()
        {
            string id = RowSelectionModelKhenThuong.SelectedRecordID;
            if (id == "")
            {
                ExtNet.Msg.Alert("Thông báo", "Bạn chưa chọn bản ghi nào").Show();
            }
            else
            {
                var reward = RewardController.GetById(Convert.ToInt32(id));
                txtKhenThuongSoQuyetDinh.Text = reward.DecisionNumber;
                if (!Util.GetInstance().IsDateNull(reward.DecisionDate))
                    dfKhenThuongNgayQuyetDinh.SetValue(reward.DecisionDate);
                cbHinhThucKhenThuong.Text = reward.FormRewardName;
                hdfRewardFormId.Text = reward.FormRewardId.ToString();
                cbxCapKhenThuong.Text = reward.LevelRewardName;
                hdfLevelRewardId.Text = reward.LevelRewardId.ToString();
                txtKhenThuongGhiCHu.Text = reward.Note;
                txtRewardDecisionMaker.Text = reward.DecisionMaker;
                cbLyDoKhenThuong.Text = reward.Reason;
                if (!string.IsNullOrEmpty(reward.AttachFileName))
                {
                    int pos = reward.AttachFileName.LastIndexOf('/');
                    if (pos != -1)
                    {
                        string tenTT = reward.AttachFileName.Substring(pos + 1);
                        fufKhenThuongTepTinDinhKem.Text = tenTT;
                    }
                    hdfKhenThuongTepTinDinhKem.Text = reward.AttachFileName;
                }
                wdKhenThuong.Show();
            }
        }

        [DirectMethod]
        public void GetDataForKyLuat()
        {
            string id = RowSelectionModelKyLuat.SelectedRecordID;
            if (id == "")
            {
                ExtNet.Msg.Alert("Thông báo", "Bạn chưa chọn bản ghi nào").Show();
            }
            else
            {
                var discipline = DisciplineController.GetById(Convert.ToInt32(id));
                txtKyLuatSoQD.Text = discipline.DecisionNumber;
                dfKyLuatNgayQuyetDinh.SetValue(discipline.DecisionDate);
                cbLyDoKyLuat.SelectedItem.Value = discipline.Reason;
                cbHinhThucKyLuat.Text = discipline.FormDisciplineName;
                hdfDisciplineFormId.Text = discipline.FormDisciplineId.ToString();
                cbxCapKyLuat.Text = discipline.LevelDisciplineName;
                hdfLevelDisciplineId.Text = discipline.LevelDisciplineId.ToString();
                txtKyLuatGhiChu.Text = discipline.Note;
                tgfKyLuatNguoiQD.Text = discipline.DecisionMaker;
                if (!string.IsNullOrEmpty(discipline.AttachFileName))
                {
                    int pos = discipline.AttachFileName.LastIndexOf('/');
                    if (pos != -1)
                    {
                        string tenTT = discipline.AttachFileName.Substring(pos + 1);
                        fufKyLuatTepTinDinhKem.Text = tenTT;
                    }
                    hdfKyLuatTepTinDinhKem.Text = discipline.AttachFileName;
                }
                wdKyLuat.Show();
            }
        }

        [DirectMethod]
        public void GetDataForQuanHeGiaDinh()
        {
            string id = RowSelectionModelQHGD.SelectedRecordID;
            if (id == "")
            {
                ExtNet.Msg.Alert("Thông báo", "Bạn chưa chọn bản ghi nào").Show();
            }
            else
            {
                var relation = FamilyRelationshipController.GetById(Convert.ToInt32(id));
                txtQHGDHoTen.Text = relation.FullName;
                if (relation.Sex == true)
                {
                    cbQHGDGioiTinh.Text = "Nam";
                    hdfRelationshipSex.Text = "1";
                } else
                {
                    hdfRelationshipSex.Text = "0";
                    cbQHGDGioiTinh.Text = "Nữ";
                }
                txtQHGDNamSinh.Text = relation.BirthYear.ToString();
                cbQuanHeGiaDinh.Text = relation.RelationName;
                hdfRelationshipId.Text = relation.RelationshipId.ToString();
                txtQHGDNgheNghiep.Text = relation.Occupation;
                txtQHGDNoiLamViec.Text = relation.WorkPlace;
                txtQHGDGhiChu.Text = relation.Note;
                txtSoCMT.Text = relation.IDNumber;
                chkQHGDLaNguoiPhuThuoc.Checked = relation.IsDependent;
                if (chkQHGDLaNguoiPhuThuoc.Checked == true)
                {
                    dfQHGDBatDauGiamTru.SetValue(relation.ReduceStartDate);
                    dfQHGDKetThucGiamTru.SetValue(relation.ReduceEndDate);
                }
                wdQuanHeGiaDinh.Show();
            }
        }
        [DirectMethod]
        public void GetDataForQuaTrinhDieuChuyen()
        {
            string id = RowSelectionModelQuaTrinhDieuChuyen.SelectedRecordID;
            if (id == "")
            {
                ExtNet.Msg.Alert("Thông báo", "Bạn chưa chọn bản ghi nào").Show();
            }
            else
            {
                var work = WorkProcessController.GetById(Convert.ToInt32(id));
                txtQTDCSoQuyetDinh.Text = work.DecisionNumber;
                tgfQTDCNguoiQuyetDinh.Text = work.DecisionMaker;
                dfQTDCNgayQuyetDinh.SetValue(work.DecisionDate);
                dfQTDCNgayCoHieuLuc.SetValue(work.EffectiveDate);
                cbxQTDCBoPhanMoi.Text = work.NewDepartmentName;
                hdfNewDepartmentId.Text = work.NewDepartmentId.ToString();
                cbxQTDCChucVuMoi.Text = work.NewPositionName;
                hdfNewPositionId.Text = work.NewPositionId.ToString();
                cbxCongViecMoi.Text = work.NewJobName;
                hdfJobTitleNewId.Text = work.NewJobId.ToString();
                if (!string.IsNullOrEmpty(work.AttachFileName))
                {
                    int pos = work.AttachFileName.LastIndexOf('/');
                    if (pos != -1)
                    {
                        string tenTT = work.AttachFileName.Substring(pos + 1);
                        fufQTDCTepTinDinhKem.Text = tenTT;
                    }
                    hdfQTDCTepTinDinhKem.Text = work.AttachFileName;
                }
                txtDieuChuyenGhiChu.Text = work.Note;
                wdQuaTrinhDieuChuyen.Show();
            }
        }

        [DirectMethod]
        public void GetDataForTaiSan()
        {
            string id = RowSelectionModelTaiSan.SelectedRecordID;
            if (id == "")
            {
                ExtNet.Msg.Alert("Thông báo", "Bạn chưa chọn bản ghi nào").Show();
            }
            else
            {
                var asset = AssetController.GetById(Convert.ToInt32(id));

                txtAssetName.Text = asset.AssetName;
                txtTaiSanSoLuong.SetValue(asset.Quantity);
                txtTheTaiSan.Text = asset.AssetCode;
                txtUnitCode.SetValue(asset.UnitCode);
                if (!Util.GetInstance().IsDateNull(asset.ReceiveDate))
                    tsDateField.SetValue(asset.ReceiveDate);
                tsTxtinhTrang.Text = asset.Status;
                tsGhiChu.Text = asset.Note;
                wdAddTaiSan.Show();
            }
        }
        [DirectMethod]
        public void GetDataForDaoTao()
        {
            try
            {
                string id = RowSelectionModelQuaTrinhDaoTao.SelectedRecordID;
                if (id == "")
                {
                    X.Msg.Alert("Thông báo", "Bạn chưa chọn bản ghi nào").Show();
                }
                else
                {
                    var training = TrainingHistoryController.GetById(Convert.ToInt32(id));
                    if(training != null)
                    {
                        txtDTTenKhoaDaoTao.Text = training.TrainingName;
                        txtDTGhiChu.Text = training.Note;
                        txtNoiDaoTao.Text = training.TrainingPlace;
                        txtLyDoDaoTao.Text = training.Reason;
                        dfDTTuNgay.SelectedDate = training.StartDate.Value;
                        dfDTDenNgay.SelectedDate = training.EndDate.Value;
                        hdfDTQuocGia.Text = training.NationId.ToString();
                        cbxDTQuocGia.Text = training.NationName;
                        wdQuaTrinhDaoTao.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }
        [DirectMethod]
        public void GetDataForChungChi()
        {
            try
            {
                string id = RowSelectionModel_ChungChi.SelectedRecordID;
                if (id == "")
                {
                    ExtNet.Msg.Alert("Thông báo", "Bạn chưa chọn bản ghi nào").Show();
                }
                else
                {
                    var certificate = CertificateController.GetById(Convert.ToInt32(id));
                    var util = new Util();
                    if (certificate == null)
                    {
                        return;
                    }
                    cbx_certificate.Text = certificate.CertificateName;
                    hdfCertificateId.Text = certificate.CertificationId.ToString();
                    cbx_XepLoaiChungChi.Text = certificate.GraduationTypeName;
                    hdfCertificateGraduationTypeId.Text = certificate.GraduationTypeId.ToString();
                    txt_EducationPlace.Text = certificate.EducationPlace;
                    if (!util.IsDateNull(certificate.IssueDate))
                        df_NgayCap.SelectedDate = certificate.IssueDate.Value;
                    if (!util.IsDateNull(certificate.ExpirationDate))
                        df_NgayHetHan.SelectedDate = certificate.ExpirationDate.Value;
                    txtGhiChuChungChi.Text = certificate.Note;

                    wdAddChungChi.Show();
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }

        [DirectMethod]
        public void GetDataForTepTin()
        {
            string id = RowSelectionModelTepTinDinhKem.SelectedRecordID;
            if (id == "")
            {
                ExtNet.Msg.Alert("Thông báo", "Bạn chưa chọn bản ghi nào").Show();
            }
            else
            {
                var teptin = AttachFileController.GetById(Convert.ToInt32(id));
                if (teptin == null)
                {
                    return;
                }
                txtFileName.Text = teptin.AttachFileName;
                hdfDinhKem.Text = teptin.URL;
                file_cv.Text = GetFileName(teptin.URL);
                file_cv.Disabled = true;
                hdfFileSizeKB.Text = teptin.SizeKB.ToString();
                txtGhiChu.Text = teptin.Note;
                wdAttachFile.Show();
            }
        }

        [DirectMethod]
        public void GetDataForKinhNghiemLamViec()
        {
            string id = RowSelectionModelKinhNghiemLamViec.SelectedRecordID;
            if (id == "")
            {
                ExtNet.Msg.Alert("Thông báo", "Bạn chưa chọn bản ghi nào").Show();
            }
            else
            {
                var work = WorkHistoryController.GetById(Convert.ToInt32(id));
                if (work != null)
                {
                    txt_noilamviec.Text = work.WorkPlace;
                    txt_vitriconviec.Text = work.WorkPosition;
                    txtLyDoThoiViec.Text = work.ReasonLeave;
                    nbfMucLuong.Value = work.SalaryLevel.ToString();
                    txtGhiChuKinhNghiemLamViec.Text = work.Note;
                    if (!Util.GetInstance().IsDateNull(work.FromDate))
                        dfKNLVTuNgay.SetValue(work.FromDate);
                    if (!Util.GetInstance().IsDateNull(work.ToDate))
                        dfKNLVDenNgay.SetValue(work.ToDate);
                    txtThanhTichTrongCongViec.Text = work.ExperienceWork;
                    wdKinhNghiemLamViec.Show();
                }
            }
        }

        [DirectMethod]
        public void GetDataForQuaTrinhHocTap()
        {
            try
            {
                string id = RowSelectionModel_BangCap.SelectedRecordID;
                if (id == "")
                {
                    X.Msg.Alert("Thông báo", "Bạn chưa chọn bản ghi nào").Show();
                }
                else
                {
                    EducationHistoryModel educationHistory = EducationHistoryController.GetById(Convert.ToInt32(id));
                    hdfMaTruongDaoTao.Text = educationHistory.UniversityId.ToString();
                    cbx_NoiDaoTaoBangCap.Text = educationHistory.UniversityName;
                    hdfQuocGia.Text = educationHistory.NationId.ToString();
                    cbx_quocgia.Text = educationHistory.NationName;
                    cbx_hinhthucdaotaobang.Text = educationHistory.TrainingSystemName;
                    hdfTrainingSystemId.Text = educationHistory.TrainingSystemId.ToString();
                    txt_khoa.Text = educationHistory.Faculty;
                    hdfMaChuyenNganh.Text = educationHistory.IndustryId.ToString();
                    cbx_ChuyenNganhBangCap.Text = educationHistory.IndustryName;
                    Chk_DaTotNghiep.Checked = educationHistory.IsGraduated;
                    cbx_trinhdobangcap.Text = educationHistory.EducationName;
                    hdfEducationId.Text = educationHistory.EducationId.ToString();
                    hdfGraduationTypeId.Text = educationHistory.GraduationTypeId.ToString();
                    cbx_xeploaiBangCap.Text = educationHistory.GraduateTypeName;
                    txtTuNam.Text = educationHistory.FromDate.Value.Year.ToString();
                    txtDenNam.Text = educationHistory.ToDate.Value.Year.ToString();
                    wdAddBangCap.Show();
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }

        [DirectMethod]
        public void GetDataForNgoaiNgu()
        {
            string id = RowSelectionModel_NgoaiNgu.SelectedRecordID;
            if (id == "")
            {
                ExtNet.Msg.Alert("Thông báo", "Bạn chưa chọn bản ghi nào").Show();
            }
            else
            {
                var language = LanguageController.GetById(Convert.ToInt32(id));
                hdfLanguageLevelId.Text = language.LanguageId.ToString();
                cbxNgoaiNgu.Text = language.LanguageName;
                txtXepLoaiNgoaiNgu.Text = language.Rank;
                txtGhiChuNgoaiNgu.Text = language.Note;
                wdNgoaiNgu.Show();
            }
        }
        [DirectMethod]
        public void GetDataForDiNuocNgoai()
        {
            string id = RowSelectionModelDiNuocNgoai.SelectedRecordID;
            if (id == "")
            {
                ExtNet.Msg.Alert("Thông báo", "Bạn chưa chọn bản ghi nào").Show();
            }
            else
            {
                var goBoard = GoAboardController.GetById(Convert.ToInt32(id));
                cbx_DNN_MaNuoc.Text = goBoard.NationName;
                hdfGoBoardNationId.Text = goBoard.NationId.ToString();
                dfNgayBatDau.SetValue(goBoard.StartDate);
                dfNgayKetThuc.SetValue(goBoard.EndDate);
                txtGoAboardReason.Text = goBoard.Reason;
                txtGoAboardNote.Text = goBoard.Note;
                wdDiNuocNgoai.Show();
            }
        }
        #endregion

        [DirectMethod]
        public void SetNgayHetHD()
        {
            var util = new Util();
            var contractTypeId = cbHopDongLoaiHopDong.SelectedItem != null ? cbHopDongLoaiHopDong.SelectedItem.Value : "";
            if (contractTypeId == "" || util.IsDateNull(dfNgayCoHieuLuc.SelectedDate)) return;
            var month = cat_ContractTypeServices.GetFieldValueById(int.Parse("0" + contractTypeId), "ContractMonth");
            var mont = int.Parse("0" + month);
            if (mont <= 0) return;
            var ngayBd = dfNgayCoHieuLuc.SelectedDate;
            ngayBd = ngayBd.AddMonths(mont);
            ngayBd = ngayBd.AddDays(-1);

            dfHopDongNgayKiKet.SetValue(ngayBd);
        }
        protected void StoreBusiness_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            if (string.IsNullOrEmpty(hdfRecordId.Text)) return;
            storeBusiness.DataSource = BusinessHistoryController.GetAll(Convert.ToInt32(hdfRecordId.Text), hdfBusinessType.Text);
            storeBusiness.DataBind();
        }

        private string GetFileName(string pathOfFile)
        {
            int pos = pathOfFile.LastIndexOf('/');
            if (pos == -1)
                pos = pathOfFile.LastIndexOf('\\');
            if (pos != -1)
            {
                return pathOfFile.Substring(pos + 1);
            }
            return "";
        }
    }

}

