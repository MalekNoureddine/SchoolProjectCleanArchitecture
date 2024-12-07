using CleanArchProject.Core.Featurs.Students.Commands.Models;
using CleanArchProject.Core.Featurs.Students.Queries.Response;
using CleanArchProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Mapping.Students
{
    public partial class StudentProfile
    {
        private void AddStudentMapping()
        {
            CreateMap<AddStudentCommand ,Student>()
                .ForMember(dest => dest.DID, opt => opt.MapFrom(src => src.DepartmentId));
        }
    }
}
