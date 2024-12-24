using AutoMapper;
using CleanArchProject.Core.Bases;
using CleanArchProject.Core.Featurs.Authorization.Queries.Models;
using CleanArchProject.Core.Featurs.Authorization.Queries.Responses;
using CleanArchProject.Core.SharedResources;
using CleanArchProject.Service.Interfaces;
using MediatR;
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
        IRequestHandler<GetRoleByNameQuery, Response<GetRoleResponse>>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources.SharedResources> _stringLocalizer;
        private readonly IAuthorizationService _authorizationService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public RoleQueryHandler(IStringLocalizer<SharedResources.SharedResources> stringLocalizer,
            IAuthorizationService authorizationService, IMapper mapper) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _authorizationService = authorizationService;
            _mapper = mapper;
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

        #endregion
    }
}
