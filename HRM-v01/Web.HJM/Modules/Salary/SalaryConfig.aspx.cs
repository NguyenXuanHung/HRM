using System;
using System.Collections.Generic;
using Ext.Net;
using Web.Core.Framework;
using Web.Core;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.HumanRecord;
using Web.Core.Object.Catalog;
using Web.Core.Object.Salary;
using Web.Core.Service.Catalog;
using Web.Core.Service.Salary;

namespace Web.HJM.Modules.Salary
{
    public partial class SalaryConfig : BasePage
    {
        /// <summary>
        /// Page load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!ExtNet.IsAjaxRequest)
            {
                hdfConfigId.Text = Request.QueryString["id"];
                //init 
                InitController();
            }

            if (btnEdit.Visible)
            {
                RowSelectionModel1.Listeners.RowSelect.Handler += "btnEdit.enable();";
                RowSelectionModel1.Listeners.RowDeselect.Handler += "btnEdit.disable(); ";
                gridSalaryConfig.DirectEvents.RowDblClick.Event += btnEdit_Click;
            }

            if (btnDelete.Visible)
            {
                RowSelectionModel1.Listeners.RowSelect.Handler += "btnDelete.enable();";
                RowSelectionModel1.Listeners.RowDeselect.Handler += "btnDelete.disable(); ";
            }
        }

        private void InitController()
        {
            var type = typeof(hr_SalaryBoardInfo);
            var props = type.GetProperties();
            var lstColumnCode = new List<SalaryBoardInfoColumnCodeModel>();
            var index = 0;
            foreach (var item in props)
            {
                if (item.Name != "Id" && item.Name != "SalaryBoardId"
                                      && item.Name != "RecordId"
                                      && item.Name != "CreatedDate"
                                      && item.Name != "EditedDate")
                {
                    index++;
                    var columnModel = new SalaryBoardInfoColumnCodeModel()
                    {
                        Id = index,
                        Name = item.Name
                    };
                    lstColumnCode.Add(columnModel);
                }
            }

            cbxChoseFieldFixedStore.DataSource = lstColumnCode;
            cbxChoseFieldFixedStore.DataBind();

            if (string.IsNullOrEmpty(hdfConfigId.Text))
            {
                btnAdd.Disabled = true;
                btnEdit.Disabled = true;
                btnDelete.Disabled = true;
                wdConfigList.Show();
            }
            else
            {
                var payroll = sal_PayrollConfigServices.GetById(Convert.ToInt32(hdfConfigId.Text));
                if(payroll != null)
                    gridSalaryConfig.Title = payroll.Name;
            }
        }

        #region event
        protected void btnBack_Click(object sender, DirectEventArgs e)
        {
            Response.Redirect("~/Modules/Salary/PayrollConfig.aspx");
        }

        protected void btnEdit_Click(object sender, DirectEventArgs e)
        {
            if (string.IsNullOrEmpty(hdfId.Text))
            {
                ExtNet.Msg.Alert("Thông báo", "Bạn chưa chọn bản ghi nào").Show();
            }
            else
            {
                var config = hr_SalaryBoardConfigServices.GetById(Convert.ToInt32(hdfId.Text));
                if (config != null)
                {
                    txtColumnExcel.Text = config.ColumnExcel;
                    txtDisplay.Text = config.Display;
                    txtFormula.Text = config.Formula;
                    txtDescription.Text = config.Description;
                    chk_IsInUsed.Checked = config.IsInUsed;
                    chk_IsReadOnly.Checked = config.IsReadOnly;
                    cbxDataType.SetValue((int)config.DataType);
                    txtOrder.SetValue(config.Order);
                    hdfConfigId.Text = config.ConfigId.ToString();
                    if ((int)config.DataType == (int)SalaryConfigDataType.FieldValue)
                    {
                        cbxChoseFieldFixed.Show();
                        txtColumnCode.Hide();
                        hdfChoseFieldFixed.Text = config.ColumnCode;
                        cbxChoseFieldFixed.Text = config.ColumnCode;
                    }
                    else
                    {
                        cbxChoseFieldFixed.Hide();
                        txtColumnCode.Show();
                        txtColumnCode.Text = config.ColumnCode;
                    }
                }

                wdSalaryBoardConfig.Title = @"Cập nhật cấu hình bảng lương";
                btUpdateNew.Hide();
                btnUpdate.Show();
                wdSalaryBoardConfig.Show();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdate_Click(object sender, DirectEventArgs e)
        {
            // check command name
            if (e.ExtraParams["Command"] == "Update")
            {
                // update
                Update();
            }
            else
            {
                // insert
                Insert();
            }
            //reload data
            gridSalaryConfig.Reload();
        }

        /// <summary>
        /// delete boardConfig
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, DirectEventArgs e)
        {
            foreach (var item in RowSelectionModel1.SelectedRows)
            {
                //delete
                hr_SalaryBoardConfigServices.DeleteByCondition(int.Parse("0" + item.RecordID));
            }
            // reload data
            gridSalaryConfig.Reload();
        }

        /// <summary>
        /// Chọn bảng cấu hình
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAcceptConfig_Click(object sender, EventArgs e)
        {
            Response.Redirect("SalaryConfig.aspx?id=" + hdfConfigId.Text);
        }

        protected void btnDeletePayrollConfig_Click(object sender, DirectEventArgs e)
        {
            if (!string.IsNullOrEmpty(hdfConfigId.Text))
            {
                sal_PayrollConfigServices.Delete(Convert.ToInt32(hdfConfigId.Text));
                gridConfigList.Reload();
            }
        }

        [DirectMethod]
        public void btnEditPayrollConfig_Click()
        {
            if (!string.IsNullOrEmpty(hdfConfigId.Text))
            {
                var payrollConfig = sal_PayrollConfigServices.GetById(Convert.ToInt32(hdfConfigId.Text));
                if (payrollConfig != null)
                {
                    txtPayrollConfigName.Text = payrollConfig.Name;
                    txtPayrollConfigDescription.Text = payrollConfig.Description;
                }
            }
            btnCreatePayrollConfig.Hide();
            btnUpdatePayrollConfig.Show();
            wdCreatePayrollConfig.Show();
        }

        [DirectMethod]
        public void btnAddPayrollConfig_Click()
        {
            btnCreatePayrollConfig.Show();
            btnUpdatePayrollConfig.Hide();
            wdCreatePayrollConfig.Show();
        }

        protected void CreatePayrollConfig_Click(object sender, DirectEventArgs e)
        {
            var payrollConfig = new sal_PayrollConfig()
            {
                Name = txtPayrollConfigName.Text,
                Description = txtPayrollConfigDescription.Text,
                CreatedDate = DateTime.Now
            };
            sal_PayrollConfigServices.Create(payrollConfig);
            ResetFormPayrollConfig();
            wdCreatePayrollConfig.Hide();
            gridConfigList.Reload();
            Dialog.Alert("Thêm thành công");
        }

        protected void UpdatePayrollConfig_Click(object sender, DirectEventArgs e)
        {
            if (!string.IsNullOrEmpty(hdfConfigId.Text))
            {
                var payrollConfig = sal_PayrollConfigServices.GetById(int.Parse(hdfConfigId.Text));
                if (payrollConfig != null)
                {
                    if (!string.IsNullOrEmpty(txtPayrollConfigName.Text))
                        payrollConfig.Name = txtPayrollConfigName.Text;
                    if (!string.IsNullOrEmpty(txtPayrollConfigDescription.Text))
                        payrollConfig.Description = txtPayrollConfigDescription.Text;
                    payrollConfig.EditedDate = DateTime.Now;
                    sal_PayrollConfigServices.Update(payrollConfig);
                    ResetFormPayrollConfig();
                    wdCreatePayrollConfig.Hide();
                    gridConfigList.Reload();
                    Dialog.Alert("Cập nhật thành công");
                }
            }

        }
        #endregion

        #region Private Methods

        private void Insert()
        {
            var config = new hr_SalaryBoardConfig()
            {
                CreatedDate = DateTime.Now
            };
            //Edit data
            EditData(config);
            //Create
            hr_SalaryBoardConfigServices.Create(config);
            ResetForm();
            gridSalaryConfig.Reload();
            wdSalaryBoardConfig.Hide();
        }

        private void Update()
        {
            if (!string.IsNullOrEmpty(hdfId.Text))
            {
                // get config id
                var config = hr_SalaryBoardConfigServices.GetById(Convert.ToInt32(hdfId.Text));
                if (config != null)
                {
                    // edit config
                    EditData(config);
                }

                hr_SalaryBoardConfigServices.Update(config);
                ResetForm();
                gridSalaryConfig.Reload();
                wdSalaryBoardConfig.Hide();
            }
        }

        [DirectMethod]
        public void ResetForm()
        {
            txtColumnCode.Reset();
            txtColumnExcel.Reset();
            txtDisplay.Reset();
            chk_IsInUsed.Checked = false;
            chk_IsReadOnly.Checked = false;
            txtFormula.Reset();
            cbxDataType.Reset();
            txtDescription.Reset();
            hdfChoseFieldFixed.Reset();
            cbxChoseFieldFixed.Reset();
            txtColumnCode.Show();
            cbxChoseFieldFixed.Hide();
            txtOrder.Reset();
        }

        [DirectMethod]
        public void ResetFormPayrollConfig()
        {
            txtPayrollConfigDescription.Reset();
            txtPayrollConfigName.Reset();
        }

        private void EditData(hr_SalaryBoardConfig config)
        {

            config.ColumnCode = txtColumnCode.Text;
            config.ColumnExcel = txtColumnExcel.Text;
            config.Description = txtDescription.Text;
            config.Display = txtDisplay.Text;
            config.Formula = txtFormula.Text;
            config.IsInUsed = chk_IsInUsed.Checked;
            config.IsReadOnly = chk_IsReadOnly.Checked;
            config.Order = !string.IsNullOrEmpty(txtOrder.Text) ? Convert.ToInt32(txtOrder.Text) : 0;
            if (!string.IsNullOrEmpty(hdfConfigId.Text))
            {
                config.ConfigId = Convert.ToInt32(hdfConfigId.Text);
            }
            if (!string.IsNullOrEmpty(cbxDataType.SelectedItem.Value))
            {
                switch (Convert.ToInt32(cbxDataType.SelectedItem.Value))
                {
                    case (int)SalaryConfigDataType.Formula:
                        config.DataType = SalaryConfigDataType.Formula;
                        break;
                    case (int)SalaryConfigDataType.FieldValue:
                        config.DataType = SalaryConfigDataType.FieldValue;
                        config.ColumnCode = hdfChoseFieldFixed.Text;
                        break;
                    case (int)SalaryConfigDataType.DynamicValue:
                        config.DataType = SalaryConfigDataType.DynamicValue;
                        break;
                    case (int)SalaryConfigDataType.FixedValue:
                        config.DataType = SalaryConfigDataType.FixedValue;
                        break;
                    default:
                        config.DataType = 0;
                        break;
                }

            }
        }

        #endregion
    }
}
