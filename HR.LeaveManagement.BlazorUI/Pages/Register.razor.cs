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
using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Models.Auth;

namespace HR.LeaveManagement.BlazorUI.Pages
{
    

    public partial class Register
    {
        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        protected IAuthenticationService AuthenticationService { get; set; }

        protected RegisterModel Model { get; set; }
        protected string Message { get; set; }  
        protected override void OnInitialized()
        {
            Model = new RegisterModel();
        }

        protected async Task HandleRegistration()
        {
            var result = await AuthenticationService.RegisterAsync(Model.FirstName, Model.LastName, Model.Email, Model.Password);

            if (result)
            {
                NavigationManager.NavigateTo("home");
            }
            Message = "Something went wrong, please try again.";
        }
    }
}