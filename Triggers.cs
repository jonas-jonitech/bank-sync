using bank_sync.Core;
using bank_sync.GoCardless;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Jonitech.BankSync;

public class Triggers(ILoggerFactory loggerFactory, IConfiguration configuration)
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<Triggers>();
    private readonly BankSys _bankSys = new();
    private readonly IConfiguration _configuration = configuration;

    [Function("Scheduled")]
    public async Task Run([TimerTrigger("0 * */6 * * *")] TimerInfo myTimer)
    {
        _logger.LogInformation("Scheduled bank sync executed at: {timestamp}", DateTime.Now);
        
        if (myTimer.ScheduleStatus is not null)
        {
            _logger.LogInformation("Next bank sync schedule at: {timestamp}", myTimer.ScheduleStatus.Next);
        }


    }

    [Function("Manual")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        return new OkObjectResult("Welcome to Azure Functions!");
    }
}
