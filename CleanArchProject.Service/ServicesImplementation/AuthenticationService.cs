using CleanArchProject.Data.Entities.Identities;
using CleanArchProject.Data.Healper;
using CleanArchProject.Service.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Service.ServicesImplementation
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Fields
        private readonly JwtSettings _jwtSettings;

        #endregion

        #region Constructors
        public AuthenticationService(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }

        #endregion

        #region Actions
        public Task<string> GetJWTToken(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(nameof(UserClaimModel.UserName), user.UserName),
                new Claim(nameof(UserClaimModel.Email), user.Email),
                new Claim(nameof(UserClaimModel.PhoneNumber), user.PhoneNumber)
            };

            var jwtSecurityToken = new JwtSecurityToken(_jwtSettings.Issuer, _jwtSettings.Audience
                , claims, null, DateTime.UtcNow.AddMinutes(2),
                new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret))
                , SecurityAlgorithms.HmacSha256Signature));

            var acsessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return Task.FromResult(acsessToken);
        }
        #endregion
    }
}
