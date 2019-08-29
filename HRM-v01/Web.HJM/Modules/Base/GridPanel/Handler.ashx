<%@ WebHandler Language="C#" Class="Handler" %>

using System.Web;
using System.Linq;
public class Handler : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "application/json";

        var start = 0;
        var limit = 10;
        var tableName = "";
        var column = "*";
        var keyword = "";
        var where = "";
        var gridName = string.Empty;
        var primaryKey = string.Empty;
        string orderBy;
        if (!string.IsNullOrEmpty(context.Request["start"]))
        {
            start = int.Parse(context.Request["start"]);
        }

        if (!string.IsNullOrEmpty(context.Request["limit"]))
        {
            limit = int.Parse(context.Request["limit"]);
        }
        if (!string.IsNullOrEmpty(context.Request["gridName"]))
        {
            gridName = context.Request["gridName"];
        }
        if (!string.IsNullOrEmpty(context.Request["table"]))
        {
            tableName = context.Request["table"];
        }
        if (!string.IsNullOrEmpty(context.Request["primarykey"]))
        {
            primaryKey = context.Request["primarykey"];
        }
        if (!string.IsNullOrEmpty(context.Request["keyword"]))
        {
            keyword = context.Request["keyword"];
        }
        if (!string.IsNullOrEmpty(context.Request["where"]))
        {
            where = "where " + context.Request["where"];
        }
        if (!string.IsNullOrEmpty(context.Request["orderBy"]))
        {
            orderBy = context.Request["orderBy"];
        }
        else
        {
            orderBy = "order by " + primaryKey + " desc";
        }
        if (!string.IsNullOrEmpty(context.Request["Column"]))
        {
            column = context.Request["Column"];
        }
        if (!string.IsNullOrEmpty(keyword))
        {
            if (string.IsNullOrEmpty(where))
            {
                where = "where ";
            }
            else
            {
                where += " and ";
            }
            var search = "";
            var columnList = (from t in WebUI.Controller.GridController.GetInstance().GetColumnInfo(gridName, tableName, 1)
                                                                                            where t.AllowSearch
                                                                                            select t).ToList();
            foreach (var col in columnList)
            {
                if (col.DataType.Equals("System.String"))
                {
                    var splitedKeyWord = SoftCore.Util.GetInstance().GetKeyword(keyword).Split(' ');
                    var s = "";
                    foreach (var item in splitedKeyWord)
                    {
                        s += "[" + col.ColumnName + "] like N'%" + item + "%' and ";
                    }
                    s = s.Remove(s.LastIndexOf(" and "));
                    search += "(" + s + ") or ";
                }
                else
                {
                    search += "[" + col.ColumnName + "] like N'" + keyword + "' or ";
                }
            }
            where = where + " (" + search.Remove(search.LastIndexOf(" or ")) + ")";
        }
        if (!string.IsNullOrEmpty(context.Request["OutsideQuery"]))
        {
            var outsideQuery = context.Request["OutsideQuery"];
            //Query từ ngoài truyền vào, query này là do control phụ của developer phát triển truyền vào
            if (string.IsNullOrEmpty(@where))
            {
                where = " where " + outsideQuery;
            }
            else
            {
                where += " and " + outsideQuery;
            }
            //#end
        }
        try
        {
            var count = int.Parse(DataController.DataHandler.GetInstance().ExecuteScalar("select count(" + primaryKey + ") from " + tableName + " " + @where).ToString());
            var sql = string.Format("select {0} from (select {1},ROW_NUMBER() OVER(" + orderBy + ") row from {2} {5})a where row between {3} and {4}", column, column, tableName, start, start + limit, where);
            var data = DataController.DataHandler.GetInstance().ExecuteDataTable(sql);
            context.Response.Write(string.Format("{{TotalRecords:{1},'Data':{0}}}", Ext.Net.JSON.Serialize(data), count));
        }
        catch
        {
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}