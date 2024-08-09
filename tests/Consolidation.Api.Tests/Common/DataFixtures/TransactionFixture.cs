using Consolidation.Api.Integrations.Internal.Cashflow.Models;

namespace Consolidation.Api.Tests.Common.DataFixtures
{
    public static class TransactionFixture
    {
        public static IList<TransactionsResponse> GetTransactionsResponsesFake(int count)
        {
            var transacrionsResponseFake = Fixture.Get<TransactionsResponse>()
            .RuleFor(x => x.Id, (fk, _) => fk.Random.Int())
            .RuleFor(x => x.Description, (fk, _) => fk.Random.Words(2))
            .RuleFor(x => x.Amount, (fk, _) => fk.Random.Decimal())
            .RuleFor(x => x.Date, (fk, _) => DateTime.Now)
            .RuleFor(x => x.Type, (fk, _) => fk.Random.Int(1, 2))
            .Generate(count);

            return transacrionsResponseFake;
        }
    }
}
