using AutoMapper;
using CleanArchProject.Core.Bases;
using CleanArchProject.Core.Featurs.Departments.Queries.Models;
using CleanArchProject.Core.Featurs.Departments.Queries.Response;
using CleanArchProject.Core.Featurs.Students.Queries.Response;
using CleanArchProject.Core.SharedResources;
using CleanArchProject.Data.Entities;
using CleanArchProject.Service.Interfaces;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Featurs.Departments.Queries.Handler
{
    public class DepartmentQueryHandler : ResponseHandler,
        IRequestHandler<GetADepartmentQuery,Response<GetDepartmentByIdResponse>>,
        IRequestHandler<GetAllDepartmentsQuery, Response<List<GetAllDepartmentResponse>>>,
        IRequestHandler<GetPaginatedDepartmentsListQuery,PaginatedResult<GetPagenatedDepartmentsResponse>>
    {
        #region Fields
        private readonly IDepartmentService _department;
        private readonly IStudentService _student;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources.SharedResources> _stringLocalizer;
        #endregion
        public DepartmentQueryHandler(IDepartmentService department, IStudentService student, IMapper mapper,
            IStringLocalizer<SharedResources.SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _department = department;
            _student = student;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<Response<GetDepartmentByIdResponse>> Handle(GetADepartmentQuery request, CancellationToken cancellationToken)
        {
            var department = await _department.GetDepartmentByIdAsync(request.Id, true);
            if (department == null) return NotFound<GetDepartmentByIdResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);
            //int NumberOfStudents = department.Students.Count;
            int NumberOfInstructors = department.Instructors.Count;
            var departmentmapper = _mapper.Map<GetDepartmentByIdResponse>(department);

            Expression<Func<Student, StudentResponse>> expression = e =>
            new StudentResponse(e.StudID, e.GetLocalized(e.Name, e.NameAr));

            var studentquery = _student.GetAllStudentsByDepartmentQuerable(request.Id);
            int NumberOfStudents = studentquery.Count();
            var paginatedList = await studentquery.Select(expression).ToPaginatedListAsync(request.PageNumber, request.PageSize);
            departmentmapper.StudentList = paginatedList;

            var result = Success(departmentmapper);
            result.Meta = new
            {
                NumberOfStudents = NumberOfStudents,
                NumberOfInstructors = NumberOfInstructors,
            };// you can add any thing you want
            return result;
        }

        public async Task<Response<List<GetAllDepartmentResponse>>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
        {
            var departments = await _department.GetAllDepartmentsAsync(true);
            var NumberOfStudents = departments.SelectMany(d => d.Students).Count();
            var NumberOfInstructors = departments.SelectMany(d => d.Instructors).Count();
            var departmentsMapper = _mapper.Map<List<GetAllDepartmentResponse>>(departments);
            var result = Success(departmentsMapper);
            result.Meta = new
            {
                NumberOfStudents = NumberOfStudents,
                NumberOfInstructors = NumberOfInstructors,
            };// you can add any thing you want
            return result;
        }

        public async Task<PaginatedResult<GetPagenatedDepartmentsResponse>> Handle(GetPaginatedDepartmentsListQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Department, GetPagenatedDepartmentsResponse>> expression = d =>
            new GetPagenatedDepartmentsResponse(d.DID, d.DName, d.InsId);

            //var querable = _student.GetAllStudentsQuerable();
            var FilteredQuerable = _department.GetFilteredDepartmentsQuerable(request.OrderBy, request.Search??"");
            var paginatedList = await FilteredQuerable.Select(expression).ToPaginatedListAsync(request.PageNumber, request.PageSize);
            paginatedList.Meta = new { Count = paginatedList.Data.Count };
            return paginatedList;
        }

    }
}
