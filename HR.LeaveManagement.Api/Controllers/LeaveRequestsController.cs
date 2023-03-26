using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CancelLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.DeleteLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetail;
using HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HR.LeaveManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveRequestsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<LeaveRequestController>
        [HttpGet]
        public async Task<IEnumerable<LeaveRequestListDTO>> Get()
        {
            return await _mediator.Send(new GetLeaveRequestListQueryRequest());
        }

        // GET api/<LeaveRequestController>/5
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var leaveRequest = await _mediator.Send(new GetLeaveRequestDetailsQueryRequest(){ Id = id });
            return Ok(leaveRequest);
        }

        // POST api/<LeaveRequestController>
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [HttpPost]
        public async Task<ActionResult> Post(CreateLeaveRequestCommandRequest request)
        {
            var response = await _mediator.Send(request);

            return CreatedAtAction(nameof(Get), new { id = response });
        }

        // PUT api/<LeaveRequestController>/5
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(UpdateLeaveRequestCommandRequest request)
        {
            await _mediator.Send(request);

            return NoContent();
        }

        // DELETE api/<LeaveRequestController>/5
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteLeaveRequestCommandRequest() { Id = id });
            return NoContent();
        }
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [HttpPut("CancelRequest/{id}")]
        public async Task<ActionResult> CancelRequest(int id)
        {
            await _mediator.Send(new CancelLeaveRequestCommandRequest() { Id = id });

            return NoContent();
        }
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [HttpPut]
        public async Task<ActionResult> UpdateApproval(ChangeLeaveRequestApprovalCommandRequest request)
        {
            await _mediator.Send(request);

            return NoContent();
        }
    }
}
