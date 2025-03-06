using System.Net.Http.Json;
using Blazored.LocalStorage;
using StaticBlazePOC.helper;

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

    public string GenerateAuthorizationUrl()
    {
        var codeVerifier = PkceHelper.GenerateCodeVerifier();
        var codeChallenge = PkceHelper.GenerateCodeChallenge(codeVerifier);

        // Store the code verifier for later use
        _localStorage.SetItemAsync("code_verifier", codeVerifier);

        return $"https://github.com/login/oauth/authorize?client_id={ClientId}&redirect_uri={Uri.EscapeDataString("https://your-app.com/callback")}&scope=repo&code_challenge={codeChallenge}&code_challenge_method=S256";
    }

    public async Task<string> ExchangeCodeForToken(string code)
    {
        var codeVerifier = await _localStorage.GetItemAsync<string>("code_verifier");

        var response = await _http.PostAsJsonAsync(
            "https://github.com/login/oauth/access_token",
            new
            {
                client_id = ClientId,
                code = code,
                redirect_uri = "https://your-app.com/callback",
                code_verifier = codeVerifier,
                grant_type = "authorization_code"
            });

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
        await _localStorage.RemoveItemAsync("code_verifier");
    }
}

public class TokenResponse
{
    public string AccessToken { get; set; }
}

public class DeviceCodeResponse
{
    public string UserCode { get; set; }
    public string VerificationUri { get; set; }
    public int ExpiresIn { get; set; }
    public int Interval { get; set; }
}
