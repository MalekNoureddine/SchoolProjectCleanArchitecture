using CleanArchProject.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Featurs.Instructors.Commands.Models
{
    public class EditInstructorCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public string NameInArabic { get; set; }
        public string Name { get; set; }
        [Phone]
        public string Phone { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Address { get; set; }
        public string Position { get; set; }
        public int? SupervisorId { get; set; }
        public decimal? Salary { get; set; }
        public string? Image { get; set; }
    }
}
