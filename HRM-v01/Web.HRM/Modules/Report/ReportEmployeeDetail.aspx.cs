using System;
using System.Web.UI;
using DevExpress.XtraReports.UI;
using Web.Core.Framework;
using Web.Core.Framework.Interface;
using Web.Core.Framework.Report;

namespace Web.HRM.Modules.Report
{
    public partial class ReportPrint : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // create report
            var report = CreateReport();

            // check create result
            if (report != null)
            {
                // create report success
                reportViewer.Report = report;

            }
        }

        /// <summary>
        /// Create report
        /// </summary>
        /// <returns></returns>
        private XtraReport CreateReport()
        {

            // init report
            //var report = new XtraReport();
            // init report
            if (Enum.TryParse(Request.QueryString["rp"], true, out ReportTypeHJM rpType))
            {

                switch (rpType)
                {
                    //Curriculum
                    case ReportTypeHJM.CurriculumVitae:
                        var report = new rp_PrintCurriculumVitae();
                        // get report filter
                        var filter = ((IBaseReport)report).GetFilter();

                        // set filter props
                        filter.Departments = Request.QueryString["departments"];
                        filter.RecordIds = Request.QueryString["recordIds"];
                        filter.ReportDate = DateTime.Now;
                        // update filter report
                        ((IBaseReport)report).SetFilter(filter);

                        // bind data
                        ((IBaseReport)report).BindData();
                        return report;
                    
                    //info employee detail 2C
                    case ReportTypeHJM.InfoEmployeeDetail:
                        var info = new rp_InformationEmployeeDetail();
                        var recordId = int.Parse(Request.QueryString["recordId"]);
                        var record = RecordController.GetById(recordId);
                        info.BindData(record);
                        return info;

                    //info employee detail 2C - 98
                    case ReportTypeHJM.InfoEmployeeDetailV2:
                        var infoDetail = new rp_InformationEmployeeDetail_V2();
                        var recordIdV2 = int.Parse(Request.QueryString["recordId"]);
                        var recordV2 = RecordController.GetById(recordIdV2);
                        infoDetail.BindData(recordV2);
                        return infoDetail;
                }
            }

            // return report
            return null;
        }
    }
}