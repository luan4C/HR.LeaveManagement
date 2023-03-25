using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations
{
    public class GetLeaveAllocationListQueryHandler : IRequestHandler<GetLeaveAllocationListQueryRequest, List<LeaveAllocationDTO>>
    {
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly IMapper _mapper;

        public GetLeaveAllocationListQueryHandler(ILeaveAllocationRepository leaveAllocationRepository, IMapper mapper )
        {
            this._leaveAllocationRepository = leaveAllocationRepository;
            this._mapper = mapper;
        }
        public async Task<List<LeaveAllocationDTO>> Handle(GetLeaveAllocationListQueryRequest request, CancellationToken cancellationToken)
        {
            var leaveAllocations = await _leaveAllocationRepository.GetLeaveAllocationsWithDetails();
            var allocations = _mapper.Map<List<LeaveAllocationDTO>>( leaveAllocations );

            return allocations;
        }
    }
}
