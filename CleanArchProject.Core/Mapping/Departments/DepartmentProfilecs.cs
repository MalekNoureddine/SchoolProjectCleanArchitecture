using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Mapping.Departments
{
    public partial class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            GetAllDepartmentsMapping();
            GetDepartmentByIdMapping();
            AddDepartmentMapping();
            EditDepartmentMapping();
        }
    }
}
