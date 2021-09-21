using AutoFixture.Xunit2;
using AutoMapper;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkShifts.Application.Models;
using WorkShifts.Application.Repositories;
using WorkShifts.Application.Services;
using WorkShifts.Domain.Entities;
using WorkShifts.Domain.Exceptions;
using Xunit;

namespace WorkShifts.Application.UnitTests.Services
{
    public class WorkerShiftServiceTests
    {
        private readonly WorkerShiftService _workerShiftService;

        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IWorkerRepository> _workerRepositoryMock;
        private readonly Mock<IWorkerShiftRepository> _workerShiftRepositoryMock;

        public WorkerShiftServiceTests()
        {
            _mapperMock = new Mock<IMapper>();
            _workerRepositoryMock = new Mock<IWorkerRepository>();
            _workerShiftRepositoryMock = new Mock<IWorkerShiftRepository>();

            _workerShiftService = new WorkerShiftService(
                _mapperMock.Object,
                _workerRepositoryMock.Object,
                _workerShiftRepositoryMock.Object);
        }

        [Theory]
        [AutoData]
        public void CreateAsync_WhenWorkerNotFound_ThrowsException(WorkerShift workerShift, WorkerShiftDto workerShiftDto)
        {
            // arrange
            _mapperMock.Setup(x => x.Map<WorkerShift>(workerShiftDto))
                .Returns(workerShift);
            _workerRepositoryMock.Setup(x => x.GetAsync(workerShift.WorkerId))
                .ReturnsAsync(null as Worker);

            // act
            Func<Task> invoke = () => _workerShiftService.CreateAsync(workerShiftDto);

            // assert
            invoke.Should().ThrowAsync<WorkerNotFoundException>();
        }

        [Theory]
        [AutoData]
        public void CreateAsync_WhenWorkerHasAlreadyBeenAssignedToShift_ThrowsException(WorkerShift workerShift, WorkerShiftDto workerShiftDto)
        {
            // arrange
            _mapperMock.Setup(x => x.Map<WorkerShift>(workerShiftDto))
                .Returns(workerShift);
            _workerRepositoryMock.Setup(x => x.GetAsync(workerShift.WorkerId))
                .ReturnsAsync(new Worker());
            _workerShiftRepositoryMock.Setup(x => x.GetByWorkerIdAsync(workerShift.WorkerId))
                .ReturnsAsync(new List<WorkerShift>());
            var existingShifts = new List<WorkerShift>
            {
                new WorkerShift
                {
                    Date = workerShift.Date
                }
            };

            // act
            Func<Task> invoke = () => _workerShiftService.CreateAsync(workerShiftDto);

            // assert
            invoke.Should().ThrowAsync<WorkerHasAlreadyBeenAssignedException>();
        }

        [Theory]
        [AutoData]
        public async Task CreateAsync_WhenWorkerHasNotBeenAssignedToShift_ReturnsCreatedWorker(
            WorkerShift workerShift,
            WorkerShiftDto workerShiftDto,
            WorkerShift createdWorkerShift,
            WorkerShiftDto expectedWorkerShiftDto)
        {
            // arrange
            var initialId = workerShift.Id;
            _mapperMock.Setup(x => x.Map<WorkerShift>(workerShiftDto))
                .Returns(workerShift);
            _mapperMock.Setup(x => x.Map<WorkerShiftDto>(createdWorkerShift))
                .Returns(expectedWorkerShiftDto);
            _workerRepositoryMock.Setup(x => x.GetAsync(workerShift.WorkerId))
                .ReturnsAsync(new Worker());
            _workerShiftRepositoryMock.Setup(x => x.GetByWorkerIdAsync(workerShift.WorkerId))
                .ReturnsAsync(new List<WorkerShift>());
            var existingShifts = new List<WorkerShift>
            {
                new WorkerShift
                {
                    Date = workerShift.Date.AddDays(1)
                }
            };
            _workerShiftRepositoryMock.Setup(x => x.CreateAsync(It.Is<WorkerShift>(ws => ws.Id != initialId)))
                .ReturnsAsync(createdWorkerShift);

            // act
            var createdWorkerShiftDto = await _workerShiftService.CreateAsync(workerShiftDto);

            // assert
            createdWorkerShiftDto.Should().BeEquivalentTo(expectedWorkerShiftDto);
        }
    }
}
