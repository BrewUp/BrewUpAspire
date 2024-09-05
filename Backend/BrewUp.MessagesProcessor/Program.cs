using BrewUp.MessagesProcessor;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddHostedService<Worker>();

builder.AddAzureServiceBusClient("serviceBusConnection");

var host = builder.Build();
host.Run();