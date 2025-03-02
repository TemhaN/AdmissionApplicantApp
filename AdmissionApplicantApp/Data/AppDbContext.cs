using System.Data.Entity;
using AdmissionApplicantApp.Models;

namespace AdmissionApplicantApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("name=AdmissionDB") { }

        public DbSet<Application> Applications { get; set; }  // Добавляем DbSet для Application
        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<ExamResult> ExamResults { get; set; }  // Добавьте эту строку
        public DbSet<Specialization> Specializations { get; set; }
    }
}
