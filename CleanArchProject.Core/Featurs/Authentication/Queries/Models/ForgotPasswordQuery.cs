using CleanArchProject.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Featurs.Authentication.Queries.Models
{
    public class ForgotPasswordQuery: IRequest<Response<string>>
    {
        public string Email { get; set; }
    }
}
