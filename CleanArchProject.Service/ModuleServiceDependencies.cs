﻿using CleanArchProject.Infrastracture.Interfaces;
using CleanArchProject.Infrastracture.Repositories;
using CleanArchProject.Service.CurrentUserServices.Interfaces;
using CleanArchProject.Service.CurrentUserServices.Implementations;

using CleanArchProject.Service.Interfaces;
using CleanArchProject.Service.ServicesImplementation;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchProject.Service
{
    public static class ModuleServiceDependencies
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
        {
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<IDepartmentService, DepartmentService>();
            services.AddTransient<ISubjectService, SubjectService>();
            services.AddTransient<IInstructorService, InstructorService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IAuthorizationService, AuthorizationService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICurrentUserService, CurrentUserService>();
            return services;
        }
    }
}
