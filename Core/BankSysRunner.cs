using bank_sync.GoCardless;
using bank_sync.Notion;

namespace bank_sync.Core;

public class BankSysRunner(GoCardlessApi goCardless, NotionApi notion)
{
    private static readonly DateOnly START_DATE = DateOnly.FromDateTime(new DateTime(2024, 1, 1));

    public async Task Run() 
    {
        // Fetch all transactions from GoCardless
        var transactionsToSync = await goCardless.FetchTransactions(START_DATE, DateOnly.FromDateTime(DateTime.Today));    

        // Fetch all transactions already synced to Notion
        var syncedTransactions = await notion.GetItems();

        // Match already synced transactions and remove them from the toSync list
        foreach (var synced in syncedTransactions)
        {
            var match = transactionsToSync.Find(t => t.Amount == synced.Amount && t.Date == synced.Date && t.Payee == synced.Payee);
            if (match != null)
            {
                // already synced
                transactionsToSync.Remove(match);
            }
        }

        // Add toSync to Notion
        await notion.AddItems(transactionsToSync);
    }
}
