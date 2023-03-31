using AutoMapper;
using HR.LeaveManagement.BlazorUI.Models.LeaveAllocation;
using HR.LeaveManagement.BlazorUI.Models.LeaveRequest;
using HR.LeaveManagement.BlazorUI.Models.LeaveTypes;
using HR.LeaveManagement.BlazorUI.Services.Base;

namespace HR.LeaveManagement.BlazorUI.MappingProfiles
{
    public class MappingConfig: Profile
    {
        public MappingConfig()
        {
            CreateMap<LeaveTypeDTO, LeaveTypeVM>().ReverseMap();
            CreateMap<LeaveRequestListDTO, LeaveRequestVM>().ReverseMap();
            CreateMap<LeaveRequestDetailDTO, LeaveRequestVM>().ReverseMap();
            CreateMap<LeaveAllocation, LeaveAllocationVM>().ReverseMap();
            CreateMap<CreateLeaveTypeCommandRequest, LeaveTypeVM>().ReverseMap();
            CreateMap<UpdateLeaveTypeCommandRequest, LeaveTypeVM>().ReverseMap();
            CreateMap<CreateLeaveRequestCommandRequest, LeaveRequestVM>().ReverseMap();
        }
    }
}
