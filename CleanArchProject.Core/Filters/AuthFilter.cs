using CleanArchProject.Data.Entities.Identities;
using CleanArchProject.Service.CurrentUserServices.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Filters
{
    public class AuthFilter : ActionFilterAttribute //can use IAsyncActionFilter without overriding
    {
        #region Fields
        private readonly ICurrentUserService _currentUserService;
        private readonly UserManager<User> _userManager;
        #endregion

        #region Constructor
        public AuthFilter(ICurrentUserService currentUserService, UserManager<User> userManager)
        {
            _currentUserService = currentUserService;
            _userManager = userManager;
        }
        #endregion

        #region Actions
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                var rols = await _currentUserService.GetCurrentUserRolesAsync();
                if(rols.Any(x => x == "User"))
                {
                    await next();
                }
                else
                {
                    context.Result = new ObjectResult("Forbidden")
                    {
                        StatusCode = StatusCodes.Status403Forbidden
                    };
                }
            }
        }
        #endregion
    }
}
