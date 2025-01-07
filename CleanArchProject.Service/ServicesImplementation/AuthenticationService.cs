using Azure.Core;
using CleanArchProject.Data.Entities;
using CleanArchProject.Data.Entities.Identies;
using CleanArchProject.Data.Entities.Identities;
using CleanArchProject.Data.Healper;
using CleanArchProject.Data.Results;
using CleanArchProject.Infrastracture.Data;
using CleanArchProject.Infrastracture.Interfaces;
using CleanArchProject.Service.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace CleanArchProject.Service.ServicesImplementation
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Fields
        private readonly JwtSettings _jwtSettings;
        private readonly UserManager<User> _userManager;
        private readonly IUserRefreshTokenRepository _userRefreshTokenRepository;
        private readonly IMemoryCache _tokenCache;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailService _emailsService;
        private readonly IUrlHelper _urlHelper;
        private readonly IResetPasswordRepository _resetPasswordRepo;

        #endregion

        #region Constructors
        public AuthenticationService(JwtSettings jwtSettings, IUserRefreshTokenRepository userRefreshTokenRepository, UserManager<User> userManager, IMemoryCache tokenCache, IHttpContextAccessor httpContextAccessor, IEmailService emailsService, IUrlHelper urlHelper, IResetPasswordRepository resetPasswordRepo)
        {
            _jwtSettings = jwtSettings;
            _userRefreshTokenRepository = userRefreshTokenRepository;
            _userManager = userManager;
            _tokenCache = tokenCache;
            _httpContextAccessor = httpContextAccessor;
            _emailsService = emailsService;
            _urlHelper = urlHelper;
            _resetPasswordRepo = resetPasswordRepo;
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
            var claims = await GetClaims(user);
            var jwtToken = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                expires: DateTime.Now.AddDays(_jwtSettings.AccessTokenExpireDate),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256Signature));
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return (jwtToken, accessToken);
        }

        public async Task<List<Claim>> GetClaims(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(nameof(UserClaimModel.PhoneNumber), user.PhoneNumber),
                new Claim(nameof(UserClaimModel.Id), user.Id.ToString()),
            };
            
            foreach(var role in roles){
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);
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

        public async Task<JwtAuthResult> GetRefreshToken(User user,JwtSecurityToken jwtToken,DateTime? ExpiryDate, string refreshToken)
        {
            var (response, newToken) = await GenerateJWTToken(user);
            //Generater refresh token
            var refreshTokenResult = new RefreshToken
            {
                Username = jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value,
                ExpireDate = (DateTime)ExpiryDate,
                TokenString = refreshToken
            };

            var result = new JwtAuthResult
            {
                AccessToken = newToken,
                RefreshToken = refreshTokenResult
            };
            return result;

        }
        public JwtSecurityToken ReadJwtToken(string accessToken)
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
                    return "InvalidToken";
                }
                return "IsActive";
            }
            catch (Exception ex)
            {

                return $"Error: {ex.Message}";
            }
        }

        public async Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string accessToken, string refreshToken)
        {
            if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
            {
                return ("AlgorithmIsWrong", null);
            }
            if (jwtToken.ValidTo > DateTime.UtcNow)
            {
                return ("TokenIsNotExpired", null);
            }

            //Get User

            var userId = jwtToken.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimModel.Id)).Value;
            var userRefreshToken = await _userRefreshTokenRepository.GetTableNoTracking()
                                             .FirstOrDefaultAsync(x => x.Token == accessToken &&
                                                                     x.RefreshToken == refreshToken &&
                                                                     x.UserId == int.Parse(userId));
            if (userRefreshToken == null)
            {
                return ("RefreshTokenIsNotFound", null);
            }

            if (userRefreshToken.ExpiryDate < DateTime.UtcNow)
            {
                userRefreshToken.IsRevoked = true;
                userRefreshToken.IsUsed = false;
                await _userRefreshTokenRepository.UpdateAsync(userRefreshToken);
                return ("RefreshTokenIsExpired", null);
            }
            var expirydate = userRefreshToken.ExpiryDate;
            return (userId, expirydate);
        }

        public async Task<string> ConfirmEmail(int userId, string code)
        {
            if (code == null)
                return "FailedToConfirmEmail";

            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
                return "NotFound";

            var confirmEmail = await _userManager.ConfirmEmailAsync(user, code);
            if (!confirmEmail.Succeeded)
                return "FailedToConfirmEmail";
            return "Succeeded";
        }
        public async Task<string> ForgotPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return "NotFound";

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var hashedToken = HashToken(token);

            var resetPasswordEntry = new ResetPassword
            {
                UserId = user.Id,
                Token = hashedToken,
                ExpirationDate = DateTime.UtcNow.AddHours(1)
            };
            await _resetPasswordRepo.AddAsync(resetPasswordEntry);

            var resetLink = BuildResetLink(token);
            var message = $"To reset your password, click the link: <a href='{resetLink}'>Reset Password</a>";
            await _emailsService.SendEmail(user.Email, message, "Password Reset");

            return "PasswordResetLinkSent";
        }

        public async Task<string> ResetPassword( string token, string newPassword)
        {
            var hashedToken = HashToken(token);
            var resetPasswordEntry = await _resetPasswordRepo.GetTableNoTracking()
                .FirstOrDefaultAsync(rp => rp.Token == hashedToken);

            if (resetPasswordEntry == null || resetPasswordEntry.ExpirationDate < DateTime.UtcNow)
            {
                if (resetPasswordEntry != null)
                    await _resetPasswordRepo.DeleteAsync(resetPasswordEntry);

                return "InvalidOrExpiredToken";
            }

            var user = await _userManager.FindByIdAsync(resetPasswordEntry.UserId.ToString());
            if (user == null) return "NotFound";

            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
            if (!result.Succeeded)
                return $"Error: {string.Join(", ", result.Errors.Select(e => e.Description))}";

            await _resetPasswordRepo.DeleteAsync(resetPasswordEntry);
            return "PasswordResetSuccess";
        }
        #endregion

        #region HealperFunctions

        private string BuildResetLink(string token)
        {
            var encodedToken = WebUtility.UrlEncode(token);
            var request = _httpContextAccessor.HttpContext.Request;
            return $"{request.Scheme}://{request.Host}/Authentication/ResetPassword?token={encodedToken}";
        }
        private string HashToken(string token)
        {
            var encryptionKey = Environment.GetEnvironmentVariable("ENCRYPTION_KEY") ?? throw new ArgumentNullException();
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(encryptionKey)))
            {
                var bytes = Encoding.UTF8.GetBytes(token);
                var hash = hmac.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        //private bool VerifyHashedToken(string hashedToken, string plainToken)
        //{
        //    var plainTokenHash = HashToken(plainToken);
        //    return CryptographicOperations.FixedTimeEquals(
        //        Encoding.UTF8.GetBytes(hashedToken),
        //        Encoding.UTF8.GetBytes(plainTokenHash));
        //}
        #endregion
    }
}
