using AccountCalculator.Domain;

namespace AccountCalculator
{

    public interface ICurrencyConverter
    {
        Money ConvertCurrency(
            Money originalAmount,
            Currency targetCurrency,
            string timeOfConversion);
    }
}
