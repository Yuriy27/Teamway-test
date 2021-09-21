using AutoMapper;
using System;
using System.Linq;
using System.Threading.Tasks;
using WorkShifts.Application.Models;
using WorkShifts.Application.Repositories;
using WorkShifts.Domain.Entities;
using WorkShifts.Domain.Exceptions;

namespace WorkShifts.Application.Services
{
    internal class WorkerService : IWorkerService
    {
        private readonly IMapper _mapper;
        private readonly IWorkerRepository _workerRepository;
        private readonly IWorkerShiftRepository _workerShiftRepository;

        public WorkerService(
            IMapper mapper,
            IWorkerRepository workerRepository,
            IWorkerShiftRepository workerShiftRepository)
        {
            _mapper = mapper;
            _workerRepository = workerRepository;
            _workerShiftRepository = workerShiftRepository;
        }

        public async Task<WorkerDto> CreateAsync(WorkerDto workerDto)
        {
            var worker = _mapper.Map<Worker>(workerDto);
            worker.Id = Guid.NewGuid();
            var createdWorker = await _workerRepository.CreateAsync(worker);

            return _mapper.Map<WorkerDto>(createdWorker);
        }

        public async Task<WorkerDto> DeleteAsync(Guid id)
        {
            var shifts = await _workerShiftRepository.GetByWorkerIdAsync(id);
            if (shifts.Any())
            {
                throw new WorkerHasShiftsException(id);
            }

            var worker = await _workerRepository.DeleteAsync(id);

            return _mapper.Map<WorkerDto>(worker);
        }

        public async Task<WorkerDto> GetAsync(Guid id)
        {
            var worker = await _workerRepository.GetAsync(id);
            if (worker == null)
            {
                throw new WorkerNotFoundException(id);
            }

            return _mapper.Map<WorkerDto>(worker);
        }

        public async Task<WorkerDto> UpdateAsync(WorkerDto workerDto)
        {
            var worker = await _workerRepository.GetAsync(workerDto.Id);
            if (worker == null)
            {
                throw new WorkerNotFoundException(workerDto.Id);
            }
            _mapper.Map(workerDto, worker);
            var updatedWorker = await _workerRepository.UpdateAsync(worker);

            return _mapper.Map<WorkerDto>(updatedWorker);
        }
    }
}
