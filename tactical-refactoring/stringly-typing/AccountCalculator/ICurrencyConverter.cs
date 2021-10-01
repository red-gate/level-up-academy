namespace AccountCalculator
{

    public interface ICurrencyConverter
    {
        decimal ConvertCurrency(
            decimal originalValue,
            CurrencyCode originalCurrency,
            CurrencyCode targetCurrency,
            string timeOfConversion);
    }
}
