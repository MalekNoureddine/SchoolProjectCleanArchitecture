﻿using CleanArchProject.Core.Featurs.Subject.Commands.Modles;
using CleanArchProject.Core.SharedResources;
using CleanArchProject.Service.Interfaces;
using FluentValidation;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Featurs.Subject.Commands.Validators
{
    public class EditSubjectValidator : AbstractValidator<EditSubjectCommand>
    {
        #region Fields
        private readonly ISubjectService _subjectService;
        private readonly IStringLocalizer<SharedResources.SharedResources> _stringLocalizer;
        #endregion

        #region Constructor
        public EditSubjectValidator(ISubjectService subjectService,
            IStringLocalizer<SharedResources.SharedResources> stringLocalizer)
        {
            _subjectService = subjectService;
            _stringLocalizer = stringLocalizer;
            ApplayValidationRules();
            ApplayCostumeValidationRules();
        }
        #endregion

        #region Actions
        public void ApplayValidationRules()
        {
            RuleFor(s => s.SubjectName).NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull])
                .Length(2, 30);

            RuleFor(s => s.SubjectArabicName).NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull])
                .Length(2, 30);

            RuleFor(s => s.Period).NotNull()
                .WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull])
                .GreaterThan(0).WithMessage(_stringLocalizer[SharedResourcesKeys.GreaterThan0]);

        }
        public void ApplayCostumeValidationRules()
        {
            RuleFor(s => s.SubjectArabicName).MustAsync(async (module, key, cancellationToken) => !await _subjectService.IsSubjectArabicNameExistsById(module.SubjectArabicName, module.SubjectID))
                .WithMessage(_stringLocalizer[SharedResourcesKeys.IsAlreadyExits]);

            RuleFor(s => s.SubjectName).MustAsync(async (module, key, cancellationToken) => !await _subjectService.IsSubjectNameExistsById(module.SubjectName, module.SubjectID))
                .WithMessage(_stringLocalizer[SharedResourcesKeys.IsAlreadyExits]);
        }
        #endregion
    }
}
