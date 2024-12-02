using System.Net.Http.Json;
using Azure;
using bank_sync.Core;
using bank_sync.GoCardless.models;
using bank_sync.GoCardless.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;

namespace bank_sync.GoCardless;

public class GoCardlessApi(IConfiguration configuration)
{
    private readonly HttpClient _http = new()
    {
        BaseAddress = new Uri("https://bankaccountdata.gocardless.com")
    };

    private readonly string _secretId = configuration["GoCardless:SecretId"] ?? throw new ArgumentNullException("GoCardless:SecretId");
    private readonly string _secretKey = configuration["GoCardless:SecretKey"] ?? throw new ArgumentNullException("GoCardless:SecretKey");
    private readonly string _accountId = configuration["GoCardless:AccountId"] ?? throw new ArgumentNullException("GoCardless:AccountId");

    /// <summary>
    /// Fetches transactions for an account within the provided date range.
    /// </summary>
    /// <param name="account">Account ID</param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    public async Task<List<Core.Transaction>> FetchTransactions(DateOnly from, DateOnly to, CancellationToken cancellationToken = default) 
    {
        var token = await FetchToken(cancellationToken);
        _http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.AccessToken);

        var query = $"api/v2/accounts/{_accountId}/transactions?from={from:yyyy-MM-dd}&to={to:yyyy-MM-dd}";

        var response = await _http.GetAsync(query, cancellationToken);
        var result = await response.Content.ReadFromJsonAsync<TransactionResponse>()!;

        return result!.Transactions.Booked.Select(t => new Core.Transaction() 
        {
            Amount = t.TransactionAmount.Amount,
            Type = t.TransactionAmount.Amount < 0 
                ? TransactionType.Expense 
                : TransactionType.Income,
            Payee = t.TransactionAmount.Amount < 0
                ? t.CreditorName
                : t.DebitorName,
            Date = t.ValueDate
        }).ToList();
    }

    private async Task<TokenResponse> FetchToken(CancellationToken cancellationToken = default) 
    {
        var response = await _http.PostAsJsonAsync("api/v2/token/new/", new TokenRequest 
        {
            SecretId = _secretId,
            SecretKey = _secretKey
        }, cancellationToken);

        if (!response.IsSuccessStatusCode) 
        {
            throw new AuthenticationFailureException(await response.Content.ReadAsStringAsync(cancellationToken));
        }

        return (await response.Content.ReadFromJsonAsync<TokenResponse>(cancellationToken))!;
    }
}
