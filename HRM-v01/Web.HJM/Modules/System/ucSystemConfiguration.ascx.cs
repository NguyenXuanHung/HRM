using System;
using System.Web.UI;
using Ext.Net;
using System.Data;
using Web.Core.Framework;
using Web.Core;
using System.Linq;
using Web.Core.Framework.Adapter;
using Web.Core.Service.Catalog;
using Web.Core.Object.Security;

namespace Web.HJM.Modules.System
{
    public partial class ucSystemConfiguration : global::System.Web.UI.UserControl
    {
        private UserModel _userModel;

        protected void Page_Load(object sender, EventArgs e)
        {
            _userModel = ((BasePage)Page).CurrentUser;
        }

        protected void btnUpdateConfig_Click(object sender, DirectEventArgs e)
        {
            try
            {
                if (txtSystemMail.Text.Contains("@gmail.com") == false && !string.IsNullOrEmpty(txtSystemMail.Text))
                {
                    X.MessageBox.Alert("Thông báo", "Hệ thống chỉ chấp nhận định dạng gmail").Show();
                    return;
                }
                string maDonVi = string.Join(",", ((BasePage)Page).CurrentUser.Departments.Select(d => d.Id));
                //SystemController htController = new SystemController();
                var htController = new SystemConfigController();
                var departments = string.Join(",", _userModel.Departments.Select(d => d.Id));

                // tab thông tin chung
                htController.CreateOrSave(SystemConfigParameter.EMAIL, txtSystemMail.Text.Trim(), departments);
                htController.CreateOrSave(SystemConfigParameter.PASSWORD_EMAIL, txtPassword.Text.Trim(), departments);
                htController.CreateOrSave(SystemConfigParameter.MENU_TYPE, cbMenuType.SelectedItem.Value, departments);
                htController.CreateOrSave(SystemConfigParameter.COMPANY_NAME, txtCompanyName.Text.Trim(), departments);
                htController.CreateOrSave(SystemConfigParameter.CITY, txtCity.Text.Trim(), departments);
                htController.CreateOrSave(SystemConfigParameter.PREFIX, txtTienTo.Text.Trim(), departments);
                htController.CreateOrSave(SystemConfigParameter.NUMBER_OF_CHARACTER, txtSoLuong.Text.Trim(), departments);
                htController.CreateOrSave(SystemConfigParameter.COMPANY_ADDRESS, txt_DiaChi.Text.Trim(), departments);
                htController.CreateOrSave(SystemConfigParameter.COMPANY_MASOTHUE, txt_MaSoThue.Text.Trim(), departments);
                htController.CreateOrSave(SystemConfigParameter.COMPANY_DIENTHOAI, txt_DienThoai.Text.Trim(), departments);
                htController.CreateOrSave(SystemConfigParameter.COMPANY_FAX, txt_Fax.Text.Trim(), departments);
                htController.CreateOrSave(SystemConfigParameter.COMPANY_EMAIL, txt_Email.Text.Trim(), departments);
                htController.CreateOrSave(SystemConfigParameter.MNG_COMPANY_NAME, txtManagementCompanyName.Text.Trim(), departments);

                // tab sinh mã, số quyết định
                htController.CreateOrSave(SystemConfigParameter.SUFFIX_SOHOPDONG, txtSoHopDong.Text.Trim(), departments);
                htController.CreateOrSave(SystemConfigParameter.SUFFIX_SOQDLUONG, txtSoQuyetDinhLuong.Text.Trim(), departments);
                htController.CreateOrSave(SystemConfigParameter.SUFFIX_SOQDKHENTHUONG, txtSoQDKhenThuong.Text.Trim(), departments);
                htController.CreateOrSave(SystemConfigParameter.SUFFIX_SOQDKYLUAT, txtSoQDKyLuat.Text.Trim(), departments);
                htController.CreateOrSave(SystemConfigParameter.SUFFIX_SOQDCONGTAC, txtSoQDCongTac.Text.Trim(), departments);
                htController.CreateOrSave(SystemConfigParameter.SUFFIX_SOQDDIEUCHUYEN, txtSoQĐieuChuyen.Text.Trim(), departments);

                // tab cấu hình chữ ký báo cáo
                htController.CreateOrSave(SystemConfigParameter.SuDungTenDangNhap, chknguoidangnhap.Checked.ToString(), departments);
                htController.CreateOrSave(SystemConfigParameter.chuky1, txtnguoiky1.Text.Trim(), departments);
                htController.CreateOrSave(SystemConfigParameter.chuky2, txtnguoiky2.Text.Trim(), departments);
                htController.CreateOrSave(SystemConfigParameter.chuky3, txtnguoiky3.Text.Trim(), departments);
                htController.CreateOrSave(SystemConfigParameter.chuky4, txtnguoiky4.Text.Trim(), departments);

                wdWindow.Hide();
            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Thông báo", ex.Message).Show();
            }
        }

        //  [DirectMethod]
        // public void FillData()
        protected void wdWindow_BeforeShow(object sender, DirectEventArgs e)
        {
            try
            {
                string strMau = "Mẫu: 001/" + DateTime.Now.Year + "/";
                //DataTable table = DataHandler.GetInstance().ExecuteDataTable("SystemConfig_GetConfigByMaDonVi", "@MaDonVi", Session["MaDonVi"].ToString());
                var departments = string.Join(",", _userModel.Departments.Select(d => d.Id));
                // department
                var arrDepartment = departments.Split(new[] { ',' }, StringSplitOptions.None);
                for (var i = 0; i < arrDepartment.Length; i++)
                {
                    arrDepartment[i] = "{0}".FormatWith(arrDepartment[i]);
                }
                var table = SQLHelper.ExecuteTable(SQLManagementAdapter.GetStore_GetAllByDepartment(string.Join(",", arrDepartment)));
                foreach (DataRow item in table.Rows)
                {
                    switch (item["Name"].ToString())
                    {
                        case SystemConfigParameter.EMAIL:
                            txtSystemMail.Text = item["Value"].ToString();
                            break;
                        case SystemConfigParameter.PASSWORD_EMAIL:
                            txtPassword.Text = item["Value"].ToString();
                            txtConfirmPassword.Text = txtPassword.Text;
                            break;
                        case SystemConfigParameter.MENU_TYPE:
                            cbMenuType.SetValue(item["Value"].ToString());
                            break;
                        case SystemConfigParameter.COMPANY_NAME:
                            //if (string.IsNullOrEmpty(item["VAR_VALUE"].ToString()))
                            //{
                            //    txtCompanyName.Text = new DM_DONVIController().GetById(Session["MaDonVi"].ToString()).TEN_DONVI;
                            //}
                            //else
                            //{
                            txtCompanyName.Text = item["Value"].ToString();
                            //  }
                            break;
                        case SystemConfigParameter.COMPANY_ADDRESS:
                            txt_DiaChi.Text = item["Value"].ToString();
                            break;
                        case SystemConfigParameter.COMPANY_MASOTHUE:
                            txt_MaSoThue.Text = item["Value"].ToString();
                            break;
                        case SystemConfigParameter.COMPANY_DIENTHOAI:
                            txt_DienThoai.Text = item["Value"].ToString();
                            break;
                        case SystemConfigParameter.COMPANY_FAX:
                            txt_Fax.Text = item["Value"].ToString();
                            break;
                        case SystemConfigParameter.COMPANY_EMAIL:
                            txt_Email.Text = item["Value"].ToString();
                            break;
                        case SystemConfigParameter.CITY:
                            txtCity.Text = item["Value"].ToString();
                            break;
                        case SystemConfigParameter.MNG_COMPANY_NAME:
                            txtManagementCompanyName.Text = item["Value"].ToString();
                            break;
                        //case SystemConfigParameter.LUONG_CB:
                        //    txtLuongCB.SetValue(item["VAR_VALUE"].ToString());
                        //    break;
                        case SystemConfigParameter.PREFIX:
                            txtTienTo.Text = item["Value"].ToString();
                            break;
                        case SystemConfigParameter.NUMBER_OF_CHARACTER:
                            txtSoLuong.Text = item["Value"].ToString();
                            break;
                        //case SystemConfigParameter.CONG_CHUAN:
                        //    txtSoNgayCongChuan.Text = item["VAR_VALUE"].ToString();
                        //    break;
                        // tab sinh mã, số quyết định
                        case SystemConfigParameter.SUFFIX_SOHOPDONG:
                            txtSoHopDong.Text = item["Value"].ToString();
                            txtSoHopDong.Note = strMau + item["Value"].ToString();
                            break;
                        case SystemConfigParameter.SUFFIX_SOQDLUONG:
                            txtSoQuyetDinhLuong.Text = item["Value"].ToString();
                            txtSoQuyetDinhLuong.Note = strMau + item["Value"].ToString();
                            break;
                        case SystemConfigParameter.SUFFIX_SOQDKHENTHUONG:
                            txtSoQDKhenThuong.Text = item["Value"].ToString();
                            txtSoQDKhenThuong.Note = strMau + item["Value"].ToString();
                            break;
                        case SystemConfigParameter.SUFFIX_SOQDKYLUAT:
                            txtSoQDKyLuat.Text = item["Value"].ToString();
                            txtSoQDKyLuat.Note = strMau + item["Value"].ToString();
                            break;
                        case SystemConfigParameter.SUFFIX_SOQDCONGTAC:
                            txtSoQDCongTac.Text = item["Value"].ToString();
                            txtSoQDCongTac.Note = strMau + item["Value"].ToString();
                            break;
                        case SystemConfigParameter.SUFFIX_SOQDDIEUCHUYEN:
                            txtSoQĐieuChuyen.Text = item["Value"].ToString();
                            txtSoQĐieuChuyen.Note = strMau + item["Value"].ToString();
                            break;

                        // tab sinh người ký báo cáo
                        case SystemConfigParameter.SuDungTenDangNhap:
                            // chknguoidangnhap.SetValue(Boolean.Parse(item["VAR_VALUE"].ToString()));
                            chknguoidangnhap.Checked = Boolean.Parse(item["Value"].ToString());
                            break;
                        case SystemConfigParameter.chuky1:
                            txtnguoiky1.Text = item["Value"].ToString();
                            break;
                        case SystemConfigParameter.chuky2:
                            txtnguoiky2.Text = item["Value"].ToString();
                            break;
                        case SystemConfigParameter.chuky3:
                            txtnguoiky3.Text = item["Value"].ToString();
                            break;
                        case SystemConfigParameter.chuky4:
                            txtnguoiky4.Text = item["Value"].ToString();
                            break;
                    }
                }
                if (string.IsNullOrEmpty(txtCompanyName.Text.Trim()))
                {
                    int departmentId = _userModel.DepartmentsTree[0].Id;
                    txtCompanyName.Text = cat_DepartmentServices.GetFieldValueById(departmentId, "Name");
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Thông báo", ex.Message).Show();
            }
        }
    }
}

