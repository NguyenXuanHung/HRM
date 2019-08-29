using System;
using System.Collections.Generic;
using System.Linq;
using Ext.Net;
using System.Xml.Linq;
using System.IO;
using Web.Core.Framework;
using Web.Core.Service.Catalog;
using Web.Core;
using Web.Core.Service.HumanRecord;

namespace Web.HJM.Modules.Home
{
    public partial class Default : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ExtNet.IsAjaxRequest) return;
            // departments
            hdfDepartments.Text = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
            hdfCurrentUserID.Text = CurrentUser.User.Id.ToString();
            hdfSalaryRaiseRegularType.Text = ((int) SalaryDecisionType.Regular).ToString();
            hdfSalaryRaiseOutFrameType.Text = ((int)SalaryDecisionType.OverGrade).ToString();
            // chon nam 
            cbxChonNamStore.DataSource = GetYear(string.Join(",", CurrentUser.Departments.Select(d => d.Id)));
            cbxChonNamStore.DataBind();
            hdfMaDonVi.Text = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
            SetChartDefault();

            //var tmp = Session["DataHomePage"].ToString().Split(';');
            //if (int.Parse(tmp[0]) > 0)
            //{
            //    pnlSinhNhatNhanVien.Title += @" <b id='pSinhNhatNhanVien' style='color:red;'>(" + tmp[0] + @" cán bộ)</b>";
            //}
            //if (int.Parse(tmp[1]) > 0)
            //{
            //    pnlNhanVienSapHetHopDong.Title += @" <b id='pNhanVienSapHetHopDong' style='color:red;'>(" + tmp[1] + @" cán bộ)</b>";
            //}
            //if (int.Parse(tmp[2]) > 0)
            //{
            //    Panel4.Title += @" <b id='pNghiHuu' style='color:red;'>(" + tmp[2] + @" cán bộ)</b>";
            //}
            //if (int.Parse(tmp[3]) > 0)
            //{
            //    panelRiseSalary.Title += @" <b id='pCBCCVCDenKyNangLuong' style='color:red;'>(" + tmp[3] + @" cán bộ)</b>";
            //}
            //if (int.Parse(tmp[4]) > 0)
            //{
            //    panelOutFrameSalary.Title += @" <b id='pOutFrameSalaryId' style='color:red;'>(" + tmp[4] + @" cán bộ)</b>";
            //}
            SetVisibleByRole();
        }
        [DirectMethod]
        public void showWindowMoRong()
        {
            var strGet = hdfChartUrl.Text + "&height=460&size=260";
            wdMoRong.AutoLoad.Url = strGet;
            wdMoRong.AutoLoad.Mode = LoadMode.IFrame;
            wdMoRong.Render(Form);
            wdMoRong.Show();
        }

        /// <summary>
        /// Thiết lập hiển thị dựa trên quyền
        /// </summary>
        private void SetVisibleByRole()
        {
            //   DataTable table = DataHandler.GetInstance().ExecuteDataTable("role_roleOfDesktop", "@StartmenuID", "@EndmenuID", "@UserID", 5000, 5999, CurrentUser.User.Id);
            //foreach (DataRow item in table.Rows)
            //{
            //    switch (item[0].ToString())
            //    {
            //        case "5001":
            Toolbar1.Visible = true;
            pnlChart.Visible = true;
            lblIframe.Visible = true;
            //       break;
            //   case "5002":
            btnChonLoaiBieuDo.Visible = true;
            //       break;
            //   case "5003": //bieu do gioi tinh
            MenuItem1.Visible = true;
            //        break;
            //   case "5004": //Biểu đồ trình độ 
            mnuTrinhDoChart.Visible = true;
            //      break;
            //   case "5005"://Biểu đồ biến động nhân sự
            MenuItem3.Visible = true;
            //        break;
            //    case "5006"://Biểu đồ độ tuổi
            MenuItem4.Visible = true;
            //       break;
            //case "5007"://Thống kê nhân sự theo đơn vị 
            MenuBieuDoDonVi.Visible = true;
            //    break; 
            //     case "5009"://Thống kê theo tình trạng hôn nhân 
            MenuItem8.Visible = true;
            //         break;
            //    case "5010"://Biểu đồ tôn giáo 
            MenuItem9.Visible = true;
            //       break;
            //     case "5011"://Biểu đồ dân tộc 
            MenuItem10.Visible = true;
            //        break;
            //     case "5012"://Biểu đồ loại hợp đồng 
            MenuItem11.Visible = true;
            //        break;
            //case "5026": //Thống kê nhân sự theo phòng ban
            //    mnuThongKeNhanSuTheoPhongBan.Visible = true;
            //    break;
            //    case "5027": //Thống kê theo thâm niên công tác
            menuThongKeNhanSuTheoThamNienCongTac.Visible = true;
            //        break;
            //case "5028": //Biểu đồ tỉnh thành
            //    mnuBieuDoTinhThanh.Visible = true;
            //    break;
            //    case "5029"://Biểu đồ chức vụ đoàn
            mnuBieuDoChucVuDoan.Visible = true;
            //        break;
            //    case "5030": //Biểu đồ chức vụ đảng
            mnuBieuDoChucVuDang.Visible = true;
            //        break;
            //      case "5031"://Biểu đồ chức vụ quân đội
            mnuBieuDoChucVuQuanDoi.Visible = true;
            //          break;
            //   case "5013":
            btnSetChartDefault.Visible = true;
            //     break;
            // case "5015": //Tra cứu nhanh thông tin nhân viên
            tbar.Visible = true;
            GridPanel1.Visible = true;
            Panel2.Visible = true;
            //      break;
            //    case "5016":
            btnBaoCaoChiTiet1NV.Visible = true;
            MenuItem2.Visible = true;
            //        break;
            //    case "5017":
            //btnSendEmail.Visible = true;
            mnuGuiMail.Visible = true;
            //        break;
            //    case "5018": //Sinh nhật nhân viên
            pnlSinhNhatNhanVien.Visible = true;
            Panel6.Visible = true;
            //         break;
            //    case "5019": //In danh sách
            tbReportDanhSach.Visible = true;
            //        break;
            //     case "5021": //Nhân viên sắp hết hợp đồng
            pnlNhanVienSapHetHopDong.Visible = true;
            Panel6.Visible = true;
            //        break;
            //    case "5022":
            Toolbar2.Visible = true;
            //        break;
            //case "5040"://Thống kê lý do nghỉ việc
            //    //mnuLyDoNghiViec.Visible = true;
            //    break;
            //    }
            //}
        }

        protected void cbxChonNamStore_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            var maDv = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
            var obj = GetYear(maDv);
            cbxChonNamStore.DataSource = obj;
            cbxChonNamStore.DataBind();
        }

        private List<object> GetYear(string maDV)
        {
            var departments = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
            var arrDepartment = string.IsNullOrEmpty(departments)
                ? new string[] { }
                : departments.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            for (var i = 0; i < arrDepartment.Length; i++)
            {
                arrDepartment[i] = "'{0}'".FormatWith(arrDepartment[i]);
            }
            var condition = " 1= 1";
            if (!string.IsNullOrEmpty(departments))
                condition += @" AND [DepartmentId] IN ({0}) ".FormatWith(string.Join(",", arrDepartment));
            var lists = hr_RecordServices.GetAll(condition).Where(rc => rc.RecruimentDate != null).Select(rc => rc.RecruimentDate.Value.Year).ToList();
            lists.Sort(new MyComparation());
            var obj = new List<object>();
            if (!lists.Contains(DateTime.Now.Year))
            {
                obj.Add(new { ID = DateTime.Now.Year, Title = DateTime.Now.Year });
            }
            for (var i = 0; i < lists.Count; i++)
            {
                obj.Add(new { ID = lists[i], Title = lists[i] });
            }
            if (obj.Count == 0)
            {
                obj.Add(new { ID = -1, Title = "Không có dữ liệu" });
            }
            return obj;
        }


        /// <summary>
        /// Load biểu đồ mặc định của chương trình hoặc do người dùng thiết lập
        /// </summary>
        private void SetChartDefault()
        {
            if (!new FileInfo(Server.MapPath("XMLConfig.xml")).Exists) return;
            var xDoc = XDocument.Load(Server.MapPath("XMLConfig.xml"));
            var userSetDefault = (from t in xDoc.Descendants("Chart")
                                  let xAttribute = t.Attribute("userid")
                                  where xAttribute != null && xAttribute.Value == CurrentUser.User.Id.ToString()
                                  select t.Element("UserSetDefault"));
            var setDefault = userSetDefault as XElement[] ?? userSetDefault.ToArray();
            if (setDefault.Length != 0)
            {
                var orDefault = setDefault.FirstOrDefault();
                if (orDefault != null) hdfChartUrl.Text = orDefault.Value;
                var firstOrDefault = setDefault.FirstOrDefault();
                if (firstOrDefault != null && firstOrDefault.Value.IndexOf("BDNhanSu", StringComparison.Ordinal) >= 0)
                {
                    cbxChonNam.Hidden = false;
                    tbsChonNam.Hidden = false;
                }
                var xElement = setDefault.FirstOrDefault();
                if (xElement != null)
                    lblIframe.Html =
                        string.Format("<iframe height='400' frameborder='0' id='iframeChart' width='100%' src='{0}' />",
                            xElement.Value);
            }
            else
            {
                lblIframe.Html = @"<iframe height='400' frameborder='0' id='iframeChart' width='100%' src='chart/ColumnChart.aspx?type=NSDonVi' />";
            }
        }

        protected void btnSetChartDefault_Click(object sender, DirectEventArgs e)
        {
            if (string.IsNullOrEmpty(hdfChartUrl.Text)) return;
            var xDoc = XDocument.Load(Server.MapPath("XMLConfig.xml"));
            var linkChart = (from t in xDoc.Descendants("Chart")
                             let xAttribute = t.Attribute("userid")
                             where xAttribute != null && xAttribute.Value == CurrentUser.User.Id.ToString()
                             select t.Element("UserSetDefault"));
            var xElements = linkChart as XElement[] ?? linkChart.ToArray();
            if (xElements.Length != 0)
            {
                var firstOrDefault = xElements.FirstOrDefault();
                if (firstOrDefault != null) firstOrDefault.Value = hdfChartUrl.Text;
                xDoc.Save(Server.MapPath("XMLConfig.xml"));
            }
            else //Tạo mới
            {
                var column = new XElement("Chart",
                    new XElement("UserSetDefault", hdfChartUrl.Text));
                column.SetAttributeValue("userid", CurrentUser.User.Id);
                if (xDoc.Root != null) xDoc.Root.Add(column);
                xDoc.Save(Server.MapPath("XMLConfig.xml"));
            }

            //Ghi log cho việc thay đổi chart mặc định
            var chartName = hdfChartUrl.Text.Contains("Country") ? "Biểu đồ quốc tịch" : hdfChartUrl.Text;

            //var accessDiary = new AccessHistory
            //{
            //    Function = "Thay đổi chart mặc định",
            //    Description = "Biểu đồ : " + chartName,
            //    IsError = false,
            //    UserName = CurrentUser.User.UserName,
            //    Time = DateTime.Now,
            //    BusinessCode = "",
            //    ComputerName = Util.GetInstance().GetComputerName(Request.UserHostAddress),
            //    ComputerIP = Request.UserHostAddress,
            //    Referent = ""
            //};
            //AccessHistoryServices.Create(accessDiary);
        }

        protected void btnSendEmail_Click(object sender, DirectEventArgs e)
        {
            var mailto = e.ExtraParams["Email"];
            if (!string.IsNullOrEmpty(mailto)) return;
            ExtNet.Msg.Alert("Cảnh báo", "Không tìm thấy email").Show();
        }

        protected void SendEmailHappyBirthday(object sender, DirectEventArgs e)
        {
            try
            {
                var selecteds = RowSelectionModel3.SelectedRows;
                var mailto = string.Empty;
                var error = "";
                //var dt = DataController.DataHandler.GetInstance().ExecuteDataTable("sp_GetAllEmailHappyBirthDayMonth");
                var condition = " [BirthDate] IS NOT NULL AND MONTH([BirthDate])='{0}'".FormatWith(DateTime.Now.Month) +
                                   " AND [WorkStatusId] = (SELECT  TOP 1 Id FROM cat_WorkStatus WHERE [Name] LIKE N'%Đang làm việc%')";
                var dt = RecordController.GetAll(condition);
                if (e.ExtraParams["All"] == "True")
                {
                    foreach (var record in dt)
                    {
                        if (!string.IsNullOrEmpty(record.WorkEmail))
                        {
                            mailto += record.WorkEmail + ", ";
                        }
                        else if (!string.IsNullOrEmpty(record.PersonalEmail))
                        {
                            mailto += record.PersonalEmail + ", ";
                        }
                        else
                        {
                            error += record.FullName + " ";
                        }
                    }
                }
                else
                {
                    foreach (var item in selecteds)
                    {
                        var maCb = item.RecordID;
                        foreach (var record in dt)
                        {
                            if (record.EmployeeCode != maCb) continue;
                            if (!string.IsNullOrEmpty(record.WorkEmail))
                            {
                                mailto += record.WorkEmail + ", ";
                            }
                            else if (!string.IsNullOrEmpty(record.PersonalEmail))
                            {
                                mailto += record.PersonalEmail + ", ";
                            }
                            else
                            {
                                Dialog.ShowError("Không tìm thấy email");
                                return;
                            }
                        }
                    }
                }
                var mail = "";
                for (var i = 0; i < mailto.Length - 2; i++)
                {
                    mail += mailto[i];
                }
                //SystemController htController = new SystemController();
                //SendMail1.SetEmailTo(htController.GetValueByName(SystemConfigParameter.EMAIL, Session["MaDonVi"].ToString()), htController.GetValueByName(SystemConfigParameter.PASSWORD_EMAIL, Session["MaDonVi"].ToString()), mail);

                //SendMail1.Show();
                if (!string.IsNullOrEmpty(error.Trim()))
                {
                    Dialog.ShowError("Một số cán bộ không có email :" + error);
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowError("Lỗi xảy ra " + ex.Message);
            }
        }
        private class MyComparation : IComparer<int>
        {
            public int Compare(int x, int y)
            {
                if (x > y)
                    return -1;
                return x < y ? 1 : 0;
            }
        }
        protected void ShowBirthDayReport(object sender, DirectEventArgs e)
        {
            try
            {
                RM.RegisterClientScriptBlock("fd", "#{TabPanel1}.remove(0);addHomePage(#{TabPanel1},'Homepage','../Report/ReportView.aspx?rp=BornInMonth', 'Báo cáo danh sách nhân viên sinh nhật trong tháng');");
                // TODO: Sửa lổi report filter
                //var rp = new ReportFilter
                //{
                //    StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1),
                //    Date = dfNgaySinh.SelectedDate,
                //    EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1),
                //    StartMonth = DateTime.Now.Month,
                //    EndMonth = DateTime.Now.Month,
                //    SessionDepartment = Session["MaDonVi"].ToString(),
                //    WhereClause = "",
                //    SelectedDepartment = string.Join(",", CurrentUser.Departments.Select(d => d.Id)),
                //    Reporter = CurrentUser.User.DisplayName
                //};
                //Session.Add("rp", rp);
                Window1.Title = @"Báo cáo sinh nhật nhân viên";
                Window1.Show();
            }
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }
        protected void ShowRetirementReport(object sender, DirectEventArgs e)
        {
            try
            {
                RM.RegisterClientScriptBlock("fd", "#{TabPanel1}.remove(0);addHomePage(#{TabPanel1},'Homepage1','../Report/ReportView.aspx?rp=EmployeeRetirement', 'Báo cáo danh sách cán bộ công chức đến kỳ nghỉ hưu');");
                // TODO: Sửa lổi report filter
                //var rp = new ReportFilter
                //{
                //    StartDate = DateTime.Now,
                //    Date = dfNgaySinh.SelectedDate,
                //    Reporter = CurrentUser.User.DisplayName,
                //    EndDate = DateTime.Now.AddDays(30),
                //    SessionDepartment = Session["MaDonVi"].ToString(),
                //    ReportedDate = DateTime.Parse(DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day),
                //    SelectedDepartment = string.Join(",", CurrentUser.Departments.Select(d => d.Id))
                //};
                //Session.Add("rp", rp);
                Window1.Title = @"Danh sách cán bộ đến kỳ nghỉ hưu ";
                Window1.Show();
            }
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }
        protected void ShowRiseSalaryReport(object sender, DirectEventArgs e)
        {
            try
            {
                //RM.RegisterClientScriptBlock("fd", "#{TabPanel1}.remove(0);addHomePage(#{TabPanel1},'Homepage1','../Report/ReportView.aspx?rp=SalaryIncrease', 'Báo cáo danh sách cán bộ công chức đến kỳ nâng lương');");
                // TODO: Sửa lổi report filter
                //var rp = new ReportFilter
                //{
                //    Reporter = CurrentUser.User.DisplayName,
                //    Date = dfToDate.SelectedDate,
                //    StartDate = dfFromDate.SelectedDate,
                //    EndDate = dfToDate.SelectedDate,
                //    Year = dfToDate.SelectedDate.Year,
                //    StartMonth = dfToDate.SelectedDate.Month,
                //    SessionDepartment = Session["MaDonVi"].ToString(),
                //    SelectedDepartment = string.Join(",", CurrentUser.Departments.Select(d => d.Id)),
                //    ReportedDate = DateTime.Now
                //};
                //Session.Add("rp", rp);
                //Window1.Title = @"Danh sách cán bộ đến kỳ nâng lương";
                //Window1.Show();
            }
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }

        protected void ShowOutFrameSalaryReport(object sender, DirectEventArgs e)
        {
            try
            {
                RM.RegisterClientScriptBlock("fd", "#{TabPanel1}.remove(0);addHomePage(#{TabPanel1},'Homepage1','../Report/ReportView.aspx?rp=SalaryOutOfFrame', 'Báo cáo danh sách cán bộ đến kỳ xét vượt khung');");
                // TODO: Sửa lổi report filter
                //var rp = new ReportFilter
                //{
                //    Reporter = CurrentUser.User.DisplayName,
                //    SessionDepartment = Session["MaDonVi"].ToString(),
                //    SelectedDepartment = string.Join(",", CurrentUser.Departments.Select(d => d.Id)),
                //    ReportedDate = DateTime.Now
                //};
                //Session.Add("rp", rp);
                Window1.Title = @"Danh sách cán bộ đến kỳ xét vượt khung ";
                Window1.Show();
            }
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }

        protected void ShowEndContractReport(object sender, DirectEventArgs e)
        {
            try
            {
                RM.RegisterClientScriptBlock("fd", "#{TabPanel1}.remove(0);addHomePage(#{TabPanel1},'Homepage1','../Report/ReportView.aspx?rp=EmployeeExpried', '`Báo cáo danh sách nhân viên sắp hết hợp đồng');");
                // TODO: Sửa lổi report filter
                //var rp = new ReportFilter
                //{
                //    StartDate = DateTime.Now,
                //    Reporter = CurrentUser.User.DisplayName,
                //    EndDate = DateTime.Now.AddDays(30),
                //    SessionDepartment = Session["MaDonVi"].ToString(),
                //    SelectedDepartment = string.Join(",", CurrentUser.Departments.Select(d => d.Id)),
                //    ReportedDate = DateTime.Parse(DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day)
                //};
                //Session.Add("rp", rp);
                Window1.Title = @"Danh sách cán bộ nhân viên sắp hết hạn hợp đồng";
                Window1.Show();
            }
            catch (Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }
        protected void cbxTrangThaiHoSo_OnrefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            cbxTrangThaiHoSo_store.DataSource = cat_WorkStatusServices.GetAll();
            cbxTrangThaiHoSo_store.DataBind();
        }
    }

}

