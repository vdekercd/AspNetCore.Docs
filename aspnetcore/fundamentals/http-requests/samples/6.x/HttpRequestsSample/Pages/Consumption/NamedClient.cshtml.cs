using System.Text.Json;
using HttpRequestsSample.GitHub;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HttpRequestsSample.Pages;

// <snippet_Class>
public class NamedClientModel : PageModel
{
    private readonly HttpClient _gitHubHttpClient;

    public NamedClientModel(IHttpClientFactory httpClientFactory) =>
        _gitHubHttpClient = httpClientFactory.CreateClient("GitHub");

    public IEnumerable<GitHubBranch>? GitHubBranches { get; set; }

    public async Task OnGet()
    {
        var httpResponseMessage = await _gitHubHttpClient.GetAsync(
            "repos/dotnet/AspNetCore.Docs/branches");

        if (httpResponseMessage.IsSuccessStatusCode)
        {
            using var contentStream =
                await httpResponseMessage.Content.ReadAsStreamAsync();
            
            GitHubBranches = await JsonSerializer.DeserializeAsync
                <IEnumerable<GitHubBranch>>(contentStream);
        }
    }
}
// </snippet_Class>
