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
using Quartz;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IConsolidationService, ConsolidationService>();
builder.Services.AddScoped<IConsolidationRepository, ConsolidationRepository>();
builder.Services.AddScoped<ICashflowService, CashflowService>();
builder.Services.AddTransient<PdfService>();

builder.Services.AddDbContext<Consolidation.Api.Data.Context.ConsolidationContext>(options 
    => options.UseSqlite("Data Source=consolidation.db"));

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

//app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ConsolidationContext>();
    dbContext.Database.Migrate();
}

app.Run();
