using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Data.Entities.Views
{
    [Keyless]
    public class InstructorsView
    {
        public int InstructorId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Position { get; set; }
        public string? SupervisorName { get; set; }
    }
}
