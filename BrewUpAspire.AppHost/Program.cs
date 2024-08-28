var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.ResilienceBlazor>("brewapp");

builder.AddProject<Projects.BrewUp_Sales>("brewup-sales");

await builder.Build().RunAsync();
