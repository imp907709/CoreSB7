using CoreSBBL;
using CoreSBShared.Registrations;
using CoreSBShared.Universal.Checkers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.DefaultRegistrations();

builder.RegisterConnections();

builder.RegisterContexts();
builder.RegisterContextsBL();

var app = builder.Build();

app.Registration();

app.Run();
