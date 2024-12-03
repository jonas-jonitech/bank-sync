using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using bank_sync.Core;
using bank_sync.GoCardless.models;
using bank_sync.GoCardless.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;

namespace bank_sync.GoCardless;

public class GoCardlessApi(IConfiguration configuration, HttpClient http)
{
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
        // Request an access token
        await Authenticate(cancellationToken);

        // Build the request
        var request = new HttpRequestMessage 
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"{http.BaseAddress}api/v2/accounts/{_accountId}/transactions/?date_from={from:yyyy-MM-dd}&date_to={to:yyyy-MM-dd}")
        };

        // Set the Authorization header
        request.Headers.Authorization = http.DefaultRequestHeaders.Authorization;

        // Send the request
        var response = await http.SendAsync(request, cancellationToken);

        // Handle a failed response
        if (!response.IsSuccessStatusCode) 
        {
            throw new Exception($"Transaction fetching failed: {await response.Content.ReadAsStringAsync(cancellationToken)}");
        }

        // Read the successful response
        var result = await response.Content.ReadFromJsonAsync<TransactionResponse>(cancellationToken)!;

        return result!.Transactions.Booked.Select(t => new Core.Transaction
        {
            Amount = t.TransactionAmount.Amount,
            Type = t.TransactionAmount.Amount < 0 
                ? TransactionType.Expense 
                : TransactionType.Income,
            Payee = t.TransactionAmount.Amount < 0
                ? t.CreditorName
                : t.DebtorName,
            Date = t.ValueDate
        }).ToList();
    }

    private async Task Authenticate(CancellationToken cancellationToken = default) 
    {
        var response = await http.PostAsJsonAsync("api/v2/token/new/", new TokenRequest 
        {
            SecretId = _secretId,
            SecretKey = _secretKey
        }, cancellationToken);

        if (!response.IsSuccessStatusCode) 
        {
            throw new AuthenticationFailureException(await response.Content.ReadAsStringAsync(cancellationToken));
        }

        var result = (await response.Content.ReadFromJsonAsync<TokenResponse>(cancellationToken))!;
        http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);
    }
}
