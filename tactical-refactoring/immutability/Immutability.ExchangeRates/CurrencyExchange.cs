using System;
using System.Collections;
using System.Collections.Generic;

namespace Immutability.ExchangeRates
{
    public sealed class CurrencyExchange : IEnumerable<ExchangeRate>
    {
        private readonly Dictionary<(Currency from, Currency to), ExchangeRate> _exchangeRates =
            new();

        public void Add(Currency from, Currency to, decimal rate)
        {
            _exchangeRates.Add((from, to), new ExchangeRate(from, to, rate));
        }

        public Money Exchange(Money from, Currency to)
        {
            if (!_exchangeRates.TryGetValue((from.Currency, to), out var exchangeRate))
            {
                throw new InvalidOperationException($"No exchange rate found for {from.Currency} to {to}");
            }

            return exchangeRate.Convert(from);
        }

        public IEnumerator<ExchangeRate> GetEnumerator()
        {
            return _exchangeRates.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _exchangeRates.Values.GetEnumerator();
        }
    }
}
