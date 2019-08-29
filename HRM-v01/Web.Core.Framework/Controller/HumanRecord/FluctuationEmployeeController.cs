using System;
using System.Collections.Generic;
using System.Linq;
using Web.Core.Framework.Model;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.HumanRecord;

namespace Web.Core.Framework.Controller
{
    public class FluctuationEmployeeController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static FluctuationEmployeeModel GetById(int id)
        {
            var fluctuation = hr_FluctuationEmployeeServices.GetById(id);
            return new FluctuationEmployeeModel(fluctuation);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public static List<FluctuationEmployeeModel> GetAll(int? recordId)
        {
            var fluctuation = hr_FluctuationEmployeeServices.GetAll(recordId);
            return fluctuation.Select(fluc => new FluctuationEmployeeModel(fluc)).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public void Update(hr_FluctuationEmployee obj)
        {
            var record = hr_FluctuationEmployeeServices.GetById(obj.Id);
            if (record == null) return;
            record.RecordId = obj.RecordId;
            record.Reason = obj.Reason;
            record.Date = obj.Date;
            record.Type = obj.Type;
            record.CreatedDate = DateTime.Now;
            record.EditedDate = DateTime.Now;
            hr_FluctuationEmployeeServices.Update(record);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public void Create(hr_FluctuationEmployee obj)
        {
            hr_FluctuationEmployeeServices.Create(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            hr_FluctuationEmployeeServices.Delete(id);
        }
    }
}
