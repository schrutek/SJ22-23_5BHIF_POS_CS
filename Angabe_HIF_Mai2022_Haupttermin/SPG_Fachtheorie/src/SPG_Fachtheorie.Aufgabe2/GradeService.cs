using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPG_Fachtheorie.Aufgabe2
{
    public class GradeService
    {
        public class ClassStatistics
        {
            public string ClassName { get; set; }
            public int PositiveStudentsCount { get; set; }
            public int NegativeStudentsCount { get; set; }
            public List<SubjectStatistics> SubjectStatistics { get; set; }
        }

        public class SubjectStatistics
        {
            public string Shortname { get; set; }
            public int PositiveCount { get; set; }
            public int NegativeCount { get; set; }
            public decimal AverageGrade { get; set; }
        }

        private readonly GradeContext _db;

        public GradeService(GradeContext db)
        {
            _db = db;
        }

        public ClassStatistics GetClassStatistics(string @class)
        {
            ClassStatistics classStatistics = new ClassStatistics();
            classStatistics.NegativeStudentsCount = _db.Students
                .Count(s => s.Class.Name == @class && s.Grades.Any(g => g.GradeValue == 5));
            classStatistics.PositiveStudentsCount = _db.Students
                .Count(s => s.Class.Name == @class && s.Grades.All(g => g.GradeValue <= 4 && g.GradeValue >= 1));

            List<SubjectStatistics> subjectStatistics = new List<SubjectStatistics>();
            foreach (Lesson lesson in _db.Lessons)
            {
                classStatistics.SubjectStatistics.Add(new SubjectStatistics()
                {
                    NegativeCount = _db.Grades
                        .Count(g => g.Lesson.Id == lesson.Id && g.Lesson.Class.Name == @class && g.GradeValue == 5),
                    PositiveCount = _db.Grades
                        .Count(g => g.Lesson.Id == lesson.Id && g.Lesson.Class.Name == @class && g.GradeValue != 5),
                    AverageGrade = (decimal)_db.Grades
                        .Where(g => g.Lesson.Id == lesson.Id && g.Lesson.Class.Name == @class).Average(g => g.GradeValue)
                });
            }

            return classStatistics;
        }

        public bool TryAddRegistration(Student student, string subjectShortname, DateTime date)
        {
            // Init
            Grade existingGrade = _db.Grades
                .SingleOrDefault(g => g.Lesson.Subject.ShortName == subjectShortname 
                              && g.Student.Id == student.Id 
                              && g.Student.Class.Name == student.Class.Name);

            // Validation

            // Act
            Exam newExam = new Exam()
            {
                Date = date,
                Grade = existingGrade,
            };
            _db.Add(newExam);

            // Save
            try
            {
                _db.SaveChanges();
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }
    }
}
