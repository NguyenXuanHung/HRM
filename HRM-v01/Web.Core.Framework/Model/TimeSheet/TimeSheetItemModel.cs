namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for TimeSheetItemModel
    /// </summary>
    public class TimeSheetItemModel
    {
        public TimeSheetItemModel()
        {
            Id = 0;
        }

        public TimeSheetItemModel(int timeSheetItemId)
        {
            Detail = " ";
        }

        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Chi tiết
        /// </summary>
        public string Detail { get; set; }

        /// <summary>
        /// Công quy đổi
        /// </summary>
        public double? WorkConvert { get; set; }

        /// <summary>
        /// Số tiền quy đổi
        /// </summary>
        public decimal? MoneyConvert { get; set; }

        /// <summary>
        /// Thời gian quy đổi
        /// </summary>
        public double? TimeConvert { get; set; }

        /// <summary>
        /// Ký hiệu chấm công
        /// </summary>
        public string Symbol { get; set; }

        /// <summary>
        /// Hiển thị ký hiệu chấm công
        /// </summary>
        public string SymbolDisplay { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int TimeSheetModelId { get; set;}

        /// <summary>
        /// Nhóm ký hiệu chấm công
        /// </summary>
        public  string TypeGroup { get; set; }

        /// <summary>
        /// Hiển thị chi tiết trên báo cáo
        /// </summary>
        public string DetailDisplay { get; set; }

        /// <summary>
        /// Diễn giải
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Trạng thái event (active, delete, pending,... )
        /// </summary>
        public EventStatus StatusId { get; set; }
    }
}
