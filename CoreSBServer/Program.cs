using CoreSBBL;
using CoreSBShared.Registrations;
using CoreSBShared.Universal.Checkers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.FrameworkRegistrations();

builder.RegisterConnections();
builder.RegisterContexts();

builder.RegisterContextsBL();
builder.RegisterServicesBL();

var app = builder.Build();

app.Registration();

app.Run();
