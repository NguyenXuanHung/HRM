using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace Web.Core
{
    public static class DataTableExtension
    {
        public static T ToObject<T>(this DataTable dt)
        {
            return dt.ToList<T>().FirstOrDefault();
        }

        public static List<T> ToList<T>(this DataTable dt)
        {
            if (dt == null)
                return new List<T>();
            var lst = new List<T>();
            var tClass = typeof(T);
            var pClass = tClass.GetProperties();
            var dc = dt.Columns.Cast<DataColumn>().ToList();
            foreach (DataRow item in dt.Rows)
            {
                var cn = (T)Activator.CreateInstance(tClass);
                foreach (var pc in pClass)
                {
                    try
                    {
                        var d = dc.Find(c => c.ColumnName == pc.Name);
                        if (d != null)
                            pc.SetValue(cn, item[pc.Name], null);
                    }
                    catch
                    {
                    }
                }
                lst.Add(cn);
            }
            return lst;
        }

        public static DataTable GetDifferentRecords(this DataTable firstDataTable, DataTable secondDataTable)
        {
            //Create Empty Table   
            var resultDataTable = new DataTable("ResultDataTable");

            //use a Dataset to make use of a DataRelation object   
            using(var ds = new DataSet())
            {
                //Add tables   
                ds.Tables.AddRange(new[] { firstDataTable.Copy(), secondDataTable.Copy() });

                //Get Columns for DataRelation   
                var firstColumns = new DataColumn[ds.Tables[0].Columns.Count];
                for(var i = 0; i < firstColumns.Length; i++)
                {
                    firstColumns[i] = ds.Tables[0].Columns[i];
                }

                var secondColumns = new DataColumn[ds.Tables[1].Columns.Count];
                for(var i = 0; i < secondColumns.Length; i++)
                {
                    secondColumns[i] = ds.Tables[1].Columns[i];
                }

                //Create DataRelation   
                var r1 = new DataRelation(string.Empty, firstColumns, secondColumns, false);
                ds.Relations.Add(r1);

                var r2 = new DataRelation(string.Empty, secondColumns, firstColumns, false);
                ds.Relations.Add(r2);

                //Create columns for return table   
                for(var i = 0; i < firstDataTable.Columns.Count; i++)
                {
                    resultDataTable.Columns.Add(firstDataTable.Columns[i].ColumnName, firstDataTable.Columns[i].DataType);
                }

                //If FirstDataTable Row not in SecondDataTable, Add to ResultDataTable.   
                resultDataTable.BeginLoadData();
                foreach(DataRow parentRow in ds.Tables[0].Rows)
                {
                    var childRows = parentRow.GetChildRows(r1);
                    if(childRows == null || childRows.Length == 0)
                        resultDataTable.LoadDataRow(parentRow.ItemArray, true);
                }

                //If SecondDataTable Row not in FirstDataTable, Add to ResultDataTable.   
                foreach(DataRow parentRow in ds.Tables[1].Rows)
                {
                    var childRows = parentRow.GetChildRows(r2);
                    if(childRows == null || childRows.Length == 0)
                        resultDataTable.LoadDataRow(parentRow.ItemArray, true);
                }
                resultDataTable.EndLoadData();
            }

            return resultDataTable;
        }
    }
}
