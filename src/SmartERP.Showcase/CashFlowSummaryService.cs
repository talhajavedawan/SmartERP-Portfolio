namespace SmartERP.Showcase;

public sealed record CashMovement(DateOnly Date, decimal Amount, string Category);

public sealed record CashFlowSummary(decimal OpeningBalance, decimal Inflow, decimal Outflow)
{
    public decimal ClosingBalance => OpeningBalance + Inflow - Outflow;
}

public sealed class CashFlowSummaryService
{
    public CashFlowSummary Calculate(
        decimal openingBalance,
        IEnumerable<CashMovement> movements,
        DateOnly from,
        DateOnly to)
    {
        ArgumentNullException.ThrowIfNull(movements);

        if (to < from)
        {
            throw new ArgumentException("The end date cannot be earlier than the start date.");
        }

        var periodMovements = movements.Where(x => x.Date >= from && x.Date <= to).ToArray();
        var inflow = periodMovements.Where(x => x.Amount > 0).Sum(x => x.Amount);
        var outflow = Math.Abs(periodMovements.Where(x => x.Amount < 0).Sum(x => x.Amount));

        return new CashFlowSummary(openingBalance, inflow, outflow);
    }
}
