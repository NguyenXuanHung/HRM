using System;
using DevExpress.XtraReports.UI;
using Web.Core.Framework;
using Web.Core.Framework.Report;

namespace Web.HJM.Modules.Report
{
    public partial class ReportEmployeeDetailV2 : global::System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            rpEmployeeDetailV2.Report = CreateNewReport();
        }

        public XtraReport CreateNewReport()
        {
            // sơ yếu lý lịch cán bộ v2
            var info = new rp_InformationEmployeeDetail_V2();
            try
            {
                var rp = new Filter();
                var recordId = int.Parse(Request.QueryString["recordId"]);
                var record = RecordController.GetById(recordId);
                rp.RecordId = record.Id;
                info.BindData(record);
            }
            catch (Exception ex)
            {
                Dialog.ShowError("Có lỗi xảy ra " + ex.Message);
            }
            return info;
        }
    }

}

