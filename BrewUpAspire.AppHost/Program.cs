var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.BrewUp_Sales>("brewup-sales");

builder.Build().Run();
