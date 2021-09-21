using System;
using System.Threading.Tasks;
using WorkShifts.Application.Repositories;
using WorkShifts.Domain.Entities;
using WorkShifts.Domain.Exceptions;
using WorkShifts.Infrastructure.Persistence;

namespace WorkShifts.Infrastructure.Repositories
{
    internal class WorkerRepository : IWorkerRepository
    {
        private readonly ApplicationContext _context;

        public WorkerRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Worker> CreateAsync(Worker worker)
        {
            await _context.Workers.AddAsync(worker);
            await _context.SaveChangesAsync();

            return worker;
        }

        public async Task<Worker> DeleteAsync(Guid id)
        {
            var worker = await _context.Workers.FindAsync(id);
            if (worker == null)
            {
                throw new WorkerNotFoundException(id);
            }

            _context.Workers.Remove(worker);
            await _context.SaveChangesAsync();

            return worker;
        }

        public async Task<Worker> GetAsync(Guid id)
        {
            return await _context.Workers.FindAsync(id);
        }

        public async Task<Worker> UpdateAsync(Worker worker)
        {
            _context.Workers.Update(worker);
            await _context.SaveChangesAsync();

            return worker;
        }
    }
}
