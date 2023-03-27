using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace HR.LeaveManagement.Persistence.IntegrationTest
{
    public class HrDatabaseContextTest
    {
        private HrDatabaseContext _hrDbContext;

        public HrDatabaseContextTest()
        {
            var dbOptions = new DbContextOptionsBuilder<HrDatabaseContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            _hrDbContext = new HrDatabaseContext(dbOptions);
        }

        [Fact]
        public async Task Save_DateCreatedValue()
        {
            //MOCK
            var leaveType = new LeaveType()
            {
                Id = 1,
                Name = "Test Vacation",
                DefaultDays = 10,
            };

            //ACT
            await _hrDbContext.LeaveTypes.AddAsync(leaveType);
            await _hrDbContext.SaveChangesAsync();

            //ASSERT
            leaveType.DateCreated.ShouldNotBeNull();
        }

        [Fact]
        public async Task Save_DateCreatedModified()
        {
            var leaveType = new LeaveType()
            {
                Id = 1,
                Name = "Test Vacation",
                DefaultDays = 10,
            };

            //ACT
            await _hrDbContext.LeaveTypes.AddAsync(leaveType);
            await _hrDbContext.SaveChangesAsync();

            leaveType.DefaultDays = 9;
            _hrDbContext.Update(leaveType);
            await _hrDbContext.SaveChangesAsync();
            //ASSERT
            leaveType.DateModified.ShouldNotBeNull();
        }
    }
}