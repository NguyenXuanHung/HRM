using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Core.Object.HumanRecord
{
    public class hr_Bank : BaseEntity//HOSO_ATM
    {
        // id hồ sơ
        public int RecordId { get; set; } //FR_KEY
        // Ten tai khoan
        public string AccountName { get; set; } //
        //So tai khoan
        public string AccountNumber { get; set; }//ATMNumber
        // id ngan hang
        public int BankId { get; set; } //BankID
        // ghi chu
        public string Note { get; set; } //Note
        // su dung
        public bool IsInUsed { get; set; }//IsInUsed
        // các trường mặc định
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string EditedBy { get; set; }
        public DateTime EditedDate { get; set; }
    }
}
