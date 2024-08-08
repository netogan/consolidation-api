using Consolidation.Api.Integrations.Internal.Cashflow.Interfaces;
using Consolidation.Api.Integrations.Internal.Cashflow.Models;
using RestSharp;

namespace Consolidation.Api.Integrations.Internal.Cashflow
{
    public class CashflowService(IConfiguration configuration) : ICashflowService
    {
        public async Task<IEnumerable<TransactionsResponse>> GetTransactions(DateOnly createAt)
        {
            var baseUrl = configuration.GetSection("Cashflow:UrlBase").Value;

            var restClient = new RestClient(baseUrl!);
            var request = new RestRequest("/api/internal/transactions", Method.Get).AddQueryParameter("createAt", createAt);

            var transactions = new List<TransactionsResponse>();
            
            transactions = await restClient.GetAsync<List<TransactionsResponse>>(request);

            return transactions!;
        }
    }
}
