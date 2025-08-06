using WebApplication1.Infrastructure;
using WebApplication1.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc.Versioning.Conventions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddRouting();
builder.Services.AddInfrastructure();
builder.Services.AddApplication();
builder.Services.AddLogging();
