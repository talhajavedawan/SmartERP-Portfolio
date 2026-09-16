using SmartERP.Showcase;
using Xunit;

namespace SmartERP.Showcase.Tests;

public sealed class CashFlowSummaryServiceTests
{
    [Fact]
    public void Calculate_ReturnsPeriodTotalsAndClosingBalance()
    {
        var movements = new[]
        {
            new CashMovement(new DateOnly(2026, 1, 2), 1_200m, "Receipt"),
            new CashMovement(new DateOnly(2026, 1, 5), -350m, "Supplier payment"),
            new CashMovement(new DateOnly(2026, 2, 1), 9_999m, "Outside period")
        };

        var result = new CashFlowSummaryService().Calculate(
            openingBalance: 500m,
            movements,
            new DateOnly(2026, 1, 1),
            new DateOnly(2026, 1, 31));

        Assert.Equal(1_200m, result.Inflow);
        Assert.Equal(350m, result.Outflow);
        Assert.Equal(1_350m, result.ClosingBalance);
    }
}
