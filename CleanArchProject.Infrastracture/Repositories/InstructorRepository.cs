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
    public class InstructorRepository : GenericRepositoryAsync<Instructor>, IInstructorRepository
    {
        #region Fields
        private readonly DbSet<Instructor> _instructor;
        #endregion
        #region Constructors
        public InstructorRepository(AppDbContext context) : base(context)
        {
            _instructor = context.Instructors;
        }

        #endregion

        #region Functions Handle
        public async Task<List<Instructor>> GetInstructorsAsync(bool includeEntities = false)
        {
            List<Instructor> instructor;
            if (includeEntities)
                instructor = await _instructor
                    .Include(i => i.Subjects)
                    .Include(i => i.Supervisor)
                    .Include(i => i.SupervisedInstructors)
                    .Include(i => i.Departments)
                    .Include(i => i.ManagedDepartment)
                .ToListAsync();
            else
                instructor = await _instructor.ToListAsync();

            return instructor;
        }
        #endregion

    }
}
