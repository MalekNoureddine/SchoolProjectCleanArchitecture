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
    public class Subj_InstructorConfiguration : IEntityTypeConfiguration<Subj_Instructor>
    {
        public void Configure(EntityTypeBuilder<Subj_Instructor> builder)
        {
            builder.HasKey(di => new { di.InsId, di.SubID });
        }
    }
}
