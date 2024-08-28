var builder = DistributedApplication.CreateBuilder(args);

var salesApi = builder.AddProject<Projects.BrewUp_Sales>("brewup-sales");

builder.AddProject<Projects.ResilienceBlazor>("brewapp")
	.WithReference(salesApi)
	.WithExternalHttpEndpoints();

await builder.Build().RunAsync();
