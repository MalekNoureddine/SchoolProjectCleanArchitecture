using CleanArchProject.Data.Entities;
using CleanArchProject.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Service.Interfaces
{
    public interface IInstructorService
    {
        public Task<List<Instructor>> GetAllInstructorsAsync(bool includeEntities = false);
        public Task<Instructor> GetInstructorByIdAsync(int id, bool includeEntities = false);
        public Task<string> AddInstructorAsync(Instructor instructor);
        public Task<string> EditInstructorAsync(Instructor instructor);
        public Task<bool> IsInstructorPhoneExists(string Phone);
        public Task<bool> IsInstructorEmailExists(string email);
        public Task<bool> IsInstructorPhoneExistsById(string Phone, int Id);
        public Task<bool> IsInstructorEmailExistsById(string Email, int Id);
        Task<string> DeleteInstructorAsync(Instructor instructor);
        public IQueryable<Instructor> GetAllInstructorsQuerable();
        IQueryable<Instructor> GetFilteredInstructorsQuerable(enInstructorsOrderingEnum orderBy, string search);
        IQueryable<Instructor> GetInstructorsListBySupervisorIdQuerable(int id);
        Task<bool> IsInstructorExists(int Id);
    }
}
