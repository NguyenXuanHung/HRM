<%@ WebHandler Language="C#" Class="Web.HJM.Services.HandlerSalaryLevelQuantum" %>

using System;
using System.Web;
using System.Data;
using Web.Core.Framework;

namespace Web.HJM.Services
{
    public class HandlerSalaryLevelQuantum : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var Start = 0;
            var Limit = 10;
            var Count = 0;
            var max = 0;
            string searchKey = string.Empty;
            if (!string.IsNullOrEmpty(context.Request["start"]))
            {
                Start = int.Parse(context.Request["start"]);
            }
            if (!string.IsNullOrEmpty(context.Request["limit"]))
            {
                Limit = int.Parse(context.Request["limit"]);
            }
            if (!string.IsNullOrEmpty(context.Request["max"]))
            {
                max = int.Parse(context.Request["max"]);
            }
            if (!string.IsNullOrEmpty(context.Request["searchKey"]))
            {
                searchKey = "%" + SoftCore.Util.GetInstance().GetKeyword(context.Request["searchKey"]) + "%";
            }
            DataTable table = CreateDataTable(max);
            var data =  new CatalogGroupQuantumGradeController().GetAllRecord(Start, Limit, searchKey, out Count);
                
            for (int i = 0; i < data.Rows.Count; i++)
            {
                DataRow dr = table.NewRow();
                
                dr["GroupQuantumId"] = data.Rows[i]["GroupQuantumId"];
                dr["QuantumCode"] = data.Rows[i]["QuantumCode"];
                dr["QuantumName"] = data.Rows[i]["QuantumName"] + "#" + data.Rows[i]["MonthStep"];


                int GroupQuantumId = Convert.ToInt32(data.Rows[i]["GroupQuantumId"]);
                while (Convert.ToInt32(data.Rows[i]["GroupQuantumId"]) == GroupQuantumId)
                {
                    dr["Bac" + Convert.ToInt32(data.Rows[i]["SalaryGrade"])] = (data.Rows[i]["SalaryFactor"] == null ? "" : Math.Round(Convert.ToDouble(data.Rows[i]["SalaryFactor"]), 2).ToString()) + "#" + RenderMoney(data.Rows[i]["SalaryLevel"].ToString(), '.');
                    i++;
                    if (i >= data.Rows.Count)
                        break;
                }
            table.Rows.Add(dr);
            i--;
        }
        Count = table.Rows.Count;

            context.Response.Write(string.Format("{{TotalRecords:{1},'Data':{0}}}", Ext.Net.JSON.Serialize(table), Count));
        }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    private string RenderMoney(string money, char separator)
    {
        if (string.IsNullOrEmpty(money))
        {
            return "";
        }
        int length = money.Length;
        int k = 1;
        string result = "";
        for (int i = length - 1; i >= 0; i--)
        {
            result = money[i] + result;
            if (k % 3 == 0)
                result = separator + result;
            k++;
        }
        return result;
    }

    private DataTable CreateDataTable(int max)
    {
        try
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("GroupQuantumId");
            dt.Columns.Add("QuantumCode");
            dt.Columns.Add("QuantumName");
       
            for (int i = 0; i < max; i++)
            {
                int k = i + 1;
                dt.Columns.Add("Bac" + k);
            }

            return dt;
        }
        catch
        {
            return null;
        }
    }
}
}
