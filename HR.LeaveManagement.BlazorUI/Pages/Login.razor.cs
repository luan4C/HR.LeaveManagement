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
using HR.LeaveManagement.BlazorUI.Models.Auth;
using HR.LeaveManagement.BlazorUI.Contracts;

namespace HR.LeaveManagement.BlazorUI.Pages
{
    public partial class Login
    {
        [Inject]
        protected IAuthenticationService AuthenticationService { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        protected LoginModel Model { get; set; }
        protected string Message { get; set; }
        protected override void OnInitialized()
        {

            Model = new LoginModel();
        }
        protected async Task HandleLogin()
        {
            if(await AuthenticationService.AuthenticateAsync(Model.Email, Model.Password))
            {
                NavigationManager.NavigateTo("home");
            }
            Message = "Username/Password combination unknown";
        }

    }
}