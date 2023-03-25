using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CancelLeaveRequest
{
    public class CancelLeaveRequestCommandHandler : IRequestHandler<CancelLeaveRequestCommandRequest, Unit>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IMapper _mapper;
        private readonly IAppLogger<CancelLeaveRequestCommandHandler> _logger;
        public CancelLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper, IAppLogger<CancelLeaveRequestCommandHandler> logger)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(CancelLeaveRequestCommandRequest request, CancellationToken cancellationToken)
        {
            var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);

            if(leaveRequest is null)
            {
                _logger.LogWarning("{0} not found with Id: {1}", nameof(Domain.LeaveRequest), request.Id);
                throw new NotFoundException(nameof(Domain.LeaveRequest), request.Id);
            }

            leaveRequest.Cancelled = true;

            return Unit.Value;
        }
    }
}
