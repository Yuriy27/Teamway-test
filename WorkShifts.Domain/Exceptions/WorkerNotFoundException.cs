using System;
using System.Runtime.Serialization;

namespace WorkShifts.Domain.Exceptions
{
    public class WorkerNotFoundException : DomainException
    {
        private Guid WorkerId { get; }

        public override int StatusCode => 404;

        public WorkerNotFoundException(Guid id) : base($"Worker with id {id} not found")
        {
            WorkerId = id;
        }

        public WorkerNotFoundException(string message) : base(message)
        {
        }

        public WorkerNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WorkerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
