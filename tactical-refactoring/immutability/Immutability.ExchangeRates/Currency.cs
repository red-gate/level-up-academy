using System;
using System.Linq;

namespace Immutability.ExchangeRates
{
    public sealed record Currency(string Code)
    {
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

            return new Currency(code);
        }

        public override string ToString()
        {
            return Code;
        }
    }
}
