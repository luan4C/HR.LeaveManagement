﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestList
{
    public class GetLeaveRequestListQueryRequest: IRequest<List<LeaveRequestListDTO>>
    {
        public bool IsLoggedInUser { get; set; }
    }
}
