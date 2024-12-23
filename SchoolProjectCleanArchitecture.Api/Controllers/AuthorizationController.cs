using CleanArchProject.Core.Featurs.Authorization.Commands.Models;
using CleanArchProject.Core.Featurs.Users.Commands.Models;
using CleanArchProject.Data.AppMetaData;
using CleanArchProject.Data.Entities.Identities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolProjectCleanArchitecture.Api.Base;

namespace SchoolProjectCleanArchitecture.Api.Controllers
{
    [ApiController]
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
        public async Task<IActionResult> Create([FromForm] AddRoleCommand roleCommand)
        {
            var response = await _mediator.Send(roleCommand);
            return NewResult(response);
        }
    }
}
