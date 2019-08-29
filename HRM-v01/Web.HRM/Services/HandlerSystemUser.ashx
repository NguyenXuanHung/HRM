<%@ WebHandler Language="C#" Class="Web.HRM.Services.HandlerSystemUser" %>

using System;
using System.Web;
using System.Collections.Generic;
using Web.Core;
using Web.Core.Framework.Common;
using Web.Core.Object.Security;
using Web.Core.Service.Security;

namespace Web.HRM.Services
{
    public class HandlerSystemUser : Core.Framework.BaseControl.BaseHandler, IHttpHandler
    {
        private int _start = Constant.DefaultStart;
        private int _limit = Constant.DefaultPagesize;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            var action = context.Request["action"];
            PageResult<User> users;
            switch(action)
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private PageResult<User> GetUsersForRole(HttpContext context)
        {
            // init keyword
            var keyword = context.Request["keyword"];

            // init role
            int? roleId = null;
            if(!string.IsNullOrEmpty(context.Request["roleid"]) && Convert.ToInt32(context.Request["roleid"]) > 0)
            {
                roleId = Convert.ToInt32(context.Request["roleid"]);
            }

            // init departments
            var departments = context.Request["departments"];            
            
            if(!string.IsNullOrEmpty(context.Request["start"]))
            {
                _start = int.Parse(context.Request["start"]);
            }            
            
            if(!string.IsNullOrEmpty(context.Request["limit"]))
            {
                _limit = int.Parse(context.Request["limit"]);
            }


            // TODO : change using controller
            return UserServices.GetPaging(keyword, null, false, null, false, roleId, departments, null, _start, _limit);
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