using CleanArchProject.Data.Entities;
using CleanArchProject.Data.Entities.Identies;
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
    public class UserRefreshTokenRepository : GenericRepositoryAsync<UserRefreshToken>, IUserRefreshTokenRepository
    {
        #region Fields
        private readonly DbSet<UserRefreshToken> _userRefreshToken;
        #endregion
        #region Constructor
        public UserRefreshTokenRepository(AppDbContext dbContext) : base(dbContext)
        {
            _userRefreshToken = dbContext.UserRefreshToken;
        }
        #endregion
    }
}
