using CleanArchProject.Core.Featurs.Authorization.Commands.Models;
using CleanArchProject.Core.SharedResources;
using CleanArchProject.Service.Interfaces;
using FluentValidation;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Featurs.Authorization.Commands.Validation
{
    public class AddRoleValidator : AbstractValidator<AddRoleCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources.SharedResources> _stringLocalizer;
        private readonly IAuthorizationService _authorizationService;
        #endregion
        #region Constructor
        public AddRoleValidator(IStringLocalizer<SharedResources.SharedResources> stringLocalizer, IAuthorizationService authorizationService)
        {
            _stringLocalizer = stringLocalizer;
            _authorizationService = authorizationService;
            ApplayValidationRules();
            ApplayCostumeValidationRules();
        }
        #endregion

        #region Action
        public void ApplayValidationRules()
        {
            RuleFor(s => s.RoleName).NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull]);
         }
        public void ApplayCostumeValidationRules()
        {
            RuleFor(s => s.RoleName).MustAsync(async (key, cancellationToken) => !await _authorizationService.IsRoleExists(key))
    .WithMessage(_stringLocalizer[SharedResourcesKeys.RoleAlreadyExists]);
        }
        #endregion
    }
}
