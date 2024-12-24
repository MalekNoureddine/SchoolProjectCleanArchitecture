using CleanArchProject.Core.Featurs.Authorization.Commands.Models;
using CleanArchProject.Core.Featurs.Authorization.Queries.Models;
using CleanArchProject.Core.Featurs.Users.Commands.Models;
using CleanArchProject.Data.AppMetaData;
using CleanArchProject.Data.Entities.Identities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolProjectCleanArchitecture.Api.Base;

namespace SchoolProjectCleanArchitecture.Api.Controllers
{
    [ApiController]
    [Authorize(Roles = "User,Admin")]
    public class AuthorizationController : AppBaseController
    {
        public AuthorizationController(IMediator mediator) : base(mediator)
        {
        }
        [HttpPost]
        [Route(Router.AuthorizationRouting.Create)]

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Create([FromForm] AddRoleCommand roleCommand)
        {
            var response = await _mediator.Send(roleCommand);
            return NewResult(response);
        }

        [HttpPut]
        [Route(Router.AuthorizationRouting.Edit)]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Edit([FromForm] EditRoleCommand roleCommand)
        {
            var response = await _mediator.Send(roleCommand);
            return NewResult(response);
        }

        [HttpDelete]
        [Route(Router.AuthorizationRouting.Delete)]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Delete([FromRoute] string RoleName)
        {
            var response = await _mediator.Send(new DeleteRoleCommand(RoleName));
            return NewResult(response);
        }

        [HttpGet]
        [Route(Router.AuthorizationRouting.All)]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> All()
        {
            var response = await _mediator.Send(new GetRolesListQuery());
            return NewResult(response);
        }

        [HttpGet]
        [Route(Router.AuthorizationRouting.GetById)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetById([FromRoute] int Id)
        {
            var response = await _mediator.Send(new GetRoleByIdQuery() { Id = Id});
            return NewResult(response);
        }

        [HttpGet]
        [Route(Router.AuthorizationRouting.GetByName)]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetByName([FromRoute] string Name)
        {
            var response = await _mediator.Send(new GetRoleByNameQuery() {Name = Name});
            return NewResult(response);
        }
    }
}
