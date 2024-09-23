using BrewUp.Sales.Modules;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddAzureServiceBusClient("ServiceBusConnection");

builder.Services.AddProblemDetails();

// Register Modules
builder.RegisterModules();

var app = builder.Build();

app.UseExceptionHandler();

app.MapDefaultEndpoints();

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();
//app.UseSalesFailureMiddleware();

// Register endpoints
app.MapEndpoints();

// Configure the HTTP request pipeline.
app.UseSwagger(s =>
{
	s.RouteTemplate = "documentation/{documentName}/documentation.json";
});
app.UseSwaggerUI(s =>
{
	s.SwaggerEndpoint("/documentation/v1/documentation.json", "BrewUp Sales Api");
	s.RoutePrefix = "documentation";
});

await app.RunAsync();