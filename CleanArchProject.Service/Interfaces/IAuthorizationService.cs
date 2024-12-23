﻿using CleanArchProject.Data.Entities.Identies;
using CleanArchProject.Data.Requests;
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
        public Task<Role> GetRolesById(int Id);
        public Task<Role> GetRolesByName(string Name);
        public Task<string> AddRole(string roleName);
        public Task<string> EditRole(EditRoleRequest editRoleRequest);
        public Task<string> DeleteRole(string roleName);
        public Task<bool> IsRoleExists(string roleName);
        public Task<bool> IsRoleExists(string roleName, int Id);
    }
}
