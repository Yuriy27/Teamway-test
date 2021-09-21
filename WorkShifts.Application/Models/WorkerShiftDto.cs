using System;
using WorkShifts.Domain.Enums;

namespace WorkShifts.Application.Models
{
    public class WorkerShiftDto
    {
        public Guid Id { get; set; }
        public DateTimeOffset Date { get; set; }
        public Shift Shift { get; set; }
        public Guid WorkerId { get; set; }
        public WorkerDto Worker { get; set; }
    }
}
