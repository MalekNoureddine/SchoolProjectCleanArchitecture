using CleanArchProject.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Featurs.Departments.Commands.Models
{
    public class AddDepartmentCommand : IRequest<Response<string>>
    {
        public AddDepartmentCommand(string departmentName, string departmentArabicName, int managerInstructorId)
        {
            DepartmentName = departmentName;
            DepartmentArabicName = departmentArabicName;
            ManagerInstructorId = managerInstructorId;
        }

        public string DepartmentName { get; set; }
        public string DepartmentArabicName { get; set; }
        public int ManagerInstructorId { get; set; }
    }
}
