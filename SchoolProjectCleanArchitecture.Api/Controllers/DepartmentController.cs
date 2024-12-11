using CleanArchProject.Core.Featurs.Departments.Commands.Models;
using CleanArchProject.Core.Featurs.Departments.Queries.Models;
using CleanArchProject.Core.Featurs.Students.Commands.Models;
using CleanArchProject.Core.Featurs.Students.Queries.Models;
using CleanArchProject.Data.AppMetaData;
using CleanArchProject.Data.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProjectCleanArchitecture.Api.Base;

namespace SchoolProjectCleanArchitecture.Api.Controllers
{
    [ApiController]
    public class DepartmentController : AppBaseController
    {
        public DepartmentController(IMediator mediator)
            : base(mediator) { }


        [HttpGet(Router.DepartmentRouting.All)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Department>> GetAllDepartments()
        {
            var response = await _mediator.Send(new GetAllDepartmentsQuery());
            return NewResult(response);
        }

        [HttpGet(Router.DepartmentRouting.Paginated)]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Department>> GetPaginatedDepartmentss([FromQuery] GetPaginatedDepartmentsListQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet]
        [Route(Router.DepartmentRouting.GetById)]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Department>> GetDepartmentById([FromQuery] GetADepartmentQuery query)
        {
            var response = await _mediator.Send(query);
            return NewResult(response);
        }

        [HttpPost]
        [Route(Router.DepartmentRouting.Create)]

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Department>> Create([FromBody] AddDepartmentCommand departmentCommand)
        {
            var response = await _mediator.Send(departmentCommand);
            return NewResult(response);
        }


        [HttpPut]
        [Route(Router.DepartmentRouting.Edit)]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Department>> Edit([FromBody] EditDepartmentCommand departmentCommand)
        {
            var response = await _mediator.Send(departmentCommand);
            return NewResult(response);
        }

        [HttpDelete]
        [Route(Router.DepartmentRouting.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Department>> Delete([FromRoute] int Id)
        {
            var response = await _mediator.Send(new DeleteDepartmentCommand(Id));
            return NewResult(response);
        }

    }
}
