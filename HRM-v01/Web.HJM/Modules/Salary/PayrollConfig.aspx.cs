using Ext.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Core.Framework;
using Web.Core.Object.Catalog;
using Web.Core.Object.Salary;
using Web.Core.Service.Catalog;
using Web.Core.Service.Salary;

namespace Web.HJM.Modules.Salary
{
    public partial class PayrollConfig : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Init setting window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InitWindow(object sender, DirectEventArgs e)
        {
            var param = e.ExtraParams["Id"];
            if(int.TryParse(param, out var id))
            {
                if(id > 0)
                {
                    // edit
                    wdSetting.Title = @"Sửa cấu hình";
                    wdSetting.Icon = Icon.Pencil;
                }
                else
                {
                    // insert
                    wdSetting.Title = @"Thêm mới cấu hình";
                    wdSetting.Icon = Icon.Add;
                }
                hdfConfigId.Text = id.ToString();
                var payroll = new sal_PayrollConfig();
                if(id > 0)
                {
                    var result = sal_PayrollConfigServices.GetById(id);
                    if(result != null)
                        payroll = result;
                }
                txtName.Text = payroll.Name;
                txtDescription.Text = payroll.Description;
                txtOrder.Text = payroll.Order.ToString();
                cboIsDeleted.Text = payroll.IsDeleted ? "Khóa" : "Kích hoạt";
                // show window
                wdSetting.Show();
            }
        }

        [DirectMethod]
        public void SelectConfig()
        {
            if(!string.IsNullOrEmpty(hdfConfigId.Text))
            {
                Response.Redirect("~/Modules/Salary/SalaryConfig.aspx?id=" + hdfConfigId.Text);
            }
        }

        protected void rowDbl_Click(object sender, DirectEventArgs e)
        {
            if(!string.IsNullOrEmpty(hdfConfigId.Text))
            {
                Response.Redirect("~/Modules/Salary/SalaryConfig.aspx?id=" + hdfConfigId.Text);
            }
        }

        /// <summary>
        /// Insert or Update Catalog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InsertOrUpdate(object sender, DirectEventArgs e)
        {
            // init entity
            var payroll = new sal_PayrollConfig();
            // check id
            if(!string.IsNullOrEmpty(hdfConfigId.Text) && Convert.ToInt32(hdfConfigId.Text) > 0)
            {
                var result = sal_PayrollConfigServices.GetById(Convert.ToInt32(hdfConfigId.Text)); ;
                if(result != null)
                    payroll = result;
            }
            // set new props for entity
            payroll.Name = txtName.Text;
            payroll.Description = txtDescription.Text;
            payroll.Order = !string.IsNullOrEmpty(txtOrder.Text) ? Convert.ToInt32(txtOrder.Text) : 0;
            payroll.IsDeleted = !string.IsNullOrEmpty(hdfIsDeleted.Text) && Convert.ToBoolean(hdfIsDeleted.Text);
            payroll.Group = hdfGroup.Text;
            // check entity id
            if(payroll.Id > 0)
            {
                // update
                sal_PayrollConfigServices.Update(payroll);
            }
            else
            {
                // insert
                sal_PayrollConfigServices.Create(payroll);
            }
            // hide window
            wdSetting.Hide();
            // reload data
            grdPayrollConfig.Reload();
        }

        /// <summary>
        /// Delete catalog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Delete(object sender, DirectEventArgs e)
        {
            // init id
            var param = e.ExtraParams["Id"];
            // parse id
            if(!int.TryParse(param, out var id) || id <= 0)
            {
                // parse error, show error
                Dialog.ShowError("Có lỗi xảy ra trong quá trình xử lý");
                return;
            }
            // delete
            sal_PayrollConfigServices.Delete(id);
            // reload data
            grdPayrollConfig.Reload();
        }
    }
}