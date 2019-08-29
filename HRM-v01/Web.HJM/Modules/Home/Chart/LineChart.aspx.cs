using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using Highcharts.Core;
using Highcharts.Core.Data.Chart;
using Highcharts.Core.PlotOptions;
using Web.Core;
using Web.Core.Framework;
using Web.Core.Framework.Adapter;
using Web.Core.Framework.SQLAdapter;
using Web.Core.Service.Catalog;

namespace Web.Core.HRM.Modules.Home.Chart
{
    public partial class LineChart : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;
            var h = Request.QueryString["height"];
            var height = h == null ? 270 : int.Parse(h);
            if (Request.QueryString["type"] != "BDNhanSu") return;
            int year;
            var tmp = Request.QueryString["year"];
            if (tmp != null && tmp != "undefined")
            {
                year = int.Parse(tmp);
            }
            else
            {
                year = DateTime.Now.Year;
            }
            BieuDoBienDongNhanSuNew(year, height);
        }

        private void BieuDoBienDongNhanSuNew(int year, int height)
        {
            try
            {
                hcFrutas.Title = new Title("Biểu đồ biến động nhân sự {0}".FormatWith(year));
                var departments = string.Join(",", CurrentUser.Departments.Select(d => d.Id));

                int maxMonth = 12;
                if (DateTime.Now.Year == year)
                {
                    maxMonth = DateTime.Now.Month;
                }

                // ve bieu do
                hcFrutas.YAxis.Add(new YAxisItem { title = new Title(@"Số lượng") });
                hcFrutas.XAxis.Add(new XAxisItem
                {
                    categories = new object[] { "Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4",
                    "Tháng 5", "Tháng 6", "Tháng 7", "Tháng 8", "Tháng 9", "Tháng 10", "Tháng 11", "Tháng 12" }
                });

                //dados
                var series = new Collection<Serie>();

                var statusParams = cat_WorkStatusServices.GetAll();
                foreach (var item in statusParams)
                {
                    var humanResources =
                        SQLHelper.ExecuteTable(
                            SQLChartAdapter.GetStore_HumanResourceChart(departments, year, item.Id));
                    var temp = GetHumanResourceData(humanResources, item.Id, year, maxMonth, out var temName);
                    series.Add(new Serie { name = temName, data = temp });
                }

                hcFrutas.PlotOptions = new PlotOptionsLine
                {
                    dataLabels = new DataLabels
                    {
                        enabled = true
                    }
                };
                hcFrutas.Height = height;
                hcFrutas.Legend = new Legend
                {
                    layout = Layout.vertical,
                    align = Align.left,
                    verticalAlign = VerticalAlign.top,
                    x = 0,
                    y = -5,
                    floating = true,
                    shadow = true,
                    backgroundColor = "#FFF",
                    enabled = true
                };
                hcFrutas.Exporting.enabled = true;
                hcFrutas.DataSource = series;
                hcFrutas.DataBind();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private object[] GetHumanResourceData(DataTable humanResources, int id, int year, int maxMonth, out string name)
        {
            var status = cat_WorkStatusServices.GetById(id);
            name = status.Name;
            var obj = new object[maxMonth];
            for (var i = 0; i < maxMonth; i++)
            {
                obj[i] = 0;
            }
            for (var i = 0; i < humanResources.Rows.Count; i++)
            {
                if (id == 1)
                {
                    if (int.Parse(humanResources.Rows[i]["YearOfRecruiment"].ToString()) == year)
                    {
                        for (var j = int.Parse(humanResources.Rows[i]["MonthOfRecruiment"].ToString()) - 1;
                            j < maxMonth;
                            j++)
                        {
                            obj[j] = (int)obj[j] + 1;
                        }
                    }
                    else
                    {
                        for (var j = 0; j < maxMonth; j++)
                        {
                            obj[j] = (int)obj[j] + 1;
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(humanResources.Rows[i]["RecruimentDate"].ToString()))
                    {
                        if (int.Parse(humanResources.Rows[i]["YearOfRecruiment"].ToString()) == year)
                        {
                            for (var j = 0; j < int.Parse(humanResources.Rows[i]["MonthOfRecruiment"].ToString()) && j < maxMonth; j++)
                            {
                                obj[j] = (int)obj[j] + 1;
                            }
                        }
                        else
                        {
                            for (var t = 0; t < maxMonth; t++)
                            {
                                obj[t] = (int)obj[t] + 1;
                            }
                        }
                    }
                    else
                    {
                        for (var t = 0; t < maxMonth; t++)
                        {
                            obj[t] = (int)obj[t] + 1;
                        }
                    }
                }
            }
            return obj;
        }

    }
}


