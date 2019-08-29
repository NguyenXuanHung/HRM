using System;
using Ext.Net;
using Web.Core.Framework;
using Web.Core;
using Web.Core.Framework.Utils;

namespace Web.HRM.Modules.ProfileHuman.Management
{
    public partial class AnnualLeaveConfigureManagement : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ExtNet.IsAjaxRequest)
            {
                hdfDepartments.Text = DepartmentIds;

                new Core.Framework.Common.BorderLayout()
                {
                    menuID = MenuId,
                    script = "#{hdfDepartmentSelectedId}.setValue('" + Core.Framework.Common.BorderLayout.nodeID +
                             "');#{txtSearch}.reset();#{PagingToolbar1}.pageIndex = 0; #{PagingToolbar1}.doLoad();"
                }.AddDepartmentList(brlayout, CurrentUser, false);

            }
        }

        #region Event Method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                wdAnnualLeave.Hide();
            }
            catch (Exception ex)
            {
                Dialog.Alert("Có lỗi xảy ra", ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        private void Insert(DirectEventArgs e)
        {
            try
            {
                foreach (var item in chkEmployeeRowSelection.SelectedRows)
                {
                    var recordId = item.RecordID;
                    var annual = new AnnualLeaveConfigureModel();
                    EditDataAnnualLeave(annual);
                    annual.RecordId = Convert.ToInt32(recordId);
                    annual.CreatedDate = DateTime.Now;
                    annual.CreatedBy = CurrentUser.User.UserName;
                    annual.EditedDate = DateTime.Now;
                    annual.EditedBy = CurrentUser.User.UserName;
                    //create
                    AnnualLeaveConfigureController.Create(annual);
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
        /// Edit data before save DB
        /// </summary>
        /// <param name="annual"></param>
        private void EditDataAnnualLeave(AnnualLeaveConfigureModel annual)
        {
            var util = new ConvertUtils();
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

        /// <summary>
        /// 
        /// </summary>
        private void Update()
        {
            try
            {
                if (string.IsNullOrEmpty(hdfKeyRecord.Text)) return;
                var annual = AnnualLeaveConfigureController.GetById(Convert.ToInt32(hdfKeyRecord.Text));
                if (annual != null)
                {
                    EditDataAnnualLeave(annual);
                    annual.EditedDate = DateTime.Now;
                    annual.EditedBy = CurrentUser.User.UserName;
                }
                //update
                AnnualLeaveConfigureController.Update(annual);
            }
            catch (Exception ex)
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình cập nhật: {0}".FormatWith(ex.Message));
            }
        }

        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="directEventArgs"></param>
        protected void Delete(object sender, DirectEventArgs directEventArgs)
        {
            try
            {
                if (!string.IsNullOrEmpty(hdfKeyRecord.Text))
                    AnnualLeaveConfigureController.Delete(Convert.ToInt32(hdfKeyRecord.Text));

                gridAnnualLeave.Reload();
            }
            catch (Exception ex)
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình xóa: {0}".FormatWith(ex.Message));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void EditAnnualLeave_Click(object sender, DirectEventArgs e)
        {
            if (!int.TryParse(hdfKeyRecord.Text, out var id) || id <= 0) return;
            var annual = AnnualLeaveConfigureController.GetById(id);
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
                txtFullName.Text = annual.FullName;
            }

            // show window
            btnUpdate.Show();
            btnUpdateClose.Hide();
            txtFullName.Hidden = false;
            grp_ListEmployee.Hide();
            ctnEmployee.Hide();
            wdAnnualLeave.Title = @"Cập nhật cấu hình ngày phép";
            wdAnnualLeave.Show();
        }

        #endregion
    }
}