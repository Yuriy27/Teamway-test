using System;
using WorkShifts.Domain.Enums;

namespace WorkShifts.Domain.Entities
{
    public class WorkerShift
    {
        public Guid Id { get; set; }
        public DateTimeOffset Date { get; set; }
        public Shift Shift { get; set; }
        public Guid WorkerId { get; set; }
        public Worker Worker { get; set; }
    }
}
