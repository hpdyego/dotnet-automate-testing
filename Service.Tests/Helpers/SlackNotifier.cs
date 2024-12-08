using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Service.Tests.Helpers;

public class SlackNotifier
{
    private readonly string _webhookUrl;

    public SlackNotifier(string webhookUrl)
    {
        _webhookUrl = webhookUrl;
    }

    public async Task SendMessageAsync(string message, List<string> list)
    {
        using var httpClient = new HttpClient();
        var payload = new { text = message };
        var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync(_webhookUrl, content);
        response.EnsureSuccessStatusCode();
    }
}
