using CleanArchProject.Core.Featurs.Users.Commands.Models;
using CleanArchProject.Core.SharedResources;
using CleanArchProject.Data.Entities.Identities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Featurs.Users.Commands.Validators
{
    public class EditUserValidator : AbstractValidator<EditUserCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources.SharedResources> _localizer;
        private readonly UserManager<User> _userManager;

        #endregion

        #region Constructors
        public EditUserValidator(IStringLocalizer<SharedResources.SharedResources> localizer, UserManager<User> userManager)
        {
            _localizer = localizer;
            _userManager = userManager;
            ApplyValidationsRules();
            ApplayCostumeValidationRules();
        }
        #endregion

        #region Handle Functions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.FullName)
                 .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                 .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
                 .MaximumLength(100).WithMessage(_localizer[SharedResourcesKeys.MaxLengthis100]);

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
                .MaximumLength(100).WithMessage(_localizer[SharedResourcesKeys.MaxLengthis100]);

            RuleFor(x => x.Email)
                 .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                 .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);
        }
        public void ApplayCostumeValidationRules()
        {

            RuleFor(s => s.UserName).MustAsync(async (module, key, cancellationToken) => await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == module.UserName && x.Id != module.Id) == null)
                .WithMessage(_localizer[SharedResourcesKeys.IsAlreadyExits]);

            RuleFor(s => s.Email).MustAsync(async (module, key, cancellationToken) => await _userManager.Users.FirstOrDefaultAsync(x => x.Email == module.Email && x.Id != module.Id) == null)
                .WithMessage(_localizer[SharedResourcesKeys.IsAlreadyExits]);


        }

        #endregion
    }
}
