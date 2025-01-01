using CleanArchProject.Core.Featurs.Departments.Commands.Models;
using CleanArchProject.Core.Featurs.Email.Commands.Modles;
using CleanArchProject.Core.SharedResources;
using CleanArchProject.Service.Interfaces;
using FluentValidation;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Featurs.Email.Commands.Validation
{
    internal class SendEmailValidation : AbstractValidator<SendEmailCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources.SharedResources> _stringLocalizer;
        #endregion

        #region Constructor
        public SendEmailValidation( IStringLocalizer<SharedResources.SharedResources> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;

            ApplayValidationRules();
            ApplayCostumeValidationRules();
        }
        #endregion

        #region Actions
        public void ApplayValidationRules()
        {
            RuleFor(s => s.Email).NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull])
                .EmailAddress().WithMessage(_stringLocalizer[SharedResourcesKeys.EmailNotValid]);

            RuleFor(s => s.Message).NotNull()
                .WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull])
                .WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty]);
        }
        public void ApplayCostumeValidationRules()
        {
        }
        #endregion
    }
}
