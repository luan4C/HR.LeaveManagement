using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation
{
    public class UpdateLeaveAllocationCommandValidator: AbstractValidator<UpdateLeaveAllocationCommandRequest>
    {
        private readonly ILeaveAllocationRepository _leaveAllocationReository;
        private readonly ILeaveTypeRepository _leaveTypeReository;

        public UpdateLeaveAllocationCommandValidator(ILeaveAllocationRepository leaveAllocationReository, ILeaveTypeRepository leaveTypeReository)
        {
            _leaveAllocationReository = leaveAllocationReository;
            _leaveTypeReository = leaveTypeReository;

            RuleFor(p => p.Period)
                .GreaterThan(0).WithMessage("{PropertyName} must be greather than 0");

            RuleFor(p => p.NumberOfDays)
                .GreaterThan(0).WithMessage("{PropertyName} must be greather than 0");
            RuleFor(p=>p.Id).GreaterThan(0).WithMessage("{PropertyName} must be greather than 0")
                .MustAsync(LeaveTypeAllocationMustExist).WithMessage("Leave Allocation does not exist");

            RuleFor(p => p.LeaveTypeId).GreaterThan(0).WithMessage("{PropertyName} must be greather than 0")
                .MustAsync(LeaveTypeMustExist).WithMessage("Leave Type does not exist");
        }

        private async Task<bool> LeaveTypeAllocationMustExist(int id, CancellationToken arg2)
        {
            var leaveType = await _leaveAllocationReository.GetByIdAsync(id);
            return leaveType != null;
        }

        private async Task<bool> LeaveTypeMustExist(int id, CancellationToken arg2)
        {
            var leaveType = await _leaveTypeReository.GetByIdAsync(id);
            return leaveType != null;
        }
    }
}
