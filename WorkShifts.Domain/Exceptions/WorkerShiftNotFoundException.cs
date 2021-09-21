using System;
using System.Runtime.Serialization;

namespace WorkShifts.Domain.Exceptions
{
    public class WorkerShiftNotFoundException : DomainException
    {
        private Guid WorkerShiftId { get; }

        public override int StatusCode => 404;

        public WorkerShiftNotFoundException(Guid id) : base($"Worker shift with id {id} not found")
        {
            WorkerShiftId = id;
        }

        public WorkerShiftNotFoundException(string message) : base(message)
        {
        }

        public WorkerShiftNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WorkerShiftNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
