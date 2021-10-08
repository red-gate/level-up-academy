using System;
using AccountCalculator.Domain;

namespace AccountCalculator
{

    public interface ICurrencyConverter
    {
        Money ConvertCurrency(
            Money original,
            Currency targetCurrency,
            DateTimeOffset timeOfConversion);
    }
}
