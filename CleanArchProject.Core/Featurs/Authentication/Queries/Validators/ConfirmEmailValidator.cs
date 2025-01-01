using CleanArchProject.Core.Featurs.Authentication.Queries.Models;
using CleanArchProject.Core.SharedResources;
using FluentValidation;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Featurs.Authentication.Queries.Validators
{
    public class ConfirmEmailValidator : AbstractValidator<ConfirmEmailQuery>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources.SharedResources> _localizer;
        #endregion

        #region Constructors
        public ConfirmEmailValidator(IStringLocalizer<SharedResources.SharedResources> localizer)
        {
            _localizer = localizer;
            ApplyValidationsRules();
        }
        #endregion

        #region Actions
        public void ApplyValidationsRules()
        {

            RuleFor(x => x.UserId)
                 .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                 .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);

            RuleFor(x => x.Code)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);
        }


        #endregion
    }
}
