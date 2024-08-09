namespace Consolidation.Api.Data.Repositories.Interfaces
{
    public interface IConsolidationRepository
    {
        Task<Domain.Models.Consolidation> AddConsolidation(Domain.Models.Consolidation consolidation);
        Task<Domain.Models.Consolidation> UpdateConsolidation(Domain.Models.Consolidation consolidation);
        Task<Domain.Models.Consolidation> GetConsolidation(int id);
        Task<IEnumerable<Domain.Models.Consolidation>> GetConsolidationByRange(DateOnly initialDate, DateOnly finalDate);
    }
}
