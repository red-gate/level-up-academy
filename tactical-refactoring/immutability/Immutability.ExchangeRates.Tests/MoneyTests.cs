using NUnit.Framework;

namespace Immutability.ExchangeRates.Tests
{
    [TestFixture]
    public class MoneyTests
    {
        [Test]
        public void SameCurrencySameAmount_ComparesEqual()
        {
            var money1 = new Money(Currency.FromCode("GBP"), 23.12m);
            var money2 = new Money(Currency.FromCode("GBP"), 23.12m);
            Assert.That(money1, Is.EqualTo(money2));
        }

        [Test]
        public void SameCurrencyDifferentAmount_ComparesNonEqual()
        {
            var money1 = new Money(Currency.FromCode("GBP"), 23.12m);
            var money2 = new Money(Currency.FromCode("GBP"), 23.13m);
            Assert.That(money1, Is.Not.EqualTo(money2));
        }

        [Test]
        public void DifferentCurrencySameAmount_ComparesNonEqual()
        {
            var money1 = new Money(Currency.FromCode("GBP"), 23.12m);
            var money2 = new Money(Currency.FromCode("USD"), 23.12m);
            Assert.That(money1, Is.Not.EqualTo(money2));
        }

        [Test]
        public void DifferentCurrencyDifferentAmount_ComparesNonEqual()
        {
            var money1 = new Money(Currency.FromCode("GBP"), 23.12m);
            var money2 = new Money(Currency.FromCode("USD"), 23.13m);
            Assert.That(money1, Is.Not.EqualTo(money2));
        }
    }
}
