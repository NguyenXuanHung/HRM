using System;
using Web.Core.Object.TimeSheet;
using Web.Core.Service.HumanRecord;
using Web.Core.Service.Security;
using Web.Core.Service.TimeSheet;

namespace Web.Core.Framework
{
    public class TaskAnnualLeave : BaseTask
    {
        private const string ConstStatusWorking = "%Đang làm việc%";
        private const int ConstLeaveDay = 12;
        public override void Excute(string args)
        {
            // create history
            var schedulerHistory = SchedulerHistoryServices.Create(Id, Name.ToUpper(), false, string.Empty, string.Empty, DateTime.Now, DateTime.MaxValue);

            // start process
            if (schedulerHistory == null) return;

            // log start
            SchedulerLogServices.Create(schedulerHistory.Id, "START");

            // Chạy 0:05 ngày 1 hàng tháng            
            var month = DateTime.Now.Month;
            var year = DateTime.Now.Year;
            
            //Tao ngay phep mac dinh cho tat ca nhan vien
            var condition = @"WorkStatusId = (select top 1 Id from cat_WorkStatus where Name like N'{0}')".FormatWith(ConstStatusWorking);
            var listRecords = hr_RecordServices.GetAll(condition);

            foreach (var item in listRecords)
            {
                //tạo mới bảng phép
                var leave = new hr_AnnualLeaveConfigure()
                {
                    RecordId = item.Id,
                    AnnualLeaveDay = ConstLeaveDay,
                    CreatedDate = DateTime.Now,
                    Year = DateTime.Now.Year,
                    AllowUseFirstYear = false,
                    AllowUsePreviousYear = false,
                    ExpiredDate = null,

                };
                hr_AnnualLeaveConfigureServices.Create(leave);
            }
            SchedulerLogServices.Create(schedulerHistory.Id, "END");
        }
                 
    }
}