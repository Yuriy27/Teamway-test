using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WorkShifts.Application.Models;
using WorkShifts.Application.Services;

namespace WorkShifts.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerShiftController : ControllerBase
    {
        private readonly IWorkerShiftService _workerShiftService;

        public WorkerShiftController(IWorkerShiftService workerShiftService)
        {
            _workerShiftService = workerShiftService;
        }

        [HttpGet("{id}")]
        public async Task<WorkerShiftDto> GetAsync(Guid id)
        {
            return await _workerShiftService.GetAsync(id);
        }

        [HttpPost]
        public async Task<WorkerShiftDto> CreateAsync([FromBody] WorkerShiftDto worker)
        {
            return await _workerShiftService.CreateAsync(worker);
        }

        [HttpDelete("{id}")]
        public async Task<WorkerShiftDto> DeleteAsync(Guid id)
        {
            return await _workerShiftService.DeleteAsync(id);
        }
    }
}
