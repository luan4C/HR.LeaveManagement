using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.UnitTests.Mocks
{
    public class MockLeaveTypeRepository
    {

        public static Mock<ILeaveTypeRepository> GetMockedLeaveTypeRepository()
        {
            var leaveTypes = new List<LeaveType>()
            {
                new LeaveType
                {
                    Id = 1,
                    Name = "Test Vacation",
                    DefaultDays = 10,
                },
                new LeaveType
                {
                    Id = 2,
                    Name = "Test Sick",
                    DefaultDays = 4,
                },
                new LeaveType
                {
                    Id = 3,
                    Name = "Test Holiday",
                    DefaultDays = 1,
                }
            };

            var mock = new Mock<ILeaveTypeRepository>();
            mock.Setup(src => src.GetAsync()).ReturnsAsync(leaveTypes);
            mock.Setup(src => src.CreateAsync(It.IsAny<LeaveType>())).Returns((LeaveType leaveType) =>
            {
                leaveTypes.Add(leaveType);
                return Task.CompletedTask;
            }
            );

            return mock;
        }
    }
}
