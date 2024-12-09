using CleanArchProject.Data.Entities;
using CleanArchProject.Data.Enums;
using CleanArchProject.Infrastracture.Interfaces;
using CleanArchProject.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Service.ServicesImplementation
{
    public class SubjectService : ISubjectService
    {
        #region Fields
        private readonly ISubjectRepository _subjectService;
        #endregion

        #region Constructors
        public SubjectService(ISubjectRepository subjectService)
        {
            _subjectService = subjectService;
        }
        #endregion

        #region FunctionHandle

        public async Task<string> AddSubjectAsync(Subjects subject)
        {
            var transaction = _subjectService.BeginTransaction();
            try
            {
                await _subjectService.AddAsync(subject);
                transaction.Commit();
                return "Created";
            }
            catch (DbUpdateException ex)
            {
                transaction.Rollback();
                return "Conflict";
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<string> DeleteSubjectAsync(Subjects subject)
        {
            var transaction = _subjectService.BeginTransaction();
            try
            {
                await _subjectService.DeleteAsync(subject);
                transaction.Commit();
                return "Deleted";
            }
            catch (Exception)
            {

                transaction.Rollback();
                return "Faild";
            }
        }

        public async Task<string> EditSubjectAsync(Subjects subject)
        {
            var transaction = _subjectService.BeginTransaction();
            try
            {
                await _subjectService.UpdateAsync(subject);
                transaction.Commit();
                return "Success";
            }
            catch (DbUpdateException)
            {
                transaction.Rollback();
                return "Conflict";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.ToString());
                transaction.Rollback();
                throw;
            }
        }

        public async Task<List<Subjects>> GetAllSubjectsAsync(bool includeEntities = false)
        {
            var subjects = await _subjectService.GetSubjectsAsync(includeEntities);
            return subjects;
        }

        public IQueryable<Subjects> GetAllSubjectsQuerable()
        {
            return _subjectService.GetTableNoTracking()
                .Include(i => i.Instructors)
                .AsQueryable();
        }

        public IQueryable<Subjects> GetFilteredSubjectsQuerable(enSubjectsOrderingEnum orderBy, string search)
        {
            var result = GetAllSubjectsQuerable();
            if (!search.IsNullOrEmpty())
            {
                result = result.Where(s => s.SubjectName.Contains(search) || s.SubjectNameAr.Contains(search));
            }
            switch (orderBy)
            {
                case enSubjectsOrderingEnum.SubID:
                    result = result.OrderBy(s => s.SubID);
                    break;
                case enSubjectsOrderingEnum.SubjectName:
                    result = result.OrderBy(s => s.SubjectName);
                    break;
                case enSubjectsOrderingEnum.SubjectNameAr:
                    result = result.OrderBy(s => s.SubjectNameAr);
                    break;
                case enSubjectsOrderingEnum.Period:
                    result = result.OrderBy(s => s.Period);
                    break;
                default:
                    result = result.OrderBy(s => s.SubID);
                    break;
            }
            return result;
        }

        public async Task<Subjects> GetSubjectByIdAsync(int id, bool includeEntities = false)
        {
            Subjects? subject;
            if (includeEntities)
            {
                subject = await _subjectService.GetTableNoTracking()
                    .Include(i => i.Instructors)
                    .FirstOrDefaultAsync(s => s.SubID == id);

            }
            subject = await _subjectService.GetByIdAsync(id);
            return subject;
        }

        public async Task<bool> IsSubjectArabicNameExists(string subjectArabicName)
        {
            var checkDepartmentName = await _subjectService.GetTableNoTracking().Where(s => s.SubjectNameAr.Equals(subjectArabicName)).FirstOrDefaultAsync();
            if (checkDepartmentName != null) return true;
            return false;
        }
        public async Task<bool> IsSubjectNameExists(string subjectName)
        {
            var checkDepartmentName = await _subjectService.GetTableNoTracking().Where(s => s.SubjectName.Equals(subjectName)).FirstOrDefaultAsync();
            if (checkDepartmentName != null) return true;
            return false;
        }

        public async Task<bool> IsSubjectNameExistsById(string subjectName, int Id)
        {
            var checkDepartmentName = await _subjectService.GetTableNoTracking().Where(s => (s.SubjectName.Equals(subjectName)) && s.SubID != Id).FirstOrDefaultAsync();
            if (checkDepartmentName != null) return true;
            return false;
        }

        public async Task<bool> IsSubjectArabicNameExistsById(string subjectArabicName, int Id)
        {
            var checkDepartmentName = await _subjectService.GetTableNoTracking().Where(s => (s.SubjectNameAr.Equals(subjectArabicName)) && s.SubID != Id).FirstOrDefaultAsync();
            if (checkDepartmentName != null) return true;
            return false;
        }
        #endregion

    }
}
