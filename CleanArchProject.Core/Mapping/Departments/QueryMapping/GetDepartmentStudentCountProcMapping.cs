using CleanArchProject.Core.Featurs.Departments.Queries.Responses;
using CleanArchProject.Data.Entities.Procedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Mapping.Departments
{
    public partial class DepartmentProfile
    {
        private void GetDepartmentStudentCountProcMapping()
        {
            CreateMap<DepartmentStudentCountProc, GetDepartmentStudentCountProcResponse>();
        }
    }
}
