using Consolidation.Api.CronJob.Extension;
using Consolidation.Api.Data.Context;
using Consolidation.Api.Data.Repositories;
using Consolidation.Api.Data.Repositories.Interfaces;
using Consolidation.Api.Domain.Services;
using Consolidation.Api.Domain.Services.Interfaces;
using Consolidation.Api.Domain.Utils;
using Consolidation.Api.Integrations.Internal.Cashflow;
using Consolidation.Api.Integrations.Internal.Cashflow.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Quartz;
using RestSharp;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IConsolidationService, ConsolidationService>();
builder.Services.AddScoped<IConsolidationRepository, ConsolidationRepository>();
builder.Services.AddScoped<ICashflowService, CashflowService>();
builder.Services.AddTransient<PdfService>();
builder.Services.AddSingleton<RestClient>(new RestClient(new RestClientOptions()));


builder.Services.AddDbContext<Consolidation.Api.Data.Context.ConsolidationContext>(options 
    => options.UseSqlite("Data Source=consolidation.db"));

builder.Services.AddHealthChecks()
    .AddCheck("API Health Check", () => HealthCheckResult.Healthy("API is running"))
    .AddSqlite("Data Source=consolidation.db", name: "SQLite Health Check");


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddQuartz().AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.StartCronSchedulingConfig();

app.UseHttpsRedirection();

app.MapHealthChecks("/api/health");


app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ConsolidationContext>();
    dbContext.Database.Migrate();
}

app.Run();
