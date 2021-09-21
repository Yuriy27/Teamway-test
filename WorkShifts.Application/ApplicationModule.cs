using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using WorkShifts.Application.Services;

namespace WorkShifts.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient<IWorkerService, WorkerService>();
            services.AddTransient<IWorkerShiftService, WorkerShiftService>();

            return services;
        }
    }
}
