using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Core.Object.HumanRecord
{
    public class hr_Language: BaseEntity//HOSO_NGOAINGU
    {
        // id hồ sơ
        public int RecordId { get; set; } //FR_KEY
        //id ngoai ngu
        public int LanguageId { get; set; }
        //Xep hang
        public string Rank { get; set; }
        //Ghi chu
        public string Note { get; set; }

        // các trường mặc định
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string EditedBy { get; set; }
        public DateTime? EditedDate { get; set; }
    }
}
