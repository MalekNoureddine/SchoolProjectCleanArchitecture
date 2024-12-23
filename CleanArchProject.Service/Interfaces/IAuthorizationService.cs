using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Service.Interfaces
{
    public interface IAuthorizationService
    {
        public Task<string> AddRole(string roleName);
        public Task<bool> IsRoleExists(string roleName);
    }
}
