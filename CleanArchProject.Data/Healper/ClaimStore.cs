using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Data.Healper
{
    public static class ClaimStore
    {
        public static List<Claim> Claims = new()
        {
            new Claim("RetriveStudentLists","false"),
            new Claim("CreateStudent","false"),
            new Claim("EditStudent","false"),
            new Claim("DeleteStudent","false"),
            new Claim("SendingEmails","false"),
        };
    }
}
