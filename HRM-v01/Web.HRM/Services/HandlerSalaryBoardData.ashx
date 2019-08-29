<%@ WebHandler Language="C#" Class="Web.HRM.Services.HandlerSalaryBoardDyamic" %>

using System;
using System.Web;
using System.IO;
using System.Collections.Generic;
using Web.Core.Service.HumanRecord;
using Web.Core.Service.Salary;

namespace Web.HRM.Services
{
    public class HandlerSalaryBoardDyamic : Core.Framework.BaseControl.BaseHandler, IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json; charset=UTF-8";
            context.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            context.Response.AppendHeader("Access-Control-Allow-Credentials", "true");

            using (var reader = new StreamReader(context.Request.InputStream))
            {
                var values = reader.ReadToEnd();
                if (!string.IsNullOrEmpty(values))
                {
                    var json = Ext.Net.JSON.Deserialize<Dictionary<string, object>>(values);
                    var salaryBoardId = json["SalaryBoardId"].ToString();
                    var salaryBoardList = sal_PayrollServices.GetById(Convert.ToInt32(salaryBoardId));
                    if (salaryBoardList != null)
                    {
                        salaryBoardList.Data = Ext.Net.JSON.Serialize(json["Data"]);
                        sal_PayrollServices.Update(salaryBoardList);
                    }
                    context.Response.Write(Ext.Net.JSON.Serialize(json["Data"]));
                }
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
