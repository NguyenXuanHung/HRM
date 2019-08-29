using System;
using Web.Core.Object.HumanRecord;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for DelegateModel
    /// </summary>
    public class DelegateModel
    {
        private readonly hr_Delegate _delegate;

        public DelegateModel(hr_Delegate dele)
        {
            _delegate = dele ?? new hr_Delegate();
            RecordId = _delegate.RecordId;
            Type = _delegate.Type;
            Term = _delegate.Term;
            Note = _delegate.Note;
            IsApproved = _delegate.IsApproved;
            Id = _delegate.Id;
            FromDate = _delegate.FromDate;
            ToDate = _delegate.ToDate;
        }

        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// id ho so
        /// </summary>
        public int RecordId { get; set; }

        /// <summary>
        /// tu ngay
        /// </summary>
        public DateTime? FromDate { get; set; }

        /// <summary>
        /// den ngay
        /// </summary>
        public DateTime? ToDate { get; set; }

        /// <summary>
        /// loai hinh
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// nhiem ky
        /// </summary>
        public string Term { get; set; }

        /// <summary>
        /// ghi chu
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// duyet
        /// </summary>
        public bool IsApproved { get; set; }
    }
}
