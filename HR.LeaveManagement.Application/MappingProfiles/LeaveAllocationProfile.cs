using AutoMapper;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;
using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.MappingProfiles
{
    public class LeaveAllocationProfile: Profile
    {
        public LeaveAllocationProfile()
        {
            CreateMap<LeaveAllocation, LeaveAllocationDTO>().ReverseMap();
            CreateMap<LeaveAllocation, CreateLeaveAllocationCommandRequest>().ReverseMap();
            CreateMap<LeaveAllocation, UpdateLeaveAllocationCommandRequest>().ReverseMap();
        }
    }
}
