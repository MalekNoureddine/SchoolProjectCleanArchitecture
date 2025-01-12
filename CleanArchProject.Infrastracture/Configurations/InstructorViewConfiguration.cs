using CleanArchProject.Data.Entities;
using CleanArchProject.Data.Entities.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Infrastracture.Configurations
{
    public class InstructorViewConfiguration : IEntityTypeConfiguration<InstructorsView>
    {
        public void Configure(EntityTypeBuilder<InstructorsView> builder)
        {
            builder.HasNoKey();
            builder.ToView("InstructorsView");
        }
    }
}
