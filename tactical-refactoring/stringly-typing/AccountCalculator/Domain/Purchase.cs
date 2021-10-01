namespace AccountCalculator.Domain
{
    public record Purchase(string Timestamp, string Description, decimal Cost, CurrencyCode Currency);
}
