using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string? message, object? key) : base(message, key)
        {
        }
    }
}
