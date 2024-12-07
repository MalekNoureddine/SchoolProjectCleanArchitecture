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
    public class Student_SubjectRepository : GenericRepositoryAsync<StudentSubject>, IStudentSubjectRepository
    {
        #region Fields
        private readonly DbSet<StudentSubject> _studentSubject;
        #endregion
        #region Constructors
        public Student_SubjectRepository(AppDbContext context) : base(context)
        {
            _studentSubject = context.StudentSubject;
        }
        #endregion

        #region Functions Handle
        public async Task<List<StudentSubject>> GetStudentSubjectAsync(bool includeEntities = false)
        {
            List<StudentSubject> studentSubject;
            if (includeEntities)
                studentSubject = await _studentSubject
                .Include(x => x.Student)
                .Include(x => x.Subject)
                .ToListAsync();
            else
                studentSubject = await _studentSubject.ToListAsync();

            return studentSubject;

        }
        #endregion

    }
}
