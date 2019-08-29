using System;
using Ext.Net;
using Web.Core.Framework;
using Web.Core;
using Web.Core.Object.HumanRecord;
using System.Linq;
using SoftCore;
using Web.Core.Service.HumanRecord;
using WebUI.Entity;

namespace Web.HJM.Modules.ProfileHuman.Management
{
    public partial class AnnualLeaveConfigureManagement : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ExtNet.IsAjaxRequest)
            {
                hdfDepartments.Text = string.Join(",", CurrentUser.Departments.Select(d => d.Id));

                new Core.Framework.Common.BorderLayout()
                {
                    menuID = MenuId,
                    script = "#{hdfDepartmentSelectedId}.setValue('" + Core.Framework.Common.BorderLayout.nodeID +
                             "');#{txtSearch}.reset();#{PagingToolbar1}.pageIndex = 0; #{PagingToolbar1}.doLoad();"
                }.AddDepartmentList(brlayout, CurrentUser, true);

            }

            if (btnEdit.Visible)
            {
                gridAnnualLeave.Listeners.RowDblClick.Handler +=
                    " if(CheckSelectedRows(gridAnnualLeave)){btnUpdate.show();btnUpdateClose.hide()}";
            }

            ucChooseEmployee.AfterClickAcceptButton += new EventHandler(ucChooseEmployee_AfterClickAcceptButton);
        }

        #region Event Method

        public void ucChooseEmployee_AfterClickAcceptButton(object sender, EventArgs e)
        {
            try
            {
                hdfTotalRecord.Text = ucChooseEmployee.SelectedRow.Count.ToString();
                foreach (var item in ucChooseEmployee.SelectedRow)
                {
                    // get employee information
                    var hs = RecordController.GetByEmployeeCode(item.RecordID);
                    var recordId = hs.Id.ToString();
                    var employeeCode = hs.EmployeeCode;
                    var fullName = hs.FullName;
                    var departmentName = hs.DepartmentName;
                    // insert record to grid
                    RM.RegisterClientScriptBlock("insert" + recordId,
                        string.Format("addRecord('{0}', '{1}', '{2}', '{3}');", recordId, employeeCode, fullName,
                            departmentName));
                }
            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Thông báo từ hệ thống", "Có lỗi xảy ra: " + ex.Message).Show();
            }
        }

        protected void InsertOrUpdate(object sender, DirectEventArgs e)
        {
            try
            {
                if (e.ExtraParams["Command"] == "Update")
                    Update();
                else
                    Insert(e);
                //reload data
                gridAnnualLeave.Reload();
            }
            catch (Exception ex)
            {
                ExtNet.MessageBox.Alert("Có lỗi xảy ra", ex.Message).Show();
            }
        }

        private void Insert(DirectEventArgs e)
        {
            try
            {
                //check validation choose employee
                if (string.IsNullOrEmpty(e.ExtraParams["ListRecordId"]))
                {
                    ExtNet.Msg.Alert("Thông báo", "Bạn hãy chọn ít nhất 1 cán bộ").Show();
                    return;
                }

                var listId = e.ExtraParams["ListRecordId"].Split(',');
                for (var i = 0; i < listId.Length - 1; i++)
                {
                    var recordId = listId[i];
                    var annual = new hr_AnnualLeaveConfigure();
                    EditDataAnnualLeave(annual);
                    annual.RecordId = Convert.ToInt32(recordId);
                    annual.CreatedDate = DateTime.Now;
                    hr_AnnualLeaveConfigureServices.Create(annual);
                }

                if (e.ExtraParams["Close"] == "True")
                {
                    wdAnnualLeave.Hide();
                    ResetForm();
                }
            }
            catch (Exception ex)
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình thêm mới: {0}".FormatWith(ex.Message));
            }
        }

        /// <summary>
        /// edit data before save DB
        /// </summary>
        /// <param name="annual"></param>
        private void EditDataAnnualLeave(hr_AnnualLeaveConfigure annual)
        {
            var util = new Util();
            if (!string.IsNullOrEmpty(txtAnnualLeaveDay.Text))
                annual.AnnualLeaveDay = Convert.ToDouble(txtAnnualLeaveDay.Text);
            if (!string.IsNullOrEmpty(txtDayAddedStep.Text))
                annual.DayAddedStep = Convert.ToDouble(txtDayAddedStep.Text);
            if (!string.IsNullOrEmpty(txtYearStep.Text))
                annual.YearStep = Convert.ToInt32(txtYearStep.Text);
            if (!string.IsNullOrEmpty(txtMaximumPerMonth.Text))
                annual.MaximumPerMonth = Convert.ToDouble(txtMaximumPerMonth.Text);
            annual.AllowUseFirstYear = chk_AllowUseFirstYear.Checked;
            annual.AllowUsePreviousYear = chk_AllowUsePreviousYear.Checked;
            if (!util.IsDateNull(dfExpiredDate.SelectedDate))
                annual.ExpiredDate = dfExpiredDate.SelectedDate;
            if (!string.IsNullOrEmpty(txtUsedLeaveDay.Text))
                annual.UsedLeaveDay = Convert.ToDouble(txtUsedLeaveDay.Text);
            if (!string.IsNullOrEmpty(txtRemainLeaveDay.Text))
                annual.RemainLeaveDay = Convert.ToDouble(txtRemainLeaveDay.Text);
        }

        private void Update()
        {
            try
            {
                if (!string.IsNullOrEmpty(hdfKeyRecord.Text))
                {
                    var annual = hr_AnnualLeaveConfigureServices.GetById(Convert.ToInt32(hdfKeyRecord.Text));
                    if (annual != null)
                    {
                        EditDataAnnualLeave(annual);
                        annual.EditedDate = DateTime.Now;
                    }

                    hr_AnnualLeaveConfigureServices.Update(annual);
                }
            }
            catch (Exception ex)
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình cập nhật: {0}".FormatWith(ex.Message));
            }
        }

        [DirectMethod]
        public void ResetForm()
        {
            txtAnnualLeaveDay.Text = "";
            txtDayAddedStep.Reset();
            txtMaximumPerMonth.Reset();
            txtYearStep.Reset();
            txtDayAddedStep.Reset();
            chk_AllowUseFirstYear.Checked = false;
            chk_AllowUsePreviousYear.Checked = false;
            dfExpiredDate.Reset();
            txtUsedLeaveDay.Text = "";
            txtRemainLeaveDay.Text = "";
        }

        protected void Delete(object sender, DirectEventArgs directEventArgs)
        {
            try
            {
                int id;
                if (!int.TryParse(hdfKeyRecord.Text, out id) || id <= 0) return;
                foreach (var item in RowSelectionModel1.SelectedRows)
                {
                    hr_AnnualLeaveConfigureServices.Delete(id);
                }

                gridAnnualLeave.Reload();
                RM.RegisterClientScriptBlock("Grid_Reload", "ReloadGrid();");
            }
            catch (Exception ex)
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình xóa: {0}".FormatWith(ex.Message));
            }
        }

        protected void EditAnnualLeave_Click(object sender, DirectEventArgs e)
        {
            int id;
            if (!int.TryParse(hdfKeyRecord.Text, out id) || id <= 0) return;
            var annual = hr_AnnualLeaveConfigureServices.GetById(id);
            if (annual != null)
            {
                txtAnnualLeaveDay.Text = annual.AnnualLeaveDay.ToString();
                txtDayAddedStep.Text = annual.DayAddedStep.ToString();
                txtMaximumPerMonth.Text = annual.MaximumPerMonth.ToString();
                txtYearStep.Text = annual.YearStep.ToString();
                chk_AllowUseFirstYear.Checked = annual.AllowUseFirstYear;
                chk_AllowUsePreviousYear.Checked = annual.AllowUsePreviousYear;
                txtUsedLeaveDay.Text = annual.UsedLeaveDay.ToString();
                txtRemainLeaveDay.Text = annual.RemainLeaveDay.ToString();
                dfExpiredDate.SetValue(annual.ExpiredDate);
                txtFullName.Text = hr_RecordServices.GetFieldValueById(annual.RecordId, "FullName");
            }

            // show window
            btnUpdate.Show();
            btnUpdateClose.Hide();
            btnUpdateNew.Hide();
            txtFullName.Hidden = false;
            grp_ListEmployee.Hide();
            wdAnnualLeave.Title = @"Cập nhật cấu hình ngày phép";
            wdAnnualLeave.Show();
        }

        #endregion
    }
}