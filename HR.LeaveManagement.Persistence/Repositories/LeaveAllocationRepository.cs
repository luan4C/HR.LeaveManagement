using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
    {
        public LeaveAllocationRepository(HrDatabaseContext dbContext) : base(dbContext)
        {
        }

        public async Task AddAllocations(List<LeaveAllocation> allocations)
        {
            await _context.LeaveAllocations.AddRangeAsync(allocations);
            await _context.SaveChangesAsync();
         
        }

        public async Task<bool> AllocationExists(string userId, int LeaveTypeId, int period)
        {
            return await _context.LeaveAllocations.AnyAsync(p => p.EmployeeId == userId &&
            p.LeaveTypeId == LeaveTypeId && p.Period == period);
        }

        public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails()
        {
            return await _context.LeaveAllocations.Include(p => p.LeaveType).AsNoTracking()
            .ToListAsync();
        }

        public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails(string userId)
        {
            return await _context.LeaveAllocations.Include(p => p.LeaveType).AsNoTracking()
         .Where(p => p.EmployeeId == userId).ToListAsync();
        }

        public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id)
        {
            return await _context.LeaveAllocations.Include(p => p.LeaveType).AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id== id);
        }

        public async Task<LeaveAllocation> GetUserAllocation(string userId, int leaveTypeId)
        {
            return await _context.LeaveAllocations.AsNoTracking()
                 .FirstOrDefaultAsync(p => p.EmployeeId == userId && p.LeaveTypeId == leaveTypeId);
        }
    }
}
