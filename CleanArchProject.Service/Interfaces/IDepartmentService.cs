using CleanArchProject.Data.Entities;
using CleanArchProject.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Service.Interfaces
{
    public interface IDepartmentService
    {
        public Task<List<Department>> GetAllDepartmentsAsync(bool includeEntities = false);
        public Task<Department> GetDepartmentByIdAsync(int id, bool includeEntities = false);
        public Task<string> AddDepartmentAsync(Department department);
        public Task<string> EditDepartmentAsync(Department department);
        public Task<bool> IsDepartmentNameExists(string DepartmentName, string DepartmentArabicName);
        public Task<bool> IsDepartmentNameExists(string DepartmentName, string DepartmentArabicName, int Id);
        Task<string> DeleteDepartmentAsync(Department department);
        public IQueryable<Department> GetAllDepartmentsQuerable();
        IQueryable<Department> GetFilteredDepartmentsQuerable(enDepartmentOrderingEnum orderBy, string search);
    }
}
