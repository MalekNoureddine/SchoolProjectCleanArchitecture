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
    public class GetSubjectsListQuery : IRequest<Response<List<GetSubjectListResponse>>>
    {
    }
}
