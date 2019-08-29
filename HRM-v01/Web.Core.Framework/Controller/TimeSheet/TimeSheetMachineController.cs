using System.Collections.Generic;
using System.Linq;
using Web.Core.Object.TimeSheet;
using Web.Core.Service.TimeSheet;

namespace Web.Core.Framework
{
    /// <summary>
    /// Machine time sheet controller
    /// </summary>
    public class TimeSheetMachineController
    {
        /// <summary>
        ///  Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static TimeSheetMachineModel GetById(int id)
        {
            var timeSheetMachineModel = hr_TimeSheetMachineService.GetById(id);
            return timeSheetMachineModel != null ? new TimeSheetMachineModel(timeSheetMachineModel) : null;
        }

        /// <summary>
        /// Get by serialNumber
        /// </summary>
        /// <param name="serialNumber"></param>
        /// <returns></returns>
        public static TimeSheetMachineModel GetBySerialNumber(string serialNumber)
        {
            var condition = @"1=1";

            // check timesheetmachine code
            if(!string.IsNullOrEmpty(serialNumber))
            {
                condition += @" AND [SerialNumber]='{0}'".FormatWith(serialNumber);
                var timeSheetMachineModel = hr_TimeSheetMachineService.GetByCondition(condition);
                return timeSheetMachineModel != null ? new TimeSheetMachineModel(timeSheetMachineModel) : null;
            }
            // invalid code
            return null;
        }

       /// <summary>
       /// get all timesheet machine
       /// </summary>
       /// <param name="keyword"></param>
       /// <param name="serialNumber"></param>
       /// <param name="ipAddress"></param>
       /// <param name="departmentIds"></param>
       /// <param name="selectedDepartment"></param>
       /// <param name="order"></param>
       /// <param name="limit"></param>
       /// <returns></returns>
        public static List<TimeSheetMachineModel> GetAll(string keyword, string serialNumber, string ipAddress, string departmentIds, string selectedDepartment, string order,  int? limit)
        {
            // init condition
            var condition = @"1=1";

            // keyword
            if(!string.IsNullOrEmpty(keyword))
            {
                condition += @" AND ([Name] LIKE N'%{0}%' OR [Location] LIKE N'%{0}%')".FormatWith(keyword);
            }

            // serialNumber
            if(!string.IsNullOrEmpty(serialNumber))
            {
                condition += @" AND [SerialNumber] = {0}".FormatWith(serialNumber);
            }

            // ipAddress
            if(!string.IsNullOrEmpty(ipAddress))
            {
                condition += @" AND [IPAddress] IN ({0})".FormatWith(ipAddress);
            }

            // departmentIds
            if(!string.IsNullOrEmpty(departmentIds))
            {
                condition += @" AND [DepartmentId] IN ({0})".FormatWith(departmentIds);
            }

            //selected department
            if(!string.IsNullOrEmpty(selectedDepartment))
            {
                condition += @" AND [DepartmentId] IN ({0})".FormatWith(selectedDepartment);
            }

            // return
            return hr_TimeSheetMachineService.GetAll(condition, order, limit).Select(r => new TimeSheetMachineModel(r)).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="serialNumber"></param>
        /// <param name="ipAddress"></param>
        /// <param name="departmentIds"></param>
        /// <param name="selectedDepartment"></param> 
        /// <param name="order"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static PageResult<TimeSheetMachineModel> GetPaging(string keyword, string serialNumber, string ipAddress, string departmentIds, string selectedDepartment, string order, int start, int limit)
        {
            // init condition
            var condition = @"1=1";

            // keyword
            if(!string.IsNullOrEmpty(keyword))
            {
                condition += @" AND ([Name] LIKE N'%{0}%' OR [Location] LIKE N'%{0}%')".FormatWith(keyword);
            }

            // serialNumber
            if (!string.IsNullOrEmpty(serialNumber))
            {
                condition += @" AND [SerialNumber] = {0}".FormatWith(serialNumber);
            }

            // ipAddress
            if(!string.IsNullOrEmpty(ipAddress))
            {
                condition += @" AND [IPAddress] IN ({0})".FormatWith(ipAddress);
            }

            // departmentIds
            if (!string.IsNullOrEmpty(departmentIds))
            {
                condition += @" AND [DepartmentId] IN ({0})".FormatWith(departmentIds);
            }

            //selected department
            if (!string.IsNullOrEmpty(selectedDepartment))
            {
                condition += @" AND [DepartmentId] IN ({0})".FormatWith(selectedDepartment);
            }
            
            var result = hr_TimeSheetMachineService.GetPaging(condition, order, start, limit);            

            return new PageResult<TimeSheetMachineModel>(result.Total, result.Data.Select(m => new TimeSheetMachineModel(m)).ToList());
        }

        /// <summary>
        /// insert machine time sheet by model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static TimeSheetMachineModel Create(TimeSheetMachineModel model)
        {
            // init
            var entity = new hr_TimeSheetMachine();
            // fill
            model.FillEntity(ref entity);
            // create entity
            return new TimeSheetMachineModel(hr_TimeSheetMachineService.Create(entity));
          
        }

        /// <summary>
        /// update timesheet machine by model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>   
        public static TimeSheetMachineModel Update(TimeSheetMachineModel model)
        {
                // init
                var entity = hr_TimeSheetMachineService.GetById(model.Id);

                // fill
                model.FillEntity(ref entity);

                // update entity
                return new TimeSheetMachineModel(hr_TimeSheetMachineService.Update(entity));
        }
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public static void Delete(int id)
        {
            var exitsMachineTimeSheet = GetById(id);

            if (exitsMachineTimeSheet != null)
            {
                var result = hr_TimeSheetMachineService.Delete(id);
            }
        }
    }
}
