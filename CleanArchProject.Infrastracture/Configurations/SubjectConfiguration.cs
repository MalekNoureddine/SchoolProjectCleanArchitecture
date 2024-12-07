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
    public class SubjectConfiguration : IEntityTypeConfiguration<Subjects>
    {
        public void Configure(EntityTypeBuilder<Subjects> builder)
        {
            builder.ToTable("Subjects");
            builder.HasKey(x => x.SubID);
            builder.Property(x => x.SubjectNameAr).HasColumnType("NVARCHAR").HasMaxLength(200).IsRequired();
            builder.Property(x => x.SubjectName).HasColumnType("VARCHAR").HasMaxLength(200).IsRequired();
            builder.Property(x => x.Period).HasColumnType("INT").IsRequired();
        }
    }
}
