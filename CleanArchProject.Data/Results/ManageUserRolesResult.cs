using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Data.Results
{
    public class ManageUserRolesResult
    {
        public int UserId { get; set; }
        public List<Roles> Roles { get; set; } = new();
    }
    public class Roles
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool HasRole { get; set; }
    }
}
