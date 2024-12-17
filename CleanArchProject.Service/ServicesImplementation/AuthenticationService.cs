using Azure.Core;
using CleanArchProject.Data.Entities.Identies;
using CleanArchProject.Data.Entities.Identities;
using CleanArchProject.Data.Healper;
using CleanArchProject.Infrastracture.Interfaces;
using CleanArchProject.Service.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        private readonly UserManager<User> _userManager;
        private readonly IUserRefreshTokenRepository _userRefreshTokenRepository;

        #endregion

        #region Constructors
        public AuthenticationService(JwtSettings jwtSettings, IUserRefreshTokenRepository userRefreshTokenRepository, UserManager<User> userManager)
        {
            _jwtSettings = jwtSettings;
            _userRefreshTokenRepository = userRefreshTokenRepository;
            _userManager = userManager;
        }

        #endregion

        #region Actions
        public async Task<JwtAuthResult> GetJWTToken(User user)
        {

            var (jwtSecurityToken,accessToken) = await GenerateJWTToken(user);

            var refreshToken = GetRefreshToken(user.UserName);

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

        private async Task<(JwtSecurityToken, string)> GenerateJWTToken(User user)
        {
            var claims = GetClaims(user);
            var jwtToken = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                expires: DateTime.Now.AddDays(_jwtSettings.AccessTokenExpireDate),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256Signature));
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return (jwtToken, accessToken);
        }

        public List<Claim> GetClaims(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(nameof(UserClaimModel.UserName), user.UserName),
                new Claim(nameof(UserClaimModel.Email), user.Email),
                new Claim(nameof(UserClaimModel.PhoneNumber), user.PhoneNumber),
                new Claim(nameof(UserClaimModel.Id), user.Id.ToString())
            };
            return claims;
        }

        private RefreshToken GetRefreshToken(string username)
        {

            RefreshToken refreshToken = new RefreshToken
            {
                ExpireDate = DateTime.UtcNow.AddDays(_jwtSettings.AccessTokenExpireDate),
                Username = username,
                TokenString = GenerateRefreshToken()
            };
            return refreshToken;
        }
        private string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
        }

        public async Task<JwtAuthResult> GetRefreshToken(string accessToken, string refreshToken)
        {
            //Read token to get claims
            JwtSecurityToken jwtToken = ReadJwtToken(accessToken);
            if(jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
            {
                throw new SecurityTokenException("Algorithm is wrong");
            }
            if (jwtToken.ValidTo > DateTime.UtcNow)
            {
                throw new SecurityTokenException("TokenIsNotExpired");
            }
            //Get user
            //var userId = "1002";
            var userId = jwtToken.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimModel.Id)).Value;

            var userRefreshToken = await _userRefreshTokenRepository.GetTableNoTracking().FirstOrDefaultAsync(x => x.Token == accessToken
                                                                                         && x.RefreshToken == refreshToken
                                                                                         && x.UserId.ToString() == userId);
            if(userRefreshToken == null)
            {
                throw new SecurityTokenException("Refresh token was not found");

            }
            //Validate Token, RefreshToken
            if (userRefreshToken.ExpiryDate <  DateTime.UtcNow)
            {
                userRefreshToken.IsRevoked = true;
                userRefreshToken.IsUsed = false;
                await _userRefreshTokenRepository.UpdateAsync(userRefreshToken);
                throw new SecurityTokenException("Refresh token is expired");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if(user == null) throw new ArgumentNullException("user was not found");
            var (response, newToken) = await GenerateJWTToken(user);
            //Generater refresh token
            var refreshTokenResult = new RefreshToken
            {
                Username = jwtToken.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimModel.UserName)).Value,
                ExpireDate = userRefreshToken.ExpiryDate,
                TokenString = refreshToken
            };

            var result = new JwtAuthResult
            {
                AccessToken = newToken,
                RefreshToken = refreshTokenResult
            };
            return result;

        }
        private JwtSecurityToken ReadJwtToken(string accessToken)
        {
            if(string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentNullException(nameof(accessToken));
            }
            var handler = new JwtSecurityTokenHandler();
            var response = handler.ReadJwtToken(accessToken);
            return response;
        }

        public async Task<string> ValidateToken(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = _jwtSettings.ValidateIssuer,
                ValidIssuers = new[] { _jwtSettings.Issuer },
                ValidateIssuerSigningKey = _jwtSettings.ValidateIssuerSigningKey,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)),
                ValidAudience = _jwtSettings.Audience,
                ValidateAudience = _jwtSettings.ValidateAudience,
                ValidateLifetime = _jwtSettings.ValidateLifeTime,
            };
            var validator = handler.ValidateToken(accessToken, parameters, out SecurityToken validatedToken);

            try
            {
                if(validator is null)
                {
                    throw new ArgumentNullException("Invalid Token");
                }
                return "IsActive";
            }
            catch (Exception ex)
            {

                return $"Error: {ex.Message}";
            }
        }
        #endregion
    }
}
