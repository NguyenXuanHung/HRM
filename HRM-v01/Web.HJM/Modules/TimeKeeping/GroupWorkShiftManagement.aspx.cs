using System;
using Ext.Net;
using Web.Core.Framework;
using Web.Core;
using Web.Core.Object.HumanRecord;
using System.Linq;
using SoftCore;
using Web.Core.Service.HumanRecord;

namespace Web.HJM.Modules.TimeKeeping
{
    public partial class GroupWorkShiftManagement : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ExtNet.IsAjaxRequest)
            {
                hdfDepartments.Text = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
                if (btnEdit.Visible)
                {
                    gridGroupWorkShift.Listeners.RowDblClick.Handler += " if(CheckSelectedRows(gridGroupWorkShift)){btnUpdate.show();btnUpdateNew.hide();btnUpdateClose.hide()}";
                }
                gridGroupWorkShift.Reload();
            }
        }

        #region Event Method
        protected void InsertOrUpdate(object sender, DirectEventArgs e)
        {
            try
            {
                var time = new hr_TimeSheetGroupWorkShift()
                {
                    Name = txtName.Text,
                    CreatedDate = DateTime.Now,
                    CreatedBy = CurrentUser.User.UserName,
                };
               
                if (e.ExtraParams["Command"] == "Update")
                {
                    if (!string.IsNullOrEmpty(hdfKeyRecord.Text))
                    {
                        var editTime = hr_TimeSheetGroupWorkShiftServices.GetById(Convert.ToInt32(hdfKeyRecord.Text));
                        if (editTime != null)
                        {
                            editTime.Name = txtName.Text;
                            editTime.EditedDate = DateTime.Now;
                        }
                        hr_TimeSheetGroupWorkShiftServices.Update(editTime);
                    }
                }
                else
                {
                    hr_TimeSheetGroupWorkShiftServices.Create(time);
                }
                if (e.ExtraParams["Close"] == "True")
                {
                    wdGroupWorkShift.Hide();
                }
                //reload data
                gridGroupWorkShift.Reload();
            }
            catch (Exception ex)
            {
                ExtNet.MessageBox.Alert("Có lỗi xảy ra", ex.Message).Show();
            }
        }

        [DirectMethod]
        public void ResetForm()
        {
            txtName.Reset();
        }

        protected void Delete(object sender, DirectEventArgs directEventArgs)
        {
            try
            {
                if (!string.IsNullOrEmpty(hdfKeyRecord.Text))
                {
                    hr_TimeSheetGroupWorkShiftServices.Delete(Convert.ToInt32(hdfKeyRecord.Text));
                    gridGroupWorkShift.Reload();
                    RM.RegisterClientScriptBlock("Grid_Reload", "ReloadGrid();");
                }
            }
            catch (Exception ex)
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình xóa: {0}".FormatWith(ex.Message));
            }
        }

        protected void EditGroupWorkShift_Click(object sender, DirectEventArgs e)
        {
            if (!string.IsNullOrEmpty(hdfKeyRecord.Text))
            {
                var time = hr_TimeSheetGroupWorkShiftServices.GetById(Convert.ToInt32(hdfKeyRecord.Text));
                if (time != null)
                {
                    txtName.Text = time.Name;
                }
            }
            
            // show window
            btnUpdate.Show();
            btnUpdateNew.Hide();
            btnUpdateClose.Hide();
          
            wdGroupWorkShift.Title = @"Cập nhật thông tin nhóm phân ca";
            wdGroupWorkShift.Show();
        }

        #endregion
    }

}
