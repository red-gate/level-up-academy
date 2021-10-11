using System;
using NUnit.Framework;

namespace Immutability.ExchangeRates.Tests
{
    [TestFixture]
    public class CurrencyTests
    {
        [TestCase("USD")]
        [TestCase("GBP")]
        public void CurrencyHasCorrectCode(string code)
        {
            var currency = Currency.FromCode(code);
            Assert.That(currency.Code, Is.EqualTo(code));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("usd")]
        [TestCase("FOUR")]
        public void InvalidCurrencyCodeIsRejected(string code)
        {
            Assert.That(() => Currency.FromCode(code), Throws.InstanceOf<ArgumentException>());
        }
    }
}
