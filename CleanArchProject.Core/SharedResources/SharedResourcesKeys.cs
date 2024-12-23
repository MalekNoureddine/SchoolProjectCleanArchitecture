using System;
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
        public const string UpdateFailed = "UpdateFailed";
        public const string Updated = "Updated";
        public const string FaildToAdd = "FaildToAdd";

        public const string NotEmpty = "NotEmpty";
        public const string NotNull = "NotNull";
        public const string NotValid = "NotValid";
        public const string GreaterThan0 = "GreaterThan0";

        public const string Student = "Student";
        public const string Name = "Name";
        public const string Phone = "Phone";
        public const string Department = "Department";
        public const string Id = "Id";
        public const string StudentId = Student + Id;
        public const string DepartmentId = Department + Id;
        public const string DoseNotExists = "DoseNotExists";
        public const string IsAlreadyExits = "IsAlreadyExits";
        public const string MaxLengthis100 = "MaxLengthis100";
        public const string PasswordNotEqualConfirmPass = "PasswordNotEqualConfirmPass";
        public const string UserName = "UserName";
        public const string password = "password";
        public const string UserNameIsNotExist = "UserNameIsNotExist";
        public const string PasswordNotCorrect = "PasswordNotCorrect";
        public const string EmailNotConfirmed = "EmailNotConfirmed";
        public const string AlgorithmIsWrong = "AlgorithmIsWrong";
        public const string TokenIsNotExpired = "TokenIsNotExpired";
        public const string TokenIsExpired = "TokenIsExpired";
        public const string RefreshTokenIsNotFound = "RefreshTokenIsNotFound";
        public const string RefreshTokenIsExpired = "RefreshTokenIsExpired";
        public const string RoleAlreadyExists = "RoleAlreadyExists";
    }
    
}
