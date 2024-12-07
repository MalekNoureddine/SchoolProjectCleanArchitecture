using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Featurs.Departments.Queries.Response
{
    public class GetAllDepartmentResponse 
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public int ManagerId { get; set; }
        public string ManagerName { get; set; }
    }
}
