using System;
using System.Globalization;
using Ext.Net;
using Web.Core.Framework;
using System.Linq;
using Web.Core;
using Web.Core.Framework.Common;
using Web.Core.Framework.Controller.TimeSheet;
using Web.Core.Object.Security;

namespace Web.HRM.Modules.TimeSheet
{
    public partial class TimeSheetRuleWrongTimeManagement : BasePage
    {
        /// <summary>
        /// page load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!ExtNet.IsAjaxRequest)
            {
                var groupSymbol = TimeSheetGroupSymbolController.GetByGroup(Constant.TimesheetLate);
                hdfGroupSymbolId.Text = groupSymbol?.Id.ToString() ?? "0";
            }
        }

        /// <summary>
        /// init setting window
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
                if(int.TryParse(param, out var id))
                {
                    // init window props
                    if(id > 0)
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
                    var model = new TimeSheetRuleWrongTimeModel(null);

                    // check id
                    if(id > 0)
                    {
                        var result = TimeSheetRuleWrongTimeController.GetById(id);
                        if(result != null)
                            model = result;
                    }

                    // set props
                    txtFromMinute.Text = model.FromMinute.ToString();
                    txtToMinute.Text = model.ToMinute.ToString();
                    txtWorkConvert.Text = model.WorkConvert.ToString(CultureInfo.InvariantCulture).Replace("-", "");
                    txtTimeConvert.Text = model.TimeConvert.ToString("0.0");
                    cbxType.SelectedItem.Value = model.Type.ToString();
                    cbxType.Text = model.TypeName;
                    hdfType.Text = ((int)model.Type).ToString();
                    txtOrder.Text = model.Order.ToString();
                    hdfSymbolId.Text = model.SymbolId.ToString();
                    model.IsMinus = chkIsMinus.Checked;
                    cbxSymbol.Text = model.SymbolName;
                    // show window
                    wdSetting.Show();
                }
            }
            catch(Exception exception)
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
                var model = new TimeSheetRuleWrongTimeModel();

                // check id
                if(!string.IsNullOrEmpty(hdfId.Text) && Convert.ToInt32(hdfId.Text) > 0)
                {
                    var result = TimeSheetRuleWrongTimeController.GetById(Convert.ToInt32(hdfId.Text));
                    if(result != null)
                        model = result;
                }

                // set new props for entity
                model.FromMinute = Convert.ToInt32(txtFromMinute.Text);
                model.ToMinute = Convert.ToInt32(txtToMinute.Text);
                model.Order = Convert.ToInt32(txtOrder.Text);
                model.IsMinus = chkIsMinus.Checked;
                model.SymbolId = Convert.ToInt32(hdfSymbolId.Text);
                model.TimeConvert = !string.IsNullOrEmpty(txtTimeConvert.Text)
                    ? Convert.ToDouble(txtTimeConvert.Text) : 0;

                model.WorkConvert = !string.IsNullOrEmpty(txtWorkConvert.Text)
                    ? Convert.ToDouble(txtWorkConvert.Text) : 0;
                if (!string.IsNullOrEmpty(hdfType.Text))
                {
                    model.Type = (TimeSheetRuleWrongTimeType) Enum.Parse(typeof(TimeSheetRuleWrongTimeType), hdfType.Text);
                }
                // excute
                if(model.Id > 0)
                {
                    // update
                    TimeSheetRuleWrongTimeController.Update(model);
                }
                else
                {
                    // insert
                    TimeSheetRuleWrongTimeController.Create(model);
                }

                // hide window
                wdSetting.Hide();

                // reload data
                gpTimeSheetRuleEarlyOrLate.Reload();
            }
            catch(Exception exception)
            {
                Dialog.ShowError(exception);
            }
        }


        /// <summary>
       /// delete
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
                if(!int.TryParse(param, out var id) || id <= 0)
                {
                    Dialog.ShowError("Có lỗi xảy ra trong quá trình xử lý");
                    return;
                }

                // delete
                TimeSheetRuleWrongTimeController.Delete(id);

                // reload data
                gpTimeSheetRuleEarlyOrLate.Reload();
            }
            catch(Exception exepException)
            {
                Dialog.ShowError(exepException);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        [DirectMethod]
        public void SetValueSelectSymbol()
        {
            if(!string.IsNullOrEmpty(hdfSymbolId.Text))
            {
                var symbol = TimeSheetRuleWrongTimeController.GetById(Convert.ToInt32(hdfSymbolId.Text));
                if(symbol != null)
                {
                    txtWorkConvert.Text = symbol.WorkConvert.ToString("0.0");
                    txtTimeConvert.Text = symbol.TimeConvert != null ? symbol.TimeConvert.ToString() : "0";
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void storeType_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            storeType.DataSource = typeof(TimeSheetRuleWrongTimeType).GetIntAndDescription();
            storeType.DataBind();
        }

    }
}
