using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.DeleteLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;
using HR.LeaveManagement.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HR.LeaveManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveAllocationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveAllocationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<LeaveAllocations>
        [HttpGet]
        public async Task<ActionResult<List<LeaveAllocation>>> Get()
        {
            var leaveAllocations = await _mediator.Send(new GetLeaveAllocationListQueryRequest());
            return Ok(leaveAllocations);
        }

        // GET api/<LeaveAllocations>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveAllocation>> Get(int id)
        {
            var leaveAllocations = await _mediator.Send(new GetLeaveAllocationDetailsQueryRequest(id));
            return Ok(leaveAllocations);
        }

        // POST api/<LeaveAllocations>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Post(CreateLeaveAllocationCommandRequest leaveAllocation)
        {
            var response = await _mediator.Send(leaveAllocation);
            return CreatedAtAction(nameof(Get), new { id = response } );
        }
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // PUT api/<LeaveAllocations>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(UpdateLeaveAllocationCommandRequest leaveAllocation)
        {
            var response = await _mediator.Send(leaveAllocation);
            return NoContent();
        }

        // DELETE api/<LeaveAllocations>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteLeaveAllocationCommandRequest { Id = id };
            await _mediator.Send(command);

            return NoContent();
        }
    }
}
