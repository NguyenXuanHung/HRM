using System.Text.RegularExpressions;

namespace Web.Core.Framework
{
    public class BaseTask
    {
        /// <summary>
        /// Scheduler type ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Scheduler type name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Argument value
        /// </summary>
        public string Arguments { get; set; }

        /// <summary>
        /// Id of history log
        /// </summary>
        public int LogId { get; set; }

        /// <summary>
        /// True if running process throw exception
        /// </summary>
        public bool HasError { get; set; }

        /// <summary>
        /// Error message
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Error description
        /// </summary>
        public string ErrorDescription { get; set; }

        /// <summary>
        /// excute task
        /// </summary>
        public virtual void Excute(string args)
        { }

        protected string GetArgValue(string argName)
        {
            try
            {
                if (string.IsNullOrEmpty(Arguments)) return null;
                var regex = new Regex("{0} (\"[^\"]*\"|[^ ]*)".FormatWith(argName));
                var match = regex.Match(Arguments);
                return match.Success ? match.Groups[1].Value.TrimStart('\"').TrimEnd('\"') : null;
            }
            catch
            {
                return null;
            }
        }
    }
}
