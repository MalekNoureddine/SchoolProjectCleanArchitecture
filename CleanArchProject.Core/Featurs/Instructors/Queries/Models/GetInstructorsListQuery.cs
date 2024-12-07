using CleanArchProject.Core.Bases;
using CleanArchProject.Core.Featurs.Instructors.Queries.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Featurs.Instructors.Queries.Models
{
    public class GetInstructorsListQuery : IRequest<Response<List<GetInstructorsListResponse>>>
    {

    }
}
