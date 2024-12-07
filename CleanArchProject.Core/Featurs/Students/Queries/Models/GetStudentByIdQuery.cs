using CleanArchProject.Core.Bases;
using CleanArchProject.Core.Featurs.Students.Queries.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Featurs.Students.Queries.Models
{
    public class GetStudentByIdQuery : IRequest<Response<GetAStudentResponse>>
    {
        public int Id { get; set; }

        public GetStudentByIdQuery(int id)
        {
            Id = id;
        }
    }
}
