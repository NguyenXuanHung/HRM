 <%@ WebHandler Language="C#" Class="Web.HJM.Services.HandlerProcessTimeSheetLog" %>

using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.HumanRecord;

namespace Web.HJM.Services
{
    public class HandlerProcessTimeSheetLog : IHttpHandler
    {
        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["StandardConfig"].ConnectionString;

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                context.Response.ContentType = "text/plain";
                if (context.Request.RequestType == "POST")
                {
                    string requestBody = new StreamReader(context.Request.InputStream).ReadToEnd();

                    var infos = new List<hr_TimeSheetLog>();
                    var serializer = new JavaScriptSerializer();
                    infos = serializer.Deserialize<List<hr_TimeSheetLog>>(requestBody);

                    var listMachines = infos.Select(d => d.MachineSerialNumber).Distinct();
                    foreach (var itemMachine in listMachines)
                    {
                        //Kiem tra log da duoc luu chua
                        var logLast = hr_TimeSheetLogServices.GetTimeSheetLog(itemMachine);
                        if (logLast != null)
                        {
                            //lay log chua duoc luu de luu vao DB
                            var listLog = infos.Where(d => string.CompareOrdinal(d.Time, logLast.Time) > 0);
                            foreach (var info in listLog)
                            {
                                //create
                                CreateTimeSheet(info);
                            }
                        }
                        else
                        {
                            //TH chua co log luu tat ca du lieu vao DB
                            foreach (var info in infos)
                            {
                                //create  
                                CreateTimeSheet(info);
                            }

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

        private static void CreateTimeSheet(hr_TimeSheetLog info)
        {
            var timeLog = new hr_TimeSheetLog()
            {
                TimeSheetCode = info.TimeSheetCode,
                Time = info.Time,
                MachineName = info.MachineName,
                MachineSerialNumber = info.MachineSerialNumber,
                LocationName = info.LocationName,
                IPAddress = info.IPAddress,
                CreatedDate = info.CreatedDate,
            };
            timeLog.TimeDate = Convert.ToDateTime(info.Time);

            //only save data not still save DB
            hr_TimeSheetLogServices.Create(timeLog);
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