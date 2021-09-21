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
    internal class WorkerShiftService : IWorkerShiftService
    {
        private readonly IMapper _mapper;
        private readonly IWorkerRepository _workerRepository;
        private readonly IWorkerShiftRepository _workerShiftRepository;

        public WorkerShiftService(
            IMapper mapper,
            IWorkerRepository workerRepository,
            IWorkerShiftRepository workerShiftRepository)
        {
            _mapper = mapper;
            _workerRepository = workerRepository;
            _workerShiftRepository = workerShiftRepository;
        }

        public async Task<WorkerShiftDto> CreateAsync(WorkerShiftDto workerShiftDto)
        {
            var workerShift = _mapper.Map<WorkerShift>(workerShiftDto);
            var assignedWorker = await _workerRepository.GetAsync(workerShift.WorkerId);
            if (assignedWorker == null)
            {
                throw new WorkerNotFoundException(workerShift.WorkerId);
            }

            var existingShifts = await _workerShiftRepository.GetByWorkerIdAsync(workerShift.WorkerId);
            if (existingShifts.Any(x => x.Date.Date == workerShift.Date.Date))
            {
                throw new WorkerHasAlreadyBeenAssignedException(workerShift.WorkerId, workerShift.Date);
            }

            workerShift.Id = Guid.NewGuid();
            var createdShift = await _workerShiftRepository.CreateAsync(workerShift);

            return _mapper.Map<WorkerShiftDto>(createdShift);
        }

        public async Task<WorkerShiftDto> DeleteAsync(Guid id)
        {
            var worker = await _workerShiftRepository.DeleteAsync(id);

            return _mapper.Map<WorkerShiftDto>(worker);
        }

        public async Task<WorkerShiftDto> GetAsync(Guid id)
        {
            var workerShift = await _workerShiftRepository.GetAsync(id);
            if (workerShift == null)
            {
                throw new WorkerShiftNotFoundException(id);
            }

            return _mapper.Map<WorkerShiftDto>(workerShift);
        }
    }
}
