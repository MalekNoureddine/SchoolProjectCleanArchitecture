using CleanArchProject.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Featurs.Subject.Commands.Modles
{
    public class DeleteSubjectCommand : IRequest<Response<string>>
    {
        public int Id {  get; set; }

        public DeleteSubjectCommand(int id)
        {
            Id = id;
        }
    }
}
