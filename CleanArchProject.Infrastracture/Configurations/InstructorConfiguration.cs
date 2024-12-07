using Azure;
using CleanArchProject.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Infrastracture.Configurations
{
    public class InstructorConfiguration : IEntityTypeConfiguration<Instructor>
    {
        public void Configure(EntityTypeBuilder<Instructor> builder)
        {
            builder.ToTable("Instructors");
            builder.HasKey(x => x.InsId);
            builder.Property(x => x.ENameAr).HasMaxLength(500).HasColumnType("NVARCHAR").IsRequired();
            builder.Property(x => x.EName).HasMaxLength(500).HasColumnType("VARCHAR").IsRequired();

            builder.HasOne(md => md.ManagedDepartment).WithOne(m => m.Manager)
                .HasForeignKey<Department>(md => md.InsId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.Supervisor).WithMany(s => s.SupervisedInstructors)
                .HasForeignKey(s => s.SupervisorId).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(i => i.Departments)
                .WithMany(d => d.Instructors)
                .UsingEntity<Dept_Instructor>();



            builder.HasMany(i => i.Subjects)
                .WithMany(d => d.Instructors)
                .UsingEntity<Subj_Instructor>();


        }
    }
}
