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
    public class SubjectRepository : GenericRepositoryAsync<Subjects>, ISubjectRepository
    {
        #region Fields
        private readonly DbSet<Subjects> _subject;
        #endregion

        #region Constructors
        public SubjectRepository(AppDbContext context) : base(context)
        {
            _subject = context.Subjects;
        }

        #endregion

        #region Functions Handle
        public async Task<List<Subjects>> GetSubjectsAsync(bool includeEntities = false)
        {
            List<Subjects> subjects;
            if (includeEntities)
                subjects = await _subject
                    .Include(s => s.Instructors)
                    .Include(s => s.StudentsSubjects).ThenInclude(s => s.Student)
                    .Include(s => s.DepartmetSubjects).ThenInclude(s => s.Department)
                .ToListAsync();
            else
                subjects = await _subject.ToListAsync();

            return subjects;
        }
        #endregion

    }
}
