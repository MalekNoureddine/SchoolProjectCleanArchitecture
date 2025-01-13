using CleanArchProject.Data.Entities.Procedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Infrastracture.Interfaces.Procedures
{
    public interface IDepartmentStudentCountProcRepository
    {
        public Task<IReadOnlyList<DepartmentStudentCountProc>> GetDepartmentStudentCountProcs();
    }
}
