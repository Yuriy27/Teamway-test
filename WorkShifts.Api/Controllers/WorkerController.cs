using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WorkShifts.Application.Models;
using WorkShifts.Application.Services;

namespace WorkShifts.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        private readonly IWorkerService _workerService;

        public WorkerController(IWorkerService workerService)
        {
            _workerService = workerService;
        }

        [HttpGet("{id}")]
        public async Task<WorkerDto> GetAsync(Guid id)
        {
            return await _workerService.GetAsync(id);
        }

        [HttpPost]
        public async Task<WorkerDto> CreateAsync([FromBody] WorkerDto worker)
        {
            return await _workerService.CreateAsync(worker);
        }

        [HttpPut]
        public async Task<WorkerDto> UpdateAsync([FromBody] WorkerDto worker)
        {
            return await _workerService.UpdateAsync(worker);
        }

        [HttpDelete("{id}")]
        public async Task<WorkerDto> DeleteAsync(Guid id)
        {
            return await _workerService.DeleteAsync(id);
        }
    }
}
