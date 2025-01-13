using CleanArchProject.Data.Entities;
using CleanArchProject.Data.Entities.Procedures;
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
    public class DepartmentStudentCountProcConfiguration : IEntityTypeConfiguration<DepartmentStudentCountProc>
    {
        public void Configure(EntityTypeBuilder<DepartmentStudentCountProc> builder)
        {
            builder.HasNoKey();
        }
    }
}
