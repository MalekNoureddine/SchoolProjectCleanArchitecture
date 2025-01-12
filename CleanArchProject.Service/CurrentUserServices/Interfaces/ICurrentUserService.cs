using CleanArchProject.Data.Entities.Identities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Service.CurrentUserServices.Interfaces
{
    public interface ICurrentUserService
    {
        public Task<User> GetCurrentUserAsync();
        public Task<List<string>> GetCurrentUserRolesAsync();
    }
}
