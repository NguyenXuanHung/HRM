using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SmartXLS;
using Web.Core;
using Web.Core.Service.Catalog;

namespace Web.Core
{
    public class ExcelHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="workBook"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="rowEnd"></param>
        /// <param name="colEnd"></param>
        /// <param name="merge"></param>
        /// <param name="isBoid"></param>
        /// <param name="isItalia"></param>
        /// <param name="fontSize"></param>
        /// <param name="isCenter"></param>
        /// <param name="isHiddenBorder"></param>
        /// <param name="isLeft"></param>
        /// <param name="isRight"></param>
        /// <param name="textShow"></param>
        public static void CellSetting(WorkBook workBook, int row, int col, int? rowEnd, int? colEnd, bool? merge, bool isBoid, bool isItalia, int? fontSize, bool isCenter, bool isLeft, bool isRight, bool isHiddenBorder, string textShow)
        {
            workBook.setText(row, col, textShow);
            var rangeStyle = workBook.getRangeStyle(row, col, rowEnd ?? row, colEnd ?? col);
            rangeStyle.MergeCells = merge == null || merge.Value;
            if (isCenter)
            {
                rangeStyle.VerticalAlignment = RangeStyle.VerticalAlignmentCenter;
                rangeStyle.HorizontalAlignment = RangeStyle.HorizontalAlignmentCenter;
            }

            if (isLeft)
            {
                rangeStyle.HorizontalAlignment = RangeStyle.HorizontalAlignmentLeft;
            }

            if (isRight)
            {
                rangeStyle.HorizontalAlignment = RangeStyle.HorizontalAlignmentRight;
            }

            rangeStyle.FontBold = isBoid;
            rangeStyle.FontItalic = isItalia;
            if (fontSize != null)
                rangeStyle.FontSize = fontSize.Value * 20;
            if (!isHiddenBorder)
            {
                rangeStyle.BottomBorder = RangeStyle.BorderThin;
                rangeStyle.RightBorder = RangeStyle.BorderThin;
                rangeStyle.TopBorder = RangeStyle.BorderThin;
                rangeStyle.LeftBorder = RangeStyle.BorderThin;
            }
            workBook.setRangeStyle(rangeStyle, row, col, rowEnd ?? row, colEnd ?? col);
        }

        /// <summary>
        /// sign left area
        /// </summary>
        /// <param name="mbook"></param>
        /// <param name="row"></param>
        /// <param name="colStart"></param>
        /// <param name="colEnd"></param>
        /// <param name="isBold"></param>
        /// <param name="isItalic"></param>
        /// <param name="fontSize"></param>
        /// <param name="content"></param>
        public static void LeftSignArea(WorkBook mbook, int row, int colStart, int colEnd, bool isBold, bool isItalic, int? fontSize, string content)
        {
            CellSetting(mbook, row, colStart, row, colEnd, true, isBold, isItalic, fontSize, true, false, false, true, content);
        }

        /// <summary>
        /// sign right area
        /// </summary>
        /// <param name="mbook"></param>
        /// <param name="row"></param>
        /// <param name="colStart"></param>
        /// <param name="colEnd"></param>
        /// <param name="isBold"></param>
        /// <param name="isItalic"></param>
        /// <param name="fontSize"></param>
        /// <param name="content"></param>
        public static void RightSignArea(WorkBook mbook, int row, int colStart, int colEnd, bool isBold, bool isItalic, int? fontSize, string content)
        {
            CellSetting(mbook, row, colStart, row, colEnd, true, isBold, isItalic, fontSize, true, false, false, true, content);
        }

        /// <summary>
        /// sign center area
        /// </summary>
        /// <param name="mbook"></param>
        /// <param name="row"></param>
        /// <param name="colStart"></param>
        /// <param name="colEnd"></param>
        /// <param name="isBold"></param>
        /// <param name="isItalic"></param>
        /// <param name="fontSize"></param>
        /// <param name="content"></param>
        public static void CenterSignArea(WorkBook mbook, int row, int colStart, int colEnd, bool isBold, bool isItalic, int? fontSize, string content)
        {
            CellSetting(mbook, row, colStart, row, colEnd, true, isBold, isItalic, fontSize, true, false, false, true, content);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workBook"></param>
        /// <param name="rowStart"></param>
        /// <param name="colStart"></param>
        /// <param name="rowEnd"></param>
        /// <param name="colEnd"></param>
        public static void SetBorderForCellRange(WorkBook workBook, int rowStart, int colStart, int rowEnd, int colEnd)
        {
            for (var i = rowStart; i <= rowEnd; i++)
            {
                for (var j = colStart; j <= colEnd; j++)
                {
                    var rangeStyle = workBook.getRangeStyle(i, j, i, j);
                    rangeStyle.BottomBorder = RangeStyle.BorderThin;
                    rangeStyle.RightBorder = RangeStyle.BorderThin;
                    rangeStyle.TopBorder = RangeStyle.BorderThin;
                    rangeStyle.LeftBorder = RangeStyle.BorderThin;
                    workBook.setRangeStyle(rangeStyle, i, j, i, j);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workBook"></param>
        /// <param name="col"></param>
        public static void SetAutoResizeColumn(WorkBook workBook, int col)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path">Đường dẫn đến file excel</param>
        public static List<T> ImportExcel<T>(string path) where T : new()
        {
            try
            {
                var list = new List<T>();
                if (path != null)
                {
                    var workbook = new WorkBook();
                    // Read data from excel
                    workbook.readXLSX(path);

                    // Export to datatable
                    var dataTable = workbook.ExportDataTable(1, //first row
                        0, //first col
                        workbook.LastRow, //last row
                        workbook.LastCol + 1, //last col
                        true, //first row as header
                        false //convert to DateTime object if it match date pattern
                    );

                    foreach (DataRow row in dataTable.Rows)
                    {
                        var obj = new T();
                        foreach (DataColumn col in dataTable.Columns)
                        {
                            foreach (var property in typeof(T).GetProperties())
                            {
                                // get prop description
                                var attributeDisplayName = property.GetCustomAttribute(typeof(DisplayNameAttribute));
                                var attributeType = property.GetCustomAttribute(typeof(TypeConverterAttribute));
                                if (attributeDisplayName != null && col.ColumnName == property.Name && property.CanWrite)
                                {
                                    var value = row[col];
                                    if (attributeType != null)
                                    {
                                        var reg = @"\(([^)]*)\)";
                                        if (Regex.IsMatch(value.ToString(), reg))
                                        {
                                            value = Regex.Match(value.ToString(), reg).Groups[1].Value;
                                        }
                                    }
                                    if (!property.PropertyType.IsEnum)
                                    {
                                        var val = value;
                                        if (!(val is DBNull))
                                            typeof(T).InvokeMember(property.Name, BindingFlags.SetProperty, null, obj,
                                                new[] { val });
                                    }
                                    else
                                    {
                                        var val = Enum.ToObject(property.PropertyType, Convert.ToInt32(value));
                                        if (!(val is DBNull))
                                            typeof(T).InvokeMember(property.Name, BindingFlags.SetProperty, null, obj,
                                                new[] { val });
                                    }
                                }
                            }
                        }
                        // create
                        list.Add(obj);
                    }
                }

                return list;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serverPath"></param>
        /// <param name="dropDownFromRow">Tạo drop down từ dòng</param>
        /// <param name="dropDownToRow">Tạo drop down đến dòng</param>
        public static WorkBook ExportExcelTemplate<T>(string serverPath, int dropDownFromRow, int dropDownToRow)
        {
            // create datatable
            var dataTable = CreateDataTableHeader<T>();

            // create workbook
            var workbook = CreateCustomWorkBook(dataTable);

            foreach (DataColumn col in dataTable.Columns)
            {
                foreach (var property in typeof(T).GetProperties())
                {
                    var attribute = property.GetCustomAttribute(typeof(TypeConverterAttribute));
                    if (attribute != null && col.ColumnName == property.Name)
                    {
                        var typeName = ((TypeConverterAttribute)attribute).ConverterTypeName;
                        CreateDropDownExcel(workbook, col.Ordinal, dropDownFromRow, dropDownToRow, typeName);
                    }
                }
            }

            workbook.writeXLSX(serverPath);

            return workbook;
        }

        #region Private Methods

        /// <summary>
        /// Create workbook with 2 sheet
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        private static WorkBook CreateCustomWorkBook(DataTable dataTable)
        {
            var workbook = new WorkBook();
            workbook.ImportDataTable(dataTable, false, 0, 0, dataTable.Rows.Count + 1, dataTable.Columns.Count + 1);
            // set main sheet name
            workbook.setSheetName(0, "Thêm mới thông tin");
            // create a new sheet to store list catalog or list enum
            workbook.insertSheets(1, 1);
            // set new sheet name
            workbook.setSheetName(1, "Info");
            // set Info sheet hidden
            workbook.SheetHidden = WorkBook.SheetStateHidden;
            // select back to main sheet
            workbook.Sheet = 0;
            // hide DisplayName row
            workbook.setRowHidden(1, true);
            // set header style
            var range = workbook.getRangeStyle();
            range.VerticalAlignment = RangeStyle.VerticalAlignmentCenter;
            range.HorizontalAlignment = RangeStyle.HorizontalAlignmentCenter;
            range.FontBold = true;
            range.FontSize = 11 * 20;

            workbook.setRangeStyle(range, 0, 0, 0, workbook.LastCol);
            // auto resize columns
            for (var i = 0; i <= workbook.LastCol; i++)
            {
                workbook.setColWidthAutoSize(i, true);
            }


            return workbook;
        }

        /// <summary>
        /// Create DataTable with headers are T's properties
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static DataTable CreateDataTableHeader<T>()
        {
            var dataTable = new DataTable();
            var colCount = 0;

            dataTable.Rows.Add();
            dataTable.Rows.Add();
            // create header by property name and description
            foreach (var property in typeof(T).GetProperties())
            {
                var displayName = string.Empty;
                // get prop description
                var attribute = property.GetCustomAttribute(typeof(DisplayNameAttribute));
                if (attribute != null)
                {
                    displayName = ((DisplayNameAttribute)attribute).DisplayName;
                }
                // set description
                if (string.IsNullOrEmpty(displayName)) continue;

                // add column
                dataTable.Columns.Add();
                dataTable.Rows[0][colCount] = displayName;
                dataTable.Rows[1][colCount] = property.Name;

                // set column datatable name
                dataTable.Columns[colCount].ColumnName = property.Name;
                colCount++;
            }

            return dataTable;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="workbook"></param>
        /// <param name="colNum"></param>
        /// <param name="fromRow"></param>
        /// <param name="toRow"></param>
        private static void CreateDropDownExcel(WorkBook workbook, int colNum, int fromRow, int toRow, string typeName)
        {
            // get type
            var type = Type.GetType(typeName);
            if (type == null) return;
            // create validation
            var validation = workbook.CreateDataValidation();
            validation.Type = DataValidation.eUser;
            //
            string validateString;
            // check type enum
            if (type.IsEnum)
            {
                // get list enum
                var list = type.GetIntAndDescription();
                validateString = $"\"{string.Join(",", list.Select(l => $"{l.Value} ({l.Key})"))}\"";
            }
            else
            {
                // get list catalog
                var list = cat_BaseServices.GetAll(type.Name, null, null, null, false, null, null);
                if (list == null || list.Count == 0) return;
                // 
                validateString = "\"{0}\"".FormatWith(string.Join(",", list.Select(l => l.Name + " ({0})".FormatWith(l.Id))));
            }

            // formula string length cannot be greater than 256
            if (validateString.Length < 256)
            {
                // set formula by string
                validation.Formula1 = validateString;
            }
            else
            {
                // string to list
                var validateList = validateString.Trim('"').Split(',');
                // select info sheet
                workbook.Sheet = 1;
                // write list into info sheet
                foreach (var item in validateList.Select((value, index) => new { value, index }))
                {
                    workbook.setText(item.index + 1, colNum, item.value);
                }
                // select back to sheet 0
                workbook.Sheet = 0;
                // set formula by selected range
                validation.Formula1 = "Info!${0}$2:${0}${1}".FormatWith((colNum + 1).ToExcelColumnName(), validateList.Length);
            }
            // selection range
            workbook.setSelection(fromRow, colNum, toRow, colNum);
            workbook.DataValidation = validation;
        }

        /// <summary>
        /// Tạo nhiều drop down tại 1 cột
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="workbook"></param>
        /// <param name="serverPath">đường dẫn file</param>
        /// <param name="colNum">vị trí cột</param>
        /// <param name="fromRow">từ dòng</param>
        /// <param name="toRow">đến dòng</param>
        /// <param name="items">danh sách</param>
        /// <param name="showField">trường hiển thị</param>
        public static void CreateCustomDropDown<T>(WorkBook workbook, string serverPath, int colNum, int fromRow, int toRow, List<T> items, string showField)
        {
            try
            {
                var validation = workbook.CreateDataValidation();
                validation.Type = DataValidation.eUser;
                //
                var validateString = string.Empty;
                validateString += "\"{0}\"".FormatWith(string.Join(",", items.Select(item => item.GetType().GetProperty(showField)?.GetValue(item))));

                // formula string length cannot be greater than 256
                if (validateString.Length < 256)
                {
                    // set formula by string
                    validation.Formula1 = validateString;
                }
                else
                {
                    // string to list
                    var validateList = validateString.Trim('"').Split(',');
                    // select info sheet
                    workbook.Sheet = 1;
                    // write list into info sheet
                    foreach (var item in validateList.Select((value, index) => new { value, index }))
                    {
                        workbook.setText(item.index + 1, colNum, item.value);
                    }
                    // select back to sheet 0
                    workbook.Sheet = 0;
                    // set formula by selected range
                    validation.Formula1 = "Info!${0}$2:${0}${1}".FormatWith((colNum + 1).ToExcelColumnName(), validateList.Length + 1);
                }
                // selection range
                workbook.setSelection(fromRow, colNum, toRow, colNum);
                workbook.DataValidation = validation;

                workbook.writeXLSX(serverPath);

            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Get excel column name
        /// </summary>
        /// <param name="columnNumber"></param>
        /// <returns></returns>
        private static string GetExcelColumnName(int columnNumber)
        {
            var dividend = columnNumber;
            var columnName = string.Empty;

            while (dividend > 0)
            {
                var modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (int)((dividend - modulo) / 26);
            }
            return columnName;
        }

        #endregion

    }
}
