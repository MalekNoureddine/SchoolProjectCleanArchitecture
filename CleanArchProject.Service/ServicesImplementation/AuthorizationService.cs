using CleanArchProject.Data.Entities.Identies;
using CleanArchProject.Data.Entities.Identities;
using CleanArchProject.Data.Requests;
using CleanArchProject.Data.Results;
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
        private readonly UserManager<User> _userManager;
        #endregion
        #region Constructor
        public AuthorizationService(RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
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

        public async Task<string> DeleteRole(string roleName)
        {
            var roleToDelete = await _roleManager.FindByNameAsync(roleName);
            if (roleToDelete is null) return "NotFound";
            var users = await _userManager.GetUsersInRoleAsync(roleName);
            if (users.Any()) return "Used";
            var result = await _roleManager.DeleteAsync(roleToDelete);
            if (result.Succeeded) return "Succeeded";
            var errors = string.Join(",", result.Errors);
            return errors;
        }

        public async Task<List<Role>> GetRolesList()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return roles;
        }

        public async Task<Role> GetRolesById(int Id)
        {
            var role = await _roleManager.FindByIdAsync(Id.ToString());
            return role;
        }

        public async Task<Role> GetRolesByName(string Name)
        {
            var role = await _roleManager.FindByNameAsync(Name);
            return role;
        }

        public async Task<ManageUserRolesResult> GetRolesOfUsuer(User user)
        {
            var response = new ManageUserRolesResult();
            var Roles = new List<Roles>();

            var userRoles = await _userManager.GetRolesAsync(user);
            var roles = await _roleManager.Roles.ToListAsync();

            foreach (var role in roles)
            {
                var UserRole = new Roles { Id = role.Id, Name = role.Name };
                UserRole.HasRole = userRoles.Contains(role.Name);
                Roles.Add(UserRole);
            }
            response.UserId = user.Id;
            response.Roles = Roles;
            return response;

        }
        #endregion
    }
}
