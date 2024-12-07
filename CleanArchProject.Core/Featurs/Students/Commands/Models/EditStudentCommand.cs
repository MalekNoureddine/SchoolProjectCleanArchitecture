using CleanArchProject.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Featurs.Students.Commands.Models
{
    public class EditStudentCommand : IRequest<Response<string>>
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string NameAr { get; set; }

        public string Address { get; set; }
        public string Phone { get; set; }
        public int DepartmentId { get; set; }
        public EditStudentCommand(int id, string name, string nameAr, string address, string phone, int departmentId)
        {
            Id = id;
            Name = name;
            NameAr = nameAr;
            Address = address;
            Phone = phone;
            DepartmentId = departmentId;
        }
    }
}
