using CleanArchProject.Data.Entities;
using CleanArchProject.Infrastracture.InfrastractureBases__generics_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Infrastracture.Interfaces
{
    public interface ISubjectInstructorRepository : IGenericRepositoryAsync<Subj_Instructor>
    {
        public Task<List<Subj_Instructor>> GetSubj_InstructorsAsync(bool includeEntities = false);
    }
}
