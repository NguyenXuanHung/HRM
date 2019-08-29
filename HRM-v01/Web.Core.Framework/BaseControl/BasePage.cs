using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web.UI;
using Ext.Net;
using SmartXLS;
using SoftCore;
using Web.Core.Framework.Adapter;
using Web.Core.Framework.Common;
using Web.Core.Object.Security;
using Web.Core.Service.Catalog;

namespace Web.Core.Framework
{
    public class BasePage : Page
    {
        /// <summary>
        /// Current user information
        /// </summary>
        public UserModel CurrentUser;

        /// <summary>
        /// Current page permission
        /// </summary>
        public PermissionModel CurrentPermission;

        /// <summary>
        /// Current Menu
        /// </summary>
        protected int MenuId { get; set; }

        /// <summary>
        /// Get only file name from path of file
        /// </summary>        
        /// <param name="filePath"></param>
        /// <returns>the file name</returns>
        public string GetFileName(string filePath)
        {
            var index = filePath.LastIndexOf('/');
            return index != -1 ? filePath.Substring(index + 1) : "";
        }

        /// <summary>
        ///  Get deparmentIds ( join string ) from current user
        /// </summary>
        public string DepartmentIds
        {
            get { return string.Join(",", CurrentUser.Departments.Select(d => d.Id)); }
        }

        /// <summary>
        ///  Get deparmentIds for T-SQL ( join string ) from current user
        /// </summary>
        public string DepartmentIdsSql
        {
            get { return string.Join(",", CurrentUser.Departments.Select(d => "'{0}'".FormatWith(d.Id))); }
        }

        public string AddRecordString(RecordModel recordModel)
        {
            return $"addRecord('{recordModel.Id}', '{recordModel.EmployeeCode}', '{recordModel.FullName}', '{recordModel.DepartmentName}');";
        }

        #region File upload

        /// <summary>
        /// Upload file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="relativePath"></param>
        /// <returns></returns>
        [DirectMethod]
        public string UploadFile(object sender, string relativePath)
        {
            var obj = (FileUploadField)sender;
            var file = obj.PostedFile;
            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            if (!FileTypeUpload.IsAllowImageFileExtension(fileExtension) &&
                !FileTypeUpload.IsAllowDocumentFileExtension(fileExtension))
            {
                throw new Exception("Hệ thống không hỗ trợ định dạng file này");
            }            

            var dir = new DirectoryInfo(Server.MapPath("~/" + relativePath));

            // if directory not exist then create this
            if(dir.Exists == false)
                dir.Create();
            var guid = Guid.NewGuid();
            var fileRelativePath = relativePath + "/" + guid + "_" + obj.FileName;
            var filePath = Path.Combine(Server.MapPath("~/"), fileRelativePath);
            if(File.Exists(filePath))
                throw new Exception("File đã tồn tại");            
            file.SaveAs(filePath);
           
            return guid + "_" + obj.FileName;
        }

        /// <summary>
        /// Upload file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="relativePath"></param>
        /// <returns></returns>
        [DirectMethod]
        public string UploadFileAndDisplay(object sender, string relativePath)
        {
            var obj = (FileUploadField)sender;
            var file = obj.PostedFile;
            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            if (!FileTypeUpload.IsAllowImageFileExtension(fileExtension) &&
                !FileTypeUpload.IsAllowDocumentFileExtension(fileExtension))
            {
                throw new Exception("Hệ thống không hỗ trợ định dạng file này");
            }

            var dir = new DirectoryInfo(Server.MapPath("~/" + relativePath));

            // if directory not exist then create this
            if (dir.Exists == false)
                dir.Create();
            var guid = Guid.NewGuid();
            var fileRelativePath = relativePath + "/" + guid + "_" + obj.FileName;
            var filePath = Path.Combine(Server.MapPath("~/"), fileRelativePath);
            if (File.Exists(filePath))
                throw new Exception("File đã tồn tại");
            file.SaveAs(filePath);
            File.Move(filePath, Server.MapPath(fileRelativePath));
            return guid + "_" + obj.FileName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="sender"></param>
        public void DownloadAttachFile(string tableName, Hidden sender)
        {
            try
            {
                if(string.IsNullOrEmpty(sender.Text))
                {
                    Dialog.ShowNotification("Không có tệp tin đính kèm");
                    return;
                }

                var serverPath = Server.MapPath("~/" + sender.Text);
                if(Util.GetInstance().FileIsExists(serverPath) == false)
                {
                    Dialog.ShowNotification("Tệp tin không tồn tại hoặc đã bị xóa !");
                    return;
                }

                Response.Clear();
                //  Response.ContentType = "application/octet-stream";

                if(sender.Text != null)
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + sender.Text.Replace(" ", "_"));
                if(serverPath != null) Response.WriteFile(serverPath);
                Response.End();
            }
            catch(Exception ex)
            {
                ExtNet.Msg.Alert("Thông báo từ hệ thống", ex.Message).Show();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="path"></param>
        public void DownloadAttachFile(string tableName, string path)
        {
            try
            {
                if(string.IsNullOrEmpty(path))
                {
                    Dialog.ShowNotification("Không có tệp tin đính kèm");
                    return;
                }

                var serverPath = Server.MapPath(path);
                if(Util.GetInstance().FileIsExists(serverPath) == false)
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
            catch(Exception ex)
            {
                ExtNet.Msg.Alert("Thông báo từ hệ thống", ex.Message).Show();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        [DirectMethod]
        public void DownloadAttach(string path)
        {
            try
            {
                var serverPath = Server.MapPath("~/" + path);
                if(Util.GetInstance().FileIsExists(serverPath) == false)
                {
                    Dialog.ShowNotification("Tệp tin không tồn tại hoặc đã bị xóa !");
                    return;
                }

                var str = path.Replace(" ", "_");
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + str);
                Response.WriteFile(serverPath ?? throw new InvalidOperationException());
                Response.End();
            }
            catch(Exception ex)
            {
                Dialog.Alert("Có lỗi xảy ra: " + ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        public void DeleteFile(string path)
        {
            try
            {
                var serverPath = Server.MapPath(path);
                if(Util.GetInstance().FileIsExists(serverPath) == false)
                {
                    Dialog.ShowNotification("Tệp tin không tồn tại hoặc đã bị xóa !");
                    return;
                }
                File.Delete(serverPath ?? throw new InvalidOperationException());
            }
            catch(Exception ex)
            {
                Dialog.Alert("Có lỗi xảy ra: " + ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="id"></param>
        /// <param name="sender"></param>
        public void DeleteAttachFile(string tableName, int id, Hidden sender)
        {
            // xóa trong csdl
            SQLHelper.ExecuteTable(
                SQLManagementAdapter.GetStore_DeleteAttachFile(tableName, id));
            DeleteFile(sender.Text);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="id"></param>
        /// <param name="sender"></param>
        public void DeleteAttackFile(string tableName, int id, Hidden sender)
        {
            // xóa trong csdl
            SQLHelper.ExecuteTable(
                SQLManagementAdapter.GetStore_DeleteFileScan(tableName, id));
            DeleteFile(sender.Text);
        }

        #endregion

        #region Excel

        /// <summary>
        /// Get excel column name
        /// </summary>
        /// <param name="columnNumber"></param>
        /// <returns></returns>
        public string GetExcelColumnName(int columnNumber)
        {
            var dividend = columnNumber;
            var columnName = string.Empty;

            while(dividend > 0)
            {
                var modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (int)((dividend - modulo) / 26);
            }
            return columnName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="workbook"></param>
        /// <param name="col"></param>
        /// <param name="fromRow"></param>
        /// <param name="toRow"></param>
        public void CreateDropDownExcel(string obj, WorkBook workbook, DataColumn col, int fromRow, int toRow)
        {
            var list = cat_BaseServices.GetAll(obj, null, null, null, false, null, null);
            if(list == null) return;
            var validation = workbook.CreateDataValidation();
            validation.Type = DataValidation.eUser;
            var validateList = "\"{0}\"".FormatWith(string.Join(",", list.Select(l => l.Name + " ({0})".FormatWith(l.Id)).ToList()));
            // formula string length cannot be greater than 256
            if(validateList.Length < 256)
            {
                // set formula by string
                validation.Formula1 = validateList;
            }
            else
            {
                var columnName = GetExcelColumnName(col.Ordinal + 1);
                // select info sheet
                workbook.Sheet = 1;
                // write list into info sheet
                foreach(var item in list.Select((value, index) => new { value, index }))
                {
                    workbook.setText(item.index + 1, col.Ordinal, item.value.Name + " ({0})".FormatWith(item.value.Id));
                }
                // select 
                workbook.Sheet = 0;
                // set formula by selected range
                validation.Formula1 = "Info!${0}$2:${0}${1}".FormatWith(columnName, list.Count);
            }
            // selection range
            workbook.setSelection(fromRow, col.Ordinal, toRow, col.Ordinal);
            workbook.DataValidation = validation;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <param name="workbook"></param>
        /// <param name="col"></param>
        /// <param name="fromRow"></param>
        /// <param name="toRow"></param>
        public void CreateDropDownEnum(string enumName, WorkBook workbook, DataColumn col, int fromRow, int toRow)
        {
            var list = Enum.GetValues(typeof(RecordStatus)).Cast<RecordStatus>().ToList();
            var validation = workbook.CreateDataValidation();
            validation.Type = DataValidation.eUser;
            var validateList = "\"{0}\"".FormatWith(string.Join(",", list.Select(l => l.Description() + " ({0})".FormatWith((int)l)).ToList()));
            // formula string length cannot be greater than 256
            if (validateList.Length < 256)
            {
                // set formula by string
                validation.Formula1 = validateList;
            }
            else
            {
                var columnName = GetExcelColumnName(col.Ordinal + 1);
                // select info sheet
                workbook.Sheet = 1;
                // write list into info sheet
                foreach (var item in list.Select((value, index) => new { value, index }))
                {
                    workbook.setText(item.index + 1, col.Ordinal, item.value.Description() + " ({0})".FormatWith((int)item.value));
                }
                // select 
                workbook.Sheet = 0;
                // set formula by selected range
                validation.Formula1 = "Info!${0}$2:${0}${1}".FormatWith(columnName, list.Count);
            }
            // selection range
            workbook.setSelection(fromRow, col.Ordinal, toRow, col.Ordinal);
            workbook.DataValidation = validation;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serverPath"></param>
        /// <param name="table"></param>
        /// <param name="workBook"></param>
        /// <param name="autoResizeWidthColumn"></param>
        /// <param name="hiddenPropName"></param>
        public void WriteExcelFile(string serverPath, DataTable table, WorkBook workBook, bool autoResizeWidthColumn = true, bool hiddenPropName = true)
        {
            workBook.ImportDataTable(table, true, 0, 0, table.Rows.Count + 1, table.Columns.Count + 1);
            if(autoResizeWidthColumn)
            {
                // set auto resize width for column
                for(var i = 0; i < workBook.LastCol; i++)
                {
                    workBook.setColWidthAutoSize(i, true);
                }
            }
            if(hiddenPropName)
            {
                workBook.setRowHidden(1, true);
            }
            workBook.writeXLSX(serverPath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="relativePath"></param>
        /// <param name="filePath"></param>
        public void ExportToExcel(DataTable dataTable, string relativePath, string filePath)
        {
            try
            {
                var serverPath = Server.MapPath(relativePath + "/" + filePath);
                var workbook = new WorkBook();

                // write data table
                WriteExcelFile(serverPath, dataTable, workbook, true, false);

                Response.AddHeader("Content-Disposition", "attachment; filename=" + filePath);
                Response.WriteFile(serverPath);
                Response.End();
            }
            catch(Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// CheckValidation
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="fromRow"></param>
        /// <param name="toRow"></param>
        /// <param name="txtFromRow"></param>
        /// <param name="txtToRow"></param>
        /// <param name="txtSheetName"></param>
        /// <returns></returns>
        public bool CheckValidation(WorkBook workbook, out int fromRow, out int toRow, TextField txtFromRow,
            TextField txtToRow, TextField txtSheetName)
        {
            // read from excel
            fromRow = Constant.FromRow;
            toRow = Constant.ToRow;
            workbook.Sheet = !txtSheetName.IsEmpty ? workbook.findSheetByName(txtSheetName.Text) : 0;

            if (!txtFromRow.IsEmpty)
            {
                fromRow = Convert.ToInt32(txtFromRow.Text);
            }

            if (txtToRow.IsEmpty)
            {
                toRow = workbook.LastRow + 1;
            }
            else
            {
                toRow = Convert.ToInt32(txtToRow.Text);
            }

            if (fromRow < 2)
            {
                Dialog.Alert("Từ hàng lớn hơn 1");
                return false;
            }

            if (fromRow <= toRow && toRow <= workbook.LastRow + 1) return true;
            Dialog.Alert("Từ hàng đến hàng không hợp lệ, tệp đính kèm gồm có "
                         + (workbook.LastCol + 1) + " cột và " + (workbook.LastRow + 1) + " hàng.");
            return false;
        }

        #endregion

        /// <inheritdoc />
        /// <summary>
        /// Init culture
        /// </summary>
        protected override void InitializeCulture()
        {
            // check language
            if(Session["Language"] == null)
            {
                // init default language
                Session["Language"] = @"vi-VN";
            }
            // init culture
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture((string)Session["Language"]);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo((string)Session["Language"]);
            // base init culture
            base.InitializeCulture();
        }

        /// <inheritdoc />
        /// <summary>
        /// Page load
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            // check session current user
            if(Session["CurrentUser"] == null)
                Response.Redirect(Resource.Get("LoginUrl"), true);

            // init current user
            CurrentUser = (UserModel)Session["CurrentUser"];

            // init menu id
            if (int.TryParse(Request.QueryString["mId"], out var parseMenuId) && parseMenuId > 0)
            {
                MenuId = parseMenuId;
            }
            else
            {
                var menuModel = MenuController.GetByUrl(Request.ApplicationPath);
                MenuId = menuModel?.Id ?? 0;
            }
            
            // check menu id zero or use is admin
            if(CurrentUser != null && CurrentUser.User.IsSuperUser)
            {
                // init full control permission
                CurrentPermission = new PermissionModel(true, true, true, true);
            }
            else
            {
                if(Request.ApplicationPath == "/" || string.Compare(Request.ApplicationPath, "/default.aspx", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    // init full control permission for default page
                    CurrentPermission = new PermissionModel(true, true, true, true);
                }
                else
                {
                    if(MenuId == 0)
                    {
                        CurrentPermission = new PermissionModel(false, false, false, false);
                    }
                    else
                    {
                        // get permission role
                        var menuRoles = MenuRoleServices.GetAll(null, MenuId, null, null).Where(mr => CurrentUser.Roles.Select(r => r.Id).Contains(mr.RoleId));
                        // int permission
                        var canRead = false;
                        var canWrite = false;
                        var canDelete = false;
                        var fullControl = false;
                        foreach(var menuRole in menuRoles)
                        {
                            // permission in format RWDF - example 1100 => Read: true, Write: true, Delete: false, FullControl: false
                            fullControl = fullControl || menuRole.Permission[3] == '1';
                            canDelete = fullControl || canDelete || menuRole.Permission[2] == '1';
                            canWrite = canDelete || canWrite || menuRole.Permission[1] == '1';
                            canRead = canWrite || canRead || menuRole.Permission[0] == '1';
                        }
                        // update permission by level
                        canDelete = fullControl || canDelete;
                        canWrite = canDelete || canWrite;
                        canRead = canWrite || canRead;
                        // init current page permission
                        CurrentPermission = new PermissionModel(canRead, canWrite, canDelete, fullControl);
                    }
                }
            }
            // check current permission
            if(CurrentPermission.CanRead)
            {
                try
                {
                    // check permission
                    var toolbarIds = new[] { "toolbarFn" };
                    foreach(var toolbarId in toolbarIds)
                    {
                        // set toolbar permission
                        var findToolbar = FindControl(toolbarId);
                        if(findToolbar != null)
                        {
                            // cast to Ext.Net.Toolbar
                            var toolbarFn = (Toolbar)findToolbar;
                            // check all button in toolbar
                            foreach(var item in toolbarFn.Items)
                            {
                                if(item.InstanceOf == "Ext.Button")
                                {
                                    // cast obj to button
                                    var btn = (Button)item;
                                    // set write (add & edit) permission
                                    if(btn.ID.Contains("Add") || btn.ID.Contains("Edit"))
                                    {
                                        btn.Visible = CurrentPermission.CanWrite;
                                    }
                                    // set delete permission
                                    if(btn.ID.Contains("Delete"))
                                    {
                                        btn.Visible = CurrentPermission.CanDelete;
                                    }
                                }
                            }
                        }
                    }
                    // base load
                    base.OnLoad(e);
                }
                catch(Exception ex)
                {
                    SystemLogController.Create(new SystemLogModel("System", "BasePage", ex));
                }
            }
            else
            {
                Dialog.Alert("Cảnh báo truy cập", "Bạn không có quyền truy cập chức năng này");
            }
        }
    }
}
