using Azure.Core;
using CleanArchProject.Data.Entities.Identies;
using CleanArchProject.Data.Entities.Identities;
using CleanArchProject.Data.Healper;
using CleanArchProject.Infrastracture.Interfaces;
using CleanArchProject.Service.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Concurrent;
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
        private readonly IUserRefreshTokenRepository _userRefreshTokenRepository;
        private readonly ConcurrentDictionary<string, RefreshToken> _userRefreshToken;

        #endregion

        #region Constructors
        public AuthenticationService(JwtSettings jwtSettings, IUserRefreshTokenRepository userRefreshTokenRepository)
        {
            _jwtSettings = jwtSettings;
            _userRefreshToken = new ConcurrentDictionary<string, RefreshToken>();
            _userRefreshTokenRepository = userRefreshTokenRepository;
        }

        #endregion

        #region Actions
        public async Task<JwtAuthResult> GetJWTToken(User user)
        {
            var claims = GetClaims(user);

            var jwtSecurityToken = new JwtSecurityToken(_jwtSettings.Issuer, _jwtSettings.Audience
                , claims, null, DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpireDate),
                new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret))
                , SecurityAlgorithms.HmacSha256Signature));


            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            RefreshToken refreshToken = GetRefreshToken(user.UserName);

            var userRefreshToken = new UserRefreshToken
            {
                AddedTime = DateTime.Now,
                ExpiryDate = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
                IsUsed = true,
                IsRevoked = false,
                JwtId = jwtSecurityToken.Id,
                RefreshToken = refreshToken.TokenString,
                Token = accessToken,
                UserId = user.Id
            };

            await _userRefreshTokenRepository.AddAsync(userRefreshToken);

            var response = new JwtAuthResult
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

            return response;

        }

        public static List<Claim> GetClaims(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(nameof(UserClaimModel.UserName), user.UserName),
                new Claim(nameof(UserClaimModel.Email), user.Email),
                new Claim(nameof(UserClaimModel.PhoneNumber), user.PhoneNumber)
            };
            return claims;
        }

        private RefreshToken GetRefreshToken(string username)
        {

            RefreshToken refreshToken = new RefreshToken
            {
                Id = Guid.NewGuid(),
                ExpireDate = DateTime.UtcNow.AddDays(_jwtSettings.AccessTokenExpireDate),
                Username = username,
                TokenString = GenerateRefreshToken()
            };
            _userRefreshToken.AddOrUpdate(refreshToken.TokenString, refreshToken, (s, t) => refreshToken);
            return refreshToken;
        }
        private string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
        }
        #endregion
    }
}
