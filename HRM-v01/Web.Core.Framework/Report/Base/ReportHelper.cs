using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using DevExpress.Utils;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using Web.Core.Framework.Common;
using Web.Core.Object.Report;

namespace Web.Core.Framework.Report
{
    public class ReportHelper
    {
        public static int ReportWidthLanscape = 1140;
        public static int ReportWidthPortrait = 0;
        public static int DefaultCellIndexWidth = 40;
        public static int DefaultCellWith = 100;
        public static int DefaultCellHeight = 25;
        public static int DefaultFontSize = 10;

        /// <summary>
        /// Create table
        /// </summary>
        /// <param name="name"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static XRTable CreateTable(string name, int width, int height)
        {
            var table = new XRTable
            {
                Name = name,
                LocationFloat = new PointFloat(0F, 0F),
                Borders = BorderSide.None
            };
            if(width > 0)
                table.Width = width;
            if(height > 0)
                table.Height = height;
            return table;
        }

        /// <summary>
        /// Create table row
        /// </summary>
        /// <returns></returns>
        public static XRTableRow CreateTableRow()
        {
            var row = new XRTableRow
            {
                Borders = BorderSide.None
            };
            return row;
        }

        /// <summary>
        /// Create table cell
        /// </summary>
        /// <param name="name"></param>
        /// <param name="text"></param>
        /// <param name="fontSize"></param>
        /// <param name="fontStyle"></param>
        /// <param name="borders"></param>
        /// <param name="textAlign"></param>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static XRTableCell CreateTableCell(string name, string text, int fontSize, FontStyle fontStyle, BorderSide borders,
            ReportTextAlign textAlign, int? posX, int? posY, int width, int height)
        {
            // init cell
            var cell = new XRTableCell
            {
                Borders = borders,
                TextAlignment = GetTextAlignment(textAlign),
                Padding = new PaddingInfo(2, 2, 0, 0, 100F),
            };

            // name
            if(!string.IsNullOrEmpty(name)) cell.Name = name;

            // text
            if(!string.IsNullOrEmpty(text)) cell.Text = text;

            // font
            if(fontSize > 0) cell.Font = new Font("Times New Roman", fontSize, fontStyle);

            // location
            if(posX != null & posY != null) cell.Location = new Point(posX.Value, posY.Value);

            // width
            if(width > 0) cell.Width = width;

            // height
            if(height > 0) cell.Height = height;

            // return
            return cell;
        }

        /// <summary>
        /// Create label
        /// </summary>
        /// <param name="name"></param>
        /// <param name="text"></param>
        /// <param name="fontSize"></param>
        /// <param name="fontStyle"></param>
        /// <param name="borders"></param>
        /// <param name="textAlign"></param>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static XRLabel CreateLabel(string name, string text, int fontSize, FontStyle fontStyle, BorderSide borders,
            ReportTextAlign textAlign, int? posX, int? posY, int width, int height)
        {
            // init cell
            var label = new XRLabel
            {
                Multiline = true,
                Borders = borders,
                TextAlignment = GetTextAlignment(textAlign),
                Padding = new PaddingInfo(2, 2, 0, 0, 100F)
            };

            // name
            if(!string.IsNullOrEmpty(name)) label.Name = name;

            // text
            if(!string.IsNullOrEmpty(text)) label.Text = text;

            // font
            if(fontSize > 0) label.Font = new Font("Times New Roman", fontSize, fontStyle);

            // location
            if(posX != null & posY != null) label.Location = new Point(posX.Value, posY.Value);

            // width
            if(width > 0) label.Width = width;

            // height
            if(height > 0) label.Height = height;

            // return
            return label;
        }

        /// <summary>
        /// Convert report text align to extra text align
        /// </summary>
        /// <param name="align"></param>
        /// <returns></returns>
        public static TextAlignment GetTextAlignment(ReportTextAlign align)
        {
            switch(align)
            {
                case ReportTextAlign.TopCenter:
                    return TextAlignment.TopCenter;
                case ReportTextAlign.TopJustify:
                    return TextAlignment.TopJustify;
                case ReportTextAlign.TopLeft:
                    return TextAlignment.TopLeft;
                case ReportTextAlign.TopRight:
                    return TextAlignment.TopRight;
                case ReportTextAlign.BottomCenter:
                    return TextAlignment.BottomCenter;
                case ReportTextAlign.BottomJustify:
                    return TextAlignment.BottomJustify;
                case ReportTextAlign.BottomLeft:
                    return TextAlignment.BottomLeft;
                case ReportTextAlign.BottomRight:
                    return TextAlignment.BottomRight;
                case ReportTextAlign.MiddleCenter:
                    return TextAlignment.MiddleCenter;
                case ReportTextAlign.MiddleJustify:
                    return TextAlignment.MiddleJustify;
                case ReportTextAlign.MiddleLeft:
                    return TextAlignment.MiddleLeft;
                case ReportTextAlign.MiddleRight:
                    return TextAlignment.MiddleRight;
                default:
                    return TextAlignment.MiddleCenter;
            }
        }

        /// <summary>
        /// Calculate report width
        /// </summary>
        /// <param name="paperKind"></param>
        /// <param name="orientation"></param>
        /// <returns></returns>
        public static int ReportWidth(ReportPaperKind paperKind, ReportOrientation orientation)
        {
            var width = 0;
            switch (paperKind)
            {
                case ReportPaperKind.A4:
                    switch (orientation)
                    {
                        case ReportOrientation.Landscape:
                            width = Constant.ReportWidthLandscapeA4;
                            break;
                        case ReportOrientation.Portrait:
                            width = Constant.ReportWidthPotraitA4;
                            break;
                    }
                    break;
                case ReportPaperKind.A3:
                    switch (orientation)
                    {
                        case ReportOrientation.Landscape:
                            width = Constant.ReportWidthLandscapeA3;
                            break;
                        case ReportOrientation.Portrait:
                            width = Constant.ReportWidthPotraitA3;
                            break;
                    }
                    break;
                case ReportPaperKind.A2:
                    switch (orientation)
                    {
                        case ReportOrientation.Landscape:
                            width = Constant.ReportWidthLandscapeA2;
                            break;
                        case ReportOrientation.Portrait:
                            width = Constant.ReportWidthPotraitA2;
                            break;
                    }
                    break;
            }
            return width;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportColumns"></param>
        /// <param name="columnType"></param>
        /// <returns></returns>
        public static int ReportConlumnTypeWidth(List<ReportColumn> reportColumns, ReportColumnType columnType)
        {
            return reportColumns.Where(r => r.ParentId == 0 && r.Type == columnType).Sum(r => r.Width) + Constant.IndexColumnWidth;
        }
    }
}
