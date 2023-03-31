using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.JSInterop;
using HR.LeaveManagement.BlazorUI;
using HR.LeaveManagement.BlazorUI.Shared;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Authorization;
using HR.LeaveManagement.BlazorUI.Models.LeaveRequest;
using HR.LeaveManagement.BlazorUI.Models.LeaveTypes;
using HR.LeaveManagement.BlazorUI.Contracts;

namespace HR.LeaveManagement.BlazorUI.Pages.LeaveRequests
{
    public partial class Create
    {
        [Inject]
        private ILeaveTypeService leaveTypeService { get; set; }

        [Inject]

        private ILeaveRequestService leaveRequestService { get; set; }

        [Inject]
        private NavigationManager navigationManager { get; set; }
        LeaveRequestVM LeaveRequest { get; set; } = new LeaveRequestVM();

        List<LeaveTypeVM> LeaveTypesVMs { get; set; } = new List<LeaveTypeVM>();

        private async Task HandleValidSubmit()
        {
            await leaveRequestService.CreateLeaveRequest(LeaveRequest);

            navigationManager.NavigateTo("/leaverequests/");
        }

        protected override async Task OnInitializedAsync()
        {
            LeaveTypesVMs = await leaveTypeService.GetLeaveTypes();
        }
    }
}