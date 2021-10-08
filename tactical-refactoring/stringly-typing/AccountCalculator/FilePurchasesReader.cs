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
        private readonly string _file;

        public FilePurchasesReader(string file) => _file = file;

        public async Task<IEnumerable<Purchase>> ReadPurchases() =>
            (await ReadRawLines())
            .Select(line => _purchasePattern.Match(line))
            .Where(match => match.Success)
            .Select(match =>
            {
                try
                {
                    var timestamp = DateTimeOffset.Parse(match.Groups["TIMESTAMP"].Value, null,
                        DateTimeStyles.RoundtripKind);
                    var amount = decimal.Parse(match.Groups["AMOUNT"].Value);
                    var description = match.Groups["DESCRIPTION"].Value;
                    var currency = Enum.Parse<Currency>(match.Groups["CURRENCY"].Value);
                    return new Purchase(timestamp, description, new Money(amount, currency));
                }
                catch (Exception ex)
                {
                    throw new ParsePurchaseFailedException($"Failed to parse line {match.Value}", ex);
                }
            })
            .OrderBy(purchase => purchase.Timestamp)
            .ThenBy(purchase => purchase.Description)
            .ToList();

        private static readonly Regex _purchasePattern =
            new ("^(?<TIMESTAMP>.*?),(?<DESCRIPTION>.*?),(?<AMOUNT>.*?),(?<CURRENCY>[A-Z]{3})");

        private async Task<IEnumerable<string>> ReadRawLines()
        {
            using var fileStream = File.Open(_file, FileMode.Open, FileAccess.Read, FileShare.Read);
            using var reader = new StreamReader(fileStream, Encoding.UTF8);
            var content = await reader.ReadToEndAsync();
            return content.Split(
                new[] { "\n", "\r\n" },
                StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        }
    }
}