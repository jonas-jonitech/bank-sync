using Microsoft.Extensions.DependencyInjection;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.Hosting;
using bank_sync.GoCardless;
using bank_sync.Core;
using bank_sync.Notion;

var builder = FunctionsApplication.CreateBuilder(args);

builder.Services.AddSingleton<BankSysRunner>();
builder.Services.AddHttpClient<GoCardlessApi>(http => {
    http.BaseAddress = new Uri("https://bankaccountdata.gocardless.com/");
});
builder.Services.AddHttpClient<NotionApi>(http => {
    http.BaseAddress = new Uri("https://api.notion.com/");
});
    

builder.ConfigureFunctionsWebApplication();
builder.Build().Run();
