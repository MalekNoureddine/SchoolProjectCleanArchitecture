﻿using CleanArchProject.Core.Featurs.Departments.Commands.Models;
using CleanArchProject.Core.Featurs.Departments.Queries.Models;
using CleanArchProject.Core.Featurs.Instructors.Commands.Models;
using CleanArchProject.Core.Featurs.Instructors.Queries.Models;
using CleanArchProject.Data.AppMetaData;
using CleanArchProject.Data.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProjectCleanArchitecture.Api.Base;

namespace SchoolProjectCleanArchitecture.Api.Controllers
{
    [ApiController]
    public class InstructorsController : AppBaseController
    {
        public InstructorsController(IMediator mediator) : base(mediator){}

        [HttpGet(Router.InstructorRouting.All)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Instructor>> GetAllInstructors()
        {
            var response = await _mediator.Send(new GetInstructorsListQuery());
            return NewResult(response);
        }

        [HttpGet(Router.InstructorRouting.Paginated)]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Instructor>> GetPaginatedInstructors([FromQuery] GetPaginatedInstructorsListQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet]
        [Route(Router.InstructorRouting.GetById)]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Instructor>> GetInstructorById([FromQuery] GetInstructorByIdQuery query)
        {
            var response = await _mediator.Send(query);
            return NewResult(response);
        }

        [HttpPost]
        [Route(Router.InstructorRouting.Create)]

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Instructor>> Create([FromBody] AddInstructorCommand instructorCommand)
        {
            var response = await _mediator.Send(instructorCommand);
            return NewResult(response);
        }


        [HttpPut]
        [Route(Router.InstructorRouting.Edit)]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Instructor>> Edit([FromBody] EditInstructorCommand instructorCommand)
        {
            var response = await _mediator.Send(instructorCommand);
            return NewResult(response);
        }

        [HttpDelete]
        [Route(Router.InstructorRouting.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Instructor>> Delete([FromRoute] int Id)
        {
            var response = await _mediator.Send(new DeleteInstructorCommand(Id));
            return NewResult(response);
        }
    }
}
