using CleanArchProject.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Featurs.Users.Commands.Models
{
    public class ChangeUserPasswordCommand : IRequest<Response<string>>
    {
        public ChangeUserPasswordCommand(int id, string currentPassword, string newPassword, string confirmPassword)
        {
            Id = id;
            CurrentPassword = currentPassword;
            NewPassword = newPassword;
            ConfirmPassword = confirmPassword;
        }

        public int Id { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
