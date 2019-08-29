using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Core.Object.HumanRecord
{
    public class hr_Decision : BaseEntity
    {
        /// <summary>
        /// id hồ sơ
        /// </summary>
        public int RecordId { get; set; }
        /// <summary>
        /// ReasonId
        /// </summary>
        public int ReasonId { get; set; }

        /// <summary>
        /// Tên quyết định
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Loại quyết định
        /// </summary>
        public DecisionType Type { get; set; }

        /// <summary>
        /// Ngày quyết định
        /// </summary>
        public DateTime DecisionDate { get; set; }
       
      
        // các trường mặc định
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string EditedBy { get; set; }
        public DateTime EditedDate { get; set; }
    }
}
