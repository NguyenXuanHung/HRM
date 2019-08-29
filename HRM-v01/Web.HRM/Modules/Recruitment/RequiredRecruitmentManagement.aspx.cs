using System;
using System.Linq;
using Ext.Net;
using Web.Core;
using Web.Core.Framework;
using Web.Core.Helper;

namespace Web.HRM.Modules.Recruitment
{
    public partial class RequiredRecruitmentManagement : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ExtNet.IsAjaxRequest)
            {
                hdfOrder.Text = Request.QueryString["order"];
            }
        }

        /// <summary>
        /// init setting window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InitWindow(object sender, DirectEventArgs e)
        {
            try
            {
                //reset form
                ResetForm();

                // init window props
                if (e.ExtraParams["Command"] == "Update")
                {
                    // edit
                    wdSetting.Title = @"Cập nhật yêu cầu tuyển dụng";
                    wdSetting.Icon = Icon.Pencil;
                    var model = RequiredRecruitmentController.GetById(Convert.ToInt32(hdfId.Text));
                    if (model != null)
                    {
                        // set props
                        txtCode.Text = model.Code;
                        txtName.Text = model.Name;
                        dfExpiredDate.SetValue(model.ExpiredDate);
                        txtSalaryFrom.Text = model.SalaryLevelFrom != null
                            ? model.SalaryLevelFrom.Value.ToString("#,###")
                            : "";
                        txtSalaryTo.Text = model.SalaryLevelTo != null
                            ? model.SalaryLevelTo.Value.ToString("#,###")
                            : "";
                        txtAgeFrom.Text = model.AgeFrom.ToString();
                        txtAgeTo.Text = model.AgeTo.ToString();
                        txtWorkPlace.Text = model.WorkPlace;
                        txtDescription.Text = model.Description;
                        txtReason.Text = model.Reason;
                        txtSignerApproved.Text = model.SignerApprove;
                        txtRequirement.Text = model.Requirement;
                        txtWeight.Text = model.Weight.ToString();
                        txtHeight.Text = model.Height.ToString();
                        txtQuantity.Text = model.Quantity.ToString();
                        cboPosition.Text = model.PositionName;
                        hdfPosition.Text = model.PositionId.ToString();
                        cboJobTitlePosition.Text = model.JobTitlePositionName;
                        hdfJobTitlePosition.Text = model.JobTitlePositionId.ToString();
                        cboEducation.Text = model.EducationName;
                        hdfEducationId.Text = model.EducationId.ToString();
                        cboSex.Text = model.Sex.Description();
                        hdfSex.Text = ((int)model.Sex).ToString();
                        cboWorkingFormType.Text = model.WorkFormId.Description();
                        hdfWorkingFormType.Text = ((int) model.WorkFormId).ToString();
                    }
                }
                else
                {
                    // insert
                    wdSetting.Title = @"Thêm mới yêu cầu tuyển dụng";
                    wdSetting.Icon = Icon.Add;
                }
                
                // show window
                wdSetting.Show();
            }
            catch (Exception exception)
            {
                Dialog.ShowError(exception);
            }
        }

        /// <summary>
        /// insert or update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InsertOrUpdate(object sender, DirectEventArgs e)
        {
            try
            {
                // init entity
                var model = new RequiredRecruitmentModel();

                // check id
                if (!string.IsNullOrEmpty(hdfId.Text) && Convert.ToInt32(hdfId.Text) > 0)
                {
                    var result = RequiredRecruitmentController.GetById(Convert.ToInt32(hdfId.Text)); ;
                    if (result != null)
                        model = result;
                }

                // set new props for entity
                model.Name = txtName.Text;
                model.Code = txtCode.Text;
                if (!DatetimeHelper.IsNull(dfExpiredDate.SelectedDate))
                    model.ExpiredDate = dfExpiredDate.SelectedDate;
                model.Weight = !string.IsNullOrEmpty(txtWeight.Text) ? Convert.ToDecimal(txtWeight.Text.Replace(",","")) : 0;
                model.Height = !string.IsNullOrEmpty(txtHeight.Text) ? Convert.ToDecimal(txtHeight.Text.Replace(",","")) : 0;
                model.WorkPlace = txtWorkPlace.Text;
                model.Description = txtDescription.Text;
                model.Reason = txtReason.Text;
                model.Requirement = txtRequirement.Text;
                if (!string.IsNullOrEmpty(hdfWorkingFormType.Text))
                    model.WorkFormId = (WorkingFormType) Enum.Parse(typeof(WorkingFormType), hdfWorkingFormType.Text);
                if (!string.IsNullOrEmpty(hdfSex.Text))
                    model.Sex = (SexType)Enum.Parse(typeof(SexType), hdfSex.Text);
                if (!string.IsNullOrEmpty(hdfJobTitlePosition.Text))
                    model.JobTitlePositionId = Convert.ToInt32(hdfJobTitlePosition.Text);
                if (!string.IsNullOrEmpty(hdfPosition.Text))
                    model.PositionId = Convert.ToInt32(hdfPosition.Text);
                if (!string.IsNullOrEmpty(hdfEducationId.Text))
                    model.EducationId = Convert.ToInt32(hdfEducationId.Text);
                if (!string.IsNullOrEmpty(txtAgeFrom.Text))
                    model.AgeFrom = Convert.ToInt32(txtAgeFrom.Text);
                if (!string.IsNullOrEmpty(txtAgeTo.Text))
                    model.AgeTo = Convert.ToInt32(txtAgeTo.Text);
                if (!string.IsNullOrEmpty(txtSalaryFrom.Text))
                    model.SalaryLevelFrom = Convert.ToDecimal(txtSalaryFrom.Text);
                if (!string.IsNullOrEmpty(txtSalaryTo.Text))
                    model.SalaryLevelTo = Convert.ToDecimal(txtSalaryTo.Text);
                model.SignerApprove = txtSignerApproved.Text;
                if (!string.IsNullOrEmpty(txtQuantity.Text))
                    model.Quantity = Convert.ToInt32(txtQuantity.Text);
                if (!string.IsNullOrEmpty(hdfExperience.Text))
                    model.ExperienceId = (ExperienceType)Enum.Parse(typeof(ExperienceType), hdfExperience.Text);

                // check entity id
                if (model.Id > 0)
                {
                    model.EditedDate = DateTime.Now;
                    model.EditedBy = CurrentUser.User.UserName;
                    // update
                    RequiredRecruitmentController.Update(model);
                }
                else
                {
                    model.Status = RecruitmentStatus.Pending;
                    model.CreatedBy = CurrentUser.User.UserName;
                    model.CreatedDate = DateTime.Now;
                    model.EditedDate = DateTime.Now;
                    model.EditedBy = "";
                    // insert
                    RequiredRecruitmentController.Create(model);
                }

                // hide window
                wdSetting.Hide();

                //reset form
                ResetForm();
                // reload data
                gpRecruitment.Reload();
            }
            catch (Exception exception)
            {
                Dialog.ShowError(exception);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void ResetForm()
        {
            txtName.Reset();
            txtCode.Reset();
            txtAgeFrom.Reset();
            txtAgeTo.Reset();
            txtDescription.Reset();
            txtHeight.Reset();
            txtWeight.Reset();
            txtWorkPlace.Reset();
            txtQuantity.Reset();
            txtReason.Reset();
            txtRequirement.Reset();
            hdfEducationId.Reset();
            cboEducation.Reset();
            hdfSex.Reset();
            cboSex.Reset();
            hdfWorkingFormType.Reset();
            cboWorkingFormType.Reset();
            hdfExperience.Reset();
            cboExperience.Reset();
            hdfJobTitlePosition.Reset();
            cboJobTitlePosition.Reset();
            hdfPosition.Reset();
            cboPosition.Reset();
            txtSignerApproved.Reset();
            txtSalaryTo.Reset();
            txtSalaryFrom.Reset();
            dfExpiredDate.Reset();
        }

        /// <summary>
        /// Delete 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Delete(object sender, DirectEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(hdfId.Text))
                {
                    //delete
                    RequiredRecruitmentController.Delete(Convert.ToInt32(hdfId.Text));
                }

                // reload data
                gpRecruitment.Reload();
            }
            catch (Exception exception)
            {
                Dialog.ShowError(exception);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void storeWorkingFormType_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            storeWorkingFormType.DataSource = typeof(WorkingFormType).GetIntAndDescription();
            storeWorkingFormType.DataBind();
        }

        protected void storeSexType_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            storeSex.DataSource = typeof(SexType).GetIntAndDescription();
            storeSex.DataBind();
        }

        protected void storeExperience_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            storeExperience.DataSource = typeof(ExperienceType).GetIntAndDescription();
            storeExperience.DataBind();
        }

        protected void storeStatusFilter_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            storeStatusFilter.DataSource = typeof(RecruitmentStatus).GetIntAndDescription();
            storeStatusFilter.DataBind();
        }

        protected void InitWdConfig(object sender, DirectEventArgs e)
        {
            try
            {
                var eventAction = e.ExtraParams["Event"];
                if (!string.IsNullOrEmpty(eventAction))
                {
                    var id = @"0";
                    if (!string.IsNullOrEmpty(hdfIds.Text))
                    {
                        var ids = hdfIds.Text.Split(',').ToList();
                        id = ids.First();
                    }

                    var model = RequiredRecruitmentController.GetById(Convert.ToInt32(id));
                    if (model != null)
                    {
                        model.EditedDate = DateTime.Now;
                        model.EditedBy = CurrentUser.User.UserName;
                    }
                    else
                    {
                        return;
                    }

                    switch (eventAction)
                    {
                        case "Approved":
                            model.Status = RecruitmentStatus.Approved;
                            //update
                            RequiredRecruitmentController.Update(model);
                            break;
                        case "UnApproved":
                            model.Status = RecruitmentStatus.UnApproved;
                            //update
                            RequiredRecruitmentController.Update(model);
                            break;
                        case "Complete":
                            model.Status = RecruitmentStatus.Complete;
                            //update
                            RequiredRecruitmentController.Update(model);
                            break;
                        case "Cancel":
                            model.Status = RecruitmentStatus.Cancel;
                            //update
                            RequiredRecruitmentController.Update(model);
                            break;
                        case "Request":
                            model.Status = RecruitmentStatus.Pending;
                            //update
                            RequiredRecruitmentController.Update(model);
                            break;
                    }
                    //reload 
                    gpRecruitment.Reload();
                }
            }
            catch (Exception exception)
            {
                Dialog.Alert(exception.Message);
            }
        }
    }
}