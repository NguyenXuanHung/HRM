using System;
using System.Linq;
using Ext.Net;
using Web.Core.Framework;
using Web.Core.Service.Catalog;

namespace Web.HRM.Modules.UC
{
    public partial class ChooseEmployee : BaseUserControl
    {
        public SelectedRowCollection SelectedRow;
        public event EventHandler AfterClickAcceptButton;        
        public bool OnlyChooseOnePerson { get; set; }
        public string DanhSachHoTen { get; private set; } //Chứa danh sách các họ tên được chọn, ngăn cách nhau bởi dấu phẩy

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                // Set resource
                wdChooseUser.Title =  Resource.Get("WindowsChooseEmployeeTitle");

                if (OnlyChooseOnePerson)
                {
                    EmployeeGrid.GetSelectionModel().Attributes.Add("SingleSelect", "true");
                }

            }
        }

        #region Event Method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cbxTrangThaiHoSo_OnrefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            cbxTrangThaiHoSo_store.DataSource = cat_WorkStatusServices.GetAll();
            cbxTrangThaiHoSo_store.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddEmployee_Click(object sender, DirectEventArgs e)
        {
            if (!chkEmployeeRowSelection.SelectedRows.Any())
            {
                wdChooseUser.Hide();
                ExtNet.MessageBox.Alert("Cảnh báo", "Bạn phải chọn ít nhất một nhân viên").Show();
                return;
            }
            SelectedRow = chkEmployeeRowSelection.SelectedRows;

            wdChooseUser.Hide();

            DanhSachHoTen = hdfHoTenCanBo.Text;
            AfterClickAcceptButton?.Invoke(SelectedRow, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void stDepartmentList_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            stDepartmentList.DataSource = CurrentUser.DepartmentsTree;

            cbDepartmentList.DataBind();
        }

        #endregion

    }

}

