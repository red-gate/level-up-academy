using System;

namespace Immutability.ExchangeRates
{
    public sealed record ExchangeRate(Currency From, Currency To, decimal Rate)
    {
        public ExchangeRate WithRate(decimal newRate)
        {
            return this with {Rate = newRate};
        }

        public Money Convert(Money money)
        {
            if (money.Currency != From)
            {
                throw new ArgumentException(
                    $"Exchange rate from {From} to {To} was asked to convert from {money.Currency}", nameof(money));
            }

            return new Money(To, money.Amount * Rate);
        }
    }
}
