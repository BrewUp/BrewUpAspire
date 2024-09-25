using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var salesApi = builder.AddProject<BrewUp_Sales_Rest>("BrewUp_Sales_Rest");

// builder.AddProject<Projects.ResilienceBlazor>("brewapp")
//     .WithReference(salesApi)
//     .WithExternalHttpEndpoints();

await builder.Build().RunAsync();