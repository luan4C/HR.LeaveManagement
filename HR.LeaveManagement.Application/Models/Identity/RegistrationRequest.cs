using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Models.Identity
{
    public record RegistrationRequest
    {
        [Required]
        public string FirstName;

        [Required]
        public string LastName;

        [Required]
        [EmailAddress]
        public string Email;

        [Required]
        [MinLength(6)]
        public string Username;

        [Required]
        [MinLength(6)]
        public string Password;
    }
}
