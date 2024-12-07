using CleanArchProject.Core.Featurs.Departments.Queries.Response;
using CleanArchProject.Core.Featurs.Students.Queries.Response;
using CleanArchProject.Data.Enums;
using MediatR;
using SchoolProject.Core.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Featurs.Departments.Queries.Models
{
    public class GetPaginatedDepartmentsListQuery : IRequest<PaginatedResult<GetPagenatedDepartmentsResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public enDepartmentOrderingEnum OrderBy { get; set; }
        public string? Search { get; set; }

    }
}
