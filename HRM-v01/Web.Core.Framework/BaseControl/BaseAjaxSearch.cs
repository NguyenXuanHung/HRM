using Ext.Net;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for BaseAjaxSearch
    /// </summary>
    public class BaseAjaxSearch : BaseUserControl
    {
        public LabelAlign LabelAlign { get; set; }

        public string FieldLabel { get; set; }

        public string AjaxSearchName { get; set; }

        public string FieldNote { get; set; }

        public int PageSize { get; set; }

        public double Width { get; set; }

        public string DisplayField { get; set; }

        public string ValueField { get; set; }

        public string TableName { get; set; }

        public string SearchField { get; set; }

        public bool Disable { get; set; }

        public bool AllowInsert { get; set; }

        public short TabIndex { get; set; }
    }
}

