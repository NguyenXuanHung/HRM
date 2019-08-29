using System;
using System.Web;
using Ext.Net;
using System.IO;
using SoftCore;
using Web.Core.Framework;
using System.Linq;
using Web.Core.Framework.Common;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.HumanRecord;
using Web.Core.Service.Catalog;
using Web.Core.Object.Security;

namespace Web.HJM.Modules.ProfileHuman.Management
{
    public partial class Contract : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!X.IsAjaxRequest)
            {
                hdfUserID.Text = CurrentUser.User.Id.ToString();
                hdfMenuID.Text = MenuId.ToString();
                hdfDepartments.Text = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
                // load west
                new Core.Framework.Common.BorderLayout()
                {
                    menuID = MenuId,
                    script = "#{hdfDepartmentSelected}.setValue('" + Core.Framework.Common.BorderLayout.nodeID +
                             "');#{txtSearch}.reset();#{PagingToolbar1}.pageIndex = 0; #{PagingToolbar1}.doLoad();"
                }.AddDepartmentList(brlayout, CurrentUser, true);

            }

            if (btnEdit.Visible)
            {
                RowSelectionModel1.Listeners.RowSelect.Handler += "btnEdit.enable(); ";
                RowSelectionModel1.Listeners.RowDeselect.Handler += "btnEdit.disable(); ";
                grpContract.Listeners.RowDblClick.Handler +=
                    "if(CheckSelectedRows(grpContract)){btnUpdateHopDong.hide();btnEditHopDong.show();Button20.hide();}";
                grpContract.DirectEvents.RowDblClick.Event += btnEdit_Click;
            }

            if (btnDelete.Visible)
            {
                RowSelectionModel1.Listeners.RowSelect.Handler += "btnDelete.enable(); ";
                RowSelectionModel1.Listeners.RowDeselect.Handler += "btnDelete.disable(); ";
            }

            ucChooseEmployee.AfterClickAcceptButton += new EventHandler(ucChooseEmployee_AfterClickAcceptButton);
        }

        #region DirecMethods

        [DirectMethod]
        public void GenerateSoQD()
        {
            string departments = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
            string suffix =
                SystemConfigController.GetValueByNameFollowDepartment(SystemConfigParameter.SUFFIX_SOHOPDONG,
                    departments);
            if (string.IsNullOrEmpty(suffix))
                suffix = "QĐ";
            string contractNumber = GenerateContractNumber(suffix);
            txtHopDongSoHopDong.Text = contractNumber;
        }

        /// <summary>
        /// Sinh tự động số hợp đồng
        /// </summary>
        /// <param name="suffix"></param>
        /// <returns></returns>
        private string GenerateContractNumber(string suffix)
        {
            // get full suffix
            if (string.IsNullOrEmpty(suffix))
                return "";
            suffix = DateTime.Now.Year + "/" + suffix;
            // get max number of constract
            var contractNum = ContractController.GetContractNumberByCondition(suffix);
            if (contractNum != null && contractNum.Id > 0) // có số hợp đồng lớn nhất
            {
                string sohd = contractNum.ContractNumber;
                int pos = sohd.IndexOf('/');
                if (pos != -1)
                {
                    string stt = sohd.Trim().Substring(0, pos);
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

        [DirectMethod]
        public void GenerateSoQDHL()
        {
            string departments = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
            string suffix =
                SystemConfigController.GetValueByNameFollowDepartment(SystemConfigParameter.SUFFIX_SOHOPDONG,
                    departments);
            if (string.IsNullOrEmpty(suffix))
                suffix = "QĐ";
            string contractNumber = GenerateContractNumber(suffix);
            txtHopDongSoHopDongHL.Text = contractNumber;
        }

        [DirectMethod]
        public void SetNgayHetHD()
        {
            var util = new Util();
            if (hdfContractTypeId.Text != "" && !util.IsDateNull(dfNgayCoHieuLuc.SelectedDate))
            {
                string month =
                    cat_ContractTypeServices.GetFieldValueById(int.Parse("0" + hdfContractTypeId.Text),
                        "ContractMonth");
                int mont = int.Parse("0" + month);
                if (mont > 0)
                {
                    DateTime ngay_bd = dfNgayCoHieuLuc.SelectedDate;
                    ngay_bd = ngay_bd.AddMonths(mont);
                    ngay_bd = ngay_bd.AddDays(-1);

                    dfHopDongNgayKiKet.SetValue(ngay_bd);
                }
            }
        }

        [DirectMethod]
        public void SetNgayHetHDHL()
        {
            var util = new Util();
            if (hdfLoaiHopDong.Text != "" && !util.IsDateNull(dfNgayCoHieuLucHL.SelectedDate))
            {
                string month =
                    cat_ContractTypeServices.GetFieldValueById(int.Parse("0" + hdfLoaiHopDong.Text), "ContractMonth");
                int mont = int.Parse("0" + month);
                if (mont > 0)
                {
                    DateTime ngay_bd = dfNgayCoHieuLucHL.SelectedDate;
                    ngay_bd = ngay_bd.AddMonths(mont);
                    ngay_bd = ngay_bd.AddDays(-1);

                    dfHopDongNgayKiKetHL.SetValue(ngay_bd);
                }
            }
        }

        #endregion

        #region DirectEvent

        protected void btnUpdateHopDong_Click(object sender, DirectEventArgs e)
        {
            try
            {
                var contract = new hr_Contract();
                var controller = new ContractController();
                var util = new Util();
                // upload file
                string path = string.Empty;
                if (fufHopDongTepTin.HasFile)
                {
                    string directory = Server.MapPath("../");
                    path = UploadFile(fufHopDongTepTin, Constant.PathContract);
                }

                contract.RecordId = Convert.ToInt32(hdfPrKey.Text);
                if (!string.IsNullOrEmpty(hdfCbxJobId.Text))
                    contract.JobId = Convert.ToInt32(hdfCbxJobId.Text);
                if (!string.IsNullOrEmpty(hdfContractTypeId.Text))
                    contract.ContractTypeId = Convert.ToInt32(hdfContractTypeId.Text);
                if (!string.IsNullOrEmpty(hdfContractStatusId.Text))
                    contract.ContractStatusId = Convert.ToInt32(hdfContractStatusId.Text);
                if (!util.IsDateNull(dfHopDongNgayHopDong.SelectedDate))
                    contract.ContractDate = dfHopDongNgayHopDong.SelectedDate;
                if (!string.IsNullOrEmpty(hdfRecruitmentTypeId.Text))
                    contract.RecruitmentTypeId = Convert.ToInt32(hdfRecruitmentTypeId.Text);

                // sinh số hợp đồng
                string departments = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
                string suffix =
                    SystemConfigController.GetValueByNameFollowDepartment(SystemConfigParameter.SUFFIX_SOHOPDONG,
                        departments);
                contract.ContractNumber = GenerateContractNumber(suffix);
                if (!string.IsNullOrEmpty(txtHopDongSoHopDong.Text))
                    contract.ContractNumber = txtHopDongSoHopDong.Text;

                if (!util.IsDateNull(dfHopDongNgayKiKet.SelectedDate))
                    contract.ContractEndDate = dfHopDongNgayKiKet.SelectedDate;
                if (!util.IsDateNull(dfNgayCoHieuLuc.SelectedDate))
                    contract.EffectiveDate = dfNgayCoHieuLuc.SelectedDate;
                contract.PersonRepresent = txt_NguoiKyHD.Text;
                if (!string.IsNullOrEmpty(hdfCbxPositionId.Text))
                    contract.PersonPositionId = Convert.ToInt32(hdfCbxPositionId.Text);
                if (path != "")
                    contract.AttachFileName = path;
                else
                    contract.AttachFileName = hdfHopDongTepTinDK.Text;
                contract.Note = txtHopDongGhiChu.Text;
                contract.CreatedBy = CurrentUser.User.UserName;
                contract.CreatedDate = DateTime.Now;
                if (e.ExtraParams["Command"] == "Update")
                {
                    contract.Id = int.Parse("0" + hdfRecordId.Text);
                    controller.Update(contract);
                    wdHopDong.Hide();
                }
                else
                {
                    // kiểm tra còn hợp đồng nào chưa hết hạn không
                    var checkContract =
                        controller.CheckContractBeforeInsert(Convert.ToInt32(hdfRecordId.Text), contract.EffectiveDate);
                    if (checkContract != null) // cán bộ còn ít nhất 1 hợp đồng có giá trị
                    {
                        ExtNet.Msg.Alert("Thông báo",
                                "Hợp đồng hiện tại của cán bộ vẫn còn hiệu lực. Bạn không được phép thay đổi hợp đồng.")
                            .Show();
                        return;
                    }

                    controller.Insert(contract);

                    if (e.ExtraParams["Close"] == "True")
                    {
                        wdHopDong.Hide();
                    }
                    else
                    {
                        GenerateSoQD();
                    }
                }

                grpContract.Reload();
            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Thông báo từ hệ thống", "Có lỗi xảy ra: " + ex.Message).Show();
            }
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
                if (!string.IsNullOrEmpty(hdfRecordId.Text))
                {
                    DeleteTepTinDinhKem("hr_Contract", int.Parse(hdfRecordId.Text), hdfHopDongTepTinDK);
                    hdfHopDongTepTinDK.Text = "";
                    grpContract.Reload();
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

        protected void btnEdit_Click(object sender, DirectEventArgs e)
        {
            try
            {
                string contractId = hdfRecordId.Text;
                if (contractId == "")
                {
                    ExtNet.Msg.Alert("Thông báo", "Bạn chưa chọn bản ghi nào").Show();
                }
                else
                {
                    var contract = hr_ContractServices.GetById(Convert.ToInt32(contractId));
                    var util = new Util();
                    txtHopDongSoHopDong.Text = contract.ContractNumber;
                    cbxContractType.SetValue(contract.ContractTypeId);
                    cbxContractType.Text = cat_ContractTypeServices.GetFieldValueById(contract.ContractTypeId, "Name");
                    hdfContractTypeId.Text = contract.ContractTypeId.ToString();
                    cbxJob.SetValue(contract.JobId);
                    cbxJob.Text = cat_JobTitleServices.GetFieldValueById(contract.JobId, "Name");
                    hdfCbxJobId.Text = contract.JobId.ToString();
                    if (!util.IsDateNull(contract.ContractEndDate))
                        dfHopDongNgayKiKet.SetValue(contract.ContractEndDate);
                    if (!util.IsDateNull(contract.ContractDate))
                        dfHopDongNgayHopDong.SetValue(contract.ContractDate);
                    if (!util.IsDateNull(contract.EffectiveDate))
                        dfNgayCoHieuLuc.SetValue(contract.EffectiveDate);
                    txt_NguoiKyHD.Text = contract.PersonRepresent;
                    cbxPosition.SetValue(contract.PersonPositionId);
                    cbxPosition.Text = cat_PositionServices.GetFieldValueById(contract.PersonPositionId, "Name");
                    hdfContractStatusId.Text = contract.ContractStatusId.ToString();
                    cbxContractStatus.Text =
                        cat_ContractStatusServices.GetFieldValueById(contract.ContractStatusId, "Name");
                    hdfCbxPositionId.Text = contract.PersonPositionId.ToString();
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

                    txtHopDongGhiChu.Text = contract.Note;
                    wdHopDong.Show();
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }

        protected void btnDelete_Click(object sender, DirectEventArgs e)
        {
            try
            {
                foreach (var item in RowSelectionModel1.SelectedRows)
                {
                    new ContractController().Delete(int.Parse("0" + item.RecordID));
                }

                RM.RegisterClientScriptBlock("Grid_Reload", "ReloadGrid();");
            }
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }

        #endregion

        public void ucChooseEmployee_AfterClickAcceptButton(object sender, EventArgs e)
        {
            try
            {
                hdfTotalRecord.Text = ucChooseEmployee.SelectedRow.Count.ToString();
                foreach (var item in ucChooseEmployee.SelectedRow)
                {
                    // get employee information
                    var hs = RecordController.GetByEmployeeCode(item.RecordID);
                    string RecordId = hs.Id.ToString();
                    string EmployeeCode = hs.EmployeeCode;
                    string FullName = hs.FullName;
                    string DepartmentName = hs.DepartmentName;
                    string PositionName = hs.PositionName;
                    // insert record to grid
                    RM.RegisterClientScriptBlock("insert" + RecordId,
                        string.Format("addRecord('{0}', '{1}', '{2}', '{3}');", RecordId, EmployeeCode, FullName,
                            DepartmentName));
                }
            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Thông báo từ hệ thống", "Có lỗi xảy ra: " + ex.Message).Show();
            }
        }

        public string UploadFile(object sender, string relativePath)
        {
            FileUploadField obj = (FileUploadField) sender;
            HttpPostedFile file = obj.PostedFile;
            DirectoryInfo
                dir = new DirectoryInfo(Server.MapPath(relativePath)); // save file to directory HoSoNhanSu/File
            // if directory not exist then create this
            if (dir.Exists == false)
                dir.Create();
            string rdstr = Util.GetInstance().GetRandomString(7);
            string path = Server.MapPath(relativePath) + "/" + rdstr + "_" + obj.FileName;
            if (File.Exists(path))
                return "";
            FileInfo info = new FileInfo(path);
            file.SaveAs(path);
            return relativePath + "/" + rdstr + "_" + obj.FileName;
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

                string serverPath = Server.MapPath("../") + sender.Text;
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
                var contract = hr_ContractServices.GetById(prkey);
                contract.AttachFileName = "";
                new ContractController().Update(contract);
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

        protected void btnUpdateHL_Click(object sender, DirectEventArgs e)
        {
            try
            {
                //var rowSelect = grp_DanhSachCanBo.SelectionModel.Primary as RowSelectionModel;
                var listId = e.ExtraParams["ListRecordId"].Split(',');
                if (listId.Count() < 1)
                {
                    ExtNet.Msg.Alert("Thông báo", "Bạn hãy chọn ít nhất 1 cán bộ").Show();
                    return;
                }
                else
                {
                    for (var i = 0; i < listId.Length - 1; i++)
                    {
                        var RecordId = listId[i];
                        var contract = new hr_Contract();
                        var controller = new ContractController();
                        var util = new Util();
                        string error = string.Empty;

                        // upload file
                        string path = string.Empty;
                        if (fufHopDongTepTinHL.HasFile)
                        {
                            string directory = Server.MapPath("../");
                            path = UploadFile(fufHopDongTepTinHL, Constant.PathContract);
                        }

                        if (!string.IsNullOrEmpty(RecordId))
                            contract.RecordId = Convert.ToInt32(RecordId);
                        if (!string.IsNullOrEmpty(cbHopDongCongViecHL.SelectedItem.Value))
                            contract.JobId = Convert.ToInt32(cbHopDongCongViecHL.SelectedItem.Value);
                        if (!string.IsNullOrEmpty(hdfLoaiHopDong.Text))
                            contract.ContractTypeId = Convert.ToInt32(hdfLoaiHopDong.Text);
                        if (!string.IsNullOrEmpty(cbHopDongTinhTrangHopDongHL.SelectedItem.Value))
                            contract.ContractStatusId = Convert.ToInt32(cbHopDongTinhTrangHopDongHL.SelectedItem.Value);
                        if (!util.IsDateNull(dfHopDongNgayHopDongHL.SelectedDate))
                            contract.ContractDate = dfHopDongNgayHopDongHL.SelectedDate;
                        contract.ContractNumber = txtHopDongSoHopDongHL.Text;
                        if (!util.IsDateNull(dfHopDongNgayKiKetHL.SelectedDate))
                            contract.ContractEndDate = dfHopDongNgayKiKetHL.SelectedDate;
                        if (!util.IsDateNull(dfNgayCoHieuLucHL.SelectedDate))
                            contract.EffectiveDate = dfNgayCoHieuLucHL.SelectedDate;
                        contract.PersonRepresent = txt_NguoiKyHDHL.Text;
                        if (cbx_HopDongChucVuHL.SelectedItem.Value != null)
                            contract.PersonPositionId = Convert.ToInt32(cbx_HopDongChucVuHL.SelectedItem.Value);
                        if (path != "")
                            contract.AttachFileName = path;
                        else
                            contract.AttachFileName = hdfHopDongTepTinDKHL.Text;
                        contract.Note = txtHopDongGhiChuHL.Text;
                        contract.CreatedBy = CurrentUser.User.UserName;
                        contract.CreatedDate = DateTime.Now;
                        var checkContract =
                            controller.CheckContractBeforeInsert(Convert.ToInt32(RecordId), contract.EffectiveDate);
                        if (checkContract == null)
                        {
                            controller.Insert(contract);
                        }
                        else
                        {
                            error += hr_RecordServices.GetFieldValueById(Convert.ToInt32(RecordId), "EmployeeCode") +
                                     ",";
                        }

                        if (!string.IsNullOrEmpty(error))
                        {
                            ExtNet.Msg.Alert("Thông báo",
                                "Hợp đồng hiện tại của cán bộ có mã: " + error +
                                " vẫn còn hiệu lực. Bạn không được phép thay đổi hợp đồng.").Show();
                        }

                        wdHopDongHangLoat.Hide();
                        grpContract.Reload();
                    }
                }
            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Thông báo từ hệ thống", "Có lỗi xảy ra: " + ex.Message).Show();
            }
        }

        [DirectMethod]
        public void DownloadAttach(string path)
        {
            try
            {
                string serverPath = Server.MapPath(path);
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

        [DirectMethod]
        public void SetValueQuery()
        {
            hdfRecordId.Text = "";
            RM.RegisterClientScriptBlock("clearSelections", "grpContract.getSelectionModel().clearSelections();");
            string query = string.Empty;
            query += filterCbxContractType.SelectedItem.Value ?? "";
            query += ";";
            query += filterJob.SelectedItem.Value ?? "";
            query += ";";
            query += filterPosition.SelectedItem.Value ?? "";
            query += ";";
            query += filterContractStatus.SelectedItem.Value ?? "";
            query += ";";

            hdfQuery.Text = query;
            PagingToolbar1.PageIndex = 0;
            grpContract.Reload();
        }
    }
}