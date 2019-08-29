<%@ WebHandler Language="C#" Class="Web.HRM.Services.HandlerMenu" %>

using System.Collections.Generic;
using System.Web;
using Web.Core;
using Web.Core.Framework;

namespace Web.HRM.Services
{
    public class HandlerMenu : Core.Framework.BaseControl.BaseHandler, IHttpHandler
    {
        // init variables      
        private string _keyword;
        private int? _parentId;
        private bool? _isPanel;
        private MenuGroup? _group;
        private MenuStatus? _status;
        private int? _userId;
        private string _returnType;
        private string _order;
        private int _start;
        private int _limit;

        public void ProcessRequest(HttpContext context)
        {
            // init paging argument
            _start = Start;
            _limit = Limit;

            // get params value
            if(!string.IsNullOrEmpty(context.Request["start"]))
            {
                _start = int.Parse(context.Request["start"]);
            }
            if(!string.IsNullOrEmpty(context.Request["limit"]))
            {
                _limit = int.Parse(context.Request["limit"]);
            }
            if(!string.IsNullOrEmpty(context.Request["query"]))
            {
                _keyword = context.Request["query"];
            }
            if(!string.IsNullOrEmpty(context.Request["parentId"]))
            {
                _parentId = int.Parse(context.Request["parentId"]);
            }
            if(!string.IsNullOrEmpty(context.Request["isPanel"]))
            {
                _isPanel = bool.Parse(context.Request["isPanel"]);
            }
            if(!string.IsNullOrEmpty(context.Request["group"]))
            {
                _group = (MenuGroup)int.Parse(context.Request["group"]);
            }
            if(!string.IsNullOrEmpty(context.Request["status"]))
            {
                _status = (MenuStatus)int.Parse(context.Request["status"]);
            }
            if(!string.IsNullOrEmpty(context.Request["userId"]))
            {
                _userId = int.Parse(context.Request["_userId"]);
            }
            if(!string.IsNullOrEmpty(context.Request["returnType"]))
            {
                _returnType = context.Request["returnType"];
            }
            if(!string.IsNullOrEmpty(context.Request["order"]))
            {
                _order = context.Request["order"];
            }

            // init page result
            var pageResult = new PageResult<MenuModel>(0, new List<MenuModel>());

            if (_returnType == "normal")
            {
                // select from db
                pageResult = MenuController.GetPaging(_keyword, _parentId, _isPanel, _group, _status, _userId, false, _order, _start, _limit);
            }

            if (_returnType == "tree")
            {
                // get menu tree
                var treeMenus = MenuController.GetTree(_keyword, _parentId, _isPanel, _group, _status, false);

                // set page result
                pageResult = new PageResult<MenuModel>(treeMenus.Count, treeMenus);
            }

            // response
            context.Response.ContentType = "text/json";
            context.Response.Write("{{TotalRecords:{0},Data:{1}}}".FormatWith(pageResult.Total, Ext.Net.JSON.Serialize(pageResult.Data)));
        }

        public bool IsReusable => false;
    }
}