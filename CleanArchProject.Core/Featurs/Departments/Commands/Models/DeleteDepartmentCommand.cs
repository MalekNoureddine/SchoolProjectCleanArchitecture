using CleanArchProject.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Featurs.Departments.Commands.Models
{
    public class DeleteDepartmentCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public DeleteDepartmentCommand(int id)
        {
            Id = id;
        }
    }
}
