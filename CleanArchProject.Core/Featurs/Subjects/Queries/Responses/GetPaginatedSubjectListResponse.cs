using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Featurs.Subject.Queries.Responses
{
    public class GetPaginatedSubjectListResponse
    {
        public GetPaginatedSubjectListResponse(int subjectID, string subjectName, int period)
        {
            SubjectID = subjectID;
            SubjectName = subjectName;
            Period = period;
        }

        public int SubjectID { get; set; }
        public string SubjectName { get; set; }
        public int Period { get; set; }
    }
}
