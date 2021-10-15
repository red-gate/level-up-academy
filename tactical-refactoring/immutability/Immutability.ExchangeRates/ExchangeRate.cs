using System;

namespace Immutability.ExchangeRates
{
    public sealed class ExchangeRate
    {
        public Currency From { get; init; }

        public Currency To { get; init; }

        public decimal Rate { get; set; }

        public void UpdateRate(decimal newRate)
        {
            Rate = newRate;
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
