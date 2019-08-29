using System;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Web.Core;
using Web.Core.Service.Catalog;
using Web.Core.Object.Report;
using Web.Core.Framework.Adapter;

namespace Web.Core.Framework.Report
{

    /// <summary>
    /// THỐNG KÊ, TỔNG HỢP SỐ LƯỢNG, CHẤT LƯỢNG SỐ LƯỢNG NGƯỜI LÀM VIỆC LÀ NGƯỜI DÂN TỘC THIỂU SỐ
    /// </summary>
    public class rp_QuantityEthnicMinority : XtraReport
    {
        private DetailBand Detail;
        private TopMarginBand TopMargin;
        private BottomMarginBand BottomMargin;
        private ReportHeaderBand ReportHeader;
        private PageHeaderBand PageHeader;
        private FormattingRule formattingRule1;
        private ReportFooterBand ReportFooter;
        private XRTable tblReportHeader;
        private XRTableRow xrTableRow2;
        private XRTableCell xrTableCell401;
        private XRTableCell xrLabelTinhDen;
        private XRTableCell xrTableCell403;
        private XRTableRow xrTableRow3;
        private XRTableCell xrCellLastReceivedDate;
        private XRTableCell xrCellToDate;
        private XRTableCell xrTableCell503;
        private XRTableRow xrTableRow4;
        private XRTableCell xrTableCell601;
        private XRTableCell xrTableCell602;
        private XRTable tblPageHeader;
        private XRTableRow xrTableRow11;
        private XRTableCell xrTableCell241;
        private XRTableCell xrTableCell242;
        private XRTableCell xrTableCell2;
        private XRLabel lblTrongDo;
        private XRTable xrTable3;
        private XRTableRow xrTableRow5;
        private XRTableCell xrTableCell7;
        private XRTableCell xrTableCell8;
        private XRTableCell xrTableCell3;
        private XRLabel lblNgachCongChuc;
        private XRTable xrTable4;
        private XRTableRow xrTableRow6;
        private XRTableCell xrTableCell11;
        private XRTableCell xrTableCell12;
        private XRTableCell xrTableCell5;
        private XRTableCell xrTableCell6;
        private XRTableCell xrTableCell603;
        private XRLabel lblTuoi;
        private XRTable xrTable12;
        private XRTableRow xrTableRow17;
        private XRTableCell xrTableCell42;
        private XRTableCell xrTableCell43;
        private XRTableCell xrTableCell44;
        private XRTableCell xrTableCell45;
        private XRLabel lbl51Den60;
        private XRTable xrTable13;
        private XRTableRow xrTableRow18;
        private XRTableCell xrTableCell47;
        private XRTableCell xrTableCell48;
        private XRTableCell xrTableCell49;
        private XRTableRow xrTableRow19;
        private XRTableCell xrCellTenBieuMau;
        private XRTableCell xrCellReportName;
        private XRTableCell xrCellOrganization;
        private XRTable tblDetail;
        private XRTableRow xrDetailRow1;
        private XRTableCell xrTableCellSoThuTu;
        private XRTableCell xrTableCellTenDonVi;
        private XRTableCell xrTableCellTongSoCongChuc;
        private XRTableCell xrTableCellNam;
        private XRTableCell xrTableCellNu;
        private XRTableCell xrTableCellDuoi30;
        private XRTableCell xrTableCellTu30Den40;
        private XRTableCell xrTableCellNamTu56Den60;
        private XRTableCell xrTableCellNgachCVCVaTD;
        private XRTableCell xrTableCellNgachCVVaTD;
        private XRTableCell xrTableCellNgachCSVaTD;
        private XRTableCell xrTableCellNgachConLai;
        private XRTableCell xrTableCellTu41Den50;
        private XRTableCell xrTableCellNamTu51Den55;
        private XRTableCell xrTableCellNuTu51Den55;
        private XRTableCell xrTableCellTHCS;
        private XRTableCell xrTableCellTHPT;
        private XRTableCell xrTableCellCMThacSi;
        private XRTableCell xrTableCellCMDaiHoc;
        private XRTableCell xrTableCellCMCaoDang;
        private XRTableCell xrTableCellCMTrungCap;
        private XRTableCell xrTableCellCMConLai;
        private XRTableCell xrTableCellLLCTCuNhan;
        private XRTableCell xrTableCellLLCTCaoCap;
        private XRTableCell xrTableCellLLCTTrungCap;
        private XRTableCell xrTableCellQLChuyenVienChinh;
        private XRTableCell xrTableCellQLChuyenVien;
        private XRTableCell xrTableCellQLQLGD;
        private XRTableCell xrTableCellGiay;
        private XRTableCell xrTableCellCong;
        private XRTableCell xrTableCellHoa;
        private XRTableCell xrTableCellSiLa;
        private XRTableCell xrTableCellNung;
        private XRTableCell xrTableCellCaoLan;
        private XRTableCell xrTableCellTho;
        private XRTableCell xrTableCellDanTocKhac;
        private XRTable xrTable1;
        private XRTableRow xrTableRow1;
        private XRTableCell xrTextTongSo;
        private XRTableCell xrTongNam;
        private XRTableCell xrTongNu;
        private XRTableCell xrTongDuoi30;
        private XRTableCell xrTongSoCongChuc;
        private XRTableCell xrTongNamTu56Den60;
        private XRTableCell xrTongNgachCVCVaTD;
        private XRTableCell xrTongNgachCVVaTD;
        private XRTableCell xrTongNgachCSVaTD;
        private XRTableCell xrTongNgachConLai;
        private XRTableCell xrTongNuTu51Den55;
        private XRTableCell xrTongNamTu51Den55;
        private XRTableCell xrTongTu30Den40;
        private XRTableCell xrTongTHCS;
        private XRTableCell xrTongTHPT;
        private XRTableCell xrTongCMThacSi;
        private XRTableCell xrTongCMDaiHoc;
        private XRTableCell xrTongCMCaoDang;
        private XRTableCell xrTongCMTrungCap;
        private XRTableCell xrTongCMConLai;
        private XRTableCell xrTongLLCTCuNhan;
        private XRTableCell xrTongLLCTCaoCap;
        private XRTableCell xrTongLLCTTrungCap;
        private XRTableCell xrTongQLChuyenVienChinh;
        private XRTableCell xrTongQLChuyenVien;
        private XRTableCell xrTongQLQLGD;
        private XRTableCell xrTongGiay;
        private XRTableCell xrTongTu41Den50;
        private XRTableCell xrTongHoa;
        private XRTableCell xrTongSiLa;
        private XRTableCell xrTongNung;
        private XRTableCell xrTongCaoLan;
        private XRTableCell xrTongLaHu;
        private XRTableCell xrTongDanTocKhac;
        private XRTableCell xrTongCong;
        private XRTableCell xrTableCell50;
        private XRTableCell xrTableCell51;
        private XRTableCell xrTableCell19;
        private XRTableCell xrTableCellDangVien;
        private XRTableCell xrTongDangVien;
        private XRTableCell xrTableCell4;
        private XRTable xrTable2;
        private XRTableRow xrTableRow7;
        private XRTableCell xrTableCell9;
        private XRTableCell xrTableCell10;
        private XRLabel xrLabel1;
        private XRTableCell xrTableCell13;
        private XRTableCell xrTableCell14;
        private XRTableCell xrTableCell15;
        private XRLabel xrLabel2;
        private XRTable xrTable5;
        private XRTableRow xrTableRow8;
        private XRTableCell xrTableCell16;
        private XRTableCell xrTableCell17;
        private XRTableCell xrTableCell18;
        private XRTableCell xrTableCell20;
        private XRTableCell xrTableCell21;
        private XRTable xrTable6;
        private XRTableRow xrTableRow9;
        private XRTableCell xrTableCell23;
        private XRTableCell xrTableCell24;
        private XRTableCell xrTableCell25;
        private XRLabel xrLabel3;
        private XRLabel xrLabel4;
        private XRTable xrTable7;
        private XRTableRow xrTableRow10;
        private XRTableCell xrTableCell29;
        private XRTableCell xrTableCell30;
        private XRTableCell xrTableCell31;
        private XRTableCell xrTableCell32;
        private XRTableCell xrTableCellLaHu;
        private XRTableCell xrTongTho;
        private XRTableCell xrTableCellThai;
        private XRTableCell xrTableCellMong;
        private XRTableCell xrTableCellHaNhi;
        private XRTableCell xrTableCellTay;
        private XRTableCell xrTableCellMuong;
        private XRTableCell xrTableCellDao;
        private XRTable xrTable8;
        private XRTableRow xrTableRow12;
        private XRTableCell xrTableCell1;
        private XRTableCell xrTableCell22;
        private XRTableCell xrTableCell27;
        private XRTableCell xrTableCell26;
        private XRTableCell xrTableCell33;
        private XRTableCell xrTableCell34;
        private XRTableCell xrTableCell35;
        private XRTableCell xrTableCell36;
        private XRTableCell xrTableCell38;
        private XRTableCell xrTableCell39;
        private XRTableCell xrTableCell40;
        private XRTableCell xrTableCell41;
        private XRTableCell xrTableCell46;
        private XRTableCell xrTableCell28;
        private XRTableCell xrTableCell37;
        private XRLabel xrLabel5;
        private XRTableCell xrTongThai;
        private XRTableCell xrTongMong;
        private XRTableCell xrTongHaNhi;
        private XRTableCell xrTongTay;
        private XRTableCell xrTongMuong;
        private XRTableCell xrTongDao;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        /// 
        private IContainer components = null;

        public rp_QuantityEthnicMinority()
        {
            InitializeComponent();
        }
        int STT=1;
        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrTableCellSoThuTu.Text = STT.ToString();
            STT++;
        }
        public void BindData(ReportFilter filter)
        {
            try
            {
                // get organization
                var organization = cat_DepartmentServices.GetByDepartments(filter.SessionDepartment);
                if (organization != null)
                {
                    xrLabelTinhDen.Text = string.Format(xrLabelTinhDen.Text, DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year);
                    var departments = filter.SelectedDepartment;
                    // department
                    var arrDepartment = departments.Split(new[] { ',' }, StringSplitOptions.None);
                    for (var i = 0; i < arrDepartment.Length; i++)
                    {
                        arrDepartment[i] = "'{0}'".FormatWith(arrDepartment[i]);
                    }
                    var employeeType = filter.LoaiCanBo;
                    var xTongDangVien = 0;
                    var xTongNu = 0;
                    var xTongNam = 0;
                    var xDanTocThai = 0;
                    var xDanTocMong = 0;
                    var xDanTocHaNhi = 0;
                    var xDanTocTay = 0;
                    var xDanTocMuong = 0;
                    var xDanTocDao = 0;
                    var xDanTocGiay = 0;
                    var xDanTocCong = 0;
                    var xDanTocHoa = 0;
                    var xDanTocSiLa = 0;
                    var xDanTocNung = 0;
                    var xDanTocCaoLan = 0;
                    var xDanTocLaHu = 0;
                    var xDanTocTho = 0;
                    var xDanTocKhac = 0;
                    var xTongDuoi30 = 0;
                    var xTongTu30Den40 = 0;
                    var xTongTu41Den50 = 0;
                    var xTongNuTu51Den55 = 0;
                    var xTongNamTu51Den55 = 0;
                    var xTongNamTu56Den60 = 0;
                    var xTongNgachCVC = 0;
                    var xTongNgachCV = 0;
                    var xTongNgachCS = 0;
                    var xTongNgachConLai = 0;
                    var xTongTHCS = 0;
                    var xTongTHPT = 0;
                    var xTongCMThacSi = 0;
                    var xTongCMDaiHoc = 0;
                    var xTongCMCaoDang = 0;
                    var xTongCMTrungCap = 0;
                    var xTongCMConLai = 0;
                    var xTongLLCTCuNhan = 0;
                    var xTongLLCTCaoCap = 0;
                    var xTongLLCTTrungCap = 0;
                    var xTongQLChuyenVienChinh = 0;
                    var xTongQLChuyenVien = 0;
                    var xTongQLQLGD = 0;
                   
                    var tableGroup = SQLHelper.ExecuteTable(SQLReportAdapter.GetStore_SLCLNguoiLamViecLaDanTocThieuSoCountGroupTotal(string.Join(",", arrDepartment), employeeType));
                    var table = SQLHelper.ExecuteTable(SQLReportAdapter.GetStore_SLCLNguoiLamViecLaDanTocThieuSoCountTotal(string.Join(",", arrDepartment), employeeType));
                    for (var j = 0; j < table.Rows.Count; j++)
                    {
                        xTongDangVien = (int)table.Rows[j]["xDangVien"];
                        xTongNu = (int)table.Rows[j]["xNu"];
                        xTongNam = (int)table.Rows[j]["xNam"];
                        xTongDuoi30 = (int)table.Rows[j]["xDuoi30"];
                        xTongTu30Den40 = (int)table.Rows[j]["x31Den40"];
                        xTongTu41Den50 = (int)table.Rows[j]["x41Den50"];
                        xTongNuTu51Den55 = (int)table.Rows[j]["xNu51Den55"];
                        xTongNamTu51Den55 = (int)table.Rows[j]["xNam51Den55"];
                        xTongNamTu56Den60 = (int)table.Rows[j]["xNam56Den60"];
                        xTongNgachCVC = (int)table.Rows[j]["xChuyenVienChinh"];
                        xTongNgachCV = (int)table.Rows[j]["xChuyenVien"];
                        xTongNgachCS = (int)table.Rows[j]["xCanSu"];
                        xTongNgachConLai = (int)table.Rows[j]["xNgachConLai"];
                        xTongTHCS = (int)table.Rows[j]["xTHCS"];
                        xTongTHPT = (int)table.Rows[j]["xTHPT"];
                        xTongCMThacSi = (int)table.Rows[j]["xThacSi"];
                        xTongCMDaiHoc = (int)table.Rows[j]["xDaiHoc"];
                        xTongCMCaoDang = (int)table.Rows[j]["xCaoDang"];
                        xTongCMTrungCap = (int)table.Rows[j]["xTrungCap"];
                        xTongCMConLai = (int)table.Rows[j]["xTrinhDoChuyenMonConLai"];
                        xTongLLCTCuNhan = (int)table.Rows[j]["xCuNhanChinhTri"];
                        xTongLLCTCaoCap = (int)table.Rows[j]["xCaoCapChinhTri"];
                        xTongLLCTTrungCap = (int)table.Rows[j]["xTrungCapChinhTri"];
                        //xTongQLChuyenVienChinh =
                        //xTongQLChuyenVien =
                        //xTongQLQLGD =
                        xDanTocThai = (int)table.Rows[j]["xDanTocThai"];
                        xDanTocMong = (int)table.Rows[j]["xDanTocMong"];
                        xDanTocHaNhi = (int)table.Rows[j]["xDanTocHaNhi"];
                        xDanTocTay = (int)table.Rows[j]["xDanTocTay"];
                        xDanTocMuong = (int)table.Rows[j]["xDanTocMuong"];
                        xDanTocDao = (int)table.Rows[j]["xDanTocDao"];
                        xDanTocGiay = (int)table.Rows[j]["xDanTocGiay"];
                        xDanTocCong = (int)table.Rows[j]["xDanTocCong"];
                        xDanTocHoa = (int)table.Rows[j]["xDanTocHoa"];
                        xDanTocSiLa = (int)table.Rows[j]["xDanTocSiLa"];
                        xDanTocNung = (int)table.Rows[j]["xDanTocNung"];
                        xDanTocCaoLan = (int)table.Rows[j]["xDanTocCaoLan"];
                        xDanTocLaHu = (int)table.Rows[j]["xDanTocLaHu"];
                        xDanTocTho = (int)table.Rows[j]["xDanTocTho"];
                        xDanTocKhac = (int)table.Rows[j]["xDanTocKhac"];
                        break;
                    }

                    DataSource = tableGroup;
                    xrTableCellTenDonVi.DataBindings.Add("Text", DataSource, "TenLoaiCanBo");
                    xrTableCellDangVien.DataBindings.Add("Text", DataSource, "xGroupDangVien", "{0:n0}");
                    xrTableCellNam.DataBindings.Add("Text", DataSource, "xGroupNam", "{0:n0}");
                    xrTableCellNu.DataBindings.Add("Text", DataSource, "xGroupNu", "{0:n0}");
                    xrTableCellDuoi30.DataBindings.Add("Text", DataSource, "xGroupDuoi30", "{0:n0}");
                    xrTableCellTu30Den40.DataBindings.Add("Text", DataSource, "xGroup31Den40", "{0:n0}");
                    xrTableCellTu41Den50.DataBindings.Add("Text", DataSource, "xGroup41Den50", "{0:n0}");
                    xrTableCellNuTu51Den55.DataBindings.Add("Text", DataSource, "xGroupNu51Den55", "{0:n0}");
                    xrTableCellNamTu51Den55.DataBindings.Add("Text", DataSource, "xGroupNam51Den55", "{0:n0}");
                    xrTableCellNamTu56Den60.DataBindings.Add("Text", DataSource, "xGroupNam56Den60", "{0:n0}");
                    xrTableCellNgachCVCVaTD.DataBindings.Add("Text", DataSource, "xGroupChuyenVienChinh", "{0:n0}");
                    xrTableCellNgachCVVaTD.DataBindings.Add("Text", DataSource, "xGroupChuyenVien", "{0:n0}");
                    xrTableCellNgachCSVaTD.DataBindings.Add("Text", DataSource, "xGroupCanSu", "{0:n0}");
                    xrTableCellNgachConLai.DataBindings.Add("Text", DataSource, "xGroupNgachConLai", "{0:n0}");
                    xrTableCellTHCS.DataBindings.Add("Text", DataSource, "xGroupTHCS", "{0:n0}");
                    xrTableCellTHPT.DataBindings.Add("Text", DataSource, "xGroupTHPT", "{0:n0}");
                    xrTableCellCMThacSi.DataBindings.Add("Text", DataSource, "xGroupThacSi", "{0:n0}");
                    xrTableCellCMDaiHoc.DataBindings.Add("Text", DataSource, "xGroupDaiHoc", "{0:n0}");
                    xrTableCellCMCaoDang.DataBindings.Add("Text", DataSource, "xGroupCaoDang", "{0:n0}");
                    xrTableCellCMTrungCap.DataBindings.Add("Text", DataSource, "xGroupTrungCap", "{0:n0}");
                    xrTableCellCMConLai.DataBindings.Add("Text", DataSource, "xGroupTrinhDoChuyenMonConLai", "{0:n0}");

                    //xrTableCellLLCTCuNhan.DataBindings.Add("Text", DataSource, "xGroupCuNhanChinhTri", "{0:n0}");
                    //xrTableCellLLCTCaoCap.DataBindings.Add("Text", DataSource, "xGroupCaoCapChinhTri", "{0:n0}");
                    //xrTableCellLLCTTrungCap.DataBindings.Add("Text", DataSource, "xGroupTrungCapChinhTri", "{0:n0}");

                    xrTableCellQLChuyenVienChinh.DataBindings.Add("Text", DataSource, "xGroupNam56Den60", "{0:n0}");
                    xrTableCellQLChuyenVien.DataBindings.Add("Text", DataSource, "xGroupNam56Den60", "{0:n0}");
                    xrTableCellQLQLGD.DataBindings.Add("Text", DataSource, "xGroupNam56Den60", "{0:n0}");
                    xrTableCellThai.DataBindings.Add("Text", DataSource, "xGroupDanTocThai", "{0:n0}");
                    xrTableCellMong.DataBindings.Add("Text", DataSource, "xGroupDanTocMong", "{0:n0}");
                    xrTableCellHaNhi.DataBindings.Add("Text", DataSource, "xGroupDanTocHaNhi", "{0:n0}");
                    xrTableCellTay.DataBindings.Add("Text", DataSource, "xGroupDanTocTay", "{0:n0}");
                    xrTableCellMuong.DataBindings.Add("Text", DataSource, "xGroupDanTocMuong", "{0:n0}");
                    xrTableCellDao.DataBindings.Add("Text", DataSource, "xGroupDanTocDao", "{0:n0}");
                    xrTableCellGiay.DataBindings.Add("Text", DataSource, "xGroupDanTocGiay", "{0:n0}");
                    xrTableCellCong.DataBindings.Add("Text", DataSource, "xGroupDanTocCong", "{0:n0}");
                    xrTableCellHoa.DataBindings.Add("Text", DataSource, "xGroupDanTocHoa", "{0:n0}");
                    xrTableCellSiLa.DataBindings.Add("Text", DataSource, "xGroupDanTocSiLa", "{0:n0}");
                    xrTableCellNung.DataBindings.Add("Text", DataSource, "xGroupDanTocNung", "{0:n0}");
                    xrTableCellCaoLan.DataBindings.Add("Text", DataSource, "xGroupDanTocCaoLan", "{0:n0}");
                    xrTableCellLaHu.DataBindings.Add("Text", DataSource, "xGroupDanTocLaHu", "{0:n0}");
                    xrTableCellTho.DataBindings.Add("Text", DataSource, "xGroupDanTocTho", "{0:n0}");
                    xrTableCellDanTocKhac.DataBindings.Add("Text", DataSource, "xGroupDanTocKhac", "{0:n0}");
                    xrTongDangVien.Text = xTongDangVien.ToString();
                    xrTongNu.Text = xTongNu.ToString();
                    xrTongNam.Text = xTongNam.ToString();
                    xrTongDuoi30.Text = xTongDuoi30.ToString();
                    xrTongTu30Den40.Text = xTongTu30Den40.ToString();
                    xrTongTu41Den50.Text = xTongTu41Den50.ToString();
                    xrTongNuTu51Den55.Text = xTongNuTu51Den55.ToString();
                    xrTongNamTu51Den55.Text = xTongNamTu51Den55.ToString();
                    xrTongNamTu56Den60.Text = xTongNamTu56Den60.ToString();
                    xrTongNgachCVCVaTD.Text = xTongNgachCVC.ToString();
                    xrTongNgachCVVaTD.Text = xTongNgachCV.ToString();
                    xrTongNgachCSVaTD.Text = xTongNgachCS.ToString();
                    xrTongNgachConLai.Text = xTongNgachConLai.ToString();
                    xrTongTHCS.Text = xTongTHCS.ToString();
                    xrTongTHPT.Text = xTongTHPT.ToString();
                    xrTongCMThacSi.Text = xTongCMThacSi.ToString();
                    xrTongCMDaiHoc.Text = xTongCMDaiHoc.ToString();
                    xrTongCMCaoDang.Text = xTongCMCaoDang.ToString();
                    xrTongCMTrungCap.Text = xTongCMTrungCap.ToString();
                    xrTongCMConLai.Text = xTongCMConLai.ToString();
                    xrTongLLCTCuNhan.Text = xTongLLCTCuNhan.ToString();
                    xrTongLLCTCaoCap.Text = xTongLLCTCaoCap.ToString();
                    xrTongLLCTTrungCap.Text = xTongLLCTTrungCap.ToString();
                    xrTongQLChuyenVienChinh.Text = xTongQLChuyenVienChinh.ToString();
                    xrTongQLChuyenVien.Text = xTongQLChuyenVien.ToString();
                    xrTongQLQLGD.Text = xTongQLQLGD.ToString();
                    xrTongThai.Text = xDanTocThai.ToString();
                    xrTongMong.Text = xDanTocMong.ToString();
                    xrTongHaNhi.Text = xDanTocHaNhi.ToString();
                    xrTongTay.Text = xDanTocTay.ToString();
                    xrTongMuong.Text = xDanTocMuong.ToString();
                    xrTongDao.Text = xDanTocDao.ToString();
                    xrTongGiay.Text = xDanTocGiay.ToString();
                    xrTongCong.Text = xDanTocCong.ToString();
                    xrTongHoa.Text = xDanTocHoa.ToString();
                    xrTongSiLa.Text = xDanTocSiLa.ToString();
                    xrTongNung.Text = xDanTocNung.ToString();
                    xrTongCaoLan.Text = xDanTocCaoLan.ToString();
                    xrTongLaHu.Text = xDanTocLaHu.ToString();
                    xrTongTho.Text = xDanTocTho.ToString();
                    xrTongDanTocKhac.Text = xDanTocKhac.ToString();
                }
            }
            catch
            {


            }

        }
        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            string resourceFileName = "rp_QuantityEthnicMinority.resx";
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.tblDetail = new DevExpress.XtraReports.UI.XRTable();
            this.xrDetailRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCellSoThuTu = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTenDonVi = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTongSoCongChuc = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellDangVien = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellNam = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellNu = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellDuoi30 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTu30Den40 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTu41Den50 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellNuTu51Den55 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellNamTu51Den55 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellNamTu56Den60 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellNgachCVCVaTD = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellNgachCVVaTD = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellNgachCSVaTD = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellNgachConLai = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTHCS = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTHPT = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellCMThacSi = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellCMDaiHoc = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellCMCaoDang = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellCMTrungCap = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellCMConLai = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellLLCTCuNhan = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellLLCTCaoCap = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellLLCTTrungCap = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellQLChuyenVienChinh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellQLChuyenVien = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellQLQLGD = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellThai = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellMong = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellHaNhi = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTay = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellMuong = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellDao = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellGiay = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellCong = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellHoa = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellSiLa = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellNung = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellCaoLan = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellLaHu = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTho = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellDanTocKhac = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.tblReportHeader = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow19 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrCellTenBieuMau = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellReportName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellOrganization = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell401 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabelTinhDen = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell403 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrCellLastReceivedDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellToDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell503 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell601 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell602 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell603 = new DevExpress.XtraReports.UI.XRTableCell();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.tblPageHeader = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow11 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell241 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell242 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblTrongDo = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblTuoi = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable12 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow17 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell42 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell43 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell44 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell45 = new DevExpress.XtraReports.UI.XRTableCell();
            this.lbl51Den60 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable13 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow18 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell47 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell48 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell49 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblNgachCongChuc = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell50 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell51 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable5 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable6 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow9 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell25 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable7 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow10 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell29 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell30 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell31 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTableCell32 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable8 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow12 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell26 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell33 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell34 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell35 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell36 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell38 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell39 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell40 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell41 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell46 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell28 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell37 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.formattingRule1 = new DevExpress.XtraReports.UI.FormattingRule();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTextTongSo = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongSoCongChuc = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongDangVien = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongNam = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongNu = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongDuoi30 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongTu30Den40 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongTu41Den50 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongNuTu51Den55 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongNamTu51Den55 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongNamTu56Den60 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongNgachCVCVaTD = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongNgachCVVaTD = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongNgachCSVaTD = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongNgachConLai = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongTHCS = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongTHPT = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongCMThacSi = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongCMDaiHoc = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongCMCaoDang = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongCMTrungCap = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongCMConLai = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongLLCTCuNhan = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongLLCTCaoCap = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongLLCTTrungCap = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongQLChuyenVienChinh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongQLChuyenVien = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongQLQLGD = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongThai = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongMong = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongHaNhi = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongTay = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongMuong = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongDao = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongGiay = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongCong = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongHoa = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongSiLa = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongNung = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongCaoLan = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongLaHu = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongTho = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongDanTocKhac = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.tblDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblReportHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblPageHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.tblDetail});
            this.Detail.HeightF = 25F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // tblDetail
            // 
            this.tblDetail.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.tblDetail.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.tblDetail.Name = "tblDetail";
            this.tblDetail.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrDetailRow1});
            this.tblDetail.SizeF = new System.Drawing.SizeF(1140F, 25F);
            this.tblDetail.StylePriority.UseBorders = false;
            // 
            // xrDetailRow1
            // 
            this.xrDetailRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCellSoThuTu,
            this.xrTableCellTenDonVi,
            this.xrTableCellTongSoCongChuc,
            this.xrTableCellDangVien,
            this.xrTableCellNam,
            this.xrTableCellNu,
            this.xrTableCellDuoi30,
            this.xrTableCellTu30Den40,
            this.xrTableCellTu41Den50,
            this.xrTableCellNuTu51Den55,
            this.xrTableCellNamTu51Den55,
            this.xrTableCellNamTu56Den60,
            this.xrTableCellNgachCVCVaTD,
            this.xrTableCellNgachCVVaTD,
            this.xrTableCellNgachCSVaTD,
            this.xrTableCellNgachConLai,
            this.xrTableCellTHCS,
            this.xrTableCellTHPT,
            this.xrTableCellCMThacSi,
            this.xrTableCellCMDaiHoc,
            this.xrTableCellCMCaoDang,
            this.xrTableCellCMTrungCap,
            this.xrTableCellCMConLai,
            this.xrTableCellLLCTCuNhan,
            this.xrTableCellLLCTCaoCap,
            this.xrTableCellLLCTTrungCap,
            this.xrTableCellQLChuyenVienChinh,
            this.xrTableCellQLChuyenVien,
            this.xrTableCellQLQLGD,
            this.xrTableCellThai,
            this.xrTableCellMong,
            this.xrTableCellHaNhi,
            this.xrTableCellTay,
            this.xrTableCellMuong,
            this.xrTableCellDao,
            this.xrTableCellGiay,
            this.xrTableCellCong,
            this.xrTableCellHoa,
            this.xrTableCellSiLa,
            this.xrTableCellNung,
            this.xrTableCellCaoLan,
            this.xrTableCellLaHu,
            this.xrTableCellTho,
            this.xrTableCellDanTocKhac});
            this.xrDetailRow1.Name = "xrDetailRow1";
            this.xrDetailRow1.Weight = 1D;
            // 
            // xrTableCellSoThuTu
            // 
            this.xrTableCellSoThuTu.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellSoThuTu.Name = "xrTableCellSoThuTu";
            this.xrTableCellSoThuTu.StylePriority.UseBorders = false;
            this.xrTableCellSoThuTu.StylePriority.UseTextAlignment = false;
            this.xrTableCellSoThuTu.Text = " ";
            this.xrTableCellSoThuTu.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellSoThuTu.Weight = 0.28314132690429689D;
            this.xrTableCellSoThuTu.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail_BeforePrint);
            // 
            // xrTableCellTenDonVi
            // 
            this.xrTableCellTenDonVi.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTenDonVi.Name = "xrTableCellTenDonVi";
            this.xrTableCellTenDonVi.StylePriority.UseBorders = false;
            this.xrTableCellTenDonVi.StylePriority.UseTextAlignment = false;
            this.xrTableCellTenDonVi.Text = " ";
            this.xrTableCellTenDonVi.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellTenDonVi.Weight = 1.5995168655464493D;
            // 
            // xrTableCellTongSoCongChuc
            // 
            this.xrTableCellTongSoCongChuc.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTongSoCongChuc.Name = "xrTableCellTongSoCongChuc";
            this.xrTableCellTongSoCongChuc.StylePriority.UseBorders = false;
            this.xrTableCellTongSoCongChuc.StylePriority.UseTextAlignment = false;
            this.xrTableCellTongSoCongChuc.Text = " ";
            this.xrTableCellTongSoCongChuc.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTongSoCongChuc.Weight = 0.39314021517185654D;
            // 
            // xrTableCellDangVien
            // 
            this.xrTableCellDangVien.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellDangVien.Name = "xrTableCellDangVien";
            this.xrTableCellDangVien.StylePriority.UseBorders = false;
            this.xrTableCellDangVien.StylePriority.UseTextAlignment = false;
            this.xrTableCellDangVien.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellDangVien.Weight = 0.3144875998376474D;
            // 
            // xrTableCellNam
            // 
            this.xrTableCellNam.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellNam.Name = "xrTableCellNam";
            this.xrTableCellNam.StylePriority.UseBorders = false;
            this.xrTableCellNam.StylePriority.UseTextAlignment = false;
            this.xrTableCellNam.Text = " ";
            this.xrTableCellNam.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellNam.Weight = 0.21288866665881379D;
            // 
            // xrTableCellNu
            // 
            this.xrTableCellNu.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellNu.Name = "xrTableCellNu";
            this.xrTableCellNu.StylePriority.UseBorders = false;
            this.xrTableCellNu.StylePriority.UseTextAlignment = false;
            this.xrTableCellNu.Text = " ";
            this.xrTableCellNu.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellNu.Weight = 0.21288864826451132D;
            // 
            // xrTableCellDuoi30
            // 
            this.xrTableCellDuoi30.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellDuoi30.Name = "xrTableCellDuoi30";
            this.xrTableCellDuoi30.StylePriority.UseBorders = false;
            this.xrTableCellDuoi30.StylePriority.UseTextAlignment = false;
            this.xrTableCellDuoi30.Text = " ";
            this.xrTableCellDuoi30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellDuoi30.Weight = 0.21288864572005545D;
            // 
            // xrTableCellTu30Den40
            // 
            this.xrTableCellTu30Den40.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTu30Den40.Name = "xrTableCellTu30Den40";
            this.xrTableCellTu30Den40.StylePriority.UseBorders = false;
            this.xrTableCellTu30Den40.StylePriority.UseTextAlignment = false;
            this.xrTableCellTu30Den40.Text = " ";
            this.xrTableCellTu30Den40.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTu30Den40.Weight = 0.21288864699228355D;
            // 
            // xrTableCellTu41Den50
            // 
            this.xrTableCellTu41Den50.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTu41Den50.Name = "xrTableCellTu41Den50";
            this.xrTableCellTu41Den50.StylePriority.UseBorders = false;
            this.xrTableCellTu41Den50.StylePriority.UseTextAlignment = false;
            this.xrTableCellTu41Den50.Text = " ";
            this.xrTableCellTu41Den50.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTu41Den50.Weight = 0.21288799857988794D;
            // 
            // xrTableCellNuTu51Den55
            // 
            this.xrTableCellNuTu51Den55.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellNuTu51Den55.Name = "xrTableCellNuTu51Den55";
            this.xrTableCellNuTu51Den55.StylePriority.UseBorders = false;
            this.xrTableCellNuTu51Den55.StylePriority.UseTextAlignment = false;
            this.xrTableCellNuTu51Den55.Text = " ";
            this.xrTableCellNuTu51Den55.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellNuTu51Den55.Weight = 0.21288834181650218D;
            // 
            // xrTableCellNamTu51Den55
            // 
            this.xrTableCellNamTu51Den55.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellNamTu51Den55.Name = "xrTableCellNamTu51Den55";
            this.xrTableCellNamTu51Den55.StylePriority.UseBorders = false;
            this.xrTableCellNamTu51Den55.StylePriority.UseTextAlignment = false;
            this.xrTableCellNamTu51Den55.Text = " ";
            this.xrTableCellNamTu51Den55.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellNamTu51Den55.Weight = 0.21288929667690693D;
            // 
            // xrTableCellNamTu56Den60
            // 
            this.xrTableCellNamTu56Den60.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellNamTu56Den60.Name = "xrTableCellNamTu56Den60";
            this.xrTableCellNamTu56Den60.StylePriority.UseBorders = false;
            this.xrTableCellNamTu56Den60.StylePriority.UseTextAlignment = false;
            this.xrTableCellNamTu56Den60.Text = " ";
            this.xrTableCellNamTu56Den60.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellNamTu56Den60.Weight = 0.21288897056236694D;
            // 
            // xrTableCellNgachCVCVaTD
            // 
            this.xrTableCellNgachCVCVaTD.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellNgachCVCVaTD.Name = "xrTableCellNgachCVCVaTD";
            this.xrTableCellNgachCVCVaTD.StylePriority.UseBorders = false;
            this.xrTableCellNgachCVCVaTD.StylePriority.UseTextAlignment = false;
            this.xrTableCellNgachCVCVaTD.Text = " ";
            this.xrTableCellNgachCVCVaTD.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellNgachCVCVaTD.Weight = 0.21288832342219938D;
            // 
            // xrTableCellNgachCVVaTD
            // 
            this.xrTableCellNgachCVVaTD.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellNgachCVVaTD.Name = "xrTableCellNgachCVVaTD";
            this.xrTableCellNgachCVVaTD.StylePriority.UseBorders = false;
            this.xrTableCellNgachCVVaTD.StylePriority.UseTextAlignment = false;
            this.xrTableCellNgachCVVaTD.Text = " ";
            this.xrTableCellNgachCVVaTD.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellNgachCVVaTD.Weight = 0.21288864699228322D;
            // 
            // xrTableCellNgachCSVaTD
            // 
            this.xrTableCellNgachCSVaTD.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellNgachCSVaTD.Name = "xrTableCellNgachCSVaTD";
            this.xrTableCellNgachCSVaTD.StylePriority.UseBorders = false;
            this.xrTableCellNgachCSVaTD.StylePriority.UseTextAlignment = false;
            this.xrTableCellNgachCSVaTD.Text = " ";
            this.xrTableCellNgachCSVaTD.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellNgachCSVaTD.Weight = 0.21288897183459515D;
            // 
            // xrTableCellNgachConLai
            // 
            this.xrTableCellNgachConLai.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellNgachConLai.Name = "xrTableCellNgachConLai";
            this.xrTableCellNgachConLai.StylePriority.UseBorders = false;
            this.xrTableCellNgachConLai.StylePriority.UseTextAlignment = false;
            this.xrTableCellNgachConLai.Text = " ";
            this.xrTableCellNgachConLai.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellNgachConLai.Weight = 0.21288767500980421D;
            // 
            // xrTableCellTHCS
            // 
            this.xrTableCellTHCS.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTHCS.Name = "xrTableCellTHCS";
            this.xrTableCellTHCS.StylePriority.UseBorders = false;
            this.xrTableCellTHCS.StylePriority.UseTextAlignment = false;
            this.xrTableCellTHCS.Text = " ";
            this.xrTableCellTHCS.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTHCS.Weight = 0.21289026738715827D;
            // 
            // xrTableCellTHPT
            // 
            this.xrTableCellTHPT.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTHPT.Name = "xrTableCellTHPT";
            this.xrTableCellTHPT.StylePriority.UseBorders = false;
            this.xrTableCellTHPT.StylePriority.UseTextAlignment = false;
            this.xrTableCellTHPT.Text = " ";
            this.xrTableCellTHPT.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTHPT.Weight = 0.21288637182387316D;
            // 
            // xrTableCellCMThacSi
            // 
            this.xrTableCellCMThacSi.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellCMThacSi.Name = "xrTableCellCMThacSi";
            this.xrTableCellCMThacSi.StylePriority.UseBorders = false;
            this.xrTableCellCMThacSi.StylePriority.UseTextAlignment = false;
            this.xrTableCellCMThacSi.Text = " ";
            this.xrTableCellCMThacSi.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellCMThacSi.Weight = 0.23896244253054114D;
            // 
            // xrTableCellCMDaiHoc
            // 
            this.xrTableCellCMDaiHoc.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellCMDaiHoc.Name = "xrTableCellCMDaiHoc";
            this.xrTableCellCMDaiHoc.StylePriority.UseBorders = false;
            this.xrTableCellCMDaiHoc.StylePriority.UseTextAlignment = false;
            this.xrTableCellCMDaiHoc.Text = " ";
            this.xrTableCellCMDaiHoc.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellCMDaiHoc.Weight = 0.2389604922044429D;
            // 
            // xrTableCellCMCaoDang
            // 
            this.xrTableCellCMCaoDang.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellCMCaoDang.Name = "xrTableCellCMCaoDang";
            this.xrTableCellCMCaoDang.StylePriority.UseBorders = false;
            this.xrTableCellCMCaoDang.StylePriority.UseTextAlignment = false;
            this.xrTableCellCMCaoDang.Text = " ";
            this.xrTableCellCMCaoDang.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellCMCaoDang.Weight = 0.23896113807238245D;
            // 
            // xrTableCellCMTrungCap
            // 
            this.xrTableCellCMTrungCap.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellCMTrungCap.Name = "xrTableCellCMTrungCap";
            this.xrTableCellCMTrungCap.StylePriority.UseBorders = false;
            this.xrTableCellCMTrungCap.StylePriority.UseTextAlignment = false;
            this.xrTableCellCMTrungCap.Text = " ";
            this.xrTableCellCMTrungCap.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellCMTrungCap.Weight = 0.23896114188906625D;
            // 
            // xrTableCellCMConLai
            // 
            this.xrTableCellCMConLai.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellCMConLai.Name = "xrTableCellCMConLai";
            this.xrTableCellCMConLai.StylePriority.UseBorders = false;
            this.xrTableCellCMConLai.StylePriority.UseTextAlignment = false;
            this.xrTableCellCMConLai.Text = " ";
            this.xrTableCellCMConLai.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellCMConLai.Weight = 0.23896048965998668D;
            // 
            // xrTableCellLLCTCuNhan
            // 
            this.xrTableCellLLCTCuNhan.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellLLCTCuNhan.Name = "xrTableCellLLCTCuNhan";
            this.xrTableCellLLCTCuNhan.StylePriority.UseBorders = false;
            this.xrTableCellLLCTCuNhan.StylePriority.UseTextAlignment = false;
            this.xrTableCellLLCTCuNhan.Text = " ";
            this.xrTableCellLLCTCuNhan.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellLLCTCuNhan.Weight = 0.24839131547099036D;
            // 
            // xrTableCellLLCTCaoCap
            // 
            this.xrTableCellLLCTCaoCap.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellLLCTCaoCap.Name = "xrTableCellLLCTCaoCap";
            this.xrTableCellLLCTCaoCap.StylePriority.UseBorders = false;
            this.xrTableCellLLCTCaoCap.StylePriority.UseTextAlignment = false;
            this.xrTableCellLLCTCaoCap.Text = " ";
            this.xrTableCellLLCTCaoCap.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellLLCTCaoCap.Weight = 0.24837312048484922D;
            // 
            // xrTableCellLLCTTrungCap
            // 
            this.xrTableCellLLCTTrungCap.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellLLCTTrungCap.Name = "xrTableCellLLCTTrungCap";
            this.xrTableCellLLCTTrungCap.StylePriority.UseBorders = false;
            this.xrTableCellLLCTTrungCap.StylePriority.UseTextAlignment = false;
            this.xrTableCellLLCTTrungCap.Text = " ";
            this.xrTableCellLLCTTrungCap.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellLLCTTrungCap.Weight = 0.248374422398552D;
            // 
            // xrTableCellQLChuyenVienChinh
            // 
            this.xrTableCellQLChuyenVienChinh.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellQLChuyenVienChinh.Name = "xrTableCellQLChuyenVienChinh";
            this.xrTableCellQLChuyenVienChinh.StylePriority.UseBorders = false;
            this.xrTableCellQLChuyenVienChinh.StylePriority.UseTextAlignment = false;
            this.xrTableCellQLChuyenVienChinh.Text = " ";
            this.xrTableCellQLChuyenVienChinh.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellQLChuyenVienChinh.Weight = 0.28828389531712945D;
            // 
            // xrTableCellQLChuyenVien
            // 
            this.xrTableCellQLChuyenVien.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellQLChuyenVien.Name = "xrTableCellQLChuyenVien";
            this.xrTableCellQLChuyenVien.StylePriority.UseBorders = false;
            this.xrTableCellQLChuyenVien.StylePriority.UseTextAlignment = false;
            this.xrTableCellQLChuyenVien.Text = " ";
            this.xrTableCellQLChuyenVien.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellQLChuyenVien.Weight = 0.28828324944919048D;
            // 
            // xrTableCellQLQLGD
            // 
            this.xrTableCellQLQLGD.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellQLQLGD.Name = "xrTableCellQLQLGD";
            this.xrTableCellQLQLGD.StylePriority.UseBorders = false;
            this.xrTableCellQLQLGD.StylePriority.UseTextAlignment = false;
            this.xrTableCellQLQLGD.Text = " ";
            this.xrTableCellQLQLGD.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellQLQLGD.Weight = 0.2883332726207416D;
            // 
            // xrTableCellThai
            // 
            this.xrTableCellThai.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellThai.Name = "xrTableCellThai";
            this.xrTableCellThai.StylePriority.UseBorders = false;
            this.xrTableCellThai.StylePriority.UseTextAlignment = false;
            this.xrTableCellThai.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellThai.Weight = 0.25060608908017828D;
            // 
            // xrTableCellMong
            // 
            this.xrTableCellMong.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellMong.Name = "xrTableCellMong";
            this.xrTableCellMong.StylePriority.UseBorders = false;
            this.xrTableCellMong.StylePriority.UseTextAlignment = false;
            this.xrTableCellMong.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellMong.Weight = 0.2506060890801784D;
            // 
            // xrTableCellHaNhi
            // 
            this.xrTableCellHaNhi.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellHaNhi.Name = "xrTableCellHaNhi";
            this.xrTableCellHaNhi.StylePriority.UseBorders = false;
            this.xrTableCellHaNhi.StylePriority.UseTextAlignment = false;
            this.xrTableCellHaNhi.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellHaNhi.Weight = 0.25060478971093147D;
            // 
            // xrTableCellTay
            // 
            this.xrTableCellTay.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTay.Name = "xrTableCellTay";
            this.xrTableCellTay.StylePriority.UseBorders = false;
            this.xrTableCellTay.StylePriority.UseTextAlignment = false;
            this.xrTableCellTay.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTay.Weight = 0.25060543939555469D;
            // 
            // xrTableCellMuong
            // 
            this.xrTableCellMuong.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellMuong.Name = "xrTableCellMuong";
            this.xrTableCellMuong.StylePriority.UseBorders = false;
            this.xrTableCellMuong.StylePriority.UseTextAlignment = false;
            this.xrTableCellMuong.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellMuong.Weight = 0.25060543939555491D;
            // 
            // xrTableCellDao
            // 
            this.xrTableCellDao.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellDao.Name = "xrTableCellDao";
            this.xrTableCellDao.StylePriority.UseBorders = false;
            this.xrTableCellDao.StylePriority.UseTextAlignment = false;
            this.xrTableCellDao.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellDao.Weight = 0.2506034903416845D;
            // 
            // xrTableCellGiay
            // 
            this.xrTableCellGiay.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellGiay.Name = "xrTableCellGiay";
            this.xrTableCellGiay.StylePriority.UseBorders = false;
            this.xrTableCellGiay.StylePriority.UseTextAlignment = false;
            this.xrTableCellGiay.Text = " ";
            this.xrTableCellGiay.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellGiay.Weight = 0.25060673876480188D;
            // 
            // xrTableCellCong
            // 
            this.xrTableCellCong.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellCong.Name = "xrTableCellCong";
            this.xrTableCellCong.StylePriority.UseBorders = false;
            this.xrTableCellCong.StylePriority.UseTextAlignment = false;
            this.xrTableCellCong.Text = " ";
            this.xrTableCellCong.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellCong.Weight = 0.25060547872861583D;
            // 
            // xrTableCellHoa
            // 
            this.xrTableCellHoa.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellHoa.Name = "xrTableCellHoa";
            this.xrTableCellHoa.StylePriority.UseBorders = false;
            this.xrTableCellHoa.StylePriority.UseTextAlignment = false;
            this.xrTableCellHoa.Text = " ";
            this.xrTableCellHoa.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellHoa.Weight = 0.250605439395555D;
            // 
            // xrTableCellSiLa
            // 
            this.xrTableCellSiLa.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellSiLa.Name = "xrTableCellSiLa";
            this.xrTableCellSiLa.StylePriority.UseBorders = false;
            this.xrTableCellSiLa.StylePriority.UseTextAlignment = false;
            this.xrTableCellSiLa.Text = " ";
            this.xrTableCellSiLa.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellSiLa.Weight = 0.25060544194001089D;
            // 
            // xrTableCellNung
            // 
            this.xrTableCellNung.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellNung.Name = "xrTableCellNung";
            this.xrTableCellNung.StylePriority.UseBorders = false;
            this.xrTableCellNung.StylePriority.UseTextAlignment = false;
            this.xrTableCellNung.Text = " ";
            this.xrTableCellNung.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellNung.Weight = 0.25060478462201963D;
            // 
            // xrTableCellCaoLan
            // 
            this.xrTableCellCaoLan.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellCaoLan.Name = "xrTableCellCaoLan";
            this.xrTableCellCaoLan.StylePriority.UseBorders = false;
            this.xrTableCellCaoLan.StylePriority.UseTextAlignment = false;
            this.xrTableCellCaoLan.Text = " ";
            this.xrTableCellCaoLan.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellCaoLan.Weight = 0.25060543685109904D;
            // 
            // xrTableCellLaHu
            // 
            this.xrTableCellLaHu.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellLaHu.Name = "xrTableCellLaHu";
            this.xrTableCellLaHu.StylePriority.UseBorders = false;
            this.xrTableCellLaHu.StylePriority.UseTextAlignment = false;
            this.xrTableCellLaHu.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellLaHu.Weight = 0.25060673622034579D;
            // 
            // xrTableCellTho
            // 
            this.xrTableCellTho.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTho.Name = "xrTableCellTho";
            this.xrTableCellTho.StylePriority.UseBorders = false;
            this.xrTableCellTho.StylePriority.UseTextAlignment = false;
            this.xrTableCellTho.Text = " ";
            this.xrTableCellTho.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTho.Weight = 0.250605441940011D;
            // 
            // xrTableCellDanTocKhac
            // 
            this.xrTableCellDanTocKhac.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellDanTocKhac.Name = "xrTableCellDanTocKhac";
            this.xrTableCellDanTocKhac.StylePriority.UseBorders = false;
            this.xrTableCellDanTocKhac.StylePriority.UseTextAlignment = false;
            this.xrTableCellDanTocKhac.Text = " ";
            this.xrTableCellDanTocKhac.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellDanTocKhac.Weight = 0.25060617813448394D;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 15F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 0F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.tblReportHeader});
            this.ReportHeader.HeightF = 86F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // tblReportHeader
            // 
            this.tblReportHeader.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.tblReportHeader.Name = "tblReportHeader";
            this.tblReportHeader.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow19,
            this.xrTableRow2,
            this.xrTableRow3,
            this.xrTableRow4});
            this.tblReportHeader.SizeF = new System.Drawing.SizeF(1142.129F, 86F);
            // 
            // xrTableRow19
            // 
            this.xrTableRow19.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrCellTenBieuMau,
            this.xrCellReportName,
            this.xrCellOrganization});
            this.xrTableRow19.Name = "xrTableRow19";
            this.xrTableRow19.Weight = 0.79999990231146945D;
            // 
            // xrCellTenBieuMau
            // 
            this.xrCellTenBieuMau.Font = new System.Drawing.Font("Times New Roman", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.xrCellTenBieuMau.Name = "xrCellTenBieuMau";
            this.xrCellTenBieuMau.StylePriority.UseFont = false;
            this.xrCellTenBieuMau.StylePriority.UseTextAlignment = false;
            this.xrCellTenBieuMau.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrCellTenBieuMau.Weight = 1.5748560589095728D;
            // 
            // xrCellReportName
            // 
            this.xrCellReportName.Font = new System.Drawing.Font("Times New Roman", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.xrCellReportName.Name = "xrCellReportName";
            this.xrCellReportName.StylePriority.UseFont = false;
            this.xrCellReportName.Text = "THỐNG KÊ, TỔNG HỢP SỐ LƯỢNG, CHẤT LƯỢNG SỐ LƯỢNG NGƯỜI LÀM VIỆC LÀ NGƯỜI DÂN TỘC " +
    "THIỂU SỐ";
            this.xrCellReportName.Weight = 3.2928199055848522D;
            // 
            // xrCellOrganization
            // 
            this.xrCellOrganization.Font = new System.Drawing.Font("Times New Roman", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.xrCellOrganization.Name = "xrCellOrganization";
            this.xrCellOrganization.StylePriority.UseFont = false;
            this.xrCellOrganization.StylePriority.UseTextAlignment = false;
            this.xrCellOrganization.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrCellOrganization.Weight = 1.1279513169596362D;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell401,
            this.xrLabelTinhDen,
            this.xrTableCell403});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 0.933333189760807D;
            // 
            // xrTableCell401
            // 
            this.xrTableCell401.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.xrTableCell401.Multiline = true;
            this.xrTableCell401.Name = "xrTableCell401";
            this.xrTableCell401.StylePriority.UseFont = false;
            this.xrTableCell401.Weight = 1.5748560433256003D;
            // 
            // xrLabelTinhDen
            // 
            this.xrLabelTinhDen.Font = new System.Drawing.Font("Times New Roman", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.xrLabelTinhDen.Name = "xrLabelTinhDen";
            this.xrLabelTinhDen.StylePriority.UseFont = false;
            this.xrLabelTinhDen.Text = "(Tính đến {0}/{1}/{2})";
            this.xrLabelTinhDen.Weight = 3.2928199438412697D;
            // 
            // xrTableCell403
            // 
            this.xrTableCell403.Font = new System.Drawing.Font("Times New Roman", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.xrTableCell403.Name = "xrTableCell403";
            this.xrTableCell403.StylePriority.UseFont = false;
            this.xrTableCell403.StylePriority.UseTextAlignment = false;
            this.xrTableCell403.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell403.Weight = 1.1279512942871914D;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrCellLastReceivedDate,
            this.xrCellToDate,
            this.xrTableCell503});
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrTableRow3.StylePriority.UsePadding = false;
            this.xrTableRow3.Weight = 0.79999990685278288D;
            // 
            // xrCellLastReceivedDate
            // 
            this.xrCellLastReceivedDate.Font = new System.Drawing.Font("Times New Roman", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.xrCellLastReceivedDate.Name = "xrCellLastReceivedDate";
            this.xrCellLastReceivedDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrCellLastReceivedDate.StylePriority.UseFont = false;
            this.xrCellLastReceivedDate.StylePriority.UsePadding = false;
            this.xrCellLastReceivedDate.Weight = 1.5748560433256003D;
            // 
            // xrCellToDate
            // 
            this.xrCellToDate.Font = new System.Drawing.Font("Times New Roman", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.xrCellToDate.Name = "xrCellToDate";
            this.xrCellToDate.StylePriority.UseFont = false;
            this.xrCellToDate.Weight = 3.2928199438412697D;
            // 
            // xrTableCell503
            // 
            this.xrTableCell503.Font = new System.Drawing.Font("Times New Roman", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.xrTableCell503.Name = "xrTableCell503";
            this.xrTableCell503.StylePriority.UseFont = false;
            this.xrTableCell503.StylePriority.UseTextAlignment = false;
            this.xrTableCell503.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell503.Weight = 1.1279512942871914D;
            // 
            // xrTableRow4
            // 
            this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell601,
            this.xrTableCell602,
            this.xrTableCell603});
            this.xrTableRow4.Name = "xrTableRow4";
            this.xrTableRow4.Weight = 0.33333324536656561D;
            // 
            // xrTableCell601
            // 
            this.xrTableCell601.Name = "xrTableCell601";
            this.xrTableCell601.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrTableCell601.StylePriority.UsePadding = false;
            this.xrTableCell601.Weight = 1.5748560433256003D;
            // 
            // xrTableCell602
            // 
            this.xrTableCell602.Name = "xrTableCell602";
            this.xrTableCell602.Weight = 3.2928199438412697D;
            // 
            // xrTableCell603
            // 
            this.xrTableCell603.Name = "xrTableCell603";
            this.xrTableCell603.Weight = 1.1279512942871914D;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.tblPageHeader});
            this.PageHeader.HeightF = 230F;
            this.PageHeader.Name = "PageHeader";
            // 
            // tblPageHeader
            // 
            this.tblPageHeader.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.tblPageHeader.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.tblPageHeader.Name = "tblPageHeader";
            this.tblPageHeader.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow11});
            this.tblPageHeader.SizeF = new System.Drawing.SizeF(1140F, 230F);
            this.tblPageHeader.StylePriority.UseBorders = false;
            // 
            // xrTableRow11
            // 
            this.xrTableRow11.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell241,
            this.xrTableCell242,
            this.xrTableCell6,
            this.xrTableCell19,
            this.xrTableCell2,
            this.xrTableCell5,
            this.xrTableCell3,
            this.xrTableCell4,
            this.xrTableCell13,
            this.xrTableCell14,
            this.xrTableCell15,
            this.xrTableCell32});
            this.xrTableRow11.Name = "xrTableRow11";
            this.xrTableRow11.Weight = 9.2D;
            // 
            // xrTableCell241
            // 
            this.xrTableCell241.Angle = 90F;
            this.xrTableCell241.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell241.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell241.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell241.Name = "xrTableCell241";
            this.xrTableCell241.StylePriority.UseBorderColor = false;
            this.xrTableCell241.StylePriority.UseBorders = false;
            this.xrTableCell241.StylePriority.UseFont = false;
            this.xrTableCell241.Text = "STT";
            this.xrTableCell241.Weight = 0.16896727909360582D;
            // 
            // xrTableCell242
            // 
            this.xrTableCell242.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell242.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell242.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell242.Name = "xrTableCell242";
            this.xrTableCell242.StylePriority.UseBorderColor = false;
            this.xrTableCell242.StylePriority.UseBorders = false;
            this.xrTableCell242.StylePriority.UseFont = false;
            this.xrTableCell242.Text = "Tên đơn vị";
            this.xrTableCell242.Weight = 0.95452874845544056D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.Angle = 90F;
            this.xrTableCell6.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell6.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell6.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.StylePriority.UseBorderColor = false;
            this.xrTableCell6.StylePriority.UseBorders = false;
            this.xrTableCell6.StylePriority.UseFont = false;
            this.xrTableCell6.Text = "Tổng số hiện có mặt đến";
            this.xrTableCell6.Weight = 0.23460844588456109D;
            // 
            // xrTableCell19
            // 
            this.xrTableCell19.Angle = 90F;
            this.xrTableCell19.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell19.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell19.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell19.Name = "xrTableCell19";
            this.xrTableCell19.StylePriority.UseBorderColor = false;
            this.xrTableCell19.StylePriority.UseBorders = false;
            this.xrTableCell19.StylePriority.UseFont = false;
            this.xrTableCell19.Text = "Đảng viên";
            this.xrTableCell19.Weight = 0.18767350523873497D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell2.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblTrongDo,
            this.xrTable3});
            this.xrTableCell2.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseBorderColor = false;
            this.xrTableCell2.StylePriority.UseBorders = false;
            this.xrTableCell2.StylePriority.UseFont = false;
            this.xrTableCell2.Weight = 0.25408666956665982D;
            // 
            // lblTrongDo
            // 
            this.lblTrongDo.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.lblTrongDo.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.lblTrongDo.Name = "lblTrongDo";
            this.lblTrongDo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblTrongDo.SizeF = new System.Drawing.SizeF(40F, 50F);
            this.lblTrongDo.StylePriority.UseBorders = false;
            this.lblTrongDo.Text = "Giới tính";
            // 
            // xrTable3
            // 
            this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 50F);
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow5});
            this.xrTable3.SizeF = new System.Drawing.SizeF(40F, 180F);
            // 
            // xrTableRow5
            // 
            this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell7,
            this.xrTableCell8});
            this.xrTableRow5.Name = "xrTableRow5";
            this.xrTableRow5.Weight = 0.91111111111111109D;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.Angle = 90F;
            this.xrTableCell7.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.StylePriority.UseBorders = false;
            this.xrTableCell7.Text = "Nam";
            this.xrTableCell7.Weight = 0.3015074349941147D;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.Angle = 90F;
            this.xrTableCell8.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.StylePriority.UseBorders = false;
            this.xrTableCell8.Text = "Nữ";
            this.xrTableCell8.Weight = 0.30150743499411486D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell5.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell5.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblTuoi,
            this.xrTable12});
            this.xrTableCell5.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.StylePriority.UseBorderColor = false;
            this.xrTableCell5.StylePriority.UseBorders = false;
            this.xrTableCell5.StylePriority.UseFont = false;
            this.xrTableCell5.Text = "xrTableCell5";
            this.xrTableCell5.Weight = 0.76226000106382208D;
            // 
            // lblTuoi
            // 
            this.lblTuoi.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.lblTuoi.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.lblTuoi.Name = "lblTuoi";
            this.lblTuoi.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblTuoi.SizeF = new System.Drawing.SizeF(120F, 25F);
            this.lblTuoi.StylePriority.UseBorders = false;
            this.lblTuoi.Text = "Độ tuổi";
            // 
            // xrTable12
            // 
            this.xrTable12.LocationFloat = new DevExpress.Utils.PointFloat(0F, 25F);
            this.xrTable12.Name = "xrTable12";
            this.xrTable12.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow17});
            this.xrTable12.SizeF = new System.Drawing.SizeF(120F, 205F);
            // 
            // xrTableRow17
            // 
            this.xrTableRow17.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableRow17.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell42,
            this.xrTableCell43,
            this.xrTableCell44,
            this.xrTableCell45});
            this.xrTableRow17.Name = "xrTableRow17";
            this.xrTableRow17.StylePriority.UseBorders = false;
            this.xrTableRow17.Weight = 8.2D;
            // 
            // xrTableCell42
            // 
            this.xrTableCell42.Angle = 90F;
            this.xrTableCell42.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell42.Name = "xrTableCell42";
            this.xrTableCell42.StylePriority.UseBorders = false;
            this.xrTableCell42.Text = "Dưới 30";
            this.xrTableCell42.Weight = 0.16807663823451507D;
            // 
            // xrTableCell43
            // 
            this.xrTableCell43.Angle = 90F;
            this.xrTableCell43.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell43.Name = "xrTableCell43";
            this.xrTableCell43.StylePriority.UseBorders = false;
            this.xrTableCell43.Text = "30-40";
            this.xrTableCell43.Weight = 0.1680766382345151D;
            // 
            // xrTableCell44
            // 
            this.xrTableCell44.Angle = 90F;
            this.xrTableCell44.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell44.Name = "xrTableCell44";
            this.xrTableCell44.StylePriority.UseBorders = false;
            this.xrTableCell44.Text = "41-50";
            this.xrTableCell44.Weight = 0.16807663823451507D;
            // 
            // xrTableCell45
            // 
            this.xrTableCell45.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lbl51Den60,
            this.xrTable13});
            this.xrTableCell45.Name = "xrTableCell45";
            this.xrTableCell45.Weight = 0.50422992886037843D;
            // 
            // lbl51Den60
            // 
            this.lbl51Den60.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.lbl51Den60.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.lbl51Den60.Name = "lbl51Den60";
            this.lbl51Den60.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lbl51Den60.SizeF = new System.Drawing.SizeF(60F, 25F);
            this.lbl51Den60.StylePriority.UseBorders = false;
            this.lbl51Den60.Text = "Trên 50";
            // 
            // xrTable13
            // 
            this.xrTable13.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTable13.LocationFloat = new DevExpress.Utils.PointFloat(0F, 24.99999F);
            this.xrTable13.Name = "xrTable13";
            this.xrTable13.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow18});
            this.xrTable13.SizeF = new System.Drawing.SizeF(60F, 180F);
            this.xrTable13.StylePriority.UseBorders = false;
            // 
            // xrTableRow18
            // 
            this.xrTableRow18.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell47,
            this.xrTableCell48,
            this.xrTableCell49});
            this.xrTableRow18.Name = "xrTableRow18";
            this.xrTableRow18.Weight = 7.2D;
            // 
            // xrTableCell47
            // 
            this.xrTableCell47.Angle = 90F;
            this.xrTableCell47.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell47.Name = "xrTableCell47";
            this.xrTableCell47.StylePriority.UseBorders = false;
            this.xrTableCell47.Text = "Nữ từ 51 - 55";
            this.xrTableCell47.Weight = 0.16786330103320563D;
            // 
            // xrTableCell48
            // 
            this.xrTableCell48.Angle = 90F;
            this.xrTableCell48.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell48.Name = "xrTableCell48";
            this.xrTableCell48.StylePriority.UseBorders = false;
            this.xrTableCell48.Text = "Nam từ 51 - 55";
            this.xrTableCell48.Weight = 0.16786330103320563D;
            // 
            // xrTableCell49
            // 
            this.xrTableCell49.Angle = 90F;
            this.xrTableCell49.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell49.Name = "xrTableCell49";
            this.xrTableCell49.StylePriority.UseBorders = false;
            this.xrTableCell49.Text = "Nam từ 56 - 60";
            this.xrTableCell49.Weight = 0.16786331704189805D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell3.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblNgachCongChuc,
            this.xrTable4});
            this.xrTableCell3.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.StylePriority.UseBorderColor = false;
            this.xrTableCell3.StylePriority.UseBorders = false;
            this.xrTableCell3.StylePriority.UseFont = false;
            this.xrTableCell3.Text = "xrTableCell3";
            this.xrTableCell3.Weight = 0.50817334882654508D;
            // 
            // lblNgachCongChuc
            // 
            this.lblNgachCongChuc.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.lblNgachCongChuc.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.lblNgachCongChuc.Name = "lblNgachCongChuc";
            this.lblNgachCongChuc.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.lblNgachCongChuc.SizeF = new System.Drawing.SizeF(80F, 49.99998F);
            this.lblNgachCongChuc.StylePriority.UseBorders = false;
            this.lblNgachCongChuc.StylePriority.UsePadding = false;
            this.lblNgachCongChuc.Text = "Chia theo ngạch";
            // 
            // xrTable4
            // 
            this.xrTable4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 49.99998F);
            this.xrTable4.Name = "xrTable4";
            this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow6});
            this.xrTable4.SizeF = new System.Drawing.SizeF(80F, 180F);
            // 
            // xrTableRow6
            // 
            this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell11,
            this.xrTableCell12,
            this.xrTableCell50,
            this.xrTableCell51});
            this.xrTableRow6.Name = "xrTableRow6";
            this.xrTableRow6.Weight = 0.87804885492092222D;
            // 
            // xrTableCell11
            // 
            this.xrTableCell11.Angle = 90F;
            this.xrTableCell11.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell11.Name = "xrTableCell11";
            this.xrTableCell11.StylePriority.UseBorders = false;
            this.xrTableCell11.Text = "CVC và TĐ";
            this.xrTableCell11.Weight = 0.19999999999999998D;
            // 
            // xrTableCell12
            // 
            this.xrTableCell12.Angle = 90F;
            this.xrTableCell12.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.StylePriority.UseBorders = false;
            this.xrTableCell12.Text = "CV và TĐ";
            this.xrTableCell12.Weight = 0.19999999999999996D;
            // 
            // xrTableCell50
            // 
            this.xrTableCell50.Angle = 90F;
            this.xrTableCell50.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell50.Name = "xrTableCell50";
            this.xrTableCell50.StylePriority.UseBorders = false;
            this.xrTableCell50.Text = "CS và TĐ";
            this.xrTableCell50.Weight = 0.2D;
            // 
            // xrTableCell51
            // 
            this.xrTableCell51.Angle = 90F;
            this.xrTableCell51.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell51.Name = "xrTableCell51";
            this.xrTableCell51.StylePriority.UseBorders = false;
            this.xrTableCell51.Text = "Còn lại";
            this.xrTableCell51.Weight = 0.2D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell4.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell4.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2,
            this.xrLabel1});
            this.xrTableCell4.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.StylePriority.UseBorderColor = false;
            this.xrTableCell4.StylePriority.UseBorders = false;
            this.xrTableCell4.StylePriority.UseFont = false;
            this.xrTableCell4.Weight = 0.25408666170988964D;
            // 
            // xrTable2
            // 
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0.0001373291F, 49.99997F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow7});
            this.xrTable2.SizeF = new System.Drawing.SizeF(40F, 180F);
            // 
            // xrTableRow7
            // 
            this.xrTableRow7.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell9,
            this.xrTableCell10});
            this.xrTableRow7.Name = "xrTableRow7";
            this.xrTableRow7.Weight = 0.79333333333333333D;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.Angle = 90F;
            this.xrTableCell9.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.StylePriority.UseBorders = false;
            this.xrTableCell9.Text = "THCS";
            this.xrTableCell9.Weight = 0.30150747650515286D;
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.Angle = 90F;
            this.xrTableCell10.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.StylePriority.UseBorders = false;
            this.xrTableCell10.Text = "THPT";
            this.xrTableCell10.Weight = 0.30150746683302221D;
            // 
            // xrLabel1
            // 
            this.xrLabel1.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(6.103516E-05F, 0F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(40F, 49.99997F);
            this.xrLabel1.StylePriority.UseBorders = false;
            this.xrLabel1.Text = "Chia theo trình độ văn hóa";
            // 
            // xrTableCell13
            // 
            this.xrTableCell13.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell13.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell13.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable5,
            this.xrLabel2});
            this.xrTableCell13.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell13.Name = "xrTableCell13";
            this.xrTableCell13.StylePriority.UseBorderColor = false;
            this.xrTableCell13.StylePriority.UseBorders = false;
            this.xrTableCell13.StylePriority.UseFont = false;
            this.xrTableCell13.Weight = 0.713021827321715D;
            // 
            // xrTable5
            // 
            this.xrTable5.LocationFloat = new DevExpress.Utils.PointFloat(0F, 49.99997F);
            this.xrTable5.Name = "xrTable5";
            this.xrTable5.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow8});
            this.xrTable5.SizeF = new System.Drawing.SizeF(112.2469F, 180F);
            // 
            // xrTableRow8
            // 
            this.xrTableRow8.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell16,
            this.xrTableCell17,
            this.xrTableCell18,
            this.xrTableCell20,
            this.xrTableCell21});
            this.xrTableRow8.Name = "xrTableRow8";
            this.xrTableRow8.Weight = 1D;
            // 
            // xrTableCell16
            // 
            this.xrTableCell16.Angle = 90F;
            this.xrTableCell16.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell16.Name = "xrTableCell16";
            this.xrTableCell16.StylePriority.UseBorders = false;
            this.xrTableCell16.Text = "Thạc sĩ";
            this.xrTableCell16.Weight = 0.16307143722629D;
            // 
            // xrTableCell17
            // 
            this.xrTableCell17.Angle = 90F;
            this.xrTableCell17.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell17.Name = "xrTableCell17";
            this.xrTableCell17.StylePriority.UseBorders = false;
            this.xrTableCell17.Text = "ĐH";
            this.xrTableCell17.Weight = 0.16307143722629D;
            // 
            // xrTableCell18
            // 
            this.xrTableCell18.Angle = 90F;
            this.xrTableCell18.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell18.Name = "xrTableCell18";
            this.xrTableCell18.StylePriority.UseBorders = false;
            this.xrTableCell18.Text = "CĐ";
            this.xrTableCell18.Weight = 0.16307143418048559D;
            // 
            // xrTableCell20
            // 
            this.xrTableCell20.Angle = 90F;
            this.xrTableCell20.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell20.Name = "xrTableCell20";
            this.xrTableCell20.StylePriority.UseBorders = false;
            this.xrTableCell20.Text = "TC";
            this.xrTableCell20.Weight = 0.1630714405869062D;
            // 
            // xrTableCell21
            // 
            this.xrTableCell21.Angle = 90F;
            this.xrTableCell21.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell21.Name = "xrTableCell21";
            this.xrTableCell21.StylePriority.UseBorders = false;
            this.xrTableCell21.Text = "Còn lại";
            this.xrTableCell21.Weight = 0.16307143691147813D;
            // 
            // xrLabel2
            // 
            this.xrLabel2.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(112.2469F, 49.99998F);
            this.xrLabel2.StylePriority.UseBorders = false;
            this.xrLabel2.StylePriority.UsePadding = false;
            this.xrLabel2.Text = "Chia theo trình độ chuyên môn";
            // 
            // xrTableCell14
            // 
            this.xrTableCell14.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell14.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell14.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable6,
            this.xrLabel3});
            this.xrTableCell14.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell14.Name = "xrTableCell14";
            this.xrTableCell14.StylePriority.UseBorderColor = false;
            this.xrTableCell14.StylePriority.UseBorders = false;
            this.xrTableCell14.StylePriority.UseFont = false;
            this.xrTableCell14.Weight = 0.44465167212971252D;
            // 
            // xrTable6
            // 
            this.xrTable6.LocationFloat = new DevExpress.Utils.PointFloat(0F, 49.99999F);
            this.xrTable6.Name = "xrTable6";
            this.xrTable6.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow9});
            this.xrTable6.SizeF = new System.Drawing.SizeF(70.00098F, 180F);
            // 
            // xrTableRow9
            // 
            this.xrTableRow9.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell23,
            this.xrTableCell24,
            this.xrTableCell25});
            this.xrTableRow9.Name = "xrTableRow9";
            this.xrTableRow9.Weight = 1D;
            // 
            // xrTableCell23
            // 
            this.xrTableCell23.Angle = 90F;
            this.xrTableCell23.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell23.Name = "xrTableCell23";
            this.xrTableCell23.StylePriority.UseBorders = false;
            this.xrTableCell23.Text = "Cử nhân";
            this.xrTableCell23.Weight = 0.19999999999999998D;
            // 
            // xrTableCell24
            // 
            this.xrTableCell24.Angle = 90F;
            this.xrTableCell24.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell24.Name = "xrTableCell24";
            this.xrTableCell24.StylePriority.UseBorders = false;
            this.xrTableCell24.Text = "CC";
            this.xrTableCell24.Weight = 0.19999999999999996D;
            // 
            // xrTableCell25
            // 
            this.xrTableCell25.Angle = 90F;
            this.xrTableCell25.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell25.Multiline = true;
            this.xrTableCell25.Name = "xrTableCell25";
            this.xrTableCell25.StylePriority.UseBorders = false;
            this.xrTableCell25.Text = "TC\r\n";
            this.xrTableCell25.Weight = 0.2D;
            // 
            // xrLabel3
            // 
            this.xrLabel3.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 2.288818E-05F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(70.00269F, 49.99998F);
            this.xrLabel3.StylePriority.UseBorders = false;
            this.xrLabel3.StylePriority.UsePadding = false;
            this.xrLabel3.Text = "Chia theo trình độ LLCT";
            // 
            // xrTableCell15
            // 
            this.xrTableCell15.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell15.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell15.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable7,
            this.xrLabel4});
            this.xrTableCell15.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell15.Name = "xrTableCell15";
            this.xrTableCell15.StylePriority.UseBorderColor = false;
            this.xrTableCell15.StylePriority.UseBorders = false;
            this.xrTableCell15.StylePriority.UseFont = false;
            this.xrTableCell15.Weight = 0.51613138365038624D;
            // 
            // xrTable7
            // 
            this.xrTable7.LocationFloat = new DevExpress.Utils.PointFloat(0.0009765625F, 49.99999F);
            this.xrTable7.Name = "xrTable7";
            this.xrTable7.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow10});
            this.xrTable7.SizeF = new System.Drawing.SizeF(81.24927F, 180F);
            // 
            // xrTableRow10
            // 
            this.xrTableRow10.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell29,
            this.xrTableCell30,
            this.xrTableCell31});
            this.xrTableRow10.Name = "xrTableRow10";
            this.xrTableRow10.Weight = 0.99861119588216141D;
            // 
            // xrTableCell29
            // 
            this.xrTableCell29.Angle = 90F;
            this.xrTableCell29.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell29.Name = "xrTableCell29";
            this.xrTableCell29.StylePriority.UseBorders = false;
            this.xrTableCell29.Text = "Chương trình chuyên viên chính";
            this.xrTableCell29.Weight = 0.22999999999999998D;
            // 
            // xrTableCell30
            // 
            this.xrTableCell30.Angle = 90F;
            this.xrTableCell30.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell30.Name = "xrTableCell30";
            this.xrTableCell30.StylePriority.UseBorders = false;
            this.xrTableCell30.Text = "Chương trình chuyên viên";
            this.xrTableCell30.Weight = 0.22999999999999998D;
            // 
            // xrTableCell31
            // 
            this.xrTableCell31.Angle = 90F;
            this.xrTableCell31.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell31.Multiline = true;
            this.xrTableCell31.Name = "xrTableCell31";
            this.xrTableCell31.StylePriority.UseBorders = false;
            this.xrTableCell31.Text = "QLGD";
            this.xrTableCell31.Weight = 0.23D;
            // 
            // xrLabel4
            // 
            this.xrLabel4.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(81.25018F, 49.99998F);
            this.xrLabel4.StylePriority.UseBorders = false;
            this.xrLabel4.StylePriority.UsePadding = false;
            this.xrLabel4.Text = "Đã học quản lý";
            // 
            // xrTableCell32
            // 
            this.xrTableCell32.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell32.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell32.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable8,
            this.xrLabel5});
            this.xrTableCell32.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell32.Name = "xrTableCell32";
            this.xrTableCell32.StylePriority.UseBorderColor = false;
            this.xrTableCell32.StylePriority.UseBorders = false;
            this.xrTableCell32.StylePriority.UseFont = false;
            this.xrTableCell32.Text = "Ghi chú";
            this.xrTableCell32.Weight = 2.2432807294331671D;
            // 
            // xrTable8
            // 
            this.xrTable8.LocationFloat = new DevExpress.Utils.PointFloat(0.002059937F, 49.99997F);
            this.xrTable8.Name = "xrTable8";
            this.xrTable8.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow12});
            this.xrTable8.SizeF = new System.Drawing.SizeF(353.15F, 180F);
            // 
            // xrTableRow12
            // 
            this.xrTableRow12.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell22,
            this.xrTableCell27,
            this.xrTableCell26,
            this.xrTableCell33,
            this.xrTableCell34,
            this.xrTableCell35,
            this.xrTableCell36,
            this.xrTableCell38,
            this.xrTableCell39,
            this.xrTableCell40,
            this.xrTableCell41,
            this.xrTableCell46,
            this.xrTableCell28,
            this.xrTableCell37});
            this.xrTableRow12.Name = "xrTableRow12";
            this.xrTableRow12.Weight = 0.99861119588216141D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Angle = 90F;
            this.xrTableCell1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseBorders = false;
            this.xrTableCell1.Text = "Thái";
            this.xrTableCell1.Weight = 0.039419885092473295D;
            // 
            // xrTableCell22
            // 
            this.xrTableCell22.Angle = 90F;
            this.xrTableCell22.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell22.Name = "xrTableCell22";
            this.xrTableCell22.StylePriority.UseBorders = false;
            this.xrTableCell22.Text = "Mông";
            this.xrTableCell22.Weight = 0.039419884523953744D;
            // 
            // xrTableCell27
            // 
            this.xrTableCell27.Angle = 90F;
            this.xrTableCell27.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell27.Name = "xrTableCell27";
            this.xrTableCell27.StylePriority.UseBorders = false;
            this.xrTableCell27.Text = "Hà Nhì";
            this.xrTableCell27.Weight = 0.039419884784702951D;
            // 
            // xrTableCell26
            // 
            this.xrTableCell26.Angle = 90F;
            this.xrTableCell26.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell26.Name = "xrTableCell26";
            this.xrTableCell26.StylePriority.UseBorders = false;
            this.xrTableCell26.Text = "Tày";
            this.xrTableCell26.Weight = 0.039419886444324856D;
            // 
            // xrTableCell33
            // 
            this.xrTableCell33.Angle = 90F;
            this.xrTableCell33.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell33.Name = "xrTableCell33";
            this.xrTableCell33.StylePriority.UseBorders = false;
            this.xrTableCell33.Text = "Mường";
            this.xrTableCell33.Weight = 0.039419882684951724D;
            // 
            // xrTableCell34
            // 
            this.xrTableCell34.Angle = 90F;
            this.xrTableCell34.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell34.Name = "xrTableCell34";
            this.xrTableCell34.StylePriority.UseBorders = false;
            this.xrTableCell34.Text = "Dao";
            this.xrTableCell34.Weight = 0.039419882684951724D;
            // 
            // xrTableCell35
            // 
            this.xrTableCell35.Angle = 90F;
            this.xrTableCell35.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell35.Name = "xrTableCell35";
            this.xrTableCell35.StylePriority.UseBorders = false;
            this.xrTableCell35.Text = "Giáy";
            this.xrTableCell35.Weight = 0.039419882684951724D;
            // 
            // xrTableCell36
            // 
            this.xrTableCell36.Angle = 90F;
            this.xrTableCell36.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell36.Name = "xrTableCell36";
            this.xrTableCell36.StylePriority.UseBorders = false;
            this.xrTableCell36.Text = "Cống";
            this.xrTableCell36.Weight = 0.039419890203698016D;
            // 
            // xrTableCell38
            // 
            this.xrTableCell38.Angle = 90F;
            this.xrTableCell38.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell38.Name = "xrTableCell38";
            this.xrTableCell38.StylePriority.UseBorders = false;
            this.xrTableCell38.Text = "Hoa";
            this.xrTableCell38.Weight = 0.039419883968836708D;
            // 
            // xrTableCell39
            // 
            this.xrTableCell39.Angle = 90F;
            this.xrTableCell39.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell39.Name = "xrTableCell39";
            this.xrTableCell39.StylePriority.UseBorders = false;
            this.xrTableCell39.Text = "Si La";
            this.xrTableCell39.Weight = 0.039419883968836708D;
            // 
            // xrTableCell40
            // 
            this.xrTableCell40.Angle = 90F;
            this.xrTableCell40.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell40.Name = "xrTableCell40";
            this.xrTableCell40.StylePriority.UseBorders = false;
            this.xrTableCell40.Text = "Nùng";
            this.xrTableCell40.Weight = 0.039419883968836708D;
            // 
            // xrTableCell41
            // 
            this.xrTableCell41.Angle = 90F;
            this.xrTableCell41.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell41.Name = "xrTableCell41";
            this.xrTableCell41.StylePriority.UseBorders = false;
            this.xrTableCell41.Text = "Cao Lan";
            this.xrTableCell41.Weight = 0.039419883968836729D;
            // 
            // xrTableCell46
            // 
            this.xrTableCell46.Angle = 90F;
            this.xrTableCell46.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell46.Name = "xrTableCell46";
            this.xrTableCell46.StylePriority.UseBorders = false;
            this.xrTableCell46.Text = "La Hủ";
            this.xrTableCell46.Weight = 0.039419883968836715D;
            // 
            // xrTableCell28
            // 
            this.xrTableCell28.Angle = 90F;
            this.xrTableCell28.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell28.Name = "xrTableCell28";
            this.xrTableCell28.StylePriority.UseBorders = false;
            this.xrTableCell28.Text = "Thổ";
            this.xrTableCell28.Weight = 0.039419883968836722D;
            // 
            // xrTableCell37
            // 
            this.xrTableCell37.Angle = 90F;
            this.xrTableCell37.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell37.Name = "xrTableCell37";
            this.xrTableCell37.StylePriority.UseBorders = false;
            this.xrTableCell37.Text = "Các dân tộc khác";
            this.xrTableCell37.Weight = 0.0394198839688367D;
            // 
            // xrLabel5
            // 
            this.xrLabel5.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(0F, 2.288818E-05F);
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(353.1521F, 49.99998F);
            this.xrLabel5.StylePriority.UseBorders = false;
            this.xrLabel5.StylePriority.UsePadding = false;
            this.xrLabel5.Text = "Dân tộc ít người";
            // 
            // formattingRule1
            // 
            this.formattingRule1.Name = "formattingRule1";
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
            this.ReportFooter.HeightF = 252.1667F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // xrTable1
            // 
            this.xrTable1.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(1140F, 25F);
            this.xrTable1.StylePriority.UseBorders = false;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTextTongSo,
            this.xrTongSoCongChuc,
            this.xrTongDangVien,
            this.xrTongNam,
            this.xrTongNu,
            this.xrTongDuoi30,
            this.xrTongTu30Den40,
            this.xrTongTu41Den50,
            this.xrTongNuTu51Den55,
            this.xrTongNamTu51Den55,
            this.xrTongNamTu56Den60,
            this.xrTongNgachCVCVaTD,
            this.xrTongNgachCVVaTD,
            this.xrTongNgachCSVaTD,
            this.xrTongNgachConLai,
            this.xrTongTHCS,
            this.xrTongTHPT,
            this.xrTongCMThacSi,
            this.xrTongCMDaiHoc,
            this.xrTongCMCaoDang,
            this.xrTongCMTrungCap,
            this.xrTongCMConLai,
            this.xrTongLLCTCuNhan,
            this.xrTongLLCTCaoCap,
            this.xrTongLLCTTrungCap,
            this.xrTongQLChuyenVienChinh,
            this.xrTongQLChuyenVien,
            this.xrTongQLQLGD,
            this.xrTongThai,
            this.xrTongMong,
            this.xrTongHaNhi,
            this.xrTongTay,
            this.xrTongMuong,
            this.xrTongDao,
            this.xrTongGiay,
            this.xrTongCong,
            this.xrTongHoa,
            this.xrTongSiLa,
            this.xrTongNung,
            this.xrTongCaoLan,
            this.xrTongLaHu,
            this.xrTongTho,
            this.xrTongDanTocKhac});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTextTongSo
            // 
            this.xrTextTongSo.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTextTongSo.Name = "xrTextTongSo";
            this.xrTextTongSo.StylePriority.UseBorders = false;
            this.xrTextTongSo.Text = " TỔNG TOÀN TRƯỜNG";
            this.xrTextTongSo.Weight = 1.8793633873433202D;
            // 
            // xrTongSoCongChuc
            // 
            this.xrTongSoCongChuc.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongSoCongChuc.Name = "xrTongSoCongChuc";
            this.xrTongSoCongChuc.StylePriority.UseBorders = false;
            this.xrTongSoCongChuc.Weight = 0.39245208989935854D;
            // 
            // xrTongDangVien
            // 
            this.xrTongDangVien.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongDangVien.Name = "xrTongDangVien";
            this.xrTongDangVien.StylePriority.UseBorders = false;
            this.xrTongDangVien.Weight = 0.31393723645852D;
            // 
            // xrTongNam
            // 
            this.xrTongNam.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongNam.Name = "xrTongNam";
            this.xrTongNam.StylePriority.UseBorders = false;
            this.xrTongNam.Weight = 0.21251610330529425D;
            // 
            // xrTongNu
            // 
            this.xrTongNu.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongNu.Name = "xrTongNu";
            this.xrTongNu.StylePriority.UseBorders = false;
            this.xrTongNu.Weight = 0.21251640600434457D;
            // 
            // xrTongDuoi30
            // 
            this.xrTongDuoi30.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongDuoi30.Name = "xrTongDuoi30";
            this.xrTongDuoi30.StylePriority.UseBorders = false;
            this.xrTongDuoi30.Weight = 0.21251606975634024D;
            // 
            // xrTongTu30Den40
            // 
            this.xrTongTu30Den40.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongTu30Den40.Name = "xrTongTu30Den40";
            this.xrTongTu30Den40.StylePriority.UseBorders = false;
            this.xrTongTu30Den40.Weight = 0.21251639403015382D;
            // 
            // xrTongTu41Den50
            // 
            this.xrTongTu41Den50.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongTu41Den50.Name = "xrTongTu41Den50";
            this.xrTongTu41Den50.StylePriority.UseBorders = false;
            this.xrTongTu41Den50.Weight = 0.21251444838727202D;
            // 
            // xrTongNuTu51Den55
            // 
            this.xrTongNuTu51Den55.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongNuTu51Den55.Name = "xrTongNuTu51Den55";
            this.xrTongNuTu51Den55.StylePriority.UseBorders = false;
            this.xrTongNuTu51Den55.Weight = 0.21251574548252655D;
            // 
            // xrTongNamTu51Den55
            // 
            this.xrTongNamTu51Den55.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongNamTu51Den55.Name = "xrTongNamTu51Den55";
            this.xrTongNamTu51Den55.StylePriority.UseBorders = false;
            this.xrTongNamTu51Den55.Weight = 0.21251736685159522D;
            // 
            // xrTongNamTu56Den60
            // 
            this.xrTongNamTu56Den60.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongNamTu56Den60.Name = "xrTongNamTu56Den60";
            this.xrTongNamTu56Den60.StylePriority.UseBorders = false;
            this.xrTongNamTu56Den60.Weight = 0.21251639403015382D;
            // 
            // xrTongNgachCVCVaTD
            // 
            this.xrTongNgachCVCVaTD.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongNgachCVCVaTD.Name = "xrTongNgachCVCVaTD";
            this.xrTongNgachCVCVaTD.StylePriority.UseBorders = false;
            this.xrTongNgachCVCVaTD.Weight = 0.21251575806433476D;
            // 
            // xrTongNgachCVVaTD
            // 
            this.xrTongNgachCVVaTD.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongNgachCVVaTD.Name = "xrTongNgachCVVaTD";
            this.xrTongNgachCVVaTD.StylePriority.UseBorders = false;
            this.xrTongNgachCVVaTD.Weight = 0.21251606975634024D;
            // 
            // xrTongNgachCSVaTD
            // 
            this.xrTongNgachCSVaTD.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongNgachCSVaTD.Name = "xrTongNgachCSVaTD";
            this.xrTongNgachCSVaTD.StylePriority.UseBorders = false;
            this.xrTongNgachCSVaTD.Weight = 0.21251639403015404D;
            // 
            // xrTongNgachConLai
            // 
            this.xrTongNgachConLai.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongNgachConLai.Name = "xrTongNgachConLai";
            this.xrTongNgachConLai.StylePriority.UseBorders = false;
            this.xrTongNgachConLai.Weight = 0.212515096934899D;
            // 
            // xrTongTHCS
            // 
            this.xrTongTHCS.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongTHCS.Name = "xrTongTHCS";
            this.xrTongTHCS.StylePriority.UseBorders = false;
            this.xrTongTHCS.Weight = 0.212517366851595D;
            // 
            // xrTongTHPT
            // 
            this.xrTongTHPT.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongTHPT.Name = "xrTongTHPT";
            this.xrTongTHPT.StylePriority.UseBorders = false;
            this.xrTongTHPT.Weight = 0.21251315129201737D;
            // 
            // xrTongCMThacSi
            // 
            this.xrTongCMThacSi.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongCMThacSi.Name = "xrTongCMThacSi";
            this.xrTongCMThacSi.StylePriority.UseBorders = false;
            this.xrTongCMThacSi.Weight = 0.23854488023249937D;
            // 
            // xrTongCMDaiHoc
            // 
            this.xrTongCMDaiHoc.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongCMDaiHoc.Name = "xrTongCMDaiHoc";
            this.xrTongCMDaiHoc.StylePriority.UseBorders = false;
            this.xrTongCMDaiHoc.Weight = 0.2385422860419901D;
            // 
            // xrTongCMCaoDang
            // 
            this.xrTongCMCaoDang.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongCMCaoDang.Name = "xrTongCMCaoDang";
            this.xrTongCMCaoDang.StylePriority.UseBorders = false;
            this.xrTongCMCaoDang.Weight = 0.23854293458961731D;
            // 
            // xrTongCMTrungCap
            // 
            this.xrTongCMTrungCap.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongCMTrungCap.Name = "xrTongCMTrungCap";
            this.xrTongCMTrungCap.StylePriority.UseBorders = false;
            this.xrTongCMTrungCap.Weight = 0.23854358313724464D;
            // 
            // xrTongCMConLai
            // 
            this.xrTongCMConLai.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongCMConLai.Name = "xrTongCMConLai";
            this.xrTongCMConLai.StylePriority.UseBorders = false;
            this.xrTongCMConLai.Weight = 0.23856109392318331D;
            // 
            // xrTongLLCTCuNhan
            // 
            this.xrTongLLCTCuNhan.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongLLCTCuNhan.Name = "xrTongLLCTCuNhan";
            this.xrTongLLCTCuNhan.StylePriority.UseBorders = false;
            this.xrTongLLCTCuNhan.Weight = 0.24793974116232556D;
            // 
            // xrTongLLCTCaoCap
            // 
            this.xrTongLLCTCaoCap.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongLLCTCaoCap.Name = "xrTongLLCTCaoCap";
            this.xrTongLLCTCaoCap.StylePriority.UseBorders = false;
            this.xrTongLLCTCaoCap.Weight = 0.24793714697181626D;
            // 
            // xrTongLLCTTrungCap
            // 
            this.xrTongLLCTTrungCap.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongLLCTTrungCap.Name = "xrTongLLCTTrungCap";
            this.xrTongLLCTTrungCap.StylePriority.UseBorders = false;
            this.xrTongLLCTTrungCap.Weight = 0.24793066149554283D;
            // 
            // xrTongQLChuyenVienChinh
            // 
            this.xrTongQLChuyenVienChinh.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongQLChuyenVienChinh.Name = "xrTongQLChuyenVienChinh";
            this.xrTongQLChuyenVienChinh.StylePriority.UseBorders = false;
            this.xrTongQLChuyenVienChinh.Weight = 0.2877891015773632D;
            // 
            // xrTongQLChuyenVien
            // 
            this.xrTongQLChuyenVien.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongQLChuyenVien.Name = "xrTongQLChuyenVien";
            this.xrTongQLChuyenVien.StylePriority.UseBorders = false;
            this.xrTongQLChuyenVien.Weight = 0.28777807626769825D;
            // 
            // xrTongQLQLGD
            // 
            this.xrTongQLQLGD.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongQLQLGD.Name = "xrTongQLQLGD";
            this.xrTongQLQLGD.StylePriority.UseBorders = false;
            this.xrTongQLQLGD.Weight = 0.28783064915974077D;
            // 
            // xrTongThai
            // 
            this.xrTongThai.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongThai.Name = "xrTongThai";
            this.xrTongThai.StylePriority.UseBorders = false;
            this.xrTongThai.Weight = 0.25016749367391489D;
            // 
            // xrTongMong
            // 
            this.xrTongMong.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongMong.Name = "xrTongMong";
            this.xrTongMong.StylePriority.UseBorders = false;
            this.xrTongMong.Weight = 0.25016814222154221D;
            // 
            // xrTongHaNhi
            // 
            this.xrTongHaNhi.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongHaNhi.Name = "xrTongHaNhi";
            this.xrTongHaNhi.StylePriority.UseBorders = false;
            this.xrTongHaNhi.Weight = 0.25016684512628751D;
            // 
            // xrTongTay
            // 
            this.xrTongTay.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongTay.Name = "xrTongTay";
            this.xrTongTay.StylePriority.UseBorders = false;
            this.xrTongTay.Weight = 0.25016689579407092D;
            // 
            // xrTongMuong
            // 
            this.xrTongMuong.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongMuong.Name = "xrTongMuong";
            this.xrTongMuong.StylePriority.UseBorders = false;
            this.xrTongMuong.Weight = 0.25016557843170278D;
            // 
            // xrTongDao
            // 
            this.xrTongDao.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongDao.Name = "xrTongDao";
            this.xrTongDao.StylePriority.UseBorders = false;
            this.xrTongDao.Weight = 0.25016814222154227D;
            // 
            // xrTongGiay
            // 
            this.xrTongGiay.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongGiay.Name = "xrTongGiay";
            this.xrTongGiay.StylePriority.UseBorders = false;
            this.xrTongGiay.Weight = 0.2501668451262874D;
            // 
            // xrTongCong
            // 
            this.xrTongCong.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongCong.Name = "xrTongCong";
            this.xrTongCong.StylePriority.UseBorders = false;
            this.xrTongCong.Weight = 0.25016689424888039D;
            // 
            // xrTongHoa
            // 
            this.xrTongHoa.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongHoa.Name = "xrTongHoa";
            this.xrTongHoa.StylePriority.UseBorders = false;
            this.xrTongHoa.Weight = 0.25016624570125279D;
            // 
            // xrTongSiLa
            // 
            this.xrTongSiLa.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongSiLa.Name = "xrTongSiLa";
            this.xrTongSiLa.StylePriority.UseBorders = false;
            this.xrTongSiLa.Weight = 0.25016883989176253D;
            // 
            // xrTongNung
            // 
            this.xrTongNung.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongNung.Name = "xrTongNung";
            this.xrTongNung.StylePriority.UseBorders = false;
            this.xrTongNung.Weight = 0.25016365151074349D;
            // 
            // xrTongCaoLan
            // 
            this.xrTongCaoLan.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongCaoLan.Name = "xrTongCaoLan";
            this.xrTongCaoLan.StylePriority.UseBorders = false;
            this.xrTongCaoLan.Weight = 0.25017078553464456D;
            // 
            // xrTongLaHu
            // 
            this.xrTongLaHu.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongLaHu.Name = "xrTongLaHu";
            this.xrTongLaHu.StylePriority.UseBorders = false;
            this.xrTongLaHu.Weight = 0.25016689424888017D;
            // 
            // xrTongTho
            // 
            this.xrTongTho.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongTho.Name = "xrTongTho";
            this.xrTongTho.StylePriority.UseBorders = false;
            this.xrTongTho.Weight = 0.25016688795797604D;
            // 
            // xrTongDanTocKhac
            // 
            this.xrTongDanTocKhac.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongDanTocKhac.Name = "xrTongDanTocKhac";
            this.xrTongDanTocKhac.StylePriority.UseBorders = false;
            this.xrTongDanTocKhac.Weight = 0.25016115752986967D;
            // 
            // rp_QuantityEthnicMinority
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.PageHeader,
            this.ReportFooter});
            this.BorderColor = System.Drawing.Color.DarkGray;
            this.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.FormattingRuleSheet.AddRange(new DevExpress.XtraReports.UI.FormattingRule[] {
            this.formattingRule1});
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(12, 12, 15, 0);
            this.PageHeight = 827;
            this.PageWidth = 1169;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.Version = "15.1";
            ((System.ComponentModel.ISupportInitialize)(this.tblDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblReportHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblPageHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion
    }
}


