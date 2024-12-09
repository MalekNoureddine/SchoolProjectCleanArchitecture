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
        private void GetPaginatedStudentsListMapping()
        {
            CreateMap<Student, GetStudentsListPagiatedResponse>()
                .ForMember(dest => dest.DepartmenName, opt => opt.MapFrom(src => src.GetLocalized(src.Department.DName, src.Department.DNameAr)))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.GetLocalized(src.Name, src.NameAr)))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.StudID, opt => opt.MapFrom(src => src.StudID))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));

        }
    }
}
