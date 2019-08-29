using System;
using Ext.Net;
using SmartXLS;
using Web.Core;
using Web.Core.Framework;

namespace Web.HRM.Modules.Kpi
{
    public partial class GroupKpiManagement : BasePage
    {
        private const string CriterionGroupKpiManagementUrl = @"~/Modules/Kpi/CriterionGroupKpiManagement.aspx";
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!ExtNet.IsAjaxRequest)
            {
                hdfStatus.Text = ((int)KpiStatus.Active).ToString();
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
                    wdSetting.Title = @"Cập nhật loại KPI";
                    wdSetting.Icon = Icon.Pencil;
                    var model = GroupKpiController.GetById(Convert.ToInt32(hdfId.Text));
                    if (model != null)
                    {
                        // set props
                        txtName.Text = model.Name;
                        txtDescription.Text = model.Description;
                        if (model.Status == KpiStatus.Active)
                        {
                            chkIsActive.Checked = true;
                        }
                    }

                    gpCriterion.Disabled = true;
                }
                else
                {
                    // insert
                    wdSetting.Title = @"Thêm mới loại KPI";
                    wdSetting.Icon = Icon.Add;
                    gpCriterion.Disabled = false;
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
                var model = new GroupKpiModel();

                // check id
                if (!string.IsNullOrEmpty(hdfId.Text) && Convert.ToInt32(hdfId.Text) > 0)
                {
                    var result = GroupKpiController.GetById(Convert.ToInt32(hdfId.Text)); ;
                    if (result != null)
                        model = result;
                }

                // set new props for entity
                model.Name = txtName.Text;
                model.Description = txtDescription.Text;
                if (chkIsActive.Checked)
                {
                    model.Status = KpiStatus.Active;
                }
                else
                {
                    model.Status = KpiStatus.Locked;
                }

                // check entity id
                if (model.Id > 0)
                {
                    model.EditedDate = DateTime.Now;
                    model.EditedBy = CurrentUser.User.UserName;
                    // update
                    GroupKpiController.Update(model);
                }
                else
                {
                    model.CreatedBy = CurrentUser.User.UserName;
                    model.CreatedDate = DateTime.Now;
                    model.EditedDate = DateTime.Now;
                    model.EditedBy = "";
                    // insert
                    var resultModel = GroupKpiController.Create(model);
                    if (resultModel != null)
                    {
                        //create criterionGroup
                        foreach (var itemRow in chkCriterionRowSelection.SelectedRows)
                        {
                            // init entity
                            var criterionGroupModel = new CriterionGroupModel()
                            {
                                CriterionId = Convert.ToInt32(itemRow.RecordID),
                                GroupId = resultModel.Id
                            };
                            
                            // insert
                            CriterionGroupController.Create(criterionGroupModel);
                        }
                    }

                }

                // hide window
                wdSetting.Hide();

                //reset form
                ResetForm();
                // reload data
                gpGroupKpi.Reload();
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
            txtDescription.Reset();
            chkIsActive.Checked = false;
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
                    //delete criterionGroup
                    CriterionGroupController.DeleteByCondition(Convert.ToInt32(hdfId.Text), null);
                    //delete group
                    GroupKpiController.Delete(Convert.ToInt32(hdfId.Text));
                }
                
                // reload data
                gpGroupKpi.Reload();
            }
            catch (Exception exception)
            {
                Dialog.ShowError(exception);
            }
        }
    }
}