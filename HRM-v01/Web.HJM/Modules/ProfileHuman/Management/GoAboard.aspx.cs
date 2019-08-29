using System;
using Ext.Net;
using Web.Core.Framework;
using System.Linq;
using Web.Core;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.HumanRecord;
using SoftCore;

namespace Web.HJM.Modules.ProfileHuman.Management
{
    public partial class GoAboard : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ExtNet.IsAjaxRequest)
            {
                //hdfMaDonVi.Text = Session["MaDonVi"].ToString();
                hdfMenuID.SetValue(MenuId);
                hdfUserID.SetValue(CurrentUser.User.Id);
                hdfDepartments.Text = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
                new Core.Framework.Common.BorderLayout()
                {
                    menuID = MenuId,
                    script = "#{hdfDepartmentSelectedId}.setValue('" + Core.Framework.Common.BorderLayout.nodeID +
                             "');#{txtSearch}.reset();#{PagingToolbar1}.pageIndex = 0; #{PagingToolbar1}.doLoad();"
                }.AddDepartmentList(br, CurrentUser, true);
                // SetVisibleByControlID(btnAdd, btnEdit, btnDelete);
            }

            if (btnEdit.Visible)
            {
                RowSelectionModel1.Listeners.RowSelect.Handler += " btnEdit.enable();";
                RowSelectionModel1.Listeners.RowDeselect.Handler += " btnEdit.disable();";
                grp_HoSoDiNuocNgoai.Listeners.RowDblClick.Handler +=
                    " if(CheckSelectedRows(grp_HoSoDiNuocNgoai)){btnInserted.show();}";
            }

            if (btnDelete.Visible)
            {
                RowSelectionModel1.Listeners.RowSelect.Handler += " btnDelete.enable();";
                RowSelectionModel1.Listeners.RowDeselect.Handler += " btnDelete.disable();";
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
                    // insert record to grid
                    RM.RegisterClientScriptBlock("insert" + hs.Id,
                        string.Format("addRecord('{0}', '{1}', '{2}', '{3}');", hs.Id, hs.EmployeeCode, hs.FullName,
                            hs.DepartmentName));
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowError("" + ex.Message);
            }
        }

        protected void InsertOrUpdate(object sender, DirectEventArgs e)
        {
            if (hdfCommandName.Text == @"Update")
            {
                Update();
            }
            else
            {
                Insert(e);
            }

            RM.RegisterClientScriptBlock("Grid_Reload", "ReloadGrid();");
        }

        private void Insert(DirectEventArgs e)
        {
            try
            {
                var listId = e.ExtraParams["ListId"].Split(',');
                if (listId.Length <= 1)
                {
                    Dialog.Alert("Bạn chưa chọn cán bộ nào!");
                    return;
                }

                for (var i = 0; i < listId.Length - 1; i++)
                {
                    var id = listId[i];
                    var util = new Util();
                    var controler = new GoAboardController();
                    var hs = new hr_GoAboard
                    {
                        RecordId = int.Parse(id),
                        Reason = txtLyDo.Text.Trim(),
                        Note = txtGhiChu.Text.Trim()
                    };
                    if (!string.IsNullOrEmpty(hdfNationId.Text))
                    {
                        hs.NationId = Convert.ToInt32(hdfNationId.Text);
                    }

                    if (!util.IsDateNull(dfNgayBatDau.SelectedDate))
                    {
                        hs.StartDate = dfNgayBatDau.SelectedDate;
                    }

                    if (!util.IsDateNull(dfNgayKetThuc.SelectedDate))
                    {
                        hs.EndDate = dfNgayKetThuc.SelectedDate;
                    }

                    hs.DecisionNumber = txtDecisionNumber.Text;
                    if (!util.IsDateNull(dfDecisionDate.SelectedDate))
                        hs.DecisionDate = dfDecisionDate.SelectedDate;
                    hs.SponsorDepartment = txtSponsorDepartment.Text;
                    hs.SourceDepartment = txtSourceDepartment.Text;
                    hs.DecisionMaker = txtDecisionMaker.Text;
                    var makerPosition = string.Empty;
                    if (hdfIsMakerPosition.Text == @"0")
                        makerPosition = cbxMakerPosition.Text;
                    else
                        makerPosition = cbxMakerPosition.SelectedItem.Text;
                    hs.MakerPosition = makerPosition;
                    controler.Insert(hs);
                }

                grp_HoSoDiNuocNgoai.Reload();
                ResetForm();
            }
            catch (Exception ex)
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình tạo: {0}".FormatWith(ex.Message));
            }

        }

        private void Update()
        {
            try
            {
                int id;
                var util = new Util();
                var controler = new GoAboardController();
                if (!int.TryParse(hdfRecordId.Text, out id)) return;
                var hs = hr_GoAboardServices.GetById(id);
                if (!util.IsDateNull(dfNgayBatDau.SelectedDate))
                {
                    hs.StartDate = dfNgayBatDau.SelectedDate;
                }

                if (!util.IsDateNull(dfNgayKetThuc.SelectedDate))
                {
                    hs.EndDate = dfNgayKetThuc.SelectedDate;
                }

                hs.Note = txtGhiChu.Text;
                hs.Reason = txtLyDo.Text;
                if (!string.IsNullOrEmpty(hdfNationId.Text))
                {
                    hs.NationId = Convert.ToInt32(hdfNationId.Text);
                }

                hs.DecisionNumber = txtDecisionNumber.Text;
                if (!util.IsDateNull(dfDecisionDate.SelectedDate))
                    hs.DecisionDate = dfDecisionDate.SelectedDate;
                hs.SponsorDepartment = txtSponsorDepartment.Text;
                hs.SourceDepartment = txtSourceDepartment.Text;
                hs.DecisionMaker = txtDecisionMaker.Text;
                var makerPosition = string.Empty;
                if (hdfIsMakerPosition.Text == @"0")
                    makerPosition = cbxMakerPosition.Text;
                else
                    makerPosition = cbxMakerPosition.SelectedItem.Text;
                hs.MakerPosition = makerPosition;
                hs.EditedDate = DateTime.Now;
                controler.Update(hs);
                grp_HoSoDiNuocNgoai.Reload();
                RM.RegisterClientScriptBlock("Grid_Reload", "ReloadGrid();");
            }
            catch (Exception e)
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình cập nhật: {0}".FormatWith(e.Message));
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
                    var hs = hr_GoAboardServices.GetById(id);
                    if (hs != null)
                    {
                        hr_GoAboardServices.Delete(hs.Id);
                    }
                }

                grp_HoSoDiNuocNgoai.Reload();
            }
            catch (Exception ex)
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình xóa: {0}".FormatWith(ex.Message));
            }

        }

        protected void InitWindowEdit(object sender, DirectEventArgs e)
        {
            int id;
            if (int.TryParse(hdfRecordId.Text, out id))
            {
                var hs = GoAboardController.GetById(id);
                if (hs != null)
                {
                    cbx_quocgia.Text = hs.NationName;
                    if (hs.StartDate != null) dfNgayBatDau.SelectedDate = (DateTime) hs.StartDate;
                    if (hs.EndDate != null) dfNgayKetThuc.SelectedDate = (DateTime) hs.EndDate;
                    txtLyDo.Text = hs.Reason;
                    txtGhiChu.Text = hs.Note;
                    hdfNationId.Text = hs.NationId.ToString();
                    cbx_quocgia.Text = hs.NationName;
                    txtSponsorDepartment.Text = hs.SponsorDepartment;
                    txtSourceDepartment.Text = hs.SourceDepartment;
                    txtDecisionMaker.Text = hs.DecisionMaker;
                    cbxMakerPosition.Text = hs.MakerPosition;
                }
            }

            hdfCommandName.Text = @"Update";
            ctn23.Hide();
            wdDiNuocNgoai.Show();
        }

        [DirectMethod]
        public void ResetForm()
        {
            hdfNationId.Reset();
            cbx_quocgia.SetValue("");
            dfNgayBatDau.Reset();
            dfNgayKetThuc.Reset();
            txtDecisionMaker.Reset();
            txtSponsorDepartment.Reset();
            txtSourceDepartment.Reset();
            cbxMakerPosition.Clear();
            txtLyDo.Text = string.Empty;
            txtGhiChu.Text = string.Empty;
            ctn23.Show();
            grp_DanhSachCanBoStore.RemoveAll();
        }
    }
}