using Azure;
using CleanArchProject.Data.Entities.Identies;
using CleanArchProject.Data.Entities.Identities;
using CleanArchProject.Data.Requests;
using CleanArchProject.Data.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Service.Interfaces
{
    public interface IAuthorizationService
    {
        public Task<List<Role>> GetRolesList();
        public Task<ManageUserRolesResult> GetRolesOfUsuer(User user);
        public Task<Role> GetRolesById(int Id);
        public Task<Role> GetRolesByName(string Name);
        public Task<string> AddRole(string roleName);
        public Task<string> DeleteRole(string roleName);
        public Task<string> EditRole(EditRoleRequest editRoleRequest);
        public Task<string> ManageUserRole(UpdateUserRoleRequest request);
        public Task<string> ManageUserClaims(UpdateUserClaimsResquest request);
        public Task<bool> IsRoleExists(string roleName);
        public Task<bool> IsRoleExists(string roleName, int Id);
        Task<ManageUserClaimsResult> ManageUserClaimData(User user);
    }
}
