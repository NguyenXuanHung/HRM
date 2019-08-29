using System.ComponentModel;

namespace Web.Core.Framework.Report
{
    public enum ReportTypeHJM
    {
        #region Regulation

        [Description("Số lượng, chất lượng cán bộ, công chức cấp huyện")]
        QuantityDistrictCivilServants,
        
        [Description("Số lượng, chất lượng cán bộ, công chức cấp xã")]
        QuantityCommuneCivilServants,

        [Description("Danh sách và tiền lương cán bộ, công chức cấp huyện")]
        SalaryDistrictCivilServants,

        [Description("Danh sách và tiền lương cán bộ, công chức cấp xã")]
        SalaryCommuneCivilServants,

        [Description("Số lượng, chất lượng người làm việc trong các đơn vị sự nghiệp")]
        QuantityOfEmployee,

        [Description("Số lượng, chất lượng người làm việc là nữ")]
        QuantityFemaleEmployee,

        [Description("Số lượng, chất lượng người làm việc là người dân tộc thiểu số")]
        QuantityEthnicMinority,

        [Description("Số lượng, chất lượng biên chế sự nghiệp")]
        QuantityStaff,

        [Description("Số lượng, chất lượng cán bộ, công chức cấp huyện chi tiết")]
        QuantityDistrictCivilServantsDetail,

        [Description("Báo cáo danh sách cán bộ quản lý, giáo viên và nhân viên trong các đơn vị trường học")]
        ListEmployeeByPosition,

        #endregion

        #region Business
        [Description("Báo cáo nhân sự")]
        EmployeeList,

        [Description("Báo cáo danh sách cán bộ được điều động đến")]
        EmployeeMoveTo,

        [Description("Báo cáo danh sách cán bộ được luân chuyển đi")]
        EmployeeMoveFrom,

        [Description("Báo cáo danh sách cán bộ được luân chuyển đến")]
        EmployeeTurnoverTo,

        [Description("Báo cáo danh sách cán bộ được luân chuyển đi")]
        EmployeeTurnoverFrom,

        [Description("Báo cáo danh sách cán bộ được biệt phái đến")]
        EmployeeSecondmentTo,

        [Description("Báo cáo danh sách cán bộ được cử đi biệt phái")]
        EmployeeSecondmentFrom,

        [Description("Báo cáo danh sách cán bộ được kiêm nhiệm")]
        EmployeePlurality,

        [Description("Báo cáo danh sách cán bộ được miễn nhiệm, bãi nhiệm")]
        EmployeeDismissment,

        [Description("Báo cáo danh sách cán bộ được thuyên chuyển, điều chuyển")]
        EmployeeTranfer,

        [Description("Báo cáo thâm niên công tác của cán bộ")]
        EmployeeSeniority,

        [Description("Báo cáo danh sách cán bộ được hưởng chính sách")]
        EmployeeCompensation,

        [Description("Báo cáo danh sách cán bộ nghỉ hưu")]
        EmployeeRetirement,
        [Description("Báo cáo danh sách Đảng viên")]
        PartyMember,

        [Description("Báo cáo danh sách hợp đồng của nhân viên")]
        ContractsOfEmployee,

        [Description("Báo cáo danh sách hết hạn hợp đồng")]
        EmployeeExpired,

        [Description("Báo cáo bổ nhiệm nhân sự")]
        EmployeeAssigned,

        [Description("Báo cáo danh sách sinh nhật trong tháng")]
        BornInMonth,

        [Description("Báo cáo danh sách cán bộ là đoàn viên")]
        UnionMember,

        [Description("Báo cáo danh sách cán bộ là quân nhân")]
        MilitaryList,

        [Description("Báo cáo danh sách cán bộ theo phòng ban")]
        EmployeeByDepartment,

        [Description("Báo cáo danh sách đối tượng bồi dưỡng cán bộ nguồn tỉnh ủy quản lý")]
        EmployeeFostering,
        #endregion

        #region Salary
        [Description("Báo cáo diễn biến quá trình lương")]
        SalaryIncreaseProcess,

        [Description("Báo cáo danh sách đến kỳ tăng lương")]
        SalaryIncrease,

        [Description("Báo cáo danh sách cán bộ đến hạn xét vượt khung")]
        SalaryOutOfFrame,
        #endregion

        #region Training - Onsite
        [Description("Báo cáo danh sách cán bộ được đào tạo tại đơn vị")]
        EmployeeTraining,

        [Description("Báo cáo danh sách cán bộ công tác nước ngoài")]
        EmployeeOnsite,
        #endregion

        #region Rewared - Disciplined
        [Description("Báo cáo danh sách được nhận danh hiệu thi đua")]
        EmployeeReceivedAward,

        [Description("Báo cáo danh sách được khen thưởng")]
        EmployeeRewarded,

        [Description("Báo cáo danh sách bị kỷ luật")]
        EmployeeDisciplined,
        #endregion

        #region Report employee detail
        [Description("Báo cáo hồ sơ cán bộ")]
        CurriculumVitae,
        [Description("Báo cáo hồ sơ cán bộ")]
        InfoEmployeeDetail,
        [Description("Báo cáo hồ sơ cán bộ")]
        InfoEmployeeDetailV2,

        #endregion
    }
}
