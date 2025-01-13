using CleanArchProject.Core.Bases;
using CleanArchProject.Core.Featurs.Departments.Queries.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Featurs.Departments.Queries.Models
{
    public class GetDepartmentStudentCountProcQuery : IRequest<Response<IReadOnlyList<GetDepartmentStudentCountProcResponse>>>
    {
    }
}
