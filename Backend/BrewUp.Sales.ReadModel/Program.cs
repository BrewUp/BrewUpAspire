using BrewUp.Sales.ReadModel;
using BrewUp.Shared;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddShared();
builder.Services.AddHostedService<Worker>();

builder.AddAzureServiceBusClient("ServiceBusConnection");

var host = builder.Build();
await host.RunAsync();
