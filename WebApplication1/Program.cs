using WebApplication1.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc.Versioning.Conventions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();

// Add API Versioning
builder.Services.AddApiVersioning(options =>
{
    // Use major version only (1 instead of 1.0)
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;

    // Support both URL path and header-based versioning
    options.ApiVersionReader = ApiVersionReader.Combine(
        new UrlSegmentApiVersionReader(),
        new HeaderApiVersionReader("X-API-Version")
    );

    options.RouteConstraintName = "apiVersion";
});

// Add version controller discovery
builder.Services.AddVersionedApiExplorer(options =>
{
    // Format the version as "v1"
    options.GroupNameFormat = "'v'V";

    // Substitute the version in the controller route
    options.SubstituteApiVersionInUrl = true;
});

// Configure versioning format to use 'v1' instead of 'v1.0'
builder.Services.AddRouting(options =>
{
    // ApiVersion constraint is registered automatically by the versioning package
    // No need to manually register it
});

// Add infrastructure (repositories, services, etc.)
builder.Services.AddInfrastructure();

// Add logging
builder.Services.AddLogging();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();