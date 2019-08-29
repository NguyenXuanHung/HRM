using System;
using Ext.Net;
using System.Globalization;
using Web.Core.Framework;
using System.Linq;
using Web.Core;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.HumanRecord;
using SoftCore;
using Web.Core.Object;
using Web.Core.Service;
using Web.Core.Service.Catalog;

namespace Web.HJM.Modules.ProfileHuman.Management
{
    public partial class TrainingManagement : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ExtNet.IsAjaxRequest)
            {
                hdfType.Text = Request.QueryString["type"];
                if (!string.IsNullOrEmpty(hdfType.Text))
                {
                    //Đang kế hoạch đào tạo
                    hdfStatus.Text = ((int)TrainingStatus.Pending).ToString();
                }
                
                hdfDepartments.Text = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
                new Core.Framework.Common.BorderLayout()
                {
                    menuID = MenuId,
                    script = "#{hdfDepartmentSelectedId}.setValue('" + Core.Framework.Common.BorderLayout.nodeID +
                             "');StoreDaoTaoBoiDuong.reload();",
                }.AddDepartmentList(brlayout, CurrentUser, true);
            }
            
            if (btnEdit.Visible)
            {
                RowSelectionModel1.Listeners.RowSelect.Handler += "btnEdit.enable(); ";
                RowSelectionModel1.Listeners.RowDeselect.Handler += "btnEdit.disable(); ";
                grp_Training.Listeners.RowDblClick.Handler +=
                    "if(CheckSelectedRows(grp_Training)){Ext.net.DirectMethods.GetDataForDaoTao();}";
            }

            if (btnDelete.Visible)
            {
                RowSelectionModel1.Listeners.RowSelect.Handler += "btnDelete.enable(); ";
                RowSelectionModel1.Listeners.RowDeselect.Handler += "btnDelete.disable(); ";
            }

            ucChooseEmployee.AfterClickAcceptButton += ucChooseEmployee_AfterClickAcceptButton;

        }

        public void ucChooseEmployee_AfterClickAcceptButton(object sender, EventArgs e)
        {
            try
            {
                hdfTotalRecord.Text = ucChooseEmployee.SelectedRow.Count.ToString();
                foreach (var item in ucChooseEmployee.SelectedRow)
                {
                    // get employee information
                    var training = RecordController.GetByEmployeeCode(item.RecordID);
                    // insert record to grid
                    RM.RegisterClientScriptBlock("insert" + training.Id,
                        string.Format("addRecord('{0}', '{1}', '{2}', '{3}');", training.Id, training.EmployeeCode, training.FullName,
                            training.DepartmentName));
                }
            }
            catch (Exception ex)
            {
                Dialog.Alert("Thông báo từ hệ thống", "Có lỗi xảy ra: {0}".FormatWith(ex.Message));
            }
        }

        #region Method protected
        protected void btnDelete_Click(object sender, DirectEventArgs e)
        {
            foreach (var item in RowSelectionModel1.SelectedRows)
            {
                var training = hr_TrainingHistoryServices.GetById(Convert.ToInt32(item.RecordID));
                hr_TrainingHistoryServices.Delete(training.Id);
            }

            grp_Training.Reload();
            RM.RegisterClientScriptBlock("Grid_Reload", "ReloadGrid();");
        }

        protected void InsertOrUpdate(object sender, DirectEventArgs e)
        {
            if (e.ExtraParams["Command"] == @"Insert")
            {
                Insert(e);
            }
            else
            {
                Update();
            }

            RM.RegisterClientScriptBlock("Grid_Reload", "ReloadGrid();");
        }

        protected void InitWindowEdit(object sender, DirectEventArgs e)
        {
            if (int.TryParse(hdfRecordId.Text, out var id))
            {
                var training = TrainingHistoryController.GetById(id);
                if (training != null)
                {
                    txtDaoTao.Text = training.TrainingName;
                    txt_GhiChu.Text = training.Note;
                    txtLyDoDaoTao.Text = training.Reason;
                    txtNoiDaoTao.Text = training.TrainingPlace;
                    if (training.StartDate.HasValue &&
                        training.StartDate.Value.ToString(CultureInfo.InvariantCulture).Contains("0001") == false)
                    {
                        dfTuNgay.SelectedDate = training.StartDate.Value;
                    }

                    if (training.EndDate.HasValue &&
                        training.EndDate.Value.ToString(CultureInfo.InvariantCulture).Contains("0001") == false)
                    {
                        dfDenNgay.SelectedDate = training.EndDate.Value;
                    }

                    hdfQuocGiaDaoTao.Text = training.NationId.ToString();
                    cbx_QuocGiaDaoTao.Text = training.NationName;
                    txtUpdateSourceDepartment.Text = training.SourceDepartment;
                    txtUpdateDecisionMaker.Text = training.DecisionMaker;
                    txtUpdateSponsorDepartment.Text = training.SponsorDepartment;
                    cbxUpdateMakerPosition.Text = training.MakerPosition;
                    txtUpdateDocumentNumber.Text = training.DocumentNumber;
                    hdfUpdateFieldTrainingId.Text = training.FieldTrainingId.ToString();
                    cbxUpdateField.Text = cat_FieldOfTrainingServices.GetFieldValueById(training.FieldTrainingId, "Name");
                    hdfUpdateOrganizeId.Text = training.OrganizeDepartmentId.ToString();
                    cbxUpdateOrganizeDepartment.Text = cat_TrainingOrganizationServices.GetFieldValueById(training.OrganizeDepartmentId, "Name");
                    hdfUpdateTrainingStatusId.Text = training.TrainingStatusId.ToString();
                    cbx_UpdateTrainingStatus.Text = training.TrainingStatusId.Description();
                }
            }

            wdDaoTao.Show();
        }
        #endregion

        #region Method private
        private void Insert(DirectEventArgs e)
        {
            var listId = e.ExtraParams["ListId"].Split(',');
            if (listId.Length <= 1)
            {
                Dialog.Alert("Bạn chưa chọn cán bộ nào!");
                return;
            }

            for (var i = 0; i < listId.Length - 1; i++)
            {
                var util = new Util();
                var id = listId[i];
                var record = new hr_TrainingHistory
                {
                    RecordId = Convert.ToInt32(id),
                    TrainingName = txtDTTenKhoaDaoTao.Text.Trim(),
                    Reason = txtDTLyDoDaoTao.Text.Trim(),
                    TrainingPlace = txtDTNoiDaoTao.Text.Trim(),
                    Note = txtDTGhiChu.Text.Trim(),
                    Type = hdfType.Text,
                };
                if (!string.IsNullOrEmpty(hdfTrainingStatusId.Text))
                    //Kế hoạch đào tạo
                    record.TrainingStatusId = (TrainingStatus)Enum.Parse(typeof(TrainingStatus), hdfTrainingStatusId.Text);
               
                int nationId;
                if (int.TryParse(cbxDTQuocGia.SelectedItem.Value, out nationId))
                {
                    record.NationId = nationId;
                }

                if (!util.IsDateNull(dfDTTuNgay.SelectedDate))
                {
                    record.StartDate = dfDTTuNgay.SelectedDate;
                }

                if (!util.IsDateNull(dfDTDenNgay.SelectedDate))
                {
                    record.EndDate = dfDTDenNgay.SelectedDate;
                }

                record.SponsorDepartment = txtSponsorDepartment.Text;
                record.DecisionMaker = txtDecisionMaker.Text;
                record.SourceDepartment = txtSourceDepartment.Text;
                var makerPosition = string.Empty;
                if (hdfIsMakerPosition.Text == @"0")
                    makerPosition = cbxMakerPosition.Text;
                else
                    makerPosition = cbxMakerPosition.SelectedItem.Text;
                record.MakerPosition = makerPosition;
                if (!string.IsNullOrEmpty(hdfFieldTrainingId.Text))
                {
                    record.FieldTrainingId = Convert.ToInt32(hdfFieldTrainingId.Text);
                }
                if (!string.IsNullOrEmpty(hdfOrganizeDepartmentId.Text))
                {
                    record.OrganizeDepartmentId = Convert.ToInt32(hdfOrganizeDepartmentId.Text);
                }

                record.DocumentNumber = txtDocumentNumber.Text;

                hr_TrainingHistoryServices.Create(record);
            }

            grp_Training.Reload();
        }

        private void Update()
        {
            int id;
            var util = new Util();
            if (!int.TryParse(hdfRecordId.Text, out id)) return;
            var training = hr_TrainingHistoryServices.GetById(id);
            if (training == null) return;
            training.TrainingName = txtDaoTao.Text.Trim();
            training.Reason = txtLyDoDaoTao.Text.Trim();
            training.TrainingPlace = txtNoiDaoTao.Text.Trim();
            training.Note = txt_GhiChu.Text.Trim();
            int nationId;
            if (int.TryParse(cbx_QuocGiaDaoTao.SelectedItem.Value, out nationId))
            {
                training.NationId = nationId;
            }

            if (!util.IsDateNull(dfTuNgay.SelectedDate))
            {
                training.StartDate = dfTuNgay.SelectedDate;
            }

            if (!util.IsDateNull(dfDenNgay.SelectedDate))
            {
                training.EndDate = dfDenNgay.SelectedDate;
            }

            training.SponsorDepartment = txtUpdateSponsorDepartment.Text;
            training.DecisionMaker = txtUpdateDecisionMaker.Text;
            training.SourceDepartment = txtUpdateSourceDepartment.Text;
            var makerPosition = string.Empty;
            if (hdfIsUpdateMakerPosition.Text == @"0")
                makerPosition = cbxUpdateMakerPosition.Text;
            else
                makerPosition = cbxUpdateMakerPosition.SelectedItem.Text;
            training.MakerPosition = makerPosition;

            if (!string.IsNullOrEmpty(hdfUpdateFieldTrainingId.Text))
            {
                training.FieldTrainingId = Convert.ToInt32(hdfUpdateFieldTrainingId.Text);
            }
            if (!string.IsNullOrEmpty(hdfUpdateOrganizeId.Text))
            {
                training.OrganizeDepartmentId = Convert.ToInt32(hdfUpdateOrganizeId.Text);
            }

            training.DocumentNumber = txtUpdateDocumentNumber.Text;
            if (!string.IsNullOrEmpty(hdfUpdateTrainingStatusId.Text))
                //Kế hoạch đào tạo
                training.TrainingStatusId = (TrainingStatus)Enum.Parse(typeof(TrainingStatus), hdfUpdateTrainingStatusId.Text);
            if (!string.IsNullOrEmpty(hdfType.Text))
                training.Type = hdfType.Text;
            hr_TrainingHistoryServices.Update(training);
            grp_Training.Reload();
        }
        
        #endregion
        
        [DirectMethod]
        public void ResetForm()
        {
            //Update
            txtDaoTao.Text = string.Empty;
            txt_GhiChu.Text = string.Empty;
            dfTuNgay.SetValue("");
            dfDenNgay.SetValue("");
            hdfQuocGiaDaoTao.SetValue("");
            cbx_QuocGiaDaoTao.SetValue("");
            txtNoiDaoTao.Text = string.Empty;
            txtLyDoDaoTao.Text = string.Empty;
            txtUpdateDocumentNumber.Reset();
            hdfUpdateFieldTrainingId.Reset();
            hdfUpdateOrganizeId.Reset();
            cbxUpdateField.Clear();
            cbxUpdateOrganizeDepartment.Clear();
            hdfUpdateTrainingStatusId.Reset();
            cbx_UpdateTrainingStatus.Clear();

            //Insert
            grp_ListEmployeeStore.RemoveAll();
            txtDTTenKhoaDaoTao.Reset();
            dfDTTuNgay.Reset();
            cbxDTQuocGia.Reset();
            hdfDTQuocGia.Reset();
            dfDTDenNgay.Reset();
            txtDTLyDoDaoTao.Reset();
            txtDTNoiDaoTao.Reset();
            txtDTGhiChu.Reset();
            txtDocumentNumber.Reset();
            hdfFieldTrainingId.Reset();
            hdfOrganizeDepartmentId.Reset();
            cbxFieldTraining.Clear();
            cbxOrganizeDepartment.Clear();
            hdfTrainingStatusId.Reset();
            cbx_TrainingStatus.Clear();
        }

        /// <summary>
        /// trạng thái
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void store_TrainingStatus_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            store_TrainingStatus.DataSource = typeof(TrainingStatus).GetIntAndDescription();
            store_TrainingStatus.DataBind();
        }
    }
}