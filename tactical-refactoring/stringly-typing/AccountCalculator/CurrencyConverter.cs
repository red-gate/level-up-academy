using System;
using System.Linq;
using System.Threading.Tasks;
using AccountCalculator.Domain;

namespace AccountCalculator
{
    public sealed class CurrencyConverter : ICurrencyConverter
    {
        private record ConversionRate(DateTimeOffset Start, DateTimeOffset End, decimal Rate);

        private readonly Task<ILookup<Currency, ConversionRate>> _conversionRates;

        public CurrencyConverter(IExchangeRatesProvider exchangeRatesProvider) =>
            _conversionRates = InitializeConversionRates(exchangeRatesProvider);

        private static async Task<ILookup<Currency, ConversionRate>> InitializeConversionRates(
            IExchangeRatesProvider exchangeRatesProvider) =>
            (await exchangeRatesProvider.GetExchangeRates())
            .OrderBy(x => x.Start)
            .ThenBy(x => x.Currency.Code)
            .ToLookup(x => x.Currency, x => new ConversionRate(x.Start, x.End, x.ConversionRate));

        public Money ConvertCurrency(
            Money originalAmount,
            Currency targetCurrency,
            DateTimeOffset timeOfConversion)
        {
            if (originalAmount.Currency == targetCurrency)
            {
                return originalAmount; // No conversion required.
            }

            var rateFromOriginalToGbp = GetConversionRate(originalAmount.Currency, timeOfConversion);
            var rateFromTargetToGbp = GetConversionRate(targetCurrency, timeOfConversion);

            var newAmount = originalAmount.Amount * (rateFromTargetToGbp / rateFromOriginalToGbp);
            return new Money(targetCurrency, newAmount);
        }

        private decimal GetConversionRate(Currency currency, DateTimeOffset timeOfConversion)
        {
            if (currency == new Currency("GBP"))
            {
                return 1;
            }

            var conversionRates = _conversionRates.Result;
            var conversionRate = conversionRates[currency]
                .Where(x => x.Start <= timeOfConversion)
                .FirstOrDefault(x => timeOfConversion <= x.End);
            if (conversionRate == null)
            {
                throw new ArgumentException(
                    $"No conversion available for currency {currency} at {timeOfConversion}");
            }

            return conversionRate.Rate;
        }
    }
}
