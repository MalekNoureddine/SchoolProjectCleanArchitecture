using CleanArchProject.Data.Entities.Identities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Service.Interfaces
{
    public interface IUserService
    {
        public Task<string> AddUserAsync(User user, string password);
    }
}
