using CleanArchProject.Data.Entities.Identities;
using CleanArchProject.Infrastracture.Data;
using CleanArchProject.Service.Interfaces;
using MailKit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Service.ServicesImplementation
{
    public class UserService : IUserService
    {
        #region Fields
        private readonly AppDbContext _dbcontext;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailService _emailsService;
        private readonly IUrlHelper _urlHelper;
        #endregion

        #region Constructor
        public UserService(AppDbContext dbcontext, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor, IEmailService emailsService, IUrlHelper urlHelper)
        {
            _dbcontext = dbcontext;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _emailsService = emailsService;
            _urlHelper = urlHelper;
        }

        #endregion

        #region HandleFunctions
        public async Task<string> AddUserAsync(User user, string password)
        {
            var transact = await _dbcontext.Database.BeginTransactionAsync();
            try
            {
                var createResult = await _userManager.CreateAsync(user, password);
                //Failed
                if (!createResult.Succeeded)
                {
                    await transact.RollbackAsync();
                    return string.Join(",", createResult.Errors.Select(x => x.Description).ToList());
                }
                var AddToRoleResult = await _userManager.AddToRoleAsync(user, "User");
                if (!AddToRoleResult.Succeeded)
                {
                    await transact.RollbackAsync();
                    return string.Join(",", AddToRoleResult.Errors.Select(x => x.Description).ToList());
                }
                //Send Confirm Email
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var resquestAccessor = _httpContextAccessor.HttpContext.Request;
                var returnUrl = resquestAccessor.Scheme + "://" + resquestAccessor.Host + _urlHelper.Action("ConfirmEmail", "Authentication", new { userId = user.Id, code = code });
                var message = $"To Confirm Email Click Link: <a href='{returnUrl}'>Link Of Confirmation</a>";

                await _emailsService.SendEmail(user.Email, message, "ConFirm Email");

                await transact.CommitAsync();
                return "Success";
            }
            catch (Exception)
            {

                await transact.RollbackAsync();
                return "Failed";
            }
        }
        #endregion
    }
}
