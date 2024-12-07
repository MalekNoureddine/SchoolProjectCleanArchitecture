using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Featurs.Departments.Queries.Response
{
    public class GetPagenatedDepartmentsResponse
    {
        public GetPagenatedDepartmentsResponse(int departmentID, string departmentName, int managerId)
        {
            DepartmentID = departmentID;
            DepartmentName = departmentName;
            ManagerId = managerId;
        }

        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public int ManagerId { get; set; }
    }
}
