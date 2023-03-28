using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Identity.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "fd8b748f-d2ac-4616-aaea-b71e2ef8bace",
                    UserId = "ed56a29e-ba76-4fa5-a7df-cc41876d88e9"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "63c83881-425d-4585-b495-71321ccc60e9",
                    UserId = "4c8b1d94-2b38-4c23-a886-6b630d0a7a44"
                });
        }
    }
}
