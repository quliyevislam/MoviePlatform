using Microsoft.AspNetCore.Mvc;
using MoviePlatform.Application;
using MoviePlatform.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllers();
// Determines if the filter that returns an BadRequestObjectResult when ModelState is invalid
builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
builder.Services.AddSingleton(TimeProvider.System);

var app = builder.Build();

app.MapHealthChecks("/health");
app.MapControllers();
app.Run();
