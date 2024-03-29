﻿using AutoMapper;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetail;
using HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestList;
using HR.LeaveManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.MappingProfiles
{
    public class LeaveRequestProfile: Profile
    {
        public LeaveRequestProfile() 
        {
            CreateMap<LeaveRequestListDTO, LeaveRequest>().ReverseMap();
            CreateMap<LeaveRequestDetailDTO, LeaveRequest>().ReverseMap();
            CreateMap<LeaveRequest, LeaveRequestDetailDTO>();
            CreateMap<CreateLeaveRequestCommandRequest, LeaveRequest>();
            CreateMap<UpdateLeaveRequestCommandRequest, LeaveRequest>();
        }
    }
}
