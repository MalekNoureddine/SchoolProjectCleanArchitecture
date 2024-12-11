using CleanArchProject.Core.Featurs.Users.Queries.Responses;
using CleanArchProject.Data.Enums;
using MediatR;
using SchoolProject.Core.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Featurs.Users.Queries.Models
{
    public class GetUsersListQuery : IRequest<PaginatedResult<GetUsersListResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

    }
}
