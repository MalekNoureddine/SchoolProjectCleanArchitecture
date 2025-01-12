using CleanArchProject.Data.Entities;
using CleanArchProject.Data.Entities.Views;
using CleanArchProject.Data.Enums;
using CleanArchProject.Infrastracture.Interfaces;
using CleanArchProject.Infrastracture.Interfaces.Views;
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
    public class InstructorService : IInstructorService
    {
        #region Fields
        private readonly IInstructorRepository _instructorService;
        private readonly IViewRepository<InstructorsView> _instructorsViewService;
        #endregion

        #region Constructors
        public InstructorService(IInstructorRepository instructor, IViewRepository<InstructorsView> instructorsViewService)
        {
            _instructorService = instructor;
            _instructorsViewService = instructorsViewService;
        }
        #endregion

        #region FunctionHandle
        public async Task<string> AddInstructorAsync(Instructor instructor)
        {
            var transaction = _instructorService.BeginTransaction();
            try
            {
                await _instructorService.AddAsync(instructor);
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

        public async Task<string> DeleteInstructorAsync(Instructor instructor)
        {
            var transaction = _instructorService.BeginTransaction();
            try
            {
                await _instructorService.DeleteAsync(instructor);
                transaction.Commit();
                return "Deleted";
            }
            catch (Exception)
            {

                transaction.Rollback();
                return "Faild";
            }
        }

        public async Task<string> EditInstructorAsync(Instructor instructor)
        {
            var transaction = _instructorService.BeginTransaction();
            try
            {
                await _instructorService.UpdateAsync(instructor);
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

        public async Task<List<Instructor>> GetAllInstructorsAsync(bool includeEntities = false)
        {
            var departments = await _instructorService.GetInstructorsAsync(includeEntities);
            return departments;
        }

        public IQueryable<Instructor> GetAllInstructorsQuerable()
        {
            return _instructorService.GetTableNoTracking()
                .Include(i => i.SupervisedInstructors)
                .Include(i => i.Departments)
                .Include(i => i.Subjects)
                .Include(i => i.Supervisor)
                .Include(i => i.ManagedDepartment)
                .AsQueryable();
        }

        public IQueryable<Instructor> GetFilteredInstructorsQuerable(enInstructorsOrderingEnum orderBy, string search)
        {
            var result = GetAllInstructorsQuerable();
            if (!string.IsNullOrEmpty(search))
            {
                result = result.Where(s => s.EName.Contains(search) || s.ENameAr.Contains(search) ||
                 s.Phone.Contains(search) || s.Email.Contains(search) ||
                s.Address.Contains(search));
            }
            switch (orderBy)
            {
                case enInstructorsOrderingEnum.InsId:
                    result = result.OrderBy(s => s.InsId);
                    break;
                case enInstructorsOrderingEnum.EName:
                    result = result.OrderBy(s => s.EName);
                    break;
                case enInstructorsOrderingEnum.ENameAr:
                    result = result.OrderBy(s => s.ENameAr);
                    break;
                case enInstructorsOrderingEnum.Address:
                    result = result.OrderBy(s => s.Address);
                    break;
                case enInstructorsOrderingEnum.NumberOfDepartments:
                    result = result.OrderBy(s => s.Departments.Count);
                    break;
                case enInstructorsOrderingEnum.NumberOfSubjects:
                    result = result.OrderBy(s => s.Subjects.Count);
                    break;
                case enInstructorsOrderingEnum.Salary:
                    result = result.OrderBy(s => s.Salary);
                    break;
                default:
                    result = result.OrderBy(s => s.InsId);
                    break;
            }
            return result;
        }

        public async Task<Instructor> GetInstructorByIdAsync(int id, bool includeEntities = false)
        {
            Instructor? instructor;
            if (includeEntities)
                instructor = await _instructorService.GetTableNoTracking()
                .Include(i => i.SupervisedInstructors)
                .Include(i => i.Departments)
                .Include(i => i.Subjects)
                .Include(i => i.Supervisor)
                .Include(i => i.ManagedDepartment)
                .FirstOrDefaultAsync(s => s.InsId == id);

            else
                instructor = await _instructorService.GetTableNoTracking()
                .FirstOrDefaultAsync(s => s.InsId == id);

            return instructor;
        }

        public IQueryable<Instructor> GetInstructorsListBySupervisorIdQuerable(int id)
        {
            return _instructorService.GetTableNoTracking().Where(i => i.SupervisorId == id)
                .AsQueryable();
        }

        public async Task<bool> IsInstructorPhoneExists(string Phone)
        {
            var checkInstructorPhone_and_Email = await _instructorService.GetTableNoTracking()
                .Where(s => s.Phone.Equals(Phone)).FirstOrDefaultAsync();
            if (checkInstructorPhone_and_Email != null) return true;
            return false;
        }
        public async Task<bool> IsInstructorEmailExists(string Email)
        {
            var checkInstructorPhone_and_Email = await _instructorService.GetTableNoTracking()
                .Where(s => s.Email.Equals(Email)).FirstOrDefaultAsync();
            if (checkInstructorPhone_and_Email != null) return true;
            return false;
        }

        public async Task<bool> IsInstructorPhoneExistsById(string Phone, int Id)
        {
            var checkInstructorPhone_and_Email = await _instructorService.GetTableNoTracking()
                .Where(s => s.Phone.Equals(Phone) && s.InsId != Id).FirstOrDefaultAsync();
            if (checkInstructorPhone_and_Email != null) return true;
            return false;
        }
        public async Task<bool> IsInstructorEmailExistsById(string Email, int Id)
        {
            var checkInstructorPhone_and_Email = await _instructorService.GetTableNoTracking()
                .Where(s => s.Email.Equals(Email) && s.InsId != Id).FirstOrDefaultAsync();
            if (checkInstructorPhone_and_Email != null) return true;
            return false;
        }

        public async Task<bool> IsInstructorExists(int Id)
        {
            return await _instructorService.GetTableNoTracking().AnyAsync(i => i.InsId == Id);
        }

        public async Task<InstructorsView> GetInstructorViewByIdAsync(int id)
        {
            var instructorViewResult = await _instructorsViewService.GetByIdAsync(id);
            return instructorViewResult;
        }

        public async Task<List<InstructorsView>> GetInstructorsViewListAsync()
        {
            var instructorViewListResult = await _instructorsViewService.GetTableNoTracking().ToListAsync();
            return instructorViewListResult;
        }

        public IQueryable<InstructorsView> GetFilteredInstructorsViewQuerable(enInstructorViewOrderingEnum orderBy, string search)
        {
            var result = _instructorsViewService.GetTableNoTracking().AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                result = result.Where(s => s.Name.Contains(search) || s.Email.Contains(search) ||
                 s.SupervisorName.Contains(search));
            }
            switch (orderBy)
            {
                case enInstructorViewOrderingEnum.InsId:
                    result = result.OrderBy(s => s.InstructorId);
                    break;
                case enInstructorViewOrderingEnum.EName:
                    result = result.OrderBy(s => s.Name);
                    break;
                case enInstructorViewOrderingEnum.Email:
                    result = result.OrderBy(s => s.Email);
                    break;
                default:
                    result = result.OrderBy(s => s.InstructorId);
                    break;
            }
            return result;
        }
        #endregion

    }
}
