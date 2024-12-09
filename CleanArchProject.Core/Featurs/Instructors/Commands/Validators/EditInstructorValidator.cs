using CleanArchProject.Core.Featurs.Instructors.Commands.Models;
using CleanArchProject.Core.SharedResources;
using CleanArchProject.Service.Interfaces;
using FluentValidation;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Featurs.Instructors.Commands.Validators
{
    public class EditInstructorValidator : AbstractValidator<EditInstructorCommand>
    {
        #region Fields
        private readonly IInstructorService _instructorService;
        private readonly IStringLocalizer<SharedResources.SharedResources> _stringLocalizer;
        #endregion

        #region Constructor
        public EditInstructorValidator(IInstructorService instructorService, IStringLocalizer<SharedResources.SharedResources> stringLocalizer)
        {
            _instructorService = instructorService;
            _stringLocalizer = stringLocalizer;
        }
        #endregion

        #region Actions
        public void ApplayValidationRules()
        {
            RuleFor(s => s.Id).NotNull()
                .WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull])
                .GreaterThan(0).WithMessage(_stringLocalizer[SharedResourcesKeys.GreaterThan0]);

            RuleFor(s => s.Name).NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull])
                .Length(2, 30);

            RuleFor(s => s.NameInArabic).NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull])
                .Length(2, 30);

            RuleFor(s => s.SupervisorId).NotNull()
                .WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull])
                .GreaterThan(0).WithMessage(_stringLocalizer[SharedResourcesKeys.GreaterThan0]);


            RuleFor(s => s.Email).NotNull()
                .WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull])
                .EmailAddress().WithMessage(_stringLocalizer[SharedResourcesKeys.NotValid]);

        }
        public void ApplayCostumeValidationRules()
        {
            RuleFor(s => s.Email).MustAsync(async (module, key, cancellationToken) => !await _instructorService.IsInstructorEmailExistsById(module.Email, module.Id))
                .WithMessage(_stringLocalizer[SharedResourcesKeys.DoseNotExists]);
            RuleFor(s => s.Phone).MustAsync(async (module, key, cancellationToken) => !await _instructorService.IsInstructorPhoneExistsById(module.Phone, module.Id))
                .WithMessage(_stringLocalizer[SharedResourcesKeys.DoseNotExists]);

            RuleFor(s => s.SupervisorId).MustAsync(async (key, cancellationToken) => await _instructorService.IsInstructorExists(key ?? -1))
                .WithMessage(_stringLocalizer[SharedResourcesKeys.DoseNotExists]);
        }
        #endregion
    }
}
