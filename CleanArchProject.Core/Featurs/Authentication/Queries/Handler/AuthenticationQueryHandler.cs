using CleanArchProject.Core.Bases;
using CleanArchProject.Core.Featurs.Authentication.Commands.Models;
using CleanArchProject.Core.Featurs.Authentication.Queries.Models;
using CleanArchProject.Core.SharedResources;
using CleanArchProject.Data.Entities.Identities;
using CleanArchProject.Data.Healper;
using CleanArchProject.Service.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Featurs.Authentication.Queries.Handler
{
    public class AuthenticationQueryHandler : ResponseHandler,
        IRequestHandler<AuthorizeUserQuery, Response<string>>,
        IRequestHandler<ConfirmEmailQuery, Response<string>>,
        IRequestHandler<ForgotPasswordQuery, Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources.SharedResources> _stringLocalizer;
        private readonly IAuthenticationService _authenticationService;
        #endregion

        #region Constructor
        public AuthenticationQueryHandler(IStringLocalizer<SharedResources.SharedResources> stringLocalizer, UserManager<User> userManager, SignInManager<User> signInManager, IAuthenticationService authenticationService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _authenticationService = authenticationService;
        }
        #endregion

        #region Actions
        public async Task<Response<string>> Handle(AuthorizeUserQuery request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.ValidateToken(request.AccessToken);
            return result == "IsActive" ? Success(result) : Unauthorized<string>(_stringLocalizer[SharedResourcesKeys.TokenIsExpired]);
        }

        public async Task<Response<string>> Handle(ConfirmEmailQuery request, CancellationToken cancellationToken)
        {
            var confirmEmail = await _authenticationService.ConfirmEmail(request.UserId, request.Code);
            switch (confirmEmail)
            {
                case "NotFound": return NotFound<string>(_stringLocalizer[SharedResourcesKeys.UserNotFound]);
                case "Succeeded": return Success<string>(_stringLocalizer[SharedResourcesKeys.EmailConfirmed]);
                default:
                    return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.EmailConfermationFaild]);
            }
        }

        public async Task<Response<string>> Handle(ForgotPasswordQuery request, CancellationToken cancellationToken)
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
