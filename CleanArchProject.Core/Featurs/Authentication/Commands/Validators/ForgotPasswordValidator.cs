using CleanArchProject.Core.Featurs.Authentication.Commands.Models;
using CleanArchProject.Core.SharedResources;
using FluentValidation;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Featurs.Authentication.Commands.Validators
{
    public class ForgotPasswordValidator : AbstractValidator<ForgotPasswordQuery>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources.SharedResources> _localizer;
        #endregion

        #region Constructors
        public ForgotPasswordValidator(IStringLocalizer<SharedResources.SharedResources> localizer)
        {
            _localizer = localizer;
            ApplyValidationsRules();
        }
        #endregion

        #region Actions
        public void ApplyValidationsRules()
        {

            RuleFor(x => x.Email)
                 .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                 .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]).EmailAddress();

        }

        #endregion
    }
}
