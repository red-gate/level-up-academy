using System;

namespace AccountCalculator.Domain
{
    /// <summary>
    /// Represents a currency, characterised by a three letter <see cref="CurrencyCode"/>, such as GBP or USD.
    /// </summary>
    /// <param name="CurrencyCode">The three letter currency code of this currency, such as GBP or USD.</param>
    public record Currency(string CurrencyCode)
    {
        public override string ToString() => CurrencyCode;
    }
}
