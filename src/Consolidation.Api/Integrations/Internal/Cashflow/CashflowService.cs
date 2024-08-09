using Consolidation.Api.Integrations.Internal.Cashflow.Interfaces;
using Consolidation.Api.Integrations.Internal.Cashflow.Models;
using RestSharp;

namespace Consolidation.Api.Integrations.Internal.Cashflow
{
    public class CashflowService(IConfiguration configuration, RestClient client) : ICashflowService
    {
        public async Task<IEnumerable<TransactionsResponse>> GetTransactions(DateOnly createAt)
        {
            var baseUrl = configuration.GetSection("Cashflow:UrlBase").Value;

            var request = new RestRequest($"{baseUrl}/api/internal/transactions", Method.Get).AddQueryParameter("createAt", createAt);

            var transactions = new List<TransactionsResponse>();

            transactions = await client.GetAsync<List<TransactionsResponse>>(request);

            return transactions!;
        }
    }
}
