using CleanArchProject.Core.Featurs.Departments.Commands.Models;
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

namespace CleanArchProject.Core.Featurs.Departments.Commands.Validators
{
    public class EditDepartmentValidator : AbstractValidator<EditDepartmentCommand>
    {
        #region Fields
        private readonly IDepartmentService _departmentService;
        private readonly IStringLocalizer<SharedResources.SharedResources> _stringLocalizer;
        private readonly IInstructorService _instructorService;
        #endregion

        #region Constructor
        public EditDepartmentValidator(IDepartmentService departmentService,
            IStringLocalizer<SharedResources.SharedResources> stringLocalizer,
            IInstructorService instructorService)
        {
            _departmentService = departmentService;
            _stringLocalizer = stringLocalizer;
            _instructorService = instructorService;

            ApplayValidationRules();
            ApplayCostumeValidationRules();
        }
        #endregion

        #region Actions
        public void ApplayValidationRules()
        {
            RuleFor(s => s.DepartmentId).NotNull()
            .WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull])
            .GreaterThan(0).WithMessage(_stringLocalizer[SharedResourcesKeys.GreaterThan0]);

            RuleFor(s => s.DepartmentArabicName).NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull])
                .Length(2, 30);

            RuleFor(s => s.DepartmentName).NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull])
                .Length(2, 30);

            RuleFor(s => s.ManagerInstructorId).NotNull()
                .WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull])
                .GreaterThan(0).WithMessage(_stringLocalizer[SharedResourcesKeys.GreaterThan0]);

        }
        public void ApplayCostumeValidationRules()
        {
            RuleFor(s => s.DepartmentArabicName).MustAsync(async (module, key, cancellationToken) => !await _departmentService.IsDepartmentNameExistsById(module.DepartmentName, module.DepartmentId))
                .WithMessage(_stringLocalizer[SharedResourcesKeys.IsAlreadyExits]);

            RuleFor(s => s.DepartmentName).MustAsync(async (module, key, cancellationToken) => !await _departmentService.IsDepartmentArabicNameExistsById(module.DepartmentArabicName, module.DepartmentId))
                .WithMessage(_stringLocalizer[SharedResourcesKeys.IsAlreadyExits]);

            RuleFor(s => s.ManagerInstructorId).MustAsync(async (key, cancellationToken) => await _instructorService.IsInstructorExists(key))
            .WithMessage(_stringLocalizer[SharedResourcesKeys.IsAlreadyExits]);
        }
        #endregion
    }
}
