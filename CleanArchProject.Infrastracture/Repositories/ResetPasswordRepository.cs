using CleanArchProject.Data.Entities;
using CleanArchProject.Infrastracture.Data;
using CleanArchProject.Infrastracture.InfrastractureBases__generics_;
using CleanArchProject.Infrastracture.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Infrastracture.Repositories
{
    public class ResetPasswordRepository : GenericRepositoryAsync<ResetPassword>, IResetPasswordRepository
    {
        public ResetPasswordRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
