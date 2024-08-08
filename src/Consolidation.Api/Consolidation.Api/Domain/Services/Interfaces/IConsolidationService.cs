namespace Consolidation.Api.Domain.Services.Interfaces
{
    public interface IConsolidationService
    {
        Task<bool> ProcessConsolidation(DateOnly dateProcess);
        Task<IEnumerable<Models.Consolidation>> GetConsolidationByRange(DateOnly initialDate, DateOnly finalDate);
    }
}
