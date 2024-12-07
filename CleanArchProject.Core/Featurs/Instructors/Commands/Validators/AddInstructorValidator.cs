using CleanArchProject.Core.Featurs.Instructors.Commands.Models;
using CleanArchProject.Core.SharedResources;
using CleanArchProject.Service.Interfaces;
using CleanArchProject.Service.ServicesImplementation;
using FluentValidation;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Featurs.Instructors.Commands.Validators
{
    public class AddInstructorValidator : AbstractValidator<AddInstructorCommand>
    {
        #region Fields
        private readonly IInstructorService _instructorService;
        private readonly IStringLocalizer<SharedResources.SharedResources> _stringLocalizer;
        #endregion

        #region Constructor
        public AddInstructorValidator(IInstructorService instructorService, IStringLocalizer<SharedResources.SharedResources> stringLocalizer)
        {
            _instructorService = instructorService;
            _stringLocalizer = stringLocalizer;
        }
        #endregion

        #region Actions
        public void ApplayValidationRules()
        {
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
                .EmailAddress().WithMessage("This isn't a valid Email");

        }
        public void ApplayCostumeValidationRules()
        {
            RuleFor(s => s.Email).MustAsync(async (module, key, cancellationToken) => !await _instructorService.IsInstructorEmailExists(module.Email))
                .WithMessage("Instructor with the same email is already exists");
            RuleFor(s => s.Phone).MustAsync(async (module, key, cancellationToken) => !await _instructorService.IsInstructorPhoneExists(module.Phone))
            .WithMessage("Instructor with the same phone is already exists");
        }
        #endregion


    }
}
