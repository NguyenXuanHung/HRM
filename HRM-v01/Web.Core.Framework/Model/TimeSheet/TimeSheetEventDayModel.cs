using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Core.Framework
{
    public class TimeSheetEventDayModel
    {
        /// <summary>
        /// Ngày
        /// </summary>
        public DateTime Day { get; set; }

        /// <summary>
        /// Các event trong 1 ngày
        /// </summary>
        public List<TimeSheetEventModel> TimeSheetEventModels { get; set; }

        /// <summary>
        /// Các ký hiệu hiển thị trong 1 ngày
        /// </summary>
        public string SymbolDisplay { get; set; }
    }
}
