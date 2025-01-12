using CleanArchProject.Data.Entities.Views;
using CleanArchProject.Infrastracture.InfrastractureBases__generics_;
using CleanArchProject.Infrastracture.Interfaces;
using CleanArchProject.Infrastracture.Interfaces.Views;
using CleanArchProject.Infrastracture.Repositories;
using CleanArchProject.Infrastracture.Repositories.Views;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchProject.Infrastracture
{
    public static class ModuleInfrastractureDependencies
    {
        public static IServiceCollection AddInfrastractureDependencies(this IServiceCollection services)
        {
            services.AddTransient<IStudentRepository, StudentRepository>();
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<IInstructorRepository, InstructorRepository>();
            services.AddTransient<ISubjectRepository, SubjectRepository>();

            services.AddTransient<ISubjectInstructorRepository, Subject_InstructorRepository>();
            services.AddTransient<IDepartmentSubjectRepository, Department_SubjectRepository>();
            services.AddTransient<IDepartmentInstructorRepository, Department_InstructorRepository>();
            services.AddTransient<IStudentSubjectRepository, Student_SubjectRepository>();

            services.AddTransient<IUserRefreshTokenRepository, UserRefreshTokenRepository>();
            services.AddTransient<IResetPasswordRepository, ResetPasswordRepository>();

            services.AddTransient<IViewRepository<InstructorsView>, InstructorViewRepository>();

            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            return services;
        }
    }
}
