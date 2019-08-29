using System;
using Ext.Net;
using Web.Core.Framework;
using System.Linq;
using Web.Core;
using Web.Core.Framework.Common;
using Web.Core.Framework.Utils;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.HumanRecord;
using Web.Core.Service.Catalog;
using Web.Core.Object.Security;

namespace Web.HRM.Modules.ProfileHuman.Management
{
    public partial class Contract : BasePage
    {
        private const string SuffixDecision = @"QĐ";
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!X.IsAjaxRequest)
            {
                // Set resource
                grpContract.ColumnModel.SetColumnHeader(2, "{0}".FormatWith(Resource.Get("Grid.EmployeeCode")));
                EmployeeGrid.Title = Resource.Get("Employee.List");

                // init department
                storeDepartment.DataSource = CurrentUser.DepartmentsTree;
                storeDepartment.DataBind();

                hdfUserID.Text = CurrentUser.User.Id.ToString();
                hdfMenuID.Text = MenuId.ToString();
                hdfDepartments.Text = DepartmentIds;
                  
                // load west
                new Core.Framework.Common.BorderLayout()
                {
                    menuID = MenuId,
                    script = "#{hdfDepartmentSelected}.setValue('" + Core.Framework.Common.BorderLayout.nodeID +
                             "');#{txtSearch}.reset();#{PagingToolbar1}.pageIndex = 0; #{PagingToolbar1}.doLoad();"
                }.AddDepartmentList(brlayout, CurrentUser, false);

            }
        }

        #region DirecMethods

        /// <summary>
        /// 
        /// </summary>
        [DirectMethod]
        public void GenerateSoQD()
        {
            var departments = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
            var suffix =
                SystemConfigController.GetValueByNameFollowDepartment(SystemConfigParameter.SUFFIX_SOHOPDONG,
                    departments);
            if (string.IsNullOrEmpty(suffix))
                suffix = SuffixDecision;
            var contractNumber = GenerateContractNumber(suffix);
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

        /// <summary>
        /// 
        /// </summary>
        [DirectMethod]
        public void GenerateSoQDHL()
        {
            string departments = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
            string suffix =
                SystemConfigController.GetValueByNameFollowDepartment(SystemConfigParameter.SUFFIX_SOHOPDONG,
                    departments);
            if (string.IsNullOrEmpty(suffix))
                suffix = SuffixDecision;
            string contractNumber = GenerateContractNumber(suffix);
            txtHopDongSoHopDongHL.Text = contractNumber;
        }

        /// <summary>
        /// 
        /// </summary>
        [DirectMethod]
        public void SetNgayHetHD()
        {
            var util = new ConvertUtils();
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

        /// <summary>
        /// 
        /// </summary>
        [DirectMethod]
        public void SetNgayHetHDHL()
        {
            var util = new ConvertUtils();
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdateHopDong_Click(object sender, DirectEventArgs e)
        {
            try
            {
                var contract = new hr_Contract();
                var controller = new ContractController();
                var util = new ConvertUtils();
                // upload file
                string path = string.Empty;
                if (fufHopDongTepTin.HasFile)
                {
                    // string directory = Server.MapPath("../");
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
                contract.AttachFileName = path != "" ? path : hdfHopDongTepTinDK.Text;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnHopDongAttachDownload_Click(object sender, DirectEventArgs e)
        {
            DownloadAttachFile("hr_Contract", hdfHopDongTepTinDK);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnHopDongAttachDelete_Click(object sender, DirectEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(hdfRecordId.Text))
                {
                    DeleteAttachFile("hr_Contract", int.Parse(hdfRecordId.Text), hdfHopDongTepTinDK);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEdit_Click(object sender, DirectEventArgs e)
        {
            try
            {
                string contractId = hdfRecordId.Text;
                if (contractId == "")
                {
                    Dialog.Alert("Thông báo bạn chưa chọn bản ghi nào");
                }
                else
                {
                    var contract = ContractController.GetById(Convert.ToInt32(contractId));
                    var util = new ConvertUtils();
                    txtHopDongSoHopDong.Text = contract.ContractNumber;
                    cbxContractType.SetValue(contract.ContractTypeId);
                    cbxContractType.Text = contract.ContractTypeName;
                    hdfContractTypeId.Text = contract.ContractTypeId.ToString();
                    cbxJob.SetValue(contract.JobId);
                    cbxJob.Text = contract.JobName;
                    hdfCbxJobId.Text = contract.JobId.ToString();
                    if (!util.IsDateNull(contract.ContractEndDate))
                        dfHopDongNgayKiKet.SetValue(contract.ContractEndDate);
                    if (!util.IsDateNull(contract.ContractDate))
                        dfHopDongNgayHopDong.SetValue(contract.ContractDate);
                    if (!util.IsDateNull(contract.EffectiveDate))
                        dfNgayCoHieuLuc.SetValue(contract.EffectiveDate);
                    txt_NguoiKyHD.Text = contract.PersonRepresent;
                    cbxPosition.Text = contract.PersonPositionName;
                    hdfContractStatusId.Text = contract.ContractStatusId.ToString();
                    cbxContractStatus.Text = contract.ContractStatusName;
                    hdfCbxPositionId.Text = contract.PersonPositionId.ToString();
                    if (!string.IsNullOrEmpty(contract.AttachFileName))
                    {
                        int pos = contract.AttachFileName.LastIndexOf('/');
                        if (pos != -1)
                        {
                            fufHopDongTepTin.Text = contract.AttachFileName.Substring(pos + 1);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, DirectEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(hdfRecordId.Text))
                {
                    new ContractController().Delete(Convert.ToInt32(hdfRecordId.Text));
                }
                grpContract.Reload();
            }
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdateHL_Click(object sender, DirectEventArgs e)
        {
            try
            {
                foreach (var employee in chkEmployeeRowSelection.SelectedRows)
                {
                    var recordId = Convert.ToInt32(employee.RecordID);
                    var contract = new hr_Contract();
                    var controller = new ContractController();
                    var util = new ConvertUtils();
                    var error = string.Empty;

                    // upload file
                    var path = string.Empty;
                    if (fufHopDongTepTinHL.HasFile)
                    {
                        path = UploadFile(fufHopDongTepTinHL, Constant.PathContract);
                    }
                    
                    contract.RecordId = recordId;
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
                    contract.AttachFileName = path != "" ? path : hdfHopDongTepTinDKHL.Text;
                    contract.Note = txtHopDongGhiChuHL.Text;
                    contract.CreatedBy = CurrentUser.User.UserName;
                    contract.CreatedDate = DateTime.Now;
                    var checkContract =
                        controller.CheckContractBeforeInsert(Convert.ToInt32(recordId), contract.EffectiveDate);
                    if (checkContract == null)
                    {
                        controller.Insert(contract);
                    }
                    else
                    {
                        error += hr_RecordServices.GetFieldValueById(Convert.ToInt32(recordId), "EmployeeCode") +
                                 ",";
                    }

                    if (!string.IsNullOrEmpty(error))
                    {
                        Dialog.Alert("Thông báo",
                            "Hợp đồng hiện tại của cán bộ có mã: " + error +
                            " vẫn còn hiệu lực. Bạn không được phép thay đổi hợp đồng.");
                    }

                    wdHopDongHangLoat.Hide();
                    grpContract.Reload();
                }
            }
            catch (Exception ex)
            {
                Dialog.Alert("Thông báo từ hệ thống", "Có lỗi xảy ra: " + ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [DirectMethod]
        public void SetValueQuery()
        {
            hdfRecordId.Text = "";
            RM.RegisterClientScriptBlock("clearSelections", "grpContract.getSelectionModel().clearSelections();");
            var query = string.Empty;
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