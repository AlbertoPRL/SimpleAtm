using Blazored.SessionStorage;

namespace BlazorClient.Services;

public class TokenService
{
    private readonly ISessionStorageService _sessionStorageService;

    public TokenService(ISessionStorageService sessionStorageService)
    {
        _sessionStorageService = sessionStorageService;
    }

    public async Task SaveTokenAsync(string token)
    {
        await _sessionStorageService.SetItemAsync("authToken", token);
    }

    public ValueTask<string> GetToken()
    {
        return _sessionStorageService.GetItemAsync<string>("authToken");
    }
}
