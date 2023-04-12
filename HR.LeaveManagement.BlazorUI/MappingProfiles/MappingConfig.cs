using AutoMapper;
using HR.LeaveManagement.BlazorUI.Models;
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
            CreateMap<LeaveRequestListDTO, LeaveRequestVM>()
                .ForMember(q => q.DateRequested, opt=> opt.MapFrom(src=> src.DateRequested.DateTime))
                .ForMember(q => q.StartDate, opt => opt.MapFrom(src => src.StartDate.DateTime))
                .ForMember(q => q.EndDate, opt => opt.MapFrom(src => src.EndDate.DateTime))
                .ReverseMap();
            CreateMap<LeaveRequestDetailDTO, LeaveRequestVM>()
                .ForMember(q => q.DateRequested, opt => opt.MapFrom(src => src.DateRequested.DateTime))
                .ForMember(q => q.StartDate, opt => opt.MapFrom(src => src.StartDate.DateTime))
                .ForMember(q => q.EndDate, opt => opt.MapFrom(src => src.EndDate.DateTime))
                .ReverseMap();

            CreateMap<LeaveAllocationDTO, LeaveAllocationVM>().ReverseMap();
            CreateMap<CreateLeaveTypeCommandRequest, LeaveTypeVM>().ReverseMap();
            CreateMap<UpdateLeaveTypeCommandRequest, LeaveTypeVM>().ReverseMap();
            CreateMap<CreateLeaveRequestCommandRequest, LeaveRequestVM>().ReverseMap();

            CreateMap<EmployeeVM, Employee>().ReverseMap();
        }
    }
}
