using CleanArchProject.Core.Bases;
using CleanArchProject.Core.Featurs.Instructors.Queries.Response;
using CleanArchProject.Core.Featurs.Instructors.Queries.Response.View;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Featurs.Instructors.Queries.Models.View
{
    public class GetInstructorViewByIdQuery : IRequest<Response<InstructorViewResponse>>
    {
        public int InstructorId { get; set; }
    }
}
