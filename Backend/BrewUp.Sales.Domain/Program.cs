using BrewUp.Sales.Domain;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.AddAzureServiceBusClient("serviceBusConnection");

var host = builder.Build();
host.Run();