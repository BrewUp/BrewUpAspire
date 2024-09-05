var builder = DistributedApplication.CreateBuilder(args);

var brewUpSalesRest = builder.AddProject<Projects.BrewUp_Sales>("brewup-sales");
var brewUpSalesDomain = builder.AddProject<Projects.BrewUp_Sales_Domain>("brewup-sales-domain");
var brewUpSalesReadModel = builder.AddProject<Projects.BrewUp_Sales_ReadModel>("brewup-sales-readmodel");

builder.AddProject<Projects.ResilienceBlazor>("brewUp-app")
	.WithReference(brewUpSalesRest)
	.WithReference(brewUpSalesDomain)
	.WithReference(brewUpSalesReadModel);

builder.Build().Run();
