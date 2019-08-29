using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Core.Object.Security
{
    public class SystemLog : BaseEntity
    {
        public SystemLog()
        {
            Username = "System";
            Thread = "Unknown";
            Action = SystemAction.TrackError;
            Type = SystemLogType.UnHandlerException;
            ShortDescription = "";
            LongDescription = "";
            IsException = true;
            CreatedDate = DateTime.Now;
        }

        /// <summary>
        /// Username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Thread
        /// </summary>
        public string Thread { get; set; }

        /// <summary>
        /// System action
        /// </summary>
        public SystemAction Action { get; set; }

        /// <summary>
        /// Log type
        /// </summary>
        public SystemLogType Type { get; set; }

        /// <summary>
        /// Short description
        /// </summary>
        public string ShortDescription { get; set; }

        /// <summary>
        /// Long description
        /// </summary>
        public string LongDescription { get; set; }

        /// <summary>
        /// Is exception
        /// </summary>
        public bool IsException { get; set; }

        /// <summary>
        /// Created date
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }
}
