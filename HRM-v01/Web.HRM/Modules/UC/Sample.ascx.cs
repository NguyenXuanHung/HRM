using System;
using System.Linq;
using Ext.Net;
using Web.Core.Framework;
using Web.Core.Service.Sample;
using Web.Core.Object.Sample;
using Web.Core;

namespace Web.HRM.Modules.UC
{
    public partial class Sample : BaseUserControl
    {
        public SelectedRowCollection SelectedRow;
        public event EventHandler AfterClickAcceptButton;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ExtNet.IsAjaxRequest)
            {
                hdfDepartment.Text = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddSample_Click(object sender, DirectEventArgs e)
        {
            try
            {
                var sample = new SampleList();

                var arrDepartment = hdfDepartment.Text.Split(new[] { ',' }, StringSplitOptions.None);
                sample.Name = txtSampleName.Text;
                sample.CreatedBy = CurrentUser.User.UserName;
                sample.CreatedDate = DateTime.Now;
                sample.Note = txtSampleNote.Text;
                if (e.ExtraParams["command"] == "edit")
                {
                    sample.Id = int.Parse("0" + hdfIDSample.Text);

                    foreach (var t in arrDepartment)
                    {
                        if (!string.IsNullOrEmpty(t))
                        {
                            sample.DepartmentId = Convert.ToInt32(t);
                        }
                        SampleListServices.Update(sample);
                    }
                }
                else
                {

                    foreach (var t in arrDepartment)
                    {
                        if (!string.IsNullOrEmpty(t))
                        {
                            sample.DepartmentId = Convert.ToInt32(t);
                        }
                        SampleListServices.Create(sample);
                    }
                }
                wdSample.Hide();
                grp_SampleList.Reload();

            }
            catch (Exception ex)
            {
                Dialog.ShowError("" + ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDeleteSample_Click(object sender, DirectEventArgs e)
        {
            foreach (var item in chkSampleRowSelection.SelectedRows)
            {
                try
                {
                    SampleListServices.Delete(int.Parse("0" + item.RecordID));
                }
                catch (Exception ex)
                {
                    Dialog.ShowError("" + ex.Message);
                }
            }
            grp_SampleList.Reload();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDongY_Click(object sender, DirectEventArgs e)
        {
            if (chkSampleRowSelection.SelectedRows.Count() == 0)
            {
                wdSampleList.Hide();
                ExtNet.MessageBox.Alert("Cảnh báo", "Bạn phải chọn ít nhất một mẫu").Show();
                return;
            }
            SelectedRow = chkSampleRowSelection.SelectedRows;
            wdSampleList.Hide();
            if (AfterClickAcceptButton != null)
            {
                AfterClickAcceptButton(SelectedRow, null);
            }
        }
    }
}

