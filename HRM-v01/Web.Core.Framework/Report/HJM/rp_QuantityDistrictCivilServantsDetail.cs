using System;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Web.Core.Service.Catalog;
using Web.Core.Object.Report;
using Web.Core.Framework.Adapter;

namespace Web.Core.Framework.Report
{

    /// <summary>
    /// Bao cao so luong, chat luong can bo cong chuc cap huyen chi tiet
    /// </summary>
    public class rp_QuantityDistrictCivilServantsDetail : XtraReport
    {
        private DetailBand Detail;
        private TopMarginBand TopMargin;
        private BottomMarginBand BottomMargin;
        private ReportHeaderBand ReportHeader;
        private PageHeaderBand PageHeader;
        private FormattingRule formattingRule1;
        private ReportFooterBand ReportFooter;
        private XRLabel lblKyHoTen;
        private XRLabel lblLapBang;
        private XRLabel lblThuTruong;
        private XRTable tblReportHeader;
        private XRTableRow xrTableRow2;
        private XRTableCell xrTableCell401;
        private XRTableCell xrTableCell402;
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
        private XRTableCell xrTableCell1;
        private XRTableCell xrTableCell2;
        private XRLabel lblTrongDo;
        private XRTable xrTable3;
        private XRTableRow xrTableRow5;
        private XRTableCell xrTableCell7;
        private XRTableCell xrTableCell8;
        private XRTableCell xrTableCell9;
        private XRTableCell xrTableCell10;
        private XRTableCell xrTableCell3;
        private XRLabel lblNgachCongChuc;
        private XRTable xrTable4;
        private XRTableRow xrTableRow6;
        private XRTableCell xrTableCell11;
        private XRTableCell xrTableCell12;
        private XRTableCell xrTableCell4;
        private XRLabel lblTrinhDo;
        private XRTable xrTable5;
        private XRTableRow xrTableRow7;
        private XRTableCell xrTableCell13;
        private XRLabel lblChuyenMon;
        private XRTable xrTable6;
        private XRTableRow xrTableRow8;
        private XRTableCell xrTableCell20;
        private XRTableCell xrTableCell21;
        private XRTableCell xrTableCell22;
        private XRTableCell xrTableCell23;
        private XRTableCell xrTableCell24;
        private XRTableCell xrTableCell25;
        private XRTableCell xrTableCell14;
        private XRTableCell xrTableCell15;
        private XRTableCell xrTableCell16;
        private XRTableCell xrTableCell17;
        private XRTableCell xrTableCell18;
        private XRTableCell xrTableCell5;
        private XRTableCell xrTableCell6;
        private XRTableCell xrTableCell603;
        private XRLabel lblChinhTri;
        private XRTable xrTable8;
        private XRTableRow xrTableRow13;
        private XRTableCell xrTableCell29;
        private XRTableCell xrTableCell30;
        private XRTableCell xrTableCell31;
        private XRTableCell xrTableCell32;
        private XRLabel lblTinHoc;
        private XRTable xrTable9;
        private XRTableRow xrTableRow14;
        private XRTableCell xrTableCell33;
        private XRTableCell xrTableCell34;
        private XRLabel lblNgoaiNgu;
        private XRLabel lblTiengAnh;
        private XRLabel lblNgoaiNguKhac;
        private XRTable xrTable10;
        private XRTableRow xrTableRow15;
        private XRTableCell xrTableCell35;
        private XRTableCell xrTableCell36;
        private XRTableCell xrTableCell37;
        private XRTableCell xrTableCell38;
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
        private XRTableCell xrTableCell46;
        private XRTableRow xrTableRow19;
        private XRTableCell xrCellTenBieuMau;
        private XRTableCell xrCellReportName;
        private XRTableCell xrCellOrganization;
        private XRTable xrTable1;
        private XRTableRow xrTableRow1;
        private XRTableCell xrTextTongSo;
        private XRTableCell xrTongSoBienChe;
        private XRTableCell xrTongNu;
        private XRTableCell xrTongDangVien;
        private XRTableCell xrTongDanTocThieuSo;
        private XRTableCell xrTongTonGiao;
        private XRTableCell xrTongSoCongChuc;
        private XRTableCell xrTongChuyenVienCaoCapVaTD;
        private XRTableCell xrTongTienSi;
        private XRTableCell xrTongThacSi;
        private XRTableCell xrTongDaiHoc;
        private XRTableCell xrTongCaoDang;
        private XRTableCell xrTongTrungCap;
        private XRTableCell xrTongSoCap;
        private XRTableCell xrTongCanSuVaTD;
        private XRTableCell xrTongNhanVien;
        private XRTableCell xrTongChuyenVienChinhVaTD;
        private XRTableCell xrTongCuNhanChinhTri;
        private XRTableCell xrTongCaoCapChinhTri;
        private XRTableCell xrTongTrungCapChinhTri;
        private XRTableCell xrTongSoCapChinhTri;
        private XRTableCell xrTongTrungCapTinHoc;
        private XRTableCell xrTongChungChiTinHoc;
        private XRTableCell xrTongDaiHocTiengAnh;
        private XRTableCell xrTongChungChiTiengAnh;
        private XRTableCell xrTongDaiHocNgoaiNguKhac;
        private XRTableCell xrTongChungChiNgoaiNguKhac;
        private XRTableCell xrTongChungChiTiengDanToc;
        private XRTableCell xrTongChuyenVienCaoCap;
        private XRTableCell xrTongChuyenVienChinh;
        private XRTableCell xrTongChuyenVien;
        private XRTableCell xrTongChuyenVienVaTD;
        private XRTableCell xrTong31Den40;
        private XRTableCell xrTong41Den50;
        private XRTableCell xrTong51Den60;
        private XRTableCell xrTongNu51Den55;
        private XRTableCell xrTongNam56Den60;
        private XRTableCell xrTongTrenTuoiNghiHuu;
        private XRTableCell xrTongDuoi30;
        private XRLabel lblReportDate;
        private XRLabel lblKyDongDau;
        private XRTableCell xrTableCell50;
        private XRTableCell xrTableCell51;
        private XRTableCell xrTableCell52;
        private XRLabel lblQuanLyNhaNuoc;
        private XRTable xrTable11;
        private XRTableRow xrTableRow16;
        private XRTableCell xrTableCell39;
        private XRTableCell xrTableCell40;
        private XRTableCell xrTableCell41;
        private GroupHeaderBand GroupHeader1;
        private XRTable xrTable7;
        private XRTableRow xrTableRow10;
        private XRTableCell xrTableCellSoThuTuD;
        private XRTableCell xrTableCellTenCanBo;
        private XRTableCell xrTableCell53;
        private XRTableCell xrTableCell54;
        private XRTableCell xrTableCellNuD;
        private XRTableCell xrTableCellDangVienD;
        private XRTableCell xrTableCellDanTocD;
        private XRTableCell xrTableCellTonGiaoD;
        private XRTableCell xrTableCellChuyenVienCaoCapVaTDD;
        private XRTableCell xrTableCellChuyenVienChinhVaTDD;
        private XRTableCell xrTableCellChuyenVienVaTDD;
        private XRTableCell xrTableCellCanSuVaTDD;
        private XRTableCell xrTableCellNhanVienD;
        private XRTableCell xrTableCellTienSiD;
        private XRTableCell xrTableCellThacSiD;
        private XRTableCell xrTableCellDaiHocD;
        private XRTableCell xrTableCellCaoDangD;
        private XRTableCell xrTableCellTrungCapD;
        private XRTableCell xrTableCellSoCapD;
        private XRTableCell xrTableCellCuNhanChinhTriD;
        private XRTableCell xrTableCellCaoCapChinhTriD;
        private XRTableCell xrTableCellTrungCapChinhTriD;
        private XRTableCell xrTableCellSoCapChinhTriD;
        private XRTableCell xrTableCellTrungCapTinHocD;
        private XRTableCell xrTableCellChungChiTinHocD;
        private XRTableCell xrTableCellDaiHocTiengAnhD;
        private XRTableCell xrTableCellChungChiTiengAnhD;
        private XRTableCell xrTableCellDaiHocNgoaiNguKhacD;
        private XRTableCell xrTableCellChungChiNgoaiNguKhacD;
        private XRTableCell xrTableCellChungChiTiengDanTocD;
        private XRTableCell xrTableCellChuyenVienCaoCapD;
        private XRTableCell xrTableCellChuyenVienChinhD;
        private XRTableCell xrTableCellChuyenVienD;
        private XRTableCell xrTableCellDuoi30D;
        private XRTableCell xrTableCellT31Den40D;
        private XRTableCell xrTableCellT41Den50D;
        private XRTableCell xrTableCellT51Den60D;
        private XRTableCell xrTableCellNu51Den55D;
        private XRTableCell xrTableCellNam56Den60D;
        private XRTableCell xrTableCellTrenTuoiNghiHuuD;
        private XRTable tblDetail;
        private XRTableRow xrDetailRow1;
        private XRTableCell xrTableCellSoThuTu;
        private XRTableCell xrTableCellTenDonVi;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        /// 
        private IContainer components = null;

        public rp_QuantityDistrictCivilServantsDetail()
        {
            InitializeComponent();
        }
        int index = 0;
        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            index++;
            xrTableCellSoThuTuD.Text = index.ToString();


        }
        private void Group_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            index = 0;
            xrTableCellSoThuTuD.Text = index.ToString();

        }
        public void BindData(ReportFilter filter)
        {
            try
            {
                // get organization
                var organization = cat_DepartmentServices.GetByDepartments(filter.SessionDepartment);
                if (organization == null) return;
                // set report name
                xrCellReportName.Text = string.Format(xrCellReportName.Text, filter.Year);
                // set report organization
                xrCellOrganization.Text = string.Format(xrCellOrganization.Text, organization.Name);
                // set report to date
                var toDate = filter.ReportedDate;
                xrCellToDate.Text = string.Format(xrCellToDate.Text, toDate.Day, toDate.Month, toDate.Year);
                // set end report info
                var location = new ReportController().GetCityName(filter.SessionDepartment);
                lblReportDate.Text = string.Format(lblReportDate.Text, location, toDate.Day, toDate.Month, toDate.Year);

                // select form db
                var arrDepartment = string.IsNullOrEmpty(filter.SelectedDepartment)
                    ? new string[] { }
                    : filter.SelectedDepartment.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                for (var i = 0; i < arrDepartment.Length; i++)
                {
                    arrDepartment[i] = "'{0}'".FormatWith(arrDepartment[i]);
                }
                // select from database
                var table = SQLHelper.ExecuteTable(
                    SQLReportAdapter.GetStore_QuantyDistrictDetail(string.Join(",", arrDepartment)));
                // set data source
                DataSource = table;
                //lay tung ccvc
                xrTableCellTenCanBo.DataBindings.Add("Text", DataSource, "fullname");
                xrTableCellNuD.DataBindings.Add("Text", DataSource, "Nu");
                xrTableCellDangVienD.DataBindings.Add("Text", DataSource, "DangVien");
                xrTableCellDanTocD.DataBindings.Add("Text", DataSource, "DanTocThieuSo");
                xrTableCellTonGiaoD.DataBindings.Add("Text", DataSource, "TonGiao");
                xrTableCellChuyenVienCaoCapVaTDD.DataBindings.Add("Text", DataSource, "NgachCVCC");
                xrTableCellChuyenVienChinhVaTDD.DataBindings.Add("Text", DataSource, "NgachCVC");
                xrTableCellChuyenVienVaTDD.DataBindings.Add("Text", DataSource, "NgachCV");
                xrTableCellCanSuVaTDD.DataBindings.Add("Text", DataSource, "NgachCS");
                xrTableCellNhanVienD.DataBindings.Add("Text", DataSource, "NgachNV");
                xrTableCellTienSiD.DataBindings.Add("Text", DataSource, "TienSi");
                xrTableCellThacSiD.DataBindings.Add("Text", DataSource, "ThacSi");
                xrTableCellDaiHocD.DataBindings.Add("Text", DataSource, "DaiHoc");
                xrTableCellCaoDangD.DataBindings.Add("Text", DataSource, "CaoDang");
                xrTableCellTrungCapD.DataBindings.Add("Text", DataSource, "TrungCap");
                xrTableCellSoCapD.DataBindings.Add("Text", DataSource, "SoCap");
                xrTableCellCuNhanChinhTriD.DataBindings.Add("Text", DataSource, "CuNhanChinhTri");
                xrTableCellCaoCapChinhTriD.DataBindings.Add("Text", DataSource, "CaoCapChinhTri");
                xrTableCellTrungCapChinhTriD.DataBindings.Add("Text", DataSource, "TrungCapChinhTri");
                xrTableCellSoCapChinhTriD.DataBindings.Add("Text", DataSource, "SoCapChinhTri");
                xrTableCellTrungCapTinHocD.DataBindings.Add("Text", DataSource, "TrungCapTinHoc");
                xrTableCellChungChiTinHocD.DataBindings.Add("Text", DataSource, "ChungChiTinHoc");
                xrTableCellDaiHocTiengAnhD.DataBindings.Add("Text", DataSource, "DaiHocTiengAnh");
                xrTableCellChungChiTiengAnhD.DataBindings.Add("Text", DataSource, "ChungChiTiengAnh");
                xrTableCellDaiHocNgoaiNguKhacD.DataBindings.Add("Text", DataSource, "DaiHocNgoaiNguKhac");
                xrTableCellChungChiNgoaiNguKhacD.DataBindings.Add("Text", DataSource, "ChungChiNgoaiNguKhac");
                xrTableCellChungChiTiengDanTocD.DataBindings.Add("Text", DataSource, "ChungChiTiengDanToc");
                xrTableCellChuyenVienCaoCapD.DataBindings.Add("Text", DataSource, "ChuyenVienCaoCap");
                xrTableCellChuyenVienChinhD.DataBindings.Add("Text", DataSource, "ChuyenVienChinh");
                xrTableCellChuyenVienD.DataBindings.Add("Text", DataSource, "ChuyenVien");
                xrTableCellDuoi30D.DataBindings.Add("Text", DataSource, "Duoi30");
                xrTableCellT31Den40D.DataBindings.Add("Text", DataSource, "T31Den40");
                xrTableCellT41Den50D.DataBindings.Add("Text", DataSource, "T41Den50");
                xrTableCellT51Den60D.DataBindings.Add("Text", DataSource, "T51Den60");
                xrTableCellNu51Den55D.DataBindings.Add("Text", DataSource, "Nu51Den55");
                xrTableCellNam56Den60D.DataBindings.Add("Text", DataSource, "Nam56Den60");
                xrTableCellTrenTuoiNghiHuuD.DataBindings.Add("Text", DataSource, "TrenTuoiNghiHuu");
                //group header
                GroupHeader1.GroupFields.AddRange(new[] {
                    new GroupField("DepartmentId", XRColumnSortOrder.Ascending)});
                // bind report detail
                xrTableCellTenDonVi.DataBindings.Add("Text", DataSource, "DepartmentName");

                // bind report footer
                xrTongSoBienChe.DataBindings.Add("Text", DataSource, "", "{0:n0}");
                xrTongSoCongChuc.DataBindings.Add("Text", DataSource, "xTongSo", "{0:n0}");
                xrTongNu.DataBindings.Add("Text", DataSource, "xNu", "{0:n0}");
                xrTongDangVien.DataBindings.Add("Text", DataSource, "xDangVien", "{0:n0}");
                xrTongDanTocThieuSo.DataBindings.Add("Text", DataSource, "xDanTocThieuSo", "{0:n0}");
                xrTongTonGiao.DataBindings.Add("Text", DataSource, "xTonGiao", "{0:n0}");
                xrTongChuyenVienCaoCapVaTD.DataBindings.Add("Text", DataSource, "xNgachCVCC", "{0:n0}");
                xrTongChuyenVienChinhVaTD.DataBindings.Add("Text", DataSource, "xNgachCVC", "{0:n0}");
                xrTongChuyenVienVaTD.DataBindings.Add("Text", DataSource, "xNgachCV", "{0:n0}");
                xrTongCanSuVaTD.DataBindings.Add("Text", DataSource, "xNgachCS", "{0:n0}");
                xrTongNhanVien.DataBindings.Add("Text", DataSource, "xNgachNV", "{0:n0}");
                xrTongTienSi.DataBindings.Add("Text", DataSource, "xTienSi", "{0:n0}");
                xrTongThacSi.DataBindings.Add("Text", DataSource, "xThacSi", "{0:n0}");
                xrTongDaiHoc.DataBindings.Add("Text", DataSource, "xDaiHoc", "{0:n0}");
                xrTongCaoDang.DataBindings.Add("Text", DataSource, "xCaoDang", "{0:n0}");
                xrTongTrungCap.DataBindings.Add("Text", DataSource, "xTrungCap", "{0:n0}");
                xrTongSoCap.DataBindings.Add("Text", DataSource, "xSoCap", "{0:n0}");
                xrTongCuNhanChinhTri.DataBindings.Add("Text", DataSource, "xCuNhanChinhTri", "{0:n0}");
                xrTongCaoCapChinhTri.DataBindings.Add("Text", DataSource, "xCaoCapChinhTri", "{0:n0}");
                xrTongTrungCapChinhTri.DataBindings.Add("Text", DataSource, "xTrungCapChinhTri", "{0:n0}");
                xrTongSoCapChinhTri.DataBindings.Add("Text", DataSource, "xSoCapChinhTri", "{0:n0}");
                xrTongTrungCapTinHoc.DataBindings.Add("Text", DataSource, "xTrungCapTinHoc", "{0:n0}");
                xrTongChungChiTinHoc.DataBindings.Add("Text", DataSource, "xChungChiTinHoc", "{0:n0}");
                xrTongDaiHocTiengAnh.DataBindings.Add("Text", DataSource, "xDaiHocTiengAnh", "{0:n0}");
                xrTongChungChiTiengAnh.DataBindings.Add("Text", DataSource, "xChungChiTiengAnh", "{0:n0}");
                xrTongDaiHocNgoaiNguKhac.DataBindings.Add("Text", DataSource, "xDaiHocNgoaiNguKhac", "{0:n0}");
                xrTongChungChiNgoaiNguKhac.DataBindings.Add("Text", DataSource, "xChungChiNgoaiNguKhac", "{0:n0}");
                xrTongChungChiTiengDanToc.DataBindings.Add("Text", DataSource, "xChungChiTiengDanToc", "{0:n0}");
                xrTongChuyenVienCaoCap.DataBindings.Add("Text", DataSource, "xChuyenVienCaoCap", "{0:n0}");
                xrTongChuyenVienChinh.DataBindings.Add("Text", DataSource, "xChuyenVienChinh", "{0:n0}");
                xrTongChuyenVien.DataBindings.Add("Text", DataSource, "xChuyenVien", "{0:n0}");
                xrTongDuoi30.DataBindings.Add("Text", DataSource, "xDuoi30", "{0:n0}");
                xrTong31Den40.DataBindings.Add("Text", DataSource, "x31Den40", "{0:n0}");
                xrTong41Den50.DataBindings.Add("Text", DataSource, "x41Den50", "{0:n0}");
                xrTong51Den60.DataBindings.Add("Text", DataSource, "x51Den60", "{0:n0}");
                xrTongNu51Den55.DataBindings.Add("Text", DataSource, "xNu51Den55", "{0:n0}");
                xrTongNam56Den60.DataBindings.Add("Text", DataSource, "xNam56Den60", "{0:n0}");
                xrTongTrenTuoiNghiHuu.DataBindings.Add("Text", DataSource, "xTrenTuoiNghiHuu", "{0:n0}");
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
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable7 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow10 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCellSoThuTuD = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTenCanBo = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell53 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell54 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellNuD = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellDangVienD = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellDanTocD = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTonGiaoD = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellChuyenVienCaoCapVaTDD = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellChuyenVienChinhVaTDD = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellChuyenVienVaTDD = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellCanSuVaTDD = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellNhanVienD = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTienSiD = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellThacSiD = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellDaiHocD = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellCaoDangD = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTrungCapD = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellSoCapD = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellCuNhanChinhTriD = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellCaoCapChinhTriD = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTrungCapChinhTriD = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellSoCapChinhTriD = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTrungCapTinHocD = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellChungChiTinHocD = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellDaiHocTiengAnhD = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellChungChiTiengAnhD = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellDaiHocNgoaiNguKhacD = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellChungChiNgoaiNguKhacD = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellChungChiTiengDanTocD = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellChuyenVienCaoCapD = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellChuyenVienChinhD = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellChuyenVienD = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellDuoi30D = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellT31Den40D = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellT41Den50D = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellT51Den60D = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellNu51Den55D = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellNam56Den60D = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTrenTuoiNghiHuuD = new DevExpress.XtraReports.UI.XRTableCell();
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
            this.xrTableCell402 = new DevExpress.XtraReports.UI.XRTableCell();
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
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblTrongDo = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblNgachCongChuc = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell50 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell51 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell52 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblTrinhDo = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable5 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable6 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell25 = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblChuyenMon = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblChinhTri = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable8 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow13 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell29 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell30 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell31 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell32 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblTinHoc = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable9 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow14 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell33 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell34 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblNgoaiNgu = new DevExpress.XtraReports.UI.XRLabel();
            this.lblTiengAnh = new DevExpress.XtraReports.UI.XRLabel();
            this.lblNgoaiNguKhac = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable10 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow15 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell35 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell36 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell37 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell38 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblQuanLyNhaNuoc = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable11 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow16 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell39 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell40 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell41 = new DevExpress.XtraReports.UI.XRTableCell();
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
            this.xrTableCell46 = new DevExpress.XtraReports.UI.XRTableCell();
            this.formattingRule1 = new DevExpress.XtraReports.UI.FormattingRule();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.lblReportDate = new DevExpress.XtraReports.UI.XRLabel();
            this.lblKyDongDau = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTextTongSo = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongSoBienChe = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongSoCongChuc = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongNu = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongDangVien = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongDanTocThieuSo = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongTonGiao = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongChuyenVienCaoCapVaTD = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongChuyenVienChinhVaTD = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongChuyenVienVaTD = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongCanSuVaTD = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongNhanVien = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongTienSi = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongThacSi = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongDaiHoc = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongCaoDang = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongTrungCap = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongSoCap = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongCuNhanChinhTri = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongCaoCapChinhTri = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongTrungCapChinhTri = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongSoCapChinhTri = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongTrungCapTinHoc = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongChungChiTinHoc = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongDaiHocTiengAnh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongChungChiTiengAnh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongDaiHocNgoaiNguKhac = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongChungChiNgoaiNguKhac = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongChungChiTiengDanToc = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongChuyenVienCaoCap = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongChuyenVienChinh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongChuyenVien = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongDuoi30 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTong31Den40 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTong41Den50 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTong51Den60 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongNu51Den55 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongNam56Den60 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTongTrenTuoiNghiHuu = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblKyHoTen = new DevExpress.XtraReports.UI.XRLabel();
            this.lblLapBang = new DevExpress.XtraReports.UI.XRLabel();
            this.lblThuTruong = new DevExpress.XtraReports.UI.XRLabel();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.tblDetail = new DevExpress.XtraReports.UI.XRTable();
            this.xrDetailRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCellSoThuTu = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTenDonVi = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblReportHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblPageHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable7});
            this.Detail.HeightF = 25F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrTable7
            // 
            this.xrTable7.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTable7.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable7.Name = "xrTable7";
            this.xrTable7.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow10});
            this.xrTable7.SizeF = new System.Drawing.SizeF(1138F, 25F);
            this.xrTable7.StylePriority.UseBorders = false;
            // 
            // xrTableRow10
            // 
            this.xrTableRow10.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCellSoThuTuD,
            this.xrTableCellTenCanBo,
            this.xrTableCell53,
            this.xrTableCell54,
            this.xrTableCellNuD,
            this.xrTableCellDangVienD,
            this.xrTableCellDanTocD,
            this.xrTableCellTonGiaoD,
            this.xrTableCellChuyenVienCaoCapVaTDD,
            this.xrTableCellChuyenVienChinhVaTDD,
            this.xrTableCellChuyenVienVaTDD,
            this.xrTableCellCanSuVaTDD,
            this.xrTableCellNhanVienD,
            this.xrTableCellTienSiD,
            this.xrTableCellThacSiD,
            this.xrTableCellDaiHocD,
            this.xrTableCellCaoDangD,
            this.xrTableCellTrungCapD,
            this.xrTableCellSoCapD,
            this.xrTableCellCuNhanChinhTriD,
            this.xrTableCellCaoCapChinhTriD,
            this.xrTableCellTrungCapChinhTriD,
            this.xrTableCellSoCapChinhTriD,
            this.xrTableCellTrungCapTinHocD,
            this.xrTableCellChungChiTinHocD,
            this.xrTableCellDaiHocTiengAnhD,
            this.xrTableCellChungChiTiengAnhD,
            this.xrTableCellDaiHocNgoaiNguKhacD,
            this.xrTableCellChungChiNgoaiNguKhacD,
            this.xrTableCellChungChiTiengDanTocD,
            this.xrTableCellChuyenVienCaoCapD,
            this.xrTableCellChuyenVienChinhD,
            this.xrTableCellChuyenVienD,
            this.xrTableCellDuoi30D,
            this.xrTableCellT31Den40D,
            this.xrTableCellT41Den50D,
            this.xrTableCellT51Den60D,
            this.xrTableCellNu51Den55D,
            this.xrTableCellNam56Den60D,
            this.xrTableCellTrenTuoiNghiHuuD});
            this.xrTableRow10.Name = "xrTableRow10";
            this.xrTableRow10.Weight = 1D;
            // 
            // xrTableCellSoThuTuD
            // 
            this.xrTableCellSoThuTuD.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellSoThuTuD.Name = "xrTableCellSoThuTuD";
            this.xrTableCellSoThuTuD.StylePriority.UseBorders = false;
            this.xrTableCellSoThuTuD.StylePriority.UseTextAlignment = false;
            this.xrTableCellSoThuTuD.Text = " ";
            this.xrTableCellSoThuTuD.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellSoThuTuD.Weight = 0.22999999999999998D;
            this.xrTableCellSoThuTuD.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail_BeforePrint);
            // 
            // xrTableCellTenCanBo
            // 
            this.xrTableCellTenCanBo.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTenCanBo.Name = "xrTableCellTenCanBo";
            this.xrTableCellTenCanBo.StylePriority.UseBorders = false;
            this.xrTableCellTenCanBo.StylePriority.UseTextAlignment = false;
            this.xrTableCellTenCanBo.Text = " ";
            this.xrTableCellTenCanBo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellTenCanBo.Weight = 2.4D;
            // 
            // xrTableCell53
            // 
            this.xrTableCell53.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell53.Name = "xrTableCell53";
            this.xrTableCell53.StylePriority.UseBorders = false;
            this.xrTableCell53.StylePriority.UseTextAlignment = false;
            this.xrTableCell53.Text = " ";
            this.xrTableCell53.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell53.Weight = 0.22999999999999998D;
            // 
            // xrTableCell54
            // 
            this.xrTableCell54.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell54.Name = "xrTableCell54";
            this.xrTableCell54.StylePriority.UseBorders = false;
            this.xrTableCell54.StylePriority.UseTextAlignment = false;
            this.xrTableCell54.Text = " ";
            this.xrTableCell54.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell54.Weight = 0.22999999999999998D;
            // 
            // xrTableCellNuD
            // 
            this.xrTableCellNuD.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellNuD.Name = "xrTableCellNuD";
            this.xrTableCellNuD.StylePriority.UseBorders = false;
            this.xrTableCellNuD.StylePriority.UseTextAlignment = false;
            this.xrTableCellNuD.Text = " ";
            this.xrTableCellNuD.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellNuD.Weight = 0.22999999999999998D;
            // 
            // xrTableCellDangVienD
            // 
            this.xrTableCellDangVienD.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellDangVienD.Name = "xrTableCellDangVienD";
            this.xrTableCellDangVienD.StylePriority.UseBorders = false;
            this.xrTableCellDangVienD.StylePriority.UseTextAlignment = false;
            this.xrTableCellDangVienD.Text = " ";
            this.xrTableCellDangVienD.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellDangVienD.Weight = 0.22999999999999998D;
            // 
            // xrTableCellDanTocD
            // 
            this.xrTableCellDanTocD.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellDanTocD.Name = "xrTableCellDanTocD";
            this.xrTableCellDanTocD.StylePriority.UseBorders = false;
            this.xrTableCellDanTocD.StylePriority.UseTextAlignment = false;
            this.xrTableCellDanTocD.Text = " ";
            this.xrTableCellDanTocD.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellDanTocD.Weight = 0.22999999999999998D;
            // 
            // xrTableCellTonGiaoD
            // 
            this.xrTableCellTonGiaoD.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTonGiaoD.Name = "xrTableCellTonGiaoD";
            this.xrTableCellTonGiaoD.StylePriority.UseBorders = false;
            this.xrTableCellTonGiaoD.StylePriority.UseTextAlignment = false;
            this.xrTableCellTonGiaoD.Text = " ";
            this.xrTableCellTonGiaoD.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTonGiaoD.Weight = 0.22999999999999998D;
            // 
            // xrTableCellChuyenVienCaoCapVaTDD
            // 
            this.xrTableCellChuyenVienCaoCapVaTDD.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellChuyenVienCaoCapVaTDD.Name = "xrTableCellChuyenVienCaoCapVaTDD";
            this.xrTableCellChuyenVienCaoCapVaTDD.StylePriority.UseBorders = false;
            this.xrTableCellChuyenVienCaoCapVaTDD.StylePriority.UseTextAlignment = false;
            this.xrTableCellChuyenVienCaoCapVaTDD.Text = " ";
            this.xrTableCellChuyenVienCaoCapVaTDD.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellChuyenVienCaoCapVaTDD.Weight = 0.22999999999999998D;
            // 
            // xrTableCellChuyenVienChinhVaTDD
            // 
            this.xrTableCellChuyenVienChinhVaTDD.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellChuyenVienChinhVaTDD.Name = "xrTableCellChuyenVienChinhVaTDD";
            this.xrTableCellChuyenVienChinhVaTDD.StylePriority.UseBorders = false;
            this.xrTableCellChuyenVienChinhVaTDD.StylePriority.UseTextAlignment = false;
            this.xrTableCellChuyenVienChinhVaTDD.Text = " ";
            this.xrTableCellChuyenVienChinhVaTDD.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellChuyenVienChinhVaTDD.Weight = 0.22999999999999998D;
            // 
            // xrTableCellChuyenVienVaTDD
            // 
            this.xrTableCellChuyenVienVaTDD.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellChuyenVienVaTDD.Name = "xrTableCellChuyenVienVaTDD";
            this.xrTableCellChuyenVienVaTDD.StylePriority.UseBorders = false;
            this.xrTableCellChuyenVienVaTDD.StylePriority.UseTextAlignment = false;
            this.xrTableCellChuyenVienVaTDD.Text = " ";
            this.xrTableCellChuyenVienVaTDD.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellChuyenVienVaTDD.Weight = 0.22999999999999998D;
            // 
            // xrTableCellCanSuVaTDD
            // 
            this.xrTableCellCanSuVaTDD.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellCanSuVaTDD.Name = "xrTableCellCanSuVaTDD";
            this.xrTableCellCanSuVaTDD.StylePriority.UseBorders = false;
            this.xrTableCellCanSuVaTDD.StylePriority.UseTextAlignment = false;
            this.xrTableCellCanSuVaTDD.Text = " ";
            this.xrTableCellCanSuVaTDD.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellCanSuVaTDD.Weight = 0.22999999999999998D;
            // 
            // xrTableCellNhanVienD
            // 
            this.xrTableCellNhanVienD.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellNhanVienD.Name = "xrTableCellNhanVienD";
            this.xrTableCellNhanVienD.StylePriority.UseBorders = false;
            this.xrTableCellNhanVienD.StylePriority.UseTextAlignment = false;
            this.xrTableCellNhanVienD.Text = " ";
            this.xrTableCellNhanVienD.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellNhanVienD.Weight = 0.22999999999999998D;
            // 
            // xrTableCellTienSiD
            // 
            this.xrTableCellTienSiD.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTienSiD.Name = "xrTableCellTienSiD";
            this.xrTableCellTienSiD.StylePriority.UseBorders = false;
            this.xrTableCellTienSiD.StylePriority.UseTextAlignment = false;
            this.xrTableCellTienSiD.Text = " ";
            this.xrTableCellTienSiD.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTienSiD.Weight = 0.22999999999999998D;
            // 
            // xrTableCellThacSiD
            // 
            this.xrTableCellThacSiD.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellThacSiD.Name = "xrTableCellThacSiD";
            this.xrTableCellThacSiD.StylePriority.UseBorders = false;
            this.xrTableCellThacSiD.StylePriority.UseTextAlignment = false;
            this.xrTableCellThacSiD.Text = " ";
            this.xrTableCellThacSiD.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellThacSiD.Weight = 0.22999999999999998D;
            // 
            // xrTableCellDaiHocD
            // 
            this.xrTableCellDaiHocD.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellDaiHocD.Name = "xrTableCellDaiHocD";
            this.xrTableCellDaiHocD.StylePriority.UseBorders = false;
            this.xrTableCellDaiHocD.StylePriority.UseTextAlignment = false;
            this.xrTableCellDaiHocD.Text = " ";
            this.xrTableCellDaiHocD.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellDaiHocD.Weight = 0.22999999999999998D;
            // 
            // xrTableCellCaoDangD
            // 
            this.xrTableCellCaoDangD.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellCaoDangD.Name = "xrTableCellCaoDangD";
            this.xrTableCellCaoDangD.StylePriority.UseBorders = false;
            this.xrTableCellCaoDangD.StylePriority.UseTextAlignment = false;
            this.xrTableCellCaoDangD.Text = " ";
            this.xrTableCellCaoDangD.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellCaoDangD.Weight = 0.22999999999999998D;
            // 
            // xrTableCellTrungCapD
            // 
            this.xrTableCellTrungCapD.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTrungCapD.Name = "xrTableCellTrungCapD";
            this.xrTableCellTrungCapD.StylePriority.UseBorders = false;
            this.xrTableCellTrungCapD.StylePriority.UseTextAlignment = false;
            this.xrTableCellTrungCapD.Text = " ";
            this.xrTableCellTrungCapD.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTrungCapD.Weight = 0.22999999999999998D;
            // 
            // xrTableCellSoCapD
            // 
            this.xrTableCellSoCapD.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellSoCapD.Name = "xrTableCellSoCapD";
            this.xrTableCellSoCapD.StylePriority.UseBorders = false;
            this.xrTableCellSoCapD.StylePriority.UseTextAlignment = false;
            this.xrTableCellSoCapD.Text = " ";
            this.xrTableCellSoCapD.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellSoCapD.Weight = 0.22999999999999998D;
            // 
            // xrTableCellCuNhanChinhTriD
            // 
            this.xrTableCellCuNhanChinhTriD.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellCuNhanChinhTriD.Name = "xrTableCellCuNhanChinhTriD";
            this.xrTableCellCuNhanChinhTriD.StylePriority.UseBorders = false;
            this.xrTableCellCuNhanChinhTriD.StylePriority.UseTextAlignment = false;
            this.xrTableCellCuNhanChinhTriD.Text = " ";
            this.xrTableCellCuNhanChinhTriD.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellCuNhanChinhTriD.Weight = 0.22999999999999998D;
            // 
            // xrTableCellCaoCapChinhTriD
            // 
            this.xrTableCellCaoCapChinhTriD.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellCaoCapChinhTriD.Name = "xrTableCellCaoCapChinhTriD";
            this.xrTableCellCaoCapChinhTriD.StylePriority.UseBorders = false;
            this.xrTableCellCaoCapChinhTriD.StylePriority.UseTextAlignment = false;
            this.xrTableCellCaoCapChinhTriD.Text = " ";
            this.xrTableCellCaoCapChinhTriD.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellCaoCapChinhTriD.Weight = 0.22999999999999998D;
            // 
            // xrTableCellTrungCapChinhTriD
            // 
            this.xrTableCellTrungCapChinhTriD.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTrungCapChinhTriD.Name = "xrTableCellTrungCapChinhTriD";
            this.xrTableCellTrungCapChinhTriD.StylePriority.UseBorders = false;
            this.xrTableCellTrungCapChinhTriD.StylePriority.UseTextAlignment = false;
            this.xrTableCellTrungCapChinhTriD.Text = " ";
            this.xrTableCellTrungCapChinhTriD.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTrungCapChinhTriD.Weight = 0.22999999999999998D;
            // 
            // xrTableCellSoCapChinhTriD
            // 
            this.xrTableCellSoCapChinhTriD.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellSoCapChinhTriD.Name = "xrTableCellSoCapChinhTriD";
            this.xrTableCellSoCapChinhTriD.StylePriority.UseBorders = false;
            this.xrTableCellSoCapChinhTriD.StylePriority.UseTextAlignment = false;
            this.xrTableCellSoCapChinhTriD.Text = " ";
            this.xrTableCellSoCapChinhTriD.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellSoCapChinhTriD.Weight = 0.22999999999999998D;
            // 
            // xrTableCellTrungCapTinHocD
            // 
            this.xrTableCellTrungCapTinHocD.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTrungCapTinHocD.Name = "xrTableCellTrungCapTinHocD";
            this.xrTableCellTrungCapTinHocD.StylePriority.UseBorders = false;
            this.xrTableCellTrungCapTinHocD.StylePriority.UseTextAlignment = false;
            this.xrTableCellTrungCapTinHocD.Text = " ";
            this.xrTableCellTrungCapTinHocD.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTrungCapTinHocD.Weight = 0.22999999999999998D;
            // 
            // xrTableCellChungChiTinHocD
            // 
            this.xrTableCellChungChiTinHocD.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellChungChiTinHocD.Name = "xrTableCellChungChiTinHocD";
            this.xrTableCellChungChiTinHocD.StylePriority.UseBorders = false;
            this.xrTableCellChungChiTinHocD.StylePriority.UseTextAlignment = false;
            this.xrTableCellChungChiTinHocD.Text = " ";
            this.xrTableCellChungChiTinHocD.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellChungChiTinHocD.Weight = 0.22999999999999998D;
            // 
            // xrTableCellDaiHocTiengAnhD
            // 
            this.xrTableCellDaiHocTiengAnhD.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellDaiHocTiengAnhD.Name = "xrTableCellDaiHocTiengAnhD";
            this.xrTableCellDaiHocTiengAnhD.StylePriority.UseBorders = false;
            this.xrTableCellDaiHocTiengAnhD.StylePriority.UseTextAlignment = false;
            this.xrTableCellDaiHocTiengAnhD.Text = " ";
            this.xrTableCellDaiHocTiengAnhD.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellDaiHocTiengAnhD.Weight = 0.22999999999999998D;
            // 
            // xrTableCellChungChiTiengAnhD
            // 
            this.xrTableCellChungChiTiengAnhD.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellChungChiTiengAnhD.Name = "xrTableCellChungChiTiengAnhD";
            this.xrTableCellChungChiTiengAnhD.StylePriority.UseBorders = false;
            this.xrTableCellChungChiTiengAnhD.StylePriority.UseTextAlignment = false;
            this.xrTableCellChungChiTiengAnhD.Text = " ";
            this.xrTableCellChungChiTiengAnhD.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellChungChiTiengAnhD.Weight = 0.22999999999999998D;
            // 
            // xrTableCellDaiHocNgoaiNguKhacD
            // 
            this.xrTableCellDaiHocNgoaiNguKhacD.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellDaiHocNgoaiNguKhacD.Name = "xrTableCellDaiHocNgoaiNguKhacD";
            this.xrTableCellDaiHocNgoaiNguKhacD.StylePriority.UseBorders = false;
            this.xrTableCellDaiHocNgoaiNguKhacD.StylePriority.UseTextAlignment = false;
            this.xrTableCellDaiHocNgoaiNguKhacD.Text = " ";
            this.xrTableCellDaiHocNgoaiNguKhacD.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellDaiHocNgoaiNguKhacD.Weight = 0.22999999999999998D;
            // 
            // xrTableCellChungChiNgoaiNguKhacD
            // 
            this.xrTableCellChungChiNgoaiNguKhacD.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellChungChiNgoaiNguKhacD.Name = "xrTableCellChungChiNgoaiNguKhacD";
            this.xrTableCellChungChiNgoaiNguKhacD.StylePriority.UseBorders = false;
            this.xrTableCellChungChiNgoaiNguKhacD.StylePriority.UseTextAlignment = false;
            this.xrTableCellChungChiNgoaiNguKhacD.Text = " ";
            this.xrTableCellChungChiNgoaiNguKhacD.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellChungChiNgoaiNguKhacD.Weight = 0.22999999999999998D;
            // 
            // xrTableCellChungChiTiengDanTocD
            // 
            this.xrTableCellChungChiTiengDanTocD.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellChungChiTiengDanTocD.Name = "xrTableCellChungChiTiengDanTocD";
            this.xrTableCellChungChiTiengDanTocD.StylePriority.UseBorders = false;
            this.xrTableCellChungChiTiengDanTocD.StylePriority.UseTextAlignment = false;
            this.xrTableCellChungChiTiengDanTocD.Text = " ";
            this.xrTableCellChungChiTiengDanTocD.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellChungChiTiengDanTocD.Weight = 0.22999999999999998D;
            // 
            // xrTableCellChuyenVienCaoCapD
            // 
            this.xrTableCellChuyenVienCaoCapD.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellChuyenVienCaoCapD.Name = "xrTableCellChuyenVienCaoCapD";
            this.xrTableCellChuyenVienCaoCapD.StylePriority.UseBorders = false;
            this.xrTableCellChuyenVienCaoCapD.StylePriority.UseTextAlignment = false;
            this.xrTableCellChuyenVienCaoCapD.Text = " ";
            this.xrTableCellChuyenVienCaoCapD.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellChuyenVienCaoCapD.Weight = 0.22999999999999998D;
            // 
            // xrTableCellChuyenVienChinhD
            // 
            this.xrTableCellChuyenVienChinhD.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellChuyenVienChinhD.Name = "xrTableCellChuyenVienChinhD";
            this.xrTableCellChuyenVienChinhD.StylePriority.UseBorders = false;
            this.xrTableCellChuyenVienChinhD.StylePriority.UseTextAlignment = false;
            this.xrTableCellChuyenVienChinhD.Text = " ";
            this.xrTableCellChuyenVienChinhD.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellChuyenVienChinhD.Weight = 0.22999999999999998D;
            // 
            // xrTableCellChuyenVienD
            // 
            this.xrTableCellChuyenVienD.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellChuyenVienD.Name = "xrTableCellChuyenVienD";
            this.xrTableCellChuyenVienD.StylePriority.UseBorders = false;
            this.xrTableCellChuyenVienD.StylePriority.UseTextAlignment = false;
            this.xrTableCellChuyenVienD.Text = " ";
            this.xrTableCellChuyenVienD.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellChuyenVienD.Weight = 0.24D;
            // 
            // xrTableCellDuoi30D
            // 
            this.xrTableCellDuoi30D.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellDuoi30D.Name = "xrTableCellDuoi30D";
            this.xrTableCellDuoi30D.StylePriority.UseBorders = false;
            this.xrTableCellDuoi30D.StylePriority.UseTextAlignment = false;
            this.xrTableCellDuoi30D.Text = " ";
            this.xrTableCellDuoi30D.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellDuoi30D.Weight = 0.22999999999999998D;
            // 
            // xrTableCellT31Den40D
            // 
            this.xrTableCellT31Den40D.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellT31Den40D.Name = "xrTableCellT31Den40D";
            this.xrTableCellT31Den40D.StylePriority.UseBorders = false;
            this.xrTableCellT31Den40D.StylePriority.UseTextAlignment = false;
            this.xrTableCellT31Den40D.Text = " ";
            this.xrTableCellT31Den40D.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellT31Den40D.Weight = 0.22999999999999998D;
            // 
            // xrTableCellT41Den50D
            // 
            this.xrTableCellT41Den50D.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellT41Den50D.Name = "xrTableCellT41Den50D";
            this.xrTableCellT41Den50D.StylePriority.UseBorders = false;
            this.xrTableCellT41Den50D.StylePriority.UseTextAlignment = false;
            this.xrTableCellT41Den50D.Text = " ";
            this.xrTableCellT41Den50D.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellT41Den50D.Weight = 0.22999999999999998D;
            // 
            // xrTableCellT51Den60D
            // 
            this.xrTableCellT51Den60D.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellT51Den60D.Name = "xrTableCellT51Den60D";
            this.xrTableCellT51Den60D.StylePriority.UseBorders = false;
            this.xrTableCellT51Den60D.StylePriority.UseTextAlignment = false;
            this.xrTableCellT51Den60D.Text = " ";
            this.xrTableCellT51Den60D.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellT51Den60D.Weight = 0.22999999999999998D;
            // 
            // xrTableCellNu51Den55D
            // 
            this.xrTableCellNu51Den55D.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellNu51Den55D.Name = "xrTableCellNu51Den55D";
            this.xrTableCellNu51Den55D.StylePriority.UseBorders = false;
            this.xrTableCellNu51Den55D.StylePriority.UseTextAlignment = false;
            this.xrTableCellNu51Den55D.Text = " ";
            this.xrTableCellNu51Den55D.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellNu51Den55D.Weight = 0.22999999999999998D;
            // 
            // xrTableCellNam56Den60D
            // 
            this.xrTableCellNam56Den60D.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellNam56Den60D.Name = "xrTableCellNam56Den60D";
            this.xrTableCellNam56Den60D.StylePriority.UseBorders = false;
            this.xrTableCellNam56Den60D.StylePriority.UseTextAlignment = false;
            this.xrTableCellNam56Den60D.Text = " ";
            this.xrTableCellNam56Den60D.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellNam56Den60D.Weight = 0.22999999999999998D;
            // 
            // xrTableCellTrenTuoiNghiHuuD
            // 
            this.xrTableCellTrenTuoiNghiHuuD.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTrenTuoiNghiHuuD.Name = "xrTableCellTrenTuoiNghiHuuD";
            this.xrTableCellTrenTuoiNghiHuuD.StylePriority.UseBorders = false;
            this.xrTableCellTrenTuoiNghiHuuD.StylePriority.UseTextAlignment = false;
            this.xrTableCellTrenTuoiNghiHuuD.Text = " ";
            this.xrTableCellTrenTuoiNghiHuuD.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellTrenTuoiNghiHuuD.Weight = 0.22999999999999998D;
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
            this.xrCellTenBieuMau.Text = "BM01/BNV";
            this.xrCellTenBieuMau.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrCellTenBieuMau.Weight = 1.5748560589095728D;
            // 
            // xrCellReportName
            // 
            this.xrCellReportName.Font = new System.Drawing.Font("Times New Roman", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.xrCellReportName.Name = "xrCellReportName";
            this.xrCellReportName.StylePriority.UseFont = false;
            this.xrCellReportName.Text = "BÁO CÁO SỐ LƯỢNG, CHẤT LƯỢNG CÔNG CHỨC TỪ CẤP HUYỆN TRỞ LÊN NĂM {0}";
            this.xrCellReportName.Weight = 3.2928199055848522D;
            // 
            // xrCellOrganization
            // 
            this.xrCellOrganization.Font = new System.Drawing.Font("Times New Roman", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.xrCellOrganization.Name = "xrCellOrganization";
            this.xrCellOrganization.StylePriority.UseFont = false;
            this.xrCellOrganization.StylePriority.UseTextAlignment = false;
            this.xrCellOrganization.Text = "Đơn vị báo cáo: {0}";
            this.xrCellOrganization.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrCellOrganization.Weight = 1.1279513169596362D;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell401,
            this.xrTableCell402,
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
            this.xrTableCell401.Text = "Ban hành kèm theo Thông tư số 11/2012/TT-BNV\r\nngày 17 tháng 12 năm 2012 của Bộ Nộ" +
    "i vụ";
            this.xrTableCell401.Weight = 1.5748560433256003D;
            // 
            // xrTableCell402
            // 
            this.xrTableCell402.Font = new System.Drawing.Font("Times New Roman", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.xrTableCell402.Name = "xrTableCell402";
            this.xrTableCell402.StylePriority.UseFont = false;
            this.xrTableCell402.Text = "(Áp dụng đối với tỉnh, thành phố trực thuộc Trung ương, Bộ, Ban, ngành)";
            this.xrTableCell402.Weight = 3.2928199438412697D;
            // 
            // xrTableCell403
            // 
            this.xrTableCell403.Font = new System.Drawing.Font("Times New Roman", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.xrTableCell403.Name = "xrTableCell403";
            this.xrTableCell403.StylePriority.UseFont = false;
            this.xrTableCell403.StylePriority.UseTextAlignment = false;
            this.xrTableCell403.Text = "Đơn vị nhận báo cáo: Bộ Nội Vụ";
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
            this.xrCellLastReceivedDate.Text = "Thời hạn nhận báo cáo: ngày 30 tháng 6";
            this.xrCellLastReceivedDate.Weight = 1.5748560433256003D;
            // 
            // xrCellToDate
            // 
            this.xrCellToDate.Font = new System.Drawing.Font("Times New Roman", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.xrCellToDate.Name = "xrCellToDate";
            this.xrCellToDate.StylePriority.UseFont = false;
            this.xrCellToDate.Text = "Tính đến ngày {0} tháng {1} năm {2}";
            this.xrCellToDate.Weight = 3.2928199438412697D;
            // 
            // xrTableCell503
            // 
            this.xrTableCell503.Font = new System.Drawing.Font("Times New Roman", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.xrTableCell503.Name = "xrTableCell503";
            this.xrTableCell503.StylePriority.UseFont = false;
            this.xrTableCell503.StylePriority.UseTextAlignment = false;
            this.xrTableCell503.Text = "Đơn vị tính: người";
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
            this.tblPageHeader.SizeF = new System.Drawing.SizeF(1138F, 230F);
            this.tblPageHeader.StylePriority.UseBorders = false;
            // 
            // xrTableRow11
            // 
            this.xrTableRow11.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell241,
            this.xrTableCell242,
            this.xrTableCell1,
            this.xrTableCell6,
            this.xrTableCell2,
            this.xrTableCell3,
            this.xrTableCell4,
            this.xrTableCell5});
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
            this.xrTableCell241.Text = "Số thứ tự";
            this.xrTableCell241.Weight = 0.23D;
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
            this.xrTableCell242.Weight = 2.4D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Angle = 90F;
            this.xrTableCell1.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell1.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseBorderColor = false;
            this.xrTableCell1.StylePriority.UseBorders = false;
            this.xrTableCell1.StylePriority.UseFont = false;
            this.xrTableCell1.Text = "Tổng số biên chế được giao";
            this.xrTableCell1.Weight = 0.23D;
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
            this.xrTableCell6.Text = "Tổng số công chức hiện có";
            this.xrTableCell6.Weight = 0.22999999999999932D;
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
            this.xrTableCell2.Weight = 0.91999999999999982D;
            // 
            // lblTrongDo
            // 
            this.lblTrongDo.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.lblTrongDo.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.lblTrongDo.Name = "lblTrongDo";
            this.lblTrongDo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.lblTrongDo.SizeF = new System.Drawing.SizeF(92F, 25F);
            this.lblTrongDo.StylePriority.UseBorders = false;
            this.lblTrongDo.Text = "Trong đó";
            // 
            // xrTable3
            // 
            this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 25F);
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow5});
            this.xrTable3.SizeF = new System.Drawing.SizeF(92F, 205F);
            // 
            // xrTableRow5
            // 
            this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell7,
            this.xrTableCell8,
            this.xrTableCell9,
            this.xrTableCell10});
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
            this.xrTableCell7.Text = "Nữ";
            this.xrTableCell7.Weight = 0.34673355024323194D;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.Angle = 90F;
            this.xrTableCell8.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.StylePriority.UseBorders = false;
            this.xrTableCell8.Text = "Đảng viên";
            this.xrTableCell8.Weight = 0.34673355024323205D;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.Angle = 90F;
            this.xrTableCell9.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.StylePriority.UseBorders = false;
            this.xrTableCell9.Text = "Dân tộc thiểu số";
            this.xrTableCell9.Weight = 0.34673357771926716D;
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.Angle = 90F;
            this.xrTableCell10.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.StylePriority.UseBorders = false;
            this.xrTableCell10.Text = "Tôn giáo";
            this.xrTableCell10.Weight = 0.34673363522725131D;
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
            this.xrTableCell3.Weight = 1.1499999999999997D;
            // 
            // lblNgachCongChuc
            // 
            this.lblNgachCongChuc.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.lblNgachCongChuc.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.lblNgachCongChuc.Name = "lblNgachCongChuc";
            this.lblNgachCongChuc.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.lblNgachCongChuc.SizeF = new System.Drawing.SizeF(115F, 25F);
            this.lblNgachCongChuc.StylePriority.UseBorders = false;
            this.lblNgachCongChuc.StylePriority.UsePadding = false;
            this.lblNgachCongChuc.Text = "Chia theo ngạch công chức";
            // 
            // xrTable4
            // 
            this.xrTable4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 25F);
            this.xrTable4.Name = "xrTable4";
            this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow6});
            this.xrTable4.SizeF = new System.Drawing.SizeF(115F, 205F);
            // 
            // xrTableRow6
            // 
            this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell11,
            this.xrTableCell12,
            this.xrTableCell50,
            this.xrTableCell51,
            this.xrTableCell52});
            this.xrTableRow6.Name = "xrTableRow6";
            this.xrTableRow6.Weight = 1D;
            // 
            // xrTableCell11
            // 
            this.xrTableCell11.Angle = 90F;
            this.xrTableCell11.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell11.Name = "xrTableCell11";
            this.xrTableCell11.StylePriority.UseBorders = false;
            this.xrTableCell11.Text = "Chuyên viên cao cấp & TĐ";
            this.xrTableCell11.Weight = 0.22999999999999998D;
            // 
            // xrTableCell12
            // 
            this.xrTableCell12.Angle = 90F;
            this.xrTableCell12.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.StylePriority.UseBorders = false;
            this.xrTableCell12.Text = "Chuyên viên chính và TĐ";
            this.xrTableCell12.Weight = 0.22999999999999998D;
            // 
            // xrTableCell50
            // 
            this.xrTableCell50.Angle = 90F;
            this.xrTableCell50.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell50.Name = "xrTableCell50";
            this.xrTableCell50.StylePriority.UseBorders = false;
            this.xrTableCell50.Text = "Chuyên viên & TĐ";
            this.xrTableCell50.Weight = 0.22999999999999998D;
            // 
            // xrTableCell51
            // 
            this.xrTableCell51.Angle = 90F;
            this.xrTableCell51.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell51.Name = "xrTableCell51";
            this.xrTableCell51.StylePriority.UseBorders = false;
            this.xrTableCell51.Text = "Cán sự & TĐ";
            this.xrTableCell51.Weight = 0.22999999999999998D;
            // 
            // xrTableCell52
            // 
            this.xrTableCell52.Angle = 90F;
            this.xrTableCell52.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell52.Name = "xrTableCell52";
            this.xrTableCell52.StylePriority.UseBorders = false;
            this.xrTableCell52.Text = "Nhân viên";
            this.xrTableCell52.Weight = 0.22999999999999998D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableCell4.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell4.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblTrinhDo,
            this.xrTable5});
            this.xrTableCell4.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.StylePriority.UseBorderColor = false;
            this.xrTableCell4.StylePriority.UseBorders = false;
            this.xrTableCell4.StylePriority.UseFont = false;
            this.xrTableCell4.Weight = 4.6099999999999994D;
            // 
            // lblTrinhDo
            // 
            this.lblTrinhDo.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.lblTrinhDo.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.lblTrinhDo.Name = "lblTrinhDo";
            this.lblTrinhDo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.lblTrinhDo.SizeF = new System.Drawing.SizeF(461F, 25F);
            this.lblTrinhDo.StylePriority.UseBorders = false;
            this.lblTrinhDo.Text = "Chia theo trình độ đào tạo";
            // 
            // xrTable5
            // 
            this.xrTable5.LocationFloat = new DevExpress.Utils.PointFloat(0F, 24.99999F);
            this.xrTable5.Name = "xrTable5";
            this.xrTable5.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow7});
            this.xrTable5.SizeF = new System.Drawing.SizeF(461.0001F, 205F);
            // 
            // xrTableRow7
            // 
            this.xrTableRow7.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell13,
            this.xrTableCell15,
            this.xrTableCell16,
            this.xrTableCell17,
            this.xrTableCell18,
            this.xrTableCell14});
            this.xrTableRow7.Name = "xrTableRow7";
            this.xrTableRow7.Weight = 9.11111111111111D;
            // 
            // xrTableCell13
            // 
            this.xrTableCell13.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable6,
            this.lblChuyenMon});
            this.xrTableCell13.Name = "xrTableCell13";
            this.xrTableCell13.Text = "xrTableCell13";
            this.xrTableCell13.Weight = 1.38D;
            // 
            // xrTable6
            // 
            this.xrTable6.LocationFloat = new DevExpress.Utils.PointFloat(0F, 25F);
            this.xrTable6.Name = "xrTable6";
            this.xrTable6.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow8});
            this.xrTable6.SizeF = new System.Drawing.SizeF(138F, 180F);
            // 
            // xrTableRow8
            // 
            this.xrTableRow8.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell20,
            this.xrTableCell21,
            this.xrTableCell22,
            this.xrTableCell23,
            this.xrTableCell24,
            this.xrTableCell25});
            this.xrTableRow8.Name = "xrTableRow8";
            this.xrTableRow8.Weight = 1D;
            // 
            // xrTableCell20
            // 
            this.xrTableCell20.Angle = 90F;
            this.xrTableCell20.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell20.Name = "xrTableCell20";
            this.xrTableCell20.StylePriority.UseBorders = false;
            this.xrTableCell20.Text = "Tiến sĩ";
            this.xrTableCell20.Weight = 0.22999999999999998D;
            // 
            // xrTableCell21
            // 
            this.xrTableCell21.Angle = 90F;
            this.xrTableCell21.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell21.Name = "xrTableCell21";
            this.xrTableCell21.StylePriority.UseBorders = false;
            this.xrTableCell21.Text = "Thạc sĩ";
            this.xrTableCell21.Weight = 0.22999999999999998D;
            // 
            // xrTableCell22
            // 
            this.xrTableCell22.Angle = 90F;
            this.xrTableCell22.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell22.Name = "xrTableCell22";
            this.xrTableCell22.StylePriority.UseBorders = false;
            this.xrTableCell22.Text = "Đại học";
            this.xrTableCell22.Weight = 0.22999999999999998D;
            // 
            // xrTableCell23
            // 
            this.xrTableCell23.Angle = 90F;
            this.xrTableCell23.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell23.Name = "xrTableCell23";
            this.xrTableCell23.StylePriority.UseBorders = false;
            this.xrTableCell23.Text = "Cao đẳng";
            this.xrTableCell23.Weight = 0.22999999999999998D;
            // 
            // xrTableCell24
            // 
            this.xrTableCell24.Angle = 90F;
            this.xrTableCell24.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell24.Name = "xrTableCell24";
            this.xrTableCell24.StylePriority.UseBorders = false;
            this.xrTableCell24.Text = "Trung cấp";
            this.xrTableCell24.Weight = 0.22999999999999998D;
            // 
            // xrTableCell25
            // 
            this.xrTableCell25.Angle = 90F;
            this.xrTableCell25.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell25.Name = "xrTableCell25";
            this.xrTableCell25.StylePriority.UseBorders = false;
            this.xrTableCell25.Text = "Sơ cấp";
            this.xrTableCell25.Weight = 0.22999999999999998D;
            // 
            // lblChuyenMon
            // 
            this.lblChuyenMon.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.lblChuyenMon.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.lblChuyenMon.Name = "lblChuyenMon";
            this.lblChuyenMon.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblChuyenMon.SizeF = new System.Drawing.SizeF(138F, 25F);
            this.lblChuyenMon.StylePriority.UseBorders = false;
            this.lblChuyenMon.Text = "Chuyên môn";
            // 
            // xrTableCell15
            // 
            this.xrTableCell15.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblChinhTri,
            this.xrTable8});
            this.xrTableCell15.Name = "xrTableCell15";
            this.xrTableCell15.Text = "xrTableCell15";
            this.xrTableCell15.Weight = 0.91999999999999993D;
            // 
            // lblChinhTri
            // 
            this.lblChinhTri.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.lblChinhTri.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.lblChinhTri.Name = "lblChinhTri";
            this.lblChinhTri.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblChinhTri.SizeF = new System.Drawing.SizeF(92F, 25F);
            this.lblChinhTri.StylePriority.UseBorders = false;
            this.lblChinhTri.Text = "Chính trị";
            // 
            // xrTable8
            // 
            this.xrTable8.LocationFloat = new DevExpress.Utils.PointFloat(5.501967E-05F, 25F);
            this.xrTable8.Name = "xrTable8";
            this.xrTable8.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow13});
            this.xrTable8.SizeF = new System.Drawing.SizeF(92F, 180F);
            // 
            // xrTableRow13
            // 
            this.xrTableRow13.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell29,
            this.xrTableCell30,
            this.xrTableCell31,
            this.xrTableCell32});
            this.xrTableRow13.Name = "xrTableRow13";
            this.xrTableRow13.Weight = 1D;
            // 
            // xrTableCell29
            // 
            this.xrTableCell29.Angle = 90F;
            this.xrTableCell29.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell29.Name = "xrTableCell29";
            this.xrTableCell29.StylePriority.UseBorders = false;
            this.xrTableCell29.Text = "Cử nhân";
            this.xrTableCell29.Weight = 0.22999999999999998D;
            // 
            // xrTableCell30
            // 
            this.xrTableCell30.Angle = 90F;
            this.xrTableCell30.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell30.Name = "xrTableCell30";
            this.xrTableCell30.StylePriority.UseBorders = false;
            this.xrTableCell30.Text = "Cao cấp";
            this.xrTableCell30.Weight = 0.22999999999999998D;
            // 
            // xrTableCell31
            // 
            this.xrTableCell31.Angle = 90F;
            this.xrTableCell31.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell31.Name = "xrTableCell31";
            this.xrTableCell31.StylePriority.UseBorders = false;
            this.xrTableCell31.Text = "Trung cấp";
            this.xrTableCell31.Weight = 0.22999999999999998D;
            // 
            // xrTableCell32
            // 
            this.xrTableCell32.Angle = 90F;
            this.xrTableCell32.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell32.Name = "xrTableCell32";
            this.xrTableCell32.StylePriority.UseBorders = false;
            this.xrTableCell32.Text = "Sơ cấp";
            this.xrTableCell32.Weight = 0.22999999999999998D;
            // 
            // xrTableCell16
            // 
            this.xrTableCell16.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblTinHoc,
            this.xrTable9});
            this.xrTableCell16.Name = "xrTableCell16";
            this.xrTableCell16.Text = "xrTableCell16";
            this.xrTableCell16.Weight = 0.46D;
            // 
            // lblTinHoc
            // 
            this.lblTinHoc.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.lblTinHoc.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.lblTinHoc.Name = "lblTinHoc";
            this.lblTinHoc.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblTinHoc.SizeF = new System.Drawing.SizeF(46F, 25F);
            this.lblTinHoc.StylePriority.UseBorders = false;
            this.lblTinHoc.Text = "Tin học";
            // 
            // xrTable9
            // 
            this.xrTable9.LocationFloat = new DevExpress.Utils.PointFloat(0F, 25F);
            this.xrTable9.Name = "xrTable9";
            this.xrTable9.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow14});
            this.xrTable9.SizeF = new System.Drawing.SizeF(46F, 180F);
            // 
            // xrTableRow14
            // 
            this.xrTableRow14.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell33,
            this.xrTableCell34});
            this.xrTableRow14.Name = "xrTableRow14";
            this.xrTableRow14.Weight = 1D;
            // 
            // xrTableCell33
            // 
            this.xrTableCell33.Angle = 90F;
            this.xrTableCell33.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell33.Name = "xrTableCell33";
            this.xrTableCell33.StylePriority.UseBorders = false;
            this.xrTableCell33.Text = "Trung cấp trở lên";
            this.xrTableCell33.Weight = 0.22999999999999998D;
            // 
            // xrTableCell34
            // 
            this.xrTableCell34.Angle = 90F;
            this.xrTableCell34.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell34.Name = "xrTableCell34";
            this.xrTableCell34.StylePriority.UseBorders = false;
            this.xrTableCell34.Text = "Chứng chỉ";
            this.xrTableCell34.Weight = 0.22999999999999998D;
            // 
            // xrTableCell17
            // 
            this.xrTableCell17.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblNgoaiNgu,
            this.lblTiengAnh,
            this.lblNgoaiNguKhac,
            this.xrTable10});
            this.xrTableCell17.Name = "xrTableCell17";
            this.xrTableCell17.Text = "xrTableCell17";
            this.xrTableCell17.Weight = 0.92D;
            // 
            // lblNgoaiNgu
            // 
            this.lblNgoaiNgu.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.lblNgoaiNgu.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.lblNgoaiNgu.Name = "lblNgoaiNgu";
            this.lblNgoaiNgu.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblNgoaiNgu.SizeF = new System.Drawing.SizeF(92F, 25F);
            this.lblNgoaiNgu.StylePriority.UseBorders = false;
            this.lblNgoaiNgu.Text = "Ngoại ngữ";
            // 
            // lblTiengAnh
            // 
            this.lblTiengAnh.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.lblTiengAnh.LocationFloat = new DevExpress.Utils.PointFloat(0F, 25F);
            this.lblTiengAnh.Name = "lblTiengAnh";
            this.lblTiengAnh.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblTiengAnh.SizeF = new System.Drawing.SizeF(46F, 40F);
            this.lblTiengAnh.StylePriority.UseBorders = false;
            this.lblTiengAnh.Text = "Tiếng Anh";
            // 
            // lblNgoaiNguKhac
            // 
            this.lblNgoaiNguKhac.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.lblNgoaiNguKhac.LocationFloat = new DevExpress.Utils.PointFloat(46F, 25F);
            this.lblNgoaiNguKhac.Name = "lblNgoaiNguKhac";
            this.lblNgoaiNguKhac.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblNgoaiNguKhac.SizeF = new System.Drawing.SizeF(46F, 40F);
            this.lblNgoaiNguKhac.StylePriority.UseBorders = false;
            this.lblNgoaiNguKhac.Text = "Ngoại ngữ khác";
            // 
            // xrTable10
            // 
            this.xrTable10.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTable10.LocationFloat = new DevExpress.Utils.PointFloat(0F, 64.99999F);
            this.xrTable10.Name = "xrTable10";
            this.xrTable10.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow15});
            this.xrTable10.SizeF = new System.Drawing.SizeF(92F, 140F);
            this.xrTable10.StylePriority.UseBorders = false;
            // 
            // xrTableRow15
            // 
            this.xrTableRow15.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell35,
            this.xrTableCell36,
            this.xrTableCell37,
            this.xrTableCell38});
            this.xrTableRow15.Name = "xrTableRow15";
            this.xrTableRow15.Weight = 5.6D;
            // 
            // xrTableCell35
            // 
            this.xrTableCell35.Angle = 90F;
            this.xrTableCell35.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell35.Name = "xrTableCell35";
            this.xrTableCell35.StylePriority.UseBorders = false;
            this.xrTableCell35.Text = "Đại học trở lên";
            this.xrTableCell35.Weight = 0.22999999999999998D;
            // 
            // xrTableCell36
            // 
            this.xrTableCell36.Angle = 90F;
            this.xrTableCell36.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell36.Name = "xrTableCell36";
            this.xrTableCell36.StylePriority.UseBorders = false;
            this.xrTableCell36.Text = "Chứng chỉ (A,B,C)";
            this.xrTableCell36.Weight = 0.22999999999999998D;
            // 
            // xrTableCell37
            // 
            this.xrTableCell37.Angle = 90F;
            this.xrTableCell37.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell37.Name = "xrTableCell37";
            this.xrTableCell37.StylePriority.UseBorders = false;
            this.xrTableCell37.Text = "Đại học trở lên";
            this.xrTableCell37.Weight = 0.22999999999999998D;
            // 
            // xrTableCell38
            // 
            this.xrTableCell38.Angle = 90F;
            this.xrTableCell38.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell38.Name = "xrTableCell38";
            this.xrTableCell38.StylePriority.UseBorders = false;
            this.xrTableCell38.Text = "Chứng chỉ (A,B,C)";
            this.xrTableCell38.Weight = 0.22999999999999998D;
            // 
            // xrTableCell18
            // 
            this.xrTableCell18.Angle = 90F;
            this.xrTableCell18.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell18.Name = "xrTableCell18";
            this.xrTableCell18.StylePriority.UseBorders = false;
            this.xrTableCell18.Text = "Chứng chỉ tiếng dân tộc";
            this.xrTableCell18.Weight = 0.23000000000000009D;
            // 
            // xrTableCell14
            // 
            this.xrTableCell14.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblQuanLyNhaNuoc,
            this.xrTable11});
            this.xrTableCell14.Name = "xrTableCell14";
            this.xrTableCell14.Text = "xrTableCell14";
            this.xrTableCell14.Weight = 0.69D;
            // 
            // lblQuanLyNhaNuoc
            // 
            this.lblQuanLyNhaNuoc.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.lblQuanLyNhaNuoc.LocationFloat = new DevExpress.Utils.PointFloat(0.0001220703F, 0F);
            this.lblQuanLyNhaNuoc.Name = "lblQuanLyNhaNuoc";
            this.lblQuanLyNhaNuoc.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblQuanLyNhaNuoc.SizeF = new System.Drawing.SizeF(68.99988F, 25F);
            this.lblQuanLyNhaNuoc.StylePriority.UseBorders = false;
            this.lblQuanLyNhaNuoc.Text = "Quản lý nhà nước";
            // 
            // xrTable11
            // 
            this.xrTable11.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTable11.LocationFloat = new DevExpress.Utils.PointFloat(0.0001220703F, 25.00002F);
            this.xrTable11.Name = "xrTable11";
            this.xrTable11.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow16});
            this.xrTable11.SizeF = new System.Drawing.SizeF(68.99988F, 180F);
            this.xrTable11.StylePriority.UseBorders = false;
            // 
            // xrTableRow16
            // 
            this.xrTableRow16.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableRow16.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell39,
            this.xrTableCell40,
            this.xrTableCell41});
            this.xrTableRow16.Name = "xrTableRow16";
            this.xrTableRow16.StylePriority.UseBorders = false;
            this.xrTableRow16.Weight = 7.2D;
            // 
            // xrTableCell39
            // 
            this.xrTableCell39.Angle = 90F;
            this.xrTableCell39.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell39.Name = "xrTableCell39";
            this.xrTableCell39.StylePriority.UseBorders = false;
            this.xrTableCell39.Text = "Chuyên viên cao cấp & TĐ";
            this.xrTableCell39.Weight = 0.22999999999999998D;
            // 
            // xrTableCell40
            // 
            this.xrTableCell40.Angle = 90F;
            this.xrTableCell40.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell40.Name = "xrTableCell40";
            this.xrTableCell40.StylePriority.UseBorders = false;
            this.xrTableCell40.Text = "Chuyên viên chính & TĐ";
            this.xrTableCell40.Weight = 0.22999999999999998D;
            // 
            // xrTableCell41
            // 
            this.xrTableCell41.Angle = 90F;
            this.xrTableCell41.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell41.Name = "xrTableCell41";
            this.xrTableCell41.StylePriority.UseBorders = false;
            this.xrTableCell41.Text = "Chuyên viên & TĐ";
            this.xrTableCell41.Weight = 0.24D;
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
            this.xrTableCell5.Weight = 1.6099999999999992D;
            // 
            // lblTuoi
            // 
            this.lblTuoi.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right)));
            this.lblTuoi.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.lblTuoi.Name = "lblTuoi";
            this.lblTuoi.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblTuoi.SizeF = new System.Drawing.SizeF(161F, 25F);
            this.lblTuoi.StylePriority.UseBorders = false;
            this.lblTuoi.Text = "Chia theo độ tuổi";
            // 
            // xrTable12
            // 
            this.xrTable12.LocationFloat = new DevExpress.Utils.PointFloat(0F, 25F);
            this.xrTable12.Name = "xrTable12";
            this.xrTable12.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow17});
            this.xrTable12.SizeF = new System.Drawing.SizeF(161F, 205F);
            // 
            // xrTableRow17
            // 
            this.xrTableRow17.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableRow17.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell42,
            this.xrTableCell43,
            this.xrTableCell44,
            this.xrTableCell45,
            this.xrTableCell46});
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
            this.xrTableCell42.Text = "Từ 30 trở xuống\t";
            this.xrTableCell42.Weight = 0.22999999999999998D;
            // 
            // xrTableCell43
            // 
            this.xrTableCell43.Angle = 90F;
            this.xrTableCell43.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell43.Name = "xrTableCell43";
            this.xrTableCell43.StylePriority.UseBorders = false;
            this.xrTableCell43.Text = "Từ 31 đến 40";
            this.xrTableCell43.Weight = 0.22999999999999998D;
            // 
            // xrTableCell44
            // 
            this.xrTableCell44.Angle = 90F;
            this.xrTableCell44.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell44.Name = "xrTableCell44";
            this.xrTableCell44.StylePriority.UseBorders = false;
            this.xrTableCell44.Text = "Từ 41 đến 50";
            this.xrTableCell44.Weight = 0.22999999999999998D;
            // 
            // xrTableCell45
            // 
            this.xrTableCell45.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lbl51Den60,
            this.xrTable13});
            this.xrTableCell45.Name = "xrTableCell45";
            this.xrTableCell45.Weight = 0.69D;
            // 
            // lbl51Den60
            // 
            this.lbl51Den60.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.lbl51Den60.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.lbl51Den60.Name = "lbl51Den60";
            this.lbl51Den60.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lbl51Den60.SizeF = new System.Drawing.SizeF(69F, 25F);
            this.lbl51Den60.StylePriority.UseBorders = false;
            this.lbl51Den60.Text = "Từ 51 đến 60";
            // 
            // xrTable13
            // 
            this.xrTable13.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTable13.LocationFloat = new DevExpress.Utils.PointFloat(0F, 25F);
            this.xrTable13.Name = "xrTable13";
            this.xrTable13.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow18});
            this.xrTable13.SizeF = new System.Drawing.SizeF(69F, 180F);
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
            this.xrTableCell47.Text = "Tổng số";
            this.xrTableCell47.Weight = 0.22999999999999998D;
            // 
            // xrTableCell48
            // 
            this.xrTableCell48.Angle = 90F;
            this.xrTableCell48.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell48.Name = "xrTableCell48";
            this.xrTableCell48.StylePriority.UseBorders = false;
            this.xrTableCell48.Text = "Nữ từ 51 đến 55";
            this.xrTableCell48.Weight = 0.22999999999999998D;
            // 
            // xrTableCell49
            // 
            this.xrTableCell49.Angle = 90F;
            this.xrTableCell49.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell49.Name = "xrTableCell49";
            this.xrTableCell49.StylePriority.UseBorders = false;
            this.xrTableCell49.Text = "Nam từ 56 đến 60";
            this.xrTableCell49.Weight = 0.22999999999999998D;
            // 
            // xrTableCell46
            // 
            this.xrTableCell46.Angle = 90F;
            this.xrTableCell46.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell46.Name = "xrTableCell46";
            this.xrTableCell46.StylePriority.UseBorders = false;
            this.xrTableCell46.Text = "Trên tuổi nghỉ hưu";
            this.xrTableCell46.Weight = 0.23000000000000004D;
            // 
            // formattingRule1
            // 
            this.formattingRule1.Name = "formattingRule1";
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblReportDate,
            this.lblKyDongDau,
            this.xrTable1,
            this.lblKyHoTen,
            this.lblLapBang,
            this.lblThuTruong});
            this.ReportFooter.HeightF = 252.1667F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // lblReportDate
            // 
            this.lblReportDate.Font = new System.Drawing.Font("Times New Roman", 7F, System.Drawing.FontStyle.Italic);
            this.lblReportDate.LocationFloat = new DevExpress.Utils.PointFloat(687.391F, 46.54177F);
            this.lblReportDate.Name = "lblReportDate";
            this.lblReportDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblReportDate.SizeF = new System.Drawing.SizeF(300F, 15F);
            this.lblReportDate.StylePriority.UseFont = false;
            this.lblReportDate.StylePriority.UseTextAlignment = false;
            this.lblReportDate.Text = "{0}, ngày {1} tháng {2} năm {3}";
            this.lblReportDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblKyDongDau
            // 
            this.lblKyDongDau.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Italic);
            this.lblKyDongDau.LocationFloat = new DevExpress.Utils.PointFloat(687.391F, 86.54176F);
            this.lblKyDongDau.Name = "lblKyDongDau";
            this.lblKyDongDau.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblKyDongDau.SizeF = new System.Drawing.SizeF(300F, 10F);
            this.lblKyDongDau.StylePriority.UseFont = false;
            this.lblKyDongDau.StylePriority.UseTextAlignment = false;
            this.lblKyDongDau.Text = "(Ký, đóng dấu)";
            this.lblKyDongDau.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTable1
            // 
            this.xrTable1.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(1138F, 25F);
            this.xrTable1.StylePriority.UseBorders = false;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTextTongSo,
            this.xrTongSoBienChe,
            this.xrTongSoCongChuc,
            this.xrTongNu,
            this.xrTongDangVien,
            this.xrTongDanTocThieuSo,
            this.xrTongTonGiao,
            this.xrTongChuyenVienCaoCapVaTD,
            this.xrTongChuyenVienChinhVaTD,
            this.xrTongChuyenVienVaTD,
            this.xrTongCanSuVaTD,
            this.xrTongNhanVien,
            this.xrTongTienSi,
            this.xrTongThacSi,
            this.xrTongDaiHoc,
            this.xrTongCaoDang,
            this.xrTongTrungCap,
            this.xrTongSoCap,
            this.xrTongCuNhanChinhTri,
            this.xrTongCaoCapChinhTri,
            this.xrTongTrungCapChinhTri,
            this.xrTongSoCapChinhTri,
            this.xrTongTrungCapTinHoc,
            this.xrTongChungChiTinHoc,
            this.xrTongDaiHocTiengAnh,
            this.xrTongChungChiTiengAnh,
            this.xrTongDaiHocNgoaiNguKhac,
            this.xrTongChungChiNgoaiNguKhac,
            this.xrTongChungChiTiengDanToc,
            this.xrTongChuyenVienCaoCap,
            this.xrTongChuyenVienChinh,
            this.xrTongChuyenVien,
            this.xrTongDuoi30,
            this.xrTong31Den40,
            this.xrTong41Den50,
            this.xrTong51Den60,
            this.xrTongNu51Den55,
            this.xrTongNam56Den60,
            this.xrTongTrenTuoiNghiHuu});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTextTongSo
            // 
            this.xrTextTongSo.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTextTongSo.Name = "xrTextTongSo";
            this.xrTextTongSo.StylePriority.UseBorders = false;
            this.xrTextTongSo.Text = "Tổng số";
            this.xrTextTongSo.Weight = 2.6300000000000003D;
            // 
            // xrTongSoBienChe
            // 
            this.xrTongSoBienChe.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongSoBienChe.Name = "xrTongSoBienChe";
            this.xrTongSoBienChe.StylePriority.UseBorders = false;
            this.xrTongSoBienChe.Weight = 0.22999999999999998D;
            // 
            // xrTongSoCongChuc
            // 
            this.xrTongSoCongChuc.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongSoCongChuc.Name = "xrTongSoCongChuc";
            this.xrTongSoCongChuc.StylePriority.UseBorders = false;
            this.xrTongSoCongChuc.Weight = 0.22999999999999998D;
            // 
            // xrTongNu
            // 
            this.xrTongNu.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongNu.Name = "xrTongNu";
            this.xrTongNu.StylePriority.UseBorders = false;
            this.xrTongNu.Weight = 0.22999999999999998D;
            // 
            // xrTongDangVien
            // 
            this.xrTongDangVien.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongDangVien.Name = "xrTongDangVien";
            this.xrTongDangVien.StylePriority.UseBorders = false;
            this.xrTongDangVien.Weight = 0.22999999999999998D;
            // 
            // xrTongDanTocThieuSo
            // 
            this.xrTongDanTocThieuSo.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongDanTocThieuSo.Name = "xrTongDanTocThieuSo";
            this.xrTongDanTocThieuSo.StylePriority.UseBorders = false;
            this.xrTongDanTocThieuSo.Weight = 0.22999999999999998D;
            // 
            // xrTongTonGiao
            // 
            this.xrTongTonGiao.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongTonGiao.Name = "xrTongTonGiao";
            this.xrTongTonGiao.StylePriority.UseBorders = false;
            this.xrTongTonGiao.Weight = 0.22999999999999998D;
            // 
            // xrTongChuyenVienCaoCapVaTD
            // 
            this.xrTongChuyenVienCaoCapVaTD.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongChuyenVienCaoCapVaTD.Name = "xrTongChuyenVienCaoCapVaTD";
            this.xrTongChuyenVienCaoCapVaTD.StylePriority.UseBorders = false;
            this.xrTongChuyenVienCaoCapVaTD.Weight = 0.22999999999999998D;
            // 
            // xrTongChuyenVienChinhVaTD
            // 
            this.xrTongChuyenVienChinhVaTD.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongChuyenVienChinhVaTD.Name = "xrTongChuyenVienChinhVaTD";
            this.xrTongChuyenVienChinhVaTD.StylePriority.UseBorders = false;
            this.xrTongChuyenVienChinhVaTD.Weight = 0.22999999999999998D;
            // 
            // xrTongChuyenVienVaTD
            // 
            this.xrTongChuyenVienVaTD.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongChuyenVienVaTD.Name = "xrTongChuyenVienVaTD";
            this.xrTongChuyenVienVaTD.StylePriority.UseBorders = false;
            this.xrTongChuyenVienVaTD.Weight = 0.22999999999999998D;
            // 
            // xrTongCanSuVaTD
            // 
            this.xrTongCanSuVaTD.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongCanSuVaTD.Name = "xrTongCanSuVaTD";
            this.xrTongCanSuVaTD.StylePriority.UseBorders = false;
            this.xrTongCanSuVaTD.Weight = 0.22999999999999998D;
            // 
            // xrTongNhanVien
            // 
            this.xrTongNhanVien.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongNhanVien.Name = "xrTongNhanVien";
            this.xrTongNhanVien.StylePriority.UseBorders = false;
            this.xrTongNhanVien.Weight = 0.22999999999999998D;
            // 
            // xrTongTienSi
            // 
            this.xrTongTienSi.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongTienSi.Name = "xrTongTienSi";
            this.xrTongTienSi.StylePriority.UseBorders = false;
            this.xrTongTienSi.Weight = 0.22999999999999998D;
            // 
            // xrTongThacSi
            // 
            this.xrTongThacSi.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongThacSi.Name = "xrTongThacSi";
            this.xrTongThacSi.StylePriority.UseBorders = false;
            this.xrTongThacSi.Weight = 0.22999999999999998D;
            // 
            // xrTongDaiHoc
            // 
            this.xrTongDaiHoc.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongDaiHoc.Name = "xrTongDaiHoc";
            this.xrTongDaiHoc.StylePriority.UseBorders = false;
            this.xrTongDaiHoc.Weight = 0.22999999999999998D;
            // 
            // xrTongCaoDang
            // 
            this.xrTongCaoDang.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongCaoDang.Name = "xrTongCaoDang";
            this.xrTongCaoDang.StylePriority.UseBorders = false;
            this.xrTongCaoDang.Weight = 0.22999999999999998D;
            // 
            // xrTongTrungCap
            // 
            this.xrTongTrungCap.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongTrungCap.Name = "xrTongTrungCap";
            this.xrTongTrungCap.StylePriority.UseBorders = false;
            this.xrTongTrungCap.Weight = 0.22999999999999998D;
            // 
            // xrTongSoCap
            // 
            this.xrTongSoCap.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongSoCap.Name = "xrTongSoCap";
            this.xrTongSoCap.StylePriority.UseBorders = false;
            this.xrTongSoCap.Weight = 0.22999999999999998D;
            // 
            // xrTongCuNhanChinhTri
            // 
            this.xrTongCuNhanChinhTri.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongCuNhanChinhTri.Name = "xrTongCuNhanChinhTri";
            this.xrTongCuNhanChinhTri.StylePriority.UseBorders = false;
            this.xrTongCuNhanChinhTri.Weight = 0.22999999999999998D;
            // 
            // xrTongCaoCapChinhTri
            // 
            this.xrTongCaoCapChinhTri.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongCaoCapChinhTri.Name = "xrTongCaoCapChinhTri";
            this.xrTongCaoCapChinhTri.StylePriority.UseBorders = false;
            this.xrTongCaoCapChinhTri.Weight = 0.22999999999999998D;
            // 
            // xrTongTrungCapChinhTri
            // 
            this.xrTongTrungCapChinhTri.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongTrungCapChinhTri.Name = "xrTongTrungCapChinhTri";
            this.xrTongTrungCapChinhTri.StylePriority.UseBorders = false;
            this.xrTongTrungCapChinhTri.Weight = 0.22999999999999998D;
            // 
            // xrTongSoCapChinhTri
            // 
            this.xrTongSoCapChinhTri.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongSoCapChinhTri.Name = "xrTongSoCapChinhTri";
            this.xrTongSoCapChinhTri.StylePriority.UseBorders = false;
            this.xrTongSoCapChinhTri.Weight = 0.22999999999999998D;
            // 
            // xrTongTrungCapTinHoc
            // 
            this.xrTongTrungCapTinHoc.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongTrungCapTinHoc.Name = "xrTongTrungCapTinHoc";
            this.xrTongTrungCapTinHoc.StylePriority.UseBorders = false;
            this.xrTongTrungCapTinHoc.Weight = 0.22999999999999998D;
            // 
            // xrTongChungChiTinHoc
            // 
            this.xrTongChungChiTinHoc.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongChungChiTinHoc.Name = "xrTongChungChiTinHoc";
            this.xrTongChungChiTinHoc.StylePriority.UseBorders = false;
            this.xrTongChungChiTinHoc.Weight = 0.22999999999999998D;
            // 
            // xrTongDaiHocTiengAnh
            // 
            this.xrTongDaiHocTiengAnh.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongDaiHocTiengAnh.Name = "xrTongDaiHocTiengAnh";
            this.xrTongDaiHocTiengAnh.StylePriority.UseBorders = false;
            this.xrTongDaiHocTiengAnh.Weight = 0.22999999999999998D;
            // 
            // xrTongChungChiTiengAnh
            // 
            this.xrTongChungChiTiengAnh.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongChungChiTiengAnh.Name = "xrTongChungChiTiengAnh";
            this.xrTongChungChiTiengAnh.StylePriority.UseBorders = false;
            this.xrTongChungChiTiengAnh.Weight = 0.22999999999999998D;
            // 
            // xrTongDaiHocNgoaiNguKhac
            // 
            this.xrTongDaiHocNgoaiNguKhac.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongDaiHocNgoaiNguKhac.Name = "xrTongDaiHocNgoaiNguKhac";
            this.xrTongDaiHocNgoaiNguKhac.StylePriority.UseBorders = false;
            this.xrTongDaiHocNgoaiNguKhac.Weight = 0.22999999999999998D;
            // 
            // xrTongChungChiNgoaiNguKhac
            // 
            this.xrTongChungChiNgoaiNguKhac.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongChungChiNgoaiNguKhac.Name = "xrTongChungChiNgoaiNguKhac";
            this.xrTongChungChiNgoaiNguKhac.StylePriority.UseBorders = false;
            this.xrTongChungChiNgoaiNguKhac.Weight = 0.22999999999999998D;
            // 
            // xrTongChungChiTiengDanToc
            // 
            this.xrTongChungChiTiengDanToc.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongChungChiTiengDanToc.Name = "xrTongChungChiTiengDanToc";
            this.xrTongChungChiTiengDanToc.StylePriority.UseBorders = false;
            this.xrTongChungChiTiengDanToc.Weight = 0.22999999999999998D;
            // 
            // xrTongChuyenVienCaoCap
            // 
            this.xrTongChuyenVienCaoCap.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongChuyenVienCaoCap.Name = "xrTongChuyenVienCaoCap";
            this.xrTongChuyenVienCaoCap.StylePriority.UseBorders = false;
            this.xrTongChuyenVienCaoCap.Weight = 0.22999999999999998D;
            // 
            // xrTongChuyenVienChinh
            // 
            this.xrTongChuyenVienChinh.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongChuyenVienChinh.Name = "xrTongChuyenVienChinh";
            this.xrTongChuyenVienChinh.StylePriority.UseBorders = false;
            this.xrTongChuyenVienChinh.Weight = 0.22999999999999998D;
            // 
            // xrTongChuyenVien
            // 
            this.xrTongChuyenVien.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongChuyenVien.Name = "xrTongChuyenVien";
            this.xrTongChuyenVien.StylePriority.UseBorders = false;
            this.xrTongChuyenVien.Weight = 0.24D;
            // 
            // xrTongDuoi30
            // 
            this.xrTongDuoi30.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongDuoi30.Name = "xrTongDuoi30";
            this.xrTongDuoi30.StylePriority.UseBorders = false;
            this.xrTongDuoi30.Weight = 0.22999999999999998D;
            // 
            // xrTong31Den40
            // 
            this.xrTong31Den40.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTong31Den40.Name = "xrTong31Den40";
            this.xrTong31Den40.StylePriority.UseBorders = false;
            this.xrTong31Den40.Weight = 0.22999999999999998D;
            // 
            // xrTong41Den50
            // 
            this.xrTong41Den50.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTong41Den50.Name = "xrTong41Den50";
            this.xrTong41Den50.StylePriority.UseBorders = false;
            this.xrTong41Den50.Weight = 0.22999999999999998D;
            // 
            // xrTong51Den60
            // 
            this.xrTong51Den60.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTong51Den60.Name = "xrTong51Den60";
            this.xrTong51Den60.StylePriority.UseBorders = false;
            this.xrTong51Den60.Weight = 0.22999999999999998D;
            // 
            // xrTongNu51Den55
            // 
            this.xrTongNu51Den55.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongNu51Den55.Name = "xrTongNu51Den55";
            this.xrTongNu51Den55.StylePriority.UseBorders = false;
            this.xrTongNu51Den55.Weight = 0.22999999999999998D;
            // 
            // xrTongNam56Den60
            // 
            this.xrTongNam56Den60.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongNam56Den60.Name = "xrTongNam56Den60";
            this.xrTongNam56Den60.StylePriority.UseBorders = false;
            this.xrTongNam56Den60.Weight = 0.22999999999999998D;
            // 
            // xrTongTrenTuoiNghiHuu
            // 
            this.xrTongTrenTuoiNghiHuu.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTongTrenTuoiNghiHuu.Name = "xrTongTrenTuoiNghiHuu";
            this.xrTongTrenTuoiNghiHuu.StylePriority.UseBorders = false;
            this.xrTongTrenTuoiNghiHuu.Weight = 0.22999999999999998D;
            // 
            // lblKyHoTen
            // 
            this.lblKyHoTen.Font = new System.Drawing.Font("Times New Roman", 5F, System.Drawing.FontStyle.Italic);
            this.lblKyHoTen.LocationFloat = new DevExpress.Utils.PointFloat(108.5785F, 86.54176F);
            this.lblKyHoTen.Name = "lblKyHoTen";
            this.lblKyHoTen.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblKyHoTen.SizeF = new System.Drawing.SizeF(180F, 10F);
            this.lblKyHoTen.StylePriority.UseFont = false;
            this.lblKyHoTen.StylePriority.UseTextAlignment = false;
            this.lblKyHoTen.Text = "(Ký, họ tên)";
            this.lblKyHoTen.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblLapBang
            // 
            this.lblLapBang.Font = new System.Drawing.Font("Times New Roman", 6F, System.Drawing.FontStyle.Bold);
            this.lblLapBang.LocationFloat = new DevExpress.Utils.PointFloat(108.5785F, 61.54176F);
            this.lblLapBang.Name = "lblLapBang";
            this.lblLapBang.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblLapBang.SizeF = new System.Drawing.SizeF(180F, 25F);
            this.lblLapBang.StylePriority.UseFont = false;
            this.lblLapBang.StylePriority.UseTextAlignment = false;
            this.lblLapBang.Text = "NGƯỜI LẬP BẢNG";
            this.lblLapBang.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblThuTruong
            // 
            this.lblThuTruong.Font = new System.Drawing.Font("Times New Roman", 6F, System.Drawing.FontStyle.Bold);
            this.lblThuTruong.LocationFloat = new DevExpress.Utils.PointFloat(687.391F, 61.54176F);
            this.lblThuTruong.Name = "lblThuTruong";
            this.lblThuTruong.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblThuTruong.SizeF = new System.Drawing.SizeF(300F, 25F);
            this.lblThuTruong.StylePriority.UseFont = false;
            this.lblThuTruong.StylePriority.UseTextAlignment = false;
            this.lblThuTruong.Text = "THỦ TRƯỞNG ĐƠN VỊ";
            this.lblThuTruong.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.tblDetail});
            this.GroupHeader1.HeightF = 25F;
            this.GroupHeader1.Name = "GroupHeader1";
            this.GroupHeader1.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Group_BeforePrint);
            // 
            // tblDetail
            // 
            this.tblDetail.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.tblDetail.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.tblDetail.Name = "tblDetail";
            this.tblDetail.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrDetailRow1});
            this.tblDetail.SizeF = new System.Drawing.SizeF(1142.129F, 25F);
            this.tblDetail.StylePriority.UseBorders = false;
            // 
            // xrDetailRow1
            // 
            this.xrDetailRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCellSoThuTu,
            this.xrTableCellTenDonVi});
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
            this.xrTableCellSoThuTu.Weight = 0.22999999999999998D;
            // 
            // xrTableCellTenDonVi
            // 
            this.xrTableCellTenDonVi.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTenDonVi.Name = "xrTableCellTenDonVi";
            this.xrTableCellTenDonVi.StylePriority.UseBorders = false;
            this.xrTableCellTenDonVi.StylePriority.UseTextAlignment = false;
            this.xrTableCellTenDonVi.Text = " ";
            this.xrTableCellTenDonVi.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCellTenDonVi.Weight = 11.191290283203125D;
            // 
            // rp_QuantityDistrictCivilServantsDetail
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.PageHeader,
            this.ReportFooter,
            this.GroupHeader1});
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
            ((System.ComponentModel.ISupportInitialize)(this.xrTable7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblReportHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblPageHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion
    }
}