using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes
{
    public class GetLeaveTypesQueryHandler : IRequestHandler<GetLeaveTypesQueryRequest, List<LeaveTypeDTO>>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IAppLogger<GetLeaveTypesQueryHandler> _logger;
        public GetLeaveTypesQueryHandler(IMapper mapper, ILeaveTypeRepository leaveRepository, 
            IAppLogger<GetLeaveTypesQueryHandler> logger)
        {
            this._mapper = mapper;
            this._leaveTypeRepository = leaveRepository;
            this._logger = logger;
        }

        public async Task<List<LeaveTypeDTO>> Handle(GetLeaveTypesQueryRequest request, CancellationToken cancellationToken)
        {
            var leaveTypes = await _leaveTypeRepository.GetAsync();

            var data = _mapper.Map<List<LeaveTypeDTO>>(leaveTypes);
            _logger.LogInformation("Leave Types retrieve successfully");
            return data;
        }
    }
}
