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

    public class StudentRepository : GenericRepositoryAsync<Student> ,IStudentRepository
    {
        #region Fields
        private readonly DbSet<Student> _student;
        #endregion
        #region Constructors
        public StudentRepository(AppDbContext context) : base(context)
        {
            _student = context.Student;
        }
        #endregion

        #region Handles functions
        public async Task<List<Student>> GetStudentsAsync()
        {
            return await _student.Include(s => s.Department).ToListAsync();
        }
        #endregion
    }
}
