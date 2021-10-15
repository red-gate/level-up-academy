using System;
using System.Collections.Generic;

namespace Immutability.ExchangeRates
{
    public sealed class CurrencyExchange
    {
        private readonly Dictionary<(Currency from, Currency to), ExchangeRate> _exchangeRates =
            new Dictionary<(Currency from, Currency to), ExchangeRate>();

        public void UpdateExchangeRate(Currency from, Currency to, decimal rate)
        {
            if (_exchangeRates.TryGetValue((from, to), out var exchangeRate))
            {
                _exchangeRates[(from, to)] = exchangeRate.WithRate(rate);
            }
            else
            {
                _exchangeRates.Add((from, to), new ExchangeRate(from, to, rate));
            }
        }

        public Money Exchange(Money from, Currency to)
        {
            if (!_exchangeRates.TryGetValue((from.Currency, to), out var exchangeRate))
            {
                throw new InvalidOperationException($"No exchange rate found for {from.Currency} to {to}");
            }

            return exchangeRate.Convert(from);
        }

        public IEnumerable<ExchangeRate> GetCurrentRates()
        {
            return _exchangeRates.Values;
        }
    }
}
