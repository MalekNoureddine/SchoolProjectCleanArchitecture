using CleanArchProject.Data.Healper;
using CleanArchProject.Service.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Service.ServicesImplementation
{
    public class EmailService : IEmailService
    {
        #region Fields
        private readonly EmailSettings _emailSettings;
        #endregion
        #region constructor
        public EmailService(EmailSettings emailSettings)
        {
            _emailSettings = emailSettings;
        }
        #endregion

        #region HandleFunctions
        public async Task<string> SendEmail(string email, string message, string? reason)
        {
            try
            {
                //sending the Message of passwordResetLink
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_emailSettings.Host, _emailSettings.Port, true);
                    client.Authenticate(_emailSettings.FromEmail, _emailSettings.Password);
                    var bodybuilder = new BodyBuilder
                    {
                        HtmlBody = $"{message}",
                        TextBody = "wellcome",
                    };
                    var Message = new MimeMessage
                    {
                        Body = bodybuilder.ToMessageBody()
                    };
                    Message.From.Add(new MailboxAddress("Future Team", _emailSettings.FromEmail));
                    Message.To.Add(new MailboxAddress("testing", email));
                    Message.Subject = reason == null ? "No Submitted" : reason;
                    await client.SendAsync(Message);
                    await client.DisconnectAsync(true);
                }
                //end of sending email
                return "Success";
            }
            catch (Exception ex)
            {
                return "Failed";
            }
        }
        #endregion
    }
}
