using System;
using Ext.Net;
using Web.Core.Framework;
using Web.Core;
using Web.Core.Object.HumanRecord;
using System.Linq;
using Web.Core.Service;
using Web.Core.Service.Catalog;
using Web.Core.Service.HumanRecord;

namespace Web.HRM.Modules.ProfileHuman.Management
{
    public partial class PlanJobTitleManagement : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ExtNet.IsAjaxRequest)
            {
                hdfBusinessType.Text = Request.QueryString["businessType"];
                hdfTypePlanPhase.Text = @"PlanPhase";
                hdfTypePlanJobTitle.Text = @"PlanJobTitle";
                hdfDepartments.Text = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
                if (btnEdit.Visible)
                {
                    gridPlanJob.Listeners.RowDblClick.Handler +=
                        " if(CheckSelectedRows(gridPlanJob)){btnUpdate.show();btnUpdateNew.hide();}";
                }
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
            if (e.ExtraParams["Command"] == "Update")
            {
                Update();
                wdPlanJob.Hide();
            }
            else
            {
                Insert();
            }

            //reload data
            gridPlanJob.Reload();
           
        }

        /// <summary>
        /// 
        /// </summary>
        private void Insert()
        {
            var planJob = new hr_BusinessHistory()
            {
                BusinessType = hdfBusinessType.Text,
                CreatedBy = CurrentUser.User.UserName,
                CreatedDate = DateTime.Now
            };
            //Edit data
            EditData(planJob);

            //Create
            hr_BusinessHistoryServices.Create(planJob);
            //Reset
            ResetForm();
        }

        /// <summary>
        /// edit data
        /// </summary>
        /// <param name="planJob"></param>
        private void EditData(hr_BusinessHistory planJob)
        {
            if (!string.IsNullOrEmpty(hdfEmployeeSelectedId.Text))
            {
                planJob.RecordId = Convert.ToInt32(hdfEmployeeSelectedId.Text);
            }
            if (!string.IsNullOrEmpty(hdfPlanJobTitleId.Text))
            {
                planJob.PlanJobTitleId = Convert.ToInt32(hdfPlanJobTitleId.Text);
            }
            if (!string.IsNullOrEmpty(hdfPlanPhaseId.Text))
            {
                planJob.PlanPhaseId = Convert.ToInt32(hdfPlanPhaseId.Text);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void Update()
        {
            if (!string.IsNullOrEmpty(hdfKeyRecord.Text))
            {
                var planJob = hr_BusinessHistoryServices.GetById(Convert.ToInt32(hdfKeyRecord.Text));
                if (planJob != null)
                {
                    planJob.EditedDate = DateTime.Now;
                }
                //Edit data
                EditData(planJob);

                hr_BusinessHistoryServices.Update(planJob);
            }
        }

        [DirectMethod]
        public void ResetForm()
        {
            hdfPlanJobTitleId.Reset();
            hdfPlanPhaseId.Reset();
            cbx_PlanJobTitle.Clear();
            cbx_PlanPhase.Clear();
            hdfEmployeeSelectedId.Reset();
            cbxSelectedEmployee.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="directEventArgs"></param>
        protected void Delete(object sender, DirectEventArgs directEventArgs)
        {
            foreach (var item in RowSelectionModel1.SelectedRows)
            {
                hr_BusinessHistoryServices.Delete(int.Parse("0" + item.RecordID));
            }
             
            gridPlanJob.Reload();
            RM.RegisterClientScriptBlock("Grid_Reload", "ReloadGrid();");
           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void EditPlanJob_Click(object sender, DirectEventArgs e)
        {
            if (!string.IsNullOrEmpty(hdfKeyRecord.Text))
            {
               var planJob =  BusinessHistoryController.GetById(Convert.ToInt32(hdfKeyRecord.Text));
                if (planJob != null)
                {
                    cbxSelectedEmployee.Text = hr_RecordServices.GetFieldValueById(planJob.RecordId, "FullName");
                    hdfEmployeeSelectedId.Text = planJob.RecordId.ToString();
                    hdfPlanJobTitleId.Text = planJob.PlanJobTitleId.ToString();
                    hdfPlanPhaseId.Text = planJob.PlanPhaseId.ToString();
                    cbx_PlanPhase.Text = cat_PlanPhaseServices.GetFieldValueById(planJob.PlanPhaseId, "Name");
                    cbx_PlanJobTitle.Text = cat_PlanJobTitleServices.GetFieldValueById(planJob.PlanJobTitleId, "Name");
                }
            }
            
            // show window
            btnUpdate.Show();
            btnUpdateNew.Hide();
            cbxSelectedEmployee.Disabled = true;
            wdPlanJob.Title = @"Cập nhật thông tin quy hoạch chức danh";
            wdPlanJob.Show();
        }

        #endregion
    }
}