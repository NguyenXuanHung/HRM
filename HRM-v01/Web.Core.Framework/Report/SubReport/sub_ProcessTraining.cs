using DevExpress.XtraReports.UI;

namespace Web.Core.Framework.Report
{
    /// <summary>
    /// Quá trình đào tạo
    /// Summary description for sub_ProcessTraining
    /// </summary>
    public class sub_ProcessTraining : DevExpress.XtraReports.UI.XtraReport
    {
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private ReportHeaderBand ReportHeader;
        private XRTable xrTable1;
        private XRTableRow xrTableRow1;
        private XRTableCell xrTableCell4;
        private XRTableCell xrTableCell1;
        private XRTableCell xrTableCell5;
        private XRTableCell xrTableCell2;
        private XRTableCell xrTableCell3;
        private XRTable xrTable3;
        private XRTableRow xrTableRow3;
        private XRTableCell xrt_tentruong;
        private XRTableCell xrt_chuyennghanh;
        private XRTableCell xrt_tuthangnam;
        private XRTableCell xrt_hinhthuc;
        private XRTableCell xrt_vanbang;
        private ReportFooterBand ReportFooter;
        private XRLabel xrLabel1;
        private ReportFooterBand ReportFooter1;
        private XRLabel xrLabel2;
        private XRLabel xrLabel30;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public sub_ProcessTraining()
        {
            InitializeComponent();

        }

        public void BindData(int id)
        {
            DataSource = SQLHelper.ExecuteTable(SQLBusinessAdapter.GetStore_SRQuaTrinhDaoTao(id));
            xrt_tentruong.DataBindings.Add("Text", DataSource, "UniversityName");
            xrt_chuyennghanh.DataBindings.Add("Text", DataSource, "IndustryName");
            xrt_tuthangnam.DataBindings.Add("Text", DataSource, "thang");
            xrt_hinhthuc.DataBindings.Add("Text", DataSource, "TrainingSystemName");
            xrt_vanbang.DataBindings.Add("Text", DataSource, "EducationName");
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
            this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrt_tentruong = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_chuyennghanh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_tuthangnam = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_hinhthuc = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrt_vanbang = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.ReportFooter1 = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel30 = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3});
            this.Detail.Dpi = 254F;
            this.Detail.HeightF = 58.42F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrTable3
            // 
            this.xrTable3.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable3.Dpi = 254F;
            this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0.0003229777F, 0F);
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.xrTable3.SizeF = new System.Drawing.SizeF(1680F, 58.42F);
            this.xrTable3.StylePriority.UseBorders = false;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrt_tentruong,
            this.xrt_chuyennghanh,
            this.xrt_tuthangnam,
            this.xrt_hinhthuc,
            this.xrt_vanbang});
            this.xrTableRow3.Dpi = 254F;
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 1D;
            // 
            // xrt_tentruong
            // 
            this.xrt_tentruong.Dpi = 254F;
            this.xrt_tentruong.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrt_tentruong.Name = "xrt_tentruong";
            this.xrt_tentruong.Padding = new DevExpress.XtraPrinting.PaddingInfo(8, 8, 8, 8, 254F);
            this.xrt_tentruong.StylePriority.UseFont = false;
            this.xrt_tentruong.StylePriority.UsePadding = false;
            this.xrt_tentruong.StylePriority.UseTextAlignment = false;
            this.xrt_tentruong.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrt_tentruong.Weight = 0.8740701595595D;
            // 
            // xrt_chuyennghanh
            // 
            this.xrt_chuyennghanh.Dpi = 254F;
            this.xrt_chuyennghanh.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrt_chuyennghanh.Name = "xrt_chuyennghanh";
            this.xrt_chuyennghanh.Padding = new DevExpress.XtraPrinting.PaddingInfo(8, 8, 8, 8, 254F);
            this.xrt_chuyennghanh.StylePriority.UseFont = false;
            this.xrt_chuyennghanh.StylePriority.UsePadding = false;
            this.xrt_chuyennghanh.StylePriority.UseTextAlignment = false;
            this.xrt_chuyennghanh.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrt_chuyennghanh.Weight = 0.89007176340464145D;
            // 
            // xrt_tuthangnam
            // 
            this.xrt_tuthangnam.Dpi = 254F;
            this.xrt_tuthangnam.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrt_tuthangnam.Name = "xrt_tuthangnam";
            this.xrt_tuthangnam.StylePriority.UseFont = false;
            this.xrt_tuthangnam.StylePriority.UseTextAlignment = false;
            this.xrt_tuthangnam.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrt_tuthangnam.Weight = 0.55779982520894733D;
            // 
            // xrt_hinhthuc
            // 
            this.xrt_hinhthuc.Dpi = 254F;
            this.xrt_hinhthuc.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrt_hinhthuc.Name = "xrt_hinhthuc";
            this.xrt_hinhthuc.Padding = new DevExpress.XtraPrinting.PaddingInfo(8, 8, 8, 8, 254F);
            this.xrt_hinhthuc.StylePriority.UseFont = false;
            this.xrt_hinhthuc.StylePriority.UsePadding = false;
            this.xrt_hinhthuc.StylePriority.UseTextAlignment = false;
            this.xrt_hinhthuc.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrt_hinhthuc.Weight = 0.54810036228301662D;
            // 
            // xrt_vanbang
            // 
            this.xrt_vanbang.Dpi = 254F;
            this.xrt_vanbang.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrt_vanbang.Name = "xrt_vanbang";
            this.xrt_vanbang.Padding = new DevExpress.XtraPrinting.PaddingInfo(8, 8, 8, 8, 254F);
            this.xrt_vanbang.StylePriority.UseFont = false;
            this.xrt_vanbang.StylePriority.UsePadding = false;
            this.xrt_vanbang.StylePriority.UseTextAlignment = false;
            this.xrt_vanbang.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrt_vanbang.Weight = 0.65233719541165458D;
            // 
            // TopMargin
            // 
            this.TopMargin.Dpi = 254F;
            this.TopMargin.HeightF = 19F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.Dpi = 254F;
            this.BottomMargin.HeightF = 22F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel1
            // 
            this.xrLabel1.Dpi = 254F;
            this.xrLabel1.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(185.2086F, 0F);
            this.xrLabel1.Multiline = true;
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(1494.792F, 115.8875F);
            this.xrLabel1.StylePriority.UseBorders = false;
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.Text = "Hình thức đào tạo: Chính quy, tại chức, chuyên tu, bồi dưỡng .../ Văn bằng: Tiến " +
    "sĩ, Phó tiến sĩ, Thạc sĩ, Cử nhân, Kỹ sư...";
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1,
            this.xrLabel30});
            this.ReportHeader.Dpi = 254F;
            this.ReportHeader.HeightF = 250F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrTable1
            // 
            this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable1.Dpi = 254F;
            this.xrTable1.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 135.3F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(1680F, 114.7F);
            this.xrTable1.StylePriority.UseBorders = false;
            this.xrTable1.StylePriority.UseFont = false;
            this.xrTable1.StylePriority.UseTextAlignment = false;
            this.xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell4,
            this.xrTableCell1,
            this.xrTableCell5,
            this.xrTableCell2,
            this.xrTableCell3});
            this.xrTableRow1.Dpi = 254F;
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Dpi = 254F;
            this.xrTableCell4.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.StylePriority.UseFont = false;
            this.xrTableCell4.Text = "Tên trường";
            this.xrTableCell4.Weight = 0.874070635694411D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Dpi = 254F;
            this.xrTableCell1.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseFont = false;
            this.xrTableCell1.Text = "Ngành học hoặc tên lớp học";
            this.xrTableCell1.Weight = 0.89007241493287648D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.Dpi = 254F;
            this.xrTableCell5.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.StylePriority.UseFont = false;
            this.xrTableCell5.Text = "Thời gian học";
            this.xrTableCell5.Weight = 0.55780004683555062D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Dpi = 254F;
            this.xrTableCell2.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseFont = false;
            this.xrTableCell2.Text = "Hình thức học";
            this.xrTableCell2.Weight = 0.54810021286832888D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Dpi = 254F;
            this.xrTableCell3.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.StylePriority.UseFont = false;
            this.xrTableCell3.Text = "Văn bằng, chứng chỉ, trình độ gì";
            this.xrTableCell3.Weight = 0.652337215521841D;
            // 
            // ReportFooter1
            // 
            this.ReportFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel2,
            this.xrLabel1});
            this.ReportFooter1.Dpi = 254F;
            this.ReportFooter1.HeightF = 131.7625F;
            this.ReportFooter1.Name = "ReportFooter1";
            // 
            // xrLabel2
            // 
            this.xrLabel2.Dpi = 254F;
            this.xrLabel2.Font = new System.Drawing.Font("Times New Roman", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(25.00001F, 0F);
            this.xrLabel2.Multiline = true;
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(149.8126F, 115.8875F);
            this.xrLabel2.StylePriority.UseBorders = false;
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.Text = "Ghi chú:";
            // 
            // xrLabel30
            // 
            this.xrLabel30.Dpi = 254F;
            this.xrLabel30.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel30.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabel30.Multiline = true;
            this.xrLabel30.Name = "xrLabel30";
            this.xrLabel30.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel30.SizeF = new System.Drawing.SizeF(1680F, 135.3F);
            this.xrLabel30.StylePriority.UseFont = false;
            this.xrLabel30.StylePriority.UseTextAlignment = false;
            this.xrLabel30.Text = "26) ĐÀO TẠO, BỒI DƯỠNG VỀ CHUYÊN MÔN, NGHIỆP VỤ, LÝ LUẬN CHÍNH TRỊ, NGOẠI NGỮ";
            this.xrLabel30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // sub_ProcessTraining
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.ReportFooter1});
            this.Dpi = 254F;
            this.Margins = new System.Drawing.Printing.Margins(60, 60, 19, 22);
            this.PageHeight = 2970;
            this.PageWidth = 2100;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
            this.Version = "15.1";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion
    }
}
