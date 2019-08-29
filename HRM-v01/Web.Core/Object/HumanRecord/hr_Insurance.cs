using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Core.Object.HumanRecord
{
    public class hr_Insurance : BaseEntity// HOSO_BAOHIEM
    {
        // id hồ sơ
        public int RecordId { get; set; } //FR_KEY
        //He so luong
        public decimal SalaryFactor { get; set; }//HS_LUONG
        //Phu cap
        public decimal Allowance { get; set; }//PHUCAP
        //Muc luong
        public decimal SalaryLevel { get; set; }//MUC_LUONG
        //Ty le
        public string Rate { get; set; }//TYLE
        //Ghi chu
        public string Note { get; set; }//GHI_CHU
        //Tu ngay
        public DateTime? FromDate { get; set; }//TU_NGAY
        //Den ngay
        public DateTime? ToDate { get; set; }// DEN_NGAY    
        // id chuc vu
        public int PositionId { get; set; }
        //id vi tri
        public int DepartmentId { get; set; }
        // các trường mặc định
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string EditedBy { get; set; }
        public DateTime? EditedDate { get; set; }
    }
}
