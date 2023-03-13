using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType
{
    public class UpdateLeaveTypeCommandValidator: AbstractValidator<UpdateLeaveTypeCommandRequest>
    {
        private readonly ILeaveTypeRepository _repository;
        public UpdateLeaveTypeCommandValidator(ILeaveTypeRepository repository)
        {
            _repository = repository;

            RuleFor(req => req.Id).NotNull().MustAsync(LeaveTypeMustExists);

            RuleFor(req => req.Name).NotEmpty()
                .WithMessage("{PropertyName} is required")
                .MaximumLength(70).WithMessage("{PropertyName} must be fewer than 70 characters");

            RuleFor(req => req.DefaultDays)
                .LessThan(100).WithMessage("{PropertyName} cannot exceed 100")
                .GreaterThan(1).WithMessage("{PropertyName} cannot be less than 1");

            
        }

        private async Task<bool> LeaveTypeMustExists(int id, CancellationToken token)
        {
            var leaveType = await _repository.GetByIdAsync(id);
            return leaveType != null;
        }
    }
}
