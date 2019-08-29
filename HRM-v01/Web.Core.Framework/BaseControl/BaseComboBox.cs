using Ext.Net;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for BaseComboBox
    /// </summary>
    public class BaseComboBox : BaseUserControl
    {
        public LabelAlign LabelAlign { get; set; }

        public string FieldLabel { get; set; }

        public string FieldNote { get; set; }

        public string ComboName { get; set; }

        public double Width { get; set; }

        public string DisplayField { get; set; }

        public string SecondDisplayField { get; set; }

        public string ValueField { get; set; }

        public string TableName { get; set; }

        public string Where { get; set; }

        public bool AllowInsert { get; set; }

        public bool Disable { get; set; }

        public int PageSize { get; set; }

        public int TabIndex { get; set; }
    }
}

