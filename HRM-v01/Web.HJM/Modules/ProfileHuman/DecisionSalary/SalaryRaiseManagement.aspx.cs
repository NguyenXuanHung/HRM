using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Ext.Net;
using SoftCore;
using Web.Core;
using Web.Core.Framework;
using Web.Core.Framework.Common;
using Web.Core.Framework.Report;
using Web.Core.Object.HumanRecord;
using Web.Core.Object.Salary;
using Web.Core.Service.Catalog;
using Web.Core.Service.HumanRecord;
using Web.Core.Service.Salary;

namespace Web.HJM.Modules.ProfileHuman.DecisionSalary
{
    public partial class SalaryRaiseManagement : BasePage
    {
        private const int SalaryBase = 1300000;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ExtNet.IsAjaxRequest)
            {
                hdfSalaryType.Text = Request.QueryString["salaryType"];
                if (!string.IsNullOrEmpty(hdfSalaryType.Text) && hdfSalaryType.Text == SalaryDecisionType.Regular.ToString())
                {
                    //Nang luong
                    hdfSalaryRaiseType.Text = ((int)SalaryDecisionType.Regular).ToString();
                }else if (!string.IsNullOrEmpty(hdfSalaryType.Text) && hdfSalaryType.Text == SalaryDecisionType.OutFrame.ToString())
                {
                    //Vuot khung
                    hdfSalaryRaiseType.Text = ((int)SalaryDecisionType.OutFrame).ToString();
                }
                else
                {
                    hdfSalaryRaiseType.Text = @"0"; //Default
                }

                hdfDepartments.Text = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
                
            }
        }

        /// <summary>
        /// báo cáo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ShowRiseSalaryReport(object sender, DirectEventArgs e)
        {
            if (!string.IsNullOrEmpty(hdfSalaryType.Text) && hdfSalaryType.Text == SalaryDecisionType.Regular.ToString())
            {
                //Nang luong
                RM.RegisterClientScriptBlock("fd", "#{TabPanel1}.remove(0);addHomePage(#{TabPanel1},'Homepage','../../Report/ReportView.aspx?rp=SalaryIncrease', 'Báo cáo danh sách cán bộ công chức đến kỳ nâng lương');");
            }
            if (!string.IsNullOrEmpty(hdfSalaryType.Text) && hdfSalaryType.Text == SalaryDecisionType.OutFrame.ToString())
            {
                //Vuot khung
                RM.RegisterClientScriptBlock("fd", "#{TabPanel1}.remove(0);addHomePage(#{TabPanel1},'Homepage','../../Report/ReportView.aspx?rp=SalaryOutOfFrame', 'Báo cáo danh sách cán bộ đến hạn xét vượt khung');");
            }
           
            var rp = new Filter
            {
                CreatedByName = CurrentUser.User.DisplayName,
                Departments = string.Join(",", CurrentUser.Departments.Select(d => d.Id)),
                ReportDate = DateTime.Now
            };
            Session.Add("rp", rp);
            wpReport.Show();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cbxSalaryGradeStore_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            if (!string.IsNullOrEmpty(hdfQuantumHLId.Text))
            {
                var quantum = cat_QuantumServices.GetById(Convert.ToInt32(hdfQuantumHLId.Text));
                var quantumGrade = cat_GroupQuantumServices.GetFieldValueById(quantum.GroupQuantumId, "GradeNumberMax");
                var grade = Convert.ToInt32(quantumGrade);
                hdfSalaryGradeHL.Text = grade.ToString();
                var objs = new List<StoreComboxObject>();
                for (var i = 1; i <= grade; i++)
                {
                    var stob = new StoreComboxObject
                    {
                        MA = i.ToString(),
                        TEN = "Bậc " + i
                    };
                    objs.Add(stob);
                }

                cbxSalaryGradeStore.DataSource = objs;
                cbxSalaryGradeStore.DataBind();
            }
        }

        [DirectMethod]
        public void GetSalaryInfo()
        {
            if (!string.IsNullOrEmpty(hdfQuantumHLId.Text) && !string.IsNullOrEmpty(hdfSalaryGradeHL.Text))
            {
                var quantum = cat_QuantumServices.GetById(Convert.ToInt32(hdfQuantumHLId.Text));
                var salaryFactor = SalaryLevelQuantumController.GetSalaryFactor(quantum.GroupQuantumId,
                    Convert.ToInt32(hdfSalaryGradeHL.Text));
                txtSalaryFactor.SetValue(salaryFactor);
                txtSalaryLevel.SetValue(Math.Round(salaryFactor * SalaryBase).ToString("##,###"));
            }
            else
            {
                txtSalaryFactor.Text = @"0";
                txtSalaryLevel.Text = @"0";
            }
        }

        /// <summary>
        /// get list employee
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ListEmployee_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            var salaryIds = "0";
            foreach (var item in chkSelectionModel.SelectedRows)
            {
                salaryIds += "," + item.RecordID;
            }
            var salaries = sal_SalaryDecisionServices.GetAll("[Id] IN ({0})".FormatWith(salaryIds.TrimStart(',')));
            var recordIds = string.Join(",", salaries.Select(d => d.RecordId));
            hdfRecordIds.Text = recordIds;
            var listEmployee =
                RecordController.GetAll("[Id] IN ({0})".FormatWith(!string.IsNullOrEmpty(recordIds) ? recordIds : "0"));
            gridListEmployee_Store.DataSource = listEmployee;
            gridListEmployee_Store.DataBind();
        }

        /// <summary>
        /// save DB
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, DirectEventArgs e)
        {
            if (!string.IsNullOrEmpty(hdfRecordIds.Text))
            {
                // upload file
                var path = string.Empty;
                if (cpfAttachHL.Visible && fufAttachFile.HasFile)
                {
                    path = UploadFile(fufAttachFile, Constant.PathDecisionSalary);
                }
                var recordIds = string.IsNullOrEmpty(hdfRecordIds.Text) ? new string[] { }
                    : hdfRecordIds.Text.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var item in recordIds)
                {
                    var salary = new sal_SalaryDecision()
                    {
                        RecordId = Convert.ToInt32(item),
                        Type = !string.IsNullOrEmpty(hdfSalaryRaiseType.Text) ? (SalaryDecisionType) Enum.Parse(typeof(SalaryDecisionType),hdfSalaryRaiseType.Text) : (int) SalaryDecisionType.Regular,
                        DecisionStatus = SalaryDecisionStatus.Approved,
                        AttachFileName = path,
                        CreatedBy = CurrentUser.User.UserName,
                        CreatedDate = DateTime.Now
                    };
                    //Edit data salary
                    EditData(salary);

                    //create
                    sal_SalaryDecisionServices.Create(salary);
                }
            }

            wdDecision.Hide();
            gridListEmployee.Reload();
            RM.RegisterClientScriptBlock("Grid_Reload", "ReloadGrid();");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="salaryDecision"></param>
        private void EditData(sal_SalaryDecision salaryDecision)
        {
            var util = new Util();
            salaryDecision.DecisionNumber = txtDecisionNumber.Text;
            salaryDecision.Name = txtDecisionName.Text;
            if (!util.IsDateNull(dfDecisionDate.SelectedDate))
                salaryDecision.DecisionDate = dfDecisionDate.SelectedDate;
            if (!util.IsDateNull(dfEffectiveDate.SelectedDate))
                salaryDecision.EffectiveDate = dfEffectiveDate.SelectedDate;
            //if (!util.IsDateNull(dfSalaryRaiseNextDate.SelectedDate))
            //    salaryDecision.SalaryRaiseNextDate = dfSalaryRaiseNextDate.SelectedDate;
            //if (!util.IsDateNull(dfProRaiseSalaryDisDate.SelectedDate))
            //    salaryDecision.ProlongRaiseSalaryDisciplineDate = dfProRaiseSalaryDisDate.SelectedDate;
            salaryDecision.QuantumId = !string.IsNullOrEmpty(hdfQuantumHLId.Text) ? Convert.ToInt32(hdfQuantumHLId.Text) : 0;
            salaryDecision.SignerPosition = cbxMakerPositionHL.SelectedItem.Text;
            salaryDecision.SignerName = txtDecisionMaker.Text;
            salaryDecision.Note = txtNote.Text;
            //salaryDecision.SalaryFactor =
            //    !string.IsNullOrEmpty(txtSalaryFactor.Text) ? Convert.ToDouble(txtSalaryFactor.Text.Replace(".", ",")) : 0;
            //salaryDecision.OutFrame = !string.IsNullOrEmpty(txtOutFrame.Text) ? Convert.ToDouble(txtOutFrame.Text) : 0;
            //salaryDecision.SalaryInsurance = !string.IsNullOrEmpty(txtSalaryInsurance.Text) ? Convert.ToDouble(txtSalaryInsurance.Text) : 0;
            //salaryDecision.SalaryGradeLift = txtSalaryLiftGrade.Text;
            //salaryDecision.SalaryBasic = !string.IsNullOrEmpty(txtSalaryLevel.Text) ? Convert.ToDouble(txtSalaryLevel.Text) : 0;
            //salaryDecision.Reason = txtReason.Text;
        }

        /// <summary>
        /// upload file from computer to server
        /// </summary>
        /// <param name="sender">ID of FileUploadField</param>
        /// <param name="relativePath">the relative path to place you want to save file</param>
        /// <returns>The path of file after upload to server</returns>
        public string UploadFile(object sender, string relativePath)
        {
            var obj = (FileUploadField)sender;
            var file = obj.PostedFile;
            var dir = new DirectoryInfo(Server.MapPath(relativePath)); // save file to directory
            // if directory not exist then create this
            if (dir.Exists == false)
                dir.Create();
            var rdstr = SoftCore.Util.GetInstance().GetRandomString(7);
            var path = Server.MapPath(relativePath) + "/" + rdstr + "_" + obj.FileName;
            if (File.Exists(path))
                return "";
            file.SaveAs(path);
            return relativePath + "/" + rdstr + "_" + obj.FileName;
        }

        [DirectMethod]
        public void ResetForm()
        {
            hdfRecordIds.Reset();
            txtDecisionNumber.Reset();
            txtDecisionName.Reset();
            txtDecisionMaker.Reset();
            hdfQuantumHLId.Reset();
            hdfMakerPositionHL.Reset();
            cbxSalaryGrade.Clear();
            cbxQuantum.Clear();
            hdfSalaryGradeHL.Reset();
            dfSalaryRaiseNextDate.Reset();
            dfDecisionDate.Reset();
            dfEffectiveDate.Reset();
            dfProRaiseSalaryDisDate.Reset();
            dfSalaryPayDate.Reset();
            txtNote.Reset();
            txtOutFrame.Reset();
            txtSalaryLevel.Reset();
            txtSalaryFactor.Reset();
            txtSalaryLiftGrade.Reset();
            txtSalaryInsurance.Reset();
            txtOtherAllowance.Reset();
            txtPositionAllowance.Reset();
            txtReason.Reset();
        }
    }
}