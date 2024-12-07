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
    public class Department_SubjectRepository : GenericRepositoryAsync<DepartmetSubject>, IDepartmentSubjectRepository
    {
        #region Fields
        private readonly DbSet<DepartmetSubject> _departmetSubject;
        #endregion

        #region Constructors
        public Department_SubjectRepository(AppDbContext context) : base(context)
        {
            _departmetSubject = context.DepartmetSubject;
        }
        #endregion

        #region Functions Handle
        public async Task<List<DepartmetSubject>> GetDepartmetSubjectAsync(bool includeEntities = false)
        {
            List<DepartmetSubject> departmetSubject;
            if (includeEntities)
                departmetSubject = await _departmetSubject
                .Include(x => x.Department)
                .Include(x => x.Subject)
                .ToListAsync();
            else
                departmetSubject = await _departmetSubject.ToListAsync();

            return departmetSubject;
        }
        #endregion

    }
}
