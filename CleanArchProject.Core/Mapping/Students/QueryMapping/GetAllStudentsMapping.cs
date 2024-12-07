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
        private void GetAllStudentsMapping()
        {
            CreateMap<Student, GetAllStudentsResponse>()
                .ForMember(dest => dest.DepartmenName, opt => opt.MapFrom(src => src.Department.DName))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.GetLocalized(src.Name, src.NameAr)));
        }
    }
}
