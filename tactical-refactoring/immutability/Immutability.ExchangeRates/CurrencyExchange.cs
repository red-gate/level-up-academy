using System;
using System.Collections.Generic;
using System.Linq;

namespace Immutability.ExchangeRates
{
    public sealed class CurrencyExchange
    {
        private readonly Dictionary<(Currency from, Currency to), ExchangeRate> _exchangeRates;

        public CurrencyExchange(IEnumerable<ExchangeRate> exchangeRates)
        {
            _exchangeRates = exchangeRates.ToDictionary(x => (x.From, x.To));
        }

        public Money Exchange(Money from, Currency to)
        {
            if (!_exchangeRates.TryGetValue((from.Currency, to), out var exchangeRate))
            {
                throw new InvalidOperationException($"No exchange rate found for {from.Currency} to {to}");
            }

            return exchangeRate.Convert(from);
        }

        public IEnumerable<ExchangeRate> ExchangeRates => _exchangeRates.Values;
    }
}
