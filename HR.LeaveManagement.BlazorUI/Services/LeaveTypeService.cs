using AutoMapper;
using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Models.LeaveTypes;
using HR.LeaveManagement.BlazorUI.Services.Base;

namespace HR.LeaveManagement.BlazorUI.Services
{
    public class LeaveTypeService : BaseHttpService, ILeaveTypeService
    {
        private readonly IMapper _mapper;
        public LeaveTypeService(IClient client, IMapper mapper) : base(client)
        {
            _mapper = mapper;
        }

        public async Task<Response<Guid>> CreateLeaveType(LeaveTypeVM leaveType)
        {
            try
            {
            var createLeaveTypeCommand = _mapper.Map<CreateLeaveTypeCommandRequest>(leaveType);

            await _client.LeaveTypesPOSTAsync(createLeaveTypeCommand);
                return new Response<Guid>()
                {
                    Success = true,
                };
            }catch (ApiException ex)
            {
                return ConvertApiExceptions<Guid>(ex);
            }   
        }

        public async Task<Response<Guid>> DeleteLeaveType(int id)
        {
            try
            {
                await _client.LeaveTypesDELETEAsync(id);
                return new Response<Guid>()
                {
                    Success = true
                };
            }catch (ApiException ex)
            {
                return ConvertApiExceptions<Guid>(ex);
            }
        }

        public Task<LeaveTypeVM> GetLeaveTypeDetails(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<LeaveTypeVM>> GetLeaveTypes()
        {
            var leaveTypes = await _client.LeaveTypesAllAsync();

            return _mapper.Map<List<LeaveTypeVM>>(leaveTypes);

        }

        public async Task<Response<Guid>> UpdateLeaveType(int id, LeaveTypeVM leaveType)
        {
            try
            {
                var createLeaveTypeCommand = _mapper.Map<UpdateLeaveTypeCommandRequest>(leaveType);

                await _client.LeaveTypesPUTAsync(id.ToString(),createLeaveTypeCommand);
                return new Response<Guid>()
                {
                    Success = true,
                };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<Guid>(ex);
            }
        }
    }
}
