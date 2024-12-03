using bank_sync.Core;
using bank_sync.GoCardless;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Jonitech.BankSync;

public class Triggers(ILoggerFactory loggerFactory, BankSysRunner bankSysRunner)
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<Triggers>();

    [Function("Scheduled")]
    public async Task Scheduled([TimerTrigger("0 0,3,6,9,12,15,18,21 * * *")] TimerInfo myTimer)
    {
        _logger.LogInformation("Scheduled bank sync executed at: {timestamp}", DateTime.Now);
        
        if (myTimer.ScheduleStatus is not null)
        {
            _logger.LogInformation("Next bank sync schedule at: {timestamp}", myTimer.ScheduleStatus.Next);
        }

        await bankSysRunner.Run();
    }

    [Function("Manual")]
    public async Task<ActionResult> Manual([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        await bankSysRunner.Run();

        return new OkResult();
    }
}
