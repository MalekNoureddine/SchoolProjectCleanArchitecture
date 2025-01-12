using AutoMapper;
using CleanArchProject.Core.Bases;
using CleanArchProject.Core.Featurs.Departments.Queries.Response;
using CleanArchProject.Core.Featurs.Instructors.Queries.Models;
using CleanArchProject.Core.Featurs.Instructors.Queries.Models.View;
using CleanArchProject.Core.Featurs.Instructors.Queries.Response;
using CleanArchProject.Core.Featurs.Instructors.Queries.Response.View;
using CleanArchProject.Core.Featurs.Students.Queries.Models;
using CleanArchProject.Core.Featurs.Students.Queries.Response;
using CleanArchProject.Core.SharedResources;
using CleanArchProject.Data.Entities;
using CleanArchProject.Data.Entities.Views;
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

namespace CleanArchProject.Core.Featurs.Instructors.Queries.Handler
{
    public class InstructorQueryHandler : ResponseHandler,
        IRequestHandler<GetInstructorsListQuery, Response<List<GetInstructorsListResponse>>>,
        IRequestHandler<GetInstructorByIdQuery, Response<GetInstructorByIdResponse>>,
        IRequestHandler<GetPaginatedInstructorsListQuery, PaginatedResult<GetPaginatedInstructorsListResponse>>,
        IRequestHandler<GetInstructorViewByIdQuery, Response<InstructorViewResponse>>,
        IRequestHandler<GetInstructorsViewsListQuery, Response<List<InstructorViewResponse>>>,
        IRequestHandler<GetInstructorViewsPaginatedList, PaginatedResult<InstructorViewResponse>>
    {
        #region Fields
        private readonly IInstructorService _instructorService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources.SharedResources> _stringLocalizer;
        #endregion

        #region Constructor
        public InstructorQueryHandler(IInstructorService instructorService, IMapper mapper,
            IStringLocalizer<SharedResources.SharedResources> stringLocalizer)
            : base(stringLocalizer)
        {
            _instructorService = instructorService;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }
        #endregion

        #region Functions Handling
        public async Task<Response<List<GetInstructorsListResponse>>> Handle(GetInstructorsListQuery request, CancellationToken cancellationToken)
        {
            var instructor = await _instructorService.GetAllInstructorsAsync(true);

            var departmentsMapper = _mapper.Map<List<GetInstructorsListResponse>>(instructor);
            var result = Success(departmentsMapper);
            
            return result;
        }

        public async  Task<Response<GetInstructorByIdResponse>> Handle(GetInstructorByIdQuery request, CancellationToken cancellationToken)
        {
            var instructor = await _instructorService.GetInstructorByIdAsync(request.Id, true);
            if (instructor == null) return NotFound<GetInstructorByIdResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);

            var instructormapper = _mapper.Map<GetInstructorByIdResponse>(instructor);

            Expression<Func<Instructor, Supervised>> expression = e =>
            new Supervised(e.InsId, e.GetLocalized(e.EName, e.ENameAr));

            var supervisedInstructorquery = _instructorService.GetInstructorsListBySupervisorIdQuerable(request.Id);
            int NumberOfsupervisedInstructors = supervisedInstructorquery.Count();
            var paginatedList = await supervisedInstructorquery.Select(expression).ToPaginatedListAsync(request.PageNumber, request.PageSize);
            instructormapper.Superviseds = paginatedList;


            
            var result = Success(instructormapper);
            result.Meta = new { NumberOfsupervisedInstructors };
            return result;
        }

        public async Task<PaginatedResult<GetPaginatedInstructorsListResponse>> Handle(GetPaginatedInstructorsListQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Instructor, GetPaginatedInstructorsListResponse>> expression = i =>
            new GetPaginatedInstructorsListResponse(i.InsId, i.GetLocalized
            (i.EName, i.ENameAr), i.Address, i.Position, i.SupervisorId, i.Image);

            var FilteredQuerable = _instructorService.GetFilteredInstructorsQuerable(request.OrderBy, request.Search ?? "");
            var paginatedList = await FilteredQuerable.Select(expression).ToPaginatedListAsync(request.InstructorsPageNumber, request.InstructorsPageSize);
            paginatedList.Meta = new { Count = paginatedList.Data.Count };
            return paginatedList;
        }

        public async Task<Response<InstructorViewResponse>> Handle(GetInstructorViewByIdQuery request, CancellationToken cancellationToken)
        {
            var instructor = await _instructorService.GetInstructorViewByIdAsync(request.InstructorId);
            if (instructor == null) return NotFound<InstructorViewResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);

            var instructormapper = _mapper.Map<InstructorViewResponse>(instructor);
            return Success(instructormapper);
        }

        public async Task<Response<List<InstructorViewResponse>>> Handle(GetInstructorsViewsListQuery request, CancellationToken cancellationToken)
        {
            var instructor = await _instructorService.GetInstructorsViewListAsync();

            var departmentsMapper = _mapper.Map<List<InstructorViewResponse>>(instructor);
            var result = Success(departmentsMapper);

            return result;
        }

        public async Task<PaginatedResult<InstructorViewResponse>> Handle(GetInstructorViewsPaginatedList request, CancellationToken cancellationToken)
        {

            Expression<Func<InstructorsView, InstructorViewResponse>> expression = i =>
            new InstructorViewResponse(i.InstructorId, i.Name
            , i.Email, i.Position, i.SupervisorName);

            var FilteredQuerable = _instructorService.GetFilteredInstructorsViewQuerable(request.OrderBy, request.Search ?? "");
            var paginatedList = await FilteredQuerable.Select(expression).ToPaginatedListAsync(request.InstructorsPageNumber, request.InstructorsPageSize);
            paginatedList.Meta = new { Count = paginatedList.Data.Count };
            return paginatedList;

        }

        #endregion

    }
}
