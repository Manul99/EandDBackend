using EandDBackend.Interfaces.Repositories;
using EandDBackend.Interfaces.Services;
using EandDBackend.Reporsitory;
using EandDBackend.Service;

namespace EandDBackend.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddProjectServies(this IServiceCollection services)
        {
            
            //Reporsitories
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            //Services
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            return services;
        }
    }
}
