using Ext.Net;
using System;
using Web.Core.Framework;
using Web.Core.Service.HumanRecord;
using Web.Core.Service.Salary;

namespace Web.HRM.Modules.Salary
{
    public partial class SalaryBoardInfo : BasePage
    {
        private const string SalaryBoardListUrl = "~/Modules/Salary/SalaryBoardList.aspx";        

        protected int SalaryBoardListId;

        protected void Page_Load(object sender, EventArgs e)
        {
            hdfType.Text = Request.QueryString["TimeSheetHandlerType"];

            SalaryBoardListId = Request.QueryString["id"] != null ? Convert.ToInt32(Request.QueryString["id"]) : 0;

            if (!ExtNet.IsAjaxRequest)
            {
                //init
                InitController();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitController()
        {          
            if (Request.QueryString["id"] == null)
            {
                //wdSalaryBoardManage.Show();
            }
            else
            {
                //set configId
                var salaryBoard = sal_PayrollServices.GetById(SalaryBoardListId);
                if(salaryBoard != null)
                {
                    gridSalaryInfo.Title = salaryBoard.Title;
                }
            }
        }

        /// <summary>
        /// Quay trở lại trang danh sách bảng lương
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBack_Click(object sender, DirectEventArgs e)
        {
            Response.Redirect( SalaryBoardListUrl + "?mId=" + MenuId, true);
        }
    }
}

