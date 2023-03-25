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

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation
{
 
    public class UpdateLeaveAllocationCommandHandler : IRequestHandler<UpdateLeaveAllocationCommandRequest, Unit>
    {
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;
        private readonly IAppLogger<UpdateLeaveAllocationCommandRequest> _logger;

        public UpdateLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository, ILeaveTypeRepository leaveTypeRepository, IMapper mapper, IAppLogger<UpdateLeaveAllocationCommandRequest> logger)
        {
            this._leaveAllocationRepository = leaveAllocationRepository;
            this._leaveTypeRepository = leaveTypeRepository;
            this._mapper = mapper;
            this._logger = logger;
        }
        public async Task<Unit> Handle(UpdateLeaveAllocationCommandRequest request, CancellationToken cancellationToken)
        {
            var validator = new UpdateLeaveAllocationCommandValidator(_leaveAllocationRepository, _leaveTypeRepository);

            var validatorResult = await validator.ValidateAsync(request);

            if (validatorResult.Errors.Any())
            {
                _logger.LogWarning($"{nameof(UpdateLeaveAllocationCommandRequest)} is Invalid");
                throw new BadRequestException("Invalid Leave Allocation", validatorResult);
            }
            var leaveAllocation = await _leaveAllocationRepository.GetByIdAsync(request.Id);
            if(leaveAllocation == null)
            {
                throw new NotFoundException(nameof(LeaveAllocation), request.Id);
            }

            _mapper.Map(request, leaveAllocation);
            await _leaveAllocationRepository.UpdateAsync(leaveAllocation);
            return Unit.Value;
        }
    }
}
