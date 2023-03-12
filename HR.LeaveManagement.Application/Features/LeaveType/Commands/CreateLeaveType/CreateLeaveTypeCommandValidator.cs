using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType
{
    public class CreateLeaveTypeCommandValidator: AbstractValidator<CreateLeaveTypeCommandRequest>
    {
        private readonly ILeaveTypeRepository _repository;
        public CreateLeaveTypeCommandValidator(ILeaveTypeRepository repository) {
            _repository = repository;

            RuleFor(req => req.Name).NotEmpty()
                .WithMessage("{PropertyName} is required")
                .MaximumLength(70).WithMessage("{PropertyName} must be fewer than 70 characters");

            RuleFor(req => req.DefaultDays)
                .LessThan(100).WithMessage("{PropertyName} cannot exceed 100")
                .GreaterThan(1).WithMessage("{PropertyName} cannot be less than 1");

            RuleFor(request => request).MustAsync(LeaveTypeNameUnique).WithMessage("Leave type already exists");
        }

        private Task<bool> LeaveTypeNameUnique(CreateLeaveTypeCommandRequest command, CancellationToken token)
        {
            return _repository.IsLeaveTypeUnique(command.Name);
        }
    }
}
