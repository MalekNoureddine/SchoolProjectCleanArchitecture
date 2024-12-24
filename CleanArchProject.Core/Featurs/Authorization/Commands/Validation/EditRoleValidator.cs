using CleanArchProject.Core.Featurs.Authorization.Commands.Models;
using CleanArchProject.Core.SharedResources;
using CleanArchProject.Data.Requests;
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
    internal class EditRoleValidator : AbstractValidator<EditRoleRequest>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources.SharedResources> _stringLocalizer;
        private readonly IAuthorizationService _authorizationService;
        #endregion
        #region Constructor
        public EditRoleValidator(IStringLocalizer<SharedResources.SharedResources> stringLocalizer, IAuthorizationService authorizationService)
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
            RuleFor(s => s.Name).NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull]);
            RuleFor(s => s.Id).NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull]);
        }
        public void ApplayCostumeValidationRules()
        {
            RuleFor(s => s.Name).MustAsync(async (x, key, cancellationToken) => !await _authorizationService.IsRoleExists(x.Name, x.Id))
            .WithMessage(_stringLocalizer[SharedResourcesKeys.RoleAlreadyExists]);
        }
        #endregion
    }
}
