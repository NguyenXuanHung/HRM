using System;
using System.Collections.Generic;
using System.Linq;
using Ext.Net;
using Web.Core.Framework; 
using Web.Core.Service.HumanRecord;
using Web.Core.Service.Catalog;
using Web.Core;
using Web.Core.Framework.Common;
using Web.Core.Helper;
using Web.Core.Object.Salary;
using Web.Core.Object.Security;

namespace Web.HRM.Modules.ProfileHuman.DecisionSalary
{
    public partial class DecisionSalary : BasePage
    {
        private const string ConstNameCondition = "QDL_%";
        private const int ConstSalaryBasic = 1300000;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!ExtNet.IsAjaxRequest)
            {
                HideColumn();
                hdfMenuID.SetValue(MenuId);
                hdfUserID.SetValue(CurrentUser.User.Id);
                hdfDepartments.Text = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
                new Core.Framework.Common.BorderLayout()
                {
                    menuID = MenuId,
                    script = "#{hdfDepartmentSelectedId}.setValue('" + Core.Framework.Common.BorderLayout.nodeID + "');Store1.reload();",
                }.AddDepartmentList(br, CurrentUser, true);

                // Handle config in manifest
                var lists =
                    new XMLProcess().GetManifestByVisible(Server.MapPath("../../../") + "Manifest.xml",
                        "SalaryDecision", false);
                foreach(var item in lists)
                {
                    FindControl(item.id).Visible = item.visible;
                }
            }

            if(btnEdit.Visible)
            {
                GridPanel1.DirectEvents.RowDblClick.Event += new ComponentDirectEvent.DirectEventHandler(btnEdit_Click);
                GridPanel1.DirectEvents.RowDblClick.Before +=
                    "#{hdfButtonClick}.setValue('Edit');#{btnCapNhat}.hide();#{btnCapNhatSua}.show();#{btnCapNhatDongLai}.hide();";
                GridPanel1.DirectEvents.RowDblClick.EventMask.ShowMask = true;
            }

            ucChooseEmployee1.AfterClickAcceptButton += new EventHandler(ucChooseEmployee1_AfterClickAcceptButton);
        }

        #region cấu hình cột cho gridpanel

        /// <summary>
        /// Ẩn cột
        /// </summary>
        private void HideColumn()
        {
            try
            {
                var departments = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
                var list = SystemConfigController.GetAllByNameAndDepartment(ConstNameCondition, departments);
                var columnList = new List<string>();
                foreach (var item in list)
                {
                    if (string.IsNullOrEmpty(item.Value) || (!string.IsNullOrEmpty(item.Value) && bool.Parse(item.Value)))
                    {
                        columnList.Add(item.Name);
                    }
                }

                foreach(var item in GridPanel1.ColumnModel.Columns)
                {
                    if(columnList.Contains(item.ColumnID))
                    {
                        item.Hidden = true;
                    }
                }
            }
            catch(Exception ex)
            {
                X.Msg.Alert("Thông báo từ hệ thống", "Có lỗi xảy ra: " + ex.Message).Show();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [DirectMethod]
        public void LoadConfigGridPanel()
        {
            try
            {
                var departments = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
                var list = SystemConfigController.GetAllByNameAndDepartment(ConstNameCondition, departments);
                foreach(var item in list)
                {
                    switch(item.Name)
                    {
                        case SystemConfigParameter.QDL_LUONGCUNG:
                            chkLuongCung.Checked = bool.Parse(item.Value);
                            break;
                        case SystemConfigParameter.QDL_HESOLUONG:
                            chkHeSoLuong.Checked = bool.Parse(item.Value);
                            break;
                        case SystemConfigParameter.QDL_PHANTRAMHL:
                            chkPhuCapKhac.Checked = bool.Parse(item.Value);
                            break;
                        case SystemConfigParameter.QDL_VUOTKHUNG:
                            chkVuotKhung.Checked = bool.Parse(item.Value);
                            break;
                        case SystemConfigParameter.QDL_LUONGDONGBHXH:
                            chkLuongDongBHXH.Checked = bool.Parse(item.Value);
                            break;
                        case SystemConfigParameter.QDL_BACLUONG:
                            chkBacLuong.Checked = bool.Parse(item.Value);
                            break;
                        case SystemConfigParameter.QDL_BACLUONGNB:
                            chkBacLuongNB.Checked = bool.Parse(item.Value);
                            break;
                        case SystemConfigParameter.QDL_NGAYHL:
                            chkNgayHL.Checked = bool.Parse(item.Value);
                            break;
                        case SystemConfigParameter.QDL_NGAYHLNB:
                            chkNgayHLNB.Checked = bool.Parse(item.Value);
                            break;
                        case SystemConfigParameter.QDL_SOQD:
                            chkSoQD.Checked = bool.Parse(item.Value);
                            break;
                        case SystemConfigParameter.QDL_NGAYQD:
                            chkNgayQD.Checked = bool.Parse(item.Value);
                            break;
                        case SystemConfigParameter.QDL_NGAYHIEULUC:
                            chkNgayHieuLuc.Checked = bool.Parse(item.Value);
                            break;
                        case SystemConfigParameter.QDL_NGAYHETHIEULUC:
                            chkNgayHetHieuLuc.Checked = bool.Parse(item.Value);
                            break;
                        case SystemConfigParameter.QDL_NGUOIQD:
                            chkNguoiQD.Checked = bool.Parse(item.Value);
                            break;
                    }
                }
            }
            catch(Exception ex)
            {
                X.Msg.Alert("Thông báo từ hệ thống", "Có lỗi xảy ra: " + ex.Message).Show();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCapNhatCauHinh_Click(object sender, DirectEventArgs e)
        {
            try
            {
                //SystemController controller = new SystemController();
                var controller = new SystemConfigController();
                var departments = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
                // department
                var arrDepartment = departments.Split(new[] { ',' }, StringSplitOptions.None);
                for(var i = 0; i < arrDepartment.Length; i++)
                {
                    arrDepartment[i] = "{0}".FormatWith(arrDepartment[i]);
                }

                controller.CreateOrSave(SystemConfigParameter.QDL_LUONGCUNG, chkLuongCung.Checked.ToString(),
                    string.Join(",", arrDepartment));
                controller.CreateOrSave(SystemConfigParameter.QDL_HESOLUONG, chkHeSoLuong.Checked.ToString(),
                    string.Join(",", arrDepartment));
                controller.CreateOrSave(SystemConfigParameter.QDL_PHANTRAMHL, chkPhuCapKhac.Checked.ToString(),
                    string.Join(",", arrDepartment));
                controller.CreateOrSave(SystemConfigParameter.QDL_VUOTKHUNG, chkVuotKhung.Checked.ToString(),
                    string.Join(",", arrDepartment));
                controller.CreateOrSave(SystemConfigParameter.QDL_LUONGDONGBHXH, chkLuongDongBHXH.Checked.ToString(),
                    string.Join(",", arrDepartment));
                controller.CreateOrSave(SystemConfigParameter.QDL_BACLUONG, chkBacLuong.Checked.ToString(),
                    string.Join(",", arrDepartment));
                controller.CreateOrSave(SystemConfigParameter.QDL_BACLUONGNB, chkBacLuongNB.Checked.ToString(),
                    string.Join(",", arrDepartment));
                controller.CreateOrSave(SystemConfigParameter.QDL_NGAYHL, chkNgayHL.Checked.ToString(),
                    string.Join(",", arrDepartment));
                controller.CreateOrSave(SystemConfigParameter.QDL_NGAYHLNB, chkNgayHLNB.Checked.ToString(),
                    string.Join(",", arrDepartment));
                controller.CreateOrSave(SystemConfigParameter.QDL_SOQD, chkSoQD.Checked.ToString(),
                    string.Join(",", arrDepartment));
                controller.CreateOrSave(SystemConfigParameter.QDL_NGAYQD, chkNgayQD.Checked.ToString(),
                    string.Join(",", arrDepartment));
                controller.CreateOrSave(SystemConfigParameter.QDL_NGAYHIEULUC, chkNgayHieuLuc.Checked.ToString(),
                    string.Join(",", arrDepartment));
                controller.CreateOrSave(SystemConfigParameter.QDL_NGAYHETHIEULUC, chkNgayHetHieuLuc.Checked.ToString(),
                    string.Join(",", arrDepartment));
                controller.CreateOrSave(SystemConfigParameter.QDL_NGUOIQD, chkNguoiQD.Checked.ToString(),
                    string.Join(",", arrDepartment));

                Dialog.ShowNotification("Đã lưu cấu hình");
                wdConfigGridPanel.Hide();
                Response.Redirect(Request.RawUrl);
            }
            catch(Exception ex)
            {
                ExtNet.Msg.Alert("Thông báo từ hệ thống", "Có lỗi xảy ra: " + ex.Message).Show();
            }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ucChooseEmployee1_AfterClickAcceptButton(object sender, EventArgs e)
        {
            foreach(var item in ucChooseEmployee1.SelectedRow)
            {
                //RecordId, EmployeeCode, FullName, DepartmentName, PositionName, QuantumId, SalaryGrade, SalaryFactor, SalaryBasic,
                //SalaryInsurance, SalaryPayDate, PositionAllowance, OtherAllowance, OutFrame
                // get employee information
                if(!string.IsNullOrEmpty(item.RecordID))
                {
                    // get employee information
                    var recordModel = RecordController.GetById(Convert.ToInt32(item.RecordID));
                    // insert record to grid
                    RM.RegisterClientScriptBlock("insert" + recordModel.Id, AddRecordString(recordModel));
                }


            }
        }        

        #region các hàm xử lý sự kiện direct event click

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDownloadAttachFile_Click(object sender, DirectEventArgs e)
        {
            try
            {
                if(hdfRecordId.Text == "")
                {
                    X.Msg.Alert("Thông báo từ hệ thống", "Không tìm thấy quyết định lương").Show();
                }

                var hsl = SalaryDecisionController.GetById(Convert.ToInt32(hdfRecordId.Text));
                DownloadAttachFile("sal_SalaryDecision", hsl.AttachFileName);
            }
            catch(Exception ex)
            {
                X.Msg.Alert("Thông báo từ hệ thống", "Có lỗi xảy ra: " + ex.Message).Show();
            }
        }

        /// <summary>
        /// #############################################
        /// Create salary decision for employee
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCapNhat_Click(object sender, DirectEventArgs e)
        {
            try
            {
               
                var hsl = new sal_SalaryDecision();

                // upload file
                var path = string.Empty;
                if(fufTepTinDinhKem.HasFile)
                {
                    path = UploadFile(fufTepTinDinhKem, Constant.PathDecisionSalary);
                }

                // decide information
                if(!string.IsNullOrEmpty(hdfChonCanBo.Text))
                    hsl.RecordId = Convert.ToInt32(hdfChonCanBo.Text);

                if(txtSoQDMoi.Visible)
                    hsl.DecisionNumber = txtSoQDMoi.Text;
                if(!DatetimeHelper.IsNull(dfNgayQDMoi.SelectedDate))
                    hsl.DecisionDate = dfNgayQDMoi.SelectedDate;
                if(txtTenQDMoi.Visible)
                    hsl.Name = txtTenQDMoi.Text;
                if(!DatetimeHelper.IsNull(dfNgayHieuLucMoi.SelectedDate))
                    hsl.EffectiveDate = dfNgayHieuLucMoi.SelectedDate;
                if(txtNguoiQD.Visible)
                    hsl.SignerName = txtNguoiQD.Text;
                var makerPosition = hdfIsMakerPosition.Text == @"0" ? cbxMakerPosition.Text : cbxMakerPosition.SelectedItem.Text;
                //hsl.SignerPosition = makerPosition;
                if(cbHopDongLoaiHopDongMoi.SelectedItem.Value != null)
                {
                    hsl.ContractId = Convert.ToInt32(hdfLoaiHopDong.Text);
                }

                // attach file
                hsl.AttachFileName = path != "" ? path : hdfTepTinDinhKem.Text;
                if(txtGhiChuMoi.Visible)
                    hsl.Note = txtGhiChuMoi.Text;

                // salary information
                //if(cbx_ngachMoi.Visible)
                //    hsl.QuantumId = Convert.ToInt32(hdfQuantumId.Text);
                //    hsl.SalaryFactor = double.Parse(txtHeSoLuongMoi.Text.Replace('.', ','));
                //if(txtMucLuongMoi.Visible && !string.IsNullOrEmpty(txtMucLuongMoi.Text))
                //    hsl.SalaryBasic = double.Parse(txtMucLuongMoi.Text.Replace('.', ','));
                //if(txtLuongDongBHMoi.Visible && !string.IsNullOrEmpty(txtLuongDongBHMoi.Text))
                //    hsl.SalaryInsurance = double.Parse(txtLuongDongBHMoi.Text.Replace('.', ','));
                //if(txtBacLuongNBMoi.Visible)
                //    hsl.SalaryGradeLift = txtBacLuongNBMoi.Text;
                //if(dfNgayHLNBMoi.Visible && !util.IsDateNull(dfNgayHLNBMoi.SelectedDate))
                //    hsl.SalaryGradeDate = dfNgayHLNBMoi.SelectedDate;
                //if(txtVuotKhungMoi.Visible && !string.IsNullOrEmpty(txtVuotKhungMoi.Text))
                //    hsl.OutFrame = double.Parse("0" + txtVuotKhungMoi.Text);
                //if(txtThamnien.Visible && !string.IsNullOrEmpty(txtThamnien.Text))
                //    hsl.Seniority = double.Parse("0" + txtThamnien.Text);
                //if(chkNangNgach.Checked == true)
                //    hsl.IsLiftQuantum = true;

                hsl.CreatedBy = CurrentUser.User.UserName;
                hsl.CreatedDate = DateTime.Now;

                if(e.ExtraParams["Command"] == "Edit")
                {
                    hsl.Id = int.Parse("0" + hdfRecordId.Text);
                    SalaryDecisionController.Update(new SalaryDecisionModel(hsl));
                    wdTaoQuyetDinhLuong.Hide();
                }
                else
                {
                    // add salary decision
                    SalaryDecisionController.Create(new SalaryDecisionModel(hsl));
                    if(e.ExtraParams["Close"] == "True")
                    {
                        wdTaoQuyetDinhLuong.Hide();
                    }
                    else
                    {
                        RM.RegisterClientScriptBlock("resetform1",
                            "ResetWdTaoQuyetDinhLuong(); Ext.net.DirectMethods.GenerateSoQD();");
                    }
                }

                RM.RegisterClientScriptBlock("rlgr", "Store1.reload();");
            }
            catch(Exception ex)
            {
                X.Msg.Alert("Thông báo từ hệ thống", "Có lỗi xảy ra khi lưu quyết định lương: " + ex.Message).Show();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQDLDownload_Click(object sender, DirectEventArgs e)
        {
            DownloadAttachFile("sal_SalaryDecision", hdfTepTinDinhKem);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQDLDelete_Click(object sender, DirectEventArgs e)
        {
            if(hdfRecordId.Text != "")
            {
                DeleteAttackFile("sal_SalaryDecision", int.Parse("0" + hdfRecordId.Text), hdfTepTinDinhKem);
                hdfTepTinDinhKem.Text = "";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCapNhatHL_Click(object sender, DirectEventArgs e)
        {
            try
            {
                var salaryController = new SalaryDecisionController();

                var errorStr = string.Empty;
                // upload file
                var path = string.Empty;
                if(cpfAttachHL.Visible && fufTepTinDinhKemHL.HasFile)
                {
                    path = UploadFile(fufTepTinDinhKemHL, Constant.PathDecisionSalary);
                }

                var rs = JSON.Deserialize<List<SalaryDecisionModel>>(e.ExtraParams["json"]);
                foreach(var created in rs)
                {
                    var hsl = new sal_SalaryDecision();

                    // decision information
                    if(txtSoQDHL.Visible)
                        hsl.DecisionNumber = txtSoQDHL.Text.Trim();
                    if(txtTenQDHL.Visible)
                        hsl.Name = txtTenQDHL.Text.Trim();
                    if(dfNgayQDHL.Visible && !DatetimeHelper.IsNull(dfNgayQDHL.SelectedDate))
                        hsl.DecisionDate = dfNgayQDHL.SelectedDate;
                    if(dfNgayHieuLucHL.Visible && !DatetimeHelper.IsNull(dfNgayHieuLucHL.SelectedDate))
                        hsl.EffectiveDate = dfNgayHieuLucHL.SelectedDate;
                    if(txtNguoiQDHL.Visible && !string.IsNullOrEmpty(txtNguoiQDHL.Text))
                        hsl.SignerName = txtNguoiQDHL.Text;
                    var makerPosition = hdfIsMakerPositionHL.Text == @"0" ? cbxMakerPositionHL.Text : cbxMakerPositionHL.SelectedItem.Text;
                    //hsl.SignerPosition = makerPosition;
                    hsl.AttachFileName = path != "" ? path : hdfTepTinDinhKemHL.Text;
                    hsl.Note = txtGhiChuHL.Text;
                    // salary information
                    hsl.RecordId = created.RecordId;
                    hsl.QuantumId = created.QuantumId;
                    //hsl.SalaryFactor = created.SalaryFactor;
                    //hsl.SalaryBasic = created.SalaryBasic;
                    //hsl.SalaryInsurance = created.SalaryInsurance;
                    //hsl.OutFrame = created.OutFrame;
                    hsl.CreatedBy = CurrentUser.User.UserName;
                    hsl.CreatedDate = DateTime.Now;
                    SalaryDecisionController.Create(new SalaryDecisionModel(hsl));
                }

                if(e.ExtraParams["close"] == "True")
                {
                    wdTaoQuyetDinhLuongHangLoat.Hide();
                }
                else
                {
                    RM.RegisterClientScriptBlock("resetform",
                        "ResetWdTaoQuyetDinhLuongHangLoat(); Ext.net.DirectMethods.GenerateSoQDHL();");
                }

                GridPanel1.Reload();
                RM.RegisterClientScriptBlock("rlst", "Store1.reload();");
            }
            catch(Exception ex)
            {
                Dialog.ShowError(ex.Message);
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
                if(!string.IsNullOrEmpty(cbxChonCanBo.SelectedItem.Value))
                {
                    int recordId = Convert.ToInt32(cbxChonCanBo.SelectedItem.Value);
                    var hs = RecordController.GetById(recordId);
                    if(hs != null)
                    {
                        // set general infomation
                        txtChucVu.Text = hs.PositionName;
                        txtCongViec.Text = hs.JobTitleName;
                        txtDepartment.Text = hs.DepartmentName;

                        // get newest salary development
                        var hsl = SalaryDecisionController.GetCurrent(recordId);
                        if(hsl != null)
                        {
                            if(!string.IsNullOrEmpty(hsl.DecisionNumber))
                                txtSoQDCu.Text = hsl.DecisionNumber;
                            //if(hsl.DecisionName != null)
                            //    txtTenQDCu.Text = hsl.DecisionName;
                            //dfNgayQDCu.SetValue(hsl.DecisionDate);
                            ////if (hsl.HOSO != null)
                            ////    txtNguoiQDCu.Text = hsl.HOSO.HO_TEN;
                            //dfNgayCoHieuLucCu.SetValue(hsl.EffectiveDate);
                            //cbxNgachCu.SetValue(hsl.QuantumName);

                            //if(!string.IsNullOrEmpty(hsl.SalaryGrade))
                            //    txtBacLuongCu.Text = "Bậc " + hsl.SalaryGrade;
                            //if(hsl.SalaryFactor != null)
                            //    txtHeSoLuongCu.SetValue(hsl.SalaryFactor);
                            //if(hsl.SalaryBasic != null)
                            //    txtMucLuongCu.SetValue(hsl.SalaryBasic);
                            //if(hsl.SalaryInsurance != null)
                            //    txtLuongDongBHCu.SetValue(hsl.SalaryInsurance);
                            //if(hsl.OtherAllowance != null)
                            //    txtPhuCapKhacCu.SetValue(hsl.OtherAllowance);
                            //if(hsl.OutFrame != null)
                            //    txtVuotKhungCu.SetValue(hsl.OutFrame);
                            var strContractTypeId =
                                hr_ContractServices.GetFieldValueById(hsl.ContractId, "ContractTypeId");
                            if(!string.IsNullOrEmpty(strContractTypeId))
                            {
                                var contractTypeName =
                                    cat_ContractTypeServices.GetFieldValueById(Convert.ToInt32(strContractTypeId),
                                        "Name");
                                cbHopDongLoaiHopDongCu.Text = "" + contractTypeName;
                            }

                            //if(hsl.Seniority != null)
                            //    txtThamNienCu.SetValue(hsl.Seniority);
                            //if(hsl.BranchAllowance != null)
                            //    txtPCNganhCu.SetValue(hsl.BranchAllowance);
                        }
                        else
                        {
                            txtSoQDCu.Text = "";
                            txtTenQDCu.Text = "";
                            dfNgayQDCu.SetValue("");
                            txtNguoiQDCu.Text = "";
                            dfNgayCoHieuLucCu.SetValue("");
                            cbxNgachCu.Text = "";
                            txtBacLuongCu.Text = "";
                            txtHeSoLuongCu.Text = "";
                            txtMucLuongCu.Text = "";
                            txtLuongDongBHCu.Text = "";
                            txtPhuCapKhacCu.Text = "";
                            txtVuotKhungCu.Text = "";
                            cbHopDongLoaiHopDongCu.Text = "";
                        }
                    }
                    else
                    {
                        txtChucVu.Text = "";
                        txtCongViec.Text = "";
                        txtDepartment.Text = "";
                    }
                }

            }
            catch(Exception ex)
            {
                X.Msg.Alert("Thông báo từ hệ thống", "Có lỗi xảy ra khi chọn cán bộ: " + ex.Message).Show();
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
                foreach(var item in RowSelectionModel1.SelectedRows)
                {
                    // delete salary decision
                    SalaryDecisionController.Delete(int.Parse(item.RecordID));
                }

                btnEdit.Disabled = true;
                btnDelete.Disabled = true;
                GridPanel1.Reload();
            }
            catch(Exception ex)
            {
                X.MessageBox.Alert("Có lỗi xảy ra", ex.Message).Show();
            }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private DateTime GetDateTimeFromString(string dateTime)
        {
            string[] date = dateTime.Remove(dateTime.IndexOf("T")).Split('-');
            return new DateTime(int.Parse(date[0]), int.Parse(date[1]), int.Parse(date[2]));
        }

        /// <summary>
        /// Sửa thông tin quyết định lương
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEdit_Click(object sender, DirectEventArgs e)
        {
            try
            {
                var hsLuong = SalaryDecisionController.GetById(Convert.ToInt32(hdfRecordId.Text));
                if(hsLuong == null)
                {
                    X.Msg.Alert("Thông báo từ hệ thống", "Không tìm thấy quyết định lương").Show();
                    return;
                }

                // load general information for employee
                var hs = RecordController.GetById(hsLuong.RecordId);
                if(hs != null)
                {
                    hdfChonCanBo.Text = hs.Id.ToString();
                    cbxChonCanBo.Text = hs.FullName;
                    txtDepartment.Text = hs.DepartmentName;
                    txtChucVu.Text = hs.PositionName;
                    txtCongViec.Text = hs.JobTitleName;
                }

                // load newest salary decision information
                var hsl = SalaryDecisionController.GetCurrent(hsLuong.RecordId);
                if(hsl != null)
                {
                    //if(txtSoQDCu.Visible == true && !string.IsNullOrEmpty(hsl.DecisionNumber))
                    //    txtSoQDCu.Text = hsl.DecisionNumber;
                    //if(txtTenQDCu.Visible == true && !string.IsNullOrEmpty(hsl.DecisionName))
                    //    txtTenQDCu.Text = hsl.DecisionName;
                    //if(dfNgayQDCu.Visible == true)
                    //    dfNgayQDCu.SetValue(hsl.DecisionDate);
                    //if(txtNguoiQDCu.Visible == true && hsl.DecisionMaker != null)
                    //    txtNguoiQDCu.Text = hsl.DecisionMaker;
                    //if(dfNgayCoHieuLucCu.Visible == true)
                    //    dfNgayCoHieuLucCu.SetValue(hsl.EffectiveDate);
                    //if(cbxNgachCu.Visible == true)
                    //{
                    //    cbxNgachCu.SetValue(string.IsNullOrEmpty(hsl.QuantumCode) ? "" : hsl.QuantumName);
                    //}

                    //if(txtBacLuongCu.Visible == true && !string.IsNullOrEmpty(hsl.SalaryGrade))
                    //    txtBacLuongCu.Text = hsl.SalaryGrade;
                    //if(txtHeSoLuongCu.Visible == true && hsl.SalaryFactor != null)
                    //    txtHeSoLuongCu.SetValue(hsl.SalaryFactor);
                    //if(txtMucLuongCu.Visible == true && hsl.SalaryBasic != null)
                    //    txtMucLuongCu.SetValue(hsl.SalaryBasic);
                    //if(txtLuongDongBHCu.Visible == true && hsl.SalaryInsurance != null)
                    //    txtLuongDongBHCu.SetValue(hsl.SalaryInsurance);
                    //if(txtPhuCapKhacCu.Visible == true && hsl.OtherAllowance != null)
                    //    txtPhuCapKhacCu.SetValue(hsl.OtherAllowance);
                    //if(txtPCTrachNhiemCu.Visible == true && hsl.ResponsibilityAllowance != null)
                    //    txtPCTrachNhiemCu.SetValue(hsl.ResponsibilityAllowance);
                    //if(txtPCKhuVucCu.Visible == true && hsl.AreaAllowance != null)
                    //    txtPCKhuVucCu.SetValue(hsl.AreaAllowance);
                    //if(txtVuotKhungCu.Visible == true && hsl.OutFrame != null)
                    //    txtVuotKhungCu.SetValue(hsl.OutFrame);
                    var strContractTypeOldId =
                        hr_ContractServices.GetFieldValueById(hsLuong.ContractId, "ContractTypeId");
                    if(!string.IsNullOrEmpty(strContractTypeOldId))
                    {
                        var contractTypeOldName =
                            cat_ContractTypeServices.GetFieldValueById(Convert.ToInt32(strContractTypeOldId), "Name");
                        cbHopDongLoaiHopDongCu.Text = "" + contractTypeOldName;
                    }
                }

                // load current decision information
                if(txtSoQDMoi.Visible == true)
                    txtSoQDMoi.Text = hsLuong.DecisionNumber;
                if(dfNgayQDMoi.Visible == true)
                    dfNgayQDMoi.SetValue(hsLuong.DecisionDate);
                //if(txtTenQDMoi.Visible == true)
                //    txtTenQDMoi.Text = hsLuong.DecisionName;
                if(dfNgayHieuLucMoi.Visible == true)
                    dfNgayHieuLucMoi.SetValue(hsLuong.EffectiveDate);
                //if(txtNguoiQD.Visible == true)
                //    txtNguoiQD.Text = hsLuong.DecisionMaker;
                //cbxMakerPosition.Text = hsLuong.MakerPosition;
                var strContractTypeId = hr_ContractServices.GetFieldValueById(hsLuong.ContractId, "ContractTypeId");
                if(!string.IsNullOrEmpty(strContractTypeId))
                {
                    var contractTypeName =
                        cat_ContractTypeServices.GetFieldValueById(Convert.ToInt32(strContractTypeId), "Name");
                    cbHopDongLoaiHopDongMoi.Text = "" + contractTypeName;
                    hdfLoaiHopDong.SetValue(strContractTypeId);
                }

                // them ten nguoi qd
                if(composifieldAttach.Visible == true)
                {
                    hdfTepTinDinhKem.Text = hsLuong.AttachFileName;
                    if(!string.IsNullOrEmpty(hsLuong.AttachFileName))
                    {
                        hdfTepTinDinhKem.Text = hsLuong.AttachFileName;
                        fufTepTinDinhKem.Text = GetFileName(hsLuong.AttachFileName);
                    }
                }

                if(txtGhiChuMoi.Visible == true)
                    txtGhiChuMoi.Text = hsLuong.Note;
                // load current salary information
                if(cbx_ngachMoi.Visible == true)
                {
                    //if(string.IsNullOrEmpty(hsl.QuantumCode))
                    //{
                    //    hdfQuantumId.SetValue("");
                    //    cbx_ngachMoi.SetValue("");
                    //}
                    //else
                    //{
                    //    hdfQuantumId.SetValue(hsl.QuantumId);
                    //    cbx_ngachMoi.SetValue(hsl.QuantumName);
                    //}
                }

                //if(cbxBacMoi.Visible == true)
                //{
                //    hdfSalaryGrade.SetValue(hsLuong.SalaryGrade);
                //    cbxBacMoi.SetValue("Bậc " + hsLuong.SalaryGrade.ToString());
                //}

                //if(txtHeSoLuongMoi.Visible)
                //    txtHeSoLuongMoi.SetValue(hsLuong.SalaryFactor);
                //if(txtMucLuongMoi.Visible)
                //    txtMucLuongMoi.SetValue(hsLuong.SalaryBasic);
                //if(txtLuongDongBHMoi.Visible == true)
                //    txtLuongDongBHMoi.SetValue(hsLuong.SalaryInsurance);
                //if(txtBacLuongNBMoi.Visible == true)
                //    txtBacLuongNBMoi.Text = hsLuong.SalaryGradeLift;
                //if(dfNgayHLNBMoi.Visible == true && !Util.GetInstance().IsDateNull(hsLuong.SalaryGradeDate))
                //    dfNgayHLNBMoi.SetValue(hsLuong.SalaryGradeDate);
                //if(txtPhuCapKhacMoi.Visible == true)
                //    txtPhuCapKhacMoi.SetValue(hsLuong.OtherAllowance);
                //if(txtPhuCapChucVuMoi.Visible == true)
                //    txtPhuCapChucVuMoi.SetValue(hsLuong.PositionAllowance);
                //if(txtVuotKhungMoi.Visible == true)
                //    txtVuotKhungMoi.SetValue(hsLuong.OutFrame);
                //txtThamnien.SetValue(hsLuong.Seniority);
                //if(txtPhuCapTrachNhiemMoi.Visible == true)
                //    txtPhuCapTrachNhiemMoi.SetValue(hsLuong.ResponsibilityAllowance);
                //if(txtPhuCapKhuVucMoi.Visible == true)
                //    txtPhuCapKhuVucMoi.SetValue(hsLuong.AreaAllowance);
                //txtPCNganh.SetValue(hsLuong.BranchAllowance);

                // update form information
                wdTaoQuyetDinhLuong.Title = "Cập nhật thông tin quyết định lương";
                wdTaoQuyetDinhLuong.Icon = Icon.Pencil;
                btnCapNhat.Hide();
                btnCapNhatSua.Show();
                btnCapNhatDongLai.Hide();
                wdTaoQuyetDinhLuong.Show();
            }
            catch(Exception ex)
            {
                X.MessageBox.Alert("Có lỗi xảy ra: ", ex.Message).Show();
            }
        }


        #region store onrefreshdata

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cbHopDongLoaiHopDongStore_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            try
            {
                if(!string.IsNullOrEmpty(hdfChonCanBo.Text))
                {
                    var datas = ContractController.GetAllByRecordCondition(Convert.ToInt32(hdfChonCanBo.Text));
                    if(datas.Count == 0)
                    {
                        RM.RegisterClientScriptBlock("rl1",
                            "alert('Không tìm thấy hợp đồng nào còn hiệu lực. Vui lòng tạo hợp đồng mới trước khi tạo quyết định lương!');");
                    }

                    cbHopDongLoaiHopDongStore.DataSource = datas;
                    cbHopDongLoaiHopDongStore.DataBind();
                }
                else
                {
                    RM.RegisterClientScriptBlock("rl", "alert('Bạn chưa chọn cán bộ'); cbxChonCanBo.focus();");
                }
            }
            catch(Exception ex)
            {
                X.Msg.Alert("Thông báo từ hệ thống", "Có lỗi xảy ra: " + ex.Message).Show();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void cbxBacStore_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            if(string.IsNullOrEmpty(hdfQuantumId.Text)) return;
            var quantum = cat_QuantumServices.GetById(Convert.ToInt32(hdfQuantumId.Text));
            var quantumGrade = cat_GroupQuantumServices.GetFieldValueById(quantum.GroupQuantumId, "GradeMax");
            var grade = Convert.ToInt32(quantumGrade);
            hdfSalaryGrade.Text = grade.ToString();
            var objs = new List<StoreComboxObject>();
            for(var i = 1; i <= grade; i++)
            {
                var stob = new StoreComboxObject
                {
                    MA = i.ToString(),
                    TEN = "Bậc " + i
                };
                objs.Add(stob);
            }

            cbxBacStore.DataSource = objs;
            cbxBacStore.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cbxBacLuongHLStore_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            if(string.IsNullOrEmpty(hdfQuantumHLId.Text)) return;
            var quantum = cat_QuantumServices.GetById(Convert.ToInt32(hdfQuantumHLId.Text));
            var quantumGrade = cat_GroupQuantumServices.GetFieldValueById(quantum.GroupQuantumId, "GradeMax");
            var grade = Convert.ToInt32(quantumGrade);
            hdfSalaryGradeHL.Text = grade.ToString();
            var objs = new List<StoreComboxObject>();
            for(var i = 1; i <= grade; i++)
            {
                var stob = new StoreComboxObject
                {
                    MA = i.ToString(),
                    TEN = "Bậc " + i
                };
                objs.Add(stob);
            }

            cbxBacLuongHLStore.DataSource = objs;
            cbxBacLuongHLStore.DataBind();
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        [DirectMethod]
        public void GetThongTinLuong()
        {
            if(!string.IsNullOrEmpty(hdfQuantumId.Text) && !string.IsNullOrEmpty(hdfSalaryGrade.Text))
            {
                var quantum = cat_QuantumServices.GetById(Convert.ToInt32(hdfQuantumId.Text));
                var salaryFactor = CatalogGroupQuantumGradeController.GetUnique(quantum.GroupQuantumId,
                    Convert.ToInt32(hdfSalaryGrade.Text)).Factor;
                txtHeSoLuongMoi.SetValue(salaryFactor);
                var luong = Math.Round(salaryFactor * ConstSalaryBasic);
                txtMucLuongMoi.SetValue(luong);
            }
            else
            {
                txtHeSoLuongMoi.Text = "0";
                txtMucLuongMoi.Text = "0";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [DirectMethod]
        public void GetSalaryInfoHl()
        {
            if(!string.IsNullOrEmpty(hdfQuantumHLId.Text) && !string.IsNullOrEmpty(hdfSalaryGradeHL.Text))
            {
                var quantum = cat_QuantumServices.GetById(Convert.ToInt32(hdfQuantumHLId.Text));
                var salaryFactor = CatalogGroupQuantumGradeController.GetUnique(quantum.GroupQuantumId,
                    Convert.ToInt32(hdfSalaryGradeHL.Text)).Factor;
                txtHeSoLuongHL.SetValue(salaryFactor);
                var luong = Math.Round(salaryFactor * ConstSalaryBasic);
                txtMucLuongHL.SetValue(luong);
            }
            else
            {
                txtHeSoLuongHL.Text = "0";
                txtMucLuongHL.Text = "0";
            }
        }
    }
}