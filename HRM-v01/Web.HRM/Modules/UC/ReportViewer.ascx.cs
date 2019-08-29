using System;
using System.Web.UI.WebControls;
using DevExpress.XtraReports.UI;

namespace Web.HRM.Modules.UC
{
    public partial class ReportViewer : global::System.Web.UI.UserControl
    {
        public XtraReport Report
        {
            get { return ReportViewer1.Report; }
            set { ReportViewer1.Report = value; }
        }

        public bool ReportToolbarsAutoWidth
        {
            get { return ReportToolbar1.Width == Unit.Percentage(100) || ReportToolbar1.Width == Unit.Percentage(100); }
            set { ReportToolbar1.Width = ReportToolbar1.Width = (value ? Unit.Percentage(100) : Unit.Empty); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

            XRLabel xrl_TENHETHONG = new XRLabel();
            ReportToolbar1.ReportViewer = ReportViewer1;
            ReportToolbar2.ReportViewer = ReportViewer1;
            //  DataBind();
        }
    }

}

