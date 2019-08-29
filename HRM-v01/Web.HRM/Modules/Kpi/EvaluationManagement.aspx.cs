using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using SmartXLS;
using Web.Core;
using Web.Core.Framework;

namespace Web.HRM.Modules.Kpi
{
    public partial class EvaluationManagement : BasePage
    {
        private const string EmployeeArgumentManagementUrl = @"~/Modules/Kpi/EmployeeArgumentManagement.aspx?groupId={0}&month={1}&year={2}";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ExtNet.IsAjaxRequest)
            {
                // get month
                if(!string.IsNullOrEmpty(Request.QueryString["month"]))
                {
                    hdfMonthFilter.Text = Request.QueryString["month"];
                    cboMonthFilter.Text = @"Tháng " + Request.QueryString["month"];
                }
                else
                {
                    hdfMonthFilter.Text = DateTime.Now.Month.ToString();
                    cboMonthFilter.Text = @"Tháng " + DateTime.Now.Month;
                }

                // get year
                if(!string.IsNullOrEmpty(Request.QueryString["year"]))
                {
                    hdfYearFilter.Text = Request.QueryString["year"];
                    spnYearFilter.SetValue(Request.QueryString["year"]);
                }
                else
                {
                    hdfYearFilter.Text = DateTime.Now.Year.ToString();
                    spnYearFilter.SetValue(DateTime.Now.Year);
                }
                // get status
                hdfStatus.Text = ((int)KpiStatus.Active).ToString();

                // get group
                var groupId = Request.QueryString["groupId"];
                if(!string.IsNullOrEmpty(groupId))
                {
                    var group = GroupKpiController.GetById(int.Parse(groupId));
                    if(group != null)
                    {
                        cboGroupFilter.Text = group.Name;
                        hdfGroupFilter.Text = group.Id.ToString();
                    }
                }
                else
                {
                    var groups = GroupKpiController.GetAll(null, false, KpiStatus.Active, null, null);
                    if(groups.Count > 0)
                    {
                        cboGroupFilter.Text = groups.First().Name;
                        hdfGroupFilter.Text = groups.First().Id.ToString();
                    }
                }

                hdfDepartment.Text = DepartmentIds;
                new Core.Framework.Common.BorderLayout()
                {
                    menuID = MenuId,
                    script = "#{hdfDepartmentSelectedId}.setValue('" + Core.Framework.Common.BorderLayout.nodeID +
                             "');#{txtKeyword}.reset();#{pagingToolbar}.pageIndex = 0; #{pagingToolbar}.doLoad();"
                }.AddDepartmentList(brlayout, CurrentUser, false);

                // load grid
                LoadGridPanel();
            }
        }

        protected void ReloadGridColumn(object sender, DirectEventArgs e)
        {
            // reload data
            gpEvaluation.Reload();

            // load grid
            LoadGridPanel();
        }


        private void LoadGridPanel()
        {
            // reconfigure
            gpEvaluation.Reconfigure();

            // add new column
            AddColumn();
        }

        private void AddColumn()
        {
            // remove fields
            storeEvaluation.RemoveFields();

            // column index
            var colIndex = new RowNumbererColumn
            {
                ColumnID = "colIndex",
                Header = "STT",
                Align = Alignment.Center,
                Width = 40
            };
            gpEvaluation.AddColumn(colIndex);

            // column employee code
            var colEmployeeCode = new Column
            {
                ColumnID = "colEmployeeCode",
                Header = "Mã nhân viên",
                DataIndex = "EmployeeCode",
                Align = Alignment.Left,
                Width = 100
            };
            var recordFieldEmployeeCode = new RecordField()
            {
                Name = "EmployeeCode",
                Mapping = "EmployeeCode"
            };
            storeEvaluation.AddField(recordFieldEmployeeCode);
            gpEvaluation.AddColumn(colEmployeeCode);

            // init column employee name
            var colFullName = new Column
            {
                ColumnID = "colFullName",
                Header = "Họ và tên",
                DataIndex = "FullName",
                Align = Alignment.Left,
                Width = 150
            };
            var recordFieldFullName = new RecordField()
            {
                Name = "FullName",
                Mapping = "FullName"
            };
            storeEvaluation.AddField(recordFieldFullName);
            gpEvaluation.AddColumn(colFullName);

            //init column departmentName
            var colDepartmentName = new Column
            {
                ColumnID = "colDepartmentName",
                Header = "Đơn vị",
                DataIndex = "DepartmentName",
                Align = Alignment.Left,
                Width = 150
            };
            var recordFieldDepartmentName = new RecordField()
            {
                Name = "DepartmentName",
                Mapping = "DepartmentName"
            };
            storeEvaluation.AddField(recordFieldDepartmentName);
            gpEvaluation.AddColumn(colDepartmentName);

            // get criterion in group
            var criterionGroups = CriterionGroupController.GetAll(null, null, Convert.ToInt32(hdfGroupFilter.Text), null, null);

            // init count
            var count = 0;

            // add kpi criterion column
            foreach (var item in criterionGroups)
            {
                // init field
                var recordField = new RecordField()
                {
                    Name = item.CriterionCode,
                    Mapping = "CriterionDetailModels[{0}].Display".FormatWith(count++)
                };

                // init column
                var col = new Column
                {
                    ColumnID = recordField.Name,
                    Header = item.CriterionName,
                    DataIndex = recordField.Name,
                    Align = Alignment.Center,
                    Width = 150,
                    Renderer = { Fn = "RenderCriterion" }
                };
                
                // add field
                storeEvaluation.AddField(recordField);

                // add column
                gpEvaluation.AddColumn(col);
            }
        }

        /// <summary>
        /// delete data by criterionGroupId
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Delete(object sender, DirectEventArgs e)
        {
            try
            {
                //delete
                EvaluationController.DeleteByCondition(null, null);
                //reload 
                gpEvaluation.Reload();
            }
            catch (Exception exception)
            {
                Dialog.ShowError(exception);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InsertEvaluationByGroup(object sender, DirectEventArgs e)
        {
            try
            {
                if(!string.IsNullOrEmpty(hdfGroupFilter.Text))
                {
                    var criterionModels = CriterionController.GetAll(null, Convert.ToInt32(hdfGroupFilter.Text), false,
                        KpiStatus.Active, null, null, null);
                    //create new all employee
                    var records = RecordController.GetAll(null, null, DepartmentIds, RecordType.Default, null, null);
                    foreach(var criterion in criterionModels)
                    {
                        foreach(var item in records)
                        {
                            var model = new EvaluationModel()
                            {
                                RecordId = item.Id,
                                CriterionId = criterion.Id,
                                Month = DateTime.Now.Month,
                                Year = DateTime.Now.Year,
                                Value = ""
                            };

                            //get value
                            GetValueCriterionWorkbook(model, criterion, Convert.ToInt32(hdfGroupFilter.Text));

                            //check exist
                            var evaluation = EvaluationController.CheckExist(model.RecordId, model.CriterionId, model.Month, model.Year);
                            if(evaluation != null)
                            {
                                model.Id = evaluation.Id;
                                //update
                                EvaluationController.Update(model);
                            }
                            else
                            {
                                //create
                                EvaluationController.Create(model);
                            }
                        }
                    }
                    //hide window
                    wdEvaluation.Hide();
                    // reload grid
                    gpEvaluation.Reload();
                }
            }
            catch(Exception exception)
            {
                Dialog.ShowError(exception);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InitWindowEvaluation(object sender, DirectEventArgs e)
        {
            if(string.IsNullOrEmpty(hdfGroupFilter.Text)) return;

            var groupKpi = GroupKpiController.GetById(int.Parse(hdfGroupFilter.Text));

            if(groupKpi == null) return;

            // set window name
            wdEvaluation.Title += " " + groupKpi.Name;
            // reload grid
            gpCriterionEvaluation.Reload();
            // show window
            wdEvaluation.Show();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AddKpiClick(object sender, DirectEventArgs e)
        {
            Response.Redirect(EmployeeArgumentManagementUrl.FormatWith(hdfGroupFilter.Text, hdfMonthFilter.Text, hdfYearFilter.Text) + "&mId=" + MenuId, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="criterion"></param>
        /// <param name="groupId"></param>
        private static void GetValueCriterionWorkbook(EvaluationModel model, CriterionModel criterion, int? groupId)
        {
            var empArgument =
                EmployeeArgumentController.GetAll(null, null, groupId, model.RecordId, model.Month, model.Year, null, null);
            var workbook = new WorkBook { AutoCalc = false };
            foreach(var argument in empArgument)
            {
                switch(argument.ValueType)
                {
                    case KpiValueType.String:
                        workbook.setText($"{argument.ArgumentCalculateCode}1", argument.Value);
                        break;
                    case KpiValueType.Number:
                        workbook.setNumber($"{argument.ArgumentCalculateCode}1",
                            double.TryParse(argument.Value, out var parseResultNumber) ? parseResultNumber : 0);
                        break;
                    case KpiValueType.Percent:
                        workbook.setFormula($"{argument.ArgumentCalculateCode}1",
                            double.TryParse(argument.Value, out var parseResultPercent) ? $"{parseResultPercent} * 0.01" : "0");
                        break;
                    case KpiValueType.Formula:
                        workbook.setFormula($"{argument.ArgumentCalculateCode}1", argument.Value.TrimStart('='));
                        break;
                }
            }

            workbook.setFormula("A1", criterion.Formula.TrimStart('='));

            workbook.recalc();

            switch(criterion.ValueType)
            {
                case KpiValueType.Number:
                    model.Value = workbook.getNumber("A1").ToString("#,##0.##");
                    break;
                case KpiValueType.Percent:
                    model.Value = (workbook.getNumber("A1") * 100).ToString("0.00");
                    break;
                default:
                    model.Value = workbook.getText("A1");
                    break;
            }
        }
    }
}