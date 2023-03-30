using Blazored.LocalStorage;
using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Providers;
using HR.LeaveManagement.BlazorUI.Services.Base;
using Microsoft.AspNetCore.Components.Authorization;

namespace HR.LeaveManagement.BlazorUI.Services
{
    public class AuthenticationService: BaseHttpService, IAuthenticationService
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        public AuthenticationService(IClient client, ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider)
            : base(client, localStorage)
        {
            _authenticationStateProvider = authenticationStateProvider;
        }
    
        public async Task<bool> AuthenticateAsync(string username, string password)
        {
            try
            {
                var authRequest = new AuthRequest { Email = username, Password = password };
                var response = await _client.LoginAsync(authRequest);
                if (response.Token != string.Empty)
                {
                    await _localStorage.SetItemAsync("token", response.Token);
                    //Set claims in blazor and login state

                    await ((ApiAuthenticationStateProvider)_authenticationStateProvider).LoggedIn();
                    
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task Logout()
        {
            //Remove claims in blazor and invalidate login state
            await ((ApiAuthenticationStateProvider)_authenticationStateProvider).LoggedOut();
        }

        public async Task<bool> RegisterAsync(string firstName, string lastName, string email, string password)
        {
            try
            {
                var RegisterRequest = new RegistrationRequest
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    Password = password,
                    Username = email
                };

                var RegisterResponse = await _client.RegisterAsync(RegisterRequest);

                if (RegisterResponse.UserId != string.Empty)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
            
        }
    }
}
