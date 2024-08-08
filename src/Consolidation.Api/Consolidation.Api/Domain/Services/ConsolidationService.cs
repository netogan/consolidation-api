using Consolidation.Api.Data.Repositories.Interfaces;
using Consolidation.Api.Domain.Models;
using Consolidation.Api.Domain.Services.Interfaces;
using Consolidation.Api.Integrations.Internal.Cashflow.Interfaces;

namespace Consolidation.Api.Domain.Services
{
    public class ConsolidationService : IConsolidationService
    {
        private readonly ICashflowService _cashflowService;
        private readonly ILogger<ConsolidationService> _logger;
        private readonly IConsolidationRepository _consolidationRepository;
        public ConsolidationService(ICashflowService cashflowService, ILogger<ConsolidationService> logger, IConsolidationRepository consolidationRepository)
        {
            _cashflowService = cashflowService;
            _logger = logger;
            _consolidationRepository = consolidationRepository;
        }

        public async Task<bool> ProcessConsolidation(DateOnly dateProcess)
        {
            try
            {
                Models.Consolidation consolidation = (await _consolidationRepository.GetConsolidationByRange(dateProcess, dateProcess)).FirstOrDefault();

                var alreadyConsoliationExists = false;

                if (consolidation == null)
                    alreadyConsoliationExists = false;
                else
                    alreadyConsoliationExists = true;

                decimal ? previousFinalBalance = (await _consolidationRepository.GetConsolidationByRange(dateProcess.AddDays(-1), dateProcess.AddDays(-1)))
                    .FirstOrDefault()?.FinalBalance;

                var transactions = await _cashflowService.GetTransactions(dateProcess);

                var totalRevenue = transactions.Where(e => e.Type == 1).Sum(e => e.Amount);
                var totalExpense = transactions.Where(e => e.Type == 2).Sum(e => e.Amount);

                if (alreadyConsoliationExists) 
                {
                    consolidation.HourUpdateAt = TimeOnly.FromDateTime(DateTime.Now);
                }
                else
                {
                    consolidation = new()
                    {
                        DateCreateAt = dateProcess,
                        HourCreateAt = TimeOnly.FromDateTime(DateTime.Now),
                        HourUpdateAt = TimeOnly.FromDateTime(DateTime.Now)
                    };
                }

                consolidation.TotalRevenue = totalRevenue;
                consolidation.TotalExpense = totalExpense;
                consolidation.OpeningBalance = previousFinalBalance.GetValueOrDefault();
                consolidation.FinalBalance = consolidation.OpeningBalance + (totalRevenue - (-totalExpense));

                if(alreadyConsoliationExists)
                    await _consolidationRepository.UpdateConsolidation(consolidation);
                else 
                    await _consolidationRepository.AddConsolidation(consolidation);
    

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error in process consolidation at {dateProcess} with message: {ex.Message}");
                return false;
            }
        }

        public async Task<IEnumerable<Models.Consolidation>> GetConsolidationByRange(DateOnly initialDate, DateOnly finalDate)
            => await _consolidationRepository.GetConsolidationByRange(initialDate, finalDate);
    }
}
