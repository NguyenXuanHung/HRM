using System;
using System.Collections.Generic;
using System.Linq;
using Ext.Net;
using Web.Core.Framework;
using Web.Core.Service.Catalog;

namespace Web.HJM.Modules.UserControl
{
    public partial class ucChooseEmployee : BaseUserControl
    {
        public SelectedRowCollection SelectedRow;
        public event EventHandler AfterClickAcceptButton;
        int _countRole = -1;

        private string[] departmentList;
        //public enum WorkingStatus
        //{
        //    DangLamViec,
        //    DaRoiDonVi,
        //    TuTran,
        //    TatCa
        //}
        public bool ChiChonMotCanBo { get; set; }
        // public WorkingStatus DisplayWorkingStatus { get; set; }
        public string DanhSachHoTen { get; private set; } //Chứa danh sách các họ tên được chọn, ngăn cách nhau bởi dấu phẩy
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                if (ChiChonMotCanBo)
                {
                    EmployeeGrid.GetSelectionModel().Attributes.Add("SingleSelect", "true");
                }

            }
        }

        #region Event Method

        protected void cbxTrangThaiHoSo_OnrefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            cbxTrangThaiHoSo_store.DataSource = cat_WorkStatusServices.GetAll();
            cbxTrangThaiHoSo_store.DataBind();
        }

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
            if (AfterClickAcceptButton != null)
            {
                AfterClickAcceptButton(SelectedRow, null);
            }
        }

        protected void stDepartmentList_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            stDepartmentList.DataSource = CurrentUser.DepartmentsTree;

            cbDepartmentList.DataBind();
        }

        #endregion

    }

}

