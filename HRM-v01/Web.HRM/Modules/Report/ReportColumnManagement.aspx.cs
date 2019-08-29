using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Ext.Net;
using Web.Core;
using Web.Core.Framework;
using Web.Core.Framework.Report;

namespace Web.HRM.Modules.Report
{
    public partial class ReportColumnManagement : BasePage
    {
        // order of grid
        private const string Order = "[Type], [ParentId], [Order]";
        private const string PreviewUrl = "/Modules/Report/DynamicReportView.aspx?reportId={0}";

        /// <summary>
        /// Page load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ExtNet.IsAjaxRequest)
            {
                // init data
                hdfReportId.Text = Request.QueryString["reportId"];
                hdfOrder.Text = Order;
                var reportDynamic = ReportDynamicController.GetById(int.Parse(hdfReportId.Text));
                if (reportDynamic != null)
                {
                    hdfHeaderWidth.Text = reportDynamic.HeaderWidth.ToString();
                    hdfFooterWidth.Text = reportDynamic.FooterWidth.ToString();
                    hdfReportWidth.Text = reportDynamic.Width.ToString();
                }
            }

            var win = new Window
            {
                ID = "WindowPreview",
                Title = "Mẫu báo cáo",
                Width = Unit.Pixel(1200),
                Height = Unit.Pixel(600),
                Modal = true,
                Collapsible = true,
                Maximizable = true,
                Hidden = true,
            };
            
            win.AutoLoad.Url = PreviewUrl.FormatWith(hdfReportId.Text);
            win.AutoLoad.Mode = LoadMode.IFrame;

            win.Render(Form);
        }

        /// <summary>
        /// Init setting window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InitWindow(object sender, DirectEventArgs e)
        {
            try
            {
                // init id
                var param = e.ExtraParams["Id"];

                // parse id
                if (int.TryParse(param, out var id))
                {
                    // init window props
                    if (id > 0)
                    {
                        // edit
                        wdSetting.Title = @"Sửa";
                        wdSetting.Icon = Icon.Pencil;
                    }
                    else
                    {
                        // insert
                        wdSetting.Title = @"Thêm mới";
                        wdSetting.Icon = Icon.Add;
                    }

                    // init id
                    hdfId.Text = id.ToString();

                    // init object
                    var model = new ReportColumnModel();

                    // check id
                    if (id > 0)
                    {
                        var result = ReportColumnController.GetById(id);
                        if (result != null)
                            model = result;
                    }

                    // set props
                    txtName.Text = model.Name;
                    hdfFieldName.Text = model.FieldName;
                    cboFieldName.Text = model.FieldName;
                    hdfTextAlign.Text = ((int)model.TextAlign).ToString();
                    cboTextAlign.Text = model.TextAlignName;
                    txtFontSize.Text = model.FontSize.ToString();
                    txtFormat.Text = model.Format;
                    hdfParentId.Text = model.ParentId.ToString();
                    cboParent.Text = model.ParentName;
                    txtWidth.Text = model.Width.ToString();
                    txtHeight.Text = model.Height.ToString();
                    txtOrder.Text = model.Order.ToString();
                    hdfIsGroup.Text = model.IsGroup.ToString();
                    cboGroup.Text = model.GroupName;
                    hdfDataType.Text = ((int)model.DataType).ToString();
                    cboDataType.Text = model.DataTypeName;
                    hdfStatus.Text = ((int)model.Status).ToString();
                    cboStatus.Text = model.StatusName;
                    hdfType.Text = ((int)model.Type).ToString();
                    cboType.Text = model.TypeName;
                    hdfSummaryRunning.Text = ((int)model.SummaryRunning).ToString();
                    cboSummaryRunning.Text = model.SummaryRunningName;
                    hdfSummaryFunction.Text = ((int)model.SummaryFunction).ToString();
                    cboSummaryFunction.Text = model.SummaryFunctionName;
                    txtValue.Text = model.SummaryValue;

                    // show window
                    wdSetting.Show();
                }
            }
            catch (Exception exception)
            {
                Dialog.ShowError(exception);
            }
        }

        /// <summary>
        /// Insert or Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InsertOrUpdate(object sender, DirectEventArgs e)
        {
            try
            {
                // init entity
                var model = new ReportColumnModel();
                ReportColumnModel resultModel;

                // check id
                if (!string.IsNullOrEmpty(hdfId.Text) && Convert.ToInt32(hdfId.Text) > 0)
                {
                    var result = ReportColumnController.GetById(Convert.ToInt32(hdfId.Text));
                    if (result != null)
                        model = result;
                }

                // set new props for entity
                model.ReportId = Convert.ToInt32(hdfReportId.Text);
                model.Name = txtName.Text;
                model.FieldName = hdfFieldName.Text;
                model.TextAlign = (ReportTextAlign)Convert.ToInt32(hdfTextAlign.Text);
                model.FontSize = !string.IsNullOrEmpty(txtFontSize.Text)
                    ? Convert.ToInt32(txtFontSize.Text) : ReportHelper.DefaultFontSize;
                model.Format = txtFormat.Text;
                model.ParentId = !string.IsNullOrEmpty(hdfParentId.Text)
                    ? Convert.ToInt32(hdfParentId.Text) : 0;
                model.Width = !string.IsNullOrEmpty(txtWidth.Text)
                    ? Convert.ToInt32(txtWidth.Text) : ReportHelper.DefaultCellHeight;
                model.Height = !string.IsNullOrEmpty(txtHeight.Text)
                    ? Convert.ToInt32(txtHeight.Text) : 0;
                model.Order = !string.IsNullOrEmpty(txtOrder.Text)
                    ? Convert.ToInt32(txtOrder.Text) : 0;
                model.IsGroup = Convert.ToBoolean(hdfIsGroup.Text);
                model.DataType = (ReportColumnDataType)Convert.ToInt32(hdfDataType.Text);
                model.Status = (ReportColumnStatus)Convert.ToInt32(hdfStatus.Text);
                model.Type = (ReportColumnType)Convert.ToInt32(hdfType.Text);
                model.SummaryRunning = (ReportSummaryRunning)Convert.ToInt32(hdfSummaryRunning.Text);
                model.SummaryFunction = (ReportSummaryFunction)Convert.ToInt32(hdfSummaryFunction.Text);
                model.SummaryValue = txtValue.Text;

                // check entity id
                if (model.Id > 0)
                {
                    // update
                    resultModel = ReportColumnController.Update(model);
                }
                else
                {
                    // insert
                    resultModel = ReportColumnController.Create(model);
                }

                // check result
                if (resultModel != null)
                {
                    Dialog.ShowNotification("Lưu thành công");
                    // hide window
                    wdSetting.Hide();
                    // reload data
                    gpReportColumn.Reload();
                }
                else
                {
                    Dialog.ShowNotification("Độ rộng của cột không hợp lệ");
                }
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
        protected void MultipleUpdate(object sender, DirectEventArgs e)
        {
            var ids = hdfIds.Text.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var id in ids)
            {
                var reportColumn = ReportColumnController.GetById(int.Parse(id));
                if (reportColumn == null) continue;

                // set new props for model
                if (!string.IsNullOrEmpty(hdfTextAlign2.Text))
                    reportColumn.TextAlign = (ReportTextAlign)Convert.ToInt32(hdfTextAlign2.Text);
                if (!string.IsNullOrEmpty(txtFontSize2.Text))
                    reportColumn.FontSize = Convert.ToInt32(txtFontSize2.Text);
                if (!string.IsNullOrEmpty(txtFormat2.Text))
                    reportColumn.Format = txtFormat2.Text;
                if (!string.IsNullOrEmpty(txtWidth2.Text))
                    reportColumn.Width = Convert.ToInt32(txtWidth2.Text);
                if (!string.IsNullOrEmpty(txtHeight2.Text))
                    reportColumn.Height = Convert.ToInt32(txtHeight2.Text);
                if (!string.IsNullOrEmpty(hdfDataType2.Text))
                    reportColumn.DataType = (ReportColumnDataType)Convert.ToInt32(hdfDataType2.Text);
                if (!string.IsNullOrEmpty(hdfIsGroup2.Text))
                    reportColumn.IsGroup = Convert.ToBoolean(hdfIsGroup2.Text);
                if (!string.IsNullOrEmpty(hdfSummaryFunction2.Text))
                    reportColumn.SummaryRunning = (ReportSummaryRunning)Convert.ToInt32(hdfSummaryRunning2.Text);
                if (!string.IsNullOrEmpty(hdfSummaryFunction2.Text))
                    reportColumn.SummaryFunction = (ReportSummaryFunction)Convert.ToInt32(hdfSummaryFunction2.Text);

                // update model
                ReportColumnController.Update(reportColumn);
            }

            // hide window
            wdMultipleSetting.Hide();

            // reload data
            gpReportColumn.Reload();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InitMultipleEditWindow(object sender, DirectEventArgs e)
        {
            // clear control
            cboTextAlign2.Reset();
            hdfTextAlign2.Reset();
            txtFontSize2.Reset();
            txtFormat2.Reset();
            txtWidth2.Reset();
            txtHeight2.Reset();
            cboDataType2.Reset();
            hdfDataType2.Reset();

            // show window
            wdMultipleSetting.Show();
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
                // init id
                var param = e.ExtraParams["Id"];

                // parse id
                if (!int.TryParse(param, out var id) || id <= 0)
                {
                    // parse error, show error
                    Dialog.ShowError("Tham số không hợp lệ");
                    return;
                }

                // delete
                ReportColumnController.Delete(id);

                // reload data
                gpReportColumn.Reload();
            }
            catch (Exception exception)
            {
                Dialog.ShowError(exception);
            }
        }

        /// <summary>
        /// Redirect back
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RedirectBack(object sender, DirectEventArgs e)
        {
            try
            {
                // redirect
                Response.Redirect("~/Modules/Report/DynamicReportManagement.aspx", true);
            }
            catch (Exception exception)
            {
                Dialog.ShowError(exception);
            }
        }

        /// <summary>
        /// Nhân đôi cột
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InitWindowDuplicateData(object sender, DirectEventArgs e)
        {
            try
            {
                // init id
                var param = e.ExtraParams["Id"];

                // parse id
                if (int.TryParse(param, out var id))
                {
                    // insert
                    wdSetting.Title = @"Thêm mới";
                    wdSetting.Icon = Icon.Add;

                    // init id
                    hdfId.Text = "0";

                    // init object
                    var model = new ReportColumnModel();

                    // check id
                    if (id > 0)
                    {
                        var result = ReportColumnController.GetById(id);
                        if (result != null)
                            model = result;
                    }

                    // set props
                    txtName.Text = string.Empty;
                    hdfFieldName.Text = model.FieldName;
                    cboFieldName.Text = model.FieldName;
                    hdfTextAlign.Text = ((int)model.TextAlign).ToString();
                    cboTextAlign.Text = model.TextAlignName;
                    txtFontSize.Text = model.FontSize.ToString();
                    txtFormat.Text = model.Format;
                    hdfParentId.Text = model.ParentId.ToString();
                    cboParent.Text = model.ParentName;
                    txtWidth.Text = model.Width.ToString();
                    txtHeight.Text = model.Height.ToString();
                    txtOrder.Text = model.Order.ToString();
                    hdfIsGroup.Text = model.IsGroup.ToString();
                    cboGroup.Text = model.GroupName;
                    hdfDataType.Text = ((int)model.DataType).ToString();
                    cboDataType.Text = model.DataTypeName;
                    hdfStatus.Text = ((int)model.Status).ToString();
                    cboStatus.Text = model.StatusName;
                    hdfType.Text = ((int)model.Type).ToString();
                    cboType.Text = model.TypeName;
                    hdfSummaryRunning.Text = ((int)model.SummaryRunning).ToString();
                    cboSummaryRunning.Text = model.SummaryRunningName;
                    hdfSummaryFunction.Text = ((int)model.SummaryFunction).ToString();
                    cboSummaryFunction.Text = model.SummaryFunctionName;
                    txtValue.Text = model.SummaryValue;

                    // show window
                    wdSetting.Show();
                }
            }
            catch (Exception exception)
            {
                Dialog.ShowError(exception);
            }
        }

        #region Store

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void storeFieldName_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            if (!string.IsNullOrEmpty(hdfReportId.Text) && int.TryParse(hdfReportId.Text, out var reportId) && reportId > 0)
            {
                var model = ReportDynamicController.GetById(reportId);
                if (model.Id > 0 || !string.IsNullOrEmpty(model.ReportSource))
                {
                    // store report template
                    storeFieldName.DataSource = JSON.Deserialize<List<ListItemModel>>(model.ReportSource);
                    storeFieldName.DataBind();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void storeReportTextAlign_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            // store report template
            storeReportTextAlign.DataSource = typeof(ReportTextAlign).GetIntAndDescription();
            storeReportTextAlign.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void storeReportColumnType_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            storeReportColumnType.DataSource = typeof(ReportColumnType).GetIntAndDescription().OrderBy(r => new List<int> { 1, 3, 5, 4, 2 }.IndexOf(r.Key));
            storeReportColumnType.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void storeReportColumnDataType_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            storeReportColumnDataType.DataSource = typeof(ReportColumnDataType).GetIntAndDescription();
            storeReportColumnDataType.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void storeReportColumnStatus_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            storeReportColumnStatus.DataSource = typeof(ReportColumnStatus).GetIntAndDescription();
            storeReportColumnStatus.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void storeReportSummaryRunning_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            storeReportSummaryRunning.DataSource = typeof(ReportSummaryRunning).GetIntAndDescription();
            storeReportSummaryRunning.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void storeReportSummaryFunction_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            storeReportSummaryFunction.DataSource = typeof(ReportSummaryFunction).GetIntAndDescription();
            storeReportSummaryFunction.DataBind();
        }

        #endregion

    }
}