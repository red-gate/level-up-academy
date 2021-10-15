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

            var currencyExchange = new CurrencyExchange()
                .WithExchangeRate(gbp, usd, 1.36m)
                .WithExchangeRate(gbp, eur, 1.18m)
                .WithExchangeRate(usd, gbp, 0.73m)
                .WithExchangeRate(usd, eur, 0.86m)
                .WithExchangeRate(eur, gbp, 0.85m)
                .WithExchangeRate(eur, usd, 1.16m);

            output.WriteLine("Current exchange rates:");
            foreach (var exchangeRate in currencyExchange.GetCurrentRates().OrderBy(er => er.From.Code)
                .ThenBy(er => er.To.Code))
            {
                output.WriteLine($"{exchangeRate.From} => {exchangeRate.To} = {exchangeRate.Rate}");
            }

            output.WriteLine();

            var money1InUsd = currencyExchange.Exchange(new Money(gbp, 50.00m), usd);
            output.WriteLine($"£50.00 is worth {money1InUsd}");
            output.WriteLine();

            var currencyExchange2 = currencyExchange.WithExchangeRate(gbp, usd, 1.38m)
                .WithExchangeRate(gbp, eur, 1.19m)
                .WithExchangeRate(usd, gbp, 0.71m)
                .WithExchangeRate(usd, eur, 0.86m)
                .WithExchangeRate(eur, gbp, 0.84m)
                .WithExchangeRate(eur, usd, 1.17m);

            output.WriteLine("New exchange rates:");
            foreach (var exchangeRate in currencyExchange2.GetCurrentRates().OrderBy(er => er.From.Code)
                .ThenBy(er => er.To.Code))
            {
                output.WriteLine($"{exchangeRate.From} => {exchangeRate.To} = {exchangeRate.Rate}");
            }

            output.WriteLine();

            var money2InUsd = currencyExchange2.Exchange(new Money(gbp, 50.00m), usd);
            output.WriteLine($"£50.00 is now worth {money2InUsd}");
        }
    }
}