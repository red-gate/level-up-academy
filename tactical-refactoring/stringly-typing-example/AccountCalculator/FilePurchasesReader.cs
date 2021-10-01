using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AccountCalculator.Domain;

namespace AccountCalculator
{
    public class FilePurchasesReader : IPurchasesReader
    {
        private readonly FileInfo _file;

        public FilePurchasesReader(FileInfo file) => _file = file;

        public async Task<IEnumerable<Purchase>> ReadPurchases() =>
            (await ReadRawLines())
            .Select(line => _purchasePattern.Match(line))
            .Where(match => match.Success)
            .Where(match => TryParseDate(match.Groups["TIMESTAMP"].Value, out _))
            .Where(match => decimal.TryParse(match.Groups["AMOUNT"].Value, out _))
            .Select(match =>
            {
                TryParseDate(match.Groups["TIMESTAMP"].Value, out var timestamp);
                decimal.TryParse(match.Groups["AMOUNT"].Value, out var amount);
                var description = match.Groups["DESCRIPTION"].Value;
                var currency = new Currency(match.Groups["CURRENCY"].Value);
                return new Purchase(timestamp, description, new Money(amount, currency));
            })
            .OrderBy(purchase => purchase.Timestamp)
            .ThenBy(purchase => purchase.Description)
            .ToList();

        private static bool TryParseDate(string s, out DateTimeOffset result)
        {
            var success = DateTimeOffset.TryParse(s, null, DateTimeStyles.RoundtripKind, out result);
            return success;
        }

        private static readonly Regex _purchasePattern =
            new ("^(?<TIMESTAMP>.*?),(?<DESCRIPTION>.*?),(?<AMOUNT>.*?),(?<CURRENCY>[A-Z]{3})");

        private async Task<IEnumerable<string>> ReadRawLines()
        {
            using var fileStream = _file.Open(FileMode.Open, FileAccess.Read, FileShare.Read);
            using var reader = new StreamReader(fileStream, Encoding.UTF8);
            var content = await reader.ReadToEndAsync();
            return content.Split(
                new[] { "\n", "\r\n" },
                StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        }
    }
}