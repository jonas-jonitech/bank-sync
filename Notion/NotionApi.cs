using System.Net.Http.Headers;
using System.Net.Http.Json;
using bank_sync.Core;
using bank_sync.GoCardless.models;
using bank_sync.Notion.models;
using Microsoft.Extensions.Configuration;

namespace bank_sync.Notion;

public class NotionApi(IConfiguration configuration, HttpClient http)
{
    private readonly string _apiKey = configuration["Notion:ApiKey"] ?? throw new ArgumentNullException("Notion:ApiKey");
    private readonly string _databaseId = configuration["Notion:DatabaseId"] ?? throw new ArgumentNullException("Notion:DatabaseId");
    private readonly string _version = configuration["Notion:Version"] ?? throw new ArgumentNullException("Notion:Version");

    public async Task<List<Core.Transaction>> GetItems(CancellationToken cancellationToken = default) 
    {
        // Build request
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri($"{http.BaseAddress}v1/databases/{_databaseId}/query")
        };

        // Set headers
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
        request.Headers.Add("Notion-Version", _version);

        // Send request
        var response = await http.SendAsync(request, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Something went wrong");
        }

        var result = await response.Content.ReadFromJsonAsync<PageList>(cancellationToken);

        return result!.Results.Select(r => new Core.Transaction
        {
            Type = r.Properties.Inflow.Number.HasValue 
                ? TransactionType.Income 
                : TransactionType.Expense,
            Payee = r.Properties.Payee.RichText[0].Text.Content,
            Amount = r.Properties.Inflow.Number.HasValue
                ? (decimal)r.Properties.Inflow.Number.Value
                : (decimal)r.Properties.Outflow.Number.GetValueOrDefault(),
            Date = DateOnly.Parse(r.Properties.Date.Date.Start)
        }).ToList();
    }

    public async Task AddItems(List<Core.Transaction> items, CancellationToken cancellationToken = default)
    {
        // Build the request
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri($"{http.BaseAddress}v1/pages")
        };

        // Set headers
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
        request.Headers.Add("Notion-Version", _version);

        foreach (var item in items)
        {
            var content = new PageRequest
            {
                parent = new Parent
                {
                    DatabaseId = _databaseId
                },
                Properties = new PageProperties
                {
                    Id = new TitleProperty
                    {
                        Title = [
                            new RichText
                            {
                                Text = new Text
                                {
                                    Content = Guid.NewGuid().ToString()
                                }
                            }
                        ]
                    },
                    Date = new DateProperty
                    {
                        Date = new Date
                        {
                            Start = item.Date.ToString()
                        }
                    },
                    Payee = new RichTextProperty
                    {
                        RichText = [
                            new RichText
                            {
                                Text = new Text
                                {
                                    Content = item.Payee
                                }
                            }
                        ]
                    },
                    Inflow = item.Type is TransactionType.Income ? new NumberProperty
                    {
                        Number = 
                    }
                }
            }
        }
    }
}
