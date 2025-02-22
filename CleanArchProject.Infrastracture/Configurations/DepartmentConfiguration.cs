﻿using CleanArchProject.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchProject.Infrastracture.Configurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("Departments");
            builder.HasKey(x => x.DID);
            builder.Property(x => x.DNameAr).HasMaxLength(500).HasColumnType("NVARCHAR").IsRequired();
            builder.Property(x => x.DName).HasMaxLength(500).HasColumnType("VARCHAR").IsRequired();
        }
    }
}
