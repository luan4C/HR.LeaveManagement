using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Models.Identity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
        private readonly IUserService _userService;

        public CreateLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository, ILeaveTypeRepository leaveTypeRepository, IMapper mapper, IAppLogger<CreateLeaveAllocationCommandRequest> logger, IUserService userService)
        {
            this._leaveAllocationRepository = leaveAllocationRepository;
            this._leaveTypeRepository = leaveTypeRepository;
            this._mapper = mapper;
            this._logger = logger;
            _userService = userService;
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

            List<Employee> employees = await _userService.GetEmployees();

            var period = DateTime.Now.Year;

            var allocations = new List<Domain.LeaveAllocation>();
            foreach(var employee in employees)
            {
                var leaveAllocationExists = await _leaveAllocationRepository.AllocationExists(employee.Id, request.LeaveTypeId, period);

                if (leaveAllocationExists)
                {
                    continue;
                }

                allocations.Add(new Domain.LeaveAllocation
                {
                    LeaveTypeId = request.LeaveTypeId,
                    Period = period,
                    EmployeeId = employee.Id,
                    NumberOfDays = leaveType.DefaultDays
                });
            }
            if (allocations.Any())
            {
            await _leaveAllocationRepository.AddAllocations(allocations);
            }
            //var leaveAllocation = _mapper.Map<Domain.LeaveAllocation>(request);

            //await _leaveAllocationRepository.CreateAsync(leaveAllocation);
            return Unit.Value;
        }
    }
}
