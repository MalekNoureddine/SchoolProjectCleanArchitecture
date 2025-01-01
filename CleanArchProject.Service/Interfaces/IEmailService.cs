using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Service.Interfaces
{
    public interface IEmailService
    {
        public Task<string> SendEmail(string email, string message, string? reason);
    }
}
