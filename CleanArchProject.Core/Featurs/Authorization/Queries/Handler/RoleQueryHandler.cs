using AutoMapper;
using CleanArchProject.Core.Bases;
using CleanArchProject.Core.Featurs.Authorization.Queries.Models;
using CleanArchProject.Core.Featurs.Authorization.Queries.Responses;
using CleanArchProject.Core.SharedResources;
using CleanArchProject.Data.Entities.Identities;
using CleanArchProject.Data.Results;
using CleanArchProject.Service.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Featurs.Authorization.Queries.Handler
{
    public class RoleQueryHandler : ResponseHandler,
        IRequestHandler<GetRolesListQuery, Response<List<GetRoleResponse>>>,
        IRequestHandler<GetRoleByIdQuery, Response<GetRoleResponse>>,
        IRequestHandler<GetRoleByNameQuery, Response<GetRoleResponse>>,
        IRequestHandler<ManageUserRolesQuery, Response<ManageUserRolesResult>>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources.SharedResources> _stringLocalizer;
        private readonly IAuthorizationService _authorizationService;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        #endregion

        #region Constructor
        public RoleQueryHandler(IStringLocalizer<SharedResources.SharedResources> stringLocalizer,
            IAuthorizationService authorizationService, IMapper mapper, UserManager<User> userManager) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _authorizationService = authorizationService;
            _mapper = mapper;
            _userManager = userManager;
        }
        #endregion

        #region Actions
        public async Task<Response<List<GetRoleResponse>>> Handle(GetRolesListQuery request, CancellationToken cancellationToken)
        {
            var roles = await _authorizationService.GetRolesList();
            var result = _mapper.Map<List<GetRoleResponse>>(roles);
            return Success(result);
        }

        public async Task<Response<GetRoleResponse>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _authorizationService.GetRolesById(request.Id);
            if (role is null) return NotFound<GetRoleResponse>(SharedResourcesKeys.RoleDoseNotExist);
            var result = _mapper.Map<GetRoleResponse>(role);
            return Success(result);
        }

        public async Task<Response<GetRoleResponse>> Handle(GetRoleByNameQuery request, CancellationToken cancellationToken)
        {
            var role = await _authorizationService.GetRolesByName(request.Name);
            if (role is null) return NotFound<GetRoleResponse>(SharedResourcesKeys.RoleDoseNotExist);
            var result = _mapper.Map<GetRoleResponse>(role);
            return Success(result);
        }

        public async Task<Response<ManageUserRolesResult>> Handle(ManageUserRolesQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user is null) return NotFound<ManageUserRolesResult>(_stringLocalizer[SharedResourcesKeys.NotFound]);
            var response = await _authorizationService.GetRolesOfUsuer(user);
            return Success(response);
        }

        #endregion
    }
}
