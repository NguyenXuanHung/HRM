using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Web.Core.Service.Security;

namespace Web.Core.Object.Security
{
    public class SchedulerHistory: BaseEntity
    {
        // Scheduler Id
        public int SchedulerId { get; set; }
        // Description
        public string Description { get; set; }
        // Has Error
        public bool HasError { get; set; }
        // Error Message
        public string ErrorMessage { get; set; }
        // Error Description
        public string ErrorDescription { get; set; }
        // Start time
        public DateTime StartTime { get; set; }
        // End time
        public DateTime EndTime { get; set; }

        public string SchedulerName
        {
            get { return SchedulerServices.GetFieldValueById(SchedulerId); }
        }
        //#region Custom Properties
        //[JsonIgnore]
        //public virtual Scheduler Scheduler { get; set; }
        //[JsonIgnore]
        //public virtual ICollection<SchedulerLog> SchedulerLogs { get; set; }

        //#endregion
    }
}
