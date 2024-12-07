using CleanArchProject.Data.Entities;
using CleanArchProject.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Service.Interfaces
{
    public interface IStudentService
    {
        public Task<List<Student>> GetAllStudentsAsync();
        public Task<Student> GetStudentByIdAsync(int id, bool includeDepartment = false);
        public Task<string> AddStudentAsync(Student student);
        public Task<string> EditStudentAsync(Student student);
        public Task<bool> IsPhoneNumberExists(string phone);
        public Task<bool> IsPhoneNumberExists(string phone, int id);
        Task<string> DeleteStudentAsync(Student student);
        public IQueryable<Student> GetAllStudentsQuerable();
        public IQueryable<Student> GetAllStudentsByDepartmentQuerable(int DepId);
        IQueryable<Student> GetFilteredStudentsQuerable(enStudentOrderingEnum orderBy, string search);
    }
}
