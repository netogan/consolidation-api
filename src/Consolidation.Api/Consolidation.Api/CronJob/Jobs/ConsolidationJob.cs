using Consolidation.Api.Domain.Services.Interfaces;
using Quartz;

namespace Consolidation.Api.CronJob.Jobs
{
    public class ConsolidationJob : IJob
    {
        private readonly IConsolidationService _consolidationService;

        public ConsolidationJob(IConsolidationService consolidationService)
        {
            _consolidationService = consolidationService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var dateProcessing = DateOnly.FromDateTime(DateTime.Now);

            var result = await _consolidationService.ProcessConsolidation(dateProcessing);
        }
    }
}
