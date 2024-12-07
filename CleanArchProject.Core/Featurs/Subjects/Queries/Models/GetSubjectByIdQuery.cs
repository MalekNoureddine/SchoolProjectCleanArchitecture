using CleanArchProject.Core.Bases;
using CleanArchProject.Core.Featurs.Subject.Queries.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Featurs.Subject.Queries.Models
{
    public class GetSubjectByIdQuery : IRequest<Response<GetSubjectsByIdResponse>>
    {
        public int Id { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
