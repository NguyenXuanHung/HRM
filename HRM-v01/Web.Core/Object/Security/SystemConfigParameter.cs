namespace Web.Core.Object.Security
{
    /// <summary>
    /// Summary description for SystemConfigParameter
    /// </summary>
    public class SystemConfigParameter
    {
        public SystemConfigParameter()
        {
        }

        public const string EMAIL = "EMAIL";
        public const string PASSWORD_EMAIL = "PASSWORD";
        public const string MENU_TYPE = "MENU_TYPE";
        public const string COMPANY_NAME = "COMPANY_NAME";
        public const string MNG_COMPANY_NAME = "MNG_COMPANY_NAME";
        public const string COMPANY_ADDRESS = "COMPANY_ADDRESS";
        public const string COMPANY_MASOTHUE = "COMPANY_MASOTHUE";
        public const string COMPANY_DIENTHOAI = "COMPANY_DIENTHOAI";
        public const string COMPANY_FAX = "COMPANY_FAX";
        public const string COMPANY_EMAIL = "COMPANY_EMAIL";
        public const string CITY = "CITY";
        public const string PREFIX = "PREFIX";
        public const string NUMBER_OF_CHARACTER = "NUMBER_OF_CHARACTER";
        public const string EXCEL_FORMAT_DOC = "DOC";
        public const string EXCEL_FORMAT_NGANG = "NGANG";
        public const string PHANCA_TYPE_THANG = "THANG";
        public const string PHANCA_TYPE_BOPHAN = "BOPHAN";
        public const string BAO_HET_HAN_HOP_DONG = "HETHANHOPDONG";

        #region cấu hình sinh số quyết định
        public const string SUFFIX_SOHOPDONG = "SUFFIX_SOHOPDONG";
        public const string SUFFIX_SOQDKHENTHUONG = "SUFFIX_SOQDKHENTHUONG";
        public const string SUFFIX_SOQDKYLUAT = "SUFFIX_SOQDKYLUAT";
        public const string SUFFIX_SOQDCONGTAC = "SUFFIX_SOQDCONGTAC";
        public const string SUFFIX_SOQDDIEUCHUYEN = "SUFFIX_SOQDDIEUCHUYEN";
        public const string SUFFIX_SOQDLUONG = "SUFFIX_SOQDLUONG";
        #endregion
        #region  cấu hình sinh chữ ký báo cáo
        public const string SuDungTenDangNhap = "SuDungTenDangNhap";
        public const string chuky1 = "chuky1";
        public const string chuky2 = "chuky2";
        public const string chuky3 = "chuky3";
        public const string chuky4 = "chuky4";
        #endregion

        //lưu thông tin lương cơ bản theo quy định của nhà nước
        public const string LUONG_CB = "LUONG_CB";

        #region cấu hình các cột hiển thị trong quyết định lương
        public const string QDL_LUONGCUNG = "QDL_LUONGCUNG";
        public const string QDL_HESOLUONG = "QDL_HESOLUONG";
        public const string QDL_PHANTRAMHL = "QDL_PHANTRAMHL";
        public const string QDL_LOAILUONG = "QDL_LOAILUONG";
        public const string QDL_LUONGDONGBHXH = "QDL_LUONGDONGBHXH";
        public const string QDL_BACLUONG = "QDL_BACLUONG";
        public const string QDL_BACLUONGNB = "QDL_BACLUONGNB";
        public const string QDL_NGAYHL = "QDL_NGAYHL";
        public const string QDL_NGAYHLNB = "QDL_NGAYHLNB";
        public const string QDL_SOQD = "QDL_SOQD";
        public const string QDL_NGAYQD = "QDL_NGAYQD";
        public const string QDL_NGAYHIEULUC = "QDL_NGAYHIEULUC";
        public const string QDL_NGAYHETHIEULUC = "QDL_NGAYHETHIEULUC";
        public const string QDL_NGUOIQD = "QDL_NGUOIQD";
        public const string QDL_VUOTKHUNG = "QDL_VUOTKHUNG";
        #endregion

    }
}