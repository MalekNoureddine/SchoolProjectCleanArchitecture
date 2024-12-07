using AutoMapper;
using CleanArchProject.Core.Bases;
using CleanArchProject.Core.Featurs.Departments.Commands.Models;
using CleanArchProject.Core.Featurs.Subject.Commands.Modles;
using CleanArchProject.Data.Entities;
using CleanArchProject.Service.Interfaces;
using CleanArchProject.Service.ServicesImplementation;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Featurs.Subject.Commands.Handler
{
    internal class SubjectCommandHandler : ResponseHandler,
        IRequestHandler<AddSubjectCommand, Response<string>>
       , IRequestHandler<EditSubjectCommand, Response<string>>
       , IRequestHandler<DeleteSubjectCommand, Response<string>>
    {
        #region Fields
        private readonly ISubjectService _subjectService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources.SharedResources> _stringLocalizer;
        #endregion
        #region Constructor
        public SubjectCommandHandler(ISubjectService subjectService, IMapper mapper, IStringLocalizer<SharedResources.SharedResources> stringLocalizer)
            : base(stringLocalizer)
        {
            _subjectService = subjectService;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }

        #endregion

        #region Actions
        public async Task<Response<string>> Handle(AddSubjectCommand request, CancellationToken cancellationToken)
        {
            var subject = _mapper.Map<Subjects>(request);
            var result = await _subjectService.AddSubjectAsync(subject);

            if (result == "Created")
                return Created<string>("");
            else if (result == "Conflict")
                return Conflict<string>("");
            else
                return InternalServerError<string>();
        }

        public async Task<Response<string>> Handle(EditSubjectCommand request, CancellationToken cancellationToken)
        {
            //check if Id exists
            var subject = await _subjectService.GetSubjectByIdAsync(request.SubjectID);
            //return not found if not exist
            if (subject == null)
                return NotFound<string>();
            //map between the request and the subject
            var subjectmapper = _mapper.Map(request, subject);
            //cal the edit service
            if (subjectmapper is null)
                return NotFound<string>();

            var result = await _subjectService.EditSubjectAsync(subjectmapper);
            //return response
            if (result == "Success")
                return Success("");
            else if (result == "Conflict")
                return Conflict<string>("");
            else
                return InternalServerError<string>();
        }

        public async Task<Response<string>> Handle(DeleteSubjectCommand request, CancellationToken cancellationToken)
        {
            //check if Id exists
            var subject = await _subjectService.GetSubjectByIdAsync(request.Id);

            //return not found if not exist
            if (subject == null)
                return NotFound<string>();

            //cal the edit service
            var result = await _subjectService.DeleteSubjectAsync(subject);

            //return response
            if (result == "Deleted") return Deleted<string>("");
            else return InternalServerError<string>();
        }

        #endregion

    }
}
