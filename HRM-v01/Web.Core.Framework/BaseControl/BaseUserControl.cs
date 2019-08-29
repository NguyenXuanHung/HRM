using System;
using System.Web.UI;

namespace Web.Core.Framework
{
    public class BaseUserControl : UserControl
    {
        public UserModel CurrentUser;

        protected override void OnLoad(EventArgs e)
        {
            // check session current user
            if (Session["CurrentUser"] == null)
                Response.Redirect(Resource.Get("LoginUrl"), true);

            // init current user
            CurrentUser = (UserModel)Session["CurrentUser"];

            // base load
            base.OnLoad(e);
        }
    }
}
