using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPG_Fachtheorie.Aufgabe1
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions opt) : base(opt) { }
        // TODO: Füge die DbSet<T> Collections hinzu.

        public DbSet<Department> Departments => Set<Department>();
        public DbSet<Model.Task> Tasks => Set<Model.Task>();
        public DbSet<Upload> Uploads => Set<Upload>();
        public DbSet<Applicant> Applicants => Set<Applicant>();
        public DbSet<ApplicantStatus> ApplicantStatus => Set<ApplicantStatus>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>().Property(p => p.Name).IsRequired();
            modelBuilder.Entity<Department>().HasKey(p => p.Name);
        }
    }
}
