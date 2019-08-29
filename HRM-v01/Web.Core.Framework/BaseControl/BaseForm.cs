using System.Web.UI;
using WebUI.Entity;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for BaseForm
    /// </summary>
    public class BaseForm : BaseUserControl
    {
        public const string InputControl_TrueFalseComboBox = "TrueFalseComboBox";
        public const string InputControl_ComboBox = "ComboBox";
        public const string InputControl_AjaxSearch = "AjaxSearch";
        public const string InputControl_RadioButton = "RadioButton";
        public const string InputControl_UserControl = "UserControl";
        public const string InputControl_TreePanel = "TreePanel";
        public const string InputControl_HTMLInput = "HTMLInput";
        public const string InputControl_Image = "Image";
        public const string InputControl_TextArea = "TextArea";

        public FormInfo formInfo;

        public bool AllowBorder { get; set; }

        public bool AllowHeader { get; set; }

        public bool AllowChangeTable { get; set; }

        public bool WindowHidden { get; set; }

        public Command CommandButton { get; set; }

        public FormKind FormType { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public string TableName { get; set; }

        public string FormName { get; set; }

        public string GridPanelName { get; set; }

        public string PrimaryColumnName { get; set; }

        public string PrimaryColumnValue { get; set; }

        public string FormReference { get; set; }

        public string XmlConfigUrl { get; set; }

        public object SCOPE_IDENTITY { get; set; }

        public AccessHistory AccessHistory { get; set; }

        public virtual Control GetComponent(string controlId)
        {
            return null;
        }

        public enum Command
        {
            Display,
            Update,
            Insert,
        }

        public enum FormKind
        {
            Form,
            Window,
        }
    }
}

