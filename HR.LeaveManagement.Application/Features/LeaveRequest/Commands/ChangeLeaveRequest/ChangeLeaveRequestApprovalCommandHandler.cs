using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequest
{
    public class ChangeLeaveRequestApprovalCommandHandler : IRequestHandler<ChangeLeaveRequestApprovalCommandRequest,Unit>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IAppLogger<ChangeLeaveRequestApprovalCommandHandler> _logger;
        public ChangeLeaveRequestApprovalCommandHandler(ILeaveRequestRepository leaveRequestRepository, IAppLogger<ChangeLeaveRequestApprovalCommandHandler> logger)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(ChangeLeaveRequestApprovalCommandRequest request, CancellationToken cancellationToken)
        {
            var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);   

            if(leaveRequest is null)
            {
                _logger.LogWarning("{0} does not exist with Id: {1}", nameof(Domain.LeaveRequest), request.Id);
                throw new NotFoundException(nameof(Domain.LeaveRequest), request.Id);
            }

            leaveRequest.Approved = request.Approved;

            await _leaveRequestRepository.UpdateAsync(leaveRequest);

            return Unit.Value;
        }
    }
}
