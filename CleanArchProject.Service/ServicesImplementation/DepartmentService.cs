using CleanArchProject.Data.Entities;
using CleanArchProject.Data.Enums;
using CleanArchProject.Infrastracture.Interfaces;
using CleanArchProject.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Service.ServicesImplementation
{
    public class DepartmentService : IDepartmentService
    {
        #region Fields
        private readonly IDepartmentRepository _departmentService;
        #endregion

        #region Constructors
        public DepartmentService(IDepartmentRepository department)
        {
            _departmentService = department;
        }
        #endregion

        #region FunctionHandle
        public async Task<string> AddDepartmentAsync(Department department)
        {
            var transaction = _departmentService.BeginTransaction();
            try
            {
                await _departmentService.AddAsync(department);
                transaction.Commit();
                return "Created";
            }
            catch (DbUpdateException ex)
            {
                transaction.Rollback();
                return "Conflict";
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<string> DeleteDepartmentAsync(Department department)
        {
            var transaction = _departmentService.BeginTransaction();
            try
            {
                await _departmentService.DeleteAsync(department);
                transaction.Commit();
                return "Deleted";
            }
            catch (Exception)
            {

                transaction.Rollback();
                return "Faild";
            }
        }

        public async Task<string> EditDepartmentAsync(Department department)
        {
            var transaction = _departmentService.BeginTransaction();
            try
            {
                await _departmentService.UpdateAsync(department);
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

        public async Task<List<Department>> GetAllDepartmentsAsync(bool includeEntities = false)
        {
            var departments = await _departmentService.GetDepartmentsAsync(includeEntities);
            return departments;
        }

        public IQueryable<Department> GetAllDepartmentsQuerable()
        {
            return _departmentService.GetTableNoTracking()
                .Include(d => d.Manager)
                .Include(d => d.DepartmentSubjects)
                .Include(d => d.Instructors)
                .AsQueryable();
        }

        public async Task<Department> GetDepartmentByIdAsync(int id, bool includeEntities = false)
        {
            Department? department;
            if (includeEntities)
                department = await _departmentService.GetTableNoTracking()
                .Include(d => d.Manager)
                .Include(d => d.DepartmentSubjects).ThenInclude(npp => npp.Subject)
                .Include(d => d.Instructors)
                .FirstOrDefaultAsync(s => s.DID == id);

            else
                department = await _departmentService.GetTableNoTracking()
                .FirstOrDefaultAsync(s => s.DID == id);

            return department;
        }

        public IQueryable<Department> GetFilteredDepartmentsQuerable(enDepartmentOrderingEnum orderBy, string search)
        {
            var result = GetAllDepartmentsQuerable();
            if (!string.IsNullOrEmpty(search))
            {
                result = result.Where(s => s.DName.Contains(search) || s.DNameAr.Contains(search) ||
                s.Manager.ENameAr.Contains(search) ||
                s.Manager.EName.Contains(search) ||
                s.Manager.Address.Contains(search));
            }
            switch (orderBy)
            {
                case enDepartmentOrderingEnum.DepartmentId:
                    result = result.OrderBy(s => s.DID);
                    break;
                case enDepartmentOrderingEnum.Name:
                    result = result.OrderBy(s => s.DName);
                    break;
                case enDepartmentOrderingEnum.ManagerName:
                    result = result.OrderBy(s => s.Manager.EName);
                    break;
                case enDepartmentOrderingEnum.StudentsNumber:
                    result = result.OrderBy(s => s.Students.Count);
                    break;
                case enDepartmentOrderingEnum.InstructorsNumber:
                    result = result.OrderBy(s => s.Instructors.Count);
                    break;
                default:
                    result = result.OrderBy(s => s.DID);
                    break;
            }
            return result;
        }

        public async Task<bool> IsDepartmentExists(int Id)
        {
            return await _departmentService.GetTableNoTracking().AnyAsync(s => s.DID == Id);
        }

        public async Task<bool> IsDepartmentNameExists(string DepartmentName)
        {
            var checkDepartmentName = await _departmentService.GetTableNoTracking()
                .Where(s =>  s.DName.Equals(DepartmentName)).FirstOrDefaultAsync();
            if (checkDepartmentName != null) return true;
            return false;
        }

        public async Task<bool> IsDepartmentArabicNameExists(string DepartmentArabicName)
        {
            var checkDepartmentName = await _departmentService.GetTableNoTracking()
                .Where(s => s.DNameAr.Equals(DepartmentArabicName)).FirstOrDefaultAsync();
            if (checkDepartmentName != null) return true;
            return false;
        }

        public async Task<bool> IsDepartmentNameExistsById(string DepartmentName, int Id)
        {
            var checkDepartmentName = await _departmentService.GetTableNoTracking()
                .Where(s => (s.DName.Equals(DepartmentName)) && s.DID != Id).FirstOrDefaultAsync();
            if (checkDepartmentName != null) return true;
            return false;
        }

        public async Task<bool> IsDepartmentArabicNameExistsById(string DepartmentArabicName, int Id)
        {
            var checkDepartmentName = await _departmentService.GetTableNoTracking()
                .Where(s => (s.DNameAr.Equals(DepartmentArabicName)) && s.DID != Id).FirstOrDefaultAsync();
            if (checkDepartmentName != null) return true;
            return false;
        }
        #endregion

    }
}
