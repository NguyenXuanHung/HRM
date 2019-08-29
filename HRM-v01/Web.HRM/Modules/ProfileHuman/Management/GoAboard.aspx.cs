using System;
using Ext.Net;
using Web.Core.Framework;
using Web.Core;
using Web.Core.Helper;

namespace Web.HRM.Modules.ProfileHuman.Management
{
    public partial class GoAboard : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!ExtNet.IsAjaxRequest)
            {
                hdfDepartments.Text = DepartmentIds;
                new Core.Framework.Common.BorderLayout()
                {
                    menuID = MenuId,
                    script = "#{hdfDepartmentSelectedId}.setValue('" + Core.Framework.Common.BorderLayout.nodeID +
                             "');#{txtSearch}.reset();#{PagingToolbar1}.pageIndex = 0; #{PagingToolbar1}.doLoad();"
                }.AddDepartmentList(br, CurrentUser, false);
               
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InsertOrUpdate(object sender, DirectEventArgs e)
        {
            if(hdfCommandName.Text == @"Update")
            {
                Update();
            }
            else
            {
                Insert(e);
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
                foreach (var itemRow in chkEmployeeRowSelection.SelectedRows)
                {
                    var model = new GoAboardModel()
                    {
                        RecordId = Convert.ToInt32(itemRow.RecordID),
                        CreatedBy = CurrentUser.User.UserName,
                        CreatedDate = DateTime.Now,
                        EditedDate = DateTime.Now,
                        EditedBy = CurrentUser.User.UserName,
                    };

                    //edit data
                    EditDataSave(model);

                    //create
                    GoAboardController.Create(model);
                }
               
                gpGoAboard.Reload();
                ResetForm();
                wdGoAboard.Hide();
            }
            catch(Exception ex)
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình tạo: {0}".FormatWith(ex.Message));
            }
        }

        /// <summary>
        /// edit data
        /// </summary>
        /// <param name="model"></param>
        private void EditDataSave(GoAboardModel model)
        {
            model.Reason = txtReason.Text;
            model.Note = txtNote.Text;
            model.SponsorDepartment = txtSponsorDepartment.Text;
            model.SourceDepartment = txtSourceDepartment.Text;
            model.DecisionNumber = txtDecisionNumber.Text;
            model.DecisionMaker = txtDecisionMaker.Text;
            model.MakerPosition = hdfIsMakerPosition.Text == @"0" ? cbxMakerPosition.Text : cbxMakerPosition.SelectedItem.Text;

            if (!string.IsNullOrEmpty(hdfNationId.Text))
            {
                model.NationId = Convert.ToInt32(hdfNationId.Text);
            }

            if (!DatetimeHelper.IsNull(dfFromDate.SelectedDate))
            {
                model.StartDate = dfFromDate.SelectedDate;
            }

            if (!DatetimeHelper.IsNull(dfToDate.SelectedDate))
            {
                model.EndDate = dfToDate.SelectedDate;
            }

            if (!DatetimeHelper.IsNull(dfDecisionDate.SelectedDate))
                model.DecisionDate = dfDecisionDate.SelectedDate;
        }

        /// <summary>
        /// 
        /// </summary>
        private void Update()
        {
            try
            {
                if(!int.TryParse(hdfRecordId.Text, out var id)) return;
                var model = GoAboardController.GetById(id);
                if(model == null) return;
                //edit data
                EditDataSave(model);
                model.EditedDate = DateTime.Now;
                model.EditedBy = CurrentUser.User.UserName;

                //update
                GoAboardController.Update(model);
                gpGoAboard.Reload();
                wdGoAboard.Hide();
            }
            catch(Exception e)
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình cập nhật: {0}".FormatWith(e.Message));
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
                if(!string.IsNullOrEmpty(hdfRecordId.Text))
                    GoAboardController.Delete(Convert.ToInt32(hdfRecordId.Text));
                gpGoAboard.Reload();
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
        protected void InitWindowEdit(object sender, DirectEventArgs e)
        {
            if(int.TryParse(hdfRecordId.Text, out var id))
            {
                var model = GoAboardController.GetById(id);
                if(model != null)
                {
                    dfFromDate.SetValue(model.StartDate);
                    dfToDate.SetValue(model.EndDate);
                    txtReason.Text = model.Reason;
                    txtNote.Text = model.Note;
                    hdfNationId.Text = model.NationId.ToString();
                    cboNation.Text = model.NationName;
                    txtSponsorDepartment.Text = model.SponsorDepartment;
                    txtSourceDepartment.Text = model.SourceDepartment;
                    txtDecisionMaker.Text = model.DecisionMaker;
                    cbxMakerPosition.Text = model.MakerPosition;
                    txtDecisionNumber.Text = model.DecisionNumber;
                    dfDecisionDate.SetValue(model.DecisionDate);
                }
            }

            ctnEmployee.Hide();
            hdfCommandName.Text = @"Update";
            wdGoAboard.Show();
        }

        [DirectMethod]
        public void ResetForm()
        {
            hdfNationId.Reset();
            cboNation.Reset();
            dfFromDate.Reset();
            dfToDate.Reset();
            txtDecisionMaker.Reset();
            txtSponsorDepartment.Reset();
            txtSourceDepartment.Reset();
            cbxMakerPosition.Clear();
            txtReason.Reset();
            txtNote.Reset();
            ctnEmployee.Show();
            txtDecisionNumber.Reset();
            dfDecisionDate.Reset();
        }
    }
}