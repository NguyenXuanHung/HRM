using System;
using Ext.Net;
using Web.Core.Framework;
using System.Linq;
using Web.Core;
using Web.Core.Service.Catalog;
using Web.Core.Service.HumanRecord;

namespace Web.HJM.Modules.ProfileHuman.Management
{
    public partial class CPVManagement : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ExtNet.IsAjaxRequest)
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
                //SetVisibleByControlID(btnAdd, btnEdit, btnDelete);


            }

            if (btnEdit.Visible)
            {
                RowSelectionModel1.Listeners.RowSelect.Handler += "btnEdit.enable(); ";
                RowSelectionModel1.Listeners.RowDeselect.Handler += "btnEdit.disable(); ";
            }

            if (btnDelete.Visible)
            {
                RowSelectionModel1.Listeners.RowSelect.Handler += "btnDelete.enable(); ";
                RowSelectionModel1.Listeners.RowDeselect.Handler += "btnDelete.disable(); ";
            }

            ucChooseEmployee.AfterClickAcceptButton += ucChooseEmployee_AfterClickAcceptButton;
        }

        #region Event Method

        protected void InitWindowEditCpv(object sender, DirectEventArgs e)
        {
            int id;
            if (!int.TryParse(hdfRecordId.Text, out id)) return;
            var hs = hr_RecordServices.GetById(id);
            if (hs == null) return;
            cbxCPVPosition.Text = cat_CPVPositionServices.GetFieldValueById(hs.CPVPositionId, "Name");
            if (hs.CPVJoinedDate != null) dfNgayVaoD.SelectedDate = (DateTime) hs.CPVJoinedDate;
            if (hs.CPVOfficialJoinedDate != null) dfNgayCTDang.SelectedDate = (DateTime) hs.CPVOfficialJoinedDate;
            txtSoTheD.Text = hs.CPVCardNumber;
            txtNoiKetNapD.Text = hs.CPVJoinedPlace;

            wdCapNhatDangVien.Show();
        }

        protected void InsertOrUpdate(object sender, DirectEventArgs e)
        {
            if (e.ExtraParams["Command"] == "UpdateARecord")
            {
                UpdateARecord();
            }
            else
            {
                UpdateRecords(e);
            }

        }

        protected void Delete(object sender, DirectEventArgs e)
        {
            try
            {
                int id;
                if (!int.TryParse(hdfRecordId.Text, out id)) return;
                var hs = hr_RecordServices.GetById(id);
                if (hs == null) return;
                hs.CPVOfficialJoinedDate = null;
                hr_RecordServices.Update(hs);
                grp_QuanLyDangVien.Reload();
            }
            catch (Exception ex)
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình xóa: {0}".FormatWith(ex.Message));
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
                    var hs = RecordController.GetByEmployeeCode(item.RecordID);
                    // insert record to grid
                    RM.RegisterClientScriptBlock("insert" + hs.Id.ToString(),
                        string.Format("addRecord('{0}', '{1}', '{2}', '{3}');", hs.Id.ToString(), hs.EmployeeCode,
                            hs.FullName, hs.DepartmentName));
                }
            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Thông báo từ hệ thống", "Có lỗi xảy ra: " + ex.Message).Show();
            }
        }

        [DirectMethod]
        public void ResetForm()
        {
            // Update records
            grp_DanhSachCanBoStore.RemoveAll();
            hdfChucVuDang.Text = null;
            dfNgayVaoDangHangLoat.SetValue("");
            SoTheDangHangLoat.Text = string.Empty;
            dfNgayCTDangHangLoat.SetValue("");
            txtNoiKetNapDangHangLoat.Text = string.Empty;
            cbxCPVPositionAdd.SetValue("");

            //Edit a record
            cbxCPVPosition.SetValue("");
            dfNgayVaoD.SetValue("");
            dfNgayCTDang.SetValue("");
            txtSoTheD.Text = string.Empty;
            txtNoiKetNapD.Text = string.Empty;
        }

        #region Private Method

        private void UpdateRecords(DirectEventArgs e)
        {
            try
            {
                var listId = e.ExtraParams["ListId"].Split(',');
                for (var i = 0; i < listId.Length - 1; i++)
                {
                    var id = listId[i];
                    var hs = hr_RecordServices.GetById(Convert.ToInt32(id));
                    if (!SoftCore.Util.GetInstance().IsDateNull(dfNgayVaoDangHangLoat.SelectedDate) &&
                        !SoftCore.Util.GetInstance().IsDateNull(dfNgayCTDangHangLoat.SelectedDate))
                    {
                        if (dfNgayCTDangHangLoat.SelectedDate >= dfNgayVaoDangHangLoat.SelectedDate)
                        {
                            hs.CPVOfficialJoinedDate = dfNgayCTDangHangLoat.SelectedDate;
                            hs.CPVJoinedDate = dfNgayVaoDangHangLoat.SelectedDate;
                        }
                        else
                        {
                            Dialog.Alert("Ngày vào đảng phải nhỏ hơn ngày chính thức");
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }

                    hs.CPVCardNumber = SoTheDangHangLoat.Text.Trim();
                    hs.CPVJoinedPlace = txtNoiKetNapDangHangLoat.Text.Trim();
                    int cpvPositionId;
                    if (int.TryParse(cbxCPVPositionAdd.SelectedItem.Value, out cpvPositionId) && cpvPositionId > 0)
                    {
                        hs.CPVPositionId = cpvPositionId;
                    }

                    hr_RecordServices.Update(hs);

                }

                grp_QuanLyDangVien.Reload();
                wdAddNewCPV.Hide();
            }
            catch (Exception ex)
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình cập nhật: {0}".FormatWith(ex.Message));
            }

        }

        private void UpdateARecord()
        {
            try
            {
                int id;
                if (!int.TryParse(hdfRecordId.Text, out id)) return;
                var hs = hr_RecordServices.GetById(id);
                if (hs == null) return;
                int cpvPositionId;
                if (int.TryParse(cbxCPVPosition.SelectedItem.Value, out cpvPositionId))
                {
                    hs.CPVPositionId = cpvPositionId;
                }

                if (dfNgayCTDang.SelectedDate >= dfNgayVaoD.SelectedDate)
                {
                    hs.CPVOfficialJoinedDate = dfNgayCTDang.SelectedDate;
                    hs.CPVJoinedDate = dfNgayVaoD.SelectedDate;
                }
                else
                {
                    Dialog.Alert("Ngày vào đảng phải nhỏ hơn ngày chính thức");
                }

                hs.CPVCardNumber = txtSoTheD.Text.Trim();
                hs.CPVJoinedPlace = txtNoiKetNapD.Text.Trim();

                hr_RecordServices.Update(hs);
                grp_QuanLyDangVien.Reload();
            }
            catch (Exception e)
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình cập nhật: {0}".FormatWith(e.Message));
            }

        }

        #endregion

    }
}