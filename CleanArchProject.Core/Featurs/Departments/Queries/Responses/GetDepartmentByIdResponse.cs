using SchoolProject.Core.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Featurs.Departments.Queries.Response
{
    public class GetDepartmentByIdResponse 
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public string ManagerName { get; set; }
        public string ManagerId { get; set; }
        public PaginatedResult<StudentResponse>? StudentList { get; set; }
        public List<SubjectResponse>? SubjectList { get; set; }
        public List<InstructorResponse>? InstructorList { get; set; }
    }

    public class InstructorResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class SubjectResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class StudentResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public StudentResponse(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
