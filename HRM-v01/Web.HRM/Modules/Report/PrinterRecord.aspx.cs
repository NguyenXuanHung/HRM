using System;
using Web.Core.Framework;

namespace Web.HRM.Modules.Report
{
    public partial class PrinterRecord : global::System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            reportEmployeeSelected.Report = CreateReport();
        }

        public DevExpress.XtraReports.UI.XtraReport CreateReport()
        {
            // TODO: Sửa lổi report filter
            return null;
            //var lyLich = new rp_PrintCurriculumVitae();
            //try
            //{
            //    var filter = new ReportFilter();
            //    var recordIds = Request.QueryString["ListRecordId"];

            //    filter.SessionDepartment = Request.QueryString["departments"];
            //    filter.RecordIds = recordIds;
            //    lyLich.BindData(filter);
            //}
            //catch (Exception ex)
            //{
            //    Dialog.ShowError("Có lỗi xảy ra " + ex.Message);
            //}
            //return lyLich;
        }
    }
}
