 <%@ WebHandler Language="C#" Class="Web.HRM.Services.HandlerProcessTimeSheetLog" %>

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using Web.Core;
using Web.Core.Framework;
using Web.Core.Framework.SQLAdapter;

namespace Web.HRM.Services
{
    public class HandlerProcessTimeSheetLog : Core.Framework.BaseControl.BaseHandler, IHttpHandler
    {
        private const int MaxRecord = 500;
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                context.Response.ContentType = "text/plain";
                if (context.Request.RequestType == "POST")
                {
                    var requestBody = new StreamReader(context.Request.InputStream).ReadToEnd();

                    var serializer = new JavaScriptSerializer {MaxJsonLength = int.MaxValue};

                    var infos = serializer.Deserialize<List<TimeSheetLogHandlerProcessModel>>(requestBody);

                    var listMachines = infos.Select(d => d.MachineSerialNumber).Distinct();
                    foreach (var itemMachine in listMachines)
                    {
                        var logValues = string.Empty;
                        var sql = string.Empty;
                        var order = @" [Id] DESC";
                        //Kiem tra log da duoc luu chua
                        var logLast = TimeSheetLogController.GetTimeSheetLog(itemMachine, null, order);
                        if (logLast != null)
                        {
                            //lay log chua duoc luu de luu vao DB
                            var listLog = infos.Where(d => DateTime.ParseExact(d.Time, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) > logLast.TimeDate
                                                           && d.MachineSerialNumber == itemMachine).ToList();

                            //create data save DB
                            CreateTimeLog(listLog, logValues, sql);
                        }
                        else
                        {
                            //create data save DB
                            CreateTimeLog(infos, logValues, sql);
                        }
                    }
                    context.Response.Write("Saved data to Database");
                }
            }
            catch (Exception ex)
            {
                context.Response.Write(ex.ToString());
            }

        }

        /// <summary>
        /// split data and create to DB
        /// </summary>
        /// <param name="infos"></param>
        /// <param name="logValues"></param>
        /// <param name="sql"></param>
        private static void CreateTimeLog(List<TimeSheetLogHandlerProcessModel> infos, string logValues, string sql)
        {
            try
            {
                var totalPage = infos.Count % MaxRecord == 0 ? infos.Count / MaxRecord : infos.Count / MaxRecord + 1;
                for (var i = 0; i < totalPage; i++)
                {
                    var start = i * MaxRecord;
                    var stop = start + MaxRecord < infos.Count ? start + MaxRecord : infos.Count;
                    for (var j = start; j < stop; j++)
                    {
                        var info = infos[j];
                        var logDateTime = Convert.ToDateTime(info.Time).ToString("yyyy-MM-dd HH:mm:ss");
                        var result = "(" +
                                     "'{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}' ".FormatWith(info.TimeSheetCode,
                                         info.Time, logDateTime, info.MachineSerialNumber,
                                         info.MachineName, info.IPAddress, info.LocationName,
                                         DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                                     + ")";
                        // set list values
                        logValues += "," + result;
                    }

                    // create insert query
                    sql = SQLTimeTrackingAdapter.EditSql(sql, logValues);
                    // execute query
                    SQLHelper.ExecuteNonQuery(sql);
                    //Reset
                    logValues = "";
                    sql = "";
                }
            }
            catch
            {
                // log error
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
}