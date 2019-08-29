using System;
using System.Web;
using Web.Core.Framework;

namespace Web.HJM
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            try
            {
                #region Schedule;

                var mainScheduler = new MainScheduler();
                mainScheduler.Run();

                #endregion
            }
            catch
            {
                // do nothing
            }
        }
    }
}