# Stringly-typing workshop

What is stringly-typed code? It's a play on words, derived from strongly-typed. Stringly-typed code is characterised by a failure to take full advantage of the strong type system that a language provides. In extreme cases, strings may be used to represent integers, floating-point values or dates, when there are other more appropriate types to represent these concept. See https://www.techopedia.com/definition/31876/stringly-typed for a more detailed description *(or Google "stringly typed"!)*.

In less extreme cases, it's still common to use built-in types to represent concepts where the chosen type is not sufficiently constrained to provide guarantees about the data being represented. For example:

- We often use `DateTime` or `DateTimeOffset` to represent a UTC date-time. But the `DateTime` can have a `Kind` other than UTC *(such as Local or Unspecified)* and `DateTimeOffset` isn't guaranteed to have a zero offset. Time offset bugs can therefore creep into software where we assume that a `DateTime` or `DateTimeOffset` represents a UTC date-time, when in fact it does not.
- We often see `string` used instead of `Uri`. If the variable's value is not a valid URI, we wouldn't know that's the case until we try to use the value. Bugs can be found earlier if `Uri` is used, so that an invalid value can be identified closer to its source, when we fail to construct a `Uri` from that value.

# The exercise

The `StringlyTyping.sln` solution contains a console application that takes an input file containing a series of purchases that have been made in 2021. See this [example input](PurchasedItems.csv). Each purchase was made at a specific point in time and in a specific currency. The point of the program is to convert all of the purchases to the same currency, and then add up the cost of each purchase to determine the total cost.

The program taskes two command-line arguments, the path to the input purchases file and the common currency to convert each purchase to. We recommend setting the following string as the program arguments when running the application via your IDE, otherwise you'll get a usage error message.

```
..\..\..\..\PurchasedItems.csv USD
```

You can vary the currency code to calculate the result in different currencies. The supported currencies are USD, GBP, EUR, JPY and CNY.

The point of this exercise is to try to eliminate the use of stringly typing. You might want to consider the following:

- Replace the use of strings to represent dates with `DateTimeOffset` *(this will be simpler that using `DateTime`!)*.
- Introduce a `UtcDateTime` to represent actual UTC date-times. The timestamps of the purchases are genuinely in local times, where `DateTimeOffset` is appropriate. But the timestamps used `ExchangeRateRecord` and `CurrencyConverter` should always be in UTC.
- Replace the use of strings to represent currencies with an actual Currency type. What type is appropriate? An enum? A simple wrapper around the three digit currency codes? Maybe another `record` type? You decide.
- Try to combine the decimal value of an amount of money with a type that combines both the numeric value and the associated currency. e.g. introduce a type that combines two properties, the decimal value and the Currency. By combining these two properties, you can simplify the API of `ICurrencyConverter.ConvertCurrency`. By reducing the number of parameters, and giving them distinct types, you can eliminate a class of bugs where the method is called with the arguments being in the wrong order. i.e. you could change from this:
  ```c#
  decimal ConvertCurrency(decimal originalValue, string originalCurrency, string targetCurrency, string timeOfConversion)
  ```
  to something like this:
  ```c#
  Money ConvertCurrency(Money originalValue, Currency targetCurrency, UtcDateTime timeOfConversion)
  ```
- Find somewhere in the code where the use of a `Uri` could be more appropriate than just a `string`.

When you complete the exercise, try to find examples in your own product's code where introducing stronger types could help improve the clarity of the code and reduce the risk of bugs.
