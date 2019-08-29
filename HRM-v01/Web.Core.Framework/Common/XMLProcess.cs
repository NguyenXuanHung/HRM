using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using SoftCore;
using System.IO;
using System.Data;

namespace Web.Core.Framework.Common
{
    /// <summary>
    /// Summary description for XMLProcess
    /// </summary>
    public class XMLProcess
    {
        public XMLProcess()
        {
        }

        public List<MathRule> GetAll(System.Xml.Linq.XDocument document, bool displayOnGrid)
        {
            var rs = from t in document.Descendants("Column")
                where bool.Parse(t.Attribute("DisplayOnGrid").Value) == displayOnGrid
                select new MathRule
                {
                    ColumnInDB = t.Element("DataBase").Value,
                    ColumnInSoftware = t.Element("Software").Value,
                    ColumnInExcel = t.Element("Excel").Value,
                    AllowBlank = t.Attribute("AllowBlank").Value,
                    DataType = t.Attribute("DataType").Value,
                    DisplayOnGrid = bool.Parse(t.Attribute("DisplayOnGrid").Value),
                    DefaultValue = t.Attribute("DefaultValue").Value
                };
            return rs.ToList();
        }

        public List<MathRule> GetAllByMathStatus(System.Xml.Linq.XDocument document, bool mathStatus)
        {
            var rs = from t in document.Descendants("Column")
                where bool.Parse(t.Attribute("AllowBlank").Value) == mathStatus
                select new MathRule
                {
                    ColumnInDB = t.Element("DataBase").Value,
                    ColumnInSoftware = t.Element("Software").Value,
                    ColumnInExcel = t.Element("Excel").Value,
                    AllowBlank = t.Attribute("AllowBlank").Value,
                    DataType = t.Attribute("DataType").Value,
                    DisplayOnGrid = bool.Parse(t.Attribute("DisplayOnGrid").Value),
                    DefaultValue = t.Attribute("DefaultValue").Value
                };
            return rs.ToList();
        }

        public List<MathRule> GetAll(System.Xml.Linq.XDocument document)
        {
            var rs = from t in document.Descendants("Column")
                select new MathRule
                {
                    ColumnInDB = t.Element("DataBase").Value,
                    ColumnInSoftware = t.Element("Software").Value,
                    ColumnInExcel = t.Element("Excel").Value,
                    AllowBlank = t.Attribute("AllowBlank").Value,
                    DataType = t.Attribute("DataType").Value,
                    DisplayOnGrid = bool.Parse(t.Attribute("DisplayOnGrid").Value),
                    DefaultValue = t.Attribute("DefaultValue").Value
                };
            return rs.ToList();
        }

        public MathRule GetByColumnName(System.Xml.Linq.XDocument document, string ColumnInDB)
        {
            var rs = from t in document.Descendants("Column")
                where t.Element("DataBase").Value == ColumnInDB
                select new MathRule
                {
                    ColumnInDB = t.Element("DataBase").Value.ToString(),
                    ColumnInSoftware = t.Element("Software").Value.ToString(),
                    ColumnInExcel = t.Element("Excel").Value.ToString(),
                    AllowBlank = t.Attribute("AllowBlank").Value,
                    DataType = t.Attribute("DataType").Value,
                    DisplayOnGrid = bool.Parse(t.Attribute("DisplayOnGrid").Value),
                    DefaultValue = t.Attribute("DefaultValue").Value
                };
            return rs.FirstOrDefault();
        }

        public void UpdateTuDien(string fileName, MathRule mathRule)
        {
            System.Xml.Linq.XDocument document = System.Xml.Linq.XDocument.Load(fileName);
            var node = (from t in document.Descendants("Column")
                where t.Element("DataBase").Value == mathRule.ColumnInDB
                select t).FirstOrDefault();

            node.Element("Excel").Value = mathRule.ColumnInExcel;
            document.Save(fileName);
        }

        public void Update(string fileName, MathRule mathRule)
        {
            System.Xml.Linq.XDocument document = System.Xml.Linq.XDocument.Load(fileName);
            var node = (from t in document.Descendants("Column")
                where t.Element("DataBase").Value == mathRule.ColumnInDB
                select t).FirstOrDefault();
            // check exist element excel
            string[] arr = node.Element("Excel").Value.Split(';');
            List<string> list = new List<string>();
            for (int i = 0; i < arr.Length; i++)
            {
                list.Add(arr[i].Trim());
            }

            if (!list.Contains(mathRule.ColumnInExcel.Trim()))
                node.Element("Excel").Value += ";" + mathRule.ColumnInExcel;

            document.Save(fileName);
        }

        /// <summary>
        /// Tạo mới 1 file MathRule
        /// </summary>
        /// <param name="p"></param>
        /// <param name="tableName"></param>
        public void CreateNewMathRuleFile(string p, string tableName)
        {

            XDocument document = new XDocument();
            XElement root = new XElement("MathRule");
            document.Add(root);
            DataTable columnInformation = Util.GetInstance().GetMoreColumnInforOfTable(tableName);
            foreach (DataRow row in columnInformation.Rows)
            {
                XElement column = new XElement("Column",
                    new XElement("DataBase", row["COLUMN_NAME"]),
                    new XElement("Software", row["COLUMN_NAME"]),
                    new XElement("Excel", row["COLUMN_NAME"]));
                if (row["IS_NULLABLE"].ToString() == "NO")
                {
                    column.SetAttributeValue("AllowBlank", "false");
                }
                else
                {
                    column.SetAttributeValue("AllowBlank", "true");
                }

                switch (row["DATA_TYPE"].ToString())
                {
                    case "nvarchar":
                    case "ntext":
                    case "varchar":
                        column.SetAttributeValue("DataType", "String");
                        break;
                    case "datetime":
                    case "date":
                    case "smalldatetime":
                        column.SetAttributeValue("DataType", "DateTime");
                        break;
                    case "bit":
                        column.SetAttributeValue("DataType", "Bit");
                        break;
                    case "decimal":
                    case "numeric":
                        column.SetAttributeValue("DataType", "Float");
                        break;
                    case "int":
                        column.SetAttributeValue("DataType", "Integer");
                        break;
                }

                column.SetAttributeValue("DisplayOnGrid", "true");
                column.SetAttributeValue("DefaultValue", "");
                root.Add(column);
            }

            FileInfo f = new FileInfo(p);
            if (f.Exists)
                f.Delete();
            document.Save(p);
        }

        /// <summary>
        /// Get config information at Manifest file
        /// </summary>
        /// <param name="fileName">file path</param>
        /// <param name="elementName">Element want to get</param>
        /// <returns></returns>
        public List<ManifestInfo> GetAllManifest(string fileName, string elementName)
        {
            try
            {
                XElement document = XElement.Load(fileName);
                XElement salaryElement = document.Element(elementName);
                var rs = from t in salaryElement.Descendants("Control")
                    select new ManifestInfo
                    {
                        id = t.Attribute("id").Value,
                        desc = t.Attribute("desc").Value,
                        visible = bool.Parse(t.Attribute("visible").Value)
                    };
                return rs.ToList();
            }
            catch (Exception)
            {
                return new List<ManifestInfo>();
            }
        }

        /// <summary>
        /// Get config information at Manifest with visible
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="elementName"></param>
        /// <param name="visible"></param>
        /// <returns></returns>
        public List<ManifestInfo> GetManifestByVisible(string fileName, string elementName, bool visible)
        {
            try
            {
                XElement document = XElement.Load(fileName);
                XElement salaryElement = document.Element(elementName);
                var rs = from t in salaryElement.Descendants("Control")
                    where bool.Parse(t.Attribute("visible").Value) == visible
                    select new ManifestInfo
                    {
                        id = t.Attribute("id").Value,
                        desc = t.Attribute("desc").Value,
                        visible = bool.Parse(t.Attribute("visible").Value)
                    };
                return rs.ToList();

            }
            catch (Exception ex)
            {
                return new List<ManifestInfo>();
            }
        }
    }

    public class MathRule
    {
        public string ColumnInDB { get; set; }
        public string ColumnInSoftware { get; set; }
        public string ColumnInExcel { get; set; }
        public string AllowBlank { get; set; }
        public string DataType { get; set; }
        public string MathStatus { get; set; }
        public bool DisplayOnGrid { get; set; }
        public string DefaultValue { get; set; }
    }
}
