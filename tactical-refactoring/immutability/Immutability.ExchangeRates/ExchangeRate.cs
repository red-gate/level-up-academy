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
            set => _from = value;
        }

        public Currency To
        {
            get => _to;
            set => _to = value;
        }

        public decimal Rate
        {
            get => _rate;
            set => _rate = value;
        }

        public void UpdateRate(decimal newRate)
        {
            _rate = newRate;
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

        public void Swap()
        {
            var newFrom = _to;
            var newTo = _from;
            _from = newFrom;
            _to = newTo;
            _rate = 1m / _rate;
        }
    }
}
