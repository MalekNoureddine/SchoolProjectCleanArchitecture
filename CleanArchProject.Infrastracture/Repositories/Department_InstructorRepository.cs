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
    public class Department_InstructorRepository : GenericRepositoryAsync<Dept_Instructor>, IDepartmentInstructorRepository
    {
        #region Fields
        private readonly DbSet<Dept_Instructor> _dept_Instructor;
        #endregion

        #region Constructors
        public Department_InstructorRepository(AppDbContext context) : base(context)
        {
            _dept_Instructor = context.Dept_Instructors;
        }
        #endregion

        #region Functions Handle
        public async Task<List<Dept_Instructor>> GetDept_InstructorsAsync(bool includeEntities = false)
        {

            List<Dept_Instructor> dept_Instructor;
            if (includeEntities)
                dept_Instructor = await _dept_Instructor
                .Include(x => x.Department)
                .Include(x => x.Instructor)
                .ToListAsync();
            else
                dept_Instructor = await _dept_Instructor.ToListAsync();

            return dept_Instructor;
        }
        #endregion

    }
}