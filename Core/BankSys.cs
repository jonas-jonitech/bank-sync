using bank_sync.GoCardless;

namespace bank_sync.Core;

public class BankSys
{
    private static readonly DateOnly START_DATE = DateOnly.FromDateTime(new DateTime(2024, 1, 1));

    private readonly GoCardlessApi _goCardless = new();

    public async Task Run() 
    {
        await _goCardless.FetchTransactions(START_DATE, DateOnly.FromDateTime(DateTime.Today));
    }
}
