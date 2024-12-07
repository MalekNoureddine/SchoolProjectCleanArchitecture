using AutoMapper;
using CleanArchProject.Core.Featurs.Students.Queries.Response;
using CleanArchProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Mapping.Students
{
    public partial class StudentProfile : Profile
    {
        public StudentProfile()
        {
            GetAllStudentsMapping();
            GetAStudentMapping();
            AddStudentMapping();
            EditStudentMapping();
        }
    }
}
