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
    public class DepartmentSubjectConfiguration : IEntityTypeConfiguration<DepartmetSubject>
    {
        public void Configure(EntityTypeBuilder<DepartmetSubject> builder)
        {
            builder.HasKey(x => new { x.DID, x.SubID });

            builder.HasOne(d => d.Department).WithMany(d => d.DepartmentSubjects).HasForeignKey(d => d.DID);
            builder.HasOne(d => d.Subject).WithMany(d => d.DepartmetSubjects).HasForeignKey(d => d.SubID);
        }
    }
}
