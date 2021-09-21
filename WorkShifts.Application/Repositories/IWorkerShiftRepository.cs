using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkShifts.Domain.Entities;

namespace WorkShifts.Application.Repositories
{
    public interface IWorkerShiftRepository
    {
        Task<WorkerShift> GetAsync(Guid id);
        Task<IEnumerable<WorkerShift>> GetByWorkerIdAsync(Guid id);
        Task<WorkerShift> CreateAsync(WorkerShift worker);
        Task<WorkerShift> DeleteAsync(Guid id);
    }
}
