using Ext.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Web.Core.Framework;
using Web.Core.Framework.Controller;
using Web.Core.Framework.Controller.TimeSheet;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.Catalog;
using Web.Core.Service.HumanRecord;
using Web.Core.Service.Salary;
using Web.Core.Service.TimeSheet;

namespace Web.HJM.Modules.Salary
{
    public partial class SalaryBoardDynamic : BasePage
    {
        private const int ReduceFamily = 3600000;
        private const int ReducePersonal = 9000000;
        private const int SalaryProbationPercentage = 85;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ExtNet.IsAjaxRequest)
            {
                hdfType.Text = Request.QueryString["TimeSheetHandlerType"];
                // init department
                storeDepartment.DataSource = CurrentUser.DepartmentsTree;
                storeDepartment.DataBind();

                hdfDepartments.Text = string.Join(",", CurrentUser.Departments.Select(d => d.Id));
                hdfMonth.Text = DateTime.Now.Month.ToString();
                hdfYear.Text = DateTime.Now.Year.ToString();
                cbxMonth.SetValue(DateTime.Now.Month);
                spnYear.SetValue(DateTime.Now.Year);
            }
        }

        protected void CreateSalaryBoardList_Click(object sender, DirectEventArgs e)
        {
            try
            {
                var salaryBoard = new hr_SalaryBoardList();
                if (!string.IsNullOrEmpty(hdfDepartmentId.Text))
                    salaryBoard.DepartmentId = Convert.ToInt32(hdfDepartmentId.Text);
                if (!string.IsNullOrEmpty(hdfMonth.Text))
                    salaryBoard.Month = Convert.ToInt32(hdfMonth.Text);
                if (!string.IsNullOrEmpty(hdfYear.Text))
                    salaryBoard.Year = Convert.ToInt32(hdfYear.Text);
                salaryBoard.Title = txtTitleSalaryBoard.Text;
                salaryBoard.Description = txtDesciptionSalaryBoard.Text;
                salaryBoard.CreatedDate = DateTime.Now;
                salaryBoard.CreatedBy = CurrentUser.User.UserName;

                hr_SalaryBoardListServices.Create(salaryBoard);
                ResetFormSalaryList();
                grpSalaryBoardList.Reload();
            }
            catch (Exception ex)
            {
                Dialog.ShowNotification("Có lỗi xảy ra" + ex.Message);
            }
        }

        [DirectMethod]
        private void ResetFormSalaryList()
        {
            cbxDepartment.Text = "";
            hdfDepartmentId.Reset();
            txtTitleSalaryBoard.Text = "";
            txtDesciptionSalaryBoard.Text = "";
            hdfMonth.Text = DateTime.Now.Month.ToString();
            cbxMonth.Text = DateTime.Now.Month.ToString();
            hdfYear.Text = DateTime.Now.Year.ToString();
            spnYear.Text = DateTime.Now.Year.ToString();
        }
        public void UpdateSalaryBoardList_Click(object sender, DirectEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(hdfSalaryBoardListID.Text))
                {
                    var salaryBoard = hr_SalaryBoardListServices.GetById(Convert.ToInt32(hdfSalaryBoardListID.Text));
                    if (salaryBoard != null)
                    {
                        if (!string.IsNullOrEmpty(hdfDepartmentId.Text))
                            salaryBoard.DepartmentId = Convert.ToInt32(hdfDepartmentId.Text);
                        if (!string.IsNullOrEmpty(hdfMonth.Text))
                            salaryBoard.Month = Convert.ToInt32(hdfMonth.Text);
                        if (!string.IsNullOrEmpty(hdfYear.Text))
                            salaryBoard.Year = Convert.ToInt32(hdfYear.Text);
                        salaryBoard.Title = txtTitleSalaryBoard.Text;
                        salaryBoard.Description = txtDesciptionSalaryBoard.Text;
                        salaryBoard.EditedDate = DateTime.Now;

                        hr_SalaryBoardListServices.Update(salaryBoard);
                        grpSalaryBoardList.Reload();
                    }
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowNotification("Có lỗi xảy ra" + ex.Message);
            }
        }

        protected void btnDeleteSalaryBoardList_Click(object sender, DirectEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(hdfSalaryBoardListID.Text))
                {
                    //Xoa bang luong dong
                    hr_SalaryBoardDynamicServices.DeleteByBoardListId(hdfSalaryBoardListID.Text);
                    //Xoa trong danh sach bang luong
                    hr_SalaryBoardListServices.Delete(Convert.ToInt32(hdfSalaryBoardListID.Text));
                    grpSalaryBoardList.Reload();
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowNotification("Có lỗi xảy ra" + ex.Message);
            }
        }

        [DirectMethod]
        public void btnEditSalaryBoardList_Click()
        {
            try
            {
                if (!string.IsNullOrEmpty(hdfSalaryBoardListID.Text))
                {
                    var salary = hr_SalaryBoardListServices.GetById(Convert.ToInt32(hdfSalaryBoardListID.Text));
                    if (salary != null)
                    {
                        txtTitleSalaryBoard.Text = salary.Title;
                        txtDesciptionSalaryBoard.Text = salary.Description;
                        hdfMonth.Text = salary.Month.ToString();
                        cbxMonth.Text = salary.Month.ToString();
                        hdfYear.Text = salary.Year.ToString();
                        spnYear.Text = salary.Year.ToString();
                        hdfDepartmentId.Text = salary.DepartmentId.ToString();
                        cbxDepartment.Text = cat_DepartmentServices.GetFieldValueById(salary.DepartmentId, "Name");
                    }
                    wdCreateSalaryBoardList.SetTitle("Cập nhật bảng tính lương");
                    btnUpdateEditSalaryBoard.Show();
                    btnCreateSalaryBoard.Hide();
                    wdCreateSalaryBoardList.Show();
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowNotification("Có lỗi xảy ra" + ex.Message);
            }
        }

        /// <summary>
        /// Tạo bảng tính lương
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ChooseSalaryBoardList_Click(Object sender, DirectEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(hdfSalaryBoardListID.Text))
                {
                    hdfSalaryBoardId.Text = hdfSalaryBoardListID.Text;
                    //Lay danh sach bang luong
                    var boardList = hr_SalaryBoardListServices.GetById(Convert.ToInt32(hdfSalaryBoardListID.Text));
                    if (boardList != null)
                    {
                        //TH ton tai bang luong roi thi hien thi
                        //TH chua ton tai bang luong thi di tao moi bang luong
                        var salaryBoard = hr_SalaryBoardDynamicServices.GetSalaryBoardDynamicByBoardId(boardList.Id);
                        if (salaryBoard == null)
                        {
                            //Lay danh sach nhan vien cua don vi can tinh bang luong
                            var listRecordId = TimeSheetCodeController.GetAll(null, null, boardList.DepartmentId, null,
                                null, true, null, null, null, null);
                            foreach (var item in listRecordId)
                            {
                                //Tao moi bang tinh luong
                                var salaryBoardDynamic = new hr_SalaryBoardDynamic();
                                salaryBoardDynamic.SalaryBoardId = boardList.Id;
                                //Lay ID bang cham cong 
                                //var timeSheet = hr_TimeSheetReportServices.GetTimeSheetReport(boardList.DepartmentId,
                                //    boardList.Month, boardList.Year, hdfType.Text);
                                //if (timeSheet != null)
                                //    salaryBoardDynamic.TimeSheetReportId = timeSheet.Id;
                                salaryBoardDynamic.RecordId = item.RecordId;
                                salaryBoardDynamic.CreatedBy = CurrentUser.User.UserName;
                                salaryBoardDynamic.CreatedDate = DateTime.Now;
                                hr_SalaryBoardDynamicServices.Create(salaryBoardDynamic);
                            }
                        }
                        gridSalaryInfo.Reload();
                    }
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowNotification("Có lỗi xảy ra" + ex.Message);
            }
        }

        /// <summary>
        /// Lấy công cho tất cả nhân viên
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GetMenuAllEmployee(Object sender, DirectEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(hdfSalaryBoardListID.Text))
                {
                    //Lay danh sach bang luong
                    var boardList = hr_SalaryBoardListServices.GetById(Convert.ToInt32(hdfSalaryBoardListID.Text));
                    if (boardList != null)
                    {
                        //Lấy công chuẩn
                        var standardWork = 0.0;
                        //var timeReport = hr_TimeSheetReportServices.GetTimeSheetReport(boardList.DepartmentId, boardList.Month, boardList.Year, hdfType.Text);
                        //if (timeReport != null)
                        //    standardWork = timeReport.WorkInMonth;

                        // Lấy bảng lương động 
                        var salaryBoard = hr_SalaryBoardDynamicServices.GetSalaryBoardDynamicList(boardList.Id);
                        foreach (var item in salaryBoard)
                        {
                            //Lấy mã chấm công
                            GetTimeSheetCodeForEmployee(item);
                            //Công chuẩn
                            item.WorkStandard = standardWork;
                            //Cập nhật vào bảng lương động
                            hr_SalaryBoardDynamicServices.Update(item);
                        }
                        gridSalaryInfo.Reload();
                    }
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowNotification("Có lỗi xảy ra" + ex.Message);
            }
        }

        

        /// <summary>
        /// Lấy công cho từng nhân viên
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GetDataForSelectedEmployee_Click(Object sender, DirectEventArgs e)
        {
            try
            {
                var selecteds = RowSelectionModel1.SelectedRows;
                foreach (var item in selecteds)
                {
                    var boardDynamicId = int.Parse(item.RecordID);
                    //Lấy thông tin bảng lương động
                    var salaryBoard = hr_SalaryBoardDynamicServices.GetById(boardDynamicId);
                    if (salaryBoard != null)
                    {
                        //Lấy mã chấm công
                        GetTimeSheetCodeForEmployee(salaryBoard);
                    }
                    //Cập nhật vào bảng lương động
                    hr_SalaryBoardDynamicServices.Update(salaryBoard);
                }
                gridSalaryInfo.Reload();
            }
            catch (Exception ex)
            {
                Dialog.ShowNotification("Có lỗi xảy ra" + ex.Message);
            }
        }

        /// <summary>
        /// Tính lương cho tất cả nhân viên
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CaculateAllEmployee(Object sender, DirectEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(hdfSalaryBoardListID.Text))
                {
                    //Lay danh sach bang luong
                    var boardList = hr_SalaryBoardListServices.GetById(Convert.ToInt32(hdfSalaryBoardListID.Text));
                    if (boardList != null)
                    {
                        // Lấy bảng lương động 
                        var salaryBoard = hr_SalaryBoardDynamicServices.GetSalaryBoardDynamicList(boardList.Id);
                        foreach (var item in salaryBoard)
                        {
                            CaculateSalaryForEmployee(item);
                            //Cập nhật vào bảng lương động
                            hr_SalaryBoardDynamicServices.Update(item);
                        }
                        gridSalaryInfo.Reload();
                    }
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowNotification("Có lỗi xảy ra" + ex.Message);
            }
        }

        /// <summary>
        /// Xử lý tính lương
        /// </summary>
        /// <param name="item"></param>
        private void CaculateSalaryForEmployee(hr_SalaryBoardDynamic item)
        {
            try
            {
                //Lấy thông tin lương theo từng RecordId
                var salary = hr_SalaryServices.GetSalaryByRecordId(item.RecordId);
                if (salary != null)
                {
                    item.SalaryBasic = salary.SalaryBasic;
                    item.SalaryNet = salary.SalaryNet;
                    item.SalaryGross = salary.SalaryGross;
                    item.SalaryContract = salary.SalaryContract;
                    item.PercentagePaySalary = salary.PercentageSalary;
                    //Mức lương
                    if (item.SalaryBasic > 0)
                    {
                        item.SalaryLevel = AddNumber(AddNumber(item.SalaryBasic, item.ResponsibilityAllowance),
                            item.OtherAllowance);
                    }
                    else
                    {
                        if (item.SalaryContract > 0)
                        {
                            item.SalaryLevel = item.SalaryContract;
                        }
                        else
                        {
                            if (item.SalaryNet > 0)
                                item.SalaryLevel = item.SalaryNet;
                            else
                                item.SalaryLevel = item.SalaryGross;
                        }
                    }
                    //Mức lương thử việc
                    if (item.PercentagePaySalary != null && item.PercentagePaySalary == SalaryProbationPercentage)
                        item.SalaryProbationLevel = item.SalaryContract;
                    //else
                    //    item.SalaryProbationLevel = 0;

                    //Lương theo ngày công
                    if (item.WorkStandard > 0)
                        item.SalaryWorkDay = item.SalaryLevel / item.WorkStandard;
                    //Lương theo ngày công thử việc
                    if (item.WorkStandard > 0 && item.PercentagePaySalary != null)
                        item.SalaryWorkProbation = item.SalaryProbationLevel / item.WorkStandard * item.PercentagePaySalary / 100;
                    //Tổng lương theo ngày công thử việc
                    if (item.WorkProbation != null)
                        item.TotalSalaryWorkProbation = item.SalaryWorkProbation * item.WorkProbation;
                    //Tổng lương theo ngày công
                    var totalSalaryWork = item.SalaryWorkDay * item.WorkPaySalary;
                    if (item.TaxArrears != null)
                        totalSalaryWork += item.TaxArrears;
                    if (item.TaxOffset != null)
                        totalSalaryWork -= item.TaxOffset;
                    if (item.SalaryWorkProbation != null)
                        totalSalaryWork += item.SalaryWorkProbation;
                    item.TotalSalaryWorkDay = totalSalaryWork;

                    //Tổng thu nhập
                    item.TotalIncome = AddNumber(item.MoneyOverTime, item.TotalSalaryWorkDay);
                    //BH
                    double? salaryBasic = item.SalaryBasic;
                    if (item.ResponsibilityAllowance != null)
                        salaryBasic += item.ResponsibilityAllowance;
                    if (salaryBasic != null)
                    {
                        double? perSocial = salaryBasic * 8 / 100;
                        double? perHealth = salaryBasic * 1.5 / 100;
                        double? perVoluntary = salaryBasic * 1 / 100;
                        double? departmentSocial = salaryBasic * 17.5 / 100;
                        double? departmentHealth = salaryBasic * 3 / 100;
                        double? departmentVoluntary = salaryBasic * 1 / 100;
                        //Tính tổng 10,5% BH cho người lao động
                        //BHXH 8%
                        item.PersonalSocialInsurance = perSocial;
                        //BHYT 1,5%
                        item.PersonalHealthInsurance = perHealth;
                        //BHTN 1/%
                        item.PersonalVoluntaryInsurance = perVoluntary;
                        item.TotalInsurance = AddNumber(AddNumber(perSocial, perHealth), perVoluntary);
                        //Tính tổng 21,5% BH đơn vị đóng
                        //BHXH 17,5%
                        item.DepartmentSocialInsurance = departmentSocial;
                        //BHYT 3%
                        item.DepartmentHealthInsurance = departmentHealth;
                        //BHTN 1%
                        item.DepartmentVoluntaryInsurance = departmentVoluntary;
                        item.TotalDepartmentInsurance = AddNumber(AddNumber(departmentSocial, departmentHealth),
                            departmentVoluntary);
                    }
                    //Thu nhập sau BHXH
                    item.TotalAfterSocialInsurance = AddNumber(item.TotalIncome, -item.TotalInsurance);
                }

                //Lấy số người phụ thuộc theo từng RecordId
                item.DependenceNumber = hr_FamilyRelationshipServices.GetDependenceNumber(item.RecordId);
                item.ReduceFamily = item.DependenceNumber * ReduceFamily;
                if (item.SalaryContract > ReducePersonal)
                    item.ReducePersonal = ReducePersonal;
                if (item.SalaryBasic != null && item.SalaryBasic > 0 || item.SalaryContract != null && item.SalaryContract > 0)
                    //Tổng thu nhập chịu thuế
                    item.TotalIncomeTax = AddNumber(AddNumber(item.TotalAfterSocialInsurance, -item.ReduceFamily), -item.ReducePersonal);
                //Tính thuế thu nhập cá nhân
                CaculatePersonalIncomeTax(item);
                //Thuế TNCN (10% thử việc)
                if (item.TotalSalaryWorkProbation != null)
                    item.ProbationIncomeTax = item.TotalSalaryWorkProbation * 0.1;
                //Thuế TNCN (10% cộng tác)
                if (item.SalaryNet != null)
                    item.CooperationIncomeTax = item.SalaryNet * 0.1;
                //Tổng thuế
                item.TotalPersonalIncomeTax = AddNumber(AddNumber(item.PersonalIncomeTax, item.ProbationIncomeTax), item.CooperationIncomeTax);
                //Thực lãnh chuyển khoản
                item.ActualTransfer = AddNumber(
                    AddNumber(
                        AddNumber(
                            AddNumber(
                                AddNumber(
                                    AddNumber(
                                        AddNumber(AddNumber(item.TotalIncome, -item.TotalInsurance),
                                            -item.TotalPersonalIncomeTax), -item.OtherRevenue), item.Expense),
                                item.SalaryAddition), -item.SalaryAdvance), item.NonTaxOffset),
                    -item.NonTaxArrears);
                //Lấy ATMNumber cho từng nhân viên
                var bank = hr_BankServices.GetBankByRecordId(item.RecordId);
                if (bank != null && !string.IsNullOrEmpty(bank.AccountNumber))
                    item.ATMNumber = double.Parse(bank.AccountNumber);
            }
            catch (Exception ex)
            {
                Dialog.ShowNotification("Có lỗi xảy ra" + ex.Message);
            }
        }

        /// <summary>
        /// Tính lương cho những nhân viên được chọn
        /// </summary>
        protected void CaculateSelectedEmployee_Click(Object sender, DirectEventArgs e)
        {
            try
            {
                var selecteds = RowSelectionModel1.SelectedRows;
                foreach (var item in selecteds)
                {
                    var boardDynamicId = int.Parse(item.RecordID);
                    //Lấy thông tin bảng lương động
                    var salaryBoard = hr_SalaryBoardDynamicServices.GetById(boardDynamicId);
                    if (salaryBoard != null)
                    {
                        CaculateSalaryForEmployee(salaryBoard);
                        //Cập nhật vào bảng lương động
                        hr_SalaryBoardDynamicServices.Update(salaryBoard);
                    }
                }
                gridSalaryInfo.Reload();
            }
            catch (Exception ex)
            {
                Dialog.ShowNotification("Có lỗi xảy ra" + ex.Message);
            }
        }

        /// <summary>
        /// Tính thuế TNCN
        /// </summary>
        /// <param name="item"></param>
        private void CaculatePersonalIncomeTax(hr_SalaryBoardDynamic item)
        {
            try
            {
                if (item.TotalIncomeTax == null || !(item.TotalIncomeTax > 0)) return;
                if (item.TotalIncomeTax <= 5000000)
                {
                    item.PersonalIncomeTax = item.TotalIncomeTax * 0.05;
                }
                else if (item.TotalIncomeTax > 5000000 && item.TotalIncomeTax <= 10000000)
                {
                    item.PersonalIncomeTax = AddNumber(item.TotalIncomeTax * 0.1, -250000);
                }
                else if (item.TotalIncomeTax > 10000000 && item.TotalIncomeTax <= 18000000)
                {
                    item.PersonalIncomeTax = AddNumber(item.TotalIncomeTax * 0.15, -750000);
                }
                else if (item.TotalIncomeTax > 18000000 && item.TotalIncomeTax <= 32000000)
                {
                    item.PersonalIncomeTax = AddNumber(item.TotalIncomeTax * 0.2, -1650000);
                }
                else if (item.TotalIncomeTax > 32000000 && item.TotalIncomeTax <= 52000000)
                {
                    item.PersonalIncomeTax = AddNumber(item.TotalIncomeTax * 0.25, -3250000);
                }
                else if (item.TotalIncomeTax > 52000000 && item.TotalIncomeTax <= 80000000)
                {
                    item.PersonalIncomeTax = AddNumber(item.TotalIncomeTax * 0.3, -5850000);
                }
                else if (item.TotalIncomeTax > 80000000)
                {
                    item.PersonalIncomeTax = AddNumber(item.TotalIncomeTax * 0.35, -9850000);
                }
                else
                {
                    item.PersonalIncomeTax = 0;
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowNotification("Có lỗi xảy ra" + ex.Message);
            }
        }

        protected void btnAcceptAdjustment_Click(object sender, DirectEventArgs e)
        {
            try
            {
                //Điều chỉnh cho những nhân viên được chọn
                if (chkApplySelectedEmployee.Checked)
                {
                    if (!string.IsNullOrEmpty(hdfSelectColumnId.Text))
                    {
                        //Lấy cấu hình bảng lương
                        var boardConfig = hr_SalaryBoardConfigServices.GetById(Convert.ToInt32(hdfSelectColumnId.Text));
                        if (boardConfig != null)
                        {
                            var selecteds = RowSelectionModel1.SelectedRows;
                            foreach (var item in selecteds)
                            {
                                var boardDynamicId = int.Parse(item.RecordID);
                                //Lấy thông tin bảng lương động
                                var salaryBoard = hr_SalaryBoardDynamicServices.GetById(boardDynamicId);
                                if (salaryBoard != null)
                                {
                                    //set value cho bang luong dong
                                    SetValueColumnSalaryBoardDynamic(salaryBoard, boardConfig);
                                    //Cập nhật vào bảng lương động
                                    hr_SalaryBoardDynamicServices.Update(salaryBoard);
                                }
                            }
                            gridSalaryInfo.Reload();
                        }
                    }
                }

                //Điều chỉnh cho tất cả nhân viên
                if (chkApplyForAll.Checked)
                {
                    if (!string.IsNullOrEmpty(hdfSelectColumnId.Text))
                    {
                        //Lấy cấu hình bảng lương
                        var boardConfig = hr_SalaryBoardConfigServices.GetById(Convert.ToInt32(hdfSelectColumnId.Text));
                        if (boardConfig != null)
                        {
                            if (!string.IsNullOrEmpty(hdfSalaryBoardListID.Text))
                            {
                                //Lay danh sach bang luong
                                var boardList = hr_SalaryBoardListServices.GetById(Convert.ToInt32(hdfSalaryBoardListID.Text));
                                if (boardList != null)
                                {
                                    // Lấy bảng lương động 
                                    var salaryBoard = hr_SalaryBoardDynamicServices.GetSalaryBoardDynamicList(boardList.Id);
                                    foreach (var item in salaryBoard)
                                    {
                                        //set value cho bang luong dong
                                        SetValueColumnSalaryBoardDynamic(item, boardConfig);

                                        //Cập nhật vào bảng lương động
                                        hr_SalaryBoardDynamicServices.Update(item);
                                    }
                                }
                            }
                            gridSalaryInfo.Reload();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowNotification("Có lỗi xảy ra" + ex.Message);
            }
        }

        private void SetValueColumnSalaryBoardDynamic(hr_SalaryBoardDynamic item, hr_SalaryBoardConfig boardConfig)
        {
            try
            {
                //get property item by class 
                var property = item.GetType().GetProperties().FirstOrDefault(c => c.Name == boardConfig.ColumnCode);
                if (property != null)
                {
                    var typeName = property.PropertyType.Name;
                    if (typeName == "String")
                    {
                        //convert string to type property
                        var changeType = Convert.ChangeType(txtValueAdjustment.Text,
                            typeof(String));
                        //set value to property
                        property.SetValue(item, changeType, null);
                    }
                    else
                    {
                        //Truong hop gia tri nhap la chuoi thi default 0
                        double result;
                        if (!double.TryParse(txtValueAdjustment.Text, out result) || result < 0)
                        {
                            txtValueAdjustment.Text = "0";
                        }
                        //convert string to type property
                        var changeType = Convert.ChangeType(txtValueAdjustment.Text,
                            Nullable.GetUnderlyingType(property.PropertyType));
                        //set value to property
                        property.SetValue(item, changeType, null);
                    }
                }
            }
            catch (Exception ex)
            {
                Dialog.ShowNotification("Có lỗi xảy ra" + ex.Message);
            }
        }

        protected void cbxSelectColumnStore_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            try
            {
                cbxSelectColumnStore.DataSource = SalaryBoardConfigController.GetAll();
                cbxSelectColumnStore.DataBind();
            }
            catch (Exception ex)
            {
                Dialog.ShowNotification("Có lỗi xảy ra" + ex.Message);
            }
        }

        private double? AddNumber(double? number1, double? number2)
        {
            double? total = 0;
            if (number1 != null)
            {
                total = number1;
            }
            if (number2 != null)
                total = total + number2;
            return total;
        }
    }

}

