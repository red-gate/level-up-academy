using System;
using System.IO;
using System.Linq;

namespace Immutability.ExchangeRates
{
    public static class Program
    {
        private static void Main()
        {
            Main(Console.Out);
        }

        public static void Main(TextWriter output)
        {
            var gbp = Currency.FromCode("GBP");
            var usd = Currency.FromCode("USD");
            var eur = Currency.FromCode("EUR");

            var currencyExchange = new CurrencyExchange();
            currencyExchange.UpdateExchangeRate(gbp, usd, 1.36m);
            currencyExchange.UpdateExchangeRate(gbp, eur, 1.18m);
            currencyExchange.UpdateExchangeRate(usd, gbp, 0.73m);
            currencyExchange.UpdateExchangeRate(usd, eur, 0.86m);
            currencyExchange.UpdateExchangeRate(eur, gbp, 0.85m);
            currencyExchange.UpdateExchangeRate(eur, usd, 1.16m);

            output.WriteLine("Current exchange rates:");
            foreach (var exchangeRate in currencyExchange.GetCurrentRates().OrderBy(er => er.From.Code).ThenBy(er => er.To.Code))
            {
                output.WriteLine($"{exchangeRate.From} => {exchangeRate.To} = {exchangeRate.Rate}");
            }
            output.WriteLine();

            var money1 = new Money(gbp, 50.00m);
            currencyExchange.Exchange(money1, usd);
            output.WriteLine($"£50.00 is worth {money1}");
            output.WriteLine();

            currencyExchange.UpdateExchangeRate(gbp, usd, 1.38m);
            currencyExchange.UpdateExchangeRate(gbp, eur, 1.19m);
            currencyExchange.UpdateExchangeRate(usd, gbp, 0.71m);
            currencyExchange.UpdateExchangeRate(usd, eur, 0.86m);
            currencyExchange.UpdateExchangeRate(eur, gbp, 0.84m);
            currencyExchange.UpdateExchangeRate(eur, usd, 1.17m);

            output.WriteLine("New exchange rates:");
            foreach (var exchangeRate in currencyExchange.GetCurrentRates().OrderBy(er => er.From.Code).ThenBy(er => er.To.Code))
            {
                output.WriteLine($"{exchangeRate.From} => {exchangeRate.To} = {exchangeRate.Rate}");
            }
            output.WriteLine();

            var money2 = new Money(gbp, 50.00m);
            currencyExchange.Exchange(money2, usd);
            output.WriteLine($"£50.00 is now worth {money2}");
        }
    }
}
