using CleanArchProject.Data.Entities;
using SchoolProject.Core.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Featurs.Instructors.Queries.Response
{
    public class GetInstructorByIdResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public int? SupervisorId { get; set; }
        public string? Image { get; set; }
        public Supervisor supervisor { get; set; }
        public PaginatedResult<Supervised> Superviseds { get; set; }
    }

    public class Supervisor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
    }
    public class Supervised
    {
        public Supervised(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
