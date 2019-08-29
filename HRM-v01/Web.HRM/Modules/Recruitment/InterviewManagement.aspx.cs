using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using Ext.Net;
using SmartXLS;
using Web.Core;
using Web.Core.Framework;
using Web.Core.Framework.Common;
using Web.Core.Helper;

namespace Web.HRM.Modules.Recruitment
{
    public partial class InterviewManagement : BasePage
    {
        private const string CandidateInterviewManagementUrl = "~/Modules/Recruitment/CandidateInterviewManagement.aspx";
        private const string ImportCandidateInterviewExcelFile = "/ImportCandidateInterview.xlsx";
        private const string EmployeeCode = @"Mã ứng viên";
        private const string FullName = @"Họ tên ứng viên";
        private const string TimeInterview = @"Giờ phỏng vấn(HH:mm)";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ExtNet.IsAjaxRequest)
            {
                hdfOrder.Text = Request.QueryString["order"];
                hdfDepartmentIds.Text = DepartmentIds;
                hdfCandidateType.Text = ((int) CandidateType.Interview).ToString();
                hdfRequiredRecruitmentStatus.Text = ((int)RecruitmentStatus.Approved).ToString();
            }
        }

        /// <summary>
        /// init setting window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InitWindow(object sender, DirectEventArgs e)
        {
            try
            {
                //reset form
                ResetForm();

                // init window props
                if (e.ExtraParams["Command"] == "Update")
                {
                    // edit
                    wdSetting.Title = @"Cập nhật lịch phỏng vấn";
                    wdSetting.Icon = Icon.Pencil;
                    var model = InterviewController.GetById(Convert.ToInt32(hdfId.Text));
                    if (model != null)
                    {
                        // set props
                        txtName.Text = model.Name;
                        dfInterviewDate.SetValue(model.InterviewDate);
                        txtInterviewer.Text = model.Interviewer;
                        tfFromTime.Text = model.FromTime.ToString("HH:mm");
                        tfToTime.Text = model.ToTime.ToString("HH:mm");
                        hdfRecruitment.Text = model.RequiredRecruitmentId.ToString();
                        cboRecruitment.Text = model.RequiredRecruitmentName;
                    }
                    //reload
                    fsCandidate.Disabled = true;
                }
                else
                {
                    // insert
                    wdSetting.Title = @"Thêm mới lịch phỏng vấn";
                    wdSetting.Icon = Icon.Add;
                    fsCandidate.Disabled = false;
                }
                
                // show window
                wdSetting.Show();
            }
            catch (Exception exception)
            {
                Dialog.ShowError(exception);
            }
        }

        /// <summary>
        /// insert or update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InsertOrUpdate(object sender, DirectEventArgs e)
        {
            try
            {
                // init entity
                var model = new InterviewModel();

                // check id
                if (!string.IsNullOrEmpty(hdfId.Text) && Convert.ToInt32(hdfId.Text) > 0)
                {
                    var result = InterviewController.GetById(Convert.ToInt32(hdfId.Text)); ;
                    if (result != null)
                        model = result;
                }

                // set new props for entity
                model.Name = txtName.Text;
                if(!DatetimeHelper.IsNull(dfInterviewDate.SelectedDate))
                    model.InterviewDate = dfInterviewDate.SelectedDate;
                if (!string.IsNullOrEmpty(hdfRecruitment.Text))
                    model.RequiredRecruitmentId = Convert.ToInt32(hdfRecruitment.Text);
                var date = dfInterviewDate.SelectedDate;
                model.FromTime = new DateTime(date.Year, date.Month, date.Day, tfFromTime.SelectedTime.Hours, tfFromTime.SelectedTime.Minutes, 0);
                model.ToTime = new DateTime(date.Year, date.Month, date.Day, tfToTime.SelectedTime.Hours, tfToTime.SelectedTime.Minutes, 0);
                model.Interviewer = txtInterviewer.Text;

                var interviewId = 0;
                // check entity id
                if (model.Id > 0)
                {
                    model.EditedDate = DateTime.Now;
                    model.EditedBy = CurrentUser.User.UserName;
                    // update
                    var result = InterviewController.Update(model);
                    interviewId = result.Id;
                }
                else
                {
                    model.CreatedBy = CurrentUser.User.UserName;
                    model.CreatedDate = DateTime.Now;
                    model.EditedDate = DateTime.Now;
                    model.EditedBy = "";
                    // insert
                    var result = InterviewController.Create(model);
                    interviewId = result.Id;
                }

                if (interviewId > 0)
                {
                    if (!string.IsNullOrEmpty(hdfCandidate.Text))
                    {
                        var interviewTime = new DateTime(date.Year, date.Month, date.Day,
                            tfInterview.SelectedTime.Hours, tfInterview.SelectedTime.Minutes, 0);
                        var candidateModel = new CandidateInterviewModel()
                        {
                            InterviewId = interviewId
                        };
                        var candidate = CandidateController.GetById(Convert.ToInt32(hdfCandidate.Text));
                        if (candidate != null)
                        {
                            var candidateInterview = CandidateInterviewController.GetAll(null, interviewId, candidate.RecordId, false, null, null);
                            if (candidateInterview.Count > 0)
                            {
                                candidateModel = candidateInterview.First();
                                candidateModel.TimeInterview = interviewTime;
                                candidateModel.EditedDate = DateTime.Now;
                                candidateModel.EditedBy = CurrentUser.User.UserName;
                                //update
                                CandidateInterviewController.Update(candidateModel);
                            }
                            else
                            {
                                candidateModel.TimeInterview = interviewTime;
                                candidateModel.RecordId = candidate.RecordId;
                                candidateModel.CreatedDate = DateTime.Now;
                                candidateModel.CreatedBy = CurrentUser.User.UserName;
                                candidateModel.EditedDate = DateTime.Now;
                                candidateModel.EditedBy = CurrentUser.User.UserName;

                                //create
                                CandidateInterviewController.Create(candidateModel);
                            }
                        }
                    }
                }

                // hide window
                wdSetting.Hide();

                //reset form
                ResetForm();
                // reload data
                gpInterview.Reload();
            }
            catch (Exception exception)
            {
                Dialog.ShowError(exception);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void ResetForm()
        {
            txtName.Reset();
            txtInterviewer.Reset();
            dfInterviewDate.Reset();
            tfFromTime.Reset();
            tfToTime.Reset();
            hdfRecruitment.Reset();
            cboRecruitment.Reset();
            hdfCandidate.Reset();
            cboEmployee.Reset();
            tfInterview.Reset();
        }

        /// <summary>
        /// Delete 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Delete(object sender, DirectEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(hdfId.Text))
                {
                    //delete candidate
                    if(Convert.ToInt32(hdfId.Text) > 0)
                        CandidateInterviewController.DeleteByCondition(Convert.ToInt32(hdfId.Text));

                    //delete
                    InterviewController.Delete(Convert.ToInt32(hdfId.Text));
                }

                // reload data
                gpInterview.Reload();
            }
            catch (Exception exception)
            {
                Dialog.ShowError(exception);
            }
        }

        #region Candidate interview
        protected void CandidateDetail(object sender, DirectEventArgs e)
        {
            if (!string.IsNullOrEmpty(hdfId.Text))
            {
                Response.Redirect(CandidateInterviewManagementUrl + "?interviewId=" + hdfId.Text, true);
            }
        }

        protected void CandidateDelete(object sender, DirectEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(hdfId.Text))
                {
                    CandidateInterviewController.DeleteByCondition(Convert.ToInt32(hdfId.Text));
                }
            }
            catch (Exception exception)
            {
                Dialog.ShowError(exception);
            }
        }
        #endregion

        #region Import Excel
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DownloadTemplate_Click(object sender, DirectEventArgs e)
        {
            // init table
            var dataTable = new DataTable();

            // adjust table
            dataTable.Rows.Add();
            dataTable.Columns.Add(new DataColumn(EmployeeCode));
            dataTable.Columns.Add(new DataColumn(FullName));
            dataTable.Columns.Add(new DataColumn(TimeInterview));
            
            // get record
            var records = CandidateController.GetAll(null, null, null, CandidateType.Interview, null, null, false, null, null);

            // fill employee name and code
            for (var i = 0; i < records.Count; i++)
            {
                dataTable.Rows.Add();
                dataTable.Rows[i][EmployeeCode] = records[i].Code;
                dataTable.Rows[i][FullName] = records[i].FullName;
            }

            ExportToExcel(dataTable, "~/" + Constant.PathTemplate, ImportCandidateInterviewExcelFile);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdateImportExcel_Click(object sender, DirectEventArgs e)
        {
            var workbook = new WorkBook();

            // upload file

            if (fileExcel.HasFile)
            {
                var path = UploadFile(fileExcel, Constant.PathTemplate);
                if (path != null)
                {
                    // Read data from excel
                    workbook.readXLSX(Path.Combine(Server.MapPath("~/"), Constant.PathTemplate, path));

                    // Check validation workbook
                    if (CheckValidation(workbook, out _, out _, txtFromRow, txtToRow, txtSheetName) == false)
                    {
                        return;
                    }

                    // Export to datatable
                    var dataTable = workbook.ExportDataTable(0, //first row
                        0, //first col
                        workbook.LastRow + 1, //last row
                        workbook.LastCol + 1, //last col
                        true, //first row as header
                        false //convert to DateTime object if it match date pattern
                    );

                    var interview = InterviewController.GetById(Convert.ToInt32(hdfId.Text));
                    var interviewDate = new DateTime();
                    if (interview != null)
                    {
                        interviewDate = interview.InterviewDate;
                    }

                    foreach (DataRow row in dataTable.Rows)
                    {
                        //get employee code
                        var employeeCode = row[EmployeeCode].ToString();
                        if (string.IsNullOrEmpty(employeeCode)) continue;

                        // get record by employee code
                        var candidate = CandidateController.GetByCode(employeeCode, CandidateType.Interview);
                        //check if value exists
                        var candidateInterviewModel = CandidateInterviewController.GetUnique(Convert.ToInt32(hdfId.Text), candidate.RecordId);
                        if (candidateInterviewModel != null)
                        {
                            //update value
                            EditDataToModel(dataTable, row, candidateInterviewModel, interviewDate);
                            candidateInterviewModel.EditedBy = CurrentUser.User.UserName;
                            candidateInterviewModel.EditedDate = DateTime.Now;
                            CandidateInterviewController.Update(candidateInterviewModel);
                        }
                        else
                        {
                            var model = new CandidateInterviewModel()
                            {
                                RecordId = candidate.RecordId,
                                InterviewId = Convert.ToInt32(hdfId.Text),
                                EditedBy = CurrentUser.User.UserName,
                                EditedDate = DateTime.Now,
                                CreatedBy = CurrentUser.User.UserName,
                                CreatedDate = DateTime.Now
                            };

                            EditDataToModel(dataTable, row, model, interviewDate);
                            //create
                            CandidateInterviewController.Create(model);
                        }
                    }
                }
            }
            else
            {
                Dialog.Alert("Bạn chưa chọn tệp tin đính kèm. Vui lòng chọn.");
                return;
            }

            Dialog.Alert("Cập nhật thành công");
            //Reset form excel
            fileExcel.Reset();
            txtSheetName.Reset();
            //close window
            wdImportExcelFile.Hide();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="row"></param>
        /// <param name="model"></param>
        /// <param name="interviewDate"></param>
        private static void EditDataToModel(DataTable dataTable, DataRow row, CandidateInterviewModel model,
            DateTime interviewDate)
        {
            foreach (DataColumn col in dataTable.Columns)
            {
                switch (col.ColumnName)
                {
                    case EmployeeCode:
                        break;
                    case FullName:
                        break;
                    default:
                        //check empty string
                        var value = Convert.ToString(row[col], CultureInfo.InvariantCulture);
                        if (string.IsNullOrEmpty(value)) continue;

                        var timeHourStr = value.Substring(0, 2);
                        var timeMinuteStr = value.Substring(3, 2);
                        model.TimeInterview = new DateTime(interviewDate.Year, interviewDate.Month, interviewDate.Day, Convert.ToInt32(timeHourStr), Convert.ToInt32(timeMinuteStr), 0);
                        break;
                }
            }
        }

        #endregion
    }
}