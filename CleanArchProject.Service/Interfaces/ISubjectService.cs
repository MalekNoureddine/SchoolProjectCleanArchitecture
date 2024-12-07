using CleanArchProject.Data.Entities;
using CleanArchProject.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Service.Interfaces
{
    public interface ISubjectService
    {
        public Task<List<Subjects>> GetAllSubjectsAsync(bool includeEntities = false);
        public Task<Subjects> GetSubjectByIdAsync(int id, bool includeEntities = false);
        public Task<string> AddSubjectAsync(Subjects subject);
        public Task<string> EditSubjectAsync(Subjects subject);
        public Task<bool> IsSubjectNameExists(string subjectName, string subjectArabicName);
        public Task<bool> IsSubjectNameExistsById(string subjectName, string subjectArabicName, int Id);
        Task<string> DeleteSubjectAsync(Subjects subject);
        public IQueryable<Subjects> GetAllSubjectsQuerable();
        IQueryable<Subjects> GetFilteredSubjectsQuerable(enSubjectsOrderingEnum orderBy, string search);
    }
}
