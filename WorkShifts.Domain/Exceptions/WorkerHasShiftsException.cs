using System;
using System.Runtime.Serialization;

namespace WorkShifts.Domain.Exceptions
{
    public class WorkerHasShiftsException : DomainException
    {
        private Guid WorkerId { get; }

        public override int StatusCode => 404;

        public WorkerHasShiftsException(Guid id) : base($"Worker with id {id} has assigned shifts")
        {
            WorkerId = id;
        }

        public WorkerHasShiftsException(string message) : base(message)
        {
        }

        public WorkerHasShiftsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WorkerHasShiftsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
