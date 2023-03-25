using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails
{
    public class GetLeaveAllocationDetaisQueryHandler : IRequestHandler<GetLeaveAllocationDetailsQueryRequest, LeaveAllocationDetailsDTO>
    {
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly IMapper _mapper;

        public GetLeaveAllocationDetaisQueryHandler(ILeaveAllocationRepository leaveAllocationRepository, IMapper mapper)
        {
            this._leaveAllocationRepository = leaveAllocationRepository;
            this._mapper = mapper;
        }
        public async Task<LeaveAllocationDetailsDTO> Handle(GetLeaveAllocationDetailsQueryRequest request, CancellationToken cancellationToken)
        {
            var leaveAllocation = await _leaveAllocationRepository.GetLeaveAllocationWithDetails(request.Id);
            if (leaveAllocation == null)
                throw new NotFoundException(nameof(LeaveAllocation), request.Id);
            var leaveAllocationDTO = _mapper.Map<LeaveAllocationDetailsDTO>(leaveAllocation);
            return leaveAllocationDTO;
        }
    }
}
