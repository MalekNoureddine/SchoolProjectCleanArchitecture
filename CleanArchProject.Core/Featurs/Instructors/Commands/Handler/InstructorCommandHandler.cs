using AutoMapper;
using CleanArchProject.Core.Bases;
using CleanArchProject.Core.Featurs.Instructors.Commands.Models;
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

namespace CleanArchProject.Core.Featurs.Instructors.Commands.Handler
{
    public class InstructorCommandHandler : ResponseHandler
       , IRequestHandler<AddInstructorCommand, Response<string>>
       , IRequestHandler<DeleteInstructorCommand, Response<string>>
       , IRequestHandler<EditInstructorCommand, Response<string>>
    {
        #region Fields
        private readonly IInstructorService _instructorService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public InstructorCommandHandler(IInstructorService instructorService, IMapper mapper,
            IStringLocalizer<SharedResources.SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _instructorService = instructorService;
            _mapper = mapper;
        }
        #endregion

        #region Actions

        public async Task<Response<string>> Handle(EditInstructorCommand request, CancellationToken cancellationToken)
        {
            //check if Id exists
            var instructor = await _instructorService.GetInstructorByIdAsync(request.Id);
            //return not found if not exist
            if (instructor == null)
                return NotFound<string>();
            //map between the request and the student
            var instructormapper = _mapper.Map(request, instructor);
            //cal the edit service
            if (instructormapper is null)
                return NotFound<string>();

            var result = await _instructorService.EditInstructorAsync(instructormapper);
            //return response
            if (result == "Success")
                return Success("");
            else if (result == "Conflict")
                return Conflict<string>("");
            else
                return InternalServerError<string>();
        }

        public async Task<Response<string>> Handle(AddInstructorCommand request, CancellationToken cancellationToken)
        {
            var instructor = _mapper.Map<Instructor>(request);
            var result = await _instructorService.AddInstructorAsync(instructor);

            if (result == "Created")
                return Created<string>("");
            else if (result == "Conflict")
                return Conflict<string>("");
            else
                return InternalServerError<string>();
        }

        public async Task<Response<string>> Handle(DeleteInstructorCommand request, CancellationToken cancellationToken)
        {
            //check if Id exists
            var instructor = await _instructorService.GetInstructorByIdAsync(request.Id);

            //return not found if not exist
            if (instructor == null)
                return NotFound<string>();

            //cal the edit service
            var result = await _instructorService.DeleteInstructorAsync(instructor);

            //return response
            if (result == "Deleted") return Deleted<string>("");
            else return InternalServerError<string>();
        }
        #endregion

    }
}
