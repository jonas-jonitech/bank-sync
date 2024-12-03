using Microsoft.Extensions.DependencyInjection;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.Hosting;
using bank_sync.GoCardless;
using bank_sync.Core;

var builder = FunctionsApplication.CreateBuilder(args);

builder.Services.AddSingleton<BankSysRunner>()
    .AddHttpClient<GoCardlessApi>(http => {
        http.BaseAddress = new Uri("https://bankaccountdata.gocardless.com/");
    });

builder.ConfigureFunctionsWebApplication();
builder.Build().Run();
