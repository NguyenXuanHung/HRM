<%@ WebHandler Language="C#" Class="Web.HJM.Services.HandlerSystemUser" %>

using System;
using System.Web;
using System.Collections.Generic;
using Web.Core;
using Web.Core.Object.Security;
using Web.Core.Service.Security;

namespace Web.HJM.Services
{
    public class HandlerSystemUser : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var action = context.Request["action"];
            PageResult<User> users;
            switch (action)
            {
                case "getUsersForRole":
                    users = GetUsersForRole(context);
                    break;
                default:
                    users = new PageResult<User>(0, new List<User>());
                    break;
            }
            // write response
            context.Response.Write("{{TotalRecords:{1},'Data':{0}}}".FormatWith(Ext.Net.JSON.Serialize(users.Data), users.Total));
        }

        private PageResult<User> GetUsersForRole(HttpContext context)
        {
            // init keyword
            var keyword = context.Request["keyword"];
            // init role
            int? roleId = null;
            if (!string.IsNullOrEmpty(context.Request["roleid"]) && Convert.ToInt32(context.Request["roleid"]) > 0)
            {
                roleId = Convert.ToInt32(context.Request["roleid"]);
            }
            // init departments
            var departments = context.Request["departments"];
            // init start
            var start = 1;
            if (!string.IsNullOrEmpty(context.Request["start"]))
            {
                start = int.Parse(context.Request["start"]);
            }
            // init limit
            var limit = 25;
            if (!string.IsNullOrEmpty(context.Request["limit"]))
            {
                limit = int.Parse(context.Request["limit"]);
            }
            // get paging
            return UserServices.GetPaging(keyword, null, false, null, false, roleId, departments, null, start, limit);
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