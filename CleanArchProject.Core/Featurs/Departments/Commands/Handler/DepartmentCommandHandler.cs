using AutoMapper;
using CleanArchProject.Core.Bases;
using CleanArchProject.Core.Featurs.Departments.Commands.Models;
using CleanArchProject.Data.Entities;
using CleanArchProject.Service.Interfaces;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Featurs.Departments.Commands.Handler
{
    public class DepartmentCommandHandler : ResponseHandler,
        IRequestHandler<AddDepartmentCommand, Response<string>>
       , IRequestHandler<EditDepartmentCommand, Response<string>>
       , IRequestHandler<DeleteDepartmentCommand, Response<string>>
    {
        #region Fields
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;
        #endregion
        #region constructor
        public DepartmentCommandHandler(IMapper mapper, IDepartmentService departmentService, IStringLocalizer<SharedResources.SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _departmentService = departmentService;
            _mapper = mapper;
        }
        #endregion

        #region Actions
        public async Task<Response<string>> Handle(AddDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = _mapper.Map<Department>(request);
            var result = await _departmentService.AddDepartmentAsync(department);

            if (result == "Created")
                return Created<string>("");
            else if (result == "Conflict")
                return Conflict<string>("");
            else
                return InternalServerError<string>();
        }
   
        public async Task<Response<string>> Handle(EditDepartmentCommand request, CancellationToken cancellationToken)
        {
            //check if Id exists
            var department = await _departmentService.GetDepartmentByIdAsync(request.DepartmentId);
            //return not found if not exist
            if (department == null)
                return NotFound<string>();
            //map between the request and the student
            var departmentmapper = _mapper.Map(request, department);
            //cal the edit service
            if (departmentmapper is null)
                return NotFound<string>();

            var result = await _departmentService.EditDepartmentAsync(departmentmapper);
            //return response
            if (result == "Success")
                return Success("");
            else if (result == "Conflict")
                return Conflict<string>("");
            else
                return InternalServerError<string>();
        }
        
        public async Task<Response<string>> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            //check if Id exists
            var department = await _departmentService.GetDepartmentByIdAsync(request.Id);

            //return not found if not exist
            if (department == null)
                return NotFound<string>();

            //cal the edit service
            var result = await _departmentService.DeleteDepartmentAsync(department);

            //return response
            if (result == "Deleted") return Deleted<string>("");
            else return InternalServerError<string>();
        }
        #endregion

    }
}
