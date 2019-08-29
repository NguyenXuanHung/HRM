using System;
using Ext.Net;
using Web.Core.Framework;
using System.Linq;
using Web.Core;
using Web.Core.Framework.Controller;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.HumanRecord;

namespace Web.HJM.Modules.ProfileHuman.Management
{
    public partial class FluctuationEmployeeManagement : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            hdfDepartments.Text = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
            new Core.Framework.Common.BorderLayout()
            {
                menuID = MenuId,
                script = "#{hdfDepartmentSelectedId}.setValue('" + Core.Framework.Common.BorderLayout.nodeID +
                         "');#{txtSearch}.reset();#{PagingToolbar1}.pageIndex = 0; #{PagingToolbar1}.doLoad();"
            }.AddDepartmentList(brlayout, CurrentUser, true);
            //if (btnEdit.Visible)
            //{
            //    grp_FluctuationEmployee.Listeners.RowDblClick.Handler += " if(CheckSelectedRows(grp_QuanLyNangNgach)){Ext.net.DirectMethods.GetDataForCanBo();}";
            //}
        }

        #region Event Method

        protected void cbxChonCanBo_Selected(object sender, DirectEventArgs e)
        {
            try
            {
                var id = int.Parse(cbxChonCanBo.SelectedItem.Value);
                var hs = RecordController.GetById(id);
                var hsTeam = TeamController.GetByRecordId(id);
                if (hs == null) return;
                txtEmployeeCode.Text = hs.EmployeeCode;
                txtBirthDate.Text = hs.BirthDateVn;
                txtDepartment.Text = hs.DepartmentName;
                txtPosition.Text = hs.PositionName;
                txtJobTitle.Text = hs.JobTitleName;
                txtIDNumber.Text = hs.IDNumber;
                txtIDIssueDate.Text = hs.IDIssueDate.ToString();
                txtTeam.Text = hsTeam.TeamName;
                txtConstruction.Text = hsTeam.ConstructionName;
            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Thông báo từ hệ thống", "Có lỗi xảy ra khi chọn cán bộ: " + ex.Message).Show();
            }
        }

        protected void InsertOrUpdate(object sender, DirectEventArgs e)
        {
            if (hdfCommandName.Text != @"Update")
            {
                Insert();
            }
            else
            {
                Update();
            }

            if (e.ExtraParams["Close"] == "True")
            {
                wdCreateFluctuation.Hide();
            }

            //reload data
            grp_FluctuationEmployee.Reload();
            RM.RegisterClientScriptBlock("Grid_Reload", "ReloadGrid();");
        }

        protected void Delete(object sender, DirectEventArgs directEventArgs)
        {
            try
            {
                int id;
                if (!int.TryParse(hdfKeyRecord.Text, out id) || id <= 0) return;
                hr_FluctuationEmployeeServices.Delete(id);
                grp_FluctuationEmployee.Reload();
                RM.RegisterClientScriptBlock("Grid_Reload", "ReloadGrid();");
            }
            catch (Exception ex)
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình xóa: {0}".FormatWith(ex.Message));
            }

        }

        #endregion

        [DirectMethod]
        public void ResetForm()
        {
            cbxChonCanBo.Reset();
            txtDepartment.Reset();
            txtDate.Reset();
            txtReason.Reset();
            txtBirthDate.Reset();
            txtConstruction.Reset();
            txtEmployeeCode.Reset();
            txtIDIssueDate.Reset();
            txtIDNumber.Reset();
            txtJobTitle.Reset();
            txtPosition.Reset();
            txtTeam.Reset();
            rgType.Reset();
        }

        #region Private Method

        private void Insert()
        {
            try
            {
                hdfChonCanBo.Text = cbxChonCanBo.SelectedItem.Value;
                var fluctuation = new hr_FluctuationEmployee
                {
                    RecordId = int.Parse(hdfChonCanBo.Text),
                    Type = rbDecrease.Checked,
                    Reason = txtReason.Text.Trim(),
                    Date = txtDate.SelectedDate,
                    CreatedDate = DateTime.Now,
                    EditedDate = DateTime.Now
                };
                hr_FluctuationEmployeeServices.Create(fluctuation);
            }
            catch (Exception e)
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình thêm mới: {0}".FormatWith(e.Message));
            }
        }


        private void Update()
        {
            try
            {
                int id;
                if (!int.TryParse(hdfKeyRecord.Text, out id) || id <= 0) return;
                var recordId = Convert.ToInt32(hdfChonCanBo.Text);
                var record = hr_FluctuationEmployeeServices.GetById(id);
                record.RecordId = recordId;
                record.Reason = txtReason.Text;
                record.Date = txtDate.SelectedDate;
                record.Type = rbDecrease.Checked;
                record.EditedDate = DateTime.Now;
                hr_FluctuationEmployeeServices.Update(record);
            }
            catch (Exception e)
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình cập nhật: {0}".FormatWith(e.Message));

            }

        }

        #endregion

        protected void InitWindowFluctuation(object sender, DirectEventArgs e)
        {
            int id;
            if (!int.TryParse(hdfKeyRecord.Text, out id) || id <= 0) return;
            var recordId = int.Parse(hdfRecordId.Text);
            var hs = RecordController.GetById(recordId);
            var hsTeam = TeamController.GetByRecordId(recordId);
            if (hs != null)
            {
                cbxChonCanBo.SelectedItem.Text = hs.FullName;
                txtEmployeeCode.Text = hs.EmployeeCode;
                txtDepartment.Text = hs.DepartmentName;
                txtBirthDate.Text = hs.BirthDateVn;
                txtPosition.Text = hs.PositionName;
                txtJobTitle.Text = hs.JobTitleName;
                txtIDNumber.Text = hs.IDNumber;
                if (hs.IDIssueDate != null) txtIDIssueDate.SelectedDate = (DateTime) hs.IDIssueDate;
            }

            if (hsTeam == null) return;
            txtTeam.Text = hsTeam.TeamName;
            txtConstruction.Text = hsTeam.ConstructionName;
            var fluc = hr_FluctuationEmployeeServices.GetById(Convert.ToInt32(hdfKeyRecord.Text));
            txtReason.Text = fluc.Reason;
            txtDate.SelectedDate = fluc.Date;
            if (fluc.Type == false)
                rbIncrease.Checked = true;
            else
            {
                rbDecrease.Checked = true;
            }

            wdCreateFluctuation.Title = @"Cập nhật quyết định tăng giảm nhân sự";
            wdCreateFluctuation.Show();
            hdfCommandName.Text = @"Update";
            hdfChonCanBo.Text = hsTeam.RecordId.ToString();
        }
    }
}