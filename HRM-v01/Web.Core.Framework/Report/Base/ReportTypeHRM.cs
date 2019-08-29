using System.ComponentModel;

namespace Web.Core.Framework.Report
{
    public enum ReportTypeHRM
    {
        #region Regulation

        [Description("Khai trình lao động")]
        LabourList,

        [Description("Khai trình tăng lao động")]
        LabourIncrease,

        [Description("Khai trình giảm lao động")]
        LabourDecrease,

        [Description("Báo cáo nhân sự")]
        EmployeeList,

        [Description("Báo cáo tăng nhân sự")]
        EmployeeIncrease,

        [Description("Báo cáo giảm nhân sự")]
        EmployeeDecrease,

        [Description("Báo cáo điều động nhân sự")]
        EmployeeTransferred,

        [Description("Báo báo biệt phái nhân sự")]
        EmployeeSent,

        [Description("Báo cáo bổ nhiệm nhân sự")]
        EmployeeAssigned,

        [Description("Báo cáo thâm niên công tác")]
        EmployeeSeniority,

        [Description("Báo cáo nhân sự nghỉ hưu")]
        EmployeeRetired,

        #endregion

        #region Internal business 

        [Description("Báo cáo danh sách đảng viên")]
        PartyMember,

        [Description("Báo cáo danh sách đoàn viên")]
        UnionMember,

        [Description("Báo cáo danh sách quân nhân")]
        MilitaryList,

        [Description("Báo cáo danh sách hợp đồng của nhân viên")]
        ContractsOfEmployee,

        [Description("Báo các danh sách hết hạn hợp đồng")]
        EmployeeExpired,

        [Description("Báo cáo danh sách đang thử việc")]
        EmployeeTrainee,

        [Description("Báo cáo danh sách đóng BHXH")]
        InsuranceList,

        [Description("Báo cáo danh sách tăng BHXH")]
        InsurancIncrease,

        [Description("Báo cáo danh sách giảm BHXH")]
        InsuranceDecrease,

        [Description("Báo cáo thống kê nghề nghiệp")]
        OccupationStatistics,

        [Description("Báo cáo danh sách sinh nhật trong tháng")]
        BornInMonth,

        [Description("Báo cáo danh sách các con được quà 1/6")]
        ChildernDayGift,

        [Description("Báo cáo danh sách nhân sự là nữ")]
        EmployeeFemale,

        [Description("Báo cáo danh sách tiền lương")]
        EmployeeSalary,

        [Description("Báo cáo diễn biến quá trình lương")]
        SalaryIncreaseProcess,

        [Description("Báo cáo danh sách đến kỳ tăng lương")]
        SalaryIncrease,

        [Description("Báo cáo danh sách cán bộ đến hạn xét vượt khung")]
        SalaryOutOfFrame,

        [Description("Báo cáo danh sách tài khoản ngân hàng")]
        EmployeeBank,

        [Description("Báo cáo danh sách mã số thuế")]
        EmployeeTaxCode,

        [Description("Báo cáo danh sách người phụ thuộc")]
        DependentPerson,

        [Description("Báo cáo danh sách được cử đi đào tạo")]
        EmployeeTraining,

        [Description("Báo cáo danh sách đi công tác")]
        EmployeeOnsite,

        [Description("Báo cáo danh sách được nhận danh hiệu thi đua")]
        EmployeeReceivedAward,

        [Description("Báo cáo danh sách được khen thưởng")]
        EmployeeRewarded,

        [Description("Báo cáo danh sách bị kỷ luật")]
        EmployeeDisciplined,

        #endregion

        #region TimeKeeping
        [Description("Báo cáo tổng hợp công")]
        TotalTimeKeeping,

        [Description("Báo cáo chi tiết chấm công")]
        TimeKeepingDetail,

        [Description("Báo cáo chi tiết tăng ca")]
        TimeKeepingOverTimeDetail,

        #endregion
    }
}
