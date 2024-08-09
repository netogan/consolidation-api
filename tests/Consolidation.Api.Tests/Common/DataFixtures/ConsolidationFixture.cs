namespace Consolidation.Api.Tests.Common.DataFixtures
{
    public static class ConsolidationFixture
    {
        public static IList<Api.Domain.Models.Consolidation> GenerateConsolidationFake(int count)
        {
            var consolidation = Fixture.Get<Api.Domain.Models.Consolidation>()
                .RuleFor(x => x.Id, (fk, _) => fk.Random.Int())
                .RuleFor(x => x.TotalRevenue, (fk, _) => fk.Random.Decimal())
                .RuleFor(x => x.TotalExpense, (fk, _) => fk.Random.Decimal())
                .RuleFor(x => x.OpeningBalance, (fk, _) => fk.Random.Decimal())
                .RuleFor(x => x.FinalBalance, (fk, _) => fk.Random.Decimal())
                .RuleFor(x => x.DateCreateAt, DateOnly.FromDateTime(DateTime.Now))
                .RuleFor(x => x.HourCreateAt, TimeOnly.FromDateTime(DateTime.Now))
                .RuleFor(x => x.HourUpdateAt, TimeOnly.FromDateTime(DateTime.Now))
                .Generate(count);

            return consolidation;
        }
    }
}
