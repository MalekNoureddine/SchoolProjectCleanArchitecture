﻿using CleanArchProject.Core.Bases;
using CleanArchProject.Core.Featurs.Departments.Queries.Response;
using CleanArchProject.Data.Enums;
using MediatR;
using SchoolProject.Core.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Featurs.Departments.Queries.Models
{
    public class GetADepartmentQuery : IRequest<Response<GetDepartmentByIdResponse>>
    {
        public int Id { get; set; }
        public int StudentsPageNumber { get; set; }
        public int StudentsPageSize { get; set; }


    }
}
