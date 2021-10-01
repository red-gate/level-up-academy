using System;

namespace AccountCalculator.Domain
{
    public record Purchase(DateTimeOffset Timestamp, string Description, decimal Cost, string Currency);
}
