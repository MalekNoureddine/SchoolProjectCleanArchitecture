using CleanArchProject.Core.Bases;
using CleanArchProject.Core.Featurs.Subject.Queries.Responses;
using CleanArchProject.Data.Enums;
using MediatR;
using SchoolProject.Core.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Featurs.Subject.Queries.Models
{
    public class GetPaginatedSubjectListQuery : IRequest<PaginatedResult<GetPaginatedSubjectListResponse>>
    {
        public int SubjectsPageNumber { get; set; }
        public int SubjectsPageSize { get; set; }
        public enSubjectsOrderingEnum OrderBy { get; set; }
        public string? Search { get; set; }
    }
}
