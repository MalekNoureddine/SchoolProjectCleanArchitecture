using AutoMapper;
using CleanArchProject.Core.Bases;
using CleanArchProject.Core.Featurs.Students.Queries.Models;
using CleanArchProject.Core.Featurs.Students.Queries.Response;
using CleanArchProject.Core.SharedResources;
using CleanArchProject.Data.Entities;
using CleanArchProject.Service.Interfaces;
using CleanArchProject.Service.ServicesImplementation;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Wrappers;
using System.Linq.Expressions;

namespace CleanArchProject.Core.Featurs.Students.Queries.Handler
{

    public class StudentQueryHandler :ResponseHandler,
        IRequestHandler<GetAllStudentsQuery, Response< List<GetAllStudentsResponse>>>,
        IRequestHandler<GetStudentByIdQuery, Response<GetAStudentResponse>>,
        IRequestHandler<GetStudentsListPaginatedQuery, PaginatedResult<GetStudentsListPagiatedResponse>>
    {
        #region Fields
        private readonly IStudentService _student;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources.SharedResources> _stringLocalizer;
        #endregion

        #region Constructors
        public StudentQueryHandler(IStudentService student, IMapper mapper,
            IStringLocalizer<SharedResources.SharedResources> stringLocalizer): base(stringLocalizer)
        {
            _student = student;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }
        #endregion

        #region Handle Functions
        public async Task<Response<List<GetAllStudentsResponse>>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
        {
            var students =  await _student.GetAllStudentsAsync();
            var studentsmapper = _mapper.Map<List<GetAllStudentsResponse>>(students);
            var result = Success(studentsmapper);
            result.Meta = new {
                Count = studentsmapper.Count,
                Last_Student_Created = studentsmapper.LastOrDefault()
            };// you can add any thing you want
            return result;
        }

        public async Task<Response<GetAStudentResponse>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var student = await _student.GetStudentByIdAsync(request.Id);
            if (student == null) return NotFound<GetAStudentResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);
            var studentmapper = _mapper.Map<GetAStudentResponse>(student);
            return Success(studentmapper);
        }

        public async Task<PaginatedResult<GetStudentsListPagiatedResponse>> Handle(GetStudentsListPaginatedQuery request, CancellationToken cancellationToken)
        {
            var FilterQuery = _student.GetFilteredStudentsQuerable(request.OrderBy, request.Search);
            var paginatedList = await _mapper.ProjectTo<GetStudentsListPagiatedResponse>(FilterQuery).ToPaginatedListAsync(request.StudentPageNumber, request.StudentPageSize);

            paginatedList.Meta = new {Count = paginatedList.Data.Count};
            return paginatedList;
        }
        #endregion

    }
}
