using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Shared
{
    public class BaseLeaveRequestValidator: AbstractValidator<BaseLeaveRequest>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        public BaseLeaveRequestValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;
            RuleFor(src=>src.EndDate).GreaterThan(src=>src.StartDate)
                .WithMessage("{PropertyName} must be after {ComparisonValue}");
            RuleFor(src => src.StartDate).LessThan(src => src.EndDate)
                .WithMessage("{PropertyName} must be before {ComparisonValue}");

            RuleFor(src => src.LeaveTypeId).GreaterThan(0)
                .MustAsync(LeaveTypeMustExist)
                .WithMessage("{PropertyName} does not exist");
        }

        private async Task<bool> LeaveTypeMustExist(int leaveTypeId, CancellationToken arg2)
        {
            var leaveType= await _leaveTypeRepository.GetByIdAsync(leaveTypeId);
            return leaveType!= null;
        }
    }
}
