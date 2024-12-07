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
    public class Dept_InstructorConfiguration : IEntityTypeConfiguration<Dept_Instructor>
    {
        public void Configure(EntityTypeBuilder<Dept_Instructor> builder)
        {
            builder.ToTable("Dept_Instructor");
            builder.HasKey(di => new { di.InsId, di.DID });
        }
    }
}
