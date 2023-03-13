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

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation
{
    public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommandRequest, Unit>
    {
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;
        private readonly IAppLogger<CreateLeaveAllocationCommandRequest> _logger;

        public CreateLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository, ILeaveTypeRepository leaveTypeRepository, IMapper mapper,IAppLogger<CreateLeaveAllocationCommandRequest> logger)
        {
            this._leaveAllocationRepository = leaveAllocationRepository;
            this._leaveTypeRepository = leaveTypeRepository;
            this._mapper = mapper;
            this._logger = logger;
        }

        public async Task<Unit> Handle(CreateLeaveAllocationCommandRequest request, CancellationToken cancellationToken)
        {
            var validator = new CreateLeaveAllocationCommandValidator(_leaveTypeRepository);

            var validatorResult = await validator.ValidateAsync(request);
            if (validatorResult.Errors.Any())
            {
                _logger.LogWarning($"{nameof(CreateLeaveAllocationCommandRequest)} is Invalid");
                throw new BadRequestException("Invalid Leave Allocation", validatorResult);
            }

            var leaveType = await _leaveTypeRepository.GetByIdAsync(request.LeaveTypeId);

            var leaveAllocation = _mapper.Map<Domain.LeaveAllocation>(request);

            await _leaveAllocationRepository.CreateAsync(leaveAllocation);
            return Unit.Value;
        }
    }
}
