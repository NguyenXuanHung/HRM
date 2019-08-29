using System;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.HumanRecord;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for AssetModel
    /// </summary>
    public class AssetModel
    {
        // private property
        private readonly hr_Asset _asset;

        public AssetModel(hr_Asset asset)
        {
            _asset = asset ?? new hr_Asset();
            RecordId = _asset.RecordId;
            AssetCode = _asset.AssetCode;
            AssetName = _asset.AssetName;
            Quantity = _asset.Quantity;
            UnitCode = _asset.UnitCode;
            Status = _asset.Status;
            Note = _asset.Note;
            IsApproved = _asset.IsApproved;
            NoteDeliveryAfter = _asset.NoteDeliveryAfter;
            AttachFileName = _asset.AttachFileName;
            ReceiveDate = _asset.ReceiveDate;
            DeliveryDate = _asset.DeliveryDate;
            Id = _asset.Id;
        }

        /// <summary>
        /// id tai san
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// id ho so
        /// </summary>
        public int RecordId { get; set; }

        /// <summary>
        /// Ma tai san
        /// </summary>
        public string AssetCode { get; set; }

        /// <summary>
        /// ten tai san
        /// </summary>
        public string AssetName { get; set; }

        /// <summary>
        /// Số lượng
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// ma don vi tinh
        /// </summary>
        public string UnitCode { get; set; }

        /// <summary>
        /// ngay nhan
        /// </summary>
        public DateTime? ReceiveDate { get; set; }

        /// <summary>
        /// tinh trang
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// ghi chu
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// duyet
        /// </summary>
        public bool IsApproved { get; set; }

        /// <summary>
        /// ngay ban giao
        /// </summary>
        public DateTime? DeliveryDate { get; set; }

        /// <summary>
        /// ghi chu sau ban giao
        /// </summary>
        public string NoteDeliveryAfter { get; set; }

        /// <summary>
        /// tep tin dinh kem
        /// </summary>
        public string AttachFileName { get; set; }

        /// <summary>
        /// Số hiệu CB
        /// </summary>
        public string EmployeeCode
        {
            get { return hr_RecordServices.GetFieldValueById(_asset.RecordId, "EmployeeCode"); }
        }
        
        /// <summary>
        /// Họ và tên cán bộ
        /// </summary>
        public string FullName
        {
            get { return hr_RecordServices.GetFieldValueById(_asset.RecordId, "FullName"); }
        }
    }
}
