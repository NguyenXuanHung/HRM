using System.Collections.Generic;
using System.Linq;
using Web.Core.Object.Security;
using Web.Core.Service.Security;

namespace Web.Core.Framework
{
    public class MenuController
    {
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static MenuModel GetById(int id)
        {
            var entity = MenuServices.GetById(id);
            return entity != null ? new MenuModel(entity) : null;
        }

        /// <summary>
        /// Get by url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static MenuModel GetByUrl(string url)
        {
            // check url
            if (string.IsNullOrEmpty(url)) return null;

            // init condition
            var condition = "[LinkUrl]='{0}'".FormatWith(url.TrimStart('/').EscapeQuote());

            // get by condition
            var entity = MenuServices.GetByCondition(condition);

            // return
            return entity != null ? new MenuModel(entity) : null;
        }

        /// <summary>
        /// Get all by condition
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="parentId"></param>
        /// <param name="isPanel"></param>
        /// <param name="group"></param>
        /// <param name="status"></param>
        /// <param name="userId"></param>
        /// <param name="isDeleted"></param>
        /// <param name="order"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<MenuModel> GetAll(string keyword, int? parentId, bool? isPanel, MenuGroup? group, MenuStatus? status, int? userId,
            bool? isDeleted, string order, int? limit)
        {
            // init condition
            var condition = "1=1";
            
            // keyword
            if (!string.IsNullOrEmpty(keyword))
                condition += " AND ([MenuName] LIKE N'%{0}%' OR [TabName] LIKE N'%{0}%')".FormatWith(keyword.EscapeQuote());

            // parent id
            if(parentId != null)
                condition += " AND [ParentId]={0}".FormatWith(parentId.Value);

            // is panel
            if(isPanel != null)
                condition += " AND [IsPanel]={0}".FormatWith(isPanel.Value ? 1 : 0);

            // group
            if(group != null)
                condition += " AND [Group]={0}".FormatWith((int)group.Value);

            // status
            if(status != null)
                condition += " AND [Status]={0}".FormatWith((int)status.Value);

            // userId
            if(userId != null)
                condition += @" AND [Id] IN (" +
                             "      SELECT [MenuId] FROM [MenuRole] WHERE [RoleId] IN (" +
                             "          SELECT [RoleId] FROM [UserRole] WHERE [UserId]='{0}'))".FormatWith(userId.Value);

            // deleted
            if(isDeleted != null)
                condition += " AND [IsDeleted]={0}".FormatWith(isDeleted.Value ? 1 : 0);

            // order
            if(string.IsNullOrEmpty(order))
                order = @"[Order]";

            // return
            return MenuServices.GetAll(condition, order, limit).Select(e => new MenuModel(e)).ToList();
        }

        /// <summary>
        /// Get all in tree view
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="parentId"></param>
        /// <param name="isPanel"></param>
        /// <param name="group"></param>
        /// <param name="status"></param>
        /// <param name="isDeleted"></param>
        /// <returns></returns>
        public static List<MenuModel> GetTree(string keyword, int? parentId, bool? isPanel, MenuGroup? group, MenuStatus? status, bool? isDeleted)
        {
            // get all menu
            var allMenus = GetAll(keyword, parentId, isPanel, group, status, null, isDeleted, null, null);

            // init menu tree
            var treeMenus = new List<MenuModel>();

            // get root menu
            var rootMenus = allMenus.Where(m => m.ParentId == 0).OrderBy(m => m.Order);

            // loop
            foreach (var rootMenu in rootMenus)
            {
                // add into menu tree
                treeMenus.Add(rootMenu);

                // get child menu
                var childMenus = allMenus.Where(m => m.ParentId == rootMenu.Id).OrderBy(m => m.Order);

                // loop
                foreach (var childMenu in childMenus)
                {
                    // populate child menu
                    PopulateChildMenu(allMenus, childMenu, 1, ref treeMenus);
                }
            }

            // return
            return treeMenus;
        }

        /// <summary>
        /// Get paging by condition
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="parentId"></param>
        /// <param name="isPanel"></param>
        /// <param name="group"></param>
        /// <param name="status"></param>
        /// <param name="userId"></param>
        /// <param name="isDeleted"></param>
        /// <param name="order"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static PageResult<MenuModel> GetPaging(string keyword, int? parentId, bool? isPanel, MenuGroup? group, MenuStatus? status, int? userId,
            bool? isDeleted, string order, int start, int limit)
        {
            // init condition
            var condition = "1=1";

            // keyword
            if(!string.IsNullOrEmpty(keyword))
                condition += " AND ([MenuName] LIKE N'%{0}%' OR [TabName] LIKE N'%{0}%')".FormatWith(keyword.EscapeQuote());

            // parent id
            if(parentId != null)
                condition += " AND [ParentId]={0}".FormatWith(parentId.Value);

            // is panel
            if(isPanel != null)
                condition += " AND [IsPanel]={0}".FormatWith(isPanel.Value ? 1 : 0);

            // group
            if(group != null)
                condition += " AND [Group]={0}".FormatWith((int)group.Value);

            // status
            if(status != null)
                condition += " AND [Status]={0}".FormatWith((int)status.Value);

            // userId
            if(userId != null)
                condition += @" AND [Id] IN (" +
                             "      SELECT [MenuId] FROM [MenuRole] WHERE [RoleId] IN (" +
                             "          SELECT [RoleId] FROM [UserRole] WHERE [UserId]='{0}'))".FormatWith(userId.Value);

            // deleted
            if(isDeleted != null)
                condition += " AND [IsDeleted]={0}".FormatWith(isDeleted.Value ? 1 : 0);

            // get result
            var result = MenuServices.GetPaging(condition, order, start, limit);

            // return
            return new PageResult<MenuModel>(result.Total, result.Data.Select(r => new MenuModel(r)).ToList());
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static MenuModel Create(MenuModel model)
        {
            var entity = new Menu();
            model.FillEntity(ref entity);
            return new MenuModel(MenuServices.Create(entity));
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static MenuModel Update(MenuModel model)
        {
            var entity = new Menu();
            model.FillEntity(ref entity);
            return new MenuModel(MenuServices.Update(entity));
        }

        /// <summary>
        /// Update deleted status
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static MenuModel Delete(int id)
        {
            var model = GetById(id);
            if(model != null)
            {
                model.IsDeleted = true;
                return Update(model);
            }
            return null;
        }

        #region Private Methods

        /// <summary>
        /// Populate child menu
        /// </summary>
        /// <param name="allMenus"></param>
        /// <param name="menu"></param>
        /// <param name="level"></param>
        /// <param name="treeMenus"></param>
        private static void PopulateChildMenu(List<MenuModel> allMenus, MenuModel menu, int level, ref List<MenuModel> treeMenus)
        {
            // init prefix
            var prefix = string.Empty;

            // set prefix value
            for (var levelCount = 1; levelCount <= level; levelCount++)
            {
                prefix += "+---";
            }

            // set display name
            menu.DisplayName = "{0}{1}".FormatWith(prefix, menu.MenuName);

            // add into menu tree
            treeMenus.Add(menu);

            // get child
            var childMenus = allMenus.Where(m => m.ParentId == menu.Id).OrderBy(m => m.Order);

            // loop
            foreach (var childMenu in childMenus)
            {
                PopulateChildMenu(allMenus, childMenu, level + 1, ref treeMenus);
            }
        }

        #endregion
    }
}
