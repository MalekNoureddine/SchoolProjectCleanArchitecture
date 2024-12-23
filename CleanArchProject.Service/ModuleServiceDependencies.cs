using CleanArchProject.Infrastracture.Interfaces;
using CleanArchProject.Infrastracture.Repositories;
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
            return services;
        }
    }
}
