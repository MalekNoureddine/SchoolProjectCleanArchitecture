using CleanArchProject.Data.Entities;
using CleanArchProject.Data.Entities.Identities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using CleanArchProject.Data.Entities.Identies;
using EntityFrameworkCore.EncryptColumn.Interfaces;
using EntityFrameworkCore.EncryptColumn.Util;
using EntityFrameworkCore.EncryptColumn.Extension;
using System.Reflection;


namespace CleanArchProject.Infrastracture.Data
{
    public class AppDbContext : IdentityDbContext<User, Role,int,IdentityUserClaim<int>,IdentityUserRole<int>,IdentityUserLogin<int>,IdentityRoleClaim<int>,IdentityUserToken<int>>
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
        public DbSet<User> User { get; set; }
        public DbSet<UserRefreshToken> UserRefreshToken { get; set; }
        public DbSet<ResetPassword> ResetPasswords { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);           
        }
    }
}
