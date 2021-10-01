using System;
using System.Linq;
using System.Threading.Tasks;

namespace AccountCalculator
{
    public sealed class CurrencyConverter : ICurrencyConverter
    {
        private record ConversionRate(string Start, string End, decimal Rate);

        private readonly Task<ILookup<CurrencyCode, ConversionRate>> _conversionRates;

        public CurrencyConverter(IExchangeRatesProvider exchangeRatesProvider) =>
            _conversionRates = InitializeConversionRates(exchangeRatesProvider);

        private static async Task<ILookup<CurrencyCode, ConversionRate>> InitializeConversionRates(
            IExchangeRatesProvider exchangeRatesProvider) =>
            (await exchangeRatesProvider.GetExchangeRates())
            .OrderBy(x => x.Start)
            .ThenBy(x => x.Currency)
            .ToLookup(x => x.Currency, x => new ConversionRate(x.Start, x.End, x.ConversionRate));

        public decimal ConvertCurrency(
            decimal originalValue,
            CurrencyCode originalCurrency,
            CurrencyCode targetCurrency,
            string timeOfConversion)
        {
            if (originalCurrency == targetCurrency)
            {
                return originalValue; // No conversion required.
            }

            var rateFromOriginalToGbp = GetConversionRate(originalCurrency, timeOfConversion);
            var rateFromTargetToGbp = GetConversionRate(targetCurrency, timeOfConversion);

            var newValue = originalValue * (rateFromTargetToGbp / rateFromOriginalToGbp);
            return newValue;
        }

        private decimal GetConversionRate(CurrencyCode currency, string timeOfConversion)
        {
            if (currency == CurrencyCode.GBP)
            {
                return 1;
            }

            var conversionDate = DateTimeOffset.Parse(timeOfConversion).UtcDateTime.ToString("yyyy/MM/dd");

            var conversionRates = _conversionRates.Result;
            var conversionRate = conversionRates[currency]
                .Where(x => string.CompareOrdinal(x.Start, conversionDate) <= 0)
                .FirstOrDefault(x => string.CompareOrdinal(conversionDate, x.End) <= 0);
            if (conversionRate == null)
            {
                throw new ArgumentException(
                    $"No conversion available for currency {currency} at {timeOfConversion}");
            }

            return conversionRate.Rate;
        }
    }
}
