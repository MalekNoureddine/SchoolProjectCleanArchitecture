using CleanArchProject.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Featurs.Instructors.Commands.Models
{
    public class DeleteInstructorCommand : IRequest<Response<string>>
    {
        public DeleteInstructorCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }

    }
}
