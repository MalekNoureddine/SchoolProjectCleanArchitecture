using CleanArchProject.Data.Entities.Identies;
using CleanArchProject.Infrastracture.InfrastractureBases__generics_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Infrastracture.Interfaces
{
    public interface IUserRefreshTokenRepository : IGenericRepositoryAsync<UserRefreshToken>
    {
    }
}
