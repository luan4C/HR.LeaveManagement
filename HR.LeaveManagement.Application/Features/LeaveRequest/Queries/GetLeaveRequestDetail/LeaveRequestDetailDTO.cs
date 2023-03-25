﻿using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetail
{
    public class LeaveRequestDetailDTO
    {
        public string RequestingEmployeeId { get; set; }

        public LeaveTypeDTO LeaveType { get; set; }
        public int LeaveTypeId { get; set; }
        public string RequestComments { get; set; }
        public DateTime DateRequested { get; set; }

        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
        public DateTime? DateActioned { get; set; }
        public bool Cancelled { get; set; }
        public bool? Approved { get; set; }
    }
}
