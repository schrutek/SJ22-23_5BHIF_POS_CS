using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using SPG_Fachtheorie.Aufgabe2;
using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe2.Model;

namespace SPG_Fachtheorie.Aufgabe2.Test
{
    [Collection("Sequential")]
    public class GradeServiceTests
    {
        /// <summary>
        /// Legt die Datenbank an und befüllt sie mit Musterdaten. Die Datenbank ist
        /// nach Ausführen des Tests ServiceClassSuccessTest in
        /// C:\Scratch\Aufgabe2_Test\bin\Debug\net6.0\Grades.db
        /// und kann mit SQLite Manager, DBeaver, ... betrachtet werden.
        /// </summary>
        private GradeContext GetContext(bool deleteDb = true)
        {
            var options = new DbContextOptionsBuilder()
                .UseSqlite("Data Source=Grades.db")
                .Options;

            var db = new GradeContext(options);
            if (deleteDb)
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                db.Seed();
            }
            return db;
        }

        private GradeContext SetupTest(GradeContext db)
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            Class newClass = new Class()
            {
                Id = new Guid("3eef9689-3664-47f4-9378-bfdf0226c69b"),
                Name = "6XKIF"
            };
            db.Classes.Add(newClass);
            db.SaveChanges();

            Student newStudent = new Student()
            {
                Firstname = "Student_01",
                Lastname = "Student_01",
                Email = "xy@gmx.at",
                Class = newClass,
                Id = new Guid("54696644-fe9b-4db7-9b8e-bded38390c79")
            };
            db.Students.Add(newStudent);
            db.SaveChanges();

            Teacher newTeacher = new Teacher()
            {
                Id = new Guid("69484312-fc27-4c6f-84bb-20dc9de8b42a"),
                Firstname = "Teacher_01",
                Lastname = "Teacher_01",
                Email = "xy@gmx.at"
            };
            db.Teachers.Add(newTeacher);
            db.SaveChanges();

            List<Subject> newSubjects = new List<Subject>()
            {
                new Subject() { LongName="POS", ShortName="POS" },
                new Subject() { LongName="DBI", ShortName="DBI" },
            };

            List<Lesson> newLessons = new List<Lesson>()
            {
                new Lesson() { Id=new Guid("8d413ab5-2c70-43e3-95b8-09636a4c60a8"), Class=newClass, Subject=newSubjects[0], Teacher=newTeacher },
                new Lesson() { Id=new Guid("733c8740-2364-40fe-8db6-95f5910dffea"), Class=newClass, Subject=newSubjects[1], Teacher=newTeacher },
            };
            db.Lessons.AddRange(newLessons);
            db.SaveChanges();

            List<Grade> newGrades = new List<Grade>()
            {
                new Grade() { Id=new Guid("4dde1f7d-31ad-46ff-8ed8-46e733411892"), GradeValue=1, Lesson=newLessons[0], Student=newStudent },
                new Grade() { Id=new Guid("7aeb2358-dc33-4ba5-a8c7-06e3485db3cc"), GradeValue=5, Lesson=newLessons[1], Student=newStudent },
            };
            db.Grades.AddRange(newGrades);
            db.SaveChanges();

            List<Exam> newExams = new List<Exam>()
            {
                new Exam() { Date=DateTime.Now, Grade= newGrades[0] }, // POS bereits vorhanden (Note 1)
            };
            db.Exams.AddRange(newExams);
            db.SaveChanges();

            return db;
        }

        /// <summary>
        /// Erzeugt die Datenbank in C:\Scratch\Aufgabe2_Test\Debug\net6.0
        /// </summary>
        [Fact]
        public void ServiceClassSuccessTest()
        {
            using var db = GetContext();
            Assert.True(db.Students.Count() > 0);
            Assert.True(db.Students.Include(s => s.Grades).First().Grades.Count() > 0);
            var service = new GradeService(db);
        }

        [Fact]
        public void TryAddRegistrationReturnsFalseIfSubjectDoesNotExist()
        {
            throw new NotImplementedException();
        }
        [Fact]
        public void TryAddRegistrationReturnsFalseIfSubjectIsNotNegative()
        {
            throw new NotImplementedException();
        }
        [Fact]
        public void TryAddRegistrationReturnsFalseIfExamExists()
        {
            throw new NotImplementedException();
        }
        [Fact]
        public void TryAddRegistrationReturnsFalseOnDateConflict()
        {
            throw new NotImplementedException();
        }
        [Fact]
        public void TryAddRegistrationReturnsSuccessTest()
        {
            // Arrange
            GradeContext db = SetupTest(GetContext());
            GradeService service = new GradeService(db);

            // Act
            service.TryAddRegistration(null, "", DateTime.Now);

            // Assert
        }
    }
}
