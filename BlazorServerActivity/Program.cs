using BlazorServerActivity;
using BlazorServerActivity.Components;
using Microsoft.AspNetCore.Components.Server.Circuits;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddOpenTelemetry()
    .ConfigureResource(resources => resources.AddService("BlazorServerActivity"))
    .WithTracing(tracing => tracing
        .AddSource("CounterActivity")
        .AddProcessor<BaggageActivityEnricher>()
        // does not work as the processor will be registered as singleton and cannot access scoepd services
        //.AddProcessor<BaggageActivityEnricher>()
        .SetSampler<AlwaysOnSampler>()
        .AddConsoleExporter());

builder.Services.AddScoped<CircuitHandler, MyCircuitHandler>();
builder.Services.AddScoped<CircuitIdContainer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();