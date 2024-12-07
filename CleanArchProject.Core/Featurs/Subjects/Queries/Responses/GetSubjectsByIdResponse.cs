using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Featurs.Subject.Queries.Responses
{
    public class GetSubjectsByIdResponse
    {
        public int SubjectID { get; set; }
        public string SubjectName { get; set; }
        public int Period { get; set; }
        public List<InstructorsOfTheSubject> Instructors { get; set; } = new(); 
    }

    public class InstructorsOfTheSubject
    {
        public int id { get; set; }
        public string Name { get; set; }
    }
}
