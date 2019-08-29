using System;
using System.Collections.Generic;

namespace Web.Core.Framework
{
    public class Resource
    {
        public static List<ResourceItem> ResourceHRM = new List<ResourceItem>
        {
            new ResourceItem("LoginUrl", "~/LoginHRM.aspx"),
            new ResourceItem("ApplicationName", "PHẦN MỀM QUẢN LÝ NGUỒN NHÂN LỰC"),

            //common
            new ResourceItem("Grid.EmployeeCode", "Mã CNV"),

           // Chart
            new ResourceItem("Chart.EmployeeBySex", "Thống kê CNV theo giới tính"),
            new ResourceItem("Chart.EmployeeByDegree", "Thống kê CNV theo trình độ"),
            new ResourceItem("Chart.EmployeeByMatrimony", "Thống kê CNV theo tình trạng hôn nhân"),
            new ResourceItem("Chart.EmployeeByReligion", "Thống kê CNV theo tôn giáo"),
            new ResourceItem("Chart.EmployeeByFolk", "Thống kê CNV theo dân tộc"),
            new ResourceItem("Chart.EmployeeByContractType", "Thống kê CNV theo loại hợp đồng"),
            new ResourceItem("Chart.EmployeeByTitle", "Thống kê CNV theo chức vụ đoàn"),
            new ResourceItem("Chart.EmployeeByPartyLevel", "Thống kê CNV theo chức vụ đảng"),
            new ResourceItem("Chart.EmployeeByArmyLevel", "Thống kê CNV theo cấp bậc quân đội"),
            new ResourceItem("Chart.EmployeeBySeniority", "Thống kê CNV theo thâm niên công tác"),
            new ResourceItem("Chart.Volatility", "Biểu đồ biến động"),
            new ResourceItem("Chart.EmployeeByAge", "Thống kê CNV theo độ tuổi"),
            new ResourceItem("Chart.EmployeeByWorkUnit", "Thống kê CNV theo đơn vị"),

            // Employee Management
            new ResourceItem("QuikLookUp", "Tra cứu nhanh thông tin CNV"),
            new ResourceItem("Employee.ReportExpried", "Nhân sự sắp hết hạn hợp đồng"),
            new ResourceItem("Employee.Code", "Mã CNV"),
            new ResourceItem("Employee.WindowUpdateTitle", "Cập nhật thông tin CNV"),
            new ResourceItem("Employee.CurriculumVitae", "Sơ yếu lý lịch nhân viên"),
            new ResourceItem("Employee.ManagementDepartment", "Đơn vị quản lý CNV"),
            new ResourceItem("Employee.ShortCode", "Mã NV"),
            new ResourceItem("Employee.ShortDepartment", "Đơn vị sử dụng CNV"),
            new ResourceItem("Employee.List", "Danh sách nhân viên"),
            new ResourceItem("Employee.Choose", "Chọn nhân viên"),

            // Window choose Employee
            new ResourceItem("WindowsChooseEmployeeTitle", "Chọn danh sách CNV"),
        };

        public static List<ResourceItem> ResourceHJM = new List<ResourceItem>
        {
            new ResourceItem("LoginUrl", "~/LoginHJM.aspx"),
            new ResourceItem("ApplicationName", "PHẦN MỀM QUẢN LÝ HỒ SƠ CÁN BỘ, CÔNG CHỨC, VIÊN CHỨC"),

            //common
            new ResourceItem("Grid.EmployeeCode", "Mã CBCC"),
            new ResourceItem("Grid.EmployeeList", "Danh sách cán bộ"),           

            // Chart
            new ResourceItem("Chart.EmployeeBySex", "Thống kê CBCC theo giới tính"),
            new ResourceItem("Chart.EmployeeByDegree", "Thống kê CBCC theo trình độ"),
            new ResourceItem("Chart.EmployeeByMatrimony", "Thống kê CBCC theo tình trạng hôn nhân"),
            new ResourceItem("Chart.EmployeeByReligion", "Thống kê CBCC theo tôn giáo"),
            new ResourceItem("Chart.EmployeeByFolk", "Thống kê CBCC theo dân tộc"),
            new ResourceItem("Chart.EmployeeByContractType", "Thống kê CBCC theo loại hợp đồng"),
            new ResourceItem("Chart.EmployeeByTitle", "Thống kê CBCC theo chức vụ đoàn"),
            new ResourceItem("Chart.EmployeeByPartyLevel", "Thống kê CBCC theo chức vụ đảng"),
            new ResourceItem("Chart.EmployeeByArmyLevel", "Thống kê CBCC theo cấp bậc quân đội"),
            new ResourceItem("Chart.EmployeeBySeniority", "Thống kê CBCC theo thâm niên công tác"),
            new ResourceItem("Chart.Volatility", "Biểu đồ biến động"),
            new ResourceItem("Chart.EmployeeByAge", "Thống kê CBCC theo độ tuổi"),
            new ResourceItem("Chart.EmployeeByWorkUnit", "Thống kê CBCC theo đơn vị"),

            // Employee Management
            new ResourceItem("QuikLookUp", "Tra cứu nhanh thông tin CBCCVC"),
            new ResourceItem("Employee.ReportExpried", "Cán bộ sắp hết hạn hợp đồng"),
            new ResourceItem("Employee.WindowUpdateTitle", "Cập nhật thông tin CBCC"),
            new ResourceItem("Employee.CurriculumVitae", "Thêm nhanh hồ sơ cán bộ thu gọn"),
            new ResourceItem("Employee.ManagementDepartment", "Cơ quan, đơn vị có thẩm quyền quản lý CBCC"),
            new ResourceItem("Employee.ShortCode", "Số hiệu cán bộ, công chức"),
            new ResourceItem("Employee.ShortDepartment", "Cơ quan, đơn vị sử dụng CBCC"),
            new ResourceItem("Employee.WindowsChooseTitle", "Chọn danh sách CBCCVC"),
            new ResourceItem("Employee.Choose", "Chọn Cán bộ"),

            // Training Maganement
            new ResourceItem("Training.WindowsEmployeeList", "Chọn danh sách CBCCVC"),
        };

        public static string Get(string key)
        {
            try
            {
                return IsHJM() ? ResourceHJM.Find(r => r.Key == key).Value : ResourceHRM.Find(r => r.Key == key).Value;
            }
            catch 
            {
                return string.Empty;
            }
        }

        public static bool IsHJM()
        {
            try
            {
                // get application type
                var application = AppSettingHelper.GetAppSetting(typeof(string), "Application", true).ToString();

                // return is hjm
                return application.ToLower() == "hjm";
            }
            catch
            {
                return false;
            }
        }
    }

    public class ResourceItem
    {
        public ResourceItem(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; set; }
        public string Value { get; set; }
    }
}
