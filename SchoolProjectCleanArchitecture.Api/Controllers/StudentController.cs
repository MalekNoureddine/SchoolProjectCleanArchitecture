using CleanArchProject.Core.Featurs.Departments.Commands.Models;
using CleanArchProject.Core.Featurs.Students.Commands.Models;
using CleanArchProject.Core.Featurs.Students.Queries.Models;
using CleanArchProject.Data.AppMetaData;
using CleanArchProject.Data.Entities;
using MediatR;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProjectCleanArchitecture.Api.Base;
using Router = CleanArchProject.Data.AppMetaData.Router;

namespace SchoolProjectCleanArchitecture.Api.Controllers
{
    [ApiController]
    public class StudentController : AppBaseController
    {
        public StudentController(IMediator mediator)
            :base(mediator){ }


        [HttpGet(Router.StudentRouteing.All)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Student>> GetAllStudents()
        {
            var response =  await _mediator.Send(new GetAllStudentsQuery());
            return Ok(response);
        }


        [HttpGet(Router.StudentRouteing.Paginated)]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Student>> GetPaginatedStudents([FromQuery] GetStudentsListPaginatedQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet]
        [Route(Router.StudentRouteing.GetById)]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Student>> GetAllStudent([FromRoute]int Id)
        {
            var response = await _mediator.Send(new GetStudentByIdQuery(Id));
            return NewResult(response);
        }

        [HttpPost]
        [Route(Router.StudentRouteing.Create)]

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Student>> Create([FromBody] AddStudentCommand studentCommand)
        {
            var response = await _mediator.Send(studentCommand);
            return NewResult(response);
        }


        [HttpPut]
        [Route(Router.StudentRouteing.Edit)]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Student>> Edit([FromBody] EditStudentCommand studentCommand)
        {
            var response = await _mediator.Send(studentCommand);
            return NewResult(response);
        }

        [HttpDelete]
        [Route(Router.StudentRouteing.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Student>> Delete([FromRoute] int Id)
        {
            var response = await _mediator.Send(new DeleteStudentCommand(Id));
            return NewResult(response);
        }

    }
}
