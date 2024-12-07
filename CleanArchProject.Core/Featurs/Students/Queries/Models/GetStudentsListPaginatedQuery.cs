using CleanArchProject.Core.Bases;
using CleanArchProject.Core.Featurs.Students.Queries.Response;
using CleanArchProject.Data.Enums;
using MediatR;
using SchoolProject.Core.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Featurs.Students.Queries.Models
{
    public class GetStudentsListPaginatedQuery : IRequest<PaginatedResult<GetStudentsListPagiatedResponse>>
    {
        public int StudentPageNumber { get; set; }
        public int StudentPageSize { get; set; }
        public enStudentOrderingEnum OrderBy { get; set; }
        public string? Search { get; set; }

    }
}
