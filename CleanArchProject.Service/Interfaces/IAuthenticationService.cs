using CleanArchProject.Data.Entities.Identities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Service.Interfaces
{
    public interface IAuthenticationService
    {
        public Task<string> GetJWTToken(User user);
    }
}
