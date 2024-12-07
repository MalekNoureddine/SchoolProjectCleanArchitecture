using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Featurs.Subject.Queries.Responses
{
    public class GetSubjectListResponse
    {
        public int SubjectID { get; set; }
        public string SubjectName { get; set; }
        public int Period { get; set; }
    }
}
