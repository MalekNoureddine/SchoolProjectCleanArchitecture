using CleanArchProject.Core.Featurs.Authorization.Queries.Responses;
using CleanArchProject.Data.Entities.Identies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Mapping.Roles
{
    public partial class RoleProfile
    {
        private void GetRolesListMapping()
        {
            CreateMap<Role, GetRoleResponse>();
        }
    }
}
