using CleanArchProject.Data.Entities.Identies;
using CleanArchProject.Data.Requests;
using CleanArchProject.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

        public async Task<bool> IsRoleExists(string roleName, int Id)
        {
            var role = await _roleManager.Roles.AnyAsync(x => x.Id != Id && x.Name == roleName);
            return role;
        }
        public async Task<string> EditRole(EditRoleRequest editRoleRequest)
        {
            var role = await _roleManager.FindByIdAsync(editRoleRequest.Id.ToString());
            if (role is null) return "NotFound";
            role.Name = editRoleRequest.Name;
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded) return "Succeeded";
            var errors = string.Join(",", result.Errors);
            if (errors == "Microsoft.AspNetCore.Identity.IdentityError")
                return "IdentityError";
            return errors;
        }
        #endregion
    }
}
