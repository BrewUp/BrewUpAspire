using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var salesApi = builder.AddProject<BrewUp_Sales_Rest>("BrewUp_Sales_Rest");

await builder.Build().RunAsync();