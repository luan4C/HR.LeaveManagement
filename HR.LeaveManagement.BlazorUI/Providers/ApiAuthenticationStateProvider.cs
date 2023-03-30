using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace HR.LeaveManagement.BlazorUI.Providers
{
    public class ApiAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;
        private readonly JwtSecurityTokenHandler _tokenHandler;
        public ApiAuthenticationStateProvider(ILocalStorageService localstorage)
        {
            _localStorage = localstorage;
            _tokenHandler = new JwtSecurityTokenHandler();    
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            // Create blank ClaimsPrinicipal
            var user = new ClaimsPrincipal(new ClaimsIdentity());
            var isTokenPresent = await _localStorage.ContainKeyAsync("token");
            // Ensures that the token is in local storage
            if (isTokenPresent == false)
            {
                // If not return Authentication state with the blank ClaimsPrincipal
                return new AuthenticationState(user);
            }
            var token = await _localStorage.GetItemAsync<string>("token");

            var tokenContent = _tokenHandler.ReadJwtToken(token);
            // Ensures that the token is valid until now
            if(tokenContent.ValidTo < DateTime.Now)
            {
                //If not removes the token and return Authentication with the blank ClaimsPrincipal
                await _localStorage.RemoveItemAsync("token");
                return new AuthenticationState(user);
            }
            // Get the user claims and return the ClaimsPrincipal by the user
            var claims = await GetClaimsAsync();
            user = new ClaimsPrincipal(new ClaimsIdentity(claims, "token"));
            return new AuthenticationState(user);
        }

        public async Task LoggedIn()
        {
            var claims = await GetClaimsAsync();
            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
            var autheticationState = Task.FromResult(new AuthenticationState(user));
            NotifyAuthenticationStateChanged(autheticationState);
        }
        public async Task LoggedOut()
        {
            await _localStorage.RemoveItemAsync("token");
            var nobody = new ClaimsPrincipal(new ClaimsIdentity());
            var authenticationState = Task.FromResult(new AuthenticationState(nobody));
            NotifyAuthenticationStateChanged(authenticationState);
        }
        private async Task<List<Claim>> GetClaimsAsync()
        {
            var savedToken = await _localStorage.GetItemAsync<string>("token");
            var tokenContent = _tokenHandler.ReadJwtToken(savedToken);
            var claims = tokenContent.Claims.ToList();

            claims.Add(new Claim(ClaimTypes.Name, tokenContent.Subject));
            return claims;

        }
    }
}
