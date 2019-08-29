using System;
using System.Collections.Generic;
using System.Text;
using Web.Core.Framework;
using Web.Core.Object.Security;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for ReportController
    /// </summary>
    public class ReportController
    {
        public ReportController()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public string GetVillageName(string type)
        {
            switch (type.ToLower())
            {
                case "phuong":
                    return "Phường";
                case "thitran":
                    return "Thị trấn";
                default:
                    return "Xã";
            }
        }

        public string GetDistrictName(string type)
        {
            switch (type.ToLower())
            {
                case "thanhpho":
                    return "Thành phố";
                case "quan":
                    return "Quận";
                case "thixa":
                    return "Thị xã";
                default:
                    return "Huyện";
            }
        }

        public string GetProvinceName(string type)
        {
            switch (type.ToLower())
            {
                case "thanhphotw":
                    return "Thành phố";
                default:
                    return "Tỉnh";
            }
        }

        private static ReportController _instance;

        public static ReportController GetInstance()
        {
            return _instance ?? (_instance = new ReportController());
        }

        /// <summary>
        /// Lấy tên người tạo báo cáo
        /// @Lê Anh
        /// </summary>
        /// <param name="maDonVi"></param>
        /// <param name="reporter"></param>
        /// <returns></returns>
        public string GetCreatedReporterName(string maDonVi, string reporter)
        {
            var usingCurrentUser = SystemConfigController.GetValueByName(SystemConfigParameter.SuDungTenDangNhap, Convert.ToInt32(maDonVi));
            return usingCurrentUser == "True" ? reporter : SystemConfigController.GetValueByName(SystemConfigParameter.chuky1, Convert.ToInt32(maDonVi));
        }

        public enum DataType
        {
            DateTime, String, Number
        }

        /// <summary>
        /// Lấy giá trị của một biến để hiển thị, nếu giá trị của biến không tồn tại thì lấy giá trị mặc định ...
        /// @daibx
        /// </summary>
        /// <param name="value">giá trị của biến</param>
        /// <param name="type"></param>
        /// <returns></returns>
        public string GetValueDefault(string value, DataType type)
        {
            string defaultValue = "...";
            try
            {
                switch (type)
                {
                    case DataType.String:
                        if (!string.IsNullOrEmpty(value))
                            return value;
                        break;
                    case DataType.DateTime:
                        DateTime date = DateTime.Parse(value);
                        if (date != null)
                            return date.ToString("dd/MM/yyyy");
                        break;
                    case DataType.Number:
                        long number = long.Parse(value);
                        return number.ToString("n0");
                }
                return defaultValue;
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Lấy theo định dạng: Từ ngày 1/5/1014 đến ngày 15/5/2014
        /// @daibx
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public string GetFromDateToDate(DateTime? fromDate, DateTime? toDate)
        {
            var rs = string.Empty;
            if (fromDate != null)
            {
                rs += "Từ ngày " + string.Format("{0:dd/MM/yyyy}", fromDate);
                if (toDate != null)
                    rs += " đến ngày " + string.Format("{0:dd/MM/yyyy}", toDate);
            }
            else
            {
                if (toDate != null)
                    rs += "Đến ngày " + string.Format("{0:dd/MM/yyyy}", toDate);
            }
            return rs;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="departments"></param>
        /// <returns></returns>
        public string GetCityName(string departments)
        {
            return SystemConfigController.GetValueByNameFollowDepartment(SystemConfigParameter.CITY, departments);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departments"></param>
        /// <returns></returns>
        public string GetCompanyName(string departments)
        {
            var companyName = SystemConfigController.GetValueByNameFollowDepartment(SystemConfigParameter.COMPANY_NAME, departments);
            return string.IsNullOrEmpty(companyName) ? "CHƯA CÓ THÔNG TIN CÔNG TY" : companyName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="maDonVi"></param>
        /// <returns></returns>
        public string GetMngCompanyName(string maDonVi)
        {
            var companyName = SystemConfigController.GetValueByName(SystemConfigParameter.MNG_COMPANY_NAME, Convert.ToInt32(maDonVi));
            return string.IsNullOrEmpty(companyName) ? "CHƯA CÓ THÔNG TIN ĐƠN VỊ" : companyName;
        }

        /// <summary>
        /// Lấy địa chỉ công ty
        /// @daibx
        /// </summary>
        /// <param name="maDonVi"></param>
        /// <returns></returns>
        public string GetCompanyAddress(string maDonVi)
        {
            return SystemConfigController.GetValueByName(SystemConfigParameter.COMPANY_ADDRESS, Convert.ToInt32(maDonVi));
        }

        /// <summary>
        /// Lấy mã số thuế của công ty
        /// @daibx
        /// </summary>
        /// <param name="maDonVi"></param>
        /// <returns></returns>
        public string GetCompanyTaxCode(int maDonVi)
        {
            return SystemConfigController.GetValueByName(SystemConfigParameter.COMPANY_MASOTHUE, maDonVi);
        }

        /// <summary>
        /// Lấy số điện thoại của công ty
        /// @daibx
        /// </summary>
        /// <param name="maDonVi"></param>
        /// <returns></returns>
        public string GetCompanyPhoneNumber(int maDonVi)
        {
            return SystemConfigController.GetValueByName(SystemConfigParameter.COMPANY_DIENTHOAI, maDonVi);
        }

        /// <summary>
        /// Lấy số fax của công ty
        /// @daibx
        /// </summary>
        /// <param name="maDonVi"></param>
        /// <returns></returns>
        public string GetCompanyFax(int maDonVi)
        {
            return SystemConfigController.GetValueByName(SystemConfigParameter.COMPANY_FAX, maDonVi);
        }

        /// <summary>
        /// Lấy email của công ty
        /// @daibx
        /// </summary>
        /// <param name="maDonVi"></param>
        /// <returns></returns>
        public string GetCompanyEmail(int maDonVi)
        {
            return SystemConfigController.GetValueByName(SystemConfigParameter.COMPANY_EMAIL, maDonVi);
        }

        /// <summary>
        /// Lấy tiêu đề của chữ ký
        /// @Lê Anh
        /// </summary>
        /// <param name="defaultTitle">Tiêu đề do người dùng nhập vào</param>
        /// <param name="newTitle"></param>
        /// <returns></returns>
        public string GetTitleOfSignature(string defaultTitle, string newTitle)
        {
            return !string.IsNullOrEmpty(newTitle) ? newTitle : defaultTitle;
        }
        /// <summary>
        /// Lấy định dạng chuỗi như : Hà Nội, Ngày ? Tháng ? Năm ?
        /// @Lê Anh
        /// </summary>
        /// <returns></returns>
        public string GetFooterReport(string maDonVi, DateTime? reportDate)
        {
            if (reportDate == null)
                reportDate = DateTime.Now;
            var cityName = GetCityName(maDonVi);
            return (string.IsNullOrEmpty(cityName) ? "N" : cityName + ", n") + "gày     " +
             " tháng      " + " năm      ";
        }

        private string BuildRTF(string input)
        {
            StringBuilder backslashed = new StringBuilder(input);
            backslashed.Replace(@"\", @"\\");
            backslashed.Replace(@"{", @"\{");
            backslashed.Replace(@"}", @"\}");
            StringBuilder sb = new StringBuilder();
            foreach (char character in backslashed.ToString())
            {
                if (character <= 0x7f)
                    sb.Append(character);
                else
                    sb.Append("\\u" + Convert.ToUInt32(character) + "?");
            }
            return " " + sb;
        }

        public string GetRtfString(string input)
        {
            return BuildRTF(input);
        }
        /// <summary>
        /// Được sử dụng để replace giá trị trong RichTextBox (Báo cáo sử dụng Dev)
        /// @Đức Anh
        /// </summary>
        /// <param name="strnguon"></param>
        /// <param name="kytuthaythe"></param>
        /// <param name="chuoithaythe"></param>
        /// <returns></returns>
        public string Convertstringtortf(string strnguon, string kytuthaythe, string chuoithaythe)
        {
            kytuthaythe = @"\{" + kytuthaythe.Substring(1, kytuthaythe.Length - 2) + @"\}";
            return strnguon.Replace(kytuthaythe, BuildRTF(chuoithaythe));
        }

        /// <summary>
        /// Lấy danh sách tháng, quý
        /// @Lê Anh
        /// </summary>
        /// <returns></returns>
        public List<object> GetMonthList()
        {
            var data = new List<object> { new { Name = "Cả năm", Value = "FullYear" } };
            for (var i = 1; i <= 12; i++)
            {
                data.Add(new { Name = "Tháng " + i, Value = i });
            }
            data.Add(new { Name = "Quý I", Value = "I" });
            data.Add(new { Name = "Quý II", Value = "II" });
            data.Add(new { Name = "Quý III", Value = "III" });
            data.Add(new { Name = "Quý IV", Value = "IV" });
            return data;
        }
        /// <summary>
        /// Lấy diễn giải khoảng thời gian
        /// @Lê Anh
        /// </summary>
        /// <param name="startMonth"></param>
        /// <param name="endMonth"></param>
        /// <returns></returns>
        public string GetDescriptionTime(int startMonth, int endMonth)
        {
            if (startMonth == endMonth)
            {
                return "Tháng " + startMonth;
            }
            if (startMonth == 1 && endMonth == 3)
            {
                return "Quý I";
            }
            if (startMonth == 4 && endMonth == 6)
            {
                return "Quý II";
            }
            if (startMonth == 7 && endMonth == 9)
            {
                return "Quý III";
            }
            if (startMonth == 10 && endMonth == 12)
            {
                return "Quý IV";
            }
            if (startMonth == 1 && endMonth == 12)
            {
                return "Cả năm";
            }
            return startMonth < endMonth ? string.Format("Từ tháng {0} đến tháng {1}", startMonth, endMonth) : "";
        }

        /// <summary>
        /// Lấy giá trị tháng từ combobox nếu combobox đó sử dụng data ở phương thức GetMonthList()
        /// @Lê Anh
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int GetStartMonth(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return 1;
            }
            switch (value)
            {
                case "":
                case "FullYear":
                    return 1;
                case "I":
                    return 1;
                case "II":
                    return 4;
                case "III":
                    return 7;
                case "IV":
                    return 10;

                default:
                    return int.Parse(value);
            }
        }

        /// <summary>
        /// Lấy giá trị tháng từ combobox nếu combobox đó sử dụng data ở phương thức GetMonthList()
        /// @Lê Anh
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int GetEndMonth(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return 12;
            }
            switch (value)
            {
                case "":
                case "FullYear":
                    return 12;
                case "I":
                    return 3;
                case "II":
                    return 6;
                case "III":
                    return 9;
                case "IV":
                    return 12;
                default:
                    return int.Parse(value);
            }
        }

        /// <summary>
        /// Tính giá trị giữa 2 khoảng thời gian
        /// @Unknown
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns></returns>
        public string CalculateTime(DateTime d1, DateTime d2)
        {
            d1 = d1.Date;
            d2 = d2.Date;
            string bc;
            if (d1.Day == d2.Day && d1.Month == d2.Month && d1.Year == d2.Year)
            {
                bc = "Ngày " + d1.ToString("dd") + " tháng " + d1.ToString("MM") + " năm " + d1.ToString("yyyy");
            }
            else
                if (d1.Day != 1)
            {
                bc = "Từ ngày " + d1.ToString("dd/MM/yyyy") + " đến ngày " + d2.ToString("dd/MM/yyyy");
            }
            else if (d1 == d2.AddDays(1).AddMonths(-1))
            {
                bc = "Tháng " + d1.ToString("MM") + " năm " + d1.ToString("yyyy");
            }
            else if (d1.Year == d2.Year && d1.Month == 1 && d2.Month == 12 && d2.Day == 31)
            {
                bc = "Năm " + d2.ToString("yyyy");
            }
            else if (d1.Month == 1 && d2.Day == 31 && d2.Month == 3)
            {
                bc = "Quý I năm " + d2.ToString("yyyy");
            }
            else if (d1.Month == 4 && d2.Day == 30 && d2.Month == 6)
            {
                bc = "Quý II năm " + d2.ToString("yyyy");
            }
            else if (d1.Month == 7 && d2.Day == 30 && d2.Month == 9)
            {
                bc = "Quý III năm " + d2.ToString("yyyy");
            }
            else if (d1.Month == 10 && d2.Day == 31 && d2.Month == 12)
            {
                bc = "Quý IV năm " + d2.ToString("yyyy");
            }

            else
            {
                bc = "Từ ngày " + d1.ToString("dd/MM/yyyy") + " đến ngày " + d2.ToString("dd/MM/yyyy");
            }
            return bc;
        }

        /// <summary>
        /// Lấy tên trưởng phòng hành chính nhân sự
        /// @daibx
        /// </summary>
        /// <param name="maDonVi"></param>
        /// <param name="displayName">Tên người ký nhập ở trên form filter</param>
        /// <returns></returns>
        public string GetHeadOfHRroom(string maDonVi, string displayName)
        {
            return !string.IsNullOrEmpty(displayName) ? displayName : SystemConfigController.GetValueByName(SystemConfigParameter.chuky4, Convert.ToInt32(maDonVi));
        }

        /// <summary>
        /// Lấy giá trị tháng từ combobox month
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int GetMonth(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return DateTime.Now.Month;
            }
            switch (value)
            {
                case "":
                case "FullYear":
                    return 1;
                case "I":
                    return 1;
                case "II":
                    return 4;
                case "III":
                    return 7;
                case "IV":
                    return 10;

                default:
                    return int.Parse(value);
            }
        }

    }
}