using System;
using Ext.Net;
using Web.Core.Framework;

namespace Web.HRM.Modules.Recruitment
{
    public partial class CandidateInterviewManagement : BasePage
    {
        private const string InterviewManagementUrl = "~/Modules/Recruitment/InterviewManagement.aspx";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ExtNet.IsAjaxRequest)
            {
                hdfOrder.Text = Request.QueryString["order"];
                hdfDepartmentIds.Text = DepartmentIds;
               
                if (!string.IsNullOrEmpty(Request.QueryString["interviewId"]))
                {
                    var interviewModel =
                        InterviewController.GetById(Convert.ToInt32(Request.QueryString["interviewId"]));
                    if (interviewModel != null)
                    {
                        gpInterview.Title = @"Chi tiết lịch phỏng vấn: " + interviewModel.Name;
                        hdfInterviewId.Text = interviewModel.Id.ToString();
                    }
                }
            }
        }

        #region Back to interview
        protected void CandidateBack(object sender, DirectEventArgs e)
        {
            Response.Redirect(InterviewManagementUrl, true);
        }
        
        #endregion

        protected void Delete(object sender, DirectEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(hdfId.Text))
                {
                    CandidateInterviewController.Delete(Convert.ToInt32(hdfId.Text));
                    //reload grid
                    gpInterview.Reload();
                    wdSetting.Hide();
                }
            }
            catch (Exception exception)
            {
                Dialog.ShowError(exception);
            }
        }

        protected void BtnSaveClick(object sender, DirectEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(hdfId.Text))
                {
                    var model = CandidateInterviewController.GetById(Convert.ToInt32(hdfId.Text));
                    if (model != null)
                    {
                        var time = new DateTime(model.TimeInterview.Year, model.TimeInterview.Month, model.TimeInterview.Day, tfInterview.SelectedTime.Hours, tfInterview.SelectedTime.Minutes, 0);

                        model.TimeInterview = time;
                    }
                    //update
                    CandidateInterviewController.Update(model);
                    //reload grid
                    gpInterview.Reload();
                    wdSetting.Hide();
                }
            }
            catch (Exception exception)
            {
                Dialog.ShowError(exception);
            }
        }

        protected void EditClick(object sender, DirectEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(hdfId.Text))
                {
                    var model = CandidateInterviewController.GetById(Convert.ToInt32(hdfId.Text));
                    if (model != null)
                    {
                        tfInterview.SetValue(model.TimeInterviewDisplay);
                        txtFullName.Text = model.FullName;

                        //show window
                        wdSetting.Show();
                    }
                }
            }
            catch (Exception exception)
            {
                Dialog.ShowError(exception);
            }   
        }
    }
}