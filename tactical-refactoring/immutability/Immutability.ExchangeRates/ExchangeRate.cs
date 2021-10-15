using System;

namespace Immutability.ExchangeRates
{
    public sealed class ExchangeRate
    {
        private Currency _from;
        private Currency _to;
        private decimal _rate;

        public Currency From
        {
            get => _from;
            init => _from = value;
        }

        public Currency To
        {
            get => _to;
            init => _to = value;
        }

        public decimal Rate
        {
            get => _rate;
            init => _rate = value;
        }

        public ExchangeRate WithRate(decimal newRate)
        {
            return new() {From = From, To = To, Rate = newRate};
        }

        public Money Convert(Money money)
        {
            if (money.Currency != _from)
            {
                throw new ArgumentException(
                    $"Exchange rate from {_from} to {_to} was asked to convert from {money.Currency}", nameof(money));
            }

            return new Money(_to, money.Amount * _rate);
        }
    }
}
