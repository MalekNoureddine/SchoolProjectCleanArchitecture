using CleanArchProject.Data.Entities.Identies;
using CleanArchProject.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Service.ServicesImplementation
{
    public class AuthorizationService : IAuthorizationService
    {
        #region Fields
        private readonly RoleManager<Role> _roleManager;
        #endregion
        #region Constructor
        public AuthorizationService(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }
        #endregion
        #region Actions
        public async Task<string> AddRole(string roleName)
        {
            var role = new Role
            {
                Name = roleName,
            };
            var response = await _roleManager.CreateAsync(role);
            return response.Succeeded ? "Success" : "Failed" ;
        }

        public Task<bool> IsRoleExists(string roleName)
        {
            return _roleManager.RoleExistsAsync(roleName);
        }
        #endregion
    }
}
