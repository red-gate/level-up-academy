using System;
using System.Collections.Concurrent;
using System.Linq;

namespace Immutability.ExchangeRates
{
    public sealed class Currency
    {
        private static readonly ConcurrentDictionary<string, Currency> Cache =
            new ConcurrentDictionary<string, Currency>();

        public string Code { get; }

        private Currency(string code)
        {
            Code = code;
        }

        public static Currency FromCode(string code)
        {
            if (code == null)
            {
                throw new ArgumentNullException(nameof(code));
            }

            if (code.Length != 3 || !code.All(c => c is >= 'A' and <= 'Z'))
            {
                throw new ArgumentException($"'{code}' is not a valid currency code; currency codes must be 3 capital letters", nameof(code));
            }

            return Cache.GetOrAdd(code, c => new Currency(c));
        }

        public override string ToString()
        {
            return Code;
        }
    }
}
