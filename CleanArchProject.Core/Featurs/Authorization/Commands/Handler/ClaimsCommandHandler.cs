using CleanArchProject.Core.Bases;
using CleanArchProject.Core.Featurs.Authorization.Commands.Models;
using CleanArchProject.Core.SharedResources;
using CleanArchProject.Service.Interfaces;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Featurs.Authorization.Commands.Handler
{
    public class ClaimsCommandHandler : ResponseHandler,
        IRequestHandler<UpdateUserClaimsCommand, Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources.SharedResources> _stringLocalizer;
        private readonly IAuthorizationService _authorizationService;
        #endregion

        #region Constructor
        public ClaimsCommandHandler(IStringLocalizer<SharedResources.SharedResources> stringLocalizer, IAuthorizationService authorizationService) : base(stringLocalizer)
        {
            this._stringLocalizer = stringLocalizer;
            _authorizationService = authorizationService;
        }
        #endregion

        #region Action
        public async Task<Response<string>> Handle(UpdateUserClaimsCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.ManageUserClaims(request);
            switch (result)
            {
                case "NotFound": return NotFound<string>(_stringLocalizer[SharedResourcesKeys.UserNotFound]);
                case "FailedToRemoveOldClaims": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToRemoveOldClaims]);
                case "FailedToAddNewClaims": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToAddNewClaims]);
                case "Success": return Success<string>(_stringLocalizer[SharedResourcesKeys.Updated]);
                case "FailedToUpdateUserClaims": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.UpdateFailed]);
                default:
                    return Success<string>(result);
            }
        }
        #endregion

    }
}
