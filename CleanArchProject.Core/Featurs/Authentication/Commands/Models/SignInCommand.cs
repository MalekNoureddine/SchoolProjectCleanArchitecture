using CleanArchProject.Core.Bases;
using CleanArchProject.Data.Healper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Featurs.Authentication.Commands.Models
{
    public class SignInCommand : IRequest<Response<JwtAuthResult>>
    {
        public string userName { get; set; }
        public string password { get; set; }
    }
}
