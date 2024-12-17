using CleanArchProject.Data.Entities.Identities;
using CleanArchProject.Data.Healper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Service.Interfaces
{
    public interface IAuthenticationService
    {
        public Task<JwtAuthResult> GetJWTToken(User user);
        public Task<JwtAuthResult> GetRefreshToken(string accessToken, string refreshToken);
        public Task<string> ValidateToken(string accessToken);
    }
}
