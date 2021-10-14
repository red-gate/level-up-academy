using System;

namespace Immutability.ExchangeRates
{
    public sealed class Money
    {
        public Currency Currency { get; set; }
        public decimal Amount { get; set; }

        public Money(Currency currency, decimal amount)
        {
            Currency = currency;
            Amount = amount;
        }

        private bool Equals(Money other)
        {
            return Currency.Equals(other.Currency) && Amount == other.Amount;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is Money other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Currency, Amount);
        }

        public static bool operator ==(Money left, Money right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Money left, Money right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return Currency.Code + '\u00A0' + Amount;
        }
    }
}
