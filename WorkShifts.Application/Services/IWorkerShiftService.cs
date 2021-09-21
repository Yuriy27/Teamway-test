using System;
using System.Threading.Tasks;
using WorkShifts.Application.Models;

namespace WorkShifts.Application.Services
{
    public interface IWorkerShiftService
    {
        Task<WorkerShiftDto> GetAsync(Guid id);
        Task<WorkerShiftDto> CreateAsync(WorkerShiftDto workerShiftDto);
        Task<WorkerShiftDto> DeleteAsync(Guid id);
    }
}
