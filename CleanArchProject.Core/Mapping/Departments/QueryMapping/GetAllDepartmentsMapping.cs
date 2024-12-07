using CleanArchProject.Core.Featurs.Departments.Queries.Models;
using CleanArchProject.Core.Featurs.Departments.Queries.Response;
using CleanArchProject.Core.Featurs.Students.Queries.Response;
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
        private void GetAllDepartmentsMapping()
        {
            CreateMap<Department, GetAllDepartmentResponse>()
                .ForMember(dest => dest.ManagerId, opt => opt.MapFrom(src => src.InsId))
                .ForMember(dest => dest.ManagerName, opt => opt.MapFrom(src => src.GetLocalized(src.Manager.EName, src.Manager.ENameAr)))
                .ForMember(dest => dest.DepartmentID, opt => opt.MapFrom(src => src.DID))
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.GetLocalized(src.DName, src.DNameAr)));
        }
    }
}
