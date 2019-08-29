using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Core.Object.HumanRecord
{
    public class hr_WorkHistory : BaseEntity//HOSO_UNGVIEN_KINHNGHIEMLAMVIEC
    {
        // id hồ sơ
        public int RecordId { get; set; } //FR_KEY
        //Noi lam viec
        public string WorkPlace { get; set; }//NoiLamViec
        //Vi tri cong viec
        public string WorkPosition { get; set; }//ViTriCongViec
        //Ly do thoi viec
        public string ReasonLeave { get; set; }//LyDoThoiViec
        //Tu thang nam
        public DateTime? FromDate { get; set; }//TuThangNam
        //Den thang nam
        public DateTime? ToDate { get; set; }//DenThangNam
        //Kinh nghiem dat duoc
        public string ExperienceWork { get; set; }//KinhNghiemDatDuoc
        //Duyet
        public bool IsApproved { get; set; }//Duyet
        //Ghi chu
        public string Note { get; set; }// GhiChu
        //Muc luong
        public decimal SalaryLevel { get; set; }//MucLuong
        // Dia chi cong ty
        public string AddressCompany { get; set; }//DiaChiCongTy

        // các trường mặc định
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string EditedBy { get; set; }
        public DateTime? EditedDate { get; set; }
    }
}
