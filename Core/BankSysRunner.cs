using bank_sync.GoCardless;

namespace bank_sync.Core;

public class BankSysRunner(GoCardlessApi goCardless)
{
    private static readonly DateOnly START_DATE = DateOnly.FromDateTime(new DateTime(2024, 1, 1));

    public async Task Run() 
    {
        await goCardless.FetchTransactions(START_DATE, DateOnly.FromDateTime(DateTime.Today));
    }
}
