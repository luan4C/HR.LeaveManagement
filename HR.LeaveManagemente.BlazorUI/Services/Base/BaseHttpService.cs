namespace HR.LeaveManagemente.BlazorUI.Services.Base
{
    public partial class BaseHttpService
    {
        protected IClient _client;
         public BaseHttpService(IClient client)
        {
            _client = client;
        }
    }
}
