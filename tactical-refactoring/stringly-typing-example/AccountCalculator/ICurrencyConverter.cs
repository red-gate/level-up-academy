using AccountCalculator.Domain;

namespace AccountCalculator
{

    public interface ICurrencyConverter
    {
        Money ConvertCurrency(Money originalMoney, Currency targetCurrency, UtcDateTime timeOfConversion);
    }
}
