using System;
using Web.Core;
using Web.Core.Framework;

namespace Web.HJM.Modules.Catalog
{

    public partial class CatalogManagement : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var id = Request.QueryString["id"];
            var title = Request.QueryString["title"];
            var gridPanel = (BaseGridTable)LoadControl("~/Modules/Base/GridPanel/GridPanel.ascx");
            gridPanel.ID = id;
            gridPanel.IsPrimaryKeyAutoIncrement = true;
            gridPanel.AccessHistory = new WebUI.Entity.AccessHistory()
            {
                ModuleDescription = "Quản lý {0}".FormatWith(title),
                Insert_AccessHistoryDescription = "Thêm mới {0}".FormatWith(title),
                Update_AccessHistoryDescription = "Cập nhật thông tin {0}".FormatWith(title),
                Delete_AccessHistoryDescription = "Xóa {0}".FormatWith(title),
            };
            plhGridPanel.Controls.Add(gridPanel);
        }
    }
}