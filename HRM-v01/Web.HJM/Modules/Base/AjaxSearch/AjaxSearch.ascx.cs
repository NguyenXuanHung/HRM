using System;
using System.Web.UI;
using Ext.Net;
using WebUI.Entity;
using WebUI.Controller;
using DataController;
using Web.Core.Framework;

namespace Web.HRM.Modules.UserControl
{
    public partial class Modules_Base_ComboAjaxSearch_AjaxSearch : BaseAjaxSearch, IBaseControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.AllowInsert)
            {
                AddWindow();
            }
            if (!X.IsAjaxRequest)
            {
                if (LabelAlign == Ext.Net.LabelAlign.Right)
                {
                    cContainerAjaxSearch.LabelAlign = Ext.Net.LabelAlign.Right;
                }
                if (LabelAlign == Ext.Net.LabelAlign.Top)
                {
                    cContainerAjaxSearch.Height = 60;
                    cContainerAjaxSearch.LabelAlign = Ext.Net.LabelAlign.Top;
                }
                if (string.IsNullOrEmpty(this.FieldNote))
                    cbAjaxSearch.Note = this.FieldNote;
                cbAjaxSearch.FieldLabel = FieldLabel;
                if (Width == 0)
                {
                    cbAjaxSearch.AnchorHorizontal = "100%";
                }
                else if (Width > 0 && Width <= 1)
                {
                    cbAjaxSearch.AnchorHorizontal = (100 * Width) + "%";
                }
                else if (Width > 1)
                {
                    cbAjaxSearch.Width = (int)Width;
                }
                cbAjaxSearch.PageSize = PageSize != 0 ? PageSize : 5;
                cbAjaxSearch.DisplayField = DisplayField;
                cbAjaxSearch.ValueField = ValueField;
                cbAjaxSearch.Disabled = this.Disable;
                //add param to store
                Store1.BaseParams.Add(new Ext.Net.Parameter("Table", this.TableName));
                Store1.BaseParams.Add(new Ext.Net.Parameter("SearchField", this.SearchField));
                Store1.BaseParams.Add(new Ext.Net.Parameter("ValueField", this.ValueField));
                Store1.BaseParams.Add(new Ext.Net.Parameter("DisplayField", this.DisplayField));

                //add field to store
                JsonReader json = new JsonReader();
                json.Root = "plants";
                json.TotalProperty = "total";
                json.Fields.Add(new RecordField(DisplayField));
                json.Fields.Add(new RecordField(ValueField));
                json.Fields.Add(new RecordField(SearchField));
                Store1.Reader.Add(json);

                //set template for ComBoBox
                //            cbAjaxSearch.Template.Html = @"
                //               <tpl for=""."">
                //                  <div class=""search-item"">
                //                     <h3><span>{" + ValueField + "}</span>{" + DisplayField + "}</h3>{" + SearchField + "}</div></tpl>";
                cbAjaxSearch.Template.Html = @"
               <tpl for=""."">
                  <div class=""search-item"">
                     <h3>{" + DisplayField + "}</h3>{" + SearchField + "}</div></tpl>";
                if (!AllowInsert)
                {
                    cbAjaxSearch.Triggers.RemoveAt(1);
                }
                SetJs();
            }
        }

        private void SetJs()
        {
            try
            {
                if (AllowInsert)
                {
                    Modules_Base_Form_Form ct = plcInput.Controls[0] as Modules_Base_Form_Form;
                    cbAjaxSearch.Listeners.TriggerClick.Handler = "if (index == 0) { this.clearValue(); this.triggers[0].hide(); } else{" + ct.GetWindowClientID() + ".show();}";
                }
                else
                {
                    cbAjaxSearch.Listeners.TriggerClick.Handler = "if (index == 0) { this.clearValue(); this.triggers[0].hide(); }";
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowError("AjaxSearch.ascx/SetJs() = " + ex.Message);
            }
        }

        private void AddWindow()
        {
            Control ct = this.Page.LoadControl("../Base/Form/Form.ascx");
            if (string.IsNullOrEmpty(AjaxSearchName))
            {
                ct.ID = this.ID + "frmAddWindow";
            }
            else
            {
                ct.ID = AjaxSearchName;
            }
            plcInput.Controls.Add(ct);
            var frm = ct as BaseForm;
            frm.TableName = this.TableName;
            frm.WindowHidden = true;
            frm.AllowChangeTable = false;
            frm.CommandButton = BaseForm.Command.insert;
            frm.FormType = BaseForm.FormKind.Window;

            //Tiến hành update lại bảng nếu thay đổi Table
            if (CurrentUser.User.IsSuperUser)
            {
                FormInfo frmInfo = FormController.GetInstance().GetForm(ct.ID);
                if (frmInfo != null)
                    if (frmInfo.TableName != this.TableName)
                    {
                        frmInfo.DeleteFormElement();
                        frmInfo.TableName = this.TableName;
                        FormController.GetInstance().UpdateForm(frmInfo);
                    }
            }
        }

        public string GetID()
        {
            return this.ID;
        }

        public object GetValue()
        {
            if (string.IsNullOrEmpty(hdfValue.Text) == false)
                return hdfValue.Text;
            return cbAjaxSearch.Value;
        }

        public void SetValue(object value)
        {
            if (value != null)
            {
                cbAjaxSearch.Value = value.ToString();
                hdfValue.Text = value.ToString();
                object display = DataHandler.GetInstance().ExecuteScalar("select top 1 " + this.DisplayField + " from " + this.TableName + " where " + this.ValueField + " = N'" + value + "'");
                if (display != null)
                    cbAjaxSearch.Text = display.ToString();
            }
            else
            {
                cbAjaxSearch.Text = "";
                hdfValue.Text = "";
            }
        }

        public override string ToString()
        {
            return "Dth.Net.AjaxSearch";
        }
    }

}

