namespace Immutability.ExchangeRates
{
    public sealed record Money(Currency Currency, decimal Amount)
    {
        public override string ToString()
        {
            return Currency.Code + '\u00A0' + Amount;
        }
    }
}