using Projects;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<BrewUp_Sales_Rest>("brewupsalesrest");

// builder.AddProject<Projects.ResilienceBlazor>("brewapp")
//     .WithReference(salesApi)
//     .WithExternalHttpEndpoints();

await builder.Build().RunAsync();