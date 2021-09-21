using System;
using System.Threading.Tasks;
using WorkShifts.Application.Models;

namespace WorkShifts.Application.Services
{
    public interface IWorkerService
    {
        Task<WorkerDto> GetAsync(Guid id);
        Task<WorkerDto> CreateAsync(WorkerDto workerDto);
        Task<WorkerDto> UpdateAsync(WorkerDto workerDto);
        Task<WorkerDto> DeleteAsync(Guid id);
    }
}
