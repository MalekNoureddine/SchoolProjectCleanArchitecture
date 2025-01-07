using CleanArchProject.Core.Featurs.Authentication.Commands.Models;
using CleanArchProject.Core.Featurs.Authentication.Queries.Models;
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

        public async Task<IActionResult> SignIn([FromForm] SignInCommand singInCommand)
        {
            var response = await _mediator.Send(singInCommand);
            return NewResult(response);
        }

        [HttpPost]
        [Route(Router.AuthenticationRouting.RefreshToken)]

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public async Task<IActionResult> RefreshToken([FromForm] RefreshTokenCommand refreshTokenCommand)
        {
            var response = await _mediator.Send(refreshTokenCommand);
            return NewResult(response);
        }

        [HttpGet]
        [Route(Router.AuthenticationRouting.ValidateToken)]

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public async Task<IActionResult> ValidateToken([FromQuery] AuthorizeUserQuery authorizeUserQuery)
        {
            var response = await _mediator.Send(authorizeUserQuery);
            return NewResult(response);
        }

        [HttpGet]
        [Route(Router.AuthenticationRouting.ConfirmEmail)]

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailQuery confirmEmailQuery)
        {
            var response = await _mediator.Send(confirmEmailQuery);
            return NewResult(response);
        }

        [HttpGet]
        [Route(Router.AuthenticationRouting.ForgotPassword)]

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public async Task<IActionResult> ForgotPassword([FromRoute] string Email)
        {
            var response = await _mediator.Send(new ForgotPasswordQuery { Email = Email});
            return NewResult(response);
        }

        [HttpGet]
        [Route(Router.AuthenticationRouting.ResetPassword)]

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public async Task<IActionResult> ResetPassword([FromQuery] ResetPasswordCommand resetPasswordCommand)
        {
            var response = await _mediator.Send(resetPasswordCommand);
            return NewResult(response);
        }
    }
}
