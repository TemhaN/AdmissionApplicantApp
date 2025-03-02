using System.ComponentModel.DataAnnotations;

namespace AdmissionApplicantApp.Models
{
    public class ExamResult
    {
        [Key] 
        public int ResultID { get; set; } 
        public int ApplicationID { get; set; } 
        public int StageID { get; set; }
        public int ExamScore { get; set; }

        public Application Application { get; set; }
    }
}