using CleanArchProject.Core.Featurs.Departments.Commands.Models;
using CleanArchProject.Core.Featurs.Students.Commands.Models;
using CleanArchProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Mapping.Departments
{
    public partial class DepartmentProfile
    {
        void AddDepartmentMapping()
        {
            CreateMap<AddDepartmentCommand, Department>()
                .ForMember(dest => dest.DName, opt => opt.MapFrom(src => src.DepartmentName))
                .ForMember(dest => dest.DNameAr, opt => opt.MapFrom(src => src.DepartmentArabicName))
                .ForMember(dest => dest.InsId, opt => opt.MapFrom(src => src.ManagerInstructorId));
        }
    }
}
