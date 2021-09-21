using System;
using System.Threading.Tasks;
using WorkShifts.Domain.Entities;

namespace WorkShifts.Application.Repositories
{
    public interface IWorkerRepository
    {
        Task<Worker> GetAsync(Guid id);
        Task<Worker> CreateAsync(Worker worker);
        Task<Worker> UpdateAsync(Worker worker);
        Task<Worker> DeleteAsync(Guid id);
    }
}
