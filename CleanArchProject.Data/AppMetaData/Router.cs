using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Data.AppMetaData
{
    public static class Router
    {
        public const string root = "Api";
        public const string version = "V1";
        public const string rule = root + "/" + version + "/";
        public static class StudentRouteing
        {
            public const string Prefix = "Students/";
            public const string All = Prefix + "All";
            public const string Paginated = Prefix + "Paginated";
            public const string GetById = Prefix + "{Id}";
            public const string Create = Prefix + "Create";
            public const string Edit = Prefix + "Edit";
            public const string Delete = Prefix + "Delete/" + "{Id}";
            
        }
        public static class DepartmentRouting
        {
            public const string Prefix = "Departments/";
            public const string All = Prefix + "All";
            public const string Paginated = Prefix + "Paginated";
            public const string GetById = Prefix + "Id";
            public const string Create = Prefix + "Create";
            public const string Edit = Prefix + "Edit";
            public const string Delete = Prefix + "Delete/" + "{Id}";

        }

        public static class InstructorRouting
        {
            public const string Prefix = "Instructors/";
            public const string All = Prefix + "All";
            public const string Paginated = Prefix + "Paginated";
            public const string GetById = Prefix + "Id";
            public const string Create = Prefix + "Create";
            public const string Edit = Prefix + "Edit";
            public const string Delete = Prefix + "Delete/" + "{Id}";

        }

        public static class SubjectRouting
        {
            public const string Prefix = "Subjects/";
            public const string All = Prefix + "All";
            public const string Paginated = Prefix + "Paginated";
            public const string GetById = Prefix + "Id";
            public const string Create = Prefix + "Create";
            public const string Edit = Prefix + "Edit";
            public const string Delete = Prefix + "Delete/" + "{Id}";

        }
        public static class AppUserRouting
        {
            public const string Prefix = "Users/";
            public const string All = Prefix + "All";
            public const string Paginated = Prefix + "Paginated";
            public const string GetById = Prefix + "{Id}";
            public const string Create = Prefix + "Create";
            public const string Edit = Prefix + "Edit";
            public const string ChangePassword = Prefix + "ChangePassword";
            public const string Delete = Prefix + "Delete/" + "{Id}";

        }
        public static class AuthenticationRouting
        {
            public const string Prefix = "Authentication/";
            public const string SignIn = Prefix + "SignIn";
            public const string RefreshToken = Prefix + "Refresh-Token";
            public const string ValidateToken = Prefix + "Validate-Token";
        }
        public static class AuthorizationRouting
        {
            public const string Prefix = "AuthorizationRouting";
            public const string Roles = Prefix + "/Roles";
            public const string Claims = Prefix + "/Claims";
            public const string Create = Roles + "/Create";
            public const string Edit = Roles + "/Edit";
            public const string Delete = Roles + "/Delete/{Id}";
            public const string RolesList = Roles + "/Role-List";
            public const string GetRoleById = Roles + "/Role-By-Id/{Id}";
            public const string GetRoleByName = Roles + "/Role-By-Name/{Name}";
            public const string ManageUserRoles = Roles + "/Manage-User-Roles/{userId}";
            public const string UpdateUserRoles = Roles + "/Update-User-Roles";
            public const string ManageUserClaims = Claims + "/Manage-User-Claims/{userId}";
            public const string UpdateUserClaims = Claims + "/Update-User-Claims";

        }

        public static class EmailsRoute
        {
            public const string Prefix = "Email/";
            public const string SendEmail = Prefix + "SendEmail";
        }
    }


}
