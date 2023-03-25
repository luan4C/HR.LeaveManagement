using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestList
{
    public class GetLeaveRequestListQueryHandler : IRequestHandler<GetLeaveRequestListQueryRequest, List<LeaveRequestListDTO>>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IMapper _mapper;

        public GetLeaveRequestListQueryHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _mapper = mapper;
        }

        public async Task<List<LeaveRequestListDTO>> Handle(GetLeaveRequestListQueryRequest request, CancellationToken cancellationToken)
        {
            var leaveRequest = await _leaveRequestRepository.GetLeaveRequestsWithDetails();
            var requests = _mapper.Map<List<LeaveRequestListDTO>>(leaveRequest);

            return requests;
        }
    }
}
