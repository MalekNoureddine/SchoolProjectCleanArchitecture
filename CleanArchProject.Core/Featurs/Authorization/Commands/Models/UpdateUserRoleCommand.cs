using CleanArchProject.Core.Bases;
using CleanArchProject.Data.Requests;
using CleanArchProject.Data.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Featurs.Authorization.Commands.Models
{
    public class UpdateUserRoleCommand : UpdateUserRoleRequest, IRequest<Response<string>>
    {
    }
}
