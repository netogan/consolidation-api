using Consolidation.Api.Data.Context;
using Consolidation.Api.Data.Repositories.Interfaces;
using Consolidation.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Consolidation.Api.Data.Repositories
{
    public class ConsolidationRepository : IConsolidationRepository
    {
        public readonly ConsolidationContext _context;

        public ConsolidationRepository(ConsolidationContext context)
        {
            _context = context;
        }

        public async Task<Domain.Models.Consolidation> AddConsolidation(Domain.Models.Consolidation consolidation)
        {
            _context.Consolidations.Add(consolidation);
            await _context.SaveChangesAsync();

            return await GetConsolidation(consolidation.Id);
        }

        public async Task<Domain.Models.Consolidation> UpdateConsolidation(Domain.Models.Consolidation consolidation)
        {
            var consolidationExist = await GetConsolidation(consolidation.Id);

            if(consolidationExist != null)
            {
                _context.Entry(consolidationExist).CurrentValues.SetValues(consolidation);
            }

            return await GetConsolidation(consolidation.Id);
        }

        public async Task<Domain.Models.Consolidation> GetConsolidation(int id) 
            => await _context.Consolidations.FirstOrDefaultAsync(u => u.Id == id);

        public async Task<IEnumerable<Domain.Models.Consolidation>> GetConsolidationByRange(DateOnly initialDate, DateOnly finalDate)
            => await _context.Consolidations.Where(u => u.DateCreateAt >= initialDate && u.DateCreateAt <= finalDate).ToListAsync();

    }
}
