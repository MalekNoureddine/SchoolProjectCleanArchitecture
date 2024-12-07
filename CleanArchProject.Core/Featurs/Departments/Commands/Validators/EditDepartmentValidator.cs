using CleanArchProject.Core.Featurs.Departments.Commands.Models;
using CleanArchProject.Core.SharedResources;
using CleanArchProject.Service.Interfaces;
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
        #endregion

        #region Constructor
        public EditDepartmentValidator(IDepartmentService departmentService,
            IStringLocalizer<SharedResources.SharedResources> stringLocalizer)
        {
            _departmentService = departmentService;
            _stringLocalizer = stringLocalizer;
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
            RuleFor(s => s.DepartmentArabicName).MustAsync(async (module, key, cancellationToken) => !await _departmentService.IsDepartmentNameExists(module.DepartmentName, module.DepartmentArabicName, module.DepartmentId))
                .WithMessage("Department with the same Name is already exists");
        }
        #endregion
    }
}
