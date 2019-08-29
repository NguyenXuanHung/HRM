using System;
using System.Linq;
using System.Collections.ObjectModel;
using Ext.Net;
using Highcharts.Core.Data.Chart;
using Highcharts.Core.PlotOptions;
using Highcharts.Core;
using System.Data;
using Web.Core;
using Web.Core.Framework;
using Web.Core.Framework.Adapter;
using Web.Core.Framework.SQLAdapter;
using ToolTip = Highcharts.Core.ToolTip;

namespace Web.HRM.Modules.Home.Chart
{
    public partial class QualificationsChart : BasePage
    {
        private bool displayPercentage = false;
        private int userID = -1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["userID"]))
                {
                    userID = int.Parse(Request.QueryString["userID"]);
                }
                string h = Request.QueryString["height"];
                int height = (h == null) ? 300 : Int32.Parse(h);
                string s = Request.QueryString["size"];
                int size = (s == null) ? 160 : Int32.Parse(s);
                string maDotTD = Request.QueryString["MaDotTuyenDung"];
                int maDotTuyenDung = (maDotTD == null) ? 0 : int.Parse(maDotTD);
                switch (Request.QueryString["type"])
                {
                    case "Level":
                        BieuDoTrinhDo(height, size);
                        break;
                    case "Gender":
                        Gender(height, size);
                        break;
                    case "TTHonNhan":
                        MarriageStatus(height, size);
                        break;
                    case "TonGiao":
                        Religion(height, size);
                        break;
                    case "DanToc":
                        Ethnicity(height, size);
                        break;
                    case "LoaiHD":
                        ContractType(height, size);
                        break;
                    case "NhanSuTheoChucVuDoan":
                        VyuPosition(height, size);
                        break;
                    case "CapBacQuanDoi":
                        ArmyLevel(height, size);
                        break;
                    case "ThongKeChucVuDang":
                        CpvPosition(height, size);
                        break;
                    case "ThongKeTheoThamNienCT":
                        Seniority(height, size);
                        break;

                    // Báo cáo cho phân hệ tuyển dụng: daibx 19/07/2013
                    case "NguonUngVien":
                        TuyenDung_ThongKeUngVienTheoNguon(height, size, maDotTuyenDung);
                        break;
                    case "TyLeTruot":
                        TuyenDung_ThongKeUngVienTruot(height, size, maDotTuyenDung);
                        break;
                }
            }
        }

        /// <summary>
        /// Chức vụ đảng
        /// </summary>
        /// <param name="height"></param>
        /// <param name="size"></param>
        private void CpvPosition(int height, int size)
        {
            string departments = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
            var table = SQLHelper.ExecuteTable(SQLChartAdapter.GetStore_CPVPositionChart(departments));
            int total = 0, tong = 0, k = 0;
            foreach (DataRow item in table.Rows)
            {
                if (int.Parse(item["Count"].ToString()) > 0)
                {
                    total += int.Parse(item["Count"].ToString());
                    tong++;
                }
            }
            object[] data = new object[tong];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                var percentage = (float.Parse(table.Rows[i]["Count"].ToString()) / (float)total) * 100;
                if (displayPercentage)
                {
                    data[k++] = new object[] { table.Rows[i]["CPVPositionName"].ToString() + " (" + table.Rows[i]["Count"] + " người, chiếm " + percentage + " %)", percentage };
                }
                else
                {
                    data[k++] = new object[] { table.Rows[i]["CPVPositionName"].ToString() + " (" + table.Rows[i]["Count"] + ")", percentage };
                }
            }
            BindData(data, @"Thống kê theo chức vụ Đảng", height, size, true);
        }

        /// <summary>
        /// Cấp bậc quân đội
        /// </summary>
        /// <param name="height"></param>
        /// <param name="size"></param>
        private void ArmyLevel(int height, int size)
        {
            string departments = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
            //DataTable table = DataHandler.GetInstance().ExecuteDataTable("BieuDo_ThongKeNhanSuTheoCapBacQuanDoi", "@MaDonVi", "@UserID", "@MenuID", departments, userID, menuID);
            var table = SQLHelper.ExecuteTable(SQLChartAdapter.GetStore_ArmyLevelChart(departments));
            int total = 0, tong = 0, k = 0;
            foreach (DataRow item in table.Rows)
            {
                if (int.Parse(item["Count"].ToString()) > 0)
                {
                    total += int.Parse(item["Count"].ToString());
                    tong++;
                }
            }
            object[] data = new object[tong];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                var percentage = (float.Parse(table.Rows[i]["Count"].ToString()) / (float)total) * 100;
                if (displayPercentage)
                {
                    data[k++] = new object[] { table.Rows[i]["ArmyLevelName"].ToString() + " (" + table.Rows[i]["Count"] + " người, chiếm " + percentage + " %)", percentage };
                }
                else
                {
                    data[k++] = new object[] { table.Rows[i]["ArmyLevelName"].ToString() + " (" + table.Rows[i]["Count"] + ")", percentage };
                }
            }
            BindData(data, @"Thống kê theo cấp bậc quân đội", height, size, true);
        }

        /// <summary>
        /// Chức vụ đoàn
        /// </summary>
        /// <param name="height"></param>
        /// <param name="size"></param>
        private void VyuPosition(int height, int size)
        {
            string departments = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
            var table = SQLHelper.ExecuteTable(SQLChartAdapter.GetStore_VYUPositionChart(departments));
            int total = 0, tong = 0, k = 0;
            foreach (DataRow item in table.Rows)
            {
                if (int.Parse(item["Count"].ToString()) > 0)
                {
                    total += int.Parse(item["Count"].ToString());
                    tong++;
                }
            }
            object[] data = new object[tong];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (int.Parse(table.Rows[i]["Count"].ToString()) > 0)
                {
                    var percentage = (float.Parse(table.Rows[i]["Count"].ToString()) / (float)total) * 100;
                    if (displayPercentage)
                    {
                        data[k++] = new object[] { table.Rows[i]["VYUPositionName"].ToString() + " (" + table.Rows[i]["Count"] + " người, chiếm " + percentage + " %)", percentage };
                    }
                    else
                    {
                        data[k++] = new object[] { table.Rows[i]["VYUPositionName"].ToString() + " (" + table.Rows[i]["Count"] + ")", percentage };
                    }
                }
            }
            BindData(data, @"Thống kê theo chức vụ Đoàn", height, size, true);
        }

        /// <summary>
        /// Tình trạng hôn nhân
        /// </summary>
        /// <param name="height"></param>
        /// <param name="size"></param>
        private void MarriageStatus(int height, int size)
        {
            var departments = string.Join(",", CurrentUser.Departments.Select(d => d.Id));

            //DataTable table = DataHandler.GetInstance().ExecuteDataTable("BieuDo_ThongKeNhanSuTheoTinhTrangHonNhan", "@MaDonVi", "@UserID", "@MenuID", maDV, userID, menuID);
            var table = SQLHelper.ExecuteTable(SQLChartAdapter.GetStore_MarriageStatusChart(departments));
            int total = 0, tong = 0, k = 0;
            foreach (DataRow item in table.Rows)
            {
                if (int.Parse(item["Count"].ToString()) <= 0) continue;
                total += int.Parse(item["Count"].ToString());
                tong++;
            }
            var data = new object[tong];
            for (var i = 0; i < table.Rows.Count; i++)
            {
                if (int.Parse(table.Rows[i]["Count"].ToString()) <= 0) continue;
                var percentage = float.Parse(table.Rows[i]["Count"].ToString()) / total * 100;
                if (displayPercentage)
                {
                    data[k++] = new object[] { table.Rows[i]["MaritalStatusName"] + " (" + table.Rows[i]["Count"] + " người, chiếm " + percentage + " %)", percentage };
                }
                else
                {
                    data[k++] = new object[] { table.Rows[i]["MaritalStatusName"] + " (" + table.Rows[i]["Count"] + ")", percentage };
                }
            }
            BindData(data, @"Thống kê theo tình trạng hôn nhân", height, size, true);
        }

        /// <summary>
        /// Tôn giáo
        /// </summary>
        /// <param name="height"></param>
        /// <param name="size"></param>
        private void Religion(int height, int size)
        {
            string departments = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
            // DataTable table = DataHandler.GetInstance().ExecuteDataTable("BieuDo_ThongKeNhanSuTheoTonGiao", "@MaDonVi", "@UserID", "@MenuID", maDV, userID, menuID);
            var table = SQLHelper.ExecuteTable(SQLChartAdapter.GetStore_ReligionChart(departments));
            int total = 0, tong = 0, k = 0;
            foreach (DataRow item in table.Rows)
            {
                if (int.Parse(item["Count"].ToString()) > 0)
                {
                    total += int.Parse(item["Count"].ToString());
                    tong++;
                }
            }
            object[] data = new object[tong];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (int.Parse(table.Rows[i]["Count"].ToString()) > 0)
                {
                    var percentage = (float.Parse(table.Rows[i]["Count"].ToString()) / (float)total) * 100;
                    if (displayPercentage)
                    {
                        data[k++] = new object[] { table.Rows[i]["ReligionName"].ToString() + " (" + table.Rows[i]["Count"] + " người, chiếm " + percentage + " %)", percentage };
                    }
                    else
                    {
                        data[k++] = new object[] { table.Rows[i]["ReligionName"].ToString() + " (" + table.Rows[i]["Count"] + ")", percentage };
                    }
                }
            }
            BindData(data, @"Thống kê theo tôn giáo", height, size, true);
        }

        /// <summary>
        /// Dân tộc
        /// </summary>
        /// <param name="height"></param>
        /// <param name="size"></param>
        private void Ethnicity(int height, int size)
        {
            string departments = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
            //DataTable table = DataHandler.GetInstance().ExecuteDataTable("BieuDo_ThongKeTheoDanToc", "@MaDonVi", "@UserID", "@MenuID", maDV, userID, menuID);
            var table = SQLHelper.ExecuteTable(SQLChartAdapter.GetStore_FolkChart(departments));
            int total = 0, tong = 0, k = 0;
            foreach (DataRow item in table.Rows)
            {
                if (int.Parse(item["Count"].ToString()) > 0)
                {
                    total += int.Parse(item["Count"].ToString());
                    tong++;
                }
            }
            object[] data = new object[tong];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (int.Parse(table.Rows[i]["Count"].ToString()) > 0)
                {
                    var percentage = (float.Parse(table.Rows[i]["Count"].ToString()) / (float)total) * 100;
                    if (displayPercentage)
                    {
                        data[k++] = new object[] { table.Rows[i]["FolkName"].ToString() + " (" + table.Rows[i]["Count"] + " người, chiếm " + percentage + " %)", percentage };
                    }
                    else
                    {
                        data[k++] = new object[] { table.Rows[i]["FolkName"].ToString() + " (" + table.Rows[i]["Count"] + ")", percentage };
                    }
                }
            }
            BindData(data, @"Thống kê theo dân tộc", height, size, true);
        }

        /// <summary>
        /// Loại hợp đồng
        /// </summary>
        /// <param name="height"></param>
        /// <param name="size"></param>
        private void ContractType(int height, int size)
        {
            string departments = string.Join(",", CurrentUser.Departments.Select(d => d.Id));

            //DataTable table = DataHandler.GetInstance().ExecuteDataTable("BieuDo_ThongKeNhanSuTheoLoaiHopDong", "@MaDonVi", "@UserID", "@MenuID", departments, userID, menuID);
            var table = SQLHelper.ExecuteTable(SQLChartAdapter.GetStore_ContractTypeChart(departments));
            int total = 0, tong = 0, k = 0;
            foreach (DataRow item in table.Rows)
            {
                if (int.Parse(item["Count"].ToString()) > 0)
                {
                    total += int.Parse(item["Count"].ToString());
                    tong++;
                }
            }
            object[] data = new object[tong];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (int.Parse(table.Rows[i]["Count"].ToString()) > 0)
                {
                    var percentage = (float.Parse(table.Rows[i]["Count"].ToString()) / (float)total) * 100;
                    if (displayPercentage)
                    {
                        data[k++] = new object[] { table.Rows[i]["ContractTypeName"].ToString() + " (" + table.Rows[i]["Count"] + " người, chiếm " + percentage + " %)", percentage };
                    }
                    else
                    {
                        data[k++] = new object[] { table.Rows[i]["ContractTypeName"] + " (" + table.Rows[i]["Count"] + ")", percentage };
                    }
                }
            }
            BindData(data, @"Thống kê theo loại hợp đồng", height, size, true);
        }

        /// <summary>
        /// Bind biểu đồ trình độ
        /// </summary>
        private void BieuDoTrinhDo(int height, int size)
        {
                string departments = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
                // DataTable table = DataHandler.GetInstance().ExecuteDataTable("BieuDo_ThongKeNhanSuTheoTrinhDo", "@MaDonVi", "@UserID", "@MenuID", maDV, userID, menuID);
                var table = SQLHelper.ExecuteTable(SQLChartAdapter.GetStore_EducationChart(departments));

                int total = 0, max = 0, k = 0;
                foreach (DataRow item in table.Rows)
                {
                    if (int.Parse(item["Count"].ToString()) > 0)
                    {
                        total += int.Parse(item["Count"].ToString());
                        max++;
                    }
                }
                object[] data = new object[max];
            for (int i = 0; i < table.Rows.Count; i++)
                {
                    if (int.Parse(table.Rows[i]["Count"].ToString()) > 0)
                    {
                        var percentage = (float.Parse(table.Rows[i]["Count"].ToString()) / (float)total) * 100;
                        if (displayPercentage)
                        {
                            data[k++] = new object[] { table.Rows[i]["EducationName"].ToString() + " (" + table.Rows[i]["Count"] + " người, chiếm " + percentage + " %)", percentage };
                        }
                        else
                        {
                            data[k++] = new object[] { table.Rows[i]["EducationName"].ToString() + " (" + table.Rows[i]["Count"] + ")", percentage };
                        }
                    }
                }
                BindData(data, @"Thống kê theo trình độ", height, size, true);
        }

        /// <summary>
        /// Bind Biểu đồ giới tính
        /// </summary>
        private void Gender(int height, int size)
        {
            // lay id don vi
            string departments = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
            float percentageMale = 0;
            float percentageFemale = 0;
            GetGenderNumber(out var male, out var female, departments);
            float nam = ((float)male / (float)(male + female)) * 100;
            percentageMale = ((float)male / ((float)male + (float)female)) * 100;
            percentageFemale = (100 - percentageMale);
            if (displayPercentage)
            {
                object[] data = new object[]
                {
                    new object[]{"Nam" + " (" + male + " người, chiếm "+percentageMale+" % )",nam},
                    new object[]{"Nữ" + " (" + female + " người, chiếm "+percentageFemale+" % )",100-nam},
                };
                BindData(data, @"Thống kê theo giới tính", height, size, true);
            }
            else
            {
                object[] data = new object[]
                {
                    new object[]{"Nam" + " (" + male + ")",nam},
                    new object[]{"Nữ" + " (" + female + ")",100-nam},
                };
                BindData(data, @"Thống kê theo giới tính", height, size, true);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="male"></param>
        /// <param name="female"></param>
        /// <param name="departments"></param>
        private void GetGenderNumber(out int male, out int female, string departments)
        {
            var maleCount = SQLHelper.ExecuteScalar(SQLChartAdapter.GetStore_CountMaleRecordChart(departments));
            male = (int)maleCount;

            var femaleCount = SQLHelper.ExecuteScalar(SQLChartAdapter.GetStore_CountFemaleRecordChart(departments));
            female = (int)femaleCount;
        }

        /// <summary>
        /// Thâm niên
        /// </summary>
        /// <param name="height"></param>
        /// <param name="size"></param>
        private void Seniority(int height, int size)
        {
            string departments = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
            var table = SQLHelper.ExecuteTable(SQLChartAdapter.GetStore_Seniority(departments));
            int[] mocTn = { 0, 5, 10 };
            int count = mocTn.Length;
            object[] tenMoc = new object[count + 1];
            int[] data = new int[count + 1];

            // create MocTN[]
            tenMoc[0] = "Dưới " + mocTn[1] + " năm";
            data[0] = 0;
            for (int i = 1; i < count - 1; i++)
            {
                tenMoc[i] = mocTn[i] + "-" + mocTn[i + 1] + " năm";
                data[i] = 0;
            }
            tenMoc[count - 1] = "Trên " + mocTn[count - 1] + " năm";
            data[count - 1] = 0;
            tenMoc[count] = "Không xác định";
            data[count] = 0;
            int total = 0;

            foreach (DataRow item in table.Rows)
            {
                int index = 0;
                for (int i = 0; i < count; i++)
                {
                    if (int.Parse(item["years"].ToString()) == -1)
                    {
                        index = count;
                        break;
                    }
                    if (int.Parse(item["years"].ToString()) >= mocTn[count - 1])
                    {
                        index = count - 1;
                        break;
                    }
                    if (int.Parse(item["years"].ToString()) < mocTn[i] && int.Parse(item["years"].ToString()) >= 0)
                    {
                        if (i > 0)
                            index = i - 1;
                        break;
                    }
                }
                total++;
                data[index] = data[index] + 1;
            }

            int sz = 0;
            for (int j = 0; j < data.Count(); j++)
                if (data[j] > 0)
                    sz++;

            object[] obj = new object[sz];
            int k = 0;
            for (int i = 0; i <= count; i++)
            {
                if (data[i] > 0)
                    obj[k++] = new object[] { tenMoc[i] + " (" + data[i] + ")", ((float)data[i]) / ((float)total) * 100 };
            }

            BindData(obj, @"Thống kê theo thâm niên", height, size, true);

        }
        
        /// <summary>
        /// Biểu đồ: Báo cáo kết quả tuyển dụng 
        /// </summary>
        /// <param name="height"></param>
        /// <param name="size"></param>
        /// <param name="maDotTuyenDung"></param>
        private void TuyenDung_ThongKeUngVienTheoNguon(int height, int size, int maDotTuyenDung)
        {
            object[] data = GetData(maDotTuyenDung);
            BindData(data, "Thống kê ứng viên theo nguồn", height, size, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="height"></param>
        /// <param name="size"></param>
        /// <param name="maDotTuyenDung"></param>
        private void TuyenDung_ThongKeUngVienTruot(int height, int size, int maDotTuyenDung)
        {
            object[] data = GetDataTyLeDoTruot(maDotTuyenDung);
            BindData(data, "Thống kê tỷ lệ ứng viên trượt", height, size, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="maDotTuyenDung"></param>
        /// <returns></returns>
        private object[] GetDataTyLeDoTruot(int maDotTuyenDung)
        {
            try
            {
                switch (maDotTuyenDung)
                {
                    case 70:
                        object[] data1 = new object[3];
                        data1[0] = new object[] { "Trượt (50)", (float)50 * 100 / 100 };
                        data1[1] = new object[] { "Qua vòng sơ loại (50)", (float)50 * 100 / 100 };
                        return data1;
                    case 69:
                        object[] data2 = new object[2];
                        data2[0] = new object[] { "Trượt (22)", (float)22 * 100 / 37 };
                        data2[1] = new object[] { "Qua vòng sơ loại (15)", (float)15 * 100 / 37 };
                        return data2;
                    case 71:
                        object[] data3 = new object[2];
                        data3[0] = new object[] { "Trượt (6)", (float)6 * 100 / 20 };
                        data3[1] = new object[] { "Qua vòng sơ loại (14)", (float)14 * 100 / 20 };
                        return data3;
                    case 72:
                        object[] data4 = new object[2];
                        data4[0] = new object[] { "Trượt (9)", (float)9 * 100 / 36 };
                        data4[1] = new object[] { "Qua vòng sơ loại (27)", (float)27 * 100 / 36 };
                        return data4;
                    default:
                        return new object[0];
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="maDotTuyenDung"></param>
        /// <returns></returns>
        private object[] GetData(int maDotTuyenDung)
        {
            try
            {
                switch (maDotTuyenDung)
                {
                    case 70:
                        object[] data1 = new object[3];
                        data1[0] = new object[] { "Website của công ty (6)", (float)6 * 100 / 15 };
                        data1[1] = new object[] { "Các mối quan hệ (3)", (float)3 * 100 / 15 };
                        data1[2] = new object[] { "Vieclam24h.vn (6)", (float)6 * 100 / 15 };
                        return data1;
                    case 69:
                        object[] data2 = new object[2];
                        data2[0] = new object[] { "Vietnamworks.com (3)", (float)3 * 100 / 4 };
                        data2[1] = new object[] { "Vieclam24h.vn (1)", (float)1 * 100 / 4 };
                        return data2;
                    case 71:
                        object[] data3 = new object[1];
                        data3[0] = new object[] { "Vietnamworks.com (2)", (float)2 * 100 / 2 };
                        return data3;
                    case 72:
                        object[] data4 = new object[4];
                        data4[0] = new object[] { "Vietnamworks.com (5)", (float)6 * 100 / 10 };
                        data4[1] = new object[] { "Website của công ty (1)", (float)1 * 100 / 10 };
                        data4[2] = new object[] { "Các mối quan hệ (1)", (float)1 * 100 / 10 };
                        data4[3] = new object[] { "Vieclam24h.vn (3)", (float)3 * 100 / 10 };
                        return data4;
                    default:
                        return new object[0];
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_data"></param>
        /// <param name="Title"></param>
        /// <param name="nHeight"></param>
        /// <param name="nSize"></param>
        /// <param name="showInLegend"></param>
        private void BindData(object[] _data, string Title, int nHeight, int nSize, bool showInLegend)
        {
            hcVendas.Title = new Title(Title);
            //   hcVendas.SubTitle = new SubTitle("(Chỉ thống kê theo những nhân viên đang làm việc)");
            hcVendas.Height = nHeight;
            var series = new Collection<Serie>
            {
                new Serie
                {
                    size = nSize,
                    data = _data
                }
            };

            hcVendas.PlotOptions = new PlotOptionsPie
            {
                allowPointSelect = true,
                cursor = "pointer",
                dataLabels = new DataLabels
                {
                    enabled = true,
                    align = Align.right,
                    y = 10,
                    verticalAlign = Highcharts.Core.VerticalAlign.top

                },
                animation = true,
                enableMouseTracking = true,
                showInLegend = showInLegend,
            };
            hcVendas.Legend = new Legend()
            {
                enabled = true,
                layout = Highcharts.Core.Layout.vertical,
                align = Align.left,
                verticalAlign = Highcharts.Core.VerticalAlign.top,
                x = 5,
                y = 10,
                floating = true,
                shadow = true,
                backgroundColor = "#FFF",
            };
            hcVendas.Tooltip = displayPercentage ? new ToolTip("'<b>'+ this.point.name +'</b>'") : new ToolTip("'<b>'+ this.point.name +'</b>: '+ this.y +' %'");
            hcVendas.Exporting.enabled = true;
            hcVendas.DataSource = series;
            hcVendas.DataBind();
        }
    }

}


