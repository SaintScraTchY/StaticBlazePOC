using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace StaticBlazePOC.Pages;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

public class GitHubService
{
    private readonly HttpClient _http;

    public GitHubService(HttpClient http)
    {
        _http = http;
    }

    public async Task PushMarkdownFile(string repoOwner, string repoName, string path, string content, string? pat)
    {
        var url = $"https://api.github.com/repos/{repoOwner}/{repoName}/contents/{path}";
        var base64Content = Convert.ToBase64String(Encoding.UTF8.GetBytes(content));

        var payload = new
        {
            message = $"Add {path}",
            content = base64Content,
            branch = "main"
        };

        // Add GitHub API headers
        _http.DefaultRequestHeaders.Clear();
        _http.DefaultRequestHeaders.Add("Authorization", $"Bearer {pat}");
        _http.DefaultRequestHeaders.Add("User-Agent", "BlazorGitPOC");

        var response = await _http.PutAsJsonAsync(url, payload);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Failed to push file: {error}");
            
        }
    }

    public async Task PushImageFile(string repoOwner, string repoName, string path, byte[] imageBytes, string? pat)
    {
        var url = $"https://api.github.com/repos/{repoOwner}/{repoName}/contents/{path}";
        var base64Content = Convert.ToBase64String(imageBytes);

        var payload = new
        {
            message = $"Add {path}",
            content = base64Content,
            branch = "main"
        };

        // Add GitHub API headers
        _http.DefaultRequestHeaders.Clear();
        _http.DefaultRequestHeaders.Add("Authorization", $"Bearer {pat}");
        _http.DefaultRequestHeaders.Add("User-Agent", "BlazorGitPOC");

        var response = await _http.PutAsJsonAsync(url, payload);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Failed to push image: {error}");
        }
    }
}