using System.IO;
using NUnit.Framework;

namespace Immutability.ExchangeRates.Tests
{
    [TestFixture]
    public class GoldenMasterTests
    {
        [Test]
        public void OutputMatchesExpectation()
        {
            const string expected = @"Current exchange rates:
EUR => GBP = 0.85
EUR => USD = 1.16
GBP => EUR = 1.18
GBP => USD = 1.36
USD => EUR = 0.86
USD => GBP = 0.73

£50.00 is worth USD 68.0000

New exchange rates:
EUR => GBP = 0.84
EUR => USD = 1.17
GBP => EUR = 1.19
GBP => USD = 1.38
USD => EUR = 0.86
USD => GBP = 0.71

£50.00 is now worth USD 69.0000
";
            var stringWriter = new StringWriter();
            Program.Main(stringWriter);
            var actual = stringWriter.ToString();
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
