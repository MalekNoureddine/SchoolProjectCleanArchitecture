using CleanArchProject.Core.Featurs.Users.Queries.Responses;
using CleanArchProject.Data.Entities.Identities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Mapping.Users
{
    public partial class UserProfile
    {
        private void GetUserListMapping()
        {
            CreateMap<User, GetUsersListResponse>();
        }
    }
}
