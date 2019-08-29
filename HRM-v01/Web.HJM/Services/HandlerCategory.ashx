<%@ WebHandler Language="C#" Class="Web.HJM.Services.HandlerCategory" %>

using System;
using System.Collections.Generic;
using System.Web;
using Web.Core;
using Web.Core.Framework;
using Web.Core.Framework.Controller;
using Web.Core.Framework.Model;
using Web.Core.Object;

namespace Web.HJM.Services
{
    public class HandlerCategory : IHttpHandler
    {
        // init default value
        private int _start = Constants.DEFAULT_START;
        private int _limit = Constants.DEFAULT_PAGESIZE;
        private string _keyword = string.Empty;
        private string _type = string.Empty;
        private string _order = string.Empty;

        public void ProcessRequest(HttpContext context)
        {
            // init page result
            var pageResult = new PageResult<CategoryModel>(0, new List<CategoryModel>());
            try
            {
                // parse params value from query string
                if(!string.IsNullOrEmpty(context.Request["query"]))
                {
                    _keyword = context.Request["query"];
                }
                if(!string.IsNullOrEmpty(context.Request["type"]))
                {
                    _type = context.Request["type"];
                }
                if(!string.IsNullOrEmpty(context.Request["order"]))
                {
                    _order = context.Request["order"];
                }
                if(!string.IsNullOrEmpty(context.Request["start"]))
                {
                    _start = int.Parse(context.Request["start"]);
                }
                if(!string.IsNullOrEmpty(context.Request["limit"]))
                {
                    _limit = int.Parse(context.Request["limit"]);
                }
              
                // select from database
                pageResult = CategoryController.GetPaging(_keyword, null, null, _type, false, _order, _start, _limit);
            }
            catch(Exception exception)
            {
                Dialog.ShowError(exception);
            }
            finally
            {
                // response
                context.Response.ContentType = "text/json";
                context.Response.Write("{{TotalRecords:{0},Data:{1}}}".FormatWith(pageResult.Total, Ext.Net.JSON.Serialize(pageResult.Data)));
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