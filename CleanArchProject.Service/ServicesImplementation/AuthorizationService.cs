using CleanArchProject.Data.Entities.Identies;
using CleanArchProject.Data.Entities.Identities;
using CleanArchProject.Data.Healper;
using CleanArchProject.Data.Requests;
using CleanArchProject.Data.Results;
using CleanArchProject.Infrastracture.Data;
using CleanArchProject.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Service.ServicesImplementation
{
    public class AuthorizationService : IAuthorizationService
    {
        #region Fields
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly AppDbContext _dbContext;
        #endregion
        #region Constructor
        public AuthorizationService(RoleManager<Role> roleManager, UserManager<User> userManager, AppDbContext dbContext)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _dbContext = dbContext;
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

        public async Task<string> ManageUserRole(UpdateUserRoleRequest request)
        {
            var transact = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var user =  await _userManager.FindByIdAsync(request.UserId.ToString());
                if (user is null) return "NotFound";

                var userRoles = await _userManager.GetRolesAsync(user);

                var RemoveOldRolesresult = await _userManager.RemoveFromRolesAsync(user, userRoles);
                if (!RemoveOldRolesresult.Succeeded)
                {
                    transact.Rollback();
                    return "FailedToRemoveOldRoles";
                }
                var selectRoles = request.Roles.Where(x => x.HasRole).Select(x => x.Name);

                var AddNewRolesResult = await _userManager.AddToRolesAsync(user, selectRoles);
                if (!AddNewRolesResult.Succeeded)
                {
                    transact.Rollback();
                    return "FailedToAddNewRoles";
                }
                transact.Commit();
                return "Success";
            }
            catch (Exception)
            {
                transact.Rollback();
                return "FailedToUpdateUserRoles";
            }
        }

        public async Task<ManageUserClaimsResult> ManageUserClaimData(User user)
        {
            var response = new ManageUserClaimsResult();
            var userClaimList = new List<UserClaims>();

            response.UserId = user.Id;
            var userClaims = await _userManager.GetClaimsAsync(user);
            foreach (var claim in ClaimStore.Claims)
            {
                var userClaim = new UserClaims();
                userClaim.Type = claim.Type;
                userClaim.Value = userClaims.Any(x => x.Type == claim.Type);
                userClaimList.Add(userClaim);
            }
            response.userClaims = userClaimList;
            return response;
        }

        public async Task<string> ManageUserClaims(UpdateUserClaimsResquest request)
        {
            var transact = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var user = await _userManager.FindByIdAsync(request.UserId.ToString());
                if (user is null) return "NotFound";

                var userClaims = await _userManager.GetClaimsAsync(user);

                var RemoveOldClaimsResult = await _userManager.RemoveClaimsAsync(user, userClaims);
                if (!RemoveOldClaimsResult.Succeeded)
                {
                    transact.Rollback();
                    return "FailedToRemoveOldClaims";
                }
                var selectClaims = request.userClaims.Where(x => x.Value).Select(x => new Claim(x.Type,x.Value.ToString()));

                var AddNewClaimsResult = await _userManager.AddClaimsAsync(user, selectClaims);
                if (!AddNewClaimsResult.Succeeded)
                {
                    transact.Rollback();
                    return "FailedToAddNewClaims";
                }
                await transact.CommitAsync();
                return "Success";
            }
            catch (Exception)
            {
                transact.Rollback();
                return "FailedToUpdateUserClaims";
            }
        }
        #endregion
    }
}
