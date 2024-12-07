using CleanArchProject.Core.Featurs.Instructors.Queries.Response;
using CleanArchProject.Data.Enums;
using MediatR;
using SchoolProject.Core.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Featurs.Instructors.Queries.Models
{
    public class GetPaginatedInstructorsListQuery : IRequest<PaginatedResult<GetPaginatedInstructorsListResponse>>
    {
        public int InstructorsPageNumber { get; set; }
        public int InstructorsPageSize { get; set; }
        public enInstructorsOrderingEnum OrderBy { get; set; }
        public string? Search { get; set; }
    }

}
