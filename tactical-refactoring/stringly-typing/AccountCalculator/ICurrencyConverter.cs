using System;

namespace AccountCalculator
{

    public interface ICurrencyConverter
    {
        decimal ConvertCurrency(
            decimal originalValue,
            string originalCurrency,
            string targetCurrency,
            DateTimeOffset timeOfConversion);
    }
}
