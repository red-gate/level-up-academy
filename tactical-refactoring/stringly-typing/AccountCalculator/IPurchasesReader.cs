using System.Collections.Generic;
using System.Threading.Tasks;
using AccountCalculator.Domain;

namespace AccountCalculator
{
    public interface IPurchasesReader
    {
        Task<IEnumerable<Purchase>> ReadPurchases();
    }

    public class FilePurchasesReader : IPurchasesReader
    {
        private readonly FileInfo _file;

        public FilePurchasesReader(FileInfo file) => _file = file;

        public async Task<IEnumerable<Purchase>> ReadPurchases() =>
            (await ReadRawLines())
            .Select(line => _purchasePattern.Match(line))
            .Where(match => match.Success)
            .Where(match => IsValidDateTime(match.Groups["TIMESTAMP"].Value))
            .Where(match => decimal.TryParse(match.Groups["AMOUNT"].Value, out _))
            .Select(match =>
            {
                var timestamp = match.Groups["TIMESTAMP"].Value;
                decimal.TryParse(match.Groups["AMOUNT"].Value, out var amount);
                var description = match.Groups["DESCRIPTION"].Value;
                var currency = match.Groups["CURRENCY"].Value;
                return new Purchase(DateTimeOffset.Parse(timestamp), description, amount, currency);
            })
            .OrderBy(purchase => purchase.Timestamp)
            .ThenBy(purchase => purchase.Description)
            .ToList();

        private static bool IsValidDateTime(string s)
        {
            var success = DateTimeOffset.TryParse(s, null, DateTimeStyles.RoundtripKind, out _);
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
