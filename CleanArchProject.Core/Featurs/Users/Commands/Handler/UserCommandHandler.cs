using AutoMapper;
using CleanArchProject.Core.Bases;
using CleanArchProject.Core.Featurs.Users.Commands.Models;
using CleanArchProject.Core.SharedResources;
using CleanArchProject.Data.Entities.Identities;
using CleanArchProject.Service.Interfaces;
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
            IRequestHandler<EditUserCommand, Response<string>>
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources.SharedResources> _sharedResources;
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;
        #endregion

        #region Constructors
        public UserCommandHandler(IStringLocalizer<SharedResources.SharedResources> stringLocalizer,
                                  IMapper mapper,
                                  UserManager<User> userManager,
                                  IUserService userService) : base(stringLocalizer)
        {
            _mapper = mapper;
            _sharedResources = stringLocalizer;
            _userManager = userManager;
            _userService = userService;
        }
        #endregion

        #region Actions
        public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var identityUser = _mapper.Map<User>(request);
            //Create
            var createResult = await _userService.AddUserAsync(identityUser, request.Password);
            switch (createResult)
            {
                case "ErrorInCreateUser": return BadRequest<string>(_sharedResources[SharedResourcesKeys.FaildToAdd]);
                case "Failed": return BadRequest<string>(_sharedResources[SharedResourcesKeys.TryAgain]);
                case "Success": return Success<string>("");
                default: return BadRequest<string>(createResult);
            }
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




        #endregion

    }
}