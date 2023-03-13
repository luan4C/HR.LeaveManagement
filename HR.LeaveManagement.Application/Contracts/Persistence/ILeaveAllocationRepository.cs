using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.Contracts.Persistence
{
    public interface ILeaveAllocationRepository: IGenericRepository<LeaveAllocation>
    {
        Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails();
        Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails(string userId);
        Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id);

        Task<bool> AllocationExists(string userId, int LeaveTypeId, int period);

        Task AddAllocations(List<LeaveAllocation> allocations);

        Task<LeaveAllocation> GetUserAllocation(string userId, int leaveTypeId);

    }

}
