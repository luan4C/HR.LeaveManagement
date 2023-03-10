using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManager.Domain;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes
{
    public class GetLeaveTypesQueryHandler : IRequestHandler<GetLeaveTypesQueryRequest, List<LeaveTypeDTO>>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        public GetLeaveTypesQueryHandler(IMapper mapper, ILeaveTypeRepository leaveRepository)
        {
            this._mapper = mapper;
            this._leaveTypeRepository = leaveRepository;
        }

        public async Task<List<LeaveTypeDTO>> Handle(GetLeaveTypesQueryRequest request, CancellationToken cancellationToken)
        {
            var leaveTypes = await _leaveTypeRepository.GetAsync();

            var data = _mapper.Map<List<LeaveTypeDTO>>(leaveTypes);

            return data;
        }
    }
}
