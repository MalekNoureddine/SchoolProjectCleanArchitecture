using CleanArchProject.Core.Featurs.Departments.Commands.Models;
using CleanArchProject.Core.Featurs.Departments.Queries.Models;
using CleanArchProject.Core.Featurs.Subject.Commands.Modles;
using CleanArchProject.Core.Featurs.Subject.Queries.Models;
using CleanArchProject.Data.AppMetaData;
using CleanArchProject.Data.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProjectCleanArchitecture.Api.Base;

namespace SchoolProjectCleanArchitecture.Api.Controllers
{
    [ApiController]
    public class SubjectController : AppBaseController
    {
        public SubjectController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet(Router.SubjectRouting.All)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Subjects>> GetAllDepartments()
        {
            var response = await _mediator.Send(new GetSubjectsListQuery());
            return NewResult(response);
        }

        [HttpGet(Router.SubjectRouting.Paginated)]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Department>> GetPaginatedStudents([FromQuery] GetPaginatedSubjectListQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet]
        [Route(Router.SubjectRouting.GetById)]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Department>> GetDepartmentById([FromQuery] GetSubjectByIdQuery query)
        {
            var response = await _mediator.Send(query);
            return NewResult(response);
        }

        [HttpPost]
        [Route(Router.SubjectRouting.Create)]

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Department>> Create([FromBody] AddSubjectCommand departmentCommand)
        {
            var response = await _mediator.Send(departmentCommand);
            return NewResult(response);
        }


        [HttpPut]
        [Route(Router.SubjectRouting.Edit)]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Department>> Edit([FromBody] EditSubjectCommand departmentCommand)
        {
            var response = await _mediator.Send(departmentCommand);
            return NewResult(response);
        }

        [HttpDelete]
        [Route(Router.SubjectRouting.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Department>> Delete([FromRoute] int Id)
        {
            var response = await _mediator.Send(new DeleteSubjectCommand(Id));
            return NewResult(response);
        }
    }
}
