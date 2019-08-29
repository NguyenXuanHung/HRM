using System;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.Catalog;

/// <summary>
/// Summary description for EducationHistoryModel
/// </summary>
public class EducationHistoryModel
{
    private readonly hr_EducationHistory _educationHistory;

    public EducationHistoryModel()
    { }

    public EducationHistoryModel(hr_EducationHistory educationHistory)
    {
        _educationHistory = educationHistory ?? new hr_EducationHistory();

        RecordId = _educationHistory.RecordId;
        Faculty = _educationHistory.Faculty;
        IsGraduated = _educationHistory.IsGraduated;
        IsApproved = _educationHistory.IsApproved;
        DecisionNumber = _educationHistory.DecisionNumber;
        UniversityId = _educationHistory.UniversityId;
        NationId = _educationHistory.NationId;
        IndustryId = _educationHistory.IndustryId;
        EducationId = _educationHistory.EducationId;
        TrainingSystemId = _educationHistory.TrainingSystemId;
        GraduationTypeId = _educationHistory.GraduationTypeId;
        FromDate = _educationHistory.FromDate;
        ToDate = _educationHistory.ToDate;
        Id = _educationHistory.Id;
    }

    /// <summary>
    /// Id
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Record Id
    /// </summary>
    public int RecordId { get; set; }

    /// <summary>
    /// From date
    /// </summary>
    public DateTime? FromDate { get; set; }

    /// <summary>
    /// To Date
    /// </summary>
    public DateTime? ToDate { get; set; }

    /// <summary>
    /// Khóa
    /// </summary>
    public string Faculty { get; set; }

    /// <summary>
    /// Chuyên ngành đào tạo
    /// </summary>
    public int IndustryId { get; set; }

    /// <summary>
    /// Chuyên ngành
    /// </summary>
    public string IndustryName
    {
        get
        {
            var industry = cat_IndustryServices.GetById(_educationHistory.IndustryId);
            return industry != null ? industry.Name : "";
        }
    }

    /// <summary>
    /// Trình độ chuyên môn
    /// </summary>
    public string EducationName
    {
        get
        {
            var education = cat_EducationServices.GetById(_educationHistory.EducationId);
            return education != null ? education.Name : "";
        }
    }

    /// <summary>
    /// Trình độ chuyên môn
    /// </summary>
    public int EducationId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int TrainingSystemId { get; set; }

    // hệ đào tạo
    /// <summary>
    /// Hệ đào tạo 
    /// </summary>
    public string TrainingSystemName
    {
        get
        {
            var trainingSystem = cat_TrainingSystemServices.GetById(_educationHistory.TrainingSystemId);
            return trainingSystem != null ? trainingSystem.Name : "";
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public int UniversityId { get; set; }

    /// <summary>
    /// Trường đào tạo
    /// </summary>
    public string UniversityName
    {
        get
        {
            var university = cat_UniversityServices.GetById(_educationHistory.UniversityId);
            return university != null ? university.Name : "";
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public int NationId { get; set; }

    /// <summary>
    /// Quốc gia đào tạo
    /// </summary>
    public string NationName
    {
        get
        {
            var nation = cat_NationServices.GetById(_educationHistory.NationId);
            return nation != null ? nation.Name : "";
        }
    }

    /// <summary>
    /// Đã tốt nghiệp
    /// </summary>
    public bool IsGraduated { get; set; }

    /// <summary>
    /// id xep loai tot nghiep
    /// </summary>
    public int GraduationTypeId { get; set; }

    /// <summary>
    /// Xếp loại tốt nghiệp
    /// </summary>
    public string GraduateTypeName
    {
        get
        {
            var graduateType = cat_GraduationTypeServices.GetById(_educationHistory.GraduationTypeId);
            return graduateType != null ? graduateType.Name : "";
        }
    }

    /// <summary>
    /// Đã được duyệt
    /// </summary>
    public bool IsApproved { get; set; }

    /// <summary>
    /// Số quyết định
    /// </summary>
    public string DecisionNumber { get; set; }

    /// <summary>
    /// Ngày quyết định
    /// </summary>
    public string DecisionDate
    {
        get { return _educationHistory.ToDate != null ? _educationHistory.ToDate.Value.ToString("dd/MM/yyyy") : ""; }
    }
}