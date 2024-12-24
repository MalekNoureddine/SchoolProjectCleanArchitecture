using CleanArchProject.Core.Bases;
using CleanArchProject.Core.Featurs.Authorization.Queries.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Featurs.Authorization.Queries.Models
{
    public class GetRoleByIdQuery : IRequest<Response<GetRoleResponse>>
    {
        public int Id { get; set; }
    }
}
