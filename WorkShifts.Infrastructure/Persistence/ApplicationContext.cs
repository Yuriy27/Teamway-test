using Microsoft.EntityFrameworkCore;
using WorkShifts.Domain.Entities;

namespace WorkShifts.Infrastructure.Persistence
{
    internal class ApplicationContext : DbContext
    {
        public DbSet<Worker> Workers { get; set; }
        public DbSet<WorkerShift> WorkerShifts { get; set; }

        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }
    }
}
