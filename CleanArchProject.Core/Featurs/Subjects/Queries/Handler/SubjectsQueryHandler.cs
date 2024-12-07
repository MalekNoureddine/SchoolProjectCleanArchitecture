using AutoMapper;
using CleanArchProject.Core.Bases;
using CleanArchProject.Core.Featurs.Departments.Queries.Response;
using CleanArchProject.Core.Featurs.Instructors.Queries.Models;
using CleanArchProject.Core.Featurs.Instructors.Queries.Response;
using CleanArchProject.Core.Featurs.Subject.Queries.Models;
using CleanArchProject.Core.Featurs.Subject.Queries.Responses;
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

namespace CleanArchProject.Core.Featurs.Subject.Queries.Handler
{
    public class SubjectsQueryHandler : ResponseHandler ,
        IRequestHandler<GetSubjectsListQuery, Response<List<GetSubjectListResponse>>>,
        IRequestHandler<GetSubjectByIdQuery, Response<GetSubjectsByIdResponse>>,
        IRequestHandler<GetPaginatedSubjectListQuery, PaginatedResult<GetPaginatedSubjectListResponse>>


    {
        #region Fields
        private readonly ISubjectService _subjectService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources.SharedResources> _stringLocalizer;
        #endregion
        #region Constructor
        public SubjectsQueryHandler(ISubjectService subjectService, IMapper mapper, IStringLocalizer<SharedResources.SharedResources> stringLocalizer)
            : base(stringLocalizer) 
        {
            _subjectService = subjectService;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }

        #endregion

        #region Actions
        public async Task<Response<List<GetSubjectListResponse>>> Handle(GetSubjectsListQuery request, CancellationToken cancellationToken)
        {
            var subjects = await _subjectService.GetAllSubjectsAsync(true);
            var NumberOfInstructors = subjects.SelectMany(d => d.Instructors).Count();
            var subjectsMapper = _mapper.Map<List<GetSubjectListResponse>>(subjects);
            var result = Success(subjectsMapper);
            result.Meta = new
            {
                NumberOfInstructors = NumberOfInstructors,
            };// you can add any thing you want
            return result;
        }

        public async Task<Response<GetSubjectsByIdResponse>> Handle(GetSubjectByIdQuery request, CancellationToken cancellationToken)
        {
            var subject = await _subjectService.GetSubjectByIdAsync(request.Id, true);
            if (subject == null) return NotFound<GetSubjectsByIdResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);
            //int NumberOfStudents = department.Students.Count;
            int NumberOfInstructors = subject.Instructors.Count;
            var subjectmapper = _mapper.Map<GetSubjectsByIdResponse>(subject);

            var result = Success(subjectmapper);
            result.Meta = new
            {
                NumberOfInstructors = NumberOfInstructors,
            };// you can add any thing you want
            return result;
        }

        public async Task<PaginatedResult<GetPaginatedSubjectListResponse>> Handle(GetPaginatedSubjectListQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Subjects, GetPaginatedSubjectListResponse>> expression = d =>
                        new GetPaginatedSubjectListResponse(d.SubID, d.GetLocalized(d.SubjectName, d.SubjectNameAr), d.Period);

            //var querable = _student.GetAllStudentsQuerable();
            var FilteredQuerable = _subjectService.GetFilteredSubjectsQuerable(request.OrderBy, request.Search ?? "");
            var paginatedList = await FilteredQuerable.Select(expression).ToPaginatedListAsync(request.SubjectsPageNumber, request.SubjectsPageSize);
            paginatedList.Meta = new { Count = paginatedList.Data.Count };
            return paginatedList;
        }

        #endregion
    }
}
