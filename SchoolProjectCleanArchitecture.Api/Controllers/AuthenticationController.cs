using CleanArchProject.Core.Featurs.Authentication.Commands.Models;
using CleanArchProject.Core.Featurs.Users.Commands.Models;
using CleanArchProject.Data.AppMetaData;
using CleanArchProject.Data.Entities.Identities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProjectCleanArchitecture.Api.Base;

namespace SchoolProjectCleanArchitecture.Api.Controllers
{
    [ApiController]
    public class AuthenticationController : AppBaseController
    {
        public AuthenticationController(IMediator mediator) : base(mediator)
        {
        }
        [HttpPost]
        [Route(Router.AuthenticationRouting.SignIn)]

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public async Task<IActionResult> Create([FromForm] SignInCommand singInCommand)
        {
            var response = await _mediator.Send(singInCommand);
            return NewResult(response);
        }
    }
}
