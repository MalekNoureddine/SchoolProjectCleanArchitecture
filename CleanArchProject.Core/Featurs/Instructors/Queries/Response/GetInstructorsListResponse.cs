using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Featurs.Instructors.Queries.Response
{
    public class GetInstructorsListResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Position { get; set; }
        public int? SupervisorId { get; set; }
        public string? Image { get; set; }
    }
}
