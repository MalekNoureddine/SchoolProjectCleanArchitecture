using CleanArchProject.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Featurs.Departments.Commands.Models
{
    public class EditDepartmentCommand : IRequest<Response<string>>
    {

        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentArabicName { get; set; }
        public int ManagerInstructorId { get; set; }
    }
}
