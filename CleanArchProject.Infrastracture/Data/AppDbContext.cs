using Azure;
using CleanArchProject.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Infrastracture.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(){
                
        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {

        }
        public DbSet<Department> Department { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<DepartmetSubject> DepartmetSubject { get; set; }
        public DbSet<StudentSubject> StudentSubject { get; set; }
        public DbSet<Subjects> Subjects { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Dept_Instructor> Dept_Instructors { get; set; }
        public DbSet<Subj_Instructor> Subj_Instructors { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
