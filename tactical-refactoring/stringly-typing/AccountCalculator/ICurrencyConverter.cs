using AccountCalculator.Domain;

namespace AccountCalculator
{

    public interface ICurrencyConverter
    {
        decimal ConvertCurrency(
            decimal originalValue,
            Currency originalCurrency,
            Currency targetCurrency,
            string timeOfConversion);
    }
}
