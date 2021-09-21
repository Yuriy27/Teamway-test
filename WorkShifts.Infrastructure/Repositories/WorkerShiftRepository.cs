using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkShifts.Application.Repositories;
using WorkShifts.Domain.Entities;
using WorkShifts.Domain.Exceptions;
using WorkShifts.Infrastructure.Persistence;

namespace WorkShifts.Infrastructure.Repositories
{
    internal class WorkerShiftRepository : IWorkerShiftRepository
    {
        private readonly ApplicationContext _context;

        public WorkerShiftRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<WorkerShift> CreateAsync(WorkerShift worker)
        {
            await _context.WorkerShifts.AddAsync(worker);
            await _context.SaveChangesAsync();

            return worker;
        }

        public async Task<WorkerShift> DeleteAsync(Guid id)
        {
            var workerShift = await _context.WorkerShifts.FindAsync(id);
            if (workerShift == null)
            {
                throw new WorkerShiftNotFoundException(id);
            }

            _context.WorkerShifts.Remove(workerShift);
            await _context.SaveChangesAsync();

            return workerShift;
        }

        public async Task<WorkerShift> GetAsync(Guid id)
        {
            return await _context.WorkerShifts
                .Include(x => x.Worker)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<WorkerShift>> GetByWorkerIdAsync(Guid id)
        {
            return await _context.WorkerShifts
                .Where(x => x.WorkerId == id)
                .ToListAsync();
        }
    }
}
