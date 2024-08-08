namespace Consolidation.Api.Domain.Models
{
    public class Consolidation
    {
        public int Id { get; set; }
        //Total de receitas
        public decimal TotalRevenue { get; set; }
        //Total de despesas
        public decimal TotalExpense { get; set; }
        //Saldo inicial
        public decimal OpeningBalance { get; set; }
        //Saldo final
        public decimal FinalBalance { get; set; }
        public DateOnly DateCreateAt { get; set; }
        public TimeOnly HourCreateAt { get; set; }
        public TimeOnly HourUpdateAt { get; set; }
    }
}
