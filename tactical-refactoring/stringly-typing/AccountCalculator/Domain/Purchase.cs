using System;

namespace AccountCalculator.Domain
{
    public record Purchase(DateTimeOffset Timestamp, string Description, decimal Cost, CurrencyCode Currency);
}
