using System;
using System.Linq;
using Highcharts.Core;
using Highcharts.Core.Data.Chart;
using System.Collections.ObjectModel;
using System.Data;
using Highcharts.Core.PlotOptions;
using Ext.Net;
using Web.Core;
using Web.Core.Framework;
using Web.Core.Framework.Adapter;
using Web.Core.Framework.SQLAdapter;

namespace Web.HJM.Modules.Home.Chart
{
    public partial class ColumnChart : BasePage
    {
        private bool displayPercentage = true;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                string h = Request.QueryString["height"];
                int height = (h == null) ? 250 : Int32.Parse(h);
                string macb = Request.QueryString["id"];
                int thang = int.Parse(Request.QueryString["thang"] ?? "0");
                int nam = int.Parse(Request.QueryString["nam"] ?? "0");

                switch (Request.QueryString["type"])
                {
                    case "NSDonVi":
                        GenerateNhanSuTheoDonVi(height);
                        break;

                    case "Age":
                        AgeChart(height);
                        break;

                    default:
                        break;
                }
            }
        }

        private void GenerateNhanSuTheoDonVi(int height)
        {
            try
            {
                hcFrutas.Title = new Title(@"Thống kê theo đơn vị");
                hcFrutas.Height = height;
                //Danh sách giới tinh theo đơn vị
                var lstDepartment = CurrentUser.Departments.Where(d => d.IsLocked == false).ToList();
                var lstRecord = RecordController.GetAll(string.Empty);
                var lstLabel = new object[lstDepartment.Count];
                var lstMale = new object[lstDepartment.Count];
                var lstFemale = new object[lstDepartment.Count];

                for (var i = 0; i < lstDepartment.Count; i++)
                {
                    var countOfMale = lstRecord.Where(hr => hr.DepartmentId == lstDepartment[i].Id && hr.Sex == true).Count();
                    var countOfFemale = lstRecord.Where(hr => hr.DepartmentId == lstDepartment[i].Id && hr.Sex == false).Count();
                    lstLabel[i] = lstDepartment[i].Name;
                    lstMale[i] = countOfMale;
                    lstFemale[i] = countOfFemale;
                }


                //definições de eixos
                hcFrutas.YAxis.Add(new YAxisItem { title = new Title("Số lượng") });
                hcFrutas.XAxis.Add(new XAxisItem { categories = lstLabel });

                //dados
                var series = new Collection<Serie>();

                series.Add(new Serie { name = "Nam", data = lstMale });
                series.Add(new Serie { name = "Nữ", data = lstFemale });

                hcFrutas.PlotOptions = new PlotOptionsColumn()
                {
                    borderColor = "#dedede",
                    borderRadius = 4,
                    dataLabels = new DataLabels()
                    {
                        enabled = true,
                    },
                };
                hcFrutas.Legend = new Legend()
                {
                    layout = Highcharts.Core.Layout.horizontal,
                    align = Align.left,
                    verticalAlign = Highcharts.Core.VerticalAlign.top,
                    x = 70,
                    y = -5,
                    floating = true,
                    shadow = true,
                    backgroundColor = "#FFF",
                };
                hcFrutas.Exporting.enabled = true;
                hcFrutas.DataSource = series;
                hcFrutas.DataBind();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Độ tuổi
        /// </summary>
        /// <param name="height"></param>
        private void AgeChart(int height)
        {
            try
            {
                hcFrutas.Title = new Title(@"Thống kê theo độ tuổi");

                string departments = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
                var table = SQLHelper.ExecuteTable(SQLChartAdapter.GetStore_AgeChart(departments));
                int[] ageMileStone = { 0, 18, 30, 40, 50 };
                int count = ageMileStone.Length;
                var nameMileStone = new object[count];
                var ageData = new object[count];

                //Create milestone
                nameMileStone[0] = "0 - 18";
                ageData[0] = 0;
                for (var i = 1; i < count - 1; i++)
                {
                    nameMileStone[i] = ageMileStone[i] + 1 + " - " + ageMileStone[i + 1];
                    ageData[i] = 0;
                }

                nameMileStone[count - 1] = "Trên " + ageMileStone[count - 1];
                ageData[count - 1] = 0;

                foreach (DataRow item in table.Rows)
                {
                    var index = 0;
                    for (var i = 0; i < count; i++)
                    {
                        if (int.Parse(item["Age"].ToString()) >= ageMileStone[count - 1])
                        {
                            index = count - 1;
                            break;
                        }
                        if (int.Parse(item["Age"].ToString()) <= ageMileStone[i] && int.Parse(item["Age"].ToString()) >= 0)
                        {
                            if (i > 0)
                                index = i - 1;
                            break;
                        }
                    }
                    ageData[index] = (int)ageData[index] + 1;
                }

                // ve bieu do
                hcFrutas.YAxis.Add(new YAxisItem { title = new Title(@"Số lượng") });
                hcFrutas.XAxis.Add(new XAxisItem { categories = nameMileStone });

                //dados
                var series = new Collection<Serie> { new Serie { name = "Cán bộ", data = ageData } };


                hcFrutas.PlotOptions = new PlotOptionsColumn()
                {
                    borderColor = "#dedede",
                    borderRadius = 4,
                    dataLabels = new DataLabels()
                    {
                        enabled = true,
                    },
                };
                hcFrutas.Height = height;
                hcFrutas.Legend = new Legend()
                {
                    layout = Highcharts.Core.Layout.horizontal,
                    align = Align.left,
                    verticalAlign = Highcharts.Core.VerticalAlign.top,
                    x = 70,
                    y = -5,
                    floating = true,
                    shadow = true,
                    backgroundColor = "#FFF",
                    enabled = false,
                };
                hcFrutas.Exporting.enabled = true;
                hcFrutas.DataSource = series;
                hcFrutas.DataBind();
            }
            catch
            {
                throw;
            }
        }
    }
}