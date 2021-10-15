using System;

namespace Immutability.ExchangeRates
{
    public sealed class ExchangeRate
    {
        public Currency From { get; set; }

        public Currency To { get; set; }

        public decimal Rate { get; set; }

        public void UpdateRate(decimal newRate)
        {
            Rate = newRate;
        }

        public void Convert(Money money)
        {
            if (money.Currency != From)
            {
                throw new ArgumentException(
                    $"Exchange rate from {From} to {To} was asked to convert from {money.Currency}", nameof(money));
            }

            money.Currency = To;
            money.Amount *= Rate;
        }

        public void Swap()
        {
            var newFrom = To;
            var newTo = From;
            From = newFrom;
            To = newTo;
            Rate = 1m / Rate;
        }
    }
}
