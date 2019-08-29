using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Ext.Net;

using Web.Core;
using Web.Core.Framework;
using Web.Core.Framework.Adapter;
using Web.Core.Framework.Utils;
using Web.Core.Helper;
using Web.Core.Service.HumanRecord;

namespace Web.HRM.Modules.Home
{
    public partial class Default : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // init control
            InitControl();

            if (!ExtNet.IsAjaxRequest)
            {
                // departments
                hdfDepartments.Text = DepartmentIds;
                hdfCurrentUserID.Text = CurrentUser.User.Id.ToString();
                hdfSalaryRaiseRegularType.Text = ((int) SalaryDecisionType.Regular).ToString();
                hdfSalaryRaiseOutFrameType.Text = ((int)SalaryDecisionType.OverGrade).ToString();

                // years 
                cbxChonNamStore.DataSource = GetYear();
                cbxChonNamStore.DataBind();

                // init control
                hdfDepartmentIds.Text = string.Join(",", CurrentUser.Departments.Select(d => d.Id));

                var watch = new System.Diagnostics.Stopwatch();
                watch.Start();

                // execute code here
                LoadData();

                watch.Stop();
                System.Diagnostics.Debug.WriteLine($@"LoadData: {watch.ElapsedMilliseconds} ms");

                
            }
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

        #region Protect Method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cbxChonNamStore_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            var obj = GetYear();
            cbxChonNamStore.DataSource = obj;
            cbxChonNamStore.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSetChartDefault_Click(object sender, DirectEventArgs e)
        {
            if(string.IsNullOrEmpty(hdfChartUrl.Text)) return;
            var xDoc = XDocument.Load(Server.MapPath("XMLConfig.xml"));
            var linkChart = (from t in xDoc.Descendants("Chart")
                             let xAttribute = t.Attribute("userid")
                             where xAttribute != null && xAttribute.Value == CurrentUser.User.Id.ToString()
                             select t.Element("UserSetDefault"));
            var xElements = linkChart as XElement[] ?? linkChart.ToArray();
            if(xElements.Length != 0)
            {
                var firstOrDefault = xElements.FirstOrDefault();
                if(firstOrDefault != null) firstOrDefault.Value = hdfChartUrl.Text;
                xDoc.Save(Server.MapPath("XMLConfig.xml"));
            }
            else //Tạo mới
            {
                var column = new XElement("Chart",
                    new XElement("UserSetDefault", hdfChartUrl.Text));
                column.SetAttributeValue("userid", CurrentUser.User.Id);
                if(xDoc.Root != null) xDoc.Root.Add(column);
                xDoc.Save(Server.MapPath("XMLConfig.xml"));
            }

            //Ghi log cho việc thay đổi chart mặc định
            var chartName = hdfChartUrl.Text.Contains("Country") ? "Biểu đồ quốc tịch" : hdfChartUrl.Text;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSendEmail_Click(object sender, DirectEventArgs e)
        {
            var mailto = e.ExtraParams["Email"];
            if(!string.IsNullOrEmpty(mailto)) return;
            ExtNet.Msg.Alert("Cảnh báo", "Không tìm thấy email").Show();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                if(e.ExtraParams["All"] == "True")
                {
                    foreach(var record in dt)
                    {
                        if(!string.IsNullOrEmpty(record.WorkEmail))
                        {
                            mailto += record.WorkEmail + ", ";
                        }
                        else if(!string.IsNullOrEmpty(record.PersonalEmail))
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
                    foreach(var item in selecteds)
                    {
                        var maCb = item.RecordID;
                        foreach(var record in dt)
                        {
                            if(record.EmployeeCode != maCb) continue;
                            if(!string.IsNullOrEmpty(record.WorkEmail))
                            {
                                mailto += record.WorkEmail + ", ";
                            }
                            else if(!string.IsNullOrEmpty(record.PersonalEmail))
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

                if(!string.IsNullOrEmpty(error.Trim()))
                {
                    Dialog.ShowError("Một số cán bộ không có email :" + error);
                }
            }
            catch(Exception ex)
            {
                Dialog.ShowError("Lỗi xảy ra " + ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ShowBirthDayReport(object sender, DirectEventArgs e)
        {
            try
            {
                RM.RegisterClientScriptBlock("fd", "#{TabPanel1}.remove(0);addHomePage(#{TabPanel1},'Homepage','../Report/ReportView.aspx?rp=BornInMonth&startDate=" +
                    DateTime.Now.Month +
                    "&endDate=" +
                    DateTime.Now.Month +
                    "', 'Báo cáo danh sách nhân viên sinh nhật trong tháng');");
                Window1.Title = @"Báo cáo sinh nhật nhân viên";
                Window1.Show();
            }
            catch(Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ShowRetirementReport(object sender, DirectEventArgs e)
        {
            try
            {
                var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);  // get start of month
                var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1); // last day of month
                RM.RegisterClientScriptBlock("fd", "#{TabPanel1}.remove(0);addHomePage(#{TabPanel1},'Homepage1','../Report/ReportView.aspx?rp=EmployeeRetired', 'Báo cáo danh sách cán bộ công chức đến kỳ nghỉ hưu');");                
                Window1.Title = @"Danh sách cán bộ đến kỳ nghỉ hưu ";
                Window1.Show();
            }
            catch(Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ShowRiseSalaryReport(object sender, DirectEventArgs e)
        {
            try
            {
                RM.RegisterClientScriptBlock("fd", "#{TabPanel1}.remove(0);addHomePage(#{TabPanel1},'Homepage1','../Report/ReportView.aspx?rp=SalaryIncrease&startDate=" + dfToDate.SelectedDate + "&endDate=" + dfFromDate.SelectedDate + "', 'Báo cáo danh sách cán bộ công chức đến kỳ nâng lương');");                
                Window1.Title = @"Danh sách cán bộ đến kỳ nâng lương";
                Window1.Show();
            }
            catch(Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ShowOutFrameSalaryReport(object sender, DirectEventArgs e)
        {
            try
            {
                RM.RegisterClientScriptBlock("fd", "#{TabPanel1}.remove(0);addHomePage(#{TabPanel1},'Homepage1','../Report/ReportView.aspx?rp=SalaryOutOfFrame&startDate=" + dfToDate.SelectedDate + "&endDate=" + dfFromDate.SelectedDate + "', 'Báo cáo danh sách cán bộ đến kỳ xét vượt khung');");                
                Window1.Title = @"Danh sách cán bộ đến kỳ xét vượt khung ";
                Window1.Show();
            }
            catch(Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ShowEndContractReport(object sender, DirectEventArgs e)
        {
            try
            {
                var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);  // get start of month
                var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1); // last day of month
                RM.RegisterClientScriptBlock("fd", "#{TabPanel1}.remove(0);addHomePage(#{TabPanel1},'Homepage1','../Report/ReportView.aspx?rp=EmployeeExpired&startDate=', " + firstDayOfMonth.ToString("yyyy-MM-dd") + "&endDate=" + lastDayOfMonth.ToString("yyyy-MM-dd") + "'Báo cáo danh sách nhân viên sắp hết hợp đồng');");                
                Window1.Title = @"Danh sách cán bộ nhân viên sắp hết hạn hợp đồng";
                Window1.Show();
            }
            catch(Exception ex)
            {
                Dialog.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void storeRecordStatus_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            storeRecordStatus.DataSource = typeof(RecordStatus).GetIntAndDescription();
            storeRecordStatus.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void storeNextRaise_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            var fromDate = ConvertUtils.GetStartDayOfMonth();
            var toDate = fromDate.AddMonths(3).AddDays(-1);

            if(!DatetimeHelper.IsNull(dfFromDate.SelectedDate) && !DatetimeHelper.IsNull(dfToDate.SelectedDate))
            {
                fromDate = dfFromDate.SelectedDate;
                toDate = dfToDate.SelectedDate;
            }

            storeNextRaise.DataSource = SalaryDecisionController.GetNextRaise(fromDate, toDate);
            storeNextRaise.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void storeOutFrameSalary_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            var fromDate = ConvertUtils.GetStartDayOfMonth();
            var toDate = fromDate.AddMonths(3).AddDays(-1);

            if(!DatetimeHelper.IsNull(dateFieldFromDate.SelectedDate) && !DatetimeHelper.IsNull(dateFieldToDate.SelectedDate))
            {
                fromDate = dateFieldFromDate.SelectedDate;
                toDate = dateFieldToDate.SelectedDate;
            }

            storeOutFrameSalary.DataSource = SalaryDecisionController.GetOverGrade(fromDate, toDate);
            storeOutFrameSalary.DataBind();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private List<object> GetYear()
        {            
            var condition = " 1= 1";
            if(!string.IsNullOrEmpty(DepartmentIdsSql))
                condition += @" AND [DepartmentId] IN ({0}) ".FormatWith(DepartmentIdsSql);
            var lists = hr_RecordServices.GetAll(condition).Where(rc => rc.RecruimentDate != null).Select(rc => rc.RecruimentDate.Value.Year).ToList();
            var obj = new List<object>();
            if(!lists.Contains(DateTime.Now.Year))
            {
                obj.Add(new { ID = DateTime.Now.Year, Title = DateTime.Now.Year });
            }
            foreach(var t in lists)
            {
                obj.Add(new { ID = t, Title = t });
            }
            if(obj.Count == 0)
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
            if(!new FileInfo(Server.MapPath("XMLConfig.xml")).Exists) return;
            var xDoc = XDocument.Load(Server.MapPath("XMLConfig.xml"));
            var userSetDefault = (from t in xDoc.Descendants("Chart")
                                  let xAttribute = t.Attribute("userid")
                                  where xAttribute != null && xAttribute.Value == CurrentUser.User.Id.ToString()
                                  select t.Element("UserSetDefault"));
            var setDefault = userSetDefault as XElement[] ?? userSetDefault.ToArray();
            if(setDefault.Length != 0)
            {
                var orDefault = setDefault.FirstOrDefault();
                if(orDefault != null) hdfChartUrl.Text = orDefault.Value;
                var firstOrDefault = setDefault.FirstOrDefault();
                if(firstOrDefault != null && firstOrDefault.Value.IndexOf("BDNhanSu", StringComparison.Ordinal) >= 0)
                {
                    cbxChonNam.Hidden = false;
                    tbsChonNam.Hidden = false;
                }
                var xElement = setDefault.FirstOrDefault();
                if(xElement != null)
                    lblIframe.Html = $@"<iframe height='400' frameborder='0' id='iframeChart' width='100%' src='{xElement.Value}' />";
            }
            else
            {
                lblIframe.Html = @"<iframe height='400' frameborder='0' id='iframeChart' width='100%' src='chart/ColumnChart.aspx?type=NSDonVi' />";
            }
        }

        /// <summary>
        /// Load data
        /// </summary>
        private void LoadData()
        {
            // display total label
            DisplayTotalContractCount();
            DisplayTotalBirthInMonth();
            DisplayTotalRetirement();
            DisplayTotalNextRaise();
            DisplayTotalOverGrade();

            // set chart
            SetChartDefault();

            // set role
            SetVisibleByRole();
        }

        /// <summary>
        /// 
        /// </summary>
        private void DisplayTotalContractCount()
        {
            // count contract
            var totalContractExpirse =
                SQLHelper.ExecuteTable(
                    SQLManagementAdapter.GetStore_DanhSachNhanVienSapHetHopDong(null, null, 30, DepartmentIds)).Rows.Count;
            pnEmployeeExpried.Title += @" <b id='pNhanVienSapHetHopDong' style='color:red;'>(" + totalContractExpirse + @" cán bộ)</b>";
        }

        /// <summary>
        /// 
        /// </summary>
        private void DisplayTotalBirthInMonth()
        {
            var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);  // get start of month
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1); // last day of month
            // count birth in month            
            var totalBirdInMonth = SQLHelper
                .ExecuteTable(SQLManagementAdapter.GetStore_BirthdayOfEmployee(DepartmentIds, firstDayOfMonth, lastDayOfMonth, null, null)).Rows.Count;
            pnlSinhNhatNhanVien.Title += @" <b id='pSinhNhatNhanVien' style='color:red;'>(" + totalBirdInMonth + @" cán bộ)</b>";
        }

        /// <summary>
        /// 
        /// </summary>
        private void DisplayTotalRetirement()
        {            
            // retrirement of employee          
            var totalRetirementOfEmployee = SQLHelper
                .ExecuteTable(SQLManagementAdapter.GetStore_RetirementOfEmployee(null, null, DepartmentIds)).Rows.Count;
            pnlEmployeeRetired.Title += @" <b id='pNghiHuu' style='color:red;'>(" + totalRetirementOfEmployee + @" cán bộ)</b>";
        }

        /// <summary>
        /// 
        /// </summary>
        public void DisplayTotalNextRaise()
        {
            // init default from date, to date
            var fromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var toDate = fromDate.AddMonths(3).AddDays(-1);

            // count total
            var total = SalaryDecisionController.CountNextRaise(fromDate, toDate);

            // update panel title
            pnlNextRaise.Title += " <b id='pCBCCVCDenKyNangLuong' style='color:red;'>({0} cán bộ)</b>".FormatWith(total);
        }

        /// <summary>
        /// 
        /// </summary>
        private void DisplayTotalOverGrade()
        {
            // init default from date, to date
            var fromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var toDate = fromDate.AddMonths(3).AddDays(-1);

            // count total
            var total = SalaryDecisionController.CountOverGrade(fromDate, toDate);

            // update panel title
            pnlOverGrade.Title += " <b id='pOutFrameSalaryId' style='color:red;'>({0} cán bộ)</b>".FormatWith(total);
        }

        /// <summary>
        /// Thiết lập hiển thị dựa trên quyền
        /// </summary>
        private void SetVisibleByRole()
        {
            Toolbar1.Visible = true;
            pnlChart.Visible = true;
            lblIframe.Visible = true;
            btnChonLoaiBieuDo.Visible = true;
            mnuChartBySex.Visible = true;
            mnuChartByDegree.Visible = true;
            mnuVolatility.Visible = true;
            mnuAge.Visible = true;
            mnuWorkUnit.Visible = true; 
            mnuMatrimony.Visible = true;
            mnuReligion.Visible = true;
            mnuAge.Visible = true;
            mnuContractType.Visible = true;
            mnuSeniority.Visible = true;
            mnuTitle.Visible = true;
            mnuPartyLevel.Visible = true;
            mnuArmyLevel.Visible = true;
            btnSetChartDefault.Visible = true;
            tbar.Visible = true;
            gpRecord.Visible = true;
            pnlQuickSearch.Visible = true;
            pnlSinhNhatNhanVien.Visible = true;
            tbReportDanhSach.Visible = true;
            pnEmployeeExpried.Visible = true;
            Toolbar2.Visible = true;
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitControl()
        {
            // set resource
            mnuChartBySex.Text = Resource.Get("Chart.EmployeeBySex");
            mnuChartByDegree.Text = Resource.Get("Chart.EmployeeByDegree");
            mnuMatrimony.Text = Resource.Get("Chart.EmployeeByMatrimony");
            mnuReligion.Text = Resource.Get("Chart.EmployeeByReligion");
            mnuFolk.Text = Resource.Get("Chart.EmployeeByFolk");
            mnuContractType.Text = Resource.Get("Chart.EmployeeByContractType");
            mnuTitle.Text = Resource.Get("Chart.EmployeeByTitle");
            mnuPartyLevel.Text = Resource.Get("Chart.EmployeeByPartyLevel");
            mnuArmyLevel.Text = Resource.Get("Chart.EmployeeByArmyLevel");
            mnuSeniority.Text = Resource.Get("Chart.EmployeeBySeniority");
            mnuVolatility.Text = Resource.Get("Chart.Volatility");
            mnuAge.Text = Resource.Get("Chart.EmployeeByAge");
            mnuWorkUnit.Text = Resource.Get("Chart.EmployeeByWorkUnit");

            pnlQuickSearch.Title = Resource.Get("QuikLookUp");
            pnEmployeeExpried.Title = Resource.Get("Employee.ReportExpried");

            // Display HJM control
            if(Resource.IsHJM())
            {
                pnlEmployeeRetired.Visible = true;
                pnlOverGrade.Visible = true;
            }

            // Set Access Right
            if (CurrentPermission.HasFullControl)
            {
                btnPrintRecord.Hidden = false;
            }
        }

        #endregion

        
    }

}

