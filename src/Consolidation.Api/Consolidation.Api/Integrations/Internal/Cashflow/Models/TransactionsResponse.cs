namespace Consolidation.Api.Integrations.Internal.Cashflow.Models
{
    public class TransactionsResponse
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public int Type { get; set; }
    }
}
