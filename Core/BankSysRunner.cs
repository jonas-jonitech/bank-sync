using bank_sync.GoCardless;
using bank_sync.Notion;

namespace bank_sync.Core;

public class BankSysRunner(GoCardlessApi goCardless, NotionApi notion)
{
    private static readonly DateOnly START_DATE = DateOnly.FromDateTime(new DateTime(2024, 1, 1));

    public async Task Run() 
    {
        // Fetch all transactions from GoCardless
        //var transactions = await goCardless.FetchTransactions(START_DATE, DateOnly.FromDateTime(DateTime.Today));

        // Fetch all transactions already synced to Notion
        var syncedTransactions = await notion.GetItems();

    }
}
