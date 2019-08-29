using System;
using Ext.Net;
using System.IO;
using SoftCore;
using Web.Core.Framework;
using System.Linq;
using Web.Core;
using Web.Core.Framework.Adapter;
using Web.Core.Framework.Common;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.Catalog;
using Web.Core.Service.HumanRecord;

namespace Web.HJM.Modules.ProfileHuman.Management
{
    public partial class EmulationTitleManagement : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ExtNet.IsAjaxRequest)
            {
                // init department
                storeDepartment.DataSource = CurrentUser.DepartmentsTree;
                storeDepartment.DataBind();
                hdfBusinessType.Text = Request.QueryString["businessType"];
                hdfDepartments.Text = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
                new Core.Framework.Common.BorderLayout()
                {
                    menuID = MenuId,
                    script = "#{hdfDepartmentSelectedId}.setValue('" + Core.Framework.Common.BorderLayout.nodeID +
                             "');#{txtSearch}.reset();#{PagingToolbar1}.pageIndex = 0; #{PagingToolbar1}.doLoad();"
                }.AddDepartmentList(brlayout, CurrentUser, true);
                if (btnEdit.Visible)
                {
                    gridEmulation.Listeners.RowDblClick.Handler +=
                        " if(CheckSelectedRows(gridEmulation)){btnUpdateAndSave.show();}";
                }
            }
        }

        #region Event Method

        protected void InsertOrUpdate(object sender, DirectEventArgs e)
        {
            if (hdfCommandName.Text == @"Update" || hdfCommandUpdate.Text == @"Update")
            {
                Update();
            }
            else
            {
                Insert();
            }

            if (e.ExtraParams["Close"] != "True")
            {
                RM.RegisterClientScriptBlock("resetform1", "ResetFormBusiness();");
                return;
            }

            //hide window
            wdEmulation.Hide();
            wdUpdateEmulation.Hide();

            //reload data
            gridEmulation.Reload();
            txtDescription.Text = "";
            RM.RegisterClientScriptBlock("resetform1", "ResetFormBusiness();");
            RM.RegisterClientScriptBlock("Grid_Reload", "ReloadGrid();");
        }

        protected void Delete(object sender, DirectEventArgs directEventArgs)
        {
            try
            {
                int id;
                if (!int.TryParse(hdfKeyRecord.Text, out id) || id <= 0) return;
                hr_BusinessHistoryServices.Delete(id);
                gridEmulation.Reload();
                RM.RegisterClientScriptBlock("Grid_Reload", "ReloadGrid();");
            }
            catch (Exception ex)
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình xóa: {0}".FormatWith(ex.Message));
            }

        }

        protected void EditEmulationClick(object sender, DirectEventArgs e)
        {
            int id;
            if (!int.TryParse(hdfKeyRecord.Text, out id) || id <= 0) return;
            var recordId = int.Parse(hdfRecordId.Text);
            var hs = RecordController.GetById(recordId);

            if (hs != null)
            {
                txtUpdateFullname.Text = hs.FullName;
            }

            var business = hr_BusinessHistoryServices.GetById(id);
            if (business == null) return;
            txtUpdateDecisionNumber.Text = business.DecisionNumber;
            txtUpdateDescription.Text = business.Description;
            txtUpdateCurrentPosition.Text = business.CurrentPosition;
            txtUpdateShortDecision.Text = business.ShortDecision;
            cbxUpdateDestDepartment.Text = business.DestinationDepartment;
            txtUpdateEmulationTitle.Text = business.EmulationTitle;
            cbxUpdateMakerPosition.Text = business.DecisionPosition;
            txtUpdateCurrentDepartment.Text = business.CurrentDepartment;
            txtUpdateMoney.Text = business.Money.ToString();

            if (!string.IsNullOrEmpty(business.FileScan))
            {
                int pos = business.FileScan.LastIndexOf('/');
                if (pos != -1)
                {
                    string tenTT = business.FileScan.Substring(pos + 1);
                    uploadFileScan.Text = tenTT;
                }

                hdfTepTinDinhKem.Text = business.FileScan;
            }

            if (business.DecisionDate != null)
                dfUpdateDecisionDate.SetValue(business.DecisionDate);
            txtUpdateDecisionMaker.Text = business.DecisionMaker;

            // init command name & window properties
            hdfCommandName.Text = @"Update";
            hdfCommandUpdate.Text = @"Update";
            // show window
            txtUpdateDecisionNumber.Disabled = false;
            dfUpdateDecisionDate.Disabled = false;
            wdUpdateEmulation.Show();
        }

        private string GetContent(string fileName)
        {
            return Util.GetInstance().ReadFile(Server.MapPath(fileName));
        }

        public void DownloadAttachFile(string tableName, Hidden sender)
        {
            try
            {
                if (string.IsNullOrEmpty(sender.Text))
                {
                    Dialog.ShowNotification("Không có tệp tin đính kèm");
                    return;
                }

                var serverPath = Server.MapPath(sender.Text);
                if (Util.GetInstance().FileIsExists(serverPath) == false)
                {
                    Dialog.ShowNotification("Tệp tin không tồn tại hoặc đã bị xóa !");
                    return;
                }

                Response.Clear();
                //  Response.ContentType = "application/octet-stream";

                if (sender.Text != null)
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + sender.Text.Replace(" ", "_"));
                if (serverPath != null) Response.WriteFile(serverPath);
                Response.End();
            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Thông báo từ hệ thống", ex.Message).Show();
            }
        }

        public void DownloadAttachFile(string tableName, string path)
        {
            try
            {
                if (string.IsNullOrEmpty(path))
                {
                    Dialog.ShowNotification("Không có tệp tin đính kèm");
                    return;
                }

                var serverPath = Server.MapPath(path);
                if (Util.GetInstance().FileIsExists(serverPath) == false)
                {
                    Dialog.ShowNotification("Tệp tin không tồn tại hoặc đã bị xóa !");
                    return;
                }

                Response.Clear();
                //  Response.ContentType = "application/octet-stream";

                Response.AddHeader("Content-Disposition", "attachment; filename=" + path.Replace(" ", "_"));
                Response.WriteFile(serverPath);
                Response.End();
            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Thông báo từ hệ thống", ex.Message).Show();
            }
        }

        public void DeleteTepTinDinhKem(string tableName, int id, Hidden sender)
        {
            // xóa trong csdl
            SQLHelper.ExecuteTable(
                SQLManagementAdapter.GetStore_DeleteFileScan("hr_BusinessHistory", Convert.ToInt32(hdfKeyRecord.Text)));
            // xóa file trong thư mục
            var serverPath = Server.MapPath(sender.Text);
            if (Util.GetInstance().FileIsExists(serverPath) == false)
            {
                Dialog.ShowNotification("Tệp tin không tồn tại hoặc đã bị xóa !");
                return;
            }

            File.Delete(serverPath);
        }

        protected void btnMoveToDownload_Click(object sender, DirectEventArgs e)
        {
            DownloadAttachFile("hr_BusinessHistory", hdfTepTinDinhKem);
        }

        protected void btnMoveToDelete_Click(object sender, DirectEventArgs e)
        {
            try
            {
                if (hdfRecordId.Text == "") return;
                DeleteTepTinDinhKem("hr_BusinessHistory", int.Parse("0" + hdfRecordId.Text), hdfTepTinDinhKem);
                hdfTepTinDinhKem.Text = "";
            }
            catch (Exception)
            {
                Dialog.ShowNotification("Tệp tin không tồn tại hoặc đã bị xóa !");
            }

        }

        #endregion

        #region Private Method

        private void Insert()
        {
            try
            {
                var makerPosition = string.Empty;
                var destDepartment = string.Empty;
                if (hdfIsMakerPosition.Text == @"0")
                    makerPosition = cbxMakerPosition.Text;
                else
                    makerPosition = cbxMakerPosition.SelectedItem.Text;
                if (hdfIsDestDepartment.Text == @"0")
                    destDepartment = cbxDestDepartment.Text;
                else
                    destDepartment = cbxDestDepartment.SelectedItem.Text;

                var business = new hr_BusinessHistory
                {
                    DecisionNumber = txtDecisionNumber.Text.Trim(),
                    DecisionDate = dfDecisionDate.SelectedDate,
                    DecisionMaker = txtDecisionMaker.Text.Trim(),
                    SourceDepartment = txtSourceDepartment.Text,
                    ShortDecision = txtShortDecision.Text,
                    CurrentPosition = txtCurrentPosition.Text,
                    CurrentDepartment = txtCurrentDepartment.Text,
                    EmulationTitle = txtEmulationTitle.Text,
                    DestinationDepartment = destDepartment.TrimStart('-'),
                    DecisionPosition = makerPosition,
                    BusinessType = hdfBusinessType.Text,
                    CreatedDate = DateTime.Now,
                    EditedDate = DateTime.Now,
                    Description = txtDescription.Text.Trim()
                };
                if (!string.IsNullOrEmpty(txtMoney.Text))
                    business.Money = double.Parse("0" + txtMoney.Text);
                if (!string.IsNullOrEmpty(hdfChonCanBo.Text))
                    business.RecordId = int.Parse("0" + hdfChonCanBo.Text);
                // upload file
                var path = string.Empty;
                if (fufTepTinDinhKem.HasFile)
                {
                    string directory = Server.MapPath("../");
                    path = UploadFile(fufTepTinDinhKem, Constant.PathAttachFile);
                }

                business.FileScan = path != "" ? path : hdfTepTinDinhKem.Text;

                hr_BusinessHistoryServices.Create(business);
                gridEmulation.Reload();
            }
            catch (Exception e)
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình thêm mới: {0}".FormatWith(e.Message));
            }
        }

        public string UploadFile(object sender, string relativePath)
        {
            var obj = (FileUploadField) sender;
            var file = obj.PostedFile;
            var dir = new DirectoryInfo(Server.MapPath("../") + relativePath); // save file to directory HoSoNhanSu/File
            // if directory not exist then create this
            if (dir.Exists == false)
                dir.Create();
            var rdstr = Util.GetInstance().GetRandomString(7);
            var path = Server.MapPath("../") + relativePath + "/" + rdstr + "_" + obj.FileName;
            if (File.Exists(path))
                return "";
            var info = new FileInfo(path);
            file.SaveAs(path);
            return relativePath + "/" + rdstr + "_" + obj.FileName;
        }

        private void Update()
        {
            try
            {
                var makerPosition = string.Empty;
                var destDepartment = string.Empty;
                if (hdfIsUpdateMakerPosition.Text == @"0")
                    makerPosition = cbxUpdateMakerPosition.Text;
                else
                    makerPosition = cbxUpdateMakerPosition.SelectedItem.Text;
                if (hdfIsUpdateDestDepartment.Text == @"0")
                    destDepartment = cbxUpdateDestDepartment.Text;
                else
                    destDepartment = cbxUpdateDestDepartment.SelectedItem.Text;

                int id;
                if (!int.TryParse(hdfKeyRecord.Text, out id) || id <= 0) return;
                var business = hr_BusinessHistoryServices.GetById(id);
                var util = new Util();
                if (business == null) return;
                if (!string.IsNullOrEmpty(hdfRecordId.Text))
                    business.RecordId = Convert.ToInt32(hdfRecordId.Text);
                if (!util.IsDateNull(dfUpdateDecisionDate.SelectedDate))
                    business.DecisionDate = dfUpdateDecisionDate.SelectedDate;
                business.DecisionNumber = txtUpdateDecisionNumber.Text;
                // upload file
                var path = string.Empty;
                if (uploadFileScan.HasFile)
                {
                    string directory = Server.MapPath("../");
                    path = UploadFile(uploadFileScan, Constant.PathAttachFile);
                }

                business.FileScan = path != "" ? path : hdfTepTinDinhKem.Text;
                business.DecisionMaker = txtUpdateDecisionMaker.Text;
                business.DecisionPosition = makerPosition;
                business.CurrentPosition = txtUpdateCurrentPosition.Text;
                business.CurrentDepartment = txtUpdateCurrentDepartment.Text;
                business.ShortDecision = txtUpdateShortDecision.Text;
                business.EmulationTitle = txtUpdateEmulationTitle.Text;
                business.DestinationDepartment = destDepartment.Trim('-');
                business.Description = txtUpdateDescription.Text;
                business.BusinessType = hdfBusinessType.Text;
                business.CreatedDate = DateTime.Now;
                business.EditedDate = DateTime.Now;
                if (!string.IsNullOrEmpty(txtUpdateMoney.Text))
                    business.Money = double.Parse("0" + txtUpdateMoney.Text);

                hr_BusinessHistoryServices.Update(business);
            }
            catch (Exception e)
            {
                Dialog.Alert("Có lỗi xảy ra trong quá trình cập nhật: {0}".FormatWith(e.Message));
            }

        }

        #endregion
    }
}