var builder = DistributedApplication.CreateBuilder(args);

var brewUpSalesDomain = builder.AddProject<Projects.BrewUp_Sales_Domain>("brewup-sales-domain");
//var brewUpSalesReadModel = builder.AddProject<Projects.BrewUp_Sales_ReadModel>("brewup-sales-readmodel");

var brewUpSalesRest = builder.AddProject<Projects.BrewUp_Sales>("brewup-sales")
	.WithReference(brewUpSalesDomain);

builder.AddProject<Projects.ResilienceBlazor>("brewUp-app")
	.WithReference(brewUpSalesRest);

await builder.Build().RunAsync();
