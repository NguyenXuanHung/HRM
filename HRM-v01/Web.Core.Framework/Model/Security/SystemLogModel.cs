using System;
using Web.Core.Object.Security;

namespace Web.Core.Framework
{
    public class SystemLogModel : BaseModel<SystemLog>
    {
        public SystemLogModel()
        {
            Init(new SystemLog());
        }

        public SystemLogModel(SystemLog systemLog)
        {
            // init entity
            systemLog = systemLog ?? new SystemLog();

            // set props
            Init(systemLog);
        }

        public SystemLogModel(Exception ex)
        {
            Username = "System";
            Thread = "Unknown";
            Action = SystemAction.TrackError;
            Type = SystemLogType.HandlerException;
            ShortDescription = ex.Message;
            LongDescription = ex.ToString();
            IsException = true;
            CreatedDate = DateTime.Now;
        }

        public SystemLogModel(string username, string thread, Exception ex)
        {
            Username = username;
            Thread = thread;
            Action = SystemAction.TrackError;
            Type = SystemLogType.HandlerException;
            ShortDescription = ex.Message;
            LongDescription = ex.ToString();
            IsException = true;
            CreatedDate = DateTime.Now;
        }

        public SystemLogModel(string username, string thread, SystemAction action, string shortDesc)
        {
            Username = username;
            Thread = thread;
            Action = action;
            Type = SystemLogType.UserAction;
            ShortDescription = shortDesc;
            IsException = false;
            CreatedDate = DateTime.Now;
        }

        public SystemLogModel(string username, string thread, SystemAction action, SystemLogType type, string shortDesc)
        {
            Username = username;
            Thread = thread;
            Action = action;
            Type = type;
            ShortDescription = shortDesc;
            IsException = false;
            CreatedDate = DateTime.Now;
        }

        public SystemLogModel(string username, string thread, SystemAction action, SystemLogType type, string shortDesc,
            string longDesc, bool isException)
        {
            Username = username;
            Thread = thread;
            Action = action;
            Type = type;
            ShortDescription = shortDesc;
            LongDescription = longDesc;
            IsException = isException;
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

        #region Custom Properties

        public string ActionName => Action.Description();

        public string TypeName => Type.Description();

        #endregion
    }
}
