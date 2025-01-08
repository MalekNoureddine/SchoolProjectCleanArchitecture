using Azure.Core;
using CleanArchProject.Core.Bases;
using CleanArchProject.Core.Featurs.Authentication.Commands.Models;
using CleanArchProject.Core.SharedResources;
using CleanArchProject.Data.Entities.Identities;
using CleanArchProject.Data.Results;
using CleanArchProject.Service.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Featurs.Authentication.Commands.Handler
{
    public class AuthenticationCommandHandler : ResponseHandler,
        IRequestHandler<SignInCommand, Response<JwtAuthResult>>,
        IRequestHandler<RefreshTokenCommand, Response<JwtAuthResult>>,
        IRequestHandler<ResetPasswordCommand, Response<string>>,
        IRequestHandler<ForgotPasswordCommand, Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources.SharedResources> _stringLocalizer;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IAuthenticationService _authenticationService;
        #endregion

        #region Constructor
        public AuthenticationCommandHandler(IStringLocalizer<SharedResources.SharedResources> stringLocalizer, UserManager<User> userManager, SignInManager<User> signInManager, IAuthenticationService authenticationService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _userManager = userManager;
            _signInManager = signInManager;
            _authenticationService = authenticationService;
        }
        #endregion

        #region Actions
        public async Task<Response<JwtAuthResult>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            //Check if user is exist or not
            var user = await _userManager.FindByNameAsync(request.userName);
            //Return The UserName Not Found
            if (user == null) return BadRequest<JwtAuthResult>(_stringLocalizer[SharedResourcesKeys.UserNameIsNotExist]);
            //try To Sign in 
            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.password, false);
            //if Failed Return Passord is wrong
            if (!signInResult.Succeeded) return BadRequest<JwtAuthResult>(_stringLocalizer[SharedResourcesKeys.PasswordNotCorrect]);
            //confirm email
            if (!user.EmailConfirmed)
                return BadRequest<JwtAuthResult>(_stringLocalizer[SharedResourcesKeys.EmailNotConfirmed]);
            //Generate Token
            var result = await _authenticationService.GetJWTToken(user);
            //return Token 
            return Success(result);
        }

        public async Task<Response<JwtAuthResult>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            JwtSecurityToken jwtToken = _authenticationService.ReadJwtToken(request.AccessToken);
            var userIdAndExpireDate = await _authenticationService.ValidateDetails(jwtToken, request.AccessToken, request.RefreshToken);
            switch (userIdAndExpireDate)
            {
                case ("AlgorithmIsWrong", null): return Unauthorized<JwtAuthResult>(_stringLocalizer[SharedResourcesKeys.AlgorithmIsWrong]);
                case ("TokenIsNotExpired", null): return Unauthorized<JwtAuthResult>(_stringLocalizer[SharedResourcesKeys.TokenIsNotExpired]);
                case ("RefreshTokenIsNotFound", null): return Unauthorized<JwtAuthResult>(_stringLocalizer[SharedResourcesKeys.RefreshTokenIsNotFound]);
                case ("RefreshTokenIsExpired", null): return Unauthorized<JwtAuthResult>(_stringLocalizer[SharedResourcesKeys.RefreshTokenIsExpired]);
            }
            var (userId, expiryDate) = userIdAndExpireDate;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound<JwtAuthResult>();

            var response = await _authenticationService.GetRefreshToken(user,jwtToken,expiryDate, request.RefreshToken);
            return Success(response);
        }

        public async Task<Response<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.ResetPassword(request.Token, request.NewPassword);

            switch (result)
            {
                case "NotFound":
                    return NotFound<string>("User not found.");
                case "TokenExpired":
                    return BadRequest<string>("The password reset token has expired.");
                case "PasswordResetSuccess":
                    return Success("Password has been successfully reset.");
                case "Error":
                    return InternalServerError<string>();
                default:
                    return InternalServerError<string>();
            }
        }

        public async Task<Response<string>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.ForgotPassword(request.Email);
            switch (result)
            {
                case "NotFound": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.UserNotFound]);
                case "PasswordResetLinkSent": return Success("");
                default:
                    return BadRequest<string>("");
            }
        }


        #endregion
    }
}
