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
    public class WorkerServiceTests
    {
        private readonly WorkerService _workerService;

        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IWorkerRepository> _workerRepositoryMock;
        private readonly Mock<IWorkerShiftRepository> _workerShiftRepositoryMock;

        public WorkerServiceTests()
        {
            _mapperMock = new Mock<IMapper>();
            _workerRepositoryMock = new Mock<IWorkerRepository>();
            _workerShiftRepositoryMock = new Mock<IWorkerShiftRepository>();

            _workerService = new WorkerService(
                _mapperMock.Object,
                _workerRepositoryMock.Object,
                _workerShiftRepositoryMock.Object);
        }

        [Theory]
        [AutoData]
        public async Task DeleteAsync_WhenWorkerHasNoShifts_ReturnsDeletedWorker(Worker worker, WorkerDto workerDto)
        {
            // arrange
            _workerShiftRepositoryMock.Setup(x => x.GetByWorkerIdAsync(worker.Id))
                .ReturnsAsync(new List<WorkerShift>());
            _workerRepositoryMock.Setup(x => x.DeleteAsync(worker.Id))
                .ReturnsAsync(worker);
            _mapperMock.Setup(x => x.Map<WorkerDto>(worker))
                .Returns(workerDto);

            // act
            var deletedWorker = await _workerService.DeleteAsync(worker.Id);

            // assert
            deletedWorker.Should().BeEquivalentTo(workerDto);
        }

        [Theory]
        [AutoData]
        public void DeleteAsync_WhenWorkerHasShifts_ThrowsException(WorkerShift workerShift)
        {
            // arrange
            _workerShiftRepositoryMock.Setup(x => x.GetByWorkerIdAsync(workerShift.WorkerId))
                .ReturnsAsync(new List<WorkerShift> { workerShift });

            // act
            Func<Task> invoke = () => _workerService.DeleteAsync(workerShift.WorkerId);

            // assert
            invoke.Should().ThrowAsync<WorkerHasAlreadyBeenAssignedException>();
        }
    }
}
