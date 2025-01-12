using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Featurs.Instructors.Queries.Response.View
{
    public class InstructorViewResponse
    {
        public InstructorViewResponse()
        {
                
        }
        public InstructorViewResponse(int instructorId, string name, string email, string position, string supervisorName)
        {
            InstructorId = instructorId;
            Name = name;
            Email = email;
            Position = position;
            SupervisorName = supervisorName;
        }

        public int InstructorId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public string SupervisorName { get; set; }
    }
}
