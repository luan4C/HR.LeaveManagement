using HR.LeaveManagement.Application.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Contracts.Identity
{
    internal interface IAuthService
    {
        Task<AuthResponse> Login(AuthRequest request);

        Task<RegistrationResponse> Register(RegistrationRequest request);
    }
}
