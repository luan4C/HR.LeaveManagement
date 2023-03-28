﻿using HR.LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Identity.DbContext
{
    public class HRLeaveManagementIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public HRLeaveManagementIdentityDbContext(DbContextOptions<HRLeaveManagementIdentityDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(HRLeaveManagementIdentityDbContext).Assembly);
            base.OnModelCreating(builder);
        }
    }
}
