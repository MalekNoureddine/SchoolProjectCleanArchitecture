using AutoMapper;
using CleanArchProject.Core.Bases;
using CleanArchProject.Core.Featurs.Students.Commands.Models;
using CleanArchProject.Data.Entities;
using CleanArchProject.Service.Interfaces;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Featurs.Students.Commands.Handler
{
    public class StudentCommandHandler :ResponseHandler, IRequestHandler<AddStudentCommand, Response<string>>,
        IRequestHandler<EditStudentCommand, Response<string>>,
        IRequestHandler<DeleteStudentCommand, Response<string>>
    {
        
        #region Fields
        private readonly IStudentService _student;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public StudentCommandHandler(IStudentService student, IMapper mapper,
            IStringLocalizer<SharedResources.SharedResources> stringLocalizer): base(stringLocalizer)
        {
            _student = student;
            _mapper = mapper;
        }
        #endregion

        #region Handle Functions
        public async Task<Response<string>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            var student = _mapper.Map<Student>(request);
            var result =  await _student.AddStudentAsync(student);

            if (result == "Created")
                return Created<string>("");
            else if (result == "Conflict")
                return Conflict<string>("");
            else
                return InternalServerError<string>();
        }

        public async Task<Response<string>> Handle(EditStudentCommand request, CancellationToken cancellationToken)
        {
            //check if Id exists
            var student = await _student.GetStudentByIdAsync(request.Id);
            //return not found if not exist
            if (student == null)
                return NotFound<string>();
            //map between the request and the student
            var studentmapper = _mapper.Map(request,student);
            //cal the edit service
            if (studentmapper is null)
                return NotFound<string>();

            var result = await _student.EditStudentAsync(studentmapper);
            //return response
            if (result == "Success")
                return Success("");
            else if (result == "Conflict")
                return Conflict<string>("");
            else
                return InternalServerError<string>();
        }
        public async Task<Response<string>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            //check if Id exists
            var student = await _student.GetStudentByIdAsync(request.Id);

            //return not found if not exist
            if (student == null)
                return NotFound<string>();

            //cal the edit service
            var result = await _student.DeleteStudentAsync(student);

            //return response
            if (result == "Deleted") return Deleted<string>("");
            else return InternalServerError<string>();
        }

        #endregion

    }
}
