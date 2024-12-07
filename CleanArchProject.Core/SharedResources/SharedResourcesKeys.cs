﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.SharedResources
{
    public static class SharedResourcesKeys
    {
        public const string Required = "Required";
        public const string NotFound = "NotFound";
        public const string BadRequest = "BadRequest";
        public const string Deleted = "Deleted";
        public const string Created = "Created";
        public const string Conflict = "Conflict";
        public const string UnAuthorized = "UnAuthorized";
        public const string UnprocessableEntity = "UnprocessableEntity";
        public const string Success = "Success";

        public const string NotEmpty = "NotEmpty";
        public const string NotNull = "NotNull";
        public const string GreaterThan0 = "GreaterThan0";

        public const string Student = "Student";
        public const string Name = "Name";
        public const string Phone = "Phone";
        public const string Department = "Department";
        public const string Id = "Id";
        public const string StudentId = Student + Id;
        public const string DepartmentId = Department + Id;
    }
    
}
