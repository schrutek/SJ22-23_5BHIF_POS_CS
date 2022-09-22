using System;

namespace SPG_Fachtheorie.Aufgabe1.Model
{
    public class Task
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        public Department DepartmentNavigation { get; set; } = default!;
        public int DepartmentId { get; set; }
    }
}
