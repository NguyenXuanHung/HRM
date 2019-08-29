using DevExpress.XtraReports.UI;

namespace Web.Core.Framework.Report
{
    /// <summary>
    /// Quan hệ gia đình
    /// Summary description for sub_FamilyRelationship
    /// </summary>
    public class sub_FamilyRelationship : DevExpress.XtraReports.UI.XtraReport
    {
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private XRTable xrTable2;
        private XRTableRow xrTableRow2;
        private XRTableCell xrRelationShipName;
        private XRTableCell xrName;
        private XRTableCell xrDateOfBirth;
        private XRTableCell xrWorkPlace;
        private PageHeaderBand PageHeader;
        private XRTable xrTable1;
        private XRTableRow xrTableRow1;
        private XRTableCell xrTableCell1;
        private XRTableCell xrTableCell2;
        private XRTableCell xrTableCell4;
        private XRTableCell xrTableCell5;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public sub_FamilyRelationship()
        {
            InitializeComponent();
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if(disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void BindData(int id, int moiQuanHe)
        {
            DataSource = SQLHelper.ExecuteTable(SQLBusinessAdapter.GetStore_SRQuanHeGiaDinh(id, moiQuanHe));
            xrRelationShipName.DataBindings.Add("Text", DataSource, "RelationshipName");
            xrName.DataBindings.Add("Text", DataSource, "FullName");
            xrDateOfBirth.DataBindings.Add("Text", DataSource, "Age");
            xrWorkPlace.DataBindings.Add("Text", DataSource, "Note");
        }
        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrRelationShipName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrDateOfBirth = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrWorkPlace = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
            this.Detail.Dpi = 254F;
            this.Detail.HeightF = 113.7708F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrTable2
            // 
            this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable2.Dpi = 254F;
            this.xrTable2.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(1680F, 113.7708F);
            this.xrTable2.StylePriority.UseBorders = false;
            this.xrTable2.StylePriority.UseFont = false;
            this.xrTable2.StylePriority.UseTextAlignment = false;
            this.xrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrRelationShipName,
            this.xrName,
            this.xrDateOfBirth,
            this.xrWorkPlace});
            this.xrTableRow2.Dpi = 254F;
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // xrRelationShipName
            // 
            this.xrRelationShipName.Dpi = 254F;
            this.xrRelationShipName.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrRelationShipName.Name = "xrRelationShipName";
            this.xrRelationShipName.Padding = new DevExpress.XtraPrinting.PaddingInfo(8, 0, 0, 0, 254F);
            this.xrRelationShipName.StylePriority.UseFont = false;
            this.xrRelationShipName.StylePriority.UsePadding = false;
            this.xrRelationShipName.StylePriority.UseTextAlignment = false;
            this.xrRelationShipName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrRelationShipName.Weight = 0.37980764996024691D;
            // 
            // xrName
            // 
            this.xrName.Dpi = 254F;
            this.xrName.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrName.Name = "xrName";
            this.xrName.Padding = new DevExpress.XtraPrinting.PaddingInfo(8, 0, 0, 0, 254F);
            this.xrName.StylePriority.UseFont = false;
            this.xrName.StylePriority.UsePadding = false;
            this.xrName.StylePriority.UseTextAlignment = false;
            this.xrName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrName.Weight = 0.7041605226611769D;
            // 
            // xrDateOfBirth
            // 
            this.xrDateOfBirth.Dpi = 254F;
            this.xrDateOfBirth.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrDateOfBirth.Name = "xrDateOfBirth";
            this.xrDateOfBirth.StylePriority.UseFont = false;
            this.xrDateOfBirth.StylePriority.UseTextAlignment = false;
            this.xrDateOfBirth.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrDateOfBirth.Weight = 0.32322830670779168D;
            // 
            // xrWorkPlace
            // 
            this.xrWorkPlace.Dpi = 254F;
            this.xrWorkPlace.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrWorkPlace.Name = "xrWorkPlace";
            this.xrWorkPlace.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 5, 5, 254F);
            this.xrWorkPlace.StylePriority.UseFont = false;
            this.xrWorkPlace.StylePriority.UsePadding = false;
            this.xrWorkPlace.StylePriority.UseTextAlignment = false;
            this.xrWorkPlace.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrWorkPlace.Weight = 2.3649211293911909D;
            // 
            // TopMargin
            // 
            this.TopMargin.Dpi = 254F;
            this.TopMargin.HeightF = 27F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.Dpi = 254F;
            this.BottomMargin.HeightF = 27F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
            this.PageHeader.Dpi = 254F;
            this.PageHeader.HeightF = 178.73F;
            this.PageHeader.Name = "PageHeader";
            // 
            // xrTable1
            // 
            this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable1.Dpi = 254F;
            this.xrTable1.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(1680F, 178.73F);
            this.xrTable1.StylePriority.UseBorders = false;
            this.xrTable1.StylePriority.UseFont = false;
            this.xrTable1.StylePriority.UseTextAlignment = false;
            this.xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell2,
            this.xrTableCell4,
            this.xrTableCell5});
            this.xrTableRow1.Dpi = 254F;
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Dpi = 254F;
            this.xrTableCell1.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseFont = false;
            this.xrTableCell1.Text = "Quan hệ";
            this.xrTableCell1.Weight = 0.37980764996024691D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Dpi = 254F;
            this.xrTableCell2.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseFont = false;
            this.xrTableCell2.Text = "Họ và tên";
            this.xrTableCell2.Weight = 0.70416035727392634D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Dpi = 254F;
            this.xrTableCell4.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.StylePriority.UseFont = false;
            this.xrTableCell4.Text = "Năm sinh";
            this.xrTableCell4.Weight = 0.32322834474775641D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.Dpi = 254F;
            this.xrTableCell5.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.StylePriority.UseFont = false;
            this.xrTableCell5.Text = "Quê quán, nghề nghiệp, chức danh, chức vụ, đơn vị công tác, học tập, nơi ở (trong" +
    ", ngoài nước); thành viên các tổ chức chính trị - xã hội ...";
            this.xrTableCell5.Weight = 2.3649205980346535D;
            // 
            // sub_FamilyRelationship
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.PageHeader});
            this.Dpi = 254F;
            this.Margins = new System.Drawing.Printing.Margins(60, 60, 27, 27);
            this.PageHeight = 2970;
            this.PageWidth = 2100;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
            this.Version = "15.1";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion
    }

}


