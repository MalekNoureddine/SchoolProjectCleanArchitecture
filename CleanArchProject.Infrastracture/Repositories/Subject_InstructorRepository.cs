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
    internal class Subject_InstructorRepository : GenericRepositoryAsync<Subj_Instructor>, ISubjectInstructorRepository
    {
        #region Fields
        private readonly DbSet<Subj_Instructor> _subjectInstructor;
        #endregion
        #region Constructors
        public Subject_InstructorRepository(AppDbContext context) : base(context)
        {
            _subjectInstructor = context.Subj_Instructors;
        }
        #endregion

        #region Functions Handle
        public async Task<List<Subj_Instructor>> GetSubj_InstructorsAsync(bool includeEntities = false)
        {
            List<Subj_Instructor> subjectInstructor;
            if (includeEntities)
                subjectInstructor = await _subjectInstructor
                .Include(x => x.Instructor)
                .Include(x => x.Subject)
                .ToListAsync();
            else
                subjectInstructor = await _subjectInstructor.ToListAsync();

            return subjectInstructor;
        }
        #endregion

    }
}
