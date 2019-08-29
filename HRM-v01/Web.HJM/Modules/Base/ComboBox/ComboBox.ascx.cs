using System;
using System.Web.UI;
using Ext.Net;
using DataController;
using Web.Core.Framework;
using WebUI.Entity;
using WebUI.Controller;

namespace Web.HRM.Modules.UserControl
{
    public partial class Modules_Base_ComboBox_ComboBox : BaseComboBox, IBaseControl
    {
        public int SelectedIndex { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (AllowInsert)
            {
                AddWindow();
            }

            if (!ExtNet.IsAjaxRequest)
            {
                // cbBox.SelectedIndex = 0;
                if (this.LabelAlign == Ext.Net.LabelAlign.Top)
                {
                    cContainerComboBox.LabelAlign = Ext.Net.LabelAlign.Top;
                }
                else if (this.LabelAlign == Ext.Net.LabelAlign.Right)
                {
                    cContainerComboBox.LabelAlign = Ext.Net.LabelAlign.Right;
                }

                if (string.IsNullOrEmpty(this.FieldNote) == false)
                    cbBox.Note = this.FieldNote;
                cbBox.DisplayField = this.DisplayField;
                cbBox.ValueField = this.ValueField;
                cbBox.Disabled = this.Disable;
                cbBox.TabIndex = (short) this.TabIndex;
                //if (PageSize != 0)
                //{
                //    cbBox.PageSize = this.PageSize;
                //} 
                //add param to store
                Store1.BaseParams.Add(new Ext.Net.Parameter("TableName", this.TableName));
                Store1.BaseParams.Add(new Ext.Net.Parameter("DisplayField", this.DisplayField));
                Store1.BaseParams.Add(new Ext.Net.Parameter("ValueField", this.ValueField));
                Store1.BaseParams.Add(new Ext.Net.Parameter("Where", this.Where));

                //Store1.AutoLoadParams.Add(new Ext.Net.Parameter("Start","0",ParameterMode.Raw));
                //Store1.AutoLoadParams.Add(new Ext.Net.Parameter("Limit", "10", ParameterMode.Raw));

                //add field to store
                JsonReader json = new JsonReader();
                json.Root = "plants";
                json.TotalProperty = "total";
                json.Fields.Add(new RecordField(DisplayField));
                json.Fields.Add(new RecordField(ValueField));
                Store1.Reader.Add(json);

                if (Width == 0)
                {
                    cbBox.AnchorHorizontal = "100%";
                }
                else if (Width > 0 && Width <= 1)
                {
                    cbBox.ColumnWidth = Width;
                }
                else if (Width > 1)
                {
                    cbBox.Width = (int) Width;
                }

                cbBox.FieldLabel = this.FieldLabel;

                //tích hợp nút thêm mới
                if (!AllowInsert)
                {
                    cbBox.Triggers.RemoveAt(1);
                }

                SetJs();
            }
        }

        private void AddWindow()
        {
            Control ct = this.Page.LoadControl("../Base/Form/Form.ascx");
            if (string.IsNullOrEmpty(ComboName))
            {
                ct.ID = this.ID + "frmAddWindow";
            }
            else
            {
                ct.ID = ComboName;
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

        /// <summary>
        /// Thiết lập Listener for combobox
        /// </summary>
        private void SetJs()
        {
            try
            {
                cbBox.Listeners.Expand.Handler = "if(" + hdfInsertStatus.ClientID + ".getValue()=='refresh'){" +
                                                 hdfInsertStatus.ClientID + ".setValue('');" + Store1.ClientID +
                                                 ".reload(); }";
                if (AllowInsert)
                {
                    var ct = plcInput.Controls[0] as Modules_Base_Form_Form;
                    cbBox.Listeners.TriggerClick.Handler =
                        "if (index == 0) { this.clearValue(); this.triggers[0].hide(); } else{" +
                        ct.GetWindowClientID() + ".show();" + hdfInsertStatus.ClientID + ".setValue('refresh');}";
                }
                else
                {
                    cbBox.Listeners.TriggerClick.Handler =
                        "if (index == 0) { this.clearValue(); this.triggers[0].hide(); } else{" +
                        hdfInsertStatus.ClientID + ".setValue('refresh');}";
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowError("ComboBox.ascx/SetJs() = " + ex.Message);
            }
        }

        [DirectMethod]
        public void ShowForm()
        {
            try
            {
                if (plcInput.Controls.Count != 0)
                {
                    var window = plcInput.Controls[0] as IBaseWindow;
                    window.Show();
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowError("ComboBox.ascx/ShowForm() = " + ex.Message);
            }
        }

        public string GetID()
        {
            return this.ID;
        }

        public object GetValue()
        {
            if (string.IsNullOrEmpty(hdfValue.Text) == false)
                return hdfValue.Value;
            if (string.IsNullOrEmpty(cbBox.SelectedItem.Value))
                return "";
            return cbBox.SelectedItem.Value;
        }

        public void SetValue(object value)
        {
            try
            {
                if (value != null)
                {
                    cbBox.SetValue(value.ToString());
                    object display = DataHandler.GetInstance()
                        .ExecuteScalar("select top 1 " + this.DisplayField + " from " + this.TableName + " where " +
                                       this.ValueField + " = N'" + value + "'");
                    if (display != null)
                        cbBox.Text = display.ToString();
                    hdfValue.Text = value.ToString();
                }
                else
                {
                    cbBox.SetValue("");
                    cbBox.Text = "";
                    hdfValue.Text = "";
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowError("ComboBox/SetValue = " + ex.Message);
            }
        }

        public override string ToString()
        {
            return "Dth.Net.ComboBox";
        }
    }
}