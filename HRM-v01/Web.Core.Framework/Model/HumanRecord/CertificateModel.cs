using System;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.Catalog;

/// <summary>
/// Summary description for CertificateModel
/// </summary>
public class CertificateModel
{
    private readonly hr_Certificate _certificate;

    public CertificateModel(hr_Certificate certificate)
    {
        _certificate = certificate ?? new hr_Certificate();
        Id = _certificate.Id;
        RecordId = _certificate.RecordId;
        CertificationId = _certificate.CertificationId;
        GraduationTypeId = _certificate.GraduationTypeId;
        EducationPlace = _certificate.EducationPlace;
        IsApproved = _certificate.IsApproved;
        Note = _certificate.Note;
        IssueDate = _certificate.IssueDate;
        ExpirationDate = _certificate.ExpirationDate;

    }
    /// <summary>
    /// id chung chi
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// id hồ sơ
    /// </summary>        
    public int RecordId { get; set; }

    /// <summary>
    /// id chung chi
    /// </summary>
    public int CertificationId { get; set; }

    /// <summary>
    /// ten chung chi
    /// </summary>
    public string CertificateName
    {
        get { return cat_CertificateServices.GetFieldValueById(_certificate.CertificationId); }
    }

    /// <summary>
    /// id xep loai
    /// </summary>
    public int GraduationTypeId { get; set; }

    /// <summary>
    /// ten xep loai
    /// </summary>
    public string GraduationTypeName
    {
        get { return cat_GraduationTypeServices.GetFieldValueById(_certificate.GraduationTypeId); }
    }

    /// <summary>
    /// Noi dao tao
    /// </summary>
    public string EducationPlace { get; set; }

    /// <summary>
    /// Ngay cap
    /// </summary>
    public DateTime? IssueDate { get; set; }

    /// <summary>
    /// Ngay het han
    /// </summary>
    public DateTime? ExpirationDate { get; set; }

    /// <summary>
    /// Duyet
    /// </summary>
    public bool IsApproved { get; set; }

    /// <summary>
    /// Ghi chu
    /// </summary>
    public string Note { get; set; }
}