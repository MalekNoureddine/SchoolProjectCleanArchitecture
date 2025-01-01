using CleanArchProject.Core.Bases;
using CleanArchProject.Core.Featurs.Email.Commands.Modles;
using CleanArchProject.Core.SharedResources;
using CleanArchProject.Service.Interfaces;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Featurs.Email.Commands.Handler
{
    public class EmailCommandHandler : ResponseHandler,
        IRequestHandler<SendEmailCommand, Response<string>>
    {
        private readonly IStringLocalizer<SharedResources.SharedResources> _stringLocalizer;
        private readonly IEmailService _emailsService;

        public EmailCommandHandler(IStringLocalizer<SharedResources.SharedResources> stringLocalizer, IEmailService emailsService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _emailsService = emailsService;
        }

        public async Task<Response<string>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            var response = await _emailsService.SendEmail(request.Email, request.Message, null);
            if (response == "Success")
                return Success<string>("");
            return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.SendEmailFailed]);
        }
    }
}
