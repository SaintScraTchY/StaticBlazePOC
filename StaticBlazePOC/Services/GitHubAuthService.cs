
using System.Net.Http.Json;
using Blazored.LocalStorage;

namespace StaticBlazePOC.Services;
public class GitHubAuthService
{
    private readonly HttpClient _http;
    private readonly ILocalStorageService _localStorage;
    private const string ClientId = "Ov23liz6jrSB5gAPrzO9";

    public GitHubAuthService(HttpClient http, ILocalStorageService localStorage)
    {
        _http = http;
        _localStorage = localStorage;
    }

    public async Task<string> GetDeviceCode()
    {
        var response = await _http.PostAsJsonAsync(
            "https://github.com/login/device/code",
            new { client_id = ClientId, scope = "repo" });

        var deviceCodeResponse = await response.Content.ReadFromJsonAsync<DeviceCodeResponse>();
        return deviceCodeResponse.UserCode; // Display this to the user
    }

    public async Task<string> PollForToken(string deviceCode)
    {
        var response = await _http.PostAsJsonAsync(
            "https://github.com/login/oauth/access_token",
            new { client_id = ClientId, device_code = deviceCode, grant_type = "urn:ietf:params:oauth:grant-type:device_code" });

        var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponse>();
        return tokenResponse.AccessToken;
    }

    public async Task StoreToken(string token)
    {
        await _localStorage.SetItemAsync("github_token", token);
    }

    public async Task<string?> GetToken()
    {
        return await _localStorage.GetItemAsync<string>("github_token");
    }

    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync("github_token");
    }
}

public class DeviceCodeResponse
{
    public string UserCode { get; set; }
    public string VerificationUri { get; set; }
    public int ExpiresIn { get; set; }
    public int Interval { get; set; }
}

public class TokenResponse
{
    public string AccessToken { get; set; }
}