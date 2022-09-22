using System.Collections.Generic;

namespace SPG_Fachtheorie.Aufgabe1.Model
{
    public class Applicant
    {
        public Applicant(Department department, ApplicantStatus applicantStatus)
        {
            DepartmentNavigation = department;
            ApplicantStatusNavigation = applicantStatus;
        }

        public int Id { get; set; } // Id wird automatisch zu PK ; int wird autom. zu autoincrement
        public List<Upload> Uploads { get; set; } = new();

        public Department DepartmentNavigation { get; set; } = default!;
        public int DepartmentId { get; set; }

        public ApplicantStatus ApplicantStatusNavigation { get; set; } = default!;
        public int ApplicantStatusId { get; set; }
    }
}
