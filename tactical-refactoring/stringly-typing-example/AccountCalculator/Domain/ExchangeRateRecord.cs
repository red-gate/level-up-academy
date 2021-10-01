namespace AccountCalculator.Domain
{
    /// <summary>
    /// Represents an exchange rate offer made over a specific period of time.
    /// </summary>
    /// <param name="Currency">The currency being converted from.</param>
    /// <param name="ConversionRate">The conversion rate. This represents how much of the <paramref name="Currency"/>
    /// can be converted to 1 pound sterling (GBP).</param>
    /// <param name="Start">The start of the period of time the exchange rate applies.</param>
    /// <param name="End">The end of the period of time the exchange rate applies.</param>
    public record ExchangeRateRecord(
        Currency Currency,
        decimal ConversionRate,
        UtcDateTime Start,
        UtcDateTime End);
}
