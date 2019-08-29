using System;
using Web.Core.Framework;
using Web.Core.Framework.Report;

namespace Web.HJM.Modules.Report
{
    public partial class PrinterRecord : global::System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            reportEmployeeSelected.Report = CreateReport();
        }

        public DevExpress.XtraReports.UI.XtraReport CreateReport()
        {
            var curriculum = new rp_PrintCurriculumVitae();
            try
            {

                var filter = new Filter();
                var recordIds = Request.QueryString["ListRecordId"];

                filter.Departments = Request.QueryString["departments"];
                filter.RecordIds = recordIds;
                curriculum.BindData();
            }
            catch (Exception ex)
            {
                Dialog.ShowError("Có lỗi xảy ra " + ex.Message);
            }
            return curriculum;
        }
    }
}
