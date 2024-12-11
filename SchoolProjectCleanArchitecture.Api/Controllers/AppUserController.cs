using CleanArchProject.Core.Featurs.Departments.Queries.Models;
using CleanArchProject.Core.Featurs.Students.Commands.Models;
using CleanArchProject.Core.Featurs.Students.Queries.Models;
using CleanArchProject.Core.Featurs.Users.Commands.Models;
using CleanArchProject.Core.Featurs.Users.Queries.Models;
using CleanArchProject.Core.Featurs.Users.Queries.Responses;
using CleanArchProject.Data.AppMetaData;
using CleanArchProject.Data.Entities;
using CleanArchProject.Data.Entities.Identities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProjectCleanArchitecture.Api.Base;

namespace SchoolProjectCleanArchitecture.Api.Controllers
{
    [ApiController]
    public class AppUserController : AppBaseController
    {
        public AppUserController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [Route(Router.AppUserRouting.Create)]

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<User>> Create([FromBody] AddUserCommand userCommand)
        {
            var response = await _mediator.Send(userCommand);
            return NewResult(response);
        }

        [HttpGet]
        [Route(Router.AppUserRouting.GetById)]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<User>> GetUserById([FromRoute] int Id)
        {
            var response = await _mediator.Send(new GetUserByIdQuery(Id));
            return NewResult(response);
        }

        [HttpGet(Router.AppUserRouting.Paginated)]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<User>> GetPaginatedUsers([FromQuery] GetUsersListQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }


    }
}
