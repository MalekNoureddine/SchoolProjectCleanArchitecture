using CleanArchProject.Data.Entities;
using CleanArchProject.Infrastracture.Data;
using CleanArchProject.Infrastracture.InfrastractureBases__generics_;
using CleanArchProject.Infrastracture.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Infrastracture.Repositories
{
    public class DepartmentRepository : GenericRepositoryAsync<Department>, IDepartmentRepository
    {
        #region Fields
        private readonly DbSet<Department> _departmend;
        #endregion
        #region Constructors
        public DepartmentRepository(AppDbContext context) : base(context)
        {
            _departmend = context.Department;
        }
        #endregion

        #region Functions Handle
        public async Task<List<Department>> GetDepartmentsAsync(bool includeEntities = false)
        {
            List<Department> department;
            if (includeEntities)
                department = await _departmend
                .Include(d => d.Manager)
                .Include(d => d.Instructors)
                .Include(d => d.Students)
                .ToListAsync();
            else
                department = await _departmend.ToListAsync();

            return department;
        }

        #endregion
    }
}
