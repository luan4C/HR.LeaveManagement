using HR.LeaveManagement.Application.Features.LeaveRequest.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest
{
    public class UpdateLeaveRequestCommandRequest: BaseLeaveRequest, IRequest<Unit> 
    {
        public int Id { get; set; }

        public string? Comments { get; set; }

        public bool? Cancelled { get; set; }
    }
}
