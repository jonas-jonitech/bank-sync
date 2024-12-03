using System.Net.Http.Headers;
using bank_sync.Core;
using Microsoft.Extensions.Configuration;

namespace bank_sync.Notion;

public class NotionApi(IConfiguration configuration, HttpClient http)
{
    private readonly string _apiKey = configuration["Notion:ApiKey"] ?? throw new ArgumentNullException("Notion:ApiKey");
    private readonly string _databaseId = configuration["Notion:DatabaseId"] ?? throw new ArgumentNullException("Notion:DatabaseId");
    private readonly string _version = configuration["Notion:Version"] ?? throw new ArgumentNullException("Notion:Version");

    public async Task<List<Transaction>> GetItems(CancellationToken cancellationToken = default) 
    {
        // Build request
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"{http.BaseAddress}v1/databases/{_databaseId}")
        };

        // Set headers
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
        request.Headers.Add("Notion-Version", _version);

        // Send request
        var response = await http.SendAsync(request, cancellationToken);

        return await Task.FromResult<List<Transaction>>([]);
    }
}
