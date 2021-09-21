using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WorkShifts.Application.Repositories;
using WorkShifts.Infrastructure.Persistence;
using WorkShifts.Infrastructure.Repositories;

namespace WorkShifts.Infrastructure
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IWorkerRepository, WorkerRepository>();
            services.AddTransient<IWorkerShiftRepository, WorkerShiftRepository>();
            services.AddDbContext<ApplicationContext>(opts => opts.UseInMemoryDatabase("ApplicationDb"));

            return services;
        }
    }
}
