using System;
using Newtonsoft.Json;
using Web.Core.Service.Security;

namespace Web.Core.Object.Security
{
    public class SchedulerLog : BaseEntity
    {
        // Scheduler History Id
        public int SchedulerHistoryId { get; set; }
        // Description
        public string Description { get; set; }
        // Created Date
        public DateTime CreatedDate { get; set; }

        //Scheduler History Description
        public string SchedulerHistoryDescription
        {
            get { return SchedulerHistoryServices.GetFieldValueById(SchedulerHistoryId, "Description"); }
        }
    }
}
