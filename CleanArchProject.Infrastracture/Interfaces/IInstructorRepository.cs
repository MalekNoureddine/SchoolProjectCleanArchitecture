using CleanArchProject.Data.Entities;
using CleanArchProject.Infrastracture.InfrastractureBases__generics_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Infrastracture.Interfaces
{
    public interface IInstructorRepository : IGenericRepositoryAsync<Instructor>
    {
        public Task<List<Instructor>> GetInstructorsAsync(bool includeEntities = false);
    }

}
