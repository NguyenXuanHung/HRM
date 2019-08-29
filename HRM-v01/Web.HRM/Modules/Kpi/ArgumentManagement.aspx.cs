using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Ext.Net;
using SmartXLS;
using Web.Core;
using Web.Core.Framework;
using Web.Core.Framework.Common;
using Web.Core.Framework.Model.Kpi;
using Web.Core.Object.Kpi;
using Web.Core.Service.Catalog;

namespace Web.HRM.Modules.Kpi
{
    public partial class ArgumentManagement : BasePage
    {
        private const string KpiArgumentFileName = "ImportKpiArgument.xlsx";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ExtNet.IsAjaxRequest)
            {
                hdfOrder.Text = Request.QueryString["order"];
            }
        }

        #region Event Methods

        /// <summary>
        /// init setting window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InitWindow(object sender, DirectEventArgs e)
        {
            try
            {
                //reset form
                ResetForm();

                // init window props
                if (e.ExtraParams["Command"] == "Update")
                {
                    // edit
                    wdSetting.Title = @"Cập nhật tham số cho mục tiêu đánh giá";
                    wdSetting.Icon = Icon.Pencil;
                    var model = ArgumentController.GetById(Convert.ToInt32(hdfId.Text));
                    if (model != null)
                    {
                        // set props
                        txtImportCode.Text = model.Code;
                        cboCalculateCode.Text = model.CalculateCode;
                        txtOrder.Text = model.Order.ToString();
                        cboValueType.Text = model.ValueTypeName;
                        hdfValueType.Text = ((int)model.ValueType).ToString();
                        txtName.Text = model.Name;
                        txtDescription.Text = model.Description;
                        if (model.Status == KpiStatus.Active)
                        {
                            chkIsActive.Checked = true;
                        }
                    }
                }
                else
                {
                    // insert
                    wdSetting.Title = @"Thêm mới tham số cho mục tiêu đánh giá";
                    wdSetting.Icon = Icon.Add;
                }

                // show window
                wdSetting.Show();
            }
            catch (Exception exception)
            {
                Dialog.ShowError(exception);
            }
        }

        /// <summary>
        /// insert or update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InsertOrUpdate(object sender, DirectEventArgs e)
        {
            try
            {
                // init entity
                var model = new ArgumentModel();
                var resultModel = new ArgumentModel();

                // check id
                if (!string.IsNullOrEmpty(hdfId.Text) && Convert.ToInt32(hdfId.Text) > 0)
                {
                    var result = ArgumentController.GetById(Convert.ToInt32(hdfId.Text)); ;
                    if (result != null)
                        model = result;
                }

                // set new props for entity

                model.CalculateCode = cboCalculateCode.SelectedItem.Value;
                model.ValueType = (KpiValueType)Enum.Parse(typeof(KpiValueType), hdfValueType.Text);
                model.Name = txtName.Text;
                model.Code = txtName.Text.ToUpperString();
                model.Description = txtDescription.Text;
                model.Order = !string.IsNullOrEmpty(txtOrder.Text) ? Convert.ToInt32(txtOrder.Text) : 0;
                model.Status = chkIsActive.Checked ? KpiStatus.Active : KpiStatus.Locked;

                // check entity id
                if (model.Id > 0)
                {
                    model.EditedDate = DateTime.Now;
                    model.EditedBy = CurrentUser.User.UserName;
                    // update
                    resultModel = ArgumentController.Update(model);
                }
                else
                {
                    model.CreatedBy = CurrentUser.User.UserName;
                    model.CreatedDate = DateTime.Now;
                    model.EditedDate = DateTime.Now;
                    model.EditedBy = "";

                    // insert
                    resultModel = ArgumentController.Create(model);
                }

                if (resultModel != null)
                {
                    // show success notification
                    Dialog.ShowNotification("Lưu thành công");
                    // hide window
                    wdSetting.Hide();
                    //reset form
                    ResetForm();
                    // reload data
                    gpCriterionArgument.Reload();
                }
                else
                {
                    Dialog.ShowNotification("Lưu không thành công, mã tham số hoặc mã tính toán đã tồn tại");
                }
            }
            catch (Exception exception)
            {
                Dialog.ShowError(exception);
            }
        }

        /// <summary>
        /// Delete 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Delete(object sender, DirectEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(hdfId.Text))
                {
                    //delete
                    ArgumentController.Delete(Convert.ToInt32(hdfId.Text));
                }

                // reload data
                gpCriterionArgument.Reload();
            }
            catch (Exception exception)
            {
                Dialog.ShowError(exception);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void storeValueType_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            storeValueType.DataSource = typeof(KpiValueType).GetIntAndDescription();
            storeValueType.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void storeCalculateCode_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            storeCalculateCode.DataSource = GetCalculateCodes(50);
            storeCalculateCode.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DownloadTemplate_Click(object sender, DirectEventArgs e)
        {
            var serverPath = Server.MapPath("~/" + Constant.PathTemplate + "/" + KpiArgumentFileName);
            if (!File.Exists(serverPath))
                File.Create(serverPath);

            // get list calculate code
            var codes = GetCalculateCodes(50);

            // Create template file
            var workbook = ExcelHelper.ExportExcelTemplate<ArgumentModel>(serverPath, 2, 50);

            // Create custom column drop down
            ExcelHelper.CreateCustomDropDown(workbook, serverPath, 0, 2, 50, codes, nameof(ArgumentCalculateCodeModel.Code));

            Response.AddHeader("Content-Disposition", "attachment; filename=" + KpiArgumentFileName);
            Response.WriteFile(serverPath);
            Response.End();
        }

        /// <summary>
        /// Nhập dữ liệu file excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ImportFile(object sender, DirectEventArgs e)
        {
            try
            {
                if (fileExcel.HasFile)
                {
                    // upload file
                    var path = UploadFile(fileExcel, Constant.PathTemplate);
                    // get file path
                    path = Path.Combine(Server.MapPath("~/"), Constant.PathTemplate, path);
                    // get list import
                    var importList = ExcelHelper.ImportExcel<ArgumentModel>(path);

                    foreach (var item in importList)
                    {
                        item.Code = item.Name.ToUpperString();
                        ArgumentController.Create(item);
                    }
                }
                gpCriterionArgument.Reload();
                ResetForm();
                wdExcel.Hide();
                Dialog.ShowNotification("Lưu thành công");
            }
            catch (Exception ex)
            {
                Dialog.ShowError(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExcel_Click(object sender, DirectEventArgs e)
        {
            ResetForm();
            wdExcel.Show();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// 
        /// </summary>
        private void ResetForm()
        {
            fileExcel.Reset();
            txtSheetName.Reset();
            txtName.Reset();
            txtDescription.Reset();
            chkIsActive.Checked = false;
            cboCalculateCode.Reset();
            txtOrder.Reset();
            hdfValueType.Reset();
            cboValueType.Clear();
        }

        /// <summary>
        /// get unused caculated code
        /// </summary>
        /// <returns></returns>
        private static List<ArgumentCalculateCodeModel> GetCalculateCodes(int num)
        {
            var codes = new List<ArgumentCalculateCodeModel>();

            var argument = ArgumentController.GetAll(null, false, null, null, null, null);
            for (var i = 1; i < num + argument.Count; i++)
            {
                codes.Add(new ArgumentCalculateCodeModel { Code = i.ToExcelColumnName() });
            }

            //check calculateCode exist
            var argumentCodes = argument.Select(a => a.CalculateCode).ToList();

            codes.RemoveAll(o => argumentCodes.Contains(o.Code));

            return codes;
        }

        #endregion

    }
}