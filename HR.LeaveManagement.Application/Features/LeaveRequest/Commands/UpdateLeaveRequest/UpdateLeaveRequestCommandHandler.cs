using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveRequest.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest
{
    public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommandRequest, Unit>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IAppLogger<UpdateLeaveRequestCommandHandler> _logger;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;

        public UpdateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository, IAppLogger<UpdateLeaveRequestCommandHandler> logger, ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _logger = logger;
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateLeaveRequestCommandRequest request, CancellationToken cancellationToken)
        {
            var validator = new UpdateLeaveRequestCommandValidator(_leaveRequestRepository,_leaveTypeRepository);

            var validatorResult = await validator.ValidateAsync(request);

            if (validatorResult.Errors.Any())
            {
                _logger.LogWarning($"{nameof(UpdateLeaveRequestCommandRequest)} is Invalid");
                throw new BadRequestException("Invalid Leave Request", validatorResult);
            }
            var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);

            _mapper.Map(request, leaveRequest);

            await _leaveRequestRepository.UpdateAsync(leaveRequest);

            return Unit.Value;
        }
    }
}
