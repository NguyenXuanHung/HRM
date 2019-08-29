using Ext.Net;
using System;
using Web.Core.Framework;
using Web.Core.Framework.Utils;
using Web.Core.Framework.Controller.TimeSheet;
using Web.Core.Framework.Model.TimeSheet;
using Web.Core.Helper;


namespace Web.HRM.Modules.TimeSheet
{
    public partial class TimeSheetListManagement : BasePage
    {
        private const string TimeSheetManagementUrl = "~/Modules/TimeSheet/TimeSheetDetailManagement.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {
            if(ExtNet.IsAjaxRequest) return;
            
            dfFromDate.SetValue(ConvertUtils.GetStartDayOfMonth());
            dfToDate.SetValue(ConvertUtils.GetLastDayOfMonth());
        }

        /// <summary>
        /// Init setting window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InitWindow(object sender, DirectEventArgs e)
        {
            var param = e.ExtraParams["Id"];
            if(!int.TryParse(param, out var id)) return;
            if(id > 0)
            {
                // edit
                wdTimeSheet.Title = @"Sửa bảng chấm công";
                wdTimeSheet.Icon = Icon.Pencil;
                EmployeeGrid.Disabled = true;
                cbDepartmentList.Disabled = true;
                btnUpdate.Show();
                btnUpdateNew.Hide();
            }
            else
            {
                // insert
                wdTimeSheet.Title = @"Thêm mới bảng chấm công";
                wdTimeSheet.Icon = Icon.Add;
                txtName.Reset();
                EmployeeGrid.Disabled = false;
                cbDepartmentList.Disabled = false;
                btnUpdate.Hide();
                btnUpdateNew.Show();
            }
            hdfTimeSheetReportId.Text = id.ToString();
            if(id > 0)
            {
                var result = TimeSheetReportController.GetById(Convert.ToInt32(hdfTimeSheetReportId.Text));
                if(result != null)
                {
                    txtName.Text = result.Name;
                    dfFromDate.SetValue(result.StartDate);
                    dfToDate.SetValue(result.EndDate);
                }
            }
            // show window
            wdTimeSheet.Show();
        }

        /// <summary>
        /// Tạo mới bảng chấm công
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InsertOrUpdate(object sender, DirectEventArgs e)
        {           
            if(e.ExtraParams["Command"] == "Update")
                Update();
            else
                Insert();
            wdTimeSheet.Hide();
            gridTimeSheetList.Reload();
        }

        /// <summary>
        /// 
        /// </summary>
        private void Insert()
        {
            var timeSheetReportModel = new TimeSheetReportModel
            {
                CreatedBy = CurrentUser.User.UserName,
                CreatedDate = DateTime.Now,
            };
            //edit data
            EditData(timeSheetReportModel);
            var newTimeSheetReport = TimeSheetReportController.Create(timeSheetReportModel);

            //create employeeReport
            foreach (var employee in chkEmployeeRowSelection.SelectedRows)
            {
                var timeSheetEmployeeReport = new TimeSheetEmployeeReportModel
                {
                    RecordId = int.Parse(employee.RecordID),
                    ReportId = newTimeSheetReport.Id
                };
                TimeSheetEmployeeReportController.Create(timeSheetEmployeeReport);
            }

            // create time sheet employee report
            Dialog.ShowNotification("Lưu thành công");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeSheetReportModel"></param>
        private void EditData(TimeSheetReportModel timeSheetReportModel)
        {
            timeSheetReportModel.Name = txtName.Text;
            if (!DatetimeHelper.IsNull(dfFromDate.SelectedDate))
                timeSheetReportModel.StartDate = dfFromDate.SelectedDate;
            if (!DatetimeHelper.IsNull(dfToDate.SelectedDate))
                timeSheetReportModel.EndDate = dfToDate.SelectedDate;
        }

        /// <summary>
        /// 
        /// </summary>
        private void Update()
        {
            var timeSheetReportModel = TimeSheetReportController.GetById(Convert.ToInt32(hdfTimeSheetReportId.Text));
            if (timeSheetReportModel != null)
            {
                timeSheetReportModel.EditedBy = CurrentUser.User.UserName;
                timeSheetReportModel.EditedDate = DateTime.Now;
                //edit data
                EditData(timeSheetReportModel);
            }

            TimeSheetReportController.Update(timeSheetReportModel);
            Dialog.ShowNotification("Lưu thành công");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Delete(object sender, DirectEventArgs e)
        {
            var param = e.ExtraParams["Id"];
            // parse id
            if(!int.TryParse(param, out var id) || id <= 0)
            {
                // parse error, show error
                Dialog.ShowError("Có lỗi xảy ra trong quá trình xử lý");
                return;
            }
            // delete timeSheetReport
            TimeSheetReportController.Delete(id);

            // delete timeSheetEmployeeReport
            var timeSheetEmployeeReports = TimeSheetEmployeeReportController.GetAll(null, null, null, id, null, null);
            if (timeSheetEmployeeReports != null)
            {
                foreach (var timeSheetEmployeeReport in timeSheetEmployeeReports)
                {
                    TimeSheetEmployeeReportController.Delete(timeSheetEmployeeReport.Id);
                }
            }
            //reload
            gridTimeSheetList.Reload();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SelectTimeSheetReport_Click(object sender, DirectEventArgs e)
        {
            if (!string.IsNullOrEmpty(hdfTimeSheetReportId.Text))
            {
                Response.Redirect(TimeSheetManagementUrl + "?mId=" + MenuId + "&id=" + hdfTimeSheetReportId.Text, true);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void stDepartmentList_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            stDepartmentList.DataSource = CurrentUser.DepartmentsTree;
            stDepartmentList.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        [DirectMethod]
        public void ReloadForm()
        {
            dfFromDate.Reset();
            dfToDate.Reset();
            txtName.Reset();
            cbDepartmentList.Reset();
        }
    }
}