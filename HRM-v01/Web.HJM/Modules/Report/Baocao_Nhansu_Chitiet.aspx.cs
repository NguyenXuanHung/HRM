using System;
using DevExpress.XtraReports.UI;
using Web.Core.Framework;
using Web.Core.Framework.Report;

namespace Web.HJM.Modules.Report
{
    public partial class Baocao_Nhansu_Chitiet : global::System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Report_baocao_nhansu_chitiet.Report = CreateNewReport();
        }

        public XtraReport CreateNewReport()
        {
            // sơ yếu lý lịch cán bộ v1
            var info = new rp_InformationEmployeeDetail();
            try
            {

                var rp = new Filter();
                var prkey = int.Parse(Request.QueryString["prkey"]);
                var hs = RecordController.GetById(prkey);
                rp.RecordId = hs.Id;
                info.BindData(hs);
            }
            catch (Exception ex)
            {
                Dialog.ShowError("Có lỗi xảy ra " + ex.Message);
            }
            return info;
        }
    }

}

