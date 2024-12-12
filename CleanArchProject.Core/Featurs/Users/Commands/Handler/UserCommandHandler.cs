using AutoMapper;
using CleanArchProject.Core.Bases;
using CleanArchProject.Core.Featurs.Users.Commands.Models;
using CleanArchProject.Core.SharedResources;
using CleanArchProject.Data.Entities.Identities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Featurs.Users.Commands.Handler
{
    public class UserCommandHandler : ResponseHandler,
            IRequestHandler<AddUserCommand, Response<string>>,
            IRequestHandler<ChangeUserPasswordCommand, Response<string>>,
            IRequestHandler<EditUserCommand, Response<string>>,
            IRequestHandler<DeleteUserCommand, Response<string>>
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources.SharedResources> _sharedResources;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion

        #region Constructors
        public UserCommandHandler(IStringLocalizer<SharedResources.SharedResources> stringLocalizer,
                                  IMapper mapper,
                                  UserManager<User> userManager,
                                  IHttpContextAccessor httpContextAccessor) : base(stringLocalizer)
        {
            _mapper = mapper;
            _sharedResources = stringLocalizer;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion

        #region Actions
        public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var identityUser = _mapper.Map<User>(request);
            var result = await _userManager.CreateAsync(identityUser, request.Password);
            if (!result.Succeeded)
                return  BadRequest<string>(string.Join(",", result.Errors.Select(x => x.Description).ToList()));
            return Success("");
        }

        public async Task<Response<string>> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id.ToString());

            if (user == null) return NotFound<string>();

            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

            if (!result.Succeeded) return BadRequest<string>(result.Errors.FirstOrDefault().Description);
            return Success((string)_sharedResources[SharedResourcesKeys.Success]);
        }

        public async Task<Response<string>> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {

            var oldUser = await _userManager.FindByIdAsync(request.Id.ToString());
            
            if (oldUser == null) return NotFound<string>();
            
            var newUser = _mapper.Map(request, oldUser);

            //update
            var result = await _userManager.UpdateAsync(newUser);
            //result is not success
            if (!result.Succeeded) return BadRequest<string>(_sharedResources[SharedResourcesKeys.UpdateFailed]);
            //message
            return Success((string)_sharedResources[SharedResourcesKeys.Updated]);
        }

        public async Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            //check if user is exist
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            //if Not Exist notfound
            if (user == null) return NotFound<string>();
            //Delete the User
            var result = await _userManager.DeleteAsync(user);
            //in case of Failure
            if (!result.Succeeded) return BadRequest<string>(_sharedResources[SharedResourcesKeys.DeleteionFailed]);
            return Success((string)_sharedResources[SharedResourcesKeys.Deleted]);
        }




        #endregion

    }
}