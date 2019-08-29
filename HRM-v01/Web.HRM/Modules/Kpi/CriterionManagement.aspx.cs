using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using SmartXLS;
using Web.Core;
using Web.Core.Framework;
using Icon = Ext.Net.Icon;

namespace Web.HRM.Modules.Kpi
{
    public partial class CriterionManagement : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!ExtNet.IsAjaxRequest)
            {
                hdfOrder.Text = Request.QueryString["order"];
            }
        }

        #region Event Methods

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
                if(e.ExtraParams["Command"] == "Update")
                {
                    // edit
                    wdSetting.Title = @"Cập nhật tiêu chí đánh giá";
                    wdSetting.Icon = Icon.Pencil;
                    var model = CriterionController.GetById(Convert.ToInt32(hdfId.Text));
                    if(model != null)
                    {
                        // set props
                        txtCode.Text = model.Code;
                        txtFormula.Text = model.Formula;
                        txtOrder.Text = model.Order.ToString();
                        cboValueType.Text = model.ValueTypeName;
                        hdfValueType.Text = ((int)model.ValueType).ToString();
                        txtName.Text = model.Name;
                        txtDescription.Text = model.Description;
                        if(model.Status == KpiStatus.Active)
                        {
                            chkIsActive.Checked = true;
                        }
                        hdfFormulaRange.Text = model.FormulaRange;
                    }
                }
                else
                {
                    // insert
                    wdSetting.Title = @"Thêm mới tiêu chí đánh giá";
                    wdSetting.Icon = Icon.Add;
                }

                // show window
                wdSetting.Show();
            }
            catch(Exception exception)
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
                var model = new CriterionModel();

                // check id
                if(!string.IsNullOrEmpty(hdfId.Text) && Convert.ToInt32(hdfId.Text) > 0)
                {
                    var result = CriterionController.GetById(Convert.ToInt32(hdfId.Text)); ;
                    if(result != null)
                        model = result;
                }

                // set new props for entity
                model.Code = txtName.Text.ToUpperString();
                model.ValueType = (KpiValueType)Enum.Parse(typeof(KpiValueType), hdfValueType.Text);
                model.Name = txtName.Text;
                model.Description = txtDescription.Text;
                model.Formula = txtFormula.Text;
                model.Order = !string.IsNullOrEmpty(txtOrder.Text) ? Convert.ToInt32(txtOrder.Text) : 0;
                model.Status = chkIsActive.Checked ? KpiStatus.Active : KpiStatus.Locked;
                model.FormulaRange = hdfFormulaRange.Text;

                // check entity id
                if(model.Id > 0)
                {
                    model.EditedDate = DateTime.Now;
                    model.EditedBy = CurrentUser.User.UserName;
                    // update
                    CriterionController.Update(model);
                }
                else
                {
                    model.CreatedBy = CurrentUser.User.UserName;
                    model.CreatedDate = DateTime.Now;
                    model.EditedDate = DateTime.Now;
                    model.EditedBy = "";
                    // insert
                    CriterionController.Create(model);
                }
                // hide window
                wdSetting.Hide();

                //reset form
                ResetForm();
                // reload data
                gpCriterion.Reload();
            }
            catch(Exception exception)
            {
                Dialog.ShowError(exception);
            }
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
                if(!string.IsNullOrEmpty(hdfId.Text))
                {
                    //delete
                    CriterionController.Delete(Convert.ToInt32(hdfId.Text));

                    //delete criterion in group
                    CriterionGroupController.DeleteByCondition(null, Convert.ToInt32(hdfId.Text));
                }

                // reload data
                gpCriterion.Reload();
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
        protected void storeValueType_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            storeValueType.DataSource = typeof(KpiValueType).GetIntAndDescription();
            storeValueType.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void EvaluationClick(object sender, DirectEventArgs e)
        {
            try
            {
                if(!string.IsNullOrEmpty(hdfId.Text))
                {
                    //create new all employee
                    var records = RecordController.GetAll(null, null, DepartmentIds, RecordType.Default, null, null);
                    var criterion = CriterionController.GetById(Convert.ToInt32(hdfId.Text));

                    foreach(var item in records)
                    {
                        var model = new EvaluationModel()
                        {
                            RecordId = item.Id,
                            CriterionId = Convert.ToInt32(hdfId.Text),
                            Month = DateTime.Now.Month,
                            Year = DateTime.Now.Year,
                            Value = ""
                        };

                        //get value
                        GetValueCriterionWorkbook(model, criterion, null);

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
        protected void txtCriterionValue_OnChange(object sender, DirectEventArgs e)
        {
            txtValue1.Text = txtCriterionValue.Text;
            txtValue2.Text = txtCriterionValue.Text;
            txtValue3.Text = txtCriterionValue.Text;
            txtValue4.Text = txtCriterionValue.Text;
            txtValue5.Text = txtCriterionValue.Text;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnFormula_Click(object sender, DirectEventArgs e)
        {
            // init
            var count = 1;
            var criterionFormulas = new List<CriterionFormulaModel>();

            foreach(Control control in ctnFormula.Controls)
            {
                if(!(control is CompositeField)) continue;
                // get composite field
                if(control.ID != $"CompositeField{count}") continue;

                var criterionFormula = new CriterionFormulaModel();

                foreach(Control ctrl in control.Controls)
                {
                    if(ctrl.ID == $"txtSmaller{count}")
                    {
                        if(decimal.TryParse(((NumberField)ctrl).Text, out var smaller))
                            criterionFormula.Smaller = smaller;
                    }
                    if(ctrl.ID == $"txtValue{count}")
                    {
                        criterionFormula.Value = ((TextField)ctrl).Text;
                    }
                    if(ctrl.ID == $"txtGreater{count}")
                    {
                        if(decimal.TryParse(((NumberField)ctrl).Text, out var greater))
                            criterionFormula.Greater = greater;
                    }
                    if(ctrl.ID == $"txtResult{count}")
                    {
                        criterionFormula.Result = ((TextField)ctrl).Text;
                    }
                    if(ctrl.ID == $"dropDownField{count}")
                    {
                        criterionFormula.Color = $"#{((DropDownField)ctrl).Text}";
                    }
                }

                count++;
                criterionFormulas.Add(criterionFormula);

                if(criterionFormula.Greater == null) break;
            }

            // set formula
            txtFormula.Text = CreateFormula(criterionFormulas);
            // save to hidden field
            hdfFormulaRange.Text = JSON.Serialize(criterionFormulas);
            // hide window
            wdFormula.Hide();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddRange_Click(object sender, DirectEventArgs e)
        {
            if(!Enum.TryParse<KpiValueType>(hdfValueType.Text, out var result))
            {
                Dialog.ShowError("Chọn loại dữ liệu");
                return;
            }

            //var numberRegex = new Regex(@"/^(\(?\+?[0-9]*\)?)?[0-9_\- \(\)]*$/");

            if(string.IsNullOrEmpty(hdfFormulaRange.Text))
            {
                wdFormula.Show();
                return;
            }
            // get formula range
            var formulaRanges = JSON.Deserialize<List<CriterionFormulaModel>>(hdfFormulaRange.Text);

            // fill data to form
            var count = 1;
            txtCriterionValue.Text = formulaRanges[0].Value;
            foreach(Control control in ctnFormula.Controls)
            {
                // get composite field
                if(!(control is CompositeField)) continue;

                if(control.ID != $"CompositeField{count}") continue;

                if(formulaRanges.Count < count) break;

                foreach(Control ctrl in control.Controls)
                {
                    if(ctrl.ID == $"txtSmaller{count}")
                    {
                        ((NumberField)ctrl).SetValue(formulaRanges[count - 1].Smaller.ToString());
                    }
                    if(ctrl.ID == $"txtValue{count}")
                    {
                        ((TextField)ctrl).Text = formulaRanges[count - 1].Value;
                    }
                    if(ctrl.ID == $"txtGreater{count}")
                    {
                        ((NumberField)ctrl).SetValue(formulaRanges[count - 1].Greater.ToString());
                    }
                    if(ctrl.ID == $"dropDownField{count}")
                    {
                        ((DropDownField)ctrl).Text = formulaRanges[count - 1].Color.Remove(0, 1);
                    }
                    if(ctrl.ID == $"txtResult{count}")
                    {
                        ((TextField)ctrl).Text = formulaRanges[count - 1].Result;
                    }
                }
                count++;
            }

            wdFormula.Show();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtGreater_OnChange(object sender, DirectEventArgs e)
        {
            var id = e.ExtraParams["Id"];
            if(int.TryParse(id, out var result))
            {
                switch(result)
                {
                    case 1:
                        txtSmaller2.Text = txtGreater1.Text;
                        break;
                    case 2:
                        txtSmaller3.Text = txtGreater2.Text;
                        break;
                    case 3:
                        txtSmaller4.Text = txtGreater3.Text;
                        break;
                    case 4:
                        txtSmaller5.Text = txtGreater4.Text;
                        break;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cboCriterionValue_OnSelect(object sender, DirectEventArgs e)
        {
            // add selected value to text field
            txtCriterionValue.Text += cboCriterionValue.Text + " ";
            txtCriterionValue_OnChange(sender, e);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Tạo công thức
        /// </summary>
        /// <param name="criterionFormulas"></param>
        /// <returns></returns>
        private string CreateFormula(List<CriterionFormulaModel> criterionFormulas)
        {
            var condition = "IF({0},{1},{2})";
            var formula = condition;

            if(criterionFormulas == null || criterionFormulas.Count == 0) return string.Empty;

            for(var i = 0; i < criterionFormulas.Count - 1; i++)
            {
                if(i == 0)
                {
                    formula = formula.FormatWith($"{criterionFormulas[i].Value}<{criterionFormulas[i].Greater}", $"\"{criterionFormulas[i].Result}\"", criterionFormulas.Count > 2 ? condition : $"\"{criterionFormulas[i + 1].Result}\"");
                }
                else if(i == criterionFormulas.Count - 2)
                {
                    formula = formula.FormatWith($"AND({criterionFormulas[i].Value}>={criterionFormulas[i].Smaller}, {criterionFormulas[i].Value}<{criterionFormulas[i].Greater})", $"\"{criterionFormulas[i].Result}\"", $"\"{criterionFormulas[i + 1].Result}\"");
                }
                else
                {
                    formula = formula.FormatWith($"AND({criterionFormulas[i].Value}>={criterionFormulas[i].Smaller}, {criterionFormulas[i].Value}<{criterionFormulas[i].Greater})", $"\"{criterionFormulas[i].Result}\"", condition);
                }
            }

            return $"={formula}";
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

        /// <summary>
        /// 
        /// </summary>
        private void ResetForm()
        {
            txtName.Reset();
            txtDescription.Reset();
            chkIsActive.Checked = false;
            txtFormula.Reset();
            txtOrder.Reset();
            hdfValueType.Reset();
            cboValueType.Clear();
            ResetFormulaForm();
        }

        /// <summary>
        /// 
        /// </summary>
        private void ResetFormulaForm()
        {
            txtCriterionValue.Reset();
            hdfFormulaRange.Reset();
            cboCriterionValue.Reset();
            foreach(Control control in ctnFormula.Controls)
            {
                if(!(control is CompositeField)) continue;

                foreach(Control ctrl in control.Controls)
                {
                    switch(ctrl)
                    {
                        case TextField text:
                            text.Reset();
                            break;
                        case NumberField number:
                            number.Reset();
                            break;
                        case DropDownField dropDownField:
                            dropDownField.Reset();
                            break;
                        default:
                            continue;
                    }

                }
            }
        }

        #endregion
    }
}