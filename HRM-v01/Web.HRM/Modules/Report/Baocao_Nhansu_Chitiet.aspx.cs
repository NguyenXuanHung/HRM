using System;
using DevExpress.XtraReports.UI;
using Web.Core.Framework;

namespace Web.HRM.Modules.Report
{
    public partial class Baocao_Nhansu_Chitiet : global::System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Report_baocao_nhansu_chitiet.Report = CreateNewReport();
        }

        public XtraReport CreateNewReport()
        {
            // TODO: Sửa lổi report filter
            return null;
            //var nsct = new rp_InformationEmployeeDetail_V2();
            //try
            //{

            //    var rp = new ReportFilter();
            //    var prkey = int.Parse(Request.QueryString["prkey"]);
            //    var hs = RecordController.GetById(prkey);
            //    rp.RecordId = hs.Id;
            //    nsct.BindData(hs);
            //}
            //catch (Exception ex)
            //{
            //    Dialog.ShowError("Có lỗi xảy ra " + ex.Message);
            //}
            //return nsct;
        }
    }

}

