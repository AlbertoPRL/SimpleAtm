using Blazored.SessionStorage;

namespace BlazorClient.Services;

public class TokenService
{
    private readonly ISessionStorageService _sessionStorageService;
    public string token = string.Empty;

    public TokenService(ISessionStorageService sessionStorageService)
    {
        _sessionStorageService = sessionStorageService;
    }

    public async Task LoadTokenAsync()
    {
        token = await _sessionStorageService.GetItemAsync<string>("authToken");
    }

    public string GetToken()
    {
        return token;
    }

    public void ClearToken()
    {
        token = string.Empty;
    }
}
