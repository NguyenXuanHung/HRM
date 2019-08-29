using System;
using System.Linq;
using Ext.Net;
using Web.Core.Framework;

namespace Web.HJM.Modules.UserControl
{
    public partial class Modules_Base_DateTime_DateTime : BaseUserControl, IBaseControl
    {
        public int Width { get; set; }
        public string FieldLabel { get; set; }
        public bool Disable { get; set; }
        public bool DisplayToday { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                spinnerHours.Text = DateTime.Now.Hour.ToString();
                spinnerMin.Text = DateTime.Now.Minute.ToString();
                if (DisplayToday)
                    DateField1.SelectedDate = DateTime.Today;
                if (Width != 0)
                {
                    DateField1.Width = Width;
                }
                else
                {
                    DateField1.AnchorHorizontal = "100%";
                }

                DateField1.FieldLabel = FieldLabel;
                DateField1.Disabled = this.Disable;
                SetJs();
            }
        }

        /// <summary>
        /// Thiết lập Listener cho DateField
        /// </summary>
        private void SetJs()
        {
            DateField1.Listeners.Blur.Handler =
                hdfTimeValue.ClientID + ".setValue(" + DateField1.ClientID + ".getValue());";
            DateField1.Listeners.TriggerClick.Handler = wdInputTime.ClientID + ".show();";
        }

        public string GetID()
        {
            return this.ID;
        }

        public object GetValue()
        {
            try
            {
                int h = 12;
                int m = 0;
                if (!string.IsNullOrEmpty(hdfTimeValue.Text))
                {
                    //Tue Dec 25 2012 05:00:00 GMT+0700 (SE Asia Standard Time)
                    string[] time = { };
                    int index = 0;
                    char[] s = hdfTimeValue.Text.ToCharArray();
                    for (int i = 0; i < s.Length; i++)
                    {
                        if (index == 4)
                        {
                            time = hdfTimeValue.Text.Substring(i, 8).Split(':');
                            break;
                        }

                        if (s[i] == ' ')
                        {
                            index++;
                        }
                    }

                    if (time.Count() > 0)
                    {
                        h = int.Parse(time[0].Replace(".", ""));
                        m = int.Parse(time[1].Replace(".", ""));
                    }
                }

                DateTime date = new DateTime(DateField1.SelectedDate.Year, DateField1.SelectedDate.Month,
                    DateField1.SelectedDate.Day, h, m, 0);
                return date;
            }
            catch (Exception ex)
            {
                Dialog.ShowNotification("Datetime.ascx/GetValue() " + ex.Message);
            }

            return DateTime.Now;
        }

        public void SetValue(object value)
        {
            if (value == null)
            {
                DateField1.Text = "";
                return;
            }

            DateTime date = (DateTime) value;
            string h = date.Hour < 10 ? "0" + date.Hour : date.Hour.ToString();
            string m = date.Minute < 10 ? "0" + date.Minute : date.Minute.ToString();
            string time = h + ":" + m + ":00";
            DateField1.SelectedDate = date;
            DateField1.Text = DateField1.Text.Trim().Remove(10) + " " + time;
            hdfTimeValue.Text = ". . . . ." + time;

        }
    }
}