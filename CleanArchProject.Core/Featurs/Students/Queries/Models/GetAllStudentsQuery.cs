using CleanArchProject.Core.Bases;
using CleanArchProject.Core.Featurs.Students.Queries.Response;
using CleanArchProject.Data.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Featurs.Students.Queries.Models
{
    public class GetAllStudentsQuery : IRequest<Response<List<GetAllStudentsResponse>>>
    {
    }
}
