using System;
using System.Collections.Generic;
using System.Linq;
using Ext.Net;
using Web.Core.Framework;
using Web.Core;
using Web.Core.Framework.Common;
using Web.Core.Framework.Controller;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.HumanRecord;
using Web.Core.Object.Catalog;
using Web.Core.Service.Salary;

namespace Web.HRM.Modules.Salary
{
    public partial class SalaryConfig : BasePage
    {
        private const string PayrollConfigUrl  = @"~/Modules/Salary/PayrollConfig.aspx";
        private const string SalaryConfigUrl = @"SalaryConfig.aspx";
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
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitController()
        {
            var type = typeof(hr_SalaryBoardInfo);
            var props = type.GetProperties();
            var lstColumnCode = new List<SalaryBoardInfoColumnCodeModel>();
            var lstColumnCodeFormula = new List<SalaryBoardInfoColumnCodeModel>();
            var index = 0;

            //from field formula
            //add column TotalIncome
            lstColumnCodeFormula.Add(new SalaryBoardInfoColumnCodeModel()
            {
                Id = 0,
                Name = Constant.TotalIncome
            });
            //add column IndividualTax
            lstColumnCodeFormula.Add(new SalaryBoardInfoColumnCodeModel()
            {
                Id = 1,
                Name = Constant.IndividualTax
            });
            //add column IndividualTax
            lstColumnCodeFormula.Add(new SalaryBoardInfoColumnCodeModel()
            {
                Id = 2,
                Name = Constant.EnterpriseSocialInsurance
            });
            //add column LaborerSocialInsurance
            lstColumnCodeFormula.Add(new SalaryBoardInfoColumnCodeModel()
            {
                Id = 3,
                Name = Constant.LaborerSocialInsurance
            });
            //add column ActualSalary
            lstColumnCodeFormula.Add(new SalaryBoardInfoColumnCodeModel()
            {
                Id = 4,
                Name = Constant.ActualSalary
            });

            cbxChoseFieldFormulaStore.DataSource = lstColumnCodeFormula;
            cbxChoseFieldFormulaStore.DataBind();

            //from field value
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
                //wdConfigList.Show();
            }
            else
            {
                var payroll = sal_PayrollConfigServices.GetById(Convert.ToInt32(hdfConfigId.Text));
                if(payroll != null)
                    gridSalaryConfig.Title = payroll.Name;
            }
        }

        #region Event
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBack_Click(object sender, DirectEventArgs e)
        {
            Response.Redirect(PayrollConfigUrl + "?mId=" + MenuId, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEdit_Click(object sender, DirectEventArgs e)
        {
            if (string.IsNullOrEmpty(hdfId.Text))
            {
                ExtNet.Msg.Alert("Thông báo", "Bạn chưa chọn bản ghi nào").Show();
            }
            else
            {
                var config = SalaryBoardConfigController.GetById(Convert.ToInt32(hdfId.Text));
                if (config != null)
                {
                    cboExcelColumn.Text = config.ColumnExcel;
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
                    }else if ((int)config.DataType == (int)SalaryConfigDataType.FieldFormula)
                    {
                        cbxChoseFieldFormula.Show();
                        txtColumnCode.Hide();
                        cbxChoseFieldFixed.Hide();
                        hdfChoseFieldFormula.Text = config.ColumnCode;
                        cbxChoseFieldFormula.Text = config.ColumnCode;
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
                SalaryBoardConfigController.Delete(int.Parse("0" + item.RecordID));
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
            Response.Redirect(SalaryConfigUrl + "?id=" + hdfConfigId.Text, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 
        /// </summary>
        [DirectMethod]
        public void btnAddPayrollConfig_Click()
        {
            btnCreatePayrollConfig.Show();
            btnUpdatePayrollConfig.Hide();
            wdCreatePayrollConfig.Show();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void storeExcelColumn_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            storeExcelColumn.DataSource = GetCalculateCodes(50);
            storeExcelColumn.DataBind();
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
        [DirectMethod]
        public void ResetForm()
        {
            txtColumnCode.Reset();
            cboExcelColumn.Reset();
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
            hdfChoseFieldFormula.Reset();
            cbxChoseFieldFormula.Reset();
        }

        /// <summary>
        /// 
        /// </summary>
        [DirectMethod]
        public void ResetFormPayrollConfig()
        {
            txtPayrollConfigDescription.Reset();
            txtPayrollConfigName.Reset();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        private void EditData(hr_SalaryBoardConfig config)
        {

            config.ColumnCode = txtColumnCode.Text;
            config.ColumnExcel = cboExcelColumn.Text;
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
                    case (int)SalaryConfigDataType.FieldFormula:
                        config.DataType = SalaryConfigDataType.FieldFormula;
                        config.ColumnCode = hdfChoseFieldFormula.Text;
                        break;
                    default:
                        config.DataType = 0;
                        break;
                }

            }
        }

        /// <summary>
        /// get unused caculated code
        /// </summary>
        /// <returns></returns>
        private List<ListItemModel> GetCalculateCodes(int num)
        {
            var codes = new List<ListItemModel>();

            var configs = SalaryBoardConfigController.GetAll(int.Parse(hdfConfigId.Text), null, null, null, null, null, null);
            for(var i = 1; i < num + configs.Count; i++)
            {
                codes.Add(new ListItemModel { Key = i.ToExcelColumnName(), Value = i.ToExcelColumnName()});
            }

            //check calculateCode exist
            var colExcels = configs.Select(a => a.ColumnExcel).ToList();

            codes.RemoveAll(o => colExcels.Contains(o.Value));

            return codes;
        }
        #endregion

    }
}
