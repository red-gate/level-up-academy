using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AccountCalculator.Domain;

namespace AccountCalculator
{
    public class ExchangeRatesProvider : IExchangeRatesProvider
    {
        /// <summary>
        /// Pattern for matching a single line of CSV data for an exchange rate record.
        /// </summary>
        private static readonly Regex RecordRegex =
            new ("^(?<COUNTRY>.+?),(?<CURRENCY>.+?),(?<CODE>[A-Z]{3}?),(?<RATE>.+?),(?<START>.+?),(?<END>.+?)$");

        /// <summary>
        /// Pattern for matching the specific date format used in an exchange rate record.
        /// </summary>
        private static readonly Regex DateRegex =
            new ("^0?(?<DAY>[0-9]{1,2})/0?(?<MONTH>[0-9]{1,2})/(?<YEAR>[0-9]{4})$");

        private readonly Lazy<Task<string>> _getRawExchangeRates;

        public ExchangeRatesProvider(Func<Task<string>> getRawExchangeRates)
        {
            _getRawExchangeRates = new Lazy<Task<string>>(getRawExchangeRates);
        }

        public async Task<IEnumerable<ExchangeRateRecord>> GetExchangeRates()
        {
            var rawExchangeRates = await _getRawExchangeRates.Value;

            return rawExchangeRates
                .Split(new[] { "\n", "\r\n" }, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                .Select(line => RecordRegex.Match(line))
                .Where(match => match.Success)
                .Where(match => decimal.TryParse(match.Groups["RATE"].Value, out _))
                .Where(match => TryParseDate(match.Groups["START"].Value, out _))
                .Where(match => TryParseDate(match.Groups["END"].Value, out _))
                .Select(match =>
                {
                    var currency = new Currency(match.Groups["CODE"].Value);
                    var conversionRate = decimal.Parse(match.Groups["RATE"].Value);
                    TryParseDate(match.Groups["START"].Value, out var start);
                    TryParseDate(match.Groups["END"].Value, out var end);
                    return new ExchangeRateRecord(currency, conversionRate, start, end);
                })
                .ToList();
        }

        private static bool TryParseDate(string s, out string result)
        {
            var match = DateRegex.Match(s);
            if (match.Success)
            {
                var day = int.Parse(match.Groups["DAY"].Value);
                var month = int.Parse(match.Groups["MONTH"].Value);
                var year = int.Parse(match.Groups["YEAR"].Value);
                result = $"{year}/{month:D2}/{day:D2}";
                return true;
            }

            result = "";
            return false;
        }
    }
}
