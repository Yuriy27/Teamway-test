using System;
using System.Runtime.Serialization;

namespace WorkShifts.Domain.Exceptions
{
    public class WorkerHasAlreadyBeenAssignedException : DomainException
    {
        private Guid WorkerId { get; }
        private DateTimeOffset Date { get; }

        public override int StatusCode => 409;

        public WorkerHasAlreadyBeenAssignedException(Guid workerId, DateTimeOffset date)
            : base($"Worker with id {workerId} has already been assigned to shift on {date}")
        {
            WorkerId = workerId;
            Date = date;
        }

        public WorkerHasAlreadyBeenAssignedException(string message) : base(message)
        {
        }

        public WorkerHasAlreadyBeenAssignedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WorkerHasAlreadyBeenAssignedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
