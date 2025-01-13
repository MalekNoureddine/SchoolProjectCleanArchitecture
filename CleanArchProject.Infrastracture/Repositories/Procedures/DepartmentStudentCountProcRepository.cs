using CleanArchProject.Data.Entities.Procedures;
using CleanArchProject.Infrastracture.Data;
using CleanArchProject.Infrastracture.Interfaces.Procedures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Infrastracture.Repositories.Procedures
{
    public class DepartmentStudentCountProcRepository : IDepartmentStudentCountProcRepository
    {
        #region Fields
        private readonly AppDbContext _context;
        #endregion
        #region Constructors
        public DepartmentStudentCountProcRepository(AppDbContext context)
        {
            _context = context;
        }
        #endregion
        #region Handle Functions
        public async Task<IReadOnlyList<DepartmentStudentCountProc>> GetDepartmentStudentCountProcs()
        {
            var rows = new List<DepartmentStudentCountProc>();
            rows = await _context.DepartmentStudentCountProc.FromSql($"EXEC dbo.DepartmentStudentCountProc").ToListAsync();
            return rows;
        }
        #endregion

    }
}
