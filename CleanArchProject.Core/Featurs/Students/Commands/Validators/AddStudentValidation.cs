using CleanArchProject.Core.Featurs.Students.Commands.Models;
using CleanArchProject.Core.SharedResources;
using CleanArchProject.Service.Interfaces;
using CleanArchProject.Service.ServicesImplementation;
using FluentValidation;
using Microsoft.Extensions.Localization;
using System.Numerics;

namespace CleanArchProject.Core.Featurs.Students.Commands.Validators
{
    public class AddStudentValidation : AbstractValidator<AddStudentCommand>
    {
        #region Fields
        private readonly IStudentService _studentService;
        private readonly IDepartmentService _departmentService;
        private readonly IStringLocalizer<SharedResources.SharedResources> _stringLocalizer;
        #endregion

        #region Constructor
        public AddStudentValidation(IStudentService studentService,
            IStringLocalizer<SharedResources.SharedResources> stringLocalizer,
            IDepartmentService departmentService)
        {
            _studentService = studentService;
            _stringLocalizer = stringLocalizer;
            _departmentService = departmentService;
            ApplayValidationRules();
            ApplayCostumeValidationRules();
        }
        #endregion

        #region Actions
        public void ApplayValidationRules()
        {
            RuleFor(s => s.Name).NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull])
                .Length(3, 30);

            RuleFor(s => s.NameAr).NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull])
                .Length(3, 30);

            RuleFor(s => s.Address).NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull]).MaximumLength(100);

            RuleFor(s => s.Phone).Matches(@"^\+?\d{10,15}$").WithMessage("Phone number must be between 10 and 15 digits and may start with a '+'.")
                .NotEmpty();

            RuleFor(s => s.DepartmentId).GreaterThan(0).WithMessage(_stringLocalizer[SharedResourcesKeys.GreaterThan0]);
        }
        public void ApplayCostumeValidationRules()
        {
            RuleFor(s => s.Phone).MustAsync(async (key, cancellationToken) => !await _studentService.IsPhoneNumberExists(key))
                .WithMessage("Student with the same phone number is already exists");

            RuleFor(s => s.DepartmentId).MustAsync(async (module, key, cancellationToken) => await _departmentService.IsDepartmentExists(module.DepartmentId))
                .WithMessage(_stringLocalizer[SharedResourcesKeys.DoseNotExists]);

        }
        #endregion
    }
}
