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
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Students");
            builder.HasKey(x => x.StudID);
            builder.Property(x => x.NameAr).HasColumnType("NVARCHAR").HasMaxLength(100).IsRequired();
            builder.Property(x => x.Name).HasColumnType("VARCHAR").HasMaxLength(200).IsRequired();
            builder.Property(x => x.Address).HasColumnType("VARCHAR").HasMaxLength(200).IsRequired();
            builder.Property(x => x.Phone).HasColumnType("VARCHAR").HasMaxLength(15).IsRequired();

            builder.HasOne(s => s.Department).WithMany(d => d.Students).HasForeignKey(s => s.DID);
        }
    }
}
