using AutoMapper;
using WorkShifts.Application.Models;
using WorkShifts.Domain.Entities;

namespace WorkShifts.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappings();
        }

        private void ApplyMappings()
        {
            CreateMap<WorkerDto, Worker>().ReverseMap();
            CreateMap<WorkerShiftDto, WorkerShift>().ReverseMap();
        }
    }
}
