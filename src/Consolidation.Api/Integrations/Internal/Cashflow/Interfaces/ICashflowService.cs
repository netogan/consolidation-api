using Consolidation.Api.Integrations.Internal.Cashflow.Models;

namespace Consolidation.Api.Integrations.Internal.Cashflow.Interfaces
{
    public interface ICashflowService
    {
        Task<IEnumerable<TransactionsResponse>> GetTransactions(DateOnly createAt);
    }
}
