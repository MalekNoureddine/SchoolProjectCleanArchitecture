using CleanArchProject.Core.Bases;
using CleanArchProject.Core.Featurs.Authorization.Commands.Models;
using CleanArchProject.Core.SharedResources;
using CleanArchProject.Data.Entities.Identies;
using CleanArchProject.Service.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Featurs.Authorization.Commands.Handler
{
    public class RoleCommandsHandler : ResponseHandler, IRequestHandler<AddRoleCommand, Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources.SharedResources> _stringLocalizer;
        private readonly IAuthorizationService _authorizationService;
        #endregion

        #region Constructor
        public RoleCommandsHandler(RoleManager<Role> roleManager
            , IStringLocalizer<SharedResources.SharedResources> stringLocalizer,
IAuthorizationService authorizationService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _authorizationService = authorizationService;
        }
        #endregion


        public async Task<Response<string>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.AddRole(request.RoleName);
            return result == "Success" ? Success<string>("") : BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FaildToAdd]);
        }
    }
}
