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
    public class StudentSubjectConfiguration : IEntityTypeConfiguration<StudentSubject>
    {
        public void Configure(EntityTypeBuilder<StudentSubject> builder)
        {
            builder.HasKey(x => new {x.StudID, x.SubID});
            builder.HasOne(s => s.Student).WithMany(s => s.Stud_Subjects).HasForeignKey(s => s.StudID);  
            builder.HasOne(s => s.Subject).WithMany(s => s.StudentsSubjects).HasForeignKey(s => s.SubID);
        }
    }
}
