using System;
using System.Collections.Generic;

namespace Immutability.ExchangeRates
{
    public sealed class CurrencyExchange
    {
        private readonly Dictionary<(Currency from, Currency to), ExchangeRate> _exchangeRates;
        public CurrencyExchange WithExchangeRate(Currency from, Currency to, decimal rate)
        {
            var newExchangeRate = new Dictionary<(Currency from, Currency to), ExchangeRate>(_exchangeRates)
            {
                [(from, to)] = new ExchangeRate {From = from, To = to, Rate = rate}
            };

            return new CurrencyExchange(newExchangeRate);
        }

        private CurrencyExchange(Dictionary<(Currency from, Currency to), ExchangeRate> exchangeRate)
        {
            _exchangeRates = exchangeRate;
        }

        public CurrencyExchange()
        {
            _exchangeRates = new Dictionary<(Currency from, Currency to), ExchangeRate>();
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
