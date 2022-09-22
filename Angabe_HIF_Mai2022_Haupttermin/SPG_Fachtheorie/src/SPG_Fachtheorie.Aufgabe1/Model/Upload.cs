using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPG_Fachtheorie.Aufgabe1.Model
{
    public class Upload
    {
        public int Id { get; set; }
        public int MyProperty { get; set; }
        public Applicant ApplicantNavigation { get; set; } = default!;
        public int ApplicantId { get; set; }

        public Task TaskNavigation { get; set; } = default!;
        public int TaskId { get; set; }
    }
}
