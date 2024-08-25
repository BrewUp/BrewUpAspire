var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.BrewUp_Sales_Rest>("brewup-sales-rest");
builder.AddProject<Projects.BrewUp_Warehouses_Rest>("brewup-warehouses-rest");

builder.Build().Run();
