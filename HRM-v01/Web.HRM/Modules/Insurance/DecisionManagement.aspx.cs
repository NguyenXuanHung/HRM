using System;
using Ext.Net;
using Web.Core;
using Web.Core.Framework;
using Web.Core.Framework.Controller;
using Web.Core.Framework.Model;
using Web.Core.Framework.Utils;

namespace Web.HRM.Modules.Insurance
{
    public partial class DecisionManagement : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ExtNet.IsAjaxRequest)
            {
                //init
                hdfDepartmentIds.Text = DepartmentIds;
                var type = Request.QueryString["type"];
                hdfType.Text = Request.QueryString["type"];
                if (!string.IsNullOrEmpty(type) && type == DecisionType.Leave.ToString())
                    hdfType.Text = ((int)DecisionType.Leave).ToString();
                if (!string.IsNullOrEmpty(type) && type == DecisionType.Maternity.ToString())
                    hdfType.Text = ((int)DecisionType.Maternity).ToString();
                if (!string.IsNullOrEmpty(type) && type == DecisionType.Retire.ToString())
                    hdfType.Text = ((int)DecisionType.Retire).ToString();
                if (!string.IsNullOrEmpty(type) && type == DecisionType.OffMode.ToString())
                    hdfType.Text = ((int)DecisionType.OffMode).ToString();

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
                    wdSetting.Title = @"Cập nhật quyết định";
                    wdSetting.Icon = Icon.Pencil;
                    var model = DecisionController.GetById(Convert.ToInt32(hdfId.Text));
                    if (model != null)
                    {
                        // set props
                        txtName.Text = model.Name;
                        cboEmployee.Text = model.FullName;
                        hdfChooseEmployee.Text = model.RecordId.ToString();
                        dfDecisionDate.SetValue(model.DecisionDate);
                        hdfReason.Text = model.ReasonId.ToString();
                        cboReason.Text = model.ReasonName;
                        cboEmployee.Disabled = true;
                    }
                }
                else
                {
                    // insert
                    wdSetting.Title = @"Thêm mới quyết định";
                    wdSetting.Icon = Icon.Add;
                    cboEmployee.Disabled = false;
                    if (!string.IsNullOrEmpty(hdfType.Text) && hdfType.Text == ((int)DecisionType.Leave).ToString())
                        txtName.Text = @"Quyết định {0}".FormatWith(DecisionType.Leave.Description());
                    if (!string.IsNullOrEmpty(hdfType.Text) && hdfType.Text == ((int)DecisionType.Maternity).ToString())
                        txtName.Text = @"Quyết định {0}".FormatWith(DecisionType.Maternity.Description());
                    if (!string.IsNullOrEmpty(hdfType.Text) && hdfType.Text == ((int)DecisionType.Retire).ToString())
                        txtName.Text = @"Quyết định {0}".FormatWith(DecisionType.Retire.Description());
                    if (!string.IsNullOrEmpty(hdfType.Text) && hdfType.Text == ((int)DecisionType.OffMode).ToString())
                        txtName.Text = @"Quyết định {0}".FormatWith(DecisionType.OffMode.Description());

                    dfDecisionDate.SetValue(DateTime.Now);
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
                var model = new DecisionModel();

                // check id
                if (!string.IsNullOrEmpty(hdfId.Text) && Convert.ToInt32(hdfId.Text) > 0)
                {
                    var result = DecisionController.GetById(Convert.ToInt32(hdfId.Text)); ;
                    if (result != null)
                        model = result;
                }

                // set new props for entity
                model.Name = txtName.Text;
                if (!string.IsNullOrEmpty(hdfType.Text))
                    model.Type = (DecisionType)Enum.Parse(typeof(DecisionType), hdfType.Text);
                var util = new ConvertUtils();
                if (!util.IsDateNull(dfDecisionDate.SelectedDate))
                    model.DecisionDate = dfDecisionDate.SelectedDate;
                if (!string.IsNullOrEmpty(hdfReason.Text))
                    model.ReasonId = Convert.ToInt32(hdfReason.Text);
                if (!string.IsNullOrEmpty(hdfChooseEmployee.Text))
                    model.RecordId = Convert.ToInt32(hdfChooseEmployee.Text);
                // check entity id
                if (model.Id > 0)
                {
                    model.EditedDate = DateTime.Now;
                    model.EditedBy = CurrentUser.User.UserName;
                    // update
                    DecisionController.Update(model);
                }
                else
                {
                    model.CreatedBy = CurrentUser.User.UserName;
                    model.CreatedDate = DateTime.Now;
                    model.EditedDate = DateTime.Now;
                    model.EditedBy = "";

                    // insert
                    DecisionController.Create(model);

                    //create decrease insurance
                    if (!string.IsNullOrEmpty(hdfType.Text) &&
                        (hdfType.Text == ((int) DecisionType.Leave).ToString() ||
                         hdfType.Text == ((int) DecisionType.Maternity).ToString()))
                    {
                        var insuranceModel = new FluctuationInsuranceModel()
                        {
                            RecordId = model.RecordId,
                            Type = InsuranceType.Decrease,
                            EffectiveDate = model.DecisionDate,
                            ReasonId = model.ReasonId,
                            CreatedBy = CurrentUser.User.UserName,
                            CreatedDate = DateTime.Now,
                            EditedDate = DateTime.Now,
                            EditedBy = ""
                        };

                        //create
                        FluctuationInsuranceController.Create(insuranceModel);
                    }
                }


                // hide window
                wdSetting.Hide();

                //reset form
                ResetForm();
                // reload data
                gpDecision.Reload();
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
            cboEmployee.Reset();
            hdfChooseEmployee.Reset();
            dfDecisionDate.Reset();
            cboReason.Reset();
            hdfReason.Reset();
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
                    var decision = DecisionController.GetById(Convert.ToInt32(hdfId.Text));
                    if (decision != null)
                    {
                        //delete decrease insurance
                        var insurance = FluctuationInsuranceController.GetByRecordId(decision.RecordId, decision.DecisionDate.Month, decision.DecisionDate.Year);
                        if (insurance != null)
                        {
                            FluctuationInsuranceController.Delete(insurance.Id);
                        }

                    }
                    
                    //delete 
                    DecisionController.Delete(Convert.ToInt32(hdfId.Text));
                }

                // reload data
                gpDecision.Reload();
            }
            catch (Exception exception)
            {
                Dialog.ShowError(exception);
            }
        }

    }
}