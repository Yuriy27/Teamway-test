using System;

namespace WorkShifts.Domain.Entities
{
    public class Worker
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
    }
}
