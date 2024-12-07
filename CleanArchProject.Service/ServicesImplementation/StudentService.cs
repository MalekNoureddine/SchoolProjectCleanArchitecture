using CleanArchProject.Data.Entities;
using CleanArchProject.Data.Enums;
using CleanArchProject.Infrastracture.Interfaces;
using CleanArchProject.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Service.ServicesImplementation
{
    public class StudentService : IStudentService
    {
        #region Fields
        private readonly IStudentRepository _studentService;
        #endregion

        #region Constructors
        public StudentService(IStudentRepository student)
        {
            _studentService = student;
        }

        #endregion

        #region Handle Functions
        public async Task<List<Student>> GetAllStudentsAsync()
        {
            var students =  await _studentService.GetStudentsAsync();
            return students;
        }

        public IQueryable<Student> GetAllStudentsQuerable()
        {
            return _studentService.GetTableNoTracking().Include(s => s.Department).AsQueryable();
        }

        public IQueryable<Student> GetFilteredStudentsQuerable(enStudentOrderingEnum orderBy, string search)
        {  
            var result = _studentService.GetTableNoTracking().Include(s => s.Department).AsQueryable();
            if (!search.IsNullOrEmpty())
            {
                result = result.Where(s =>s.Name.Contains(search) || s.Department.DName.Contains(search) || s.Address.Contains(search));
            }
            switch (orderBy)
            {
                case enStudentOrderingEnum.StudID:
                    result = result.OrderBy(s => s.StudID);
                    break;
                case enStudentOrderingEnum.Name:
                    result = result.OrderBy(s => s.Name);
                    break;
                case enStudentOrderingEnum.Address:
                    result = result.OrderBy(s => s.Address);
                    break;
                case enStudentOrderingEnum.DepartmentName:
                    result = result.OrderBy(s => s.Department.DName);
                    break;
                default:
                    result = result.OrderBy(s => s.StudID);
                    break;
            }
            return result;
        }

        public async Task<Student> GetStudentByIdAsync(int id, bool includeDepartment = false)
        {
            //var student =  _studentService.GetByIdAsync(id);
            Student? student;
            if (includeDepartment)
                 student = await _studentService.GetTableNoTracking()
                .Include(s=>s.Department)
                .FirstOrDefaultAsync(s => s.StudID == id);
            
            else
                student = await _studentService.GetTableNoTracking()
                .FirstOrDefaultAsync(s => s.StudID == id);

            return student;
        }

        public async Task<bool> IsPhoneNumberExists(string phone)
        {
            var checkStudent = await _studentService.GetTableNoTracking().Where(s => s.Phone.Equals(phone)).FirstOrDefaultAsync();
            if (checkStudent != null) return true;
            return false;
        }

        public async Task<bool> IsPhoneNumberExists(string phone, int id)
        {
            var checkStudent = await _studentService.GetTableNoTracking().Where(s => s.Phone.Equals(phone) && s.StudID != id).FirstOrDefaultAsync();
            if (checkStudent != null) return true;
            return false;
        }
        public async Task<string> AddStudentAsync(Student student)
        {
            var transaction = _studentService.BeginTransaction();
            try
            {
                await _studentService.AddAsync(student);
                transaction.Commit();
                return "Created";
            }
            catch (DbUpdateException ex)
            {
                transaction.Rollback();
                return "Conflict";
            }
            catch(Exception ex) {
                transaction.Rollback();
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<string> DeleteStudentAsync(Student student)
        {
            var transaction = _studentService.BeginTransaction();
            try
            {
                await _studentService.DeleteAsync(student);
                transaction.Commit();
                return "Deleted";
            }
            catch (Exception)
            {

                transaction.Rollback();
                return "Faild";
            }
        }

        public async Task<string> EditStudentAsync(Student student)
        {
            var transaction = _studentService.BeginTransaction();
            try
            {
                await _studentService.UpdateAsync(student);
                transaction.Commit();
                return "Success";
            }
            catch (DbUpdateException)
            {
                transaction.Rollback();
                return "Conflict";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.ToString());
                transaction.Rollback();
                throw;
            }
            
        }

        public IQueryable<Student> GetAllStudentsByDepartmentQuerable(int DepId)
        {
            return _studentService.GetTableNoTracking().Where(s => s.DID == DepId).AsQueryable();
        }
        #endregion

    }
}
