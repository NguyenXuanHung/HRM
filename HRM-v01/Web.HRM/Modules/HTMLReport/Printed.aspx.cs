using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.HRM.Modules.HTMLReport
{
    public partial class Printed : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["report"] != null)
            {
                printable.InnerHtml = Session["report"].ToString();
            }
        }
    }

}

