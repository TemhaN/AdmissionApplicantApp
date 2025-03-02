using AdmissionApplicantApp.Models;
using System;

public class Application
{
    public int ApplicationID { get; set; }
    public int ApplicantID { get; set; } // Внешний ключ
    public int SpecializationID { get; set; }
    public DateTime SubmissionDate { get; set; }
    public string Status { get; set; }
    public int FacultyID { get; set; }

    // Связь с таблицей Applicants
    public virtual Applicant Applicant { get; set; }
    public virtual Faculty Faculty { get; set; }
    public virtual Specialization Specialization { get; set; }
}
