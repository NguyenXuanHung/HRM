using System;
using Ext.Net;
using Web.Core.Framework;
using System.Linq;
using Web.Core;
using Web.Core.Helper;

namespace Web.HRM.Modules.ProfileHuman.Management
{
    public partial class TrainingManagement : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!ExtNet.IsAjaxRequest)
            {
                hdfDepartments.Text = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
                new Core.Framework.Common.BorderLayout()
                {
                    menuID = MenuId,
                    script = "#{hdfDepartmentSelectedId}.setValue('" + Core.Framework.Common.BorderLayout.nodeID +
                             "');storeTrainingHistory.reload();",
                }.AddDepartmentList(brlayout, CurrentUser, false);
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
                if(!string.IsNullOrEmpty(hdfRecordId.Text))
                    TrainingHistoryController.Delete(Convert.ToInt32(hdfRecordId.Text));
                gpTrainingHistory.Reload();
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
        protected void InsertOrUpdate(object sender, DirectEventArgs e)
        {
            if(e.ExtraParams["Command"] == @"Insert")
            {
                Insert(e);
            }
            else
            {
                Update();
            }
            gpTrainingHistory.Reload();
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
                    var model = new TrainingHistoryModel()
                    {
                        RecordId = Convert.ToInt32(itemRow.RecordID),
                        TrainingName = txtDTTenKhoaDaoTao.Text.Trim(),
                        Reason = txtDTLyDoDaoTao.Text.Trim(),
                        TrainingPlace = txtDTNoiDaoTao.Text.Trim(),
                        Note = txtDTGhiChu.Text.Trim(),
                        SponsorDepartment = txtSponsorDepartment.Text,
                        DecisionMaker = txtDecisionMaker.Text,
                        SourceDepartment = txtSourceDepartment.Text,
                        MakerPosition = hdfIsMakerPosition.Text == @"0" ? cbxMakerPosition.Text : cbxMakerPosition.SelectedItem.Text,
                        CreatedBy = CurrentUser.User.UserName,
                        CreatedDate = DateTime.Now,
                        EditedDate = DateTime.Now,
                        EditedBy = CurrentUser.User.UserName
                    };

                    if (!string.IsNullOrEmpty(hdfNationIdMany.Text))
                    {
                        model.NationId = Convert.ToInt32(hdfNationIdMany.Text);
                    }

                    if (!DatetimeHelper.IsNull(dfDTTuNgay.SelectedDate))
                    {
                        model.StartDate = dfDTTuNgay.SelectedDate;
                    }

                    if (!DatetimeHelper.IsNull(dfDTDenNgay.SelectedDate))
                    {
                        model.EndDate = dfDTDenNgay.SelectedDate;
                    }

                    //create
                    TrainingHistoryController.Create(model);
                }

                wdTraining.Hide();
                gpTrainingHistory.Reload();
            }
            catch(Exception ex)
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình thêm mới: {0}".FormatWith(ex.Message));
            }

        }

        /// <summary>
        /// 
        /// </summary>
        private void Update()
        {
            try
            {
                if(!int.TryParse(hdfRecordId.Text, out var id)) return;
                var model = TrainingHistoryController.GetById(id);
                if (model != null)
                {
                    model.TrainingName = txtDaoTao.Text.Trim();
                    model.Reason = txtLyDoDaoTao.Text.Trim();
                    model.TrainingPlace = txtNoiDaoTao.Text.Trim();
                    model.Note = txt_GhiChu.Text.Trim();
                    if (!string.IsNullOrEmpty(hdfNationId.Text))
                    {
                        model.NationId = Convert.ToInt32(hdfNationId.Text);
                    }

                    if (!DatetimeHelper.IsNull(dfTuNgay.SelectedDate))
                    {
                        model.StartDate = dfTuNgay.SelectedDate;
                    }

                    if (!DatetimeHelper.IsNull(dfDenNgay.SelectedDate))
                    {
                        model.EndDate = dfDenNgay.SelectedDate;
                    }

                    model.SponsorDepartment = txtUpdateSponsorDepartment.Text;
                    model.DecisionMaker = txtUpdateDecisionMaker.Text;
                    model.SourceDepartment = txtUpdateSourceDepartment.Text;
                    model.MakerPosition = hdfIsUpdateMakerPosition.Text == @"0" ? cbxUpdateMakerPosition.Text : cbxUpdateMakerPosition.SelectedItem.Text;
                    model.EditedDate = DateTime.Now;
                    model.EditedBy = CurrentUser.User.UserName;

                    //update
                    TrainingHistoryController.Update(model);
                }
               
                wdDaoTao.Hide();
                gpTrainingHistory.Reload();
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
        protected void InitWindowEdit(object sender, DirectEventArgs e)
        {
            if(int.TryParse(hdfRecordId.Text, out var id))
            {
                var model = TrainingHistoryController.GetById(id);
                if(model != null)
                {
                    txtDaoTao.Text = model.TrainingName;
                    txt_GhiChu.Text = model.Note;
                    txtLyDoDaoTao.Text = model.Reason;
                    txtNoiDaoTao.Text = model.TrainingPlace;
                    dfTuNgay.SetValue(model.StartDate);
                    dfDenNgay.SetValue(model.EndDate);
                    hdfNationId.Text = model.NationId.ToString();
                    cboNation.Text = model.NationName;
                    txtUpdateSourceDepartment.Text = model.SourceDepartment;
                    txtUpdateDecisionMaker.Text = model.DecisionMaker;
                    txtUpdateSponsorDepartment.Text = model.SponsorDepartment;
                    cbxUpdateMakerPosition.Text = model.MakerPosition;
                }
            }

            wdDaoTao.Show();
        }

        /// <summary>
        /// 
        /// </summary>
        [DirectMethod]
        public void ResetForm()
        {
            //Update
            txtDaoTao.Text = string.Empty;
            txt_GhiChu.Text = string.Empty;
            dfTuNgay.SetValue("");
            dfDenNgay.SetValue("");
            hdfNationId.Reset();
            cboNation.Reset();
            txtNoiDaoTao.Text = string.Empty;
            txtLyDoDaoTao.Text = string.Empty;

            //Insert
            txtDTTenKhoaDaoTao.Reset();
            dfDTTuNgay.Reset();
            hdfNationIdMany.Reset();
            cboNationMany.Reset();
            dfDTDenNgay.Reset();
            txtDTLyDoDaoTao.Reset();
            txtDTNoiDaoTao.Reset();
            txtDTGhiChu.Reset();
        }
    }
}