using System;
using System.Collections.Generic;
using Ext.Net;
using Web.Core.Framework;
using Web.Core.Service.Catalog;
using Web.Core.Object.Catalog;
using Web.Core;

namespace Web.HJM.Modules.Catalog
{
    /// <summary>
    /// Danh mục mức lương ngạch
    /// </summary>
    public partial class SalaryLevelQuantum : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                var maxSalaryGrade = cat_GroupQuantumServices.getMaxSalaryGrade();
                hdfMax.Text = maxSalaryGrade.ToString();
                var salary = CatalogBasicSalaryController.GetCurrent();
                if (salary != null)
                    hdfLuongCoBan.Text = salary.Value.ToString();

                var grid = GridPanel1;
                var store = grid.GetStore();
                var cm = grid.ColumnModel;

                Renderer f = new Renderer {Fn = "RenderCap"};

                for (var i = 0; i < maxSalaryGrade; i++)
                {
                    var k = i + 1;
                    // add to store
                    store.Reader[0].Fields.Add("Bac" + k);
                    var column = new Column()
                    {
                        ColumnID = "Bac" + k,
                        Header = "Bậc " + k,
                        Width = 100,
                        DataIndex = "Bac" + k,
                        Align = Alignment.Right,
                        Renderer = f
                    };
                    cm.Columns.Add(column);
                }
            }
        }

        #region combobox refresh data

        public void cbxBacStore_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            if (string.IsNullOrEmpty(hdfGroupQuantumId.Text)) return;
            var nn = cat_GroupQuantumServices.GetById(Convert.ToInt32(hdfGroupQuantumId.Text));
            if (nn == null) return;
            var objs = new List<StoreComboxObject>();
            for (var i = 1; i <= nn.GradeMax; i++)
            {
                var stob = new StoreComboxObject
                {
                    MA = i.ToString(),
                    TEN = "Bậc " + i
                };
                objs.Add(stob);

            }
            cbxBacStore.DataSource = objs;
            cbxBacStore.DataBind();
        }

        #endregion

        #region direct event

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void mnuThemMoi_Click(object sender, DirectEventArgs e)
        {
            try
            {
                var sm = GridPanel1.SelectionModel.Primary as CellSelectionModel;
                var groupQuantumId = 0;
                if (!string.IsNullOrEmpty(sm.SelectedCell.RecordID))
                    groupQuantumId = Convert.ToInt32(sm.SelectedCell.RecordID);
                var tmpBac = sm.SelectedCell.Name;
                if (tmpBac.StartsWith("Bac"))
                {
                    var bac = int.Parse(tmpBac.Replace("Bac", "").Trim());
                    var groupQuantum = GetSalaryGroupQuantumByCondition(groupQuantumId, null, bac);
                    if (groupQuantum != null)
                    {
                        var quantumName = cat_GroupQuantumServices.GetFieldValueById(groupQuantum.Id);
                        X.Msg.Alert("Thông báo từ hệ thống", "Ngạch: " + quantumName + ", bậc " + groupQuantum.SalaryGrade + " đã có giá trị").Show();
                        return;
                    }
                    hdfGroupQuantumId.Text = groupQuantumId.ToString();
                    cbxNhomNgach.Text = cat_GroupQuantumServices.GetFieldValueById(groupQuantumId);
                    hdfBac.Text = bac.ToString();
                    cbxBac.Text = "Bậc " + bac;
                }
                wdThemMoiMucLuongNgach.Show();
            }
            catch (Exception ex)
            {
                X.Msg.Alert("Thông báo từ hệ thống", "Có lỗi xảy ra: " + ex.Message).Show();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCapNhat_Click(object sender, DirectEventArgs e)
        {
            try
            {
                if (e.ExtraParams["Command"] != "Edit")
                {
                    var groupQuantumId = 0;
                    var grade = 0;
                    if (!string.IsNullOrEmpty(hdfGroupQuantumId.Text))
                        groupQuantumId = Convert.ToInt32(hdfGroupQuantumId.Text);
                    if (!string.IsNullOrEmpty(hdfBac.Text))
                        grade = Convert.ToInt32(hdfBac.Text);
                    var groupQuantum = GetSalaryGroupQuantumByCondition(groupQuantumId, null, grade);
                    if (groupQuantum != null)
                    {
                        X.Msg.Alert("Thông báo từ hệ thống", "Ngạch và bậc " + groupQuantum.SalaryGrade + " đã có dữ liệu. Bạn không thể thêm mới dữ liệu khác").Show();
                        return;
                    }
                }

                var salaryController = new CatalogGroupQuantumGradeController();
                var salary = new cat_GroupQuantumGrade();
                if (!string.IsNullOrEmpty(hdfGroupQuantumId.Text))
                    salary.GroupQuantumId = Convert.ToInt32(hdfGroupQuantumId.Text);
                salary.SalaryLevel = txtMucLuong.Text;
                if (!string.IsNullOrEmpty(txtHeSoLuong.Text))
                    salary.SalaryFactor = Convert.ToDecimal(txtHeSoLuong.Text.Replace('.', ','));
                if (!string.IsNullOrEmpty(hdfBac.Text))
                    salary.SalaryGrade = Convert.ToInt32(hdfBac.Text);
                salary.Description = txtGhiChu.Text;
                salary.CreatedDate = DateTime.Now;

                if (e.ExtraParams["Command"] == "Edit")
                {
                    if (!string.IsNullOrEmpty(hdfRecordId.Text))
                        salary.Id = Convert.ToInt32(hdfRecordId.Text);
                    salaryController.Update(salary);
                    Dialog.ShowNotification("Cập nhật dữ liệu thành công");
                    wdThemMoiMucLuongNgach.Hide();
                }
                else
                {
                    if (cat_GroupQuantumServices.checkOutFrame(salary.GroupQuantumId, salary.SalaryGrade) == false)
                    {
                        Dialog.ShowError("Bậc bạn chọn đã vượt quá số bậc tối đa của ngạch");
                        return;
                    }
                    salaryController.Insert(salary);
                    Dialog.ShowNotification("Thêm mới dữ liệu thành công!");
                    if (e.ExtraParams["Close"] == "True")
                    {
                        wdThemMoiMucLuongNgach.Hide();
                    }
                    else
                    {
                        RM.RegisterClientScriptBlock("rs1", "ResetWdThemMucLuongNgach();");
                    }
                }
                GridPanel1.Reload();
            }
            catch (Exception ex)
            {
                X.Msg.Alert("Thông báo từ hệ thống", "Có lỗi xảy ra: " + ex.Message).Show();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupQuantumId"></param>
        /// <param name="quantumId"></param>
        /// <param name="grade"></param>
        /// <returns></returns>
        private cat_GroupQuantumGrade GetSalaryGroupQuantumByCondition(int? groupQuantumId, int? quantumId, int? grade)
        {
            var condition = " 1=1 ";
            if (!string.IsNullOrEmpty(groupQuantumId.ToString()))
            {
                condition += @" AND [GroupQuantumId] = '{0}' ".FormatWith(groupQuantumId);
            }
            if (!string.IsNullOrEmpty(quantumId.ToString()))
            {
                condition += @" AND [QuantumId] = '{0}' ".FormatWith(quantumId);
            }
            if (!string.IsNullOrEmpty(grade.ToString()))
            {
                condition += @" AND [SalaryGrade] = '{0}' ".FormatWith(grade);
            }
            var groupQuantum = cat_GroupQuantumGradeServices.GetByCondition(condition);
            return cat_GroupQuantumGradeServices.GetByCondition(condition);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEditQuantum_Click(object sender, DirectEventArgs e)
        {
            try
            {
                var sm = GridPanel1.SelectionModel.Primary as CellSelectionModel;
                var groupQuantumId = Convert.ToInt32(sm.SelectedCell.RecordID);
                var tmpBac = sm.SelectedCell.Name;
                if (tmpBac.StartsWith("Bac"))
                {
                    var bac = int.Parse(tmpBac.Replace("Bac", "").Trim());

                    var groupQuantum = GetSalaryGroupQuantumByCondition(groupQuantumId, null, bac);
                    if (groupQuantum == null)
                    {
                        X.Msg.Alert("Thông báo từ hệ thống", "Không tìm thấy dữ liệu").Show();
                        return;
                    }

                    hdfRecordId.Text = groupQuantum.Id.ToString();
                    hdfGroupQuantumId.Text = groupQuantum.GroupQuantumId.ToString();
                    cbxNhomNgach.Text = cat_GroupQuantumServices.GetFieldValueById(groupQuantum.GroupQuantumId);
                    hdfBac.Text = groupQuantum.SalaryGrade.ToString();
                    cbxBac.Text = "Bậc " + groupQuantum.SalaryGrade.ToString();
                    txtHeSoLuong.SetValue(groupQuantum.SalaryFactor);
                    txtMucLuong.SetValue(groupQuantum.SalaryLevel);
                    txtGhiChu.Text = groupQuantum.Description;

                    wdThemMoiMucLuongNgach.Title = "Sửa thông tin lương của ngạch";
                    wdThemMoiMucLuongNgach.Icon = Icon.Pencil;
                    wdThemMoiMucLuongNgach.Show();
                }
                else
                {
                    Dialog.ShowNotification("Thông tin này không được phép thay đổi");
                }
            }
            catch (Exception ex)
            {
                X.Msg.Alert("Thông báo từ hệ thống", "Có lỗi xảy ra: " + ex.Message).Show();
            }
        }

        protected void btnDeleteQuantum_Click(object sender, DirectEventArgs e)
        {
            try
            {
                var sm = GridPanel1.SelectionModel.Primary as CellSelectionModel;
                var groupQuantumId = Convert.ToInt32(sm.SelectedCell.RecordID);
                var tmpBac = sm.SelectedCell.Name;
                if (tmpBac.StartsWith("Bac"))
                {
                    var bac = int.Parse(tmpBac.Replace("Bac", "").Trim());
                    cat_GroupQuantumGrade groupQuantumGrade = GetSalaryGroupQuantumByCondition(groupQuantumId, null, bac);
                    if (groupQuantumGrade == null)
                    {
                        X.Msg.Alert("Thông báo từ hệ thống", "Không tìm thấy dữ liệu").Show();
                        return;
                    }
                    cat_GroupQuantumGradeServices.Delete(groupQuantumGrade.Id);
                    var quantumName = cat_GroupQuantumServices.GetFieldValueById(groupQuantumGrade.GroupQuantumId);
                    Dialog.ShowNotification("Đã xóa dữ liệu của ngạch: " + quantumName + ", bậc : " + groupQuantumGrade.SalaryGrade);
                    GridPanel1.Reload();
                }
                else
                {
                    Dialog.ShowNotification("Dữ liệu này không thể xóa!");
                }
            }
            catch (Exception ex)
            {
                X.Msg.Alert("Thông báo từ hệ thống", "Có lỗi xảy ra: " + ex.Message).Show();
            }
        }
        #endregion

        #region DirectMethods

        /// <summary>
        /// 
        /// </summary>
        public void ResetWindowTitle()
        {
            wdThemMoiMucLuongNgach.Title = "Thêm mới thông tin lương cho ngạch";
            wdThemMoiMucLuongNgach.Icon = Icon.Add;
        }
        #endregion
    }
}

